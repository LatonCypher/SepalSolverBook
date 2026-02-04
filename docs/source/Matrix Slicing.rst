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
      0.1208    0.3910    0.3739    0.4594
   
   R1[2] = 0.3738805657250549
   C1 = 
      0.3369
      0.1034
      0.9641
      0.0610
      0.2815
      0.0310
      0.6512
      0.8580
   
   C1[5] = 0.0309671949615018

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
      0.1982    0.9492    0.5381    0.8253    0.7010
      0.9239    0.6826    0.5340    0.7701    0.6125
   

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
   
      0.6282    0.8196    0.9860    0.5032    0.7140    0.8610    0.9639    0.4334
      0.2264    0.3506    0.9863    0.2196    0.6657    0.1271    0.3615    0.8429
      0.8462    0.8545    0.3504    0.4787    0.3571    0.8179    0.5340    0.4129
      0.9483    0.2267    0.7359    0.4390    0.5117    0.0690    0.3971    0.2803
      0.4607    0.5105    0.8645    0.5828    0.4683    0.2059    0.0537    0.3460
      0.7500    0.6973    0.2316    0.1155    0.0322    0.5969    0.9081    0.0650
      0.4583    0.0264    0.1967    0.7220    0.1692    0.0094    0.9978    0.9475
      0.6074    0.6748    0.4109    0.0799    0.2974    0.6368    0.6196    0.4408
   
   B = 
   
      0.2831    0.8462    0.6243    0.2554    0.5880    0.9221    0.7210    0.0734
      0.0966    0.1807    0.3409    0.8638    0.9063    0.2459    0.4002    0.9983
      0.6568    0.1968    0.1721    0.1686    0.6049    0.9136    0.9980    0.5640
      0.7969    0.8288    0.5735    0.5007    0.4338    0.2782    0.3340    0.6423
      0.4766    0.3831    0.1270    0.1757    0.7058    0.4823    0.7753    0.9536
      0.9551    0.6893    0.4949    0.8292    0.2511    0.7075    0.3379    0.4433
      0.3803    0.6575    0.8599    0.6684    0.5901    0.3472    0.9192    0.0252
      0.1384    0.8207    0.9547    0.3499    0.5577    0.9854    0.7998    0.1670
   
   C = 
   
      2.8948    3.1472    2.8893    2.9220    3.4576    3.5369    4.0103    2.9029
      1.6135    1.9032    1.8195    1.3958    2.3279    2.6242    2.9267    1.9049
      2.1452    2.7267    2.4579    2.4954    2.6944    2.7868    2.8360    2.2058
      1.6231    2.0868    1.7559    1.2925    2.1678    2.4343    2.6648    1.5684
      1.7001    1.7759    1.4826    1.4062    2.1162    2.2334    2.3527    2.0022
      1.4636    1.9761    1.9544    2.0211    2.0079    1.9239    2.2030    1.2850
      1.4371    2.5347    2.5317    1.5706    1.9647    2.1781    2.5879    0.9836
      1.6172    2.1050    2.0323    1.9960    2.2333    2.3671    2.5127    1.6565
   
   D = 
   
      2.8948    3.1472    2.8893    2.9220    3.4576    3.5369    4.0103    2.9029
      1.6135    1.9032    1.8195    1.3958    2.3279    2.6242    2.9267    1.9049
      2.1452    2.7267    2.4579    2.4954    2.6944    2.7868    2.8360    2.2058
      1.6231    2.0868    1.7559    1.2925    2.1678    2.4343    2.6648    1.5684
      1.7001    1.7759    1.4826    1.4062    2.1162    2.2334    2.3527    2.0022
      1.4636    1.9761    1.9544    2.0211    2.0079    1.9239    2.2030    1.2850
      1.4371    2.5347    2.5317    1.5706    1.9647    2.1781    2.5879    0.9836
      1.6172    2.1050    2.0323    1.9960    2.2333    2.3671    2.5127    1.6565
   


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

   
      0.1370    0.4096    0.4970    0.9909    0.3297    0.2138
      0.1608    0.8960    0.4495    0.8403    0.2143    0.8660
      0.1210    0.3065    0.7063    0.2997    0.4765    0.8517
      0.2247    0.0524    0.7832    0.2489    0.7995    0.3501
      0.8358    0.1718    0.0202    0.7992    0.0911    0.5145
   
   
      0.8358
      0.8960
      0.7063
      0.7832
      0.9909
      0.8403
      0.7992
      0.7995
      0.8660
      0.8517
      0.5145
   

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

   
      0.1258    2.3789    4.3243    8.7254    3.7075    6.4363
      1.3843    2.2008    7.0729    5.8871    1.3960    6.6477
      9.3528    5.5410    6.2319    8.4414    9.8787    7.6850
      9.7717    3.9827    8.4107    6.5996    0.5484    9.5227
      4.0966    2.7106    4.0845    1.0089    0.3532    7.1369
   
   
      0.0000    0.0000    0.0000    8.7254    0.0000    6.4363
      0.0000    0.0000    7.0729    5.8871    0.0000    6.6477
      9.3528    5.5410    6.2319    8.4414    9.8787    7.6850
      9.7717    0.0000    8.4107    6.5996    0.0000    9.5227
      0.0000    0.0000    0.0000    0.0000    0.0000    7.1369
   
   
      0.0000    0.0000    0.0000    8.7254    0.0000    6.4363
      0.0000    0.0000    7.0729    5.8871    0.0000    6.6477
         NaN    5.5410    6.2319    8.4414       NaN    7.6850
         NaN    0.0000    8.4107    6.5996    0.0000       NaN
      0.0000    0.0000    0.0000    0.0000    0.0000    7.1369
   

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

   
      1.0144    3.7206    6.5000    9.3214    8.2168    6.5000
      3.9730    1.7640    4.1767    6.5000    4.8664    6.5000
      1.7119    1.0856    2.1975    1.4078    4.8495    6.5000
      4.3520    4.6307    4.1586    9.6890    4.4036    0.8447
      8.8222    4.6794    0.0498    8.2492    2.6992    9.1676
   
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
   
