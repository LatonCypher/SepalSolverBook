using ConsoleApp1;
{
    {
        //double BubblePointPressure(double[] Z, double[] Tc, double[] Tb, double[] Pc, double[] W, double T, double Psc)
        //{
        //    double[] TcInv = [.. Tc.Select(t => 1 / t)], TbInv = [.. Tb.Select(t => 1 / t)],
        //        aPoly = [15e-8, 4.5e-4, 1.2], bPoly = [-3.5e-8, -1.7e-4, 0.89];
        //    double Tinv = 1 / T;
        //    double PbFunc(double Pb)
        //    {
        //        double a = Polyval(aPoly, Pb), b = Polyval(bPoly, Pb), s = 0, F, K;
        //        for (int i = 0; i < 3; i++)
        //        {
        //            F = (TbInv[i] - Tinv) / (TbInv[i] - TcInv[i]) * Log10(Pc[i] / Psc);
        //            K = Pow(10, a + b * F) / Pb; s += Z[i] * K;
        //        }
        //        return s - 1;
        //    }
        //    double Pb0 = 0;
        //    for (int i = 0; i < 3; i++)
        //        Pb0 += Z[i] * Pc[i] * Exp(5.371 * (1 + W[i]) * (1 - Tc[i] / T));
        //    double Pb = Fsolve(PbFunc, Pb0);
        //    return Pb;
        //}

        //double DewPointPressure(double[] Z, double[] Tc, double[] Tb, double[] Pc, double[] W, double T, double Psc)
        //{
        //    double[] TcInv = [.. Tc.Select(t => 1 / t)], TbInv = [.. Tb.Select(t => 1 / t)],
        //        aPoly = [15e-8, 4.5e-4, 1.2], bPoly = [-3.5e-8, -1.7e-4, 0.89];
        //    double Tinv = 1 / T;
        //    double PdFunc(double Pd)
        //    {
        //        double a = Polyval(aPoly, Pd), b = Polyval(bPoly, Pd), s = 0, F, K;
        //        for (int i = 0; i < 3; i++)
        //        {
        //            F = (TbInv[i] - Tinv) / (TbInv[i] - TcInv[i]) * Log10(Pc[i] / Psc);
        //            K = Pow(10, a + b * F) / Pd; s += Z[i] / K;
        //        }
        //        return s - 1;
        //    }
        //    double Pd0 = 0;
        //    for (int i = 0; i < 3; i++)
        //        Pd0 += Z[i] /(Pc[i] * Exp(5.371 * (1 + W[i]) * (1 - Tc[i] / T)));
        //    double Pd = Fsolve(PdFunc, Pd0);
        //    return Pd;
        //}

        //string[] Components = ["C3", "C4", "C5"];
        //double[] Z = [0.6, 0.3, 0.1], Mw = [44.09, 58.12, 72.15];
        //double[] Tc = [665.7, 765.3, 845.4], TcInv = [.. Tc.Select(t => 1 / t)];
        //double[] Tb = [416, 490.8, 556.6], TbInv = [.. Tb.Select(t => 1 / t)];
        //double[] Pc = [616.3, 550.7, 488.6], W = [0.1454, 0.1928, 0.2510];
        //double T = 760, Tinv = 1/T, Psc = 14.7, Pb, Pd;
        //List<double> Plist = [], Tlist = [];
        //while (T > 350)
        //{
        //    Pb = BubblePointPressure(Z, Tc, Tb, Pc, W, T, Psc);
        //    Pd = DewPointPressure(Z, Tc, Tb, Pc, W, T, Psc);
        //    Console.WriteLine($"T = {T:0.00} K, Bubble Point Pressure = {Pb:0.00} psia, Dew Point Pressure = {Pd:0.00} psia");
        //    Plist.Add(Pd); Plist.Insert(0, Pb); Tlist.Add(T); Tlist.Insert(0, T);
        //    T -= 10; Tinv = 1 / T;
        //}
        //Plot([..Tlist], [..Plist], Linewidth: 2);
    }

    static Matrix Strass(Matrix A, Matrix B)
    {
        if (A.Cols != B.Rows)
            throw new Exception("Matrices are not conformable for multiplication");
        if (A.Cols == 1)
            return A[0, 0] * B[0, 0];
        else
        {
            // get matrix size
            int N = A.Cols / 2; 

            // split A and B into submatrices
            Matrix A11 = A[..N, ..N], A12 = A[..N, N..], B11 = B[..N, ..N], B12 = B[..N, N..],
                   A21 = A[N.., ..N], A22 = A[N.., N..], B21 = B[N.., ..N], B22 = B[N.., N..],

            // compute the strassen submatrices (7 multiplication)
            M1 = Strass(A11 + A22, B11 + B22),
            M2 = Strass(A21 + A22, B11),
            M3 = Strass(A11, B12 - B22),
            M4 = Strass(A22, B21 - B11),
            M5 = Strass(A11 + A12, B22),
            M6 = Strass(A21 - A11, B11 + B12),
            M7 = Strass(A12 - A22, B21 + B22);

            // compose the result
            return new Matrix[,]
            {
                { M1 + M4 - M5 + M7,        M3 + M5      },
                {      M2 + M4,        M1 - M2 + M3 + M6 }
            };
        }
    }


    {
        folderpath = "C:\\Users\\lateef.a.kareem\\Documents";
        //{
        //    // SOLVE_BURGERS_EQUATION Solves the 1D burgers equation using pdepe
        //    // Equation:  du/dt = nu * du/dx - u*du/dx
        //    // Domain:    x in [-1, 1],  t in [0, 1]
        //    // IC:        u(x,0) = 100
        //    // BC:        u(0,t) = 0,  du/dr(1,t) = h*(U(1,0) - Tinf)
        //    //
        //    // 1. Model Parameters & Mesh Setup
        //    int m = 0; // Slab geometry
        //    double nu = 0.05; // Kinematic viscosity

        //    double[] x = Linspace(-1, 1, 101);  // [-1, 1]
        //    double[] t = Linspace(0, 1, 6);     // [0, 1]

        //    // 1. PDE Components
        //    (double c, double f, double s) PdeFun(double x, double t, double u, double dudx)
        //    {
        //        double capacity = 1.0;
        //        double flux = nu * dudx - 0.5 * u * u;
        //        double source = 0.0;
        //        return (capacity, flux, source);
        //    }

        //    // 2. Initial Condition: Sinusoidal disturbance
        //    double IcFun(double x) => -Sin(pi * x);

        //    // 3. Boundary Conditions: Fixed zero velocity at endpoints (Dirichlet)
        //    (double pl, double ql, double pr, double qr) BcFun(double xl, double ul, double xr, double ur, double t)
        //        => (ul, 0.0, ur, 0.0);

        //    // 4. Solve PDE
        //    (ColVec T, Matrix U) = Pdepe(m, PdeFun, IcFun, BcFun, x, t);

        //    // Plot Results
        //    Plot(x, U, Linewidth: 2);
        //    Title("Viscous Burger's Equation (Shock Formation)");
        //    Xlabel("Position x"); Ylabel("Concentration C");
        //    Legend(T.Select(t => $"t = {t:0.00}"));
        //    SaveAs("BurgersEquation.png");
        //}


        //{
        //    // SOLVE_HEAT_EQUATION Solves the 1D heat equation in cylindrical cooordinates using pdepe
        //    // Equation:  du/dt = alpha * (1/r)d/du(r*du/dr)
        //    // Domain:    r in [0, 1],  t in [0, 2]
        //    // IC:        u(x,0) = 100
        //    // BC:        u(0,t) = 0,  du/dr(1,t) = h*(U(1,0) - Tinf)
        //    //
        //    // 1. Model Parameters & Mesh Setup
        //    int m = 1;             // Cylindrical coordinates
        //    double alpha = 0.1;    // Thermal diffusivity
        //    double h = 2.0;        // Convective heat transfer coefficient
        //    double Tinf = 20.0;    // Ambient temperature

        //    double[] r = Linspace(0, 1, 51); // Radius r in [0, 1]
        //    double[] t = Linspace(0, 2, 6);  // Time t in [0, 2]

        //    // 2. Set up the PDE
        //    (double c, double f, double s) PdeFun(double r, double t, double u, double dudr)
        //        => (1.0, alpha * dudr, 0.0);

        //    // 3. Uniform initial temperature profile at 100°C
        //    double IcFun(double r) => 100.0;

        //    // 4. Boundary Conditions
        //    (double pl, double ql, double pr, double qr) BcFun(double rl, double ul, double rr, double ur, double t) =>
        //        (0, 1, h * (ur - Tinf), 1);

        //    // 5. Solve the PDE
        //    (ColVec T, Matrix U) = Pdepe(m, PdeFun, IcFun, BcFun, r, t);

        //    Plot(r, U, Linewidth: 2);
        //    Title("Radial Cooling of Cylinder (m = 1)");
        //    Xlabel("Radius r"); Ylabel("Temperature T");
        //    Legend(T.Select(t => $"t = {t:0.00}"));
        //    SaveAs("CylindricalCooling.png");
        //}


        //{
        //    // SOLVE_HEAT_EQUATION Solves the 1D heat equation using pdepe
        //    // Equation:  du/dt = alpha * d^2u/dx^2
        //    // Domain:    x in [0, 1],  t in [0, 0.5]
        //    // IC:        u(x,0) = sin(pi*x)
        //    // BC:        u(0,t) = 0,  u(1,t) = 0
        //    //
        //    // 1. Model Parameters & Mesh Setup
        //    double alpha = 0.5;         // Thermal diffusivity coefficient
        //    int m = 0;                  // 0 = Slab / Cartesian coordinates
        //    double L = 1;               // Length of the rod
        //    double t_final = 0.5;       // Final simulation time

        //    double[] x = Linspace(0, L, 51);        // 51 spatial grid points
        //    double[] t = Linspace(0, t_final, 6);   // 6 time steps

        //    // 2. Defines the PDE components: c* du/ dt = x ^ (-m) * d / dx(x ^ m * f) + s
        //    (double c, double f, double s) pdefun(double x, double t, double u, double dudx) =>
        //        (1, alpha * dudx, 0);

        //    // 3. Initial sinusoidal temperature distribution
        //    double icfun(double x) => Sin(pi * x);

        //    // 4. Boundary Conditions defined as: p(x, t, u) + q(x, t) * f = 0
        //    (double pl, double ql, double pr, double qr) bcfun(double xl, double ul, double xr, double ur, double t) => (ul, 0, ur, 0);

        //    // 5. Solve the PDE
        //    (var T, var U) = Pdepe(m, pdefun, icfun, bcfun, x, t);

        //    // Subplot 2: 2D Temperature Profiles at Selected Times
        //    Plot(x, U, Linewidth: 2);
        //    Xlabel("Position x"); Ylabel("Temperature T");
        //    Title("Temperature vs. Position over Time");
        //    Legend(T.Select(t => $"t = {t:0.00}"));
        //    SaveAs("Temperature.png");
        //}


        //{
        //    // SOLVE-DIFFUSION-REACTION-EQUATION Solves the 1D heat equation using pdepe
        //    // Equation:  du/dt = D * d^2u/dx^2 + g*u*(1 - u);
        //    // Domain:    x in [0, 5],  t in [0, 6]
        //    // IC:        u(x,0) = 0.8 if x < 0.5;
        //    //                     0.0 otherwise
        //    // BC:        du/dx(0,t) = 0,  du/dx(1,t) = 0
        //    //
        //    // 1. Model Parameters & Mesh Setup
        //    int m = 0; // Slab geometry
        //    double D = 0.01; // Diffusion coefficient
        //    double growthRate = 1.0; // Growth rate

        //    double[] x = Linspace(0, 5, 101);  // [0, 5]
        //    double[] t = Linspace(0, 6, 7);    // [0, 6]

        //    // 2. Defines the PDE components: du/ dt = D * d^2u/dx^2 + g*u*(1 - u);
        //    (double c, double f, double s) PdeFun(double x, double t, double u, double dudx)
        //    {
        //        double capacity = 1.0;
        //        double flux = D * dudx;
        //        double source = growthRate * u * (1.0 - u); // Logistic reaction term
        //        return (capacity, flux, source);
        //    }

        //    // 3. Initial localized pulse at x = 0
        //    double IcFun(double x) => x < 0.5 ? 0.8 : 0.0;

        //    // 4. Insulated endpoints (zero flux)
        //    (double pl, double ql, double pr, double qr) BcFun(double xl, double ul, double xr, double ur, double t)
        //        => (0.0, 1.0, 0.0, 1.0);

        //    // 5. Solve the PDE
        //    (ColVec T, Matrix U) = Pdepe(m, PdeFun, IcFun, BcFun, x, t);
        //    Plot(x, U, Linewidth: 2);
        //    Title("Fisher-KPP Traveling Wave Front");
        //    Xlabel("Position x"); Ylabel("Concentration C");
        //    Legend(T.Select(t => $"t = {t:0.00}"));
        //    SaveAs("FisherKPP.png");
        //}


        //{
        //    // SOLVE-DIFFUSION-REACTION-EQUATION
        //    // Solves the 1D reaction-diffusion equation in cylindrical coordinates using pdepe
        //    // Equation:  du/dt = D/r*d/dr(r*du/dr) + g*u*(1 - u);
        //    // Domain:    x in [0, 5],  t in [0, 6]
        //    // IC:        u(x,0) = 0.8 if x < 0.5;
        //    //                     0.0 otherwise
        //    // BC:        du/dx(0,t) = 0,  du/dx(1,t) = 0
        //    //
        //    // 1. Model Parameters & Mesh Setup
        //    int m = 1; // Slab geometry
        //    double D = 0.01; // Diffusion coefficient
        //    double growthRate = 1.0; // Growth rate

        //    double[] r = Linspace(0, 5, 101);  // [0, 5]
        //    double[] t = Linspace(0, 6, 7);    // [0, 6]

        //    // 2. Defines the PDE components: du/dt = D/r*d/dr(r*du/dr) + g*u*(1 - u);
        //    (double c, double f, double s) PdeFun(double r, double t, double u, double dudr)
        //    {
        //        double capacity = 1.0;
        //        double flux = D * dudr;
        //        double source = growthRate * u * (1.0 - u); // Logistic reaction term
        //        return (capacity, flux, source);
        //    }

        //    // 3. Initial localized pulse at x = 0
        //    double IcFun(double r) => r < 0.4 ? 1.0 : 0.0;

        //    // 4. Insulated endpoints (zero flux)
        //    (double pl, double ql, double pr, double qr) BcFun(double rl, double ul, double rr, double ur, double t)
        //        => (0.0, 1.0, 0.0, 1.0);

        //    // 5. Solve the PDE
        //    (ColVec T, Matrix U) = Pdepe(m, PdeFun, IcFun, BcFun, r, t);
        //    Plot(r, U, Linewidth: 2); GridOn();
        //    Title("Cylindrical Fisher-KPP Radial Wave Front (m = 1)");
        //    Xlabel("Position r"); Ylabel("Population Density u(r,t)");
        //    Legend(T.Select(t => $"t = {t:0.00}"));
        //    SaveAs("Cylindrical_FisherKPP.png");
        //}


        //{
        //    double D = 0.01;         // Diffusion coefficient
        //    double growthRate = 1.0; // Growth rate
        //    double C_ambient = 0.0;  // C ambient

        //    (ColVec T, Matrix U) = Pdepe(
        //        m: 1,                                                                     // 1 for Cylindrical, 2 for Spherical
        //        pdefun: (r, t, u, dudr) => (1.0, D * dudr, growthRate * u * (1.0 - u)),   // Pdefunction
        //        icfun: r => r < 0.4 ? 1.0 : 0.0,                                          // Initial condition
        //        bcfun: (rl, ul, rr, ur, t) => (0, 1, ur - C_ambient, 0),                  // Boundary Condition :Symmetry at origin, Dirichlet at boundary
        //        x: Linspace(0, 5, 101), t: Linspace(0, 6, 7));
        //}
    }

    //{
    //    // System Parameters
    //    double rho = 1000.0;    // Density
    //    double Cp = 4.184;      // Heat capacity
    //    double k = 0.6;         // Thermal conductivity
    //    double D = 1.0e-5;      // Diffusivity
    //    double dH = -50000.0;   // Heat of reaction (exothermic)
    //    double k0 = 100.0;      // Reaction rate constant
    //    double Ea = 20000.0;    // Activation energy
    //    double Rg = 8.314;      // Gas constant

    //    // 1. PDE Definition
    //    (ColVec c, ColVec f, ColVec s) PdeFun(double r, double t, ColVec u, ColVec dudr)
    //    {
    //        double T = u[0];
    //        double C = u[1];

    //        double dTdr = dudr[0];
    //        double dCdr = dudr[1];

    //        // Reaction rate term
    //        double rate = k0 * C * Exp(-Ea / (Rg * T));

    //        double[] cVec = [rho * Cp, 1.0];
    //        double[] fVec = [k * dTdr, D * dCdr];
    //        double[] sVec = [-dH * rate, -rate];

    //        return (cVec, fVec, sVec);
    //    }

    //    // 2. Initial Conditions (T0 = 300K, C0 = 1.0 mol/L)
    //    ColVec IcFun(double r)
    //    {
    //        double[] ic = [300.0, 1.0];
    //        return ic;
    //    }

    //    // 3. Boundary Conditions
    //    // At r = 0 (Symmetry): Zero flux for both T and C -> f(0,t) = 0
    //    // At r = R (Cylindrical wall): Constant wall temp Tw, zero mass flux
    //    (ColVec pl, ColVec ql, ColVec pr, ColVec qr) BcFun(double rLeft, ColVec uLeft, double rRight, ColVec uRight, double t)
    //    {
    //        double Tw = 350.0; // Cooling/heating wall temperature

    //        // Left Boundary (r = 0) -> 0 + 1*f = 0
    //        double[] pl = [0.0, 0.0];
    //        double[] ql = [1.0, 1.0];

    //        // Right Boundary (r = R) -> Dirichlet for T, Neumann (Zero flux) for C
    //        double[] pr = [uRight[0] - Tw, 0.0];
    //        double[] qr = [0.0, 1.0];

    //        return (pl, ql, pr, qr);
    //    }

    //    // 4. Execution Call
    //    int m = 1; // Cylindrical coordinates
    //    double[] rMesh = Linspace(0.0, 0.05, 51); // Cylinder radius = 5 cm
    //    double[] tMesh = Linspace(0.0, 100.0, 6);

    //    // Solves and returns Ys[0] for T(r,t) and Ys[1] for C(r,t)
    //    (ColVec t, Matrix[] Ys) = Pdepe(m, PdeFun, IcFun, BcFun, rMesh, tMesh);

    //    Matrix T_sol = Ys[0]; // [TimeSteps x SpatialNodes] for Temperature
    //    Matrix C_sol = Ys[1]; // [TimeSteps x SpatialNodes] for Concentration

    //    Subplot(1, 2, 0);
    //    Plot(rMesh, T_sol, Linewidth: 2); GridOn();
    //    Title("Temperature Profile T(r,t)");
    //    Xlabel("Position r"); Ylabel("Temperature (K)");
    //    Legend(t.Select(t => $"t = {t:0.00}"), UpperLeft);

    //    Subplot(1, 2, 1);
    //    Plot(rMesh, C_sol, Linewidth: 2); GridOn();
    //    Title("Concentration Profile C(r,t)");
    //    Xlabel("Position r"); Ylabel("Concentration (mol/L)");
    //    Legend(t.Select(t => $"t = {t:0.00}"), UpperLeft);

    //    SaveAs("System of PDE.png");
    //}

    //{
    //    // 1. PDE Definition
    //    (ColVec c, ColVec f, ColVec s) PdeFun(double r, double t, ColVec u, ColVec dudx)
    //    {
    //        double speed2 = 1;
    //        double V = u[1];
    //        double dUdx = dudx[0];

    //        double[] cVec = [1, 1];
    //        double[] fVec = [0, speed2 * dUdx];
    //        double[] sVec = [V, 0];

    //        return (cVec, fVec, sVec);
    //    }

    //    // 2. Initial Conditions (T0 = 300K, C0 = 1.0 mol/L)
    //    ColVec IcFun(double r)
    //    {
    //        double[] ic = [0, 0];
    //        return ic;
    //    }

    //    // 3. Boundary Conditions
    //    // At r = 0 (Symmetry): Zero flux for both T and C -> f(0,t) = 0
    //    // At r = R (Cylindrical wall): Constant wall temp Tw, zero mass flux
    //    (ColVec pl, ColVec ql, ColVec pr, ColVec qr) BcFun(double rLeft, ColVec uLeft, double rRight, ColVec uRight, double t)
    //    {
    //        // Left Boundary (x = 0) -> u-0.5*sin(20t) = 0
    //        double[] pl = [uLeft[0] - 0.5 * Sin(20 * t), uLeft[1] - 10 * Cos(20 * t)];
    //        double[] ql = [0.0, 0.0];

    //        // Right Boundary (x = L) -> Fixed: Dirichlet for U and V, 
    //        double[] pr = [uRight[0], uRight[1]];
    //        double[] qr = [0.0, 0.0];

    //        return (pl, ql, pr, qr);
    //    }

    //    // 4. Execution Call
    //    int m = 0; // Cartesian coordinates
    //    double[] x = Linspace(0, 1, 101);
    //    double[] t = Linspace(0, 1, 51);

    //    tic();
    //    (ColVec T, Matrix[] Ys) = Pdepe(m, PdeFun, IcFun, BcFun, x, t);
    //    Console.WriteLine(toc());

    //    Matrix U = Ys[0]; // [TimeSteps x SpatialNodes] for Position
    //    Matrix V = Ys[1]; // [TimeSteps x SpatialNodes] for Velocity

    //    var wave = Plot(x, U[0, ..], Linewidth: 2);
    //    Axis([0, 1, -1, 1]);

    //    // Animation
    //    byte[] Animfun(int i)
    //    {
    //        wave.Ydata = U[i, ..].T;
    //        return GetFrame();
    //    }
    //    AnimationMaker(Animfun, "Wave_Equation_Using_PDEPE.gif", 10, T.Numel);
    //    CloseFig();
    //}

    //{
    //    //Large Nonlinear Systems
    //    int n = 3000;

    //    ColVec b = Ones(n), xstart;

    //    ColVec nlsf(ColVec x)
    //    {
    //        ColVec F = new double[n];
    //        F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
    //        F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
    //        F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
    //        return F;
    //    }

    //    xstart = -b;
    //    var opts = SolverSet(Display: true);
    //    tic();
    //    var x = Fsolve(nlsf, xstart, opts);
    //    Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
    //    Console.WriteLine(opts.ans.FunVal.Norm());
    //    Console.WriteLine($"Elapsed time: {toc()} seconds");
    //}

    //{
    //    //Large Nonlinear Systems
    //    int n = 3000;
    //    Range i = 0..n, j = 0..(n - 1), jp1 = 1..n;

    //    ColVec e = Ones(n), xstart = -e;

    //    ColVec nlsf(ColVec x)
    //    {
    //        ColVec F = new double[n];
    //        F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
    //        F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
    //        F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
    //        return F;
    //    }
    //    SparseMatrix Jpattern = Spdiags([e,e,e], [-1, 0, 1], n, n);
    //    var opts = SolverSet(Display: true, Jpattern: Jpattern);
    //    tic();
    //    var x = Fsolve(nlsf, xstart, opts);
    //    Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
    //    Console.WriteLine(opts.ans.FunVal.Norm());
    //    Console.WriteLine($"Elapsed time: {toc()} seconds");
    //}

    //{
    //    //Large Nonlinear Systems
    //    int n = 3000;
    //    Range i = 0..n, j = 0..(n - 1), jp1 = 1..n;

    //    ColVec a = Ones(n - 1), b = Ones(n), e = -a,
    //        c = 2 * e, d, xstart;

    //    ColVec nlsf(ColVec x)
    //    {
    //        ColVec F = new double[n];
    //        F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
    //        F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
    //        F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
    //        return F;
    //    }

    //    SparseMatrix C, D, E;
    //    Func<ColVec, SparseMatrix> Jac = x =>
    //    {
    //        d = -4 * x + 3 * b;
    //        D = new(i, i, d, n, n);
    //        C = new(j, jp1, c, n, n);
    //        E = new(jp1, j, e, n, n);
    //        return C + D + E;
    //    };

    //    xstart = -b;
    //    var opts = SolverSet(Display: true, UserDefinedJac: Jac);
    //    tic();
    //    var x = Fsolve(nlsf, xstart, opts);
    //    Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
    //    Console.WriteLine(opts.ans.FunVal.Norm());
    //    Console.WriteLine($"Elapsed time: {toc()} seconds");
    //}

    //{
    //    //define ODE
    //    double[] robertsonimplicit(double t, double[] y, double[] yp) =>
    //        [yp[0] + 0.04 * y[0] - 1e4 * y[1]*y[2],
    //                 yp[1] - 0.04 * y[0] + 1e4 * y[1]*y[2] + 3e7*y[1]*y[1],
    //                 y[0] + y[1] + y[2] - 1];

    //    var opts = Odeset(Stats: true
    //    , UserDefinedJacobian: new(
    //    (t, y, yp) => new double[,]
    //    {
    //                { 0.04, -1e4 * y[2], -1e4 * y[1] },
    //                { -0.04, 1e4 * y[2]+ 6e7 * y[1], 1e4 * y[1] },
    //                { 1, 1, 1 }
    //    },
    //    (t, y, yp) => new double[,]
    //    {
    //                {1, 0, 0},
    //                {0, 1, 0},
    //                {0, 0, 0}
    //    })
    //    );

    //    // Set intial conditions for y0 and guess values for yp0
    //    double[] y0 = [1, 0, 0], yp0 = [0, 0, 0];

    //    // Solve for yp0, Truth array for y0 = [1,1,1] but for yp0 it is [0,0,0]. 
    //    (y0, yp0) = decic(robertsonimplicit, 0, y0, [1, 1, 0], yp0, [0, 0, 0]);
    //    //Solve ODE
    //    (ColVec T, Matrix Y, Matrix Yp) = Ode45i(robertsonimplicit, (y0, yp0), [0, 4e6], opts);
    //    // Plot the result
    //    Y[.., 1] = 1e4 * Y[.., 1];
    //    SemiLogx(T, Y);
    //    Xlabel("Time t"); Ylabel("Soluton y");
    //    Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
    //    Title("Solution of implicit Robertson's ODE with ODE45i");
    //    SaveAs("Implicit-Robertson-ODE-Ode45i_With_jacobian.png");
    //}

    //{
    //    double[] robertson_f(double t, double[] y) =>
    //        [(-0.04 * y[0] + 1e4 * y[1] * y[2]),
    //                 (0.04 * y[0] - 1e4 * y[1] * y[2] - 3e7 * y[1]*y[1]),
    //                 y[0] + y[1] + y[2] - 1.0];

    //    double[,] mass_f(double t, double[] y) => Diag([1, 1, 0]);

    //    double[] y0 = [1.0, 0.0, 0.0];
    //    (ColVec T, Matrix Y) = Ode45a(robertson_f, mass_f, y0, [0, 1e7]);
    //    // Plot the result
    //    Y[.., 1] = 1e4 * Y[.., 1];
    //    SemiLogx(T, Y);
    //    Xlabel("Time t"); Ylabel("Soluton y");
    //    Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
    //    Title("Solution of Robertson's ODE with ODE45a");
    //    SaveAs("Robertson-ODE-given-points-Ode45a.png");
    //}
    //{
    //    double g = 9.81;

    //    // State vector y = [x, y, u, v, λ]
    //    double[] pendulum_f(double t, double[] y) =>
    //        [y[2],
    //                 y[3],
    //                 -y[0] * y[4],
    //                 -y[1] * y[4] - g,
    //                 y[2]*y[2] + y[3]*y[3] - y[1] * g - y[4]];

    //    double[,] mass_f(double t, double[] y) => Diag([1, 1, 1, 1, 0]);

    //    double[] y0 = [0, 1, 1, 0, 1 - g];
    //    var opts = Odeset(Stats: true, RelTol: 1e-6);
    //    (ColVec T, Matrix Y) = Ode45a(pendulum_f, mass_f, y0, [0, 6], opts);
    //    Plot(T, Y, Linewidth: 2); Xlabel("x"); Ylabel("y");
    //    Legend(["x", "y", "u", "v", "λ"]);
    //    Title("Pendulum Trajectory (DAE)");
    //    SaveAs("Index_1-Pendulum-Problem-Ode45a.png");
    //}

    //{
    //    double Ub = 6, R0 = 1000, R15 = 9000, alpha = 0.99,
    //        beta = 1e-6, Uf = 0.026, c1 = 1e-6, c2 = 2e-6, c3 = 3e-6;
    //    double[,] Mass(double t, double[] y) => new double[,]
    //    {
    //                {-c1,  c1,  0,   0,   0 },
    //                { c1, -c1,  0,   0,   0 },
    //                { 0,   0,  -c2,  0,   0 },
    //                { 0,   0,   0,  -c3,  c3},
    //                { 0,   0,   0,   c3, -c3}
    //    };
    //    double Ue(double t) => 0.4 * Sin(200 * pi * t);
    //    double[] dudt(double t, double[] u)
    //    {
    //        double f23 = beta * (Exp((u[1] - u[2]) / Uf) - 1);
    //        return [ -(Ue(t) - u[0])/R0,
    //                         -(Ub/R15 - u[1]*2/R15 - (1-alpha)*f23),
    //                         -(f23 - u[2]/R15),
    //                         -((Ub - u[3])/R15 - alpha*f23),
    //                         u[4]/R15 ];
    //    }
    //    double[] tspan = [0, 0.1];
    //    double[] y0 = [0, Ub / 2, Ub / 2, Ub, 0];

    //    var opts = Odeset(RelTol: 1e-5);
    //    (ColVec T, Matrix Y) = Ode45a(dudt, Mass, y0, tspan, opts);
    //    Scatter(T, Arrayfun(Ue, T), "o"); HoldOn();
    //    Plot(T, Y[.., 4], "--r"); HoldOff();
    //    Legend(["Input", "Output"], UpperLeft);
    //    Xlabel("Time t"); Ylabel("Solution y");
    //    Title("One Transistor Amplifier DAE Problem-Ode45a");
    //    SaveAs("One-Transistor-Amplifier-DAE-Problem-Ode45a.png");
    //}

    //{
    //    double k1 = 18.7, k2 = 0.58, k3 = 0.09, k4 = 0.42, K = 34.4, Fin = 0.012;
    //    double r1(double[] y) => k1 * Pow(y[0], 4) * Pow(y[1], 0.5);
    //    double r2(double[] y) => k2 * y[2] * y[3];
    //    double r3(double[] y) => (k2 / K) * y[0] * y[4];
    //    double r4(double[] y) => k3 * y[0] * Pow(y[3], 2);
    //    double r5(double[] y) => k4 * Pow(y[5], 2) * Pow(y[1], 0.5);

    //    double[] akzo_f(double t, double[] y) =>
    //        [
    //            -2*r1(y) + r2(y) - r3(y) - r4(y),
    //                    -0.5*r1(y) - r5(y) + 0.5*Fin,
    //                    r1(y) - r2(y) + r3(y),
    //                    -r2(y) + r3(y) - 2*r4(y),
    //                    r2(y) - r3(y) + r4(y),
    //                    -r5(y),
    //                    y[0] * y[2] - y[6],
    //                    y[3] * y[4] - y[7]
    //        ];

    //    double[,] mass_f(double t, double[] y) => Diag([1, 1, 1, 1, 1, 1, 0, 0]);
    //    double[] y0 = [0.444, 0.0012, 0.0, 0.0037, 0.0, 0.0, 0.0, 0.0];
    //    (ColVec T, Matrix Y) = Ode45a(akzo_f, mass_f, y0, [0, 180]);
    //    Plot(T, Y);
    //    Xlabel("Time"); Ylabel("Concentration");
    //    Title("Akzo Nobel Chemical Kinetics (DAE)");
    //    SaveAs("Akzo-Nobel-Ode45a.png");
    //}

    //{
    //    // define the DAE
    //    double alpha = 10;
    //    double[] Ercan(double t, double[] x) =>
    //        [ (alpha - 1/(2-t))*x[0] + (2-t)*alpha*x[2] + (3-t)/(2-t)*x[1],
    //                  (1-alpha)/(t-2)*x[0] - x[1] + (alpha-1)*x[2] + 2*Exp(t),
    //                  (t+2)*x[0] + (t*t-4)*x[1] - (t*t+t-2)*Exp(t) ];

    //    double[,] mass_f(double t, double[] x) => Diag([1, 1, 0]);
    //    double[] y0 = [1, 1, 0]; // only the differential variables need initial conditions
    //    var opts = Odeset(Stats: true);
    //    (ColVec T, Matrix Y) = Ode45a(Ercan, mass_f, y0, [0, 1], opts);
    //    Scatter(T, Hcart(Exp(T), Exp(T), -Exp(T).Div(2 - T)), "o"); HoldOn();
    //    Plot(T, Y); HoldOff();
    //    Xlabel("Time t"); Ylabel("Solution x");
    //    Legend(["x_1_Exact", "x_2_Exact", "z_Exact", "x_1_NumSol", "x_2_NumSol", "z_NumSol"]);
    //    Title("Index-2 DAE Example (Ercan Celık)");
    //    SaveAs("Index-2-DAE-Ercan-Celik.png");

    //    // We can actually print out the result to compare with the analytical solution
    //    Console.WriteLine("""
    //                    t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
    //                --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
    //                """);
    //    for (int i = 0; i < T.Numel; i++)
    //    {
    //        Console.WriteLine($"""
    //                      {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i]) / (2 - T[i]):F6}
    //                    """);
    //    }

    //    // We can compute the solution to a higher accuracy 
    //    Console.WriteLine("\n\nNow we compute the solution to a higher accuracy (RelTol = 1e-5):\n");
    //    opts = Odeset(Stats: true, RelTol: 1e-5);
    //    (T, Y) = Ode45a(Ercan, mass_f, y0, [0, 1], opts);
    //    Console.WriteLine("""
    //                    t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
    //                --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
    //                """);
    //    for (int i = 0; i < T.Numel; i++)
    //    {
    //        Console.WriteLine($"""
    //                      {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i]) / (2 - T[i]):F6}
    //                    """);
    //    }
    //}

    //{
    //    double g = 9.81;

    //    // State vector y = [x, y, u, v, λ]
    //    double[] pendulum_f(double t, double[] y) =>
    //        [ y[2],
    //          y[3],
    //         -y[0] * y[4],
    //         -y[1] * y[4] - g,
    //          y[0]*y[2] + y[1]*y[3] ];

    //    double[,] mass_f = Diag([1, 1, 1, 1, 0]);

    //    double[] y0 = [0, 1, 1, 0, -1];
    //    var opts = Odeset(Stats: true);
    //    (ColVec T, Matrix Y) = Ode45a(pendulum_f, mass_f, y0, [0, 6], opts);
    //    Plot(T, Y, Linewidth: 2); Xlabel("x"); Ylabel("y");
    //    Legend(["x", "y", "u", "v", "λ"]);
    //    Title("Pendulum Trajectory (DAE)");
    //    SaveAs("Index_2-Pendulum-Problem-Ode45a.png");

    //    Console.WriteLine("\n\n");
    //    Console.WriteLine(Hcart(T, Y));
    //}
    Writer.Run();
    {
        // Reservoir Simulation
        folderpath = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\Morgana Data\";
        Matrix Base = ReadMatrix("Base.txt");
        var id = Base[.., 2] == 3075;

        ColVec x = Base[id, 0], y = Base[id, 1];
        Plot(x, y, "k");
        var rx = x.Range(); var ry = y.Range();
    }
    {
        // Morgana Field Development Plan
        folderpath = @"C:\Users\lateef.a.kareem\Documents\GitHub\SepalSolverBook\Morgana Data\";
        Matrix Top = ReadMatrix("Top.txt"), Base = ReadMatrix("Base.txt");
        double Area(Matrix Data, double level)
        {
            var id = Data[.., 2] == level;
            ColVec x = Data[id, 0], y = Data[id, 1];
            x = Vcart(x, x[0]); y = Vcart(y, y[0]);
            return 0.5*Abs((x[..^1].Times(y[1..]) - y[..^1].Times(x[1..])).Sum());
        }

        // processing top
        double[] toplevels = [..Top[.., 2].Distinct()], 
            baselevels = [..Base[.., 2].Distinct()], 
            alllevels = [.. toplevels.Union(baselevels)];

        double[] allareas = [.. alllevels.Select(level => (toplevels.Contains(level)?Area(Top, level):0) - (baselevels.Contains(level)?Area(Base, level):0))];

        for (int i = 0; i < alllevels.Length; i++) Console.WriteLine($"Level = {alllevels[i]}, Area = {allareas[i]}");

        // RockVolume
        double GrossRockVolume = 0.5*(allareas[0] + 2*allareas[1..^1].Sum() + allareas[^1])*(alllevels[1]-alllevels[0]);

        // PretroPhysics Data

        (double Mean, double StdDev) ComputeStatistics(ColVec data, bool isSample = true)
        {
            // 1. Compute Mean
            double mean = data.Average();

            // 2. Compute Sum of Squares of Differences
            double sumOfSquares = (data - mean).SumSq();

            // 3. Compute Standard Deviation
            double divisor = isSample ? data.Numel - 1 : data.Numel;

            if (divisor <= 0) return (mean, 0.0); // Guard against single-element sample sets
            return (mean, Sqrt(sumOfSquares / divisor));
        }

        Matrix CapPress = ReadMatrix("CapPressure.txt"), NTGPoro = ReadMatrix("PetroPhysics.txt");
        var (NTG_P50, NTGstd) = ComputeStatistics(NTGPoro[.., 0]);
        var (Poro_P50, Porostd) = ComputeStatistics(NTGPoro[.., 1]);

        // From PVT (P = 300bars, T = 363K)
        Matrix PVT = ReadMatrix("PVT.txt");
        ColVec P_pvt = PVT[.., 0], Z_pvt = PVT[.., 1], mu_pvt = PVT[.., 2],
            cgr_pvt = PVT[.., 3], Shrinkage = PVT[.., 4], P_Z_pvt = P_pvt.Div(Z_pvt);
        double Pi = 300; // Initial reservoir pressure (psi)
        var z = 0.95; var Bgi = 0.00336*0.95*363/Pi;
        var rhog = 0.65*1.293*288*300/(363*0.95);

        ColVec cPress = CapPress[.., 0], sPress = CapPress[.., 1];
        double[] Sw = [..alllevels.Select(level => Interp1(cPress, sPress, Min(cPress.Max(), 9.8*(alllevels.Last() - level)*(1000 - rhog)*1e-5)))];
        for (int i = 0; i < alllevels.Length; i++) Console.WriteLine($"Level = {alllevels[i]}, Saturation = {Sw[i]}");
        double Swavg = Sw.Zip(allareas, (sw, area) => sw * area).Sum() / allareas.Sum();

        var NTG_P90 = NTG_P50 - 1.282*NTGstd;
        var NTG_P10 = NTG_P50 + 1.282*NTGstd;
        var Poro_P90 = Poro_P50 - 1.282*Porostd;
        var Poro_P10 = Poro_P50 + 1.282*Porostd;

        var GIIP_P50 = GrossRockVolume*NTG_P50*Poro_P50*(1-Swavg)/Bgi;
        var GIIP_P10 = GrossRockVolume*NTG_P10*Poro_P10*(1-Swavg)/Bgi;
        var GIIP_P90 = GrossRockVolume*NTG_P90*Poro_P90*(1-Swavg)/Bgi;


        // Production Data fro Merlin  
        Matrix ProductionData = ReadMatrix("Merlin Production History.txt");
        ColVec Time = ProductionData[.., 0], CumGasProd = ProductionData[.., 1], Pressure = ProductionData[.., 2];
        ColVec P_Z = Pressure.Select(p => p/Interp1(P_pvt, Z_pvt, p)).ToArray();
        // Use Regression to estimate the slope of P/Z vs CumGasProd, which is related to the initial gas in place.
        var parMerlin = Polyfit([.. CumGasProd], [.. P_Z], 1);
        var GIIP_Merlin = -parMerlin[1]/parMerlin[0];
        var RF_Merlin = CumGasProd.Last()/GIIP_Merlin;
        Figure(1000, 500);
        Scatter(CumGasProd, P_Z, "fob"); HoldOn();
        Plot([0, GIIP_Merlin], [parMerlin[1], 0], "k", 3);
        Scatter(GIIP_Merlin, 0, "fog", 20);
        Title("MerlinProdData"); Xlabel("CumGasProd"); Ylabel("P/Z");
        HoldOff(); SaveAs("MerlinProdData.png"); CloseFig();

        // Well Test Data
        Matrix TestData = ReadMatrix("Well Test.txt");
        ColVec Qipr = TestData[.., 0], Pipr = TestData[.., 1], Qipr_estimate;
        ColVec LnQ = Log(Qipr), DelP2 = Pi*Pi - Pipr.Pow(2), LnDelP2 = Log(DelP2);
        // Use Regression to estimate C and n in PressureSquared IPR
        var par = Polyfit([.. LnDelP2], [.. LnQ], 1);
        var n = par[0]; var C = Exp(par[1]);
        HoldOn();
        Scatter(Qipr, Pipr, "fob");
        Plot(Qipr_estimate = C*DelP2.Pow(n), Pipr, "r", 2);
        Xlabel("Q(m^3/day)"); Ylabel("P(bar)");
        Title($"C = {C}, and n = {n}");
        SaveAs("WellTest.png");

        // VFP data
        Matrix VFP = ReadMatrix("VFP.txt");
        ColVec Qvfp = VFP[.., 0], Pvfp20 = VFP[.., 1], Pvfp40 = VFP[.., 2], Pvfp60 = VFP[.., 3], Pvfp80 = VFP[.., 4], Pvfp100 = VFP[.., 5];
        Plot(Qvfp, VFP[.., 1..]);
        Legend(["", "IPR", "VFP_20", "VFP_40", "VFP_60", "VFP_80", "VFP_100"], LowerRight);
        

        var (nodq1, nodp1) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp100); Scatter(nodq1, nodp1, "fog");
        var (nodq2, nodp2) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp80); Scatter(nodq2, nodp2, "fog");
        var (nodq3, nodp3) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp60); Scatter(nodq3, nodp3, "fog");
        var (nodq4, nodp4) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp40); Scatter(nodq4, nodp4, "fog");
        var (nodq5, nodp5) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp20); Scatter(nodq5, nodp5, "fog");
        SaveAs("Nodal.png"); CloseFig();


        Plot(Qipr_estimate = 2*C*DelP2.Pow(n), Pipr, "r", 2);
        Xlabel("Q(m^3/day)"); Ylabel("P(bar)");
        Title($"C = {C}, and n = {n}");
        HoldOn();

        // VFP data
        Plot(Qvfp, VFP[.., 1..]);
        Legend(["", "IPR", "VFP_20", "VFP_40", "VFP_60", "VFP_80", "VFP_100"], LowerRight);


        var (nodq1e, nodp1e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp100); Scatter(nodq1e, nodp1e, "fog");
        var (nodq2e, nodp2e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp80); Scatter(nodq2e, nodp2e, "fog");
        var (nodq3e, nodp3e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp60); Scatter(nodq3e, nodp3e, "fog");
        var (nodq4e, nodp4e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp40); Scatter(nodq4e, nodp4e, "fog");
        var (nodq5e, nodp5e) = Intersection(Qipr_estimate, Pipr, Qvfp, Pvfp20); Scatter(nodq5e, nodp5e, "fog");
        SaveAs("Nodal2Wells.png"); CloseFig();

        // Comparing with Merlin
        double Pabn = 75, Zabn = Interp1(P_pvt, Z_pvt, Pabn), 
            Zi = Interp1(P_pvt, Z_pvt, Pi), pz_i = Pi / Zi;
        double RF = 1 - Pabn / Zabn / pz_i;

        double pwf_abandonment = Interp1(Qvfp, Pvfp20, 0.2e6);
        double pr_abandonment = Sqrt(Pow(0.2e6/C, 1/n) + pwf_abandonment*pwf_abandonment);

        // assuming tha tlike Merlin, Morgana will also undergoing volumentric depletion
        double RF2 = 1 - pr_abandonment / Interp1(P_pvt, Z_pvt, pr_abandonment) / pz_i;
        double EUR_P10 = GIIP_P10*RF, EUR_P50 = GIIP_P50*RF, EUR_P90 = GIIP_P90*RF;
        

        (ColVec Q, ColVec Pu, ColVec Pd, ColVec Thp, ColVec Cum, ColVec CumCond) 
            ProductionProfile(double EUR, double GIIP, double Nyears, double factor, int Nwells)
        {
            double Qplt = Min(1e6, factor * EUR/((Nyears)*365));
            double pr = Pi, cumprod = 0, pu, pd;
            double initcond = Interp1(P_pvt, cgr_pvt, pr)*GIIP/1e6;
            List<ColVec> PVFP = [Pvfp100, Pvfp60, Pvfp20];
            List<double> THP = [100, 60, 20];
            int pindx = 0; ColVec pwf;
            List<double> Q = [], Pu = [], Pd = [], Thp = [], Cum = [], CumCond = [];
            for (int i = 0; i <= Nyears; i++)
            {
                pwf = Linspace(pr, 0, 10);
                DelP2 = pr*pr - pwf.Pow(2);
                Qipr_estimate = Nwells * C*DelP2.Pow(n);
                pu = Interp1(Qipr_estimate, pwf, Qplt);
                pd = Interp1(Qvfp, PVFP[pindx], Qplt);
                if(pu < pd)
                {
                    if (pindx < 2)
                    {
                        pindx++;
                        pd = Interp1(Qvfp, PVFP[pindx], Qplt);
                    }
                    else
                    {
                        (Qplt, pu) = Intersection(Qipr_estimate, pwf, Qvfp, PVFP[pindx]);
                        pd = pu;
                    }
                }
                Q.Add(Qplt); Pu.Add(pu); Pd.Add(pd); Thp.Add(THP[pindx]); Cum.Add(cumprod); 
                CumCond.Add(initcond - (GIIP - cumprod)*Interp1(P_pvt, cgr_pvt, pr)/1e6);
                cumprod += Qplt*365;
                pr = Interp1(P_Z_pvt, P_pvt, pz_i*(GIIP - cumprod)/GIIP);
            }
            while(Qplt > 0.2e6)
            {
                cumprod += Qplt*365;
                pr = Interp1(P_Z_pvt, P_pvt, pz_i*(GIIP - cumprod)/GIIP); 
                pwf = Linspace(pr, 0, 10);
                DelP2 = pr*pr - pwf.Pow(2);
                Qipr_estimate = Nwells * C*DelP2.Pow(n);
                (Qplt, pu) = Intersection(Qipr_estimate, pwf, Qvfp, PVFP[pindx]);
                pd = pu;
                Q.Add(Qplt); Pu.Add(pu); Pd.Add(pd); Thp.Add(THP[pindx]); Cum.Add(cumprod);
                CumCond.Add(initcond - (GIIP - cumprod)*Interp1(P_pvt, cgr_pvt, pr)/1e6);
            }
            return (Q, Pu, Pd, Thp, Cum, CumCond);
        }

        (ColVec Q, ColVec Pu, ColVec Pd, ColVec Thp, ColVec Cum, ColVec CumCond) 
            EnforcePlateauDeclineCondition(double EUR, double GIIP, int Nyears, int Nwells, double maxD)
        {
            double f = 1;
            var (Q, Pu, Pd, Thp, Cum, CumCond) = ProductionProfile(EUR, GIIP, Nyears, f, Nwells);
            ColVec Ratio = Q[1..].Div(Q[..^1]);
            while (Q[Nyears] < Q[0] || Ratio.Any(r => r < (1 - maxD)))
            {
                f -= 0.01;
                (Q, Pu, Pd, Thp, Cum, CumCond) = ProductionProfile(EUR, GIIP, Nyears, f, Nwells);
                Ratio = Q[1..].Div(Q[..^1]);
            }
            return (Q, Pu, Pd, Thp, Cum, CumCond);
        }

        // High P90
        double  maxDeclineRate = 0.3; int Nwells = 2, NplatYears = 10; ColVec index;
        var (Q10, Pu10, Pd10, Thp10, Cum10, Cond10) = EnforcePlateauDeclineCondition(EUR_P10, GIIP_P10, NplatYears, Nwells, maxDeclineRate);
        var (Q50, Pu50, Pd50, Thp50, Cum50, Cond50) = EnforcePlateauDeclineCondition(EUR_P50, GIIP_P50, NplatYears, Nwells, maxDeclineRate);
        var (Q90, Pu90, Pd90, Thp90, Cum90, Cond90) = EnforcePlateauDeclineCondition(EUR_P90, GIIP_P90, NplatYears, Nwells, maxDeclineRate);

        for (int i = 0; i < Q10.Numel; i++)
            Console.WriteLine($"{i}, {Q10[i]/1e6:F4}, {Thp10[i]:F4}, {Cum10[i]/1e6:F4}, {Cond10[i]/1e6:F4}");
        Console.WriteLine("#################################################################################");
        for (int i = 0; i < Q50.Numel; i++)
            Console.WriteLine($"{i}, {Q50[i]/1e6:F4}, {Thp50[i]:F4}, {Cum50[i]/1e6:F4}, {Cond50[i]/1e6:F4}");
        Console.WriteLine("#################################################################################");
        for (int i = 0; i < Q90.Numel; i++)
            Console.WriteLine($"{i}, {Q90[i]/1e6:F4}, {Thp90[i]:F4}, {Cum90[i]/1e6:F4}, {Cond90[i]/1e6:F4}");
        Console.WriteLine("#################################################################################");


        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Q10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Q50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Q90, "r", 3);
        Xlabel("Time(years)"); 
        Ylabel("Production Rate(m^3/Day)");
        Title("Production Profile");
        Legend(["P10", "P50", "P90"]);
        SaveAs("Profile.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Cum10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Cum50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Cum90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Cumulative Production (m^3)");
        Title("Cumulative Production Profile");
        Legend(["P10", "P50", "P90"]);
        SaveAs("CumulativeProductionProfile.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Cond10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Cond50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Cond90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Cumulative Condensate Production (m^3)");
        Title("Cumulative Condensate Production Profile");
        Legend(["P10", "P50", "P90"]);
        SaveAs("CumulativeCondensateProductionProfile.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Pu10-Pd10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Pu50-Pd50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Pu90-Pd90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Choke Pressure Drop (bar)");
        Title("Choke Pressure Drop ");
        Legend(["P10", "P50", "P90"]);
        SaveAs("ChokePressureDrop.png"); CloseFig();

        Figure(1000, 500);
        index = Linspace(0, Q10.Numel, Q10.Numel);
        Plot(index, Thp10, "b", 3); HoldOn();
        index = Linspace(0, Q50.Numel, Q50.Numel);
        Plot(index, Thp50, "g", 3);
        index = Linspace(0, Q90.Numel, Q90.Numel);
        Plot(index, Thp90, "r", 3);
        Xlabel("Time(years)");
        Ylabel("Tubing Head Pressure (bar)");
        Title("Tubing Head Pressure");
        Legend(["P10", "P50", "P90"]);
        SaveAs("TubingHeadPressure.png"); CloseFig();
    }


    //FormatLong();
    {
        Matrix A = new double[,]
        {
        { 0.1419,    0.6557,         0,         0,         0 },
        { 0.4218,    0.0357,    0.7431,         0,         0 },
        { 0,         0.8491,    0.3922,    0.2769,         0 },
        { 0,              0,    0.6555,    0.0462,    0.9502 },
        { 0,              0,         0,    0.0971,    0.0344 }
        };

        //A = A.T;
        Console.WriteLine($"Matrix A = {A}");
        var (U, S, V) = Svd(A, 5);
        //Console.WriteLine($"Matrix U = {U}");
        //Console.WriteLine($"Matrix S = {S}");
        //Console.WriteLine($"Matrix V = {V}");

        var A_recon = U*S*V.T;
        Console.WriteLine($"Reconstructed A = {A_recon}");
        //Console.WriteLine($"Reconstructed A = {A_recon.Full()}");
    }

    {
        SparseMatrix A = new double[,]
        {
        { 0.1419,    0.6557,         0,         0,         0 },
        { 0.4218,    0.0357,    0.7431,         0,         0 },
        { 0,         0.8491,    0.3922,    0.2769,         0 },
        { 0,              0,    0.6555,    0.0462,    0.9502 },
        { 0,              0,         0,    0.0971,    0.0344 }
        };

        //A = A.T;
        Console.WriteLine($"Matrix A = {A.Full()}");
        var (U, S, V) = Svd(A, 5);
        //Console.WriteLine($"Matrix U = {U.Full()}");
        //Console.WriteLine($"Matrix S = {S.Full()}");
        //Console.WriteLine($"Matrix V = {V.Full()}");

        var A_recon = U*S*V.T;
        Console.WriteLine($"Reconstructed A = {A_recon.Full()}");
        //Console.WriteLine($"Reconstructed A = {A_recon.Full()}");
    }

    {
        Matrix A = new double[,]
        {
        { 0.8147,    0.9134,    0.2785,    0.9649,    0.9572 },
        { 0.9058,    0.6324,    0.5469,    0.1576,    0.4854 },
        { 0.1270,    0.0975,    0.9575,    0.9706,    0.8003 }
        };

        Console.WriteLine($"Matrix A = {A}");
        var (U, S, V) = Svd(A);
        Console.WriteLine($"Matrix U = {U}");
        Console.WriteLine($"Matrix S = {S}");
        Console.WriteLine($"Matrix V = {V}");

        Console.WriteLine($"Matrix UTU = {U.T*U}");
        Console.WriteLine($"Matrix UUT = {U*U.T}");
        Console.WriteLine($"Matrix VTV = {V.T*V}");
        Console.WriteLine($"Matrix VVT = {V*V.T}");
    }
}


