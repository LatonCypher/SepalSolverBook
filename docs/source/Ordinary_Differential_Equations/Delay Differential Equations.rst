Delay Differential Equations
============================

1. What is a Delay Differential Equation (DDE)?
-----------------------------------------------
A Delay Differential Equation (DDE) is a differential equation in which the derivative of the unknown function at any given time depends on the state of the system at earlier times. While standard ODEs assume instantaneous reactions, real-world physical, biological, and engineering systems inherently experience transport delays, reaction lags, and latency.

- The Current State: :math:`y(t)`: Represents the present value of the system variable (e.g., current population, reactor concentration, or valve position).
- The Delayed State: :math:`y(t - \tau)`: Represents the state of the system at a past time, shifted by the delay parameter :math:`\tau`.
- The Derivative: :math:`\cfrac{dy}{dt}`: Represents the rate of change, dictated by both present and past states: :math:`\cfrac{dy}{dt} = f(t, y(t), y(t - \tau))`.

In classical ODEs, the state space is finite-dimensional. In DDEs, because the solution on an interval depends on an entire past continuum, the system becomes an **infinite-dimensional** dynamical system.

2. Types of Delay Equations: Retarded vs. Neutral
-------------------------------------------------
Delay differential equations are classified based on where the time lag appears:

**1. Retarded DDEs (Standard Delays)**
The delay appears only in the state variables:
:math:`y'(t) = f(t, y(t), y(t - \tau))`
As time moves forward past the delay interval, derivative discontinuities gradually smooth out (:math:`C^0 \to C^1 \to C^2 \dots`).

**2. Neutral DDEs (NDDEs)**
The delay appears in both the state variable and the derivative itself:
:math:`y'(t) = f(t, y(t), y(t - \tau), y'(t - \tau_p))`
Neutral equations govern wave propagation, transmission lines, and fluid piping networks. Discontinuities in derivatives do **not** smooth out over time; they propagate across the entire integration domain.

3. The History Segment: Beyond Single Initial Values
----------------------------------------------------
In an ODE initial value problem, specifying a single starting point :math:`y(t_0) = y_0` is sufficient. 
For a DDE with delay :math:`\tau`, evaluating the equation at :math:`t = t_0` requires past information :math:`y(t_0 - \tau)`.

Consequently, an Initial Value Problem for a DDE requires an **Initial History Function** defined over the entire pre-simulation window:
:math:`y(t) = \phi(t), \quad \text{for } t \le t_0 \quad (\text{or } t \in [t_0 - \tau, t_0])`

If the initial derivative :math:`\phi'(t_0^-)` does not match the initial rate dictated by the equation :math:`f(t_0, \phi(t_0), \phi(t_0 - \tau))`, a **jump discontinuity** is born at :math:`t = t_0` and echoes at :math:`t = t_0 + \tau, t_0 + 2\tau, \dots`.

4. Numerical Solution: The Method of Steps & Spline Interpolation
-----------------------------------------------------------------
Numerical solvers like SepalSolver's ``Dde23`` and ``Dde45`` solve DDEs by extending adaptive Runge-Kutta pairs with continuous polynomial extensions:

1. **Step-by-Step Advance:** The solver integrates forward using adaptive time steps :math:`h`.
2. **Continuous Hermite Extension:** At every accepted step, the solver maintains a :math:`C^1`-continuous cubic or quintic Hermite spline across past points.
3. **Dense Lag Lookups:** When intermediate Runge-Kutta stages evaluate :math:`y(t - \tau)` or :math:`y'(t - \tau_p)`, the values are interpolated accurately from the history ring buffer.


Examples
~~~~~~~~

