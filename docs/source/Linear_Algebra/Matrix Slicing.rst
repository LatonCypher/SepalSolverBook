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
      0.6102    0.7969    0.8748    0.1026
   
   R1[2] = 0.8748431593412622
   C1 = 
      0.2480
      0.8018
      0.4364
      0.5220
      0.9910
      0.9913
      0.9387
      0.0193
   
   C1[5] = 0.9912740919377545

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
   A[2..5] = new double[] { 10, 15, 20 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a row
   A[1, 2..4] = new double[] { 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a col
   A[0..3, 3] = new double[] { 100, 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set submatrix elements
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
      0.0455    0.3055    0.6498    0.8489    0.1084
      0.6957    0.2807    0.7009    0.2673    0.0389
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.3511    0.0089    0.0445    0.0112    0.5001    0.0227    0.4946    0.2252
      0.0129    0.8476    0.6620    0.3707    0.5506    0.7390    0.2648    0.9557
      0.8103    0.3552    0.6851    0.3177    0.5502    0.3394    0.6346    0.3743
      0.2276    0.1611    0.3883    0.6748    0.8877    0.6107    0.2786    0.1492
      0.5248    0.3558    0.7560    0.9738    0.4239    0.5675    0.4628    0.0928
      0.2727    0.2445    0.2577    0.0010    0.2955    0.6746    0.2127    0.3594
      0.8565    0.6745    0.2918    0.2258    0.1306    0.7592    0.7531    0.2872
      0.7681    0.4059    0.9800    0.8104    0.2622    0.2737    0.3318    0.1646
   
   B = 
   
      0.4351    0.1244    0.6543    0.0669    0.2417    0.7042    0.6711    0.1006
      0.7558    0.1536    0.1916    0.1627    0.1716    0.8130    0.2361    0.0724
      0.5973    0.2109    0.5910    0.7929    0.5008    0.9008    0.7662    0.0864
      0.9296    0.2836    0.7425    0.0565    0.1736    0.5184    0.0503    0.7575
      0.9145    0.4987    0.1882    0.9827    0.7054    0.3129    0.3375    0.7959
      0.3155    0.5223    0.5413    0.7424    0.5990    0.8793    0.4691    0.5646
      0.7121    0.1570    0.4844    0.2345    0.5517    0.2101    0.0762    0.3396
      0.6865    0.9025    0.1137    0.3325    0.2795    0.7687    0.9521    0.9384
   
   C = 
   
      1.1678    0.5997    0.6376    0.7600    0.8128    0.7538    0.7039    0.8384
      2.9676    1.9412    1.5779    2.1542    1.7887    3.0990    2.1972    2.2429
      2.6448    1.2791    1.8763    1.7391    1.7012    2.5329    1.9183    1.6035
      2.3854    1.2664    1.5598    1.8282    1.5817    1.9789    1.2717    1.8654
      2.8142    1.2198    2.2031    1.7248    1.6558    2.6449    1.5974    1.7838
      1.3397    0.9836    0.9430    1.2230    1.0674    1.6301    1.2128    1.0939
      2.3591    1.1748    1.8627    1.3752    1.5507    2.6188    1.7000    1.3892
      2.6552    1.0688    2.1382    1.5336    1.4647    2.6928    1.8019    1.4357
   
   D = 
   
      1.1678    0.5997    0.6376    0.7600    0.8128    0.7538    0.7039    0.8384
      2.9676    1.9412    1.5779    2.1542    1.7887    3.0990    2.1972    2.2429
      2.6448    1.2791    1.8763    1.7391    1.7012    2.5329    1.9183    1.6035
      2.3854    1.2664    1.5598    1.8282    1.5817    1.9789    1.2717    1.8654
      2.8142    1.2198    2.2031    1.7248    1.6558    2.6449    1.5974    1.7838
      1.3397    0.9836    0.9430    1.2230    1.0674    1.6301    1.2128    1.0939
      2.3591    1.1748    1.8627    1.3752    1.5507    2.6188    1.7000    1.3892
      2.6552    1.0688    2.1382    1.5336    1.4647    2.6928    1.8019    1.4357
   


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

   
      0.3467    0.8117    0.9104    0.6130    0.2115    0.4619
      0.5240    0.4481    0.2884    0.1431    0.5110    0.7774
      0.9153    0.5492    0.8567    0.4243    0.7509    0.9137
      0.4314    0.3707    0.0399    0.4470    0.4835    0.3299
      0.9925    0.9921    0.4133    0.4584    0.0593    0.5613
   
   
      0.5240
      0.9153
      0.9925
      0.8117
      0.5492
      0.9921
      0.9104
      0.8567
      0.6130
      0.5110
      0.7509
      0.7774
      0.9137
      0.5613
   

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

   
      5.8138    5.9505    0.0615    8.7280    1.2480    9.6193
      1.3229    8.7541    3.4338    1.0929    7.8712    4.2568
      1.7951    2.2486    0.4694    5.2955    2.6367    6.4163
      1.4554    3.9961    6.0300    8.1117    9.3844    1.5760
      7.2365    6.2708    9.0398    8.3643    1.9683    9.5184
   
   
      5.8138    5.9505    0.0000    8.7280    0.0000    9.6193
      0.0000    8.7541    0.0000    0.0000    7.8712    0.0000
      0.0000    0.0000    0.0000    5.2955    0.0000    6.4163
      0.0000    0.0000    6.0300    8.1117    9.3844    0.0000
      7.2365    6.2708    9.0398    8.3643    0.0000    9.5184
   
   
      5.8138    5.9505    0.0000    8.7280    0.0000       NaN
      0.0000    8.7541    0.0000    0.0000    7.8712    0.0000
      0.0000    0.0000    0.0000    5.2955    0.0000    6.4163
      0.0000    0.0000    6.0300    8.1117       NaN    0.0000
      7.2365    6.2708       NaN    8.3643    0.0000       NaN
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      6.5000    6.5000    8.5965    6.5000    2.7618    6.5000
      1.5850    6.5000    4.2645    2.9919    2.9646    3.0265
      2.5942    2.8045    8.2908    6.5000    4.4943    8.2311
      6.5000    8.7346    6.5000    4.9177    3.6575    3.1356
      2.0515    8.6154    3.4755    6.5000    6.5000    8.9463
   
Advantages
^^^^^^^^^^


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
   
