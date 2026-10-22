using ScottPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Mechanical_Engineering
{
    internal class Section_2_ViscousFluid
    {
        public static void Run()
        {
            /// <BookContent>
            /// <code>
            {
                (RowVec, Matrix, double) SolveBlasiusTopferTransform()
                {
                    ColVec dydt(double t, ColVec y) =>
                        [y[1], y[2], -0.5 * y[0] * y[2]];

                    // Integrate with F''(0) = 1.0 until F'' decays to numerical zero (~ xi = 6)
                    double t = 0, dt = 0.01; ColVec f = [0.0, 0.0, 1.0];
                    List<double> T = [0]; List<ColVec> F = [f];

                    while (f[2] > 1e-10)
                    {
                        f = rk4(dydt, t, f, dt);
                        t += dt;
                        T.Add(t); F.Add(f);
                    }

                    // Value of F' at numerical infinity
                    double F_prime_inf = f[1];

                    // Scaling factor C = 1 / sqrt(F'_inf)
                    double C = 1.0 / Sqrt(F_prime_inf),
                        C2 = C * C, C3 = C2 * C;

                    // Exact Blasius values
                    double fpp0 = Pow(F_prime_inf, -1.5);

                    // Rescale coordinates and fields to physical variables
                    RowVec Eta = [.. T.Select(t => t / C)];
                    ColVec p = [C, C2, C3];
                    List<ColVec> Y = [.. F.Select(f => f.Times(p))];
                    return (Eta, Y, fpp0);
                }

                (RowVec Eta, Matrix Y, double fpp0) = SolveBlasiusTopferTransform();

                Console.WriteLine($"Computed f''(0)   : {fpp0:F7}");
                Console.WriteLine($"Blasius Benchmark : 0.3320573");
                Console.WriteLine($"Absolute Error    : {Abs(fpp0 - 0.3320573):E3}");

                // Slice columns 1 and 2 (f' and f'') to align with the legend and [0, 1.1] y-axis
                Plot(Eta, Y, Linewidth: 2);

                Legend([@"f (\delta)", @"f' (u/U)", @"f'' (\tau)"], MiddleRight, interpreter: Latex);
                Xlabel(@"\eta", fontsize: 20, interpreter: Latex);
                Ylabel(@"f, \, f', \, f''", fontsize: 20, interpreter: Latex);
                Title("Blasius Profile via Non-Iterative Töpfer Scaling", fontsize: 20);
                Axis([0, Eta[^1], 0, 1.1]); GridOn();
                SaveAs("Blasius-Incompressible-Topfer.png");
                CloseFig();
            }
            /// </code>
            /// 
            /// <code>
            {
                (ColVec, Matrix, double, double) SolveBlasiusDynamicEta()
                {
                    ColVec dydt(double t, ColVec y)
                    {
                        double f = y[0], fp = y[1], fpp = y[2];
                        return [fp, fpp, -0.5 * f * fpp];
                    }

                    ColVec T = null;
                    Matrix Y = null;

                    // Unknown vector: p = [f''(0), eta_max]
                    ColVec fun(ColVec p)
                    {
                        double s = p[0];
                        double eta_inf = p[1];

                        double[] y0 = [0.0, 0.0, s];
                        double[] tspan = [0.0, eta_inf];

                        (T, Y) = Ode45(dydt, y0, tspan);

                        // Enforce both freestream velocity and zero shear stress at the edge
                        double res_vel = Y[^1, 1] - 1.0; // f'(eta_inf) - 1 = 0
                        double res_shear = Y[^1, 2];       // f''(eta_inf) = 0

                        return [res_vel, res_shear];
                    }

                    // Initial guesses: f''(0) ≈ 0.35, eta_max ≈ 7.0
                    ColVec p_guess = [0.35, 7.0];

                    ColVec p_sol = Fsolve(fun, p_guess);

                    return (T, Y, p_sol[0], p_sol[1]);
                }

                // -------------------------------------------------------------
                // Execution, Verification, and Plotting
                // -------------------------------------------------------------
                (ColVec T, Matrix Y, double fpp0, double eta_max) = SolveBlasiusDynamicEta();

                Console.WriteLine($"Computed f''(0)   : {fpp0:F7}");
                Console.WriteLine($"Blasius Benchmark : 0.3320573");
                Console.WriteLine($"Absolute Error    : {Abs(fpp0 - 0.3320573):E3}");

                // Slice columns 1 and 2 (f' and f'') to align with the legend and [0, 1.1] y-axis
                Plot(T, Y, Linewidth: 2);

                Legend([@"f (\delta)", @"f' (u/U)", @"f'' (\tau)"], MiddleRight, interpreter: Latex);
                Xlabel(@"\eta", fontsize: 20, interpreter: Latex);
                Ylabel(@"f, \, f', \, f''", fontsize: 20, interpreter: Latex);
                Title("Incompressible Blasius Boundary Layer", fontsize: 20);
                Axis([0, 10, 0, 1.1]); GridOn();
                SaveAs("Blasius-Incompressible-Shooting.png");
                CloseFig();
            }
            /// </code>
            /// 
            /// <code>
            {
                (ColVec, Matrix, double) HowarthTransform(double M)
                {

                    ColVec T = null; Matrix Y = null;
                    double gamma = 1.4, Pr = 0.7, C = Pr * (gamma - 1.0) * M * M;

                    // Thermodynamic relations: rho*mu = h^(-1/3)
                    double rho(double h) => 1.0 / h;
                    double drhodh(double h) => -1.0 / (h * h);
                    double mu(double h) => Pow(h, 2.0 / 3.0);
                    double dmudh(double h) => (2.0 / 3.0) * Pow(h, -1.0 / 3.0);
                    double rhomu(double h) => rho(h) * mu(h);
                    double drhomu(double h, double dh) => (rho(h) * dmudh(h) + drhodh(h) * mu(h)) * dh;

                    // Coupled state vector: y = [f, f', f'', h, h']
                    ColVec dydt(double t, ColVec y)
                    {
                        double f = y[0], fp = y[1], fpp = y[2], h = y[3], hp = y[4];
                        double rhomu_h = rhomu(h), drhomu_h_eta = drhomu(h, hp);
                        double fppp = -(drhomu_h_eta + f) * fpp / rhomu_h;
                        double hpp = -(drhomu_h_eta * hp + Pr * f * hp + C * rhomu_h * fpp * fpp) / rhomu_h;
                        return [fp, fpp, fppp, hp, hpp];
                    }

                    // Dynamic shooting guess: wall shear ~ 0.45; heat flux flips with M^2 dissipation
                    ColVec y35guess = [0.45, 0.5 * (M - 1), 5];

                    // 2D Residual vector: [f'(eta_inf) - 1, h(eta_inf) - 1, f''(eta_inf)]
                    ColVec fun(ColVec s)
                    {
                        double[] y0 = [0, 0, s[0], 2.0, s[1]], tspan = [0, s[2]];
                        (T, Y) = Ode45(dydt, y0, tspan);
                        double fp_inf = Y[^1, 1], fpp_inf = Y[^1, 2], h_inf = Y[^1, 3];
                        return [fp_inf - 1.0, h_inf - 1.0, fpp_inf];
                    }

                    // Solve for the missing wall shear, heat flux and eta_inf
                    ColVec sol = Fsolve(fun, y35guess);
                    return (T, Y, sol[2]);
                }

                // Subsonic boundary layer (M = 0)
                (ColVec T0, Matrix Y0, double eta_inf) = HowarthTransform(0);
                Plot(T0, Y0[.., 1], "b--", 2); HoldOn();
                Plot(T0, Y0[.., 3] - 1.0, "r--", 2);

                // Supersonic boundary layer with intense viscous dissipation (M = 5)
                (ColVec T5, Matrix Y5, eta_inf) = HowarthTransform(5);
                Plot(T5, Y5[.., 1], "b", 2);
                Plot(T5, Y5[.., 3] - 1.0, "r", 2);

                Legend(["f'_{M=0}", "h-1_{M=0}", "f'_{M=5}", "h-1_{M=5}"], UpperRight, interpreter: Latex);
                Xlabel(@"\eta", fontsize: 16, interpreter: Latex);
                Title("Howarth Compressible Boundary Layer with Thermal Coupling");
                Axis([0, eta_inf, -0.1, 2.0]); GridOn();
                SaveAs("Howarth-Transformation-Compressible.png");
                CloseFig();
            }
            /// </code>
            /// 
            /// </BookContent>
        }
    }
}
