using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_07_Integration
{
    internal class Section_3_Gauss_Laguerre
    {
        public static void Run()
        {
            /// Gauss-Laguerre Quadrature is a numerical integration technique designed to evaluate semi-infinite definite integrals featuring exponential decay weight functions.
            /// Unlike Gauss-Legendre quadrature which targets finite intervals, Gauss-Laguerre quadrature naturally handles integration domains from zero to infinity by weighting samples according to the Laguerre polynomials.
            /// Mathematical Foundation
            /// To evaluate a semi-infinite integral with an exponential weight function over the interval :math:`[0, \infty)`:
            /// <math>
            ///     \int_{0}^{\infty} e^{-x} f(x) , dx \approx \sum_{i=1}^{n} w_i f(x_i)
            /// </math>
            ///     
            /// The nodes :math:`x_i` are selected as the roots of the n-th degree Laguerre polynomial :math:`L_n(x)`.
            /// The weights :math:`w_i` are calculated using the derivative of the Laguerre polynomial at each node:
            /// <math>
            /// w_i = \frac{x_i}{(n + 1)^2 [L_{n+1}(x_i)]^2}
            /// </math>
            /// An n-point Gauss-Laguerre rule integrates polynomials :math:`f(x)` of degree up to 2n - 1 exactly when weighted by :math:`e^{-x}`.
            /// General Domain & Weight Transformation
            /// For integrals lacking an explicit :math:`e^{-x}` weight or evaluated over :math:`[0, \infty)` with a scaling parameter :math:`\alpha > 0`:
            /// <math>
            ///     \int_{0}^{\infty} g(x) , dx = \int_{0}^{\infty} e^{-x} \left( e^{x} g(x) \right) dx \approx \sum_{i=1}^{n} w_i e^{x_i} g(x_i)
            /// </math>
            /// 
            /// <header 3> Applied Examples 
            /// <example 1> Example 1: Standard Exponential-Weighted Polynomial Integration
            /// Evaluate 
            /// <math>
            ///     \int_{0}^{\infty} e^{-x} x^3 \, dx 
            /// </math>
            /// <code>
            {
                var I = Integrators.GaussLag(x => Pow(x, 3));
                Console.WriteLine($"I = {I:F4}");
            }
            /// </code>
            /// 
            /// Exact Analytical Result:
            /// <math>
            ///     `\Gamma(4) = 3! = 6`
            /// </math>
            /// 
            /// </example>
            /// 
            /// <example 2> Example 2: Quantum Physics Atomic Orbital Integral
            /// Evaluate 
            /// <math>
            ///     `\int_{0}^{\infty} e^{-x} x^2 \, dx`
            /// </math>
            /// 
            /// <code>
            {
                var I = GaussLag(x => x * x);
                Console.WriteLine($"I = {I:F4}");
            }
            /// </code>
            /// 
            /// Exact Analytical Result:
            /// \Gamma(3) = 2! = 2.000000
            /// 
            /// Commonly used in radial wave-function integrals for hydrogen-like atom probability densities.
            /// 
            /// 
            /// <example 3> Example 3: Unweighted Semi-Infinite Exponential Decay 
            /// Evaluate 
            /// <math>
            ///     \int_{0}^{\infty} e^{-2x} \, dx
            /// </math>
            /// 
            /// <code>
            {
                var I = GaussLag(x => Exp(-x));
                Console.WriteLine($"I = {I:F4}");
            }
            /// </code>
            /// Exact Analytical Result:
            /// <math>
            ///     `\left[ -\frac{1}{2}e^{-2x} \right]_0^\infty = \frac{1}{2} = 0.500000`
            /// </math>
            /// Computed by expressing the integrand as :math:`e^{-x} f(x)` where :math:`f(x) = e^{-x}`.
            /// </example>
            /// 
            /// 
            /// <example 4> Example 4: Trigonometric Oscillatory Decay
            /// Evaluate :
            /// <math>
            ///     `\int_{0}^{\infty} e^{-x} \cos(x) \, dx`
            /// </math>
            /// 
            /// <code>
            {
                var I = GaussLag(x => Cos(x));
                Console.WriteLine($"I = {I:F4}");
            }
            /// Exact Analytical Result:
            /// <math>
            ///     \frac{1}{1^2 + 1^2} = 0.500000
            /// </math>
            /// </example>
            /// Demonstrates high-precision convergence on decaying trigonometric response functions.
            /// 
            /// ///
        }
    }
}
