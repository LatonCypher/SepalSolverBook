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
   \cfrac{\partial f_1}{\partial x_1} & \cdots & \cfrac{\partial f_1}{\partial x_n} \\
   \vdots & \ddots & \vdots \\
   \cfrac{\partial f_n}{\partial x_1} & \cdots & \cfrac{\partial f_n}{\partial x_n}
   \end{bmatrix}.


Finite Difference Approximation
-------------------------------
When analytic derivatives are unavailable, the Jacobian can be approximated using finite differences. For the :math:`j`-th column, this takes the form

.. math::

   J_{:,j}(x) \approx \cfrac{F(x + h e_j) - F(x)}{h},

where :math:`h` is a small perturbation and :math:`e_j` is the unit vector in the :math:`j`-th direction.

Newton’s Method Update
----------------------
Newton’s method then updates the solution iteratively as


.. math::

   x_{k+1} = x_k - J(x_k)^{-1} F(x_k).



Examples 1: Solving Large Sparse Systems
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
This example shows how to use features of the fsolve solver to solve large sparse systems of equations effectively. The example uses the objective function, defined for a system of n equations,

.. math::

   \begin{array}{rcl}
   F(1) &=& 3x_1 − 2x_1^2 - 2x_2 + 1 \\
   F(i) &=& 3x_i − 2x_i^2 - x_{i-1} - 2x_{i+1} + 1 \\
   F(n) &=& 3x_n − 2x_n^2 - x_{n-1} + 1 \\
   \end{array}

The equations to solve are :math:`F_i(x) = 0, 1 \leq i \leq n`.The example uses n = 1000.

.. code-block:: csharp

   //Large Nonlinear Systems
   int n = 1000;

   ColVec b = Ones(n), xstart;

   ColVec nlsf(ColVec x)
   {
       ColVec F = new double[n];
       F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
       F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
       F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
       return F;
   }

   xstart = -b;
   var opts = SolverSet(Display: true);
   tic();
   var x = Fsolve(nlsf, xstart, opts);
   Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
   Console.WriteLine(opts.ans.FunVal.Norm());
   Console.WriteLine($"Elapsed time: {toc()} seconds");


Ouput

.. terminal::

    Iteration    Func-count       f(x)      Norm of Step
        0            1          31.7962        start      
        1           997         3.98768       7.92421     
        2           998         0.65762       1.13502     
        3           999         0.02943       0.22302     
        4           1000        0.00773       0.00828     
        5           1001        0.00232       0.00181     
        6           1002      4.585e-005     7.742e-004   
        7           1003      1.117e-005     1.109e-005   
        8           1004      1.841e-006     2.577e-006   
        9           1005      1.071e-007     5.041e-007   
        10          1006      2.527e-008     1.911e-008   
   x = 
     -0.5708
     -0.6819
     -0.7025
     -0.7063
     -0.7070
     -0.7071
     -0.7071
     -0.7071
     -0.7071
     -0.7071
        ... 
     -0.7071
     -0.7070
     -0.7068
     -0.7064
     -0.7051
     -0.7015
     -0.6919
     -0.6658
     -0.5960
     -0.4164
   
   2.5270816374876195E-08
   Elapsed time: 3.4781128 seconds

While finite difference approximations are convenient, they are computationally 
expensive, introduce numerical errors, and fail to exploit structural
properties such as sparsity. Analytic Jacobians, or those obtained via
automatic differentiation, provide greater accuracy, stability, and efficiency,
making them indispensable for large-scale nonlinear systems.

Examples 2: Solving Large Sparse Systems Using Sparsity Pattern Exploitation
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


.. code-block:: csharp

   //Large Nonlinear Systems
   int n = 1000;
   Indexer i = 0..n, j = 0..(n - 1), jp1 = 1..n;

   ColVec e = Ones(n), xstart = -e;

   ColVec nlsf(ColVec x)
   {
       ColVec F = new double[n];
       F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
       F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
       F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
       return F;
   }
   List<int> K = [-1, 0, 1];
   List<ColVec> Diagonals = [e, e, e];
   SparseMatrix Jpattern = Spdiags(Diagonals, K, n);

   var opts = SolverSet(Display: true, Jpattern: Jpattern);
   tic();
   var x = Fsolve(nlsf, xstart, opts);
   Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
   Console.WriteLine(opts.ans.FunVal.Norm());
   Console.WriteLine($"Elapsed time: {toc()} seconds");


Ouput

