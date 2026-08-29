NonLinear Optimization
======================


Rosenbrook funcion with constraint
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The goal is to find the parameter vector :math:`\mathbf{x} = [x_0, x_1]^T` that 
minimizes the non - convex Rosenbrock objective function:


.. math::

   \min_{\mathbf{ x} } f(x_0, x_1) = 100(x_1 - x_0 ^ 2) ^ 2 + (1 - x_0) ^ 2


subject to the non-linear inequality constraint restricting the domain to the unit disk:

.. math::

   g(\mathbf{ x}) = x_0 ^ 2 + x_1 ^ 2 - 1 \le 0


* **Initial Guess * *: :math:`\mathbf{ x}_0 = (0, 0)`


.. code-block:: csharp

   Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
   double[] x0 = [0, 0];
   var result = Fmincon(fun, x0, x => Pow(x[0], 2) + Pow(x[1], 2) - 1);
   Console.WriteLine(result);


Ouput

.. terminal::

   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   (
      0.7864
      0.6177
   , 0.04567344593692437, 0,    0.0000, , System.Collections.Generic.List`1[SepalSolver.IterationState])

Rosenbrook funcion with constraint, Lower and Upperbound
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code-block:: csharp

   // Define the quadratic objective function
   double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);

   // Define Inequality constraint
   ColVec Ineqconstraints(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;

   double[] lb = [0.0, 0.2], ub = [0.5, 0.8], x0 = [0.25, 0.25];

   // Solve the optimization problem
   var result = Fmincon(fun, x0, Ineqconstraints, null, lb, ub);
   Console.WriteLine(result);


Ouput

.. terminal::

   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   (
      0.5000
      0.2500
   , 0.25, 1,   -0.0763, , System.Collections.Generic.List`1[SepalSolver.IterationState])

Rosenbrock function with constraint
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The goal is to find the parameter vector :math:\mathbf{x} = [x_0, x_1]^T that
minimizes the non-convex Rosenbrock objective function:


\min_{\mathbf{x}} f(x_0, x_1) = 100(x_1 - x_0^2)^2 + (1 - x_0)^2


subject to the non-linear inequality constraint restricting the domain to the unit disk:


g(\mathbf{x}) = x_0^2 + x_1^2 - 1 \le 0


* Initial Guess: :math:\mathbf{x}_0 = (0, 0)


.. code-block:: csharp

   Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
   double[] x0 = [0, 0];
   var result = Fmincon(fun, x0, x => Pow(x[0], 2) + Pow(x[1], 2) - 1);
   Console.WriteLine(result);


Ouput

.. terminal::

   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Solving not completed
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   (
      0.7864
      0.6177
   , 0.04567344593692437, 0,    0.0000, , System.Collections.Generic.List`1[SepalSolver.IterationState])

Rosenbrock function with constraint, Lower and Upperbound
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Minimizes the Rosenbrock objective subject to a shifted circular inequality constraint combined with explicit lower (lb) and upper (ub) parameter boundaries:


g(\mathbf{x}) = (x_0 - 0.333)^2 + (x_1 - 0.333)^2 - 0.11111 \le 0



0.0 \le x_0 \le 0.5, \quad 0.2 \le x_1 \le 0.8



.. code-block:: csharp

   // Define the objective function
   double fun(ColVec x) => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
   // Define Inequality constraint
   ColVec Ineqconstraints(ColVec x) => Pow(x[0] - 0.333, 2) + Pow(x[1] - 0.333, 2) - 0.11111;

   double[] lb = [0.0, 0.2], ub = [0.5, 0.8], x0 = [0.25, 0.25];

   // Solve the constrained optimization problem
   var result = Fmincon(fun, x0, Ineqconstraints, null, lb, ub);
   Console.WriteLine(result);


Ouput

.. terminal::

   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   Optimal solution found
   (
      0.5000
      0.2500
   , 0.25, 1,   -0.0763, , System.Collections.Generic.List`1[SepalSolver.IterationState])

Unconstrained Derivative-Free Optimization with Fminsearch
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

When gradient information is unavailable or the objective is non-differentiable, Fminsearch uses the Nelder-Mead Simplex algorithm to locate the unconstrained global minimum at :math:\mathbf{x}^* = (1, 1) where :math:f(\mathbf{x}^*) = 0.

* Initial Guess: :math:\mathbf{x}_0 = (-1.2, 1.0)


.. code-block:: csharp

   // Define unconstrained objective function
   Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);
   double[] x0 = [-1.2, 1.0];

   // Solve using Nelder-Mead direct search
   var result = Fminsearch(fun, x0);
   Console.WriteLine(result);


Ouput

.. terminal::

   (
      1.0000
      1.0000
   , 4.4768823537358065E-13, 1, , , System.Collections.Generic.List`1[SepalSolver.IterationState])

Global Stochastic Optimization with Genetic Algorithm
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

For non-convex or multimodal objective functions where gradient-based solvers risk getting trapped in local minima, the Genetic Algorithm (GA) uses population-based operators to explore bounded search spaces without requiring an initial guess.


-2.0 \le x_0 \le 2.0, \quad -2.0 \le x_1 \le 2.0



.. code-block:: csharp

   // Define the objective function
   Func<ColVec, double> fun = x => 100 * Pow(x[1] - x[0] * x[0], 2) + Pow(1 - x[0], 2);

   double[] lb = [-2.0, -2.0];
   double[] ub = [2.0, 2.0];

   // Configure GA options
   var opts = OptimSet(PopulationSize: 100, MaxIter: 200);

   // Solve for global minimum within bounds
   var result = Ga(fun, lb: lb, ub: ub, options: opts);
   Console.WriteLine(result);


Ouput

.. terminal::

   
                                   Best             Mean             Max             Stall
    Generation   Func-count        f(x)             f(x)         Constraints      Generations
   Stopping: no improvement for too long.
   (
      0.9612
      0.9239
   , False, 0.0015085990616608056, 0x1 empty double row vector, 0x1 empty double row vector)


