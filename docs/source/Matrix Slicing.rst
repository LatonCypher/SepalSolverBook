Matrix Slicing
==============

Matrix Slicing(Extracting Parts of Matrix)
Matrix can be indexed to extract/set a single element, a row, a column, or a submatrix. 


Extracting/Setting part of a Vector
-----------------------------------


.. code-block:: csharp

   // A Vector can be indexed with one index
   RowVec R1 = Rand(4);
   Console.WriteLine($"R1 = {R1}");
   Console.WriteLine($"R1[2] = {R1[2]}");


   ColVec C1 = Rand(8);
   Console.WriteLine($"C1 = {C1}");
   Console.WriteLine($"C1[5] = {C1[5]}");


Ouput

.. terminal::

   R1 = 
      0.1556    0.8710    0.5122    0.8794
   
   R1[2] = 0.5121864652577712
   C1 = 
      0.1406
      0.1068
      0.2576
      0.6010
      0.4322
      0.7427
      0.9229
      0.4616
   
   C1[5] = 0.7427062179103255

Extracting part of a Matrix
---------------------------

.. code-block:: csharp

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

// 

Ouput

.. terminal::

   A = 
    8   1   6   1  16 
    3   5   6   2  15 
    4   7   2   1  14 
   
   A[1,2] = 6
   A[5] = 7
   A[2..5] = 
    4 
    1 
    5 
   
   A[1, 2..4] = 
    6   2 
   
   A[0..3, 3] = 
    1 
    2 
    1 
   
   A[0..3, 1..3] = 
    1   6 
    5   6 
    7   2 
   
   A[1, ..] = 
    3   5   6   2  15 
   
   A[1..3, ..] = 
    3   5   6   2  15 
    4   7   2   1  14 
   

Setting Portions of a Matrix
----------------------------

.. code-block:: csharp

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


Ouput

.. terminal::

   A = 
    8   1   6   1  16 
    3   5   6   2  15 
    4   7   2   1  14 
   
   A = 
    8   1   6   1  16 
    3   5  125  2  15 
    4   7   2   1  14 
   
   A = 
    8   1   6   1  16 
    3   5  125  2  15 
    4  110  2   1  14 
   
   A = 
    8  15   6   1  16 
    3  20  125  2  15 
   10  110  2   1  14 
   
   A = 
    8  15   6   1  16 
    3  20  150 200 15 
   10  110  2   1  14 
   
   A = 
    8  15   6  100 16 
    3  20  150 150 15 
   10  110  2  200 14 
   
   A = 
    8  100 150 100 16 
    3  100 150 150 15 
   10  100 150 200 14 
   
   A = 
    8  100 150 100 16 
    1   2   3   4   5 
   10  100 150 200 14 
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      0.0643    0.2633    0.7930    0.8508    0.6500
      0.2105    0.2179    0.6171    0.4897    0.1234
   

Application of Matrix Slicing: Strassen Multiplication
------------------------------------------------------
Strassen’s Matrix Multiplication
Overview
--------


- **Inventor**: Volker Strassen, 1969
- **Purpose**: Improve efficiency of matrix multiplication beyond the classical cubic-time algorithm.
- **Key Idea**: Replace some multiplications with additions/subtractions by reorganizing computation.

Standard vs. Strassen Multiplication
------------------------------------


.. list-table:: 
   :header-rows: 1

   * - Feature
     - Standard Algorithm
     - Strassen Algorithm
   * - Approach
     - Direct row-by-column multiplication
     - Divide-and-conquer with recursive submatrices
   * - Multiplications for 2×2 matrices
     - 8
     - 7
   * - Additions/Subtractions
     - 4
     - 18
   * - Time Complexity
     - O(n^3)
     - O(n^(log2 7)) ≈ O(n^2.81)
   * - Best Use Case
     - Small matrices
     - Large matrices

Algorithm Steps
---------------

1. **Divide**: Split each n×n matrix into four (n/2)×(n/2) submatrices

.. math::

   A = \begin{bmatrix}
   A_{11} & A_{12} \\
   A_{21} & A_{22}
   \end{bmatrix}
   
   B = \begin{bmatrix}
   B_{11} & B_{12} \\
   B_{21} & B_{22}
   \end{bmatrix}


2. **Compute 7 products** (instead of 8)

.. math::

   \begin{array}{rcl}
   M_1 &=& \left(A_{11} + A_{22}\right)\left(B_{11} + B_{22}\right) \\
   M_2 &=& \left(A_{21} + A_{22}\right)B_{11} \\
   M_3 &=& A_{11}\left(B_{12} - B_{22}\right) \\
   M_4 &=& A_{22}\left(B_{21} - B_{11}\right) \\
   M_5 &=& \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 &=& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 &=& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix

.. math::

   \begin{array}{rcl}
   C_{11} &=& M_1 + M_4 - M_5 + M_7 \\
   C_{12} &=& M_3 + M_5 \\
   C_{21} &=& M_2 + M_4 \\
   C_{22} &=& M_1 - M_2 + M_3 + M_6
   \end{array}


4. ** Return the result

.. math::

   C = \begin{bmatrix}
   C_{11} & C_{12} \\
   C_{21} & C_{22}
   \end{bmatrix}



Advantages
----------

- Fewer multiplications → faster for large matrices.
- Foundation for advanced algorithms (e.g., Coppersmith–Winograd).
- Works over any ring (addition and multiplication defined).


Limitations
-----------

- Overhead of additions makes it slower for small matrices.
- Numerical stability issues (rounding errors).
- Not optimal compared to modern optimized libraries (BLAS, GPU-based methods).


Applications
------------

-Computer graphics (large matrix transformations).
-Scientific computing (linear algebra problems).
-Machine learning (deep learning frameworks).


.. code-block:: csharp

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


Ouput

.. terminal::

   A = 
   
      0.5212    0.8093    0.4616    0.6185    0.4776    0.7276    0.0815    0.2316
      0.9190    0.7436    0.3093    0.3008    0.7704    0.7064    0.7954    0.3597
      0.6355    0.6025    0.2037    0.1881    0.8559    0.6522    0.6331    0.6227
      0.9207    0.5545    0.1402    0.1747    0.1106    0.1015    0.3777    0.5446
      0.0251    0.0816    0.3319    0.3769    0.3724    0.5577    0.3822    0.4234
      0.3257    0.8347    0.8671    0.6230    0.6866    0.7701    0.4040    0.5237
      0.7419    0.0818    0.0764    0.1520    0.3843    0.6127    0.1237    0.9753
      0.8283    0.0605    0.1735    0.6006    0.1684    0.1434    0.2227    0.7478
   
   B = 
   
      0.3299    0.7333    0.7804    0.1978    0.0674    0.8445    0.9293    0.2719
      0.7253    0.5414    0.6308    0.8357    0.5737    0.2163    0.2478    0.7548
      0.1898    0.1624    0.3437    0.7894    0.2781    0.3468    0.9950    0.2835
      0.3306    0.0211    0.3635    0.6461    0.2971    0.2458    0.4335    0.5820
      0.6400    0.1119    0.6413    0.9324    0.3700    0.7283    0.0247    0.9596
      0.4695    0.0522    0.8752    0.9670    0.0708    0.9547    0.9376    0.6451
      0.9665    0.1469    0.3694    0.9436    0.0188    0.6756    0.5390    0.4872
      0.0790    0.3770    0.5024    0.1365    0.0641    0.5254    0.8430    0.8864
   
   C = 
   
      1.7952    1.0990    2.3903    2.8007    1.0562    2.1464    2.3454    2.4160
      2.6225    1.5085    2.9888    3.4428    1.0370    3.0800    2.8898    2.9752
      2.2624    1.2868    2.6808    3.0226    0.9157    2.7847    2.5229    2.8972
      1.3167    1.2802    1.7528    1.5010    0.5612    1.7077    1.9688    1.6486
      1.1580    0.4109    1.4029    1.8835    0.4644    1.5308    1.6320    1.6604
      2.3161    1.2185    2.8319    3.6864    1.2768    2.6927    3.0405    3.1436
      1.0989    1.0647    2.0304    1.5742    0.4137    2.1688    2.3243    2.0622
      0.9980    1.0219    1.6539    1.3472    0.4418    1.7232    2.1067    1.6950
   
   D = 
   
      1.7952    1.0990    2.3903    2.8007    1.0562    2.1464    2.3454    2.4160
      2.6225    1.5085    2.9888    3.4428    1.0370    3.0800    2.8898    2.9752
      2.2624    1.2868    2.6808    3.0226    0.9157    2.7847    2.5229    2.8972
      1.3167    1.2802    1.7528    1.5010    0.5612    1.7077    1.9688    1.6486
      1.1580    0.4109    1.4029    1.8835    0.4644    1.5308    1.6320    1.6604
      2.3161    1.2185    2.8319    3.6864    1.2768    2.6927    3.0405    3.1436
      1.0989    1.0647    2.0304    1.5742    0.4137    2.1688    2.3243    2.0622
      0.9980    1.0219    1.6539    1.3472    0.4418    1.7232    2.1067    1.6950
   


Logical Indexing
----------------
Logical indexing is a powerful feature in **Sepal Solver** that allows you to access or modify matrix elements based on specific conditions rather than explicit coordinates. If you are familiar with MATLAB or NumPy, this syntax will feel natural.

Instead of using integer coordinates (e.g., ``A[0, 5]``), you pass a **boolean condition** into the indexer. Sepal Solver evaluates this condition across the entire matrix to create a mask, then applies the operation only to the elements where the condition is ``true``.

