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
      0.8313    0.6433    0.9011    0.0310
   
   R1[2] = 0.90109428011787
   C1 = 
      0.3890
      0.3901
      0.4800
      0.4296
      0.2483
      0.5242
      0.8128
      0.4774
   
   C1[5] = 0.5242072207963928

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
      6.0000    2.0000
   
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
      0.2668    0.6227    0.7596    0.2087    0.5379
      0.4595    0.4690    0.4674    0.7310    0.6964
   

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
   
      0.8659    0.0862    0.5453    0.1524    0.6670    0.3348    0.0380    0.7584
      0.5365    0.7973    0.0528    0.8460    0.2343    0.3466    0.4413    0.1955
      0.2881    0.6847    0.7979    0.2878    0.7907    0.6509    0.3295    0.1717
      0.9149    0.1365    0.1447    0.4719    0.4780    0.1332    0.4888    0.8541
      0.7985    0.2611    0.6800    0.3597    0.5190    0.6230    0.5821    0.4921
      0.2782    0.0680    0.8058    0.8817    0.5147    0.1877    0.6422    0.6612
      0.8886    0.8684    0.1331    0.1397    0.7347    0.4887    0.5745    0.8638
      0.1381    0.6563    0.4714    0.9874    0.6102    0.8353    0.7701    0.9629
   
   B = 
   
      0.9810    0.0230    0.8331    0.9647    0.3664    0.8145    0.4301    0.9630
      0.7994    0.1201    0.3608    0.6430    0.3330    0.7252    0.8798    0.7195
      0.6758    0.6977    0.1983    0.0593    0.2974    0.9912    0.2757    0.2623
      0.4450    0.8806    0.7605    0.6445    0.7740    0.4254    0.9587    0.4681
      0.0056    0.2735    0.2220    0.8271    0.3616    0.3355    0.5158    0.5327
      0.2344    0.9243    0.4465    0.8962    0.4443    0.6011    0.9034    0.4200
      0.1530    0.4464    0.3107    0.8366    0.6595    0.8569    0.4653    0.7155
      0.0927    0.1787    0.8888    0.0294    0.7985    0.2551    0.0342    0.0457
   
   C = 
   
      1.5129    1.1894    1.9600    1.9272    1.6468    2.0241    1.4349    1.6679
      1.7440    1.5063    1.9062    2.4580    1.8185    2.1423    2.4038    2.0952
      1.7207    1.8946    1.5854    2.4691    1.7232    2.6272    2.3772    2.0522
      1.5023    1.1787    2.2757    2.2320    2.0255    2.0656    1.6295    1.9374
      1.8953    1.9065    2.1796    2.6995    2.1016    2.8397    2.2237    2.2806
      1.4706    2.0722    2.0720    2.0787    2.2677    2.4541    2.0034    1.7836
      2.0047    1.4041    2.5140    3.0653    2.3140    2.7978    2.4341    2.6280
      1.8244    2.7352    2.7999    3.1454    3.0422    3.0880    3.1740    2.4619
   
   D = 
   
      1.5129    1.1894    1.9600    1.9272    1.6468    2.0241    1.4349    1.6679
      1.7440    1.5063    1.9062    2.4580    1.8185    2.1423    2.4038    2.0952
      1.7207    1.8946    1.5854    2.4691    1.7232    2.6272    2.3772    2.0522
      1.5023    1.1787    2.2757    2.2320    2.0255    2.0656    1.6295    1.9374
      1.8953    1.9065    2.1796    2.6995    2.1016    2.8397    2.2237    2.2806
      1.4706    2.0722    2.0720    2.0787    2.2677    2.4541    2.0034    1.7836
      2.0047    1.4041    2.5140    3.0653    2.3140    2.7978    2.4341    2.6280
      1.8244    2.7352    2.7999    3.1454    3.0422    3.0880    3.1740    2.4619
   
