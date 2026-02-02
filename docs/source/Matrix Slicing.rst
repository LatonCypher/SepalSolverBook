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
      0.0387    0.3371    0.9811    0.1817
   
   R1[2] = 0.9810718599052055
   C1 = 
      0.7254
      0.5226
      0.5144
      0.3202
      0.2124
      0.0291
      0.6759
      0.4772
   
   C1[5] = 0.029062348457111198

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
      0.8444    0.0264    0.4560    0.0846    0.2569
      0.2694    0.3927    0.1405    0.8759    0.2168
   

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
   
      0.6781    0.5947    0.9879    0.2325    0.2526    0.0030    0.2867    0.6152
      0.9638    0.5693    0.6079    0.4027    0.6223    0.1476    0.6423    0.1156
      0.4638    0.9925    0.1360    0.8524    0.1439    0.6466    0.4337    0.2236
      0.7908    0.4817    0.8072    0.2528    0.6118    0.6991    0.7409    0.1553
      0.8571    0.5231    0.2032    0.0642    0.6475    0.9584    0.4014    0.5200
      0.8161    0.2834    0.6694    0.0526    0.5375    0.5047    0.5078    0.4613
      0.9753    0.6713    0.7815    0.3303    0.2294    0.3537    0.2282    0.2035
      0.6370    0.4243    0.8004    0.4109    0.3967    0.1880    0.1008    0.2680
   
   B = 
   
      0.8392    0.5089    0.5021    0.4091    0.6716    0.6419    0.7045    0.2675
      0.3416    0.3167    0.9693    0.9178    0.0583    0.9111    0.9371    0.4831
      0.9339    0.6358    0.4030    0.6837    0.0331    0.8508    0.0445    0.5708
      0.3994    0.7835    0.1047    0.1707    0.4868    0.2781    0.6041    0.2541
      0.5809    0.0056    0.2029    0.2479    0.7484    0.2103    0.3711    0.7166
      0.5478    0.5792    0.6559    0.9624    0.6301    0.5498    0.4464    0.6552
      0.6539    0.6864    0.2008    0.7294    0.4395    0.1536    0.9951    0.3161
      0.2266    0.2373    0.2515    0.2803    0.5649    0.8785    0.9010    0.6127
   
   C = 
   
      2.2629    1.6896    1.6050    1.9854    1.3003    2.5216    2.1542    1.7422
      2.6203    1.9301    1.7041    2.1984    1.8029    2.1788    2.5230    1.7986
      1.9676    2.0307    1.9356    2.3760    1.6207    2.2034    2.7528    1.6985
      2.9410    2.2200    1.9863    2.7690    2.0207    2.4667    2.6135    2.1951
      2.3948    1.7390    1.9975    2.5020    2.2027    2.3990    2.6780    2.1517
      2.4531    1.7252    1.6179    2.1793    1.8164    2.2400    2.2478    1.9097
      2.4318    1.8756    1.8655    2.2265    1.4906    2.4509    2.2040    1.7081
      2.0512    1.5333    1.3883    1.6954    1.2901    2.0285    1.7032    1.5403
   
   D = 
   
      2.2629    1.6896    1.6050    1.9854    1.3003    2.5216    2.1542    1.7422
      2.6203    1.9301    1.7041    2.1984    1.8029    2.1788    2.5230    1.7986
      1.9676    2.0307    1.9356    2.3760    1.6207    2.2034    2.7528    1.6985
      2.9410    2.2200    1.9863    2.7690    2.0207    2.4667    2.6135    2.1951
      2.3948    1.7390    1.9975    2.5020    2.2027    2.3990    2.6780    2.1517
      2.4531    1.7252    1.6179    2.1793    1.8164    2.2400    2.2478    1.9097
      2.4318    1.8756    1.8655    2.2265    1.4906    2.4509    2.2040    1.7081
      2.0512    1.5333    1.3883    1.6954    1.2901    2.0285    1.7032    1.5403
   
