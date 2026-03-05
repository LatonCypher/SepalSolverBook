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
      0.9324    0.6626    0.0269    0.9904
   
   R1[2] = 0.026898753286571653
   C1 = 
      0.0302
      0.1878
      0.0297
      0.5466
      0.3796
      0.3119
      0.8383
      0.8194
   
   C1[5] = 0.31188437899792365

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
      0.4954    0.3504    0.8502    0.4212    0.4780
      0.3635    0.0069    0.3302    0.8132    0.3000
   

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
   
      0.5664    0.2881    0.9239    0.5047    0.1329    0.5375    0.5212    0.6078
      0.9378    0.2999    0.0894    0.4001    0.0014    0.2579    0.3982    0.0396
      0.8672    0.4886    0.1373    0.1442    0.8966    0.1212    0.3566    0.2372
      0.2492    0.5555    0.0483    0.6046    0.6684    0.5649    0.4230    0.3704
      0.8622    0.4019    0.8893    0.2355    0.1166    0.3566    0.9386    0.5847
      0.0829    0.3365    0.4155    0.4569    0.3382    0.3510    0.1576    0.8074
      0.8386    0.1139    0.8733    0.4525    0.5563    0.6702    0.3811    0.8141
      0.2314    0.8526    0.5358    0.4716    0.0895    0.6467    0.4840    0.7016
   
   B = 
   
      0.4513    0.8108    0.9392    0.8234    0.8828    0.9969    0.3540    0.3190
      0.8416    0.6682    0.6950    0.3395    0.9296    0.2350    0.0108    0.7352
      0.5588    0.3547    0.9629    0.7123    0.8110    0.6657    0.2381    0.9502
      0.3007    0.3088    0.4146    0.8542    0.6393    0.5197    0.1529    0.6602
      0.0843    0.8023    0.6989    0.5535    0.1204    0.7339    0.0768    0.4473
      0.0569    0.0794    0.4054    0.8005    0.1902    0.0746    0.2016    0.1221
      0.8423    0.2026    0.4794    0.0668    0.4775    0.6928    0.4740    0.5514
      0.5644    0.4610    0.6325    0.5966    0.8895    0.3348    0.0831    0.3895
   
   C = 
   
      1.9900    1.6704    2.7762    2.5546    2.7476    2.2119    0.9169    2.2528
      1.2185    1.2365    1.6627    1.5369    1.7096    1.5822    0.6619    1.1359
      1.4395    2.0334    2.3429    1.8595    1.9357    2.1392    0.6491    1.5664
      1.4427    1.6147    2.0506    2.0160    1.8816    1.6751    0.5947    1.6785
      2.4458    1.9373    3.0889    2.4425    3.0567    2.6266    1.1317    2.4120
      1.3271    1.2839    1.8661    1.8290    1.9160    1.3296    0.4403    1.5658
      1.9640    2.1576    3.2535    3.0933    2.9444    2.6741    1.0014    2.3378
      2.1113    1.6377    2.5219    2.2826    2.7220    1.7170    0.7158    2.1803
   
   D = 
   
      1.9900    1.6704    2.7762    2.5546    2.7476    2.2119    0.9169    2.2528
      1.2185    1.2365    1.6627    1.5369    1.7096    1.5822    0.6619    1.1359
      1.4395    2.0334    2.3429    1.8595    1.9357    2.1392    0.6491    1.5664
      1.4427    1.6147    2.0506    2.0160    1.8816    1.6751    0.5947    1.6785
      2.4458    1.9373    3.0889    2.4425    3.0567    2.6266    1.1317    2.4120
      1.3271    1.2839    1.8661    1.8290    1.9160    1.3296    0.4403    1.5658
      1.9640    2.1576    3.2535    3.0933    2.9444    2.6741    1.0014    2.3378
      2.1113    1.6377    2.5219    2.2826    2.7220    1.7170    0.7158    2.1803
   


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

   
      0.5381    0.8553    0.9132    0.6919    0.4279    0.6560
      0.4262    0.5995    0.7845    0.6458    0.4800    0.5572
      0.1892    0.0196    0.1768    0.9806    0.9321    0.6671
      0.4590    0.3348    0.7847    0.6880    0.9665    0.2489
      0.4644    0.0398    0.0297    0.2729    0.0133    0.1799
   
   
      0.5381
      0.8553
      0.5995
      0.9132
      0.7845
      0.7847
      0.6919
      0.6458
      0.9806
      0.6880
      0.9321
      0.9665
      0.6560
      0.5572
      0.6671
   

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

   
      4.4921    9.3153    2.0349    6.7051    5.0962    0.2408
      3.2542    0.8180    0.6971    2.0646    1.0014    1.0545
      8.6429    5.1940    5.1341    1.6635    7.2254    6.9064
      0.0113    6.9671    1.2496    6.2907    5.8877    6.5057
      4.4707    0.0653    7.5327    1.5049    4.1235    3.6253
   
   
      0.0000    9.3153    0.0000    6.7051    5.0962    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.6429    5.1940    5.1341    0.0000    7.2254    6.9064
      0.0000    6.9671    0.0000    6.2907    5.8877    6.5057
      0.0000    0.0000    7.5327    0.0000    0.0000    0.0000
   
   
      0.0000       NaN    0.0000    6.7051    5.0962    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.6429    5.1940    5.1341    0.0000    7.2254    6.9064
      0.0000    6.9671    0.0000    6.2907    5.8877    6.5057
      0.0000    0.0000    7.5327    0.0000    0.0000    0.0000
   

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

   
      4.9227    4.6754    3.6611    6.5000    9.5966    3.0461
      6.5000    6.5000    2.2087    2.2095    8.6620    8.3184
      3.1263    0.3879    1.4350    9.7315    0.1734    4.2545
      6.5000    0.4290    1.8845    3.7619    4.4403    1.8458
      6.5000    8.6170    4.8926    1.9554    0.7595    3.7131
   
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
   
