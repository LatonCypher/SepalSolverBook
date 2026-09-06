NonLinear Optimization
======================

Nonlinear optimization (also known as nonlinear programming, or NLP) deals with the problem of finding an optimal input vector :math:`x \in \mathbb{R}^n` that minimizes or maximizes an objective function subject to a set of equality and inequality constraints, where at least one of the functions involved is nonlinear.

Mathematical Formulation
------------------------
The canonical minimization formulation of a nonlinear optimization problem is defined as:

.. math::

   \min_{x \in \mathbb{R}^n} f(x)



.. math::

   g_i(x) \le 0, \quad i = 1, \dots, m



.. math::

   h_j(x) = 0, \quad j = 1, \dots, p


where:

* :math:`f: \mathbb{R}^n \to \mathbb{R}` is the objective function.
* :math:`g_i: \mathbb{R}^n \to \mathbb{R}` are the inequality constraint functions.
* :math:`h_j: \mathbb{R}^n \to \mathbb{R}` are the equality constraint functions.
* :math:`\Omega = \{x \in \mathbb{R}^n \mid g_i(x) \le 0, \; h_j(x) = 0\}` defines the feasible region.

The sepalsolver library provides a suite of optimization algorithms for solving nonlinear optimization problems, 
including gradient-based methods, derivative-free methods, and global optimization techniques.


.. admonition:: Example 1 :  Rosenbrook funcion with constraint 

   
   The goal is to find the parameter vector :math:`\mathbf{x} = [x_0, x_1]^T` that 
   minimizes the non - convex Rosenbrock objective function:
   
   
   .. math::
   
      \min_{\mathbf{ x} } f(x_0, x_1) = 100(x_1 - x_0 ^ 2) ^ 2 + (1 - x_0) ^ 2
   
   
   subject to the non-linear inequality constraint restricting the domain to the unit disk:
   
   .. math::
   
      g(\mathbf{ x}) = x_0 ^ 2 + x_1 ^ 2 - 1 \le 0
   
   
   * **Initial Guess**: 
   :math:`\mathbf{ x}_0 = (0, 0)`
   
   
   .. code-block:: csharp
   
      static double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
      static ColVec Ineq(ColVec x) => Pow(x[0], 2) + Pow(x[1], 2) - 1;
      double[] x0 = [0, 0];
      var result = Fmincon(fun, x0, Ineq);
      Console.WriteLine($"x = {result.x.T}");
   
   
   
Ouput
   
   .. terminal::
   
      x = 
         0.7864    0.6177
      



.. admonition:: Example 2 :  Rosenbrook funcion with constraint, Lower and Upperbound

   
   
   .. code-block:: csharp
   
      // Define the quadratic objective function
      static double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
   
      // Define Inequality constraint
      static ColVec Ineq(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;
   
      double[] lb = [0.0, 0.2], ub = [0.5, 0.8], x0 = [0.25, 0.25];
   
      // Solve the optimization problem
      var result = Fmincon(fun, x0, Ineq, null, lb, ub);
      Console.WriteLine($"x = {result.x.T}");
   
   
   
Ouput
   
   .. terminal::
   
      x = 
         0.5000    0.2500
      



.. admonition:: Example 3 :  Rosenbrock function with constraint 

   
   The goal is to find the parameter vector :math:`\mathbf{x} = [x_0, x_1]^T` that
   minimizes the non-convex Rosenbrock objective function:
   
   
   .. math::
   
      \min_{\mathbf{x}} f(x_0, x_1) = 100(x_1 - x_0^2)^2 + (1 - x_0)^2
   
   
   subject to the non-linear inequality constraint restricting the domain to the unit disk:
   
   
   .. math::
   
      g(\mathbf{x}) = x_0^2 + x_1^2 - 1 \le 0
   
   
   * Initial Guess: :math:`\mathbf{x}_0 = (0, 0)`
   
   
   .. code-block:: csharp
   
      // Define the objective function
      static double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
      // Define Inequality constraint
      static ColVec Ineq(ColVec x) => Pow(x[0], 2) + Pow(x[1], 2) - 1;
      double[] x0 = [0, 0];
      var result = Fmincon(fun, x0, Ineq);
      Console.WriteLine($"x = {result.x.T}");
   
   
   
Ouput
   
   .. terminal::
   
      x = 
         0.7864    0.6177
      


.. admonition:: Example 4 :  Rosenbrock function with constraint, Lower and Upperbound </example>

   
   Minimizes the Rosenbrock objective subject to a shifted circular inequality constraint combined with explicit lower (lb) and upper (ub) parameter boundaries:
   
   
   .. math::
   
      g(\mathbf{x}) = (x_0 - 0.333)^2 + (x_1 - 0.333)^2 - 0.11111 \le 0
   
   
   
   .. math::
   
      0.0 \le x_0 \le 0.5, \quad 0.2 \le x_1 \le 0.8
   
   
   
   
   .. code-block:: csharp
   
      // Define the objective function
      static double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
      // Define Inequality constraint
      static ColVec Ineq(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;
   
      double[] lb = [0.0, 0.2], ub = [0.5, 0.8], x0 = [0.25, 0.25];
   
      // Solve the constrained optimization problem
      var result = Fmincon(fun, x0, Ineq, null, lb, ub);
      Console.WriteLine($"x = {result.x.T}");
   
   
   
Ouput
   
   .. terminal::
   
      x = 
         0.5000    0.2500
      



.. admonition:: Example 5 :  Unconstrained Derivative-Free Optimization with Fminsearch 

   
   When gradient information is unavailable or the objective is non-differentiable, optimzation can still be performed using Fminsearch.
   
   Fminsearch uses the Nelder-Mead Simplex algorithm to locate the unconstrained global minimum at :math:`\mathbf{x}^* = (1, 1)` where :math:`f(\mathbf{x}^*) = 0`.
   
   * Initial Guess: :math:`\mathbf{x}_0 = (-1.2, 1.0)`
   
   
   .. code-block:: csharp
   
      // Define unconstrained objective function
      static double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
      double[] x0 = [-1.2, 1.0];
   
      // Solve using Nelder-Mead direct search
      var result = Fminsearch(fun, x0);
      Console.WriteLine($"x = {result.x.T}");
   
   
   
Ouput
   
   .. terminal::
   
      x = 
         1.0000    1.0000
      


.. admonition:: Example 6 :  Global Stochastic Optimization with Genetic Algorithm 

   
   For non-convex or multimodal objective functions where gradient-based solvers risk getting trapped in local minima, the Genetic Algorithm (GA) uses population-based operators to explore bounded search spaces without requiring an initial guess.
   
   
   .. math::
   
      -2.0 \le x_0 \le 2.0, \quad -2.0 \le x_1 \le 2.0
   
   
   
   .. code-block:: csharp
   
      // Define the objective function
      static double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
   
      double[] lb = [-2.0, -2.0];
      double[] ub = [2.0, 2.0];
   
      // Configure GA options
      var opts = OptimSet(PopulationSize: 100, MaxIter: 200);
   
      // Solve for global minimum within bounds
      var result = Ga(fun, lb: lb, ub: ub, options: opts);
      Console.WriteLine($"x = {result.x.T}");
   
   
   
Ouput
   
   .. terminal::
   
      average change in the fitness value is less than FuncTol
      x = 
         1.0010    1.0019
      

