using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Petroleum_Engineering
{
    internal class Section_01_GasCompressibility
    {
        public static void Run()
        {
            /// <BookContent>
            /// <code>
            {
                //Z factor application
                static double ZfactorHY(double Pr, double Tr)
                {
                    double z = 1, t, tm1, tm1e2, A, B,
                        C, D, r, y2, y3, y4, Den;
                    if (Pr != 0)
                    {
                        t = 1 / Tr;
                        tm1 = 1 - t; tm1e2 = tm1 * tm1;
                        A = 0.06125 * t * Exp(-1.2 * Pow(1 - t, 2));
                        B = t * (14.76 - t * (9.76 - t * 4.58));
                        C = t * (90.7 - t * (242.2 - t * 42.4));
                        D = 2.18 + 2.82 * t; r = A * Pr;
                        var yfunc = new Func<double, double>(y =>
                        {
                            y2 = y * y; y3 = y2 * y; y4 = y3 * y;
                            Den = Pow(1 - y, 3);
                            return -A * Pr + (y + y2 + y3 - y4) / Den -
                            B * y2 + C * Pow(y, D);
                        });
                        r *= Pr < 5 ? 2 : 1;
                        r /= Pr > 13 ? 2 : 1;
                        double y = Fsolve(yfunc, r);
                        z = A * Pr / y;
                    }
                    return z;
                }


                // set up ressure and temperature mesh
                ColVec Pr = Linspace(0, 15, 501);
                double[] Tr = [1.05,    1.10,   1.15,   1.20,   1.25,   1.30,   1.35,
                               1.40,    1.45,   1.50,   1.60,   1.70,   1.80,   1.90,
                               2.00,    2.20,   2.40,   2.60,   2.80,   3.00];

                // compute z factors and plot them
                List<string> Tlabels = [.. Tr.Select(tr => "Tr = " + tr)];
                Matrix ZHY = Meshfun(ZfactorHY, Pr, Tr);

                // Plot result.
                Plot(Pr, ZHY);
                Legend(Tr.Select(tr => "Tr = " + tr), UpperRight);
                SaveAs("Zfactor_Hall_Yarborough_.png");

                // Literature style plot
                Figure(640, 880);
                var z1 = Plot(Pr[Pr <= 8], ZHY[Pr <= 8, ..], "k"); HoldOn();
                var z2 = Plot(Pr[Pr >= 7], ZHY[Pr >= 7, ..], "k"); HoldOff();
                SetAxis(z1, X_Axis.Top, Y_Axis.Left, 0, 8, 0, 1.1);
                SetAxis(z2, X_Axis.Bottom, Y_Axis.Right, 7, 15, 0.9, 2.0);
                SaveAs("Hall_Yarborough_Chart.png");
                CloseFig();
            }
            /// </code>
            /// 
            /// <code>
            {
                static double ZfactorDAK(double Ppr, double Tpr)
                {
                    double z = 1;
                    if (Ppr != 0)
                    {
                        double Tpr2 = Tpr * Tpr, Tpr3 = Tpr2 * Tpr,
                            Tpr4 = Tpr3 * Tpr, Tpr5 = Tpr4 * Tpr,
                        A1 = 0.3265, A2 = -1.0700, A3 = -0.5339,
                        A4 = 0.01569, A5 = -0.05165, A6 = 0.5475,
                        A7 = -0.7361, A8 = 0.1844, A9 = 0.1056,
                        A10 = 0.6134, A11 = 0.7210,
                        R1 = A1 + A2 / Tpr + A3 / Tpr3 + A4 / Tpr4 + A5 / Tpr5,
                        R2 = 0.27 * Ppr / Tpr,
                        R3 = A6 + A7 / Tpr + A8 / Tpr2,
                        R4 = A9 * (A7 / Tpr + A8 / Tpr2),
                        R5 = A10 / Tpr3;
                        double yfunc(double y)
                        {
                            double y2 = y * y, y5 = y2 * y2 * y;
                            double E = (1 + A11 * y2) * Exp(-A11 * y2);
                            return R5 * y2 * E + R1 * y - R2 / y + R3 * y2 - R4 * y5 + 1;
                        }
                        ;
                        var options = SolverSet(StepFactor: 0.5);
                        double y = Fsolve(yfunc, R2, options);
                        z = R2 / y;
                    }
                    return z;
                }

                // set up ressure and temperature mesh
                ColVec Pr = Linspace(0, 15, 501);
                double[] Tr = [1.05,    1.10,   1.15,   1.20,   1.25,   1.30,   1.35,
                               1.40,    1.45,   1.50,   1.60,   1.70,   1.80,   1.90,
                               2.00,    2.20,   2.40,   2.60,   2.80,   3.00];

                // compute z factors and plot them
                List<string> Tlabels = [.. Tr.Select(tr => "Tr = " + tr)];
                Matrix ZDAK = Meshfun(ZfactorDAK, Pr, Tr);

                // Plot result.
                Plot(Pr, ZDAK);
                Legend(Tr.Select(tr => "Tr = " + tr), UpperRight);
                SaveAs("Zfactor_Dranchuk_Abou_Kassem.png");

                // Literature style plot
                Figure(640, 880);
                var z1 = Plot(Pr[Pr <= 8], ZDAK[Pr <= 8, ..], "k"); HoldOn();
                var z2 = Plot(Pr[Pr >= 7], ZDAK[Pr >= 7, ..], "k"); HoldOff();
                SetAxis(z1, X_Axis.Top, Y_Axis.Left, 0, 8, 0, 1.1);
                SetAxis(z2, X_Axis.Bottom, Y_Axis.Right, 7, 15, 0.9, 2.0);
                SaveAs("Dranchuk_Abou_Kassem_Chart.png");
                CloseFig();
            }
            /// </code>
            /// 
            /// <code>
            {
                // Constants from Table 3 for Kareem et al. (2016) Correlation 
                const double a1 = 0.317842, a2 = 0.382216, a3 = -7.768354, a4 = 14.290531;
                const double a5 = 0.000002, a6 = -0.004693, a7 = 0.096254, a8 = 0.166720;
                const double a9 = 0.966910, a10 = 0.063069, a11 = -1.966847, a12 = 21.0581;
                const double a13 = -27.0246, a14 = 16.23, a15 = 207.783, a16 = -488.161;
                const double a17 = 176.29, a18 = 1.88453, a19 = 3.05921;
                double[] poly1 = [a6, a7, a8, a9];
                double[] poly2 = [a14, a13, a12];
                double[] poly3 = [a17, a16, a15];
                double[] poly4 = [0.01853, -0.8725, 3.182, -0.0523];
                double _kareem_z(double Ppr, double Tpr)
                {
                    double t = 1.0 / Tpr, dt = 1.0 - t, dt2 = dt * dt, tPpr = t * Ppr;

                    // Intermediate variables (A through G)
                    double A = a1 * t * Exp(a2 * dt2) * Ppr,
                        B = a3 * t + a4 * t * t + a5 * Pow(t, 6) * Pow(Ppr, 6),
                        C = Polyval(poly1, tPpr), D = a10 * t * Exp(a11 * dt2),
                        E = t * Polyval(poly2, t), F = t * Polyval(poly3, t), G = a18 + a19 * t;

                    // Equation 15: Reduced density y
                    double A2 = A * A, C2 = C * C, C3 = C2 * C,
                        denom_y = (1.0 + A2) / C - (A2 * B) / C3,
                        y = (D * Ppr) / denom_y;

                    // Equation 14: Compressibility factor z
                    double y2 = y * y, y3 = y2 * y, num_z = D * Ppr * (1.0 + y + y2 - y3);
                    double denom_z = (D * Ppr + E * y2 - F * Pow(y, G)) * Pow(1.0 - y, 3);

                    return num_z / denom_z;
                }

                // set up ressure and temperature mesh
                ColVec Pr = Linspace(0, 15, 501);
                double[] Tr = [1.05,    1.10,   1.15,   1.20,   1.25,   1.30,   1.35,
                               1.40,    1.45,   1.50,   1.60,   1.70,   1.80,   1.90,
                               2.00,    2.20,   2.40,   2.60,   2.80,   3.00];

                // compute z factors and plot them
                List<string> Tlabels = [.. Tr.Select(tr => "Tr = " + tr)];
                Matrix ZDAK = Meshfun(_kareem_z, Pr, Tr);

                // Plot result.
                Plot(Pr, ZDAK);
                Legend(Tr.Select(tr => "Tr = " + tr), UpperRight);
                SaveAs("Zfactor_Kareem.png");

                // Literature style plot
                Figure(640, 880);
                var z1 = Plot(Pr[Pr <= 8], ZDAK[Pr <= 8, ..], "k"); HoldOn();
                var z2 = Plot(Pr[Pr >= 7], ZDAK[Pr >= 7, ..], "k"); HoldOff();
                SetAxis(z1, X_Axis.Top, Y_Axis.Left, 0, 8, 0, 1.1);
                SetAxis(z2, X_Axis.Bottom, Y_Axis.Right, 7, 15, 0.9, 2.0);
                SaveAs("Kareem_ZFactor_Chart.png");
                CloseFig();
            }
            /// </code>
            /// <code>
            /// </code>
            /// </BookContent>
        }
    }
}
