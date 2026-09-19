using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.TrainingFiles.Chapter_09_Numerical_Optimization
{
    internal class Section_05_Constrained_Curve_Fitting
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> Constrained Curve Fitting </header 2>
            /// Constrained curve fitting is a mathematical optimization technique used to construct a model curve that best fits empirical observations while strictly satisfying auxiliary constraints on the parameter domain. 
            /// 
            /// While classical unconstrained regression seeks parameters that minimize residual variance across uninhibited Euclidean space :math:`\mathbb{R}^P`, real-world systems operate under strict physical laws, thermodynamic limits, and operational realities. Unconstrained non-linear solvers frequently fall prey to non-physical solutions—such as negative absolute temperatures, negative mass fractions, unbounded growth rates, or violated conservation laws. Constrained curve fitting integrates prior domain knowledge directly into the objective framework, guaranteeing that the converged parameter vector remains physically meaningful, statistically stable, and within operational limits.
            /// 
            /// <header 3> The General Constrained Optimization Problem </header 3>
            /// Formally, constrained non-linear curve fitting formulates the parameter search as a constrained non-linear least-squares (NLLS) optimization problem:
            /// 
            /// <math>
            /// \min_{\mathbf{p} \in \Omega} f_0(\mathbf{p}) = \frac{1}{2} \sum_{i=1}^{M} \left( y_i - f(x_i; \mathbf{p}) \right)^2 = \frac{1}{2} \|\mathbf{y} - \mathbf{f}(\mathbf{x}_{\text{data}}; \mathbf{p})\|_2^2
            /// </math>
            /// 
            /// where :math:`\mathbf{y} \in \mathbb{R}^M` denotes measured responses, :math:`f(x_i; \mathbf{p})` is the non-linear forward model, :math:`\mathbf{p} \in \mathbb{R}^P` is the unknown parameter vector, and :math:`\Omega \subset \mathbb{R}^P` defines the feasible parameter space.
            /// 
            /// The feasible domain :math:`\Omega` is governed by three primary classes of mathematical conditions:
            /// 
            /// <header 4> 1. Box Bounds (Simple Upper and Lower Limits) </header 4>
            /// Parameter box limits enforce explicit coordinate-wise intervals:
            /// 
            /// <math>
            /// \mathbf{lb} \le \mathbf{p} \le \mathbf{ub} \iff \text{lb}_j \le p_j \le \text{ub}_j, \quad \forall j \in \{0, 1, \dots, P-1\}
            /// </math>
            /// 
            /// Box constraints enforce fundamental physical constants and non-negativity rules, such as ensuring half-lives, decay constants, or diffusion coefficients satisfy :math:`p_j > 0`.
            /// 
            /// <header 4> 2. Linear Inequality and Equality Constraints </header 4>
            /// Linear restrictions model affine dependencies, mass/energy conservation, and monotonic parameter relationships:
            /// 
            /// <math>
            /// A_{\text{ineq}} \mathbf{p} \le \mathbf{b}_{\text{ineq}}, \quad A_{\text{eq}} \mathbf{p} = \mathbf{b}_{\text{eq}}
            /// </math>
            /// 
            /// In canonical inequality form, linear conditions are written as :math:`g_k(\mathbf{p}) = A_k \mathbf{p} - b_k \le 0`. Common engineering examples include component mass fractions summing to unity (:math:`\sum p_j = 1`) or total amplitude bounds (:math:`p_0 + p_2 \le p_{\text{max}}`).
            /// 
            /// <header 4> 3. Non-Linear Inequality and Equality Constraints </header 4>
            /// Non-linear constraints define arbitrary smooth geometries that parameters must satisfy:
            /// 
            /// <math>
            /// \mathbf{g}(\mathbf{p}) \le \mathbf{0}, \quad \mathbf{h}(\mathbf{p}) = \mathbf{0}
            /// </math>
            /// 
            /// These model coupled physical interactions, such as enforcing multi-component thermodynamic phase equilibria, stability criteria in dynamical systems (e.g., eigenvalue boundaries), or geometric energy thresholds (such as limiting the Euclidean parameter radius :math:`p_0^2 + p_1^2 \le R^2`).
            /// 
            /// <header 3> Optimality and Algorithmic Mechanics </header 3>
            /// Constrained least squares introduces trade-offs between objective minimization (residual goodness of fit) and boundary feasibility. The solution is governed by the Karush-Kuhn-Tucker (KKT) conditions:
            /// 
            /// <math>
            /// \nabla_{\mathbf{p}} f_0(\mathbf{p}^*) + \sum_{k \in \mathcal{E}} \lambda_k^* \nabla h_k(\mathbf{p}^*) + \sum_{j \in \mathcal{I}} \mu_j^* \nabla g_j(\mathbf{p}^*) = \mathbf{0}
            /// </math>
            /// 
            /// subject to the complementary slackness requirements:
            /// 
            /// <math>
            /// \mu_j^* \ge 0, \quad \mu_j^* g_j(\mathbf{p}^*) = 0, \quad \forall j \in \mathcal{I}
            /// </math>
            /// 
            /// When the unconstrained optimum lies outside :math:`\Omega`, the constraint becomes **active** (:math:`g_j(\mathbf{p}^*) = 0`), and the associated Lagrange multiplier :math:`\mu_j^* > 0` represents the shadow price—quantifying the exact penalty paid in residual error to satisfy physical feasibility.
            /// 
            /// In SepalSolver, constrained problems are resolved using algorithms such as Subspace Trust-Region Reflective methods and Sequential Quadratic Programming (SQP). These methods compute feasible descent steps by projecting search directions onto the null space of active constraint normals and reflecting trajectories off boundary limits, guaranteeing numerical robustness and preventing divergence.
            ///
            /// <header 3> Example: Curve Fitting with Linear Inequality Constraints </header 3>
            /// In physical parameter estimation, models often require parameters to satisfy linear relationships, such as conservation balances or monotonic thresholds. This example fits a multi-parameter model subject to parameter box bounds :math:`\mathbf{lb} \le \mathbf{x} \le \mathbf{ub}` and a coupled linear inequality constraint:
            /// 
            /// <math>
            /// A\mathbf{x} \le 0
            /// </math>
            /// 
            /// specified via a `RowVec` inner product delegate (`funInEq`). The resulting fit is evaluated for feasibility and plotted alongside the raw data.
            /// 
            /// <code>
            {
                // -------------------------------------------------------------------------
                // 1. Synthetic Data Generation & Setup
                // -------------------------------------------------------------------------
                int seed = 23;
                var rgn = new Random(seed); // Seed the RNG for reproducible synthetic observations

                // True parameter ground truth: [x0, x1, x2, x3]
                ColVec xstar = new([2.0, 4.0, 5.0, 0.5]);

                // Forward model: f(x, xdata) = x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata
                static ColVec Model(ColVec x, ColVec xdata) =>
                    x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;

                // Discretized independent variable domain (100 evenly spaced points from 2 to 7)
                ColVec xdata = Linspace(2, 7);

                // Add scaled Gaussian noise to simulate sensor/measurement uncertainty
                ColVec noise = Randn(100);
                ColVec ydata = Model(xstar, xdata) + noise / 10.0;

                // -------------------------------------------------------------------------
                // 2. Initial Estimates and Box Constraints
                // -------------------------------------------------------------------------
                ColVec startpt = new([1.0, 2.0, 3.0, 1.0]);

                // Parameter bounds: 0.0 <= x[i] <= 7.0 for all parameters
                ColVec lb = Zeros(4);
                ColVec ub = 7.0 + lb;

                // -------------------------------------------------------------------------
                // 3. Linear Inequality Constraint: A * x <= 0
                // -------------------------------------------------------------------------
                // Defined via the row vector A = [-1, -1, 1, 1].
                // Matrix formulation: A * x <= 0  -->  -x[0] - x[1] + x[2] + x[3] <= 0
                // Rearranged: (x[2] + x[3]) <= (x[0] + x[1])
                RowVec A = new([-1.0, -1.0, 1.0, 1.0]);
                ColVec Fineq(ColVec x) => A * x;

                // -------------------------------------------------------------------------
                // 4. Solver Configuration via OptimSet
                // -------------------------------------------------------------------------
                var opts = OptimSet(
                    Display: true,
                    MaxIter: 200,
                    StepTol: 1e-6,
                    OptimalityTol: 1e-6
                );

                // -------------------------------------------------------------------------
                // 5. Execute Linear Inequality-Constrained Least Squares
                // -------------------------------------------------------------------------
                var ans = Lsqcurvefit(
                    Model,
                    startpt,
                    xdata,
                    ydata,
                    funInEq: Fineq, // Linear constraint wrapped in the inequality delegate
                    funEq: null,    // Equality constraints (none)
                    lb: lb,         // Lower box bounds
                    ub: ub,         // Upper box bounds
                    options: opts
                );

                // -------------------------------------------------------------------------
                // 6. Diagnostics & Verification
                // -------------------------------------------------------------------------
                Console.WriteLine($"Recovered Parameters: x = {ans.x.T}");

                // Directly evaluate constraint feasibility at the converged solution: fineq(x*) <= 0
                Console.WriteLine($"Inequality Value:    c = {ans.fineq.T}");

                // -------------------------------------------------------------------------
                // 7. Visual Inspection & Figure Export
                // -------------------------------------------------------------------------
                Scatter(xdata, ydata, "ro"); // Red open circles for measured observations
                HoldOn();
                Plot(xdata, ans.y_hat, "-b", Linewidth: 2); // Blue line for converged model estimate

                Xlabel("x");
                Ylabel("y");
                Legend(["Measured Data", "Model Estimate"], UpperRight);

                // Export figure to disk and release plot context memory
                SaveAs("Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png");
                CloseFig();
            }
            /// </code>
            ///
            /// <header 4> Mathematical Theory: Karush-Kuhn-Tucker (KKT) Conditions for Linear Constraints </header 4>
            /// The optimization problem combines box limits and linear inequality constraints:
            ///
            /// <math>
            /// \min_{\mathbf{x}} f_0(\mathbf{x}) = \frac{1}{2} \sum_{i=1}^M \left( \text{Model}(\mathbf{x}, x_i) - y_i \right)^2
            /// </math>
            ///
            /// subject to:
            ///
            /// <math>
            /// A \mathbf{x} \le 0, \quad \mathbf{lb} - \mathbf{x} \le 0, \quad \mathbf{x} - \mathbf{ub} \le 0
            /// </math>
            ///
            /// The Lagrangian function :math:`\mathcal{L}` is defined with Lagrange multiplier :math:`\mu \ge 0` and bound multiplier vectors :math:`\boldsymbol{\lambda}_l, \boldsymbol{\lambda}_u \ge \mathbf{0}`:
            ///
            /// <math>
            /// \mathcal{L}(\mathbf{x}, \mu, \boldsymbol{\lambda}_l, \boldsymbol{\lambda}_u) = f_0(\mathbf{x}) + \mu (A \mathbf{x}) + \boldsymbol{\lambda}_l^T (\mathbf{lb} - \mathbf{x}) + \boldsymbol{\lambda}_u^T (\mathbf{x} - \mathbf{ub})
            /// </math>
            ///
            /// First-order optimality requires that the solution :math:`\hat{\mathbf{x}}` satisfy the Karush-Kuhn-Tucker (KKT) conditions:
            ///
            /// <math>
            /// \nabla_{\mathbf{x}} f_0(\hat{\mathbf{x}}) + A^T \mu - \boldsymbol{\lambda}_l + \boldsymbol{\lambda}_u = \mathbf{0}
            /// </math>
            ///
            /// along with complementary slackness:
            ///
            /// <math>
            /// \mu (A \hat{\mathbf{x}}) = 0, \quad \lambda_{l,j} (\text{lb}_j - \hat{x}_j) = 0, \quad \lambda_{u,j} (\hat{x}_j - \text{ub}_j) = 0
            /// </math>
            ///
            /// For the target solution :math:`\mathbf{x}^* = [2.0, 4.0, 5.0, 0.5]^T`, evaluation yields:
            ///
            /// <math>
            /// A \mathbf{x}^* = -2.0 - 4.0 + 5.0 + 0.5 = -0.5 < 0
            /// </math>
            ///
            /// Because the constraint is strictly negative (:math:`A \mathbf{x}^* < 0`), the constraint is **inactive**. Complementary slackness requires :math:`\mu = 0`, meaning the gradient of the objective function vanishes directly: :math:`\nabla f_0(\hat{\mathbf{x}}) = \mathbf{0}`.
            /// 
            /// <header 3> Example: Curve Fitting with Non-Linear Inequality Constraints </header 3>
            /// When parameter boundaries involve non-linear dependencies, such as radial or volumetric constraints, non-linear inequality functions are used. In this example, the two primary parameters are restricted to lie within a circle of radius 4:
            /// 
            /// <math>
            /// x_0^2 + x_1^2 - 16 \le 0
            /// </math>
            /// 
            /// Because the unconstrained parameters violate this threshold:
            /// 
            /// <math>
            /// 2^2 + 4^2 = 20 > 16
            /// </math>
            /// 
            /// the constraint is actively enforced, leading `Lsqcurvefit` to converge along the active feasible boundary.
            /// 
            /// <code>
            {
                // -------------------------------------------------------------------------
                // 1. Reproducible Synthetic Data Generation
                // -------------------------------------------------------------------------
                int seed = 23;
                var rgn = new Random(seed); // Seeds the random number generator for reproducibility

                // True parameter vector: [x0, x1, x2, x3]
                ColVec xstar = new([2.0, 4.0, 5.0, 0.5]);

                // Forward model: f(x, xdata) = x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata
                static ColVec Model(ColVec x, ColVec xdata) =>
                    x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;

                // Generate synthetic grid across [2, 7] (Linspace defaults to 100 points)
                ColVec xdata = Linspace(2, 7);

                // Add Gaussian white noise scaled down by a factor of 10
                ColVec noise = Randn(100);
                ColVec ydata = Model(xstar, xdata) + noise / 10.0;

                // -------------------------------------------------------------------------
                // 2. Initial Estimates and Boundary Constraints
                // -------------------------------------------------------------------------
                // Initial parameter estimate far from xstar
                ColVec startpt = new([1.0, 2.0, 3.0, 1.0]);

                // Box constraints: 0.0 <= x[i] <= 7.0 for all parameters
                ColVec lb = Zeros(4);
                ColVec ub = 7.0 + lb;

                // -------------------------------------------------------------------------
                // 3. Nonlinear Inequality Constraint: g(x) <= 0
                // -------------------------------------------------------------------------
                // Constrains the vector norm of the first two parameters to lie within a circle of radius 4:
                // x[0]^2 + x[1]^2 <= 16  -->  x[0]^2 + x[1]^2 - 16 <= 0
                static ColVec Fineq(ColVec x) => new([x[..2].SumSq() - 16.0]);

                // -------------------------------------------------------------------------
                // 4. Solver Configuration via OptimSet
                // -------------------------------------------------------------------------
                var opts = OptimSet(
                    Display: true,
                    MaxIter: 200,
                    StepTol: 1e-6,
                    OptimalityTol: 1e-6
                );

                // -------------------------------------------------------------------------
                // 5. Solve Constrained Non-Linear Least Squares
                // -------------------------------------------------------------------------
                var ans = Lsqcurvefit(
                    Model,
                    startpt,
                    xdata,
                    ydata,
                    funInEq: Fineq, // Nonlinear inequality delegate
                    funEq: null,    // Equality constraints (none)
                    lb: lb,         // Lower box bounds
                    ub: ub,         // Upper box bounds
                    options: opts
                );

                // -------------------------------------------------------------------------
                // 6. Inspect Numerical Results
                // -------------------------------------------------------------------------
                Console.WriteLine($"Recovered Parameters: x = {ans.x.T}");
                Console.WriteLine($"Inequality Residual:  c = {ans.fineq.T}");

                // -------------------------------------------------------------------------
                // 7. Visual Inspection & Plotting
                // -------------------------------------------------------------------------
                // Plot raw noisy measurements as red open circles
                Scatter(xdata, ydata, "ro");
                HoldOn();

                // Overlay the optimal model fit (ans.y_hat) as a solid blue line
                Plot(xdata, ans.y_hat, "-b", Linewidth: 2);

                // Add axes metadata and legend
                Xlabel("x");
                Ylabel("y");
                Legend(["Measured Data", "Model Estimate"], UpperRight);

                // Export high-resolution chart and release plotting resources
                SaveAs("Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png");

                AnimateHistory(
                    Model,
                    xdata,
                    ydata,
                    ans.history,
                    "CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.gif"
                );
                CloseFig();
            }
            /// </code>
            ///
            /// <header 4> Mathematical Theory: Active Boundary Projections and Sequential Quadratic Programming </header 4>
            /// When an unconstrained parameter minimum :math:`\mathbf{x}^*` lies in the infeasible domain:
            ///
            /// <math>
            /// g(\mathbf{x}^*) = (x_0^*)^2 + (x_1^*)^2 - 16 = 20 - 16 = +4 > 0
            /// </math>
            ///
            /// the constraint becomes **active** at the solution :math:`\hat{\mathbf{x}}`, meaning :math:`g(\hat{\mathbf{x}}) = 0`.
            ///
            /// Under Sequential Quadratic Programming (SQP) or Interior-Point Trust-Region formulations, the algorithm constructs a local quadratic model of the objective subject to linearized constraints at iteration :math:`k`:
            ///
            /// <math>
            /// \min_{\mathbf{d}} \nabla f_0(\mathbf{x}_k)^T \mathbf{d} + \frac{1}{2} \mathbf{d}^T \mathbf{B}_k \mathbf{d} \quad \text{subject to} \quad g(\mathbf{x}_k) + \nabla g(\mathbf{x}_k)^T \mathbf{d} \le 0
            /// </math>
            ///
            /// where :math:`\mathbf{B}_k` is a positive-definite quasi-Newton approximation of the Hessian of the Lagrangian, and the constraint gradient is:
            ///
            /// <math>
            /// \nabla g(\mathbf{x}) = \begin{bmatrix} 2 x_0 & 2 x_1 & 0 & 0 \end{bmatrix}^T
            /// </math>
            ///
            /// At the constrained optimum :math:`\hat{\mathbf{x}}`, the objective gradient does not vanish. Instead, it balances the outward outward-pointing normal to the constraint surface:
            ///
            /// <math>
            /// -\nabla f_0(\hat{\mathbf{x}}) = \mu^* \nabla g(\hat{\mathbf{x}}), \quad \mu^* > 0
            /// </math>
            ///
            /// The Lagrange multiplier :math:`\mu^*` acts as a shadow price, quantifying the decrease in the residual sum of squares that would occur if the feasible disc radius were relaxed:
            ///
            /// <math>
            /// \frac{d f_0}{d(\text{radius}^2)} = -\mu^*
            /// </math>
            /// 
            /// <header 3> Example: Damped Harmonic Model with Coupled Inequality Bounds </header 3>
            /// Parameter bounds are frequently paired with coupled linear constraints to reflect physical operational envelopes. This example fits a 4-parameter damped harmonic model:
            /// 
            /// <math>
            /// f(\mathbf{p}, x) = p_0 e^{-p_1 x} + p_2 \sin(p_3 x)
            /// </math>
            /// 
            /// subject to bound limits :math:`\mathbf{lb} \le \mathbf{p} \le \mathbf{ub}` and an amplitude sum constraint:
            /// 
            /// <math>
            /// p_0 + p_2 - 5.0 \le 0
            /// </math>
            /// 
            /// The routine executes using `Lsqcurvefit`, reporting parameter outcomes alongside `resnorm`, constraint evaluations (`fineq`), and convergence status flags.
            /// 
            /// <code>
            {
                // 1. Experimental observation vectors
                ColVec xdata = new([0.1, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0]);
                ColVec ydata = new([3.8, 2.9, 2.1, 1.8, 1.4, 1.2, 0.9, 0.7, 0.5]);

                // 2. Initial parameter guess: [p0, p1, p2, p3]
                ColVec startpt = new([2.0, 0.5, 1.0, 1.0]);

                // 3. Parameterized model function: f(p, x) = p[0]*exp(-p[1]*x) + p[2]*sin(p[3]*x)
                static ColVec Model(ColVec p, ColVec x) =>
                    p[0] * Exp(-p[1] * x) + p[2] * Sin(p[3] * x);


                // 4. Physical parameter box bounds (lb <= p <= ub)
                ColVec lb = new([0.0, 0.01, 0.0, 0.0]); // Non-negative rates and amplitudes
                ColVec ub = new([10.0, 5.00, 5.0, 10.0]);

                // 5. Inequality constraint: p[0] + p[2] <= 5.0  --> (p[0] + p[2] - 5.0 <= 0)
                Func<ColVec, ColVec> fineq = p => new([p[0] + p[2] - 5.0]);

                // 6. Solver configuration via OptimSet
                var opts = OptimSet(
                    MaxIter: 300,
                    StepTol: 1e-8,
                    OptimalityTol: 1e-8,
                    Display: true
                );

                // 7. Execute constrained least-squares curve fit
                var ans = Lsqcurvefit(
                    Model,
                    startpt,
                    xdata,
                    ydata,
                    funInEq: fineq,
                    funEq: null,
                    lb: lb,
                    ub: ub,
                    options: opts
                );

                // 8. Diagnostics & post-fit inspection
                Console.WriteLine($"Fitted Parameters:       {ans.x.T}");
                Console.WriteLine($"Residual Norm (resnorm): {ans.resnorm:E4}");
                Console.WriteLine($"Inequality Residual:    {ans.fineq.T}");
                Console.WriteLine($"Exit Flag:              {ans.exitflag}");

                AnimateHistory(
                    Model,
                    xdata,
                    ydata,
                    ans.history,
                    "Damped Harmonic Model with Coupled Inequality Bounds.gif"
                );
            }
            /// </code>
            ///
            /// <header 4> Mathematical Theory: Box-Constrained Trust-Region Reflective Mechanics </header 4>
            /// In SepalSolver, constrained least-squares with box bounds :math:`\mathbf{lb} \le \mathbf{p} \le \mathbf{ub}` and coupled constraints :math:`\mathbf{g}(\mathbf{p}) \le \mathbf{0}` is solved using a subspace trust-region reflective algorithm.
            ///
            /// To handle parameter bounds, the parameter vector :math:`\mathbf{p}` is mapped to a scaled coordinate system via the diagonal scaling matrix :math:`D(\mathbf{p})`:
            ///
            /// <math>
            /// D(\mathbf{p}) = \text{diag}\left( |v_1|^{-1/2}, \dots, |v_4|^{-1/2} \right)
            /// </math>
            ///
            /// where each directional component :math:`v_j` measures the distance to the active bound depending on the sign of the gradient :math:`g_j = (\nabla f_0)_j`:
            ///
            /// <math>
            /// v_j = \begin{cases}
            /// p_j - \text{lb}_j, & \text{if } g_j < 0 \text{ and } \text{lb}_j > -\infty \\
            /// \text{ub}_j - p_j, & \text{if } g_j > 0 \text{ and } \text{ub}_j < \infty \\
            /// -1, & \text{otherwise}
            /// \end{cases}
            /// </math>
            ///
            /// Inside the trust region of radius :math:`\Delta_k`, a quadratic model step :math:`\mathbf{s}_k` is determined:
            ///
            /// <math>
            /// \min_{\mathbf{s}} \mathbf{g}_k^T \mathbf{s} + \frac{1}{2} \mathbf{s}^T \left( J_k^T J_k + C_k \right) \mathbf{s} \quad \text{subject to} \quad \| D_k \mathbf{s} \|_2 \le \Delta_k
            /// </math>
            ///
            /// where :math:`C_k = D_k^{-1} \text{diag}(\mathbf{g}_k) \nabla v(\mathbf{p}_k)` accounts for boundary curvature.
            ///
            /// If a trial step :math:`\mathbf{p}_k + \mathbf{s}_k` encounters a boundary bound :math:`\text{lb}_j` or :math:`\text{ub}_j`, the trajectory does not truncate; instead, it undergoes specular reflection:
            ///
            /// <math>
            /// p_j^{\text{reflected}} = 2 \cdot \text{bound}_j - (p_{k,j} + s_{k,j})
            /// </math>
            ///
            /// This reflective mechanism allows the optimizer to follow valleys along physical constraint edges (such as :math:`p_1 \ge 0.01` and :math:`p_0 + p_2 \le 5.0`) without getting trapped at the boundaries, guaranteeing both feasibility and asymptotic convergence.
            /// 
            /// /// <header 3> Example: Curve Fitting with Equality Constraints </header 3>
            /// In many engineering and physical problems, parameters must obey exact equality relationships derived from conservation laws, known boundary conditions, or normalization requirements.
            /// 
            /// Consider fitting a two-component mixture or fractional partition model:
            /// 
            /// <math>
            /// f(\mathbf{x}, t) = x_0 e^{-x_1 t} + x_2 e^{-x_3 t}
            /// </math>
            /// 
            /// where the initial total response at :math:`t = 0` must strictly match a known baseline value :math:`Y_0 = 5.0`:
            /// 
            /// <math>
            /// f(\mathbf{x}, 0) = x_0 + x_2 = 5.0
            /// </math>
            /// 
            /// In SepalSolver, equality constraints are passed via the `funEq` delegate in standard canonical form :math:`\mathbf{h}(\mathbf{x}) = \mathbf{0}`:
            /// 
            /// <math>
            /// h(\mathbf{x}) = x_0 + x_2 - 5.0 = 0
            /// </math>
            /// 
            /// <code>
            {
                // -------------------------------------------------------------------------
                // 1. Synthetic Observation Data Generation
                // -------------------------------------------------------------------------
                int seed = 42;
                var rgn = new Random(seed);

                // True underlying parameters: [x0, x1, x2, x3]
                // Note that xstar[0] + xstar[2] = 3.0 + 2.0 = 5.0 (satisfies equality)
                ColVec xstar = new([3.0, 0.8, 2.0, 0.1]);

                // Forward bi-exponential decay model:
                // f(x, t) = x[0]*exp(-x[1]*t) + x[2]*exp(-x[3]*t)
                static ColVec Model(ColVec x, ColVec t) =>
                    x[0] * Exp(-x[1] * t) + x[2] * Exp(-x[3] * t);

                // Independent time points across [0, 10]
                ColVec xdata = Linspace(0.0, 10.0, 50);

                // Additive Gaussian noise (standard deviation = 0.05)
                ColVec noise = Randn(50);
                ColVec ydata = Model(xstar, xdata) + noise * 0.05;

                // -------------------------------------------------------------------------
                // 2. Initial Estimates & Box Constraints
                // -------------------------------------------------------------------------
                // Initial guess does NOT satisfy the equality constraint: 1.5 + 1.5 = 3.0 != 5.0
                ColVec startpt = new([1.5, 0.5, 1.5, 0.2]);

                // Physical box bounds: non-negative amplitudes and decay rates
                ColVec lb = new([0.0, 0.01, 0.0, 0.001]);
                ColVec ub = new([10.0, 5.00, 10.0, 2.000]);

                // -------------------------------------------------------------------------
                // 3. Equality Constraint Definition: h(x) = 0
                // -------------------------------------------------------------------------
                // Exact requirement: x[0] + x[2] == 5.0  -->  (x[0] + x[2] - 5.0) == 0
                Func<ColVec, ColVec> feq = x => new([x[0] + x[2] - 5.0]);

                // -------------------------------------------------------------------------
                // 4. Solver Configuration via OptimSet
                // -------------------------------------------------------------------------
                var opts = OptimSet(
                    Display: true,
                    MaxIter: 200,
                    StepTol: 1e-7,
                    OptimalityTol: 1e-7
                );

                // -------------------------------------------------------------------------
                // 5. Execute Equality-Constrained Least Squares
                // -------------------------------------------------------------------------
                var ans = Lsqcurvefit(
                    Model,
                    startpt,
                    xdata,
                    ydata,
                    funInEq: null, // No inequality constraints
                    funEq: feq,    // Equality constraint delegate
                    lb: lb,
                    ub: ub,
                    options: opts
                );

                // -------------------------------------------------------------------------
                // 6. Diagnostics & Verification
                // -------------------------------------------------------------------------
                Console.WriteLine($"Recovered Parameters: x = {ans.x.T}");
                Console.WriteLine($"Amplitude Sum (x0+x2): {ans.x[0] + ans.x[2]:F6}");
                Console.WriteLine($"Equality Residual:    h = {ans.feq.T}");
                Console.WriteLine($"Residual Norm (resnorm): {ans.resnorm:E4}");
                Console.WriteLine($"Convergence ExitFlag:  {ans.exitflag}");

                // -------------------------------------------------------------------------
                // 7. Visual Inspection & Plotting
                // -------------------------------------------------------------------------

                AnimateHistory(
                    Model,
                    xdata,
                    ydata,
                    ans.history,
                    "CurveFitting_with_Equality_Constraints.gif"
                );
                CloseFig();
            }
            /// </code>
            /// 
            /// <header 4> Mathematical Theory: Lagrange Multipliers and Manifold Projection </header 4>
            /// An equality-constrained non-linear least-squares problem has the general form:
            /// 
            /// <math>
            /// \min_{\mathbf{x}} f_0(\mathbf{x}) = \frac{1}{2} \|\mathbf{y} - \mathbf{f}(\mathbf{x}_{\text{data}}; \mathbf{x})\|_2^2 \quad \text{subject to} \quad \mathbf{h}(\mathbf{x}) = \mathbf{0}
            /// </math>
            /// 
            /// The equality constraints restrict the feasible parameter search space to a lower-dimensional Riemannian submanifold :math:`\mathcal{M} = \{ \mathbf{x} \in \mathbb{R}^P \mid \mathbf{h}(\mathbf{x}) = \mathbf{0} \}`.
            /// 
            /// The Lagrangian function associated with the system is:
            /// 
            /// <math>
            /// \mathcal{L}(\mathbf{x}, \boldsymbol{\lambda}) = f_0(\mathbf{x}) + \boldsymbol{\lambda}^T \mathbf{h}(\mathbf{x})
            /// </math>
            /// 
            /// where :math:`\boldsymbol{\lambda} \in \mathbb{R}^K` is the vector of unconstrained Lagrange multipliers. First-order stationarity requires:
            /// 
            /// <math>
            /// \nabla_{\mathbf{x}} \mathcal{L}(\mathbf{x}^*, \boldsymbol{\lambda}^*) = \nabla f_0(\mathbf{x}^*) + J_h(\mathbf{x}^*)^T \boldsymbol{\lambda}^* = \mathbf{0}
            /// </math>
            /// 
            /// along with strict primal feasibility:
            /// 
            /// <math>
            /// \mathbf{h}(\mathbf{x}^*) = \mathbf{0}
            /// </math>
            /// 
            /// where :math:`J_h(\mathbf{x}) = \nabla_{\mathbf{x}} \mathbf{h}(\mathbf{x})` is the constraint Jacobian. For the linear sum constraint :math:`h(\mathbf{x}) = x_0 + x_2 - 5.0 = 0`, the gradient is constant:
            /// 
            /// <math>
            /// \nabla h(\mathbf{x}) = \begin{bmatrix} 1 & 0 & 1 & 0 \end{bmatrix}^T
            /// </math>
            /// 
            /// In SepalSolver, rather than penalizing violations with large penalty weights (which ill-conditions the Hessian), the algorithm projects trial Gauss-Newton search steps :math:`\mathbf{d}` onto the null space of :math:`J_h`:
            /// 
            /// <math>
            /// J_h(\mathbf{x}_k) \mathbf{d}_k = -\mathbf{h}(\mathbf{x}_k)
            /// </math>
            /// 
            /// This guarantees that every accepted step remains tangential to the constraint surface, enforcing :math:`\|\mathbf{h}(\mathbf{x}^*)\| \le \text{Tol}` while driving the residual sum of squares to its constrained minimum.
            /// 
            /// </BookContent>
        }
    }
}
