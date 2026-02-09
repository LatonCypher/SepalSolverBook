Implicit Differential Equations
===============================

Implicit differential equations (IDEs) are a fascinating, if slightly rebellious, branch of calculus. While most standard differential equations are "explicit"—meaning you can neatly isolate the derivative on one side—IDEs keep things tangled.

Think of it like the difference between a recipe that says "Add 2 cups of flour"(explicit) and one that says "The amount of flour plus the amount of sugar must equal 5 cups" (implicit). You know the relationship, but you have to do some work to find the specific values.

What Makes an Equation Implicit?
--------------------------------

In a standard explicit first-order ODE, we write:

.. math::

   \frac{dy}{dx} = f(x, y)


In an **implicit differential equation**, the derivative is embedded within a function where it cannot be (or simply isn't) isolated:


.. math::

   F(x, y, \frac{dy}{dx}) = 0


Why Use Them?
-------------

* **Physics & Constraints:** Many physical systems are governed by constraints (like a bead sliding on a wire) where the relationship between position and velocity is fixed by the geometry, not a direct formula.
* **Singularities:** IDEs can describe behaviors where the derivative might become undefined or "multi-valued"(where one  point has multiple possible slopes).
* **Differential-Algebraic Equations(DAEs):** These are a subset of IDEs often used in electrical circuit simulation and multi-body dynamics.

Solving Strategies
------------------
Because you can't always "solve for :math:`y'`," the approach changes:

1. **Implicit Differentiation:** If you have an equation like, :math:`x^2 + y^2 = 1`, you differentiate every term with respect to, treating  as a function of: :math: `2x + 2y \frac{dy}{dx} = 0`
Then, you isolate :math:`\cfrac{dy}{dx}` if possible.

2. **Direction Fields:** You can still visualize these equations! For any point, you solve the algebraic equation :math:`F(x,y,y') = 0` for :math:`y'`. If there are multiple solutions for :math:`y'`, the slope field might have overlapping segments.

3. **Numerical Solvers:** For complex IDEs or DAEs, standard solvers like Runge-Kutta might struggle.Specialized algorithms(like the Backward Differentiation Formula, or Diagonally implicit rungekutta) are used to handle the "stiffness" of these equations.

A Classic Example: Clairaut's Equation
--------------------------------------
One of the most famous IDEs is **Clairaut's Equation**: :math:`y = x \frac{dy}{dx} + f\left(\cfrac{dy}{dx}\right)`. This equation is unique because it often yields two types of solutions: a family of straight lines(the general solution) and a "singular solution" that acts as an envelope to those lines.

Numerical Solution
------------------
SepalSolver's Ode45i can handle implicit equations, but you need to provide the function in the form :math:`F(x, y, y') = 0`. Here's a simple example of how to set up and solve an implicit equation using SepalSolver:
To solve the clairaut's equation, we can rearrange it to fit the form :math:`F(x, y, y') = 0`: ie
, :math:`F(x, y, y') = y - x y' - f(y')`.


.. Admonition:: Example 1 :  Solving Clairaut's Equation :math:`y = x y' + \left(y'\right)^2`

   
   
   .. math::
   
      F(x, y, y') = y - x y' - \left(y'\right)^2, \quad y(0) = 1
   
   
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
   
      // Axis label and legend
      Xlabel("x"); Ylabel("y"); 
      Legend(["Numerical Solution", "Analytical Solution"], LowerRight);
   
      // Save the plot
      SaveAs("Clairaut.png");
   
   
   .. figure:: images/Clairaut.png
      :align: center
      :alt: Clairaut.png
   


.. Admonition:: Example 2 :  Solve Weissinger implicit ODE :math:`y = x^n f(y') + g(y')`

   While Clairaut's equation is a textbook classic, **Weissinger’s Implicit Differential Equation** takes things a step further into the realm of higher-degree implicit equations. It is specifically a first-order equation where the derivative  is raised to a power, but it maintains a structure that allows for a clever substitution method.
   The general form of a Weissinger equation is:
   :math:`y = x^n f(y') + g(y')`
   
   In many contexts, particularly in the study of aerodynamics(where Weissinger’s name is prominent due to his work on lifting-line theory), you might see specialized versions of this.However, in pure mathematics, it is often treated as a generalization of d'Alembert’s equation.
   
   1. Structure and Characteristics
   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
   Unlike a standard ODE, the Weissinger equation is **nonlinear in the derivative**.
   
   * **Relationship to Clairaut:** If you set :math:`n = 1` and :math:`f(y') = y'`, you essentially return to the Clairaut form.
   * **The Power of x:** The :math:`x^n` term dictates how the geometry of the solution curves scales as you move away from the origin.
   
   2. The Solution Strategy: Parameterization
   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
   To solve a Weissinger equation, we rarely try to isolate  algebraically.Instead, we use a parameter, where:
   :math:`p = y' = \frac{dy}{dx}`
   substituting :math:`p` into the equation gives:
   :math:`y = x^n f(p) + g(p)`
   To find the relationship between :math:`x` and :math:`p`, we differentiate the entire equation with respect to :math:`x`:
   
   .. math::
   
      \cfrac{dy}{dx} = nx^{n-1} f(p) + x^n f'(p) \cfrac{dp}{dx} + g'(p) \cfrac{dp}{dx}
   
   
   Since :math:`\cfrac{dy}{dx} = p` , we get a **linear differential equation for  in terms of** :math:`p`:  
   
   .. math::
   
      p  = nx^{n-1} f(p) + \left[x^n f'(p) + g'(p) \right] \cfrac{dp}{dx}
   
   
   This transformation is powerful because it turns a difficult implicit equation into a linear one(usually of the Bernoulli type or similar), which we can solve to get :math:`x(p)`. Once you have :math:`x(p)` and :math:`y(fp)`, you have a** parametric solution** to the original ODE.
   
   3. Why Weissinger Equations Matter
   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
   Weissinger's work is most famous in **fluid dynamics**, specifically the **Weissinger Area Rule** and his "L-method" for calculating lift distribution on swept wings.
   In these engineering contexts, implicit equations arise because the induced downwash(the change in airflow direction) depends on the lift, but the lift itself is a function of that downwash.
   
   Applications include:
   ~~~~~~~~~~~~~~~~~~~~~
   * **Aerodynamics:** Modeling the circulation around wings with non-rectangular shapes.
   * **Classical Mechanics:** Describing trajectories where the velocity constraint is non-linear.
   * **Singularities:** Just like Clairaut equations, Weissinger equations often have "envelope" solutions where the uniqueness of the solution breaks down.
   
   Consider :math:`F(t, y, y') = ty^2(y')^3 - y^3(y')^2 + t(t^2 + 1)y' - t^2y = 0`
   
   In this case, fix the initial value :math:`y(t_0) = \sqrt{\cfrac{3}{2}}` and let decic compute a consistent initial value for the derivative :math:`y'(t_0)`, starting from an initial guess of :math:`y'(t_0) = 0`.
   
   
   .. code-block:: csharp
   
      // Define the implicit function F(t, y, yp) = 0
      double F(double t, double y, double yp) => t*y*y*yp*yp*yp - y*y*y*yp*yp + t*(t*t + 1)*yp - t*t*y;
      
      // Initial conditions y(t0) = sqrt(3/2), and guess for y'(t0) = 0
      double t0 = 1, y0 = Sqrt(3.0/2.0), yp0 = 0;
      
      // Compute y'(t0) using decic
      (y0, yp0) = decic(F, t0, y0, 1, yp0, 0);
      
      // Now solve the implicit ODE using Ode45i
      (ColVec T, ColVec Y) = Ode45i(F, (y0, yp0), [t0, 5]);
      
      // Plot the results
      Scatter(T, Y, "fob"); HoldOn();
   
      // Add analytical solution for comparison 
      Plot(T, Sqrt(T.Pow(2) + 0.5), "r"); HoldOff();
   
      // Axis label and legend
      Xlabel("t"); Ylabel("y"); 
      Legend(["Numerical Solution", "Analytical Solution"], LowerRight);
   
      // Save the plot
      SaveAs("Weissinger.png");
   
   
   .. figure:: images/Weissinger.png
      :align: center
      :alt: Weissinger.png
   


.. Admonition:: Example 3 :  Robertson differential equation in implicit form

   Here we reformulate the robertson ode as a pully implicit system of differential algebraic equations
   
   .. math::
   
      \begin{array}{rcl}
      y'_1 &=& -0.04y_1 + 10^4 y_2 y_3 \\
      y'_2 &=&  0.04y_1 - 10^4 y_2 y_3 -(3 \times 10^7) y_2^2 \\
      y'_3 &=&  (3 \times 10^7) y_2^2
      \end{array}
   
   We previously solved this system of ODEs to steady state with the initial conditions :math:`y_1 = 1`, :math:`y_2 = 0`, and :math:`y_3 = 0`.
   
   But the equations also satisfy a linear conservation law,
   
   .. math::
   
      `y'_1 + y'_2 + y'_3 = 0`
   
   In terms of the solution and initial conditions, the conservation law is
   
   .. math::
   
      `y_1 + y_2 + y_3 = 1.0`
   
   
   The problem can be rewritten as a system of DAEs by using the conservation law to determine the state of :math:`y_3`. This reformulates the problem as the implicit DAE system
   
   .. math::
   
      \begin{array}{rcl}
      0 &=& y'_1 + 0.04y_1 - 10^4 y_2y_3 \\ 
      0 &=& y'_2 - 0.04y_1 + 10^4 y_2y_3 + (3 \times 10^7)y_2^2\\ 
      0 &=& y_1 + y_2 + y_3 - 1.
      \end{array}
   
   
   
   .. code-block:: csharp
   
      //define ODE
      double[] robertsonimplicit(double t, double[] y, double[] yp) =>
          [yp[0] + 0.04 * y[0] - 1e4 * y[1]*y[2],
           yp[1] - 0.04 * y[0] + 1e4 * y[1]*y[2] + 3e7*y[1]*y[1],
           y[0] + y[1] + y[2] - 1];
   
      double[] y0 = [1, 0, 0.001], yp0 = [0, 0, 0];
   
      (y0, yp0) = decic(robertsonimplicit, 0, y0, [1, 1, 0], yp0, [0, 0, 1]);
   
      //Solve ODE
      (ColVec T, Matrix Y) = Ode45i(robertsonimplicit, (y0, yp0), [0, 4e6]);
      // Plot the result
      Y[.., 1] = 1e4*Y[.., 1];
      SemiLogx(T, Y);
      Xlabel("Time t"); Ylabel("Soluton y");
      Legend(["y_1", "1e4*y_2", "y_3"], UpperLeft);
      Title("Solution of implicit Robertson's ODE with ODE45i");
      SaveAs("Implicit Robertson-ODE-Ode45s.png");
   
   
   .. figure:: images/Implicit Robertson-ODE-Ode45s.png
      :align: center
      :alt: Implicit Robertson-ODE-Ode45s.png
   

