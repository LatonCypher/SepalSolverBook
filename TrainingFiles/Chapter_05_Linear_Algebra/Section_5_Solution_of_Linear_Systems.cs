using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algerba
{
    public class Section_5_Solution_of_Linear_Systems
    {
        public static void Run()
        {
            // Solve linear system of equations
            Matrix A = new double[,] { {  1,  1,  1,  1 },
                                       {  2, -1,  3, -1 },
                                       { -1,  4, -1,  2 },
                                       {  3,  2,  2, -1 }};
            ColVec b = new double[] {10, 5, 8, 20 };
            ColVec x = Mldivide(A, b);
            Console.WriteLine($"x = \n{x}");
        }
    }
}
