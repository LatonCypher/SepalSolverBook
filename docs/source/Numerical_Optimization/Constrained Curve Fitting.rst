Constrained Curve Fitting
=========================

Constrained Curve Fitting
-------------------------
Constrained curve fitting is a mathematical optimization technique used to construct a model curve that best fits empirical observations while strictly satisfying auxiliary constraints on the parameter domain. 

While classical unconstrained regression seeks parameters that minimize residual variance across uninhibited Euclidean space :math:`\mathbb{R}^P`, real-world systems operate under strict physical laws, thermodynamic limits, and operational realities. Unconstrained non-linear solvers frequently fall prey to non-physical solutions—such as negative absolute temperatures, negative mass fractions, unbounded growth rates, or violated conservation laws. Constrained curve fitting integrates prior domain knowledge directly into the objective framework, guaranteeing that the converged parameter vector remains physically meaningful, statistically stable, and within operational limits.

The General Constrained Optimization Problem
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Formally, constrained non-linear curve fitting formulates the parameter search as a constrained non-linear least-squares (NLLS) optimization problem:


.. math::

   \min_{\mathbf{p} \in \Omega} f_0(\mathbf{p}) = \frac{1}{2} \sum_{i=1}^{M} \left( y_i - f(x_i; \mathbf{p}) \right)^2 = \frac{1}{2} \|\mathbf{y} - \mathbf{f}(\mathbf{x}_{\text{data}}; \mathbf{p})\|_2^2


where :math:`\mathbf{y} \in \mathbb{R}^M` denotes measured responses, :math:`f(x_i; \mathbf{p})` is the non-linear forward model, :math:`\mathbf{p} \in \mathbb{R}^P` is the unknown parameter vector, and :math:`\Omega \subset \mathbb{R}^P` defines the feasible parameter space.

The feasible domain :math:`\Omega` is governed by three primary classes of mathematical conditions:

<header 4> 1. Box Bounds (Simple Upper and Lower Limits) </header 4>
Parameter box limits enforce explicit coordinate-wise intervals:


.. math::

   \mathbf{lb} \le \mathbf{p} \le \mathbf{ub} \iff \text{lb}_j \le p_j \le \text{ub}_j, \quad \forall j \in \{0, 1, \dots, P-1\}


Box constraints enforce fundamental physical constants and non-negativity rules, such as ensuring half-lives, decay constants, or diffusion coefficients satisfy :math:`p_j > 0`.

<header 4> 2. Linear Inequality and Equality Constraints </header 4>
Linear restrictions model affine dependencies, mass/energy conservation, and monotonic parameter relationships:


.. math::

   A_{\text{ineq}} \mathbf{p} \le \mathbf{b}_{\text{ineq}}, \quad A_{\text{eq}} \mathbf{p} = \mathbf{b}_{\text{eq}}


In canonical inequality form, linear conditions are written as :math:`g_k(\mathbf{p}) = A_k \mathbf{p} - b_k \le 0`. Common engineering examples include component mass fractions summing to unity (:math:`\sum p_j = 1`) or total amplitude bounds (:math:`p_0 + p_2 \le p_{\text{max}}`).

<header 4> 3. Non-Linear Inequality and Equality Constraints </header 4>
Non-linear constraints define arbitrary smooth geometries that parameters must satisfy:


.. math::

   \mathbf{g}(\mathbf{p}) \le \mathbf{0}, \quad \mathbf{h}(\mathbf{p}) = \mathbf{0}


These model coupled physical interactions, such as enforcing multi-component thermodynamic phase equilibria, stability criteria in dynamical systems (e.g., eigenvalue boundaries), or geometric energy thresholds (such as limiting the Euclidean parameter radius :math:`p_0^2 + p_1^2 \le R^2`).

Optimality and Algorithmic Mechanics
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Constrained least squares introduces trade-offs between objective minimization (residual goodness of fit) and boundary feasibility. The solution is governed by the Karush-Kuhn-Tucker (KKT) conditions:


