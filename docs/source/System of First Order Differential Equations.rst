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
   \cfrac{dy_1}{dt} = f_1(t, y_1, y_2,\dots, y_n) \\
   \cfrac{dy_2}{dt} = f_2(t, y_1, y_2,\dots, y_n) \\
   \vdots                      \\
   \cfrac{dy_n}{dt} = f_n(t, y_1, y_2,\dots, y_n) 
   \end{array}

In SepalSolver, this is represented by a function:

.. code-block:: csharp
 {
     // Define the ODE as a function
     double[] dydt(double t, double[] y) => [f1, f2, ..., fn];
 }
where :math:`y` is the vector of dependent variables.

Examples
--------
These are examples of how to use SepalSolver to solve various systems of first-order ODEs.


.. Admonition:: Example 1 :  Simple Harmonic Oscillator

   A simple harmonic oscillator can be modeled as a system of first-order ODEs:
   
   .. math::
   
      \begin{eqnarray}
      y_1' &= y_2 \\
      y_2' &= -y_1
      \end{eqnarray}
   
   
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
   



.. Admonition:: Example 2 :  Lotka–Volterra Predator–Prey

   The Lotka–Volterra equations model the dynamics between predator and prey populations. Mathemtically, it is defined as:
   
   
   .. math::
   
      \begin{eqnarray}
      x' &= \alpha x-\beta xy \\
      y' &= \delta xy-\gamma y
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
      // Define the ODE as a function
      double alpha = 1.0, beta = 0.01, delta = 0.02, gamma = 1.0;
      double[] dydt(double t, double[] y) => [
          alpha * y[0] - beta * y[0] * y[1],
          delta * y[0] * y[1] - gamma * y[1]];
      // Initial condition
      double[] y0 = [20.0, 20.0];
      // Time span
      double[] tspan = [0, 15];
      // Solve the ODE using Ode45
      (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
      // Plot the results
      Plot(T, Y, Linewidth: 2);
      Legend(["Prey", "Predator"], UpperLeft);
      Title("Lotka–Volterra Predator–Prey System");
      SaveAs("Lotka_Volterra_Predator_Prey_System.png");
   
   
   .. figure:: images/Lotka_Volterra_Predator_Prey_System.png
      :align: center
      :alt: Lotka_Volterra_Predator_Prey_System.png
   



.. Admonition:: Example 3 :  Lorenz System (Chaotic, Non‑analytical)

   The Lorenz system is a set of three coupled, first‑order ODEs that exhibit chaotic behavior:
   
   .. math::
   
      \begin{eqnarray}
      \cfrac{dx}{dt} &= \sigma (y - x) \\
      \cfrac{dy}{dt} &= x(\rho - z) - y \\
      \cfrac{dz}{dt} &= xy - \beta z
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
      // Define the ODE as a function
      double sigma = 10.0, rho = 28.0, beta = 8.0 / 3.0;
      double[] dydt(double t, double[] y) => [
          sigma * (y[1] - y[0]),
          y[0] * (rho - y[2]) - y[1],
          y[0] * y[1] - beta * y[2]];
      // Initial condition
      double[] y0 = [ 1.0, 1.0, 1.0 ];
      // Time span
      double[] tspan = [ 0, 30 ];
      // Solve the ODE using Ode45
      (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
      //  Plot the results
      Plot(T, Y, Linewidth: 2);
      Legend(["x", "y", "z"], UpperLeft);
      Title("Lorenz System (σ = 10, ρ = 28, β = 8/3)");
      Xlabel("t");
      Ylabel("state");
      SaveAs("Lorenz_System.png");
   
   
   .. figure:: images/Lorenz_System.png
      :align: center
      :alt: Lorenz_System.png
   


.. Admonition:: Example 4 :  SIR Epidemic Model

   The SIR model divides a population into three compartments: Susceptible (S), Infected (I), and Recovered (R). The model is defined by the following system of ODEs:
   
   .. math::
   
      \begin{eqnarray}
      \cfrac{dS}{dt} &= -\beta\cfrac{S I}{N} \\
      \cfrac{dI}{dt} &= \beta\cfrac{S I}{N} - \gamma I \\
      \cfrac{dR}{dt} &= \gamma I
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
      // Define the ODE as a function
      double betaSIR = 0.3, gammaSIR = 0.1, N = 1000.0;
      double[] dydt(double t, double[] y) => [
          -betaSIR * y[0] * y[1] / N,
           betaSIR * y[0] * y[1] / N - gammaSIR * y[1],
           gammaSIR * y[1]];
      // Initial condition
      double[] y0 = [999.0, 1.0, 0.0];
      // Time span
      double[] tspan = [0, 160];
      // Solve the ODE using Ode45
      (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
      //  Plot the results
      Plot(T, Y, Linewidth: 2);
      Legend(["S", "I", "R"], UpperLeft);
      Title("SIR Epidemic Model");
      Xlabel("t");
      Ylabel("population");
      SaveAs("SIR_Epidemic_Model.png");
   
   
   .. figure:: images/SIR_Epidemic_Model.png
      :align: center
      :alt: SIR_Epidemic_Model.png
   



.. Admonition:: Example 5 :  Brusselator Model

   The brusselator is a theoretical model for a type of autocatalytic reaction. It is defined by the following system of ODEs:
   
   .. math::
   
      \begin{eqnarray}
      \cfrac{dx}{dt} &= A - (B + 1)x + x^2y \\
      \cfrac{dy}{dt} &= Bx - x^2y
      \end{eqnarray}
   
   
   .. code-block:: csharp
   
      // Define the ODE as a function
      double A = 1.0, B = 3.0;
      double[] dydt(double t, double[] y) => [
          A - (B + 1.0) * y[0] + y[0] * y[0] * y[1],
          B * y[0] - y[0] * y[0] * y[1]];
   
      // Initial condition
      double[] y0 = [1.5, 1.0];
      // Time span
      double[] tspan = [0, 15];
      // Solve the ODE using Ode45
      (ColVec T, Matrix Y) = Ode45(dydt, y0, tspan);
      //  Plot the results
      Plot(T, Y, Linewidth: 2);
      Legend(["X", "Y"], UpperLeft);
      Title("Brusselator (A=1, B=3)");
      Xlabel("t");
      Ylabel("concentration");
      SaveAs("Brusselator_Model.png");
   
   
   .. figure:: images/Brusselator_Model.png
      :align: center
      :alt: Brusselator_Model.png
   
