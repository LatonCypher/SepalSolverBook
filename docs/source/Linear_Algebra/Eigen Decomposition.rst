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

   
      0.3886    0.5325    0.8192    0.5281    0.6706
      0.8080    0.8015    0.1578    0.1947    0.7856
      0.5151    0.3906    0.0515    0.8957    0.9713
      0.4724    0.5824    0.0228    0.3673    0.2923
      0.9324    0.6022    0.2908    0.5442    0.7494
   
   EigVecs = 
     0.4777 - 0.0000i ,  0.6923 - 0.1591i , -0.1591 + 0.6923i , -0.4063 + 0.0170i , -0.0170 + 0.4063i 
     0.4777 + 0.0000i , -0.2684 + 0.0704i ,  0.0704 - 0.2684i ,  0.6120 + 0.1291i , -0.1291 - 0.6120i 
     0.4447 - 0.0000i ,  0.0031 + 0.5111i ,  0.5111 + 0.0031i , -0.2639 - 0.1681i ,  0.1681 + 0.2639i 
     0.2826 - 0.0000i , -0.1286 + 0.0386i ,  0.0386 - 0.1286i ,  0.1536 - 0.4759i ,  0.4759 - 0.1536i 
     0.5157 + 0.0000i , -0.3312 - 0.1717i , -0.1717 - 0.3312i , -0.1392 + 0.2759i , -0.2759 + 0.1392i 
   
   EigVals = 
     2.7199 - 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i , -0.3159 + 0.3601i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.3159 - 0.3601i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.1351 + 0.3224i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.1351 - 0.3224i 
   
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

   
      0.8252    0.5085    0.8538    1.0274    0.5494
      0.5085    0.7930    0.5289    1.0415    0.4968
      0.8538    0.5289    1.0451    1.2083    0.7441
      1.0274    1.0415    1.2083    1.9219    1.1522
      0.5494    0.4968    0.7441    1.1522    1.1849
   
   EigVecs = 
      0.4243    0.6143    0.3980    0.3843   -0.3695
     -0.4344    0.3809   -0.6947    0.2714   -0.3316
     -0.6445   -0.2794    0.5314    0.1919   -0.4328
      0.4607   -0.5592   -0.2705    0.0583   -0.6313
     -0.0601    0.2947    0.0589   -0.8593   -0.4095
   
   EigVals = 
      0.0453    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0804    0.0000    0.0000    0.0000
      0.0000    0.0000    0.4605    0.0000    0.0000
      0.0000    0.0000    0.0000    0.5380    0.0000
      0.0000    0.0000    0.0000    0.0000    4.6460
   


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
