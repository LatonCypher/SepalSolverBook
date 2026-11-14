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

   
      0.5490    0.0218    0.4702    0.6464    0.6845
      0.2021    0.4525    0.8364    0.0400    0.8851
      0.5745    0.5368    0.1566    0.5923    0.1855
      0.4911    0.5743    0.1902    0.0422    0.6774
      0.4788    0.2165    0.2775    0.1234    0.3308
   
   EigVecs = 
     0.4946 + 0.0000i , -0.4798 + 0.3080i , -0.3008 - 0.5403i , -0.3143 + 0.2785i ,  0.0970 - 0.4086i 
     0.5061 - 0.0000i ,  0.5997 - 0.3850i , -0.2006 - 0.3603i ,  0.3031 - 0.0063i ,  0.1377 + 0.2701i 
     0.4679 - 0.0000i ,  0.2726 - 0.1750i ,  0.1551 + 0.2786i , -0.4986 - 0.0206i , -0.2537 - 0.4297i 
     0.4253 - 0.0000i ,  0.0924 - 0.0593i ,  0.2537 + 0.4557i ,  0.2211 - 0.5667i , -0.3950 + 0.4626i 
     0.3152 - 0.0000i , -0.1885 + 0.1210i ,  0.1322 + 0.2374i ,  0.3202 + 0.1008i ,  0.2401 + 0.2346i 
   
   EigVals = 
     2.0081 - 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.3990 - 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.5249 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.1755 + 0.3353i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.1755 - 0.3353i 
   
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

   
      1.1647    1.3777    0.8820    0.6869    1.5869
      1.3777    1.9541    1.5582    1.0959    2.1589
      0.8820    1.5582    1.6443    1.1004    1.5852
      0.6869    1.0959    1.1004    1.2098    1.0733
      1.5869    2.1589    1.5852    1.0733    2.7073
   
   EigVecs = 
      0.4949    0.5627   -0.4346    0.3469   -0.3594
     -0.7721    0.3653    0.0675    0.0635   -0.5118
      0.3800    0.1905    0.6180   -0.5114   -0.4193
     -0.0311   -0.2690   -0.6428   -0.6460   -0.3101
      0.1163   -0.6643    0.1066    0.4437   -0.5805
   
   EigVals = 
      0.0228    0.0000    0.0000    0.0000    0.0000
      0.0000    0.1559    0.0000    0.0000    0.0000
      0.0000    0.0000    0.3231    0.0000    0.0000
      0.0000    0.0000    0.0000    0.8672    0.0000
      0.0000    0.0000    0.0000    0.0000    7.3112
   


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
