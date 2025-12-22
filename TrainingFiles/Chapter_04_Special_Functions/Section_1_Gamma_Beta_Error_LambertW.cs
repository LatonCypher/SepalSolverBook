using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_3_Special_Functions
{
    internal class Section_1_Gamma_Beta_Error_LambertW
    {
        public static void Run()
        {

            {
                // Lambert W function
                double x = -LambertW(0, -Log(2)/3)/Log(2);
                double error = 3*x - Pow(2, x);
                Console.WriteLine(x);
            }

        }
    }
}
