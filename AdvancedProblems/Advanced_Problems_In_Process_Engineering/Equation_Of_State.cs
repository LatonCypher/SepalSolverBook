using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Process_Engineering
{
    public static class Equation_Of_State
    {
        public static void Run()
        {
            int nC = 4;
            string[] Component = ["C4", "C5", "C6", "C7"];
            double[] zi = [0.25, 0.25, 0.25, 0.25],
                     Tc = [425.2, 469.6, 507.6, 540.2],
                     Pc = [37.96, 33.70, 30.25, 27.36],
                     w  = [0.200, 0.251, 0.300, 0.349];
            double[,] k =
            {
                    { 0.000,  0.010,  0.015,  0.020 },
                    { 0.010,  0.000,  0.010,  0.015 },
                    { 0.015,  0.010,  0.000,  0.010 },
                    { 0.020,  0.015,  0.010,  0.000 }
            };

            //Step 1 — Initial K-value estimate (Wilson)
            double T = 400; // K
            double P = 5; // bar
            double[] K = [..Component.Select((comp, i) => 
                            (Pc[i] / P) * Exp(5.373 * (1 + w[i]) * (1 - Tc[i] / T)))];

            double error = 1.0;
            while (error > 1e-6)
            {
                // Step 2: Estimate vapor fraction (Rachford-Rice)
                double f(double V) =>
                    zi.Zip(K, (zi, Ki) => zi * (Ki - 1) / (1 + V * (Ki - 1))).Sum();
                double V;
                if(f(0) < 0) V = 0; // only Liquid
                else if(f(1) > 0) V = 1; // only Gas
                else V = Fzero(f, [0, 1]); // 2 Phase

                // Step 3: Calculate liquid and vapor compositions
                double[] xi = [.. zi.Zip(K, (zi, Ki) => zi / (1 + V * (Ki - 1)))];
                double[] yi = [.. K.Zip(xi, (Ki, xi) => Ki * xi)];

                // Step 4: Peng-Robinson EOS parameters
                double R = 8.3144626e-5; // bar m^3/(mol·K)
                double[] m = [.. w.Select(wi => 0.37464 + 1.54226*wi​ - 0.26992*Sqr(wi))];
                double[] alpha = [.. m.Zip(Tc, (mi, Tci) => Sqr(1 + mi*(1 - Sqrt(T/Tci))))];
                double[] ai = [.. Component.Select((comp, i) => 0.45724 * R * R * Tc[i] * Tc[i] / Pc[i] * alpha[i])];
                double[] bi = [.. Component.Select((comp, i) => 0.07780 * R * Tc[i] / Pc[i])];

                //Step 5 — Mixture a and b for Liq & Vap
                (double af, double bf, double[] a2) mix_rule(double[] x, double[] a, double[] b)
                {
                    double[,] aij = new double[nC, nC];
                    for (int i = 0; i < nC; i++)
                        for (int j = 0; j < nC; j++)
                            aij[i, j] = Sqrt(ai[i] * ai[j])*(1 - k[i, j]);
                    Matrix Aij = aij;
                    ColVec c = (ColVec)x;
                    double af = c.T * (Aij * c);
                    double bf = c.T*b;
                    double[] a2f = [.. Aij * c];
                    return (af, bf, a2f);
                }

                (double aL, double bL, double[] a2L) = mix_rule(xi, ai, bi);
                (double aV, double bV, double[] a2V) = mix_rule(yi, ai, bi);

                double AL = aL*P/(R*R*T*T), AV = aV*P/(R*R*T*T);
                double BL = bL*P/(R*T), BV = bV*P/(R*T);

                // Liquid
                var CoeffsL = PengRobinsion(AL, BL); var rawL = SolveCubic(CoeffsL);
                double[] rootsL = [.. rawL.Where(r => r.Imag == 0).Select(r => r.Real)];
                double ZL = rootsL.Min();
                // Vapour
                var CoeffsV = PengRobinsion(AV, BV); var rawV = SolveCubic(CoeffsV);
                double[] rootsV = [.. rawV.Where(r => r.Imag == 0).Select(r => r.Real)];
                double ZV = rootsV.Max();

                double[] FugL = Fugacity(bi, aL, bL, a2L, AL, BL, ZL);
                double[] FugV = Fugacity(bi, aV, bV, a2V, AV, BV, ZV);

                double[] Knew = [.. FugL.Zip(FugV, (fl, fv) => fl/fv)];

                error = Knew.Zip(K, (kn, k) => Sqr(kn - k)).Sum();

                K = Knew;
            }
        }
        static double[] PengRobinsion(double A, double B) => [1, B-1, A - B*(2 + 3*B), -B*(A - B*(1 + B))];
        static Complex[] SolveCubic(double[] Coeffs)
        {
            double a = Coeffs[0], b = Coeffs[1], c = Coeffs[2], d = Coeffs[3];
            if (Abs(a) < 1e-14)
                throw new ArgumentException("Coefficient a must not be zero.");

            // Normalize to monic form x^3 + A x^2 + B x + C = 0
            double A = b / a, B = c / a, C = d / a;

            // Depressed cubic substitution: x = y - A/3
            double p = B - A * A / 3.0;
            double q = 2.0 * A * A * A / 27.0 - A * B / 3.0 + C;

            double Δ = Pow(q / 2.0, 2) + Pow(p / 3.0, 3);

            Complex sqrtΔ = Complex.Sqrt(Δ);

            Complex u = Complex.Pow(-q / 2.0 + sqrtΔ, 1.0/3);
            Complex v = Complex.Pow(-q / 2.0 - sqrtΔ, 1.0/3);

            // Three cube roots of unity
            Complex omega = new(-0.5, Sqrt(3) / 2.0),
                    omega2 = new(-0.5, -Sqrt(3) / 2.0);

            Complex[] roots = [u + v - A / 3.0,
                               u * omega  + v * omega2 - A / 3.0,
                               u * omega2 + v * omega  - A / 3.0];

            return roots;
        }

        static double[] Fugacity(double[] b, double af, double bf, double[] a2, double Af, double Bf, double Zf)
        {
            double Sqrt2 = Sqrt(2), c0 = 2*Sqrt2, c1 = 1 + Sqrt2, c2 = 1 - Sqrt2, C = Log(Zf - Bf), 
                D = Af/(c0*Bf), E = Log((Zf + c1 * Bf)/(Zf + c2*Bf));
            return [.. b.Zip(a2, (bi, a2i) => Exp(bi/bf * (Zf - 1) - C - D * (2 * a2i/af - bi/bf) * E))];
        }
    }
}
