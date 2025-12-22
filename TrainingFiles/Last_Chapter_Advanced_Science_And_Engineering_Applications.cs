
namespace ConsoleApp1.TrainingFiles
{
    public class Last_Chapter_Advanced_Science_And_Engineering_Applications
    {
        public static void Run()
        {
            // This is a placeholder for the advanced engineering applications chapter.
            // You can add your code here to implement the specific applications you want to demonstrate.
            Console.WriteLine("Advanced Engineering Applications Chapter");
            Console.WriteLine("This chapter will cover various advanced engineering applications.");


            {
                // Damping Systems
                double stiffness = 2.5, damping = 0.2,
                    mass = 1.0, k = stiffness/mass,
                    c = damping/mass, t = 0, dt = 1.0/15;
                double[] Dynamic(ColVec y) =>
                    [y[1], -(k*y[0]+c*y[1])/mass];
                ColVec Evolve(ColVec y, double t) =>
                    rk4((t, y) => Dynamic(y), t, y, dt);
                double[] y0 = [1.5, 0]; ColVec y = y0;

                hold = true;
                var rec = Rectangle([-3, -5, 0.5, 3]);
                rec.FillColor = [0.7, 0.7, 0.7];
                rec = Rectangle([-2.4, -5.2, 0.2, 3.4]);
                rec.FillColor = [0.4, 0.4, 0.4];
                rec = Rectangle([-2.2, -5.2, 0.2, 3.4]);
                rec.FillColor = [0.4, 0.4, 0.4];

                rec = Rectangle([-4, -7, 3, 7], 0.5);
                rec.FillColor = [0, 0, 0];
                rec.FillAlpha = 0.5;
                rec = Rectangle([-0.5, -3.5, 1, 8.5], 0.1);
                rec.FillColor = [0.7, 0.7, 0.7];
                rec = Rectangle([-1, -4, 2, 1], 1);
                rec.FillColor = [0.0, 0.0, 0.0];
                var body = Rectangle([-0.9, y[0]-0.5, 1.8, 3], 0.2);
                body.FillColor = [0.0, 0.0, 0.0];
                var Marker = Fill([1.5, 1.5, 2], [2.2, 2.8, 2.5], "r");
                ColVec my = Marker.Ydata - y[0];
                ColVec z = Linspace(-3, 1, 321),
                    w = ((z+3)*4*pi)%(2*pi), x = 0.7*Sin(w);
                Indexer gthan = 0.75*pi < w, lthan = w < 1.25*pi;
                x[gthan & lthan] = double.NaN;
                var Spring = Plot(x, z, "k", 3);

                Plot([1.5, 14], [1, 1], "k");
                Plot([2, 2], [-1, 3], "k");
                var plt = Plot([2], [2.5], "r", 2);
                Axis([-5, 15, -8, 6]); hold = false;
                //AxisOff();
                SaveAs("Damping System.png");
                Delete(body);

                byte[] AnimFunc(int i)
                {
                    y = Evolve(y, t); t += dt;
                    body.Y = y[0] - 0.5;
                    Spring.Ydata = Linspace(-3, y[0]-0.5, 321);
                    Marker.Ydata = my + y[0];
                    plt.Xdata = Vcart(2, plt.Xdata + 0.02);
                    plt.Ydata = Vcart(y[0] + 1, plt.Ydata);
                    return GetFrame(800, 560);
                }
                AnimationMaker(AnimFunc, "Damper-Simulation.gif", 30, 600);
                CloseFig();
            }

            {
                // Park Transformation

                double[] clr1 = [1, 0, 0]; // ColorOrder[0];
                double[] clr2 = [0, 0, 1]; // ColorOrder[2];
                double[] clr3 = [0, 0, 0];
                //Park Transform :Ref to q-axis transformation
                double vq = 1.3, vd = 2.0, vs = Hypot(vq, vd),
                    vα = vq, vβ = vd, lq = 2.5, ld = 2.5, offset = 0.2;
                double t = 0, dt = 1.0/50, θ0 = Atan2(vd, vq), θ = 0,
                    dθ = pi/3*dt, sdθ = Sin(dθ), cdθ = Cos(dθ);
                (double vα, double vβ) PkTrans(double θ) =>
                    (Cos(θ+θ0) * vs, Sin(θ+θ0) * vs);
                ArrowHandle ArrowPlotter(double[] p1, double[] p2, double aw, double[] lc,
                    double[] fc) => Arrow(p1, p2, ArrowWidth: aw, LineColor: lc, FillColor: fc);

                Matrix R = new double[,] { { cdθ, sdθ }, { -sdθ, cdθ } },
                    M = new double[,] { {0, vd }, {vq, vd }, {vq, 0 }, {0, -ld }, {lq, 0 } };

                // Plot 1
                Subplot(2, 2, 0); hold = true;
                ArrowPlotter([0, -3], [0, 3], 1, clr3, clr3);
                ArrowPlotter([-0.5, 0], [6.7, 0], 1, clr3, clr3);
                var Va = Plot([0], [vα], "", 2); Va.LineColor = clr1;
                var Vb = Plot([0], [vβ], "", 2); Vb.LineColor = clr2;
                var Ta = Text(offset, vα, @"v_{\alpha}", 25, clr1, Latex);
                var Tb = Text(offset, vβ, @"v_{\beta}", 25, clr2, Latex);
                Axis([-0.5, 6.7, -3, 3]);
                AxisOff(); hold = false;

                // Plot 2
                Subplot(2, 2, 2); hold = true;
                ArrowPlotter([0, -3], [0, 3], 1, clr3, clr3);
                ArrowPlotter([-0.5, 0], [6.7, 0], 1, clr3, clr3);
                var Vq = Plot([0], [vq], "", 2); Vq.LineColor = clr1;
                var Vd = Plot([0], [-vd], "", 2); Vd.LineColor = clr2;
                var Tq = Text(offset, vq, @"v_q", 25, clr1, Latex);
                var Td = Text(offset, -vd, @"v_d", 25, clr2, Latex);
                Axis([-0.5, 6.7, -3, 3]);
                AxisOff(); hold = false;

                Subplot(2, 2, [1, 3]); hold = true;
                ArrowPlotter([-2.6, 0], [2.6, 0], 1, clr3, clr1);
                ArrowPlotter([0, -2.6], [0, 2.6], 1, clr3, clr2);
                var Vs = ArrowPlotter([0, 0], [vq, vd], 2, clr3, clr3);
                var Vr = Plot([vq], [vd], ":k", 2);
                var Pra = Plot([vq, vq], [vd, 0], ":", 2); Pra.LineColor = clr1;
                var Prb = Plot([vq, 0], [vd, vd], ":", 2); Prb.LineColor = clr2;
                var Vra = ArrowPlotter([0, 0], [vα, 0], 2, clr1, clr1);
                var Vrb = ArrowPlotter([0, 0], [0, vβ], 2, clr2, clr2);
                Text(3, 0, @"\alpha", 25, clr1, Latex);
                Text(0, 3, @"\beta", 25, clr2, Latex);
                var Tra = Text(1.1*vα, 0, @"v_{\alpha}", 25, clr1, Latex);
                var Trb = Text(0, 1.1*vβ, @"v_{\beta}", 25, clr2, Latex);

                var Aq = ArrowPlotter([0, 0], [M[4, 0], M[4, 1]], 0.3, clr1, clr1);
                var Ad = ArrowPlotter([0, 0], [M[3, 0], M[3, 1]], 0.3, clr2, clr2);
                var Trq = Text(1.1*M[4, 0], 1.1*M[4, 1], "q", 25, clr1, Latex);
                var Trd = Text(1.1*M[3, 0], 1.1*M[3, 1], "d", 25, clr2, Latex);

                int[] qindx = [0, 1], dindx = [1, 2];
                var Prq = Plot(M[dindx, 0], M[dindx, 1], ":", 2); Prq.LineColor = clr1;
                var Prd = Plot(M[qindx, 0], M[qindx, 1], ":", 2); Prd.LineColor = clr2;
                var Vrq = ArrowPlotter([0, 0], [M[2, 0], M[2, 1]], 2, clr1, clr1);
                var Vrd = ArrowPlotter([0, 0], [M[0, 0], M[0, 1]], 2, clr2, clr2);
                var Trqt = Text(1.1*M[2, 0], 1.1*M[2, 1], @"v_q", 25, clr1, Latex);
                var Trdt = Text(1.1*M[0, 0], 1.1*M[0, 1], @"v_d", 25, clr2, Latex);

                ColVec θrange = Linspace(0, θ);
                var Vθ = Plot(0.4*Cos(θrange), 0.5*Sin(θrange)); Vθ.LineColor = clr3;
                var Tθ = Text(0.7*Cos(0.5*θ), 0.7*Sin(0.5*θ), @"\theta", 25, clr3, Latex);

                Text(0, -2.7, @"v_q - j v_d = (v_{\alpha} + j v_{\beta})e^{-j \theta}", 30, clr3, Latex);
                Axis([-3.2, 3.2, -3.2, 3.2]);
                AxisOff(); hold = false;

                SaveAs("Parktransform2.png", 1000, 500);
                double[] x, y;
                byte[] Animfun(int i)
                {
                    θ += dθ; t += dt;
                    (vα, vβ) = PkTrans(θ); M = R * M;
                    Ta.X = t + offset; Ta.Y = vα;
                    Tb.X = t + offset; Tb.Y = vβ;
                    Va.Ydata = Vcart(Va.Ydata, vα);
                    Va.Xdata = Vcart(Va.Xdata, t);
                    Vb.Ydata = Vcart(Vb.Ydata, vβ);
                    Vb.Xdata = Vcart(Vb.Xdata, t);

                    Tq.X = t + offset; Tq.Y = vq;
                    Td.X = t + offset; Td.Y = -vd;
                    Vq.Ydata = Vcart(Vq.Ydata, vq);
                    Vq.Xdata = Vcart(Vq.Xdata, t);
                    Vd.Ydata = Vcart(Vd.Ydata, -vd);
                    Vd.Xdata = Vcart(Vd.Xdata, t);

                    Vr.Ydata = Vcart(Vr.Ydata, vβ);
                    Vr.Xdata = Vcart(Vr.Xdata, vα);

                    Vs.X2 = vα; Vs.Y2 = vβ; Vra.X2 = vα; Vrb.Y2 = vβ;
                    Pra.Xdata = x = [vα, vα]; Pra.Ydata = y = [vβ, 0];
                    Prb.Xdata = x = [vα, 0]; Prb.Ydata = y = [vβ, vβ];
                    Tra.X = 1.1*vα; Trb.Y = 1.1*vβ;

                    Aq.X2 = M[4, 0]; Aq.Y2 = M[4, 1];
                    Ad.X2 = M[3, 0]; Ad.Y2 = M[3, 1];
                    Trq.X = 1.1*M[4, 0]; Trq.Y = 1.1*M[4, 1];
                    Trd.X = 1.1*M[3, 0]; Trd.Y = 1.1*M[3, 1];

                    Prq.Xdata = M[dindx, 0]; Prq.Ydata = M[dindx, 1];
                    Prd.Xdata = M[qindx, 0]; Prd.Ydata = M[qindx, 1];
                    Vrq.X2 = M[0, 2]; Vrq.Y2 = M[1, 2];
                    Vrd.X2 = M[0, 0]; Vrd.Y2 = M[1, 0];
                    Trqt.X = 1.1*M[0, 2]; Trqt.Y = 1.1*M[1, 2];
                    Trdt.X = 1.1*M[0, 0]; Trdt.Y = 1.1*M[1, 0];

                    θrange = Linspace(0, θ);
                    Vθ.Xdata = 0.5*Cos(θrange); Vθ.Ydata = 0.5*Sin(θrange);
                    Tθ.X = 0.7*Cos(0.5*θ); Tθ.Y = 0.7*Sin(0.5*θ);
                    return GetFrame(1000, 500);
                }
                AnimationMaker(Animfun, "Parktransform2.gif", 30, 300);
                CloseFig();
            }

            {
                // Lengths and fixed points
                double L2 = 2, L3 = 6, L4 = 4, L5 = 6, 
                    L6 = 4, f = 0.667, Twopi = 2*pi;
                ColVec p1 = new double[] { 0, 0 }, 
                       p4 = new double[] { 5, 0 },
                       p6 = new double[] { 10, 1 };

                // Polar to Cartesian
                ColVec Polar(double theta) => 
                    new double[] { Cos(theta), Sin(theta) };

                // Bar Position Functions
                Func<double, ColVec>
                    r2 = theta => L2*Polar(theta), 
                    r3 = theta => L3*Polar(theta),
                    r4 = theta => L4*Polar(theta), 
                    r5 = theta => L5*Polar(theta),
                    r6 = theta => L6*Polar(theta);

                // Constraint
                ColVec p2 = p1, p3 = null, p5 = null;
                ColVec[] Points = [p2, p3, p4, p5, p6];
                double a2 = 0.4791;
                ColVec Con(ColVec y)
                {
                    p3 = p2 + r2(y[0]);
                    p5 = p3 + f*r3(y[1]);
                    Points[1] = p3; Points[3] = p5;
                    return new ColVec[]{ y[0] - a2,
                    p3 + r3(y[1]) - r4(y[2]) - (p4-p1),
                    p5 + r5(y[3]) - r6(y[4]) - (p6-p1) };
                }
                ColVec Theta = new double[] { 0.5, 0.5, 1, 5, 4 };

                // Compute Start Position
                var sol = Fsolve(Con, Theta);

                // Bar Coordinates
                (ColVec BarX, ColVec BarY) BarCoordinates(double L)
                {
                    ColVec t1 = Linspace(0.5*pi, 1.5*pi), t2 = t1 + pi;
                    ColVec[] BarX = [0.4*Cos(t1), 0.4*Cos(t2) + L],
                             BarY = [0.4*Sin(t1), 0.4*Sin(t2)];
                    return (BarX, BarY);
                }

                // Bar Transformation
                (ColVec X, ColVec Y) BarsTrans
                    (double s, ColVec p, ColVec BarX, ColVec BarY)
                {
                    double ct = Cos(s), st = Sin(s);
                    ColVec X = p[0] + ct * BarX - st * BarY,
                           Y = p[1] + st * BarX + ct * BarY;
                    return (X, Y);
                }

                // Anchors 1, 4, and 6
                ColVec t = Linspace(0, Twopi, 7),
                    c = Cos(t), s = Sin(t);

                // Colors
                Matrix clrs = new double[,]{
                    { 51, 200, 255 }, { 0, 0, 255 },
                    { 0, 204, 204 }, { 255, 183, 234 },
                    { 204, 102, 0 }
                };
                clrs /= 255;

                // Plot Anchors and Bars at Start Position
                Subplot(3, 2, [0, 2, 4]); hold = true;
                double[] clr = [0.8, 0.8, 0.8];
                Fill(p1[0] + c, p1[1] + s, clr);
                Fill(p4[0] + c, p4[1] + s, clr);
                Fill(p6[0] + c, p6[1] + s, clr);

                double[] Lengths = [L2, L3, L4, L5, L6];
                (ColVec BarX, ColVec BarY)[] BarsCoords =
                    [.. Lengths.Select(BarCoordinates)];

                FillHandle[] Bars = new FillHandle[5];
                for (int i = 0; i < BarsCoords.Length; i++)
                {
                    (ColVec X, ColVec Y) = BarsCoords[i];
                    (X, Y) = BarsTrans(sol[i], Points[i], X, Y);
                    Bars[i] = Fill(X, Y, [.. clrs[i, ""]]);
                }
                hold = false; Axis([-3, 12, -7, 8]);

                // Compute Velocities and Accelerations
                ColVec AngPos_im1 = sol.ToArray(), 
                       AngPos_i = sol.ToArray(), 
                       AngPos_ip1 = sol.ToArray(),
                       Vel = 15*(AngPos_ip1 - AngPos_im1), 
                       Acc = 900*(AngPos_ip1 - 2*AngPos_i + AngPos_im1);

                // Plot Angles, Velocities, and Accelerations
                Subplot(3, 2, 1); hold = true; int count = 0;
                PlotHandle[] AngPlot = [.. AngPos_ip1.
                    Select(w => Plot([0.0], [w], Linewidth: 2))];
                Array.ForEach(AngPlot, plt => 
                { plt.LineColor = [.. clrs[count, ""]]; count++; });
                Axis([0, 10, 0, 7]); hold = false;

                Subplot(3, 2, 3); hold = true; count = 0;
                PlotHandle[] VelPlot = [.. Vel.
                    Select(w => Plot([0.0], [w], Linewidth: 2))];
                Array.ForEach(VelPlot, plt => 
                { plt.LineColor = [.. clrs[count, ""]]; count++; });
                Axis([0, 10, -4, 4]); hold = false;

                Subplot(3, 2, 5); hold = true; count = 0;
                PlotHandle[] AccPlot = [.. Acc.
                    Select(w => Plot([0.0], [w], Linewidth: 2))];
                Array.ForEach(AccPlot, plt => 
                { plt.LineColor = [.. clrs[count, ""]]; count++; });
                Axis([0, 10, -20, 20]); hold = false;

                byte[] Animfun(int i)
                {
                    a2 = (AngPos_ip1[0] + pi/30) % Twopi;
                    (AngPos_im1, AngPos_i, AngPos_ip1) =
                        (AngPos_i, AngPos_ip1, Fsolve(Con, AngPos_ip1) % Twopi);
                    Vel = 15*(AngPos_ip1 - AngPos_im1);
                    Acc = 900*(AngPos_ip1 - 2*AngPos_i + AngPos_im1);
                    for (int j = 0; j < BarsCoords.Length; j++)
                    {
                        (ColVec X, ColVec Y) = BarsCoords[j];
                        (Bars[j].Xdata, Bars[j].Ydata) =
                            BarsTrans(AngPos_ip1[j], Points[j], X, Y);
                        AngPlot[j].Xdata = Vcart(0, AngPlot[j].Xdata + 1.0/30);
                        AngPlot[j].Ydata = Vcart(AngPos_ip1[j], AngPlot[j].Ydata);
                        VelPlot[j].Xdata = Vcart(0, VelPlot[j].Xdata + 1.0/30);
                        VelPlot[j].Ydata = Vcart(Vel[j], VelPlot[j].Ydata);
                        AccPlot[j].Xdata = Vcart(0, AccPlot[j].Xdata + 1.0/30);
                        AccPlot[j].Ydata = Vcart(Acc[j], AccPlot[j].Ydata);
                    }
                    return GetFrame(1000, 500);
                }
                AnimationMaker(Animfun, "Six_Link.gif", 30, 300);
                CloseFig();
            }

            {
                //X Grid, Ship Structure and Dynamics Parameters
                double nan = double.NaN;
                double[] Xs = [-3,-4,0,0,nan,3,4,0,0,nan,-3,-2.5,2.5,3,
                    nan,-1.5,-1.5,1.5,1.5,nan,-0.5,-0.5,0.5,0.5];
                double[] Ys = [-5,-1,0,-5,nan,-5,-1,0,-5,nan,-1,2,2,-1,
                    nan,2,3,3,2,nan,3,5,5,3];

                double I = 1.2e5, c = 1e5, k = 3e5, alp = 1.5e6, M = 5e4;

                // Wave Parameters
                double[] amplitude = Rand(10),
                    wavelength = [.. Rand(10).Select(r => 10*r)],
                    phase = [.. Rand(10).Select(p => pi*p)];

                // Roll Transform
                (ColVec X, ColVec Y) Roll(double phi, ColVec X, ColVec Y)
                {
                    double ct = Cos(phi), st = Sin(phi);
                    return (ct * X - st * Y, st * X + ct * Y);
                }

                // Water Wave
                ColVec Wave(ColVec x, double t)
                {
                    ColVec height = -3+0*x; double TwoPi = 2*pi;
                    for (int i = 0; i < amplitude.Length; i++)
                    {
                        double k = TwoPi / wavelength[i];
                        double omega = Sqrt(9.81 * k);
                        height += 0.2 * amplitude[i] *
                            Sin(k * x - omega * t + phase[i]);
                    }
                    return height;
                }

                // Diagram
                var Ship = Plot(Xs, Ys, "k", 2); hold = true;
                ColVec X = Linspace(-10, 10, 501); double dx = 20.0/500;
                ColVec D = X.Select(x => -4 < x && x < 4 ? -x : 0).ToArray();
                double[] Xw = [-10, .. X, 10], Yw = [-10, .. Wave(X, 0), -10];
                var Water = Fill(Xw, Yw, [0.5294, 0.8078, 0.9216]);
                hold = false; Axis([-10, 10, -6, 6]); AspectRatio(1, 1);

                // Water Wave Force Torque about the Ship Center of Gravity
                double WaveForce(double t) =>
                    dx*Wave(X, t).Zip(D, (y, d) => y*d).Sum();

                // Ships Roll Dynamics
                double[] RollDynamics(double t, double[] y) =>
                    [y[1],
                     (-c*y[1] - k*y[0] - alp*Pow(y[0], 3) - M*WaveForce(t))/I];
                double t = 0, phi = 0; double[] y = [0, 0];

                // Animation Function
                byte[] Animfun(int i)
                {
                    y = [.. rk4((t, y) =>
                    RollDynamics(t, [.. y]), t, y, 0.03)];
                    t += 0.03; phi = y[0];
                    (Ship.Xdata, Ship.Ydata) = Roll(phi, Xs, Ys);
                    Yw = [-10, .. Wave(X, t), -10];
                    Water.Ydata = Yw;
                    return GetFrame();
                }
                AnimationMaker(Animfun, "Ship_Roll.gif", 30, 300);
                CloseFig();
            }

            {
                // Example of 2 Bars Double Pendulum different lengths
                double G = 9.8;      // acceleration due to gravity, in m/s^2
                double L1 = 1.0;     // length of pendulum 1 in m
                double L2 = 0.5;     // length of pendulum 1 in m
                ColVec t1 = Linspace(0, pi), t2 = t1 + pi,
                    Bar1X = new List<ColVec> { 0.1*Cos(t1), 0.1*Cos(t2) },
                    Bar1Y = new List<ColVec> { 0.1*Sin(t1), 0.1*Sin(t2) - L1 },
                    Bar2X = new List<ColVec> { 0.1*Cos(t1), 0.1*Cos(t2) },
                    Bar2Y = new List<ColVec> { 0.1*Sin(t1), 0.1*Sin(t2) - L2 };


                //Dynamics of double pendulum
                Indexer i = new(0, 2), j = new(2, 4);
                ColVec derivs(ColVec state)
                {
                    ColVec y = state[i], yp = state[j];
                    double delta = y[0] - y[1],
                        cd = Cos(delta), sd = Sin(delta);
                    Matrix A = new double[,] {
                        { L1*cd/2, L2/3 },
                        { L1*L1/3 + L1*L2, L2*L2*cd/2 }
                    };
                    ColVec b = new double[] {
                        (L1*sd*yp[0]*yp[0] - G*Sin(y[1]))/2,
                        -(L2*L2*sd*yp[1]*yp[1] + (L1 + 2*L2)*G*Sin(y[0]))/2
                    };
                    ColVec[] dydx = [yp, Mldivide(A, b)];
                    return dydx;
                }

                int framerate = 30;

                // create a time array from 0..t_stop
                // sampled at 0.02 second steps
                double dt = 1.0/framerate, T = 10;

                // th1 and th2 are the initial angles (degrees)
                // w10 and w20 are the initial angular velocities (degrees per second)
                double th1 = 90, w1 = 0, th2 = 90, w2 = 0.0, f = pi/180;

                Random Random = new();
                double rand() => Random.NextDouble();

                int N = (int)(2*T*framerate + 1);
                int[] offset = [-1, 0, 1];
                Matrix[] States = [..offset.Select(i=> Ode45((t, s) =>
                    derivs(s), [th1 * f, th2 * f, w1 * f +
                    i * 0.01 * rand(), w2 * f - i * 0.01 * rand()],
                    Linspace(0, T, N)).Y)];

                (ColVec X, ColVec Y) BarsTrans
                    (double s, double x, double y,
                    ColVec BarX, ColVec BarY)
                {
                    double ct = Cos(s), st = Sin(s);
                    ColVec X = x + ct * BarX - st * BarY,
                           Y = y + st * BarX + ct * BarY;
                    return (X, Y);
                }
                double x0 = 0, x1, x2, y0 = 0, y1, y2;
                ColVec barX, barY; double[] xplot, yplot;
                string clr = "rgb";

                (FillHandle Bar1, FillHandle Bar2, ScatterHandle Pins)
                    DBars(RowVec s, string c)
                {
                    (barX, barY) = BarsTrans(s[0], x0, y0, Bar1X, Bar1Y);
                    var Bar1 = Fill(barX, barY, c);
                    x1 = x0 + L1*Sin(s[0]); y1 = y0 - L1*Cos(s[0]);
                    (barX, barY) = BarsTrans(s[1], x1, y1, Bar2X, Bar2Y);
                    var Bar2 = Fill(barX, barY, c);
                    x2 = x1 + L2*Sin(s[1]); y2 = y1 - L2*Cos(s[1]);
                    var Pins = Scatter([0, x1, x2], [0, y1, y2], "fow");
                    return (Bar1, Bar2, Pins);
                }

                hold = true;
                var Systems = offset.Select((_, i) => DBars(States[i][0, ""], $"{clr[i]}")).ToArray();
                hold = false;
                double xmin = -1.1*(L1 + L2), ymin = xmin, xmax = -xmin, ymax = xmax;
                Axis([xmin, xmax, ymin, ymax]); AspectRatio(1, 1);

                byte[] Animfun(int i)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var s = States[j][i, ""];
                        x1 = x0 + L1*Sin(s[0]); x2 = x1 + L2*Sin(s[1]);
                        y1 = y0 - L1*Cos(s[0]); y2 = y1 - L2*Cos(s[1]);
                        xplot = [x0, x1, x2]; yplot = [y0, y1, y2];
                        (Systems[j].Bar1.Xdata, Systems[j].Bar1.Ydata) = BarsTrans(s[0], x0, y0, Bar1X, Bar1Y);
                        (Systems[j].Bar2.Xdata, Systems[j].Bar2.Ydata) = BarsTrans(s[1], x1, y1, Bar2X, Bar2Y);
                        Systems[j].Pins.Xdata = xplot; Systems[j].Pins.Ydata = yplot;
                    }
                    return GetFrame();
                }
                AnimationMaker(Animfun, 
                    "Chaos in 3 Double Pendulums_Different_Lengths.gif", framerate, N);
                CloseFig();
            }

