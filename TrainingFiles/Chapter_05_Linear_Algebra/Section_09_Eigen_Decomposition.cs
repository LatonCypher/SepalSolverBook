using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algebra
{
    public class Section_09_Eigen_Decomposition
    {
        public static void Run()
        {
            /// <BookContent>
            /// Eigen Decomposition is a fundamental concept in linear algebra that allows us to analyze the intrinsic properties of square matrices. It provides a way to express a matrix in terms of its eigenvalues and eigenvectors, revealing important characteristics about the transformation it represents.
            /// Also called Spectral Decomposition, Eigen Decomposition is the foundational cornerstone of matrix theory and dynamic systems analysis. It provides an intrinsic coordinate system where a linear transformation behaves simply as independent scalar stretchings along fixed spatial directions.
            /// 
            /// **Definition**
            /// For a square matrix :math:`A`, an eigenvalue :math:`\lambda` and its corresponding eigenvector :math:`v` satisfy the equation:
            /// <math>
            ///     A v = \lambda v
            /// </math>
            /// 
            /// Where:
            /// 
            /// - **:math:`\lambda`**: The eigenvalue, a scalar that indicates how much the eigenvector is stretched or compressed during the transformation.
            /// - **:math:`v`**: The eigenvector, a non-zero vector that remains in the same direction after the transformation by :math:`A`.
            /// 
            /// **Eigen Decomposition**
            /// If a matrix :math:`A` has :math:`n` linearly independent eigenvectors, it can be decomposed as:
            /// <math>
            ///     A = V \Lambda V^{-1}
            /// </math>
            /// 
            /// Where:
            /// 
            /// - **:math:`V`**: An :math:`n \times n` matrix whose columns :math:`v_i` are the **eigenvectors** of :math:`A`. These define the invariant axes where transformation under :math:`A` does not rotate the vector.
            /// - **:math:`\Lambda`**: An :math:`n \times n` **diagonal** matrix containing the **eigenvalues** (:math:`\lambda_i`). Each entry specifies the scale factor by which the corresponding eigenvector is dilated, compressed, or flipped.
            /// - **:math:`V^{-1}`**: The inverse matrix of eigenvectors, projecting general vectors from the original basis into the eigenspace coordinates. (For symmetric matrices, :math:`V` is orthogonal, yielding :math:`V^{-1} = V^T`).
            ///
            /// **Geometric Interpretation**
            /// Applying an arbitrary linear transformation typically rotates and scales a vector simultaneously. Eigen decomposition reveals the unique directions where the transformation acts strictly as an axis-aligned stretch:
            /// 
            /// 1. **Change of Basis:** Map the coordinate system from the standard Cartesian basis into the eigenspace (via :math:`V^{-1}`).
            /// 2. **Independent Scaling:** Scale each coordinate independently along its eigen-axis by factor :math:`\lambda_i` (via :math:`\Lambda`).
            /// 3. **Reconstruction:** Map coordinates back to the original physical space (via :math:`V`).
            ///
            /// **Applications in Technology**
            /// **1. Modal Analysis and Structural Dynamics**
            /// In mechanical and civil engineering, complex multi-degree-of-freedom vibrations reduce to independent natural frequencies. The eigenvalues of the stiffness-mass pencil define natural frequencies (resonance points), while the eigenvectors represent the corresponding physical mode shapes.
            ///
            /// **2. State-Space Stability and Differential Equations**
            /// For continuous linear dynamic systems :math:`\dot{x}(t) = A x(t)`, the solution expands as :math:`x(t) = V e^{\Lambda t} V^{-1} x(0)`. The real parts of the eigenvalues (:math:`\text{Re}(\lambda_i)`) strictly dictate asymptotic stability, damping, or explosive divergence.
            ///
            /// **3. Graph Algorithms and PageRank**
            /// Google's PageRank algorithm computes the stationary distribution of web-surfer transition matrices via the dominant eigenvector (:math:`\lambda = 1`). Spectral graph theory leverages the eigenvalues of graph Laplacian matrices to cluster networks and partition graphs.
            ///
            /// **Numerical Example**
            /// Using **SepalSolver**, we compute the spectral decomposition of a square matrix to evaluate modal directions and characteristic values:
            /// <code>
            {
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
            }
            /// </code>
            /// The decomposition of a nonsymmetric matrix is shown below 
            /// Notice that all eigenvalues and eigenvectors are complex.
            /// <code>
            {
                Matrix A = Rand(5, 5);
                Console.WriteLine(A);
                (var EigVecs, var EigVals) = Eig(A);
                Console.WriteLine($"EigVecs = {EigVecs}");
                Console.WriteLine($"EigVals = {EigVals}");
            }
            /// </code>
            /// The decomposition of a symmetric matrix is shown below 
            /// Notice that all eigenvalues and eigenvectors are real.
            /// <code>
            {
                Matrix A = Rand(5, 5);
                Matrix ATA = A.T * A;
                Console.WriteLine(ATA);
                (var EigVecs, var EigVals) = Eig(ATA);
                Console.WriteLine($"EigVecs = {EigVecs}");
                Console.WriteLine($"EigVals = {EigVals}");
            }
            /// </code>
            /// **Summary of Matrix Properties**
            /// <table>
            /// Component | Property | Geometric Meaning
            /// **:math:`V**` | Linearly Independent Columns | Modal directions (invariant axes)
            /// **:math:`\Lambda**` | Diagonal Matrix | Scaling along invariant axes
            /// **:math:`V^{-1}**` | :math:`V^{-1}V = I` (or :math:`V^T` if symmetric) | Projection into the modal coordinate system
            /// </table>
            /// </BookContent>
        }
    }
}
