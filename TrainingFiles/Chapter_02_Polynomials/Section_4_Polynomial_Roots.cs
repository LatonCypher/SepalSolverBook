using SepalSolver;

namespace ConsoleApp1.TrainingFiles.Chapter_02_Polynomials
{
    public class Section_4_Polynomial_Roots
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// <header 2> Root Finding for Polynomials </header 2>
            /// Finding the roots of a polynomial—the values of :math:`x` for which :math:`P(x) = 0`—is a fundamental task in engineering. Whether you are finding the natural frequencies of a structure or the break-even point in a cost model, SepalSolver provides robust methods to extract these critical values from the coefficient array.
            /// 
            /// <header 3> 1. The Companion Matrix Method </header 3>
            /// For general polynomials, the most stable way to find all roots (including complex ones) is to construct a Companion Matrix. The eigenvalues of this matrix are exactly the roots of the polynomial. By transforming a polynomial problem into an eigenvalue problem, we leverage the power of our linear algebra engine.
            /// 
            /// <code> Finding All Roots
            {
                // P(x) = x^2 - 5x + 6
                double[] coeffs = [1.0, -5.0, 6.0];// Returns an array of Complex numbers: { (2,0), (3,0) }
                Complex[] roots = Roots(coeffs);
                Console.WriteLine("Roots = \n " + string.Join("\n ", roots));
            }
            /// </code>
            /// 
            /// <header 3> 2. Iterative Refinement (Newton-Raphson) </header 3>
            /// 
            /// If you only need a single real root near a specific guess, the Newton-Raphson method is incredibly fast. It uses the polynomial and its derivative (Polyder) to "walk" toward the zero-crossing: :math:`x_{next} = x - \frac{P(x)}{P'(x)}`
            /// 
            /// <header 2> Examples </header 2>
            /// 
            /// <example 1> Finding Natural Frequencies
            /// 
            /// In vibration analysis, the roots of a characteristic polynomial :math:`(s^2 + 4s + 13)`represent the squared natural frequencies of a system. By passing the coefficients of the system's governing equation to Roots, we can identify the resonance points. 
            /// <code>
            {
                // Characteristic Equation: s^2 + 4s + 13 = 0
                double[] characteristic = { 1.0, 4.0, 13.0 };
                var frequencies = Roots(characteristic);

                // Results will be complex: -2 + 3i and -2 - 3i
                Console.WriteLine($"Principal Root = \n " + string.Join("\n ", frequencies));
            }
            /// </code>
            /// </example 1>
            /// 
            /// <example 2> Intersection of a Curve and an Axis
            /// 
            /// Imagine a robot arm path described by :math:`f(x) = x^3 - 2x^2 - 5x + 6`. We need to know exactly where the arm crosses the baseline (the x-axis). Using the Roots function allows us to find all three points of intersection simultaneously. 
            /// <code>
            {
                double[] path = [1.0, -2.0, -5.0, 6.0];
                var intersections = Roots(path);

                foreach (var r in intersections)
                {
                    if(Complex.IsReal(r)) 
                        Console.WriteLine($"Crossing at: {r.Real}");
                }
            }
            /// </code>
            /// </example 2>
            /// 
            /// <example 3>
            /// Compute the roots of :math:`x^8 + 2x^7 + 3x^6 + 4x^5 + 5x^4 + 6x^3 + 7x^2 + 2x + 3`
            /// <code>
            {
                // Roots of polynomials
                double[] coeff = [1, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 7, 2, 3];
                var roots = Roots(coeff);
                Console.WriteLine("Roots = \n " + string.Join("\n ", roots));
            }
            /// </code>
            /// </example 3>
            /// 
            /// 
            /// <example 4> Refining a Guess with Newton-Raphson
            /// When high precision is required for a specific physical boundary, we take a rough estimate from a graph and refine it. By combining Evaluate and Polyder, we can implement a custom search that converges in just a few iterations.
            /// <code>
            {
                double[] p = { 1.0, 0.0, -2.0 }; // x^2 - 2 (Roots are ±sqrt(2))
                double x = 1.5; // Initial guess
                var dp = Polyder(p, 1);
                for (int i = 0; i < 5; i++)
                {
                    double f = Polyval(p, x);
                    double df = Polyval(dp, x);
                    x -= (f / df);
                }
                Console.WriteLine($"Refined sqrt(2): {x}");
            }
            /// </code>
            /// </example 4>
            /// 
            /// <header 3> Numerical Note: Complex Roots </header 3>
            /// Even if your input coefficients are real, the roots can be complex. SepalSolver always returns roots as an array of Complex numbers to prevent data loss. If you only care about real solutions, you can filter the results by checking if the imaginary part is below a small tolerance (e.g., Math.Abs(r.Imaginary) < 1e-12).
            /// </BookContent>
            
        }
    }
}
