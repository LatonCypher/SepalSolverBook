using Microsoft.VisualBasic;
using ScottPlot.PlotStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CSharpMath.Rendering.Text.TextAtom;
using static SepalSolver.Statistics;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_6_Pressure_Volume_Temperature_PVT
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
            /// The Bubble Point Pressure (:math:`P_b`) The pressure at which the first bubble of gas comes out of solution. Below this pressure, the fluid is "saturated." A common correlation used is Standing’s method.
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
            /// 2. Oil Formation Volume Factor(:math:B_o)Since oil shrinks as gas escapes, :math:B_o is almost always greater than 1.0.For pressures below the bubble point, we use the Standing correlation:
            /// <math>
            ///     B_o = 0.9759 + 0.00012\left[R_s \left( \frac{\gamma_g}{\gamma_o} \right)^{0.5} + 1.25 T \right]^{1.2}
            /// </math>
            /// 
            /// Numerical Example:
            /// 
            /// - :math:`\gamma_o = 0.85`(Typical for 35 API)
            /// Using: math: `R_s`, 
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
            /// 3.Gas Compressibility Factor(:math:`z`)
            /// For gas modeling, the Ideal Gas Law fails at high pressure. We use the $Z$-factor to correct it. The Hall-Yarborough or Dranchuk-Abu-Kassam methods are standard for coding this.
            /// 
            /// Linearization for Gas Density:
            /// <math>
            ///     \rho_g = \frac{P \cdot MW_g}{ Z \cdot R \cdot T}
            /// </math>
            /// 
            /// 4.Isothermal Oil Compressibility(:math: c_o)
            /// Above the bubble point (undersaturated), the oil volume changes only slightly due to pressure.
            /// <math>
            ///     c_o = \frac{-1}{ V} \left( \frac{\partial V}{\partial P} \right)_T
            /// </math>
            /// 
            /// Code Implementation for Undersaturated :math:`B_o`: If: math:`P > P_b`, 
            /// we adjust the :math:`B_{ ob}`(at bubble point) using compressibility:
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
            /// </BookContent>
        }
    }
}