To extract elements that meet a specific criterion, use relational operators directly within the brackets. This returns a vector containing all matching values.


.. code-block:: csharp

   Matrix A = Rand(5, 6);
   Console.WriteLine(A);

   // Extract all values greater than 0.5
   var L = A[A > 0.5];
   Console.WriteLine(L);


Ouput

.. terminal::

   
      0.7842    0.1070    0.0046    0.2322    0.4559    0.5544
      0.5704    0.5865    0.3393    0.4526    0.7479    0.8575
      0.7085    0.2850    0.5132    0.2206    0.5196    0.7758
      0.8182    0.0234    0.9146    0.9558    0.9374    0.9222
      0.5563    0.9603    0.4677    0.5866    0.3704    0.4400
   
   
      0.7842
      0.5704
      0.7085
      0.8182
      0.5563
      0.5865
      0.9603
      0.5132
      0.9146
      0.9558
      0.5866
      0.7479
      0.5196
      0.9374
      0.5544
      0.8575
      0.7758
      0.9222
   

Logical indexing is most effective when performing bulk updates. You can set values for specific elements without affecting the rest of the matrix.


.. code-block:: csharp

   Matrix A = Rand(5, 6);
   A *= 10;
   Console.WriteLine(A);

   // Set all elements less than 5 to zero
   A[A < 5] = 0;
   Console.WriteLine(A);

   // Replace specific "masquerading" integers or outliers
   A[A > 9] = double.NaN;
   Console.WriteLine(A);


Ouput

.. terminal::

   
      6.7980    9.7504    1.4232    1.8884    0.8436    7.4466
      5.5718    6.1481    2.8367    9.9782    3.3884    1.9417
      1.2566    1.2324    9.9549    0.9555    7.8882    8.6789
      5.0557    4.4693    9.1605    1.2031    0.4945    6.8482
      4.6755    9.4341    2.0442    1.4770    0.1532    5.0213
   
   
      6.7980    9.7504    0.0000    0.0000    0.0000    7.4466
      5.5718    6.1481    0.0000    9.9782    0.0000    0.0000
      0.0000    0.0000    9.9549    0.0000    7.8882    8.6789
      5.0557    0.0000    9.1605    0.0000    0.0000    6.8482
      0.0000    9.4341    0.0000    0.0000    0.0000    5.0213
   
   
      6.7980       NaN    0.0000    0.0000    0.0000    7.4466
      5.5718    6.1481    0.0000       NaN    0.0000    0.0000
      0.0000    0.0000       NaN    0.0000    7.8882    8.6789
      5.0557    0.0000       NaN    0.0000    0.0000    6.8482
      0.0000       NaN    0.0000    0.0000    0.0000    5.0213
   

Complex Conditions
~~~~~~~~~~~~~~~~~~
You can combine multiple conditions using logical operators. This allows for precise data "clipping" or windowing.
* Use ``&`` for **AND**
* Use ``|`` for **OR**

.. code-block:: csharp

   Matrix A = Rand(5, 6);
   A *= 10;
   // Set values within the range (5, 8) to a new value
   A[(A > 5) & (A < 8)] = 6.5;
   Console.WriteLine(A);


Ouput

.. terminal::

   
      8.2199    2.7928    8.7471    6.5000    6.5000    6.5000
      9.0289    4.4235    6.5000    3.7435    4.7632    6.5000
      8.9440    3.7378    8.6985    6.5000    6.5000    6.5000
      6.5000    1.8329    6.5000    0.6833    1.6238    9.0504
      3.9217    9.4270    8.8926    3.9710    2.1160    1.8298
   
Advantages
~~~~~~~~~~


.. list-table:: 
   :header-rows: 1

   * - - Feature
     - - Benefit
   * - - **Declarative Syntax**
     - - Express *what* to filter rather than *how* to loop, making code easier to read.
   * - - **Vectorization**
     - - Operations are optimized internally, providing better performance than manual C# nested loops.
   * - - **In-place Updates**
     - - Modify subsets of large matrices efficiently without creating intermediate copies.

Example: Finding Integers in a Double Matrix
As discussed in the type-checking guidelines, you can use logical indexing to identify and manipulate whole numbers stored as doubles:

.. code-block:: csharp

   Matrix A = new double[,]
   {
       {1.1, 2.0, 3.9, 4.2 },
       {1.5, 3.5, 4.0, 5.1 }
   };
   Console.WriteLine(A);
   // Find all "integers" and scale them by 10
   A[A % 1 == 0] *= 10;
   Console.WriteLine(A);



Ouput

.. terminal::

   
      1.1000    2.0000    3.9000    4.2000
      1.5000    3.5000    4.0000    5.1000
   
   
      1.1000   20.0000    3.9000    4.2000
      1.5000    3.5000   40.0000    5.1000
   
