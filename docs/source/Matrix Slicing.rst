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
      0.3014    0.0060    0.2251    0.9248
   
   R1[2] = 0.2250943605360285
   C1 = 
      0.8077
      0.0827
      0.2745
      0.7234
      0.1415
      0.3730
      0.9455
      0.4630
   
   C1[5] = 0.37299530394464486

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
      0.6287    0.2301    0.3489    0.0103    0.8361
      0.7540    0.9316    0.5994    0.8675    0.8220
   

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
   
      0.4697    0.5454    0.9738    0.5540    0.3751    0.1104    0.3416    0.1596
      0.2224    0.2484    0.5808    0.0296    0.7363    0.2229    0.6326    0.0233
      0.7787    0.2409    0.1862    0.2772    0.0969    0.9648    0.0355    0.4603
      0.8554    0.6000    0.4669    0.4391    0.3435    0.0047    0.0559    0.3675
      0.6732    0.3105    0.3077    0.7584    0.6365    0.6856    0.2854    0.6778
      0.7103    0.8178    0.7777    0.5482    0.7960    0.4345    0.6094    0.3599
      0.3655    0.3387    0.3271    0.5432    0.9620    0.1951    0.5134    0.7473
      0.1746    0.0630    0.3846    0.2871    0.5187    0.8762    0.9968    0.0021
   
   B = 
   
      0.7895    0.7869    0.7421    0.4234    0.8091    0.0433    0.6091    0.3259
      0.7481    0.1272    0.0533    0.9935    0.5918    0.4235    0.7546    0.5707
      0.1140    0.9799    0.2562    0.7264    0.8423    0.2627    0.2235    0.2448
      0.4681    0.7062    0.7403    0.4593    0.7480    0.4176    0.9421    0.6930
      0.9263    0.4084    0.4039    0.7884    0.2008    0.8597    0.6439    0.4338
      0.7933    0.4412    0.8032    0.4655    0.7103    0.4383    0.4446    0.1781
      0.5462    0.6783    0.7444    0.0216    0.3273    0.2134    0.0687    0.8496
      0.8642    0.2065    0.2032    0.7429    0.6715    0.9221    0.3588    0.5022
   
   C = 
   
      1.9088    2.2511    1.5643    2.1757    2.3101    1.3294    1.8087    1.6394
      1.6660    1.6297    1.3012    1.4917    1.3672    1.1670    1.1058    1.2852
      2.2182    1.6061    1.7776    1.6998    2.1622    1.2385    1.6178    1.1041
      2.0529    1.7732    1.3703    2.0463    2.1062    1.2454    1.8508    1.4217
      3.0290    2.3024    2.3144    2.4961    2.7183    2.0918    2.4051    1.9786
      3.2436    2.8167    2.3731    3.0403    3.0333    2.1470    2.6168    2.3897
      2.8056    2.0163    1.8546    2.3939    2.1796    2.1832    2.0727    2.0325
      2.0850    2.0000    2.0997    1.3876    1.7713    1.2997    1.3031    1.6149
   
   D = 
   
      1.9088    2.2511    1.5643    2.1757    2.3101    1.3294    1.8087    1.6394
      1.6660    1.6297    1.3012    1.4917    1.3672    1.1670    1.1058    1.2852
      2.2182    1.6061    1.7776    1.6998    2.1622    1.2385    1.6178    1.1041
      2.0529    1.7732    1.3703    2.0463    2.1062    1.2454    1.8508    1.4217
      3.0290    2.3024    2.3144    2.4961    2.7183    2.0918    2.4051    1.9786
      3.2436    2.8167    2.3731    3.0403    3.0333    2.1470    2.6168    2.3897
      2.8056    2.0163    1.8546    2.3939    2.1796    2.1832    2.0727    2.0325
      2.0850    2.0000    2.0997    1.3876    1.7713    1.2997    1.3031    1.6149
   


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

   
      0.0410    0.3782    0.9794    0.5345    0.0280    0.5995
      0.8599    0.8067    0.1551    0.4129    0.2129    0.2066
      0.7014    0.0460    0.6541    0.3226    0.1078    0.1552
      0.9579    0.8571    0.3232    0.7228    0.0468    0.4979
      0.7124    0.6693    0.7481    0.8979    0.8230    0.1363
   
   
      0.8599
      0.7014
      0.9579
      0.7124
      0.8067
      0.8571
      0.6693
      0.9794
      0.6541
      0.7481
      0.5345
      0.7228
      0.8979
      0.8230
      0.5995
   

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

   
      6.2699    2.1525    9.6297    2.4245    9.3796    3.1416
      4.0083    6.5235    1.3009    4.3763    4.2129    1.3193
      8.5173    2.2594    7.3558    1.6930    1.3712    2.9211
      6.7336    8.8082    3.9085    9.8990    9.4702    2.9084
      0.0071    3.2769    4.8733    5.6392    8.0814    1.6055
   
   
      6.2699    0.0000    9.6297    0.0000    9.3796    0.0000
      0.0000    6.5235    0.0000    0.0000    0.0000    0.0000
      8.5173    0.0000    7.3558    0.0000    0.0000    0.0000
      6.7336    8.8082    0.0000    9.8990    9.4702    0.0000
      0.0000    0.0000    0.0000    5.6392    8.0814    0.0000
   
   
      6.2699    0.0000       NaN    0.0000       NaN    0.0000
      0.0000    6.5235    0.0000    0.0000    0.0000    0.0000
      8.5173    0.0000    7.3558    0.0000    0.0000    0.0000
      6.7336    8.8082    0.0000       NaN       NaN    0.0000
      0.0000    0.0000    0.0000    5.6392    8.0814    0.0000
   

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

   
      4.4793    4.5368    0.6715    6.5000    6.5000    1.1303
      8.0750    0.3806    3.0749    6.5000    3.9356    3.0266
      4.9435    8.4857    9.0079    6.5000    0.2241    6.5000
      4.8901    8.3945    6.5000    3.1238    3.2622    1.7846
      9.7540    6.5000    8.8295    6.5000    6.5000    2.4373
   
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
   