//static (double x1, double x2) bracket_minimum(Func<double, double> f, double x = 0, double s = 0.01, double k = 2.0)
//{
//    var (a, ya) = (x, f(x));
//    var (b, yb) = (a + s, f(a + s));
//    if (yb > ya)
//        (a, b, ya, yb, s) = (b, a, yb, ya, -s);

//    while (true)
//    {
//        var (c, yc) = (b + s, f(b + s));
//        if (yc > yb)
//            return a < c ? (a, c) : (c, a);
//        (a, ya, b, yb, s) = (b, yb, c, yc, s*k);
//    }
//}




//{
//    // Super heated water table
//    // Absolute Pressure (kPa)
//    double[] P = { 10, 50, 75, 100, 150, 400 };

//    // Temperature (°C)
//    double[] T = { 100, 150, 200, 250, 300, 360, 420, 500 };

//    // Specific Volume v (m³/kg)
//    double[,] v =
//        {
//            { 17.196, 19.512, 21.825, 24.136, 26.445, 29.216, 31.986, 35.679 },
//            { 3.418, 3.889, 4.356, 4.820, 5.284, 5.839, 6.394, 7.134 },
//            { 2.270, 2.587, 2.900, 3.211, 3.520, 3.891, 4.262, 4.755 },
//            { 1.6958, 1.9364, 2.172, 2.406, 2.639, 2.917, 3.195, 3.565 },
//            { double.NaN, 1.2853, 1.4443, 1.6012, 1.7570, 1.9432, 2.129, 2.376 },
//            { double.NaN, 0.4708, 0.5342, 0.5951, 0.6548, 0.7257, 0.7960, 0.8893 }
//        };