.. admonition:: Example 1 :  Delayed Logistic Equation (Hutchinson's Equation)

   | Solve the delayed logistic population model: :math:`\cfrac{dy}{dt} = r\,y(t)\left(1 - \cfrac{y(t - \tau)}{K}\right)`,
   | with growth rate :math:`r = 1.5`, carrying capacity :math:`K = 10`, delay :math:`\tau = 1.0`,
   | initial history: :math:`y(t) = 2.0` for :math:`t \le 0`,
   | over the interval: :math:`t \in [0, 20]`.
   
   .. code-block:: csharp
   
      // Parameters
      double r = 1.5, K = 10.0;
      double tau = 1.0;
   
      // Define DDE: ydel represents y(t - tau)
      double ddefun(double t, double y, double ydel, double ypdel) 
          => r * y * (1.0 - ydel / K);
   
      // Constant history for t <= 0
      double yhistory = 2.0;
   
      // Time span
      double[] tspan = [0, 20];
   
      // Solve using Dde45 (retarded DDE, so delyp is null)
      (ColVec T, ColVec Y, _) = Dde45(ddefun, yhistory, tspan, dely: tau);
   
      // Plot results
      Plot(T, Y, Linewidth: 2);
      Title("Delayed Logistic Population Model (Limit Cycle Oscillation)");
      Xlabel("Time t");
      Ylabel("Population y(t)");
      SaveAs("Delayed_Logistic_Model.png");
   
   
   .. figure:: images/Delayed_Logistic_Model.png
      :align: center
      :alt: Delayed_Logistic_Model.png
   


.. admonition:: Example 2 :  Linear Feedback System with Time Lag

   | Solve the damped oscillator with delayed state feedback: :math:`\cfrac{dy}{dt} = -2y(t) - 1.5y(t - 1)`,
   | with constant initial history: :math:`y(t) = 1.0` for :math:`t \le 0`,
   | over the interval: :math:`t \in [0, 15]`.
   
   .. code-block:: csharp
   
      // Define DDE
      double ddefun(double t, double y, double ydel, double ypdel) 
          => -2.0 * y - 1.5 * ydel;
   
      // Constant history for t <= 0
      double yhistory = 1.0;
      double tau = 1.0;
      double[] tspan = [0, 15];
   
      // Solve using Dde45
      (ColVec T, ColVec Y, _) = Dde45(ddefun, yhistory, tspan, dely: tau);
   
      // Plot the results
      Plot(T, Y, Linewidth: 2);
      Title("Delayed Linear Feedback: y' = -2y(t) - 1.5y(t - 1)");
      Xlabel("Time t");
      Ylabel("State y(t)");
      SaveAs("Delayed_Linear_Feedback.png");
   
   
   .. figure:: images/Delayed_Linear_Feedback.png
      :align: center
      :alt: Delayed_Linear_Feedback.png
   


.. admonition:: Example 3 :  Neutral Delay Differential Equation (NDDE)

   | Solve the neutral DDE with both state and derivative lags:
   | :math:`\cfrac{dy}{dt} = 1 + y(t) - 2\left[y\left(\cfrac{t}{2}\right)\right]^2 - y'(t - \pi)`,
   | with functional history: :math:`y(t) = \cos(t)` for :math:`t \le 0`,
   | over the interval: :math:`t \in [0, 3\pi]`.
   | *Analytical exact solution:* :math:`y(t) = \cos(t)`
   
   .. code-block:: csharp
   
      // Dynamic state delay: tau1(t) = t / 2
      double dely(double t, double y) => t / 2.0;
   
      // Dynamic derivative delay: tau2(t) = t - pi
      double delyp(double t, double y) => t - pi;
   
      // Functional history for t <= 0
      double yhistory(double t) => Cos(t);
   
      // Neutral DDE formulation
      double ddefun(double t, double y, double ydel, double ypdel)
          => 1.0 + y - 2.0 * Pow(ydel, 2) - ypdel;
   
      double[] tspan = [0, 3 * pi];
   
      // Solve using Dde45
      (ColVec T, ColVec Y, _) = Dde45(ddefun, yhistory, tspan, dely, delyp);
   
      // Plot numerical result against exact analytical curve
      Scatter(T, Y, "ro", 10); HoldOn();
      Plot(T, Cos(T), "b-"); HoldOff();
   
      Title("Neutral DDE: Comparison with Exact Solution cos(t)");
      Xlabel("Time t");
      Ylabel("Solution y(t)");
      Legend(["Dde45", "Exact cos(t)"]);
      SaveAs("Neutral_DDE_Verification.png");
   
   
   .. figure:: images/Neutral_DDE_Verification.png
      :align: center
      :alt: Neutral_DDE_Verification.png
   


.. admonition:: Example 4 :  Coupled 2D System with Multiple Delays

   | Solve the 2-dimensional coupled delay system:
   | :math:`\cfrac{dy_1}{dt} = -2\,y_1(t - 2) + y_2(t)`
   | :math:`\cfrac{dy_2}{dt} = y_1(t) - 2\,y_2(t - 1)`
   | with discrete delays :math:`\tau_1 = 2, \tau_2 = 1`,
   | constant initial vector: :math:`y(t) = [0.1, 0.5]^T` for :math:`t \le 0`,
   | over the interval: :math:`t \in [0, 10]`.
   
   .. code-block:: csharp
   
      // 1. Initial history vector for t <= 0
      ColVec YHistory = new double[] { 0.1, 0.5 };
   
      // 2. State delay vector [tau1, tau2]
      ColVec dely = new double[] { 2.0, 1.0 };
      ColVec delyp = null; // No derivative delay
   
      // 3. Define 2D coupled vector field
      ColVec ddefun(double t, ColVec y, Matrix ydel, Matrix ypdel)
      {
          ColVec ydel1 = ydel[.., 0]; // y(t - 2)
          ColVec ydel2 = ydel[.., 1]; // y(t - 1)
   
          return new double[] {
              -2.0 * ydel1[0] + y[1],
               y[0] - 2.0 * ydel2[1]
          };
      }
   
      // 4. Solve using vector Dde45
      double[] tspan = [0, 10];
      (ColVec T, Matrix Y, _) = Dde45(ddefun, YHistory, tspan, dely, delyp);
   
      // 5. Plot state trajectories
      Plot(T, Y, Linewidth: 2);
      Title("Coupled 2D Delay System with Multiple Lags");
      Xlabel("Time t");
      Ylabel("State Variables");
      Legend(["y_1(t)", "y_2(t)"]);
      SaveAs("Coupled_2D_Delay_System.png");
   
   
   .. figure:: images/Coupled_2D_Delay_System.png
      :align: center
      :alt: Coupled_2D_Delay_System.png
   
All ode and dde solvers return a third argument (fourth argument in case of Ode32i, Ode43i, and Ode85i) which can be used to 
obtain a denser solution using the Deval function. The Deval function evaluates the solution at specified time points, providing 
a smooth representation of the solution trajectory. The Deval function is particularly useful for plotting and analyzing the 
solution in greater detail, especially when the solver's output is sparse or when specific time points of interest are required.

This is demonstrated in the following example.

.. admonition:: Example 5 :  Mackey-Glass Physiological Delay Equation

   | The following example demonstrates a classic application of delay differential 
   | equations in modeling physiological processes, specifically the Mackey-Glass equation, 
   | which is known for exhibiting chaotic behavior due to the presence of delays.
   | :math:`\cfrac{dy}{dt} = \cfrac{\beta_0\,y(t - \tau)}{1 + [y(t - \tau)]^n} - \gamma\,y(t)`,
   | with parameters :math:`\beta_0 = 0.2`, :math:`\gamma = 0.1`, :math:`n = 10`, delay :math:`\tau = 17.0`,
   | constant history: :math:`y(t) = 0.5` for :math:`t \le 0`,
   | over the interval: :math:`t \in [0, 300]`.
   | *(This benchmark exhibits deterministic delay-induced chaos).*
   
   .. code-block:: csharp
   
      // Model parameters
      double beta0 = 0.2, gamma = 0.1, n = 10.0;
      double tau = 17.0;
   
      // Mackey-Glass equation
      double ddefun(double t, double y, double ydel, double ypdel)
          => (beta0 * ydel) / (1.0 + Pow(ydel, n)) - gamma * y;
   
      double yhistory = 0.5;
      double[] tspan = [0, 300];
   
      // Solver options with statistics enabled
      var opts = Ddeset(Stats: true, RelTol: 1e-6, AbsTol: 1e-8);
   
      // Solve using Dde45
      (var T, var Y, var result) = Dde45(ddefun, yhistory, tspan, dely: tau, options: opts);
      var Ydel = Interp1(T, Y, T - tau);
   
      // Plot chaotic time-series
      Subplot(2, 2, 0);
      Plot(T, Y, "r", 3); GridOn();
      Title("Mackey-Glass Chaotic Attractor: tau = 17");
      Xlabel("Time t"); Ylabel("Concentration y(t)");
      // Phase-space reconstruction using delay embedding
      Subplot(2, 2, 1);
      Plot(Ydel, Y, "b", 2); GridOn();
      Title("Phase-Space Reconstruction");
      Xlabel("y(t - tau)"); Ylabel("y(t)");
   
   
      ColVec Tsmooth = Linspace(0, tspan[^1], 3000);
      var Ysmooth = Deval(result, Tsmooth);
      var Ydelsmooth = Deval(result, Tsmooth - tau);
      Subplot(2, 2, 2);
      Plot(Tsmooth, Ysmooth, "r", 3); GridOn();
      Title("Mackey-Glass Chaotic Attractor: tau = 17");
      Xlabel("Time t"); Ylabel("Concentration y(t)");
      // Phase-space reconstruction using delay embedding
      Subplot(2, 2, 3);
      Plot(Ydelsmooth, Ysmooth, "b", 2); GridOn();
      Title("Phase-Space Reconstruction");
      Xlabel("y(t - tau)"); Ylabel("y(t)");
   
   
      SaveAs("Mackey_Glass_Chaotic.png", 1000, 1000);
   
   
   

Ouput

   
   .. terminal::
   
      Summary of statistics by Dde45
              164 successful steps
              34 failed attempts
              1188 function evaluations
      
   
   .. figure:: images/Mackey_Glass_Chaotic.png
      :align: center
      :alt: Mackey_Glass_Chaotic.png
   

