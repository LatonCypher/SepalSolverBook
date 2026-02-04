using ConsoleApp1;
{
    //Z factor application
    static double ZfactorHY(double Pr, double Tr)
    {
        double z = 1, t, tm1, tm1e2, A, B,
            C, D, r, y2, y3, y4, Den;
        if (Pr != 0)
        {
            t = 1 / Tr;
            tm1 = 1 - t; tm1e2 = tm1 * tm1;
            A = 0.06125 * t * Exp(-1.2 * Pow(1 - t, 2));
            B = t * (14.76 - t * (9.76 - t * 4.58));
            C = t * (90.7 - t * (242.2 - t * 42.4));
            D = 2.18 + 2.82 * t; r = A * Pr;
            var yfunc = new Func<double, double>(y =>
            {
                y2 = y * y; y3 = y2 * y; y4 = y3 * y;
                Den = Pow(1 - y, 3);
                return -A * Pr + (y + y2 + y3 - y4) / Den -
                B * y2 + C * Pow(y, D);
            });
            r *= Pr < 5 ? 2 : 1;
            r /= Pr > 13 ? 2 : 1;
            double y = Fsolve(yfunc, r);
            z = A * Pr / y;
        }
        return z;
    }


    // set up ressure and temperature mesh
    ColVec Pr = Linspace(0, 15, 501);
    double[] Tr = [1.05,    1.10,   1.15,   1.20,   1.25,   1.30,   1.35,
                                   1.40,    1.45,   1.50,   1.60,   1.70,   1.80,   1.90,
                                   2.00,    2.20,   2.40,   2.60,   2.80,   3.00];

    // compute z factors and plot them
    List<string> Tlabels = [.. Tr.Select(tr => "Tr = " + tr)];
    Matrix ZHY = Meshfun((p, t) => ZfactorHY(p, t), Pr, Tr);

    // Plot result.
    Plot(Pr, ZHY);
    Legend(Tr.Select(tr => "Tr = " + tr), UpperRight);
    SaveAs("Zfactor_Hall_Yarborough_.png");

    // Literature style plot
    Figure(640, 880);
    ActivateRightAxis(); ActivateTopAxis();
    var z1 = Plot(Pr[Pr <= 8], ZHY[Pr <= 8, ..], "k"); hold  = true;
    var z2 = Plot(Pr[Pr >= 7], ZHY[Pr >= 7, ..], "k"); hold = false;
    SetAxis(z1, X_Axis.Top, Y_Axis.Left, 0, 8, 0, 1.1);
    SetAxis(z2, X_Axis.Bottom, Y_Axis.Right, 7, 15, 0.9, 2.0);
    SaveAs("Hall_Yarborough_Chart.png"); 
    CloseFig();
}


Writer.Run();