//    // Enthalpy h (kJ/kg)
//    double[,] h =
//        {
//            { 2687.5, 2783.0, 2879.5, 2977.3, 3076.5, 3197.6, 3320.9, 3489.1 },
//            { 2682.5, 2780.1, 2877.7, 2976.0, 3075.5, 3196.8, 3320.4, 3488.7 },
//            { 2679.4, 2778.2, 2876.5, 2975.2, 3074.9, 3196.4, 3320.0, 3488.4 },
//            { 2672.2, 2776.4, 2875.3, 2974.3, 3074.3, 3195.9, 3319.6, 3488.1 },
//            { double.NaN, 2772.6, 2872.9, 2972.7, 3073.1, 3195.0, 3318.9, 3487.6 },
//            { double.NaN, 2752.8, 2860.5, 2964.2, 3066.8, 3190.3, 3315.3, 3484.9 }
//        };

//    // Entropy s (kJ/kg·K)
//    double[,] s =
//        {
//            { 8.4479, 8.6882, 8.9038, 9.1002, 9.2813, 9.4821, 9.6682, 9.8978 },
//            { 7.6947, 7.9401, 8.1580, 8.3556, 8.5373, 8.7385, 8.9249, 9.1546 },
//            { 7.5009, 7.7496, 7.9690, 8.1673, 8.3493, 8.5508, 8.7374, 8.9672 },
//            { 7.3614, 7.6134, 7.8343, 8.0333, 8.2158, 8.4175, 8.6042, 8.8342 },
//            { double.NaN, 7.4193, 7.6433, 7.8438, 8.0720, 8.2293, 8.4163, 8.6466 },
//            { double.NaN, 6.9299, 7.1706, 7.3789, 7.5662, 7.7712, 7.9598, 8.1913 }
//        };
//    Console.WriteLine("==============================================================");
//    Console.WriteLine($"Enthalpy at P = {39}, T = {102} is {Interp2(P, T, h, 39, 102)}");
//    Console.WriteLine($"specific volume at P = {39}, T = {102} is {Interp2(P, T, v, 39, 102)}");
//    Console.WriteLine($"entropy at P = {39}, T = {102} is {Interp2(P, T, s, 39, 102)}");
//}

