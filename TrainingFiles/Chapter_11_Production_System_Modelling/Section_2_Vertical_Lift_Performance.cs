using ScottPlot.ArrowShapes;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_2_Vertical_Lift_Performance
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// **Definition:**
            /// The Vertical Lift Performance (VLP) describes the relationship between the bottom-hole flowing pressure (:math:`p_{wf}`) and the production rate (:math:`q`). It represents the pressure required to lift fluids from the bottom-hole to the surface against gravity, friction, and acceleration.
            /// 
            /// <header 3> Oil Well VLP (Single Phase) </header>
            /// For a single-phase liquid, the pressure gradient (:math:`dp/dz`) is the sum of hydrostatic (elevation) and frictional components.
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
                var (Z, P) = Ode45((z, p)=>pressureGradient(z, p, q_o), p_surf, [0, depth]);
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
            /// 
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
