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
      0.7312    0.9794    0.0520    0.3832
   
   R1[2] = 0.051966907949733465
   C1 = 
      0.7355
      0.6941
      0.2239
      0.5521
      0.4214
      0.0068
      0.4178
      0.7584
   
   C1[5] = 0.006772893626226684

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
   A[2..5] = new double[] { 10, 15, 20 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a row
   A[1, 2..4] = new double[] { 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a col
   A[0..3, 3] = new double[] { 100, 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set submatrix elements
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
      0.4429    0.6177    0.9802    0.8012    0.4473
      0.5370    0.9630    0.4352    0.0379    0.3581
   

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
   
      0.9546    0.1282    0.9268    0.0895    0.9067    0.2052    0.4052    0.2399
      0.3809    0.5083    0.8417    0.2412    0.1995    0.6603    0.8785    0.5154
      0.1843    0.4746    0.8635    0.8918    0.8459    0.9684    0.2928    0.4827
      0.0053    0.6396    0.2810    0.7315    0.5009    0.6802    0.0083    0.9396
      0.8613    0.3680    0.5029    0.5819    0.5488    0.5528    0.8172    0.1658
      0.1940    0.2790    0.2605    0.8762    0.6772    0.7983    0.9901    0.7770
      0.8818    0.1061    0.0588    0.1540    0.2173    0.8749    0.0681    0.7131
      0.6176    0.0046    0.4232    0.7982    0.2814    0.8088    0.9581    0.2109
   
   B = 
   
      0.0731    0.8085    0.3717    0.9292    0.3182    0.0425    0.5297    0.5171
      0.3485    0.8136    0.9693    0.2978    0.0388    0.3827    0.6482    0.9650
      0.3619    0.2483    0.5096    0.8527    0.2896    0.8901    0.6917    0.6824
      0.4907    0.6995    0.2365    0.0271    0.6752    0.9983    0.2442    0.5672
      0.2660    0.1757    0.8088    0.2858    0.5852    0.0371    0.3850    0.2178
      0.0671    0.8453    0.7944    0.5765    0.1695    0.7733    0.2940    0.3181
      0.8189    0.0533    0.3218    0.2324    0.0325    0.3794    0.9966    0.8554
      0.6019    0.5953    0.9191    0.3521    0.5106    0.4644    0.6898    0.6118
   
   C = 
   
      1.2250    1.6660    2.2199    2.2741    1.3388    1.4614    2.2305    2.0567
      1.7549    2.0459    2.5625    2.0528    1.0679    2.2913    2.6743    2.7189
      1.7493    2.6435    3.1709    2.1112    1.8444    2.9639    2.4556    2.6865
      1.4352    2.3289    2.7500    1.3230    1.4903    2.2095    1.8397    2.1341
      1.6108    2.2336    2.3691    2.0784    1.3530    2.0409    2.4874    2.5696
      2.1478    2.3705    2.8972    1.6667    1.7002    2.6005    2.6960    2.7681
      0.7998    2.1273    2.0450    1.7387    1.0475    1.3258    1.5149    1.5060
      1.6322    2.0761    2.0107    1.8014    1.2988    2.2989    2.2643    2.3325
   
   D = 
   
      1.2250    1.6660    2.2199    2.2741    1.3388    1.4614    2.2305    2.0567
      1.7549    2.0459    2.5625    2.0528    1.0679    2.2913    2.6743    2.7189
      1.7493    2.6435    3.1709    2.1112    1.8444    2.9639    2.4556    2.6865
      1.4352    2.3289    2.7500    1.3230    1.4903    2.2095    1.8397    2.1341
      1.6108    2.2336    2.3691    2.0784    1.3530    2.0409    2.4874    2.5696
      2.1478    2.3705    2.8972    1.6667    1.7002    2.6005    2.6960    2.7681
      0.7998    2.1273    2.0450    1.7387    1.0475    1.3258    1.5149    1.5060
      1.6322    2.0761    2.0107    1.8014    1.2988    2.2989    2.2643    2.3325
   


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

   
      0.8204    0.4951    0.7753    0.9622    0.6833    0.8404
      0.8154    0.4690    0.7973    0.4287    0.7306    0.6841
      0.8928    0.7301    0.5756    0.6672    0.1603    0.6673
      0.4916    0.2952    0.3229    0.9162    0.8641    0.1108
      0.1288    0.7359    0.7044    0.2164    0.8764    0.5753
   
   
      0.8204
      0.8154
      0.8928
      0.7301
      0.7359
      0.7753
      0.7973
      0.5756
      0.7044
      0.9622
      0.6672
      0.9162
      0.6833
      0.7306
      0.8641
      0.8764
      0.8404
      0.6841
      0.6673
      0.5753
   

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

   
      7.9804    6.6565    0.9678    9.7638    7.5953    9.7780
      9.3999    0.0743    4.5012    7.0534    6.1269    4.6833
      6.6157    3.1777    1.1312    1.8575    1.8956    9.3410
      9.4836    5.4236    7.3624    5.0497    0.8959    4.9772
      1.9361    3.5652    0.3583    3.3195    6.2105    6.2152
   
   
      7.9804    6.6565    0.0000    9.7638    7.5953    9.7780
      9.3999    0.0000    0.0000    7.0534    6.1269    0.0000
      6.6157    0.0000    0.0000    0.0000    0.0000    9.3410
      9.4836    5.4236    7.3624    5.0497    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    6.2105    6.2152
   
   
      7.9804    6.6565    0.0000       NaN    7.5953       NaN
         NaN    0.0000    0.0000    7.0534    6.1269    0.0000
      6.6157    0.0000    0.0000    0.0000    0.0000       NaN
         NaN    5.4236    7.3624    5.0497    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    6.2105    6.2152
   

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

   
      1.6952    4.2039    6.5000    6.5000    6.5000    0.1409
      4.8143    0.9802    9.3844    8.7060    6.5000    6.5000
      8.8735    1.5669    6.5000    1.0413    4.6857    6.5000
      1.4367    6.5000    6.5000    6.5000    8.8414    6.5000
      4.1273    9.5102    6.5000    6.5000    3.1074    0.2125
   
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
   
