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
      0.7670    0.4021    0.9070    0.0567
   
   R1[2] = 0.9070138220199646
   C1 = 
      0.9984
      0.2699
      0.2185
      0.7628
      0.7955
      0.7351
      0.9254
      0.4504
   
   C1[5] = 0.735070370608152

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
      0.7607    0.8346    0.2810    0.5108    0.5816
      0.8313    0.4033    0.7511    0.0881    0.0255
   

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

   * - +--------------------+----------------------+--------------------+
   * - 
     - Feature
     - Standard Algorithm
     - Strassen Algorithm
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Approach
     - Direct row-by-column
     - Divide-and-conquer
     - 
   * - 
     - 
     - multiplication
     - with recursive
     - 
   * - 
     - 
     - 
     - submatrices
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Multiplications
     - 8
     - 7
     - 
   * - 
     - for 2×2 matrices
     - 
     - 
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Additions/
     - 4
     - 18
     - 
   * - 
     - Subtractions
     - 
     - 
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Time Complexity
     - O(n^3)
     - O(n^(log2 7))
     - 
   * - 
     - 
     - 
     - ≈ O(n^2.81)
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Best Use Case
     - Small matrices
     - Large matrices
     - 
   * - +--------------------+----------------------+--------------------+

Algorithm Steps
---------------

1. **Divide**: Split each n×n matrix into four (n/2)×(n/2) submatrices::

:math:`
A = \begin{pmatrix}
A_{11} & A_{12} \\
A_{21} & A_{22}
\end{pmatrix}`

:math:`
B = \begin{pmatrix}
B_{11} & B_{12} \\
B_{21} & B_{22}
\end{pmatrix}`

2. **Compute 7 products (instead of 8)**::
:math:`
\begin{array}
M_1 &=& (A_{11} + A_{22})(_{B11} + B_{22}) \\
M_2 &=& (A_{21} + A_{22})B_{11} \\
M_3 &=& A_{11}(B_{12} - B_{22}) \\
M_4 &=& A_{22}(B_{21} - B_{11}) \\
M_5 &=& (A_{11} + A_{12})B_{22} \\
M_6 &=& (A_{21} - A_{11})(B_{11} + B_{12}) \\
M_7 &=& (A_{12} - A_{22})(B_{21} + B_{22})
\end{array}`

3. **Combine results** to form the product matrix::
:math:`
\begin{array}
C_{11} = M_1 + M_4 - M_5 + M_7
C_{12} = M_3 + M_5
C_{21} = M_2 + M_4
C_{22} = M_1 - M_2 + M_3 + M_6
\end{array}`

4. ** Return the result
:math:`
C = \begin{pmatrix}
C_{11} & C_{12} \\
C_{21} & C_{22}
\end{pmatrix}`

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
           }

Ouput

.. terminal::

   A = 
   
      0.7002    0.6605    0.8051    0.0185    0.1863    0.4615    0.9229    0.0793
      0.5229    0.4149    0.7932    0.1798    0.3698    0.3738    0.0097    0.7409
      0.3208    0.0719    0.7747    0.8472    0.2817    0.1288    0.0493    0.7297
      0.2891    0.6936    0.1939    0.9475    0.2072    0.6971    0.7881    0.6925
      0.4345    0.7775    0.5420    0.5201    0.2250    0.0307    0.4071    0.8772
      0.0480    0.2674    0.4984    0.9897    0.4761    0.8661    0.1244    0.4161
      0.3381    0.3626    0.4781    0.7975    0.1564    0.8048    0.9772    0.4584
      0.3959    0.8956    0.3889    0.7946    0.1641    0.1543    0.6086    0.2269
   
   B = 
   
      0.9692    0.2705    0.2062    0.3681    0.3246    0.6218    0.6129    0.8546
      0.9382    0.1275    0.2323    0.0448    0.6453    0.9556    0.7874    0.6872
      0.3936    0.8042    0.6772    0.8120    0.1483    0.6759    0.5707    0.3960
      0.7113    0.3143    0.5224    0.2523    0.1015    0.4122    0.4775    0.3279
      0.1187    0.7552    0.3956    0.2077    0.7776    0.6249    0.6981    0.1495
      0.8259    0.0706    0.1129    0.2549    0.2534    0.0559    0.5609    0.9404
      0.5363    0.2700    0.7320    0.5955    0.3151    0.8501    0.7769    0.8390
      0.1630    0.1966    0.0695    0.5767    0.6810    0.0846    0.2701    0.7974
   
   C = 
   
      2.5395    1.3650    1.6595    1.6974    1.3814    2.5518    2.5448    2.6765
      1.8147    1.3427    1.0823    1.5057    1.4633    1.6547    1.8612    2.1108
      1.5712    1.3639    1.2629    1.5057    1.1157    1.4280    1.6043    1.6948
      2.8170    1.1750    1.6327    1.6234    1.7240    2.2611    2.6214    3.0111
      2.1472    1.2705    1.3604    1.5690    1.6844    2.1565    2.1638    2.3944
      2.1039    1.2950    1.3326    1.3177    1.2747    1.5170    2.0242    2.0684
      2.7054    1.3018    1.7943    1.8141    1.4412    2.2208    2.5900    2.9546
      2.4525    1.1276    1.5118    1.2689    1.3577    2.3401    2.2845    2.2295
   
   D = 
   
      2.5395    1.3650    1.6595    1.6974    1.3814    2.5518    2.5448    2.6765
      1.8147    1.3427    1.0823    1.5057    1.4633    1.6547    1.8612    2.1108
      1.5712    1.3639    1.2629    1.5057    1.1157    1.4280    1.6043    1.6948
      2.8170    1.1750    1.6327    1.6234    1.7240    2.2611    2.6214    3.0111
      2.1472    1.2705    1.3604    1.5690    1.6844    2.1565    2.1638    2.3944
      2.1039    1.2950    1.3326    1.3177    1.2747    1.5170    2.0242    2.0684
      2.7054    1.3018    1.7943    1.8141    1.4412    2.2208    2.5900    2.9546
      2.4525    1.1276    1.5118    1.2689    1.3577    2.3401    2.2845    2.2295
   
