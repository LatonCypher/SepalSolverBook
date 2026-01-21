using SepalSolver;

namespace ConsoleApp1.TrainingFiles.Chapter_02_Polynomials
{
    public class Section_2_Polynomial_Fitting
    {
        public static void Run()
        {
            /// <BookContent> 
            /// <header 2> Polynomial Fitting (Polyfit) </header 2> 
            /// In engineering and data science, we often encounter discrete data points that represent a physical process. Polynomial Fitting is the mathematical technique used to find a continuous function—specifically a polynomial—that minimizes the discrepancy between the curve and the observed data. In SepalSolver, we use the Least Squares approach to determine these coefficients. 
            /// 
            /// <header 3> 1. Mathematical Objective </header 3> 
            /// The goal of ``Polyfit`` is to find a set of coefficients for a polynomial of degree :math:`N`:, :math:`P(x) = a_0 x^N + a_1 x^{N-1} + \dots + a_N` such that the sum of the squares of the residuals (the vertical distance between the data points and the curve) is minimized. 
            /// 
            /// 
            /// <header 3> 2. Coefficient Order </header>
            /// Note that while the internal math often builds from the constant term up, SepalSolver returns the resulting `double[] array` in descending order. This means the first element in the array is the coefficient for the highest power :math:`x^N`. 
            /// 
            /// <header 2> Examples </header 2> 
            /// 
            /// <example 1> Fitting a Perfect Quadratic 
            /// If our data follows a perfect square relationship, such as :math:`y = x^2`, we expect `Polyfit` to return coefficients that reflect :math:`1x^2 + 0x + 0`. In this example, we fit a degree :math:`N=2` polynomial to a set of coordinates. 
            /// 
            /// <code> 
            { 
                // Define data points
                double[] X = [1, 2, 3, 4]; 
                double[] Y = [1, 4, 9, 16]; 
                int N = 2;

                // Perform the fit
                double[] coefficients = Polyfit(X, Y, N);

                // Output: 1, 0, 0 (representing 1x^2 + 0x + 0)
                Console.WriteLine($"Coefficients: {string.Join(", ", coefficients)}");

                // Plotting the results
                Scatter(X, Y, "fob"); hold = true;
                double[] x = Linspace(1, 4);
                double[] y = [..x.Select(x => Polyval(coefficients, x))];
                Plot(x, y, "r", Linewidth: 2); hold = false;
                SaveAs("Polyfit_Example1.png");
            }
            /// </code> 
            /// </example 1> 
            /// 
            /// <example 2> Linear Regression from Sensor Data
            /// Imagine you are testing a linear spring. You record the force :math:`Y` applied at various displacements :math:`X`. By fitting a degree :math:`N=1` polynomial, you can find the spring constant :math:`k`, which corresponds to the first coefficient in the returned array.
            /// <code> 
            { 
                double[] X = [0.1, 0.2, 0.3, 0.4];     // Displacement
                double[] Y = [10.2, 20.1, 29.9, 40.2]; // Force

                // Fit a line: P(x) = a0*x + a1
                double[] p = Polyfit(X, Y, 1);

                Console.WriteLine($"Spring Constant k: {p[0]} N/m");
            } 
            /// </code> 
            /// </example 2> 
            /// 
            /// <example 3> Handling Noise in Experimental Data 
            /// Real-world data is rarely perfect. When data points are "noisy," fitting a lower-degree polynomial acts as a filter, capturing the general trend without being distracted by individual measurement errors. 
            /// <code> 
            { 
                double[] X = [1, 2, 3, 4, 5]; 
                double[] Y = [2.1, 3.9, 6.2, 8.1, 9.8]; // Roughly y = 2x

                // Even if we fit a degree 2, the leading coefficient should be near zero
                double[] p = Polyfit(X, Y, 2);

                Console.WriteLine($"Quadratic term (should be small): {p[0]}");
            }
            /// </code> 
            /// </example 3> 
            /// 
            /// <header 3> Usage Warning </header 3> 
            /// Ensure that the length of arrays :math:`X` and :math:`Y` are identical. Additionally, to find a unique solution for a degree :math:`N` polynomial, you must provide at least :math:`N+1` data points. Providing fewer points will result in an underdetermined system and numerical instability. 
            /// It is also important to choose the correct degree of the polynomial to get a good fit. 
            /// Overfitting can occur if the degree is too high, while underfitting can happen if the degree is too low.
            /// 
            /// <example 4> Underfitting Problem
            /// In this example, we compare a linear and quaradtic fit for the same data. showing the importance of choosing the right degree for the fitting. 
            /// <code> 
            {
                // Example of polynomial fitting
                double[] x = [1, 2, 3, 4, 5], y = [6, 9, 14, 21, 30];
                Scatter(x, y, "fob"); hold  = true;

                double[] xp = Linspace(1, 5);
                double[] fit1 = Polyfit(x, y, 1);
                double[] yp1 = [.. xp.Select(x => Polyval(fit1, x))];
                Plot(xp, yp1, "r");
                Console.WriteLine($"""
                    Linear fit : [{string.Join(",", fit1)}] 
                    Residual: {yp1.Zip(y, (l, m) => Pow(l-m, 2)).Sum()}
                    """);

                double[] fit2 = Polyfit(x, y, 2);
                double[] yp2 = [.. xp.Select(x => Polyval(fit2, x))];
                Plot(xp, yp2, "g");
                Console.WriteLine($"""
                    Linear fit : [{string.Join(",", fit2)}] 
                    Residual: {yp2.Zip(y, (q, m) => Pow(q-m, 2)).Sum()}
                    """);

                Legend(["Data", "Linear Fit", "Quadratic Fit"]);
                hold = false;
                SaveAs("Polyfit_Example_4.png");
            }
            /// </code> 
            /// </example 4> 
            /// 
            /// </BookContent>


        }
    }
}
