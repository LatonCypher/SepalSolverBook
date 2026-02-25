using ConsoleApp1;
static double ComputeHallYarboroughZ(double Pr, double Tr)
{
    double z = 1, r, t, tm1, tm1e2, A, B, C, D;
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
            return (y*(1 + y*(1 + y*(1 - y)))) / Pow(1 - y, 3)
                       - A * Pr - B * Pow(y, 2) + C * Pow(y, D);
        });
        r *= Pr < 5 ? 2 : 1; r /= Pr > 13 ? 2 : 1;
        double y = Fsolve(yfunc, r);
        z = A * Pr / y;
    }
    return z;
}
// Input: Pseudo-reduced pressure and temperature
double Ppr = 1.5, Tpr = 1.1;
double zFactor = ComputeHallYarboroughZ(Ppr, Tpr);
Console.WriteLine($"Calculated Z-factor for Ppr = {Ppr}, and Tpr = {Tpr} is: {zFactor:F4}");
Writer.Run();