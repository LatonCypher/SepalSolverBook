using SepalSolver;
using static SepalSolver.Math;
using static SepalSolver.PlotLib.Chart;
using static SepalSolver.PlotLib.Chart.Location;

namespace ConsoleApp1.TrainingFiles
{
    public class WaterInfluxPublication
    {
        public static void Run()
        {
            Func<double, double> J0, J1, Y0, Y1, I0, I1, K0, K1, EigFun1, EigFun2;
            J0 = (x) => BesselJ(0, x); J1 = (x) => BesselJ(1, x);
            Y0 = (x) => BesselY(0, x); Y1 = (x) => BesselY(1, x);
            I0 = (x) => BesselI(0, x); I1 = (x) => BesselI(1, x);
            K0 = (x) => BesselK(0, x); K1 = (x) => BesselK(1, x);
            EigFun1=(x) => J1(x*10)*Y0(x) - J0(x)*Y1(x*10);
            EigFun2=(x) => J1(x*10)*Y1(x) - J1(x)*Y1(x*10);


            double[] ZerosJ0 = {2.404825558, 5.520078112, 8.653727913, 11.79153444, 14.93091771,
                                18.07106397, 21.21163663, 24.35247153, 27.49347913, 30.63460647,
                                33.77582021, 36.91709835, 40.05842576, 43.19979171, 46.34118837,
                                49.48260990, 52.62405184, 55.76551076, 58.90698393, 62.04846920,
                                65.18996481, 68.33146933, 71.47298161, 74.61450065, 77.75602563,
                                80.89755587, 84.03909078, 87.18062985, 90.32217264, 93.46371878,
                                96.60526795, 99.74681986, 102.8883743, 106.0299309, 109.1714897,
                                112.3130503, 115.4546127, 118.5961766, 121.7377421, 124.8793089,
                                128.0208770, 131.1624463, 134.3040166, 137.4455880, 140.5871604,
                                143.7287336, 146.8703076, 150.0118825, 153.1534580, 156.2950343,
                                159.4366112, 162.5781887, 165.7197667, 168.8613454, 172.0029245,
                                175.1445041, 178.2860842, 181.4276647, 184.5692456, 187.7108270,
                                190.8524087, 193.9939907, 197.1355731, 200.2771558, 203.4187388,
                                206.5603221, 209.7019057, 212.8434896, 215.9850737, 219.1266580,
                                222.2682426, 225.4098274, 228.5514125, 231.6929977, 234.8345831,
                                237.9761688, 241.1177546, 244.2593406, 247.4009267, 250.5425130,
                                253.6840995, 256.8256861, 259.9672729, 263.1088598, 266.2504469,
                                269.3920340, 272.5336214, 275.6752088, 278.8167963, 281.9583840,
                                285.0999718, 288.2415596, 291.3831476, 294.5247357, 297.6663239,
                                300.8079121, 303.9495005, 307.0910889, 310.2326775, 313.3742661,
                                316.5158548, 319.6574435, 322.7990324, 325.9406213, 329.0822103,
                                332.2237994, 335.3653885, 338.5069777, 341.6485669, 344.7901563,
                                347.9317456, 351.0733351, 354.2149246, 357.3565141, 360.4981037,
                                363.6396934, 366.7812831, 369.9228729, 373.0644627, 376.2060525,
                                379.3476424, 382.4892324, 385.6308224, 388.7724124, 391.9140025},

                     ZerosJ1 = {3.831705970, 7.015586671, 10.17346814, 13.32369194, 16.47063005,
                                19.61585851, 22.76008438, 25.90367209, 29.04682853, 32.18967991,
                                35.33230755, 38.47476623, 41.61709421, 44.75931900, 47.90146089,
                                51.04353518, 54.18555364, 57.32752544, 60.46945785, 63.61135670,
                                66.75322674, 69.89507184, 73.03689523, 76.17869959, 79.32048718,
                                82.46225992, 85.60401944, 88.74576715, 91.88750425, 95.02923181,
                                98.17095073, 101.3126618, 104.4543658, 107.5960633, 110.7377548,
                                113.8794408, 117.0211219, 120.1627983, 123.3044705, 126.4461387,
                                129.5878032, 132.7294644, 135.8711224, 139.0127774, 142.1544297,
                                145.2960793, 148.4377266, 151.5793716, 154.7210145, 157.8626554,
                                161.0042944, 164.1459316, 167.2875672, 170.4292012, 173.5708336,
                                176.7124647, 179.8540944, 182.9957229, 186.1373501, 189.2789762,
                                192.4206012, 195.5622252, 198.7038481, 201.8454702, 204.9870913,
                                208.1287115, 211.2703310, 214.4119497, 217.5535676, 220.6951848,
                                223.8368013, 226.9784171, 230.1200323, 233.2616469, 236.4032609,
                                239.5448744, 242.6864873, 245.8280997, 248.9697116, 252.1113230,
                                255.2529340, 258.3945445, 261.5361546, 264.6777643, 267.8193735,
                                270.9609824, 274.1025909, 277.2441991, 280.3858069, 283.5274144,
                                286.6690215, 289.8106284, 292.9522349, 296.0938411, 299.2354471,
                                302.3770527, 305.5186581, 308.6602633, 311.8018682, 314.9434728,
                                318.0850773, 321.2266814, 324.3682854, 327.5098891, 330.6514927,
                                333.7930960, 336.9346991, 340.0763021, 343.2179048, 346.3595074,
                                349.5011098, 352.6427120, 355.7843140, 358.9259159, 362.0675176,
                                365.2091192, 368.3507206, 371.4923218, 374.6339230, 377.7755239,
                                380.9171248, 384.0587255, 387.2003261, 390.3419265, 393.4835268};


            double Pd_Infinite_Approx(double tD)
            {
                double Pd = 0;
                if (tD <= 0.01)
                {
                    Pd = 2*Sqrt(tD)/pi;
                }
                else if (tD <= 500)
                {
                    double[] b = [107.5868, 37.60613, 7.038188, 95.13748,
                                 77.0034, 16.63856, 0.5003552, 1.338479];

                    Pd = (b[0]*Pow(tD, b[6]) + b[1]*tD + b[2]*Pow(tD, b[7]))/
                        (b[3] + b[4]*Pow(tD, b[6]) + b[5]*tD + Pow(tD, b[7]));
                }
                else
                {
                    Pd = (0.5*Log(tD) + 0.40454)*(1 + 0.5/tD);
                }
                return Pd;
            }
            double DPd_Infinite_Approx(double tD)
            {
                double Pd = 0;
                if (tD <= 0.01)
                {
                    Pd = 1/Sqrt(pi*tD);
                }
                else if (tD <= 500)
                {
                    double[] b = [3577.752441, 5121.404179, 552.462473, 364.062209, 26.908805, 896.239475,
                          -0.499645, 0.5003552, 0.838834, 1.338479, 0.338479, 95.13748, 77.0034, 16.63856];

                    Pd = (b[0] + b[1]*Pow(tD, b[6]) + b[2]*Pow(tD, b[7]) + b[3]*Pow(tD, b[8]) + 
                        b[4]*Pow(tD, b[9]) + b[5]*Pow(tD, b[10]))/
                        Pow(b[11] + b[12]*Pow(tD, b[7]) + b[13]*tD + Pow(tD, b[9]), 2);
                }
                else
                {
                    Pd = 0.5*(1 - 0.5*Log(tD)/tD + 0.09546/tD)/tD;
                }
                return Pd;
            }
            double Pd_Finite_Approx(double tD, double rD)
            {
                double[] b = [0.0980958, 0.100683, 2.03863];
                double tcross = b[0]*(rD - 1) + b[1]*Pow(rD - 1, b[2]);

                double Pd = 0, beta1, beta12, beta2, beta22, rD2, rD4;
                if (tD <= tcross) Pd = Pd_Infinite_Approx(tD);
                else
                {
                    rD2 = rD*rD; rD4 = rD2*rD2;
                    b = [-0.00870415, -1.08984, 12.4458, -2.8446, 3.4234, -0.949162];
                    beta1 = b[0] + b[1]/Sinh(rD) + b[2]*Pow(rD, b[3]) + b[4]*Pow(rD, b[5]);
                    beta12 = beta1 * beta1;

                    b = [-0.0191642, -2.47644, 25.3343, -2.73054, 6.13184, -0.939529];
                    beta2 = b[0] + b[1]/Sinh(rD) + b[2]*Pow(rD, b[3]) + b[4]*Pow(rD, b[5]);
                    beta22 = beta2 * beta2;

                    Pd = 2/(rD2 - 1)*(0.25 + tD) -
                        (3*rD4 - 4*rD4*Log(rD) - 2*rD2 - 1)/(4*Pow(rD2 - 1, 2)) +
                        2*Exp(-beta12*tD) * Pow(J1(beta1*rD), 2)/
                        (beta12*(Pow(J1(beta1*rD), 2) - Pow(J1(beta1), 2))) +
                        2*Exp(-beta22*tD) * Pow(J1(beta2*rD), 2)/
                        (beta22*(Pow(J1(beta2*rD), 2) - Pow(J1(beta2), 2)));
                }
                return Pd;
            }
            double DPd_Finite_Approx(double tD, double rD)
            {
                double[] b = [0.0980958, 0.100683, 2.03863];
                double tcross = b[0]*(rD - 1) + b[1]*Pow(rD - 1, b[2]);

                double Pd = 0, beta1, beta12, beta2, beta22, rD2, rD4;
                if (tD <= tcross) Pd = DPd_Infinite_Approx(tD);
                else
                {
                    rD2 = rD*rD; rD4 = rD2*rD2;
                    b = [-0.00870415, -1.08984, 12.4458, -2.8446, 3.4234, -0.949162];
                    beta1 = b[0] + b[1]/Sinh(rD) + b[2]*Pow(rD, b[3]) + b[4]*Pow(rD, b[5]);
                    beta12 = beta1 * beta1;

                    b = [-0.0191642, -2.47644, 25.3343, -2.73054, 6.13184, -0.939529];
                    beta2 = b[0] + b[1]/Sinh(rD) + b[2]*Pow(rD, b[3]) + b[4]*Pow(rD, b[5]);
                    beta22 = beta2 * beta2;

                    Pd = 2/(rD2 - 1)
                        - 2*Exp(-beta12*tD) * Pow(J1(beta1*rD), 2)/
                        (Pow(J1(beta1*rD), 2) - Pow(J1(beta1), 2))
                        - 2*Exp(-beta22*tD) * Pow(J1(beta2*rD), 2)/
                        (Pow(J1(beta2*rD), 2) - Pow(J1(beta2), 2));
                }
                return Pd;
            }
            double Qd_Infinite_Approx(double tD)
            {
                double Qd = 0, sqtD;
                double[] b;
                if (tD <= 0.01)
                    Qd = 2*Sqrt(tD/pi);
                else if (tD <= 200)
                {
                    sqtD = Sqrt(tD);
                    b = [1.129552, 1.160436, 0.2642821, 0.01131791, 0.5900113,
                        0.04589742, 1.0, 0.5002034, 1.5, 1.979139];
                    Qd = (b[0]*Pow(tD, b[7]) + b[1]*tD + b[2]*Pow(tD, b[8]) + b[3]*Pow(tD, b[9])) /
                         (b[4]*Pow(tD, b[7]) + b[5]*tD + b[6]);
                }
                else
                {
                    b = [4.3989, 0.43693, -4.16078, 0.09];
                    Qd = Pow(10, b[0] + b[1]*Log(tD) +  b[2]*Pow(Log(tD), b[3]));
                }
                return Qd;
            }
            double Qd_Finite_Approx(double tD, double rD)
            {
                double[] b = [-1.767, -0.606, 0.12368, 3.02, 2.25, 0.50];
                double tcross = b[0] + b[1]*rD + b[2]*Pow(rD, b[4]) + b[3]*Pow(Log(rD), b[5]);

                double Qd = 0, alpha1, alpha12, alpha2, alpha22, rD2, rD4;
                if (double.IsNaN(tcross) || tD <= tcross) Qd = Qd_Infinite_Approx(tD);
                else
                {
                    rD2 = rD*rD; rD4 = rD2*rD2;
                    b = [-0.00222107, -0.627638, 6.277915, -2.734405, 1.2708, -1.100417];

                    alpha1 = b[0] + b[1]/Sinh(rD) + b[2]*Pow(rD, b[3]) + b[4]*Pow(rD, b[5]);
                    alpha12 = alpha1 * alpha1;

                    b = [-0.0191642, -2.47644, 25.3343, -2.73054, 6.13184, -0.939529];
                    alpha2 = b[0] + b[1]/Sinh(rD) + b[2]*Pow(rD, b[3]) + b[4]*Pow(rD, b[5]);
                    alpha22 = alpha2 * alpha2;

                    Qd = (rD2 - 1)/2
                        - 2*Exp(-alpha12*tD) * Pow(J1(alpha1*rD), 2)/
                        (alpha12*(Pow(J0(alpha1), 2) - Pow(J1(alpha1*rD), 2)))
                        - 2*Exp(-alpha22*tD) * Pow(J1(alpha2*rD), 2)/
                        (alpha22*(Pow(J0(alpha2), 2) - Pow(J1(alpha2*rD), 2)));
                }
                return Qd;
            }


            double Pd_Infinite_Exact(double tD)
            {
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), sqrts3 = s*sqrts;
                    double Num = K0(sqrts), Den = K1(sqrts);
                    return Num / (Den * sqrts3);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double DPd_Infinite_Exact(double tD)
            {
                double LapP(double s)
                {
                    double sqrts = Sqrt(s);
                    double Num = K0(sqrts), Den = K1(sqrts);
                    return Num / (Den * sqrts);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double Pd_Finite_Exact(double tD, double rD)
            {
                if (double.IsPositiveInfinity(rD))
                    return Pd_Infinite_Exact(tD);
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), sqrts3 = s*sqrts, rDsqrts = rD*sqrts;
                    double Num = I1(rDsqrts) * K0(sqrts) + K1(rDsqrts) * I0(sqrts),
                           Den = I1(rDsqrts) * K1(sqrts) - K1(rDsqrts) * I1(sqrts);
                    return Num / (Den * sqrts3);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double DPd_Finite_Exact(double tD, double rD)
            {
                if (double.IsPositiveInfinity(rD))
                    return DPd_Infinite_Exact(tD);
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), rDsqrts = rD*sqrts;
                    double Num = I1(rDsqrts) * K0(sqrts) + K1(rDsqrts) * I0(sqrts),
                           Den = I1(rDsqrts) * K1(sqrts) - K1(rDsqrts) * I1(sqrts);
                    return Num /(Den * sqrts);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double Qd_Infinite_Exact(double tD)
            {
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), sqrts3 = s * sqrts;
                    double Num = K1(sqrts), Den = K0(sqrts);
                    return Num /(Den * sqrts3);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double IQd_Infinite_Exact(double tD)
            {
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), sqrts3 = s * sqrts;
                    double Num = K1(sqrts), Den = K0(sqrts);
                    return Num /(Den * sqrts3 * s);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double Qd_Finite_Exact(double tD, double rD)
            {
                if (double.IsPositiveInfinity(rD))
                    return Qd_Infinite_Exact(tD);
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), sqrts3 = s * sqrts, rDsqrts = rD*sqrts;
                    double Num = I1(rDsqrts) * K1(sqrts) - K1(rDsqrts) * I1(sqrts),
                           Den = I1(rDsqrts) * K0(sqrts) + K1(rDsqrts) * I0(sqrts);
                    return Num /(Den * sqrts3);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }
            double IQd_Finite_Exact(double tD, double rD)
            {
                if (double.IsPositiveInfinity(rD))
                    return IQd_Infinite_Exact(tD);
                double LapP(double s)
                {
                    double sqrts = Sqrt(s), sqrts3 = s * sqrts, rDsqrts = rD*sqrts;
                    double Num = I1(rDsqrts) * K1(sqrts) - K1(rDsqrts) * I1(sqrts),
                           Den = I1(rDsqrts) * K0(sqrts) + K1(rDsqrts) * I0(sqrts);
                    return Num /(Den * sqrts3 * s);
                }
                return tD == 0 ? 0 : NiLaplace(LapP, tD);
            }



            //double BottomClosed_Qd(double tD, double hD, double rD)
            //{
            //    Func<double, double> lapW = s =>
            //    {
            //        double phi, lap, s2, bm, bm_rD, jr, term;
            //        phi = Sqrt(s); s2 = s * s;
            //        lap = 2 * hD * s2 / Tanh(phi * hD) / (rD * rD * phi);
            //        for (int m = 0; m <= 100; m++)
            //        {
            //            bm = ZerosJ1[m]; bm_rD = bm / rD;
            //            phi = Sqrt(bm_rD * bm_rD + s);
            //            jr = Pow(J1(bm_rD) / J0(bm), 2);
            //            lap += (term = 8 * hD * s2 / Tanh(phi * hD) * jr / (phi * bm * bm));
            //            //Console.WriteLine($"{m}, {term}");
            //        }
            //        return 1 / lap;
            //    };
            //    //Console.WriteLine("============================");
            //    return tD == 0 ? 0 : NiLaplace(lapW, tD);
            //}

            //double BottomClosed_Qd2(double tD, double hD, double rD)
            //{
            //    Func<double, double> lapW = s =>
            //    {
            //        double phi, lap, s2, bm, bm_rD, jr, term;
            //        phi = Sqrt(s); s2 = s * s;
            //        lap = hD * s2 / Tanh(phi * hD) / (rD * rD * phi);
            //        for (int m = 0; m <= 100; m++)
            //        {
            //            bm = ZerosJ1[m]; bm_rD = bm / rD;
            //            phi = Sqrt(bm_rD * bm_rD + s);
            //            jr = Pow(J1(bm_rD) / J0(bm), 2);
            //            lap += (term = 4 * hD * s2 / Tanh(phi * hD) * jr / (phi * bm * bm));
            //            //Console.WriteLine($"{m}, {term}");
            //        }
            //        return 1 / lap;
            //    };
            //    //Console.WriteLine("============================");
            //    return tD == 0 ? 0 : NiLaplace(lapW, tD);
            //}


            //Matrix Data = ReadMatrix(@"C:\Users\lateef.a.kareem\Documents\MATLAB\WaterInflux\data2.txt");
            //double[] zD = [0.05, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0];
            //double td;
            //var wi = zD.Select(z => string.Format("{0,8:F2}", z)).ToArray();
            //Console.WriteLine($"|   td   |{string.Join(' ', wi)}");
            //Console.WriteLine("--------------------------------------------------------------------------");
            //for (int i = 0; i < Data.Rows; i++)
            //{
            //    td = Data[i, 0];
            //    wi = zD.Select(z => string.Format("{0,8:F4}", BottomClosed_Qd(td, z, 10))).ToArray();
            //    Console.WriteLine($"|  {string.Format("{0,4:F0}", td)}  | {string.Join(' ', wi)}");
            //}

            //wi = zD.Select(z => string.Format("{0,8:F2}", z)).ToArray();
            //Console.WriteLine($"|   td   |{string.Join(' ', wi)}");
            //Console.WriteLine("--------------------------------------------------------------------------");
            //for (int i = 0; i < Data.Rows; i++)
            //{
            //    td = Data[i, 0];
            //    wi = zD.Select(z => string.Format("{0,8:F4}", BottomClosed_Qd2(td, z, 10))).ToArray();
            //    Console.WriteLine($"|  {string.Format("{0,4:F0}", td)}  | {string.Join(' ', wi)}");
            //}

            

            ColVec tD = Logspace(-2, 3, 101);
            ColVec rD = new double[] { 2, 3, 4, 5, 6, 8, double.PositiveInfinity };
            string[] legend = rD.Select(r => $"rD = {r}").ToArray();
            Indexer I = new(0, 10, 101);

            // Polynomial
            Matrix Pd = rD.Select(r => Arrayfun(t => Pd_Finite_Approx(t, r), tD)).ToList();
            Scatter(Log10(tD[I]), Log10(Pd[I, ""])); hold = true;
            //Numerical Laplace Inversion
            Pd = rD.Select(r => Arrayfun(t => Pd_Finite_Exact(t, r), tD)).ToList();
            LogLog(tD, Pd, Linewidth: 2); hold = false;
            minorgrid = true;
            Xlabel("tD"); Ylabel("PD");
            Legend(legend, UpperLeft);
            Axis([0.01, 1000, 0.1, 1000]);
            SaveAs("Pd_Plot.png");


            // Polynomial
            Matrix Qd = rD.Select(r => Arrayfun(t => Qd_Finite_Approx(t, r), tD)).ToList();
            Scatter(Log10(tD[I]), Qd[I, ""]); hold = true;
            //Numerical Laplace Inversion
            Qd = rD.Select(r => Arrayfun(t => Qd_Finite_Exact(t, r), tD)).ToList();
            SemiLogx(tD, Qd, Linewidth: 2); hold = false;
            minorgrid = true;
            Xlabel("tD"); Ylabel("PD");
            Legend(legend, UpperLeft);
            Axis([0.01, 1000, 0, 35]);
            SaveAs("Qd_Plot.png");


            // Discontinuity Problem
            tD = Linspace(25, 55);
            Pd = Hcart(Arrayfun(t => Pd_Finite_Exact(t, 20), tD),
                       Arrayfun(t => Pd_Finite_Approx(t, 20), tD));
            Plot(tD, Pd, Linewidth: 2);
            Legend(["Exact", "Approx"], UpperLeft);
            Axis([25, 55, 2.1, 2.4]);
            SaveAs("Pd_Compare_Plot.png", 750, 600);


            tD = Linspace(80, 110);
            Qd = Hcart(Arrayfun(t => Qd_Finite_Exact(t, 20), tD),
                       Arrayfun(t => Qd_Finite_Approx(t, 20), tD));
            Plot(tD, Qd, Linewidth: 2);
            Legend(["Exact", "Approx"], UpperLeft);
            Axis([80, 110, 39, 44]);
            SaveAs("Qd_Compare_Plot.png", 750, 600);



            {
                double r = 10, t = 20,
                    Pe = Pd_Finite_Exact(t, r), Dpe = DPd_Finite_Exact(t, r),
                    Pa = Pd_Finite_Approx(t, r), Dpa = DPd_Finite_Approx(t, r);
            }

            {
                double r = 10, t = 1000,
                    Pe = Pd_Finite_Exact(t, r), Dpe = DPd_Finite_Exact(t, r),
                    Pa = Pd_Finite_Approx(t, r), Dpa = DPd_Finite_Approx(t, r);
            }

            {
                double t = 1000,
                    Pe = Pd_Infinite_Exact(t), Dpe = DPd_Infinite_Exact(t),
                    Pa = Pd_Infinite_Approx(t), Dpa = DPd_Infinite_Approx(t);
            }


            {
                double r = 10, t = 20,
                    Qe = Qd_Finite_Exact(t, r),
                    Qa = Qd_Finite_Approx(t, r);
            }

            {
                double r = 10, t = 1000,
                    Qe = Qd_Finite_Exact(t, r), IQe = IQd_Finite_Exact(t, r),
                    Qa = Qd_Finite_Approx(t, r);
            }

            {
                double t = 1000,
                    Qe = Qd_Infinite_Exact(t), IQe = IQd_Infinite_Exact(t),
                    Qa = Qd_Infinite_Approx(t);
            }
        }
    }
}
