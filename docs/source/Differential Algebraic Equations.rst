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
   
   The position of the pendulum :math:`(x, y)` must satisfy the rigid rod constraint: 
   :math:`x^2 + y^2 - 1 = 0`
   
   **The Index-1 Formulation**
   To reduce the index, we differentiate the constraint twice. The second derivative introduces the accelerations :math:`x''` and :math:`y''`, allowing us to solve for the Lagrange multiplier :math:`\lambda` (tension).
   
   The resulting Index-1 system is:
   
   .. math::
   
      \begin{array}{rcl}
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
   - The voltages at the nodes are given by: :math:`U_i(t)(i = 1, 2, 3, 4, 5)`.
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
      \end{pmatrix}
      \begin{pmatrix}
      U'_1 \\  U'_2 \\ U'_3 \\ U'_4 \\ U'_5 
      \end{pmatrix} = 
      \begin{pmatrix}
      (U_1 - U_e(t))/R_0 \\  
      (U_2 - U_b)/R_1 + U_2/R_1 + 0.01f(U_2 - U_3) \\ 
      U_3/R_3 - f(U_2 - U_3) \\ 
      (U_4 - U_b)/R_4 + 0.99f(U_2 - U_3) \\ 
      U_5/R_5
      \end{pmatrix}
   
   
   
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
      double Ue(double t) => 0.4 * Sin(200 * pi * t);
      double[] dudt(double t, double[] u)
      {
          double f23 = beta * (Exp((u[1] - u[2]) / Uf) - 1);
          return [ -(Ue(t) - u[0])/R0,
                   -(Ub/R15 - u[1]*2/R15 - (1-alpha)*f23),
                   -(f23 - u[2]/R15),
                   -((Ub - u[3])/R15 - alpha*f23),
                   u[4]/R15 ];
      }
      double[] tspan = [0, 0.1];
      double[] y0 = [0, Ub / 2, Ub / 2, Ub, 0];
   
      var opts = Odeset(RelTol: 1e-5);
      (ColVec T, Matrix Y) = Ode45a(dudt, Mass, y0, tspan, opts);
      Scatter(T, Arrayfun(Ue, T), "o"); HoldOn();
      Plot(T, Y[.., 4], "--r"); HoldOff();
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
   
      \begin{align}
      x'_1 &= \left(\alpha - \cfrac{1}{2 - t}\right)x_1 + (2 - t)\alpha z + \cfrac{3 - t}{2 - t}x_2 \\
      x'_2 &= \cfrac{1 - \alpha}{t - 2} x_1 - x_2 + (\alpha - 1)z + 2e^t \\
      0 &= (t + 2)x_1 + (t^2 - 4)x_2 - (t^2 + t - 2)e^t
      \end{ align}
   
   
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
              t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
          --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
          """);
      for (int i = 0; i < T.Numel; i++)
      {
          Console.WriteLine($"""
                {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i])/(2-T[i]):F6}
              """);
      }
   
      // We can compute the solution to a higher accuracy 
      Console.WriteLine("\n\nNow we compute the solution to a higher accuracy (RelTol = 1e-5):\n");
      opts = Odeset(Stats: true, RelTol: 1e-5);
      (T, Y) = Ode45a(Ercan, mass_f, y0, [0, 1], opts);
      Console.WriteLine("""
              t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
          --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
          """);
      for (int i = 0; i < T.Numel; i++)
      {
          Console.WriteLine($"""
                {T[i]:F2}  ||     {Y[i, 0]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 1]:F6}    |    {Exp(T[i]):F6}    ||     {Y[i, 2]:F6}   |  {-Exp(T[i])/(2-T[i]):F6}
              """);
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
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
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
      
      
      Now we compute the solution to a higher accuracy (RelTol = 1e-5):
      
      Summary of statistics by Ode45a
              52 successful steps
              0 failed attempts
              1662 function evaluations
              208 partial derivatives
              208 LU decompositions
              814 solutions of linear systems
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
      --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
        0.00  ||     1.000000    |    1.000000    ||     1.000000    |    1.000000    ||     -0.500000   |  -0.500000
        0.01  ||     1.010050    |    1.010050    ||     1.010050    |    1.010050    ||     -0.507565   |  -0.507563
        0.02  ||     1.021441    |    1.021441    ||     1.021441    |    1.021441    ||     -0.516199   |  -0.516196
        0.03  ||     1.033834    |    1.033834    ||     1.033834    |    1.033834    ||     -0.525666   |  -0.525662
        0.05  ||     1.046989    |    1.046989    ||     1.046989    |    1.046989    ||     -0.535800   |  -0.535796
        0.06  ||     1.060746    |    1.060746    ||     1.060746    |    1.060746    ||     -0.546491   |  -0.546487
        0.07  ||     1.075004    |    1.075004    ||     1.075004    |    1.075004    ||     -0.557673   |  -0.557668
        0.09  ||     1.089700    |    1.089700    ||     1.089700    |    1.089700    ||     -0.569307   |  -0.569302
        0.10  ||     1.104800    |    1.104800    ||     1.104800    |    1.104800    ||     -0.581376   |  -0.581371
        0.11  ||     1.120287    |    1.120287    ||     1.120287    |    1.120287    ||     -0.593876   |  -0.593871
        0.13  ||     1.136153    |    1.136153    ||     1.136153    |    1.136153    ||     -0.606811   |  -0.606806
        0.14  ||     1.152401    |    1.152401    ||     1.152401    |    1.152401    ||     -0.620192   |  -0.620187
        0.16  ||     1.169037    |    1.169037    ||     1.169037    |    1.169037    ||     -0.634035   |  -0.634030
        0.17  ||     1.186070    |    1.186070    ||     1.186070    |    1.186070    ||     -0.648360   |  -0.648354
        0.19  ||     1.203513    |    1.203513    ||     1.203513    |    1.203513    ||     -0.663188   |  -0.663182
        0.20  ||     1.221381    |    1.221381    ||     1.221381    |    1.221381    ||     -0.678544   |  -0.678538
        0.21  ||     1.239689    |    1.239690    ||     1.239689    |    1.239690    ||     -0.694456   |  -0.694450
        0.23  ||     1.258456    |    1.258456    ||     1.258456    |    1.258456    ||     -0.710952   |  -0.710947
        0.25  ||     1.277701    |    1.277701    ||     1.277701    |    1.277701    ||     -0.728067   |  -0.728061
        0.26  ||     1.297444    |    1.297445    ||     1.297444    |    1.297445    ||     -0.745834   |  -0.745828
        0.28  ||     1.317709    |    1.317709    ||     1.317709    |    1.317709    ||     -0.764292   |  -0.764286
        0.29  ||     1.338519    |    1.338519    ||     1.338519    |    1.338519    ||     -0.783482   |  -0.783476
        0.31  ||     1.359900    |    1.359900    ||     1.359900    |    1.359900    ||     -0.803450   |  -0.803444
        0.32  ||     1.381880    |    1.381880    ||     1.381880    |    1.381880    ||     -0.824245   |  -0.824238
        0.34  ||     1.404491    |    1.404491    ||     1.404491    |    1.404491    ||     -0.845920   |  -0.845913
        0.36  ||     1.427765    |    1.427765    ||     1.427765    |    1.427765    ||     -0.868536   |  -0.868528
        0.37  ||     1.451738    |    1.451738    ||     1.451738    |    1.451738    ||     -0.892156   |  -0.892148
        0.39  ||     1.476449    |    1.476449    ||     1.476449    |    1.476449    ||     -0.916852   |  -0.916844
        0.41  ||     1.501941    |    1.501942    ||     1.501942    |    1.501942    ||     -0.942704   |  -0.942696
        0.42  ||     1.528263    |    1.528263    ||     1.528263    |    1.528263    ||     -0.969799   |  -0.969791
        0.44  ||     1.555464    |    1.555464    ||     1.555464    |    1.555464    ||     -0.998236   |  -0.998228
        0.46  ||     1.583604    |    1.583604    ||     1.583604    |    1.583604    ||     -1.028125   |  -1.028116
        0.48  ||     1.612746    |    1.612746    ||     1.612746    |    1.612746    ||     -1.059589   |  -1.059580
        0.50  ||     1.642962    |    1.642962    ||     1.642962    |    1.642962    ||     -1.092768   |  -1.092759
        0.52  ||     1.674332    |    1.674333    ||     1.674333    |    1.674333    ||     -1.127821   |  -1.127812
        0.53  ||     1.706949    |    1.706949    ||     1.706949    |    1.706949    ||     -1.164930   |  -1.164921
        0.55  ||     1.740915    |    1.740915    ||     1.740915    |    1.740915    ||     -1.204305   |  -1.204295
        0.57  ||     1.776351    |    1.776351    ||     1.776351    |    1.776351    ||     -1.246189   |  -1.246178
        0.60  ||     1.813394    |    1.813394    ||     1.813394    |    1.813394    ||     -1.290867   |  -1.290856
        0.62  ||     1.852207    |    1.852207    ||     1.852207    |    1.852207    ||     -1.338677   |  -1.338666
        0.64  ||     1.892982    |    1.892982    ||     1.892982    |    1.892982    ||     -1.390023   |  -1.390012
        0.66  ||     1.935949    |    1.935950    ||     1.935949    |    1.935950    ||     -1.445396   |  -1.445383
        0.68  ||     1.981390    |    1.981390    ||     1.981390    |    1.981390    ||     -1.505398   |  -1.505386
        0.71  ||     2.029654    |    2.029654    ||     2.029654    |    2.029654    ||     -1.570789   |  -1.570776
        0.73  ||     2.081186    |    2.081187    ||     2.081186    |    2.081187    ||     -1.642543   |  -1.642530
        0.76  ||     2.136570    |    2.136570    ||     2.136570    |    2.136570    ||     -1.721946   |  -1.721932
        0.79  ||     2.196597    |    2.196597    ||     2.196597    |    2.196597    ||     -1.810760   |  -1.810745
        0.82  ||     2.262399    |    2.262400    ||     2.262399    |    2.262400    ||     -1.911514   |  -1.911499
        0.85  ||     2.335708    |    2.335709    ||     2.335708    |    2.335709    ||     -2.028096   |  -2.028080
        0.88  ||     2.419462    |    2.419462    ||     2.419462    |    2.419462    ||     -2.167110   |  -2.167094
        0.92  ||     2.519645    |    2.519646    ||     2.519645    |    2.519646    ||     -2.341952   |  -2.341937
        0.98  ||     2.654994    |    2.654996    ||     2.654994    |    2.654996    ||     -2.593902   |  -2.593891
        1.00  ||     2.718281    |    2.718282    ||     2.718281    |    2.718282    ||     -2.718279   |  -2.718282
   
   .. figure:: images/Index-2-DAE-Ercan-Celik.png
      :align: center
      :alt: Index-2-DAE-Ercan-Celik.png
   
   
   


.. Admonition:: Example 5 :  Pendulum position constraint (Index-2)

   To reduce the index, if we differentiated the constraint once instead of twice, we end up with index 2 problem. 
   
   The resulting Index-1 system is:
   
   .. math::
   
      \begin{array}{rcl}
      x' &=& u \\
      y' &=& v \\
      u' &=& -\lambda x \\
      v' &=& -\lambda  y - g \\
      0 &=& x u + y v
      \end{array}    
   
   
   
   
   .. code-block:: csharp
   
      double g = 9.81;
   
      // State vector y = [x, y, u, v, λ]
      double[] pendulum_f(double t, double[] y) =>
          [y[2],
           y[3],
           -y[0] * y[4],
           -y[1] * y[4] - g,
           y[0]*y[2] + y[1]*y[3]];
   
      double[,] mass_f(double t, double[] y) => Diag([1, 1, 1, 1, 0]);
   
      double[] y0 = [0, 1, 1, 0, -1];
      var opts = Odeset(Stats: true);
      (ColVec T, Matrix Y) = Ode45a(pendulum_f, mass_f, y0, [0, 6], opts);
      Plot(T, Y, Linewidth: 2); Xlabel("x"); Ylabel("y");
      Legend(["x", "y", "u", "v", "λ"]);
      Title("Pendulum Trajectory (DAE)");
      SaveAs("Index_2-Pendulum-Problem-Ode45a.png");
   
      Console.WriteLine("\n\n");
      Console.WriteLine(Hcart(T, Y));
   
   
   
   Ouput
   
   .. terminal::
   
      Summary of statistics by Ode45a
              240 successful steps
              16 failed attempts
              11424 function evaluations
              1024 partial derivatives
              1024 LU decompositions
              5261 solutions of linear systems
      
      
      
      
      
         0.0000   -0.0000    1.0000    1.0000    0.0000   -8.8100
         0.0600    0.0603    0.9982    1.0159   -0.0614   -8.7513
         0.1235    0.1263    0.9920    1.0671   -0.1358   -8.5678
         0.1873    0.1969    0.9804    1.1534   -0.2316   -8.2253
         0.2477    0.2699    0.9629    1.2658   -0.3548   -7.7075
         0.3024    0.3423    0.9396    1.3890   -0.5060   -7.0207
         0.3500    0.4113    0.9115    1.5078   -0.6804   -6.1938
         0.3908    0.4749    0.8801    1.6116   -0.8697   -5.2688
         0.4253    0.5320    0.8468    1.6950   -1.0649   -4.2904
         0.4544    0.5822    0.8131    1.7567   -1.2579   -3.3000
         0.4788    0.6256    0.7802    1.7983   -1.4420   -2.3339
         0.4991    0.6624    0.7492    1.8230   -1.6117   -1.4238
         0.5122    0.6864    0.7272    1.8328   -1.7300   -0.7796
         0.5237    0.7075    0.7068    1.8367   -1.8385   -0.1783
         0.5288    0.7169    0.6972    1.8368   -1.8885    0.1010
         0.5330    0.7246    0.6892    1.8362   -1.9307    0.3385
         0.5373    0.7325    0.6808    1.8347   -1.9739    0.5839
         0.5421    0.7412    0.6713    1.8322   -2.0231    0.8649
         0.5476    0.7513    0.6600    1.8280   -2.0809    1.1975
         0.5540    0.7629    0.6465    1.8212   -2.1492    1.5944
         0.5613    0.7763    0.6304    1.8109   -2.2297    2.0672
         0.5697    0.7913    0.6114    1.7955   -2.3239    2.6277
         0.5791    0.8082    0.5890    1.7732   -2.4333    3.2887
         0.5897    0.8267    0.5626    1.7415   -2.5590    4.0640
         0.6014    0.8468    0.5319    1.6974   -2.7026    4.9697
         0.6142    0.8683    0.4961    1.6368   -2.8650    6.0238
         0.6283    0.8908    0.4545    1.5548   -3.0472    7.2476
         0.6436    0.9137    0.4063    1.4451   -3.2496    8.6656
         0.6602    0.9365    0.3506    1.2997   -3.4717   10.3068
         0.6781    0.9582    0.2861    1.1084   -3.7119   12.2052
         0.6975    0.9774    0.2115    0.8584   -3.9666   14.4017
         0.7186    0.9922    0.1251    0.5332   -4.2287   16.9455
         0.7416    0.9997    0.0248    0.1113   -4.4857   19.8980
         0.7670    0.9958   -0.0921   -0.4363   -4.7157   23.3388
         0.7956    0.9733   -0.2295   -1.1505   -4.8786   27.3805
         0.8291    0.9192   -0.3939   -2.0975   -4.8939   32.2129
         0.8683    0.8137   -0.5813   -3.2896   -4.6048   37.7096
         0.9114    0.6442   -0.7648   -4.5651   -3.8453   43.0817
         0.9548    0.4219   -0.9066   -5.6188   -2.6147   47.2275
         0.9938    0.1896   -0.9818   -6.2006   -1.1977   49.4396
         1.0284   -0.0281   -0.9996   -6.3402    0.1785   49.9734
         1.0596   -0.2238   -0.9746   -6.1439    1.4112   49.2509
         1.0888   -0.3974   -0.9176   -5.7026    2.4700   47.5839
         1.1171   -0.5503   -0.8349   -5.0785    3.3476   45.1586
         1.1454   -0.6833   -0.7301   -4.3157    4.0393   42.0808
         1.1745   -0.7962   -0.6049   -3.4480    4.5384   38.4047
         1.2053   -0.8880   -0.4597   -2.5028    4.8345   34.1386
         1.2396   -0.9563   -0.2921   -1.4993    4.9092   29.2139
         1.2812   -0.9958   -0.0910   -0.4306    4.7136   23.3105
         1.3278   -0.9929    0.1186    0.5073    4.2467   17.1611
         1.3742   -0.9533    0.3021    1.1579    3.6541   11.7701
         1.4137   -0.8999    0.4360    1.5145    3.1260    7.8186
         1.4459   -0.8479    0.5301    1.6947    2.7107    5.0390
         1.4722   -0.8022    0.5970    1.7818    2.3944    3.0633
         1.4935   -0.7637    0.6455    1.8207    2.1542    1.6303
         1.5071   -0.7389    0.6737    1.8330    2.0105    0.7955
         1.5164   -0.7218    0.6921    1.8366    1.9155    0.2544
         1.5224   -0.7107    0.7034    1.8369    1.8561   -0.0805
         1.5274   -0.7015    0.7126    1.8362    1.8077   -0.3508
         1.5322   -0.6927    0.7212    1.8346    1.7622   -0.6033
         1.5375   -0.6831    0.7303    1.8319    1.7136   -0.8714
         1.5435   -0.6722    0.7403    1.8278    1.6595   -1.1677
         1.5503   -0.6596    0.7516    1.8217    1.5989   -1.4974
         1.5583   -0.6452    0.7639    1.8130    1.5313   -1.8618
         1.5673   -0.6289    0.7775    1.8011    1.4569   -2.2597
         1.5776   -0.6104    0.7920    1.7853    1.3760   -2.6880
         1.5892   -0.5899    0.8075    1.7650    1.2894   -3.1422
         1.6022   -0.5671    0.8236    1.7396    1.1978   -3.6171
         1.6166   -0.5422    0.8402    1.7086    1.1025   -4.1063
         1.6327   -0.5150    0.8571    1.6716    1.0044   -4.6034
         1.6504   -0.4857    0.8741    1.6285    0.9050   -5.1016
         1.6700   -0.4543    0.8908    1.5792    0.8054   -5.5937
         1.6916   -0.4208    0.9071    1.5241    0.7071   -6.0732
         1.7154   -0.3853    0.9227    1.4637    0.6112   -6.5334
         1.7416   -0.3478    0.9375    1.3989    0.5190   -6.9679
         1.7705   -0.3084    0.9512    1.3308    0.4315   -7.3710
         1.8024   -0.2670    0.9637    1.2614    0.3495   -7.7371
         1.8379   -0.2236    0.9747    1.1927    0.2736   -8.0606
         1.8773   -0.1779    0.9840    1.1279    0.2039   -8.3361
         1.9212   -0.1296    0.9915    1.0709    0.1400   -8.5570
         1.9702   -0.0783    0.9969    1.0269    0.0806   -8.7149
         2.0247   -0.0231    0.9997    1.0028    0.0232   -8.7970
         2.0844    0.0367    0.9993    1.0064   -0.0370   -8.7844
         2.1457    0.0993    0.9950    1.0426   -0.1040   -8.6581
         2.2090    0.1674    0.9859    1.1144   -0.1892   -8.3864
         2.2707    0.2392    0.9710    1.2168   -0.2997   -7.9455
         2.3277    0.3118    0.9501    1.3367   -0.4387   -7.3309
         2.3781    0.3823    0.9240    1.4585   -0.6034   -6.5622
         2.4217    0.4482    0.8939    1.5693   -0.7868   -5.6765
         2.4587    0.5080    0.8613    1.6617   -0.9801   -4.7180
         2.4900    0.5612    0.8277    1.7325   -1.1747   -3.7297
         2.5163    0.6074    0.7944    1.7826   -1.3631   -2.7502
         2.5383    0.6470    0.7625    1.8142   -1.5395   -1.8130
         2.5565    0.6802    0.7330    1.8309   -1.6989   -0.9484
         2.5677    0.7007    0.7135    1.8361   -1.8031   -0.3753
         2.5748    0.7137    0.7004    1.8370   -1.8720    0.0086
         2.5808    0.7247    0.6890    1.8363   -1.9315    0.3436
         2.5860    0.7344    0.6787    1.8344   -1.9850    0.6478
         2.5916    0.7446    0.6675    1.8310   -2.0424    0.9759
         2.5977    0.7558    0.6548    1.8257   -2.1074    1.3517
         2.6047    0.7685    0.6398    1.8173   -2.1829    1.7918
         2.6126    0.7828    0.6222    1.8048   -2.2706    2.3099
         2.6215    0.7989    0.6015    1.7863   -2.3724    2.9195
         2.6315    0.8166    0.5772    1.7599   -2.4896    3.6347
         2.6426    0.8359    0.5488    1.7228   -2.6239    4.4707
         2.6549    0.8567    0.5158    1.6714   -2.7763    5.4447
         2.6683    0.8787    0.4773    1.6014   -2.9481    6.5764
         2.6829    0.9015    0.4328    1.5072   -3.1398    7.8887
         2.6988    0.9245    0.3812    1.3818   -3.3516    9.4081
         2.7161    0.9469    0.3215    1.2162   -3.5825   11.1659
         2.7347    0.9676    0.2524    0.9991   -3.8302   13.1991
         2.7549    0.9850    0.1725    0.7161   -4.0895   15.5524
         2.7768    0.9968    0.0798    0.3484   -4.3513   18.2800
         2.8009    0.9996   -0.0279   -0.1285   -4.5992   21.4510
         2.8276    0.9881   -0.1539   -0.7483   -4.8042   25.1576
         2.8582    0.9530   -0.3029   -1.5611   -4.9119   29.5392
         2.8953    0.8752   -0.4838   -2.6550   -4.8026   34.8532
         2.9367    0.7389   -0.6738   -3.9201   -4.2983   40.4224
         2.9815    0.5349   -0.8449   -5.1530   -3.2625   45.4216
         3.0232    0.3011   -0.9535   -5.9801   -1.8886   48.6063
         3.0600    0.0733   -0.9973   -6.3221   -0.4644   49.9012
         3.0928   -0.1342   -0.9909   -6.2720    0.8491   49.7275
         3.1229   -0.3184   -0.9479   -5.9362    1.9943   48.4732
         3.1516   -0.4812   -0.8765   -5.3907    2.9591   46.3835
         3.1798   -0.6236   -0.7817   -4.6874    3.7393   43.5990
         3.2083   -0.7460   -0.6658   -3.8645    4.3298   40.1967
         3.2382   -0.8479   -0.5301   -2.9523    4.7224   36.2073
         3.2706   -0.9276   -0.3734   -1.9743    4.9038   31.6059
         3.3080   -0.9816   -0.1906   -0.9408    4.8449   26.2358
         3.3520   -0.9998    0.0162    0.0728    4.5052   20.1682
         3.3991   -0.9764    0.2157    0.8733    3.9528   14.3100
         3.4417   -0.9281    0.3722    1.3585    3.3870    9.7007
         3.4772   -0.8750    0.4840    1.6144    2.9186    6.4007
         3.5061   -0.8262    0.5632    1.7425    2.5562    4.0602
         3.5298   -0.7842    0.6204    1.8034    2.2795    2.3715
         3.5491   -0.7491    0.6624    1.8291    2.0686    1.1325
         3.5610   -0.7273    0.6862    1.8359    1.9459    0.4276
         3.5686   -0.7134    0.7007    1.8371    1.8705    0.0008
         3.5750   -0.7017    0.7124    1.8363    1.8087   -0.3446
         3.5805   -0.6916    0.7222    1.8344    1.7564   -0.6349
         3.5862   -0.6810    0.7322    1.8313    1.7031   -0.9284
         3.5927   -0.6693    0.7430    1.8266    1.6454   -1.2441
         3.5999   -0.6560    0.7547    1.8198    1.5818   -1.5896
         3.6082   -0.6409    0.7675    1.8102    1.5116   -1.9671
         3.6177   -0.6239    0.7814    1.7972    1.4349   -2.3760
         3.6283   -0.6048    0.7963    1.7802    1.3521   -2.8133
         3.6403   -0.5836    0.8120    1.7585    1.2639   -3.2747
         3.6537   -0.5602    0.8283    1.7315    1.1711   -3.7547
         3.6687   -0.5346    0.8450    1.6988    1.0748   -4.2471
         3.6852   -0.5069    0.8620    1.6601    0.9762   -4.7453
         3.7034   -0.4770    0.8789    1.6152    0.8766   -5.2425
         3.7236   -0.4449    0.8955    1.5642    0.7772   -5.7318
         3.7458   -0.4109    0.9116    1.5075    0.6795   -6.2064
         3.7702   -0.3748    0.9270    1.4458    0.5845   -6.6599
         3.7971   -0.3368    0.9415    1.3799    0.4935   -7.0861
         3.8268   -0.2968    0.9549    1.3113    0.4075   -7.4792
         3.8597   -0.2548    0.9669    1.2418    0.3272   -7.8336
         3.8963   -0.2107    0.9775    1.1740    0.2531   -8.1441
         3.9369   -0.1643    0.9863    1.1109    0.1851   -8.4047
         3.9822   -0.1153    0.9933    1.0571    0.1227   -8.6086
         4.0328   -0.0629    0.9980    1.0180    0.0642   -8.7463
         4.0888   -0.0065    0.9999    1.0010    0.0065   -8.8036
         4.1498    0.0548    0.9984    1.0139   -0.0557   -8.7592
         4.2140    0.1213    0.9926    1.0629   -0.1299   -8.5846
         4.2784    0.1923    0.9813    1.1477   -0.2249   -8.2508
         4.3395    0.2656    0.9640    1.2594   -0.3470   -7.7408
         4.3946    0.3384    0.9409    1.3828   -0.4974   -7.0602
         4.4428    0.4079    0.9130    1.5025   -0.6712   -6.2375
         4.4840    0.4719    0.8816    1.6073   -0.8604   -5.3145
         4.5188    0.5294    0.8483    1.6918   -1.0558   -4.3362
         4.5482    0.5800    0.8146    1.7545   -1.2492   -3.3445
         4.5728    0.6237    0.7816    1.7971   -1.4340   -2.3757
         4.5932    0.6608    0.7505    1.8224   -1.6046   -1.4620
         4.6066    0.6851    0.7284    1.8327   -1.7239   -0.8127
         4.6182    0.7064    0.7077    1.8368   -1.8335   -0.2055
         4.6233    0.7159    0.6981    1.8371   -1.8839    0.0753
         4.6277    0.7240    0.6898    1.8364   -1.9273    0.3205
         4.6320    0.7319    0.6814    1.8350   -1.9710    0.5678
         4.6368    0.7406    0.6719    1.8326   -2.0201    0.8485
         4.6423    0.7507    0.6606    1.8284   -2.0776    1.1795
         4.6486    0.7623    0.6472    1.8218   -2.1456    1.5738
         4.6559    0.7755    0.6313    1.8117   -2.2256    2.0433
         4.6642    0.7905    0.6124    1.7966   -2.3192    2.5999
         4.6736    0.8073    0.5901    1.7746   -2.4278    3.2561
         4.6841    0.8258    0.5640    1.7434   -2.5528    4.0260
         4.6958    0.8458    0.5334    1.6999   -2.6955    4.9255
         4.7086    0.8672    0.4979    1.6402   -2.8571    5.9725
         4.7226    0.8896    0.4566    1.5593   -3.0384    7.1881
         4.7378    0.9126    0.4087    1.4510   -3.2398    8.5967
         4.7544    0.9354    0.3534    1.3074   -3.4610   10.2271
         4.7722    0.9572    0.2893    1.1185   -3.7005   12.1130
         4.7916    0.9765    0.2152    0.8716   -3.9546   14.2949
         4.8126    0.9916    0.1294    0.5502   -4.2166   16.8217
         4.8355    0.9995    0.0298    0.1333   -4.4743   19.7541
         4.8608    0.9962   -0.0863   -0.4077   -4.7062   23.1707
         4.8892    0.9749   -0.2227   -1.1131   -4.8735   27.1817
         4.9224    0.9226   -0.3856   -2.0472   -4.8985   31.9715
         4.9614    0.8199   -0.5725   -3.2312   -4.6274   37.4562
         5.0043    0.6535   -0.7569   -4.5079   -3.8922   42.8540
         5.0479    0.4321   -0.9017   -5.5819   -2.6749   47.0896
         5.0873    0.1993   -0.9799   -6.1856   -1.2582   49.3868
         5.1220   -0.0195   -0.9997   -6.3418    0.1240   49.9830
         5.1534   -0.2163   -0.9762   -6.1572    1.3644   49.3044
         5.1827   -0.3909   -0.9203   -5.7242    2.4309   47.6705
         5.2110   -0.5446   -0.8386   -5.1063    3.3162   45.2722
         5.2392   -0.6784   -0.7346   -4.3481    4.0155   42.2183
         5.2682   -0.7921   -0.6102   -3.4840    4.5224   38.5648
         5.2990   -0.8848   -0.4658   -2.5413    4.8269   34.3220
         5.3331   -0.9541   -0.2992   -1.5399    4.9109   29.4261
         5.3742   -0.9949   -0.0999   -0.4750    4.7280   23.5765
         5.4206   -0.9939    0.1093    0.4699    4.2725   17.4361
         5.4668   -0.9559    0.2934    1.1312    3.6857   12.0266
         5.5064   -0.9032    0.4290    1.4987    3.1557    8.0275
         5.5389   -0.8513    0.5246    1.6860    2.7361    5.2030
         5.5653   -0.8054    0.5927    1.7774    2.4153    3.1915
         5.5869   -0.7665    0.6421    1.8189    2.1711    1.7304
         5.6007   -0.7412    0.6711    1.8324    2.0239    0.8731
         5.6127   -0.7193    0.6946    1.8370    1.9024    0.1818
         5.6180   -0.7095    0.7046    1.8371    1.8500   -0.1140
         5.6224   -0.7015    0.7125    1.8364    1.8080   -0.3485
         5.6268   -0.6934    0.7204    1.8349    1.7662   -0.5806
         5.6317   -0.6844    0.7290    1.8325    1.7203   -0.8341
         5.6374   -0.6739    0.7387    1.8287    1.6683   -1.1188
         5.6441   -0.6618    0.7496    1.8231    1.6096   -1.4387
         5.6517   -0.6478    0.7617    1.8149    1.5437   -1.7947
         5.6605   -0.6319    0.7750    1.8037    1.4707   -2.1853
         5.6706   -0.6139    0.7893    1.7887    1.3911   -2.6076
         5.6819   -0.5937    0.8046    1.7693    1.3056   -3.0570
         5.6946   -0.5714    0.8206    1.7449    1.2149   -3.5283
         5.7088   -0.5468    0.8372    1.7150    1.1202   -4.0152
         5.7246   -0.5201    0.8540    1.6791    1.0226   -4.5113
         5.7420   -0.4912    0.8710    1.6372    0.9233   -5.0097
         5.7612   -0.4602    0.8877    1.5890    0.8237   -5.5035
         5.7824   -0.4271    0.9041    1.5350    0.7250   -5.9857
         5.8058   -0.3919    0.9199    1.4755    0.6286   -6.4499
         5.8315   -0.3548    0.9349    1.4114    0.5357   -6.8896
         5.8599   -0.3157    0.9488    1.3439    0.4472   -7.2989
         5.8912   -0.2747    0.9615    1.2746    0.3641   -7.6722
         5.9260   -0.2316    0.9727    1.2057    0.2871   -8.0039
         5.9646   -0.1863    0.9824    1.1399    0.2162   -8.2886
         6.0000   -0.1469    0.9891    1.0905    0.1619   -8.4861
      
   
   .. figure:: images/Index_2-Pendulum-Problem-Ode45a.png
      :align: center
      :alt: Index_2-Pendulum-Problem-Ode45a.png
   
   Observe that the initial condition supplied for :math:`\lambda` was :math:`-1`; but the result returned shown that the correct initial condition for the algebraic variable :math:`\lambda` is :math:`-8.81`.
   Sending in a wrong initial condition was done on purpose, to test the ability of sepalsolver to compute the initial condition of the algebraic variable. 
   


