namespace ConsoleApp1.TrainingFiles.Chapter_06_Solution_of_Nonlinear_System
{
    internal class Section_3_Large_NonLinear_Systems
    {
        public static void Run()
        {
            /// <BookContent>
            /// <code>
            {
                //Large Nonlinear Systems
                int n = 100000;
                Indexer i = new(0, n), j = new(0, n - 1), jp1 = j + 1,
                    l = new(1, n - 1), lp1 = l + 1, lm1 = l - 1;

                ColVec a = Ones(n-1), b = Ones(n), e = -a,
                    c = 2 * e, d, xstart, F = new double[n];

                SparseMatrix C, D, E;

                ColVec nlsf(ColVec x)
                {
                    F[l] = (3 - 2 * x[l]).Times(x[l]) - x[lm1] - 2 * x[lp1] + 1;
                    F[n - 1] = (3 - 2 * x[n - 1]) * x[n - 1] - x[n - 2] + 1;
                    F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
                    return F;
                }

                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    d = -4 * x + 3 * b;
                    D = new(i, i, d, n, n);
                    C = new(j, jp1, c, n, n);
                    E = new(jp1, j, e, n, n);
                    return C + D + E;
                };

                var opts = SolverSet(Display: true);
                opts.UserDefinedJacobian = Jac;
                xstart = -b;

                var result = Fsolve(nlsf, xstart, opts);
            }

            /// </code>
            /// <code>
            {
                // Large Nonlinear systems
                int n = 10000;
                Indexer odds = new(0, 2, n), evens = odds + 1;
                ColVec xstart = new double[n], One = Ones(n / 2),
                    c = -One, d = 10*One, e, F;
                SparseMatrix C, D, E;

                ColVec multirosenbrook(ColVec x)
                {
                    // Evaluate the vector function
                    F = new double[n];
                    F[odds] = 1 - x[odds];
                    F[evens] = 10 * (x[evens] - x[odds].Pow(2));
                    return F;
                }

                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    C = new(odds, odds, c, n, n);
                    D = new(evens, evens, d, n, n);
                    e = -20 * x[odds];
                    E = new(evens, odds, e, n, n);
                    return C + D + E;
                };

                var opts = SolverSet(Display: true);
                opts.UserDefinedJacobian = Jac;
                xstart[odds] = -1.9; xstart[evens] = 2;
                var result = Fsolve(multirosenbrook, xstart, opts);
            }

            /// </code>
            /// </BookContent>
        }
    }
}
