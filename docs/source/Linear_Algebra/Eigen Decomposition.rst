Eigen Decomposition
===================

Eigen Decomposition is a fundamental concept in linear algebra that allows us to analyze the intrinsic properties of square matrices. It provides a way to express a matrix in terms of its eigenvalues and eigenvectors, revealing important characteristics about the transformation it represents.
Also called Spectral Decomposition, Eigen Decomposition is the foundational cornerstone of matrix theory and dynamic systems analysis. It provides an intrinsic coordinate system where a linear transformation behaves simply as independent scalar stretchings along fixed spatial directions.

**Definition**
For a square matrix :math:`A`, an eigenvalue :math:`\lambda` and its corresponding eigenvector :math:`v` satisfy the equation:

.. math::

   A v = \lambda v


Where:

- :math:`\mathbf{\lambda}`: The eigenvalue, a scalar that indicates how much the eigenvector is stretched or compressed during the transformation.
- :math:`\mathbf{v}`: The eigenvector, a non-zero vector that remains in the same direction after the transformation by :math:`A`.

**Eigen Decomposition**
If a matrix :math:`A` has :math:`n` linearly independent eigenvectors, it can be decomposed as:

.. math::

   A = V \Lambda V^{-1}


Where:

- :math:`\mathbf{V}`: An :math:`n \times n` matrix whose columns :math:`v_i` are the **eigenvectors** of :math:`A`. These define the invariant axes where transformation under :math:`A` does not rotate the vector.
- :math:`\mathbf{\Lambda}`: An :math:`n \times n` **diagonal** matrix containing the **eigenvalues** (:math:`\lambda_i`). Each entry specifies the scale factor by which the corresponding eigenvector is dilated, compressed, or flipped.
- :math:`\mathbf{V^{-1}}`: The inverse matrix of eigenvectors, projecting general vectors from the original basis into the eigenspace coordinates. (For symmetric matrices, :math:`V` is orthogonal, yielding :math:`V^{-1} = V^T`).

**Geometric Interpretation**
Applying an arbitrary linear transformation typically rotates and scales a vector simultaneously. Eigen decomposition reveals the unique directions where the transformation acts strictly as an axis-aligned stretch:

1. **Change of Basis:** Map the coordinate system from the standard Cartesian basis into the eigenspace (via :math:`V^{-1}`).
2. **Independent Scaling:** Scale each coordinate independently along its eigen-axis by factor :math:`\lambda_i` (via :math:`\Lambda`).
3. **Reconstruction:** Map coordinates back to the original physical space (via :math:`V`).

**Applications in Technology**
**1. Modal Analysis and Structural Dynamics**
In mechanical and civil engineering, complex multi-degree-of-freedom vibrations reduce to independent natural frequencies. The eigenvalues of the stiffness-mass pencil define natural frequencies (resonance points), while the eigenvectors represent the corresponding physical mode shapes.

**2. State-Space Stability and Differential Equations**
For continuous linear dynamic systems :math:`\dot{x}(t) = A x(t)`, the solution expands as :math:`x(t) = V e^{\Lambda t} V^{-1} x(0)`. The real parts of the eigenvalues (:math:`\text{Re}(\lambda_i)`) strictly dictate asymptotic stability, damping, or explosive divergence.

**3. Graph Algorithms and PageRank**
Google's PageRank algorithm computes the stationary distribution of web-surfer transition matrices via the dominant eigenvector (:math:`\lambda = 1`). Spectral graph theory leverages the eigenvalues of graph Laplacian matrices to cluster networks and partition graphs.

**Numerical Example**
Using **SepalSolver**, we compute the spectral decomposition of a square matrix to evaluate modal directions and characteristic values:

.. code-block:: csharp

   // Symmetric positive-definite matrix
   Matrix A = new double[,]
   {
       { 4, 1 },
       { 1, 3 }
   };


   // Perform Eigen Decomposition
   var (V, D) = Eig(A);
   Console.WriteLine($"Eigenvector Matrix V = {V}");
   Console.WriteLine($"Eigenvalue Matrix D  = {D}");




Ouput


