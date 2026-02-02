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
      0.2280    0.4792    0.3771    0.0313
   
   R1[2] = 0.3771030508194444
   C1 = 
      0.4387
      0.4477
      0.8471
      0.1923
      0.8028
      0.3028
      0.2756
      0.4343
   
   C1[5] = 0.302820846625525

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
      0.6889    0.9882    0.9239    0.4207    0.5820
      0.7737    0.1919    0.5933    0.6059    0.1624
   

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

   \begin{array}
   M_1 = \left(A_{11} + A_{22}\right)\left(B_{11} + B_{22}\right) \\
   M_2 = \left(A_{21} + A_{22}\right)B_{11} \\
   M_3 = A_{11}\left(B_{12} - B_{22}\left) \\
   M_4 = A_{22}\left(B_{21} - B_{11}\left) \\
   M_5 = \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 = \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 = \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix::

.. math::

   \begin{array}
   C_{11} = M_1 + M_4 - M_5 + M_7 \\
   C_{12} = M_3 + M_5 \\
   C_{21} = M_2 + M_4 \\
   C_{22} = M_1 - M_2 + M_3 + M_6
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
   
      0.2979    0.6038    0.0549    0.8846    0.0459    0.1824    0.8715    0.1153
      0.2063    0.6957    0.7458    0.3188    0.3919    0.7568    0.1947    0.6786
      0.6048    0.7224    0.6537    0.3782    0.4954    0.0250    0.8197    0.4294
      0.0260    0.6067    0.1367    0.6994    0.4882    0.2821    0.4880    0.6817
      0.3371    0.7295    0.7613    0.7844    0.8558    0.9143    0.1633    0.5592
      0.0554    0.4112    0.4694    0.4984    0.2483    0.4657    0.0999    0.5361
      0.6628    0.6168    0.5086    0.0797    0.6547    0.6555    0.5235    0.8076
      0.3462    0.5124    0.6476    0.5620    0.3768    0.1386    0.7121    0.1656
   
   B = 
   
      0.0911    0.8043    0.8391    0.0084    0.6083    0.3604    0.0261    0.9988
      0.1863    0.6328    0.6377    0.1460    0.7732    0.6032    0.9802    0.6977
      0.1519    0.9208    0.6786    0.6890    0.4948    0.5717    0.1640    0.1696
      0.7538    0.6047    0.0117    0.6426    0.6451    0.1564    0.4265    0.8027
      0.0352    0.2258    0.5497    0.9297    0.3770    0.1391    0.9901    0.4089
      0.3697    0.1293    0.0346    0.9388    0.8044    0.8402    0.1631    0.8599
      0.3736    0.5984    0.7174    0.5445    0.4828    0.8855    0.6369    0.4724
      0.4019    0.8843    0.9577    0.7860    0.4820    0.4126    0.8535    0.0048
   
   C = 
   
      1.2558    1.8645    1.4497    1.4759    1.8862    1.6202    1.7145    2.0259
      1.1410    2.3886    2.1578    2.5363    2.4157    2.1130    2.1602    1.9801
      1.0796    2.7596    2.6887    2.0720    2.3036    2.0796    2.3756    2.1359
      1.2412    1.9952    1.7907    2.1527    1.9789    1.5812    2.3381    1.7098
      1.5274    2.8117    2.4286    3.3204    3.0582    2.3820    2.7610    2.8203
      0.9623    1.6884    1.3707    1.8480    1.6802    1.3498    1.5370    1.3736
      1.0982    2.6999    2.8271    2.6412    2.5993    2.3528    2.5171    2.3251
      1.0461    2.2144    1.9447    1.8834    1.9670    1.7598    1.8478    1.8746
   
   D = 
   
      1.2558    1.8645    1.4497    1.4759    1.8862    1.6202    1.7145    2.0259
      1.1410    2.3886    2.1578    2.5363    2.4157    2.1130    2.1602    1.9801
      1.0796    2.7596    2.6887    2.0720    2.3036    2.0796    2.3756    2.1359
      1.2412    1.9952    1.7907    2.1527    1.9789    1.5812    2.3381    1.7098
      1.5274    2.8117    2.4286    3.3204    3.0582    2.3820    2.7610    2.8203
      0.9623    1.6884    1.3707    1.8480    1.6802    1.3498    1.5370    1.3736
      1.0982    2.6999    2.8271    2.6412    2.5993    2.3528    2.5171    2.3251
      1.0461    2.2144    1.9447    1.8834    1.9670    1.7598    1.8478    1.8746
   
