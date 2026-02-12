Differential Algebraic Equations
================================


1. Introduction to DAEs
=======================
Differential-Algebraic Equations are a class of functional equations that contain both differential equations (describing the evolution of the system) and algebraic constraints (restricting the state space). Unlike standard Ordinary Differential Equations (ODEs), DAEs are not explicitly solved for all derivatives.

A general DAE system is expressed in the implicit form: :math:`F(t, y, y') = 0`

If the Jacobian :math:`\frac{\partial F}{\partial y'}` is non-singular, the system is essentially an implicit ODE. If it is singular, the system is a "true" DAE.

2. The Concept of Index
-----------------------
The difficulty of solving a DAE is measured by its **index**. The most common definition is the **differentiation index**: the number of times you must differentiate the algebraic constraints to express the system as a set of explicit ODEs.
* **Index 0:** An ODE.
* **Index 1:** The most common solvable DAE (e.g., the algebraic variables can be solved for directly).
* **Higher Index (2+):** These are numerically unstable and usually require index reduction techniques before solving.



3. Solving DAEs with `sepalsolver`
----------------------------------
In modern computational environments like C#, DAEs can be solved using the `SepalSolver` library, which utilizes a Mass Matrix formulation:
:math:`M y' = f(t, y)`
Where :math:`M` is a singular matrix.

---

4. Examples and Applications
----------------------------


.. Admonition:: Example 1 : 

   **Example 1: The Robertson Problem (Chemical Kinetics)**
   This is a classic stiff DAE representing the reaction of three species. It is an Index-1 DAE where the total mass is conserved via an algebraic constraint.
   
   
   
   .. code-block:: csharp
   
      double[] robertson_f(double t, double[] y) =>
          [(-0.04 * y[0] + 1e4 * y[1] * y[2]),
           (0.04 * y[0] - 1e4 * y[1] * y[2] - 3e7 * y[1]*y[1]),
           y[0] + y[1] + y[2] - 1.0];
   
      double[,] mass_f(double t, double[] y) => Diag([1, 1, 0]);
   
      double[] y0 = [1.0, 0.0, 0.0];
      (ColVec T, Matrix Y) = Ode45a(robertson_f, mass_f, y0, [0, 1e7]);
      // Plot the result
      Y[.., 1] = 1e4*Y[.., 1];
      SemiLogx(T, Y);
      Xlabel("Time t"); Ylabel("Soluton y");
      Legend(["y_1", "1e4*y_2", "y_3"], MiddleLeft);
      Title("Solution of Robertson's ODE with ODE45a");
      SaveAs("Robertson-ODE-given-points-Ode45a.png");
   
   
   .. figure:: images/Robertson-ODE-given-points-Ode45a.png
      :align: center
      :alt: Robertson-ODE-given-points-Ode45a.png
   


