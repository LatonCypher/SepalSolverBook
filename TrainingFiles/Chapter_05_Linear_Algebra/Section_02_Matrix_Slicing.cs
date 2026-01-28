using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algerba
{
    public class Section_02_Matrix_Slicing
    {
        public static void Run()
        {
            /// <BookContent>
            /// Matrix Slicing(Extracting Parts of Matrix)
            /// Matrix can be indexed to extract/set a single element, a row, a column, or a submatrix. 
            /// <header 2> Extracting/Setting part of a Vector </header>
            /// <code>
            {
                // A Vector can be indexed with one index
                RowVec R1 = Rand(4);
                Console.WriteLine($"R1 = {R1}");
                Console.WriteLine($"R1[2] = {R1[2]}");


                ColVec C1 = Rand(8);
                Console.WriteLine($"C1 = {C1}");
                Console.WriteLine($"C1[5] = {C1[5]}");
            }
            /// </code>
            /// 
            /// <header 2> Extracting part of a Matrix </header>
            /// <code>
            {
                Matrix A = new double[,]
                {
                    { 8,    1,    6,    1,  16 },
                    { 3,    5,    6,    2,  15 },
                    { 4,    7,    2,    1,  14 }
                };

                //Print the matrix
                Console.WriteLine($"A = {A}");

                    // Extract single element using subscript
                    Console.WriteLine($"A[1,2] = {A[1, 2]}");

                    //  Extract single element using index
                    Console.WriteLine($"A[5] = {A[5]}");

                //  Extract multiple elements using index
                Console.WriteLine($"A[2..5] = {A[2..5]}");

                //  Extract multiple elements using subscript along a row
                Console.WriteLine($"A[1, 2..4] = {A[1, 2..4]}");

                //  Extract multiple elements using subscript along a col
                Console.WriteLine($"A[0..3, 3] = {A[0..3, 3]}");

                //  Extract submatrix elements
                Console.WriteLine($"A[0..3, 1..3] = {A[0..3, 1..3]}");

                // Extract single row
                Console.WriteLine($"A[1, ..] = {A[1, ..]}");

                // Extract multiple rows
                Console.WriteLine($"A[1..3, ..] = {A[1..3, ..]}");
            }
            /// 
            /// </code>
            /// 
            /// <header 2> Setting Portions of a Matrix </header>
            /// <code>
            {
                Matrix A = new double[,]
                {
                    { 8,    1,    6,    1,  16 },
                    { 3,    5,    6,    2,  15 },
                    { 4,    7,    2,    1,  14 }
                };
                // set single element using subscript
                Console.WriteLine($"A = {A}");

                A[1, 2] = 125;
                Console.WriteLine($"A = {A}");

                //  set single element using index
                A[5] = 110;
                Console.WriteLine($"A = {A}");

                //  set multiple elements using index
                A[2..5] = new double[,] { { 10, 15, 20 } };
                Console.WriteLine($"A = {A}");

                //  set multiple elements using subscript along a row
                A[1, 2..4] = new double[] { 150, 200 };
                Console.WriteLine($"A = {A}");

                //  set multiple elements using subscript along a col
                A[0..3, 3] = new double[] { 100, 150, 200 };
                Console.WriteLine($"A = {A}");

                //  set submatrix elements
                Indexer i = new(0, 3), j = new(1, 3);
                A[0..3, 1..3] = new double[,]
                {
                        { 100, 150 },
                        { 100, 150 },
                        { 100, 150 }
                };
                Console.WriteLine($"A = {A}");

                // set single row
                A[1, ..] = new double[] { 1, 2, 3, 4, 5 };
                Console.WriteLine($"A = {A}");

                // set multiple rows
                A[1..3, ..] = Rand(2, 5);
                Console.WriteLine($"A = {A}");
            }
            /// </code>
            /// 
            /// </BookContent>
            
        }

    }
}