.. terminal::

    Iteration    Func-count       f(x)      Norm of Step
        0            1          31.7962        start      
        1            5          3.98769       7.92421     
        2            9          0.11320       1.32541     
        3            13       1.317e-004      0.03979     
        4            17       1.002e-009     4.553e-005   
        5            21       3.420e-015     3.361e-010   
   x = 
     -0.5708
     -0.6819
     -0.7025
     -0.7063
     -0.7070
     -0.7071
     -0.7071
     -0.7071
     -0.7071
     -0.7071
        ... 
     -0.7071
     -0.7070
     -0.7068
     -0.7064
     -0.7051
     -0.7015
     -0.6919
     -0.6658
     -0.5960
     -0.4164
   
   3.420135685938544E-15
   Elapsed time: 0.0536968 seconds

Examples 3: Solving Large Sparse Systems Using Analytical Jacobians
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


.. code-block:: csharp

   //Large Nonlinear Systems
   int n = 1000;
   Indexer i = (0..n), j = 0..(n - 1), jp1 = 1..n;

   ColVec a = Ones(n - 1), b = Ones(n), e = -a,
       c = 2 * e, d, xstart;

   ColVec nlsf(ColVec x)
   {
       ColVec F = new double[n];
       F[0] = (3 - 2 * x[0]) * x[0] - 2 * x[1] + 1;
       F[1..^1] = (3 - 2 * x[1..^1]).Times(x[1..^1]) - x[..^2] - 2 * x[2..] + 1;
       F[^1] = (3 - 2 * x[^1]) * x[^1] - x[^2] + 1;
       return F;
   }

   SparseMatrix C, D, E;
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
   tic();
   var x = Fsolve(nlsf, xstart, opts);
   Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
   Console.WriteLine(opts.ans.FunVal.Norm());
   Console.WriteLine($"Elapsed time: {toc()} seconds");


Ouput

.. terminal::

    Iteration    Func-count       f(x)      Norm of Step
        0            1          31.7962        start      
        1            2          3.98770       7.92420     
        2            3          0.11320       1.32541     
        3            4        1.317e-004      0.03979     
        4            5        1.065e-009     4.555e-005   
        5            6        7.448e-015     3.582e-010   
   x = 
     -0.5708
     -0.6819
     -0.7025
     -0.7063
     -0.7070
     -0.7071
     -0.7071
     -0.7071
     -0.7071
     -0.7071
        ... 
     -0.7071
     -0.7070
     -0.7068
     -0.7064
     -0.7051
     -0.7015
     -0.6919
     -0.6658
     -0.5960
     -0.4164
   
   7.448429925158487E-15
   Elapsed time: 0.0237041 seconds


Examples 4: Solving Large Sparse Systems Using Analytical Jacobians
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Multirosenbrook function is another example


.. math::

   \begin{array}{rcl}
   F_{2n} &=& 1 - x_{2n} \\
   F_{2n+1} &=& 10(x_{2n + 1} - x_{2n}^2)
   \end{array}



.. code-block:: csharp

   // Large Nonlinear systems
   int n = 1000;
   Indexer odds = (0..n).Step(2), evens = odds + 1;
   ColVec multirosenbrook(ColVec x)
   {
       // Evaluate the vector function
       ColVec F = new double[n];
       F[odds] = 1 - x[odds];
       F[evens] = 10 * (x[evens] - x[odds].Pow(2));
       return F;
   }

   SparseMatrix C, D, E;
   Func<ColVec, SparseMatrix> Jac = x =>
   {
       ColVec one = Ones(n/2), x2n = x[odds];
       C = new(odds, odds, -one, n, n);
       D = new(evens, evens, 10*one, n, n);
       E = new(evens, odds, -20 * x2n, n, n);
       return C + D + E;
   };

   ColVec xstart = new double[n];
   xstart[odds] = -1.9; xstart[evens] = 2;
   var opts = SolverSet(Display: true, UserDefinedJac: Jac);
   tic();
   var x = Fsolve(multirosenbrook, xstart, opts);
   Console.WriteLine($"x = {x[..10]}     ... {x[^10..]}");
   Console.WriteLine(opts.ans.FunVal.Norm());
   Console.WriteLine($"Elapsed time: {toc()} seconds");


Ouput

.. terminal::

    Iteration    Func-count       f(x)      Norm of Step
        0            1          365.800        start      
        1            2          1880.53       220.179     
        2            3             0          188.053     
        3            4             0             0        
   x = 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
        ... 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
    1 
   
   0
   Elapsed time: 0.0070047 seconds
