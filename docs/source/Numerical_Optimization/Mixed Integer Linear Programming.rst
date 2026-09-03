Mixed Integer Linear Programming
================================

Mixed Integer Programming
-------------------------
The Mixed-Integer Linear Programming (MILP) method extends linear programming by requiring a subset (or all) of the decision variables to take integer values.

While pure Linear Programming problems can be solved efficiently in polynomial time, forcing variables to be integers introduces discrete combinations, making MILP NP-hard. Solvers utilize Branch-and-Bound, Branch-and-Cut, or Cutting-Plane algorithms to navigate the solution tree while leveraging LP relaxations at each node.

Fundamental Components of Mixed-Integer Linear Programming:
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The 3-Step Formulation Process
Consider an optimization problem solved via the Linprog interface with integer constraints:


.. math::

   \min_{\mathbf{x}} \quad \mathbf{f}^T \mathbf{x} \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}, \quad x_j \in \mathbb{Z} \ \forall j \in I


1. Decision Variable Definition: Define continuous variables :math:`x_j \in \mathbb{R}` and discrete/integer variables :math:`x_j \in \mathbb{Z}`.

2. Integer Constraint Mask/Indices: Pass an integer variable specification array (intCon) identifying the exact 1-based or 0-based indices of variables restricted to integer values.

3. Convex Relaxation & Branching: The solver first solves the continuous LP relaxation (ignoring integer restrictions), then iteratively branches on non-integer variable values to construct a search tree bounded by optimal dual bounds.

## Variable Types in Mixed-Integer Solvers


Mixed-integer models accommodate distinct operational variable types:
- Continuous Variables (:math:`x_j \in \mathbb{R}`): Represents divisible quantities such as flow rates, fluid volumes, or financial capital allocations.
- General Integer Variables (:math:`x_j \in \mathbb{Z}`): Represents discrete counts such as the number of active wells, pumps, or batches.
- Binary Variables (:math:`x_j \in \{0, 1\}`): Used for logical decision-making, equipment ON/OFF states, or discrete selection constraints.

Comparison: LP vs. MILP Formulation Parameters
Feature | Pure Linear Programming (LP) | Mixed-Integer Linear Programming (MILP)
Solution Domain | Continuous Polyhedron | Discrete Grid over Polyhedron
Complexity | Polynomial Time | NP-Hard (Combinatorial Search)
Solver Method | Simplex / Interior-Point | Branch-and-Bound / Cutting Planes
Converted Function | Linprog(f, A, b, ...) | Linprog(f, intCon, A, b, ...)

Example 1: Basic Mixed-Integer Linear Program (MILP)
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Enforcing integer restrictions on selected decision variables by providing an integer variable index array intCon:

.. math::

   \min \quad -x_1 - 0.3333 x_2 \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}, \quad x_1 \in \mathbb{Z}



.. code-block:: csharp

   // SOLVE_MILP Solves Mixed-Integer LP where specific variables are constrained to integers
   double[,] A = new double[,]{
       { 1.00,  1.00 },
       { 1.00,  0.25 },
       { 1.00, -1.00 },
       {-0.25, -1.00 },
       {-1.00, -1.00 },
       {-1.00,  1.00 }
   };

   double[] b = [2, 1, 2, 1, -1, 2];
   double[] f = [-1, -1.0 / 3];

   // Define 1-based or 0-based indices of variables that must be integers (e.g., x1 is integer)
   int[] intCon = [0];

   var result = Intlinprog(f, intCon, A, b);
   Console.WriteLine(result);


Ouput

.. terminal::

   
      0.6667
      1.3333
   

Example 2: MILP with Equality Constraints
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Combining integer variable restrictions with linear equality systems :math:`\mathbf{A}_{eq}\mathbf{x} = \mathbf{b}_{eq}` to model fixed discrete relationship balance equations:

\min \quad \mathbf{f}^T \mathbf{x} \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}, \quad \mathbf{A}{eq}\mathbf{x} = \mathbf{b}{eq}, \quad x_1, x_2 \in \mathbb{Z}



.. code-block:: csharp

   double[,] A = new double[,]{
       { 1.00,  1.00 },
       { 1.00,  0.25 },
       { 1.00, -1.00 },
       {-0.25, -1.00 },
       {-1.00, -1.00 },
       {-1.00,  1.00 }
   };

   double[] b = [2, 1, 2, 1, -1, 2];
   double[] f = [-1, -1.0 / 3];
   int[] intCon = [0, 1]; // Both x1 and x2 must be integer values

   double[,] Aeq = new double[,] { { 1, 1.0 / 4 } };
   double[] beq = [1.0 / 2];

   var result = Intlinprog(f, intCon, A, b, Aeq, beq);
   Console.WriteLine(result);


Ouput

.. terminal::

   
    0 
    2 
   


Example 3: Fully Constrained MILP with Binary/Integer Variable Bounds
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Solving a fully constrained MILP with explicit lower (:math:`\mathbf{Lb}`) and upper (:math:`\mathbf{Ub}`) variable bounds, commonly used to model binary 0-1 decision variables (:math:`\mathbf{Lb} = 0, \mathbf{Ub} = 1`):


.. math::

   \min \quad \mathbf{f}^T \mathbf{x} \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}, \quad \mathbf{A}{eq}\mathbf{x} = \mathbf{b}{eq}, \quad \mathbf{Lb} \le \mathbf{x} \le \mathbf{Ub}, \quad x_j \in \mathbb{Z} \ \forall j \in intCon



.. code-block:: csharp

   double[,] A = new double[,]{
       { 1.00,  1.00 },
       { 1.00,  0.25 },
       { 1.00, -1.00 },
       {-0.25, -1.00 },
       {-1.00, -1.00 },
       {-1.00,  1.00 }
   };

   double[] b = [2, 1, 2, 1, -1, 2];
   double[] f = [-1, -1.0 / 3];
   int[] intCon = [0, 1];

   double[,] Aeq = new double[,] { { 1, 1.0 / 4 } };
   double[] beq = [1.0 / 2];
   double[] Lb = [-1, -0.5], Ub = [1.5, 1.25];

   var result = Intlinprog(f, intCon, A, b, Aeq, beq, Lb, Ub);
   Console.WriteLine(result);


Ouput

.. terminal::

   
      0.1875
      1.2500
   


