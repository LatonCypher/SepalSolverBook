using CSharpMath.Atom.Atoms;
using HarfBuzzSharp;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.PathStrategies;
using ScottPlot.TickGenerators.Financial;
using SepalSolver;
using System.Net.NetworkInformation;

namespace ConsoleApp1.TrainingFiles.Chapter_08_Ordinary_Differential_Equations
{
    internal class Section_5_Implicit_Differential_Equations
    {
        public static void Run()
        {
            /// <BookContent>
            /// Implicit differential equations (IDEs) are a fascinating, if slightly rebellious, branch of calculus. While most standard differential equations are "explicit"—meaning you can neatly isolate the derivative on one side—IDEs keep things tangled.
            /// 
            /// Think of it like the difference between a recipe that says "Add 2 cups of flour"(explicit) and one that says "The amount of flour plus the amount of sugar must equal 5 cups" (implicit). You know the relationship, but you have to do some work to find the specific values.
            /// 
            /// <header 2> What Makes an Equation Implicit? </header>
            /// 
            /// In a standard explicit first-order ODE, we write:
            /// <math>
            ///     \frac{dy}{dx} = f(x, y)
            /// </math>
            /// 
            /// In an **implicit differential equation**, the derivative is embedded within a function where it cannot be (or simply isn't) isolated:
            /// 
            /// <math>
            ///     F(x, y, \frac{dy}{dx}) = 0
            /// </math>
            /// 
            /// <header 2> Why Use Them? </header>
            /// 
            /// * **Physics & Constraints:** Many physical systems are governed by constraints (like a bead sliding on a wire) where the relationship between position and velocity is fixed by the geometry, not a direct formula.
            /// * **Singularities:** IDEs can describe behaviors where the derivative might become undefined or "multi-valued"(where one  point has multiple possible slopes).
            /// * **Differential-Algebraic Equations(DAEs):** These are a subset of IDEs often used in electrical circuit simulation and multi-body dynamics.
            /// 
            /// <header 2> Solving Strategies </header>
            /// Because you can't always "solve for :math:`y'`," the approach changes:
            /// 
            /// 1. **Implicit Differentiation:** If you have an equation like, :math:`x^2 + y^2 = 1`, you differentiate every term with respect to, treating  as a function of: :math: `2x + 2y \frac{dy}{dx} = 0`
            /// Then, you isolate :math:`\cfrac{dy}{dx}` if possible.
            /// 
            /// 2. **Direction Fields:** You can still visualize these equations! For any point, you solve the algebraic equation :math:`F(x,y,y') = 0` for :math:`y'`. If there are multiple solutions for :math:`y'`, the slope field might have overlapping segments.
            /// 
            /// 3. **Numerical Solvers:** For complex IDEs or DAEs, standard solvers like Runge-Kutta might struggle.Specialized algorithms(like the Backward Differentiation Formula, or Diagonally implicit rungekutta) are used to handle the "stiffness" of these equations.
            /// 
            /// <header 2> A Classic Example: Clairaut's Equation </header>
            /// One of the most famous IDEs is **Clairaut's Equation**: :math:`y = x \frac{dy}{dx} + f\left(\cfrac{dy}{dx}\right)`. This equation is unique because it often yields two types of solutions: a family of straight lines(the general solution) and a "singular solution" that acts as an envelope to those lines.
            /// 
            /// <header 2> Numerical Solution </header>
            /// SepalSolver's Ode45i can handle implicit equations, but you need to provide the function in the form :math:`F(x, y, y') = 0`. Here's a simple example of how to set up and solve an implicit equation using SepalSolver:
            /// To solve the clairaut's equation, we can rearrange it to fit the form :math:`F(x, y, y') = 0`: ie
            /// , :math:`F(x, y, y') = y - x y' - f(y')`.
            /// 
            /// <example 1> Solving Clairaut's Equation :math:`y = x y' + \left(y'\right)^2`
            /// 
            /// <math>
            ///     F(x, y, y') = y - x y' - \left(y'\right)^2, \quad y(0) = 1
            /// </math>
            /// 
            /// First we need to compute the :math:`y'(0)` from the initial condition using decic. 
            /// And then we can use the computed :math:`y'(0)` to solve the equation using Ode45i.
            /// <code>
            {
                // define the implicit function F(x, y, yp) = 0
                double F(double x, double y, double yp) => y - x*yp - yp * yp;

                // initial conditions y(0) = 1, and guess for y'(0) = 0.1   
                double y0 = 1, yp0 = 0.1;

                // Compute y'(0) using decic
                (y0, yp0) = decic(F, 0, y0, 1, yp0, 0);

                // Now solve the implicit ODE using Ode45i
                (ColVec T, ColVec Y) = Ode45i(F, (y0, yp0), [0, 5]);

                // Plot the results
                Scatter(T, Y, "fob"); HoldOn();

                // Add analytical solution for comparison
                Plot(T, T+1, "r"); HoldOff();

                // Axis label and legend
                Xlabel("x"); Ylabel("y"); 
                Legend(["Numerical Solution", "Analytical Solution"], LowerRight);

                // Save the plot
                SaveAs("Clairaut.png");
            }
            /// </code>
            /// </example>
            /// 
            /// </example> Solve Weissinger implicit ODE
            /// While Clairaut's equation is a textbook classic, **Weissinger’s Implicit Differential Equation** takes things a step further into the realm of higher-degree implicit equations. It is specifically a first-order equation where the derivative  is raised to a power, but it maintains a structure that allows for a clever substitution method.
            /// The general form of a Weissinger equation is:
            ///  :math:`y = x^n f(y') + g(y')`
            /// 
            /// In many contexts, particularly in the study of aerodynamics(where Weissinger’s name is prominent due to his work on lifting-line theory), you might see specialized versions of this.However, in pure mathematics, it is often treated as a generalization of d'Alembert’s equation.
            /// 
            /// <header 3> 1. Structure and Characteristics </header>
            /// Unlike a standard ODE, the Weissinger equation is **nonlinear in the derivative**.
            /// 
            /// * **Relationship to Clairaut:** If you set :math:`n = 1` and :math:`f(y') = y'`, you essentially return to the Clairaut form.
            /// * **The Power of :math:`x`:** The :math:`x^n` term dictates how the geometry of the solution curves scales as you move away from the origin.
            /// 
            /// <header 3> 2. The Solution Strategy: Parameterization </header>
            /// To solve a Weissinger equation, we rarely try to isolate  algebraically.Instead, we use a parameter, where:
            /// :math:`p = y' = \frac{dy}{dx}`
            /// substituting :math:`p` into the equation gives:
            /// :math:`y = x^n f(p) + g(p)`
            ///  To find the relationship between :math:`x` and :math:`p`, we differentiate the entire equation with respect to :math:`x`:
            /// :math:`\cfrac{dy}{dx} = nx^{n-1} f(p) + x^n f'(p) \cfrac{dp}{dx} + g'(p) \cfrac{dp}{dx}`
            ///  
            /// Since :math:`\frac{dy}{dx} = p` , we get a **linear differential equation for  in terms of** :math:`p`:  
            /// 
            /// :math:`p  = nx^{n-1} f(p) + \left[x^n f'(p) + g'(p)] \cfrac{dp}{dx}`
            /// 
            /// This transformation is powerful because it turns a difficult implicit equation into a linear one(usually of the Bernoulli type or similar), which we can solve to get :math:`x(p)`. Once you have :math:`x(p)` and :math:`y(fp)`, you have a** parametric solution** to the original ODE.
            /// 
            /// <header 3> 3. Why Weissinger Equations Matter </header>
            /// Weissinger's work is most famous in **fluid dynamics**, specifically the **Weissinger Area Rule** and his "L-method" for calculating lift distribution on swept wings.
            /// In these engineering contexts, implicit equations arise because the induced downwash(the change in airflow direction) depends on the lift, but the lift itself is a function of that downwash.
            /// 
            /// <header 3> Applications include: </header>
            /// * ** Aerodynamics:** Modeling the circulation around wings with non-rectangular shapes.
            /// * **Classical Mechanics:** Describing trajectories where the velocity constraint is non-linear.
            /// * ** Singularities:** Just like Clairaut equations, Weissinger equations often have "envelope" solutions where the uniqueness of the solution breaks down.
            /// 
            /// 
            /// 
            /// </example 2> 
            /// 
            /// </BookContent>
        }
    }
}
