using ConsoleApp1;
{
    double[] robertson(double t, double[] y) =>
    [-0.04 * y[0] + 1e4 * y[1]*y[2],
      0.04 * y[0] - 1e4 * y[1]*y[2] - 3e7*y[1]*y[1],
      3e7*y[1]*y[1]];

    //Solve ODE
    var opts = Odeset(Stats: true);
    //(ColVec T, Matrix Y) = Ode45s(robertson, [1, 0, 0], [0, .. Logspace(-7, 7)], opts);
    (ColVec T, Matrix Y) = Ode45s(robertson, [1, 0, 0], [0, 0.04e9], opts);
    // Plot the result
    Y[.., 1] = 1e4*Y[.., 1];
    SemiLogx(T, Y);
    Xlabel("Time t"); Ylabel("Soluton y");
    Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
    Title("Solution of Robertson's ODE with ODE45s");
    SaveAs("Robertson-ODE-Ode45s.png");
}

{
    double Pr = 13, Tr = 1.05;
    double t, tm1, tm1e2, A, B,
           C, D, y2, y3, y4, Den;

    t = 1 / Tr;
    tm1 = 1 - t; tm1e2 = tm1 * tm1;
    A = 0.06125 * t * Exp(-1.2 * tm1e2);
    B = t * (14.76 - t * (9.76 - t * 4.58));
    C = t * (90.7 - t * (242.2 - t * 42.4));
    D = 2.18 + 2.82 * t; 
    var yfunc = new Func<double, double>(y =>
    {
        y2 = y * y; y3 = y2 * y; y4 = y3 * y;
        Den = Pow(1 - y, 3);
        return -A * Pr + (y + y2 + y3 - y4) / Den -
        B * y2 + C * Pow(y, D);
    });
    double y_1 = Fsolve(yfunc, 0.5 * A * Pr);
    double y_2 = Fsolve(yfunc, 1.5 * A * Pr);
}
Writer.Run();