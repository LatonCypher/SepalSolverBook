using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_07_Integration
{
    internal class Section_2_Gauss_Legendre
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// Gauss-Legendre Quadrature is a numerical integration technique designed to yield maximum algebraic precision with a minimal number of function evaluations.
            /// Unlike standard Newton-Cotes methods (such as the Trapezoidal or Simpson's rules) that use fixed, equally spaced evaluation points, Gauss-Legendre quadrature treats both the evaluation locations (nodes) and their corresponding multipliers (weights) as free variables.
            ///
            /// Mathematical Foundation
            /// To evaluate a definite integral over a standardized interval :math:`[-1, 1]`:
            /// <math>
            ///     \int_{-1}^{1} f(x) , dx \approx \sum_{i=1}^{n} w_i f(x_i)
            /// </math>
            ///
            /// The nodes :math:`x_i` are selected as the roots of the n-th degree Legendre polynomial :math:`P_n(x)`.
            /// The weights :math:`w_i` are calculated by integrating the Lagrange interpolating polynomials over the interval:
            /// <math>
            ///     w_i = \frac{2}{(1 - x_i^2) [P_n'(x_i)]^2}
            /// </math>
            ///
            /// An n-point Gauss-Legendre rule integrates polynomials of degree up to 2n - 1 exactly.
            ///
            /// Domain Transformation (Mapping)
            /// For integrals over an arbitrary real interval [a, b], a linear change of variables maps the domain to [-1, 1]:
            /// <math>
            ///     x(t) = \frac{b - a}{2} t + \frac{a + b}{2}
            /// </math>
            /// :math:`dx = \frac{b - a}{2} dt`
            ///
            /// The transformation yields:
            /// <math>
            ///     \int_{a}^{b} f(x) , dx = \frac{b - a}{2} \sum_{i=1}^{n} w_i f\left( \frac{b - a}{2} x_i + \frac{a + b}{2} \right)
            /// </math>
            /// ---
            ///
            /// <header 3> Applied Examples (Solved via Simple Solver) </header>
            ///
            /// <example 1>
            /// Example 1: Exact Polynomial Integration (2-Point Rule)
            /// Evaluate :math:`\int_{0}^{2} (x^3 - 3x + 2) , dx` using :math:`n = 2` points.
            ///
            /// <code>
            {
                var I = Integral(x => Polyval([1.0, 0.0, -3.0, 2.0], x), 0, 2);
                Console.WriteLine($"I = {I}");
            }
            /// </code>
            ///
            /// Exact Analytical Result:
            /// \left[ \frac{x^4}{4} - \frac{3x^2}{2} + 2x \right]0^2 = (4 - 6 + 4) - 0 = 2
            /// Because the integrand is degree 3 (\le 2(2) - 1 = 3), the 2-point Gauss-Legendre result is exact.
            /// </example>
            /// ---
            ///
            /// <example 2>
            /// Example 2: Transcendental Function 
            /// Evaluate :math:`\int{0}^{\pi} \sin(x) , dx` 
            ///
            /// <code>
            {
                var I = Integral(x => Sin(x), 0, pi);
                Console.WriteLine($"I = {I}");
            }
            /// </code>
            /// 
            /// Exact Analytical Result:
            /// [-\cos(x)]_0^\pi = 1 - (-1) = 2.000000 (Relative Error < 0.07% with just 3 evaluations).
            /// </example>
            /// 
            ///
            /// <example 3>
            /// Example 3: Transcendental Function 
            /// Evaluate :math:`\int{0}^{\infty} e^{x^2}(\ln(x))^2 , dx` 
            ///
            /// <code>
            {
                var I = Integrators.GaussLag(x => Exp(-x*x)*Pow(Log(x), 2));
                Console.WriteLine($"I = {I}");
            }
            /// </code>
            /// 
            /// </example>
            /// </BookContent>
        }
    }
}
