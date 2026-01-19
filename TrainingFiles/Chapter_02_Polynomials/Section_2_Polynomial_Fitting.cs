namespace ConsoleApp1.TrainingFiles.Chapter_02_Polynomials
{
    public class Section_2_Polynomial_Fitting
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// </BookContent>
            {
                // Example of polynomial fitting
                double[] x = [1, 2, 3, 4, 5], y = [3, 4, 5, 6, 7];
                double[] fit = Polyfit(x, y, 1);
                Console.WriteLine($"Polynomial fit: {fit}");
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
        }
    }
}
