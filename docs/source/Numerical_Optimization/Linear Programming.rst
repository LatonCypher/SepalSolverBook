Linear Programming
==================



The Linear Programming (LP) method approaches optimization problems where both the objective function and all constraints are linear functions of the decision variables.

LP seeks to maximize or minimize a linear objective function subject to linear inequality constraints, equality constraints, and variable bounds—guaranteeing that any local optimum is also a global optimum due to the convexity of the feasible region.

Fundamental Components of a Linear Program:
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The 3-Step Formulation Process
Consider a standard linear program solved via the Linprog interface:

.. math::

   \min_{\mathbf{x}} \quad \mathbf{f}^T \mathbf{x}

1. Decision Variables: Define the vector of unknown variables :math:`\mathbf{x} = [x_1, x_2]^T` to be optimized.

2. Objective Function Vector: Specify the objective coefficient vector :math:`\mathbf{f}`, defining the linear cost or performance surface to minimize:

.. math::

   f(\mathbf{x}) = f_1 x_1 + f_2 x_2 = \mathbf{f}^T \mathbf{x}

3. Constraints & Bounding Domains: Formulate inequality systems :math:`\mathbf{A}\mathbf{x} \le \mathbf{b}`, equality restrictions :math:`\mathbf{A}_{eq}\mathbf{x} = \mathbf{b}_{eq}`, and lower/upper variable bounds :math:`\mathbf{Lb} \le \mathbf{x} \le \mathbf{Ub}`.

**Constraint Types in Linprog**

The Linprog solver handles three levels of constraint complexity depending on problem parameters:
1.   Inequality Constraints (:math:`\mathbf{A}\mathbf{x} \le \mathbf{b}`): Defines the primary polyhedral boundary of the feasible region.
2.   Equality Constraints (:math:`\mathbf{A}_{eq}\mathbf{x} = \mathbf{b}_{eq}`): Restricts the feasible solution domain to a lower-dimensional affine subspace or hyperplane within the polyhedron.
3.   Variable Bounds (:math:`\mathbf{Lb} \le \mathbf{x} \le \mathbf{Ub}`): Sets direct lower and upper box constraints on individual decision variables, improving solver efficiency.

Comparison: Linprog Formulation Levels

.. list-table:: 
   :header-rows: 1

   * - Feature
     - Basic Inequality LP
     - LP with Equality Constraints
     - Fully Bounded LP
   * - Objective
     - :math:`\min \mathbf{f}^T \mathbf{x}`
     - :math:`\min \mathbf{f}^T \mathbf{x}`
     - :math:`\min \mathbf{f}^T \mathbf{x}`
   * - Inequalities
     - :math:`\mathbf{A}\mathbf{x} \le \mathbf{b}`
     - :math:`\mathbf{A}\mathbf{x} \le \mathbf{b}`
     - :math:`\mathbf{A}\mathbf{x} \le \mathbf{b}`
   * - Equalities
     - None
     - :math:`\mathbf{A}_{eq}\mathbf{x} = \mathbf{b}_{eq}`
     - :math:`\mathbf{A}_{eq}\mathbf{x} = \mathbf{b}_{eq}`
   * - Variable Bounds
     - Implicit / Unbounded
     - Implicit / Unbounded
     - Explicit (:math:`\mathbf{Lb}, \mathbf{Ub}`)


.. admonition:: Example 1 :  Example 1: Standard Linear Program with Inequality Constraints 

   Minimizing a linear objective function subject to a system of linear inequalities defining a 2D polyhedral feasible region:
   
   .. math::
   
      \min \quad -x_1 - 0.3333 x_2 \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}
   
   
   
   .. code-block:: csharp
   
      // SOLVE_LP_SIMPLEX Solves 2D Linear Program using standard Linear Programming solver
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
      var result = Linprog(f, A, b);
      Console.WriteLine(result);
   
   
   
Ouput
   
   .. terminal::
   
      
         0.6667
         1.3333
      



.. admonition:: Example 2 :  Example 2: Linear Program with Inequality and Equality Constraints 

   Incorporating linear equality constraints :math:`\mathbf{A}_{eq}\mathbf{x} = \mathbf{b}_{eq}` to restrict the search space along a hyper-plane intersecting the feasible region:
   
   
   .. math::
   
      \min \quad \mathbf{f}^T \mathbf{x} \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}, \quad x_1 + \frac{1}{4} x_2 = \frac{1}{2}
   
   
   
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
      double[,] Aeq = new double[,] { { 1, 1.0 / 4 } };
      double[] beq = [1.0 / 2];
      var result = Linprog(f, A, b, Aeq, beq);
      Console.WriteLine(result);
   
   
   
Ouput
   
   .. terminal::
   
      
       0 
       2 
      


.. admonition:: Example 3 :  Example 3: Fully Constrained Linear Program with Variable Bounds 

   Enforcing explicit lower (:math:`\mathbf{Lb}`) and upper (:math:`\mathbf{Ub}`) limits on each decision variable alongside inequality and equality systems:
   
   
   .. math::
   
      \min \quad \mathbf{f}^T \mathbf{x} \quad \text{s.t.} \quad \mathbf{A}\mathbf{x} \le \mathbf{b}, \quad \mathbf{A}{eq}\mathbf{x} = \mathbf{b}{eq}, \quad \mathbf{Lb} \le \mathbf{x} \le \mathbf{Ub}
   
   
   
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
      double[] f = [-1, -1.0/3];
      double[,] Aeq = new double[,] { { 1, 1.0 / 4 } };
      double[] beq = [1.0 / 2];
      double[] Lb = [-1, -0.5], Ub = [1.5, 1.25];
      var result = Linprog(f, A, b, Aeq, beq, Lb, Ub);
      Console.WriteLine(result);
   
   
   
Ouput
   
   .. terminal::
   
      
         0.1875
         1.2500
      