//{
//    // T: Temperature (°C)
//    double[] T = { 85, 90, 95, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190,
//        200, 210, 220, 230, 240, 250, 260, 270, 280, 290, 300, 320, 340, 360 };

//    // P: Pressure (bar)
//    double[] P = { 0.5783, 0.7013, 0.8455, 1.013, 1.433, 1.985, 2.701, 3.613, 4.758,
//        6.178, 7.916, 10.02, 12.54, 15.54, 19.06, 23.18, 27.95, 33.44, 39.73, 46.88,
//        54.98, 64.11, 74.36, 85.81, 112.7, 145.9, 186.5 };

//    // vf: Spec. Vol. Sat. Liquid (m³/kg) - Adjusted for X1000 scale
//    double[] v_f = { 0.001033, 0.001036, 0.001039, 0.001044, 0.001052, 0.001060,
//        0.001069, 0.001080, 0.001091, 0.001102, 0.001114, 0.001127, 0.001141,
//        0.001156, 0.001172, 0.001190, 0.001209, 0.001229, 0.001251, 0.001275,
//        0.001302, 0.001332, 0.001365, 0.001403, 0.001499, 0.001638, 0.001893 };

//    // vg: Spec. Vol. Sat. Vapor (m³/kg)
//    double[] v_g = { 2.828, 2.361, 1.982, 1.673, 1.21, 0.892, 0.669, 0.509, 0.393,
//        0.307, 0.243, 0.194, 0.157, 0.127, 0.104, 0.086, 0.072, 0.06, 0.05, 0.042,
//        0.036, 0.03, 0.026, 0.022, 0.015, 0.011, 0.007 };

