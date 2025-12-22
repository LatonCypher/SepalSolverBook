using SepalSolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Chapter_5_Solution_of_Nonlinear_System
{
    internal class Section_1_Nonlinear_Equation
    {

        public static void Run()
        {
            //Fzero
            {
                //Single nonlinear equation
                Func<double, double> f = x => x * Exp(x) - 2;
                double x = Fzero(f, 0.5);
            }

            {
                //Single nonlinear equation (bracketted
                Func<double, double> f = x => x * Exp(x) - 2;
                double x0 = 0.5;
                double x = Fzero(f, [0.5, 1]);
            }

            //Fsolve
            {
                //Single nonlinear equation
                Func<double, double> f = x => x * Exp(x) - 2;
                double x = Fsolve(f, 0.5);
            }

            {
                //System nonlinear equations
                Func<ColVec, ColVec> f = x => new double[] { x[0] * Exp(x[1]) - 2, x[1] * Exp(x[0]) - 2 };
                double[] x0 = [0.5, 0.5];
                ColVec x = Fsolve(f, x0);
            }

            {
                // Parameterized nonlinear equations
                double[] paramfun(ColVec x, double c)
                {
                    return [ 2*x[0] + x[1]  - Exp(-c*x[0]),
                    -x[0] + 2*x[1]  - Exp(-c*x[1])];
                }
                RowVec C = Linspace(0, 20, 200);
                ColVec x = new double[] { 0.2, 0.6 };
                var opts = SolverSet(MaxIter: 1000);
                Matrix X = C.Select(c => x = Fsolve(x => paramfun(x, c), x, opts)).ToList();
                Plot(C, X, Linewidth: 2);
            }

            {
                //Large Nonlinear Systems
                int n = 100000;
                Indexer i = new(0, n), j = new(0, n - 1), jp1 = j + 1,
                    l = new(1, n - 1), lp1 = l + 1, lm1 = l - 1;

                ColVec a = Ones(n-1), b = Ones(n), e = -a,
                    c = 2 * e, d, xstart, F = new double[n];

                SparseMatrix C, D, E;

                ColVec nlsf(ColVec x)
                {
                    F[l] = (3 - 2 * x[l]).Times(x[l]) - x[lm1] - 2 * x[lp1] + 1;
                    F[n - 1] = (3 - 2 * x[n - 1]) * x[n - 1] - x[n - 2] + 1;
                    F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
                    return F;
                }

                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    d = -4 * x + 3 * b;
                    D = new(i, i, d, n, n);
                    C = new(j, jp1, c, n, n);
                    E = new(jp1, j, e, n, n);
                    return C + D + E;
                };

                var opts = SolverSet(Display: true);
                opts.UserDefinedJacobian = Jac;
                xstart = -b;

                var result = Fsolve(nlsf, xstart, opts);
            }

            {
                // Large Nonlinear systems
                int n = 10000; 
                Indexer odds = new(0, 2, n), evens = odds + 1;
                ColVec xstart = new double[n], One = Ones(n / 2), 
                    c = -One, d = 10*One, e, F;
                SparseMatrix C, D, E;

                ColVec multirosenbrook(ColVec x)
                {
                    // Evaluate the vector function
                    F = new double[n];
                    F[odds] = 1 - x[odds];
                    F[evens] = 10 * (x[evens] - x[odds].Pow(2));
                    return F;
                }

                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    C = new(odds, odds, c, n, n);
                    D = new(evens, evens, d, n, n);
                    e = -20 * x[odds];
                    E = new(evens, odds, e, n, n);
                    return C + D + E;
                };

                var opts = SolverSet(Display: true);
                opts.UserDefinedJacobian = Jac;
                xstart[odds] = -1.9; xstart[evens] = 2;
                var result = Fsolve(multirosenbrook, xstart, opts);
            }

            {
                // Solve Nonlinear System of Polynomials
                Matrix A = new double[,]
                {
                    {1, 2},
                    {3, 4}
                };
                var opts = SolverSet(Display: true);
                Matrix x = Fsolve(x => x*x*x - A, Ones(2, 2), opts);
                Console.WriteLine(x);
            }

            {
                Func<double, double> f = x => x * Exp(x) - 2;
                double x0 = 0.5;
                var options = SolverSet(StepFactor: 0.9, Display: true);
                double x = Fsolve(f, x0, options);
            }

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
                        double y = Fsolve(yfunc, r);
                        z = A * Pr / y;
                    }
                    return z;
                }

                // set up ressure and temperature mesh
                ColVec Pr = Linspace(0.2, 20, 501);
                ColVec Tr = new double[] {1.05,    1.08,   1.12,   1.18,   1.26,   1.35,   1.47,
                                          1.61,    1.75,   1.91,   2.09,   2.29,   2.62,   3.00 };
                Matrix Z;

                // compute z factors and plot them
                List<string> Tlabels = [];
                List<ColVec> ZHY = [], ZDAK = [], ZDPR = [], CHY = [];
                hold = true;
                foreach (var tr in Tr)
                {
                    ZHY.Add(Pr.Select(p => ZfactorHY(p, tr)).ToArray());
                    Tlabels.Add("Tr = " + tr);
                }

                Plot(Pr, Z = ZHY);
                SaveAs("Zfactor-Hall-Yarborough-CCL-Math.png");
            }
        }


    }
}