.. math::

   \nabla_{\mathbf{p}} f_0(\mathbf{p}^*) + \sum_{k \in \mathcal{E}} \lambda_k^* \nabla h_k(\mathbf{p}^*) + \sum_{j \in \mathcal{I}} \mu_j^* \nabla g_j(\mathbf{p}^*) = \mathbf{0}


subject to the complementary slackness requirements:


.. math::

   \mu_j^* \ge 0, \quad \mu_j^* g_j(\mathbf{p}^*) = 0, \quad \forall j \in \mathcal{I}


When the unconstrained optimum lies outside :math:`\Omega`, the constraint becomes **active** (:math:`g_j(\mathbf{p}^*) = 0`), and the associated Lagrange multiplier :math:`\mu_j^* > 0` represents the shadow price—quantifying the exact penalty paid in residual error to satisfy physical feasibility.

In SepalSolver, constrained problems are resolved using algorithms such as Subspace Trust-Region Reflective methods and Sequential Quadratic Programming (SQP). These methods compute feasible descent steps by projecting search directions onto the null space of active constraint normals and reflecting trajectories off boundary limits, guaranteeing numerical robustness and preventing divergence.

Example: Curve Fitting with Linear Inequality Constraints
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
In physical parameter estimation, models often require parameters to satisfy linear relationships, such as conservation balances or monotonic thresholds. This example fits a multi-parameter model subject to parameter box bounds :math:`\mathbf{lb} \le \mathbf{x} \le \mathbf{ub}` and a coupled linear inequality constraint:


.. math::

   A\mathbf{x} \le 0


specified via a `RowVec` inner product delegate (`funInEq`). The resulting fit is evaluated for feasibility and plotted alongside the raw data.


.. code-block:: csharp

   // -------------------------------------------------------------------------
   // 1. Synthetic Data Generation & Setup
   // -------------------------------------------------------------------------
   int seed = 23;
   var rgn = new Random(seed); // Seed the RNG for reproducible synthetic observations

   // True parameter ground truth: [x0, x1, x2, x3]
   ColVec xstar = new([2.0, 4.0, 5.0, 0.5]);

   // Forward model: f(x, xdata) = x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata
   static ColVec Model(ColVec x, ColVec xdata) =>
       x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;

   // Discretized independent variable domain (100 evenly spaced points from 2 to 7)
   ColVec xdata = Linspace(2, 7);

   // Add scaled Gaussian noise to simulate sensor/measurement uncertainty
   ColVec noise = Randn(100);
   ColVec ydata = Model(xstar, xdata) + noise / 10.0;

   // -------------------------------------------------------------------------
   // 2. Initial Estimates and Box Constraints
   // -------------------------------------------------------------------------
   ColVec startpt = new([1.0, 2.0, 3.0, 1.0]);

   // Parameter bounds: 0.0 <= x[i] <= 7.0 for all parameters
   ColVec lb = Zeros(4);
   ColVec ub = 7.0 + lb;

   // -------------------------------------------------------------------------
   // 3. Linear Inequality Constraint: A * x <= 0
   // -------------------------------------------------------------------------
   // Defined via the row vector A = [-1, -1, 1, 1].
   // Matrix formulation: A * x <= 0  -->  -x[0] - x[1] + x[2] + x[3] <= 0
   // Rearranged: (x[2] + x[3]) <= (x[0] + x[1])
   RowVec A = new([-1.0, -1.0, 1.0, 1.0]);
   ColVec Fineq(ColVec x) => A * x;

   // -------------------------------------------------------------------------
   // 4. Solver Configuration via OptimSet
   // -------------------------------------------------------------------------
   var opts = OptimSet(
       Display: true,
       MaxIter: 200,
       StepTol: 1e-6,
       OptimalityTol: 1e-6
   );

   // -------------------------------------------------------------------------
   // 5. Execute Linear Inequality-Constrained Least Squares
   // -------------------------------------------------------------------------
   var ans = Lsqcurvefit(
       Model,
       startpt,
       xdata,
       ydata,
       funInEq: Fineq, // Linear constraint wrapped in the inequality delegate
       funEq: null,    // Equality constraints (none)
       lb: lb,         // Lower box bounds
       ub: ub,         // Upper box bounds
       options: opts
   );

   // -------------------------------------------------------------------------
   // 6. Diagnostics & Verification
   // -------------------------------------------------------------------------
   Console.WriteLine($"Recovered Parameters: x = {ans.x.T}");

   // Directly evaluate constraint feasibility at the converged solution: fineq(x*) <= 0
   Console.WriteLine($"Inequality Value:    c = {ans.fineq.T}");

   // -------------------------------------------------------------------------
   // 7. Visual Inspection & Figure Export
   // -------------------------------------------------------------------------
   Scatter(xdata, ydata, "ro"); // Red open circles for measured observations
   HoldOn();
   Plot(xdata, ans.y_hat, "-b", Linewidth: 2); // Blue line for converged model estimate

   Xlabel("x");
   Ylabel("y");
   Legend(["Measured Data", "Model Estimate"], UpperRight);

   // Export figure to disk and release plot context memory
   SaveAs("Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png");
   CloseFig();




