namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_1_Pressure_Volume_Temperature_PVT
    {
        public static void Run()
        {
            /// <BookContent>
            /// **Definition:**
            /// PVT modeling describes the changes in hydrocarbon fluid properties (volume, density, and phase) as a function of pressure and temperature. It is essential for converting surface volumes (STB) to reservoir volumes (RB).
            /// 
            /// Key Properties & Symbols 
            /// 
            /// - :math:`B_o`: Oil Formation Volume Factor (RB/STB) 
            /// - :math:`R_s`: Solution Gas-Oil Ratio (scf/STB) 
            /// - :math:`\mu_o`: Oil Viscosity (cP)
            /// - :math:`\gamma_o, \gamma_g`: Specific gravities of oil and gas. 
            /// 
            /// 1. The Bubble Point Pressure (:math:`P_b`):
            /// 
            /// The pressure at which the first bubble of gas comes out of solution. Below this pressure, the fluid is "saturated." A common correlation used is Standing’s method.
            /// 
            /// <math>
            ///     P_b = 18.2 \left[ \left( \frac{R_s}{\gamma_g} \right)^{0.83} \times 10^{(0.00091 T - 0.0125 API)} - 1.4 \right]
            /// </math>
            /// 
            /// Numerical Example:
            /// 
            /// - :math:`R_s = 500 \, \text{scf/STB}`
            /// - :math:`\gamma_g = 0.65`
            /// - :math:`T = 200 \, ^\circ\text{F}`:
            /// - :math:`\text{API} = 35`
            /// 
            /// <code>
            {
                double Rs = 500, gamma_g = 0.65, T = 200,  API = 35, a = Pow(Rs / gamma_g, 0.83);
                double b = Pow(10, (0.00091 * T - 0.0125 * API));
                double Pb = 18.2 * (a * b - 1.4);
                Console.WriteLine($"Bubble Point Pressure = {Pb:F2} psia");
            }
            ///</code>
            ///
            /// 2. Oil Formation Volume Factor(:math:`B_o`): Since oil shrinks as gas escapes, :math:`B_o` is almost always greater than 1.0.For pressures below the bubble point, we use the Standing correlation:
            /// <math>
            ///     B_o = 0.9759 + 0.00012\left[R_s \left( \frac{\gamma_g}{\gamma_o} \right)^{0.5} + 1.25 T \right]^{1.2}
            /// </math>
            /// 
            /// Numerical Example:
            /// 
            /// - :math:`\gamma_o = 0.85` (Typical for 35 API)
            /// Using :math:`R_s`, 
            /// - :math:`\gamma_g`, and
            /// - :math:`T` from above:
            /// 
            /// <code>
            {
                double Rs = 500, gamma_g = 0.65, gamma_o = 0.85, T = 200;
                double F = Rs * Pow(gamma_g / gamma_o, 0.5) + 1.25 * T;
                double Bo = 0.9759 + 0.00012 * Pow(F, 1.2);
                Console.WriteLine($"Bo at Bubble Point = {Bo:F3} RB/STB");
            }
            /// </code>
            /// 
            /// 3. Gas Compressibility Factor(:math:`z`):
            /// 
            /// For gas modeling, the Ideal Gas Law fails at high pressure. We use the :math:`Z`-factor to correct it. The Hall-Yarborough or Dranchuk-Abu-Kassam methods are standard for coding this.
            /// 
            /// Linearization for Gas Density:
            /// <math>
            ///     \rho_g = \frac{P \cdot MW_g}{ Z \cdot R \cdot T}
            /// </math>
            /// 
            /// Kareem et al, z factor correlation is a really good approximation and is described below.
            /// 
            /// given that :math:`t = 1/T_{pr}`
            /// 
            /// <math>
            /// \begin{align*}
            /// A &= a_1 t e^{a_2(1-t)^2} P_{pr}, \\
            /// B &= a_3 t + a_4 t^2 + a_5 t^6 P_{pr}^6, \\
            /// C &= a_9 + a_8 t P_{pr} + a_7 t^2 P_{pr}^2 + a_6 t^3 P_{pr}^3,\\
            /// D &= a_{10} t e^{a_{11}(1-t)^2}, \\
            /// \end{align*}
            /// </math>
            /// 
            /// <math>
            /// \begin{equation}
            /// y = \frac{C^3DP_{pr}}{\left(C^2(1+A^2) - A^2 B\right)}
            /// \end{equation}
            /// </math>
            /// 
            /// <math>
            /// \begin{align*}
            /// E &= a_{12} t + a_{13} t^2 + a_{14} t^3, \\
            /// F &= a_{15} t + a_{16} t^2 + a_{17} t^3, \\
            /// G &= a_{18} + a_{19} t
            /// \end{align*}
            /// </math>
            /// 
            /// <math>
            /// \begin{equation}
            /// z = \frac{DP_{pr}(1 + y + y^2 - y^3)}{(DP_{pr} + Ey^2 - Fy^G)(1 - y)^3} 
            /// \end{equation}
            /// </math>
            /// 
            /// <code>
            {
                // Constants from Table 3 for Kareem et al. (2016) Correlation 
                const double a1 = 0.317842, a2 = 0.382216, a3 = -7.768354, a4 = 14.290531;
                const double a5 = 0.000002, a6 = -0.004693, a7 = 0.096254, a8 = 0.166720;
                const double a9 = 0.966910, a10 = 0.063069, a11 = -1.966847, a12 = 21.0581;
                const double a13 = -27.0246, a14 = 16.23, a15 = 207.783, a16 = -488.161;
                const double a17 = 176.29, a18 = 1.88453, a19 = 3.05921;
                double[] poly1 = [a6, a7, a8, a9], poly2 = [a14, a13, a12], 
                    poly3 = [a17, a16, a15], poly4 = [-1,1,1,1]; 
                double Kareem(double Ppr, double Tpr)
                {
                    double t = 1.0 / Tpr, dt = 1.0 - t, dt2 = dt * dt, tPpr = t * Ppr;

                    // Intermediate variables (A through G)
                    double A = a1 * t * Exp(a2 * dt2) * Ppr,
                        B = a3 * t + a4 * t * t + a5 * Pow(t, 6) * Pow(Ppr, 6),
                        C = Polyval(poly1, tPpr), D = a10 * t * Exp(a11 * dt2),
                        E = t * Polyval(poly2, t), F = t * Polyval(poly3, t), G = a18 + a19 * t;

                    // Equation 15: Reduced density y
                    double A2 = A * A, C2 = C * C, C3 = C2 * C,
                        denom_y = C2 * (1.0 + A2) - (A2 * B),
                        y = (C3 * D * Ppr) / denom_y;

                    // Equation 14: Compressibility factor z
                    double num_z = D * Ppr * Polyval(poly4, y);
                    double denom_z = (D * Ppr + E * y*y - F * Pow(y, G)) * Pow(1.0 - y, 3);

                    return num_z / denom_z;
                }

                ColVec Pr = Linspace(0, 15, 501);
                double[] Tr = [1.05,    1.10,   1.15,   1.20,   1.25,   1.30,   1.35,
                               1.40,    1.45,   1.50,   1.60,   1.70,   1.80,   1.90,
                               2.00,    2.20,   2.40,   2.60,   2.80,   3.00];

                // compute z factors and plot them
                List<string> Tlabels = [.. Tr.Select(tr => "Tr = " + tr)];
                Matrix ZHY = Meshfun(Kareem, Pr, Tr);

                // Literature style plot
                Figure(640, 880);
                var z1 = Plot(Pr[Pr <= 8], ZHY[Pr <= 8, ..], "k"); HoldOn();
                var z2 = Plot(Pr[Pr >= 7], ZHY[Pr >= 7, ..], "k"); HoldOff();
                SetAxis(z1, X_Axis.Top, Y_Axis.Left, 0, 8, 0, 1.1);
                SetAxis(z2, X_Axis.Bottom, Y_Axis.Right, 7, 15, 0.9, 2.0);
                SaveAs("Zfactor_Kareem_et_al_.png");
                CloseFig();
            }
            /// </code>
            /// 
            /// 4. Gas Formation Volume Factor( :math:`B_g`) is the ratio of the volume of gas at reservoir conditions to the
            /// volume of the same mass of gas at standard conditions. Because gas is highly
            /// compressible, :math:`B_g` is always a very small number (typically :math:`< 0.01`).

            /// Mathematical Expression:
            /// Derived from the Real Gas Law (:math:`pV = nzRT`):
            /// <math>
            ///     B_g = 0.02827 \frac{Z T}{p} \quad [\text{rcf/scf}]
            /// </math>
            /// Or in field units (res bbl/scf):
            /// <math>
            ///     B_g = 0.005035 \frac{Z T}{p} \quad [\text{rb/scf}]
            /// </math>
            /// Where:
            ///
            /// - :math:`p` = Reservoir pressure (psia)
            ///
            /// - :math:`T` = Reservoir temperature (:math:`^\circ R`)
            ///
            /// - :math:`Z` = Gas deviation factor at :math:`p` and :math:`T`
            /// 
            /// 5.Isothermal Oil Compressibility(:math:`c_o`)
            /// Above the bubble point (undersaturated), the oil volume changes only slightly due to pressure.
            /// <math>
            ///     c_o = \frac{-1}{ V} \left( \frac{\partial V}{\partial P} \right)_T
            /// </math>
            /// 
            /// Code Implementation for Undersaturated :math:`B_o`.
            /// If :math:`P > P_b`, we adjust the :math:`B_{ ob}` (at bubble point) using compressibility:
            /// <code>
            {
                double Bob = 1.32; // Bo at bubble point
                double co = 15e-6; // psi^-1
                double P = 5000;   // Reservoir pressure
                double Pb = 2500;  // Bubble point
                
                // Bo = Bob * exp(-co * (P - Pb))
                double Bo = Bob * Exp(-co * (P - Pb));
                Console.WriteLine($"Undersaturated Bo at {P} psi = {Bo:F3} RB/STB");
            }
            /// </code>
            /// 
            /// Practical Application: Material BalanceWe combine these PVT parameters to calculate the Original Oil In Place (OOIP):
            /// <math>
            ///     N = \frac{N_p B_o + (G_p - N_p R_s) B_g}{ B_o - B_{ oi} }
            /// </math>
            /// 
            /// Bubble Point and Dew Point Pressures are critical for determining the phase behavior of reservoir fluids. The bubble point is the pressure at which gas begins to come out of solution in oil, while the dew point is the pressure at which liquid begins to condense from gas. 
            /// These pressures can be estimated using correlations or laboratory PVT analysis.
            /// <code>
            {
                double BubblePointPressure(double[] Z, double[] Tc, double[] Tb, double[] Pc, double[] W, double T, double Psc)
                {
                    double[] TcInv = [.. Tc.Select(t => 1 / t)], TbInv = [.. Tb.Select(t => 1 / t)],
                        aPoly = [15e-8, 4.5e-4, 1.2], bPoly = [-3.5e-8, -1.7e-4, 0.89];
                    double Tinv = 1 / T;
                    double PbFunc(double Pb)
                    {
                        double a = Polyval(aPoly, Pb), b = Polyval(bPoly, Pb), s = 0, F, K;
                        for (int i = 0; i < 3; i++)
                        {
                            F = (TbInv[i] - Tinv) / (TbInv[i] - TcInv[i]) * Log10(Pc[i] / Psc);
                            K = Pow(10, a + b * F) / Pb; s += Z[i] * K;
                        }
                        return s - 1;
                    }
                    double Pb0 = 0;
                    for (int i = 0; i < 3; i++)
                        Pb0 += Z[i] * Pc[i] * Exp(5.371 * (1 + W[i]) * (1 - Tc[i] / T));
                    double Pb = Fsolve(PbFunc, Pb0);
                    return Pb;
                }

                double DewPointPressure(double[] Z, double[] Tc, double[] Tb, double[] Pc, double[] W, double T, double Psc)
                {
                    double[] TcInv = [.. Tc.Select(t => 1 / t)], TbInv = [.. Tb.Select(t => 1 / t)],
                        aPoly = [15e-8, 4.5e-4, 1.2], bPoly = [-3.5e-8, -1.7e-4, 0.89];
                    double Tinv = 1 / T;
                    double PdFunc(double Pd)
                    {
                        double a = Polyval(aPoly, Pd), b = Polyval(bPoly, Pd), s = 0, F, K;
                        for (int i = 0; i < 3; i++)
                        {
                            F = (TbInv[i] - Tinv) / (TbInv[i] - TcInv[i]) * Log10(Pc[i] / Psc);
                            K = Pow(10, a + b * F) / Pd; s += Z[i] / K;
                        }
                        return s - 1;
                    }
                    double Pd0 = 0;
                    for (int i = 0; i < 3; i++)
                        Pd0 += Z[i] / (Pc[i] * Exp(5.371 * (1 + W[i]) * (1 - Tc[i] / T)));
                    double Pd = Fsolve(PdFunc, Pd0);
                    return Pd;
                }

                string[] Components = ["C3", "C4", "C5"];
                double[] Z = [0.6, 0.3, 0.1], Mw = [44.09, 58.12, 72.15];
                double[] Tc = [665.7, 765.3, 845.4], TcInv = [.. Tc.Select(t => 1 / t)];
                double[] Tb = [416, 490.8, 556.6], TbInv = [.. Tb.Select(t => 1 / t)];
                double[] Pc = [616.3, 550.7, 488.6], W = [0.1454, 0.1928, 0.2510];
                double T = 760, Tinv = 1 / T, Psc = 14.7, Pb, Pd;
                List<double> Plist = [], Tlist = [];
                Pb = BubblePointPressure(Z, Tc, Tb, Pc, W, T, Psc);
                Pd = DewPointPressure(Z, Tc, Tb, Pc, W, T, Psc);
                Console.WriteLine($"""
                    T = {T:0.00} K, 
                    Bubble Point Pressure = {Pb:0.00} psia, 
                    Dew Point Pressure = {Pd:0.00} psia
                    """);
            }
            /// </code>
            /// </BookContent>
        }
    }
}
