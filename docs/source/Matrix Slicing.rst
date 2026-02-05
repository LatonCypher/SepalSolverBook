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
      0.7250    0.3756    0.8260    0.6769
   
   R1[2] = 0.8260370938086905
   C1 = 
      0.1969
      0.6885
      0.1864
      0.9408
      0.0353
      0.8212
      0.3793
      0.5322
   
   C1[5] = 0.821243246927638

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
      0.8493    0.4569    0.8936    0.0515    0.2949
      0.1119    0.2535    0.3612    0.8543    0.5601
   

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
   
      0.0501    0.5549    0.0228    0.8415    0.6234    0.0127    0.6740    0.4397
      0.0238    0.1567    0.2406    0.4785    0.2874    0.8150    0.3200    0.8632
      0.9312    0.1796    0.8205    0.4034    0.1702    0.2344    0.8603    0.7785
      0.1929    0.0471    0.4398    0.5623    0.7268    0.5678    0.0835    0.6885
      0.1987    0.3282    0.3921    0.5212    0.9997    0.8945    0.8920    0.1038
      0.7329    0.2141    0.3077    0.2155    0.1140    0.7649    0.8222    0.1019
      0.0071    0.1916    0.0544    0.9739    0.9535    0.9996    0.4574    0.1121
      0.4045    0.2321    0.8201    0.0781    0.7060    0.3798    0.2834    0.3773
   
   B = 
   
      0.3896    0.1276    0.8314    0.0585    0.8325    0.7139    0.7661    0.3558
      0.6937    0.7685    0.7520    0.4054    0.3887    0.4825    0.6817    0.3844
      0.4648    0.9005    0.9226    0.2350    0.4338    0.4748    0.3246    0.8567
      0.7790    0.6108    0.5758    0.8387    0.8779    0.7713    0.5511    0.9607
      0.1498    0.0225    0.3056    0.4706    0.1217    0.1369    0.6269    0.2134
      0.4461    0.6053    0.9686    0.9215    0.3557    0.4285    0.8255    0.1616
      0.5120    0.5671    0.1696    0.5113    0.3064    0.7707    0.5005    0.2452
      0.4351    0.7191    0.8552    0.9666    0.3868    0.1292    0.3346    0.8372
   
   C = 
   
      1.7059    1.6875    1.6576    2.0137    1.4630    1.6304    1.7735    1.7275
      1.5486    1.9344    2.3048    2.4070    1.3621    1.3227    1.7688    1.7287
      2.0922    2.4355    2.9891    2.1468    2.2239    2.3395    2.3157    2.4277
      1.4548    1.7028    2.3004    2.1788    1.4457    1.2991    1.8291    1.8475
      1.9440    2.0935    2.4859    2.5251    1.6740    2.1094    2.6367    1.6970
      1.5685    1.6717    2.1805    1.6600    1.5933    1.9290    2.0744    1.2485
      1.7913    1.7585    2.1940    2.6197    1.6142    1.8003    2.3798    1.6294
      1.3451    1.6940    2.2669    1.5679    1.3052    1.3770    1.8016    1.6082
   
   D = 
   
      1.7059    1.6875    1.6576    2.0137    1.4630    1.6304    1.7735    1.7275
      1.5486    1.9344    2.3048    2.4070    1.3621    1.3227    1.7688    1.7287
      2.0922    2.4355    2.9891    2.1468    2.2239    2.3395    2.3157    2.4277
      1.4548    1.7028    2.3004    2.1788    1.4457    1.2991    1.8291    1.8475
      1.9440    2.0935    2.4859    2.5251    1.6740    2.1094    2.6367    1.6970
      1.5685    1.6717    2.1805    1.6600    1.5933    1.9290    2.0744    1.2485
      1.7913    1.7585    2.1940    2.6197    1.6142    1.8003    2.3798    1.6294
      1.3451    1.6940    2.2669    1.5679    1.3052    1.3770    1.8016    1.6082
   


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

   
      0.0742    0.9308    0.2823    0.0323    0.2486    0.9868
      0.3535    0.5975    0.3985    0.9867    0.5010    0.9919
      0.6400    0.4186    0.7395    0.1697    0.8468    0.2411
      0.3350    0.6792    0.8160    0.5587    0.1137    0.1079
      0.0947    0.7430    0.3854    0.6895    0.5060    0.9254
   
   
      0.6400
      0.9308
      0.5975
      0.6792
      0.7430
      0.7395
      0.8160
      0.9867
      0.5587
      0.6895
      0.5010
      0.8468
      0.5060
      0.9868
      0.9919
      0.9254
   

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

   
      1.0847    7.1444    6.5204    9.1108    5.7773    4.1594
      3.1413    1.3392    9.7621    7.6993    9.1016    4.3310
      0.7653    6.1679    4.7902    1.1317    4.2165    6.3396
      3.2225    0.4536    7.6647    5.2730    2.8179    7.2232
      5.7173    3.5030    3.0168    7.2896    7.8977    9.1364
   
   
      0.0000    7.1444    6.5204    9.1108    5.7773    0.0000
      0.0000    0.0000    9.7621    7.6993    9.1016    0.0000
      0.0000    6.1679    0.0000    0.0000    0.0000    6.3396
      0.0000    0.0000    7.6647    5.2730    0.0000    7.2232
      5.7173    0.0000    0.0000    7.2896    7.8977    9.1364
   
   
      0.0000    7.1444    6.5204       NaN    5.7773    0.0000
      0.0000    0.0000       NaN    7.6993       NaN    0.0000
      0.0000    6.1679    0.0000    0.0000    0.0000    6.3396
      0.0000    0.0000    7.6647    5.2730    0.0000    7.2232
      5.7173    0.0000    0.0000    7.2896    7.8977       NaN
   

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

   
      3.5760    6.5000    3.1950    6.5000    9.8129    9.0557
      3.8112    1.9955    6.5000    2.2752    2.8441    2.7117
      9.8588    6.5000    0.5514    8.3254    3.9302    6.5000
      1.0104    1.7917    6.5000    4.1419    0.5189    9.1391
      2.1635    3.9928    2.4973    9.6616    9.9825    9.9278
   
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
   
