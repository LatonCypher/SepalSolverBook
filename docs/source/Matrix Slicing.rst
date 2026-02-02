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
      0.4114    0.5956    0.3600    0.3635
   
   R1[2] = 0.35999598151185175
   C1 = 
      0.2921
      0.8144
      0.1071
      0.2541
      0.5616
      0.2581
      0.1816
      0.2500
   
   C1[5] = 0.25810717707327524

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
      0.3549    0.6745    0.1292    0.7631    0.7099
      0.8748    0.7514    0.8818    0.5290    0.3740
   

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

   \begin{array}{cc}
   M_1 =& \left(A_{11} + A_{22}\right)\left(B_{11} + B_{22}\right) \\
   M_2 =& \left(A_{21} + A_{22}\right)B_{11} \\
   M_3 =& A_{11}\left(B_{12} - B_{22}\left) \\
   M_4 =& A_{22}\left(B_{21} - B_{11}\left) \\
   M_5 =& \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 =& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 =& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix::

.. math::

   \begin{array}{cc}
   C_{11} =& M_1 + M_4 - M_5 + M_7 \\
   C_{12} =& M_3 + M_5 \\
   C_{21} =& M_2 + M_4 \\
   C_{22} =& M_1 - M_2 + M_3 + M_6
   \end{array}


4. ** Return the result

.. math::

   C = \left[\begin{array}{cc}
   C_{11} & C_{12} \\
   C_{21} & C_{22}
   \end{bmatrix} \right]



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
   
      0.5203    0.4033    0.1349    0.0934    0.4209    0.5631    0.3533    0.5262
      0.2976    0.7183    0.2535    0.1248    0.4032    0.2821    0.4790    0.2742
      0.0215    0.1174    0.8240    0.6285    0.9000    0.3294    0.1771    0.4501
      0.2026    0.1722    0.7038    0.2121    0.9023    0.6489    0.1966    0.6087
      0.1557    0.9791    0.4309    0.4069    0.0525    0.9767    0.2684    0.7946
      0.8222    0.4310    0.6044    0.5878    0.3765    0.6633    0.1911    0.4957
      0.6470    0.1880    0.1075    0.3007    0.6525    0.9889    0.6886    0.0581
      0.4633    0.7676    0.0513    0.1646    0.2058    0.5721    0.2757    0.1272
   
   B = 
   
      0.7686    0.8065    0.0367    0.6635    0.5793    0.6241    0.8962    0.8218
      0.2778    0.7048    0.3927    0.3991    0.4395    0.3907    0.0586    0.8777
      0.3796    0.3974    0.7122    0.8819    0.7161    0.3504    0.3487    0.9301
      0.9225    0.8744    0.2826    0.6864    0.1710    0.0753    0.8407    0.9320
      0.5090    0.0539    0.0123    0.7662    0.2912    0.9717    0.7124    0.6336
      0.5853    0.6572    0.0126    0.3655    0.2486    0.8868    0.7671    0.7761
      0.9599    0.1679    0.1794    0.5489    0.1839    0.2959    0.2363    0.7472
      0.5108    0.7415    0.6677    0.9323    0.5389    0.7212    0.1906    0.9014
   
   C = 
   
      1.8011    1.6815    0.7270    1.9021    1.2023    1.9289    1.5311    2.4361
      1.6099    1.4471    0.7865    1.7240    1.1144    1.5460    1.1712    2.3066
      1.9926    1.6057    1.1590    2.5461    1.3807    1.9390    1.8634    2.8369
      2.0051    1.7095    1.0973    2.5734    1.5216    2.4056    1.9184    2.9409
      2.1925    2.6217    1.4038    2.4387    1.6343    2.2309    1.6907    3.4754
      2.5399    2.5769    1.1743    2.7520    1.7758    2.3057    2.3837    3.5069
      2.4694    1.8037    0.4420    2.0990    1.1795    2.2942    2.2783    2.8248
      1.5098    1.6066    0.5456    1.4085    0.9919    1.4999    1.2912    2.1505
   
   D = 
   
      1.8011    1.6815    0.7270    1.9021    1.2023    1.9289    1.5311    2.4361
      1.6099    1.4471    0.7865    1.7240    1.1144    1.5460    1.1712    2.3066
      1.9926    1.6057    1.1590    2.5461    1.3807    1.9390    1.8634    2.8369
      2.0051    1.7095    1.0973    2.5734    1.5216    2.4056    1.9184    2.9409
      2.1925    2.6217    1.4038    2.4387    1.6343    2.2309    1.6907    3.4754
      2.5399    2.5769    1.1743    2.7520    1.7758    2.3057    2.3837    3.5069
      2.4694    1.8037    0.4420    2.0990    1.1795    2.2942    2.2783    2.8248
      1.5098    1.6066    0.5456    1.4085    0.9919    1.4999    1.2912    2.1505
   
