using ConsoleApp1;
{
    double[] robertson(double t, double[] y) =>
    [-0.04 * y[0] + 1e4 * y[1]*y[2],
      0.04 * y[0] - 1e4 * y[1]*y[2] - 3e7*y[1]*y[1],
      3e7*y[1]*y[1]];

    //Solve ODE
    var opts = Odeset(Stats: true);
    (ColVec T, Matrix Y) = Ode45s(robertson, [1, 0, 0], [0, 4e6], opts);
    // Plot the result
    Y[.., 1] = 1e4*Y[.., 1];
    SemiLogx(T, Y);
    Xlabel("Time t"); Ylabel("Soluton y");
    Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
    Title("Solution of Robertson's ODE with ODE45s");
    SaveAs("Robertson-ODE-Ode45s.png");


    (T, Y) = Ode45s(robertson, [1, 0, 0], [0, .. Logspace(-5.4, 6.6)], opts);
    // Plot the result
    Y[.., 1] = 1e4*Y[.., 1];
    SemiLogx(T, Y);
    Xlabel("Time t"); Ylabel("Soluton y");
    Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
    Title("Solution of Robertson's ODE with ODE45s");
    SaveAs("Robertson-ODE-Ode45s.png");
}
Writer.Run();