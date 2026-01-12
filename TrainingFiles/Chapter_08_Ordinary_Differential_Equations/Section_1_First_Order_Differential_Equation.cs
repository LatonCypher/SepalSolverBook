using ScottPlot;

namespace ConsoleApp1.TrainingFiles.Chapter_7_Ordinary_Differential_Equations
{
    internal class Section_1_First_Order_Differential_Equation
    {
        public static void Run()
        {
            /// <BookContent>
            /// <header 2> 1. What is a Differential Equation?</header 2>
            /// A differential equation(DE) is a mathematical equation that relates a function with its derivatives. In simpler terms, it describes how a quantity changes in relation to its current state.
            /// 
            /// - The Function: math:`(y)`: Represents the "state" of a system(e.g., the position of a car, the temperature of a room).
            /// - The Derivative: :math:`\cfrac{dt}{dy}` Represents the "rate of change"(e.g., the speed of the car, how fast the room is cooling).
            ///
            /// An ODE is "Ordinary" because the unknown function depends on only one independent variable(usually time t or distance x).
            ///
            /// <header 2> 2. The Intuition: The Slope Field </header 2>
            /// If you have an equation like :math:`\cfrac{dt}{dy} = y`, the equation is telling you: "The steeper the graph gets, the higher the value of y must be."
            /// Even before solving an equation, we can visualize it using a Slope Field(or Direction Field).At every point(t, y) on a graph, we draw a tiny line segment with the slope dictated by the differential equation. A solution to the ODE is simply a curve that "follows the arrows."
            ///
            /// <header 2> 3. Analytical vs.Numerical Solutions </header 2>
            /// **Analytical Solutions(The "Exact" Way)**
            /// This is what you do in a calculus class. You use integration techniques to find a precise formula for y(t).
            /// 
            /// - example: For :math:`\cfrac{dt}{dy} = ky`, the analytical solution is  :math:`y(t) = Ce^{kt}`
            /// - Pros: Perfectly accurate; gives you a formula you can use forever.
            /// - Cons: Most complex equations in physics and engineering cannot be solved this way.
            /// 
            /// **Numerical Solutions (The "Approximate" Way)**
            /// When an equation is too "messy" for calculus, we use numerical methods.Instead of finding a pretty formula, we use a computer to start at an initial point and take tiny steps forward, calculating the slope as we go.
            /// 
            /// - Pros: Can solve almost any equation, no matter how complex.
            /// - Cons: Always contains a small amount of "truncation error" because we are approximating a smooth curve with small straight lines.
            /// 
            /// <header 2> 4. The Anatomy of an Initial Value Problem (IVP)  </header 2>
            /// To get a single, specific answer from an ODE, you need a starting point, known as the Initial Condition.
            /// The Equation:  :math:`\cfrac{dt}{dy} = f(t, y)` (The rule of change), The Initial Condition:  :math:`y(0)=y_0` (The starting point). 
            /// Without a starting point, a differential equation has an infinite number of solutions(a "family" of curves). The initial condition picks the specific path the system takes.
            /// Numerical methods are essential because most real-world ordinary differential equations (ODEs) cannot be solved analytically (with pen and paper). Instead of finding a continuous formula for $y(x)$, we calculate discrete values at specific points.
            /// This guide covers the use of sepalsolver for solving an Initial Value Problem (IVP) defined by:  :math:`\cfrac{dy}{dt} = f(t, y), \quad y(t_0) = y_0`
            /// where :math:`f(t, y)` is a function that defines the rate of change of :math:`y` with respect to :math:`t`, and :math:`y_0` is the initial value of :math:`y` at time :math:`t_0`.
            /// <example 1>
            /// | Solve the first-order ODE: :math:`\cfrac{dy}{dt} = -2y`,
            /// | with the initial condition: :math:`y(0) = 1`,
            /// | over the interval: :math:`t \in [0, 5]`.
            /// <code> 
            {
                // Define the ODE as a function
                double dydt(double t, double y) => -2*y;
                // Initial condition
                double y0 = 1;
                // Time span
                double[] tspan = [0, 5];
                // Solve the ODE using Ode45
                (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
                // Plot the results
                Plot(T, Y, Linewidth: 2);
                Title("Solution of dy/dt = -2y with y(0) = 1");
                Xlabel("Time t");
                Ylabel("Function y");
                SaveAs("First_Order_ODE_Solution.png");
            }
            /// </code>
            /// </example 1>
            /// 
            /// <example 2>
            /// | Solve the first-order ODE :math:`\cfrac{dy}{dt} = \sin(t) - y`,  
            /// | with the initial condition :math:`y(0) = 0`,  
            /// | over the interval :math:`t \in [0, 10]`.
            /// <code>
            {
                // Define the ODE as a function
                double dydt(double t, double y) => Sin(t) - y;
                // Initial condition
                double y0 = 0;
                // Time span
                double[] tspan = [0, 10];
                // Solve the ODE using Ode45
                (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
                // Plot the results
                Plot(T, Y, Linewidth: 2);
                Title("Solution of dy/dt = sin(t) - y with y(0) = 0");
                Xlabel("Time t");
                Ylabel("Function y");
                SaveAs("First_Order_ODE_Solution_example2.png");
            }
            /// </code>
            /// </example 2>
            /// 
            /// <example 3>
            /// Solve a second order ODE (simple harmonic oscillator) by first converting to system of first order equation and 
            /// then solve the system of first-order ODEs representing the simple harmonic oscillator:
            /// 
            /// .. math:: \frac{d^2y}{dt^2} = -4y
            /// .. math:: y_0 = 0, \quad y'_0 = 5, \quad t = [0, 10];
            /// 
            /// | To solve this, we first transform the problem into a system of first order differential equations:
            /// | Let :math:`v = \cfrac{dy}{dt}`,   hence :math:`\cfrac{dv}{dt} = -4y, \quad y_0 = 0, \quad v_0 = 5`, 
            /// | Now we have 2 equations :math:`\cfrac{dy}{dt} = v, \quad \cfrac{dv}{dt} = -4y`
            /// <code>
            {
                // Simple Harmonic Oscillator
                double[] dydt(double t, double[] y) => 
                    [y[1], -4*y[0]];
                (ColVec T, Matrix Y) = Ode45(dydt, [0,5], [0,10]);
                Plot(T, Y, Linewidth: 2); 
                SaveAs("Simple_Harmonic_Oscillator.png");
            }
            /// </code>
            /// </example 3>
            /// 
            /// <example 4>
            /// lets look at harmonic oscillator with damping
            /// 
            /// .. math:: m\cfrac{d^2y}{dt^2} + c\cfrac{dy}{dt} + ky = 0
            /// .. math:: y_0 = 0.7, \quad y'_0 = 0, \quad t = [0, 30];
            /// 
            /// | where :math:`m` is the mass, :math:`c` is the damping coefficient, and :math:`k` is the spring constant.
            /// | To solve this, we first transform the problem into a system of first order differential equations:
            /// 
            /// | Let :math:`v = \cfrac{dy}{dt}`,   hence :math:`\cfrac{dv}{dt} =  -\cfrac{c}{m}v - \cfrac{k}{m}y, \quad y_0 = 0.7, \quad v_0 = 0`,
            /// | Now we have 2 equations :math:`\cfrac{dy}{dt} = v, \quad \cfrac{dv}{dt} = -(k/m)y - (c/m)v`
            /// 
            /// <code>
            {
                // Damping System
                double k = 3.5, c = 0.5, m = 2.0;
                double[] dydt(double t, double[] y) =>
                    [y[1], -(k/m)*y[0] - (c/m)*y[1]];
                (ColVec T, Matrix Y) = Ode45(dydt, [0.7, 0], [0, 30]);
                Plot(T, Y, Linewidth: 2);
                SaveAs("Damping_Harmonic_Oscillator.png");
            }
            /// </code>
            /// </example 4>
            /// 
            /// <example 5>
            /// <code>
            {
                // Predator Prey Model
                double alpha = 0.01, beta = 0.02;
                double[] dydt(double t, double[] y) =>
                    [(1 - alpha*y[1])*y[0], (-1 + beta*y[0])*y[1]];
                (ColVec T, Matrix Y) = Ode45(dydt, [20, 20], [0, 15]);
                Plot(T, Y, Linewidth: 2);
                SaveAs("Predator_Prey_Model.png");
            }
            /// </code>
            /// </example 5>
            ///
            /// <example 6>
            /// <code>
            {
                // Blausius Boundary Layer

                // define function
                double[] dydt(double t, double[] y) =>
                    [y[1], y[2], -0.5 * y[2] * y[0]];

                // set time span
                double[] tspan = [0, 6];

                double[] y0 = [0, 0, 0.5];
                (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);

                // plot the result
                Plot(T, Y, Linewidth: 2);
                Legend(["f", "f'", "f''"], UpperLeft);
                Axis([0, 6, 0, 2]); Xlabel("η"); 
                Title("Blasius Boundary Layer");
                SaveAs("Blasius_Boundary_Layer.png");
            }
            /// </code>
            /// </example 6>

            /// </BookContent>
        }

    }
}
