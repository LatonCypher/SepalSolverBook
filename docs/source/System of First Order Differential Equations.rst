System of First Order Differential Equations
============================================


Introduction
------------
A system of first‑order differential equations involves multiple dependent variables, each with its own derivative, coupled together. Many physical, biological, and engineering models naturally lead to such systems.

Examples:

- Predator–prey dynamics(Lotka–Volterra)
- Coupled oscillators
- Chaotic systems(Lorenz attractor)

SepalSolver provides tools to solve these systems numerically using , just as with single ODEs, but the function signature changes to accept and return arrays.

General Form
------------
A system of :math:`n` first‑order ODEs can be written as:


.. math::

   \begin{array}{c}
   \frac{dy_1}{dt} = f_1(t, y_1, y_2,\dots, y_n) \\
   \frac{dy_2}{dt} = f_2(t, y_1, y_2,\dots, y_n) \\
   \vdots \\
   \frac{dy_n}{dt} = f_n(t, y_1, y_2,\dots, y_n)
   \end{array}


In SepalSolver, this is represented by a function:

.. code-block:: csharp

   // Define the ODE as a function
   static extern double[] dydt(double t, double[] y);

where :math:`y` is the vector of dependent variables.



.. Admonition:: Example 1 :  Simple Harmonic Oscillator

   
   :math:`y_1'=y_2,\quad y_2'=-y_1`
   
   This represents a simple harmonic oscillator written as a system.
   
   .. code-block:: csharp
   
      // Define the ODE as a function
      double[] dydt(double t, double[] y) => [ y[1], -y[0] ];
      // Initial condition
      double[] y0 = [1.0, 0.0];
      // Time span
      double[] tspan = [0, 20];
      // Solve the ODE using Ode45
      (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
      // Plot the results
      Plot(T, Y, Linewidth: 2);
      Xlabel("Time t");
      Legend(["y1", "y2"], UpperLeft);
      Title("Simple Harmonic Oscillator");
      SaveAs("Simple_Harmonic_Oscillator.png");
   
   
   .. figure:: images/Simple_Harmonic_Oscillator.png
      :align: center
      :alt: Simple_Harmonic_Oscillator.png
   