            {
                // Generate rectangular wave
                ColVec x = Linspace(0, 5*pi, 1001),
                    y = Sign(Sin(x));
                // Plot signal and its fourier series
                Plot(x, y, "k",3); hold = true;
                var Fplot = Plot(x, 0*y, "r",2); hold = false;
                // Initialize and fit the model
                LinearRegression model = new();
                List<ColVec> X = [];
                byte[] AnimFun(int i)
                {
                    X.Add(Sin((2*i+1)*x));
                    model.Fit(X, y);
                    Fplot.Ydata = model.Predict(X);
                    return GetFrame();
                }
                AnimationMaker(AnimFun,
                    "Rect-Fourier.gif", 3, 50);
                CloseFig();
            }

            {
                // Generate trapezoidal wave
                ColVec x = Linspace(0, 5*pi, 1001),
                    y = Sin(x);
                Indexer idx = new(0, 100, 1001);
                y = Interp1(x[idx], y[idx], x);
                y = Max(-0.5, Min(0.5, y));
                // Plot signal and its fourier series
                Plot(x, y, "k", 3); hold = true;
                var Fplot = Plot(x, 0*y, "r", 2); hold = false;
                // Initialize and fit the model
                LinearRegression model = new();
                List<ColVec> X = [];
                byte[] AnimFun(int i)
                {
                    X.Add(Sin((2*i+1)*x));
                    //X.Add(Cos((2*i+1)*x));
                    model.Fit(X, y);
                    Fplot.Ydata = model.Predict(X);
                    return GetFrame();
                }
                AnimationMaker(AnimFun,
                    "Trapz-Fourier.gif", 3, 50);
                CloseFig();
            }

