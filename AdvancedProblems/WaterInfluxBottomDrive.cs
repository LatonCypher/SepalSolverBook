

namespace ConsoleApp1.TrainingFiles
{
    public static class WaterInfluxCalculators
    {
        private const double PI = pi;
        private const double PI_SQ = PI * PI;

        public static double BottomInfiniteActingRadial_Wd(double tD, double hD)
        {
            if (tD <= 0) return 0;

            // Lambda closure without inline allocation
            return NiLaplace(s => EvaluateLaplaceWd(s, hD), tD);
        }

        private static double EvaluateLaplaceWd(double s, double hD)
        {
            double s3 = s * s * s;

            // Single integration call with optimized integrand
            double integralVal = AdaptiveGaussKronrod(x => Integrand(x, s, hD), 0, 100);

            return 1.0 / (2.0 * s3 * hD * integralVal);
        }

        private static double Integrand(double x, double s, double hD)
        {
            if (x < 1e-8) return 0.25 / s; // Limit as x -> 0 for J1(x)/x = 0.5

            double x2 = x * x;
            double j1x = BesselJ(1,x);
            double fac = (j1x / x) * (j1x / x);

            double hD_x = hD * x;
            double fun = 1/Tanh(hD_x) / s - 1.0 / (hD_x * (s + x2));

            // Vectorized/unrolled iteration for high-frequency decay
            double sum = 0.0;
            double inv_hD = 1.0 / hD;

            for (int m = 1; m <= 50; m++)
            {
                double am2 = (m * m * PI_SQ) * (inv_hD * inv_hD);
                double term = 2.0 * x * inv_hD / ((x2 + am2) * (s + x2 + am2));
                sum += term;
                if (term < 1e-7 * Abs(fun - sum)) break; // Correct relative convergence check
            }

            return fac * (fun - sum);
        }

        // Pre-computed Nodes and Weights for G7 / K15 on [-1, 1]
        private static readonly double[] Xk = {
        0.0000000000000000, 0.2077849550078985, 0.4058451513773972,
        0.5860872354676911, 0.7415311855993944, 0.8648644233597691,
        0.9491079123427585, 0.9914553711208126
    };

        private static readonly double[] Wk = {
        0.2094821410847288, 0.2044329400752989, 0.1903505780647854,
        0.1690047266392671, 0.1406532597155250, 0.1047900103222502,
        0.0630920926299786, 0.0229353220105292
    };

        private static readonly double[] Wg = {
        0.4179591836734694, 0.3818300505051189,
        0.2797053914892767, 0.1294849661688697
    };

        public static double AdaptiveGaussKronrod(Func<double, double> f, double a, double b, double epsAbs = 1e-8, double epsRel = 1e-8, int maxSubdivisions = 100)
        {
            var pq = new PriorityQueue<Interval, double>(Comparer<double>.Create((x, y) => y.CompareTo(x)));

            var first = EvaluateInterval(f, a, b);
            pq.Enqueue(first, first.Error);

            double totalResult = first.Result;
            double totalError = first.Error;

            int subdivisions = 0;

            while (pq.Count > 0 && subdivisions < maxSubdivisions)
            {
                if (totalError <= epsAbs || totalError <= epsRel * Abs(totalResult))
                    break;

                var current = pq.Dequeue();
                double mid = (current.A + current.B) / 2.0;

                var left = EvaluateInterval(f, current.A, mid);
                var right = EvaluateInterval(f, mid, current.B);

                totalResult += (left.Result + right.Result) - current.Result;
                totalError += (left.Error + right.Error) - current.Error;

                pq.Enqueue(left, left.Error);
                pq.Enqueue(right, right.Error);

                subdivisions++;
            }

            return totalResult;
        }

        private static Interval EvaluateInterval(Func<double, double> f, double a, double b)
        {
            double center = 0.5 * (a + b);
            double halfWidth = 0.5 * (b - a);

            double fCenter = f(center);
            double gkResult = Wk[0] * fCenter;
            double gResult = Wg[0] * fCenter;

            for (int i = 1; i < 8; i++)
            {
                double xShift = halfWidth * Xk[i];
                double fSum = f(center - xShift) + f(center + xShift);

                gkResult += Wk[i] * fSum;
                if (i % 2 == 0) // Even Kronrod indices correspond to Gauss nodes
                {
                    gResult += Wg[i / 2] * fSum;
                }
            }

            gkResult *= halfWidth;
            gResult *= halfWidth;

            double err = Abs(gkResult - gResult);

            return new Interval { A = a, B = b, Result = gkResult, Error = err };
        }

        private class Interval
        {
            public double A, B, Result, Error;
        }
    }
}
