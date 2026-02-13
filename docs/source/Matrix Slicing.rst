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
      0.9301    0.6215    0.4745    0.7771
   
   R1[2] = 0.4744779342006886
   C1 = 
      0.3645
      0.4937
      0.6484
      0.0852
      0.8057
      0.2908
      0.6994
      0.9910
   
   C1[5] = 0.29081846263741384

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
      0.8043    0.6357    0.3592    0.4689    0.1074
      0.9720    0.2331    0.7062    0.3706    0.5189
   

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
   
      0.3342    0.2139    0.4640    0.1172    0.5563    0.5100    0.9700    0.0074
      0.2421    0.6152    0.5447    0.7666    0.3739    0.7964    0.8360    0.6946
      0.2005    0.5006    0.4388    0.4545    0.1393    0.8014    0.2853    0.3214
      0.4891    0.0161    0.0698    0.9367    0.3749    0.8908    0.3485    0.1549
      0.5433    0.5253    0.7609    0.5030    0.3347    0.2380    0.6458    0.5080
      0.9399    0.5616    0.9462    0.9765    0.4986    0.3078    0.0630    0.0168
      0.3197    0.4812    0.3269    0.9920    0.4324    0.5487    0.4076    0.0432
      0.5947    0.1679    0.5655    0.6222    0.3901    0.8496    0.9046    0.4855
   
   B = 
   
      0.1308    0.0872    0.1670    0.9445    0.6990    0.6834    0.0590    0.8166
      0.2970    0.4508    0.3066    0.3112    0.8867    0.0880    0.1228    0.9174
      0.9790    0.2680    0.7043    0.9123    0.1866    0.0989    0.8440    0.6266
      0.9450    0.1861    0.0571    0.9343    0.4749    0.8325    0.3141    0.9534
      0.0719    0.6459    0.7485    0.5101    0.1711    0.8672    0.3519    0.2359
      0.6999    0.4832    0.0687    0.1346    0.7214    0.1126    0.6920    0.6095
      0.5866    0.6994    0.7606    0.1719    0.6320    0.8179    0.4453    0.8261
      0.3596    0.9554    0.5874    0.9813    0.8459    0.2286    0.1227    0.5243
   
   C = 
   
      1.6409    1.5629    1.6484    1.4414    1.6479    1.7255    1.4559    2.1189
      2.7966    2.4618    2.0349    2.7565    2.9349    2.1681    1.9306    3.4627
      1.8879    1.4292    1.0871    1.7136    1.9360    1.1207    1.3565    2.2568
      1.9329    1.3072    0.8872    1.9289    1.8721    1.8682    1.3065    2.3515
      2.1995    1.8499    1.8729    2.6531    2.2933    1.8730    1.5291    2.9059
      2.4332    1.3012    1.5034    3.1611    2.1567    2.1206    1.6482    3.1727
      2.1121    1.3879    1.1846    2.0837    1.9463    1.8990    1.3841    2.6490
      2.5972    2.1538    1.9081    2.6564    2.6276    2.2798    1.9160    3.1989
   
   D = 
   
      1.6409    1.5629    1.6484    1.4414    1.6479    1.7255    1.4559    2.1189
      2.7966    2.4618    2.0349    2.7565    2.9349    2.1681    1.9306    3.4627
      1.8879    1.4292    1.0871    1.7136    1.9360    1.1207    1.3565    2.2568
      1.9329    1.3072    0.8872    1.9289    1.8721    1.8682    1.3065    2.3515
      2.1995    1.8499    1.8729    2.6531    2.2933    1.8730    1.5291    2.9059
      2.4332    1.3012    1.5034    3.1611    2.1567    2.1206    1.6482    3.1727
      2.1121    1.3879    1.1846    2.0837    1.9463    1.8990    1.3841    2.6490
      2.5972    2.1538    1.9081    2.6564    2.6276    2.2798    1.9160    3.1989
   


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

   
      0.0305    0.9977    0.9533    0.2498    0.5159    0.9125
      0.7249    0.8689    0.8226    0.8913    0.2547    0.2225
      0.0264    0.6934    0.7188    0.7428    0.9750    0.4581
      0.5565    0.0273    0.7931    0.6836    0.3685    0.5579
      0.3375    0.2771    0.7662    0.3866    0.9467    0.3551
   
   
      0.7249
      0.5565
      0.9977
      0.8689
      0.6934
      0.9533
      0.8226
      0.7188
      0.7931
      0.7662
      0.8913
      0.7428
      0.6836
      0.5159
      0.9750
      0.9467
      0.9125
      0.5579
   

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

   
      3.5848    0.3251    9.7092    1.9745    8.6149    6.6396
      3.3622    0.1123    2.5240    3.8535    2.0148    0.1262
      5.1895    8.5142    4.3741    6.9636    2.7435    5.3657
      1.4433    5.8105    7.0582    3.6480    0.2778    6.5272
      8.6326    7.2579    4.0966    6.5920    1.0064    7.0601
   
   
      0.0000    0.0000    9.7092    0.0000    8.6149    6.6396
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      5.1895    8.5142    0.0000    6.9636    0.0000    5.3657
      0.0000    5.8105    7.0582    0.0000    0.0000    6.5272
      8.6326    7.2579    0.0000    6.5920    0.0000    7.0601
   
   
      0.0000    0.0000       NaN    0.0000    8.6149    6.6396
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      5.1895    8.5142    0.0000    6.9636    0.0000    5.3657
      0.0000    5.8105    7.0582    0.0000    0.0000    6.5272
      8.6326    7.2579    0.0000    6.5920    0.0000    7.0601
   

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

   
      6.5000    9.3842    0.7556    1.9357    6.5000    4.6914
      1.2265    6.5000    1.4172    4.9933    1.4229    1.8011
      3.8686    6.5000    6.5000    4.3933    9.3963    4.0350
      3.7427    0.5744    0.8732    1.2654    6.5000    8.1453
      3.7640    2.3735    6.5000    8.5186    9.7937    1.8317
   
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
   
