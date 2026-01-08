// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using ConsoleApp1.TrainingFiles;
using ConsoleApp1.TrainingFiles.Chapter_0_Basic_Operations_Syntax;
using ConsoleApp1.TrainingFiles.Chapter_1_Interpolation;
using ConsoleApp1.TrainingFiles.Chapter_1_Polynomials;
using ConsoleApp1.TrainingFiles.Chapter_4_Linear_Algerba;
using ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Process_Engineering;

{
    var BookInfo = new
    {
        Title = "SepalSolver User Guide",
        Author = "Lateef A. Kareem",
        Version = "1.2.0",
    };

    var ProjectFolder = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\TrainingFiles\";
    var BookFolder = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\docs\source\";
    var ImageFolder = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\docs\source\images\";
    var BookContent = new BookWriter(ProjectFolder, BookFolder);
    BookContent.WriteBook();
    {
        // Solve Nonlinear System of Polynomials
        Matrix A = new double[,]
        {
            {1, 2},
            {3, 4}
        };
        var opts = SolverSet(Display: true);
        Matrix x = Fsolve(x => x*x*x - A, Ones(2, 2), opts);
        Console.WriteLine(x);
    }

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

    Last_Chapter_Advanced_Science_And_Engineering_Applications.Run();
    { 
        Matrix A = new double[,] { { 8,    1,    6,    1,  16 },
                                   { 3,    5,    6,    2,  15 },
                                   { 4,    7,    2,    1,  14 } };

        // Extract single element using subscript
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
    }

    {
        // Vector declaration
        ColVec C = new double[] { 1, 2, 3, 4, 5 };
        RowVec R = new double[] { 1, 2, 3, 4, 5 };

        // Matrix declaration
        Matrix M = new double[,] { { 1, 2, 3, 4, 5},
                                   { 4, 5, 6, 4, 5},
                                   { 7, 8, 9, 4, 5} };

        // Printing Vectors and Matrix
        Console.WriteLine($"Vector C: {C}");
        Console.WriteLine($"Vector R: {R}");
        Console.WriteLine($"Matrix M: {M}");

        // Operations Involving Matrices and Vectors
        Console.WriteLine($"C * R = {C * R}");
        Console.WriteLine($"C + R = {C + R}");
        Console.WriteLine($"C - R = {C - R}");
        Console.WriteLine($"R * C = {R * C}");
        Console.WriteLine($"R + C = {R + C}");
        Console.WriteLine($"R - C = {R - C}");

        // Transpose
        Console.WriteLine($"Vector C Transpose: {C.T}");

        // Matrix Multiplication
        Console.WriteLine($"M * C = {M * C}");

        // TermWise Operation
        ColVec V = new double[] { 3, 2, 1, -1, -2 },
               U = new double[] { 2, 3, 1, 1, 2 };

        Console.WriteLine($"V.*U = {V.Times(U)}");
        Console.WriteLine($"V.^U = {V.Pow(U)}");
        Console.WriteLine($"V./U = {V.Div(U)}");

        // Solution of Linear System
        Matrix A = Rand(7, 7);
        ColVec b = Rand(7);
        ColVec x = Mldivide(A, b);


        Console.WriteLine($"A = {A}");
        Console.WriteLine($"b = {b}");
        Console.WriteLine($"x = {x}");

    }

    {
        //double Fs = 1000;            // Sampling frequency
        //double T = 1/Fs;             // Sampling period
        //int L = 1500;                // Length of signal
        //Indexer I = new(0, L);       // Indexer for the signal
        //ColVec t = I*T;              // Time vector

        //ColVec S = 0.8 + 0.7*Sin(2*pi*50*t) + Sin(2*pi*120*t);
        //ColVec Noise = Randn(t.Numel);
        //ColVec X = S + 2*Noise;

        //Plot(1000*t, X);
        //Title("Signal Corrupted with Zero-Mean Random Noise");
        //Xlabel("t (milliseconds)");
        //Ylabel("X(t)");
    }

    {
        //ColVec xdata, ydata, x, y_est; double[] x0;
        //xdata = new double[] { 0.9, 1.5, 13.8, 19.8, 24.1, 28.2, 35.2, 60.3, 74.6, 81.3 };
        //ydata = new double[] { 455.2, 428.6, 124.1, 67.3, 43.2, 28.1, 13.1, -0.4, -1.3, -1.5 };

        //x0 = [100, -1];
        //static ColVec model(ColVec x, ColVec xdata) => x[0] * Exp(x[1] * xdata);
        //var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
        //var ans = Lsqcurvefit(model, x0, xdata, ydata, options: opts);
        //Console.WriteLine($"x = {ans.x.T}");

        //x = Linspace(xdata.First(), xdata.Last());
        //Scatter(xdata, ydata); hold = true;
        //Plot(x, y_est = model(ans.x, x), "r", Linewidth: 2);
        //Axis([xdata.Min()-0.01*xdata.Range(), xdata.Max()+0.01*xdata.Range(),
        //     ydata.Min()-0.1*ydata.Range(), ydata.Max()+0.1*ydata.Range()]);

        //Xlabel("x"); Ylabel("y"); Legend(["Measured Data", "Model Estimate"], Alignment.UpperRight);
        //Title("Example of CurveFitting using Lsqcurvefit");
        //SaveAs("LMTest1.png");

        //AnimateHistory(model, xdata, ydata, ans.output);
    }

    {
        //Func<double, double> f = x => x*BesselJ(0, 2*x) - BesselJ(1, 3*x);
        //var  options = SolverSet(Display: true);
        //var x = NextZero(f, 3);
        //Console.WriteLine(x);

        //x = NextZero(f, x+0.5);
        //Console.WriteLine(x);

        //x = NextZero(f, x+0.5);
        //Console.WriteLine(x);
    }

    {
        //Func<double, double> f = x => Pow(Abs(x-1.0/7), -0.5)*Pow(Abs(x-2.0/3), -0.15);
        //double I = Integral(f, 0, 1, 1e-6, [1.0/7, 2.0/3]);
        //Console.WriteLine(I);
    }

    {
        //double ymin, ds;
        //ColVec xtop, ytop, xbase, ybase;
        //RowVec s, s1, s2, s3, S1, S2, S3, Lx, Ly, L = 0, Tx, Ty, T = 0, t;
        //Indexer Gas_idx, Oil_idx, Water_idx, Base_idx, ps1, ps2, ps3;

        //xtop = Linspace(-3, 3, 5001);
        //// Impermeable CapRock
        //ytop = 9 - PowTW(xtop, 2);
        //// Gas
        //Gas_idx = ytop >= 6;
        //// Oil
        //Oil_idx = (Oil_idx = 3 <= ytop) & (Oil_idx = ytop <= 6);
        //// Water
        //Water_idx =  ytop <= 3;
        //// Impermeable BaseRock
        //ymin = ytop.Min();
        //xbase = xtop.ToArray();
        //ybase = 5 - PowTW(xbase, 2);
        //Base_idx = ybase > ymin;
        //// Derick
        //RowVec dx = Linspace(0.2, 0.5, 5), dy = 8.75-8.5*dx;
        //Matrix Dx = Vcart(dx, -dx), Dy = Vcart(dy, dy);
        //ColVec DX = Dx.ToArray(), DY = Dy.ToArray();
        ////Supply line
        //Lx = new double[] { 0, 0, 6.0 };
        //Ly = new double[] { 2.6, 5.5, 5.5 };
        //L = Hcart(L, Hypot(Diff(Lx), Diff(Ly))).CumSum();
        //s = Linspace(0, L.Last(), 31);
        ////Injection line
        //Tx = new double[] { -2.0, -2.0, -5 };
        //Ty = new double[] { 1.0, 5.5, 5.5 };
        //T = Hcart(T, Hypot(Diff(Tx), Diff(Ty))).CumSum();
        //t = Linspace(0, T.Last(), 31);
        //void PlotFun(int i)
        //{
        //    Subplot(2, 2, 2*i);
        //    Plot(xtop, 0.5*ytop, "k", Linewidth: 5); hold = true;
        //    Fill(xtop[Gas_idx], 0.5*ytop[Gas_idx], "g");
        //    Fill(xtop[Oil_idx], 0.5*ytop[Oil_idx], "r");
        //    Fill(xtop[Water_idx], 0.5 * ytop[Water_idx], "b");
        //    Fill(xbase[Base_idx], 0.5*ybase[Base_idx], "k");
        //    Rectangle([-0.5, 7, 1, 0.25], 0, "k");
        //    Plot(dx, dy, "k", Linewidth: 2);
        //    Plot(-dx, dy, "k", Linewidth: 2);
        //    Plot(DX, DY, "k", Linewidth: 2);
        //    Plot(-DX, DY, "k", Linewidth: 2);
        //    Scatter(Lx[1], Ly[1], "fok");

        //    Rectangle([6.0, 5, 0.5, 1], 1, "w", Linewidth: 3);
        //    Rectangle([6.5, 5, 0.5, 1], 1, "w", Linewidth: 3);
        //    Rectangle([7.0, 5, 0.5, 1], 1, "w", Linewidth: 3);
        //    Rectangle([7.5, 5, 0.5, 1], 1, "w", Linewidth: 3);
        //    Rectangle([8, 3, 2, 5], 1, "w", Linewidth: 3);
        //    Rectangle([5, 3, 6, 2], 1, "w", Linewidth: 3);
        //    Rectangle([5, 6, 3, 1], 0, "w", Linewidth: 3);

        //    Plot(Lx, Ly, "k", Linewidth: 3);
        //}

        //double q1 = 0, q2 = 0, dt = 1; ds = Sqrt(5)/10;
        //List<double> X = [0], Y1 = [q1], Y2 = [q2];
        //byte[] AnimatingFunction(int i)
        //{
        //    Console.WriteLine(i);
        //    DeleteChart();
        //    PlotFun(0);
        //    q1 = 3*dt*random.NextDouble() * Exp(-X.Last()/500);
        //    s1 = s + q1*(i-1)*ds; ps1 = s1 > 0;
        //    S1 = Mod(s1[ps1], L.Last());
        //    Plot(Vcart(Interp1(L, Lx, S1), Interp1(L, Lx, S1 + 0.2)),
        //         Vcart(Interp1(L, Ly, S1), Interp1(L, Ly, S1 + 0.2)),
        //         "r", Linewidth: 2); Axis([-3.5, 11.5, -0.5, 8.5]);

        //    PlotFun(1);
        //    q2 = 3*dt*random.NextDouble();
        //    s2 = s + q2*(i-1)*ds; ps2 = s2 > 0;
        //    S2 = Mod(s2[ps2], L.Last());
        //    Plot(Vcart(Interp1(L, Lx, S2), Interp1(L, Lx, S2 + 0.2)),
        //         Vcart(Interp1(L, Ly, S2), Interp1(L, Ly, S2 + 0.2)),
        //         "r", Linewidth: 2);

        //    Plot(Tx, Ty, "k", Linewidth: 3);
        //    s3 = t + 3*(i-1)*ds; ps3 = s3 > 0;
        //    S3 = Mod(s3[ps3], T.Last());
        //    Plot(Vcart(Interp1(T, Tx, S3), Interp1(T, Tx, S3 + 0.2)),
        //         Vcart(Interp1(T, Ty, S3), Interp1(T, Ty, S3 + 0.2)),
        //         "b", Linewidth: 2); Axis([-3.5, 11.5, -0.5, 8.5]);

        //    X.Add(X.Last() + dt);
        //    Y1.Add(Y1.Last() + q1);
        //    Y2.Add(Y2.Last() + q2);
        //    Subplot(1, 2, 1);
        //    Plot((ColVec)X, (ColVec)Y1, "r"); hold = true;
        //    Plot((ColVec)X, (ColVec)Y2, "r", Linewidth: 2);
        //    Axis([0, 500, 0, 750]); hold = false;
        //    Xlabel("time(Days)"); Ylabel("CumProd(MSTB/Day)");
        //    Legend(["No Pressure Maintenance", "With Pressure Maintenance"], Alignment.UpperLeft);
        //    return GetImageBytes(1100, 700);
        //}
        //Animation.Make(AnimatingFunction, "ProdOptim.gif", 10, 501);
    }

    {
        //ColVec x1 = Linspace(0, 5, 500),
        //       x2 = Cos(6.2957*x1),
        //       y1 = Exp(-0.5*x1),
        //       y2 = Exp(-0.5*Sqr(x1)),
        //       y3 = Exp(-0.5*Sqrt(5.0-x1)),
        //       y4 = Sin(8.8141*x1);
        //Matrix Y = new List<ColVec> { y1, y2, y3 };
        //Subplot(1, 1, 1);
        //Plot(x1, Y);

        //Subplot(3, 3, 1);
        //Plot(x2, y4);
        //SaveAs("plottesting.png");
    }

    {
        //// Example of finding roots of a polynomial with complex coefficients
        //double[] Coeffs = [2, 3, 4, 2, 7, 4];
        //Complex[] roots = Roots(Coeffs);

        //// Print the result
        //Console.WriteLine($"Roots:\n {string.Join("\n ", roots)}");
    }

    {
        //double x = 3e200, y = 4e200;
        //var hyp = Sqrt(Pow(x, 2)+Pow(y, 2));
        //var hypotenuse = Hypot(x, y);
        //Console.WriteLine(hyp);
        //Console.WriteLine(hypotenuse);
    }
}