.. Admonition:: Example 1 :  The Simple Pendulum (Index-1)

   A pendulum in Cartesian coordinates is naturally an Index-3 DAE. We solve the stabilized Index-1 version by including velocity constraints.
   
   The position of the pendulum :math:`(x, y) must satisfy the rigid rod constraint:
   :math:`x^2 + y^2 - 1 = 0`
   
   **The Index-1 Formulation**
   To reduce the index, we differentiate the constraint twice. The second derivative introduces the accelerations :math:`x''` and :math:`y''`, allowing us to solve for the Lagrange multiplier :math:`\lambda` (tension).
   
   The resulting Index-1 system is:
   
   .. math::
   
      \begin{arra}{rcl}
      x' &=& u \\
      y' &=& v \\
      u' &=& -\lambda x \\
      v' &=& -\lambda  y - g \\
      0 &=& u^2 + v^2 - y g - \lambda
      \end{array}    
   
   
   
   
   .. code-block:: csharp
   
      double g = 9.81;
   
      // State vector y = [x, y, u, v, λ]
      double[] pendulum_f(double t, double[] y) =>
          [y[2],
           y[3],
           -y[0] * y[4],
           -y[1] * y[4] - g,
           y[2]*y[2] + y[3]*y[3] - y[1] * g - y[4]];
   
      double[,] mass_f(double t, double[] y) => Diag([1, 1, 1, 1, 0]);
   
      double[] y0 = [0, 1, 1, 0, 1 - g];
      var opts = Odeset(Stats: true, RelTol: 1e-6);
      (ColVec T, Matrix Y) = Ode45a(pendulum_f, mass_f, y0, [0, 6], opts);
      Plot(T, Y, Linewidth: 2); Xlabel("x"); Ylabel("y");
      Legend(["x", "y", "u", "v", "λ"]);
      Title("Pendulum Trajectory (DAE)");
      SaveAs("Index_1-Pendulum-Problem-Ode45a.png");
   
   
   Ouput
   
   .. terminal::
   
      Summary of statistics by Ode45a
              1321 successful steps
              14 failed attempts
              56903 function evaluations
              5340 partial derivatives
              5340 LU decompositions
              24845 solutions of linear systems
      
   
   .. figure:: images/Index_1-Pendulum-Problem-Ode45a.png
      :align: center
      :alt: Index_1-Pendulum-Problem-Ode45a.png
   

As an exercise, the reader is encouraged to solve the problem using 
this initial condition y0 = [1, 0, 0, 1, 1];



.. Admonition:: Example 2 :  Semi-Explicit DAE (The Transistor Amplifier)**

   This example mimics the "hbdae" problem from MathWorks, representing an electrical circuit with nonlinear components.
   
   The transistor amplifier circuit contains six resistors, three capacitors, and a transistor.
   .. figure:: images/Transistor.png
       :align: center
       :alt: Transistor.png
   
   - The initial voltage signal is :math:`U_e(t) = 0.4\sin(200\pi t)`.
   - The operating voltage is :math:`U_b = 6`.
   - The voltages at the nodes are given by: math:`U_i(t)(i = 1, 2, 3, 4, 5)`.
   - The values of the resistors  :math:`R_i(t)(i = 1, 2, 3, 4, 5)`. are constant, and the current through each resistor satisfies :math:`I = U/R`.
   - The values of the capacitors :math:`C_i(i = 1, 2, 3)` are constant, and the current through each capacitor satisfies :math:`I=C⋅dU/dt`.
   
   The goal is to solve for the output voltage through node 5, :math:`U_5(t)`.
   Using Kirchoff's law to equalize the current through each node (1 through 5), you can obtain a system of five equations describing the circuit:
   
   Node 1: :math:`C_1(U'_2 - U'_1) = (U_1 - U_e(t))/R_0`
   
   Node 2: :math:`C_1(U'_1 - U'_2) = (U_2 - U_b)/R_1 + U_2/R_1 + 0.01f(U_2 - U_3)`
   
   Node 3: :math:`-C_2U'_3 = U_3/R_3 - f(U_2 - U_3)`
   
   Node 4: :math:`C_3(U'_5 - U'_4) = (U_4 - U_b)/R_4 + 0.99f(U_2 - U_3)`
   
   Node 5: :math:`C_3(U'_4 - U'_5) = U_5/R_5`
   
   By extracting the coeeficients of the derivatives into a matrix, we have:
   
   .. math::
   
      \begin{pmatrix}
      -c_{1} &  c_{1} &     0    &    0     &     0    \\
      c_{1} & -c_{1} &     0    &    0     &     0    \\
      0    &   0    & -c_{ 2}  &    0     &     0    \\
      0    &   0    &    0     & -c_{ 3}  &  c_{ 3}  \\
      0    &   0    &    0     &  c_{ 3}  & -c_{ 3}
      \end{ pmatrix}
      \begin{ pmatrix}
      U'_1 \\  U'_2 \\ U'_3 \\ U'_4 \\ U'_5 
      \end{pmatrix} = 
      \begin{ pmatrix}
      (U_1 - U_e(t))/R_0 \\  
      (U_2 - U_b)/R_1 + U_2/R_1 + 0.01f(U_2 - U_3) \\ 
      U_3/R_3 - f(U_2 - U_3) \\ 
      (U_4 - U_b)/R_4 + 0.99f(U_2 - U_3) \\ 
      U_5/R_5
      \end{ pmatrix}
   
   
   
   .. code-block:: csharp
   
      double Ub = 6, R0 = 1000, R15 = 9000, alpha = 0.99,
          beta = 1e-6, Uf = 0.026, c1 = 1e-6, c2 = 2e-6, c3 = 3e-6;
      double[,] Mass(double t, double[] y) => new double[,]
      {
          {-c1,  c1,  0,   0,   0 },
          { c1, -c1,  0,   0,   0 },
          { 0,   0,  -c2,  0,   0 },
          { 0,   0,   0,  -c3,  c3},
          { 0,   0,   0,   c3, -c3}
      };
   
      double[] dudt(double t, double[] u)
      {
          double Ue = 0.4 * Sin(200 * pi * t),
                 f23 = beta * (Exp((u[1] - u[2]) / Uf) - 1);
          return [ -(Ue - u[0])/R0,
                   -(Ub/R15 - u[1]*2/R15 - (1-alpha)*f23),
                   -(f23 - u[2]/R15),
                   -((Ub - u[3])/R15 - alpha*f23),
                   u[4]/R15 ];
      }
      double[] tspan = [0, 0.1];
      double[] y0 = [0, Ub / 2, Ub / 2, Ub, 0];
   
      var opts = Odeset(RelTol: 1e-5);
      (ColVec T, Matrix Y) = Ode45a(dudt, Mass, y0, tspan, opts);
      ColVec X = T, U5 = Y["", 4];
      Scatter(X, 0.4 * Sin(200 * pi * X), "o"); HoldOn();
      Plot(X, U5, "--r"); HoldOff();
      Legend(["Input", "Output"], UpperLeft);
      Xlabel("Time t"); Ylabel("Solution y");
      Title("One Transistor Amplifier DAE Problem-Ode45a");
      SaveAs("One-Transistor-Amplifier-DAE-Problem-Ode45a.png");
   
   
   .. figure:: images/One-Transistor-Amplifier-DAE-Problem-Ode45a.png
      :align: center
      :alt: One-Transistor-Amplifier-DAE-Problem-Ode45a.png
   


