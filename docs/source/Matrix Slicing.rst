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
      0.2012    0.0111    0.5157    0.3327
   
   R1[2] = 0.5156579087562281
   C1 = 
      0.4117
      0.6958
      0.4998
      0.4472
      0.5935
      0.1882
      0.4116
      0.2356
   
   C1[5] = 0.18815981530435388

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
      0.0786    0.4794    0.8802    0.5617    0.8353
      0.4688    0.8069    0.8157    0.3578    0.0798
   

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

   A = \begin{pmatrix}
   A_{11} & A_{12} \\
   A_{21} & A_{22}
   \end{pmatrix}
   
   B = \begin{pmatrix}
   B_{11} & B_{12} \\
   B_{21} & B_{22}
   \end{pmatrix}


2. **Compute 7 products (instead of 8)**::

.. math::

   \begin{array}
   M_1 &=& (A_{11} + A_{22})(_{B11} + B_{22}) \\
   M_2 &=& (A_{21} + A_{22})B_{11} \\
   M_3 &=& A_{11}(B_{12} - B_{22}) \\
   M_4 &=& A_{22}(B_{21} - B_{11}) \\
   M_5 &=& (A_{11} + A_{12})B_{22} \\
   M_6 &=& (A_{21} - A_{11})(B_{11} + B_{12}) \\
   M_7 &=& (A_{12} - A_{22})(B_{21} + B_{22})
   \end{array}


3. **Combine results** to form the product matrix::

.. math::

   \begin{array}
   C_{11} = M_1 + M_4 - M_5 + M_7
   C_{12} = M_3 + M_5
   C_{21} = M_2 + M_4
   C_{22} = M_1 - M_2 + M_3 + M_6
   \end{array}


4. ** Return the result

.. math::

   C = \begin{pmatrix}
   C_{11} & C_{12} \\
   C_{21} & C_{22}
   \end{pmatrix}


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
   
      0.5234    0.9822    0.6042    0.0050    0.6803    0.9229    0.3782    0.9965
      0.8828    0.2152    0.8916    0.5125    0.6713    0.0612    0.0086    0.1251
      0.8978    0.1919    0.6297    0.6157    0.6247    0.9577    0.1857    0.2549
      0.3813    0.1309    0.9359    0.7888    0.8331    0.0720    0.8663    0.0132
      0.0201    0.3654    0.6954    0.0941    0.7402    0.9178    0.4169    0.9938
      0.0459    0.1586    0.6061    0.4126    0.6767    0.0217    0.8933    0.3572
      0.4704    0.8129    0.5340    0.2247    0.7148    0.3629    0.4828    0.0316
      0.1869    0.4540    0.2474    0.2894    0.4862    0.9665    0.1572    0.9477
   
   B = 
   
      0.9366    0.9918    0.9464    0.0525    0.5042    0.2942    0.8299    0.7234
      0.6358    0.9333    0.7821    0.0377    0.5654    0.3016    0.4571    0.3130
      0.2548    0.8244    0.3129    0.1123    0.4175    0.3990    0.0833    0.7232
      0.6429    0.8479    0.4390    0.7014    0.1989    0.8746    0.4648    0.5330
      0.1590    0.9069    0.3011    0.8494    0.9028    0.6135    0.0284    0.5280
      0.5577    0.0143    0.6069    0.5718    0.1007    0.5609    0.0773    0.8086
      0.4045    0.6172    0.6899    0.6164    0.4032    0.3186    0.9037    0.2829
      0.2293    0.9347    0.8529    0.6359    0.6268    0.4754    0.6702    0.3595
   
   C = 
   
      2.2763    3.7331    3.3307    2.1083    2.5567    2.2250    2.0362    2.6964
      1.6934    2.9779    1.8597    1.2041    1.7351    1.6370    1.2589    2.0753
      2.2862    3.0438    2.5820    1.9118    1.8416    2.2125    1.6018    2.7415
      1.7120    3.2444    2.0059    1.9746    1.9309    2.0488    1.6418    2.1620
      1.5149    2.8846    2.4789    2.2015    2.0775    2.0501    1.4201    2.2901
      1.1265    2.5424    1.6762    1.7308    1.6451    1.5458    1.4205    1.4971
      1.7566    2.8370    2.1423    1.4053    1.8608    1.6042    1.4167    1.9195
      1.6101    2.4961    2.3863    1.9230    1.7055    1.8848    1.3835    2.0340
   
   D = 
   
      2.2763    3.7331    3.3307    2.1083    2.5567    2.2250    2.0362    2.6964
      1.6934    2.9779    1.8597    1.2041    1.7351    1.6370    1.2589    2.0753
      2.2862    3.0438    2.5820    1.9118    1.8416    2.2125    1.6018    2.7415
      1.7120    3.2444    2.0059    1.9746    1.9309    2.0488    1.6418    2.1620
      1.5149    2.8846    2.4789    2.2015    2.0775    2.0501    1.4201    2.2901
      1.1265    2.5424    1.6762    1.7308    1.6451    1.5458    1.4205    1.4971
      1.7566    2.8370    2.1423    1.4053    1.8608    1.6042    1.4167    1.9195
      1.6101    2.4961    2.3863    1.9230    1.7055    1.8848    1.3835    2.0340
   
