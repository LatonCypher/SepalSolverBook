namespace ConsoleApp1.TrainingFiles.Chapter_02_Polynomials
{
    public class Section_01_Polynomial_Evaluation
    {
        public static void Run()
        {
            /// <BookContent> 
            /// <header 2> Polynomial Representation and Order </header 2> 
            /// As in other numerical analysis and engineering software, SepalSolver uses the standard convention of represent polynomials with coefficients in Descending Order. This means the first element of the array corresponds to the highest power of :math:`x`, making it easier to read and align with long-hand mathematical notation.
            /// 
            /// <header 3> 1. The Descending Order Convention </header 3>
            /// A polynomial :math:`P(x)= a_{n}x^{n} + a_{n-1}x^{n-1} + a_{n-2}x^{n-2} + \cdots + a_{1}x + a_{0}` is stored in an array where coeffs[0] is :math:`a_n`, coeffs[1] is :math:`a_{n-1}`, and so on, down to coeffs[n] which is :math:`a_0`. This ordering simplifies both the evaluation and manipulation of polynomials in code.
            /// <code> Descending Order Logic 
            { 
                // P(x) = 5x^2 + 2x + 1 
                // Power: 2 1 0
                double[] poly = [5, 2, 1];
                
                // Degree is determined by (Length - 1)
                int degree = poly.Length - 1; // 2
            }
            /// </code> 
            /// 
            /// 
            /// <header 3> 2. Horner's Method (Descending) </header 3> 
            /// When coefficients are in descending order, Horner's method becomes particularly elegant. We start with the first coefficient and repeatedly multiply by x and add the next coefficient: 
            /// :math:`P(x) = ( \cdot ((a_{n}x + a_{n-1})x + a_{n-2})x + \cdots + a_{1})x + a_{0}` 
            ///
            /// 
            /// <header 2> Examples </header 2> 
            /// <example 1> Real Value Evaluation (Descending)
            /// We define a cubic polynomial :math:`P(x)= 2x^3 −6x^2 + 2x−1`. Notice how the input sequence exactly matches the mathematical coefficients written from highest to lowest degree.
            /// <code> 
            { 
                // 2x^3 - 6x^2 + 2x - 1
                double[] poly = [2, -6, 2, -1.0];
                double result = Polyval(poly, 3.0);
                Console.WriteLine($"P(3.0) = {result}");
            }
            /// </code> 
            /// </example> 
            /// 
            /// <example 2> Complex Evaluation (Descending) 
            /// Evaluating at a complex point :math:`s = \sigma + j \omega` is common in control theory. Here we evaluate :math:`P(s)=1s^2 + 0s + 1` (which is :math:`s^2 + 1`) at the imaginary unit i.
            /// <code> 
            { 
                double[] poly = [1.0, 0.0, 1.0];
                Complex s = new(0, 1); // s = i
                Complex result = Polyval(poly, s);
                Console.WriteLine($"P(i) = {result}");
            } 
            /// </code> 
            /// </example> 
            /// 
            /// <example 3> Column Vector Evaluation (Vectorized) 
            /// In this case, we have a set of measurements in a ColVec and we want to pass them through our polynomial model. SepalSolver iterates /// through the vector, applying the descending-order Horner's method /// to each element. 
            /// <code> 
            { 
                // P(x) = x^2 + 2x + 3
                double[] poly = [1.0, 2.0, 3.0];

                ColVec x = new double[] { 0, 1, 2 };
                ColVec y = x.Select(x=>Polyval(poly, x)).ToArray();
                Console.WriteLine($"Result at x = {x.T} is: {y.T}"); // 4 + 4 + 3 = 11
            } 
            /// </code> 
            /// 
            /// </example>
            /// 
            /// <header 3> Implementation Tip: Power Mapping </header 3> 
            /// Because we use descending order, the power associated with a coefficient at index i is calculated as Degree - i. This is important when performing differentiation, as the derivative of the term at coeffs[i] involves multiplying by (Degree - i).
            /// </BookContent>
        }
    }
}