.. Admonition:: Example 3 :  The Akzo Nobel Problem

   A high-dimensional DAE describing a chemical process with 6 differential and 2 algebraic equations. This tests the solver's ability to handle stiff systems with coupled variables.
   
   **Mathematical Description:**
   The system is defined by reaction rates :math:`r_i` and concentrations :math:`y_1, ..., y_8`:
   
   
   .. math::
   
      \begin{array}{rcl}
      r_1 &=& k_1 \cdot y_1^4 \cdot y_2^{0.5}\\
      r_2 &=& k_2 \cdot y_3 \cdot y_4 \\
      r_3 &=& k_2 / K \cdot y_1 \cdot y_5\\
      r_4 &=& k_3 \cdot y_1 \cdot y_4^2\\
      r_5 &=& k_4 \cdot y_6^2 \cdot y_2^{0.5}
      \end{array}
   
   
   The differential equations are:
   
   .. math::
   
      \begin{array}{rcl}
      y_1' &=& -2r_1 + r_2 - r_3 - r_4\\
      y_2' &=& -0.5r_1 - r_5 + 0.5F_{in}\\
      y_3' &=& r_1 - r_2 + r_3\\
      y_4' &=& -r_2 + r_3 - 2r_4\\
      y_5' &=& r_2 - r_3 + r_4\\
      y_6' &=& -r_5
      \end{array}
   
   
   The algebraic constraints (Equilibrium):
   
   .. math::
   
      \begin{array}{rcl}
      0 &=& y_1 \cdot y_3 - y_7\\
      0 &=& y_4 \cdot y_5 - y_8
      \end{array}
   
   
   
   .. code-block:: csharp
   
      double k1 = 18.7, k2 = 0.58, k3 = 0.09, k4 = 0.42, K = 34.4, Fin = 0.012;
      double r1(double[] y) => k1 * Pow(y[0], 4) * Pow(y[1], 0.5);
      double r2(double[] y) => k2 * y[2] * y[3];
      double r3(double[] y) => (k2 / K) * y[0] * y[4];
      double r4(double[] y) => k3 * y[0] * Pow(y[3], 2);
      double r5(double[] y) => k4 * Pow(y[5], 2) * Pow(y[1], 0.5);
   
      double[] akzo_f(double t, double[] y) =>
          [
              -2*r1(y) + r2(y) - r3(y) - r4(y),
              -0.5*r1(y) - r5(y) + 0.5*Fin,
              r1(y) - r2(y) + r3(y),
              -r2(y) + r3(y) - 2*r4(y),
              r2(y) - r3(y) + r4(y),
              -r5(y),
              y[0] * y[2] - y[6],
              y[3] * y[4] - y[7]
          ];
   
      double[,] mass_f(double t, double[] y) => Diag([1, 1, 1, 1, 1, 1, 0, 0]);
      double[] y0 = [0.444, 0.0012, 0.0, 0.0037, 0.0, 0.0, 0.0, 0.0];
      (ColVec T, Matrix Y) = Ode45a(akzo_f, mass_f, y0, [0, 180]);
      Plot(T, Y);
      Xlabel("Time"); Ylabel("Concentration");
      Title("Akzo Nobel Chemical Kinetics (DAE)");
      SaveAs("Akzo-Nobel-Ode45a.png");
   
   
   .. figure:: images/Akzo-Nobel-Ode45a.png
      :align: center
      :alt: Akzo-Nobel-Ode45a.png
   

Index-2 DAE
-----------
Most DAE solvers usually avoid solving DAEs in index 2 form. But SepalSolver is able to handle most index 2 DAEs to a relative tolerance of :math:`10^{-4}`.

Now we look at examples of index 2 DAEs


