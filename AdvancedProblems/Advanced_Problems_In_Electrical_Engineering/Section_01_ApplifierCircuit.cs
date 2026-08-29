using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Electrical_Engineering
{
    internal class Section_01_ApplifierCircuit
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// <code>
            {
                double Ub = 6, R0 = 1000, R15 = 9000, alpha = 0.99,
                    beta = 1e-6, Uf = 0.026, c1 = 1e-6, c2 = 2e-6, c3 = 3e-6;
                double[,] Mass(double t, double[] y) => new double[,]
                {
                    {-c1,  c1,  0,   0,   0 },
                    { c1, -c1,  0,   0,   0 },
                    { 0,   0,  -c2,  0,   0 },
                    { 0,   0,   0,  -c3,  c3},
                    { 0,   0,   0,   c3, -c3}
                };
                double Ue(double t) => 0.4 * Sin(200 * pi * t);
                double[] dudt(double t, double[] u)
                {
                    double f23 = beta * (Exp((u[1] - u[2]) / Uf) - 1);
                    return [ -(Ue(t) - u[0])/R0,
                             -(Ub/R15 - u[1]*2/R15 - (1-alpha)*f23),
                             -(f23 - u[2]/R15),
                             -((Ub - u[3])/R15 - alpha*f23),
                             u[4]/R15 ];
                }
                double[] tspan = [0, 0.1];
                double[] y0 = [0, Ub / 2, Ub / 2, Ub, 0];

                var opts = Odeset(RelTol: 1e-5);
                (ColVec T, Matrix Y) = Ode43a(dudt, Mass, y0, tspan, opts);
                Scatter(T, Arrayfun(Ue, T), "o"); HoldOn();
                Plot(T, Y[.., 4], "--r"); HoldOff();
                Legend(["Input", "Output"], UpperLeft);
                Xlabel("Time t"); Ylabel("Solution y");
                Title("One Transistor Amplifier DAE Problem-Ode45a");
                SaveAs("One-Transistor-Amplifier-DAE-Problem-Ode45a.png");
            }
            /// </code>
            /// 
            /// </BookContent>
        }
    }
}
