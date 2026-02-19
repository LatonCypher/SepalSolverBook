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
      0.6172    0.5230    0.8387    0.1003
   
   R1[2] = 0.8386565441472688
   C1 = 
      0.3448
      0.2541
      0.5874
      0.8112
      0.6356
      0.4134
      0.9890
      0.5808
   
   C1[5] = 0.4133992979147495

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
      0.3446    0.0195    0.7846    0.1624    0.3494
      0.3480    0.6451    0.4011    0.7261    0.9054
   

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
   
      0.2661    0.7412    0.2347    0.1623    0.5047    0.8576    0.7772    0.9682
      0.1865    0.4099    0.6212    0.7193    0.7608    0.4798    0.2068    0.8848
      0.3546    0.7796    0.7732    0.3650    0.3081    0.4963    0.0481    0.7484
      0.5448    0.6188    0.6482    0.7266    0.1452    0.7032    0.3211    0.3838
      0.9584    0.1594    0.5835    0.4792    0.2423    0.2499    0.3016    0.9473
      0.8728    0.9801    0.6583    0.1831    0.6729    0.0013    0.0003    0.7383
      0.7581    0.3173    0.1034    0.8279    0.0753    0.3591    0.7924    0.2351
      0.4630    0.1203    0.9918    0.8623    0.5538    0.9921    0.3298    0.6098
   
   B = 
   
      0.5950    0.4900    0.7026    0.0395    0.2303    0.8773    0.4752    0.7229
      0.4280    0.1572    0.8894    0.4424    0.1003    0.4397    0.8165    0.1790
      0.4271    0.4785    0.4961    0.1887    0.5324    0.5253    0.2302    0.2853
      0.2045    0.4464    0.7540    0.3540    0.2058    0.5811    0.8313    0.5897
      0.8339    0.1505    0.5184    0.7108    0.5857    0.5280    0.4005    0.2480
      0.6851    0.0741    0.5477    0.6278    0.0560    0.8169    0.9193    0.1459
      0.7384    0.0810    0.8144    0.3265    0.1022    0.8120    0.0915    0.9041
      0.7073    0.8101    0.7309    0.0175    0.2141    0.3341    0.0822    0.6030
   
   C = 
   
      2.8761    1.4185    3.1570    1.6079    0.9243    2.6985    2.0617    2.0245
      2.4405    1.6578    2.8185    1.4855    1.2459    2.3454    2.0017    1.7889
      2.1113    1.5226    2.6191    1.1934    1.0200    2.1293    1.9321    1.4753
      2.1258    1.4096    2.8050    1.3310    0.9216    2.5528    2.2829    1.7784
      2.2516    1.8346    2.6664    0.8322    1.0355    2.3894    1.5505    2.1109
      2.3418    1.6780    2.8390    1.1492    1.2397    2.2521    1.8502    1.7147
      1.8605    1.1329    2.5433    1.0247    0.6273    2.3951    1.7835    2.0517
      2.7432    1.7829    3.1193    1.6987    1.3683    3.0556    2.4775    2.0957
   
   D = 
   
      2.8761    1.4185    3.1570    1.6079    0.9243    2.6985    2.0617    2.0245
      2.4405    1.6578    2.8185    1.4855    1.2459    2.3454    2.0017    1.7889
      2.1113    1.5226    2.6191    1.1934    1.0200    2.1293    1.9321    1.4753
      2.1258    1.4096    2.8050    1.3310    0.9216    2.5528    2.2829    1.7784
      2.2516    1.8346    2.6664    0.8322    1.0355    2.3894    1.5505    2.1109
      2.3418    1.6780    2.8390    1.1492    1.2397    2.2521    1.8502    1.7147
      1.8605    1.1329    2.5433    1.0247    0.6273    2.3951    1.7835    2.0517
      2.7432    1.7829    3.1193    1.6987    1.3683    3.0556    2.4775    2.0957
   


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

   
      0.6022    0.0594    0.6137    0.6198    0.4799    0.2579
      0.6191    0.7803    0.5528    0.5329    0.9502    0.5896
      0.0837    0.7128    0.0021    0.2674    0.8324    0.8036
      0.7615    0.0281    0.5558    0.6859    0.5536    0.8476
      0.2180    0.9916    0.9065    0.7352    0.8553    0.5888
   
   
      0.6022
      0.6191
      0.7615
      0.7803
      0.7128
      0.9916
      0.6137
      0.5528
      0.5558
      0.9065
      0.6198
      0.5329
      0.6859
      0.7352
      0.9502
      0.8324
      0.5536
      0.8553
      0.5896
      0.8036
      0.8476
      0.5888
   

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

   
      6.9128    3.7345    7.5191    9.6874    0.6609    3.9838
      7.8032    3.9144    1.4638    0.5612    0.1264    2.0502
      1.1408    5.7696    0.9752    4.6888    2.8225    0.2936
      2.6872    6.5507    4.6203    9.0360    4.2907    8.1322
      8.8523    4.9424    6.5937    5.1543    3.4173    4.2489
   
   
      6.9128    0.0000    7.5191    9.6874    0.0000    0.0000
      7.8032    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    5.7696    0.0000    0.0000    0.0000    0.0000
      0.0000    6.5507    0.0000    9.0360    0.0000    8.1322
      8.8523    0.0000    6.5937    5.1543    0.0000    0.0000
   
   
      6.9128    0.0000    7.5191       NaN    0.0000    0.0000
      7.8032    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    5.7696    0.0000    0.0000    0.0000    0.0000
      0.0000    6.5507    0.0000       NaN    0.0000    8.1322
      8.8523    0.0000    6.5937    5.1543    0.0000    0.0000
   

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

   
      2.6487    2.7760    6.5000    8.1948    8.9269    2.5694
      6.5000    8.4350    2.8856    0.4963    6.5000    1.0687
      3.2116    2.4227    9.9852    8.3725    0.7743    6.5000
      9.2249    0.3953    8.2708    8.6695    6.5000    2.1935
      6.5000    2.9174    6.5000    1.2132    1.0793    1.9433
   
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
   
