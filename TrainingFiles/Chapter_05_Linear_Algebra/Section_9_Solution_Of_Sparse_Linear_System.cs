using SepalSolver;
using static SepalSolver.Math;
using System.Diagnostics;
using static SepalSolver.PlotLib.Chart;

namespace ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algebra
{
    public class Section_9_Solution_Of_Sparse_Linear_System
    {

        public static void Run()
        {

            {
                SparseMatrix B = SparseMatrix.Bucky(), R, S;
                B = B + 4 * SparseMatrix.Eye(60);
                Indexer r = SparseMatrix.Symrcm(B), p = SparseMatrix.Symamd(B);
                R = B[r, r]; S = B[p, p]; 

                B.MakeChol();
                Spy(B, 1e-15);
                Spy(B.L_chol, 1e-15);
                Spy(B.L_chol * B.L_chol.T, 1e-15);


                R.MakeChol();
                Spy(R, 1e-15);
                Spy(R.L_chol, 1e-15);
                Spy(R.L_chol * R.L_chol.T, 1e-15);


                S.MakeChol();
                Spy(S, 1e-15);
                Spy(S.L_chol, 1e-15);
                Spy(S.L_chol * S.L_chol.T, 1e-15);
            }

            {
                SparseMatrix B = SparseMatrix.Bucky(), L, U;
                Spy(B, 1e-15);


                B.MakeLU();
                L = B.L_lu; U = B.U_lu;
                Spy(L[B.pi.T, ""], 1e-15);


                Spy(U, 1e-15);
                var pT = B.pi.T;
                Spy(pT * L * U, 1e-15);


                Spy(L[pT, ""] * U, 1e-15);
                Spy(L, 1e-15);
                Spy(U, 1e-15);
            }



            {
                int N = 20001;
                double tol = 1e-6, v = N/2;
                ColVec b = Ones(N);
                int maxiter = 1000;
                List<double> V = [];
                List<int> I = [], J = [];
                for (int i = 0; i<N;)
                {
                    V.Add(Abs(v--)); I.Add(i); J.Add(i);
                    if (i > 0) { V.Add(1); I.Add(i); J.Add(i-1); }
                    if (++i<N) { V.Add(1); I.Add(i-1); J.Add(i); }
                }
                SparseMatrix A = new SparseMatrix(I, J, V, N, N);
                //Console.WriteLine(A.Full());

                Stopwatch timer = new Stopwatch();
                timer.Start();
                var Output  = ConjGrad(A, b, tol, maxiter);
                timer.Stop();
                
                //double resnorm = resvec.Norm();
            }

            {
                int N = 20001;
                double tol = 1e-6, v = N/2;
                ColVec b = Ones(N);
                int maxiter = 1000;
                List<double> V = [];
                List<int> I = [], J = [];
                for (int i = 0; i<N;)
                {
                    V.Add(Abs(v--)); I.Add(i); J.Add(i);
                    if (i > 0) { V.Add(1); I.Add(i); J.Add(i-1); }
                    if (++i<N) { V.Add(1); I.Add(i-1); J.Add(i); }
                }
                SparseMatrix A = new SparseMatrix(I, J, V, N, N);
                //Console.WriteLine(A.Full());

                Stopwatch timer = new Stopwatch();
                timer.Start();
                var Output = GenMinRes(A, b, tol, maxiter);
                timer.Stop();
            }
        }
    }
}