            {
                // Example of Reduced Compressibility of natural gas
                static double CrTrHY(double Pr, double Tr)
                {
                    double t, tm1, tm1e2, A, B, C, D,
                        r, y2, y3, y4, yDm1, ym1p3, ym1p4, Den;
                    t = 1 / Tr; tm1 = 1 - t; tm1e2 = tm1 * tm1;
                    A = 0.06125 * t * Exp(-1.2 * Pow(1 - t, 2));
                    B = t * (14.76 - t * (9.76 - t * 4.58));
                    C = t * (90.7 - t * (242.2 - t * 42.4));
                    D = 2.18 + 2.82 * t; r = A * Pr;
                    var yfunc = new Func<double, double>(y =>
                    {
                        y2 = y * y; y3 = y2 * y; y4 = y3 * y;
                        ym1p3 = Pow(1 - y, 3);
                        return -A * Pr + (y + y2 + y3 - y4) /
                        ym1p3 - B * y2 + C * Pow(y, D);
                    });
                    var options = SolverSet(StepFactor: 0.5);
                    double y = Fsolve(yfunc, r, options);
                    y2 = y * y; y3 = y2 * y; y4 = y3 * y;
                    ym1p4 = Pow(1 - y, 4); yDm1 = Pow(y, D - 1);
                    Den = (1 + 4 * y + 4 * y2 - 4 * y3 + y4) /
                        ym1p4 - 2 * B * y + C * D * yDm1;
                    return A*Tr/(y*Den);
                }

                // set up pressure and temperature mesh
                double[] Pr = Linspace(0.2, 20, 501);
                double[] Tr = [1.05, 1.10, 1.20, 1.30, 1.40];
                
                // compute z factors and plot them
                List<string> Tlabels = [];
                List<ColVec> CHY = [..Tr.Select(tr=> 
                    Pr.Select(p => CrTrHY(p, tr)).ToArray())];
                LogLog(Pr, (Matrix)CHY); 
                Axis([0.1, 100, 0.01, 10]);
                Legend([..Tr.Select(tr=>"Tr =" + tr)], UpperRight);
                SaveAs("CrTr-Hall-Yarborough.png");
                CloseFig();
            }