Ouput


.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            5          1.5886e3                       1.5178e3 
        1           11          1.3066e3     2.1069e-1         1.3181e3 
        2           17          8.0091e2     4.8534e-1         8.9187e2 
        3           23          3.0345e2     8.0856e-1         3.3820e2 
        4           29          6.1074e1     9.7082e-1         6.9973e1 
        5           35          1.0557e1     6.5634e-1         3.4809e1 
        6           41          5.7666e0     3.8599e-1         2.2553e0 
        7           47          2.9444e0     5.3399e-1         3.3362e0 
        8           53          2.7817e0     5.5598e-1         1.7631e0 
        9           59          1.1985e0     2.7995e-1        3.2747e-1 
       10           65          1.1934e0     5.8716e-3        2.0812e-2 
       11           71          1.1926e0     4.4548e-2        7.5723e-3 
       12           77          1.1926e0     5.6852e-3        2.3144e-4 
       13           83          1.1926e0     2.3440e-4        2.9108e-6 
       14           89          1.1926e0     3.0170e-6        1.9548e-8 
   Recovered Parameters: x = 
      1.7037    3.9152    4.9901    0.5547
   
   Inequality Value:    c =   -0.0742

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_Linear_Inequality_Constraints.png


<header 4> Mathematical Theory: Karush-Kuhn-Tucker (KKT) Conditions for Linear Constraints </header 4>
The optimization problem combines box limits and linear inequality constraints:


.. math::

   \min_{\mathbf{x}} f_0(\mathbf{x}) = \frac{1}{2} \sum_{i=1}^M \left( \text{Model}(\mathbf{x}, x_i) - y_i \right)^2


subject to:


.. math::

   A \mathbf{x} \le 0, \quad \mathbf{lb} - \mathbf{x} \le 0, \quad \mathbf{x} - \mathbf{ub} \le 0


The Lagrangian function :math:`\mathcal{L}` is defined with Lagrange multiplier :math:`\mu \ge 0` and bound multiplier vectors :math:`\boldsymbol{\lambda}_l, \boldsymbol{\lambda}_u \ge \mathbf{0}`:


.. math::

   \mathcal{L}(\mathbf{x}, \mu, \boldsymbol{\lambda}_l, \boldsymbol{\lambda}_u) = f_0(\mathbf{x}) + \mu (A \mathbf{x}) + \boldsymbol{\lambda}_l^T (\mathbf{lb} - \mathbf{x}) + \boldsymbol{\lambda}_u^T (\mathbf{x} - \mathbf{ub})


First-order optimality requires that the solution :math:`\hat{\mathbf{x}}` satisfy the Karush-Kuhn-Tucker (KKT) conditions:


.. math::

   \nabla_{\mathbf{x}} f_0(\hat{\mathbf{x}}) + A^T \mu - \boldsymbol{\lambda}_l + \boldsymbol{\lambda}_u = \mathbf{0}


