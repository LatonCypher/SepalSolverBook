using CSharpMath.Atom.Atoms;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.Statistics;
using ScottPlot.TickGenerators.Financial;
using ScottPlot.TickGenerators.TimeUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CSharpMath.Rendering.Text.TextAtom;

namespace ConsoleApp1.TrainingFiles.Chapter_11_Production_System_Modelling
{
    internal class Section_5_Decline_Curve_Analysis
    {
        public static void Run()
        {
            /// <BookContent>
            ///
            /// **Definition:**
            /// Decline Curve Analysis involves fitting a mathematical function to historical
            /// production rate data over time. The three standard models defined by Arps
            /// are Exponential, Hyperbolic, and Harmonic decline.

            /// <header 3> The General Equation </header>
            /// All three decline types are derived from the general equation:
            /// <math>
            ///     q(t) = \frac{q_i}{(1 + b D_i t)^{1/b}}
            /// </math>
            /// Where:
            ///
            /// - :math:`q(t)` = production rate at time :math:`t`
            ///
            /// - :math:`q_i` = initial production rate
            ///
            /// - :math:`D_i` = initial nominal decline rate (1/time)
            ///
            /// - :math:`b` = decline exponent (0 for exponential, 1 for harmonic)
            ///
            /// <header 3> Exponential Decline (:math:`b = 0`) </header>
            /// Used when the decline rate is constant. This is common in highly
            /// undersaturated oil reservoirs or wells with constant pressure boundaries.
            /// <math>
            ///     q(t) = q_i e^{-D t}
            /// </math>
            ///
            /// **Numerical Example:**
            ///
            /// Given:
            ///
            /// - :math:`q_i = 1000 \, \text{STB/day}`
            ///
            /// - :math:`D = 0.05 \, \text{per month}`
            ///
            /// - Find rate after 12 months:
            ///
            /// <math>
            ///     q(12) = 1000 \cdot e^{-(0.05 \cdot 12)} = 1000 \cdot 0.5488 = 548.8 , \text{STB/day}
            /// </math>
            ///
            /// <code>
            {
                double qi = 1000; // STB/day
                double D = 0.05; // per month
                double t = 12; // months
                double q_t = qi * Exp(-D * t);
                Console.WriteLine($"Rate after 12 months = {q_t:F2} STB/day");
            }
            /// </code>
            /// 
            /// <header 3> Linearization for Exponential Decline </header>
            /// The exponential equation :math:`q = q_i e^{-Dt}` can be linearized by taking
            /// the natural logarithm of both sides:
            /// <math>
            ///     \ln(q) = \ln(q_i) - D \cdot t
            /// </math>
            /// By plotting :math:`\ln(q)` vs. :math:`t`, the slope is :math:`-D` and the
            /// intercept is :math:`\ln(q_i)`.

            /// Practical Example:
            /// <code>
            {
                // Data: t (months), q (STB/day)
                double[] t = [ 1, 2, 3, 4, 5 ];
                double[] q = [ 950, 905, 860, 820, 780 ];

            }
            /// </code>
            /// 
            /// <header 3> Linearization for Harmonic Decline (:math:`b=1`) </header>
            /// The harmonic equation :math:`q = q_i / (1 + D_i t)` is linearized by taking
            /// the reciprocal of the rate:
            /// <math>
            ///     \frac{1}{q} = \frac{1}{q_i} + \left(\frac{D_i}{q_i}\right) \cdot t
            /// </math>
            /// By plotting :math:`1/q` vs. :math:`t`, the slope is :math:`D_i/q_i` and the
            /// intercept is :math:`1/q_i`.

            /// Practical Example:
            /// <code>
            {
                double[] t = [ 1, 2, 3, 4, 5 ];
                double[] q = [ 1000, 909, 833, 769, 714 ];

            }
            /// </code>
            /// 
            /// <header 3> Hyperbolic Decline (:math:`0 < b < 1`) </header>
            /// The most common decline type. The decline rate itself decreases over time.
            /// 
            /// **Numerical Example:**
            /// 
            /// Given:
            /// 
            /// - :math:`q_i = 1500 \, \text{Mscf/day}`
            /// 
            /// - :math:`D_i = 0.10 \, \text{per month}`
            /// 
            /// - :math:`b = 0.5`
            /// 
            /// <math>
            ///      q(t) = \frac{1500}{(1 + 0.5 \cdot 0.1 \cdot 12)^{1/0.5}} = \frac{1500}{(1.6)^2} = 585.9 \, \text{Mscf/day}
            /// </math>
            /// 
            /// <code>
            {
                double qi = 1500;
                double Di = 0.10;
                double b = 0.5;
                double t = 12;
                double q_t = qi / Pow(1 + b * Di * t, 1 / b);
                Console.WriteLine($"Hyperbolic Rate = {q_t:F2} Mscf/day");
            }
            /// </code>
            ///
            /// 
            /// <header 3> Linearization for Hyperbolic Decline </header>
            /// Hyperbolic decline (:math:`0 < b < 1`) cannot be fully linearized with simple
            /// variables because of the :math:`b` exponent. Instead, we linearize the
            /// Loss Ratio (:math:`1/D`), defined as :math:`a = q / (dq/dt)`:
            /// <math>
            ///     \frac{q}{dq/dt} = \frac{1}{D_i} + b \cdot t
            /// </math>
            /// To solve this, we compute the derivative of production over time, plot the
            /// loss ratio vs. :math:`t`, and find :math:`b` (slope) and :math:`1/D_i` (intercept).

            /// Practical Example:
            /// <code>
            {
                // Loss Ratio (a) calculated from historical data
                double[] t = { 1, 2, 3, 4, 5 };
                double[] loss_ratio = { 10.5, 11.0, 11.5, 12.0, 12.5 };

            }
            /// </code>
            /// <header 3> Cumulative Production and EUR </header>
            /// To calculate the Estimated Ultimate Recovery (EUR), we integrate the rate
            /// over time until a **limit rate** (:math:`q_{limit}`) is reached.
            ///
            /// **For Exponential Decline:**
            /// <math>
            ///      G_p = \frac{q_i - q(t)}{D}
            /// </math>
            ///
            /// <code>
            {
                double qi = 1000;
                double q_limit = 50; // Economic limit
                double D = 0.05;
                double EUR = (qi - q_limit) / D;
                Console.WriteLine($"EUR (Exponential) = {EUR:F2} STB");
            }
            /// </code>
            /// 
            /// <header 3> Computing the Exponential Model Decline Rate </header>
            /// Using the given data
            /// <code>
            {
                double[] t = [0, 10, 20, 30, 40, 50, 60, 70, 80, 90];
                double[] qt = [double.NaN, 990.05, 980.20, 970.45, 960.78, 951.23, 941.76, 932.39, 923.12, 913.93];
            }
            /// </code>
            /// 
            /// **Solution**
            /// - 1. Compute the commulative production Np
            /// - 2. Plot qt versus Np and measure the slope and intercept
            /// - 3. intercept is the :math:`q_i` and slope is the :math:`D`
            /// <code>
            {
                double[] t = [0, 10, 20, 30, 40, 50, 60, 70, 80, 90];
                double[] qt = [double.NaN, 990.05, 980.20, 970.45, 960.78, 951.23, 941.76, 932.39, 923.12, 913.93];
                // Compute Cumulative
                double[] Np = Zeros(t.Length);
                for (int i = 1; i < t.Length; i++)
                    Np[i] = Np[i-1] + qt[i]*(t[i] - t[i-1]);
                // Compute Slope and Intercept
                double[] coeffs = Polyfit(Np[1..], qt[1..], 1);

                // Plot
                Scatter(Np[1..], qt[1..], "fob", 15); HoldOn();
                Plot(Np, [.. Np.Select(np => Polyval(coeffs, np))]); HoldOff();
                Legend(["Measured Data", "Line of Best Fit"]);
                SaveAs("Decline_Curve_Fitting.png");

                // Print Result
                Console.WriteLine($"D = {coeffs[0]}");
                Console.WriteLine($"q_i = {coeffs[1]}");
            }
            /// </code>
            /// </BookContent>

        }
    }
}

