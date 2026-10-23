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

   
      0.1759    0.4362    0.6546    0.9387    0.7693
      0.3979    0.2175    0.8957    0.1647    0.5961
      0.2375    0.1416    0.3194    0.3131    0.5810
      0.0895    0.6870    0.2481    0.3717    0.2648
      0.5753    0.1969    0.2279    0.4088    0.6129
   
   EigVecs = 
     0.0513 - 0.5841i ,  0.3150 + 0.2109i , -0.6472 + 0.2275i , -0.3670 - 0.5796i , -0.3696 - 0.0845i 
     0.0411 - 0.4682i , -0.4882 - 0.1404i ,  0.1912 + 0.1761i ,  0.2592 - 0.0198i ,  0.4370 + 0.2590i 
     0.0292 - 0.3329i , -0.0926 - 0.1865i , -0.1519 - 0.1672i , -0.2229 + 0.0371i ,  0.1992 - 0.0606i 
     0.0302 - 0.3441i , -0.0221 + 0.6179i , -0.1947 - 0.2982i , -0.3376 + 0.1134i , -0.4344 + 0.4399i 
     0.0395 - 0.4503i ,  0.1964 - 0.3661i ,  0.5185 + 0.1224i ,  0.4828 + 0.2253i ,  0.1316 - 0.3941i 
   
   EigVals = 
     2.0448 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0311 + 0.4623i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.2048 + 0.2036i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.2048 - 0.2036i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0311 - 0.4623i 
   
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

   
      1.5940    1.5071    1.0120    0.7863    0.9946
      1.5071    1.5630    1.2399    0.9388    1.0131
      1.0120    1.2399    2.1049    1.2273    1.2016
      0.7863    0.9388    1.2273    0.8635    0.8614
      0.9946    1.0131    1.2016    0.8614    1.0201
   
   EigVecs = 
     -0.4924    0.4380   -0.0158   -0.5990   -0.4546
      0.5411   -0.4368   -0.3637   -0.3876   -0.4836
      0.0221    0.3615   -0.4007    0.6565   -0.5267
     -0.5594   -0.6771    0.2121    0.2323   -0.3600
      0.3890    0.1678    0.8136    0.0778   -0.3905
   
   EigVals = 
      0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0918    0.0000    0.0000    0.0000
      0.0000    0.0000    0.1807    0.0000    0.0000
      0.0000    0.0000    0.0000    1.0261    0.0000
      0.0000    0.0000    0.0000    0.0000    5.8469
   


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
