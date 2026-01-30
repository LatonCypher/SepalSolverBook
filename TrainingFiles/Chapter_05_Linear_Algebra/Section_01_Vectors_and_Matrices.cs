namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algerba
{
    internal class Section_01_Vectors_and_Matrices
    {
        public static void Run()
        {
            /// <BookContent>
            /// Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: ``RowVec``, ``ColVec`` and ``Matrix``. ``RowVec`` and ``ColVec`` are 1D arrays while ``Matrix`` is a 2D array. 
            /// 
            /// <header 2> Creating Vectors and Matrices </header>
            /// 
            /// <code>
            {
                // Row vector
                RowVec R = new double[] { 5, 6, 7, 1 };
                Console.WriteLine($"R = {R}");

                // Column vector
                ColVec C = new double[] { 8, 3, 4, 2, 7 };
                Console.WriteLine($"C = {C}");

                // Matrix
                Matrix M = new double[,] 
                {
                    {5, -2, 3, 7 },
                    {2, 1, -7, 3 },
                    {4, 8, 9, 1 },
                    {0, 5, -6, -3 }
                };
                Console.WriteLine($"M = {M}");
            }
            /// </code>
            /// 
            /// 
            /// <header 2> Vectors and Matrices can also be initialized using random </header> 
            /// <code>
            {
                // Row vector
                RowVec R = Rand(7);
                Console.WriteLine($"R = {R}");

                // Column vector
                ColVec C = Rand(5);
                Console.WriteLine($"C = {C}");

                // Matrix
                Matrix M = Rand(8, 7);
                Console.WriteLine($"M = {M}");
            }
            /// </code>
            /// 
            /// <header 2> Vectors can be initialized using Zeros, Ones, Eye etc </header>
            /// <code>
            {
                // Row vector
                RowVec R = Zeros(7);
                Console.WriteLine($"R = {R}");

                // Column vector
                ColVec C = Ones(5);
                Console.WriteLine($"C = {C}");

                // Matrix
                Matrix M = Eye(7, 7);
                Console.WriteLine($"M = {M}");
            }
            /// </code>
            /// 
            /// <header 2> Vectors and Matrices can be concatenated </header>
            /// <code>
            {
                RowVec R1 = Rand(4);
                Console.WriteLine($"R1 = {R1}");
                RowVec R2 = Rand(5);
                Console.WriteLine($"R2 = {R2}");

                // Horizontal concatenation
                RowVec R3 = Hcart(R1, R2);
                Console.WriteLine($"R3 = {R3}");

                ColVec C1 = Rand(10);
                Console.WriteLine($"C1 = {C1}");
                ColVec C2 = Rand(10);
                Console.WriteLine($"C2 = {C2}");

                // Horizontal concatenation
                Matrix M = Hcart(C1, C2);
                Console.WriteLine($"M = {M}");
            }
            /// </code>
            /// 
            /// 
            /// <header 2> Vertical Concatenation </header>
            /// <code>
            {
                RowVec R1 = Rand(4);
                Console.WriteLine($"R1 = {R1}");
                RowVec R2 = Rand(4);
                Console.WriteLine($"R2 = {R2}");

                // Vertical concatenation
                Matrix M = Vcart(R1, R2);
                Console.WriteLine($"M = {M}");

                ColVec C1 = Rand(10);
                Console.WriteLine($"C1 = {C1}");
                ColVec C2 = Rand(2);
                Console.WriteLine($"C2 = {C2}");

                // Vertical concatenation
                ColVec C3 = Vcart(C1, C2);
                Console.WriteLine($"C3 = {C3}");
            }
            /// </code>
            /// 
            /// <header 2> Flipping a Matrix </header>
            /// We can flip a Matrix vertically (flipud) or horizontally (fliplr). 
            /// 
            /// <code>
            {

                Matrix M = new double[,]
                {
                    {5, -2, 3, 7 },
                    {2, 1, -7, 3 },
                    {4, 8, 9, 1 },
                    {0, 5, -6, -3 }
                };
                Console.WriteLine($"M = {M}");
                Console.WriteLine($"Flipud(M) = {Flipud(M)}");
                Console.WriteLine($"Fliplr(M) = {Fliplr(M)}");
            }
            /// </code>
            /// 
            /// <header 2> Extract a Triangular Portion of Matrix </header>
            /// <code>
            {
                Matrix M = new double[,]
                {
                    {5, -2, 3, 7 },
                    {2, 1, -7, 3 },
                    {4, 8, 9, 1 },
                    {0, 5, -6, -3 }
                };

                Console.WriteLine($"Triu(M) = {Triu(M)}");
                Console.WriteLine($"Tril(M) = {Tril(M)}");

            }
            /// </code>
            /// 
            /// </BookContent>
            
        }

    }
}
