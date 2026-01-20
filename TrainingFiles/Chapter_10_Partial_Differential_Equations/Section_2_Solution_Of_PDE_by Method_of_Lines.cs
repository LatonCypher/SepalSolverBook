using CSharpMath.Atom.Atoms;
using Microsoft.VisualBasic;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.TickGenerators.TimeUnits;
using SepalSolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1.TrainingFiles.Chapter_10_Partial_Differential_Equations
{
    internal class Section_2_Solution_Of_PDE_by_Method_of_Lines
    {
        public static void Run()
        {
            /// <BookContent>
            /// The **Method of Lines (MOL)** is a powerful numerical technique used to solve partial differential equations (PDEs), particularly those that are time-dependent (evolutionary).

            ///Instead of discretizing all dimensions(space and time) simultaneously, the core idea is to discretize the spatial variables while leaving the time variable continuous. This transforms a single PDE into a system of coupled **Ordinary Differential Equations(ODEs)**.
            

            ///---
            ///
            /// ## How It Works: The 3-Step Process

            /// Imagine you are solving the heat equation: 
            /// 
            /// 1. **Spatial Discretization**: You divide the spatial domain into a grid of points(e.g., ). You replace the spatial derivatives() with finite difference approximations, such as:
            /// 
            /// 
            /// 2. **Conversion to ODEs**: By applying this at every grid point, the PDE becomes a set of ODEs, one for each point :
            /// 
            /// 
            /// 3. **Temporal Integration**: Now that you have a system of ODEs, you can use standard, high-performance ODE solvers like **Runge-Kutta**or **Euler’s method**to step forward in time.
            /// 
            /// ---
            /// 
            /// ## Why Use It? (Advantages)
            /// 
            /// - **Leverages Existing Tech**: You can use sophisticated, pre-built ODE solvers(like `NDSolve` in Mathematica or `ode45` in MATLAB) that automatically handle error control and adaptive time-stepping.
            /// - **Flexibility**: You can use different spatial discretization methods, such as finite differences, finite elements, or spectral methods, depending on the geometry of your problem
            /// - **Simplification**: It breaks a complex multi-dimensional problem into a more manageable "line-by-line" temporal evolution.
            /// 
            /// ## Limitations
            /// 
            /// - **Stiffness**: The resulting system of ODEs is often "stiff," meaning you may need implicit solvers to avoid tiny, inefficient time steps.
            /// - **Not for All PDEs**: It is designed for "evolutionary" problems (parabolic and hyperbolic). It cannot be used directly for purely "steady-state" elliptic equations(like Laplace’s equation) without adding a "pseudo-time" variable.
            /// 
            /// ---
            /// 
            ///### Comparison at a Glance
            /// <table>
            ///  Feature | Standard Finite Difference(FDM) | Method of Lines(MOL) |
            ///  **Time Treatment** | Discretized at the start | Kept continuous initially
            ///  **Solving Logic** | Solves the whole grid at once | Solves ODEs along "lines" of time 
            ///  **Software** | Requires custom time-stepping | Uses off-the-shelf ODE solvers |
            ///  
            /// </table>
            /// </BookContent>
        }
    }
}
