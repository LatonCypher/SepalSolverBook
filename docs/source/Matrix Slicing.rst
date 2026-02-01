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
      0.7556    0.9981    0.0686    0.5623
   
   R1[2] = 0.06856252320122347
   C1 = 
      0.1225
      0.9855
      0.4764
      0.7258
      0.8008
      0.2513
      0.5068
      0.5038
   
   C1[5] = 0.25130607221522305

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
   8 1 6 1 16
   3 5 6 2 15
   4 7 2 1 14
   
   A[1,2] = 6
   A[5] = 7
   A[2..5] = 
   4
   1
   5
   
   A[1, 2..4] = 
   6 2
   
   A[0..3, 3] = 
   1
   2
   1
   
   A[0..3, 1..3] = 
   1 6
   5 6
   7 2
   
   A[1, ..] = 
   3 5 6 2 15
   
   A[1..3, ..] = 
   3 5 6 2 15
   4 7 2 1 14
   

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
   8 1 6 1 16
   3 5 6 2 15
   4 7 2 1 14
   
   A = 
   8 1 6 1 16
   3 5 125 2 15
   4 7 2 1 14
   
   A = 
   8 1 6 1 16
   3 5 125 2 15
   4 110 2 1 14
   
   A = 
   8 15 6 1 16
   3 20 125 2 15
   10 110 2 1 14
   
   A = 
   8 15 6 1 16
   3 20 150 200 15
   10 110 2 1 14
   
   A = 
   8 15 6 100 16
   3 20 150 150 15
   10 110 2 200 14
   
   A = 
   8 100 150 100 16
   3 100 150 150 15
   10 100 150 200 14
   
   A = 
   8 100 150 100 16
   1 2 3 4 5
   10 100 150 200 14
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      0.8398    0.2685    0.7997    0.4475    0.1658
      0.7241    0.3496    0.5731    0.9961    0.3772
   

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
   
      0.1392    0.8068    0.2406    0.9496    0.5543    0.5737    0.0206    0.0192
      0.9144    0.3351    0.6922    0.4377    0.2559    0.1303    0.0684    0.2366
      0.6929    0.9034    0.1248    0.8519    0.0166    0.0753    0.4413    0.0717
      0.4637    0.2566    0.9730    0.7615    0.5424    0.0684    0.3157    0.6122
      0.0080    0.9893    0.9854    0.2623    0.9491    0.3388    0.9167    0.1390
      0.8510    0.2304    0.2543    0.8435    0.7680    0.0309    0.5613    0.5042
      0.7874    0.8955    0.0472    0.3912    0.1012    0.9326    0.8697    0.2501
      0.5989    0.1349    0.8679    0.2939    0.0522    0.7013    0.6440    0.5574
   
   B = 
   
      0.2275    0.6064    0.2746    0.7923    0.6708    0.7892    0.3387    0.1373
      0.5468    0.9271    0.5871    0.6404    0.0342    0.4833    0.5203    0.4607
      0.5926    0.9438    0.7089    0.0465    0.2458    0.5620    0.9250    0.5415
      0.8607    0.0053    0.4392    0.1921    0.6981    0.5923    0.8593    0.1846
      0.0419    0.7421    0.2491    0.9945    0.9857    0.5133    0.4516    0.7763
      0.2594    0.7197    0.8061    0.7344    0.3651    0.2537    0.6111    0.4539
      0.9232    0.4231    0.5515    0.1013    0.5912    0.4387    0.3121    0.2695
      0.9753    0.6846    0.3762    0.8499    0.8300    0.3722    0.5210    0.4595
   
   C = 
   
      1.6426    1.9106    1.7187    1.8115    1.6271    1.6437    2.1229    1.4015
      1.5167    1.9954    1.4263    1.6135    1.6371    1.8144    1.8403    1.1205
      1.9564    1.6824    1.5185    1.4745    1.4854    1.8061    1.7809    0.9352
      2.4068    2.4458    1.8969    1.8652    2.3450    2.1499    2.5492    1.6679
      2.4620    3.2845    2.4642    2.1398    2.1810    2.2205    2.6485    2.2404
      2.2464    2.1489    1.6352    2.2674    2.7487    2.2614    2.1718    1.5096
      2.3264    2.6398    2.2979    2.3609    2.0055    2.0756    2.1298    1.4696
      2.2995    2.5063    2.1311    1.7635    1.9756    1.8943    2.2719    1.4570
   
   D = 
   
      1.6426    1.9106    1.7187    1.8115    1.6271    1.6437    2.1229    1.4015
      1.5167    1.9954    1.4263    1.6135    1.6371    1.8144    1.8403    1.1205
      1.9564    1.6824    1.5185    1.4745    1.4854    1.8061    1.7809    0.9352
      2.4068    2.4458    1.8969    1.8652    2.3450    2.1499    2.5492    1.6679
      2.4620    3.2845    2.4642    2.1398    2.1810    2.2205    2.6485    2.2404
      2.2464    2.1489    1.6352    2.2674    2.7487    2.2614    2.1718    1.5096
      2.3264    2.6398    2.2979    2.3609    2.0055    2.0756    2.1298    1.4696
      2.2995    2.5063    2.1311    1.7635    1.9756    1.8943    2.2719    1.4570
   
