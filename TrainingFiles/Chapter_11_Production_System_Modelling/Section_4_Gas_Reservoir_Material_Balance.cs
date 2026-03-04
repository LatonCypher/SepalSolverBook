using CSharpMath.Atom.Atoms;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.PathStrategies;
using ScottPlot.TickGenerators.TimeUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_4_Gas_Reservoir_Material_Balance
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// **Definition:**
            /// The Gas Material Balance equation (GMBE) is based on the principle of
            /// conservation of mass. For a volumetric gas reservoir (no water drive),
            /// the relationship between reservoir pressure and cumulative production
            /// is linear when expressed as :math:`p/z` vs. :math:`G_p`.

            /// <header 3> The p/z Equation </header>
            /// For a volumetric reservoir, the relationship is defined as:
            /// <math>
            ///     \frac{p}{z} = \frac{p_i}{z_i} \left( 1 - \frac{G_p}{G} \right)
            /// </math>
            /// Where:
            ///
            /// - :math:`p` = current average reservoir pressure (psia)
            ///
            /// - :math:`z` = gas deviation factor at pressure :math:`p`
            ///
            /// - :math:`p_i, z_i` = initial reservoir pressure and gas deviation factor
            ///
            /// - :math:`G_p` = cumulative gas production (Bscf)
            ///
            /// - :math:`G` = Original Gas-In-Place (OGIP) (Bscf)
            ///
            /// **Numerical Example:**
            ///
            /// Given:
            ///
            /// - :math:`p_i = 4000 \, \text{psia}, z_i = 0.91`
            ///
            /// - :math:`G = 100 \, \text{Bscf}`
            ///
            /// - Current :math:`G_p = 20 \, \text{Bscf}`
            ///
            /// - Current :math:`z = 0.88`
            ///
            /// <math>
            ///     \frac{p}{0.88} = \frac{4000}{0.91} \left( 1 - \frac{20}{100} \right) \
            ///     \frac{p}{0.88} = 4395.6 \cdot 0.8 = 3516.5 \
            ///     p = 3516.5 \cdot 0.88 = 3094.5 , \text{psia}
            /// </math>
            ///
            /// <code>
            {
                double G = 100; // Bscf (OGIP)
                double Gp = 20; // Bscf (Produced)
                double pi = 4000; // psia
                double zi = 0.91;
                double z_current = 0.88;
                double p_over_z = (pi / zi) * (1 - (Gp / G));
                double p_current = p_over_z * z_current;
                Console.WriteLine($"Current Reservoir Pressure (p) = {p_current:F2} psia");
            }
            /// </code>
            ///
            /// <header 3> Material Balance with Water Drive </header>
            /// If an active aquifer is present, water influx (:math:`W_e`) maintains reservoir
            /// pressure, causing the :math:`p/z` plot to deviate from a straight line.
            ///
            /// **General Equation:**
            /// <math>
            ///     G_p B_g + W_p B_w = G(B_g - B_{gi}) + W_e + B_{gi} \frac{c_w W + c_f V_p}{1 - S_{wi}} \Delta p
            /// </math>
            ///
            /// **Numerical Example (Solving for OGIP with Water Influx):**
            ///
            /// <code>
            {
                double Gp = 15.0; // Bscf
                double Bg = 0.00085; // res ft3/scf (Current)
                double Bgi = 0.00072; // res ft3/scf (Initial)
                double We = 2.5e6; // res ft3 (Water influx)
                // G = (Gp * Bg - We) / (Bg - Bgi)
                double G_scf = (Gp * 1e9 * Bg - We) / (Bg - Bgi);
                double G_Bscf = G_scf / 1e9;
                Console.WriteLine($"Calculated OGIP (G) = {G_Bscf:F2} Bscf");

            }
            /// </code>
            ///
            /// <header 3> Drive Mechanisms and p/z Signatures </header>
            /// The shape of the :math:`p/z` curve is a diagnostic tool for identifying reservoir behavior:
            /// <table>
            /// Curve Shape | Drive Mechanism | Interpretation 
            /// **Straight Line** | Volumetric | No water influx; depletion drive only. 
            /// **Concave Up** | Water Drive | Aquifer is providing pressure support. 
            /// **Concave Down** | Geopressured | Rock/water expansion significant at high :math:`P`. |
            /// </table>
            /// 
            /// <header 3> Advanced Problem </header>
            /// Given the following data
            /// <code>
            {
                double gas_g = 0.8;
                double res_T = 550; //Rankine
                double[] P = [300, 600, 900,  1200,  1500, 1800, 2100, 2400, 2700]; //psia
                double[] G = [52.86, 49.76, 45.69, 40.43, 33.95, 26.60, 19.05, 11.94, 5.58];
            }
            /// 
            /// </code>
            /// 1. Use Sutton correlation to compute, PseudoCritical Temperature and Pressure
            /// 2. Implement a function to compute Z factor based on Hall and Yaborough or Dranchuk Abou Kassem
            /// 3. Compute p/z
            /// 4. Determine of it is linear. 
            /// 5. Classify the drive mechanism
            /// 
            /// </BookContent>


        }
    }
}
