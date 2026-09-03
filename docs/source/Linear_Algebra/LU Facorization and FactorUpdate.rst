LU Facorization and FactorUpdate
================================


1. LU Factorization: The Matrix Backbone
----------------------------------------

LU Factorization (Lower-Upper) is a method of decomposing a square
matrix :math:`A` into the product of two triangular matrices.


.. math::

   A = LU


* **L (Lower Triangular):** Has 1s on the diagonal and non-zero elements below the diagonal. It stores the "elimination steps."
* **U (Upper Triangular):** Has non-zero elements on and above the diagonal. It is the "reduced" form of the matrix.

**Why use it?** Instead of re-calculating the inverse of a matrix
(which is computationally expensive), you solve :math:`Ly = b`
and :math:`Ux = y` using forward and backward substitution.



.. code-block:: csharp

   // Create a sample 3x3 matrix
   Matrix A = new double[,]
   {
       { 4, 3, 2 },
       { 3, 2, 1 },
       { 2, 1, 3 }
   };

   // Perform LU decomposition
   // P is the permutation matrix (for pivoting)
   A.MakeLU();

   Console.WriteLine($"Matrix A:{A}");
   Console.WriteLine($"Matrix L:{A.L_lu}");
   Console.WriteLine($"Matrix U:{A.U_lu}");
   Console.WriteLine($"Matrix P:{A.pi}");



 Ouput

.. terminal::

   Matrix A:
    4   3   2 
    3   2   1 
    2   1   3 
   
   Matrix L:
      1.0000    0.0000    0.0000
      0.5000    1.0000    0.0000
      0.7500    0.5000    1.0000
   
   Matrix U:
      4.0000    3.0000    2.0000
      0.0000   -0.5000    2.0000
      0.0000    0.0000   -1.5000
   
   Matrix P:0,	2,	1

2. Rank-1 Updates: Adjusting the Matrix
---------------------------------------
In many real-time systems (like structural engineering or
machine learning), the matrix :math:`A` changes slightly over time.
A **Rank-1 Update** happens when we add the outer product of two
vectors :math:`u` and :math:`v` to the original matrix:


.. math::

   \tilde{A} = A + uv^T


If we already have the LU factorization of :math:`A`, it is
wasteful to re-compute the LU factorization of :math:`\tilde{A}`
from scratch. Re-computing takes :math:`O(n^3)` operations,
while updating takes only :math:`O(n^2)`.

3. The Sherman-Morrison Formula
-------------------------------
While LU handles the decomposition, the **Sherman-Morrison** formula tells us how the inverse changes after a Rank-1 update:


.. math::

   (A + uv^T)^{-1} = A^{-1} - \frac{A^{-1}uv^TA^{-1}}{1 + v^TA^{-1}u}




4. usage in SepalSolverPython
-----------------------------

This script demonstrates a Rank-1 update and compares the result
to the standard matrix addition.


.. code-block:: csharp


   // Create a sample 3x3 matrix
   Matrix A = new double[,]
   {
       { 4, 3, 2 },
       { 3, 2, 1 },
       { 2, 1, 3 }
   };

   // Vectors for Rank-1 Update
   ColVec U = new double[] { 1, 0, 2 };
   RowVec V = new double[] { 2, -1, 0 };

   // Outer product (Rank-1 matrix)
   Matrix UV = U*V;

   // Updated Matrix
   Matrix A_updated = A + UV;

   // Make LU and then Update
   A.MakeLU(); A.LU_Rank1Update(UV);
   Console.WriteLine($"Matrix A:{A}");
   Console.WriteLine($"Matrix L:{A.L_lu}");
   Console.WriteLine($"Matrix U:{A.U_lu}");
   Console.WriteLine($"Matrix P:{A.pi}");

   // Verify with LU again
   A_updated.MakeLU();
   Console.WriteLine($"Updated Matrix A_tilde:{A_updated}");
   Console.WriteLine($"Matrix L:{A_updated.L_lu}");
   Console.WriteLine($"Matrix U:{A_updated.U_lu}");
   Console.WriteLine($"Matrix P:{A_updated.pi}");




 Ouput

.. terminal::

   Matrix A:
    6   2   2 
    3   2   1 
    6  -1   3 
   
   Matrix L:
      1.0000    0.0000    0.0000
      1.0000    1.0000    0.0000
      0.5000   -0.3333    1.0000
   
   Matrix U:
      6.0000    2.0000    2.0000
      0.0000   -3.0000    1.0000
      0.0000    0.0000    0.3333
   
   Matrix P:0,	2,	1
   Updated Matrix A_tilde:
    6   2   2 
    3   2   1 
    6  -1   3 
   
   Matrix L:
      1.0000    0.0000    0.0000
      1.0000    1.0000    0.0000
      0.5000   -0.3333    1.0000
   
   Matrix U:
      6.0000    2.0000    2.0000
      0.0000   -3.0000    1.0000
      0.0000    0.0000    0.3333
   
   Matrix P:0,	2,	1

5. Applications in Industry
---------------------------

* **Optimization (Quasi-Newton Methods):** Used to update Hessian approximations (BFGS algorithm).
* **Power Systems:** When a transmission line trips, the admittance matrix undergoes a Rank-1 or Rank-2 update.
* **Signal Processing:** Adaptive filtering where the correlation matrix is updated with each new data point.

