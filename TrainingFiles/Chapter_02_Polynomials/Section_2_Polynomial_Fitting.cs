namespace ConsoleApp1.TrainingFiles.Chapter_1_Polynomials
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
                double[] fit = Polyfit(x, y, 1); //This is a linear fit since degree of polynomial is 1
                Console.WriteLine($"Polynomial fit: {fit}");
            }
            {
                // Example of quadratic polynomial fitting
                double[] x = [0, 0.5, 1.0, 1.5, 2, 2.5, 3], y = [2, 14.375, 22, 24.875, 23, 16.375, 5];
                double[] fit = Polyfit(x, y, 2); //This is a quadratic fit since degree of polynomial is 2
                Console.WriteLine($"Quadratic Polynomial fit: {fit}");

            }
        }
    }
}
