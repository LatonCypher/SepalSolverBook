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
      0.0895    0.9819    0.0263    0.4158
   
   R1[2] = 0.026279131571770642
   C1 = 
      0.5829
      0.5275
      0.3176
      0.1342
      0.1852
      0.4572
      0.4812
      0.1283
   
   C1[5] = 0.45721400484795804

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
      0.9633    0.2142    0.1577    0.4993    0.5509
      0.0423    0.3933    0.8329    0.3568    0.4663
   

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
     - O(n^(log2 7)) ≈ O(n^2.81)
   * - Best Use Case
     - Small matrices
     - Large matrices

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

   \begin{array}{rcl}
   M_1 &=& \left(A_{11} + A_{22}\right)\left(B_{11} + B_{22}\right) \\
   M_2 &=& \left(A_{21} + A_{22}\right)B_{11} \\
   M_3 &=& A_{11}\left(B_{12} - B_{22}\right) \\
   M_4 &=& A_{22}\left(B_{21} - B_{11}\right) \\
   M_5 &=& \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 &=& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 &=& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix

.. math::

   \begin{array}{rcl}
   C_{11} &=& M_1 + M_4 - M_5 + M_7 \\
   C_{12} &=& M_3 + M_5 \\
   C_{21} &=& M_2 + M_4 \\
   C_{22} &=& M_1 - M_2 + M_3 + M_6
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
   
      0.7190    0.1783    0.2002    0.1960    0.5300    0.0861    0.2672    0.1888
      0.3060    0.9243    0.0859    0.1102    0.1999    0.3676    0.6425    0.1876
      0.1266    0.5281    0.9860    0.5802    0.9002    0.3180    0.7150    0.6819
      0.0433    0.8376    0.6252    0.0483    0.7625    0.7721    0.1566    0.7422
      0.0794    0.0860    0.5306    0.9223    0.4351    0.0256    0.5115    0.8675
      0.3592    0.5060    0.2429    0.4697    0.0110    0.7694    0.2246    0.9665
      0.1614    0.0463    0.1853    0.7022    0.3677    0.3871    0.5142    0.6622
      0.1477    0.9183    0.8233    0.3430    0.5706    0.6350    0.5588    0.6962
   
   B = 
   
      0.6840    0.1155    0.2740    0.2403    0.0437    0.3529    0.8818    0.0934
      0.0317    0.6478    0.0994    0.2362    0.8682    0.2456    0.1949    0.0702
      0.6722    0.0894    0.4209    0.3358    0.3636    0.5551    0.0388    0.0966
      0.1433    0.1651    0.4618    0.9401    0.2739    0.8007    0.2522    0.2610
      0.7105    0.5548    0.8107    0.3361    0.3997    0.1653    0.5531    0.3327
      0.7577    0.3177    0.3540    0.6292    0.8761    0.4657    0.2763    0.2938
      0.3462    0.6965    0.1573    0.9722    0.1476    0.0146    0.6807    0.8569
      0.7912    0.9372    0.3913    0.6491    0.0499    0.0600    0.7140    0.5347
   
   C = 
   
      1.3437    0.9332    0.9656    1.0810    0.6488    0.7085    1.3596    0.6817
      1.1036    1.5110    0.7294    1.4692    1.3835    0.6958    1.2646    0.9559
      2.5169    2.2782    1.9918    2.6721    1.7594    1.5345    1.9585    1.6658
      2.2516    2.0845    1.5871    1.8396    2.0110    1.1392    1.5095    1.1474
      1.7379    1.6834    1.4613    2.3073    0.8388    1.2255    1.5552    1.3598
      1.9255    1.7813    1.1626    2.0624    1.4319    1.1833    1.6048    1.1540
      1.5935    1.4869    1.2262    2.0690    0.9020    1.0217    1.4687    1.2503
      2.3636    2.3021    1.6844    2.4377    2.0987    1.4494    1.7961    1.4747
   
   D = 
   
      1.3437    0.9332    0.9656    1.0810    0.6488    0.7085    1.3596    0.6817
      1.1036    1.5110    0.7294    1.4692    1.3835    0.6958    1.2646    0.9559
      2.5169    2.2782    1.9918    2.6721    1.7594    1.5345    1.9585    1.6658
      2.2516    2.0845    1.5871    1.8396    2.0110    1.1392    1.5095    1.1474
      1.7379    1.6834    1.4613    2.3073    0.8388    1.2255    1.5552    1.3598
      1.9255    1.7813    1.1626    2.0624    1.4319    1.1833    1.6048    1.1540
      1.5935    1.4869    1.2262    2.0690    0.9020    1.0217    1.4687    1.2503
      2.3636    2.3021    1.6844    2.4377    2.0987    1.4494    1.7961    1.4747
   


Logical Indexing
----------------
Logical indexing is a powerful feature in **Sepal Solver** that allows you to access or modify matrix elements based on specific conditions rather than explicit coordinates. If you are familiar with MATLAB or NumPy, this syntax will feel natural.

