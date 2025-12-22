using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algerba
{
    public class Section_6_Cholesky_Factorization_FactorUpdate_Positive_Definicy
    {
        public static void Run()
        {
            {
                Matrix A = new double[,] 
                {
                    { 22.7345,    1.8859,         0,         0,    1.3000 },
                    {  1.8859,   22.2340,    2.0461,         0,         0 },
                    {       0,     2.0461,   22.7591,    2.4606,         0 },
                    {       0,          0,    2.4606,   22.5848,    2.2768 },
                    {  1.3000,          0,         0,    2.2768,   22.4853 }
                };
                Matrix R = Chol(A);
                Console.WriteLine(R);
            }

            {

            }
        }
    }
}
