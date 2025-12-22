using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Chapter_1_Interpolation
{
    internal class Section_1_Linear_Interpolation
    {
        public static void Run()
        {

            {
                // Example data points
                ColVec x = new double[] { 0, 1, 2, 3, 4 }, y = new double[] { 1, 2, 0, 2, 3 };
                // Interpolate at a new point
                double xNew = 2.5, yNew = Interp1(x, y, xNew);
                Console.WriteLine($"Interpolated value at x = {xNew}: y = {yNew}");
            }


            {
                // Hermite Spline Example
                List<double> X = [0]; Random rand = new(); double[] y = null;
                List<RowVec> Y = [y = [Sin(0), BesselJ(0, 0)]], M = [y = [Cos(0), -BesselJ(1, 0)]];
                for (int i = 1; i < 10; i++)
                {
                    X.Add(X.Last() + rand.NextDouble() * 2);
                    Y.Add(y = [Sin(X[i]), BesselJ(0, X[i])]);
                    M.Add(y = [Cos(X[i]), -BesselJ(1, X[i])]);
                }
                Scatter(X, Y);

                List<double> Xsmooth = [X[0]]; List<RowVec> Ysmooth = [Y[0]];
                double dx, dxs = 0.1; int N, j;
                for (int i = 1; i < X.Count; i++)
                {
                    j = i - 1; N = 1; dx = X[i] - X[j];
                    if (dx > dxs)
                    {
                        while (dx > dxs)
                        { dx /= 2; N *= 2; }
                        N--;
                        var xs = Enumerable.Range(1, N).Select(k => X[j] + k * dx).ToList();
                        var ys = HermiteCubicSpline(X[j], X[i], Y[j], Y[i], M[j], M[i], xs);
                        Xsmooth.AddRange(xs); Ysmooth.AddRange(ys);
                    }
                    Xsmooth.Add(X[i]); Ysmooth.Add(Y[i]);
                }
                ColVec xx = Xsmooth; Matrix yy = Ysmooth;
                hold = true;
                Plot(xx, yy);
            }
        }

        public static List<RowVec> HermiteCubicSpline(double x0, double x1,
           RowVec y0, RowVec y1, RowVec m0, RowVec m1, List<double> x)
        {
            static (double, double, double, double) polys(double t)
            {
                double t2 = t * t, t3 = t2 * t;
                return (2 * t3 - 3 * t2 + 1, t3 - 2 * t2 + t,
                        -2 * t3 + 3 * t2, t3 - t2);
            }
            double dx = x1 - x0, t, h00, h10, h01, h11;
            List<RowVec> y = [];
            for (int i = 0; i < x.Count; i++)
            {
                t = (x[i] - x0) / dx;
                (h00, h10, h01, h11) = polys(t);
                y.Add(h00 * y0 + h10 * dx * m0 + h01 * y1 + h11 * dx * m1);
            }
            return y;
        }
    }
}