along with complementary slackness:


.. math::

   \mu (A \hat{\mathbf{x}}) = 0, \quad \lambda_{l,j} (\text{lb}_j - \hat{x}_j) = 0, \quad \lambda_{u,j} (\hat{x}_j - \text{ub}_j) = 0


For the target solution :math:`\mathbf{x}^* = [2.0, 4.0, 5.0, 0.5]^T`, evaluation yields:


.. math::

   A \mathbf{x}^* = -2.0 - 4.0 + 5.0 + 0.5 = -0.5 < 0


Because the constraint is strictly negative (:math:`A \mathbf{x}^* < 0`), the constraint is **inactive**. Complementary slackness requires :math:`\mu = 0`, meaning the gradient of the objective function vanishes directly: :math:`\nabla f_0(\hat{\mathbf{x}}) = \mathbf{0}`.

Example: Curve Fitting with Non-Linear Inequality Constraints
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
When parameter boundaries involve non-linear dependencies, such as radial or volumetric constraints, non-linear inequality functions are used. In this example, the two primary parameters are restricted to lie within a circle of radius 4:


.. math::

   x_0^2 + x_1^2 - 16 \le 0


Because the unconstrained parameters violate this threshold:


.. math::

   2^2 + 4^2 = 20 > 16


the constraint is actively enforced, leading `Lsqcurvefit` to converge along the active feasible boundary.


.. code-block:: csharp

   // -------------------------------------------------------------------------
   // 1. Reproducible Synthetic Data Generation
   // -------------------------------------------------------------------------
   int seed = 23;
   var rgn = new Random(seed); // Seeds the random number generator for reproducibility

   // True parameter vector: [x0, x1, x2, x3]
   ColVec xstar = new([2.0, 4.0, 5.0, 0.5]);

   // Forward model: f(x, xdata) = x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata
   static ColVec Model(ColVec x, ColVec xdata) =>
       x[0] + x[1] * Atan(xdata - x[2]) + x[3] * xdata;

   // Generate synthetic grid across [2, 7] (Linspace defaults to 100 points)
   ColVec xdata = Linspace(2, 7);

   // Add Gaussian white noise scaled down by a factor of 10
   ColVec noise = Randn(100);
   ColVec ydata = Model(xstar, xdata) + noise / 10.0;

   // -------------------------------------------------------------------------
   // 2. Initial Estimates and Boundary Constraints
   // -------------------------------------------------------------------------
   // Initial parameter estimate far from xstar
   ColVec startpt = new([1.0, 2.0, 3.0, 1.0]);

   // Box constraints: 0.0 <= x[i] <= 7.0 for all parameters
   ColVec lb = Zeros(4);
   ColVec ub = 7.0 + lb;

   // -------------------------------------------------------------------------
   // 3. Nonlinear Inequality Constraint: g(x) <= 0
   // -------------------------------------------------------------------------
   // Constrains the vector norm of the first two parameters to lie within a circle of radius 4:
   // x[0]^2 + x[1]^2 <= 16  -->  x[0]^2 + x[1]^2 - 16 <= 0
   static ColVec Fineq(ColVec x) => new([x[0] * x[0] + x[1] * x[1] - 16.0]);

   // -------------------------------------------------------------------------
   // 4. Solver Configuration via OptimSet
   // -------------------------------------------------------------------------
   var opts = OptimSet(
       Display: true,
       MaxIter: 200,
       StepTol: 1e-6,
       OptimalityTol: 1e-6
   );

   // -------------------------------------------------------------------------
   // 5. Solve Constrained Non-Linear Least Squares
   // -------------------------------------------------------------------------
   var ans = Lsqcurvefit(
       Model,
       startpt,
       xdata,
       ydata,
       funInEq: Fineq, // Nonlinear inequality delegate
       funEq: null,    // Equality constraints (none)
       lb: lb,         // Lower box bounds
       ub: ub,         // Upper box bounds
       options: opts
   );

   // -------------------------------------------------------------------------
   // 6. Inspect Numerical Results
   // -------------------------------------------------------------------------
   Console.WriteLine($"Recovered Parameters: x = {ans.x.T}");
   Console.WriteLine($"Inequality Residual:  c = {ans.fineq.T}");

   // -------------------------------------------------------------------------
   // 7. Visual Inspection & Plotting
   // -------------------------------------------------------------------------
   // Plot raw noisy measurements as red open circles
   Scatter(xdata, ydata, "ro");
   HoldOn();

   // Overlay the optimal model fit (ans.y_hat) as a solid blue line
   Plot(xdata, ans.y_hat, "-b", Linewidth: 2);

   // Add axes metadata and legend
   Xlabel("x");
   Ylabel("y");
   Legend(["Measured Data", "Model Estimate"], UpperRight);

   // Export high-resolution chart and release plotting resources
   SaveAs("Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png");
   CloseFig();




