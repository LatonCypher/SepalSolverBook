using ConsoleApp1;
{
    //define ODE
    double[] robertson(double t, double[] y) =>
        [-0.04 * y[0] + 1e4 * y[1]*y[2],
          0.04 * y[0] - 1e4 * y[1]*y[2] - 3e7*y[1]*y[1],
          3e7*y[1]*y[1]];

    //Solve ODE
    (ColVec T, Matrix Y) = Ode45s(robertson, [1, 0, 0], Logspace(-7, 7));
    //(ColVec T, Matrix Y) = Ode45s(robertson, [1, 0, 0], [1e-7, 1e7]);
    // Plot the result
    Y[.., 1] = 1e4*Y[.., 1];
    SemiLogx(T, Y);
    Xlabel("Time t"); Ylabel("Soluton y");
    Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
    Title("Solution of Robertson's ODE with ODE45s");
    SaveAs("Robertson-ODE-Ode45s.png");
}
Writer.Run();