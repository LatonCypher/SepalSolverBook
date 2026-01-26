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
            ///     - ``fun`` : Function handle that returns a vector of equations.
            ///     - ``x0``  : Initial guess for the solution vector.
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
            /// Solve: math:`x^2 - 4 = 0`:
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
            /// \begin{ cases}
            ///     3x_1 - \cos(x_2 x_3) - \frac{1}{2} = 0 \\
            ///     x_1^2 - 81(x_2+0.1)^2 + \sin(x_3) + 1.06 = 0 \\ 
            ///     e^{ x_1x_2} +20x_3 + \frac{ 10\pi-3}{ 3} = 0
            /// \end{ cases}
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
            ///  <header 2> Summary </header>
            ///  ``Fsolve`` is SelapSolver’s go-to tool for solving nonlinear systems. It is powerful and flexible, but demands careful choice of initial guesses and problem formulation to ensure convergence.
            /// 
            /// </BookContent>
        }
    }
}

