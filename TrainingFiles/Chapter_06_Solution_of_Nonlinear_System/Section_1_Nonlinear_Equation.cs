using CSharpMath.Atom.Atoms;
using ScottPlot.PlotStyles;

namespace ConsoleApp1.TrainingFiles.Chapter_06_Solution_of_Nonlinear_System
{
    internal class Section_1_Nonlinear_Equation
    {

        public static void Run()
        {
            /// <BookContent>
            /// Root of a nonlinear equation near initial guess :math:`x_0` can be found using ``Fzero`` or `Fsolve`. It numerically locates a value: :math:`x` such that: :math:`f(x) = 0`. This is particularly useful when analytical solutions are difficult or impossible to obtain.
            /// 
            /// To solve the equation: :math:`x\exp(x) = 2`, start with initial guess of :math:`x_0 = 0.5`;
            /// <code>
            {
                //Single nonlinear equation
                double f(double x) => x * Exp(x) - 2;

                // solve equation using fzero
                double x = Fzero(f, 0.5);
                Console.WriteLine($"x = {x}");
            }
            /// </code>
            /// In this case, Fzero first search for an interval that brackets the root. Then uses brent's method to hone in on the root. 
            /// If we are sure of the interval containing the root, we can save the effort spent on bracketing the root by supplying that. 
            /// <code>
            {
                //Single nonlinear equation (bracketted)
                double f(double x) => x * Exp(x) - 2;
                double x = Fzero(f, [0.5, 1]);
                Console.WriteLine($"x = {x}");
            }
            /// </code>
            /// 
            /// To have window into the solution process, we can using solver setting `SolverSet()` to get the solver to print out the result after each iteration. 
            /// 
            /// <code>
            {
                // Single nonlinear equation
                double f(double x) => x * Exp(x) - 2;

                // set solver behaviour
                var opts = SolverSet(Display: true);

                // solve equation using fzero
                double x = Fzero(f, 0.5, opts);
                Console.WriteLine($"x = {x}");
            }
            /// </code>
            /// 
            /// by setting the solver setting in the case of bracketed root, we can see how the solution process differs from the case of a single initial guess. 
            /// 
            /// <code>
            {
                // Single nonlinear equation
                double f(double x) => x * Exp(x) - 2;

                // set solver behaviour
                var opts = SolverSet(Display: true);

                // solve equation using fzero
                double x = Fzero(f, [0.5, 1], opts);
                Console.WriteLine($"x = {x}");
            }
            /// </code>
            /// 
            /// <header 2> Practical Application </header>
            /// The gas compressibility factor (Z-factor) measures how much a real gas deviates from ideal gas behavior. It is defined as:
            /// <math>
            ///  Z = \frac{P V}{n R T}
            ///  </math>
            ///  
            /// where:
            /// 
            /// - :math:`P` = pressure
            /// - :math:`V` = volume
            /// - :math:`n` = number of moles
            /// - :math:`R` = gas constant
            /// - :math:`T` = temperature
            /// 
            /// Accurate determination of :math:`Z` is essential in petroleum engineering for reservoir simulation, material balance, and pipeline design.
            /// Unlike explicit correlations, which provide :math:`Z` directly as a function of pseudo-reduced pressure (:math:`P_{pr}`) and pseudo-reduced temperature (:math:`T_{pr}`), **implicit correlations** require solving an equation iteratively because :math:`Z` appears on both sides of the equation.
            /// 
            /// The **Hall–Yarbrough correlation (1973)** is one of the most widely used implicit methods for estimating Z. It was developed based on the hard-sphere equation of state and tested against multiple reservoir gas systems.
            /// The general form is:
            /// <math>
            /// \begin{array}{c}
            ///     A = 0.06125t \exp\left(-1.2(1 - t)^2\right) \\
            ///     B = 14.76t - 9.76t^2 + 4.58t^3 \\
            ///     C = 90.7t - 242.2t^2 + 42.4t^3 \\
            ///     D = 2.18 + 2.82t \\
            ///     -AP_{pr} + \cfrac{y + y^2 + y^3 - y^4}{(1 - y)^3} - By^2 + Cy^D = 0 \\
            ///     Z = \cfrac{A P_{pr}}{y}
            /// \end{array}
            /// </math>
            /// where:
            /// 
            /// - :math:`P_{pr} = P/P_c` (pseudo-reduced pressure)
            /// - :math:`T_{pr} = T/T_c` (pseudo-reduced temperature)
            /// - :math:`t = 1/T_{pr}` 
            /// - :math:`P_c, T_c` = pseudo-critical properties of the gas mixture
            /// 
            /// Because reduced density equation is nonlinear, iterative numerical methods such as Newton–Raphson or successive substitution are required to solve it.
            /// 
            /// **Applications**
            /// 
            /// - Reservoir engineering: material balance calculations and reserves estimation.
            /// - Pipeline design: predicting pressure drop and flow efficiency.
            /// - Simulation software: incorporated into PVT packages for automated Z-factor evaluation.
            /// 
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
                Matrix ZHY = Meshfun((p, t) => ZfactorHY(p, t), Pr, Tr);

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
                Matrix ZDAK = Meshfun((p, t) => ZfactorDAK(p, t), Pr, Tr);

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
                static double ZfactorDPR(double Pr, double Tr)
                {
                    double z = 1;
                    if (Pr != 0)
                    {
                        double Tr2 = Tr * Tr, Tr3 = Tr2 * Tr, E,
                            A1 = 0.31506237, A2 = -1.04670990, A3 = -0.57832720, A4 = 0.53530771,
                            A5 = -0.61232032, A6 = -0.10488813, A7 = 0.68157001, A8 = 0.68446549,
                            T1 = A1 + A2 / Tr + A3 / Tr3, T2 = A4 + A5 / Tr, T3 = A5 * A6 / Tr,
                            T4 = A7 / Tr3, T5 = 0.27 * Pr / Tr, y2, y5, v = T5;
                        var yfunc = new Func<double, double>(y =>
                        {
                            y2 = y * y; y5 = y2 * y2 * y; E = (1 + A8 * y2) * Exp(-A8 * y2);
                            return 1 + T1 * y + T2 * y2 + T3 * y5 + T4 * y2 * E - T5 / y;
                        });
                        var options = SolverSet(StepFactor: 0.5);
                        double y = Fsolve(yfunc, v, options);
                        z = 0.27 * Pr / (Tr * y);
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
                Matrix ZDPR = Meshfun((p, t) => ZfactorDPR(p, t), Pr, Tr);

                // Plot result.
                Plot(Pr, ZDPR);
                Legend(Tr.Select(tr => "Tr = " + tr), UpperRight);
                SaveAs("Zfactor_Dranchuk_Purvis_Robinson.png");

                // Literature style plot
                Figure(640, 880);
                var z1 = Plot(Pr[Pr <= 8], ZDPR[Pr <= 8, ..], "k"); HoldOn();
                var z2 = Plot(Pr[Pr >= 7], ZDPR[Pr >= 7, ..], "k"); HoldOff();
                SetAxis(z1, X_Axis.Top, Y_Axis.Left, 0, 8, 0, 1.1);
                SetAxis(z2, X_Axis.Bottom, Y_Axis.Right, 7, 15, 0.9, 2.0);
                SaveAs("Dranchuk_Purvis_Robinson_Chart.png");
                CloseFig();
            }
            /// </code>
            /// </BookContent>
        }
    }
}

