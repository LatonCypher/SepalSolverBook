using ScottPlot.TickGenerators.TimeUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_5_Decline_Curve_Analysis
    {
        public static void Run()
        {
            /// <BookContent>
            /// 
            /// <header 2> Inflow Performance Relationship (IPR) </header>
            /// **Definition:**
            /// The Inflow Performance Relationship (IPR) describes the relationship between 
            /// the bottom-hole flowing pressure (p_wf) and the production rate (q) of a well. 
            /// It is a fundamental tool in reservoir engineering used to evaluate well 
            /// productivity and forecast performance under different operating conditions.

            /// <header 3> IPR Above Bubble Point </header>
            /// the reservoir pressure is above the bubble point pressure, the fluid 
            /// remains single-phase (oil only). The relationship is linear and can be 
            /// expressed as:
            /// <math>
            ///     q = J \cdot (p_r - p_{wf})
            /// </math>
            /// Where:
            /// - :math:`q` = production rate (STB/day)
            /// - :math:`J` = productivity index (STB/day/psi)
            /// - :math:`p_r` = average reservoir pressure (psi)
            /// - :math:`p_{wf}` = bottom-hole flowing pressure (psi)
            /// 
            /// **Numerical Example:**
            /// Given:
            /// - :math:`J = 2 \, \text{STB/day/psi}`
            /// - :math:`p_r = 3000 \, \text{psi}`
            /// - :math:`p_{wf} = 2500 \, \text{psi}`
            /// 
            /// <math>
            ///     q = 2 \cdot (3000 - 2500) = 1000 \, \text{STB/day}
            /// </math>
            /// 
            /// <header 3> IPR Below Bubble Point </header>
            /// When the reservoir pressure falls below the bubble point, gas evolves from 
            /// solution, and the relationship becomes non-linear. Vogel’s empirical equation 
            /// is commonly used:
            ///  <math>
            ///     \frac{q}{q_{max}} = 1 - 0.2 \cdot \frac{p_{wf}}{p_r} - 0.8 \cdot \left(\frac{p_{wf}}{p_r}\right)^2
            /// </math>
            /// 
            /// Where:
            /// - :math:`q_{max}` = maximum flow rate at :math:`p_{wf} = 0`
            /// 
            /// **Numerical Example:**
            /// Given:
            /// \frac{q}{q_{max}} = 1 - 0.2 \cdot \frac{p_{wf}}{p_r} - 0.8 \cdot \left(\frac{p_{wf}}{p_r}\right)^2- :math:`q_{max} = 2000 \, \text{STB/day}`
            /// - :math:`p_r = 2500 \, \text{psi}`
            /// - :math:`p_{wf} = 1000 \, \text{psi}`
            /// 
            /// <math>
            ///      \frac{q}{2000} = 1 - 0.2 \cdot \frac{1000}{2500} - 0.8 \cdot \left(\frac{1000}{2500}\right)^2
            ///      \frac{q}{2000} = 1 - 0.2 \cdot \frac{1000}{2500} - 0.8 \cdot \left(\frac{1000}{2500}\right)^2
            ///      \frac{q}{2000} = 1 - 0.08 - 0.128 = 0.792
            ///      q = 2000 \cdot 0.792 = 1584 \, \text{STB/day}
            /// </math>
            /// 
            /// Flow Efficiency and Skin
            /// **Flow Efficiency (FE):**
            /// Flow efficiency is a measure of how effectively a well produces compared to an 
            /// ideal, undamaged well. It is defined as:
            /// <math>
            ///      FE = \frac{q_{actual}}{q_{ideal}}
            /// </math>
            /// 
            /// **Skin Factor (s):**
            /// Skin represents additional pressure drop caused by near-wellbore damage or stimulation. The productivity index with skin is:
            /// <math>
            ///      J_s = \frac{J}{1 + \frac{s}{\ln(r_e/r_w)}}
            /// </math>
            /// 
            /// Where:
            /// - :math:`r_e` = drainage radius
            /// - :math:`r_w` = wellbore radius
            /// - :math:`s` = skin factor
            /// 
            /// A positive skin reduces productivity, while a negative skin (stimulation) increases productivity.
            /// Numerical Example with Pressure Drop
            /// Consider a reservoir with:
            /// - :math:`p_r = 3000 \, \text{psi}`
            /// -Bubble point pressure :math:`p_b = 2500 \, \text{psi}`
            /// - :math:`q_{max} = 2000 \, \text{STB/day}`
            /// - :math:`J = 2 \, \text{STB/day/psi}`
            /// - :math:`r_e/r_w = 1000`
            /// - :math:`s = +5`

            /// Case 1: **Above Bubble Point** (:math:`p_{wf} = 2800 \, \text{psi}`)
            /// <math>
            ///      J_s = \frac{2}{1 + \frac{5}{\ln(1000)}} \approx \frac{2}{1 + 0.724} = 1.16
            ///      q = 1.16 \cdot(3000 - 2800) = 232 \, \text{ STB/day}
            /// </math>
            /// 
            /// Case 2: **Below Bubble Point** (:math:`p_{wf} = 2000 \, \text{psi}`)
            /// <math>
            ///      \frac{q}{2000} = 1 - 0.2 \cdot \frac{2000}{3000} - 0.8 \cdot \left(\frac{2000}{3000}\right)^2
            ///      \frac{ q}{ 2000} = 1 - 0.133 - 0.356 = 0.511
            ///      q = 2000 \cdot 0.511 = 1022 \, \text{ STB/day}
            /// </math>
            /// 
            /// Adjusted for skin: q_actual = 1022 \cdot \frac{J_s}{J} = 1022 \cdot \frac{1.16}{2} = 593 \, \text{STB/day}
            /// 
            /// 
            /// Case 3: **At Zero Bottom-Hole Pressure** (:math:`p_{wf} = 0`)
            /// <math>
            ///      q = q_{max} = 2000 \, \text{STB/day}
            /// </math>
            /// Adjusted for skin: q_actual = 2000 \cdot \frac{1.16}{2} = 1160 \, \text{STB/day}
            /// 
            /// 
            /// </BookContent>
        }
    }
}

