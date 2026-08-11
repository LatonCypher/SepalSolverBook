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
      0.6698    0.7410    0.3536    0.3345
   
   R1[2] = 0.35358389293104586
   C1 = 
      0.0691
      0.0233
      0.4291
      0.7849
      0.1950
      0.4479
      0.7193
      0.9998
   
   C1[5] = 0.44785837447742405

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
      0.8157    0.1933    0.4589    0.7910    0.1517
      0.0381    0.1949    0.1751    0.8837    0.5422
   

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
   
      0.8617    0.7820    0.1610    0.4951    0.5646    0.8131    0.6279    0.5719
      0.2560    0.1347    0.4827    0.5849    0.7887    0.0729    0.8749    0.3973
      0.2718    0.0477    0.9993    0.8762    0.3238    0.1771    0.8865    0.7785
      0.0549    0.9329    0.6602    0.0545    0.6365    0.4763    0.0849    0.3440
      0.9013    0.0418    0.2796    0.4242    0.8728    0.4811    0.9367    0.2434
      0.8898    0.5669    0.8396    0.8963    0.8563    0.6900    0.9657    0.6260
      0.8987    0.0818    0.3961    0.6619    0.7901    0.1244    0.2229    0.7564
      0.6064    0.7479    0.1378    0.2299    0.2907    0.9832    0.0108    0.5759
   
   B = 
   
      0.0834    0.1115    0.8407    0.7511    0.0251    0.5953    0.8484    0.8858
      0.1879    0.7937    0.3165    0.3670    0.7651    0.9397    0.6884    0.5825
      0.2339    0.2705    0.2536    0.3278    0.6966    0.7772    0.4144    0.9837
      0.9997    0.3473    0.5426    0.3097    0.8875    0.2224    0.3780    0.0145
      0.4018    0.0629    0.3815    0.2843    0.3166    0.1052    0.9433    0.3308
      0.2442    0.4875    0.8965    0.5298    0.7656    0.3233    0.4900    0.8530
      0.1003    0.6616    0.1607    0.3072    0.3156    0.1439    0.7520    0.6897
      0.2717    0.4997    0.3329    0.5780    0.4332    0.8316    0.8025    0.1084
   
   C = 
   
      1.3953    2.0653    2.5170    2.2551    2.4187    2.3712    3.3853    2.7598
      1.2747    1.3317    1.3368    1.3424    1.7186    1.3470    2.4875    1.7582
      1.6150    1.7249    1.6564    1.7287    2.3722    2.0444    2.6922    2.2183
      0.8628    1.4443    1.3363    1.2751    1.9653    1.9537    2.1566    1.9551
      1.2008    1.3874    2.0679    1.8469    1.6717    1.4720    3.0284    2.4756
      2.0526    2.4294    2.9355    2.6967    3.2117    2.8868    4.2061    3.5630
      1.4204    1.1380    1.9416    1.8361    1.6920    1.8513    2.8140    1.8462
      0.9677    1.5709    2.0919    1.7861    1.9852    2.0508    2.3996    2.1165
   
   D = 
   
      1.3953    2.0653    2.5170    2.2551    2.4187    2.3712    3.3853    2.7598
      1.2747    1.3317    1.3368    1.3424    1.7186    1.3470    2.4875    1.7582
      1.6150    1.7249    1.6564    1.7287    2.3722    2.0444    2.6922    2.2183
      0.8628    1.4443    1.3363    1.2751    1.9653    1.9537    2.1566    1.9551
      1.2008    1.3874    2.0679    1.8469    1.6717    1.4720    3.0284    2.4756
      2.0526    2.4294    2.9355    2.6967    3.2117    2.8868    4.2061    3.5630
      1.4204    1.1380    1.9416    1.8361    1.6920    1.8513    2.8140    1.8462
      0.9677    1.5709    2.0919    1.7861    1.9852    2.0508    2.3996    2.1165
   


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

   
      0.3774    0.3632    0.2784    0.8761    0.2135    0.3531
      0.4984    0.8095    0.0810    0.5129    0.7367    0.8940
      0.1174    0.9756    0.1447    0.8644    0.6483    0.7790
      0.3173    0.6027    0.9020    0.5780    0.4876    0.5626
      0.0854    0.9004    0.4758    0.6768    0.7038    0.1025
   
   
      0.8095
      0.9756
      0.6027
      0.9004
      0.9020
      0.8761
      0.5129
      0.8644
      0.5780
      0.6768
      0.7367
      0.6483
      0.7038
      0.8940
      0.7790
      0.5626
   

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

   
      5.0480    5.9325    3.0357    0.1164    5.4385    8.8624
      5.6658    4.9947    8.3068    5.6745    7.6613    7.1037
      3.9581    5.1303    5.4307    1.3487    9.6092    0.3946
      5.8414    2.3238    8.5565    7.3565    1.3620    8.6993
      0.8991    1.7549    1.7337    4.1112    1.2184    6.8296
   
   
      5.0480    5.9325    0.0000    0.0000    5.4385    8.8624
      5.6658    0.0000    8.3068    5.6745    7.6613    7.1037
      0.0000    5.1303    5.4307    0.0000    9.6092    0.0000
      5.8414    0.0000    8.5565    7.3565    0.0000    8.6993
      0.0000    0.0000    0.0000    0.0000    0.0000    6.8296
   
   
      5.0480    5.9325    0.0000    0.0000    5.4385    8.8624
      5.6658    0.0000    8.3068    5.6745    7.6613    7.1037
      0.0000    5.1303    5.4307    0.0000       NaN    0.0000
      5.8414    0.0000    8.5565    7.3565    0.0000    8.6993
      0.0000    0.0000    0.0000    0.0000    0.0000    6.8296
   

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

   
      3.5887    9.6061    4.4907    6.5000    3.8891    0.4203
      9.9447    8.7059    8.0514    4.7931    6.5000    6.5000
      2.8428    6.5000    9.6592    8.5086    6.5000    4.7383
      4.1198    8.6223    1.5606    4.1705    6.5000    8.4250
      9.6629    3.4465    6.5000    9.8514    6.5000    4.7495
   
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
   
