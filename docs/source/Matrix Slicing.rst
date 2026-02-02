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
      0.2012    0.8870    0.7273    0.9113
   
   R1[2] = 0.7272743469436114
   C1 = 
      0.7433
      0.8805
      0.4415
      0.5116
      0.5063
      0.5527
      0.1252
      0.5027
   
   C1[5] = 0.5527146711908766

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
   8	1	6	1	16
   3	5	6	2	15
   4	7	2	1	14
   
   A[1,2] = 6
   A[5] = 7
   A[2..5] = 
   4
   1
   5
   
   A[1, 2..4] = 
   6	2
   
   A[0..3, 3] = 
   1
   2
   1
   
   A[0..3, 1..3] = 
   1	6
   5	6
   7	2
   
   A[1, ..] = 
   3	5	6	2	15
   
   A[1..3, ..] = 
   3	5	6	2	15
   4	7	2	1	14
   

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
   8	1	6	1	16
   3	5	6	2	15
   4	7	2	1	14
   
   A = 
   8	1	6	1	16
   3	5	125	2	15
   4	7	2	1	14
   
   A = 
   8	1	6	1	16
   3	5	125	2	15
   4	110	2	1	14
   
   A = 
   8	15	6	1	16
   3	20	125	2	15
   10	110	2	1	14
   
   A = 
   8	15	6	1	16
   3	20	150	200	15
   10	110	2	1	14
   
   A = 
   8	15	6	100	16
   3	20	150	150	15
   10	110	2	200	14
   
   A = 
   8	100	150	100	16
   3	100	150	150	15
   10	100	150	200	14
   
   A = 
   8	100	150	100	16
   1	2	3	4	5
   10	100	150	200	14
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      0.5777    0.3876    0.0717    0.1186    0.3306
      0.9316    0.0477    0.2214    0.5456    0.5796
   

Application of Matrix Slicing: Strassen Multiplication
------------------------------------------------------
Strassen’s Matrix Multiplication
Overview
--------

- **Inventor**: Volker Strassen, 1969
- **Purpose**: Improve efficiency of matrix multiplication beyond the classical cubic-time algorithm.
- **Key Idea**: Replace some multiplications with additions/subtractions by reorganizing computation.

Standard vs. Strassen Multiplication
-----------------------------------


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
     - O(n^(log2 7)) ≈  O(n^2.81)
   * - Best Use Case
     - Small matrices
     - Large matrices

Algorithm Steps
---------------

1. **Divide**: Split each n×n matrix into four (n/2)×(n/2) submatrices::

A = [A11, A12,
A21, A22]

B = [B11, B12,
B21, B22]

2. **Compute 7 products (instead of 8)**::
M1 = (A11 + A22)(B11 + B22)
M2 = (A21 + A22)B11
M3 = A11(B12 - B22)
M4 = A22(B21 - B11)
M5 = (A11 + A12)B22
M6 = (A21 - A11)(B11 + B12)
M7 = (A12 - A22)(B21 + B22)

3. **Combine results** to form the product matrix::
C11 = M1 + M4 - M5 + M7
C12 = M3 + M5
C21 = M2 + M4
C22 = M1 - M2 + M3 + M6

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
                       C = new Matrix[,] { { C11, C12 }, { C21, C22 } };
                       return C;
                   }
               }

               Matrix A = Rand(8, 8), B = Rand(8, 8), C = Strass(A, B), D = A * B;
               Console.WriteLine($"A = \n{A}");
               Console.WriteLine($"B = \n{B}");
               Console.WriteLine($"C = \n{C}");
               Console.WriteLine($"D = \n{D}");
           }

Ouput

.. terminal::

   A = 
   
      0.6996    0.5407    0.8071    0.2851    0.4391    0.2771    0.2386    0.9306
      0.1578    0.4843    0.5289    0.4142    0.4657    0.0438    0.6080    0.6308
      0.2625    0.5146    0.8049    0.8820    0.7979    0.1640    0.4873    0.2302
      0.4281    0.8386    0.4204    0.8378    0.4975    0.5651    0.2355    0.0753
      0.9474    0.7277    0.6016    0.2891    0.3143    0.3708    0.4709    0.8444
      0.0353    0.2011    0.2995    0.9252    0.1382    0.7624    0.5072    0.1912
      0.7551    0.4279    0.7186    0.6417    0.0738    0.7358    0.9229    0.2506
      0.9043    0.5675    0.7235    0.8767    0.4371    0.3925    0.2234    0.6005
   
   B = 
   
      0.8001    0.4614    0.6563    0.8036    0.3156    0.2165    0.1583    0.9423
      0.7652    0.7988    0.3030    0.5092    0.1679    0.9958    0.0248    0.3779
      0.2330    0.4121    0.9722    0.5967    0.6921    0.1247    0.8533    0.0093
      0.3276    0.3394    0.0303    0.9686    0.9567    0.8148    0.8543    0.4662
      0.2000    0.2828    0.0692    0.5289    0.6610    0.9670    0.5405    0.7784
      0.3842    0.2520    0.4338    0.5827    0.3904    0.8511    0.8071    0.1547
      0.7932    0.3442    0.7651    0.8178    0.1321    0.7285    0.1932    0.3587
      0.2459    0.6712    0.5061    0.2043    0.9333    0.8107    0.5652    0.0642
   
   C = 
   
      1.8674    2.0849    2.2205    2.3743    2.4414    2.6116    2.0895    1.5339
      1.5032    1.5937    1.6128    1.9883    1.8875    2.3619    1.6033    1.1576
      1.7460    1.7525    1.7532    2.7708    2.4408    2.8410    2.2826    1.6966
      1.8785    1.7397    1.4670    2.6338    2.0192    2.8575    1.9761    1.6788
      2.3362    2.2756    2.4061    2.7108    2.3168    2.8874    1.9657    1.8331
      1.3248    1.1485    1.2285    2.1768    1.7717    2.3061    1.9526    0.9631
      2.4004    1.8964    2.5006    3.1489    2.1133    2.7752    2.2453    1.6975
      2.1767    2.1687    2.1708    3.0618    2.7522    2.9717    2.4591    2.0017
   
   D = 
   
      1.8674    2.0849    2.2205    2.3743    2.4414    2.6116    2.0895    1.5339
      1.5032    1.5937    1.6128    1.9883    1.8875    2.3619    1.6033    1.1576
      1.7460    1.7525    1.7532    2.7708    2.4408    2.8410    2.2826    1.6966
      1.8785    1.7397    1.4670    2.6338    2.0192    2.8575    1.9761    1.6788
      2.3362    2.2756    2.4061    2.7108    2.3168    2.8874    1.9657    1.8331
      1.3248    1.1485    1.2285    2.1768    1.7717    2.3061    1.9526    0.9631
      2.4004    1.8964    2.5006    3.1489    2.1133    2.7752    2.2453    1.6975
      2.1767    2.1687    2.1708    3.0618    2.7522    2.9717    2.4591    2.0017
   
