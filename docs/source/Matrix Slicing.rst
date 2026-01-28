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
      0.0844    0.0567    0.5640    0.1025
   
   R1[2] = 0.5640380587606443
   C1 = 
      0.2519
      0.5525
      0.6891
      0.3766
      0.9309
      0.0724
      0.6233
      0.9889
   
   C1[5] = 0.07238469169226436

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
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000    6.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   
   A[1,2] = 6
   A[5] = 7
   A[2..5] = 
      4.0000
      1.0000
      5.0000
   
   A[1, 2..4] = 
      3.0000    5.0000    6.0000    2.0000   15.0000
   
   A[0..3, 3] = 
      1.0000
      2.0000
      1.0000
   
   A[0..3, 1..3] = 
      1.0000    6.0000
      5.0000    6.0000
      7.0000    2.0000
   
   A[1, ..] = 
      3.0000    5.0000    6.0000    2.0000   15.0000
   
   A[1..3, ..] = 
      3.0000    5.0000    6.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   

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
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000    6.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000  125.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000  125.0000    2.0000   15.0000
      4.0000  110.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000   15.0000    6.0000    1.0000   16.0000
      3.0000   20.0000  125.0000    2.0000   15.0000
     10.0000  110.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000   15.0000    6.0000    1.0000   16.0000
      3.0000   20.0000  150.0000  200.0000   15.0000
     10.0000  110.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000   15.0000    6.0000  100.0000   16.0000
      3.0000   20.0000  150.0000  150.0000   15.0000
     10.0000  110.0000    2.0000  200.0000   14.0000
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      3.0000  100.0000  150.0000  150.0000   15.0000
     10.0000  100.0000  150.0000  200.0000   14.0000
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      1.0000    2.0000    3.0000    4.0000    5.0000
     10.0000  100.0000  150.0000  200.0000   14.0000
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      0.2437    0.0773    0.8342    0.2061    0.1088
      0.9259    0.3263    0.6099    0.6141    0.4722
   

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
   
      0.4518    0.2726    0.2319    0.3597    0.7222    0.6386    0.5739    0.5763
      0.9563    0.5429    0.1521    0.2337    0.7223    0.5178    0.9063    0.4138
      0.4109    0.8551    0.7913    0.1471    0.1918    0.6638    0.9394    0.7054
      0.1152    0.7715    0.4001    0.6323    0.6443    0.5958    0.5666    0.6147
      0.7144    0.5469    0.3437    0.7102    0.5689    0.8655    0.5635    0.1587
      0.1582    0.9055    0.7619    0.4571    0.0051    0.3121    0.9781    0.5335
      0.3639    0.2517    0.3167    0.2484    0.6764    0.0119    0.4724    0.8170
      0.3513    0.4155    0.0051    0.3820    0.7664    0.2177    0.8386    0.6067
   
   B = 
   
      0.8081    0.6596    0.9490    0.7283    0.3271    0.0616    0.5482    0.1255
      0.7947    0.2532    0.3079    0.8209    0.2956    0.5280    0.3217    0.0388
      0.5322    0.8226    0.0232    0.8101    0.8396    0.0785    0.3208    0.2325
      0.6264    0.4542    0.9114    0.7798    0.3007    0.4624    0.8028    0.8573
      0.4843    0.1458    0.7973    0.8060    0.3848    0.8089    0.5264    0.8450
      0.9856    0.3753    0.5072    0.1621    0.8588    0.4655    0.3708    0.5993
      0.2633    0.2175    0.0948    0.0832    0.4806    0.7809    0.6956    0.8643
      0.2340    0.2947    0.8733    0.3717    0.5210    0.0600    0.3448    0.7842
   
   C = 
   
      2.1956    1.3607    2.3033    1.9687    1.9337    1.7204    1.9134    2.3705
      2.6271    1.6181    2.5771    2.3429    2.0450    2.0234    2.2806    2.4053
      2.6843    1.8944    2.0003    2.3594    2.5586    1.8469    2.1159    2.3197
      2.5074    1.5095    2.3389    2.4259    2.1440    2.0161    2.1135    2.5528
      2.9536    1.7919    2.5861    2.5061    2.2131    2.0007    2.3149    2.4104
      2.2316    1.6558    1.5842    2.1665    2.1146    1.7041    1.9720    2.0789
      1.4731    1.1237    1.9603    1.8121    1.4573    1.2656    1.5523    1.9697
      1.8046    1.0692    2.1405    1.8473    1.5580    1.8306    1.9113    2.3676
   
   D = 
   
      2.1956    1.3607    2.3033    1.9687    1.9337    1.7204    1.9134    2.3705
      2.6271    1.6181    2.5771    2.3429    2.0450    2.0234    2.2806    2.4053
      2.6843    1.8944    2.0003    2.3594    2.5586    1.8469    2.1159    2.3197
      2.5074    1.5095    2.3389    2.4259    2.1440    2.0161    2.1135    2.5528
      2.9536    1.7919    2.5861    2.5061    2.2131    2.0007    2.3149    2.4104
      2.2316    1.6558    1.5842    2.1665    2.1146    1.7041    1.9720    2.0789
      1.4731    1.1237    1.9603    1.8121    1.4573    1.2656    1.5523    1.9697
      1.8046    1.0692    2.1405    1.8473    1.5580    1.8306    1.9113    2.3676
   
