using CSharpMath.Atom.Atoms;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.Palettes;
using ScottPlot.TickGenerators.Financial;
using ScottPlot.TickGenerators.TimeUnits;
using ScottPlot.Triangulation;
using SepalSolver;
using System.Runtime.Intrinsics.Arm;

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
            /// <header 2> Reodering </header>
            /// Matrix rearrangement (or reordering) aims to find a permutation matrix :math:`P` such that the factorization of :math:`PAP^T` minimizes **fill-in**.
            /// 
            /// **Reverse Cuthill-McKee(RCM)** Reduces the **bandwidth**of the matrix by clustering non-zeros near the diagonal.Ideal for simpler, structured systems.
            /// 
            /// **Minimum Degree(MD)** A greedy approach that eliminates the vertex with the lowest degree first.This is a local optimization strategy.
            /// 
            /// **Nested Dissection(ND)** A "divide and conquer" approach using graph separators.

            ///..image::https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Sparse_matrix_fill-in.svg/400px-Sparse_matrix_fill-in.svg.png
            ///   :alt: Diagram showing fill-in during factorization
            ///   :align: center
            ///   
            /// <table> 
            /// Strategy          | Logic              | Pros                           | Cons              
            /// **RCM** | Bandwidth Reduction| Fast; simple memory access     | High total fill-in risk   
            /// **Minimum Degree**| Local Greedy       | Great for general matrices     | Slow on massive systems   
            /// **Nested Diss.** | Divide & Conquer   | Best for 3D grids/parallelism  | Complex implementation    
            /// </table>
            /// 
            /// ..note::
            /// 
            ///    The fill-in is governed by the elimination tree of the matrix.A "bushy" tree allows for more parallel factorization.
            /// 
            ///  
            /// <code>
            {
                // Load squid matrix 
                SparseMatrix S = SparseMatrix.Squid();

                // Add more weight to the diagonal
                S += 20 * SparseMatrix.Eye(S.Rows);

                // Visualize the sparsity pattern
                Subplot(2, 2, 0); Spy(S); AxisEqual();
                Title("Squid");

                // Perform cholesky factorization
                S.MakeChol();

                // Visualize the sparsity pattern of the cholesky factor
                Subplot(2, 2, 1); Spy(S.L_chol); AxisEqual();
                Title("Cholesky factor of Squid");

                // Compute RCM reordering permutation
                Indexer I = SparseMatrix.Symrcm(S);

                // Reorder the squid
                SparseMatrix T = S[I, I];

                // Visualize reordered matrix
                Subplot(2, 2, 2); Spy(T, 1e-15); AxisEqual();
                Title("Reodered Squid");

                // Perform cholesky factorization of the 
                T.MakeChol();

                // Visualize the cholesky factor of the reordered matrix
                Subplot(2, 2, 3); Spy(T.L_chol); AxisEqual();
                Title("Cholesky factor of reodered Squid");

                SaveAs("RCM_reordering_of_Squid.png");
            }
            /// </code>
            /// 
            /// <code>
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
