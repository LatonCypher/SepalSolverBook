using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algerba
{
    internal class Section_3_Matrix_Vectors_Operations_Vector_Spaces
    {
        public static void Run()
        {
            // Matrix addition
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = new double[,] { { 9, 8, 7 },
                                           { 6, 5, 4 },
                                           { 3, 2, 1 } };
                Matrix C = A + B;
                Console.WriteLine($"A + B = \n{C}");
            }



            // Matrix subtraction
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = new double[,] { { 9, 8, 7 },
                                           { 6, 5, 4 },
                                           { 3, 2, 1 } };
                Matrix C = A - B;
                Console.WriteLine($"A - B = \n{C}");
            }

            // Matrix multiplication
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = new double[,] { { 9, 8, 7 },
                                           { 6, 5, 4 },
                                           { 3, 2, 1 } };
                Matrix C = A * B;
                Console.WriteLine($"A * B = \n{C}");
                Matrix D = B * A;
                Console.WriteLine($"B * A = \n{D}");
            }

            // Matrix division
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = new double[,] { { 9, 8, 7 },
                                           { 6, 5, 4 },
                                           { 3, 2, 1 } };
                Matrix C = Mldivide(A, B);
                Console.WriteLine($"A \\ B = \n{C}");
                Matrix D = Mrdivide(A, B);
                Console.WriteLine($"A / B = \n{D}");
            }

            // Matrix transpose
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = A.T;
                Console.WriteLine($"A^T = \n{B}");
            }

            // Matrix inverse
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                Matrix B = A.Inverse();
                Console.WriteLine($"A^-1 = \n{B}");
            }

            // Matrix determinant
            {
                Matrix A = new double[,] { { 1, 2, 3 },
                                           { 4, 5, 6 },
                                           { 7, 8, 9 } };
                double det = A.Det();
                Console.WriteLine($"det(A) = {det}");
            }

            // Matrix Rref
            {
                Matrix A = new double[,] { { 8,    1,    6,    1,  16 },
                                           { 3,    5,    6,    1,  15 },
                                           { 4,    7,    2,    1,  14 } };
                (Matrix R, Indexer P, Matrix N) = A.Rref();
                Console.WriteLine("\n A = \n" + A
                                + "\n R = \n" + R
                                + "\n P = \n" + P
                                + "\n N = \n" + N);
            }
        }
    }
}
