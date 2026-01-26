Large NonLinear Systems
=======================

Solving large nonlinear systems of equations is a central problem in numerical
analysis. Iterative methods, particularly Newton’s method, are widely employed
due to their rapid convergence properties. At the core of these methods lies
the Jacobian matrix, which encodes the local sensitivity of the system of
equations to its variables.

Mathematical Definition
-----------------------
For a system of equations :math:`F(x) = 0`, with :math:`F: \mathbb{R}^n \to \mathbb{R}^n`, the Jacobian is defined as

.. math::

   J(x) =
   \begin{bmatrix}
   \frac{\partial f_1}{\partial x_1} & \cdots & \frac{\partial f_1}{\partial x_n} \\
   \vdots & \ddots & \vdots \\
   \frac{\partial f_n}{\partial x_1} & \cdots & \frac{\partial f_n}{\partial x_n}
   \end{bmatrix}.


Finite Difference Approximation
-------------------------------
When analytic derivatives are unavailable, the Jacobian can be approximated using finite differences. For the :math:`j`-th column, this takes the form

.. math::

   J_{:,j}(x) \approx \frac{F(x + h e_j) - F(x)}{h},

where :math:`h` is a small perturbation and :math:`e_j` is the unit vector in the :math:`j`-th direction.

Newton’s Method Update
----------------------
Newton’s method then updates the solution iteratively as


.. math::

   x_{k+1} = x_k - J(x_k)^{-1} F(x_k).



Examples
--------

.. Admonition:: Example 1 :  

   This example shows how to use features of the fsolve solver to solve large sparse systems of equations effectively. The example uses the objective function, defined for a system of n equations,
   
   .. math::
   
      \begin{array}{c}
      F(1) &=& 3x_1 − 2x_1^2 - 2x_2 + 1 \\
      F(i) &=& 3x_i − 2x_i^2 - x_{i-1} - 2x_{i+1} + 1 \\
      F(n) &=& 3x_n − 2x_n^2 - x_{n-1} + 1 \\
      \end{array}
   
   The equations to solve are :math:`F_i(x) = 0, 1 \leq i \leq n`.The example uses n = 1000.
   
   .. code-block:: csharp
   
      //Large Nonlinear Systems
      int n = 1000;
      Indexer i = new(0, n), j = new(0, n - 1), jp1 = j + 1,
          l = new(1, n - 1), lp1 = l + 1, lm1 = l - 1;
   
      ColVec a = Ones(n-1), b = Ones(n), e = -a,
          c = 2 * e, d, xstart, F = new double[n];
   
      SparseMatrix C, D, E;
   
      ColVec nlsf(ColVec x)
      {
          F[l] = (3 - 2 * x[l]).Times(x[l]) - x[lm1] - 2 * x[lp1] + 1;
          F[n - 1] = (3 - 2 * x[n - 1]) * x[n - 1] - x[n - 2] + 1;
          F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
          return F;
      }
      
      xstart = -b;
      var opts = SolverSet(Display: true);
      var result = Fsolve(nlsf, xstart, opts);
      Console.WriteLine(opts.ans.FunVal.Norm());
   
   
   Ouput
   
   .. terminal::
   
       Iteration    Func-count       f(x)      Norm of Step
           0            1             0           Start
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      Warning: Matrix is close to singular or badly scaled.Results may be inaccurate
      31.796224996062975
   
   
   While finite difference approximations are convenient, they are computationally 
   expensive, introduce numerical errors, and fail to exploit structural
   properties such as sparsity. Analytic Jacobians, or those obtained via
   automatic differentiation, provide greater accuracy, stability, and efficiency,
   making them indispensable for large-scale nonlinear systems.
   
   
   .. code-block:: csharp
   
      //Large Nonlinear Systems
      int n = 1000;
      Indexer i = new(0, n), j = new(0, n - 1), jp1 = j + 1,
          l = new(1, n - 1), lp1 = l + 1, lm1 = l - 1;
   
      ColVec a = Ones(n-1), b = Ones(n), e = -a,
          c = 2 * e, d, xstart, F = new double[n];
   
      SparseMatrix C, D, E;
   
      ColVec nlsf(ColVec x)
      {
          F[l] = (3 - 2 * x[l]).Times(x[l]) - x[lm1] - 2 * x[lp1] + 1;
          F[n - 1] = (3 - 2 * x[n - 1]) * x[n - 1] - x[n - 2] + 1;
          F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
          return F;
      }
   
      Func<ColVec, SparseMatrix> Jac = x =>
      {
          d = -4 * x + 3 * b;
          D = new(i, i, d, n, n);
          C = new(j, jp1, c, n, n);
          E = new(jp1, j, e, n, n);
          return C + D + E;
      };
   
      xstart = -b;
      var opts = SolverSet(Display: true, UserDefinedJac: Jac);
      var result = Fsolve(nlsf, xstart, opts);
      Console.WriteLine(opts.ans.FunVal.Norm());
   
   
   Ouput
   
   .. terminal::
   
       Iteration    Func-count       f(x)      Norm of Step
           0            1             0           Start
           1            2          3.98770       7.92420     
           2            3          0.11320       1.32541     
           3            4        1.317e-004      0.03979     
           4            5        1.065e-009     4.555e-005   
           5            6        7.448e-015     3.582e-010   
           6            7        3.063e-015     3.456e-015   
      3.062686598583602E-15
   


.. code-block:: csharp

   // Large Nonlinear systems
   int n = 10000;
   Indexer odds = new(0, 2, n), evens = odds + 1;
   ColVec xstart = new double[n], One = Ones(n / 2),
       c = -One, d = 10*One, e, F;
   SparseMatrix C, D, E;

   ColVec multirosenbrook(ColVec x)
   {
       // Evaluate the vector function
       F = new double[n];
       F[odds] = 1 - x[odds];
       F[evens] = 10 * (x[evens] - x[odds].Pow(2));
       return F;
   }

   Func<ColVec, SparseMatrix> Jac = x =>
   {
       C = new(odds, odds, c, n, n);
       D = new(evens, evens, d, n, n);
       e = -20 * x[odds];
       E = new(evens, odds, e, n, n);
       return C + D + E;
   };

   var opts = SolverSet(Display: true);
   opts.UserDefinedJacobian = Jac;
   xstart[odds] = -1.9; xstart[evens] = 2;
   var result = Fsolve(multirosenbrook, xstart, opts);



Ouput

.. terminal::

    Iteration    Func-count       f(x)      Norm of Step
        0            1             0           Start
        1            2          5946.76       696.268     
        2            3             0          594.676     
        3            4             0             0        
