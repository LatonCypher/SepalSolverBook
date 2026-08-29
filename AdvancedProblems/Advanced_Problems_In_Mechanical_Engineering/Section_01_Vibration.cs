using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Summary_and_Conclusion.Advanced_Problems_In_Mechanical_Engineering
{
    internal class Section_01_Vibration
    {
        public static void Run()
        {
            /// <BookContent>
            /// <code>
            {
                ColVec t = Linspace(0, 2 * pi), c = Cos(t), s = Sin(t);
                double L1 = 6, L2 = 8, R1 = 1, R2 = 2, M1 = 1, M2 = 8,
                    k = 10, g = 9.8, x1 = -5, x2 = 5, y1 = 10 - L1, y2 = 10 - L2, tn = 0, dt = 0.03;
                RowVec st = Linspace(5, 25, 41), xs = Hcart(0, st, 30), ys = Hcart(0, Sin(pi * st) * 0.5, 0);
                Matrix springcoord = Vcart(xs, ys), coord;


                ColVec yn = new double[] { pi / 3, 0, 0, 0 };
                x1 = -5 + L1 * Sin(yn[0]); x2 = 5 + L2 * Sin(yn[1]);
                y1 = 10 - L1 * Cos(yn[0]); y2 = 10 - L2 * Cos(yn[1]);
                double theta = Atan2(y2 - y1, x2 - x1);
                double springlength = Hypot(y2 - y1, x2 - x1), f = springlength / 30;
                ColVec Offset = new double[] { x1, y1 };
                Matrix Rot = new double[,]
                {
                    { f*Cos(theta), -Sin(theta)  },
                    { f*Sin(theta), Cos(theta) }
                };

                Plot([-10, 10], [10, 10], "k", 10); HoldOn();
                coord = Rot * springcoord + Offset;
                var spring = Plot(coord[0, ..], coord[1, ..], "k", 3);
                var B1 = Fill(R1 * c + x1, R1 * s + y1, "r");
                var B2 = Fill(R2 * c + x2, R2 * s + y2, "b");
                var P1 = Plot([-5, x1], [10, y1], "r", 5);
                var P2 = Plot([5, x2], [10, y2], "b", 5);
                Axis([-15, 15, 0, 17]);
                HoldOff(); AxisEqual(); SaveAs("model.png");
                Matrix A = new double[,]
                {
                    {-(g/L1+k/M1), k*L2/(M1*L1) },
                    {k*L1/(M2*L2), -(g/L2+k/M2) }
                };
                Func<double, ColVec, ColVec> dydt = (t, y) => Vcart(y[2..], A * y[..2]);

                byte[] Animfun(int i)
                {
                    yn = rk4(dydt, tn, yn, dt); tn += dt;
                    x1 = -5 + L1 * Sin(yn[0]); x2 = 5 + L2 * Sin(yn[1]);
                    y1 = 10 - L1 * Cos(yn[0]); y2 = 10 - L2 * Cos(yn[1]);
                    theta = Atan2(y2 - y1, x2 - x1);
                    springlength = Hypot(y2 - y1, x2 - x1); f = springlength / 30;
                    Offset = new double[] { x1, y1 };
                    Rot = new double[,]
                    {
                        { f*Cos(theta), -Sin(theta)  },
                        { f*Sin(theta), Cos(theta) }
                    };
                    coord = Rot * springcoord + Offset;
                    spring.Xdata = coord[0, ..].T;
                    spring.Ydata = coord[1, ..].T;
                    B1.Xdata = R1 * c + x1; B1.Ydata = R1 * s + y1;
                    B2.Xdata = R2 * c + x2; B2.Ydata = R2 * s + y2;
                    P1.Xdata = new double[] { -5, x1 };
                    P1.Ydata = new double[] { 10, y1 };
                    P2.Xdata = new double[] { 5, x2 };
                    P2.Ydata = new double[] { 10, y2 };
                    return GetFrame();
                }
                yn = new double[] { pi / 3, 0, 0, 0 }; tn = 0;
                AnimationMaker(Animfun, "string-pendulum1.gif", 30, 450);
                yn = new double[] { 0, pi / 3, 0, 0 }; tn = 0;
                AnimationMaker(Animfun, "string-pendulum2.gif", 30, 450);
            }
            /// </code>
            /// </BookContent>
        }
    }
}
