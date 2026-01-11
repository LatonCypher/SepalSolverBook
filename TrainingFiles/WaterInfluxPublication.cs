
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


            {
                // Solve Nonlinear System of Polynomials
                Matrix A = new double[,]
                {
            {1, 2},
            {3, 4}
                };
                var opts = SolverSet(Display: true);
                Matrix x = Fsolve(x => x*x*x - A, Ones(2, 2), opts);
                Console.WriteLine(x);
            }

            {
                //Large Nonlinear Systems
                int n = 100000;
                Indexer i = new(0, n), j = new(0, n - 1), jp1 = j + 1,
                    l = new(1, n - 1), lp1 = l + 1, lm1 = l - 1;

                ColVec a = Ones(n-1), b = Ones(n), e = -a,
                    c = 2 * e, d, xstart, F = new double[n];

                SparseMatrix C, D, E;

                ColVec nlsf(ColVec x)
                {
                    F[l] = (3 - 2 * x[l]).Times(x[l]) - x[lm1] - 2 * x[lp1] + 1;
                    F[n - 1] = (3 - 2 * x[n - 1]) * x[n - 1] - x[n - 2] + 1;
                    F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
                    return F;
                }

                Func<ColVec, SparseMatrix> Jac = x =>
                {
                    d = -4 * x + 3 * b;
                    D = new(i, i, d, n, n);
                    C = new(j, jp1, c, n, n);
                    E = new(jp1, j, e, n, n);
                    return C + D + E;
                };

                var opts = SolverSet(Display: true);
                opts.UserDefinedJacobian = Jac;
                xstart = -b;

                var result = Fsolve(nlsf, xstart, opts);
            }

            {
                Matrix A = new double[,] { { 8,    1,    6,    1,  16 },
                                   { 3,    5,    6,    2,  15 },
                                   { 4,    7,    2,    1,  14 } };

                // Extract single element using subscript
                Console.WriteLine($"A = {A}");

                {
                    // Extract single element using subscript
                    Console.WriteLine($"A[1,2] = {A[1, 2]}");
                }


                {
                    //  Extract single element using index
                    Console.WriteLine($"A[5] = {A[5]}");
                }

                {
                    //  Extract multiple elements using index
                    Indexer i = new(2, 5);
                    Console.WriteLine($"A[i] = {A[i]}");
                }

                {
                    //  Extract multiple elements using subscript along a row
                    Indexer j = new(2, 4);
                    Console.WriteLine($"A[1, j] = {A[1, j]}");
                }

                {
                    //  Extract multiple elements using subscript along a col
                    Indexer i = new(0, 3);
                    Console.WriteLine($"A[i, 3] = {A[i, 3]}");
                }

                {
                    //  Extract submatrix elements
                    Indexer i = new(0, 3), j = new(1, 3);
                    Console.WriteLine($"A[i, j] = {A[i, j]}");
                }

                {
                    // Extract single row
                    Console.WriteLine($"A[1,\"\"] = {A[1, ""]}");
                }

                {
                    // Extract multiple rows
                    Indexer i = new(1, 3);
                    Console.WriteLine($"A[i,\"\"] = {A[i, ""]}");
                }
            }

            {
                // Vector declaration
                ColVec C = new double[] { 1, 2, 3, 4, 5 };
                RowVec R = new double[] { 1, 2, 3, 4, 5 };

                // Matrix declaration
                Matrix M = new double[,] { { 1, 2, 3, 4, 5},
                                   { 4, 5, 6, 4, 5},
                                   { 7, 8, 9, 4, 5} };

                // Printing Vectors and Matrix
                Console.WriteLine($"Vector C: {C}");
                Console.WriteLine($"Vector R: {R}");
                Console.WriteLine($"Matrix M: {M}");

                // Operations Involving Matrices and Vectors
                Console.WriteLine($"C * R = {C * R}");
                Console.WriteLine($"C + R = {C + R}");
                Console.WriteLine($"C - R = {C - R}");
                Console.WriteLine($"R * C = {R * C}");
                Console.WriteLine($"R + C = {R + C}");
                Console.WriteLine($"R - C = {R - C}");

                // Transpose
                Console.WriteLine($"Vector C Transpose: {C.T}");

                // Matrix Multiplication
                Console.WriteLine($"M * C = {M * C}");

                // TermWise Operation
                ColVec V = new double[] { 3, 2, 1, -1, -2 },
                       U = new double[] { 2, 3, 1, 1, 2 };

                Console.WriteLine($"V.*U = {V.Times(U)}");
                Console.WriteLine($"V.^U = {V.Pow(U)}");
                Console.WriteLine($"V./U = {V.Div(U)}");

                // Solution of Linear System
                Matrix A = Rand(7, 7);
                ColVec b = Rand(7);
                ColVec x = Mldivide(A, b);


                Console.WriteLine($"A = {A}");
                Console.WriteLine($"b = {b}");
                Console.WriteLine($"x = {x}");

            }

            {
                //double Fs = 1000;            // Sampling frequency
                //double T = 1/Fs;             // Sampling period
                //int L = 1500;                // Length of signal
                //Indexer I = new(0, L);       // Indexer for the signal
                //ColVec t = I*T;              // Time vector

                //ColVec S = 0.8 + 0.7*Sin(2*pi*50*t) + Sin(2*pi*120*t);
                //ColVec Noise = Randn(t.Numel);
                //ColVec X = S + 2*Noise;

                //Plot(1000*t, X);
                //Title("Signal Corrupted with Zero-Mean Random Noise");
                //Xlabel("t (milliseconds)");
                //Ylabel("X(t)");
            }

            {
                //ColVec xdata, ydata, x, y_est; double[] x0;
                //xdata = new double[] { 0.9, 1.5, 13.8, 19.8, 24.1, 28.2, 35.2, 60.3, 74.6, 81.3 };
                //ydata = new double[] { 455.2, 428.6, 124.1, 67.3, 43.2, 28.1, 13.1, -0.4, -1.3, -1.5 };

                //x0 = [100, -1];
                //static ColVec model(ColVec x, ColVec xdata) => x[0] * Exp(x[1] * xdata);
                //var opts = OptimSet(Display: true, MaxIter: 200, StepTol: 1e-6, OptimalityTol: 1e-6);
                //var ans = Lsqcurvefit(model, x0, xdata, ydata, options: opts);
                //Console.WriteLine($"x = {ans.x.T}");

                //x = Linspace(xdata.First(), xdata.Last());
                //Scatter(xdata, ydata); hold = true;
                //Plot(x, y_est = model(ans.x, x), "r", Linewidth: 2);
                //Axis([xdata.Min()-0.01*xdata.Range(), xdata.Max()+0.01*xdata.Range(),
                //     ydata.Min()-0.1*ydata.Range(), ydata.Max()+0.1*ydata.Range()]);

                //Xlabel("x"); Ylabel("y"); Legend(["Measured Data", "Model Estimate"], Alignment.UpperRight);
                //Title("Example of CurveFitting using Lsqcurvefit");
                //SaveAs("LMTest1.png");

                //AnimateHistory(model, xdata, ydata, ans.output);
            }

            {
                //Func<double, double> f = x => x*BesselJ(0, 2*x) - BesselJ(1, 3*x);
                //var  options = SolverSet(Display: true);
                //var x = NextZero(f, 3);
                //Console.WriteLine(x);

                //x = NextZero(f, x+0.5);
                //Console.WriteLine(x);

                //x = NextZero(f, x+0.5);
                //Console.WriteLine(x);
            }

            {
                //Func<double, double> f = x => Pow(Abs(x-1.0/7), -0.5)*Pow(Abs(x-2.0/3), -0.15);
                //double I = Integral(f, 0, 1, 1e-6, [1.0/7, 2.0/3]);
                //Console.WriteLine(I);
            }

            {
                //double ymin, ds;
                //ColVec xtop, ytop, xbase, ybase;
                //RowVec s, s1, s2, s3, S1, S2, S3, Lx, Ly, L = 0, Tx, Ty, T = 0, t;
                //Indexer Gas_idx, Oil_idx, Water_idx, Base_idx, ps1, ps2, ps3;

                //xtop = Linspace(-3, 3, 5001);
                //// Impermeable CapRock
                //ytop = 9 - PowTW(xtop, 2);
                //// Gas
                //Gas_idx = ytop >= 6;
                //// Oil
                //Oil_idx = (Oil_idx = 3 <= ytop) & (Oil_idx = ytop <= 6);
                //// Water
                //Water_idx =  ytop <= 3;
                //// Impermeable BaseRock
                //ymin = ytop.Min();
                //xbase = xtop.ToArray();
                //ybase = 5 - PowTW(xbase, 2);
                //Base_idx = ybase > ymin;
                //// Derick
                //RowVec dx = Linspace(0.2, 0.5, 5), dy = 8.75-8.5*dx;
                //Matrix Dx = Vcart(dx, -dx), Dy = Vcart(dy, dy);
                //ColVec DX = Dx.ToArray(), DY = Dy.ToArray();
                ////Supply line
                //Lx = new double[] { 0, 0, 6.0 };
                //Ly = new double[] { 2.6, 5.5, 5.5 };
                //L = Hcart(L, Hypot(Diff(Lx), Diff(Ly))).CumSum();
                //s = Linspace(0, L.Last(), 31);
                ////Injection line
                //Tx = new double[] { -2.0, -2.0, -5 };
                //Ty = new double[] { 1.0, 5.5, 5.5 };
                //T = Hcart(T, Hypot(Diff(Tx), Diff(Ty))).CumSum();
                //t = Linspace(0, T.Last(), 31);
                //void PlotFun(int i)
                //{
                //    Subplot(2, 2, 2*i);
                //    Plot(xtop, 0.5*ytop, "k", Linewidth: 5); hold = true;
                //    Fill(xtop[Gas_idx], 0.5*ytop[Gas_idx], "g");
                //    Fill(xtop[Oil_idx], 0.5*ytop[Oil_idx], "r");
                //    Fill(xtop[Water_idx], 0.5 * ytop[Water_idx], "b");
                //    Fill(xbase[Base_idx], 0.5*ybase[Base_idx], "k");
                //    Rectangle([-0.5, 7, 1, 0.25], 0, "k");
                //    Plot(dx, dy, "k", Linewidth: 2);
                //    Plot(-dx, dy, "k", Linewidth: 2);
                //    Plot(DX, DY, "k", Linewidth: 2);
                //    Plot(-DX, DY, "k", Linewidth: 2);
                //    Scatter(Lx[1], Ly[1], "fok");

                //    Rectangle([6.0, 5, 0.5, 1], 1, "w", Linewidth: 3);
                //    Rectangle([6.5, 5, 0.5, 1], 1, "w", Linewidth: 3);
                //    Rectangle([7.0, 5, 0.5, 1], 1, "w", Linewidth: 3);
                //    Rectangle([7.5, 5, 0.5, 1], 1, "w", Linewidth: 3);
                //    Rectangle([8, 3, 2, 5], 1, "w", Linewidth: 3);
                //    Rectangle([5, 3, 6, 2], 1, "w", Linewidth: 3);
                //    Rectangle([5, 6, 3, 1], 0, "w", Linewidth: 3);

                //    Plot(Lx, Ly, "k", Linewidth: 3);
                //}

                //double q1 = 0, q2 = 0, dt = 1; ds = Sqrt(5)/10;
                //List<double> X = [0], Y1 = [q1], Y2 = [q2];
                //byte[] AnimatingFunction(int i)
                //{
                //    Console.WriteLine(i);
                //    DeleteChart();
                //    PlotFun(0);
                //    q1 = 3*dt*random.NextDouble() * Exp(-X.Last()/500);
                //    s1 = s + q1*(i-1)*ds; ps1 = s1 > 0;
                //    S1 = Mod(s1[ps1], L.Last());
                //    Plot(Vcart(Interp1(L, Lx, S1), Interp1(L, Lx, S1 + 0.2)),
                //         Vcart(Interp1(L, Ly, S1), Interp1(L, Ly, S1 + 0.2)),
                //         "r", Linewidth: 2); Axis([-3.5, 11.5, -0.5, 8.5]);

                //    PlotFun(1);
                //    q2 = 3*dt*random.NextDouble();
                //    s2 = s + q2*(i-1)*ds; ps2 = s2 > 0;
                //    S2 = Mod(s2[ps2], L.Last());
                //    Plot(Vcart(Interp1(L, Lx, S2), Interp1(L, Lx, S2 + 0.2)),
                //         Vcart(Interp1(L, Ly, S2), Interp1(L, Ly, S2 + 0.2)),
                //         "r", Linewidth: 2);

                //    Plot(Tx, Ty, "k", Linewidth: 3);
                //    s3 = t + 3*(i-1)*ds; ps3 = s3 > 0;
                //    S3 = Mod(s3[ps3], T.Last());
                //    Plot(Vcart(Interp1(T, Tx, S3), Interp1(T, Tx, S3 + 0.2)),
                //         Vcart(Interp1(T, Ty, S3), Interp1(T, Ty, S3 + 0.2)),
                //         "b", Linewidth: 2); Axis([-3.5, 11.5, -0.5, 8.5]);

                //    X.Add(X.Last() + dt);
                //    Y1.Add(Y1.Last() + q1);
                //    Y2.Add(Y2.Last() + q2);
                //    Subplot(1, 2, 1);
                //    Plot((ColVec)X, (ColVec)Y1, "r"); hold = true;
                //    Plot((ColVec)X, (ColVec)Y2, "r", Linewidth: 2);
                //    Axis([0, 500, 0, 750]); hold = false;
                //    Xlabel("time(Days)"); Ylabel("CumProd(MSTB/Day)");
                //    Legend(["No Pressure Maintenance", "With Pressure Maintenance"], Alignment.UpperLeft);
                //    return GetImageBytes(1100, 700);
                //}
                //Animation.Make(AnimatingFunction, "ProdOptim.gif", 10, 501);
            }

            {
                //ColVec x1 = Linspace(0, 5, 500),
                //       x2 = Cos(6.2957*x1),
                //       y1 = Exp(-0.5*x1),
                //       y2 = Exp(-0.5*Sqr(x1)),
                //       y3 = Exp(-0.5*Sqrt(5.0-x1)),
                //       y4 = Sin(8.8141*x1);
                //Matrix Y = new List<ColVec> { y1, y2, y3 };
                //Subplot(1, 1, 1);
                //Plot(x1, Y);

                //Subplot(3, 3, 1);
                //Plot(x2, y4);
                //SaveAs("plottesting.png");
            }

            {
                //// Example of finding roots of a polynomial with complex coefficients
                //double[] Coeffs = [2, 3, 4, 2, 7, 4];
                //Complex[] roots = Roots(Coeffs);

                //// Print the result
                //Console.WriteLine($"Roots:\n {string.Join("\n ", roots)}");
            }

            {
                //double x = 3e200, y = 4e200;
                //var hyp = Sqrt(Pow(x, 2)+Pow(y, 2));
                //var hypotenuse = Hypot(x, y);
                //Console.WriteLine(hyp);
                //Console.WriteLine(hypotenuse);
            }

        }


    }
}
