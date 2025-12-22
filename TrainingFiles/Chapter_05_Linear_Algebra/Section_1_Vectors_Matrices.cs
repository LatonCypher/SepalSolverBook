using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algerba
{
    internal class Section_1_Vectors_Matrices
    {
        public static void Run()
        {
            /*
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             */

            {
                // Vector declaration
                ColVec C = new double[] { 1, 2, 3, 4, 5 };
                RowVec R = new double[] { 1, 2, 3, 4, 5 };

                // Matrix declaration
                Matrix M = new double[,] {{ 1, 2, 3 },
                                          { 4, 5, 6 },
                                          { 7, 8, 9 }};

                // Identity Matrix
                Matrix Identity = Eye(5);

                // Zero Matrix
                Matrix Zero = Zeros(10, 10);

                // One Matrix
                Matrix One = Ones(10, 10);

                // Repmat Matrix
                Matrix Two = Repmat(2, 3, 5);

                // Random Matrix
                Matrix Random = Rand(5, 7);

                // Printing Vectors and Matrix
                Console.WriteLine($"Vector C: {C}");
                Console.WriteLine($"Vector R: {R}");
                Console.WriteLine($"Matrix M: {M}");
                Console.WriteLine($"Identity Matrix = {Identity}");
                Console.WriteLine($"Zero Matrix = {Zero}");
                Console.WriteLine($"One Matrix = {One}");
                Console.WriteLine($"Two Matrix = {Two}");
                Console.WriteLine($"Random Matrix = {Random}");


                // To extract element at index 2 and 3 from C
                Indexer i = new(2, 4);
                Console.WriteLine($"C = {C}");
                Console.WriteLine($"Sub of C = {C[i]}");

                // To extract element at index 1, 2, 3 and 4 from R
                Indexer j = new(1, 5);
                Console.WriteLine($"R = {R}");
                Console.WriteLine($"Sub of R = {R[j]}");

                // To extract the submatrix rows 2 and 3, and columns 1 to 4 from random matrix.
                Console.WriteLine($"Random Matrix = {Random}");
                Console.WriteLine($"Sub of Random Matrix = {Random[i, j]}");

                // Modify Random
                Random[1, 1] = 79;
                Console.WriteLine($"Random Matrix = {Random}");

                // Set all row 3 of Random to 15
                Random[3, ""] = 15;
                Console.WriteLine($"Random Matrix = {Random}");

                // Set all column 3 of Random to 15
                Random["", 4] = 2.3946;
                Console.WriteLine($"Random Matrix = {Random}");


                // Delete Column 3 of Random
                Random[4, ""] = null;
                Console.WriteLine($"Random Matrix = {Random}");

                // Delete Row 2 of Random
                Random["", 2] = null;
                Console.WriteLine($"Random Matrix = {Random}");

                Environment.Exit(0); // 0 indicates successful termination

            }

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
