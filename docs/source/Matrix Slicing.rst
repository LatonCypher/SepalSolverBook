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
      0.9033    0.5047    0.9414    0.7432
   
   R1[2] = 0.9413709344541754
   C1 = 
      0.2757
      0.8157
      0.9150
      0.0083
      0.5395
      0.7292
      0.3806
      0.7669
   
   C1[5] = 0.7292416024173876

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
      0.3038    0.5638    0.3967    0.6236    0.3225
      0.3528    0.4786    0.7796    0.3726    0.8713
   

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
   
      0.6883    0.2373    0.9008    0.2306    0.8782    0.7115    0.3263    0.6706
      0.1296    0.5675    0.6528    0.5484    0.0581    0.8680    0.8414    0.0423
      0.9843    0.8162    0.2453    0.5220    0.8393    0.3197    0.5077    0.9468
      0.3009    0.9155    0.4257    0.5665    0.1886    0.0289    0.8109    0.6760
      0.7332    0.1071    0.0007    0.8833    0.5040    0.6467    0.0541    0.9488
      0.9657    0.2841    0.0462    0.0968    0.9283    0.4946    0.5651    0.9401
      0.0347    0.5645    0.9997    0.4093    0.0636    0.8871    0.8096    0.2488
      0.5400    0.2349    0.9503    0.1791    0.5058    0.1253    0.1363    0.2171
   
   B = 
   
      0.0757    0.4297    0.4701    0.9742    0.6958    0.5740    0.0287    0.0510
      0.2521    0.2766    0.4604    0.4557    0.8577    0.5189    0.9461    0.2998
      0.4591    0.8894    0.6943    0.0037    0.2703    0.9846    0.4272    0.3435
      0.4537    0.5687    0.9485    0.2447    0.8565    0.8925    0.2613    0.3981
      0.6138    0.0099    0.6304    0.7636    0.3151    0.5554    0.0848    0.8326
      0.9452    0.0950    0.5449    0.6938    0.9618    0.6218    0.6427    0.3786
      0.0646    0.4364    0.0921    0.0576    0.4518    0.5530    0.1958    0.3340
      0.0777    0.7212    0.8932    0.1624    0.5878    0.5887    0.4807    0.3284
   
   C = 
   
      1.9148    1.9960    2.8474    2.1303    2.6261    3.1163    1.6073    1.8372
      1.6151    1.5859    1.9205    1.2234    2.4813    2.5632    1.7106    1.2911
      1.5533    2.1069    3.0997    2.5051    3.2563    3.1991    1.8729    1.8873
      0.9539    1.9292    2.2090    1.1711    2.4458    2.5415    1.7229    1.3223
      1.4813    1.6219    2.7549    1.9699    2.7217    2.5359    1.2785    1.4155
      1.3567    1.5705    2.4552    2.3313    2.5872    2.5227    1.3007    1.6464
      1.7387    1.9106    2.1788    1.1459    2.5144    2.8435    1.9227    1.4181
      1.0722    1.4770    1.7853    1.1967    1.4566    2.0894    0.9450    1.0811
   
   D = 
   
      1.9148    1.9960    2.8474    2.1303    2.6261    3.1163    1.6073    1.8372
      1.6151    1.5859    1.9205    1.2234    2.4813    2.5632    1.7106    1.2911
      1.5533    2.1069    3.0997    2.5051    3.2563    3.1991    1.8729    1.8873
      0.9539    1.9292    2.2090    1.1711    2.4458    2.5415    1.7229    1.3223
      1.4813    1.6219    2.7549    1.9699    2.7217    2.5359    1.2785    1.4155
      1.3567    1.5705    2.4552    2.3313    2.5872    2.5227    1.3007    1.6464
      1.7387    1.9106    2.1788    1.1459    2.5144    2.8435    1.9227    1.4181
      1.0722    1.4770    1.7853    1.1967    1.4566    2.0894    0.9450    1.0811
   


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

   
      0.2239    0.3479    0.9739    0.8227    0.9258    0.4829
      0.5660    0.7001    0.9788    0.0146    0.4667    0.0509
      0.4028    0.6668    0.4392    0.9105    0.7347    0.7440
      0.9526    0.9931    0.3047    0.7344    0.8956    0.7825
      0.1460    0.6357    0.1455    0.0383    0.7972    0.8967
   
   
      0.5660
      0.9526
      0.7001
      0.6668
      0.9931
      0.6357
      0.9739
      0.9788
      0.8227
      0.9105
      0.7344
      0.9258
      0.7347
      0.8956
      0.7972
      0.7440
      0.7825
      0.8967
   

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

   
      5.2065    8.7131    1.7105    1.8644    0.3161    9.4517
      9.8140    8.6988    9.0101    9.6401    7.3168    9.7447
      2.5008    9.4230    5.6724    3.5902    7.8983    3.9226
      1.6725    6.4754    7.3759    0.9335    0.1169    9.8994
      6.0513    0.2561    7.8525    6.0521    6.5461    8.7760
   
   
      5.2065    8.7131    0.0000    0.0000    0.0000    9.4517
      9.8140    8.6988    9.0101    9.6401    7.3168    9.7447
      0.0000    9.4230    5.6724    0.0000    7.8983    0.0000
      0.0000    6.4754    7.3759    0.0000    0.0000    9.8994
      6.0513    0.0000    7.8525    6.0521    6.5461    8.7760
   
   
      5.2065    8.7131    0.0000    0.0000    0.0000       NaN
         NaN    8.6988       NaN       NaN    7.3168       NaN
      0.0000       NaN    5.6724    0.0000    7.8983    0.0000
      0.0000    6.4754    7.3759    0.0000    0.0000       NaN
      6.0513    0.0000    7.8525    6.0521    6.5461    8.7760
   

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

   
      0.1675    6.5000    9.3580    0.7792    2.4110    2.0478
      2.0725    8.1335    0.5585    6.5000    0.6669    8.0730
      8.9027    6.5000    6.5000    0.9780    3.9017    9.8427
      0.8910    4.1270    3.6842    1.8679    6.5000    2.4661
      8.6994    1.4492    4.6250    8.0604    6.5000    4.8676
   
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
   
