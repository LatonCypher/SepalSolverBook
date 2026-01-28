namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algerba
{
    internal class Section_01_Vectors_and_Matrices
    {
        public static void Run()
        {
            /// <BookContent>
            /// Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: RowVec, ColVec and Matrix. RowVec and ColVec are 1D arrays while Matrix is a 2D array. 
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

            


            //Sparse Matrix
            {
                Matrix A = new double[,] { {  5, -2,  0, -2, -2},
                                           { -2,  5, -2,  0,  0},
                                           {  0, -2,  5, -2,  0},
                                           { -2,  0, -2,  5, -2},
                                           { -2,  0,  0, -2,  5}};

                SparseMatrix B = new SparseMatrix(A);
                B.MakeiLU();
                Console.WriteLine(B.L_lu.Full());
                Console.WriteLine(B.U_lu.Full());
                Console.WriteLine((B.L_lu * B.U_lu).Full());
            }

            {
                Matrix A = new double[,] { {  5,  0,  0,  0,  0},
                                           { -2,  5,  0,  0,  0},
                                           {  0, -2,  5,  0,  0},
                                           { -2,  0, -2,  5,  0},
                                           { -2,  0,  0, -2,  5}};

                SparseMatrix B = new SparseMatrix(A);
                B.MakeiChol();
                Console.WriteLine(B.L_chol);
                Console.WriteLine(B.L_chol* B.L_chol.T);
            }

            {
                SparseMatrix M; int N = 1;
                Indexer I, J; ColVec V;
                for (int j = 0; j < 4; j++)
                {
                    N *= 20; V = Rand(N); V += 5;
                    I = new(0, N); J = new(0, N);
                    M = new SparseMatrix(I, J, V.ToArray(), N, N);
                    V = Rand(N - 1); I = new(1, N); J = new(0, N - 1);
                    M = M + new SparseMatrix(I, J, V.ToArray(), N, N) + new SparseMatrix(J, I, V.ToArray(), N, N);
                    V = Rand(N - 2); I = new(2, N); J = new(0, N - 2);
                    M = M + new SparseMatrix(I, J, V.ToArray(), N, N) + new SparseMatrix(J, I, V.ToArray(), N, N);
                    M.MakeChol();
                }
            }


            {
                Matrix A = new double[,] { { 22.7345,    1.8859,         0,         0,    1.3000 },
                                           {  1.8859,   22.2340,    2.0461,         0,         0 },
                                           {       0,     2.0461,   22.7591,    2.4606,         0 },
                                           {       0,          0,    2.4606,   22.5848,    2.2768 },
                                           {  1.3000,          0,         0,    2.2768,   22.4853 } };

                SparseMatrix B = new SparseMatrix(A);
                B.MakeChol();
                Console.WriteLine(B.L_chol);
            }

            {
                SparseMatrix S = SparseMatrix.Squid(), Sc, Si, Sr, Sic, Sim1;
                S = S + 20 * SparseMatrix.Eye(S.Rows);
                Indexer I = new(0, 2, 40);
                //S = S[I, I];

                Spy(S);
                S.MakeChol();
                Sc = S.L_chol;

                Spy(Sc);
                Sr = Sc * Sc.T;
                Spy(Sr, 1e-15);

                S.MakeiChol();
                Sc = S.L_chol;

                Spy(Sc);
                Sr = Sc * Sc.T;
                Spy(Sr, 1e-15);



                I = SparseMatrix.Symrcm(S);
                Si = S[I, I];
                Spy(Si, 1e-15);

                Si.MakeChol();
                Sic = Si.L_chol;
                Spy(Sic);

                Sim1 = Sic * Sic.T;
                Spy(Sim1, 1e-15);

                Si.MakeiChol();
                Sic = Si.L_chol;
                Spy(Sic);

                Sim1 = Sic * Sic.T;
                Spy(Sim1, 1e-15);
            }

            {
                SparseMatrix B = SparseMatrix.Bucky(), R, S;
                B = B + 4 * SparseMatrix.Eye(60);
                PermIndexer r = SparseMatrix.Symrcm(B), p = SparseMatrix.Symamd(B);
                R = B[r, r]; S = B[p, p]; B.MakeChol(); R.MakeChol(); S.MakeChol();

                Spy(B, 1e-15);
                Spy(B.L_chol, 1e-15);
                Spy(B.L_chol * B.L_chol.T, 1e-15);

                Spy(R, 1e-15);
                Spy(R.L_chol, 1e-15);
                Spy(R.L_chol * R.L_chol.T, 1e-15);

                Spy(S, 1e-15);
                Spy(S.L_chol, 1e-15);
                Spy(S.L_chol * S.L_chol.T, 1e-15);
            }

            {
                SparseMatrix B = SparseMatrix.Bucky();
                Spy(B, 1e-15);

                B.MakeLU();
                var pT = B.pi.T;
                SparseMatrix L = B.L_lu, U = B.U_lu;

                Spy(L, 1e-15);
                Spy(U, 1e-15);
                Spy(L[pT, ""] * U, 1e-15);

            }
        }

    }
}
