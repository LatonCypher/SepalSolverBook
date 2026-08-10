using SepalSolver;
using static CSharpMath.Rendering.Text.TextAtom;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_09_Numerical_Optimization
{
    public class Section_3_NonLinear_Optimization
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// <header 3> Rosenbrook funcion with constraint </header>
            /// 
            /// The goal is to find the parameter vector :math:`\mathbf{x} = [x_0, x_1]^T` that 
            /// minimizes the non - convex Rosenbrock objective function:
            /// 
            /// <math>
            /// \min_{\mathbf{ x} } f(x_0, x_1) = 100(x_1 - x_0 ^ 2) ^ 2 + (1 - x_0) ^ 2
            /// </math>
            /// 
            /// subject to the non-linear inequality constraint restricting the domain to the unit disk:
            /// <math>
            /// g(\mathbf{ x}) = x_0 ^ 2 + x_1 ^ 2 - 1 \le 0
            /// </math>
            /// 
            /// * **Initial Guess * *: :math:`\mathbf{ x}_0 = (0, 0)`
            /// 
            /// <code>
            {
                Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
                double[] x0 = [0, 0];
                var result = Fmincon(fun, x0, x => Pow(x[0], 2) + Pow(x[1], 2) - 1);
                Console.WriteLine(result);
            }
            /// </code>
            /// 
            /// <header 3> Rosenbrook funcion with constraint, Lower and Upperbound </header>
            /// <code>
            {
                // Define the quadratic objective function
                double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);

                // Define Inequality constraint
                ColVec Ineqconstraints(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;

                double[] lb = [0.0, 0.2], ub = [0.5, 0.8], x0 = [0.25, 0.25];

                // Solve the optimization problem
                var result = Fmincon(fun, x0, Ineqconstraints, null, lb, ub);
                Console.WriteLine(result);
            }
            /// </code>
            ///
            /// <header 3> Rosenbrock function with constraint </header>
            ///
            /// The goal is to find the parameter vector :math:\mathbf{x} = [x_0, x_1]^T that
            /// minimizes the non-convex Rosenbrock objective function:
            ///
            /// 
            /// \min_{\mathbf{x}} f(x_0, x_1) = 100(x_1 - x_0^2)^2 + (1 - x_0)^2
            /// 
            ///
            /// subject to the non-linear inequality constraint restricting the domain to the unit disk:
            ///
            /// 
            /// g(\mathbf{x}) = x_0^2 + x_1^2 - 1 \le 0
            /// 
            ///
            /// * Initial Guess: :math:\mathbf{x}_0 = (0, 0)
            ///
            /// <code>
            {
                Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
                double[] x0 = [0, 0];
                var result = Fmincon(fun, x0, x => Pow(x[0], 2) + Pow(x[1], 2) - 1);
                Console.WriteLine(result);
            }
            /// </code> 
            ///
            /// <header 3> Rosenbrock function with constraint, Lower and Upperbound </header>
            ///
            /// Minimizes the Rosenbrock objective subject to a shifted circular inequality constraint combined with explicit lower (lb) and upper (ub) parameter boundaries:
            ///
            /// 
            /// g(\mathbf{x}) = (x_0 - 0.333)^2 + (x_1 - 0.333)^2 - 0.11111 \le 0
            /// 
            ///
            /// 
            /// 0.0 \le x_0 \le 0.5, \quad 0.2 \le x_1 \le 0.8
            /// 
            ///
            /// <code>
            {
                // Define the objective function
                double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
                // Define Inequality constraint
                ColVec Ineqconstraints(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;

                double[] lb = [0.0, 0.2], ub = [0.5, 0.8], x0 = [0.25, 0.25];

                // Solve the constrained optimization problem
                var result = Fmincon(fun, x0, Ineqconstraints, null, lb, ub);
                Console.WriteLine(result);
            }
            /// </code>
            ///
            /// <header 3> Unconstrained Derivative-Free Optimization with Fminsearch </header>
            ///
            /// When gradient information is unavailable or the objective is non-differentiable, Fminsearch uses the Nelder-Mead Simplex algorithm to locate the unconstrained global minimum at :math:\mathbf{x}^* = (1, 1) where :math:f(\mathbf{x}^*) = 0.
            ///
            /// * Initial Guess: :math:\mathbf{x}_0 = (-1.2, 1.0)
            ///
            /// <code>
            {
                // Define unconstrained objective function
                Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
                double[] x0 = [-1.2, 1.0];

                // Solve using Nelder-Mead direct search
                var result = Fminsearch(fun, x0);
                Console.WriteLine(result);
            }
            /// </code>
            ///
            /// <header 3> Global Stochastic Optimization with Genetic Algorithm </header>
             ///
             /// For non-convex or multimodal objective functions where gradient-based solvers risk getting trapped in local minima, the Genetic Algorithm (GA) uses population-based operators to explore bounded search spaces without requiring an initial guess.
             ///
             /// 
             /// -2.0 \le x_0 \le 2.0, \quad -2.0 \le x_1 \le 2.0
             /// 
             ///
             /// <code>
            {
                // Define the objective function
                Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);

                double[] lb = [-2.0, -2.0];
                double[] ub = [2.0, 2.0];

                // Configure GA options
                var opts = OptimSet(PopulationSize: 100, MaxIter: 200);

                // Solve for global minimum within bounds
                var result = Ga(fun, lb: lb, ub: ub, options: opts);
                Console.WriteLine(result);
            }
            /// </code>
            ///
            ///
            /// </BookContent>

        }
    }
}
