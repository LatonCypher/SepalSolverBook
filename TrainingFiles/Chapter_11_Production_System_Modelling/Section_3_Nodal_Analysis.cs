
using FFmpeg.AutoGen;
using System.Drawing;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_3_Nodal_Analysis
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// Definition:
            /// Nodal Analysis is a method used to evaluate a complete producing system by
            /// isolating a single point (the Node) and ensuring the pressure and flow rate
            /// are consistent across that point. In petroleum production, the most common
            /// node is the Bottom-hole, where the Inflow (IPR) meets the Outflow (VLP).

            /// <header 3> The Node Concept </header>
            /// For any given node, two conditions must be met:
            ///
            /// 1. Flow into the node equals flow out of the node.
            ///
            /// 2. Only one pressure can exist at the node at a given flow rate.
            ///
            /// The Node Equations:
            ///
            /// - Inflow (Supply): :math:`p_{node} = p_r - \Delta p_{reservoir}`
            ///
            /// - Outflow (Demand): :math:`p_{node} = p_{surf} + \Delta p_{tubing} + \Delta p_{choke}`
            ///
            /// <header 3> Determining the Operating Point </header>
            /// The intersection of the IPR curve and the VLP curve represents the
            /// Operating Point. This is the only rate (:math:q_{actual}) at which the
            /// well will naturally flow for a given set of conditions.

            /// Numerical Example:
            ///
            /// Consider a well with:
            ///
            /// - Reservoir Pressure (:math:`p_r`) = 3500 psi
            ///
            /// - Productivity Index (:math:`J`) = 1.2 STB/day/psi
            ///
            /// - Surface Pressure (:math:`p_{surf}`) = 250 psi
            ///
            /// - VLP is simplified as: :math:`p_{wf} = p_{surf} + 0.001 \cdot q^{1.5} + 0.4 \cdot \text{Depth}/144`
            ///
            /// To find the operating point, we solve for :math:`q where :math:p_{wf, IPR} = p_{wf, VLP}`.
            ///
            /// <code>
            {
                //IPR Input
                double q_max = 2000; // STB/day
                double p_r = 2500; // psi
                double p_wf = 1000; // psi

                //IPR
                double qfun(double pwf) => q_max * (1 - 0.2 * (pwf / p_r) - 0.8 * Pow(pwf / p_r, 2));
                ColVec P_ipr = Linspace(0, p_r);
                ColVec Q_ipr = Arrayfun(qfun, P_ipr);

                // VLP Inputs
                double p_surf = 200; // psi (Wellhead Pressure)
                double depth = 2000; // ft
                                  

                // VLP
                double pressureGradient(double z, double p, double q)
                {
                    double density = 55.0; // lb/ft3 (Oil)
                    double friction_grad = 0.00002 * Pow(q, 1.8); // Simplified friction term
                    double hydro_grad = density / 144.0; // psi/ft
                    return hydro_grad + friction_grad;
                }
                double pfun(double q_g)
                {
                    var (Z, P) = Ode45((z, p) => pressureGradient(z, p, q_g), p_surf, [0, depth]);
                    double p_wf = P[^1]; // extract the pressure at the bottom
                    return p_wf;
                }

                ColVec Q_vlp = Linspace(0, 800);
                ColVec P_vlp = Arrayfun(pfun, Q_vlp);

                (double operating_q, double operating_p) = Intersection(Q_vlp, P_vlp, Q_ipr, P_ipr);
                // Assuming a simple bisection or search between 0 and AOF
                Scatter(operating_q, operating_p, "for", 15); HoldOn();
                Plot(Q_ipr, P_ipr, "b");
                Plot(Q_vlp, P_vlp, "g"); HoldOff();
                Legend(["Operating Condition", "IPR", "VLP"]);
                SaveAs("Nodal_Analysis.png");
            }
            /// </code>
            ///
            /// <header 3> Sensitivity Analysis </header>
            /// Nodal analysis is most powerful when performing "What-If" scenarios. By
            /// shifting the curves, engineers can predict the impact of changes:
            ///<table>
            /// Change | Curve Affected | Result on Operating Point 
            /// Increase Reservoir Pressure | IPR(Shifts Up) | Increase in :math:`q` and :math:`p_{ wf}`. 
            /// Wellbore Stimulation(Skin < 0) | IPR(Gets Steeper) | Increase in :math: q. 
            /// Increase Tubing Diameter | VLP(Shifts Down) | Increase in :math:`q`, decrease in :math:`p_{ wf}`. 
            /// Increase Water Cut | VLP(Shifts Up) | Decrease in :math:`q`(due to heavier fluid). |
            /// </table>
            /// 
            /// </BookContent>


        }
    }
}
