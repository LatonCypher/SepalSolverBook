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
      0.9432    0.1900    0.2687    0.6620
   
   R1[2] = 0.2686784724855358
   C1 = 
      0.0591
      0.9514
      0.1436
      0.6995
      0.9986
      0.7603
      0.3128
      0.2391
   
   C1[5] = 0.7602574619555205

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
      0.5190    0.4403    0.8075    0.2152    0.2778
      0.5429    0.7292    0.5394    0.7649    0.0244
   

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
   
      0.8212    0.5315    0.5980    0.1034    0.7744    0.2860    0.8665    0.3863
      0.8443    0.3041    0.4350    0.6468    0.0281    0.9685    0.1390    0.7616
      0.9947    0.9988    0.0636    0.1508    0.9371    0.1661    0.9951    0.0491
      0.8834    0.7283    0.0913    0.9233    0.5892    0.9615    0.6225    0.6412
      0.7910    0.9196    0.9552    0.2930    0.8335    0.9858    0.0065    0.2791
      0.3946    0.2934    0.2290    0.2244    0.3210    0.5686    0.5172    0.9658
      0.8472    0.4999    0.1596    0.0901    0.1655    0.2528    0.7650    0.0040
      0.7588    0.5492    0.3317    0.1367    0.7650    0.3736    0.6511    0.5924
   
   B = 
   
      0.8659    0.5246    0.6526    0.3798    0.7906    0.2570    0.3628    0.3328
      0.5058    0.6010    0.3936    0.2243    0.3838    0.9121    0.7223    0.0488
      0.9760    0.6613    0.4941    0.4513    0.2083    0.6368    0.0010    0.1590
      0.9995    0.3749    0.3798    0.3077    0.9982    0.0735    0.0599    0.4306
      0.5359    0.6255    0.0389    0.7217    0.4806    0.1408    0.7591    0.1312
      0.1149    0.2315    0.4553    0.1682    0.5160    0.4280    0.4889    0.7885
      0.8309    0.0677    0.0983    0.6650    0.4372    0.4399    0.9347    0.5698
      0.3084    0.1690    0.5496    0.7604    0.4231    0.4566    0.5168    0.9053
   
   C = 
   
      2.9538    1.8590    1.5377    2.2098    2.1431    1.8734    2.4259    1.6095
      2.4326    1.5357    2.0055    1.6389    2.4167    1.6463    1.5835    2.1796
      2.9425    1.9210    1.3679    2.0802    2.3256    1.8815    2.8395    1.3203
      3.2863    2.0493    2.1331    2.3125    3.2413    2.0784    2.7324    2.5123
      3.0265    2.5064    2.0966    2.0118    2.4999    2.3415    2.2347    1.7293
      1.9029    1.1496    1.4243    1.7938    1.7787    1.4884    1.8730    1.9383
      1.9869    1.0987    1.0617    1.2074    1.5311    1.2518    1.6403    1.0311
      2.5717    1.7080    1.5166    2.1015    2.1120    1.7418    2.3586    1.6932
   
   D = 
   
      2.9538    1.8590    1.5377    2.2098    2.1431    1.8734    2.4259    1.6095
      2.4326    1.5357    2.0055    1.6389    2.4167    1.6463    1.5835    2.1796
      2.9425    1.9210    1.3679    2.0802    2.3256    1.8815    2.8395    1.3203
      3.2863    2.0493    2.1331    2.3125    3.2413    2.0784    2.7324    2.5123
      3.0265    2.5064    2.0966    2.0118    2.4999    2.3415    2.2347    1.7293
      1.9029    1.1496    1.4243    1.7938    1.7787    1.4884    1.8730    1.9383
      1.9869    1.0987    1.0617    1.2074    1.5311    1.2518    1.6403    1.0311
      2.5717    1.7080    1.5166    2.1015    2.1120    1.7418    2.3586    1.6932
   


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

   
      0.5248    0.7598    0.5431    0.4509    0.3822    0.5566
      0.9902    0.0439    0.3121    0.5028    0.9813    0.9806
      0.0571    0.3688    0.9395    0.2810    0.8506    0.6706
      0.1275    0.3472    0.4811    0.1576    0.7909    0.3586
      0.1386    0.2110    0.5912    0.3043    0.6151    0.4315
   
   
      0.5248
      0.9902
      0.7598
      0.5431
      0.9395
      0.5912
      0.5028
      0.9813
      0.8506
      0.7909
      0.6151
      0.5566
      0.9806
      0.6706
   

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

   
      2.7412    3.7348    6.1844    3.1750    0.9452    6.6106
      0.8016    8.1875    2.8810    8.8364    1.3873    3.3948
      4.1963    1.9927    4.8729    5.3057    3.3982    0.6437
      7.8277    7.4068    2.8932    2.5185    8.0631    1.6260
      6.8903    5.7588    8.9797    4.7207    9.0668    4.9781
   
   
      0.0000    0.0000    6.1844    0.0000    0.0000    6.6106
      0.0000    8.1875    0.0000    8.8364    0.0000    0.0000
      0.0000    0.0000    0.0000    5.3057    0.0000    0.0000
      7.8277    7.4068    0.0000    0.0000    8.0631    0.0000
      6.8903    5.7588    8.9797    0.0000    9.0668    0.0000
   
   
      0.0000    0.0000    6.1844    0.0000    0.0000    6.6106
      0.0000    8.1875    0.0000    8.8364    0.0000    0.0000
      0.0000    0.0000    0.0000    5.3057    0.0000    0.0000
      7.8277    7.4068    0.0000    0.0000    8.0631    0.0000
      6.8903    5.7588    8.9797    0.0000       NaN    0.0000
   

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

   
      6.5000    0.5175    1.3223    9.0023    3.4544    6.5000
      6.5000    6.5000    6.5000    6.5000    0.8417    0.9153
      0.3952    9.7433    1.5970    6.5000    6.5000    6.5000
      2.9578    1.2499    9.9936    6.5000    6.5000    6.5000
      6.5000    2.3888    4.9044    6.5000    8.1383    6.5000
   
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
   