            {
                // Example of Reduced Compressibility of natural gas
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
                        double y = Fsolve(yfunc, r);
                        z = A * Pr / y;
                    }
                    return z;
                }

                static double CrTrHY(double Pr, double Tr)
                {
                    double t, tm1, tm1e2, A, B, C, D,
                        r, y2, y3, y4, yDm1, ym1p3, ym1p4, Den;
                    t = 1 / Tr; tm1 = 1 - t; tm1e2 = tm1 * tm1;
                    A = 0.06125 * t * Exp(-1.2 * Pow(1 - t, 2));
                    B = t * (14.76 - t * (9.76 - t * 4.58));
                    C = t * (90.7 - t * (242.2 - t * 42.4));
                    D = 2.18 + 2.82 * t; r = A * Pr;
                    var yfunc = new Func<double, double>(y =>
                    {
                        y2 = y * y; y3 = y2 * y; y4 = y3 * y;
                        ym1p3 = Pow(1 - y, 3);
                        return -A * Pr + (y + y2 + y3 - y4) /
                        ym1p3 - B * y2 + C * Pow(y, D);
                    });
                    var options = SolverSet(StepFactor: 0.5);
                    double y = Fsolve(yfunc, r, options);
                    y2 = y * y; y3 = y2 * y; y4 = y3 * y;
                    ym1p4 = Pow(1 - y, 4); yDm1 = Pow(y, D - 1);
                    Den = (1 + 4 * y + 4 * y2 - 4 * y3 + y4) /
                        ym1p4 - 2 * B * y + C * D * yDm1;
                    return A*Tr/(y*Den);
                }

                static double ZfactorDAK(double Pr, double Tr)
                {
                    double z = 1;
                    if (Pr != 0)
                    {
                        double Tr2 = Tr * Tr, Tr3 = Tr2 * Tr, Tr4 = Tr3 * Tr, Tr5 = Tr4 * Tr,
                        A1 = 0.3265, A2 = -1.0700, A3 = -0.5339, A4 = 0.01569, A5 = -0.05165,
                        A6 = 0.5475, A7 = -0.7361, A8 = 0.1844, A9 = 0.1056, A10 = 0.6134,
                        A11 = 0.7210, R1 = A1 + A2 / Tr + A3 / Tr3 + A4 / Tr4 + A5 / Tr5,
                        R2 = 0.27 * Pr / Tr, R3 = A6 + A7 / Tr + A8 / Tr2,
                        R4 = A9 * (A7 / Tr + A8 / Tr2), R5 = A10 / Tr3,
                        y2, y5, v = R2, E;
                        var yfunc = new Func<double, double>(y =>
                        {
                            y2 = y * y; y5 = y2 * y2 * y;
                            E = (1 + A11 * y2) * Exp(-A11 * y2);
                            return 1 + R1 * y - R2 / y + R3 * y2 - R4 * y5 + R5 * y2 * E;
                        });
                        double y = Fsolve(yfunc, v);
                        z = R2 / y;
                    }
                    return z;
                }

                static double ZfactorDPR(double Pr, double Tr)
                {
                    double z = 1;
                    if (Pr != 0)
                    {
                        double Tr2 = Tr * Tr, Tr3 = Tr2 * Tr, E,
                            A1 = 0.31506237, A2 = -1.04670990, A3 = -0.57832720, A4 = 0.53530771,
                            A5 = -0.61232032, A6 = -0.10488813, A7 = 0.68157001, A8 = 0.68446549,
                            T1 = A1 + A2 / Tr + A3 / Tr3, T2 = A4 + A5 / Tr, T3 = A5 * A6 / Tr,
                            T4 = A7 / Tr3, T5 = 0.27 * Pr / Tr, y2, y5, v = T5;
                        var yfunc = new Func<double, double>(y =>
                        {
                            y2 = y * y; y5 = y2 * y2 * y; E = (1 + A8 * y2) * Exp(-A8 * y2);
                            return 1 + T1 * y + T2 * y2 + T3 * y5 + T4 * y2 * E - T5 / y;
                        });
                        double y = Fsolve(yfunc, v);
                        z = 0.27 * Pr / (Tr * y);
                    }
                    return z;
                }


                // set up ressure and temperature mesh
                ColVec Pr = Linspace(0.2, 20, 501);
                ColVec Tr = new double[] {1.05,    1.08,   1.12,   1.18,   1.26,   1.35,   1.47,
                                          1.61,    1.75,   1.91,   2.09,   2.29,   2.62,   3.00 };
               
                // compute z factors and plot them
                List<string> Tlabels = [];
                List<ColVec> ZHY = [], ZDAK = [], ZDPR = [], CHY = [];
                hold = true;
                foreach (var tr in Tr)
                {
                    ZHY.Add(Pr.Select(p => ZfactorHY(p, tr)).ToArray());
                    ZDAK.Add(Pr.Select(p => ZfactorDAK(p, tr)).ToArray());
                    ZDPR.Add(Pr.Select(p => ZfactorDPR(p, tr)).ToArray());
                    CHY.Add(Pr.Select(p => CrTrHY(p, tr)).ToArray());
                    Tlabels.Add("Tr = " + tr);
                }
                Subplot(2, 2, 0);
                Plot(Pr, (Matrix)ZHY);

                Subplot(2, 2, 1);
                Plot(Pr, (Matrix)ZDAK);

                Subplot(2, 2, 2);
                Plot(Pr, (Matrix)ZDPR);

                Subplot(2, 2, 3);
                LogLog(Pr, (Matrix)CHY);
                Axis([0.1, 100, 0.01, 10]);

                SaveAs("Zfactor-Hall-Yarborough-CCL-Math.png");
                CloseFig();
            }

            {
                // Example of Dimensionless Water Influx
                // define Wd function in time space.
                Func<double, double> I0 = x => BesselI(0, x),
                    I1 = x => BesselI(1, x), K0 = x => BesselK(0, x), 
                    K1 = x => BesselK(1, x);
                double EdgeClosedBoundaryRadial_Wd(double tD, double rD)
                {
                    // define the embedded laplace space solution
                    Func<double, double> lapW = new(s =>
                    {
                        double sqrts, sqrts3, rDsqrts, Num, Den;
                        sqrts = Sqrt(s); sqrts3 = s * sqrts; rDsqrts = rD * sqrts;
                        if (double.IsInfinity(rD)) {   
                            Num = K1(sqrts); Den = K0(sqrts); 
                        }
                        else{
                            Num = I1(rDsqrts) * K1(sqrts) - K1(rDsqrts) * I1(sqrts);
                            Den = I1(rDsqrts) * K0(sqrts) + K1(rDsqrts) * I0(sqrts);  
                        }
                        return Num / (sqrts3 * Den);
                    });
                    return tD == 0 || rD == 1 ? 0 : NiLaplace(lapW, tD);
                }

                // Create a Plotter
                void Plotter(double[] Td, 
                    double[] Rd, double[] AxisLimit)
                {
                    List<string> lgd = [];
                    foreach (double rD in Rd)
                    {
                        double[] Wd = [..Td.Select(tD =>
                            EdgeClosedBoundaryRadial_Wd(tD, rD))];
                        SemiLogx(Td, Wd, Linewidth: 2);
                        lgd.Add("rD = " + rD);
                    }
                    lgd[^1] = "rD = ∞";
                    Xlabel("tD"); Ylabel("WD");
                    Legend(lgd, UpperLeft);
                    Axis(AxisLimit);
                }

                // define the time and radial mesh
                double inf = double.PositiveInfinity; 
                List<string> lgd; double[] Td, Rd;

                // compute the water influx and plot
                Td = Logspace(-1, 2); lgd = [];
                Rd = [2, 2.5, 3, 3.5, 4, inf];
                Subplot(2, 1, 0); hold = true;
                Plotter(Td, Rd, [0.1, 100, 1, 8]);

                // compute the water influx and plot
                Td = Logspace(0, 3); lgd = [];
                Rd = [5, 6, 7, 8, 9, 10, inf];
                Subplot(2, 1, 1); hold = true;
                Plotter(Td, Rd, [1, 1000, 0, 70]);
                SaveAs("Dimensionless-Water-Influx.png");
                CloseFig();
            }

            {
                // Blausius Boundary Layer
                // define function
                double[] dydt(double t, double[] y) 
                    => [y[1], y[2], -0.5 * y[2] * y[0]];

                // define nonlinear function to
                // shooting for terminal boundary
                ColVec? T = null; Matrix? Y = null;
                double fun(double y3_0)
                {
                    (T, Y) = Ode45(dydt, [0, 0, y3_0], [0, 6]);
                    return Y.LastRow[1] - 1;
                }

                // solve for unknown initial condition
                double y3_0 = Fsolve(fun, 0.5);

                // plot the result
                Plot(T, Y, Linewidth: 2);
                Legend(["f", "f'", "f''"], UpperLeft);
                Axis([0, 6, 0, 3]); 
                Xlabel("η"); 
                Title("Blasius Boundary layer");
                SaveAs("Blasius-Boundary-Layer-CCL-Math.png");
                CloseFig();
            }

            {
                // Howarth's Ttransformation of Blasius boundary layer problem

                // define parameters
                double rhomu_h, drhomu_h_eta, gamma, Pr, C;

                // define functions and their derivatives
                Func<double, double> rho, drhodh, mu, dmudh, rhomu;
                Func<double, double, double> drhomu;

                //define time span and intial guess
                double[] tspan, y0, y35guess;

                // define intexer for the unknwon initial conditions
                Indexer I = new int[] { 1, 3 };

                //define function for solution of howarth transformation
                (ColVec, Matrix) HowarthTransform(double M)
                {
                    // assign parameters, functions anf their derivatives
                    gamma = 1.4;
                    Pr = 0.7;
                    C = Pr * (gamma - 1) * M * M;
                    rho = h => 1.0/h;
                    drhodh = h => -1 / (h * h);
                    mu = h => Pow(h, 2.0 / 3);
                    dmudh = h => 2.0 / 3 * Pow(h, -1.0 / 3);
                    rhomu = h => rho(h) * mu(h);
                    drhomu = (h, dh) => (rho(h) * dmudh(h) + drhodh(h) * mu(h)) * dh;

                    // define the differential equation
                    ColVec dydt(double t, ColVec y)
                    {
                        rhomu_h = rhomu(y[3]);
                        drhomu_h_eta = drhomu(y[3], y[4]);
                        double[] dy = [y[1],
                       y[2],
                       -(2*drhomu_h_eta + y[0])*y[2]/(2*rhomu_h),
                       y[4],
                       -(drhomu_h_eta*y[4] + Pr*y[0]*y[4] + C*rhomu_h*y[2]*y[2])/rhomu_h ];
                        return dy;
                    }

                    // set time span and intial guess
                    tspan = [0, 5]; y35guess = [0.1, 0.2];
                    ColVec? T = null; Matrix? Y = null;

                    // define the nonlinear system to compute the initial condition
                    ColVec fun(ColVec y35_0)
                    {
                        y0 = [0, 0, y35_0[0], 2, y35_0[1]];
                        (T, Y) = Ode45(dydt, y0, tspan);
                        return Y.LastRow[I].T - 1;
                    }

                    // solve for the unknown initial conditions
                    Fsolve(fun, y35guess);
                    return (T, Y);
                }

                // generator solution for M = 0 and plot
                (ColVec T, Matrix Y) = HowarthTransform(0);
                Plot(T, Y["", 1], "b", 2); hold = true;
                Plot(T, Y["", 3] - 1, "r", 2);

                // generator solution for M = 5 and plot
                (T, Y) = HowarthTransform(5);
                Plot(T, Y["", 1], "b", 2);
                Plot(T, Y["", 3] - 1, "r", 2); hold = false;

                // add legend, axis label and title
                Legend(["f'", "h-1"], UpperRight);
                Axis([0, 5, 0, 2]);
                Xlabel("η"); 
                Title("Howarth Transformation");
                SaveAs("Howarth-Transformation-CCL-Math.png");
                CloseFig();
            }

            {
                double mu = 1;
                double[] vdp(double t, double[] y) =>
                    [y[1], mu*(1 - y[0]*y[0])*y[1] - y[0]];

                var options = Odeset(Stats: true);
                (ColVec T, Matrix Y) = 
                    Ode45(vdp, [2, 0], [0, 20], options);

                Plot(T, Y, "-o");
                Xlabel("Time t"); Ylabel("Solution y"); 
                Title("Solution of van der Pol Equation (u = 1)");
                Legend(["y_1", "y_2"], UpperLeft);
                SaveAs("Van der Pol(u = 1).png");
                CloseFig();
            }

            {
                double Ub = 6, R0 = 1000, R15 = 9000, 
                    alpha = 0.99, beta = 1e-6, 
                    Uf = 0.026, c1 = 1e-6, c2 = 2e-6, 
                    c3 = 3e-6;
                double Transfun(ColVec u) =>
                    beta * (Exp((u[1] - u[2]) / Uf) - 1);
                dynamic input(dynamic t) => 
                    0.4 * Sin(200 * pi * t);
                Matrix Mass(double t, ColVec y) =>
                    new double[,] {
                        {-c1,  c1,  0,   0,   0 },
                        { c1, -c1,  0,   0,   0 },
                        { 0,   0,  -c2,  0,   0 },
                        { 0,   0,   0,  -c3,  c3},
                        { 0,   0,   0,   c3, -c3}};

                ColVec dudt(double t, ColVec u)
                {
                    double Ue = input(t),
                           f23 = Transfun(u);
                    return new double[] {
                        -(Ue - u[0])/R0,
                        -(Ub/R15 - u[1]*2/R15 - (1-alpha)*f23),
                        -(f23 - u[2]/R15),
                        -((Ub - u[3])/R15 - alpha*f23),
                        u[4]/R15 };
                }
                double[] tspan = [0, 0.1];
                double[] y0 = [0, Ub / 2, Ub / 2, Ub, 0];

                var opts = Odeset(Stats: true, RelTol: 1e-3, 
                    MassType: Ode.MassType.Constant);

                (ColVec T, Matrix Y) = 
                    Ode45a(dudt, Mass, y0, tspan, opts);
                ColVec X = T, U5 = Y["", 4];
                Scatter(X, input(X), "o"); hold = true;
                Plot(X, U5, "--r"); hold = false;
                Legend(["Input", "Output"], UpperLeft);
                Xlabel("Time t"); Ylabel("Solution y");
                Title("One Transistor Amplifier DAE Problem-DAE45");
                SaveAs("One-Transistor-Amplifier-DAE-Problem-DAE45.png");
                CloseFig();
            }

            {
                // 7 Pleiades sisters
                // define masses
                double[] m = [1, 2, 3, 4, 5, 6, 7];

                // define function
                ColVec pleiades(double t, ColVec q)
                {
                    double[] dqdt = new double[28];
                    double x1, x2, y1, y2, dx, dy, r3;
                    for (int i = 0; i < 7; i++)
                    {
                        // x- velocity of star i
                        dqdt[i + 0] = q[i + 14];
                        // y- velocity of star j
                        dqdt[i + 7] = q[i + 21];
                        x1 = q[i]; y1 = q[i + 7];
                        for (int j = 0; j < 7; j++)
                        {
                            x2 = q[j]; y2 = q[j + 7];
                            if (j != i)
                            {   // The star does not attract itself
                                dx = x2 - x1; dy = y2 - y1;
                                r3 = Pow(dx * dx + dy * dy, 1.5);
                                //impact of star j on x-acceleration of star i
                                dqdt[i + 14] += m[j] * dx / r3;
                                //impact of star j on y-acceleration of star i
                                dqdt[i + 21] += m[j] * dy / r3;
                            }
                        }
                    }
                    return dqdt;
                }

                double[] init = [
                    3, 3,-1, -3, 2, -2, 2,
                    3, -3, 2, 0, 0, -4, 4,
                    0, 0, 0, 0, 0, 1.75, -1.5,
                    0, 0, 0, -1.25, 1, 0, 0
                ];

                Indexer I = new(0, 7), J = I + 7;
                double[] tspan = Linspace(1, 15, 200);
                var opts = Odeset(AbsTol: 1e-15, RelTol: 1e-13);

                (ColVec T, Matrix Y) = Ode89(pleiades, init, tspan, opts);
                Plot(Y["", I], Y["", J], "--");
                Title("Position of Pleiades Stars, Solved by ODE89");
                Xlabel("X Position"); Ylabel("y Position"); AxisEqual();
                SaveAs("Position-of-Pleiades-Stars-Ode89.png");

                var Stars = m.Select(i=>Scatter(0, 0, "fo", 20)).ToList();
                byte[] AnimFun(int i)
                {
                    int j = 0;
                    Stars.ForEach(s => { s.Xdata = Y[i, j];
                        s.Ydata = Y[i, j + 7]; j++; });
                    return GetFrame();
                }
                AnimationMaker(AnimFun, 
                    "Position-of-Pleiades-Stars-Ode89.gif", 10, 200);
                CloseFig();
            }

            {
                // Damping Systems
                double stiffness = 2.5, damping = 0.2, 
                    mass = 1.0, k = stiffness/mass,
                    c = damping/mass, t = 0, dt = 1.0/15;
                double[] Dynamic(ColVec y) =>
                    [y[1], -(k*y[0]+c*y[1])/mass];
                ColVec Evolve(ColVec y, double t) => 
                    rk4((t, y) => Dynamic(y), t, y, dt);
                double[] y0 = [1.5, 0]; ColVec y = y0;

                hold = true;
                var rec = Rectangle([-4, -7, 3, 7], 0.5); 
                rec.FillColor = [0.2, 0.2, 0.2];
                rec = Rectangle([-0.5, -3.5, 1, 8.5], 0.1); 
                rec.FillColor = [0.7, 0.7, 0.7];
                rec = Rectangle([-1, -4, 2, 1], 1); 
                rec.FillColor = [0.0, 0.0, 0.0];
                var body = Rectangle([-0.9, y[0]-0.5, 1.8, 3], 0.2); 
                body.FillColor = [0.0, 0.0, 0.0];
                var Marker = Fill([1.5, 1.5, 2], [2.2, 2.8, 2.5], "r");
                ColVec my = Marker.Ydata - y[0];
                ColVec z = Linspace(-3, 1, 321), 
                    w = ((z+3)*4*pi)%(2*pi), x = 0.7*Sin(w);
                Indexer gthan = 0.75*pi < w, lthan = w < 1.25*pi;
                x[gthan & lthan] = double.NaN;
                var Spring = Plot(x, z, "k", 3);

                Plot([1.5, 14], [1, 1], "k"); 
                Plot([2, 2], [-1, 3], "k");
                var plt = Plot([2], [2.5], "r", 2);
                Axis([-5, 15, -8, 6]); hold = false;
                AxisOff();

                byte[] AnimFunc(int i)
                {
                    y = Evolve(y, t); t += dt;
                    body.Y = y[0] - 0.5;
                    Spring.Ydata = Linspace(-3, y[0]-0.5, 321);
                    Marker.Ydata = my + y[0];
                    plt.Xdata = Vcart(2, plt.Xdata + 0.02);
                    plt.Ydata = Vcart(y[0] + 1, plt.Ydata);
                    return GetFrame(800, 560);
                }
                AnimationMaker(AnimFunc, "Damper-Simulation.gif", 30, 600);
                CloseFig();
            }



            {
                //Baton mechanics example
                double m1 = 0.1, m2 = 0.1, L = 1, 
                    L2 = L * L, g = 9.8;
                Matrix Mass(double t, ColVec y)
                {
                    double y4 = y[4], s = Sin(y4), c = Cos(y4);
                    return new double[,]
                    {
                        { 1, 0, 0, 0, 0, 0 },
                        { 0, m1 + m2, 0, 0, 0, -m2*L*s },
                        { 0, 0, 1, 0, 0, 0 },
                        { 0, 0, 0, m1 + m2, 0, m2*L*c },
                        { 0, 0, 0, 0, 1, 0 },
                        { 0, -L*s, 0, L*c, 0, L2 }
                    };
                }

                ColVec dydt(double t, ColVec y)
                {
                    double[] dy;
                    double y1 = y[1], y3 = y[3], y4 = y[4], 
                        y5 = y[5], s = Sin(y4), c = Cos(y4);
                    return dy = [ y1,
                                  m2*L*y5*y5*c,
                                  y3,
                                  m2*L*y5*y5*s-(m1+m2)*g,
                                  y5,
                                 -g*L*c ];
                }
                double[] tspan = [.. Linspace(0, 4, 121)];
                double[] y0 = [0, 4, L, 20, -pi / 2, 2];
                var options = Odeset(Stats: true, AbsTol: 1e-10, RelTol: 1e-4);
                (ColVec T, Matrix Z) = Ode45a(dydt, Mass, y0, tspan, options);
                ColVec X = Z["", 0], Y = Z["", 2], theta = Z["", 4];
                ColVec s = Sin(theta), c = Cos(theta);
                Matrix xvals = Hcart(X, X + L * c).T, 
                    yvals = Hcart(Y, Y + L * s).T;
                double xmin = xvals.Min()-1, ymin = yvals.Min()-1, 
                    xmax = xvals.Max()+1, ymax = yvals.Max()+1;
                hold = true;
                Indexer indx = new(0, 5, 121);
                Scatter(xvals[0, indx], yvals[0, indx], "or"); 
                Scatter(xvals[1, indx], yvals[1, indx], "og");
                Plot(xvals["", indx], yvals["", indx]); 
                Axis([xmin, xmax, ymin, ymax]);
                hold = false;
                SaveAs("baton_mechanics.png");

                hold = true;
                var End1 = Scatter(xvals[0, 0], yvals[0, 0], "or"); 
                var End2 = Scatter(xvals[1, 0], yvals[1, 0], "og"); 
                var Cnct = Plot(xvals["", 0], yvals["", 0]); 
                Axis([xmin, xmax, ymin, ymax]);
                hold = false;

                byte[] Animfun(int i)
                {
                    End1.Xdata = xvals[0, i]; End1.Ydata = yvals[0, i];
                    End2.Xdata = xvals[1, i]; End2.Ydata = yvals[1, i];
                    Cnct.Xdata = xvals["", i]; Cnct.Ydata = yvals["", i];
                    return GetFrame();
                }
                AnimationMaker(Animfun, "baton_mechanics.gif", 30, 121);
                CloseFig();
            }

            {
                //Baton mechanics example
                double m1 = 0.1, m2 = 0.1, L = 1, 
                    L2 = L * L, g = 9.8;
                Matrix Mass(double t, ColVec y)
                {
                    double y4 = y[4], s = Sin(y4), c = Cos(y4);
                    return new double[,]
                    {
                        { 1, 0, 0, 0, 0, 0 },
                        { 0, m1 + m2, 0, 0, 0, -m2*L*s },
                        { 0, 0, 1, 0, 0, 0 },
                        { 0, 0, 0, m1 + m2, 0, m2*L*c },
                        { 0, 0, 0, 0, 1, 0 },
                        { 0, -L*s, 0, L*c, 0, L2 }
                    };
                }

                ColVec dydt(double t, ColVec y)
                {
                    double y1 = y[1], y3 = y[3], y4 = y[4],
                        y5 = y[5], s = Sin(y4), c = Cos(y4);
                    double[] dy = [ y1,
                                  m2*L*y5*y5*c,
                                  y3,
                                  m2*L*y5*y5*s-(m1+m2)*g,
                                  y5,
                                 -g*L*c ];

                    return Mldivide(Mass(t, y), dy);
                }
                double[] tspan = [.. Linspace(0, 4, 121)];
                double[] y0 = [0, 4, L, 20, -pi / 2, 2];
                var options = Odeset(Stats: true, AbsTol: 1e-10, RelTol: 1e-4);
                (ColVec T, Matrix Z) = Ode45(dydt, y0, tspan, options);
                ColVec X = Z["", 0], Y = Z["", 2], theta = Z["", 4];
                ColVec s = Sin(theta), c = Cos(theta);
                Matrix xvals = Hcart(X, X + L * c).T, 
                    yvals = Hcart(Y, Y + L * s).T;
                double xmin = xvals.Min()-1, ymin = yvals.Min()-1,
                    xmax = xvals.Max()+1, ymax = yvals.Max()+1;
                hold = true;
                Indexer indx = new(0, 5, 121);
                Scatter(xvals[0, indx], yvals[0, indx], "or");
                Scatter(xvals[1, indx], yvals[1, indx], "og");
                Plot(xvals["", indx], yvals["", indx]); 
                Axis([xmin, xmax, ymin, ymax]);
                hold = false;
                SaveAs("baton_mechanics.png");

                hold = true;
                var End1 = Scatter(xvals[0, 0], yvals[0, 0], "or"); 
                var End2 = Scatter(xvals[1, 0], yvals[1, 0], "og");
                var Cnct = Plot(xvals["", 0], yvals["", 0]); 
                Axis([xmin, xmax, ymin, ymax]);
                hold = false;
                byte[] Animfun(int i)
                {
                    End1.Xdata = xvals[0, i]; End1.Ydata = yvals[0, i];
                    End2.Xdata = xvals[1, i]; End2.Ydata = yvals[1, i];
                    Cnct.Xdata = xvals["", i]; Cnct.Ydata = yvals["", i];
                    return GetFrame();
                }
                AnimationMaker(Animfun, "baton_mechanics.gif", 30, 121);
                CloseFig();
            }

            {
                //Baton mechanics example
                double m1 = 0.1, m2 = 0.1, L = 1, L2 = L * L, g = 9.8;

                Indexer I = new(0, 3), J = I + 3;
                ColVec dydt(double t, ColVec state)
                {
                    ColVec y = state[I], yp = state[J];
                    double c = Cos(y[2]), s = Sin(y[2]);
                    Matrix A = new double[,]
                    {
                        { m1+m2,  0,     -m2*L*s },
                        { 0,      m1+m2,  m2*L*c },
                        {-L*s,    L*c,    L2 }
                    };
                    ColVec b = new double[] 
                    { 
                        m2*L*yp[2]*yp[2]*c, 
                        m2*L*yp[2]*yp[2]*s-(m1+m2)*g, 
                        -g*L*c 
                    };
                    ColVec ypp = Mldivide(A, b);
                    ColVec[] dy;
                    return dy = [yp, ypp];
                }
                double[] tspan = Linspace(0, 4, 25);
                double[] y0 = [0, L, -pi/2, 4, 20, 2];
                var options = Odeset(Stats: true, AbsTol: 1e-10, RelTol: 1e-4);
                (ColVec T, Matrix Z) = Ode45(dydt, y0, tspan, options);
                Console.WriteLine(Z);
                ColVec X = Z["", 0], Y = Z["", 1], theta = Z["", 2];
                ColVec s = Sin(theta), c = Cos(theta);
                Matrix xvals = Hcart(X, X + L * c).T, 
                    yvals = Hcart(Y, Y + L * s).T;
                double xmin = xvals.Min()-1, ymin = yvals.Min()-1, 
                    xmax = xvals.Max()+1, ymax = yvals.Max()+1;
                hold = true;
                Indexer indx = new(0, 5, 121);
                Scatter(xvals[0, indx], yvals[0, indx], "or"); 
                Scatter(xvals[1, indx], yvals[1, indx], "og");
                Plot(xvals["", indx], yvals["", indx], "k"); 
                Axis([xmin, xmax, ymin, ymax]);
                hold = false;
                SaveAs("baton_mechanics.png");

                hold = true;
                var End1 = Scatter(xvals[0, 0], yvals[0, 0], "or"); 
                var End2 = Scatter(xvals[1, 0], yvals[1, 0], "og");
                var Cnct = Plot(xvals["", 0], yvals["", 0]); 
                Axis([xmin, xmax, ymin, ymax]);
                hold = false;

                byte[] Animfun(int i)
                {
                    End1.Xdata = xvals[0, i]; End1.Ydata = yvals[0, i];
                    End2.Xdata = xvals[1, i]; End2.Ydata = yvals[1, i];
                    Cnct.Xdata = xvals["", i]; Cnct.Ydata = yvals["", i];
                    return GetFrame();
                }
                AnimationMaker(Animfun, "baton_mechanics.gif", 30, 121);
                CloseFig();
            }

            {
                double G = 9.8;      // acceleration due to gravity, in m/s^2
                double L1 = 1.0;     // length of pendulum 1 in m
                double L2 = 1.0;     // length of pendulum 2 in m
                double L = L1 + L2;  // maximal length of the combined pendulum
                double M1 = 1.0;     // mass of pendulum 1 in kg
                double M2 = 8.0;     // mass of pendulum 2 in kg
                double M3 = 27.0;    // mass of pendulum 3 in kg
                double R1 = 0.1;
                double R2 = R1*Pow(M2/M1, 1.0/3), R3 = R1*Pow(M3/M1, 1.0/3);
                ColVec t = Linspace(0, 2*pi), Ballx = Cos(t), Bally = Sin(t);


                //Dynamics of double pendulum
                ColVec derivs(ColVec state, double M1, double M2)
                {
                    double[] dydx = new double[4];
                    double delta = state[2] - state[0];
                    double den1 = (M1 + M2) * L1 - M2 * L1 * Cos(delta) * Cos(delta), 
                        den2 = L2/L1 * den1; dydx[0] = state[1];
                    dydx[1] = (M2 * L1 * state[1] * state[1] * Sin(delta) * Cos(delta)
                                + M2 * G * Sin(state[2]) * Cos(delta)
                                + M2 * L2 * state[3] * state[3] * Sin(delta)
                                - (M1 + M2) * G * Sin(state[0])) / den1;
                    dydx[2] = state[3];
                    dydx[3] = (-M2 * L2 * state[3] * state[3] * Sin(delta) * Cos(delta)
                                + (M1 + M2) * G * Sin(state[0]) * Cos(delta)
                                - (M1 + M2) * L1 * state[1] * state[1] * Sin(delta)
                                - (M1 + M2) * G * Sin(state[2])) / den2;
                    return dydx;
                }

                int framerate = 30;

                // create a time array from 0..t_stop sampled at 0.02 second steps
                double dt = 1.0/framerate, T = 15;

                // th1 and th2 are the initial angles (degrees)
                // w10 and w20 are the initial angular velocities (degrees per second)
                double th1 = 120.0, w1 = 0.0, 
                    th2 = -60.0, w2 = 0.0, f = pi/180;

                double[] s0 = [th1 * f, w1 * f, th2 * f, w2 * f];
                double[] Radii = [R1, R2, R3];
                double[] X0 = [-3, 0, 3], xplot;
                double[] Y0 = [0, 0, 0], yplot;
                double x0, y0, x1, x2, y1, y2;
                double[][] clr = [[1, 0, 0],[0,1,0],[0,0,1]];

                (FillHandle Ball1, FillHandle Ball2, PlotHandle Connector)
                    SystemDrawing(RowVec s, double x0, double y0, double[] c)
                {
                    x1 = x0 + L1*Sin(s[0]); y1 = y0 - L1*Cos(s[0]);
                    x2 = x1 + L2*Sin(s[2]); y2 = y1 - L2*Cos(s[2]);
                    xplot = [x0, x1, x2]; yplot = [x0, y1, y2];
                    var Connector = Plot(xplot, yplot, "k", 5);
                    var Base = Rectangle([x0-0.1, y0-0.05, 0.2, 0.1], 0);
                    Base.FillColor = c;
                    var Ball1 = Fill(x1 + R1*Ballx, y1 + R1*Bally, c);
                    var Ball2 = Fill(x2 + R2*Ballx, y2 + R2*Bally, c);
                    return (Ball1, Ball2, Connector);
                }

                int N = (int)(T*framerate + 1);
                double[] Masses = [M1, M2, M3];
                Matrix[] States = [..Masses.Select(M => Ode45((t, s) => 
                    derivs(s, M1, M), s0, Linspace(0, T, N)).Y)];

                hold = true;
                var Systems = Masses.Select((M,i) =>
                    SystemDrawing(States[i][0,""], X0[i], Y0[i], clr[i])).ToList();
                Axis([-3*L, 3*L, -1.2*L, L]); hold  = false;
                AspectRatio(1, 1);
                byte[] Animfun(int i)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var st = States[j][i, ""];

                        x0 = X0[j]; y0 = Y0[j];
                        x1 = x0 + L1*Sin(st[0]);
                        x2 = x1 + L2*Sin(st[2]);
                        y1 = y0 - L1*Cos(st[0]);
                        y2 = y1 - L2*Cos(st[2]);
                        xplot = [x0, x1, x2];
                        yplot = [y0, y1, y2];

                        Systems[j].Connector.Xdata = xplot; 
                        Systems[j].Connector.Ydata = yplot;
                        Systems[j].Ball1.Xdata = x1+R1*Ballx; 
                        Systems[j].Ball1.Ydata = y1+R1*Bally;
                        Systems[j].Ball2.Xdata = x2+Radii[j]*Ballx; 
                        Systems[j].Ball2.Ydata = y2+Radii[j]*Bally;
                    }
                    return GetFrame();
                }
                AnimationMaker(Animfun, "Three Double Pendulums_Explicit.gif", framerate, N);
                CloseFig();

                Indexer ind1 = new(0, 2);
                ColVec implicitderivs(ColVec state, double M1, double M2)
                {
                    ColVec y = state[2 * ind1], 
                        yp = state[2 * ind1 + 1];
                    double delta = y[0] - y[1], 
                        cd = Cos(delta), sd = Sin(delta);
                    ColVec b = new double[] { 
                        L1*sd*yp[0]*yp[0] - G*Sin(y[1]),
                       -M2*L2*sd*yp[1]*yp[1] - (M1 + M2)*G*Sin(y[0])
                    };
                    Matrix A = new double[,]{ 
                        { L1*cd, L2 },
                        { (M1+M2)*L1, M2*L2*cd } 
                    };
                    ColVec ypp = Mldivide(A, b);
                    double[] dydx = [yp[0], ypp[0], yp[1], ypp[1]];
                    return dydx;
                }
                States = [..Masses.Select(M => Ode45((t, s) =>
                    derivs(s, M1, M), s0, Linspace(0, T, N)).Y)];

                Systems = [.. Masses.Select((M, i) =>
                    SystemDrawing(States[i][0, ""], X0[i], Y0[i], clr[i]))];
                Axis([-3*L, 3*L, -1.2*L, L]); hold  = false;

                byte[] Animfun2(int i)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var st = States[j][i, ""];

                        x0 = X0[j]; y0 = Y0[j];
                        x1 = x0 + L1*Sin(st[0]);
                        x2 = x1 + L2*Sin(st[2]);
                        y1 = y0 - L1*Cos(st[0]);
                        y2 = y1 - L2*Cos(st[2]);
                        xplot = [x0, x1, x2];
                        yplot = [y0, y1, y2];

                        Systems[j].Connector.Xdata = xplot;
                        Systems[j].Connector.Ydata = yplot;
                        Systems[j].Ball1.Xdata = x1+R1*Ballx;
                        Systems[j].Ball1.Ydata = y1+R1*Bally;
                        Systems[j].Ball2.Xdata = x2+Radii[j]*Ballx;
                        Systems[j].Ball2.Ydata = y2+Radii[j]*Bally;
                    }
                    return GetFrame();
                }
                AnimationMaker(Animfun2, 
                    "Three Double Pendulums_Implicit.gif", framerate, N);
                CloseFig();
            }

            {
                // Example of 2 Bars Double Pendulum
                double G = 9.8;         // acceleration due to gravity, in m/s^2
                double L = 1.0;         // length of pendulum 1 in m
                ColVec t1 = Linspace(0, pi), t2 = t1 + pi,
                    BarX = new List<ColVec> { 0.1*Cos(t1), 0.1*Cos(t2) },
                    BarY = new List<ColVec> { 0.1*Sin(t1), 0.1*Sin(t2) - L };


                //Dynamics of double pendulum
                Indexer i = new(0, 2), j = new(2, 4);
                ColVec derivs(ColVec state)
                {
                    ColVec y = state[i], yp = state[j];

                    double delta = y[0] - y[1], cd = Cos(delta), sd = Sin(delta);

                    Matrix A = new double[,] { { L*cd/2, L/3 }, { 4*L/3, L*cd/2 } };
                    ColVec b = new double[] { (L*sd*yp[0]*yp[0] -   G*Sin(y[1]))/2,
                                              -(L*sd*yp[1]*yp[1] + 3*G*Sin(y[0]))/2 };

                    ColVec[] dydx = [yp, Mldivide(A, b)];
                    return dydx;
                }

                int framerate = 30;

                // create a time array from 0..t_stop sampled at 0.02 second steps
                double dt = 1.0/framerate, T = 15;

                // th1 and th2 are the initial angles (degrees)
                // w10 and w20 are the initial angular velocities (degrees per second)
                double th1 = 90, w1 = 0.0, th2 = 90.0, w2 = 0.0, f = pi/180;

                double rand()
                {
                    Random rand = new();
                    return rand.NextDouble();
                }

                int N = (int)(2*T*framerate + 1); double[] s0; double noise = 0.1;
                s0 = [th1 * f, th2 * f, w1 * f, w2 * f];
                (_, Matrix Y1) = Ode45((t, s) => derivs(s), s0, Linspace(0, T, N));

                s0 = [th1 * f, th2 * f, w1 * f + noise * rand(), w2 * f - noise * rand()];
                (_, Matrix Y2) = Ode45((t, s) => derivs(s), s0, Linspace(0, T, N));

                s0 = [th1 * f, th2 * f, w1 * f - noise * rand(), w2 * f + noise * rand()];
                (_, Matrix Y3) = Ode45((t, s) => derivs(s), s0, Linspace(0, T, N));

                (ColVec X, ColVec Y) BarsTrans(double s, double x, double y)
                {
                    double ct = Cos(s), st = Sin(s);
                    ColVec X = x + ct * BarX - st * BarY,
                           Y = y + st * BarX + ct * BarY;
                    return (X, Y);
                }

                List<Matrix> States = [Y1, Y2, Y3];
                double x0 = 0, x1, x2, y0 = 0, y1, y2;
                ColVec barX, barY; double[] xplot, yplot;
                string clr = "rgb";

                (FillHandle Bar1, FillHandle Bar2, ScatterHandle Pins) DBars(RowVec s, string c)
                {
                    (barX, barY) = BarsTrans(s[0], x0, y0);
                    var Bar1 = Fill(barX, barY, c);
                    x1 = x0 + L*Sin(s[0]); y1 = y0 - L*Cos(s[0]);
                    (barX, barY) = BarsTrans(s[1], x1, y1);
                    var Bar2 = Fill(barX, barY, c);
                    x2 = x1 + L*Sin(s[1]); y2 = y1 - L*Cos(s[1]);
                    var Pins = Scatter([0, x1, x2], [0, y1, y2], "fow");
                    return (Bar1, Bar2, Pins);
                }

                hold = true;
                var Systems = Enumerable.Range(0, 3).Select(i => DBars(States[i][0, ""], $"{clr[i]}")).ToList();
                hold = false;
                double xmin = -1.1*(L + L), ymin = xmin, xmax = -xmin, ymax = xmax;
                Axis([xmin, xmax, ymin, ymax]);

                byte[] Animfun(int i)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        string color = clr[j].ToString();
                        var st = States[j][i, ""];
                        x1 = L * Sin(st[0]);
                        y1 = -L * Cos(st[0]);
                        x2 = x1 + L*Sin(st[1]);
                        y2 = y1 - L*Cos(st[1]);

                        (barX, barY) = BarsTrans(st[0], x0, y0);
                        Systems[j].Bar1.Xdata = barX;
                        Systems[j].Bar1.Ydata = barY;

                        (barX, barY) = BarsTrans(st[1], x1, y1);
                        Systems[j].Bar2.Xdata = barX;
                        Systems[j].Bar2.Ydata = barY;

                        xplot = [0, x1, x2]; yplot = [0, y1, y2];
                        Systems[j].Pins.Xdata = xplot;
                        Systems[j].Pins.Ydata = yplot;
                    }
                    return GetFrame();
                }
                AnimationMaker(Animfun, "Chaos in 3 Double Pendulums.gif", framerate, N);
                CloseFig();
            }

            {
                // 7 Pleiades sisters
                // define masses
                double[] m = [1, 2, 3, 4, 5, 6, 7];

                // define function
                ColVec pleiades(double t, ColVec q)
                {
                    double[] dqdt = new double[28];
                    double x1, x2, y1, y2, dx, dy, r3;
                    for (int i = 0; i < 7; i++)
                    {
                        // x- velocity of star i
                        dqdt[i + 0] = q[i + 14];
                        // y- velocity of star j
                        dqdt[i + 7] = q[i + 21];
                        x1 = q[i]; y1 = q[i + 7];
                        for (int j = 0; j < 7; j++)
                        {
                            x2 = q[j]; y2 = q[j + 7];
                            if (j != i)// The star does not attract itself
                            {
                                dx = x2 - x1; dy = y2 - y1;
                                r3 = Pow(dx * dx + dy * dy, 1.5);
                                //impact of star j on x-acceleration of star i
                                dqdt[i + 14] += m[j] * dx / r3;
                                //impact of star j on y-acceleration of star i
                                dqdt[i + 21] += m[j] * dy / r3;
                            }
                        }
                    }
                    return dqdt;
                }

                double[] init = 
                [
                    3, 3,-1, -3, 2, -2, 2,
                    3, -3, 2, 0, 0, -4, 4,
                    0, 0, 0, 0, 0, 1.75, -1.5,
                    0, 0, 0, -1.25, 1, 0, 0
                ];

                Indexer I = new(0, 7), J = I + 7;
                double[] tspan = Linspace(0, 15, 451);
                var opts = Odeset(AbsTol: 1e-15, RelTol: 1e-13);

                (ColVec T, Matrix Y) = Ode89(pleiades, init, tspan, opts);
                Plot(Y["", I], Y["", J], "--");
                Title("Position of Pleiades Stars, Solved by ODE89");
                Xlabel("X Position"); Ylabel("y Position");
                AxisEqual();
                SaveAs("Position-of-Pleiades-Stars.png");

                Plot(Y["", I], Y["", J]); hold = true;
                ScatterHandle[] Stars = [..I.Select(i => Scatter(0, 0, "fo", 20))];
                hold = false; AxisEqual();
                byte[] AnimFun(int i)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Stars[j].Xdata = Y[i, j];
                        Stars[j].Ydata = Y[i, j + 7];
                    }
                    return GetFrame();
                }
                AnimationMaker(AnimFun, "Position-of-Pleiades-Stars.gif", 30, 451);
                CloseFig();
            }

        }
    }


    public class LinearRegression
    {
        // Declare the coefficients and the intercept
        // which will be calculated by fitting the model
        public double intercept;
        public ColVec coeffs;

        /// <summary>
        /// Compute the intercept and coefficients that best fit the data
        /// </summary>
        /// <param name="X">features</param>
        /// <param name="y">target</param>
        public void Fit(Matrix X, ColVec y)
        {
            // Add column of ones for the intercept
            ColVec ones = Ones(y.Numel);
            Matrix A = Hcart(ones, X);

            //Using ATAp = ATy to evaluate the parameters
            Matrix AT = A.T;
            ColVec p = Mldivide(AT * A, AT * y);

            intercept = p[0]; p[0, ""] = null;
            coeffs = p;
        }

        /// <summary>
        /// Predits the target given feature X
        /// </summary>
        /// <param name="X">feature</param>
        /// <returns>target</returns>
        public ColVec Predict(Matrix X) =>
            intercept + X * coeffs;

        /// <summary>
        /// Calculate R-squared to evaluate model performance
        /// </summary>
        /// <param name="X">feature</param>
        /// <param name="y">target</param>
        /// <returns>R-squared</returns>
        public double Rsquared(Matrix X, ColVec y)
        {
            ColVec ypred = Predict(X);
            double ss_total = (y - y.Mean()).SumSq();
            double ss_residual = (y - ypred).SumSq();
            return 1 - ss_residual / ss_total;
        }
    }
}
