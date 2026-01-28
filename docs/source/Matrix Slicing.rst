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
      0.1168    0.7404    0.8878    0.2221
   
   R1[2] = 0.8877891023538931
   C1 = 
      0.4637
      0.0149
      0.8313
      0.7477
      0.9599
      0.8334
      0.6087
      0.8046
   
   C1[5] = 0.8334471123938394

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
      0.5685    0.0074    0.1546    0.9788    0.6803
      0.6150    0.8237    0.9524    0.8315    0.4252
   

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
   
      0.9622    0.8950    0.1753    0.6243    0.4127    0.4994    0.1198    0.1786
      0.7852    0.0688    0.8128    0.2935    0.7761    0.1533    0.3237    0.8248
      0.9809    0.8936    0.8508    0.8211    0.4803    0.6628    0.2696    0.4822
      0.8226    0.6987    0.4958    0.3021    0.2965    0.3497    0.2272    0.6445
      0.5786    0.2974    0.5681    0.5976    0.7472    0.6564    0.3121    0.7747
      0.7611    0.3349    0.5639    0.7870    0.0797    0.9118    0.6629    0.1807
      0.2838    0.6323    0.4940    0.4868    0.5002    0.2364    0.2243    0.3306
      0.5756    0.9006    0.6302    0.6223    0.2212    0.0400    0.0464    0.4721
   
   B = 
   
      0.7308    0.1184    0.0843    0.0812    0.2513    0.3146    0.0858    0.1247
      0.9448    0.3577    0.4745    0.1152    0.0612    0.9877    0.6136    0.3767
      0.8005    0.4705    0.7316    0.9671    0.2675    0.2569    0.8519    0.9738
      0.1114    0.4612    0.5812    0.0180    0.9954    0.0870    0.6890    0.7655
      0.4606    0.2541    0.0063    0.1652    0.6294    0.8709    0.7403    0.3567
      0.2853    0.0120    0.7953    0.0421    0.6097    0.3916    0.3316    0.0479
      0.5561    0.8433    0.2579    0.5034    0.2659    0.9506    0.2497    0.5016
      0.4113    0.6399    0.8997    0.3333    0.5949    0.5043    0.8633    0.1655
   
   C = 
   
      2.2313    1.1306    1.5883    0.5711    1.6672    2.0450    1.8663    1.3666
      2.2426    1.6352    1.8164    1.4355    1.8697    2.0090    2.4225    1.7230
      3.0921    1.8806    2.6398    1.4238    2.4110    2.6585    2.9818    2.3341
      2.3195    1.4035    1.8576    1.0251    1.5264    2.0387    2.0783    1.4230
      2.2487    1.6744    2.2571    1.2078    2.3247    2.2688    2.6453    1.7775
      2.1515    1.5440    2.1522    1.1054    2.0358    1.9312    1.9762    1.8072
      1.8129    1.2473    1.5147    0.8981    1.4419    1.7911    1.9586    1.4842
      2.1787    1.3718    1.7686    0.9901    1.4446    1.7773    2.1638    1.6834
   
   D = 
   
      2.2313    1.1306    1.5883    0.5711    1.6672    2.0450    1.8663    1.3666
      2.2426    1.6352    1.8164    1.4355    1.8697    2.0090    2.4225    1.7230
      3.0921    1.8806    2.6398    1.4238    2.4110    2.6585    2.9818    2.3341
      2.3195    1.4035    1.8576    1.0251    1.5264    2.0387    2.0783    1.4230
      2.2487    1.6744    2.2571    1.2078    2.3247    2.2688    2.6453    1.7775
      2.1515    1.5440    2.1522    1.1054    2.0358    1.9312    1.9762    1.8072
      1.8129    1.2473    1.5147    0.8981    1.4419    1.7911    1.9586    1.4842
      2.1787    1.3718    1.7686    0.9901    1.4446    1.7773    2.1638    1.6834
   
