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
                ColVec x0 = new double[] { 0, 0 };
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
                var solution = Fmincon(fun, x0, Ineqconstraints, null, lb, ub);
                Console.WriteLine($"Optimized Decision Variables: {solution.x.T}");
            }
            /// </code>
            /// 
            /// 
            /// </BookContent>

        }
    }
}
