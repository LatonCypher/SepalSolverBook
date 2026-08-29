namespace ConsoleApp1.TrainingFiles.Chapter_01_Basic_Operations_Syntax
{
    public class Section_05_Algorithm_Flow_Control
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> Algorithm Flow Control </header 2>
            /// Algorithms are rarely linear. In numerical analysis, we must make decisions
            /// based on error thresholds and repeat operations until a solution converges.
            /// Flow control allows us to dictate the path our code takes using conditional
            /// logic and iterative loops.
            ///
            /// <header 3> 1. Conditional Logic (if-else and switch) </header 3>
            /// Decisions in C# are handled primarily by `if` and `switch` statements.
            /// While `if` is perfect for range-based checks (like error tolerances),
            /// `switch` is ideal for choosing between discrete "modes" or "states"
            /// of an algorithm.
            ///
            ///
            ///
            /// <code> Selection Logic in Solvers
            {
                // if-else: used for logical ranges
                double residual = 1e-5;
                if (residual < 1e-6) { /* Converged */ }


                // switch: used for distinct algorithm modes
                string solverType = "Linear";
                switch (solverType)
                {
                    case "Linear": /* Use Direct Solver */ break;
                    case "NonLinear": /* Use Newton-Raphson */ break;
                    default: /* Use Default */ break;
                }

            }
            /// </code>
            ///
            /// <header 3> 2. Iterative Loops </header 3>
            /// Loops allow us to repeat a block of code. For numerical methods, the `for`
            /// loop is king when we know the number of iterations, while the `while`
            /// loop is used when we wait for a specific condition to be met.
            ///
            /// [Image comparison of for-loop, while-loop, and foreach-loop structures]
            ///
            /// <table> Loop Types in C#
            ///  Loop Type  | Best Use Case                                      | Key Characteristic
            ///  for        | Stepping through matrices with an index.           | Precise index control.
            ///  foreach    | Iterating over all nodes in a mesh.                | Read-only and safe.
            ///  while      | Running a solver until convergence.                | Condition-based exit.
            ///  do-while   | When the first iteration must occur.               | Post-condition check.
            /// </table>
            ///
            /// <header 2> Examples </header 2>
            ///
            /// <example 1> The If-Condition: Safety Checks
            /// Before performing a division in a numerical formula, such as normalizing
            /// a vector, it is vital to ensure we aren't dividing by zero. An `if`
            /// statement acts as a "Guard Clause" to keep the solver stable.
            /// <code>
            {
                double determinant = 0.00000001;
                if (Abs(determinant) < 1e-12)
                {
                    Console.WriteLine("Warning: Singular Matrix detected.");
                }
                else
                {
                    double inverse = 1.0 / determinant;
                    Console.WriteLine($"Inversion successful: {inverse}");
                }
            }
            /// </code>
            /// </example>
            ///
            /// <example 2> The Switch-Condition: Solver Selection
            /// In a multi-physics application, you might need to switch between different
            /// integration schemes. The `switch` statement makes this choice clear and
            /// organized compared to a long chain of `if-else` blocks.
            /// <code>
            {
                int methodCode = 2;

                switch (methodCode)
                {
                    case 1:
                        Console.WriteLine("Executing Forward Euler...");
                        break;
                    case 2:
                        Console.WriteLine("Executing Runge-Kutta 4th Order...");
                        break;
                    default:
                        Console.WriteLine("Falling back to Static Analysis.");
                        break;
                }

            }
            /// </code>
            /// </example>
            ///
            /// <example 3> The While-Loop: Convergence Monitor
            /// A `while` loop is the heartbeat of iterative methods. In this example,
            /// we simulate a cooling process where we keep calculating the temperature
            /// until it reaches the ambient environment temperature.
            /// <code>
            {
                double temperature = 100.0;
                double ambient = 25.0;
                int seconds = 0;

                while (temperature > ambient + 0.1)
                {
                    temperature -= (temperature - ambient) * 0.1; // Cool by 10%
                    seconds++;
                }

                Console.WriteLine($"Cooled to {temperature:F2}°C in {seconds} seconds.");

            }
            /// </code>
            /// </example>
            ///
            /// <example 4> The Foreach Loop: Property Calculation
            /// When you need to perform an operation on every element in a collection,
            /// like calculating the total mass of all elements in a structural model,
            /// `foreach` is the most readable choice because it eliminates index management.
            /// <code>
            {
                double[] elementMasses = { 5.2, 3.8, 4.1, 6.0 };
                double totalMass = 0;


                foreach (double m in elementMasses)
                {
                    totalMass += m;
                }

                Console.WriteLine($"Total System Mass: {totalMass}");
            }
            /// </code>
            /// </example>
            ///
            /// <header 3> Pro-Tip: Nested Loops </header 3>
            /// When working with 2D Matrices, you will often nest a `for` loop inside
            /// another. Remember that C# stores arrays in row-major order; iterating
            /// through rows in the outer loop and columns in the inner loop is
            /// significantly faster due to CPU cache optimization.
            /// </BookContent>
        }
    }
}