Ouput


.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            5          1.5774e3                       1.5125e3 
        1           11          1.2942e3     2.1267e-1         1.3116e3 
        2           17          7.8686e2     4.9055e-1         8.8289e2 
        3           23          2.8977e2     8.1952e-1         3.2721e2 
        4           29          5.3912e1     9.6963e-1         6.9694e1 
        5           35          5.1485e0     6.2747e-1         4.0239e1 
        6           41          2.3132e0     2.0503e-1         4.5590e0 
        7           47          2.0630e0     1.7442e-1         1.1034e0 
        8           53          1.6406e0     3.8593e-1         1.4684e0 
        9           59          1.1212e0     6.8319e-1         1.5316e0 
       10           68          1.0873e0     6.3658e-2        6.3076e-1 
       11           76          1.0775e0     1.9593e-2        6.1727e-1 
       12           84          1.0744e0     6.1396e-3        6.1297e-1 
       13           92          1.0735e0     1.9361e-3        6.1163e-1 
       14          100          1.0732e0     6.1172e-4        6.1121e-1 
       15          107          1.0729e0     6.1116e-4        6.1082e-1 
       16          114          1.0728e0     5.6159e-4        6.0437e-1 
       17          120          1.0728e0     3.5584e-4        3.9281e-1 
       18          126          1.0722e0     8.0128e-4        2.8238e-1 
       19          132          1.0720e0     9.1768e-4        2.4584e-1 
       20          138          1.0719e0     4.1538e-4        2.4841e-1 
       21          144          1.0719e0     6.5480e-5        2.5005e-1 
       22          150          1.0719e0     1.0450e-4        2.5065e-1 
       23          156          1.0718e0     3.1126e-4        2.5288e-1 
       24          162          1.0715e0     8.2376e-4        2.6346e-1 
       25          168          1.0715e0     2.7034e-4        2.6835e-1 
       26          174          1.0714e0     4.0777e-4        2.7686e-1 
       27          180          1.0712e0     9.4569e-4        3.0137e-1 
       28          186          1.0710e0     1.3905e-3        3.4693e-1 
       29          192          1.0710e0     9.4618e-4        3.8265e-1 
       30          198          1.0710e0     2.3689e-4        3.9207e-1 
       31          204          1.0710e0     1.9079e-5        3.9285e-1 
       32          210          1.0710e0     6.5409e-7        3.9287e-1 
   Recovered Parameters: x = 
      1.3558    3.7632    5.0257    0.6415
   
   Inequality Residual:  c =    0.0000

.. figure:: images/Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png
   :align: center
   :alt: Example_of_CurveFitting_using_Lsqcurvefit_with_NonLinear_Inequality_Constraints.png


<header 4> Mathematical Theory: Active Boundary Projections and Sequential Quadratic Programming </header 4>
When an unconstrained parameter minimum :math:`\mathbf{x}^*` lies in the infeasible domain:


.. math::

   g(\mathbf{x}^*) = (x_0^*)^2 + (x_1^*)^2 - 16 = 20 - 16 = +4 > 0