.. terminal::

   Eigenvector Matrix V = 
      0.8507   -0.5257
      0.5257    0.8507
   
   Eigenvalue Matrix D  = 
      4.6180    0.0000
      0.0000    2.3820
   
The decomposition of a nonsymmetric matrix is shown below 
Notice that all eigenvalues and eigenvectors are complex.

.. code-block:: csharp

   Matrix A = Rand(5, 5);
   Console.WriteLine(A);
   (var EigVecs, var EigVals) = Eig(A);
   Console.WriteLine($"EigVecs = {EigVecs}");
   Console.WriteLine($"EigVals = {EigVals}");




Ouput


.. terminal::

   
      0.0153    0.7154    0.7135    0.8112    0.7116
      0.2091    0.5349    0.2523    0.0467    0.6270
      0.4935    0.3777    0.0158    0.9534    0.9793
      0.5560    0.4643    0.6802    0.1478    0.5471
      0.9592    0.1264    0.3765    0.2890    0.1949
   
   EigVecs = 
     0.5291 - 0.0000i , -0.1204 - 0.0000i , -0.4224 - 0.0817i , -0.0817 - 0.4224i ,  0.0729 - 0.0000i 
     0.2858 - 0.0000i , -0.1067 - 0.0000i , -0.2233 - 0.2551i , -0.2551 - 0.2233i ,  0.8526 - 0.0000i 
     0.5162 - 0.0000i ,  0.8498 - 0.0000i ,  0.5060 - 0.0776i , -0.0776 + 0.5060i , -0.3851 - 0.0000i 
     0.4557 + 0.0000i , -0.4994 + 0.0000i , -0.3330 - 0.1139i , -0.1139 - 0.3330i , -0.1099 + 0.0000i 
     0.4052 + 0.0000i , -0.0509 + 0.0000i ,  0.3979 + 0.3946i ,  0.3946 + 0.3979i , -0.3277 + 0.0000i 
   
   EigVals = 
     2.3413 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i , -0.7205 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.4519 + 0.2073i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.4519 - 0.2073i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.1918 + 0.0000i 
   
The decomposition of a symmetric matrix is shown below 
Notice that all eigenvalues and eigenvectors are real.

.. code-block:: csharp

   Matrix A = Rand(5, 5);
   Matrix ATA = A.T * A;
   Console.WriteLine(ATA);
   (var EigVecs, var EigVals) = Eig(ATA);
   Console.WriteLine($"EigVecs = {EigVecs}");
   Console.WriteLine($"EigVals = {EigVals}");




Ouput


.. terminal::

   
      2.3700    2.6513    1.4166    1.1551    0.7445
      2.6513    3.1042    1.5338    1.2584    0.7838
      1.4166    1.5338    1.7881    1.3095    0.8722
      1.1551    1.2584    1.3095    1.3418    0.6285
      0.7445    0.7838    0.8722    0.6285    0.5167
   
   EigVecs = 
     -0.6561    0.4037   -0.0347    0.3326   -0.5429
      0.5319   -0.3437    0.0438    0.4704   -0.6130
     -0.2179   -0.4654   -0.4692   -0.5878   -0.4127
      0.0528    0.1071    0.7899   -0.4969   -0.3391
      0.4861    0.7006   -0.3909   -0.2753   -0.2102
   
   EigVals = 
      0.0466    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0778    0.0000    0.0000    0.0000
      0.0000    0.0000    0.2718    0.0000    0.0000
      0.0000    0.0000    0.0000    1.2745    0.0000
      0.0000    0.0000    0.0000    0.0000    7.4499
   


.. list-table:: **Summary of Matrix Properties**
   :header-rows: 1

   * - Component
     - Property
     - Geometric Meaning
   * - :math:`\mathbf{V}`
     - Linearly Independent Columns
     - Modal directions (invariant axes)
   * - :math:`\mathbf{\Lambda}`
     - Diagonal Matrix
     - Scaling along invariant axes
   * - :math:`\mathbf{V^{-1}}`
     - :math:`V^{-1}V = I` (or :math:`V^T` if symmetric)
     - Projection into the modal coordinate system
