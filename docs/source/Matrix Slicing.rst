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
      0.7371    0.2581    0.5260    0.4996
   
   R1[2] = 0.5260250696409042
   C1 = 
      0.7009
      0.6546
      0.5229
      0.9466
      0.5718
      0.8801
      0.5667
      0.7998
   
   C1[5] = 0.8801319674334436

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
      0.3724    0.7188    0.0823    0.1255    0.7404
      0.7757    0.8419    0.2899    0.4005    0.4144
   

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
   
      0.2197    0.6590    0.1124    0.5466    0.9941    0.0859    0.8171    0.8271
      0.6247    0.6210    0.3596    0.0115    0.1896    0.9538    0.4696    0.9557
      0.3691    0.1459    0.0564    0.3294    0.9951    0.0357    0.4150    0.3204
      0.4228    0.3577    0.2283    0.4158    0.5879    0.9421    0.3413    0.9318
      0.6980    0.4136    0.8399    0.9272    0.5730    0.0273    0.8679    0.7438
      0.0883    0.4236    0.4006    0.0124    0.0849    0.7289    0.4091    0.4996
      0.3823    0.5352    0.9958    0.8826    0.3760    0.0204    0.8986    0.2492
      0.9286    0.1800    0.1766    0.4195    0.5344    0.1289    0.1310    0.1580
   
   B = 
   
      0.9703    0.2971    0.8338    0.2631    0.9046    0.5129    0.7438    0.8920
      0.5705    0.6835    0.6994    0.1257    0.5937    0.3255    0.2426    0.7701
      0.2631    0.9551    0.1435    0.1072    0.5546    0.8416    0.2127    0.9994
      0.6582    0.6574    0.9321    0.6734    0.1696    0.5946    0.5825    0.8674
      0.6865    0.9386    0.2821    0.2524    0.8320    0.3018    0.0669    0.8810
      0.0931    0.2331    0.7251    0.4729    0.1345    0.1381    0.1896    0.1836
      0.8525    0.7400    0.0842    0.3782    0.7251    0.7876    0.5111    0.2727
      0.2552    0.2074    0.3647    0.2747    0.0128    0.5791    0.9983    0.1448
   
   C = 
   
      2.5766    2.7116    1.8829    1.3485    2.1866    2.1812    1.9916    2.5240
      1.9259    1.9071    2.1507    1.2277    1.7739    1.9443    2.0860    2.0134
      1.7951    1.7957    1.1834    0.8563    1.6454    1.2979    1.1192    1.8265
      1.9681    2.0787    2.2405    1.4396    1.6671    1.8887    2.0145    2.1603
      3.0702    3.2425    2.3818    1.6401    2.6193    3.0419    2.5678    3.4389
      1.0434    1.3625    1.2081    0.7859    1.0273    1.2655    1.1125    1.2086
      2.6090    3.0850    1.9461    1.3818    2.3359    2.7016    1.8771    3.1300
      1.8573    1.5048    1.6295    0.8571    1.6749    1.3068    1.3011    2.0604
   
   D = 
   
      2.5766    2.7116    1.8829    1.3485    2.1866    2.1812    1.9916    2.5240
      1.9259    1.9071    2.1507    1.2277    1.7739    1.9443    2.0860    2.0134
      1.7951    1.7957    1.1834    0.8563    1.6454    1.2979    1.1192    1.8265
      1.9681    2.0787    2.2405    1.4396    1.6671    1.8887    2.0145    2.1603
      3.0702    3.2425    2.3818    1.6401    2.6193    3.0419    2.5678    3.4389
      1.0434    1.3625    1.2081    0.7859    1.0273    1.2655    1.1125    1.2086
      2.6090    3.0850    1.9461    1.3818    2.3359    2.7016    1.8771    3.1300
      1.8573    1.5048    1.6295    0.8571    1.6749    1.3068    1.3011    2.0604
   


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

   
      0.2652    0.5403    0.4519    0.9260    0.9302    0.5116
      0.3459    0.1294    0.6383    0.8588    0.6108    0.7532
      0.0114    0.2377    0.5096    0.5342    0.2809    0.9273
      0.6067    0.7469    0.5156    0.2278    0.9503    0.1771
      0.1960    0.0751    0.4771    0.3637    0.2425    0.8495
   
   
      0.6067
      0.5403
      0.7469
      0.6383
      0.5096
      0.5156
      0.9260
      0.8588
      0.5342
      0.9302
      0.6108
      0.9503
      0.5116
      0.7532
      0.9273
      0.8495
   

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

   
      7.2400    7.4474    9.6923    1.2260    3.3747    3.2999
      3.8241    3.9729    3.6692    8.4469    5.2697    4.0011
      8.3665    8.9760    1.8050    7.8103    1.8880    3.0160
      7.7303    1.8919    7.6480    5.0670    2.9736    0.9569
      2.3744    8.8042    6.5930    5.1409    5.9171    8.3263
   
   
      7.2400    7.4474    9.6923    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.4469    5.2697    0.0000
      8.3665    8.9760    0.0000    7.8103    0.0000    0.0000
      7.7303    0.0000    7.6480    5.0670    0.0000    0.0000
      0.0000    8.8042    6.5930    5.1409    5.9171    8.3263
   
   
      7.2400    7.4474       NaN    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.4469    5.2697    0.0000
      8.3665    8.9760    0.0000    7.8103    0.0000    0.0000
      7.7303    0.0000    7.6480    5.0670    0.0000    0.0000
      0.0000    8.8042    6.5930    5.1409    5.9171    8.3263
   

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

   
      6.5000    0.3870    2.1430    1.6245    8.7449    9.7194
      6.5000    1.8357    0.8503    4.1458    6.5000    0.1464
      4.4887    3.0590    1.5169    3.4010    9.2123    6.5000
      6.5000    6.5000    3.7304    4.1203    0.7484    3.2321
      8.6225    2.9216    1.5244    4.5662    6.5000    9.2857
   
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
   
