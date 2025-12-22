using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algerba
{
    public class Section_2_Matrix_Slicing
    {
        public static void Run()
        {
            Matrix A = new double[,]
            { 
                { 8,    1,    6,    1,  16 },
                { 3,    5,    6,    2,  15 },
                { 4,    7,    2,    1,  14 } 
            };

            //Print the matrix
            Console.WriteLine($"A = {A}");

            {
                // Extract single element using subscript
                Console.WriteLine($"A[1,2] = {A[1, 2]}");
            }


            {
                //  Extract single element using index
                Console.WriteLine($"A[5] = {A[5]}");
            }

            {
                //  Extract multiple elements using index
                Indexer i = new(2, 5);
                Console.WriteLine($"A[i] = {A[i]}");
            }

            {
                //  Extract multiple elements using subscript along a row
                Indexer j = new(2, 4);
                Console.WriteLine($"A[1, j] = {A[1, j]}");
            }

            {
                //  Extract multiple elements using subscript along a col
                Indexer i = new(0, 3);
                Console.WriteLine($"A[i, 3] = {A[i, 3]}");
            }

            {
                //  Extract submatrix elements
                Indexer i = new(0, 3), j = new(1, 3);
                Console.WriteLine($"A[i, j] = {A[i, j]}");
            }

            {
                // Extract single row
                Console.WriteLine($"A[1,\"\"] = {A[1, ""]}");
            }

            {
                // Extract multiple rows
                Indexer i = new(1, 3);
                Console.WriteLine($"A[i,\"\"] = {A[i, ""]}");
            }



            // setting portions of a matrix
            {
                // set single element using subscript
                A[1, 2] = 125;
                Console.WriteLine($"A = {A}");
            }


            {
                //  set single element using index
                A[5] = 110;
                Console.WriteLine($"A = {A}");
            }

            {
                //  set multiple elements using index
                Indexer i = new(2, 5);
                A[i] = new ColVec(new double[] { 10, 15, 20 });
                Console.WriteLine($"A = {A}");
            }

            {
                //  set multiple elements using subscript along a row
                Indexer j = new(2, 4);
                A[1, j] = new RowVec(new double[] { 150, 200 });
                Console.WriteLine($"A = {A}");
            }

            {
                //  set multiple elements using subscript along a col
                Indexer i = new(0, 3);
                A[i, 3] = new ColVec(new double[] { 100, 150, 200 });
                Console.WriteLine($"A = {A}");
            }

            {
                //  set submatrix elements
                Indexer i = new(0, 3), j = new(1, 3);
                A[i, j] = new double[,] 
                {
                    { 100, 150 },
                    { 100, 150 },
                    { 100, 150 }
                };
                Console.WriteLine($"A = {A}");
            }

            {
                // set single row
                A[1, ""] = new double[] { 1, 2, 3, 4, 5 };
                Console.WriteLine($"A = {A}");
            }

            {
                // set multiple rows
                Indexer i = new(1, 3);
                A[i, ""] = Rand(2, 5);
                Console.WriteLine($"A = {A}");
            }
        }

    }
}