.. Admonition:: Example 4 :  

   Usnig the example from "On the numerical solution of differential–algebraic equations with index-2" by Ercan Celık
   
   .. math::
   
      \begin{aligned}
      x'_1 &= \left(\alpha - \cfrac{1}{2 - t}\right)x_1 + (2 - t)\alpha z + \cfrac{3 - t}{2 - t}x_2 \\
      x'_2 &= \cfrac{1 - \alpha}{t - 2} x_1 - x_2 + (\alpha - 1)z + 2e^t \\
      0 &= (t + 2)x_1 + (t^2 - 4)x_2 - (t^2 + t - 2)e^t
      \end{ aligned}
   
   
   Intial condition: :math:`x_1(0) = 1, x_2(0) = 1`;
   
   SepalSolver has the ability to compute consistent initial conditions for index 2 DAEs, so we can solve this problem without manually differentiating the algebraic constraint.
   
   
   .. code-block:: csharp
   
      // define the DAE
      double alpha = 10;
      double[] Ercan(double t, double[] x) =>
          [ (alpha - 1/(2-t))*x[0] + (2-t)*alpha*x[2] + (3-t)/(2-t)*x[1],
            (1-alpha)/(t-2)*x[0] - x[1] + (alpha-1)*x[2] + 2*Exp(t),
            (t+2)*x[0] + (t*t-4)*x[1] - (t*t+t-2)*Exp(t) ];
   
      double[,] mass_f(double t, double[] x) => Diag([1, 1, 0]);
      double[] y0 = [1, 1, 0]; // only the differential variables need initial conditions
      var opts = Odeset(Stats: true);
      (ColVec T, Matrix Y) = Ode45a(Ercan, mass_f, y0, [0, 1], opts);
      Scatter(T, Hcart(Exp(T), Exp(T), -Exp(T).Div(2-T)), "o"); HoldOn();
      Plot(T, Y); HoldOff();
      Xlabel("Time t"); Ylabel("Solution x");
      Legend(["x_1_Exact", "x_2_Exact", "z_Exact", "x_1_NumSol", "x_2_NumSol", "z_NumSol"]);
      Title("Index-2 DAE Example (Ercan Celık)");
      SaveAs("Index-2-DAE-Ercan-Celik.png");
   
      // We can actually print out the result to compare with the analytical solution
      Console.WriteLine("""
              t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_1_NumSol(t)  |  x_1_Exact(t)
          --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
          """);
      for (int i = 0; i < T.Numel; i++)
      {
          Console.WriteLine($"""
                {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i])/(2-T[i]):F6}
              """);
      }
   
      // We can compute the solution to a higher accuracy 
      var opts2 = Odeset(Stats: true, RelTol: 1e-6);
      (ColVec T2, Matrix Y2) = Ode45a(Ercan, mass_f, y0, [0, 1], opts2);
      Console.WriteLine(""" t || x_1_NumSol(t) | x_1_Exact(t) || x_1_NumSol(t) | x_1_Exact(t) || x_1_NumSol(t) | x_1_Exact(t) --------++-----------------+----------------++-----------------+----------------++-----------------+--------------- """);
      for (int i = 0; i < T2.Numel; i++)
      {
          Console.WriteLine($""" {T2[i]:F2} || {Y2[i, 0]:F6} | {Exp(T2[i]):F6} || {Y2[i, 1]:F6} | {Exp(T2[i]):F6} || {Y2[i, 2]:F6} | {-Exp(T2[i])/(2-T2[i]):F6} """);
      }
   
   
   Ouput
   
   .. terminal::
   
      Summary of statistics by Ode45a
              13 successful steps
              0 failed attempts
              380 function evaluations
              52 partial derivatives
              52 LU decompositions
              156 solutions of linear systems
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_1_NumSol(t)  |  x_1_Exact(t)
      --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
        0.00  ||     1.000000    |    1.000000    ||     1.000000    |    1.000000    ||     -0.500000   |  -0.500000
        0.01  ||     1.010050    |    1.010050    ||     1.010050    |    1.010050    ||     -0.507565   |  -0.507563
        0.04  ||     1.038906    |    1.038907    ||     1.038906    |    1.038907    ||     -0.529580   |  -0.529560
        0.09  ||     1.096658    |    1.096663    ||     1.096660    |    1.096663    ||     -0.574927   |  -0.574853
        0.18  ||     1.193077    |    1.193106    ||     1.193090    |    1.193106    ||     -0.654484   |  -0.654316
        0.28  ||     1.318531    |    1.318586    ||     1.318554    |    1.318586    ||     -0.765305   |  -0.765089
        0.38  ||     1.457205    |    1.457263    ||     1.457227    |    1.457263    ||     -0.897841   |  -0.897639
        0.48  ||     1.610468    |    1.610524    ||     1.610488    |    1.610524    ||     -1.057356   |  -1.057163
        0.58  ||     1.779852    |    1.779905    ||     1.779868    |    1.779905    ||     -1.250603   |  -1.250425
        0.68  ||     1.967052    |    1.967099    ||     1.967064    |    1.967099    ||     -1.486506   |  -1.486353
        0.78  ||     2.173942    |    2.173981    ||     2.173949    |    2.173981    ||     -1.777055   |  -1.776941
        0.88  ||     2.402592    |    2.402620    ||     2.402595    |    2.402620    ||     -2.138687   |  -2.138628
        0.98  ||     2.655291    |    2.655306    ||     2.655291    |    2.655306    ||     -2.594476   |  -2.594491
        1.00  ||     2.718274    |    2.718282    ||     2.718274    |    2.718282    ||     -2.718252   |  -2.718282
      Summary of statistics by Ode45a
              168 successful steps
              1 failed attempts
              5428 function evaluations
              676 partial derivatives
              676 LU decompositions
              2708 solutions of linear systems
      
       t || x_1_NumSol(t) | x_1_Exact(t) || x_1_NumSol(t) | x_1_Exact(t) || x_1_NumSol(t) | x_1_Exact(t) --------++-----------------+----------------++-----------------+----------------++-----------------+--------------- 
       0.00 || 1.000000 | 1.000000 || 1.000000 | 1.000000 || -0.500000 | -0.500000 
       0.00 || 1.000100 | 1.000100 || 1.000100 | 1.000100 || -0.500075 | -0.500075 
       0.00 || 1.000532 | 1.000532 || 1.000532 | 1.000532 || -0.500399 | -0.500399 
       0.00 || 1.001592 | 1.001592 || 1.001592 | 1.001592 || -0.501195 | -0.501195 
       0.00 || 1.003410 | 1.003410 || 1.003410 | 1.003410 || -0.502560 | -0.502560 
       0.01 || 1.005930 | 1.005930 || 1.005930 | 1.005930 || -0.504456 | -0.504456 
       0.01 || 1.009004 | 1.009004 || 1.009004 | 1.009004 || -0.506773 | -0.506773 
       0.01 || 1.012478 | 1.012478 || 1.012478 | 1.012478 || -0.509398 | -0.509397 
       0.02 || 1.016227 | 1.016227 || 1.016227 | 1.016227 || -0.512237 | -0.512236 
       0.02 || 1.020162 | 1.020162 || 1.020162 | 1.020162 || -0.515224 | -0.515224 
       0.02 || 1.024225 | 1.024225 || 1.024225 | 1.024225 || -0.518316 | -0.518316 
       0.03 || 1.028376 | 1.028376 || 1.028376 | 1.028376 || -0.521484 | -0.521484 
       0.03 || 1.032593 | 1.032593 || 1.032593 | 1.032593 || -0.524711 | -0.524711 
       0.04 || 1.036860 | 1.036860 || 1.036860 | 1.036860 || -0.527986 | -0.527986 
       0.04 || 1.041169 | 1.041169 || 1.041169 | 1.041169 || -0.531303 | -0.531302 
       0.04 || 1.045515 | 1.045515 || 1.045515 | 1.045515 || -0.534656 | -0.534656 
       0.05 || 1.049893 | 1.049893 || 1.049893 | 1.049893 || -0.538045 | -0.538045 
       0.05 || 1.054304 | 1.054304 || 1.054304 | 1.054304 || -0.541469 | -0.541468 
       0.06 || 1.058744 | 1.058744 || 1.058744 | 1.058744 || -0.544926 | -0.544925 
       0.06 || 1.063215 | 1.063215 || 1.063215 | 1.063215 || -0.548416 | -0.548416 
       0.07 || 1.067715 | 1.067715 || 1.067715 | 1.067715 || -0.551940 | -0.551940 
       0.07 || 1.072246 | 1.072246 || 1.072246 | 1.072246 || -0.555498 | -0.555497 
       0.07 || 1.076806 | 1.076806 || 1.076806 | 1.076806 || -0.559089 | -0.559089 
       0.08 || 1.081396 | 1.081396 || 1.081396 | 1.081396 || -0.562716 | -0.562715 
       0.08 || 1.086017 | 1.086017 || 1.086017 | 1.086017 || -0.566377 | -0.566376 
       0.09 || 1.090669 | 1.090669 || 1.090669 | 1.090669 || -0.570073 | -0.570073 
       0.09 || 1.095351 | 1.095351 || 1.095351 | 1.095351 || -0.573806 | -0.573805 
       0.10 || 1.100065 | 1.100065 || 1.100065 | 1.100065 || -0.577575 | -0.577574 
       0.10 || 1.104811 | 1.104811 || 1.104811 | 1.104811 || -0.581380 | -0.581380 
       0.10 || 1.109588 | 1.109588 || 1.109588 | 1.109588 || -0.585223 | -0.585223 
       0.11 || 1.114398 | 1.114398 || 1.114398 | 1.114398 || -0.589104 | -0.589103 
       0.11 || 1.119241 | 1.119241 || 1.119241 | 1.119241 || -0.593023 | -0.593023 
       0.12 || 1.124116 | 1.124116 || 1.124116 | 1.124116 || -0.596981 | -0.596981 
       0.12 || 1.129025 | 1.129025 || 1.129025 | 1.129025 || -0.600979 | -0.600979 
       0.13 || 1.133968 | 1.133968 || 1.133968 | 1.133968 || -0.605017 | -0.605016 
       0.13 || 1.138945 | 1.138945 || 1.138945 | 1.138945 || -0.609096 | -0.609095 
       0.13 || 1.143957 | 1.143957 || 1.143957 | 1.143957 || -0.613216 | -0.613215 
       0.14 || 1.149003 | 1.149003 || 1.149003 | 1.149003 || -0.617377 | -0.617377 
       0.14 || 1.154085 | 1.154085 || 1.154085 | 1.154085 || -0.621582 | -0.621581 
       0.15 || 1.159203 | 1.159203 || 1.159203 | 1.159203 || -0.625830 | -0.625829 
       0.15 || 1.164357 | 1.164357 || 1.164357 | 1.164357 || -0.630121 | -0.630121 
       0.16 || 1.169547 | 1.169547 || 1.169547 | 1.169547 || -0.634457 | -0.634457 
       0.16 || 1.174775 | 1.174775 || 1.174775 | 1.174775 || -0.638839 | -0.638838 
       0.17 || 1.180039 | 1.180039 || 1.180039 | 1.180039 || -0.643266 | -0.643265 
       0.17 || 1.185342 | 1.185342 || 1.185342 | 1.185342 || -0.647740 | -0.647739 
       0.17 || 1.190683 | 1.190683 || 1.190683 | 1.190683 || -0.652261 | -0.652260 
       0.18 || 1.196064 | 1.196064 || 1.196064 | 1.196064 || -0.656830 | -0.656830 
       0.18 || 1.201483 | 1.201483 || 1.201483 | 1.201483 || -0.661448 | -0.661448 
       0.19 || 1.206942 | 1.206942 || 1.206942 | 1.206942 || -0.666116 | -0.666116 
       0.19 || 1.212441 | 1.212441 || 1.212441 | 1.212441 || -0.670835 | -0.670834 
       0.20 || 1.217981 | 1.217981 || 1.217981 | 1.217981 || -0.675604 | -0.675604 
       0.20 || 1.223563 | 1.223563 || 1.223563 | 1.223563 || -0.680426 | -0.680425 
       0.21 || 1.229186 | 1.229186 || 1.229186 | 1.229186 || -0.685300 | -0.685300 
       0.21 || 1.234852 | 1.234852 || 1.234852 | 1.234852 || -0.690229 | -0.690228 
       0.22 || 1.240560 | 1.240560 || 1.240560 | 1.240560 || -0.695212 | -0.695211 
       0.22 || 1.246312 | 1.246312 || 1.246312 | 1.246312 || -0.700250 | -0.700250 
       0.22 || 1.252108 | 1.252108 || 1.252108 | 1.252108 || -0.705345 | -0.705345 
       0.23 || 1.257948 | 1.257948 || 1.257948 | 1.257948 || -0.710498 | -0.710497 
       0.23 || 1.263834 | 1.263834 || 1.263834 | 1.263834 || -0.715709 | -0.715708 
       0.24 || 1.269765 | 1.269765 || 1.269765 | 1.269765 || -0.720980 | -0.720979 
       0.24 || 1.275743 | 1.275743 || 1.275743 | 1.275743 || -0.726311 | -0.726310 
       0.25 || 1.281768 | 1.281768 || 1.281768 | 1.281768 || -0.731703 | -0.731703 
       0.25 || 1.287840 | 1.287840 || 1.287840 | 1.287840 || -0.737159 | -0.737158 
       0.26 || 1.293960 | 1.293960 || 1.293960 | 1.293960 || -0.742677 | -0.742677 
       0.26 || 1.300130 | 1.300130 || 1.300130 | 1.300130 || -0.748261 | -0.748261 
       0.27 || 1.306349 | 1.306349 || 1.306349 | 1.306349 || -0.753911 | -0.753911 
       0.27 || 1.312618 | 1.312618 || 1.312618 | 1.312618 || -0.759628 | -0.759628 
       0.28 || 1.318939 | 1.318939 || 1.318939 | 1.318939 || -0.765414 | -0.765413 
       0.28 || 1.325311 | 1.325311 || 1.325311 | 1.325311 || -0.771269 | -0.771268 
       0.29 || 1.331735 | 1.331735 || 1.331735 | 1.331735 || -0.777195 | -0.777194 
       0.29 || 1.338213 | 1.338213 || 1.338213 | 1.338213 || -0.783193 | -0.783193 
       0.30 || 1.344745 | 1.344745 || 1.344745 | 1.344745 || -0.789265 | -0.789265 
       0.30 || 1.351332 | 1.351332 || 1.351332 | 1.351332 || -0.795412 | -0.795412 
       0.31 || 1.357974 | 1.357974 || 1.357974 | 1.357974 || -0.801636 | -0.801635 
       0.31 || 1.364673 | 1.364673 || 1.364673 | 1.364673 || -0.807937 | -0.807936 
       0.32 || 1.371429 | 1.371429 || 1.371429 | 1.371429 || -0.814318 | -0.814317 
       0.32 || 1.378244 | 1.378244 || 1.378244 | 1.378244 || -0.820780 | -0.820779 
       0.33 || 1.385117 | 1.385117 || 1.385117 | 1.385117 || -0.827324 | -0.827323 
       0.33 || 1.392051 | 1.392051 || 1.392051 | 1.392051 || -0.833953 | -0.833952 
       0.34 || 1.399045 | 1.399045 || 1.399045 | 1.399045 || -0.840667 | -0.840666 
       0.34 || 1.406102 | 1.406102 || 1.406102 | 1.406102 || -0.847469 | -0.847468 
       0.35 || 1.413221 | 1.413221 || 1.413221 | 1.413221 || -0.854361 | -0.854360 
       0.35 || 1.420404 | 1.420404 || 1.420404 | 1.420404 || -0.861343 | -0.861343 
       0.36 || 1.427653 | 1.427653 || 1.427653 | 1.427653 || -0.868419 | -0.868419 
       0.36 || 1.434967 | 1.434967 || 1.434967 | 1.434967 || -0.875590 | -0.875590 
       0.37 || 1.442349 | 1.442349 || 1.442349 | 1.442349 || -0.882858 | -0.882858 
       0.37 || 1.449799 | 1.449799 || 1.449799 | 1.449799 || -0.890226 | -0.890225 
       0.38 || 1.457318 | 1.457318 || 1.457318 | 1.457318 || -0.897694 | -0.897694 
       0.38 || 1.464908 | 1.464908 || 1.464908 | 1.464908 || -0.905266 | -0.905266 
       0.39 || 1.472570 | 1.472570 || 1.472570 | 1.472570 || -0.912944 | -0.912944 
       0.39 || 1.480305 | 1.480305 || 1.480305 | 1.480305 || -0.920730 | -0.920730 
       0.40 || 1.488115 | 1.488115 || 1.488115 | 1.488115 || -0.928627 | -0.928626 
       0.40 || 1.496000 | 1.496000 || 1.496000 | 1.496000 || -0.936637 | -0.936636 
       0.41 || 1.503963 | 1.503963 || 1.503963 | 1.503963 || -0.944762 | -0.944761 
       0.41 || 1.512004 | 1.512004 || 1.512004 | 1.512004 || -0.953006 | -0.953005 
       0.42 || 1.520125 | 1.520125 || 1.520125 | 1.520125 || -0.961371 | -0.961370 
       0.42 || 1.528328 | 1.528328 || 1.528328 | 1.528328 || -0.969859 | -0.969859 
       0.43 || 1.536614 | 1.536614 || 1.536614 | 1.536614 || -0.978475 | -0.978474 
       0.44 || 1.544985 | 1.544985 || 1.544985 | 1.544985 || -0.987221 | -0.987220 
       0.44 || 1.553443 | 1.553443 || 1.553443 | 1.553443 || -0.996100 | -0.996099 
       0.45 || 1.561988 | 1.561988 || 1.561988 | 1.561988 || -1.005115 | -1.005114 
       0.45 || 1.570624 | 1.570624 || 1.570624 | 1.570624 || -1.014270 | -1.014269 
       0.46 || 1.579351 | 1.579351 || 1.579351 | 1.579351 || -1.023569 | -1.023568 
       0.46 || 1.588172 | 1.588172 || 1.588172 | 1.588172 || -1.033015 | -1.033014 
       0.47 || 1.597089 | 1.597089 || 1.597089 | 1.597089 || -1.042611 | -1.042610 
       0.47 || 1.606103 | 1.606103 || 1.606103 | 1.606103 || -1.052363 | -1.052362 
       0.48 || 1.615217 | 1.615217 || 1.615217 | 1.615217 || -1.062273 | -1.062272 
       0.49 || 1.624434 | 1.624434 || 1.624434 | 1.624434 || -1.072347 | -1.072346 
       0.49 || 1.633754 | 1.633754 || 1.633754 | 1.633754 || -1.082588 | -1.082588 
       0.50 || 1.643181 | 1.643181 || 1.643181 | 1.643181 || -1.093002 | -1.093001 
       0.50 || 1.652717 | 1.652717 || 1.652717 | 1.652717 || -1.103593 | -1.103593 
       0.51 || 1.662365 | 1.662365 || 1.662365 | 1.662365 || -1.114367 | -1.114366 
       0.51 || 1.672127 | 1.672127 || 1.672127 | 1.672127 || -1.125328 | -1.125327 
       0.52 || 1.682006 | 1.682006 || 1.682006 | 1.682006 || -1.136482 | -1.136481 
       0.53 || 1.692005 | 1.692005 || 1.692005 | 1.692005 || -1.147834 | -1.147833 
       0.53 || 1.702127 | 1.702127 || 1.702127 | 1.702127 || -1.159392 | -1.159391 
       0.54 || 1.712374 | 1.712374 || 1.712374 | 1.712374 || -1.171160 | -1.171159 
       0.54 || 1.722751 | 1.722751 || 1.722751 | 1.722751 || -1.183146 | -1.183145 
       0.55 || 1.733261 | 1.733261 || 1.733261 | 1.733261 || -1.195357 | -1.195356 
       0.56 || 1.743906 | 1.743906 || 1.743906 | 1.743906 || -1.207799 | -1.207798 
       0.56 || 1.754692 | 1.754692 || 1.754692 | 1.754692 || -1.220480 | -1.220479 
       0.57 || 1.765621 | 1.765621 || 1.765621 | 1.765621 || -1.233409 | -1.233408 
       0.57 || 1.776698 | 1.776698 || 1.776698 | 1.776698 || -1.246593 | -1.246592 
       0.58 || 1.787927 | 1.787927 || 1.787927 | 1.787927 || -1.260042 | -1.260041 
       0.59 || 1.799312 | 1.799312 || 1.799312 | 1.799312 || -1.273764 | -1.273763 
       0.59 || 1.810859 | 1.810859 || 1.810859 | 1.810859 || -1.287770 | -1.287769 
       0.60 || 1.822572 | 1.822572 || 1.822572 | 1.822572 || -1.302069 | -1.302068 
       0.61 || 1.834456 | 1.834456 || 1.834456 | 1.834456 || -1.316673 | -1.316672 
       0.61 || 1.846518 | 1.846518 || 1.846518 | 1.846518 || -1.331594 | -1.331593 
       0.62 || 1.858762 | 1.858762 || 1.858762 | 1.858762 || -1.346842 | -1.346841 
       0.63 || 1.871195 | 1.871195 || 1.871195 | 1.871195 || -1.362433 | -1.362432 
       0.63 || 1.883823 | 1.883823 || 1.883823 | 1.883823 || -1.378378 | -1.378377 
       0.64 || 1.896654 | 1.896654 || 1.896654 | 1.896654 || -1.394694 | -1.394693 
       0.65 || 1.909695 | 1.909695 || 1.909695 | 1.909695 || -1.411395 | -1.411394 
       0.65 || 1.922954 | 1.922954 || 1.922954 | 1.922954 || -1.428498 | -1.428497 
       0.66 || 1.936438 | 1.936438 || 1.936438 | 1.936438 || -1.446022 | -1.446021 
       0.67 || 1.950158 | 1.950158 || 1.950158 | 1.950158 || -1.463985 | -1.463984 
       0.68 || 1.964122 | 1.964122 || 1.964122 | 1.964122 || -1.482408 | -1.482407 
       0.68 || 1.978341 | 1.978341 || 1.978341 | 1.978341 || -1.501313 | -1.501312 
       0.69 || 1.992826 | 1.992826 || 1.992826 | 1.992826 || -1.520724 | -1.520723 
       0.70 || 2.007588 | 2.007589 || 2.007588 | 2.007589 || -1.540667 | -1.540666 
       0.70 || 2.022642 | 2.022642 || 2.022642 | 2.022642 || -1.561169 | -1.561168 
       0.71 || 2.038000 | 2.038000 || 2.038000 | 2.038000 || -1.582261 | -1.582260 
       0.72 || 2.053678 | 2.053678 || 2.053678 | 2.053678 || -1.603976 | -1.603975 
       0.73 || 2.069692 | 2.069692 || 2.069692 | 2.069692 || -1.626350 | -1.626349 
       0.74 || 2.086061 | 2.086061 || 2.086061 | 2.086061 || -1.649423 | -1.649421 
       0.74 || 2.102804 | 2.102804 || 2.102804 | 2.102804 || -1.673238 | -1.673236 
       0.75 || 2.119943 | 2.119943 || 2.119943 | 2.119943 || -1.697843 | -1.697842 
       0.76 || 2.137503 | 2.137503 || 2.137503 | 2.137503 || -1.723292 | -1.723290 
       0.77 || 2.155511 | 2.155511 || 2.155511 | 2.155511 || -1.749644 | -1.749643 
       0.78 || 2.173997 | 2.173997 || 2.173997 | 2.173997 || -1.776966 | -1.776965 
       0.79 || 2.192995 | 2.192995 || 2.192995 | 2.192995 || -1.805333 | -1.805332 
       0.79 || 2.212543 | 2.212543 || 2.212543 | 2.212543 || -1.834831 | -1.834830 
       0.80 || 2.232686 | 2.232686 || 2.232686 | 2.232686 || -1.865557 | -1.865555 
       0.81 || 2.253475 | 2.253475 || 2.253475 | 2.253475 || -1.897622 | -1.897621 
       0.82 || 2.274968 | 2.274968 || 2.274968 | 2.274968 || -1.931158 | -1.931156 
       0.83 || 2.297234 | 2.297234 || 2.297234 | 2.297234 || -1.966317 | -1.966315 
       0.84 || 2.320356 | 2.320356 || 2.320356 | 2.320356 || -2.003281 | -2.003279 
       0.85 || 2.344433 | 2.344433 || 2.344433 | 2.344433 || -2.042268 | -2.042267 
       0.86 || 2.369585 | 2.369585 || 2.369585 | 2.369585 || -2.083548 | -2.083546 
       0.87 || 2.395965 | 2.395965 || 2.395965 | 2.395965 || -2.127453 | -2.127452 
       0.89 || 2.423767 | 2.423767 || 2.423767 | 2.423767 || -2.174413 | -2.174412 
       0.90 || 2.453247 | 2.453247 || 2.453247 | 2.453247 || -2.224992 | -2.224991 
       0.91 || 2.484758 | 2.484758 || 2.484758 | 2.484758 || -2.279964 | -2.279962 
       0.92 || 2.518813 | 2.518813 || 2.518813 | 2.518813 || -2.340445 | -2.340443 
       0.94 || 2.556212 | 2.556212 || 2.556212 | 2.556212 || -2.408175 | -2.408174 
       0.95 || 2.598366 | 2.598366 || 2.598366 | 2.598366 || -2.486197 | -2.486196 
       0.97 || 2.648355 | 2.648355 || 2.648355 | 2.648355 || -2.581090 | -2.581088 
       1.00 || 2.718282 | 2.718282 || 2.718282 | 2.718282 || -2.718283 | -2.718282 
   
   .. figure:: images/Index-2-DAE-Ercan-Celik.png
      :align: center
      :alt: Index-2-DAE-Ercan-Celik.png
   
   
   


