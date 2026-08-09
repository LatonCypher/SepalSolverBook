using ScottPlot.ArrowShapes;
using ScottPlot.AxisRules;
using ScottPlot.Colormaps;
using ScottPlot.Interactivity.UserActionResponses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_3_Vertical_Lift_Performance
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// **Definition:**
            /// The Vertical Lift Performance (VLP) describes the relationship between the bottom-hole flowing pressure (:math:`p_{wf}`) and the production rate (:math:`q`). It represents the pressure required to lift fluids from the bottom-hole to the surface against gravity, friction, and acceleration.
            /// 
            /// <header 3> Oil Well VLP (Single Phase) </header>
            /// For a single-phase liquid, the pressure gradient (:math:`dp/dz`) is the sum of hydrostatic (elevation), acceleration and frictional components.
            /// 
            /// **Differential Equation:**
            /// <math>
            ///     \frac{dp}{dz} = \rho g \sin(\theta) + \frac{2f \rho v^2}{D}
            /// </math>
            /// Where:
            ///     - :math:`\rho` = fluid density (:math:`lb/ft^3`)
            ///     - :math:`g` = gravitational constant
            ///     - :math:`f` = Fanning friction factor
            ///     - :math:`v` = fluid velocity (:math:`ft/s`)
            ///     - :math:`D` = tubing internal diameter (:math:`ft`)
            ///     
            /// Numerical Example (ODEs with SepalSolver): We solve for :math:`p_{wf}` by integrating from surface pressure (:math:`p_{surf}`) to the
            /// total depth (:math:`H`).
            /// 
            /// <code>
            {
                // Inputs
                double p_surf = 200; // psi (Wellhead Pressure)
                double depth = 2000; // ft
                double q_o = 500; // STB/day
                                 // ODE Definition: dp/dz = gradient
                double pressureGradient(double z, double p, double q)
                {
                    double density = 55.0; // lb/ft3 (Oil)
                    double friction_grad = 0.00002 * Pow(q, 1.8); // Simplified friction term
                    double hydro_grad = density / 144.0; // psi/ft
                    return hydro_grad + friction_grad;
                }
                // Solve using SepalSolver Ode45
                // Integrate from z=0 (surface) to z=8000 (bottom-hole)
                var (Z, P) = Ode45((z, p) => pressureGradient(z, p, q_o), p_surf, [0, depth]);
                double p_wf = P[^1]; // extract the pressure at the bottom
                Console.WriteLine($"Bottom-hole Flowing Pressure (Pwf) = {p_wf:F2} psi");



                //FullRange
                double pfun(double q_g)
                {
                    var (Z, P) = Ode45((z, p) => pressureGradient(z, p, q_g), p_surf, [0, depth]);
                    double p_wf = P[^1]; // extract the pressure at the bottom
                    return p_wf;
                }
                ColVec Qrange = Linspace(0, 800);
                ColVec Prange = Arrayfun(pfun, Qrange);
                Plot(Qrange, Prange, "b", 2);
                Xlabel("Flowrate Q (STB/day)");
                Ylabel("Pressure P (psia)");
                Title("OilVLP");
                SaveAs("OilVLP.png");
            }
            /// </code>
            /// A comprehensive Vertical Lift Performance (VLP) model would account hydrostatic (elevation), 
            /// frictional, and kinetic (acceleration) pressure gradient components. 
            /// 
            /// For Multiphase flow, it would be necessary to consider the mixture density, two-phase friction factor, 
            /// and no-slip density. The governing equation for multiphase flow is:
            /// 
            /// <math>
            /// \cfrac{dP}{dz} = \cfrac{\rho_m g \sin(\theta)}{144} + \cfrac{2 f_{tp} \rho_{ns} v_m^2}{g D} + \cfrac{\rho_m v_m}{g} \cfrac{dv_m}{dz}
            /// </math>
            /// 
            /// :math:`\rho_m` = mixture density, :math:`f_{tp}` = two-phase friction factor, :math:`\rho_{ns}` = no-slip density, :math:`v_m` = mixture velocity, :math:`D` = tubing internal diameter, :math:`g` = gravitational constant.
            /// 
            /// Multiphase hydraulics are evaluated using the Beggs & Brill correlation, requiring 
            /// fluid PVT properties including gas solubility (:math:`R_s`), oil formation volume 
            /// factor (:math:`B_o`), and phase viscosities. Gas compressibility (:math:`Z`) is 
            /// evaluated using the Kareem et al. (2016) explicit correlation.
            /// <code>
            {
                // Constants from Table 3 for Kareem et al. (2016) Correlation 
                const double a1 = 0.317842, a2 = 0.382216, a3 = -7.768354, a4 = 14.290531;
                const double a5 = 0.000002, a6 = -0.004693, a7 = 0.096254, a8 = 0.166720;
                const double a9 = 0.966910, a10 = 0.063069, a11 = -1.966847, a12 = 21.0581;
                const double a13 = -27.0246, a14 = 16.23, a15 = 207.783, a16 = -488.161;
                const double a17 = 176.29, a18 = 1.88453, a19 = 3.05921;
                double[] poly1 = [a6, a7, a8, a9];
                double[] poly2 = [a14, a13, a12];
                double[] poly3 = [a17, a16, a15];
                double[] poly4 = [0.01853, -0.8725, 3.182, -0.0523];
                // Standing Correlations (Local Functions)
                double _api_to_sg(double api) => 141.5 / (api + 131.5);
                double _standing_ppc(double gas_sg) => 677.0 + 15.0 * gas_sg - 37.5 * gas_sg * gas_sg;
                double _standing_tpc(double gas_sg) => 168.0 + 325.0 * gas_sg - 12.5 * gas_sg * gas_sg;
                double _kareem_z(double p, double T, double gas_sg)
                {
                    double T_R = T + 460.0, Ppr = p / _standing_ppc(gas_sg), Tpr = T_R / _standing_tpc(gas_sg);
                    double t = 1.0 / Tpr, dt = 1.0 - t, dt2 = dt * dt, tPpr = t * Ppr;

                    // Intermediate variables (A through G)
                    double A = a1 * t * Exp(a2 * dt2) * Ppr,
                        B = a3 * t + a4 * t * t + a5 * Pow(t, 6) * Pow(Ppr, 6),
                        C = Polyval(poly1, tPpr), D = a10 * t * Exp(a11 * dt2),
                        E = t * Polyval(poly2, t), F = t * Polyval(poly3, t), G = a18 + a19 * t;

                    // Equation 15: Reduced density y
                    double A2 = A * A, C2 = C * C, C3 = C2 * C,
                        denom_y = (1.0 + A2) / C - (A2 * B) / C3,
                        y = (D * Ppr) / denom_y;

                    // Equation 14: Compressibility factor z
                    double y2 = y * y, y3 = y2 * y, num_z = D * Ppr * (1.0 + y + y2 - y3);
                    double denom_z = (D * Ppr + E * y2 - F * Pow(y, G)) * Pow(1.0 - y, 3);

                    return num_z / denom_z;
                }
                double _standing_rs(double p, double T, double gas_sg, double API)
                {
                    var term = (p / 18.2 + 1.4) * Pow(10, 0.0125 * API - 0.00091 * T);
                    return gas_sg * Pow(Max(term, 0.0), 1.2048);
                }
                double _standing_bo(double rs, double gas_sg, double T, double API)
                {
                    var oil_sg = _api_to_sg(API);
                    var F = rs * Pow(gas_sg / oil_sg, 0.5) + 1.25 * T;
                    return 0.972 + 0.000147 * Pow(F, 1.175);
                }
                double _standing_rho_o(double rs, double gas_sg, double T, double API)
                {
                    var bo = _standing_bo(rs, gas_sg, T, API);
                    var mass_per_stb = _api_to_sg(API) * 62.4 + 0.0136 * rs * gas_sg;
                    return mass_per_stb / (5.615 * Max(bo, 1e-9)); // In-situ density in lb/ft^3
                }
                double _beggs_robinson_mu_o(double rs, double gas_sg, double T, double API)
                {
                    var x = Pow(T, -1.163) * Pow(10, 3.0324 - 0.02023 * API);
                    var mu_od = Pow(10, x) - 1.0;
                    var a = 10.715 * Pow(rs + 100.0, -0.515);
                    var b = 5.44 * Pow(rs + 100.0, -0.338);
                    return a * Pow(Max(mu_od, 1e-4), b);
                }
                double _lee_gonzalez_eakin_mu_g(double p, double T, double gas_sg, double z)
                {
                    var T_R = T + 460.0;
                    var M = 28.97 * gas_sg;
                    var rho_g = (p * M) / (z * 10.732 * T_R * 62.4); // Gas density in g/cm^3
                    var K = (9.4 + 0.02 * M) * Pow(T_R, 1.5) / (209.0 + 19.0 * M + T_R);
                    var X = 3.5 + 986.0 / T_R + 0.01 * M;
                    var Y = 2.4 - 0.2 * X;
                    return 1e-4 * K * Exp(X * Pow(rho_g, Y));
                }
                double pressureGradient(double P, double T, double qliq, double wc, double gor, double API, double gas_sg, double d_in, double theta)
                {
                    var G = 32.174; // ft/s^2    
                    var oil_sg = _api_to_sg(API);
                    var d_ft = d_in / 12.0; // Convert diameter to feet
                    var rs = _standing_rs(P, T, gas_sg, API);
                    var z = _kareem_z(P, T, gas_sg);
                    var bg = 0.02827 * z * (T + 460) / P; // Gas formation volume factor
                    var bo = _standing_bo(rs, gas_sg, T, API);
                    var mu_O = _beggs_robinson_mu_o(rs, gas_sg, T, API);
                    var mu_G = _lee_gonzalez_eakin_mu_g(P, T, gas_sg, z);
                    var qoil = qliq * (1 - wc); // STB/day
                    var qwater = qliq * wc; // STB/day
                    var qgas = qoil * gor / 1000.0; // Mscf/day

                    var qL = (qoil * bo + qwater) * 5.615 / 86400.0; // ft^3/s
                    var qG = Max(qoil * (gor - rs), 0) * bg / 86400.0; // ft^3/s
                    var A = 0.25 * pi * d_ft * d_ft; // Cross-sectional area

                    (var vsl, var vsg) = (qL / A, qG / A); // Superficial velocities
                    var vm = Max(0, vsl + vsg); // Mixture velocity
                    var lam = Clamp(vsl / Max(vm, 1e-9), 1e-9, 1.0); // No-slip liquid holdup

                    var rho_O = _standing_rho_o(rs, gas_sg, T, API);
                    var rho_G = 2.7 * (P * gas_sg) / (z * (T + 460.0)); // Gas density
                    var rho_L = (qoil * rho_O + qwater * 62.4) / Max(qliq, 1e-9); // Liquid density

                    // Beggs & Brill Transition Boundaries
                    var NFR = Pow(vm, 2) / (G * d_ft); // Froude Number
                    var L1 = 316.0 * Pow(lam, 0.302);
                    var L2 = 0.0009252 * Pow(lam, -2.4684);
                    var L3 = 0.1 * Pow(lam, -1.4516);
                    var L4 = 0.5 * Pow(lam, -6.738);

                    // Flow Pattern Determination
                    string pattern = (lam, NFR) switch
                    {
                        var (l, n) when (l < 0.01 && n < L1) || (l >= 0.01 && n < L2) => "segregated",
                        var (l, n) when l < 0.1 && n >= L2 && n <= L3 => "transient",
                        var (l, n) when (l is >= 0.01 and <= 0.4 && n > L3 && n < L1)
                                     || (l > 0.4 && n > L3 && n < L4) => "intermittent",
                        _ => "distributed"
                    };

                    // Horizontal Holdup HL(0)
                    double h10(string pat)
                    {
                        var (a, b, c) = pat switch
                        {
                            "segregated" => (0.98, 0.4846, 0.0868),
                            "intermittent" => (0.845, 0.5351, 0.0173),
                            "distributed" => (1.065, 0.5824, 0.0609),
                            _ => (0.0, 0.0, 0.0)
                        };
                        return Max(a * Pow(lam, b) / Pow(NFR, c), lam);
                    }

                    // Inclination Correction Coefficient C
                    var sigma = 30.0; // Liquid surface tension (dyne/cm)
                    var NLv = rho_L > 0 ? vsl * Pow(rho_L / (G * sigma), 0.25) : 0;

                    double cc(string pat)
                    {
                        if (pat == "distributed") return 0.0;
                        var (e, f, gg, h) = pat switch
                        {
                            "segregated" => (0.11, -3.768, 3.539, -1.614),
                            "intermittent" => (2.96, 0.305, -0.4473, 0.0978),
                            _ => (0.0, 0.0, 0.0, 0.0)
                        };
                        return Max((1.0 - lam) * Log(e * Pow(lam, f) * Pow(Max(NLv, 1e-9), gg) * Pow(NFR, h)), 0.0);
                    }

                    double psi(string pat)
                    {
                        var theta_rad = theta * pi / 180.0;
                        var s = Sin(1.8 * theta_rad);
                        return 1.0 + cc(pat) * (s - 0.333 * Pow(s, 3));
                    }

                    // Liquid Holdup Calculation
                    var HL = h10(pattern) * psi(pattern);
                    if (pattern == "transient")
                    {
                        var w = (L3 - NFR) / (L3 - L2);
                        var Hlseg = h10("segregated") * psi("segregated");
                        var Hlint = h10("intermittent") * psi("intermittent");
                        HL = w * Hlseg + (1.0 - w) * Hlint;
                    }
                    HL = Clamp(HL, lam, 1.0);

                    // Densities & Viscosities
                    var rhom = rho_L * HL + (1.0 - HL) * rho_G; // Mixture density
                    var rhons = rho_L * lam + rho_G * (1.0 - lam); // No-slip density
                    var mu_W = 1.0; // Water viscosity (cP)
                    var mu_L = mu_O * (1.0 - wc) + mu_W * wc; // Liquid phase mixture viscosity
                    var muns = mu_L * lam + mu_G * (1.0 - lam); // No-slip viscosity

                    // Hydrostatic / Elevation Gradient
                    var dpdz_elevation = rhom * Sin(theta * pi / 180.0) / 144.0;

                    // Friction Pressure Gradient
                    var Re = rhons * vm * d_ft / (muns * 6.7197e-4); // Reynolds number
                    var fns = Re > 0 ? 0.0056 + 0.5 / Pow(Re, 0.32) : 0.02; // No-slip friction factor

                    var y = lam / Pow(Max(HL, 1e-9), 2);
                    double S = Log(2.2 * y - 1.2);
                    if (y < 1.0 || y > 1.2)
                    {
                        var ly = Log(Max(y, 1e-9));
                        var denom = Polyval(poly4, ly);
                        S = Abs(denom) > 1e-9 ? ly / denom : 0.0;
                    }

                    var ftp = fns * Exp(S); // Two-phase friction factor
                    var dpdz_friction = 2.0 * ftp * rhons * Pow(vm, 2) / (G * d_ft) / 144.0; // Friction gradient

                    // Kinetic Energy Acceleration Term (Ek)
                    var Ek = (rhom * vm * vsg) / (G * P * 144.0);

                    return (dpdz_elevation + dpdz_friction) / Max(1.0 - Ek, 0.001);
                }

                // Well Input Parameters
                double Pwh = 200;           // psia
                double depth = 9200;        // ft
                double ID = 2.441;          // in
                double API = 35;            // API gravity
                double Tsurf = 100;         // °F
                double Tdownhole = 210;     // °F
                double Tgrad = (Tdownhole - Tsurf) / depth; // °F/ft
                double watercut = 0.1;      // fraction
                double gor = 250;           // scf/STB
                double gas_sg = 0.65;       // gas specific gravity

                double pfun(double qliq)
                {
                    var (Z, P) = Ode45((z, p) => pressureGradient(p, Tsurf + Tgrad * z,
                        qliq, watercut, gor, API, gas_sg, ID, 90), Pwh, [0, depth]);
                    return P[^1]; // Bottomhole pressure
                }

                ColVec Qrange = Linspace(100, 6000, 20),
                       Prange = Arrayfun(pfun, Qrange);

                Plot(Qrange, Prange, "b", 2);
                Xlabel("Flowrate Q (STB/day)");
                Ylabel("Pressure P (psia)");
                Title("Oil VLP Curve");
                SaveAs("Multiphase_VLP.png");
            }
            /// </code>
            ///  <header 3> Gas Well VLP </header>
            ///  Gas VLP is more complex because gas density is highly dependent on pressure. As gas rises, it expands, increasing velocity and frictional losses.
            ///  
            /// Differential Equation:
            /// <math>
            ///     \frac{dp}{dz} = \frac{p M}{z R T} g \sin(\theta) + \frac{2f \rho v^2}{D}
            /// </math>
            /// 
            /// Numerical Example (ODEs with SepalSolver):
            /// In this example, the gradient function must recalculate gas density (:math:`\rho_g = \frac{p M}{Z R T}`) at every step of the integration.
            /// 
            /// <code>
            {
                double p_surf = 500; // psia
                double depth = 10000; // ft
                double q_g = 5000; // Mscf/day
                // ODE Definition for Gas
                double pressureGradient(double z, double p, double q)
                {
                    double MW = 20.0; // Gas molecular weight
                    double T = 540 + (0.015 * z); // Temp profile in Rankine
                    double Z = 0.85; // Average Z-factor
                    double R = 10.73;

                    // Density as a function of current Pressure (p)
                    double rho_g = (p * MW) / (Z * R * T);

                    double hydro_grad = rho_g / 144.0;
                    double friction_grad = 1.5e-9 * (Pow(q, 2) / p); // Simplified gas friction
                    return hydro_grad + friction_grad;
                }
                // Solve using SepalSolver Ode45
                // Integrate from z=0 (surface) to z=8000 (bottom-hole)
                var (Z, P) = Ode45((z, p)=>pressureGradient(z, p, q_g), p_surf, [0, depth]);
                double p_wf = P[^1]; // extract the pressure at the bottom
                Console.WriteLine($"Bottom-hole Flowing Pressure (Pwf) = {p_wf:F2} psi");


                //FullRange
                double pfun(double q_g)
                {
                    var (Z, P) = Ode45((z, p) => pressureGradient(z, p, q_g), p_surf, [0, depth]);
                    double p_wf = P[^1]; // extract the pressure at the bottom
                    return p_wf;
                }
                ColVec Qrange = Linspace(0, 8000);
                ColVec Prange = Arrayfun(pfun, Qrange);
                Plot(Qrange, Prange, "b", 2);
                Xlabel("Flowrate Q (Mscf/day)");
                Ylabel("Pressure P (psia)");
                Title("GasVLP");
                SaveAs("GasVLP.png");
            }
            /// </code>
            /// 
            /// </BookContent>
}
    }
}
