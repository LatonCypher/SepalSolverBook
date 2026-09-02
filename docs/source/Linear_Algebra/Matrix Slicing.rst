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
      0.3551    0.4808    0.4594    0.2701
   
   R1[2] = 0.4593633148052333
   C1 = 
      0.7117
      0.1182
      0.7015
      0.6613
      0.9366
      0.6050
      0.6464
      0.1200
   
   C1[5] = 0.6050460167146016

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
      0.9636    0.7179    0.6965    0.2385    0.1814
      0.3744    0.7770    0.5991    0.5489    0.2434
   

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
   
      0.4791    0.6953    0.3185    0.5423    0.5932    0.8578    0.2606    0.2884
      0.9762    0.5337    0.4028    0.0440    0.8841    0.7320    0.4287    0.9965
      0.7200    0.6348    0.4920    0.9553    0.4074    0.2865    0.1267    0.5586
      0.6893    0.2257    0.3572    0.0147    0.4080    0.5249    0.9022    0.9580
      0.2979    0.1915    0.6901    0.5116    0.3967    0.2951    0.1361    0.2056
      0.8512    0.6188    0.8973    0.3249    0.4604    0.9814    0.2870    0.7529
      0.0894    0.2973    0.5753    0.3658    0.6436    0.5202    0.0789    0.5278
      0.6694    0.1374    0.5516    0.1272    0.7338    0.4203    0.8780    0.3191
   
   B = 
   
      0.2679    0.6713    0.1440    0.8068    0.8441    0.4965    0.5390    0.1278
      0.3779    0.5591    0.5654    0.4145    0.2745    0.6561    0.2317    0.2142
      0.2629    0.2079    0.2058    0.4667    0.2812    0.5538    0.9182    0.6231
      0.6938    0.8824    0.9254    0.0187    0.3714    0.5299    0.5963    0.1125
      0.6492    0.4214    0.1931    0.0134    0.4768    0.7459    0.6884    0.8859
      0.9563    0.8519    0.8890    0.5612    0.6884    0.0821    0.3718    0.2572
      0.1465    0.9285    0.4900    0.8691    0.1686    0.5478    0.6640    0.2865
      0.7019    0.5177    0.7005    0.7279    0.2293    0.6475    0.5267    0.6318
   
   C = 
   
      2.2971    2.6271    2.2363    1.7592    1.8697    2.0001    2.0873    1.4726
      2.6359    2.9865    2.2956    2.7183    2.3265    2.6808    2.7362    2.2188
      2.1741    2.6061    2.2347    1.7746    1.8162    2.3111    2.3219    1.4659
      1.9455    2.6289    1.9726    2.5981    1.6774    2.1579    2.3403    1.7209
      1.3926    1.5533    1.3163    1.0902    1.1505    1.4546    1.7252    1.1629
      2.7312    3.0771    2.5873    2.7225    2.3776    2.5665    2.8889    2.0552
      1.8386    1.7295    1.6330    1.2241    1.2540    1.6596    1.8303    1.5347
      1.6954    2.4009    1.5744    2.0979    1.6656    2.0648    2.3875    1.6843
   
   D = 
   
      2.2971    2.6271    2.2363    1.7592    1.8697    2.0001    2.0873    1.4726
      2.6359    2.9865    2.2956    2.7183    2.3265    2.6808    2.7362    2.2188
      2.1741    2.6061    2.2347    1.7746    1.8162    2.3111    2.3219    1.4659
      1.9455    2.6289    1.9726    2.5981    1.6774    2.1579    2.3403    1.7209
      1.3926    1.5533    1.3163    1.0902    1.1505    1.4546    1.7252    1.1629
      2.7312    3.0771    2.5873    2.7225    2.3776    2.5665    2.8889    2.0552
      1.8386    1.7295    1.6330    1.2241    1.2540    1.6596    1.8303    1.5347
      1.6954    2.4009    1.5744    2.0979    1.6656    2.0648    2.3875    1.6843
   


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

   
      0.2862    0.5511    0.0567    0.6528    0.5038    0.0417
      0.9340    0.1194    0.8492    0.2752    0.3219    0.1299
      0.0807    0.3762    0.8254    0.1088    0.2027    0.0944
      0.9701    0.7476    0.0591    0.9887    0.7299    0.8066
      0.7994    0.2298    0.3142    0.3316    0.6989    0.8378
   
   
      0.9340
      0.9701
      0.7994
      0.5511
      0.7476
      0.8492
      0.8254
      0.6528
      0.9887
      0.5038
      0.7299
      0.6989
      0.8066
      0.8378
   

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

   
      4.7774    4.5061    0.5630    6.2643    8.5739    9.2221
      2.6378    0.7736    3.7840    5.2141    1.9446    3.1978
      5.3853    2.8517    8.0914    9.3604    9.4798    2.0296
      1.3191    2.1758    3.6132    4.6979    3.0358    9.0616
      6.9880    2.0822    4.4537    9.4759    5.5138    0.9362
   
   
      0.0000    0.0000    0.0000    6.2643    8.5739    9.2221
      0.0000    0.0000    0.0000    5.2141    0.0000    0.0000
      5.3853    0.0000    8.0914    9.3604    9.4798    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    9.0616
      6.9880    0.0000    0.0000    9.4759    5.5138    0.0000
   
   
      0.0000    0.0000    0.0000    6.2643    8.5739       NaN
      0.0000    0.0000    0.0000    5.2141    0.0000    0.0000
      5.3853    0.0000    8.0914       NaN       NaN    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      6.9880    0.0000    0.0000       NaN    5.5138    0.0000
   

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

   
      6.5000    6.5000    0.1439    6.5000    3.1278    6.5000
      2.2647    6.5000    9.8997    0.7377    4.8201    2.1237
      6.5000    6.5000    3.1383    6.5000    1.1214    1.6972
      6.5000    3.3367    8.5120    0.2971    6.5000    6.5000
      6.5000    2.3344    8.1580    2.2663    6.5000    1.7466
   
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
   
