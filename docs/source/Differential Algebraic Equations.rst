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
              1224 successful steps
              7 failed attempts
              48784 function evaluations
              4921 partial derivatives
              4921 LU decompositions
              19274 solutions of linear systems
      
   
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
              12 successful steps
              0 failed attempts
              344 function evaluations
              48 partial derivatives
              48 LU decompositions
              140 solutions of linear systems
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
      --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
        0.00  ||     1.000000    |    1.000000    ||     1.000000    |    1.000000    ||     -0.500000   |  -0.500000
        0.01  ||     1.010050    |    1.010050    ||     1.010050    |    1.010050    ||     -0.507565   |  -0.507563
        0.07  ||     1.068389    |    1.068395    ||     1.068392    |    1.068395    ||     -0.552551   |  -0.552473
        0.17  ||     1.180708    |    1.180759    ||     1.180731    |    1.180759    ||     -0.644094   |  -0.643871
        0.27  ||     1.304882    |    1.304940    ||     1.304907    |    1.304940    ||     -0.752834   |  -0.752629
        0.37  ||     1.442124    |    1.442182    ||     1.442146    |    1.442182    ||     -0.882895   |  -0.882693
        0.47  ||     1.593801    |    1.593858    ||     1.593821    |    1.593858    ||     -1.039322   |  -1.039127
        0.57  ||     1.761432    |    1.761485    ||     1.761448    |    1.761485    ||     -1.228687   |  -1.228507
        0.67  ||     1.946695    |    1.946742    ||     1.946707    |    1.946742    ||     -1.459655   |  -1.459499
        0.77  ||     2.151443    |    2.151483    ||     2.151451    |    2.151483    ||     -1.743845   |  -1.743726
        0.87  ||     2.377727    |    2.377757    ||     2.377730    |    2.377757    ||     -2.097144   |  -2.097078
        0.97  ||     2.627811    |    2.627827    ||     2.627811    |    2.627827    ||     -2.541800   |  -2.541806
        1.00  ||     2.718276    |    2.718282    ||     2.718276    |    2.718282    ||     -2.718257   |  -2.718282
      
      
      Now we compute the solution to a higher accuracy (RelTol = 1e-5):
      
      Summary of statistics by Ode45a
              23 successful steps
              0 failed attempts
              657 function evaluations
              92 partial derivatives
              92 LU decompositions
              277 solutions of linear systems
      
          t   ||  x_1_NumSol(t)  |  x_1_Exact(t)  ||  x_2_NumSol(t)  |  x_2_Exact(t)  ||   z_NumSol(t)   |   z_Exact(t) 
      --------++-----------------+----------------++-----------------+----------------++-----------------+---------------
        0.00  ||     1.000000    |    1.000000    ||     1.000000    |    1.000000    ||     -0.500000   |  -0.500000
        0.01  ||     1.010050    |    1.010050    ||     1.010050    |    1.010050    ||     -0.507565   |  -0.507563
        0.03  ||     1.032886    |    1.032886    ||     1.032886    |    1.032886    ||     -0.524948   |  -0.524936
        0.06  ||     1.065688    |    1.065689    ||     1.065688    |    1.065689    ||     -0.550376   |  -0.550351
        0.10  ||     1.104920    |    1.104921    ||     1.104920    |    1.104921    ||     -0.581501   |  -0.581468
        0.14  ||     1.148496    |    1.148499    ||     1.148498    |    1.148499    ||     -0.616998   |  -0.616960
        0.18  ||     1.195492    |    1.195495    ||     1.195494    |    1.195495    ||     -0.656387   |  -0.656346
        0.22  ||     1.245611    |    1.245614    ||     1.245612    |    1.245614    ||     -0.699680   |  -0.699637
        0.26  ||     1.298866    |    1.298870    ||     1.298868    |    1.298870    ||     -0.747164   |  -0.747119
        0.30  ||     1.355432    |    1.355436    ||     1.355434    |    1.355436    ||     -0.799301   |  -0.799254
        0.35  ||     1.415575    |    1.415579    ||     1.415577    |    1.415579    ||     -0.856698   |  -0.856649
        0.39  ||     1.479634    |    1.479639    ||     1.479636    |    1.479639    ||     -0.920109   |  -0.920058
        0.44  ||     1.548018    |    1.548022    ||     1.548019    |    1.548022    ||     -0.990456   |  -0.990404
        0.48  ||     1.621213    |    1.621218    ||     1.621215    |    1.621218    ||     -1.068880   |  -1.068825
        0.53  ||     1.699806    |    1.699811    ||     1.699808    |    1.699811    ||     -1.156798   |  -1.156741
        0.58  ||     1.784510    |    1.784515    ||     1.784512    |    1.784515    ||     -1.256004   |  -1.255946
        0.63  ||     1.876209    |    1.876214    ||     1.876210    |    1.876214    ||     -1.368815   |  -1.368756
        0.68  ||     1.976026    |    1.976031    ||     1.976027    |    1.976031    ||     -1.498290   |  -1.498231
        0.73  ||     2.085430    |    2.085436    ||     2.085431    |    2.085436    ||     -1.648595   |  -1.648537
        0.79  ||     2.206426    |    2.206432    ||     2.206427    |    2.206432    ||     -1.825629   |  -1.825575
        0.85  ||     2.341896    |    2.341901    ||     2.341896    |    2.341901    ||     -2.038191   |  -2.038143
        0.91  ||     2.496324    |    2.496329    ||     2.496324    |    2.496329    ||     -2.300420   |  -2.300385
        0.98  ||     2.677612    |    2.677616    ||     2.677612    |    2.677616    ||     -2.637862   |  -2.637855
        1.00  ||     2.718279    |    2.718282    ||     2.718279    |    2.718282    ||     -2.718271   |  -2.718282
   
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
   
      double[,] mass_f = Diag([1, 1, 1, 1, 0]);
   
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
              89 successful steps
              31 failed attempts
              5522 function evaluations
              480 partial derivatives
              480 LU decompositions
              2629 solutions of linear systems
      
      
      
      
      
         0.0000   -0.0000    1.0000    1.0000    0.0000   -8.8100
         0.0600    0.0603    0.9982    1.0159   -0.0614   -8.7513
         0.1401    0.1441    0.9896    1.0862   -0.1582   -8.4920
         0.2440    0.2652    0.9642    1.2581   -0.3461   -7.7289
         0.3689    0.4403    0.8980    1.5563   -0.7631   -5.7323
         0.4914    0.6486    0.7615    1.8153   -1.5463   -1.6542
         0.5219    0.7044    0.7102    1.8368   -1.8218   -0.2625
         0.5665    0.7858    0.6189    1.8022   -2.2884    2.4408
         0.6084    0.8589    0.5126    1.6661   -2.7918    5.5668
         0.6466    0.9183    0.3965    1.4208   -3.2908    8.9794
         0.7173    0.9917    0.1306    0.5550   -4.2139   16.8580
         0.7657    0.9966   -0.0860   -0.4060   -4.7065   23.1756
         0.8093    0.9552   -0.2970   -1.5275   -4.9116   29.3642
         0.8569    0.8496   -0.5280   -2.9384   -4.7281   36.1324
         0.9078    0.6605   -0.7512   -4.4663   -3.9268   42.6544
         0.9623    0.3789   -0.9257   -5.7640   -2.3591   47.7284
         1.0104    0.0861   -0.9965   -6.3149   -0.5458   49.8122
         1.0577   -0.2120   -0.9774   -6.1654    1.3372   49.2517
         1.0976   -0.4464   -0.8950   -5.5293    2.7583   46.8706
         1.1503   -0.7041   -0.7103   -4.1749    4.1386   41.4179
         1.2112   -0.9023   -0.4314   -2.3264    4.8657   33.2551
         1.2714   -0.9906   -0.1376   -0.6647    4.7837   24.6800
         1.3331   -0.9902    0.1408    0.5952    4.1845   16.5314
         1.3925   -0.9305    0.3667    1.3438    3.4103    9.9005
         1.4626   -0.8194    0.5736    1.7563    2.5087    3.8377
         1.5387   -0.6811    0.7326    1.8327    1.7039   -0.8380
         1.5802   -0.6058    0.7959    1.7827    1.3570   -2.7668
         1.6683   -0.4569    0.8899    1.5858    0.8143   -5.4812
         1.7782   -0.2978    0.9550    1.3165    0.4105   -7.3994
         1.9214   -0.1285    0.9922    1.0749    0.1393   -8.4960
         2.0311   -0.0153    1.0003    1.0071    0.0154   -8.7716
         2.1384    0.0938    0.9961    1.0436   -0.0983   -8.6474
         2.2335    0.1980    0.9807    1.1601   -0.2343   -8.1938
         2.3476    0.3429    0.9400    1.3938   -0.5084   -6.9692
         2.4873    0.5620    0.8281    1.7359   -1.1781   -3.5906
         2.5590    0.6907    0.7241    1.8364   -1.7517   -0.6065
         2.5847    0.7381    0.6758    1.8357   -2.0049    0.7673
         2.6261    0.8130    0.5836    1.7686   -2.4639    3.4945
         2.6662    0.8809    0.4749    1.5975   -2.9629    6.6884
         2.7033    0.9353    0.3561    1.3151   -3.4545   10.1791
         2.7724    0.9971    0.0857    0.3731   -4.3392   18.1769
         2.8220    0.9907   -0.1418   -0.6859   -4.7919   24.8136
         2.8658    0.9356   -0.3553   -1.8657   -4.9133   31.0632
         2.9142    0.8101   -0.5876   -3.3305   -4.5920   37.8604
         2.9659    0.5978   -0.8025   -4.8380   -3.6041   44.1294
         3.0218    0.2918   -0.9572   -6.0064   -1.8307   48.6132
         3.0658    0.0179   -1.0005   -6.3453   -0.1132   49.9208
         3.1088   -0.2516   -0.9685   -6.0943    1.5831   48.9851
         3.1507   -0.4927   -0.8709   -5.3465    3.0244   46.1359
         3.2065   -0.7510   -0.6613   -3.8317    4.3515   39.9581
         3.2672   -0.9274   -0.3758   -1.9878    4.9057   31.6240
         3.3274   -0.9971   -0.0835   -0.3940    4.7043   23.0968
         3.3899   -0.9822    0.1912    0.7854    4.0352   15.0649
         3.4568   -0.9029    0.4315    1.5053    3.1499    8.0273
         3.5298   -0.7793    0.6278    1.8116    2.2489    2.2700
         3.5686   -0.7082    0.7070    1.8403    1.8435   -0.1283
         3.6083   -0.6357    0.7729    1.8101    1.4886   -2.0672
         3.6943   -0.4869    0.8743    1.6354    0.9108   -4.9955
         3.7994   -0.3289    0.9452    1.3735    0.4780   -7.0824
         3.9341   -0.1626    0.9876    1.1187    0.1842   -8.3325
         4.0521   -0.0376    1.0002    1.0184    0.0383   -8.7332
         4.1439    0.0556    0.9994    1.0255   -0.0570   -8.7205
         4.2250    0.1411    0.9910    1.0938   -0.1557   -8.4734
         4.3283    0.2620    0.9661    1.2619   -0.3422   -7.7261
         4.4527    0.4366    0.9008    1.5565   -0.7545   -5.7628
         4.5756    0.6459    0.7650    1.8178   -1.5348   -1.7116
         4.6063    0.7020    0.7138    1.8406   -1.8101   -0.3263
         4.6510    0.7840    0.6227    1.8082   -2.2764    2.3653
         4.6930    0.8576    0.5166    1.6747   -2.7802    5.4815
         4.7314    0.9176    0.4006    1.4321   -3.2801    8.8845
         4.8022    0.9921    0.1348    0.5717   -4.2071   16.7467
         4.8505    0.9980   -0.0809   -0.3813   -4.7026   23.0272
         4.8948    0.9567   -0.2952   -1.5165   -4.9156   29.2974
         4.9423    0.8515   -0.5266   -2.9289   -4.7357   36.0685
         4.9933    0.6629   -0.7503   -4.4586   -3.9388   42.5927
         5.0478    0.3817   -0.9255   -5.7607   -2.3756   47.6784
         5.0960    0.0882   -0.9972   -6.3183   -0.5589   49.7837
         5.1434   -0.2109   -0.9786   -6.1722    1.3304   49.2366
         5.1832   -0.4454   -0.8966   -5.5394    2.7516   46.8727
         5.2358   -0.7032   -0.7125   -4.1892    4.1346   41.4482
         5.2968   -0.9023   -0.4335   -2.3390    4.8683   33.2975
         5.3570   -0.9913   -0.1394   -0.6736    4.7905   24.7275
         5.4187   -0.9913    0.1395    0.5903    4.1934   16.5813
         5.4777   -0.9323    0.3648    1.3396    3.4236    9.9794
         5.5477   -0.8214    0.5724    1.7572    2.5214    3.9096
         5.6238   -0.6829    0.7322    1.8374    1.7138   -0.7813
         5.6653   -0.6075    0.7958    1.7888    1.3655   -2.7160
         5.7533   -0.4582    0.8902    1.5939    0.8203   -5.4392
         5.8628   -0.2986    0.9557    1.3261    0.4143   -7.3643
         6.0000   -0.1344    0.9923    1.0922    0.1480   -8.4454
      
   
   .. figure:: images/Index_2-Pendulum-Problem-Ode45a.png
      :align: center
      :alt: Index_2-Pendulum-Problem-Ode45a.png
   
   Observe that the initial condition supplied for :math:`\lambda` was :math:`-1`; but the result returned shown that the correct initial condition for the algebraic variable :math:`\lambda` is :math:`-8.81`.
   Sending in a wrong initial condition was done on purpose, to test the ability of sepalsolver to compute the initial condition of the algebraic variable. 
   