//    // uf: Int. Energy Sat. Liquid (kJ/kg)
//    double[] u_f = { 355.8, 376.8, 397.9, 418.9, 461.1, 503.5, 546.0, 588.7, 631.7,
//        674.9, 718.3, 762.1, 806.2, 850.6, 895.5, 940.8, 986.7, 1033, 1080, 1128,
//        1177, 1227, 1279, 1332, 1445, 1570, 1725 };

//    // ug: Int. Energy Sat. Vapor (kJ/kg)
//    double[] u_g = { 2488, 2494, 2501, 2507, 2518, 2529, 2540, 2550, 2559, 2568,
//        2576, 2584, 2589, 2596, 2600, 2603, 2603, 2603, 2603, 2600, 2592, 2587,
//        2573, 2560, 2531, 2462, 2351 };

//    // hf: Enthalpy Sat. Liquid (kJ/kg)
//    double[] enthah_flpyLiq = { 355.9, 376.9, 398.0, 419.0, 461.3, 503.7, 546.3, 589.1,
//        632.2, 675.5, 719.2, 763.2, 807.6, 852.4, 897.8, 943.6, 990.1, 1037.3, 1085.3,
//        1134.4, 1184.5, 1236.0, 1289.0, 1344.0, 1461.5, 1594.1, 1760.5 };

