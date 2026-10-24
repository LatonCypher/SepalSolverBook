namespace ConsoleApp1.TrainingFiles.Chapter_08_Ordinary_Differential_Equations
{
    internal class index
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// <header 1> Introduction to Differential Equations </header>
            /// 
            /// Ordinary Differential Equations (ODEs) are fundamental in modeling dynamic systems across various fields of engineering and science. They describe how a quantity changes with respect to another, typically time. In SepalSolver, we implement numerical methods to solve ODEs, enabling engineers to simulate and analyze complex systems.
            /// 
            /// ODEs appear in numerous applications, such as mechanical vibrations, electrical circuits, population dynamics, and chemical reactions. They serve as the mathematical bedrock for modeling dynamical systems across physics, chemistry, biology, and engineering. They characterize how state variables evolve with respect to an independent continuum, most commonly time:
            /// :math:`y' = f(t, y), \quad y(t_0) = y_0`
            /// where :math:`y(t) \in \mathbb{R}^n` represents the state vector and :math:`f: \mathbb{R} \times \mathbb{R}^n \to \mathbb{R}^n` defines the vector field. While higher-order differential equations frequently arise in physical systems—such as Newton's second law in mechanical vibrations—they are readily transformed into equivalent first-order systems via state augmentation.
            /// 
            /// Because closed-form analytical solutions are rarely obtainable for nonlinear or coupled real-world systems, numerical integration is indispensable. Real-world applications—ranging from multi-body mechanical vibrations and RLC circuits to chemical reaction networks, delayed biological feedbacks, and reservoir fluid dynamics—rely fundamentally on discrete-time approximations that balance convergence order, computational efficiency, and numerical stability.
            /// By solving these equations numerically, we can predict system behavior, optimize performance, and design control strategies.
            /// In this chapter, we explore the theoretical foundations, numerical implementations, and practical applications of the differential equation solvers available in SepalSolver.
            /// 
            /// ---
            /// 
            /// <header 2> Solver Suite Overview </header>
            /// 
            /// SepalSolver organizes its differential and delay equation integrators into five specialized families based on problem stiffness, algebraic constraints, and memory lag structures:
            /// 
            /// <table>
            /// Family | Solvers | Formulation & Intended Problem Class
            /// **Non-Stiff ODEs** | `Ode23`, `Ode45`, `Ode56`, `Ode67`, `Ode78`, `Ode89` | Explicit Runge-Kutta pairs for smooth dynamical systems without severe timescale separation.
            /// **Stiff ODEs** | `Ode32s`, `Ode43s`, `Ode85s` | Implicit / Rosenbrock / L-stable schemes for systems with widely varying decay rates or fast transients.
            /// **Differential-Algebraic Equations** | `Ode32a`, `Ode43a`, `Ode85a` | Singular mass-matrix formulations (:math:`M y' = f(t, y)`) for constrained dynamic and physical conservation laws.
            /// **Implicit ODEs / DAEs** | `Ode32i`, `Ode43i`, `Ode85i` | Fully implicit equations (:math:`F(t, y, y') = 0`) returning state and derivative trajectories :math:`(T, Y, Y_p)`.
            /// **Delay Differential Equations (DDEs)** | `Dde23`, `Dde45`, `Dde43s` | Systems with past-state memory lags (:math:`y'(t) = f(t, y(t), y(t - \tau_1), \dots)`), supporting both non-stiff propagation and stiff implicit damping (`Dde43s`).
            /// </table>
            /// 
            /// ---
            /// 
            /// <header 2> Output Signatures and Solution Representation </header>
            /// 
            /// The integrators in SepalSolver provide flexible output signatures depending on whether you require an encapsulated solution object, explicit state matrices, or derivative trajectories for implicit equations.
            /// 
            /// 1. **Encapsulated Solution Object** (`OdeResult` / `DdeResult`):
            /// Calling an integrator with a single return assignment yields an encapsulated result structure containing the discrete trajectory, internal stage slopes, solver performance diagnostics, and continuous interpolating machinery:
            /// <signature>
            /// OdeResult res = Ode45(odefun, y0, tspan, opts);
            /// DdeResult ddeRes = Dde23(ddefun, history, tspan, delays, opts);
            /// </signature>
            /// 
            /// 2. **Explicit Tuple Deconstruction** (Explicit ODEs, Stiff Systems, and Mass-Matrix DAEs):
            /// For standard explicit ODEs, stiff ODEs, mass-matrix DAEs (:math:`M y' = f(t, y)`), and delay differential equations, the output can be deconstructed directly into the time vector :math:`T` and solution matrix :math:`Y`:
            /// <signature>
            /// (ColVec T, Matrix Y) = Ode45(odefun, y0, tspan, opts);
            /// 
            /// // Mass-matrix DAEs:
            /// (ColVec T, Matrix Y) = Ode43a(odefun, Mass, y0, tspan, opts);
            /// 
            /// // Delay Differential Equations:
            /// (ColVec T, Matrix Y) = Dde23(ddefun, history, tspan, delays, opts);
            /// </signature>
            /// In the solution matrix :math:`Y`, rows correspond to individual time points in :math:`T`, and columns correspond to the respective state variables :math:`y_i`.
            /// 
            /// 3. **Implicit Differential Equations** (`i` **Family**):
            /// For fully implicit differential systems (:math:`F(t, y, y') = 0`), the derivative trajectory :math:`y'(t)` is also a primary dependent variable. Solvers in the implicit family (`Ode32i`, `Ode43i`, `Ode85i`) deconstruct into a 3-tuple returning :math:`T`, :math:`Y`, and the derivative matrix :math:`Y_p`:
            /// <signature>
            /// (ColVec T, Matrix Y, Matrix Yp) = Ode43i(impidfun, y0, yp0, tspan, opts);
            /// </signature>
            /// 
            /// ---
            /// 
            /// <header 2> Continuous Dense Output and Evaluation via `Deval` </header>
            /// 
            /// Adaptive step-size solvers naturally select non-uniform step sizes :math:`h_k` to maintain local error tolerances, often skipping wide quiescent intervals or taking tightly packed steps through stiff transients. 
            /// 
            /// Both `OdeResult` and `DdeResult` package discrete solution vectors alongside internally evaluated derivatives and stage slopes. This enables :math:`C^1`-continuous Hermite spline interpolation over the entire integration domain via the `Deval` function without re-integrating the equations.
            /// 
            /// * **Continuous Evaluation of States** :math:`\mathbf{y(t)}`:
            /// Pass the solution object and query times (either a scalar `double` or a collection/vector of points) into `Deval`:
            /// <signature>
            /// OdeResult sol = Ode45(odefun, y0, [0.0, 10.0], opts);
            /// 
            /// // Query at a specific time point:
            /// ColVec yMid = Deval(sol, 2.35);
            /// 
            /// // Query over a uniformly dense grid:
            /// ColVec tQuery = Linspace(0, 10, 500);
            /// Matrix yQuery = Deval(sol, tQuery);
            /// </signature>
            /// 
            /// * **Hermite Splines and Second-Order Slopes for Implicit Systems** (`i` **Family**):
            /// For fully implicit differential systems (:math:`F(t, y, y') = 0`), `OdeResult` retains second-order derivative slopes (:math:`y''`). This provides smooth, stable Hermite interpolation for both the state trajectory :math:`y(t)` and its time-derivative trajectory :math:`y'(t)`:
            /// <signature>
            /// OdeResult impSol = Ode43i(impidfun, y0, yp0, [0.0, 5.0], opts);
            /// 
            /// // Joint evaluation of interpolated state and derivative matrices:
            /// (Matrix Y_eval, Matrix Yp_eval) = Deval(impSol, Linspace(0, 5, 200));
            /// </signature>
            /// 
            /// ---
            /// 
            /// <header 2> Controlling the Output Mesh via `tspan` </header>
            /// 
            /// The integration interval parameter `tspan` dictates the temporal span as well as whether internal adaptive steps or user-specified query points are reported.
            /// 
            /// * **Adaptive Step Output (Interval Endpoints):**
            /// When `tspan` is supplied as a two-element array `[t0, tf]`, the integrator steps adaptively according to the specified error tolerances (`RelTol`, `AbsTol`) and returns the solution at every internally accepted time step:
            /// <signature>
            /// double[] tspan = [0.0, 10.0];
            /// (ColVec T, Matrix Y) = Ode45(odefun, y0, tspan, opts);
            /// </signature>
            /// 
            /// * **Fixed Query Points** (Dense Output / Specific Evaluation Points):
            /// When `tspan` contains three or more monotonically spaced points, the integrator continues to step adaptively behind the scenes to preserve stability and accuracy, but internally calls dense interpolation to evaluate and return the solution exactly at the requested nodes:
            /// <signature>
            /// double[] tspan = [0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0];
            /// (ColVec T, Matrix Y) = Ode45(odefun, y0, tspan, opts);
            /// </signature>
            /// This eliminates the need for manual post-process interpolation when generating uniform plots, comparing numerical solutions against analytical benchmarks, or interfacing with discrete digital control loops.
            /// 
            /// </BookContent>
        }
    }
}
