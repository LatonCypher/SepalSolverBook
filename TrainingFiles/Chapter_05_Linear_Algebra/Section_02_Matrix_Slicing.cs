using SepalSolver;
using static SepalSolver.Math;

namespace ConsoleApp1.TrainingFiles.Chapter_05_Linear_Algerba
{
    public class Section_02_Matrix_Slicing
    {
        public static void Run()
        {
            /// <BookContent>
            /// Matrix Slicing(Extracting Parts of Matrix)
            /// Matrix can be indexed to extract/set a single element, a row, a column, or a submatrix. 
            /// <header 2> Extracting/Setting part of a Vector </header>
            /// 
            /// 
            /// 
            /// <code>
            {
                // A Vector can be indexed with one index
                RowVec R1 = Rand(4);
                Console.WriteLine($"R1 = {R1}");
                Console.WriteLine($"R1[2] = {R1[2]}");


                ColVec C1 = Rand(8);
                Console.WriteLine($"C1 = {C1}");
                Console.WriteLine($"C1[5] = {C1[5]}");
            }
            /// </code>
            /// 
            /// <header 2> Extracting part of a Matrix </header>
            /// <code>
            {
                Matrix A = new double[,]
                {
                    { 8,    1,    6,    1,  16 },
                    { 3,    5,    6,    2,  15 },
                    { 4,    7,    2,    1,  14 }
                };

                //Print the matrix
                Console.WriteLine($"A = {A}");

                    // Extract single element using subscript
                    Console.WriteLine($"A[1,2] = {A[1, 2]}");

                    //  Extract single element using index
                    Console.WriteLine($"A[5] = {A[5]}");

                //  Extract multiple elements using index
                Console.WriteLine($"A[2..5] = {A[2..5]}");

                //  Extract multiple elements using subscript along a row
                Console.WriteLine($"A[1, 2..4] = {A[1, 2..4]}");

                //  Extract multiple elements using subscript along a col
                Console.WriteLine($"A[0..3, 3] = {A[0..3, 3]}");

                //  Extract submatrix elements
                Console.WriteLine($"A[0..3, 1..3] = {A[0..3, 1..3]}");

                // Extract single row
                Console.WriteLine($"A[1, ..] = {A[1, ..]}");

                // Extract multiple rows
                Console.WriteLine($"A[1..3, ..] = {A[1..3, ..]}");
            }
            /// 
            /// </code>
            /// 
            /// <header 2> Setting Portions of a Matrix </header>
            /// <code>
            {
                Matrix A = new double[,]
                {
                    { 8,    1,    6,    1,  16 },
                    { 3,    5,    6,    2,  15 },
                    { 4,    7,    2,    1,  14 }
                };
                // set single element using subscript
                Console.WriteLine($"A = {A}");

                A[1, 2] = 125;
                Console.WriteLine($"A = {A}");

                //  set single element using index
                A[5] = 110;
                Console.WriteLine($"A = {A}");

                //  set multiple elements using index
                A[2..5] = new double[,] { { 10, 15, 20 } };
                Console.WriteLine($"A = {A}");

                //  set multiple elements using subscript along a row
                A[1, 2..4] = new double[] { 150, 200 };
                Console.WriteLine($"A = {A}");

                //  set multiple elements using subscript along a col
                A[0..3, 3] = new double[] { 100, 150, 200 };
                Console.WriteLine($"A = {A}");

                //  set submatrix elements
                Indexer i = new(0, 3), j = new(1, 3);
                A[0..3, 1..3] = new double[,]
                {
                        { 100, 150 },
                        { 100, 150 },
                        { 100, 150 }
                };
                Console.WriteLine($"A = {A}");

                // set single row
                A[1, ..] = new double[] { 1, 2, 3, 4, 5 };
                Console.WriteLine($"A = {A}");

                // set multiple rows
                A[1..3, ..] = Rand(2, 5);
                Console.WriteLine($"A = {A}");
            }
            /// </code>
            /// 
            /// <header 2> Application of Matrix Slicing: Strassen Multiplication </header>
            /// Strassen’s Matrix Multiplication
            /// <header 2> Overview </header>
            /// 
            /// 
            /// - **Inventor**: Volker Strassen, 1969
            /// - **Purpose**: Improve efficiency of matrix multiplication beyond the classical cubic-time algorithm.
            /// - **Key Idea**: Replace some multiplications with additions/subtractions by reorganizing computation.
            /// 
            /// <header 2> Standard vs. Strassen Multiplication </header>
            /// 
            /// <table>
            /// +--------------------+----------------------+--------------------+
            /// | Feature            | Standard Algorithm   | Strassen Algorithm | 
            /// +--------------------+----------------------+--------------------+
            /// | Approach           | Direct row-by-column | Divide-and-conquer |
            /// |                    | multiplication       | with recursive     |   
            /// |                    |                      | submatrices        |
            /// +--------------------+----------------------+--------------------+
            /// | Multiplications    | 8                    | 7                  |      
            /// | for 2×2 matrices   |                      |                    | 
            /// +--------------------+----------------------+--------------------+
            /// | Additions/         | 4                    | 18                 |
            /// | Subtractions       |                      |                    |
            /// +--------------------+----------------------+--------------------+
            /// | Time Complexity    | O(n^3)               | O(n^(log2 7))      |   
            /// |                    |                      | ≈ O(n^2.81)        |
            /// +--------------------+----------------------+--------------------+
            /// | Best Use Case      | Small matrices       | Large matrices     |  
            /// +--------------------+----------------------+--------------------+
            /// </table>
            /// 
            /// Algorithm Steps
            /// ---------------
            /// 
            /// 1. **Divide**: Split each n×n matrix into four (n/2)×(n/2) submatrices
            /// <math>
            ///     A = \begin{bmatrix}
            ///             A_{11} & A_{12} \\
            ///             A_{21} & A_{22}
            ///         \end{bmatrix}
            ///         
            ///     B = \begin{bmatrix}
            ///             B_{11} & B_{12} \\
            ///             B_{21} & B_{22}
            ///         \end{bmatrix}
            /// </math>
            ///      
            /// 2. **Compute 7 products** (instead of 8)
            /// <math>
            ///     \begin{array}{rcl}
            ///         M_1 &=& \left(A_{11} + A_{22}\right)\left(B_{11} + B_{22}\right) \\
            ///         M_2 &=& \left(A_{21} + A_{22}\right)B_{11} \\
            ///         M_3 &=& A_{11}\left(B_{12} - B_{22}\left) \\
            ///         M_4 &=& A_{22}\left(B_{21} - B_{11}\left) \\
            ///         M_5 &=& \left(A_{11} + A_{12}\right)B_{22} \\
            ///         M_6 &=& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
            ///         M_7 &=& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
            ///     \end{array}
            /// </math>
            ///     
            /// 3. **Combine results** to form the product matrix::
            /// <math>
            ///     \begin{array}{rcl}
            ///         C_{11} &=& M_1 + M_4 - M_5 + M_7 \\
            ///         C_{12} &=& M_3 + M_5 \\
            ///         C_{21} &=& M_2 + M_4 \\
            ///         C_{22} &=& M_1 - M_2 + M_3 + M_6
            ///     \end{array}
            /// </math>
            /// 
            /// 4. ** Return the result
            /// <math>
            ///     C = \begin{bmatrix}
            ///             C_{11} & C_{12} \\
            ///             C_{21} & C_{22}
            ///         \end{bmatrix}
            /// </math>
            /// 
            ///     
            /// <header 2> Advantages </header>
            /// 
            /// - Fewer multiplications → faster for large matrices.
            /// - Foundation for advanced algorithms (e.g., Coppersmith–Winograd).
            /// - Works over any ring (addition and multiplication defined).
            /// 
            /// 
            /// <header 2> Limitations </header>
            /// 
            /// - Overhead of additions makes it slower for small matrices.
            /// - Numerical stability issues (rounding errors).
            /// - Not optimal compared to modern optimized libraries (BLAS, GPU-based methods).
            /// 
            /// 
            /// <header 2> Applications </header>
            /// 
            /// -Computer graphics (large matrix transformations).
            /// -Scientific computing (linear algebra problems).
            /// -Machine learning (deep learning frameworks).
            /// 
            /// <code>
            {
                static Matrix Strass(Matrix A, Matrix B)
                {
                    if (A.Cols != B.Rows)
                        throw new Exception("Matrices are not conformable for multiplication");
                    if (A.Cols <= 2)
                        return A * B;
                    else
                    {
                        // get matrix size
                        int N = A.Cols / 2;

                        // Step 1: Divide matrices into quadrants
                        Matrix A11 = A[..N, ..N], A12 = A[..N, N..],
                               A21 = A[N.., ..N], A22 = A[N.., N..],

                               B11 = B[..N, ..N], B12 = B[..N, N..],
                               B21 = B[N.., ..N], B22 = B[N.., N..],

                        // Step 2: Calculate the 7 Strassen products (M1 through M7)
                        M1 = Strass(A11 + A22, B11 + B22),
                        M2 = Strass(A21 + A22, B11),
                        M3 = Strass(A11, B12 - B22),
                        M4 = Strass(A22, B21 - B11),
                        M5 = Strass(A11 + A12, B22),
                        M6 = Strass(A21 - A11, B11 + B12),
                        M7 = Strass(A12 - A22, B21 + B22),

                        // Step 3: Combine products into the quadrants of C
                        C11 = M1 + M4 - M5 + M7,
                        C12 = M3 + M5,
                        C21 = M2 + M4,
                        C22 = M1 - M2 + M3 + M6,

                        // Step 4: Assemble the final matrix
                        C = new Matrix[,] 
                        {
                            { C11, C12 }, 
                            { C21, C22 } 
                        };
                        return C;
                    }
                }

                Matrix A = Rand(8, 8), B = Rand(8, 8), C = Strass(A, B), D = A * B;
                Console.WriteLine($"A = \n{A}");
                Console.WriteLine($"B = \n{B}");
                Console.WriteLine($"C = \n{C}");
                Console.WriteLine($"D = \n{D}");
            }
            /// </code>
            /// 
            /// 
            /// <header 2> Logical Indexing </header>
            /// Logical indexing is a powerful feature in **Sepal Solver** that allows you to access or modify matrix elements based on specific conditions rather than explicit coordinates. If you are familiar with MATLAB or NumPy, this syntax will feel natural.
            /// 
            /// Instead of using integer coordinates (e.g., ``A[0, 5]``), you pass a **boolean condition** into the indexer. Sepal Solver evaluates this condition across the entire matrix to create a mask, then applies the operation only to the elements where the condition is ``true``.
            /// 
            /// To extract elements that meet a specific criterion, use relational operators directly within the brackets. This returns a vector containing all matching values.
            /// 
            /// <code>
            {
                Matrix A = Rand(5, 6);
                Console.WriteLine(A);

                // Extract all values greater than 0.5
                var L = A[A > 0.5];
                Console.WriteLine(L);
            }
            /// </code>
            /// 
            /// Logical indexing is most effective when performing bulk updates. You can set values for specific elements without affecting the rest of the matrix.
            /// 
            /// <code>
            {
                Matrix A = Rand(5, 6);
                A *= 10;
                Console.WriteLine(A);

                // Extract all values greater than 0.5
                var L = A[A > 0.5];
                Console.WriteLine(L);

                // Set all elements less than 5 to zero
                A[A < 5] = 0;

                // Replace specific "masquerading" integers or outliers
                A[A == -999] = double.NaN;
            }
            /// </code>
            /// 
            /// <header 3> Complex Conditions </header>
            /// You can combine multiple conditions using logical operators. This allows for precise data "clipping" or windowing.
            /// * Use ``&`` for **AND**
            /// * Use ``|`` for **OR**
            /// <code>
            {
                Matrix A = Rand(5, 6);
                A *= 10;
                // Set values within the range (5, 8) to a new value
                A[(A > 5).And(A < 8)] = 6.5;
            }
            /// </code>
            /// <header 3> Advantages </header>
            /// 
            /// <table>
            /// - Feature | - Benefit 
            /// - **Declarative Syntax** | - Express *what* to filter rather than *how* to loop, making code easier to read.
            /// - **Vectorization** | - Operations are optimized internally, providing better performance than manual C# nested loops.
            /// - **In-place Updates** | - Modify subsets of large matrices efficiently without creating intermediate copies.
            /// </table>
            /// 
            /// Example: Finding Integers in a Double Matrix
            /// As discussed in the type-checking guidelines, you can use logical indexing to identify and manipulate whole numbers stored as doubles:
            /// <code>
            {
                Matrix A = new double[,]
                {
                    {1.1, 2.0, 3.9, 4.2 },
                    {1.5, 3.5, 4.0, 5.1 }
                };
                Console.WriteLine(A);
                // Find all "integers" and scale them by 10
                A[A % 1 == 0] *= 10;
                Console.WriteLine(A);

            }
            /// </code>
            /// </BookContent>
            
        }

    }
}

