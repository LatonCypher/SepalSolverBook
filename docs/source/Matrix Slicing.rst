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
      0.6757    0.8132    0.9310    0.4951
   
   R1[2] = 0.9309597430323685
   C1 = 
      0.2365
      0.8317
      0.5932
      0.4782
      0.5623
      0.7427
      0.1228
      0.3716
   
   C1[5] = 0.7427309870615585

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
      0.7212    0.2454    0.1680    0.2294    0.9221
      0.1082    0.6285    0.7788    0.0549    0.1579
   

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
   
      0.1164    0.5694    0.8197    0.2142    0.5796    0.8886    0.7944    0.5349
      0.2396    0.9509    0.1634    0.1567    0.1932    0.7536    0.9586    0.3968
      0.8160    0.4116    0.7979    0.1232    0.1071    0.8640    0.6115    0.9374
      0.7373    0.9850    0.3685    0.7569    0.6619    0.6422    0.2596    0.0163
      0.4934    0.5772    0.0678    0.9599    0.1903    0.9707    0.7263    0.6106
      0.3487    0.0335    0.6283    0.3303    0.9528    0.0892    0.7606    0.6825
      0.0916    0.6790    0.9890    0.9938    0.9099    0.6365    0.6991    0.4127
      0.9872    0.2767    0.3163    0.8408    0.5951    0.9932    0.7441    0.5738
   
   B = 
   
      0.1650    0.8453    0.0365    0.8385    0.6799    0.3366    0.8670    0.7930
      0.3337    0.0102    0.5838    0.7735    0.9213    0.3777    0.7875    0.1710
      0.4231    0.7337    0.7897    0.9258    0.0293    0.4764    0.3583    0.7225
      0.8814    0.0552    0.2338    0.0499    0.8739    0.2401    0.3037    0.3383
      0.3461    0.7059    0.5412    0.9232    0.8165    0.1482    0.0181    0.9389
      0.6269    0.0399    0.6330    0.5275    0.8375    0.3261    0.7512    0.2854
      0.2791    0.0807    0.8871    0.3510    0.6165    0.7758    0.9554    0.4486
      0.1034    0.3808    0.4966    0.4976    0.2861    0.5780    0.9564    0.7232
   
   C = 
   
      1.7795    1.4299    2.8805    2.8564    2.6751    1.9973    2.8566    2.3954
      1.4119    0.7358    2.3587    2.2054    2.6741    1.8028    2.9278    1.6373
      1.5644    1.8026    2.5418    2.9832    2.5212    2.1537    3.4867    2.6351
      1.9792    1.4655    2.0731    2.8081    3.3239    1.4958    2.5348    2.2085
      2.0889    0.9899    2.2979    2.2174    3.2987    1.9080    3.2086    2.0868
      1.2942    1.7717    2.1914    2.4497    2.0918    1.6635    2.1178    2.6028
      2.4879    1.7463    3.1336    3.1939    3.4105    2.1204    2.8277    2.8878
      2.2257    1.8541    2.5398    2.9967    3.6107    2.1103    3.4591    2.9342
   
   D = 
   
      1.7795    1.4299    2.8805    2.8564    2.6751    1.9973    2.8566    2.3954
      1.4119    0.7358    2.3587    2.2054    2.6741    1.8028    2.9278    1.6373
      1.5644    1.8026    2.5418    2.9832    2.5212    2.1537    3.4867    2.6351
      1.9792    1.4655    2.0731    2.8081    3.3239    1.4958    2.5348    2.2085
      2.0889    0.9899    2.2979    2.2174    3.2987    1.9080    3.2086    2.0868
      1.2942    1.7717    2.1914    2.4497    2.0918    1.6635    2.1178    2.6028
      2.4879    1.7463    3.1336    3.1939    3.4105    2.1204    2.8277    2.8878
      2.2257    1.8541    2.5398    2.9967    3.6107    2.1103    3.4591    2.9342
   


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

   
      0.9139    0.3269    0.3476    0.5449    0.1777    0.7583
      0.8175    0.0845    0.5702    0.7365    0.6743    0.1932
      0.3112    0.2444    0.8278    0.0819    0.8245    0.0279
      0.5860    0.6273    0.6143    0.7447    0.4558    0.8082
      0.9667    0.7066    0.2480    0.7922    0.0355    0.2108
   
   
      0.9139
      0.8175
      0.5860
      0.9667
      0.6273
      0.7066
      0.5702
      0.8278
      0.6143
      0.5449
      0.7365
      0.7447
      0.7922
      0.6743
      0.8245
      0.7583
      0.8082
   

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

   
      3.0631    7.7873    9.3335    4.8258    9.1930    1.9240
      5.2571    4.8185    6.4594    3.2676    0.4229    4.1740
      2.0222    0.5531    7.4441    1.3351    1.3125    9.4695
      5.3916    2.4348    9.8108    9.0938    7.7594    5.9659
      3.2928    2.3313    1.1132    3.9297    0.9087    6.2126
   
   
      0.0000    7.7873    9.3335    0.0000    9.1930    0.0000
      5.2571    0.0000    6.4594    0.0000    0.0000    0.0000
      0.0000    0.0000    7.4441    0.0000    0.0000    9.4695
      5.3916    0.0000    9.8108    9.0938    7.7594    5.9659
      0.0000    0.0000    0.0000    0.0000    0.0000    6.2126
   
   
      0.0000    7.7873       NaN    0.0000       NaN    0.0000
      5.2571    0.0000    6.4594    0.0000    0.0000    0.0000
      0.0000    0.0000    7.4441    0.0000    0.0000       NaN
      5.3916    0.0000       NaN       NaN    7.7594    5.9659
      0.0000    0.0000    0.0000    0.0000    0.0000    6.2126
   

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

   
      6.5000    9.3013    0.2679    0.2053    1.0233    6.5000
      6.5000    6.5000    0.9268    6.5000    9.2550    6.5000
      6.5000    9.6546    6.5000    6.5000    9.6859    3.0483
      0.7374    2.8437    1.7831    6.5000    3.5154    2.6069
      3.7885    9.7605    8.6634    8.2862    1.6404    6.5000
   
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
   
