using Microsoft.CodeAnalysis.CSharp.Syntax;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_3_Special_Functions
{
    internal class Section_1_Gamma_Beta_Error_LambertW
    {
        public static void Run()
        {
            {
                // Gamma function
                double x = 4.0;
                double y = Gamma(x); //Here, we compute the Gamma function at x = 4.0
                Console.WriteLine(y); // Expected output: 6.0
            }

            {
                // Error function
                double x = 1.0;
                double y = Erf(x); //Here, we compute the Error function at x = 1.0
                Console.WriteLine(y);
            }
            {
                // Lambert W function
                double x = -LambertW(0, -Log(2)/3)/Log(2);
                double error = 3*x - Pow(2, x);
                Console.WriteLine(x);
            }


        }
    }
}
