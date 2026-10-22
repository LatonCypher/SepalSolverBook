Eigen Decomposition
===================

Eigen Decomposition is a fundamental concept in linear algebra that allows us to analyze the intrinsic properties of square matrices. It provides a way to express a matrix in terms of its eigenvalues and eigenvectors, revealing important characteristics about the transformation it represents.
Also called Spectral Decomposition, Eigen Decomposition is the foundational cornerstone of matrix theory and dynamic systems analysis. It provides an intrinsic coordinate system where a linear transformation behaves simply as independent scalar stretchings along fixed spatial directions.

**Definition**
For a square matrix :math:`A`, an eigenvalue :math:`\lambda` and its corresponding eigenvector :math:`v` satisfy the equation:

.. math::

   A v = \lambda v


Where:

- **:math:`\lambda`**: The eigenvalue, a scalar that indicates how much the eigenvector is stretched or compressed during the transformation.
- **:math:`v`**: The eigenvector, a non-zero vector that remains in the same direction after the transformation by :math:`A`.

**Eigen Decomposition**
If a matrix :math:`A` has :math:`n` linearly independent eigenvectors, it can be decomposed as:

.. math::

   A = V \Lambda V^{-1}


Where:

- **:math:`V`**: An :math:`n \times n` matrix whose columns :math:`v_i` are the **eigenvectors** of :math:`A`. These define the invariant axes where transformation under :math:`A` does not rotate the vector.
- **:math:`\Lambda`**: An :math:`n \times n` **diagonal** matrix containing the **eigenvalues** (:math:`\lambda_i`). Each entry specifies the scale factor by which the corresponding eigenvector is dilated, compressed, or flipped.
- **:math:`V^{-1}`**: The inverse matrix of eigenvectors, projecting general vectors from the original basis into the eigenspace coordinates. (For symmetric matrices, :math:`V` is orthogonal, yielding :math:`V^{-1} = V^T`).

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

   
      0.9343    0.8833    0.2119    0.6102    0.1499
      0.4364    0.0890    0.2295    0.8897    0.4150
      0.8583    0.3779    0.4287    0.7690    0.6499
      0.5687    0.3694    0.3974    0.6329    0.1389
      0.5970    0.0021    0.0853    0.0858    0.4044
   
   EigVecs = 
     0.5461 + 0.0000i , -0.2313 + 0.3507i , -0.3507 + 0.2313i , -0.4820 + 0.1114i ,  0.1114 - 0.4820i 
     0.3784 - 0.0000i ,  0.0178 - 0.1726i ,  0.1726 - 0.0178i ,  0.5109 - 0.3447i , -0.3447 + 0.5109i 
     0.5756 - 0.0000i ,  0.4103 - 0.4024i ,  0.4024 - 0.4103i ,  0.0213 - 0.3039i , -0.3039 + 0.0213i 
     0.4245 - 0.0000i , -0.2243 - 0.1341i ,  0.1341 + 0.2243i , -0.0457 + 0.2055i ,  0.2055 - 0.0457i 
     0.2171 - 0.0000i ,  0.6223 + 0.0873i , -0.0873 - 0.6223i ,  0.4798 + 0.0905i ,  0.0905 + 0.4798i 
   
   EigVals = 
     2.3037 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.2478 + 0.2842i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.2478 - 0.2842i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.1550 + 0.2253i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.1550 - 0.2253i 
   
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

   
      2.1793    1.3980    1.1536    1.5885    2.0165
      1.3980    1.2642    1.0530    1.0167    1.3034
      1.1536    1.0530    0.9947    1.0623    1.1759
      1.5885    1.0167    1.0623    1.7636    1.7331
      2.0165    1.3034    1.1759    1.7331    2.0719
   
   EigVecs = 
      0.2534    0.4902   -0.6468   -0.0320   -0.5255
     -0.6268    0.0277    0.1101   -0.6759   -0.3708
      0.6912   -0.0586    0.5157   -0.3765   -0.3333
     -0.2532    0.3651    0.5139    0.5801   -0.4495
     -0.0331   -0.7888   -0.1985    0.2528   -0.5228
   
   EigVals = 
      0.0173    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0583    0.0000    0.0000    0.0000
      0.0000    0.0000    0.3785    0.0000    0.0000
      0.0000    0.0000    0.0000    0.5570    0.0000
      0.0000    0.0000    0.0000    0.0000    7.2625
   
**Summary of Matrix Properties**

.. list-table:: 
   :header-rows: 1

   * - Component
     - Property
     - Geometric Meaning
   * - **:math:`V**`
     - Linearly Independent Columns
     - Modal directions (invariant axes)
   * - **:math:`\Lambda**`
     - Diagonal Matrix
     - Scaling along invariant axes
   * - **:math:`V^{-1}**`
     - :math:`V^{-1}V = I` (or :math:`V^T` if symmetric)
     - Projection into the modal coordinate system
