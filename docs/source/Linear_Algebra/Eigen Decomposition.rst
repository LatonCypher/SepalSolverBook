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

   
      0.9291    0.6858    0.8812    0.9490    0.6862
      0.8006    0.5271    0.9866    0.6281    0.9994
      0.3280    0.6954    0.6319    0.0507    0.2273
      0.7901    0.2663    0.0566    0.3279    0.0464
      0.6256    0.7345    0.4789    0.0827    0.3300
   
   EigVecs = 
      0.6047    0.3963   -0.3375   -0.5155   -0.1245
      0.5698   -0.1757   -0.6333    0.6145    0.3762
      0.3103   -0.5328    0.3193   -0.3336   -0.6933
      0.2615    0.6845    0.3882    0.4950    0.0449
      0.3807   -0.2443    0.4820   -0.0185    0.6003
   
   EigVals = 
      2.8699    0.0000    0.0000    0.0000    0.0000
      0.0000    0.6563    0.0000    0.0000    0.0000
      0.0000    0.0000   -0.6892    0.0000    0.0000
      0.0000    0.0000    0.0000   -0.2045    0.0000
      0.0000    0.0000    0.0000    0.0000    0.1135
   
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

   
      1.7931    1.7174    0.5197    0.6076    0.3906
      1.7174    2.0881    0.6931    0.5833    0.4715
      0.5197    0.6931    0.4283    0.2074    0.1625
      0.6076    0.5833    0.2074    0.2842    0.0620
      0.3906    0.4715    0.1625    0.0620    0.1812
   
   EigVecs = 
      0.2512    0.3986   -0.0285    0.6170   -0.6297
      0.0906   -0.5152    0.3044   -0.3952   -0.6910
      0.1326    0.4287   -0.6213   -0.5982   -0.2338
     -0.6781   -0.3509   -0.5518    0.2560   -0.2168
     -0.6718    0.5184    0.4647   -0.1992   -0.1560
   
   EigVals = 
      0.0020    0.0000    0.0000    0.0000    0.0000
      0.0000    0.1054    0.0000    0.0000    0.0000
      0.0000    0.0000    0.1752    0.0000    0.0000
      0.0000    0.0000    0.0000    0.3154    0.0000
      0.0000    0.0000    0.0000    0.0000    4.1768
   


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
