namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algebra
{
    internal class Section_08_Sparse_Matrices
    {
        public static void Run()
        {
            /// <BookContent>
            /// <code>
            {
                // Incomplete LU Factorization of a Sparse Matrix
                Matrix A = new double[,] { {  5, -2,  0, -2, -2},
                                           { -2,  5, -2,  0,  0},
                                           {  0, -2,  5, -2,  0},
                                           { -2,  0, -2,  5, -2},
                                           { -2,  0,  0, -2,  5} };

                SparseMatrix B = new(A);
                B.MakeiLU();
                Console.WriteLine($"L = {B.L_lu.Full()}");
                Console.WriteLine($"U = {B.U_lu.Full()}");
                Console.WriteLine($"L * U = {(B.L_lu * B.U_lu).Full()}");
                Spy(B.L_lu); 
                Title("L from Incomplete LU Factorization of B");
                SaveAs("L_from_Incomplete_LU_Factorization_of_B.png");
                Spy(B.U_lu);
                Title("U from Incomplete LU Factorization of B");
                SaveAs("U_from_Incomplete_LU_Factorization_of _B.png");
            }
            /// </code>
            /// 
            /// <code>
            {
                // Incomplete Cholesky Factorization of a Sparse Matrix
                Matrix A = new double[,] { {  5,  0,  0,  0,  0},
                                           { -2,  5,  0,  0,  0},
                                           {  0, -2,  5,  0,  0},
                                           { -2,  0, -2,  5,  0},
                                           { -2,  0,  0, -2,  5}};

                SparseMatrix B = new(A);
                B.MakeiChol();
                Console.WriteLine($"L = {B.L_chol}");
                Console.WriteLine($"L*LT = {B.L_chol* B.L_chol.T}");

                Spy(B.L_chol);
                Title("L from Incomplete Factorization of B");
                SaveAs("L_from_Incomplete_Cholesky_Factorization_of_B.png");
            }
            /// </code>
            /// 
            /// <code>
            {
                Matrix A = new double[,] { { 22.7345,    1.8859,         0,         0,    1.3000 },
                                           {  1.8859,   22.2340,    2.0461,         0,         0 },
                                           {       0,     2.0461,   22.7591,    2.4606,         0 },
                                           {       0,          0,    2.4606,   22.5848,    2.2768 },
                                           {  1.3000,          0,         0,    2.2768,   22.4853 } };

                SparseMatrix B = new (A);
                B.MakeChol();
                Console.WriteLine(B.L_chol);
            }
            /// </code>
            /// 
            /// <code>
            {

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
            }
            /// </code>
            /// 
            /// <code>
            {

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
            }
            /// </code>
            /// 
            /// <code>
            {

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
            /// </code>
            
            /// </BookContent>
        }
    }
}
