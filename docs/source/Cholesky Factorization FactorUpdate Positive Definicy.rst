Cholesky Factorization FactorUpdate Positive Definicy
=====================================================

The **Cholesky factorization** (also called Cholesky decomposition) is a matrix decomposition technique used in linear algebra. It applies to Hermitian, positive-definite matrices (in the real case, symmetric positive-definite matrices).

Definition
~~~~~~~~~~
Given a Hermitian positive-definite matrix ``A``, the Cholesky decomposition expresses ``A`` as:

.. math::

   A = L \cdot L^*

where:
- :math:`L` is a lower triangular matrix with strictly positive diagonal entries.
- :math:`L^*` denotes the conjugate transpose of :math:`L` (for real matrices, this is simply the transpose).

Properties
~~~~~~~~~~
- **Existence**: Every Hermitian positive-definite matrix has a Cholesky decomposition.
- **Uniqueness**: The decomposition is unique.
- **Efficiency**: Roughly twice as efficient as LU decomposition for solving systems of linear equations. 

- **Applications**:
* Numerical solutions of linear systems.
* Monte Carlo simulations.
* Optimization problems.
* Covariance matrix factorization in statistics.

.. Admonition:: Example 1 : 

   Consider the matrix:
   
   .. math::
   
      A = \begin{bmatrix}
      4 & 12 & -16  \\
      12 & 37 & -43 \\
      -16 & -43 & 98
      \end{bmatrix}
   
   
   Its Cholesky factorization is:
   
   .. math::
   
      L = \begin{bmatrix}
      2 & 0 & 0 \\
      6 & 1 & 0 \\
      -8 & 5 & 3
      \end{bmatrix}
   

Using SepalSolver, we call Chol on an instance of the matrix. 

.. Admonition:: Example 2 :         

   
   .. code-block:: csharp
   
      Matrix A = new double[,] 
      {
          { 22.7345,    1.8859,         0,         0,    1.3000 },
          {  1.8859,   22.2340,    2.0461,         0,         0 },
          {       0,    2.0461,   22.7591,    2.4606,         0 },
          {       0,         0,    2.4606,   22.5848,    2.2768 },
          {  1.3000,         0,         0,    2.2768,   22.4853 }
      };
      Matrix L = Chol(A);
      Console.WriteLine(L);
      if(A.IsPosDef)
          Console.WriteLine("A is positive definite");
   
   
   Ouput
   
   .. terminal::
   
      
      A is positive definite

Like LU factors, Cholesky factors can be updated too, when the Matrix undergoes rank1 update. 


.. code-block:: csharp

   Matrix A = new double[,]
   {
       { 22.7345,    1.8859,         0,         0,    1.3000 },
       {  1.8859,   22.2340,    2.0461,         0,         0 },
       {       0,    2.0461,   22.7591,    2.4606,         0 },
       {       0,         0,    2.4606,   22.5848,    2.2768 },
       {  1.3000,         0,         0,    2.2768,   22.4853 }
   };

   // Vectors for Rank-1 Update
   ColVec x = new double[] { 1, 0, 1, 0, 1 };

   // Outer product (Rank-1 matrix)
   Matrix xxT = x*x.T;

   // Updated Matrix
   Matrix A_updated = A + xxT;

   // Make Cholesky and then Update
   A.MakeChol(); A.Chol_Rank1Update(xxT);
   Console.WriteLine($"Matrix A:{A}");
   Console.WriteLine($"Matrix L:{A.L_chol}");

   // Verify with Cholesky again
   A_updated.MakeChol();
   Console.WriteLine($"Updated Matrix A_tilde:{A_updated}");
   Console.WriteLine($"Matrix L:{A_updated.L_chol}");


Ouput

.. terminal::

   Matrix A:
     23.7345    1.8859    1.0000    0.0000    2.3000
      1.8859   22.2340    2.0461    0.0000    0.0000
      1.0000    2.0461   23.7591    2.4606    1.0000
      0.0000    0.0000    2.4606   22.5848    2.2768
      2.3000    0.0000    1.0000    2.2768   23.4853
   
   Matrix L:
      4.8718    0.0000    0.0000    0.0000    0.0000
      0.3871    4.6994    0.0000    0.0000    0.0000
      0.2053    0.4185    4.8520    0.0000    0.0000
      0.0000    0.0000    0.5071    4.7252    0.0000
      0.4721   -0.0389    0.1895    0.4615    4.7971
   
   Updated Matrix A_tilde:
     23.7345    1.8859    1.0000    0.0000    2.3000
      1.8859   22.2340    2.0461    0.0000    0.0000
      1.0000    2.0461   23.7591    2.4606    1.0000
      0.0000    0.0000    2.4606   22.5848    2.2768
      2.3000    0.0000    1.0000    2.2768   23.4853
   
   Matrix L:
      4.8718    0.0000    0.0000    0.0000    0.0000
      0.3871    4.6994    0.0000    0.0000    0.0000
      0.2053    0.4185    4.8520    0.0000    0.0000
      0.0000    0.0000    0.5071    4.7252    0.0000
      0.4721   -0.0389    0.1895    0.4615    4.7971
   