//    // hg: Enthalpy Sat. Vapor (kJ/kg)
//    double[] h_g = { 2652, 2660, 2668, 2676, 2691, 2706, 2720, 2734, 2746, 2758, 2769,
//        2778, 2786, 2793, 2798, 2802, 2804, 2804, 2802, 2797, 2790, 2780, 2766, 2749,
//        2700, 2622, 2481 };

//    // sf: Entropy Sat. Liquid (kJ/kg*K)
//    double[] s_f = { 1.134, 1.193, 1.250, 1.307, 1.418, 1.528, 1.634, 1.739, 1.842, 1.943,
//        2.042, 2.140, 2.236, 2.331, 2.425, 2.518, 2.610, 2.702, 2.793, 2.884, 2.975, 3.067,
//        3.159, 3.253, 3.448, 3.659, 3.915 };

//    // sg: Entropy Sat. Vapor (kJ/kg*K)
//    double[] s_g = { 7.544, 7.479, 7.416, 7.355, 7.239, 7.130, 7.027, 6.930, 6.838, 6.750,
//        6.666, 6.586, 6.508, 6.432, 6.358, 6.286, 6.215, 6.144, 6.073, 6.002, 5.930, 5.857,
//        5.782, 5.704, 5.536, 5.336, 5.053 };

//    // Lets compute all themophysical parameters at temp T = 257C
//    Console.WriteLine($"Pressure at T = 257 = {Interp1(T, P, 257)}");
//    Console.WriteLine($"Gas Entrophy at T = 257 = {Interp1(T, s_g, 257)}");
//    Console.WriteLine($"Liquid Enthropy at T = 257 = {Interp1(T, s_f, 257)}");
//    Console.WriteLine($"Gas Specific Volume at T = 257 = {Interp1(T, v_g, 257)}");
//}