Eigen Decomposition
===================

Eigen Decomposition is a fundamental concept in linear algebra that allows us to analyze the intrinsic properties of square matrices. It provides a way to express a matrix in terms of its eigenvalues and eigenvectors, revealing important characteristics about the transformation it represents.
Also called Spectral Decomposition, Eigen Decomposition is the foundational cornerstone of matrix theory and dynamic systems analysis. It provides an intrinsic coordinate system where a linear transformation behaves simply as independent scalar stretchings along fixed spatial directions.

**Definition**
For a square matrix $A$, an eigenvalue $\lambda$ and its corresponding eigenvector $v$ satisfy the equation:

.. math::

   A v = \lambda v


Where:
* **:math:`\lambda`**: The eigenvalue, a scalar that indicates how much the eigenvector is stretched or compressed during the transformation.
* **:math:`v`**: The eigenvector, a non-zero vector that remains in the same direction after the transformation by $A$.

**Eigen Decomposition**
If a matrix $A$ has $n$ linearly independent eigenvectors, it can be decomposed as:

.. math::

   A = V \Lambda V^{-1}


Where:
- **:math:`V**`: An $n \times n$ matrix whose columns $v_i$ are the **eigenvectors** of $A$. These define the invariant axes where transformation under $A$ does not rotate the vector.
- **:math:`\Lambda**`: An $n \times n$ **diagonal** matrix containing the **eigenvalues** ($\lambda_i$). Each entry specifies the scale factor by which the corresponding eigenvector is dilated, compressed, or flipped.
- **:math:`V^{-1}**`: The inverse matrix of eigenvectors, projecting general vectors from the original basis into the eigenspace coordinates. (For symmetric matrices, $V$ is orthogonal, yielding $V^{-1} = V^T$).

**Geometric Interpretation**
Applying an arbitrary linear transformation typically rotates and scales a vector simultaneously. Eigen decomposition reveals the unique directions where the transformation acts strictly as an axis-aligned stretch:
1. **Change of Basis:** Map the coordinate system from the standard Cartesian basis into the eigenspace (via $V^{-1}$).
2. **Independent Scaling:** Scale each coordinate independently along its eigen-axis by factor $\lambda_i$ (via $\Lambda$).
3. **Reconstruction:** Map coordinates back to the original physical space (via $V$).

**Applications in Technology**
**1. Modal Analysis and Structural Dynamics**
In mechanical and civil engineering, complex multi-degree-of-freedom vibrations reduce to independent natural frequencies. The eigenvalues of the stiffness-mass pencil define natural frequencies (resonance points), while the eigenvectors represent the corresponding physical mode shapes.

**2. State-Space Stability and Differential Equations**
For continuous linear dynamic systems $\dot{x}(t) = A x(t)$, the solution expands as $x(t) = V e^{\Lambda t} V^{-1} x(0)$. The real parts of the eigenvalues ($\text{Re}(\lambda_i)$) strictly dictate asymptotic stability, damping, or explosive divergence.

**3. Graph Algorithms and PageRank**
Google's PageRank algorithm computes the stationary distribution of web-surfer transition matrices via the dominant eigenvector ($\lambda = 1$). Spectral graph theory leverages the eigenvalues of graph Laplacian matrices to cluster networks and partition graphs.

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

   
      0.1698    0.2091    0.8859    0.3687    0.9795
      0.0533    0.7717    0.5982    0.9121    0.3392
      0.2026    0.4721    0.7558    0.9953    0.2749
      0.5435    0.9568    0.8533    0.1408    0.6061
      0.5091    0.2987    0.0876    0.4075    0.2295
   
   EigVecs = 
     0.4119 - 0.0000i , -0.6103 + 0.1781i ,  0.1781 - 0.6103i ,  0.6495 + 0.0000i ,  0.1297 + 0.0000i 
     0.4902 - 0.0000i , -0.0589 - 0.1852i , -0.1852 - 0.0589i , -0.5344 + 0.0000i , -0.3880 + 0.0000i 
     0.4979 - 0.0000i ,  0.1036 - 0.2795i , -0.2795 + 0.1036i , -0.1713 + 0.0000i ,  0.7304 - 0.0000i 
     0.5234 + 0.0000i , -0.0607 + 0.5109i ,  0.5109 - 0.0607i ,  0.0157 - 0.0000i , -0.0629 + 0.0000i 
     0.2610 - 0.0000i ,  0.4223 - 0.1619i , -0.1619 + 0.4223i ,  0.5128 - 0.0000i , -0.5433 - 0.0000i 
   
   EigVals = 
     2.5785 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i , -0.6540 + 0.1800i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i , -0.6540 - 0.1800i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.5463 + 0.0000i ,  0.0000 + 0.0000i 
     0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.0000 + 0.0000i ,  0.2508 + 0.0000i 
   
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

   
      1.7394    1.1319    1.0043    1.6859    1.6508
      1.1319    1.6480    0.5863    1.8520    1.2351
      1.0043    0.5863    1.0871    1.2063    1.4165
      1.6859    1.8520    1.2063    2.9848    2.0284
      1.6508    1.2351    1.4165    2.0284    2.0501
   
   EigVecs = 
     -0.1695    0.4166    0.7438   -0.2582   -0.4217
     -0.1532   -0.6240    0.3019    0.5905   -0.3839
     -0.6623   -0.2982   -0.3366   -0.5146   -0.3070
     -0.0261    0.5119   -0.4842    0.4020   -0.5841
      0.7130   -0.2934   -0.0887   -0.3978   -0.4893
   
   EigVals = 
      0.0023    0.0000    0.0000    0.0000    0.0000
      0.0000    0.2339    0.0000    0.0000    0.0000
      0.0000    0.0000    0.4502    0.0000    0.0000
      0.0000    0.0000    0.0000    1.0709    0.0000
      0.0000    0.0000    0.0000    0.0000    7.7521
   
**Summary of Matrix Properties**

Component | Property | Geometric Meaning
**:math:`V**` | Linearly Independent Columns | Modal directions (invariant axes)
**:math:`\Lambda**` | Diagonal Matrix | Scaling along invariant axes
**:math:`V^{-1}**` | :math:`V^{-1}V = I` (or :math:`V^T` if symmetric) | Projection into the modal coordinate system

