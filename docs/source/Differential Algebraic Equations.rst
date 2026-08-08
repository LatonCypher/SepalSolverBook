Differential Algebraic Equations
================================


1. Introduction to DAEs
-----------------------
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
              49652 function evaluations
              5340 partial derivatives
              5340 LU decompositions
              17616 solutions of linear systems
      
   
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
      \end{align}
   
   
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
              355 function evaluations
              52 partial derivatives
              52 LU decompositions
              135 solutions of linear systems
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
      --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
        0.00  ||     1.000000    |    1.000000    ||     1.000000    |    1.000000    ||     -0.500000   |  -0.500000
        0.01  ||     1.010050    |    1.010050    ||     1.010050    |    1.010050    ||     -0.507565   |  -0.507563
        0.04  ||     1.038900    |    1.038900    ||     1.038900    |    1.038900    ||     -0.529574   |  -0.529555
        0.09  ||     1.096642    |    1.096647    ||     1.096644    |    1.096647    ||     -0.574914   |  -0.574840
        0.18  ||     1.193049    |    1.193078    ||     1.193062    |    1.193078    ||     -0.654460   |  -0.654292
        0.28  ||     1.318500    |    1.318555    ||     1.318523    |    1.318555    ||     -0.765277   |  -0.765061
        0.38  ||     1.457170    |    1.457228    ||     1.457193    |    1.457228    ||     -0.897806   |  -0.897604
        0.48  ||     1.610430    |    1.610486    ||     1.610449    |    1.610486    ||     -1.057315   |  -1.057121
        0.58  ||     1.779810    |    1.779863    ||     1.779826    |    1.779863    ||     -1.250552   |  -1.250374
        0.68  ||     1.967006    |    1.967052    ||     1.967017    |    1.967052    ||     -1.486444   |  -1.486291
        0.78  ||     2.173890    |    2.173929    ||     2.173897    |    2.173929    ||     -1.776979   |  -1.776864
        0.88  ||     2.402535    |    2.402563    ||     2.402538    |    2.402563    ||     -2.138592   |  -2.138532
        0.98  ||     2.655228    |    2.655243    ||     2.655229    |    2.655243    ||     -2.594355   |  -2.594369
        1.00  ||     2.718274    |    2.718282    ||     2.718274    |    2.718282    ||     -2.718252   |  -2.718282
      
      
      Now we compute the solution to a higher accuracy (RelTol = 1e-5):
      
      Summary of statistics by Ode45a
              52 successful steps
              0 failed attempts
              1283 function evaluations
              208 partial derivatives
              208 LU decompositions
              439 solutions of linear systems
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
      --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
        0.00  ||     1.000000    |    1.000000    ||     1.000000    |    1.000000    ||     -0.500000   |  -0.500000
        0.01  ||     1.010050    |    1.010050    ||     1.010050    |    1.010050    ||     -0.507565   |  -0.507563
        0.02  ||     1.021438    |    1.021438    ||     1.021438    |    1.021438    ||     -0.516197   |  -0.516194
        0.03  ||     1.033831    |    1.033831    ||     1.033831    |    1.033831    ||     -0.525663   |  -0.525660
        0.05  ||     1.046984    |    1.046984    ||     1.046984    |    1.046984    ||     -0.535796   |  -0.535792
        0.06  ||     1.060740    |    1.060740    ||     1.060740    |    1.060740    ||     -0.546487   |  -0.546482
        0.07  ||     1.074997    |    1.074997    ||     1.074997    |    1.074997    ||     -0.557668   |  -0.557663
        0.09  ||     1.089691    |    1.089691    ||     1.089691    |    1.089691    ||     -0.569300   |  -0.569295
        0.10  ||     1.104787    |    1.104787    ||     1.104787    |    1.104787    ||     -0.581365   |  -0.581360
        0.11  ||     1.120271    |    1.120271    ||     1.120271    |    1.120271    ||     -0.593863   |  -0.593858
        0.13  ||     1.136135    |    1.136135    ||     1.136135    |    1.136135    ||     -0.606796   |  -0.606791
        0.14  ||     1.152381    |    1.152381    ||     1.152381    |    1.152381    ||     -0.620175   |  -0.620170
        0.16  ||     1.169015    |    1.169015    ||     1.169015    |    1.169015    ||     -0.634017   |  -0.634012
        0.17  ||     1.186048    |    1.186048    ||     1.186048    |    1.186048    ||     -0.648341   |  -0.648336
        0.19  ||     1.203493    |    1.203493    ||     1.203493    |    1.203493    ||     -0.663171   |  -0.663165
        0.20  ||     1.221361    |    1.221361    ||     1.221361    |    1.221361    ||     -0.678527   |  -0.678521
        0.21  ||     1.239669    |    1.239669    ||     1.239669    |    1.239669    ||     -0.694438   |  -0.694432
        0.23  ||     1.258435    |    1.258435    ||     1.258435    |    1.258435    ||     -0.710934   |  -0.710928
        0.25  ||     1.277679    |    1.277679    ||     1.277679    |    1.277679    ||     -0.728047   |  -0.728041
        0.26  ||     1.297424    |    1.297424    ||     1.297424    |    1.297424    ||     -0.745815   |  -0.745809
        0.28  ||     1.317691    |    1.317691    ||     1.317691    |    1.317691    ||     -0.764276   |  -0.764269
        0.29  ||     1.338503    |    1.338503    ||     1.338503    |    1.338503    ||     -0.783468   |  -0.783461
        0.31  ||     1.359885    |    1.359885    ||     1.359885    |    1.359885    ||     -0.803437   |  -0.803430
        0.32  ||     1.381867    |    1.381867    ||     1.381867    |    1.381867    ||     -0.824232   |  -0.824225
        0.34  ||     1.404478    |    1.404478    ||     1.404478    |    1.404478    ||     -0.845908   |  -0.845901
        0.36  ||     1.427755    |    1.427755    ||     1.427755    |    1.427755    ||     -0.868526   |  -0.868519
        0.37  ||     1.451730    |    1.451730    ||     1.451730    |    1.451730    ||     -0.892148   |  -0.892140
        0.39  ||     1.476441    |    1.476441    ||     1.476441    |    1.476441    ||     -0.916844   |  -0.916836
        0.41  ||     1.501933    |    1.501933    ||     1.501933    |    1.501933    ||     -0.942695   |  -0.942687
        0.42  ||     1.528252    |    1.528252    ||     1.528252    |    1.528252    ||     -0.969788   |  -0.969780
        0.44  ||     1.555453    |    1.555453    ||     1.555453    |    1.555453    ||     -0.998224   |  -0.998216
        0.46  ||     1.583591    |    1.583592    ||     1.583591    |    1.583592    ||     -1.028111   |  -1.028103
        0.48  ||     1.612734    |    1.612734    ||     1.612734    |    1.612734    ||     -1.059576   |  -1.059567
        0.50  ||     1.642953    |    1.642953    ||     1.642953    |    1.642953    ||     -1.092758   |  -1.092749
        0.52  ||     1.674327    |    1.674328    ||     1.674327    |    1.674328    ||     -1.127815   |  -1.127806
        0.53  ||     1.706948    |    1.706949    ||     1.706949    |    1.706949    ||     -1.164930   |  -1.164920
        0.55  ||     1.740913    |    1.740914    ||     1.740913    |    1.740914    ||     -1.204303   |  -1.204293
        0.57  ||     1.776352    |    1.776352    ||     1.776352    |    1.776352    ||     -1.246191   |  -1.246180
        0.60  ||     1.813401    |    1.813401    ||     1.813401    |    1.813401    ||     -1.290875   |  -1.290865
        0.62  ||     1.852219    |    1.852219    ||     1.852219    |    1.852219    ||     -1.338691   |  -1.338680
        0.64  ||     1.892995    |    1.892996    ||     1.892995    |    1.892996    ||     -1.390040   |  -1.390029
        0.66  ||     1.935966    |    1.935967    ||     1.935967    |    1.935967    ||     -1.445418   |  -1.445406
        0.68  ||     1.981419    |    1.981419    ||     1.981419    |    1.981419    ||     -1.505437   |  -1.505424
        0.71  ||     2.029695    |    2.029695    ||     2.029695    |    2.029695    ||     -1.570846   |  -1.570833
        0.73  ||     2.081236    |    2.081236    ||     2.081236    |    2.081236    ||     -1.642613   |  -1.642599
        0.76  ||     2.136636    |    2.136636    ||     2.136636    |    2.136636    ||     -1.722042   |  -1.722028
        0.79  ||     2.196676    |    2.196677    ||     2.196677    |    2.196677    ||     -1.810879   |  -1.810865
        0.82  ||     2.262492    |    2.262492    ||     2.262492    |    2.262492    ||     -1.911658   |  -1.911643
        0.85  ||     2.335823    |    2.335823    ||     2.335823    |    2.335823    ||     -2.028282   |  -2.028266
        0.88  ||     2.419602    |    2.419603    ||     2.419603    |    2.419603    ||     -2.167350   |  -2.167334
        0.92  ||     2.519828    |    2.519829    ||     2.519828    |    2.519829    ||     -2.342279   |  -2.342264
        0.98  ||     2.655280    |    2.655282    ||     2.655281    |    2.655282    ||     -2.594455   |  -2.594445
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
              10484 function evaluations
              1024 partial derivatives
              1024 LU decompositions
              4336 solutions of linear systems
      
      
      
      
      
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
         0.7670    0.9958   -0.0921   -0.4363   -4.7157   23.3389
         0.7956    0.9733   -0.2295   -1.1505   -4.8786   27.3805
         0.8291    0.9192   -0.3939   -2.0975   -4.8939   32.2129
         0.8683    0.8137   -0.5813   -3.2896   -4.6048   37.7096
         0.9114    0.6442   -0.7648   -4.5651   -3.8453   43.0818
         0.9548    0.4219   -0.9066   -5.6188   -2.6147   47.2275
         0.9938    0.1896   -0.9818   -6.2006   -1.1977   49.4396
         1.0284   -0.0281   -0.9996   -6.3402    0.1785   49.9734
         1.0596   -0.2238   -0.9746   -6.1439    1.4112   49.2509
         1.0889   -0.3974   -0.9176   -5.7026    2.4700   47.5838
         1.1171   -0.5503   -0.8349   -5.0785    3.3477   45.1584
         1.1454   -0.6833   -0.7301   -4.3156    4.0393   42.0806
         1.1745   -0.7962   -0.6049   -3.4480    4.5384   38.4045
         1.2053   -0.8880   -0.4597   -2.5028    4.8346   34.1383
         1.2396   -0.9563   -0.2921   -1.4993    4.9092   29.2135
         1.2812   -0.9958   -0.0910   -0.4306    4.7135   23.3101
         1.3278   -0.9929    0.1186    0.5074    4.2466   17.1606
         1.3742   -0.9532    0.3021    1.1579    3.6541   11.7697
         1.4137   -0.8999    0.4360    1.5146    3.1260    7.8183
         1.4459   -0.8479    0.5301    1.6947    2.7107    5.0388
         1.4722   -0.8022    0.5970    1.7818    2.3944    3.0632
         1.4935   -0.7637    0.6455    1.8207    2.1541    1.6302
         1.5071   -0.7389    0.6737    1.8330    2.0105    0.7954
         1.5164   -0.7218    0.6921    1.8366    1.9155    0.2543
         1.5224   -0.7107    0.7034    1.8369    1.8561   -0.0805
         1.5274   -0.7015    0.7126    1.8362    1.8077   -0.3508
         1.5322   -0.6927    0.7212    1.8346    1.7622   -0.6033
         1.5375   -0.6831    0.7303    1.8319    1.7136   -0.8714
         1.5435   -0.6722    0.7403    1.8278    1.6595   -1.1677
         1.5503   -0.6596    0.7516    1.8217    1.5989   -1.4974
         1.5583   -0.6452    0.7639    1.8130    1.5313   -1.8618
         1.5673   -0.6289    0.7775    1.8011    1.4569   -2.2597
         1.5776   -0.6104    0.7920    1.7853    1.3760   -2.6879
         1.5892   -0.5899    0.8075    1.7650    1.2894   -3.1422
         1.6022   -0.5671    0.8236    1.7396    1.1978   -3.6170
         1.6166   -0.5422    0.8402    1.7086    1.1025   -4.1063
         1.6327   -0.5150    0.8571    1.6716    1.0044   -4.6034
         1.6504   -0.4857    0.8741    1.6285    0.9050   -5.1015
         1.6700   -0.4543    0.8908    1.5792    0.8054   -5.5937
         1.6916   -0.4208    0.9071    1.5241    0.7071   -6.0732
         1.7154   -0.3853    0.9227    1.4637    0.6112   -6.5333
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
         2.3277    0.3118    0.9501    1.3367   -0.4387   -7.3310
         2.3781    0.3823    0.9240    1.4585   -0.6034   -6.5623
         2.4217    0.4482    0.8939    1.5693   -0.7868   -5.6765
         2.4587    0.5080    0.8613    1.6617   -0.9801   -4.7180
         2.4900    0.5612    0.8277    1.7325   -1.1747   -3.7298
         2.5163    0.6074    0.7944    1.7826   -1.3631   -2.7503
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
         2.7549    0.9850    0.1725    0.7161   -4.0895   15.5523
         2.7768    0.9968    0.0798    0.3484   -4.3513   18.2800
         2.8009    0.9996   -0.0279   -0.1285   -4.5992   21.4510
         2.8276    0.9881   -0.1539   -0.7483   -4.8042   25.1576
         2.8582    0.9530   -0.3029   -1.5611   -4.9119   29.5392
         2.8953    0.8752   -0.4838   -2.6550   -4.8026   34.8532
         2.9367    0.7389   -0.6738   -3.9201   -4.2983   40.4224
         2.9815    0.5349   -0.8449   -5.1530   -3.2625   45.4216
         3.0232    0.3011   -0.9535   -5.9801   -1.8886   48.6063
         3.0600    0.0733   -0.9973   -6.3221   -0.4645   49.9011
         3.0928   -0.1341   -0.9909   -6.2721    0.8491   49.7274
         3.1229   -0.3184   -0.9479   -5.9362    1.9941   48.4735
         3.1516   -0.4811   -0.8766   -5.3908    2.9590   46.3839
         3.1798   -0.6235   -0.7817   -4.6876    3.7392   43.5996
         3.2083   -0.7460   -0.6659   -3.8647    4.3297   40.1974
         3.2382   -0.8479   -0.5301   -2.9525    4.7224   36.2082
         3.2706   -0.9276   -0.3735   -1.9745    4.9038   31.6069
         3.3080   -0.9816   -0.1906   -0.9410    4.8449   26.2370
         3.3520   -0.9998    0.0161    0.0726    4.5053   20.1695
         3.3991   -0.9764    0.2157    0.8732    3.9529   14.3111
         3.4417   -0.9281    0.3722    1.3584    3.3871    9.7014
         3.4772   -0.8750    0.4840    1.6143    2.9187    6.4012
         3.5061   -0.8262    0.5632    1.7424    2.5562    4.0606
         3.5298   -0.7842    0.6204    1.8034    2.2795    2.3717
         3.5491   -0.7491    0.6624    1.8291    2.0686    1.1327
         3.5610   -0.7273    0.6862    1.8359    1.9460    0.4278
         3.5686   -0.7134    0.7007    1.8371    1.8705    0.0009
         3.5749   -0.7017    0.7124    1.8363    1.8087   -0.3445
         3.5805   -0.6916    0.7222    1.8344    1.7564   -0.6349
         3.5862   -0.6810    0.7322    1.8313    1.7032   -0.9284
         3.5927   -0.6693    0.7430    1.8266    1.6454   -1.2441
         3.5999   -0.6560    0.7547    1.8198    1.5818   -1.5895
         3.6082   -0.6409    0.7675    1.8102    1.5116   -1.9671
         3.6177   -0.6239    0.7814    1.7972    1.4349   -2.3760
         3.6283   -0.6048    0.7963    1.7802    1.3521   -2.8133
         3.6403   -0.5836    0.8120    1.7585    1.2639   -3.2747
         3.6537   -0.5602    0.8283    1.7315    1.1711   -3.7547
         3.6687   -0.5346    0.8450    1.6988    1.0748   -4.2471
         3.6852   -0.5069    0.8620    1.6601    0.9762   -4.7453
         3.7034   -0.4770    0.8789    1.6152    0.8766   -5.2425
         3.7236   -0.4449    0.8955    1.5642    0.7772   -5.7318
         3.7458   -0.4109    0.9116    1.5076    0.6795   -6.2064
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
         4.2784    0.1923    0.9813    1.1477   -0.2249   -8.2509
         4.3395    0.2656    0.9640    1.2594   -0.3470   -7.7408
         4.3946    0.3384    0.9410    1.3828   -0.4974   -7.0603
         4.4428    0.4079    0.9130    1.5025   -0.6712   -6.2375
         4.4840    0.4719    0.8816    1.6073   -0.8603   -5.3145
         4.5188    0.5294    0.8483    1.6918   -1.0558   -4.3363
         4.5482    0.5800    0.8146    1.7545   -1.2492   -3.3445
         4.5728    0.6237    0.7816    1.7971   -1.4340   -2.3758
         4.5932    0.6608    0.7505    1.8224   -1.6046   -1.4620
         4.6066    0.6851    0.7284    1.8327   -1.7239   -0.8127
         4.6182    0.7064    0.7077    1.8368   -1.8335   -0.2055
         4.6233    0.7159    0.6981    1.8371   -1.8838    0.0753
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
         5.1220   -0.0195   -0.9997   -6.3418    0.1240   49.9831
         5.1534   -0.2163   -0.9762   -6.1572    1.3644   49.3044
         5.1827   -0.3909   -0.9203   -5.7241    2.4310   47.6703
         5.2110   -0.5446   -0.8386   -5.1062    3.3163   45.2719
         5.2392   -0.6784   -0.7346   -4.3480    4.0156   42.2179
         5.2682   -0.7921   -0.6102   -3.4838    4.5225   38.5643
         5.2990   -0.8848   -0.4658   -2.5412    4.8269   34.3214
         5.3331   -0.9541   -0.2992   -1.5398    4.9109   29.4253
         5.3742   -0.9949   -0.0999   -0.4748    4.7280   23.5756
         5.4206   -0.9939    0.1094    0.4701    4.2724   17.4351
         5.4668   -0.9559    0.2934    1.1314    3.6855   12.0253
         5.5064   -0.9032    0.4290    1.4988    3.1555    8.0262
         5.5389   -0.8513    0.5246    1.6861    2.7359    5.2019
         5.5653   -0.8053    0.5927    1.7775    2.4152    3.1905
         5.5869   -0.7665    0.6422    1.8189    2.1710    1.7297
         5.6007   -0.7412    0.6711    1.8324    2.0238    0.8725
         5.6127   -0.7193    0.6946    1.8370    1.9023    0.1813
         5.6180   -0.7095    0.7046    1.8371    1.8499   -0.1145
         5.6224   -0.7015    0.7125    1.8364    1.8079   -0.3491
         5.6268   -0.6934    0.7204    1.8349    1.7661   -0.5812
         5.6317   -0.6843    0.7290    1.8325    1.7201   -0.8348
         5.6374   -0.6739    0.7387    1.8287    1.6682   -1.1195
         5.6441   -0.6618    0.7496    1.8230    1.6094   -1.4395
         5.6518   -0.6478    0.7617    1.8149    1.5435   -1.7955
         5.6606   -0.6319    0.7750    1.8037    1.4706   -2.1863
         5.6706   -0.6138    0.7893    1.7887    1.3910   -2.6086
         5.6819   -0.5937    0.8046    1.7693    1.3054   -3.0581
         5.6947   -0.5713    0.8206    1.7448    1.2147   -3.5294
         5.7088   -0.5468    0.8372    1.7149    1.1200   -4.0163
         5.7246   -0.5200    0.8541    1.6790    1.0224   -4.5124
         5.7420   -0.4911    0.8710    1.6371    0.9231   -5.0108
         5.7613   -0.4601    0.8878    1.5889    0.8235   -5.5046
         5.7825   -0.4270    0.9042    1.5349    0.7248   -5.9868
         5.8058   -0.3918    0.9199    1.4754    0.6284   -6.4509
         5.8315   -0.3547    0.9349    1.4113    0.5355   -6.8906
         5.8599   -0.3156    0.9488    1.3438    0.4470   -7.2998
         5.8913   -0.2746    0.9615    1.2745    0.3640   -7.6730
         5.9260   -0.2315    0.9728    1.2055    0.2869   -8.0046
         5.9647   -0.1862    0.9824    1.1398    0.2161   -8.2892
         6.0000   -0.1469    0.9891    1.0905    0.1619   -8.4861
      
   
   .. figure:: images/Index_2-Pendulum-Problem-Ode45a.png
      :align: center
      :alt: Index_2-Pendulum-Problem-Ode45a.png
   
   Observe that the initial condition supplied for :math:`\lambda` was :math:`-1`; but the result returned shown that the correct initial condition for the algebraic variable :math:`\lambda` is :math:`-8.81`.
   Sending in a wrong initial condition was done on purpose, to test the ability of sepalsolver to compute the initial condition of the algebraic variable. 
   


