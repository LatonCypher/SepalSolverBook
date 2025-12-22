using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SepalSolver.PlotLib;

namespace ConsoleApp1.TrainingFiles.Chapter_1_Interpolation
{
    public class Section_3_Hemite_Spline
    {
        public static void Run()
        {
            {
                // Using hermite interpolation
                ColVec x = Linspace(0, 2*pi, 7), s = Sin(x);
                ColVec xq = Linspace(0, 2*pi), sq = Interp1(x, s, xq);
                Scatter(x, s, "fob", 10); hold = true;
                Plot(xq, sq, "r"); hold = false;
            }

            {
                int j = 1;
                ColVec x = Linspace(0, 2*pi, 7), s = Sin(x), c = Cos(x);
                ColVec xq = Linspace(0, 2*pi), sq = Interp1(x, s, xq);
                List<double> sh = [];
                for (int i = 0; i < xq.Numel; i++)
                {
                    while (xq[i] > x[j]) j++;
                    double x0 = x[j-1], x1 = x[j], y0 = s[j-1], y1 = s[j], m0 = c[j-1], m1 = c[j];
                    double dx = x1 - x0, t = (xq[i] - x0) / dx, t2 = t * t, t3 = t2 * t;
                    double h00 = 2 * t3 - 3 * t2 + 1, h10 = t3 - 2 * t2 + t,
                           h01 = -2 * t3 + 3 * t2, h11 = t3 - t2;
                    sh.Add(h00 * y0 + h10 * dx * m0 + h01 * y1 + h11 * dx * m1);
                }
                Scatter(x, s, "fob", 10); hold = true;
                Plot(xq, sh, "r", 3);
                Plot(xq, Sin(xq), "--k", 2); hold = false;
            }

            string path = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverTraining\TrainingFiles\Chapter_03_Interpolation\";
            {
                double[] x = [1, 2, 3], y = [2, 4, 3];
                ColVec xc = x, yc = y;
                double xq = 2.5;
                double yq = Interp1(xc, yc, xq);
                Console.WriteLine(yq);
            }

            {
                double[] x = [1, 2, 3], y = [2, 4, -2];
                ColVec xc = x, yc = y;
                ColVec xq = Linspace(1, 3, 20);
                ColVec yq = Interp1(xc, yc, xq);
                Scatter(xc, yc, "for", 20); hold = true;
                Scatter(xq, yq, "fob"); hold  = false;
            }


            {
                double[] x = [2, 3, 4, 5], y = [3, 4, 5, 6, 7];
                double[,] z =  new double[,]{
                                               {-5,     0,     7,    16},
                                               {-12,   -7,     0,     9},
                                               {-21,  -16,    -9,     0},
                                               {-32,  -27,   -20,   -11},
                                               {-45,  -40,   -33,   -24}
                                             };

                RowVec xr = x; ColVec yc = y; Matrix zm = z;
                double xq = 3.5, yq = 5.5;
                double zq = Interp2(yc, xr, zm, yq, xq);
                Console.WriteLine(zq);
            }

            {

                double[] Pressure = [0.01, 0.02, 0.03, 0.04, 0.05, 0.06],
                    Temperature = [1000, 1100, 1200, 1300, 1400, 1500, 1600, 1800, 2000];
                double[,] SpecificVolume = new double[,]{
                                   {58.758, 29.379, 19.586, 14.689, 11.751, 9.7927},
                                   {63.373, 31.686, 21.124, 15.843, 12.674, 10.562},
                                   {67.988, 33.994, 22.663, 16.997, 13.598, 11.331},
                                   {72.604, 36.302, 24.201, 18.151, 14.521, 12.101},
                                   {77.219, 38.61,  25.74,  19.305, 15.444, 12.87},
                                   {81.834, 40.917, 27.278, 20.459, 16.367, 13.639},
                                   {86.45,  43.225, 28.817, 21.613, 17.29,  14.409},
                                   {95.68,  47.84,  31.894, 23.92,  19.136, 15.947},
                                   {104.91, 52.455, 34.97,  26.228, 20.982, 17.485}};

                RowVec xr = Pressure; ColVec yc = Temperature; Matrix zm = SpecificVolume;
                double T = 1350, P = 0.0373;
                double xq = P, yq = T;
                double zq = Interp2(yc, xr, zm, yq, xq);
                Console.WriteLine($"Specific Volume of superheated water at T = {T}, P = {P} is {zq}");
            }

            {

                double[] x = [1, 2, 3], y = [2, 4, -2];
                ColVec xc = x, yc = y;
                double[] coeffs = Polyfit(x, y, 2);//degree2
                ColVec xq = Linspace(1, 3, 20);
                ColVec yq = xq.Select(x => Polyval(coeffs, x)).ToList();
                Scatter(xc, yc, "for", 20); hold = true;
                Scatter(xq, yq, "fob"); hold  = false;
            }

            {
                // Using hermite interpolation
                ColVec x = Linspace(0, 2*pi, 7), s = Sin(x);
                ColVec xq = Linspace(0, 2*pi), sq = Interp1(x, s, xq);
                Scatter(x, s, "fob"); hold = true;
                Plot(xq, sq, "r");
            }

            {
                ColVec t = Linspace(0, 1), t2 = t.Pow(2), t3 = t.Pow(3);
                ColVec h00 = 2 * t3 - 3 * t2 + 1, h10 = t3 - 2 * t2 + t,
                       h01 = -2 * t3 + 3 * t2, h11 = t3 - t2;

                Plot(t, h00, "r", 3); hold = true;
                Plot(t, h10, "g", 3);
                Plot(t, h01, "b", 3);
                Plot(t, h11, "k", 3);
                Legend(["h00", "h10", "h01", "h11"], Location.MiddleLeft);
                SaveAs(path + "hermite_modes.png");


                //h00 * y0 + h10 * dx * m0 + h01 * y1 + h11 * dx * m1;
            }
        }
    }
}
