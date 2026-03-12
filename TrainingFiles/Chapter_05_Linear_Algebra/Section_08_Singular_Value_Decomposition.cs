namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algebra
{
    internal class Section_08_Singular_Value_Decomposition
    {
        public static void Run()
        {
            /// <BookContent>
            /// Singular Value Decomposition(SVD) is arguably the most powerful tool in linear algebra. It provides a way to factorize any matrix—regardless of whether it is square, symmetric, or even full rank—into three distinct, meaningful components.
            /// 
            /// **Definition**
            /// The SVD of an $m \times n$ matrix $A$ is a factorization of the form:
            /// <math>
            ///     A = U \Sigma V^T
            /// </math>
            /// 
            /// Where:
            /// * **:math:`U`**: An math:`m \times m` **orthogonal**matrix.Its columns are the left-singular vectors(representing the output space).
            /// * **:math:`\Sigma`**: An :math:`m \times n` **diagonal**matrix containing the singular values($\sigma_i$), which are non-negative and usually sorted in descending order. These represent the "strength" or importance of each dimension.
            /// * **:math:`V^T`**: The transpose of an math:`n \times n` **orthogonal**matrix.Its columns are the right-singular vectors(representing the input space).
            /// 
            /// 
            /// **Geometric Interpretation**
            /// SVD tells us that any linear transformation can be decomposed into three simple geometric steps:
            /// 1. **Rotation** in the input space(by $V^T$).
            /// 2. **Stretching**or scaling along the axes(by $\Sigma$).
            /// 3. **Rotation** in the output space(by $U$).
            /// 
            /// **Applications in Technology**
            /// **1. Data Compression (Low-Rank Approximation)**
            /// Because the singular values in $\Sigma$ are ordered by importance, we can "truncate" the decomposition by keeping only the top $k$ singular values and discarding the rest. This creates a low-rank approximation of the original matrix $A_k$, which uses significantly less storage while retaining the most important features.
            /// 
            /// **2. Principal Component Analysis (PCA)**
            /// SVD is the underlying engine for PCA.It identifies the directions(principal components) along which the data varies the most, allowing for high-dimensional data visualization and noise reduction.
            /// 
            /// **3. Recommender Systems**
            /// Streaming services like Netflix use SVD to predict user ratings.By decomposing a "User-Movie" matrix, SVD identifies latent factors(e.g., genre preferences, actor affinity) that explain the relationship between users and content.
            /// 
            /// 
            /// **Numerical Example**
            /// If we have a matrix $A$ representing a grayscale image, we can use **SepalSolver**to compute the singular values and reconstruct the image using only the most significant components.
            /// <code>
            {
                // Assume A is our data matrix
                Matrix A = new double[,]
                { 
                    { 3, 2, 2 }, 
                    { 2, 3, -2 } 
                };
                
                // Perform SVD 
                var (U, S, V) = Svd(A);

                Console.WriteLine($"Matrix U = {U}");
                Console.WriteLine($"Matrix S = {S}");
                Console.WriteLine($"Matrix V = {V}");
            }
            /// </code>
            /// 
            /// **Summary of Matrix Properties**
            /// <table>
            /// Component | Property | Geometric Meaning 
            /// **:math:`U`** | :math:`U^TU = I` | Rotation/Reflection in :math:`\mathbb{R}^m` 
            /// **:math:`\Sigma`** | Diagonal | Scaling(Expansion/Contraction) 
            /// **:math:`$V^T`** | :math:`V^T V = I` | Rotation/Reflection in :math:`\mathbb{R}^n` 
            /// <\table>
            /// </BookContent>
        }
    }
}
