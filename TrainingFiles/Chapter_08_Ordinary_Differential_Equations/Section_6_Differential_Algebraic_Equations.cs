using ScottPlot.Hatches;

namespace ConsoleApp1.TrainingFiles.Chapter_08_Ordinary_Differential_Equations
{
    internal class Section_6_Differential_Algebraic_Equations
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// <header 2> 1. Introduction to DAEs </header>
            /// Differential-Algebraic Equations are a class of functional equations that contain both differential equations (describing the evolution of the system) and algebraic constraints (restricting the state space). Unlike standard Ordinary Differential Equations (ODEs), DAEs are not explicitly solved for all derivatives.
            ///
            /// A general DAE system is expressed in the implicit form: :math:`F(t, y, y') = 0`
            ///
            /// If the Jacobian :math:`\frac{\partial F}{\partial y'}` is non-singular, the system is essentially an implicit ODE. If it is singular, the system is a "true" DAE.
            ///
            /// <header 2> 2. The Concept of Index </header>
            /// The difficulty of solving a DAE is measured by its **index**. The most common definition is the **differentiation index**: the number of times you must differentiate the algebraic constraints to express the system as a set of explicit ODEs.
            /// * **Index 0:** An ODE.
            /// * **Index 1:** The most common solvable DAE (e.g., the algebraic variables can be solved for directly).
            /// * **Higher Index (2+):** These are numerically unstable and usually require index reduction techniques before solving.
            ///
            ///
            ///
            /// <header 2> 3. Solving DAEs with `sepalsolver` </header>
            /// In modern computational environments like C#, DAEs can be solved using the `SepalSolver` library, which utilizes a Mass Matrix formulation:
            /// :math:`M y' = f(t, y)`
            /// Where :math:`M` is a singular matrix.
            ///
            /// ---
            ///
            /// <header 2> 4. Examples and Applications </header>
            ///
            /// <example 1>
            /// **Example 1: The Robertson Problem (Chemical Kinetics)**
            /// This is a classic stiff DAE representing the reaction of three species. It is an Index-1 DAE where the total mass is conserved via an algebraic constraint.
            /// 
            ///
            /// <code>
            {
                double[] robertson_f(double t, double[] y) =>
                    [(-0.04 * y[0] + 1e4 * y[1] * y[2]),
                     (0.04 * y[0] - 1e4 * y[1] * y[2] - 3e7 * y[1]*y[1]),
                     y[0] + y[1] + y[2] - 1.0];

                double[,] mass_f = Diag([1, 1, 0]);

                double[] y0 = [1.0, 0.0, 0.0];
                (ColVec T, Matrix Y) = Ode43a(robertson_f, mass_f, y0, [0, 1e7]);
                // Plot the result
                Y[.., 1] = 1e4*Y[.., 1];
                SemiLogx(T, Y);
                Xlabel("Time t"); Ylabel("Soluton y");
                Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
                Title("Solution of Robertson's ODE with ODE43a");
                SaveAs("Robertson-ODE-given-points-Ode43a.png");
            }
            /// </code>
            /// </example>
            ///
            /// <example 1> The Simple Pendulum (Index-1)
            /// A pendulum in Cartesian coordinates is naturally an Index-3 DAE. We solve the stabilized Index-1 version by including velocity constraints.
            ///
            /// The position of the pendulum :math:`(x, y)` must satisfy the rigid rod constraint: 
            /// :math:`x^2 + y^2 - 1 = 0`
            ///
            /// **The Index-1 Formulation**
            /// To reduce the index, we differentiate the constraint twice. The second derivative introduces the accelerations :math:`x''` and :math:`y''`, allowing us to solve for the Lagrange multiplier :math:`\lambda` (tension).
            ///
            /// The resulting Index-1 system is:
            /// <math> 
            /// \begin{array}{rcl}
            ///     x' &=& u \\
            ///     y' &=& v \\
            ///     u' &=& -\lambda x \\
            ///     v' &=& -\lambda  y - g \\
            ///     0 &=& u^2 + v^2 - y g - \lambda
            /// \end{array}    
            /// </math>
            ///
            ///
            /// <code>
            {
                double g = 9.81;

                // State vector y = [x, y, u, v, λ]
                double[] pendulum_f(double t, double[] y) =>
                    [y[2],
                     y[3],
                     -y[0] * y[4],
                     -y[1] * y[4] - g,
                     y[2]*y[2] + y[3]*y[3] - y[1] * g - y[4]];

                double[,] mass_f = Diag([1, 1, 1, 1, 0]);

                double[] y0 = [0, 1, 1, 0, 1 - g];
                var opts = Odeset(Stats: true, RelTol: 1e-6);
                (ColVec T, Matrix Y) = Ode43a(pendulum_f, mass_f, y0, [0, 6], opts);
                Plot(T, Y, Linewidth: 2); Xlabel("x"); Ylabel("y");
                Legend(["x", "y", "u", "v", "λ"]);
                Title("Pendulum Trajectory (DAE)");
                SaveAs("Index_1-Pendulum-Problem-Ode43a.png");
            }
            /// </code>
            /// </example>
            /// 
            /// As an exercise, the reader is encouraged to solve the problem using 
            /// this initial condition y0 = [1, 0, 0, 1, 1];
            /// 
            ///
            /// <example 2> Semi-Explicit DAE (The Transistor Amplifier)**
            /// This example mimics the "hbdae" problem from MathWorks, representing an electrical circuit with nonlinear components.
            /// 
            /// The transistor amplifier circuit contains six resistors, three capacitors, and a transistor.
            /// 
            /// <figure> Transistor.png </figure>
            /// 
            /// - The initial voltage signal is :math:`U_e(t) = 0.4\sin(200\pi t)`.
            /// - The operating voltage is :math:`U_b = 6`.
            /// - The voltages at the nodes are given by: :math:`U_i(t)(i = 1, 2, 3, 4, 5)`.
            /// - The values of the resistors  :math:`R_i(t)(i = 1, 2, 3, 4, 5)`. are constant, and the current through each resistor satisfies :math:`I = U/R`.
            /// - The values of the capacitors :math:`C_i(i = 1, 2, 3)` are constant, and the current through each capacitor satisfies :math:`I=C⋅dU/dt`.
            /// 
            /// The goal is to solve for the output voltage through node 5, :math:`U_5(t)`.
            /// Using Kirchoff's law to equalize the current through each node (1 through 5), you can obtain a system of five equations describing the circuit:
            /// 
            /// Node 1: :math:`C_1(U'_2 - U'_1) = (U_1 - U_e(t))/R_0`
            /// 
            /// Node 2: :math:`C_1(U'_1 - U'_2) = (U_2 - U_b)/R_1 + U_2/R_1 + 0.01f(U_2 - U_3)`
            /// 
            /// Node 3: :math:`-C_2U'_3 = U_3/R_3 - f(U_2 - U_3)`
            /// 
            /// Node 4: :math:`C_3(U'_5 - U'_4) = (U_4 - U_b)/R_4 + 0.99f(U_2 - U_3)`
            /// 
            /// Node 5: :math:`C_3(U'_4 - U'_5) = U_5/R_5`
            /// 
            /// By extracting the coeeficients of the derivatives into a matrix, we have:
            /// <math>
            ///     \begin{pmatrix}
            ///         -c_{1} &  c_{1} &     0    &    0     &     0    \\
            ///          c_{1} & -c_{1} &     0    &    0     &     0    \\
            ///           0    &   0    & -c_{ 2}  &    0     &     0    \\
            ///           0    &   0    &    0     & -c_{ 3}  &  c_{ 3}  \\
            ///           0    &   0    &    0     &  c_{ 3}  & -c_{ 3}
            ///     \end{pmatrix}
            ///     \begin{pmatrix}
            ///         U'_1 \\  U'_2 \\ U'_3 \\ U'_4 \\ U'_5 
            ///     \end{pmatrix} = 
            ///     \begin{pmatrix}
            ///         (U_1 - U_e(t))/R_0 \\  
            ///         (U_2 - U_b)/R_1 + U_2/R_1 + 0.01f(U_2 - U_3) \\ 
            ///         U_3/R_3 - f(U_2 - U_3) \\ 
            ///         (U_4 - U_b)/R_4 + 0.99f(U_2 - U_3) \\ 
            ///         U_5/R_5
            ///     \end{pmatrix}
            /// </math>
            ///
            /// <code>
            {
                double Ub = 6, R0 = 1000, R15 = 9000, alpha = 0.99,
                    beta = 1e-6, Uf = 0.026, c1 = 1e-6, c2 = 2e-6, c3 = 3e-6;
                double[,] Mass = new double[,]
                {
                    {-c1,  c1,  0,   0,   0 },
                    { c1, -c1,  0,   0,   0 },
                    { 0,   0,  -c2,  0,   0 },
                    { 0,   0,   0,  -c3,  c3},
                    { 0,   0,   0,   c3, -c3}
                };
                double Ue(double t) => 0.4 * Sin(200 * pi * t);
                double[] dudt(double t, double[] u)
                {
                    double f23 = beta * (Exp((u[1] - u[2]) / Uf) - 1);
                    return [ -(Ue(t) - u[0])/R0,
                             -(Ub/R15 - u[1]*2/R15 - (1-alpha)*f23),
                             -(f23 - u[2]/R15),
                             -((Ub - u[3])/R15 - alpha*f23),
                             u[4]/R15 ];
                }
                double[] tspan = [0, 0.1];
                double[] y0 = [0, Ub / 2, Ub / 2, Ub, 0];

                var opts = Odeset(RelTol: 1e-5);
                (ColVec T, Matrix Y) = Ode43a(dudt, Mass, y0, tspan, opts);
                Scatter(T, Arrayfun(Ue, T), "o"); HoldOn();
                Plot(T, Y[.., 4], "--r"); HoldOff();
                Legend(["Input", "Output"], UpperLeft);
                Xlabel("Time t"); Ylabel("Solution y");
                Title("One Transistor Amplifier DAE Problem-Ode43a");
                SaveAs("One-Transistor-Amplifier-DAE-Problem-Ode43a.png");
            }
            /// </code>
            /// </example>
            ///
            /// <example 3> The Akzo Nobel Problem
            /// A high-dimensional DAE describing a chemical process with 6 differential and 2 algebraic equations. This tests the solver's ability to handle stiff systems with coupled variables.
            ///
            /// **Mathematical Description:**
            /// The system is defined by reaction rates :math:`r_i` and concentrations :math:`y_1, ..., y_8`:
            /// 
            /// <math>
            ///     \begin{array}{rcl}
            ///         r_1 &=& k_1 \cdot y_1^4 \cdot y_2^{0.5}\\
            ///         r_2 &=& k_2 \cdot y_3 \cdot y_4 \\
            ///         r_3 &=& k_2 / K \cdot y_1 \cdot y_5\\
            ///         r_4 &=& k_3 \cdot y_1 \cdot y_4^2\\
            ///         r_5 &=& k_4 \cdot y_6^2 \cdot y_2^{0.5}
            ///     \end{array}
            /// </math>
            ///
            /// The differential equations are:
            /// <math>
            ///     \begin{array}{rcl}
            ///         y_1' &=& -2r_1 + r_2 - r_3 - r_4\\
            ///         y_2' &=& -0.5r_1 - r_5 + 0.5F_{in}\\
            ///         y_3' &=& r_1 - r_2 + r_3\\
            ///         y_4' &=& -r_2 + r_3 - 2r_4\\
            ///         y_5' &=& r_2 - r_3 + r_4\\
            ///         y_6' &=& -r_5
            ///     \end{array}
            /// </math>
            ///
            /// The algebraic constraints (Equilibrium):
            /// <math>
            ///     \begin{array}{rcl}
            ///         0 &=& y_1 \cdot y_3 - y_7\\
            ///         0 &=& y_4 \cdot y_5 - y_8
            ///     \end{array}
            /// </math>
            /// 
            /// <code>
            {
                double k1 = 18.7, k2 = 0.58, k3 = 0.09, k4 = 0.42, K = 34.4, Fin = 0.012;
                double r1(double[] y) => k1 * Pow(y[0], 4) * Pow(y[1], 0.5);
                double r2(double[] y) => k2 * y[2] * y[3];
                double r3(double[] y) => (k2 / K) * y[0] * y[4];
                double r4(double[] y) => k3 * y[0] * Pow(y[3], 2);
                double r5(double[] y) => k4 * Pow(y[5], 2) * Pow(y[1], 0.5);

                double[] akzo_f(double t, double[] y) =>
                    [
                        -2*r1(y) + r2(y) - r3(y) - r4(y),
                        -0.5*r1(y) - r5(y) + 0.5*Fin,
                        r1(y) - r2(y) + r3(y),
                        -r2(y) + r3(y) - 2*r4(y),
                        r2(y) - r3(y) + r4(y),
                        -r5(y),
                        y[0] * y[2] - y[6],
                        y[3] * y[4] - y[7]
                    ];

                double[,] mass_f = Diag([1, 1, 1, 1, 1, 1, 0, 0]);
                double[] y0 = [0.444, 0.0012, 0.0, 0.0037, 0.0, 0.0, 0.0, 0.0];
                (ColVec T, Matrix Y) = Ode43a(akzo_f, mass_f, y0, [0, 180]);
                Plot(T, Y);
                Xlabel("Time"); Ylabel("Concentration");
                Title("Akzo Nobel Chemical Kinetics (DAE)");
                SaveAs("Akzo-Nobel-Ode43a.png");
            }
            /// </code>
            /// </example>
            /// 
            /// <header 2> Index-2 DAE </header>
            /// Most DAE solvers usually avoid solving DAEs in index 2 form. But SepalSolver is able to handle most index 2 DAEs to a relative tolerance of :math:`10^{-4}`.

