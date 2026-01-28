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
      0.2213    0.1769    0.3438    0.2291
   
   R1[2] = 0.3438264063259302
   C1 = 
      0.7416
      0.0079
      0.4747
      0.5122
      0.6173
      0.0305
      0.5566
      0.7312
   
   C1[5] = 0.03051451058100174

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
      0.7630    0.8284    0.5871    0.9220    0.5776
      0.2074    0.0544    0.6996    0.4954    0.0600
   

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
   
      0.9528    0.6038    0.3627    0.5291    0.6658    0.4513    0.7951    0.8095
      0.4359    0.9950    0.6459    0.7162    0.7381    0.6793    0.2128    0.0063
      0.8254    0.9521    0.1814    0.1403    0.2902    0.8812    0.7594    0.7514
      0.1021    0.1325    0.6384    0.7663    0.5021    0.9884    0.9501    0.6469
      0.9394    0.1100    0.8090    0.9741    0.3297    0.8525    0.7542    0.2273
      0.9650    0.8137    0.9692    0.5504    0.4957    0.1990    0.2606    0.1626
      0.1991    0.9498    0.1181    0.6487    0.5979    0.8229    0.1659    0.0530
      0.0219    0.2016    0.1680    0.3279    0.5389    0.9763    0.9064    0.2108
   
   B = 
   
      0.7686    0.1669    0.2855    0.2652    0.8601    0.7684    0.0618    0.8261
      0.1091    0.8210    0.2181    0.6261    0.7794    0.1210    0.9906    0.5651
      0.4918    0.2144    0.2675    0.4261    0.0320    0.3057    0.6166    0.3580
      0.2746    0.4322    0.6382    0.1984    0.7127    0.3908    0.2173    0.4223
      0.3117    0.0107    0.4424    0.1092    0.5425    0.8681    0.6219    0.7096
      0.0976    0.4905    0.8403    0.3792    0.0565    0.5891    0.0467    0.8319
      0.3407    0.4892    0.5928    0.8298    0.4411    0.9582    0.7451    0.3135
      0.6398    0.7303    0.3791    0.8675    0.7516    0.6232    0.5958    0.6456
   
   C = 
   
      2.1622    2.1698    2.2903    2.4960    3.0246    3.2330    2.5054    3.1014
      1.3308    1.7875    1.9972    1.6761    2.2191    2.1815    2.2196    2.6158
      1.7819    2.3745    2.1852    2.5679    2.6647    2.8267    2.3716    3.0063
      1.6079    2.0213    2.5790    2.3133    1.9913    2.9208    2.1495    2.6055
      1.9877    1.7980    2.5256    2.0381    2.3444    3.0159    1.8196    2.8656
      1.8250    1.6239    1.6660    1.7743    2.4049    2.2500    2.1915    2.5404
      0.8499    1.6485    1.7839    1.3874    1.8615    1.7533    1.7323    2.2125
      0.9184    1.4289    1.9805    1.6327    1.3208    2.2636    1.5575    1.9455
   
   D = 
   
      2.1622    2.1698    2.2903    2.4960    3.0246    3.2330    2.5054    3.1014
      1.3308    1.7875    1.9972    1.6761    2.2191    2.1815    2.2196    2.6158
      1.7819    2.3745    2.1852    2.5679    2.6647    2.8267    2.3716    3.0063
      1.6079    2.0213    2.5790    2.3133    1.9913    2.9208    2.1495    2.6055
      1.9877    1.7980    2.5256    2.0381    2.3444    3.0159    1.8196    2.8656
      1.8250    1.6239    1.6660    1.7743    2.4049    2.2500    2.1915    2.5404
      0.8499    1.6485    1.7839    1.3874    1.8615    1.7533    1.7323    2.2125
      0.9184    1.4289    1.9805    1.6327    1.3208    2.2636    1.5575    1.9455
   
