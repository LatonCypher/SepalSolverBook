using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_06_Solution_of_Nonlinear_System
{
    internal class Section_2_NonLinear_System
    {
        public static void Run()
        {
            /// <BookContent>
            /// The SepalSolver function ``Fsolve`` is used to solve **systems of nonlinear equations**. 
            /// It finds a vector :math:`\mathbf{x}` such that:
            /// <math>
            /// \mathbf{f}(\mathbf{x}) = \mathbf{0}
            /// </math>
            /// 
            /// Unlike ``Fzero``, which works for single-variable equations, ``Fsolve`` is designed for multivariable problems.
            /// 
            /// <header 2> Syntax </header>
            /// The basic syntax is:
            /// <code>
            ///{    
            ///     x = Fsolve(fun, x0);
            ///}
            /// </code>
            /// Where:
            /// 
            ///  - ``fun`` : Function handle that returns a vector of equations.
            ///  - ``x0``  : Initial guess for the solution vector.
            ///     
            /// 
            /// Just like the case of `Fzero`, we can use `SolverSet` to configure the solver and gain a window into what is going on under the hood. 
            /// <code>
            /// {
            ///     var options = SolverSet(Display: true);
            ///     x = fsolve(fun, x0, options);
            /// }
            /// </code>
            /// 
            /// <header 2> How fsolve Works </header>
            /// - ``fsolve`` uses iterative numerical methods such as:
            /// - **Newton Raphson Algorithm** (default, robust for many problems).
            /// - **Forward Differencing** for Numerical differentiation of the function
            /// - **LU rank 1 update to directly update the LU factors reducing the neet for repeated factorization.
            /// - It requires a **good initial guess** because nonlinear systems may have multiple solutions or none at all.
            /// 
            /// <header 2> Examples </header>
            ///
            /// <example 1> : Single Equation
            /// Solve: :math:`x^2 - 4 = 0`:
            /// 
            /// <code>
            {
                double fun(double x) => x*Sin(x) - 0.5;
                double x0 = 1;
                double root = Fsolve(fun, x0);
                Console.WriteLine($"root = {root}");
            }
            /// </code>
            /// 
            /// </example>
            /// 
            /// <example 2> System of Equations
            /// Solve the system:
            /// <math>
            /// \begin{array}{c}
            ///     3x_1 - \cos(x_2 x_3) - \cfrac{1}{2} = 0 \\
            ///     x_1^2 - 81(x_2+0.1)^2 + \sin(x_3) + 1.06 = 0 \\ 
            ///     e^{x_1x_2} +20x_3 + \cfrac{10\pi-3}{3} = 0
            /// \end{array}
            /// </math>
            /// Where: :math:`x_0 = [0.1, 0.1, -0.1]^T`
            ///
            /// <code>
            {
                double[] fun(double[] x) => [3 * x[0] - Cos(x[1] * x[2]) - 0.5,
                                             x[0] * x[0] - 81*Pow(x[1] + 0.1, 2) + Sin(x[2]) + 1.06,
                                             Exp(-x[0] * x[1]) + 20 * x[2] + (10 * pi - 3) / 3];
                // set initial guess
                double[] x0 = [0.1, 0.1, -0.1];

                // call the solver
                var x = Fsolve(fun, x0);

                // display the result
                Console.WriteLine(x);
            }
            /// </code>
            /// 
            /// </example>
            /// 
            /// <header 2> Applications </header>
            /// - Engineering: Nonlinear circuit analysis, chemical equilibrium.
            /// - Physics: Solving coupled nonlinear equations in dynamics.
            /// - Optimization: Finding stationary points of nonlinear functions.
            /// 
            /// <header 2> Limitations </header>
            /// - Requires a **good initial guess**; poor guesses may lead to divergence.
            /// - May converge to **local solutions** rather than global ones.
            /// - Sensitive to scaling of equations.
            /// 
            /// <header 2> Comparison with fzero </header 2>
            /// <table>
            ///  Feature         | ``fzero``                 | ``fsolve``  
            ///  Problem type    | Single nonlinear equation | System of nonlinear equations  
            ///  Input           | Function handle, scalar or interval  | Function handle, vector initial guess
            ///  Methods used    | Bisection, secant, inverse quadratic interp  | Newton-Raphson's method
            ///  Output          | Scalar root  | Vector solution  
            ///  </table>
            ///  
            /// <header 2> Summary </header>
            ///  ``Fsolve`` is SelapSolver’s go-to tool for solving nonlinear systems. It is powerful and flexible, but demands careful choice of initial guesses and problem formulation to ensure convergence.
            ///  
            /// <header 2> Parameterized Equations </header>
            /// Parameterized nonlinear equations :math:`F(x, \lambda) = 0` are equations or systems of equations that depend on one or more parameters: math:`\lambda`. They are widely used in mathematics, engineering, and economics to study how solutions change as parameters vary, enabling sensitivity analysis, bifurcation studies, and optimization.
            /// 
            /// This parameter(s) can be exploited to provide means to guarantee that a good initial guess can be estimated. For instance, some values of the parameter might help eliminate the nonlinearity of the system and hence, no guess is needed for the solution. Then variation of this parameter can then be used to move the solution :math:`x` gently to their values that corresponds to the orginally intended values of the parameter :math:`\lambda`.
            /// 
            /// <example 2>
            /// Consider this parameterized nonlinear system. The nonlinearity is controlled by parameter :math:`c`.
            /// 
            /// <math>
            /// \begin{array}{c}
            ///     2x + y - \exp(-cx) = 0 \\
            ///    -x + 2y - \exp(-cy) = 0
            /// \end{array}
            /// </math>
            /// 
            /// Setting :math:`c = 0`, turns this system into a linear system with solution of :math:`[x,y] = [0.2, 0.6]`
            /// Hence, we can gradually change :math:`c` from :math:`0` to :math:`20`, while solving for :math:`[x, y]`.
            /// <code>
            {
                // Parameterized nonlinear equations
                double[] paramfun(ColVec x, double c)
                {
                    return [ 2*x[0] + x[1]  - Exp(-c*x[0]),
                    -x[0] + 2*x[1]  - Exp(-c*x[1])];
                }

                // variatiob of c from 0 to 20.
                RowVec C = Linspace(0, 20, 200);

                // initial guess as solution of linear system when c = 0.
                ColVec x = new double[] { 0.2, 0.6 };

                // setting maximum iteration number
                var opts = SolverSet(MaxIter: 1000);
                Matrix X = C.Select(c => x = Fsolve(x => paramfun(x, c), x, opts)).ToList();
                Plot(C, X, Linewidth: 2);
                SaveAs("Parameterozed_Nonlinear_Equations.png");
            }
            /// </code>
            /// 
            /// </example>
            /// 
            /// <header 2> Matrix Equation </header >
            /// The SepalSolver also allow for easy computation of matrix equations. For instance, we can easily compute the cuberoot of a matrix. :math:`x^3 = \begin{pmatrix} 1&2 \\ 3&4  \end{pmatrix}`;
            /// <example 3>
            /// <math>
            ///     x^3 = \begin{pmatrix} 1&2 \\ 3&4  \end{pmatrix}
            /// </math>
            /// <code>
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
            /// </code>
            /// </example>
            /// </BookContent>
        }
    }
}

