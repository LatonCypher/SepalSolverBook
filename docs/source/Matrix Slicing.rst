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
      0.9336    0.8969    0.9375    0.9980
   
   R1[2] = 0.9374969586316416
   C1 = 
      0.7939
      0.1000
      0.8278
      0.5308
      0.1404
      0.1566
      0.6468
      0.5557
   
   C1[5] = 0.15664357934755135

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
      0.0665    0.2814    0.4937    0.2752    0.8750
      0.9727    0.6953    0.5463    0.4932    0.9971
   

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
   
      0.3375    0.4202    0.4733    0.8776    0.1692    0.0705    0.8219    0.2626
      0.3765    0.1211    0.1352    0.3113    0.8642    0.3682    0.7431    0.8881
      0.7870    0.4171    0.9972    0.8540    0.7252    0.8152    0.2276    0.5585
      0.8156    0.7268    0.5844    0.7871    0.5571    0.7145    0.6359    0.6387
      0.9956    0.8902    0.8532    0.3955    0.8565    0.5059    0.9032    0.8929
      0.6268    0.2722    0.6598    0.3966    0.9470    0.5381    0.9667    0.4999
      0.8992    0.1383    0.2145    0.6652    0.1679    0.8907    0.4597    0.8213
      0.5679    0.2376    0.3778    0.9960    0.2964    0.2580    0.4901    0.9466
   
   B = 
   
      0.6057    0.9848    0.8413    0.3287    0.6961    0.6526    0.5853    0.1628
      0.3581    0.5595    0.8605    0.2854    0.1983    0.4128    0.5835    0.0527
      0.3111    0.7404    0.3689    0.2672    0.8145    0.4267    0.3134    0.3808
      0.3375    0.5089    0.9588    0.9474    0.5528    0.0816    0.0041    0.8379
      0.1080    0.2519    0.5359    0.8407    0.4812    0.1898    0.9486    0.1428
      0.2692    0.8988    0.3726    0.3149    0.0962    0.6603    0.4291    0.3269
      0.4366    0.0099    0.2932    0.6927    0.7197    0.4981    0.4076    0.6642
      0.4137    0.4115    0.1683    0.0165    0.7439    0.8468    0.3991    0.7054
   
   C = 
   
      1.3031    1.5866    2.0638    1.9269    2.0640    1.3777    1.2252    1.7711
      1.3029    1.6184    1.7370    1.8613    2.2150    1.9081    1.9699    1.7438
      1.8527    3.3287    3.0608    2.4865    2.9215    2.4432    2.3735    2.1606
      1.9961    3.0946    3.1406    2.5218    2.8925    2.5810    2.4370    2.2391
      2.3132    3.3581    3.3601    2.7037    3.5581    3.1161    3.1253    2.3827
      1.6923    2.3973    2.4609    2.4794    2.8220    2.2752    2.4570    2.0058
      1.6838    2.6455    2.2872    1.7761    2.3039    2.3341    1.7336    1.9925
      1.5899    2.1797    2.3346    1.9846    2.5251    1.9835    1.5630    2.2034
   
   D = 
   
      1.3031    1.5866    2.0638    1.9269    2.0640    1.3777    1.2252    1.7711
      1.3029    1.6184    1.7370    1.8613    2.2150    1.9081    1.9699    1.7438
      1.8527    3.3287    3.0608    2.4865    2.9215    2.4432    2.3735    2.1606
      1.9961    3.0946    3.1406    2.5218    2.8925    2.5810    2.4370    2.2391
      2.3132    3.3581    3.3601    2.7037    3.5581    3.1161    3.1253    2.3827
      1.6923    2.3973    2.4609    2.4794    2.8220    2.2752    2.4570    2.0058
      1.6838    2.6455    2.2872    1.7761    2.3039    2.3341    1.7336    1.9925
      1.5899    2.1797    2.3346    1.9846    2.5251    1.9835    1.5630    2.2034
   


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

   
      0.6888    0.8234    0.3810    0.0107    0.7201    0.3128
      0.2854    0.4346    0.1159    0.9230    0.8091    0.3386
      0.2364    0.7239    0.1876    0.2458    0.0821    0.7249
      0.3059    0.6673    0.5971    0.3844    0.5679    0.7465
      0.2922    0.7132    0.1053    0.8265    0.3060    0.7862
   
   
      0.6888
      0.8234
      0.7239
      0.6673
      0.7132
      0.5971
      0.9230
      0.8265
      0.7201
      0.8091
      0.5679
      0.7249
      0.7465
      0.7862
   

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

   
      6.3678    8.9877    6.8848    8.3321    6.0834    0.9130
      3.5862    8.5139    0.1185    8.3565    2.1538    3.8227
      9.2694    7.8772    7.5011    0.3212    1.9158    9.6381
      4.2567    5.2499    1.0664    5.1527    2.4690    4.3776
      5.9175    0.6530    0.3678    3.8831    3.8335    6.5476
   
   
      6.3678    8.9877    6.8848    8.3321    6.0834    0.0000
      0.0000    8.5139    0.0000    8.3565    0.0000    0.0000
      9.2694    7.8772    7.5011    0.0000    0.0000    9.6381
      0.0000    5.2499    0.0000    5.1527    0.0000    0.0000
      5.9175    0.0000    0.0000    0.0000    0.0000    6.5476
   
   
      6.3678    8.9877    6.8848    8.3321    6.0834    0.0000
      0.0000    8.5139    0.0000    8.3565    0.0000    0.0000
         NaN    7.8772    7.5011    0.0000    0.0000       NaN
      0.0000    5.2499    0.0000    5.1527    0.0000    0.0000
      5.9175    0.0000    0.0000    0.0000    0.0000    6.5476
   

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

   
      6.5000    3.0115    6.5000    2.0339    6.5000    4.1261
      9.5900    0.8726    1.7239    6.5000    6.5000    6.5000
      6.5000    4.8293    6.5000    0.1906    3.8861    6.5000
      0.5704    8.8673    6.5000    6.5000    6.5000    4.6298
      1.0252    4.5536    6.5000    3.8612    1.5801    6.5000
   
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
   
