using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Chapter_1_Polynomials
{
    public class Section_1_Polynomial_Evaluation
    {
        public static void Run()
        {

            {
                // Example of polynomial evaluation
                double[] coeff = [1, 2, 3, 4, 5];
                double x = 2;
                double val = Polyval(coeff, x);
                Console.WriteLine($"Polynomial evaluated at x = {x}: {val}");
            }

            {
                // Example of polynomial evaluation
                double[] coeff = [1, 2, 3, 4, 5];
                Complex x = new(2,3);
                Complex val = Polyval(coeff, x);
                Console.WriteLine($"Polynomial evaluated at x = {x}: {val}");
            }

            {
                // Example of polynomial fitting
                double[] x = [1, 2, 3, 4, 5], y = [3, 4, 5, 6, 7];
                double[] fit = Polyfit(x, y, 1);
                Console.WriteLine($"Polynomial fit: [{string.Join(",", fit)}]");
                ColVec xp = Linspace(1, 5);
                ColVec yp = xp.Select(x => Polyval(fit, x)).ToList();
                Scatter(x, y, "fob"); hold  = true;
                Plot(xp, yp, "r");
            }

            {
                // Example of polynomial fitting
                double[] x = [1, 2, 3, 4, 5], y = [6, 9, 14, 21, 30];
                Scatter(x, y, "fob"); hold  = true;

                double[] fit1 = Polyfit(x, y, 1);
                Console.WriteLine($"Linear fit : [{string.Join(",", fit1)}]");
                double[] fit2 = Polyfit(x, y, 2);
                Console.WriteLine($"Quadratic fit: [{string.Join(",", fit2)}]");

                ColVec xp = Linspace(1, 5);
                ColVec yp1 = xp.Select(x => Polyval(fit1, x)).ToList();
                ColVec yp2 = xp.Select(x => Polyval(fit2, x)).ToList();
                Plot(xp, yp1, "r"); Plot(xp, yp2, "g");
            }


            {
                // Example of Polynomial Addition
                double[] poly3 = [1, 2, 3, 4, 5], poly4 = [4, 5, 6];
                var AddPoly = Polyadd(poly3, poly4);
                Console.WriteLine($"AddPoly:\n [{string.Join(", ", AddPoly)}]");
            }


            {
                // Example of Polynomial Subtraction
                double[] poly5 = [1, 2, 3, 4, 5], poly6 = [4, 5, 6];
                var SubPoly = Polysub(poly5, poly6);
                Console.WriteLine($"SubPoly:\n [{string.Join(", ", SubPoly)}]");
            }


            {
                // Example of Polynomial Convolution
                double[] poly1 = [1, 2, 3], poly2 = [4, 5, 6];
                var ConvPoly = Conv(poly1, poly2);
                Console.WriteLine($"ConvPoly:\n [{string.Join(", ", ConvPoly)}]");
            }


            {
                // Example of Polynomial Deconvolution
                double[] P = [3, 2, 4, 6, 5], D = [4, 5, 6], Q, R;
                (Q, R) = Deconv(P, D);
                Console.WriteLine($"Quotient:\n [{string.Join(", ", Q)}]");
                Console.WriteLine($"Remainder:\n [{string.Join(", ", R)}]");
            }


            {
                // Roots of polynomials
                double[] coeff = [1, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 7, 2, 3];
                var roots = Roots(coeff);
                Console.WriteLine($"Roots:\n {string.Join("\n ", roots)}");
            }
        }
    }
}
