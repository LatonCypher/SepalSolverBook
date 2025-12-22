using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_1_Polynomials
{
    public class Section_2_Polynomial_Fitting
    {
        public static void Run()
        {
            {
                // Example of polynomial fitting
                double[] x = [1, 2, 3, 4, 5], y = [3, 4, 5, 6, 7];
                double[] fit = Polyfit(x, y, 1);
                Console.WriteLine($"Polynomial fit: {fit}");
            }
        }
    }
}
