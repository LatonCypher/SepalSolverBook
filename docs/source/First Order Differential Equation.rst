First Order Differential Equation
=============================================

# Chapter 8: Ordinary Differential Equations (ODEs)
1. What is a Differential Equation?
A differential equation(DE) is a mathematical equation that relates a function with its derivatives. In simpler terms, it describes how a quantity changes in relation to its current state.
- The Function(y): Represents the "state" of a system(e.g., the position of a car, the temperature of a room).
- The Derivative :math:`\frac{dt}{dy}` Represents the "rate of change"(e.g., the speed of the car, how fast the room is cooling).

An ODE is "Ordinary" because the unknown function depends on only one independent variable(usually time t or distance x).

2. The Intuition: The Slope Field
If you have an equation like :math:`\frac{dt}{dy} = y`, the equation is telling you: "The steeper the graph gets, the higher the value of y must be."
Even before solving an equation, we can visualize it using a Slope Field(or Direction Field). At every point(t, y) on a graph, we draw a tiny line segment with the slope dictated by the differential equation. A solution to the ODE is simply a curve that "follows the arrows."

3. Analytical vs. Numerical Solutions
*Analytical Solutions (The "Exact" Way)*
- example: For :math:`\frac{dt}{dy} = ky`, the analytical solution is :math:`y(t) = Ce^{kt}`
- Pros: Perfectly accurate; gives you a formula you can use forever.
- Cons: Most complex equations in physics and engineering cannot be solved this way.

*Numerical Solutions (The "Approximate" Way)*
- Pros: Can solve almost any equation, no matter how complex.
- Cons: Always contains a small amount of "truncation error" because we are approximating a smooth curve with small straight lines.

4. The Anatomy of an Initial Value Problem (IVP)
The Equation: :math:`\frac{dt}{dy} = f(t, y)` (The rule of change), The Initial Condition: :math:`y(0)=y_0` (The starting point).
Without a starting point, a differential equation has an infinite number of solutions (a "family" of curves). The initial condition picks the specific path the system takes.
Numerical methods are essential because most real-world ordinary differential equations (ODEs) cannot be solved analytically. Instead of finding a continuous formula for :math:`y(x)`, we calculate discrete values at specific points.

This guide covers the use of sepalsolver for solving an Initial Value Problem (IVP) defined by:
:math:`\frac{dy}{dt} = f(t, y), \quad y(t_0) = y_0`
where :math:`f(t, y)` is a function that defines the rate of change of :math:`y` with respect to :math:`t`, and :math:`y_0` is the initial value of :math:`y` at time :math:`t_0`.

.. admonition:: Example 1

    Solve the first-order ODE :math:`\frac{dy}{dt} = -2y` with the initial condition :math:`y(0) = 1` over the interval :math:`t \in [0, 5]`.

    .. code-block:: csharp

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

    .. figure:: images/First_Order_ODE_Solution.png
        :align: center
        :alt: First_Order_ODE_Solution.png

.. admonition:: Example 2

    Solve the first-order ODE :math:`\frac{dy}{dt} = \sin(t) - y` with the initial condition :math:`y(0) = 0` over the interval :math:`t \in [0, 10]`.

    .. code-block:: csharp

        double dydt(double t, double y) => Sin(t) - y;
        double y0 = 0;
        double[] tspan = [0, 10];
        (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
        Plot(T, Y, Linewidth: 2);
        Title("Solution of dy/dt = sin(t) - y with y(0) = 0");
        Xlabel("Time t");
        Ylabel("Function y");
        SaveAs("First_Order_ODE_Solution_example2.png");

    .. figure:: images/First_Order_ODE_Solution_example2.png
        :align: center
        :alt: First_Order_ODE_Solution_example2.png

.. admonition:: Example 3

    Solve a second order ODE (simple harmonic oscillator) by first converting to system of first order equations:

    .. math:: \frac{d^2y}{dt^2} = -4y
    .. math:: y_0 = 0; y'_0 = 5; t = [0, 10]

    .. code-block:: csharp

        double[] dydt(double t, double[] y) =>
            [y[1], -4*y[0]];
        (ColVec T, Matrix Y) = Ode45(dydt, [0,5], [0,10]);
        Plot(T, Y, Linewidth: 2);
        SaveAs("Simple_Harmonic_Oscillator.png");

    .. figure:: images/Simple_Harmonic_Oscillator.png
        :align: center
        :alt: Simple_Harmonic_Oscillator.png

.. admonition:: Example 4

    Harmonic oscillator with damping:

    .. math:: m\frac{d^2y}{dt^2} + c\frac{dy}{dt} + ky = 0
    .. math:: y_0 = 0.7; y'_0 = 0; t = [0, 30]

    .. code-block:: csharp

        double stiffness = 3.5, damping = 0.5,
            mass = 2.0, k = stiffness/mass, c = damping/mass;
        double[] dydt(double t, double[] y) =>
            [y[1], -(k*y[0] + c*y[1])];
        (ColVec T, Matrix Y) = Ode45(dydt, [0.7, 0], [0, 30]);
        Plot(T, Y, Linewidth: 2);
        SaveAs("Damped_Harmonic_Oscillator.png");

    .. figure:: images/Damped_Harmonic_Oscillator.png
        :align: center
        :alt: Damped_Harmonic_Oscillator.png