the constraint becomes **active** at the solution :math:`\hat{\mathbf{x}}`, meaning :math:`g(\hat{\mathbf{x}}) = 0`.

Under Sequential Quadratic Programming (SQP) or Interior-Point Trust-Region formulations, the algorithm constructs a local quadratic model of the objective subject to linearized constraints at iteration :math:`k`:


.. math::

   \min_{\mathbf{d}} \nabla f_0(\mathbf{x}_k)^T \mathbf{d} + \frac{1}{2} \mathbf{d}^T \mathbf{B}_k \mathbf{d} \quad \text{subject to} \quad g(\mathbf{x}_k) + \nabla g(\mathbf{x}_k)^T \mathbf{d} \le 0


where :math:`\mathbf{B}_k` is a positive-definite quasi-Newton approximation of the Hessian of the Lagrangian, and the constraint gradient is:


.. math::

   \nabla g(\mathbf{x}) = \begin{bmatrix} 2 x_0 & 2 x_1 & 0 & 0 \end{bmatrix}^T


At the constrained optimum :math:`\hat{\mathbf{x}}`, the objective gradient does not vanish. Instead, it balances the outward outward-pointing normal to the constraint surface:


.. math::

   -\nabla f_0(\hat{\mathbf{x}}) = \mu^* \nabla g(\hat{\mathbf{x}}), \quad \mu^* > 0


The Lagrange multiplier :math:`\mu^*` acts as a shadow price, quantifying the decrease in the residual sum of squares that would occur if the feasible disc radius were relaxed:


.. math::

   \frac{d f_0}{d(\text{radius}^2)} = -\mu^*


Example: Damped Harmonic Model with Coupled Inequality Bounds
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Parameter bounds are frequently paired with coupled linear constraints to reflect physical operational envelopes. This example fits a 4-parameter damped harmonic model:


.. math::

   f(\mathbf{p}, x) = p_0 e^{-p_1 x} + p_2 \sin(p_3 x)


subject to bound limits :math:`\mathbf{lb} \le \mathbf{p} \le \mathbf{ub}` and an amplitude sum constraint:


.. math::

   p_0 + p_2 - 5.0 \le 0


The routine executes using `Lsqcurvefit`, reporting parameter outcomes alongside `resnorm`, constraint evaluations (`fineq`), and convergence status flags.


.. code-block:: csharp

   // 1. Experimental observation vectors
   ColVec xdata = new([0.1, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0]);
   ColVec ydata = new([3.8, 2.9, 2.1, 1.8, 1.4, 1.2, 0.9, 0.7, 0.5]);

   // 2. Initial parameter guess: [p0, p1, p2, p3]
   ColVec startpt = new([2.0, 0.5, 1.0, 1.0]);

   // 3. Parameterized model function: f(p, x) = p[0]*exp(-p[1]*x) + p[2]*sin(p[3]*x)
   static ColVec Model(ColVec p, ColVec x) =>
       p[0] * Exp(-p[1] * x) + p[2] * Sin(p[3] * x);


   // 4. Physical parameter box bounds (lb <= p <= ub)
   ColVec lb = new([0.0, 0.01, 0.0, 0.0]); // Non-negative rates and amplitudes
   ColVec ub = new([10.0, 5.00, 5.0, 10.0]);

   // 5. Inequality constraint: p[0] + p[2] <= 5.0  --> (p[0] + p[2] - 5.0 <= 0)
   Func<ColVec, ColVec> fineq = p => new([p[0] + p[2] - 5.0]);

   // 6. Solver configuration via OptimSet
   var opts = OptimSet(
       MaxIter: 300,
       StepTol: 1e-8,
       OptimalityTol: 1e-8,
       Display: true
   );

   // 7. Execute constrained least-squares curve fit
   var ans = Lsqcurvefit(
       Model,
       startpt,
       xdata,
       ydata,
       funInEq: fineq,
       funEq: null,
       lb: lb,
       ub: ub,
       options: opts
   );

   // 8. Diagnostics & post-fit inspection
   Console.WriteLine($"Fitted Parameters:       {ans.x.T}");
   Console.WriteLine($"Residual Norm (resnorm): {ans.resnorm:E4}");
   Console.WriteLine($"Inequality Residual:    {ans.fineq.T}");
   Console.WriteLine($"Exit Flag:              {ans.exitflag}");




