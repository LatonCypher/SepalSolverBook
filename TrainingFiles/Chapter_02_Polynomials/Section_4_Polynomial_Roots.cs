namespace ConsoleApp1.TrainingFiles.Chapter_1_Polynomials
{
    public class Section_4_Polynomial_Roots
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// </BookContent>
            {
                // Roots of polynomials
                double[] coeff = [1, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 7, 2, 3];
                var roots = Roots(coeff);
                Console.WriteLine("Roots:\n " + string.Join("\n ", roots));
            }
        }
    }
}
