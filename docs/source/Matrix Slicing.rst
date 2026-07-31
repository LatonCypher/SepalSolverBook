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
      0.0465    0.2180    0.7335    0.8888
   
   R1[2] = 0.7334782838295676
   C1 = 
      0.0214
      0.0314
      0.7896
      0.9042
      0.1514
      0.8837
      0.8328
      0.4068
   
   C1[5] = 0.8836727444189474

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
      0.5429    0.7870    0.1088    0.0894    0.0999
      0.6983    0.1464    0.7142    0.1321    0.0356
   

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
   
      0.0399    0.2661    0.6353    0.8265    0.8290    0.0124    0.0440    0.7402
      0.0723    0.5884    0.6408    0.7478    0.9789    0.2830    0.8791    0.9593
      0.2644    0.6049    0.4399    0.8837    0.9942    0.2511    0.3350    0.3852
      0.5009    0.7615    0.7272    0.3275    0.9996    0.0858    0.4745    0.3363
      0.3465    0.5981    0.4271    0.5363    0.0396    0.6121    0.9008    0.3434
      1.0000    0.0092    0.2348    0.9245    0.7853    0.0170    0.7410    0.7672
      0.4634    0.7054    0.5082    0.6710    0.3421    0.7477    0.6381    0.9812
      0.3373    0.8134    0.6124    0.1921    0.0516    0.0069    0.2237    0.0491
   
   B = 
   
      0.3952    0.2802    0.3778    0.3632    0.6699    0.2135    0.5329    0.9680
      0.0934    0.7447    0.7649    0.5649    0.0105    0.3087    0.8760    0.7015
      0.7062    0.7618    0.3588    0.5184    0.6689    0.9030    0.7344    0.1569
      0.2415    0.4257    0.0189    0.3340    0.6444    0.0780    0.5834    0.9695
      0.0474    0.2985    0.3479    0.9762    0.7119    0.9903    0.8165    0.8134
      0.4317    0.0096    0.8697    0.3248    0.2643    0.4630    0.4105    0.3761
      0.6298    0.8906    0.9644    0.2288    0.3070    0.4088    0.5217    0.1749
      0.7441    0.7233    0.9575    0.7838    0.3733    0.4584    0.0368    0.8714
   
   C = 
   
      1.3120    1.8673    1.5126    2.1738    1.8703    1.9128    1.9353    2.4580
      2.1527    3.0366    3.0744    2.9412    2.3648    2.7336    2.8701    3.2006
      1.3382    2.1120    1.9933    2.3916    2.0679    2.1237    2.6132    2.9034
      1.4952    2.3658    2.2408    2.4743    2.0465    2.4020    2.7704    2.6720
      1.7129    2.1645    2.4954    1.5771    1.4645    1.5345    2.1013    2.0612
      1.8673    2.3089    2.2237    2.3419    2.5003    1.9407    2.3157    3.3509
      2.2409    2.7152    3.2338    2.5462    2.0934    2.2235    2.5847    3.1997
      0.8710    1.4987    1.2597    1.1060    0.8935    1.0594    1.6176    1.3060
   
   D = 
   
      1.3120    1.8673    1.5126    2.1738    1.8703    1.9128    1.9353    2.4580
      2.1527    3.0366    3.0744    2.9412    2.3648    2.7336    2.8701    3.2006
      1.3382    2.1120    1.9933    2.3916    2.0679    2.1237    2.6132    2.9034
      1.4952    2.3658    2.2408    2.4743    2.0465    2.4020    2.7704    2.6720
      1.7129    2.1645    2.4954    1.5771    1.4645    1.5345    2.1013    2.0612
      1.8673    2.3089    2.2237    2.3419    2.5003    1.9407    2.3157    3.3509
      2.2409    2.7152    3.2338    2.5462    2.0934    2.2235    2.5847    3.1997
      0.8710    1.4987    1.2597    1.1060    0.8935    1.0594    1.6176    1.3060
   


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

   
      0.0199    0.8961    0.9011    0.3363    0.1129    0.5285
      0.1844    0.0204    0.9551    0.7811    0.0559    0.1332
      0.3357    0.2951    0.6651    0.5425    0.7212    0.3321
      0.2408    0.8449    0.4532    0.7608    0.2851    0.1109
      0.1423    0.3258    0.3093    0.1075    0.8377    0.0215
   
   
      0.8961
      0.8449
      0.9011
      0.9551
      0.6651
      0.7811
      0.5425
      0.7608
      0.7212
      0.8377
      0.5285
   

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

   
      1.2026    6.3277    7.2055    2.3116    0.0978    6.3337
      4.2726    1.7052    0.7227    0.2565    3.6631    4.2485
      8.5116    3.3931    8.7927    9.1819    5.6164    9.6501
      9.9538    1.3099    5.6792    8.8591    3.1004    9.0574
      6.8314    1.4946    1.7382    4.8398    4.6245    4.8258
   
   
      0.0000    6.3277    7.2055    0.0000    0.0000    6.3337
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.5116    0.0000    8.7927    9.1819    5.6164    9.6501
      9.9538    0.0000    5.6792    8.8591    0.0000    9.0574
      6.8314    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    6.3277    7.2055    0.0000    0.0000    6.3337
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.5116    0.0000    8.7927       NaN    5.6164       NaN
         NaN    0.0000    5.6792    8.8591    0.0000       NaN
      6.8314    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      3.7786    9.2290    3.3955    1.0852    6.5000    3.3794
      6.5000    4.7751    6.5000    6.5000    9.5441    0.6321
      3.7463    6.5000    4.0440    1.3930    1.8713    3.6012
      2.6411    3.2766    3.5950    1.2366    6.5000    2.8078
      6.5000    6.5000    0.6428    6.5000    3.7831    2.2066
   
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
   
