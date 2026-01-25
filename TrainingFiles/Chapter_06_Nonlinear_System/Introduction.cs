using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.Hatches;
using ScottPlot.Palettes;
using ScottPlot.Plottables;
using ScottPlot.TickGenerators.Financial;
using ScottPlot.TickGenerators.TimeUnits;
using SepalSolver;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using static SepalSolver.Statistics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1.TrainingFiles.Chapter_06_Solution_of_Nonlinear_System
{
    internal class Introduction
    {
        public static void Run()
        {
            /// <BookContent>
            /// **Root finding** refers to the process of determining the values of a variable that satisfy an equation:
            /// :math:`f(x) = 0`. These values, called *roots* or *zeros*, are fundamental in mathematics, engineering, and applied sciences because they often represent equilibrium points, intersections, or critical solutions in real-world problems.For example, finding the root of a polynomial can reveal where a curve crosses the x-axis, while solving transcendental equations can help model phenomena like oscillations or population growth.
            /// 
            /// **A nonlinear system** consists of equations where the variables appear in a non-linear manner(e.g., squared, multiplied together, or inside trigonometric/exponential functions).Unlike linear systems, nonlinear systems are more complex because they can have multiple solutions, no solutions, or solutions that are difficult to approximate.Examples include:
            /// - Chemical reaction models
            /// - Electrical circuits with nonlinear components
            /// - Mechanical systems with friction or elasticity
            /// 
            /// **Solution Methods** Since exact solutions are often impossible to obtain analytically, numerical
            /// methods are widely used. Common approaches include:
            /// - **Bisection Method**: A simple, robust technique that repeatedly halves an interval to locate a root. It relies on the Intermediate Value Theorem:
            /// <math>
            /// f(a) \cdot f(b) < 0 \quad \Rightarrow \quad \exists \, c \in (a, b) \; \text{ such that} \; f(c) = 0
            /// </math>
            /// 
            /// - **Newton-Raphson Method**: Uses derivatives to converge rapidly to a solution, but requires good initial guesses:
            /// /// <math>
            /// x_{ n+1} = x_n - \frac{ f(x_n)}{f'(x_n)}
            /// </math>
            /// 
            /// - **Secant Method**: Similar to Newton’s method but avoids explicit derivative calculation:
            /// <math>
            /// x_{n+1} = x_n - f(x_n) \cdot \frac{x_n - x_{n-1}}{ f(x_n) - f(x_{ n-1})}
            /// </math>
            /// 
            /// - **Fixed-Point Iteration**: Transforms the equation into an iterative form:
            /// <math>
            /// x_{n+1} = g(x_{ n-1})
            /// </math>
            /// 
            /// - **Multivariable Extensions**: For nonlinear systems, methods like Newton’s
            /// method are generalized to handle multiple equations simultaneously:
            ///  <math>
            ///  \mathbf{ x}_{ n+1} = \mathbf{ x}_n - J^{ -1} (\mathbf{ x}_n) \cdot \mathbf{ f} (\mathbf{ x}_n)
            ///  </math>
            ///  where: math:`J` is the Jacobian matrix of partial derivatives.
            ///  
            /// <header 2> Why It Matters </header>
            /// Root finding and solving nonlinear systems are essential tools in computational mathematics.They allow scientists and engineers to model, simulate, and optimize systems that cannot be solved with simple algebra. From predicting weather patterns to designing stable structures, these techniques form the backbone of modern problem-solving.

            /// 


        /// </BookContent>
        }
}
}
