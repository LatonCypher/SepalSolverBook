using static SepalSolver.Ode;
using static SepalSolver.Statistics;

namespace ConsoleApp1.TrainingFiles.Chapter_06_Solution_of_Nonlinear_System
{
    internal class Section_3_Large_NonLinear_Systems
    {
        public static void Run()
        {
            /// <BookContent>
            /// Solving large nonlinear systems of equations is a central problem in numerical
            /// analysis. Iterative methods, particularly Newton’s method, are widely employed
            /// due to their rapid convergence properties. At the core of these methods lies
            /// the Jacobian matrix, which encodes the local sensitivity of the system of
            /// equations to its variables.
            /// 
            /// <header 2> Mathematical Definition </header>
            /// For a system of equations :math:`F(x) = 0`, with :math:`F: \mathbb{R}^n \to \mathbb{R}^n`, the Jacobian is defined as
            /// <math>
            /// J(x) =
            /// \begin{bmatrix}
            ///     \cfrac{\partial f_1}{\partial x_1} & \cdots & \cfrac{\partial f_1}{\partial x_n} \\
            ///     \vdots & \ddots & \vdots \\
            ///     \cfrac{\partial f_n}{\partial x_1} & \cdots & \cfrac{\partial f_n}{\partial x_n}
            /// \end{bmatrix}.
            /// </math>
            /// 
            /// <header 2> Finite Difference Approximation </header>
            /// When analytic derivatives are unavailable, the Jacobian can be approximated using finite differences. For the :math:`j`-th column, this takes the form
            /// <math>
            ///     J_{:,j}(x) \approx \cfrac{F(x + h e_j) - F(x)}{h},
            /// </math>
            /// where :math:`h` is a small perturbation and :math:`e_j` is the unit vector in the :math:`j`-th direction.
            /// 
            /// <header 2> Newton’s Method Update </header>
            /// Newton’s method then updates the solution iteratively as
            /// 
            /// <math>
            ///     x_{k+1} = x_k - J(x_k)^{-1} F(x_k).
            /// </math>
            /// 
            /// 
            /// <header 3> Examples 1: Solving Large Sparse Systems </header>
            /// This example shows how to use features of the fsolve solver to solve large sparse systems of equations effectively. The example uses the objective function, defined for a system of n equations,
            /// <math>
            /// \begin{array}{rcl}
            ///     F(1) &=& 3x_1 − 2x_1^2 - 2x_2 + 1 \\
            ///     F(i) &=& 3x_i − 2x_i^2 - x_{i-1} - 2x_{i+1} + 1 \\
            ///     F(n) &=& 3x_n − 2x_n^2 - x_{n-1} + 1 \\
            /// \end{array}
            /// </math>
            /// The equations to solve are :math:`F_i(x) = 0, 1 \leq i \leq n`.The example uses n = 1000.
            /// <code>
            {
                //Large Nonlinear Systems
                int n = 1000;

                ColVec b = Ones(n), xstart;

                ColVec nlsf(ColVec x)
                {
                    ColVec F = new double[n];
                    F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
                    F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
                    F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
                    return F;
                }

                xstart = -b;
                var opts = SolverSet(Display: true);
                tic();
                var x = Fsolve(nlsf, xstart, opts);
                Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
                Console.WriteLine(opts.ans.FunVal.Norm());
                Console.WriteLine($"Elapsed time: {toc()} seconds");
            }
            /// </code>
            /// 
            /// While finite difference approximations are convenient, they are computationally 
            /// expensive, introduce numerical errors, and fail to exploit structural
            /// properties such as sparsity. Analytic Jacobians, or those obtained via
            /// automatic differentiation, provide greater accuracy, stability, and efficiency,
            /// making them indispensable for large-scale nonlinear systems.
            /// 
            /// <header 3> Examples 2: Solving Large Sparse Systems Using Sparsity Pattern Exploitation </header>
            /// 
            /// <code>
            {
                //Large Nonlinear Systems
                int n = 1000;
                Range i = 0..n, j = 0..(n - 1), jp1 = 1..n;

                ColVec e = Ones(n), xstart = -e;

                ColVec nlsf(ColVec x)
                {
                    ColVec F = new double[n];
                    F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
                    F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
                    F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
                    return F;
                }
                List<int> K = [-1, 0, 1];
                List<ColVec> Diagonals = [e, e, e];
                SparseMatrix Jpattern = Spdiags(Diagonals, K, n);

                var opts = SolverSet(Display: true, Jpattern: Jpattern);
                tic();
                var x = Fsolve(nlsf, xstart, opts);
                Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
                Console.WriteLine(opts.ans.FunVal.Norm());
                Console.WriteLine($"Elapsed time: {toc()} seconds");
            }
            /// </code>
            /// 
            /// <header 3> Examples 3: Solving Large Sparse Systems Using Analytical Jacobians </header>
            /// 
            /// <code>
            {
                //Large Nonlinear Systems
                int n = 1000;
                Range i = 0..n, j = 0..(n - 1), jp1 = 1..n;

                ColVec a = Ones(n - 1), b = Ones(n), e = -a,
                    c = 2 * e, d, xstart;

                ColVec nlsf(ColVec x)
                {
                    ColVec F = new double[n];
                    F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
                    F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
                    F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
                    return F;
                }

                SparseMatrix C, D, E;
                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    d = -4 * x + 3 * b;
                    D = new(i, i, d, n, n);
                    C = new(j, jp1, c, n, n);
                    E = new(jp1, j, e, n, n);
                    return C + D + E;
                };

                xstart = -b;
                var opts = SolverSet(Display: true, UserDefinedJac: Jac);
                tic();
                var x = Fsolve(nlsf, xstart, opts);
                Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
                Console.WriteLine(opts.ans.FunVal.Norm());
                Console.WriteLine($"Elapsed time: {toc()} seconds");
            }
            /// </code>
            /// 
            /// 
            /// <header 3> Examples 4: Solving Large Sparse Systems Using Analytical Jacobians </header>
            /// Multirosenbrook function is another example
            /// 
            /// <math>
            ///     \begin{array}{rcl}
            ///         F_{2n} &=& 1 - x_{2n} \\
            ///         F_{2n+1} &=& 10(x_{2n + 1} - x_{2n}^2)
            ///     \end{array}
            /// </math>
            /// 
            /// <code>
            {
                // Large Nonlinear systems
                int n = 1000;

                ColVec multirosenbrook(ColVec x)
                {
                    // Evaluate the vector function
                    ColVec F = new double[n], 
                        x2n = x[(0..n).Step(2)], 
                        x2np1 = x[(1..n).Step(2)];

                    F[(0..n).Step(2)] = 1 - x2n;
                    F[(1..n).Step(2)] = 10 * (x2np1 - x2n.Pow(2));
                    return F;
                }

                SparseMatrix C, D, E;
                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    ColVec one = Ones(n/2), x2n = x[(0..n).Step(2)];
                    C = new((0..n).Step(2), (0..n).Step(2), -one, n, n);
                    D = new((1..n).Step(2), (1..n).Step(2), 10*one, n, n);
                    E = new((1..n).Step(2), (0..n).Step(2), -20 * x2n, n, n);
                    return C + D + E;
                };

                ColVec xstart = new double[n];
                xstart[(0..n).Step(2)] = -1.9; xstart[(1..n).Step(2)] = 2;
                var opts = SolverSet(Display: true, UserDefinedJac: Jac);
                tic();
                var x = Fsolve(multirosenbrook, xstart, opts);
                Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
                Console.WriteLine(opts.ans.FunVal.Norm());
                Console.WriteLine($"Elapsed time: {toc()} seconds");
            }
            /// </code>
            /// </BookContent>
        }
    }
}
