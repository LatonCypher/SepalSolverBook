using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_8_Numerical_Optimization
{
    public class Section_3_NonLinear_Optimization
    {
        public void Run()
        {
            // Define the quadratic objective function
            double fun(ColVec x) => 100 * Pow(x[1] - x[0]*x[0], 2) + Pow(1 - x[0], 2);

            // Define Inequality constraint
            ColVec Ineqconstraints(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;

            ColVec lb = new double[] { 0, 0.2 };
            ColVec ub = new double[] { 0.5, 0.8 };

            ColVec x0 = new double[] { 0.25, 0.25 };

            // Solve the optimization problem
            var solution = Fmincon(fun, x0, Ineqconstraints, null, lb, ub);
            Console.WriteLine($"Optimized Decision Variables: {solution.x.T}");
        }
    }
}
