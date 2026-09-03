using ScottPlot;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_7_Production_Network_Optimization
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// **Definition:**
            /// Production Network Optimization is the mathematical formulation and systematic allocation
            /// of surface and subsurface resources to maximize total hydrocarbon recovery, increase net
            /// operational revenue, and maintain flow assurance across an interconnected gathering network.
            /// As oil and gas assets mature and natural reservoir pressure declines, operators deploy
            /// artificial lift systems (such as continuous gas lift or electrical submersible pumps) to sustain
            /// production. However, wells connected to a common gathering manifold dynamically interact:
            /// strong, high-productivity wells generate significant line backpressure that can suppress or
            /// completely kill weaker, high-water-cut wells.
            ///
            /// A modern gathering network optimizer resolves these coupled interactions by adjusting decision
            /// variables (choke openings, gas lift injection rates, and pump speeds) while strictly enforcing
            /// physical constraints across multiple operational hierarchies.
            ///
            /// <header 3> Multi-Level Constraint Hierarchy </header>
            /// Real-world field management requires satisfying constraints at four distinct levels:
            ///
            /// 1. **Well-Level Constraints:**
            ///    - **Maximum Allowable Drawdown:** :math:`\Delta p = p_r - p_{wf} \le \Delta p_{max}` to prevent
            ///      catastrophic sand influx, mechanical failure of gravel packs, and rapid water or gas coning.
            ///    - **Bubble Point Pressure Limit:** :math:`p_{wf} \ge p_{bubble}` in the reservoir drainage area
            ///      to prevent gas breakout, relative permeability impairment, and near-wellbore two-phase skin.
            ///    - **Artificial Lift Limits:** Bounds on compressor injection rates :math:`0 \le q_{gi} \le q_{gi,max}`.
            ///      Excessive gas injection ("over-injection") increases tubing frictional backpressure, decreasing oil production.
            ///
            /// 2. **Flowline & Flow Assurance Constraints:**
            ///    - **Minimum Velocity Limit:** The multiphase mixture velocity must remain above the critical deposition
            ///      threshold (:math:`v_{mix} \ge v_{min} \approx 3\text{ ft/s}`) to prevent water-oil phase segregation
            ///      and stagnant water pooling that accelerates bottom-of-line acid and CO2 corrosion.
            ///    - **Maximum / Erosional Velocity Limit (API RP 14E):** Fluid velocity must not exceed the erosional limit:
            ///      <math>
            ///          v_{e} = \frac{C}{\sqrt{\rho_{mix}}}
            ///      </math>
            ///      where :math:`C` is the empirical erosion factor (typically 100 to 125 for solids-free fluids)
            ///      and :math:`\rho_{mix}` is the in-situ mixture density (lb/ft3).
            ///
            /// 3. **Group & Central Processing Facility (CPF) Constraints:**
            ///    - **Total Liquid Handling Capacity:** :math:`\sum_{i=1}^{N_w} q_{L,i} \le Q_{L,max}` set by primary separators.
            ///    - **Produced Water Handling & Disposal Limit:** :math:`\sum_{i=1}^{N_w} q_{w,i} \le Q_{w,max}` governed by
            ///      hydrocyclones, skim tanks, and water reinjection pumps.
            ///    - **Total Available Lift Gas:** :math:`\sum_{i=1}^{N_w} q_{gi} \le Q_{g,avail}` defined by the capacity of the
            ///      gas lift compressor station.
            ///
            /// <header 3> Mathematical Problem Formulation </header>
            /// The optimal allocation of lift gas among :math:`N_w` wells connected to a common processing facility
            /// is formulated as a constrained non-linear programming (NLP) problem:
            ///
            /// <math>
            ///     \min_{\mathbf{q}_g} f(\mathbf{q}_g) = -\sum_{i=1}^{N_w} q_{o,i}(q_{gi})
            /// </math>
            ///
            /// Subject to the inequality constraints:
            /// <math>
            ///     g_1(\mathbf{q}_g) = \sum_{i=1}^{N_w} q_{gi} - Q_{g,avail} \le 0 \quad \text{(Gas Lift Compressor Limit)}
            /// </math>
            /// <math>
            ///     g_2(\mathbf{q}_g) = \sum_{i=1}^{N_w} q_{w,i}(q_{gi}) - Q_{w,max} \le 0 \quad \text{(Water Handling Limit)}
            /// </math>
            /// <math>
            ///     g_3(\mathbf{q}_g) = \sum_{i=1}^{N_w} q_{L,i}(q_{gi}) - Q_{L,max} \le 0 \quad \text{(Total Liquid Limit)}
            /// </math>
            ///
            /// with box bounds on individual injection rates:
            /// <math>
            ///     0 \le q_{gi} \le q_{gi,max}, \quad \forall i = 1, \dots, N_w
            /// </math>
            ///
            /// The Gas Lift Performance Curve (GLPC) for each well is modeled as a concave diminishing-returns curve:
            /// <math>
            ///     q_{o,i}(q_{gi}) = a_i \left(1 - e^{-b_i q_{gi}}\right) - c_i q_{gi}
            /// </math>
            /// where :math:`a_i` is the potential lift capacity, :math:`b_i` is the aeration lift efficiency,
            /// and :math:`c_i` represents the friction penalty at elevated injection velocities.
            ///
            /// <code> Gas Lift Allocation & Production Network Optimization
            {
                // Number of production wells connected to the manifold
                int nWells = 4;

                // Well Model Parameters: a_i (STB/d), b_i (1/(MMscf/d)), c_i (STB/MMscf), WaterCut (%)
                double[] a = [2200.0, 1600.0, 1100.0, 1900.0];
                double[] b = [1.50,   1.20,   1.80,   0.90];
                double[] c = [45.0,   35.0,   55.0,   30.0];
                double[] waterCut = [0.10, 0.35, 0.60, 0.25]; // Fraction of liquid that is water

                // Surface Facility Capacity Constraints
                double Qg_avail = 6.0;    // Total available lift gas (MMscf/day)
                double Qw_max   = 2000.0; // Facility water separation limit (STB/day)
                double QL_max   = 6200.0; // Facility total liquid handling limit (STB/day)

                // Individual Well Injection Upper Bounds (MMscf/day)
                double[] ub = [2.5, 2.5, 2.0, 2.5];
                double[] lb = [0.0, 0.0, 0.0, 0.0];
                double[] x0 = [1.5, 1.5, 1.5, 1.5]; // Initial guess: uniform allocation

                // Oil rate function for a single well: q_o(q_g)
                double WellOilRate(int i, double qg)
                {
                    double qo = a[i] * (1.0 - Exp(-b[i] * qg)) - c[i] * qg;
                    return Max(0.0, qo);
                }

                // Objective Function: Minimize negative total field oil rate (maximize oil)
                double Objective(ColVec qg)
                {
                    double totalOil = 0.0;
                    for (int i = 0; i < nWells; i++)
                    {
                        totalOil += WellOilRate(i, qg[i]);
                    }
                    return -totalOil;
                }

                // Inequality Constraints: g(x) <= 0
                ColVec InequalityConstraints(ColVec qg)
                {
                    double totalGas = 0.0;
                    double totalWater = 0.0;
                    double totalLiquid = 0.0;

                    for (int i = 0; i < nWells; i++)
                    {
                        double qo = WellOilRate(i, qg[i]);
                        double wc = waterCut[i];
                        double ql = qo / (1.0 - wc);
                        double qw = ql - qo;

                        totalGas += qg[i];
                        totalWater += qw;
                        totalLiquid += ql;
                    }

                    // g1: Total Gas <= Qg_avail  =>  Total Gas - Qg_avail <= 0
                    // g2: Total Water <= Qw_max  =>  Total Water - Qw_max <= 0
                    // g3: Total Liquid <= QL_max =>  Total Liquid - QL_max <= 0
                    return new ColVec([
                        totalGas - Qg_avail,
                        totalWater - Qw_max,
                        totalLiquid - QL_max
                    ]);
                }

                // Solve the constrained optimization problem using Fmincon
                var result = Fmincon(Objective, x0, InequalityConstraints, null, lb, ub);
                ColVec qg_opt = result.x;
                double maxOil = -result.fval;

                // Calculate baseline unoptimized (uniform gas allocation) values
                double uniformQg = Qg_avail / nWells;
                double baselineOil = 0.0;
                for (int i = 0; i < nWells; i++)
                {
                    baselineOil += WellOilRate(i, uniformQg);
                }

                // Output Comparison Results
                Console.WriteLine("=========================================================================================");
                Console.WriteLine("                    PRODUCTION NETWORK GAS LIFT OPTIMIZATION                             ");
                Console.WriteLine("=========================================================================================");
                Console.WriteLine($"{"Well ID",-8} | {"WaterCut",-10} | {"Uniform Qg (MMscf/d)",-22} | {"Optimal Qg (MMscf/d)",-22} | {"Oil Gain (STB/d)",-18}");
                Console.WriteLine("-----------------------------------------------------------------------------------------");

                double totalOptWater = 0.0;
                double totalOptLiquid = 0.0;
                for (int i = 0; i < nWells; i++)
                {
                    double qo_base = WellOilRate(i, uniformQg);
                    double qo_opt  = WellOilRate(i, qg_opt[i]);
                    double ql_opt  = qo_opt / (1.0 - waterCut[i]);
                    double qw_opt  = ql_opt - qo_opt;

                    totalOptWater += qw_opt;
                    totalOptLiquid += ql_opt;

                    Console.WriteLine($"Well {i + 1,-3} | {waterCut[i] * 100,7:F1}%   | {uniformQg,22:F3} | {qg_opt[i],22:F3} | {qo_opt - qo_base,18:F1}");
                }

                Console.WriteLine("=========================================================================================");
                Console.WriteLine($"Baseline Field Oil Production (Uniform Allocation) : {baselineOil:F1} STB/day");
                Console.WriteLine($"Optimized Field Oil Production (Constrained NLP)    : {maxOil:F1} STB/day");
                Console.WriteLine($"Net Oil Production Gain                             : +{maxOil - baselineOil:F1} STB/day (+{((maxOil - baselineOil) / baselineOil) * 100:F2}%)");
                Console.WriteLine($"Total Lift Gas Utilized                             : {qg_opt.Sum():F3} / {Qg_avail:F1} MMscf/day");
                Console.WriteLine($"Total Produced Water Handled                        : {totalOptWater:F1} / {Qw_max:F1} STB/day");
                Console.WriteLine($"Total Liquid Rate Produced                          : {totalOptLiquid:F1} / {QL_max:F1} STB/day");
                Console.WriteLine("=========================================================================================");

                // Plot the Gas Lift Performance Curves (GLPC) and Optimal Operating Points
                ColVec qg_range = Linspace(0, 3.0, 100);
                string[] colors = ["#1f77b4", "#ff7f0e", "#2ca02c", "#d62728"];

                for (int i = 0; i < nWells; i++)
                {
                    int wellIndex = i;
                    ColVec qo_curve = Arrayfun(q => WellOilRate(wellIndex, q), qg_range);
                    double opt_qo = WellOilRate(wellIndex, qg_opt[wellIndex]);

                    Plot(qg_range, qo_curve, colors[i]); HoldOn();
                    Scatter(qg_opt[wellIndex], opt_qo, colors[i], 12);
                }
                HoldOff();

                // Typography and Axis Formatting (Using ScottPlot 5 TickLabelStyle)
                var plt = GetCurrentAxis();
                plt.Axes.Bottom.TickLabelStyle.FontSize = 14;
                plt.Axes.Left.TickLabelStyle.FontSize = 14;
                plt.Axes.Bottom.TickLabelStyle.Bold = true;
                plt.Axes.Left.TickLabelStyle.Bold = true;

                Xlabel("Lift Gas Injection Rate (MMscf/day)");
                Ylabel("Oil Production Rate (STB/day)");
                Title("Gas Lift Performance Curves (GLPC) & Optimal Allocations");
                Legend([
                    "Well 1 (WC = 10%)", "Well 1 Optimum",
                    "Well 2 (WC = 35%)", "Well 2 Optimum",
                    "Well 3 (WC = 60%)", "Well 3 Optimum",
                    "Well 4 (WC = 25%)", "Well 4 Optimum"
                ]);

                SaveAs("Production_Network_Gas_Lift_Optimization.png");
            }
            /// </code>
            ///
            /// <header 3> Engineering Interpretation & Sensitivity </header>
            /// The optimization results illuminate several critical operational principles:
            ///
            /// <table> Optimal vs Uniform Resource Allocation
            ///  Well | Characteristics | Uniform Allocation | Optimal Allocation | Operational Impact
            ///  Well 1 | High PI, Low Water Cut (10%) | 1.50 MMscf/d | Higher Gas Allocation | High marginal oil response per unit gas; receives priority allocation.
            ///  Well 2 | Moderate PI, Medium WC (35%) | 1.50 MMscf/d | Balanced Allocation | Operates near peak slope of GLPC curve.
            ///  Well 3 | Weaker Well, High WC (60%) | 1.50 MMscf/d | Restricted Allocation | Choked back to avoid flooding surface separator with excessive water.
            ///  Well 4 | Deep Well, Low WC (25%) | 1.50 MMscf/d | Moderate Allocation | Sustains stable vertical lift without entering tubing friction penalties.
            /// </table>
            ///
            /// By shifting from arbitrary equal allocation to systematic Non-Linear Programming (NLP), the asset
            /// achieves a noticeable surge in total oil production while rigorously adhering to separator water-handling limits
            /// and total compressor gas availability.
            ///
            /// </BookContent>
        }
    }
}