Ouput


.. terminal::

                                               Norm of      First-order 
    Iteration   Func-count       Resnorm          step       optimality 
        0            5          5.6244e0                       6.4395e0 
        1           11          1.0751e0      1.1787e0         1.5888e0 
        2           17         1.5603e-1     7.4676e-1        8.6027e-1 
        3           23         6.4180e-2     4.1317e-1        4.6596e-1 
        4           31         5.8171e-2     1.9406e-1        3.2391e-1 
        5           37         3.1440e-2     1.6591e-1        1.1847e-1 
        6           43         1.9779e-2     1.9991e-1        1.2988e-1 
        7           49         1.6370e-2     8.7775e-2        3.1875e-2 
        8           55         1.6287e-2     1.0275e-2        4.7921e-4 
        9           61         1.6287e-2     7.0756e-4        1.1533e-5 
       10           67         1.6287e-2     2.9232e-5        4.0374e-7 
   Fitted Parameters:       
      4.0783    0.8431    0.6786    0.6582
   
   Residual Norm (resnorm): 1.6287E-002
   Inequality Residual:      -0.2431
   Exit Flag:              1

<header 4> Mathematical Theory: Box-Constrained Trust-Region Reflective Mechanics </header 4>
In SepalSolver, constrained least-squares with box bounds :math:`\mathbf{lb} \le \mathbf{p} \le \mathbf{ub}` and coupled constraints :math:`\mathbf{g}(\mathbf{p}) \le \mathbf{0}` is solved using a subspace trust-region reflective algorithm.

To handle parameter bounds, the parameter vector :math:`\mathbf{p}` is mapped to a scaled coordinate system via the diagonal scaling matrix :math:`D(\mathbf{p})`:


.. math::

   D(\mathbf{p}) = \text{diag}\left( |v_1|^{-1/2}, \dots, |v_4|^{-1/2} \right)


where each directional component :math:`v_j` measures the distance to the active bound depending on the sign of the gradient :math:`g_j = (\nabla f_0)_j`:


.. math::

   v_j = \begin{cases}
   p_j - \text{lb}_j, & \text{if } g_j < 0 \text{ and } \text{lb}_j > -\infty \\
   \text{ub}_j - p_j, & \text{if } g_j > 0 \text{ and } \text{ub}_j < \infty \\
   -1, & \text{otherwise}
   \end{cases}


Inside the trust region of radius :math:`\Delta_k`, a quadratic model step :math:`\mathbf{s}_k` is determined:


.. math::

   \min_{\mathbf{s}} \mathbf{g}_k^T \mathbf{s} + \frac{1}{2} \mathbf{s}^T \left( J_k^T J_k + C_k \right) \mathbf{s} \quad \text{subject to} \quad \| D_k \mathbf{s} \|_2 \le \Delta_k


where :math:`C_k = D_k^{-1} \text{diag}(\mathbf{g}_k) \nabla v(\mathbf{p}_k)` accounts for boundary curvature.

If a trial step :math:`\mathbf{p}_k + \mathbf{s}_k` encounters a boundary bound :math:`\text{lb}_j` or :math:`\text{ub}_j`, the trajectory does not truncate; instead, it undergoes specular reflection:


.. math::

   p_j^{\text{reflected}} = 2 \cdot \text{bound}_j - (p_{k,j} + s_{k,j})


This reflective mechanism allows the optimizer to follow valleys along physical constraint edges (such as :math:`p_1 \ge 0.01` and :math:`p_0 + p_2 \le 5.0`) without getting trapped at the boundaries, guaranteeing both feasibility and asymptotic convergence.
