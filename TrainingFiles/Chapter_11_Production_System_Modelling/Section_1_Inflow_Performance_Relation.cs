namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_1_Inflow_Performance_Relation
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
            /// 
            /// - :math:`J` = productivity index (STB/day/psi)
            /// 
            /// - :math:`p_r` = average reservoir pressure (psi)
            /// 
            /// - :math:`p_{wf}` = bottom-hole flowing pressure (psi)
            /// 
            /// **Numerical Example:**
            /// Given:
            /// - :math:`J = 2 \, \text{STB/day/psi}`
            /// 
            /// - :math:`p_r = 3000 \, \text{psi}`
            /// 
            /// - :math:`p_{wf} = 2500 \, \text{psi}`
            /// 
            /// <math>
            ///     q = 2 \cdot (3000 - 2500) = 1000 \, \text{STB/day}
            /// </math>
            /// 
            /// <code>
            {
                double J = 2; // STB/day/psi    
                double p_r = 3000; // psi
                double p_wf = 2500; // psi
                double q = J * (p_r - p_wf); // STB/day
                Console.WriteLine($"Production Rate (q) = {q} STB/day");
            }
            /// </code>
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
            /// -:math:`q_{max}` = maximum flow rate at :math:`p_{wf} = 0`
            /// 
            /// **Numerical Example:**
            /// Given:
            /// \frac{q}{q_{max}} = 1 - 0.2 \cdot \frac{p_{wf}}{p_r} - 0.8 \cdot \left(\frac{p_{wf}}{p_r}\right)^2
            /// 
            /// - :math:`q_{max} = 2000 \, \text{STB/day}`
            /// - :math:`p_r = 2500 \, \text{psi}`
            /// - :math:`p_{wf} = 1000 \, \text{psi}`
            /// 
            /// <math>
            ///      \frac{q}{2000} = 1 - 0.2 \cdot \frac{1000}{2500} - 0.8 \cdot \left(\frac{1000}{2500}\right)^2\\
            ///      \frac{q}{2000} = 1 - 0.08 - 0.128 = 0.792\\
            ///      q = 2000 \cdot 0.792 = 1584 \, \text{STB/day}
            /// </math>
            /// 
            /// <code>
            {
                double q_max = 2000; // STB/day
                double p_r = 2500; // psi
                double p_wf = 1000; // psi
                double q = q_max * (1 - 0.2 * (p_wf / p_r) - 0.8 * Pow(p_wf / p_r, 2)); // STB/day
                Console.WriteLine($"Production Rate (q) = {q} STB/day");
            }
            /// </code>
            /// 
            /// <header 3> Flow Efficiency and Skin </header>
            /// **Flow Efficiency (FE):**
            /// Flow efficiency is a measure of how effectively a well produces compared to an 
            /// ideal, undamaged well. It is defined as:
            /// <math>
            ///      FE = \frac{q_{actual}}{q_{ideal}}
            /// </math>
            /// 
            {
                double q_ideal = 1584; // STB/day (from previous example)
                double q_act = 1200; // STB/day (maximum flow rate)
                double FE = q_act / q_ideal; // Flow Efficiency
                Console.WriteLine($"Flow Efficiency (FE) = {FE:P2}");
            }
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
            /// - :math:`s = +3`

            /// Case 1: **Above Bubble Point** (:math:`p_{wf} = 2800 \, \text{psi}`)
            /// <math>
            ///      J_s = \frac{2}{1 + \frac{3}{\ln(1000)}} \approx \frac{2}{1 + 0.4343} = 1.3944
            ///      q = 1.3944 \cdot(3000 - 2800) = 278.9 \, \text{ STB/day}
            /// </math>
            /// 
            /// <code>
            {
                double q_max = 2000; // STB/day
                double p_r = 3000; // psi
                double p_wf = 2800; // psi
                double J = 2; // STB/day/psi
                double r_e_r_w = 1000; // dimensionless
                double s = 3; // dimensionless
                double J_s = J / (1 + s / Log(r_e_r_w)); // STB/day/psi
                double q = J_s * (p_r - p_wf); // STB/day
                Console.WriteLine($"Adjusted Productivity Index (J_s) = {J_s:F4} STB/day/psi");
            }
            /// </code>
            /// 
            /// 
            /// Case 2: **Below Bubble Point** (:math:`p_{wf} = 2000 \, \text{psi}`)
            /// <math>
            ///      \frac{q}{2000} = 1 - 0.2 \cdot \frac{2000}{3000} - 0.8 \cdot \left(\frac{2000}{3000}\right)^2
            ///      \frac{q}{ 2000} = 1 - 0.133 - 0.356 = 0.511
            ///      q = 2000 \cdot 0.511 = 1022 \, \text{ STB/day}
            /// </math>
            /// 
            /// Adjusted for skin: q_actual = 1022 \cdot \frac{J_s}{J} = 1022 \cdot \frac{1.3944}{2} = 712.5 \, \text{STB/day}
            /// 
            /// <code>
            {
                double q_max = 2000; // STB/day
                double p_wf = 1000; // psi
                double p_r = 2500; // psi
                double J = 2;
                double r_e_r_w = 1000;
                double s = 3;
                double J_s = J / (1 + s / Log(r_e_r_w)); // STB/day/psi
                double q_ideal = q_max * (1 - 0.2 * (p_wf / p_r) - 0.8 * Pow(p_wf / p_r, 2)); // STB/day
            }
            /// </code>
            /// 
            /// Case 3: **At Zero Bottom-Hole Pressure** (:math:`p_{wf} = 0`)
            /// <math>
            ///      q = q_{max} = 2000 \, \text{STB/day}
            /// </math>
            /// Adjusted for skin: q_actual = 2000 \cdot \frac{1.3944}{2} = 1394.4 \, \text{STB/day}
            /// 
            /// 
            /// </BookContent>
        }
    }
}