            /// Now we look at examples of index 2 DAEs
            /// 
            /// <example 4> 
            /// Usnig the example from "On the numerical solution of differential–algebraic equations with index-2" by Ercan Celık
            /// <math>
            ///     \begin{align}
            ///         x'_1 &= \left(\alpha - \cfrac{1}{2 - t}\right)x_1 + (2 - t)\alpha z + \cfrac{3 - t}{2 - t}x_2 \\
            ///         x'_2 &= \cfrac{1 - \alpha}{t - 2} x_1 - x_2 + (\alpha - 1)z + 2e^t \\
            ///         0 &= (t + 2)x_1 + (t^2 - 4)x_2 - (t^2 + t - 2)e^t
            ///     \end{align}
            /// </math>
            /// 
            /// Intial condition: :math:`x_1(0) = 1, x_2(0) = 1`;
            ///  
            /// SepalSolver has the ability to compute consistent initial conditions for index 2 DAEs, so we can solve this problem without manually differentiating the algebraic constraint.
            /// 
            /// <code>
            {
                // define the DAE
                double alpha = 10;
                double[] Ercan(double t, double[] x) =>
                    [ (alpha - 1/(2-t))*x[0] + (2-t)*alpha*x[2] + (3-t)/(2-t)*x[1],
                      (1-alpha)/(t-2)*x[0] - x[1] + (alpha-1)*x[2] + 2*Exp(t),
                      (t+2)*x[0] + (t*t-4)*x[1] - (t*t+t-2)*Exp(t) ];

                double[,] mass_f = Diag([1, 1, 0]);
                double[] y0 = [1, 1, 0]; // only the differential variables need initial conditions
                var opts = Odeset(Stats: true);
                (ColVec T, Matrix Y) = Ode43a(Ercan, mass_f, y0, [0, 1], opts);
                Scatter(T, Hcart(Exp(T), Exp(T), -Exp(T).Div(2-T)), "o"); HoldOn();
                Plot(T, Y); HoldOff();
                Xlabel("Time t"); Ylabel("Solution x");
                Legend(["x_1_Exact", "x_2_Exact", "z_Exact", "x_1_NumSol", "x_2_NumSol", "z_NumSol"]);
                Title("Index-2 DAE Example (Ercan Celık)");
                SaveAs("Index-2-DAE-Ercan-Celik.png");

                // We can actually print out the result to compare with the analytical solution
                Console.WriteLine("""
                        t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
                    --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
                    """);
                for (int i = 0; i < T.Numel; i++)
                {
                    Console.WriteLine($"""
                          {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i])/(2-T[i]):F6}
                        """);
                }

                // We can compute the solution to a higher accuracy 
                Console.WriteLine("\n\nNow we compute the solution to a higher accuracy (RelTol = 1e-5):\n");
                opts = Odeset(Stats: true, RelTol: 1e-5);
                (T, Y) = Ode43a(Ercan, mass_f, y0, [0, 1], opts);
                Console.WriteLine("""
                        t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
                    --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
                    """);
                for (int i = 0; i < T.Numel; i++)
                {
                    Console.WriteLine($"""
                          {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i])/(2-T[i]):F6}
                        """);
                }
            }
            /// </code>
            /// 
            /// 
            /// </example>
            /// 
            /// <example 5> Pendulum position constraint (Index-2)
            /// To reduce the index, if we differentiated the constraint once instead of twice, we end up with index 2 problem. 
            ///
            /// The resulting Index-1 system is:
            /// <math> 
            ///     \begin{array}{rcl}
            ///         x' &=& u \\
            ///         y' &=& v \\
            ///         u' &=& -\lambda x \\
            ///         v' &=& -\lambda  y - g \\
            ///         0 &=& x u + y v
            ///     \end{array}    
            /// </math>
            ///
            ///
            /// <code>
            {
                double g = 9.81;

                // State vector y = [x, y, u, v, λ]
                double[] pendulum_f(double t, double[] y) =>
                    [y[2],
                     y[3],
                     -y[0] * y[4],
                     -y[1] * y[4] - g,
                     y[0]*y[2] + y[1]*y[3]];

                double[,] mass_f = Diag([1, 1, 1, 1, 0]);

                double[] y0 = [0, 1, 1, 0, -1];
                var opts = Odeset(Stats: true);
                (ColVec T, Matrix Y) = Ode43a(pendulum_f, mass_f, y0, [0, 6], opts);
                Plot(T, Y, Linewidth: 2); Xlabel("x"); Ylabel("y");
                Legend(["x", "y", "u", "v", "λ"]);
                Title("Pendulum Trajectory (DAE)");
                SaveAs("Index_2-Pendulum-Problem-Ode43a.png");

                Console.WriteLine("\n\n");
                Console.WriteLine(Hcart(T, Y));

            }
            /// </code>
            /// 
            /// Observe that the initial condition supplied for :math:`\lambda` was :math:`-1`; but the result returned shown that the correct initial condition for the algebraic variable :math:`\lambda` is :math:`-8.81`.
            /// Sending in a wrong initial condition was done on purpose, to test the ability of sepalsolver to compute the initial condition of the algebraic variable. 
            /// 
            /// </example>
            /// 
            /// <header> Solving Index 3 </header>
            /// To show more capability of the sepalsolver with higher index DAEs, we present this solution of the Pendulum equation from index 0 to index 3 below
            /// 
            /// % --- Index 0 ---
            /// <math>
            ///     \begin{array}{rcl}
            ///         \dot{x} &= u \\
            ///         \dot{y} &= v \\
            ///         \dot{u} &= -x \lambda \\
            ///         \dot{v} &= -y \lambda - g \\
            ///         \dot{\lambda} &= -2\lambda(xu + yv) - 3gv
            ///     \end{array}
            /// </math>
            /// 
            /// % --- Index 1 ---
            /// <math>
            ///     \begin{array}{rcl}
            ///         \dot{x} &= u \\
            ///         \dot{y} &= v \\
            ///         \dot{u} &= -x \lambda \\
            ///         \dot{v} &= -y \lambda - g \\
            ///         0 &= u^2 + v^2 - y g - \lambda
            ///     \end{array}
            /// </math>
            /// 
            /// % --- Index 2 ---
            /// <math>
            ///     \begin{array}{rcl}
            ///         \dot{x} &= u \\
            ///         \dot{y} &= v \\
            ///         \dot{u} &= -x \lambda \\
            ///         \dot{v} &= -y \lambda - g \\
            ///         0 &= x u + y v
            ///     \end{array}
            /// </math>
            /// 
            /// % --- Index 3 ---
            /// <math>
            ///     \begin{array}{rcl}
            ///         \dot{x} &= u \\
            ///         \dot{y} &= v \\
            ///         \dot{u} &= -x \lambda \\
            ///         \dot{v} &= -y \lambda - g \\
            ///         0 &= x^2 + y^2 - 1
            ///     \end{array}
            /// </math>
            /// 
            /// The result is assessed using these errors
            /// <math>
            ///     \begin{array}{rcl}
            ///         r &= |x^2 + y^2 - 1| \\
            ///         \epsilon &= |xu + yv| 
            ///     \end{array}
            /// </math>
            /// 
            /// <code>
            {
                double g = 9.81; ColVec T; Matrix Y;
                double[] y0 = [0, 1, 1, 0, 1 - g], interval = [0, 6];
                var opts = Odeset(Stats: true, RelTol: 1e-6);
                double[,] Mass = Diag([1, 1, 1, 1, 0]);
                Matrix Error(Matrix Y) => Hcart(Abs(Y[.., 0].Pow(2) + Y[.., 1].Pow(2) - 1),
                                                Abs(Y[.., 0].Times(Y[.., 2]) + Y[.., 1].Times(Y[.., 3])));
                void ResultPloter(ColVec T, Matrix Y, int index)
                {
                    Subplot(4, 2, 2 * index);
                    Plot(T, Y, Linewidth: 2); GridOn();
                    Xlabel("x"); Ylabel("y");
                    Legend(["x", "y", "u", "v", "λ"]);
                    Title($"Index_{index}_Pendulum Trajectory (DAE)");

                    Subplot(4, 2, 2 * index + 1);
                    SemiLogy(T, Error(Y), Linewidth: 2); GridOn();
                    Xlabel("x"); Ylabel("error");
                    Legend(["r", "ε"]);
                    Title($"Index_{index}_Pendulum Trajectory (DAE) errors");
                }

                // Index 0
                (T, Y) = Ode45((t, y) => [y[2], y[3], -y[0] * y[4], -y[1] * y[4] - g,
                        -2 * y[4] * (y[0] * y[2] + y[1] * y[3]) - 3 * g * y[3]],
                        y0, interval, opts); ResultPloter(T, Y, 0);
                // Index 1
                (T, Y) = Ode43a((t, y) => [y[2], y[3], -y[0] * y[4], -y[1] * y[4] - g,
                         y[2]*y[2] + y[3]*y[3] - y[1] * g - y[4]], Mass, 
                         y0, interval, opts); ResultPloter(T, Y, 1);
                // Index 2
                (T, Y) = Ode43a((t, y) => [y[2], y[3], -y[0] * y[4], -y[1] * y[4] - g,
                         y[0]*y[2] + y[1]*y[3]], Mass, 
                         y0, interval, opts); ResultPloter(T, Y, 2);
                // Index 3
                (T, Y) = Ode43a((t, y) => [y[2], y[3], -y[0] * y[4], -y[1] * y[4] - g,
                         y[0]*y[0] + y[1]*y[1] - 1], Mass, 
                         y0, interval, opts); ResultPloter(T, Y, 3);

                SaveAs("Pendulum-Problem-Ode43a.png", 1200, 1800);
                CloseFig();
            }
            /// </code>
            /// 
            /// </BookContent>
        }
    }
}

