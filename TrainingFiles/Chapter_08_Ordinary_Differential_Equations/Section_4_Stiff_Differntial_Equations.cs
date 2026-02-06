using ScottPlot;
using SepalSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CSharpMath.Rendering.Text.TextAtom;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1.TrainingFiles.Chapter_08_Ordinary_Differential_Equations
{
    internal class Section_4_Stiff_Differntial_Equations
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> Stiff Differential Equations </header >
            /// 
            /// In the world of numerical simulation, Stiffness is a phenomenon where certain terms in a differential equation lead to extremely rapid changes in the solution, even if the overall behavior of the system is smooth. It is not a mathematical property of the equation itself, but rather a practical limitation of the numerical methods used to solve it.A system is generally considered "stiff" when there is a large disparity between the fastest and slowest "time scales" in the problem.<header 3> 1. The "Stability Trap" </header 3>Standard explicit solvers (like the classic Runge-Kutta 4th Order) work by taking small steps based on the current slope. In a stiff system, if the step size $h$ is even slightly too large, the solver will "overcorrect" for a rapid transient, leading to wild oscillations and eventually a total crash (divergence).To maintain stability in a stiff system, an explicit solver is forced to take steps so infinitesimally small that the simulation may take days or years to complete, even though the physical system being modeled is barely changing.
            /// 
            /// <header 2> Engineering Usage Examples </header 2>
            /// Stiff equations appear whenever a system involves processes that happen at vastly different speeds simultaneously.
            /// <header 3> 1. Chemical Kinetics and Combustion </header 3>
            /// In a car engine's combustion chamber, some chemical reactions occur in microseconds (the creation of free radicals), while the overall combustion process and the movement of the piston take milliseconds. To model the exhaust emissions accurately, you must solve a stiff system that captures both the "lightning-fast" chemistry and the "slow" mechanical motion.
            /// 
            /// <header 3> 2. Electronic Circuit Simulation </header 3>
            /// In high-frequency power electronics, a circuit might have a capacitor that charges in nanoseconds alongside a cooling system that responds over minutes. Standard simulators would choke on the nanosecond transients; stiff solvers allow the simulation to "step over" the fast transients once they have stabilized.
            /// 
            /// <header 3> 3. Stiffness in Structural Mechanics </header 3>
            /// A stiffly-supported beam or a very rigid spring system (high spring constant $k$) creates equations where high-frequency vibrations exist alongside slow, large-scale deflections. Stiff solvers are required to ensure the high-frequency "noise" doesn't blow up the math of the "signal."
            /// 
            /// <header 2> Solving Strategy: Implicit Methods </header 2>
            /// The cure for stiffness is the use of Implicit Methods (such as Backward Euler or BDF - Backward Differentiation Formulas and Diagonally Implicit Runge Kuttas).Unlike explicit methods that look at where the system is, implicit methods look at where the system is going by solving an algebraic equation at every step. While this requires more CPU power per step (often involving a matrix inversion), it allows for much larger time steps without losing stability.
            /// 
            /// <header 3> Examples </header 3>
            /// SepalSolver impelements ODE45s for stiff differential equation and we look at how to use this function to solve stiff Van der Pol Oscillator and Robertson differential equation. 
            /// <example 1>
            ///  Solve the ODE :math:`~d^2y/dt^2 = 10^{5}((1 - y^2)y' - y)~` with initial condition :math:`~y(0) = [2, 0]~` over the interval :math:`[0, 6.3]`.
            ///  
            /// First we have to convert this to a system of first order differential equations,
            /// <math>
            /// \begin{ array}{ rcl}
            ///     y' &=& v \\
            ///     v' &=& 10^{5}((1 - y^2)v - y)
            /// \end{ array}
            /// </math>
            /// 
            /// <code>
            {
                //define ODE
                double[] vdp2(double t, double[] y)=>
                    [y[1], 1e5*((1 - y[0] * y[0]) * y[1] - y[0])];

                //Solve ODE
                (ColVec T, Matrix Y) = Ode45s(vdp2, [2, 0], [0, 6.3]);
                // Plot the result
                Plot(T, Y);
                Xlabel("Time t"); Ylabel("Soluton y");
                Legend(["y_1", "y_2"], UpperLeft);
                Title("Solution of van der Pol Equation (μ = 1e5) with ODE45s");
                SaveAs("Van-der-Pol-(μ=1e5)-Ode45s");
            }
            /// </code>
            /// </example>
            /// 
            /// <example 2> 
            /// The Robertson ODE is the classic "benchmark" problem used to test the efficiency and stability of numerical solvers for stiff differential equations. It describes a simplified chemical reaction involving three species (:math:`y_1, y_2, y_3`) with reaction rates that differ by several orders of magnitude.
            /// 
            /// The Reaction Network
            /// The system models the following three reactions:
            /// :math: `y_1 \xrightarrow{ 0.04} y_2`(Slow)
            /// :math: `y_2 + y_2 \xrightarrow{ 3 \cdot 10^7} y_3 + y_2` (Very Fast)
            /// :math: `y_2 + y_3 \xrightarrow{ 10^4} y_1 + y_3` (Fast)
            /// 
            /// The resulting system of differential equations is:
            /// :math:\cfrac{dy_1}{dt} = -0.04y_1 + 10^4 y_2 y_3
            /// :math:\frac{dy_2}{dt} = 0.04y_1 - 10^4 y_2 y_3 -3 \cdot 10^7 y_2^2
            /// :math:\frac{dy_3}{dt} = 3 \cdot 10^7 y_2^2
            /// 
            /// </example>
            /// </BookContent>
        }
    }
}