Instead of using integer coordinates (e.g., ``A[0, 5]``), you pass a **boolean condition** into the indexer. Sepal Solver evaluates this condition across the entire matrix to create a mask, then applies the operation only to the elements where the condition is ``true``.

To extract elements that meet a specific criterion, use relational operators directly within the brackets. This returns a vector containing all matching values.


.. code-block:: csharp

   Matrix A = Rand(5, 6);
   Console.WriteLine(A);

   // Extract all values greater than 0.5
   var L = A[A > 0.5];
   Console.WriteLine(L);


Ouput

.. terminal::

   
      0.3870    0.5886    0.8789    0.4481    0.6747    0.0883
      0.8584    0.8093    0.9243    0.4353    0.7990    0.6133
      0.1763    0.0579    0.3510    0.1734    0.4489    0.7051
      0.8555    0.8316    0.5359    0.9494    0.9326    0.8996
      0.3731    0.6854    0.4616    0.9951    0.0321    0.7165
   
   
      0.8584
      0.8555
      0.5886
      0.8093
      0.8316
      0.6854
      0.8789
      0.9243
      0.5359
      0.9494
      0.9951
      0.6747
      0.7990
      0.9326
      0.6133
      0.7051
      0.8996
      0.7165
   

Logical indexing is most effective when performing bulk updates. You can set values for specific elements without affecting the rest of the matrix.


.. code-block:: csharp

   Matrix A = Rand(5, 6);
   A *= 10;
   Console.WriteLine(A);

   // Set all elements less than 5 to zero
   A[A < 5] = 0;
   Console.WriteLine(A);

   // Replace specific "masquerading" integers or outliers
   A[A > 9] = double.NaN;
   Console.WriteLine(A);


Ouput

.. terminal::

   
      3.3947    3.7014    1.1436    5.9288    1.4255    2.6572
      4.0603    8.8673    2.0717    3.8962    0.8807    6.6348
      1.2238    7.7339    1.9029    5.4589    9.8589    6.6902
      1.3737    3.0704    5.4955    3.0682    2.5976    1.3495
      1.3110    6.3681    3.6685    1.5296    4.7717    2.7780
   
   
      0.0000    0.0000    0.0000    5.9288    0.0000    0.0000
      0.0000    8.8673    0.0000    0.0000    0.0000    6.6348
      0.0000    7.7339    0.0000    5.4589    9.8589    6.6902
      0.0000    0.0000    5.4955    0.0000    0.0000    0.0000
      0.0000    6.3681    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    5.9288    0.0000    0.0000
      0.0000    8.8673    0.0000    0.0000    0.0000    6.6348
      0.0000    7.7339    0.0000    5.4589       NaN    6.6902
      0.0000    0.0000    5.4955    0.0000    0.0000    0.0000
      0.0000    6.3681    0.0000    0.0000    0.0000    0.0000
   

Complex Conditions
~~~~~~~~~~~~~~~~~~
You can combine multiple conditions using logical operators. This allows for precise data "clipping" or windowing.
* Use ``&`` for **AND**
* Use ``|`` for **OR**

.. code-block:: csharp

   Matrix A = Rand(5, 6);
   A *= 10;
   // Set values within the range (5, 8) to a new value
   A[(A > 5) & (A < 8)] = 6.5;
   Console.WriteLine(A);


Ouput

.. terminal::

   
      6.5000    2.6208    0.0843    6.5000    4.3814    1.9075
      9.8886    3.1058    6.5000    0.9916    8.7072    9.0623
      4.4764    2.7452    2.8035    1.1303    2.6853    8.7736
      6.5000    6.5000    1.2483    4.8607    4.4874    2.5938
      1.4916    6.5000    8.9859    1.9475    9.9861    8.0249
   
Advantages
~~~~~~~~~~


.. list-table:: 
   :header-rows: 1

   * - - Feature
     - - Benefit
   * - - **Declarative Syntax**
     - - Express *what* to filter rather than *how* to loop, making code easier to read.
   * - - **Vectorization**
     - - Operations are optimized internally, providing better performance than manual C# nested loops.
   * - - **In-place Updates**
     - - Modify subsets of large matrices efficiently without creating intermediate copies.

Example: Finding Integers in a Double Matrix
As discussed in the type-checking guidelines, you can use logical indexing to identify and manipulate whole numbers stored as doubles:

.. code-block:: csharp

   Matrix A = new double[,]
   {
       {1.1, 2.0, 3.9, 4.2 },
       {1.5, 3.5, 4.0, 5.1 }
   };
   Console.WriteLine(A);
   // Find all "integers" and scale them by 10
   A[A % 1 == 0] *= 10;
   Console.WriteLine(A);



Ouput

.. terminal::

   
      1.1000    2.0000    3.9000    4.2000
      1.5000    3.5000    4.0000    5.1000
   
   
      1.1000   20.0000    3.9000    4.2000
      1.5000    3.5000   40.0000    5.1000
   
