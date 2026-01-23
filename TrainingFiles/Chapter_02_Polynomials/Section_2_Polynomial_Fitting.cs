using ScottPlot;
using ScottPlot.LegendLayouts;
using ScottPlot.Palettes;
using ScottPlot.PlotStyles;
using ScottPlot.TickGenerators.TimeUnits;
using SepalSolver;
using System;
using System.ComponentModel;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Console.WriteLine($"Coefficients: [{string.Join(", ", coefficients.Select(n => n.ToString("F4")))}]");

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
                    Quaratic fit : [{string.Join(",", fit2)}] 
                    Residual: {yp2.Zip(y, (q, m) => Pow(q-m, 2)).Sum()}
                    """);

                Legend(["Data", "Linear Fit", "Quadratic Fit"]);
                hold = false;
                SaveAs("Polyfit_Example_4.png");
            }
            /// </code> 
            /// </example 4> 
            /// 
            /// 
            /// <header 2> Multivariate Polynomial Fitting </header 2>
            /// 
            /// In many engineering scenarios, a dependent variable :math:z is influenced by multiple independent variables (e.g., :math:`x` and :math:`y`). This is known as Multivariate Fitting or Surface Fitting. While a standard Polyfit handles a single line, a multivariate fit finds a surface that minimizes the residuals across multiple dimensions.
            /// 
            /// <header 3> 1.The Mathematical Model</header 3>
            /// For two variables :math:`x` and :math:`y`, a second-order multivariate polynomial takes the form: :math:`z(x, y) = a_0 + a_1x + a_2y + a_3x^2 + a_4xy + a_5y^2`
            /// The "cross term" :math: xy is vital because it accounts for the interaction between the two variables—how the influence of :math:x might change depending on the current value of :math:`y`.
            /// 
            /// <header 3> 2.Implementation Logic </header 3>
            /// In SepalSolver, multivariate fitting is performed by constructing an augmented matrix where each column represents a term in the polynomial expansion(1, :math:`x`, :math:`y`, :math:`x^2`, etc.) and solving the resulting linear system.
            /// 
            /// <code> 
            { 
                // Data points: (x, y) coordinates and observed z values
                double[] x = [0, 1, 2, 0, 1, 2], y = [0, 0, 0, 1, 1, 1], z = [1.1, 2.0, 3.9, 2.2, 3.1, 5.1];

                // Fitting for a plane: z = a + bx + cy
                // We construct a matrix A where rows are [1, x_i, y_i]
                List<ColVec> A = [Ones(x.Length), x, y];

                // Solve for coefficients using the Normal Equation (Least Squares)
                // A' * A * coeff = A' * z
                var coeff = Mldivide(A, z);

                Console.WriteLine($"Model: z = {coeff[0]} + {coeff[1]}x + {coeff[2]}y");
            }
            /// </code>
            /// 
            /// <header 2> Examples </header 2>
            /// 
            /// <example 1> Material Stress Analysis The stress(:math:`\sigma`) on a component might depend on both Temperature(:math:`T`) and Pressure(:math:`P`). A multivariate fit allows you to create a "Stress Surface" that can predict failure points at any combination of: math:`T` and :math:`P`. 
            /// 
            /// </example 1>
            /// 
            /// <example 2> Aero-Efficiency Mapping For a wing, the Lift Coefficient(:math:`C_L`) is a function of both the Angle of Attack(:math:`\alpha`) and the Mach Number(:math:`M`). Using multivariate fitting, flight computers can interpolate lift values instantly across the entire flight envelope.
            /// </example 2>
            /// 
            /// <header 3> Exercise: Term Expansion</header 3>
            /// Task: To fit a full quadratic surface :math:`z = a + bx + cy + dxy`, your matrix A needs four columns. Complete the column assignment for the cross-term :math:`xy`.
            /// <code> 
            {
                // Data points
                double[] x = [0, 1, 2, 0, 1, 2], y = [0, 0, 0, 1, 1, 1], z = [1.1, 2.0, 3.9, 2.2, 3.1, 5.1];

                // Compute the cross term for multivariate polynomial fitting
                double[] xy = [..x.Zip(y, (xi, yi) => xi * yi)];

                // Task: Make the Matrix A with the cross term included

                // Solve for the coefficients using Least Squares
            }
            /// </code>
            /// </BookContent>


        }
    }
}
