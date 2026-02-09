Implicit Differential Equations
===============================

Implicit differential equations (IDEs) are a fascinating, if slightly rebellious, branch of calculus. While most standard differential equations are "explicit"—meaning you can neatly isolate the derivative on one side—IDEs keep things tangled.

Think of it like the difference between a recipe that says "Add 2 cups of flour"(explicit) and one that says "The amount of flour plus the amount of sugar must equal 5 cups" (implicit). You know the relationship, but you have to do some work to find the specific values.

What Makes an Equation Implicit?
--------------------------------

In a standard explicit first-order ODE, we write:

:math:`\frac{dy}{dx} = f(x, y)`

In an **implicit differential equation**, the derivative is embedded within a function where it cannot be (or simply isn't) isolated:
:math:`F(x, y, \frac{dy}{dx}) = 0`

Why Use Them?
-------------

* **Physics & Constraints:** Many physical systems are governed by constraints (like a bead sliding on a wire) where the relationship between position and velocity is fixed by the geometry, not a direct formula.
* **Singularities:**IDEs can describe behaviors where the derivative might become undefined or "multi-valued"(where one  point has multiple possible slopes).
* **Differential-Algebraic Equations(DAEs):**These are a subset of IDEs often used in electrical circuit simulation and multi-body dynamics.

Solving Strategies
------------------
Because you can't always "solve for :math:`y'`," the approach changes:
1. **Implicit Differentiation:**If you have an equation like, :math:`x^2 + y^2 = 1`you differentiate every term with respect to, treating  as a function of :
:math: `2x + 2y \frac{dy}{dx} = 0`
Then, you isolate :math:`\frac{dy}{dx}` if possible.

2. **Direction Fields:**You can still visualize these equations! For any point, you solve the algebraic equation  for . If there are multiple solutions for , the slope field might have overlapping segments.
3. **Numerical Solvers:**For complex IDEs or DAEs, standard solvers like Runge-Kutta might struggle.Specialized algorithms(like the Backward Differentiation Formula) are used to handle the "stiffness" of these equations.

A Classic Example: Clairaut's Equation
--------------------------------------
One of the most famous IDEs is **Clairaut's Equation**: 
:math:`y = x \frac{dy}{dx} + f\left(\frac{dy}{dx}\right)`
This equation is unique because it often yields two types of solutions: a family of straight lines(the general solution) and a "singular solution" that acts as an envelope to those lines.

Numerical Solution
------------------
SepalSolver's Ode45i can handle implicit equations, but you need to provide the function in the form :math:`F(x, y, y') = 0`. Here's a simple example of how to set up and solve an implicit equation using SepalSolver:
To solve the clairaut's equation, we can rearrange it to fit the form :math:`F(x, y, y') = 0`: ie
, :math:`F(x, y, y') = y - x y' - f(y')`.


.. Admonition:: Example 1 :  Solving Clairaut's Equation :math:`y = x y' + \left(y'\right)^2`

   :math:`F(x, y, y') = y - x y' - \left(y'\right)^2`, :math: `y(0) = 1`
   First we need to compute the :math:`y'(0)` from the initial condition using decic. 
   And then we can use the computed :math:`y'(0)` to solve the equation using Ode45i.
   
   .. code-block:: csharp
   
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
      SaveAs("Clairaut.png");
   
   
   .. figure:: images/Clairaut.png
      :align: center
      :alt: Clairaut.png
   

