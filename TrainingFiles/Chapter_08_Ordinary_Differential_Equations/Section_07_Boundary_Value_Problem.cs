using CSharpMath.Atom;
using Google.Protobuf.Collections;
using Microsoft.CodeAnalysis;
using ScottPlot;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow.Operations.Activation;
using static CSharpMath.Rendering.Text.TextAtom;

namespace ConsoleApp1.TrainingFiles.Chapter_08_Ordinary_Differential_Equations
{
    public class Section_07_Boundary_Value_Problem
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// <header 2> 1. Introduction to Two-Point Boundary Value Problems (BVPs) </header>
            /// In contrast to Initial Value Problems (IVPs)—where all dependent state variables and derivatives are specified at a single origin point :math:`t_0`—Boundary Value Problems (BVPs) specify physical constraints at more than one location in the domain.
            ///
            /// The canonical two-point boundary value problem for a first-order system of ordinary differential equations is defined on the interval :math:`x \in [a, b]` as:
            /// :math:`y' = f(x, y), \quad g(y(a), y(b)) = 0`
            ///
            /// Where :math:`y \in \mathbb{R}^n` represents the state vector and :math:`g: \mathbb{R}^n \times \mathbb{R}^n \to \mathbb{R}^n` represents a system of :math:`n` linear or non-linear boundary conditions.
            ///
            /// Boundary conditions generally fall into three structural categories:
            /// - **Dirichlet (Essential):** Values of the state are prescribed directly at boundaries, :math:`y(a) = \alpha`.
            /// - **Neumann(Natural):** Gradient fluxes are prescribed at boundaries, :math:`y'(a) = \beta`.
            /// - **Robin / Mixed:** A linear or non - linear combination of state and gradient values is enforced, :math:`c_1 y(a) +c_2 y'(a) = \gamma`.
            /// - **Asymptotic / Far - Field:** The domain is semi - infinite :math:`[0, \infty)`, and boundary conditions decay to free - stream limits: :math:`\lim_{ x \to \infty} y(x) = y_\infty`.
            ///
            ///
            /// <header 2> 2. The Shooting Method </header>
            /// The shooting method transforms a boundary value problem into an equivalent root-finding problem parameterized by missing initial conditions. 
            ///
            /// Suppose :math:`p \in \mathbb{R}^k` denotes the vector of unknown initial values at :math:`x = a`. Let :math:`y(x; p)` denote the numerical trajectory obtained by integrating the initial value problem with known conditions :math:`y_0` and guessed values :math:`p`:
            /// :math:`\Phi(p) = g(y(a; p), y(b; p)) = 0`
            ///
            /// The root :math:`p^*` satisfying :math:`\Phi(p^*) = 0` is identified iteratively using multi-dimensional Newton-Raphson iterations or quasi-Newton algorithms (such as Broyden or Levenberg-Marquardt):
            /// :math:`p^{(k+1)} = p^{(k)} - \left[ J_\Phi(p^{(k)}) \right]^{-1} \Phi(p^{(k)})`
            ///
            /// In `SepalSolver`, this architecture couples the adaptive integrators (`Ode45`, `Ode43a`) directly with non-linear system solvers (`Fsolve`). `Fsolve` performs the final evaluation at the converged root, populating state trajectories without re-integration passes.
            ///
            /// ---
            ///
            /// <header 2> 3. Examples and Applications </header>
            ///
            /// <example 1>
            /// **Example 1: The Incompressible Blasius Boundary Layer**
            /// The Blasius equation models steady two-dimensional laminar boundary layer flow along a semi-infinite flat plate held parallel to an oncoming free-stream velocity :math:`U_\infty`.
            ///
            /// Prandtl's boundary layer equations reduce via similarity transformation :math:`\eta = y\sqrt{U_\infty / (\nu x)}` to a third-order non-linear differential equation:
            /// :math:`2f''' + f f'' = 0`
            ///
            /// Subject to three physical boundary conditions:
            /// :math:`f(0) = 0 \quad (\text{Impermeable wall})`
            /// :math:`f'(0) = 0 \quad (\text{No-slip wall})`
            /// :math:`\lim_{\eta \to \infty} f'(\eta) = 1 \quad (\text{Free-stream matching})`
            ///
            /// The missing boundary condition is the wall shear stress parameter :math:`s = f''(0)`.
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
            /// </example>
            ///
            /// <example 2>
            /// **Example 2: Group Invariance and Töpfer's Non-Iterative Transformation**
            /// Because the Blasius equation contains an infinite boundary condition, numerical shooting can exhibit sensitivity to domain truncation :math:`\eta_\infty`. 
            ///
            /// However, the Blasius equation possesses a Lie group scaling symmetry. If :math:`F(\xi)` solves the Initial Value Problem with an arbitrary initial condition :math:`F''(0) = 1`:
            /// :math:`F''' + \frac{1}{2} F F'' = 0, \quad F(0) = 0, \quad F'(0) = 0, \quad F''(0) = 1`
            ///
            /// Then under the affine transformation :math:`f(\eta) = C F(C \eta)` (where :math:`\xi = C \eta`), the differential equation remains invariant.
            /// Matching the outer boundary condition :math:`f'(\infty) = 1` gives:
            /// :math:`f'(\eta) = C^2 F'(C \eta) \implies C^2 F'(\infty) = 1 \implies C = \left(\frac{1}{F'(\infty)}\right)^{1/2}`
            ///
            /// The physical coordinates and exact wall shear stress follow directly in a single forward pass:
            /// :math:`\eta = \frac{\xi}{C}, \quad f''(0) = C^3 F''(0) = [F'(\infty)]^{-3/2}`
            ///
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
            /// </example>
            ///
            /// <example 3>
            /// **Example 3: Compressible Howarth Boundary Layer (Two-Way Thermal Coupling)**
            /// When compressible flow travels over a flat plate, variable fluid properties couple the momentum and energy equations. 
            ///
            /// Under the Howarth-Dorodnitsyn coordinate transformation with an ideal gas law (:math:`\tilde{\rho} = 1/\tilde{h}`) and a power-law viscosity model (:math:`\tilde{\mu} = \tilde{h}^{2/3}`), the density-viscosity product becomes temperature-dependent:
            /// :math:`C(\tilde{h}) = \tilde{\rho}\tilde{\mu} = \tilde{h}^{-1/3}`
            ///
            /// The coupled boundary value system reads:
            /// <math>
            /// \begin{aligned}
            ///     \left(\tilde{\rho}\tilde{\mu} f''\right)' + f f'' &= 0 \\
            ///     \left(\cfrac{\tilde{\rho}\tilde{\mu}}{\text{Pr}} \tilde{h}'\right)' + f \tilde{h}' + (\gamma - 1) M^2 \tilde{\rho}\tilde{\mu} (f'')^2 &= 0
            /// \end{aligned}
            /// </math>
            ///
            /// Expanding derivatives explicitly yields two coupled equations for :math:`f'''` and :math:`\tilde{h}''`:
            /// <math>
            /// \begin{aligned}
            ///     f''' &= -\cfrac{(\tilde{\rho}\tilde{\mu})' f'' + f f''}{\tilde{\rho}\tilde{\mu}} \\
            ///     \tilde{h}'' &= -\cfrac{(\tilde{\rho}\tilde{\mu})' \tilde{h}' + \text{Pr} f \tilde{h}' + \text{Pr}(\gamma - 1)M^2 (\tilde{\rho}\tilde{\mu}) (f'')^2}{\tilde{\rho}\tilde{\mu}}
            /// \end{aligned}
            /// </math>
            ///
            /// Boundary conditions:
            /// :math:`f(0) = 0, \quad f'(0) = 0, \quad \tilde{h}(0) = \tilde{h}_w = 2.0`
            /// :math:`\lim_{\eta \to \infty} f'(\eta) = 1, \quad \lim_{\eta \to \infty} \tilde{h}(\eta) = 1`
            ///
            /// This requires a two-parameter shooting method solving simultaneously for wall shear :math:`s_1 = f''(0)` and wall heat flux :math:`s_2 = \tilde{h}'(0)`.
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
            /// </example>
            ///
            /// <example 4>
            /// **Example 4: Non-linear Heat Generation with Thermal Runaway (Bratu Problem)**
            /// The one-dimensional Bratu problem models spontaneous thermal ignition and heat conduction in a zero-order combustion reaction:
            /// :math:`u'' + \lambda e^{u} = 0, \quad u(0) = 0, \quad u(1) = 0`
            ///
            /// Where :math:`\lambda > 0` is the Frank-Kamenetskii parameter.
            /// This BVP exhibits a turning point (fold bifurcation) at :math:`\lambda_c \approx 3.513830719`. For :math:`0 < \lambda < \lambda_c`, there exist two distinct solutions (one stable, one unstable branch), while for :math:`\lambda > \lambda_c`, no real steady solution exists (thermal explosion).
            ///
            /// <code>
            {
                double lambda = 1.0;

                // State vector: y = [u, u']
                ColVec bratu_f(double x, ColVec y) =>
                    [y[1], -lambda * Exp(y[0])];

                double[] tspan = [0, 1];
                ColVec X = null; Matrix U = null;

                // Shooting residual: u(1) - 0 = 0
                double fun(double s)
                {
                    double[] y0 = [0.0, s];
                    (X, U) = Ode45(bratu_f, y0, tspan);
                    return U[^1, 0];
                }

                // Branch 1: Lower (stable) solution branch
                double s1 = Fsolve(fun, 0.5);
                (ColVec X1, Matrix U1) = Ode45(bratu_f, [0.0, s1], tspan);

                // Branch 2: Upper (unstable) solution branch
                double s2 = Fsolve(fun, 4.0);
                (ColVec X2, Matrix U2) = Ode45(bratu_f, [0.0, s2], tspan);

                Plot(X1, U1[.., 0], "b", Linewidth: 2);
                HoldOn();
                Plot(X2, U2[.., 0], "r--", Linewidth: 2);

                Console.WriteLine($"Branch 1 Wall Slope u'(0): {s1:F6}");
                Console.WriteLine($"Branch 2 Wall Slope u'(0): {s2:F6}");

                Legend(["Lower Branch (Stable)", "Upper Branch (Unstable)"], LowerCenter);
                Xlabel("Coordinate x"); Ylabel("Temperature u(x)");
                Title(@$"Bratu Problem Dual Solutions (\lambda = {lambda})", interpreter: Latex);
                GridOn();
                SaveAs("Bratu-Problem-Dual-Branches.png");
                CloseFig();
            }
            /// </code>
            /// </example>
            ///
            /// </BookContent>
        }
    }
}
