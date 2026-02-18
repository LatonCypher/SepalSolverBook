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
      0.5364    0.4359    0.2438    0.2478
   
   R1[2] = 0.2438158509060685
   C1 = 
      0.2135
      0.4723
      0.8985
      0.1983
      0.0090
      0.7671
      0.1494
      0.3854
   
   C1[5] = 0.7671247225949949

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
      0.2254    0.2834    0.5793    0.5038    0.8330
      0.0211    0.0394    0.5666    0.0993    0.5448
   

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
   
      0.9238    0.4344    0.5320    0.5866    0.2467    0.6816    0.5634    0.8297
      0.8017    0.2929    0.0707    0.9363    0.1560    0.5478    0.6133    0.3378
      0.9282    0.0749    0.9822    0.6604    0.3671    0.6253    0.8883    0.2031
      0.2256    0.0373    0.3838    0.2722    0.9653    0.8910    0.9389    0.0543
      0.0803    0.7430    0.6763    0.2807    0.5494    0.5124    0.1238    0.4606
      0.1450    0.9992    0.4644    0.7729    0.3947    0.4007    0.2947    0.2366
      0.9155    0.0133    0.0439    0.5885    0.6218    0.8181    0.5259    0.1801
      0.2243    0.9278    0.3395    0.5015    0.5847    0.9894    0.0403    0.5033
   
   B = 
   
      0.8626    0.9707    0.1000    0.8979    0.5626    0.7911    0.8423    0.6084
      0.1110    0.8479    0.7043    0.5605    0.4556    0.5930    0.3567    0.6237
      0.9756    0.2475    0.2027    0.9110    0.1750    0.5492    0.5376    0.4082
      0.5483    0.8105    0.4833    0.1926    0.0722    0.9343    0.8248    0.4430
      0.6339    0.3867    0.1583    0.7812    0.4965    0.2778    0.4674    0.1449
      0.1057    0.3010    0.0258    0.0443    0.1810    0.5882    0.3759    0.7116
      0.2454    0.3428    0.9538    0.5679    0.2256    0.9274    0.1665    0.6124
      0.8630    0.8921    0.3824    0.7845    0.8102    0.2421    0.9328    0.5793
   
   C = 
   
      2.7684    3.1061    1.7010    2.8644    1.8983    3.0216    2.9422    2.6565
      1.9051    2.5396    1.5062    1.8882    1.2530    2.7376    2.2860    2.0977
      2.8214    2.5588    1.6631    2.8758    1.4363    3.2781    2.6253    2.4648
      1.7057    1.5779    1.3503    1.9949    1.1270    2.3418    1.6273    1.8181
      1.7957    1.9229    1.1984    2.0423    1.2889    1.8182    1.8275    1.6995
      1.6819    2.3148    1.6303    1.9414    1.2006    2.3604    1.9706    1.9033
      1.9218    2.2154    1.0842    1.9448    1.2927    2.4914    2.1386    1.9426
      1.8220    2.4816    1.3360    2.0456    1.5307    2.2863    2.2373    2.1808
   
   D = 
   
      2.7684    3.1061    1.7010    2.8644    1.8983    3.0216    2.9422    2.6565
      1.9051    2.5396    1.5062    1.8882    1.2530    2.7376    2.2860    2.0977
      2.8214    2.5588    1.6631    2.8758    1.4363    3.2781    2.6253    2.4648
      1.7057    1.5779    1.3503    1.9949    1.1270    2.3418    1.6273    1.8181
      1.7957    1.9229    1.1984    2.0423    1.2889    1.8182    1.8275    1.6995
      1.6819    2.3148    1.6303    1.9414    1.2006    2.3604    1.9706    1.9033
      1.9218    2.2154    1.0842    1.9448    1.2927    2.4914    2.1386    1.9426
      1.8220    2.4816    1.3360    2.0456    1.5307    2.2863    2.2373    2.1808
   


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

   
      0.1324    0.0681    0.9839    0.2443    0.3172    0.9474
      0.8540    0.5856    0.9292    0.3702    0.2803    0.5755
      0.6898    0.2223    0.3573    0.0401    0.7427    0.9415
      0.6673    0.4078    0.8178    0.0636    0.5566    0.3240
      0.4118    0.9515    0.3995    0.0671    0.1867    0.5338
   
   
      0.8540
      0.6898
      0.6673
      0.5856
      0.9515
      0.9839
      0.9292
      0.8178
      0.7427
      0.5566
      0.9474
      0.5755
      0.9415
      0.5338
   

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

   
      3.1914    3.8201    8.9261    3.4211    9.9144    2.7068
      3.1010    4.5330    6.2583    3.5236    7.3859    8.0869
      5.8882    4.5732    1.7739    1.6011    2.3795    8.1404
      3.3236    3.7424    7.8750    1.8541    7.9218    4.8576
      7.5295    9.8889    3.2269    0.9706    4.3275    0.6541
   
   
      0.0000    0.0000    8.9261    0.0000    9.9144    0.0000
      0.0000    0.0000    6.2583    0.0000    7.3859    8.0869
      5.8882    0.0000    0.0000    0.0000    0.0000    8.1404
      0.0000    0.0000    7.8750    0.0000    7.9218    0.0000
      7.5295    9.8889    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    8.9261    0.0000       NaN    0.0000
      0.0000    0.0000    6.2583    0.0000    7.3859    8.0869
      5.8882    0.0000    0.0000    0.0000    0.0000    8.1404
      0.0000    0.0000    7.8750    0.0000    7.9218    0.0000
      7.5295       NaN    0.0000    0.0000    0.0000    0.0000
   

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

   
      4.4839    1.2360    3.9009    8.4821    6.5000    3.3200
      6.5000    9.0836    2.0700    4.1174    9.2712    2.4048
      6.5000    9.8576    6.5000    0.4492    6.5000    3.9812
      6.5000    1.0117    2.1796    6.5000    1.4073    6.5000
      4.1107    4.3823    6.5000    3.8778    6.5000    9.0375
   
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
   
