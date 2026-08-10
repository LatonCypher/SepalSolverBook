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
      0.0130    0.8171    0.4087    0.6143
   
   R1[2] = 0.4086768787012076
   C1 = 
      0.8411
      0.0473
      0.9499
      0.4845
      0.4582
      0.2448
      0.2110
      0.1531
   
   C1[5] = 0.24477283318087517

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
      0.3687    0.5627    0.0168    0.8965    0.6085
      0.6846    0.3264    0.1569    0.6620    0.3086
   

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
   
      0.2305    0.0427    0.4555    0.1312    0.6534    0.5073    0.0392    0.5263
      0.4161    0.6223    0.4349    0.6035    0.6136    0.2893    0.0752    0.2891
      0.4996    0.6156    0.9618    0.1840    0.8915    0.4688    0.1889    0.1968
      0.8375    0.3125    0.7443    0.5643    0.0743    0.3939    0.5519    0.8169
      0.0068    0.2158    0.3706    0.8214    0.7098    0.2218    0.9190    0.2376
      0.1314    0.6265    0.1249    0.3295    0.6320    0.0993    0.9796    0.9955
      0.2185    0.3708    0.5111    0.3708    0.7879    0.4762    0.5059    0.8559
      0.5299    0.7089    0.7247    0.2715    0.0301    0.0467    0.2097    0.8169
   
   B = 
   
      0.5286    0.2865    0.0427    0.5546    0.4767    0.4771    0.0078    0.6299
      0.5590    0.2323    0.2707    0.4160    0.4258    0.4977    0.6275    0.5107
      0.5611    0.1762    0.7496    0.5858    0.0534    0.3636    0.9638    0.6583
      0.8373    0.9546    0.2589    0.7616    0.4785    0.3535    0.7558    0.6143
      0.9179    0.4906    0.8054    0.9425    0.8968    0.8334    0.4469    0.1913
      0.1160    0.3494    0.8630    0.7520    0.4460    0.8718    0.8147    0.0160
      0.5959    0.4856    0.3672    0.4577    0.0535    0.8063    0.7365    0.7525
      0.2980    0.4562    0.8370    0.8019    0.4182    0.1317    0.1656    0.7887
   
   C = 
   
      1.3499    1.0384    1.8157    1.9497    1.2496    1.4310    1.3880    1.1252
      2.0449    1.4870    1.6819    2.2662    1.5796    1.7421    1.8822    1.6436
      2.3459    1.4140    2.3132    2.6739    1.7407    2.2895    2.4083    1.8507
      2.1937    1.7971    2.1105    2.7342    1.4556    1.9831    2.2424    2.6040
      2.3157    1.8819    1.8487    2.3833    1.3921    2.0930    2.3277    1.8814
      2.2377    1.7942    2.1417    2.5747    1.5736    2.0706    2.0132    2.3321
      2.2550    1.7818    2.5364    2.8759    1.7708    2.1981    2.2616    2.1053
      1.7118    1.2090    1.6534    2.0347    1.1238    1.3077    1.6939    2.1484
   
   D = 
   
      1.3499    1.0384    1.8157    1.9497    1.2496    1.4310    1.3880    1.1252
      2.0449    1.4870    1.6819    2.2662    1.5796    1.7421    1.8822    1.6436
      2.3459    1.4140    2.3132    2.6739    1.7407    2.2895    2.4083    1.8507
      2.1937    1.7971    2.1105    2.7342    1.4556    1.9831    2.2424    2.6040
      2.3157    1.8819    1.8487    2.3833    1.3921    2.0930    2.3277    1.8814
      2.2377    1.7942    2.1417    2.5747    1.5736    2.0706    2.0132    2.3321
      2.2550    1.7818    2.5364    2.8759    1.7708    2.1981    2.2616    2.1053
      1.7118    1.2090    1.6534    2.0347    1.1238    1.3077    1.6939    2.1484
   


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

   
      0.1087    0.2738    0.4875    0.6211    0.6645    0.4601
      0.5091    0.5988    0.4470    0.1427    0.3693    0.0507
      0.9145    0.0505    0.8569    0.0700    0.4248    0.8600
      0.9602    0.9803    0.8360    0.2484    0.8368    0.9532
      0.3548    0.7126    0.2096    0.4912    0.7306    0.4315
   
   
      0.5091
      0.9145
      0.9602
      0.5988
      0.9803
      0.7126
      0.8569
      0.8360
      0.6211
      0.6645
      0.8368
      0.7306
      0.8600
      0.9532
   

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

   
      7.7330    7.6188    6.0032    1.2875    5.7430    6.7202
      3.7936    1.7734    5.4444    0.7077    0.6505    8.3071
      3.3401    5.1239    2.9650    0.3731    8.4763    2.1164
      3.1352    8.5281    2.2193    4.9105    3.3358    9.9080
      7.0642    2.7629    0.1742    6.3888    1.6723    6.5648
   
   
      7.7330    7.6188    6.0032    0.0000    5.7430    6.7202
      0.0000    0.0000    5.4444    0.0000    0.0000    8.3071
      0.0000    5.1239    0.0000    0.0000    8.4763    0.0000
      0.0000    8.5281    0.0000    0.0000    0.0000    9.9080
      7.0642    0.0000    0.0000    6.3888    0.0000    6.5648
   
   
      7.7330    7.6188    6.0032    0.0000    5.7430    6.7202
      0.0000    0.0000    5.4444    0.0000    0.0000    8.3071
      0.0000    5.1239    0.0000    0.0000    8.4763    0.0000
      0.0000    8.5281    0.0000    0.0000    0.0000       NaN
      7.0642    0.0000    0.0000    6.3888    0.0000    6.5648
   

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

   
      6.5000    6.5000    4.0875    1.9779    6.5000    2.3647
      0.0685    6.5000    6.5000    8.2479    0.3592    9.0723
      9.3644    4.5186    6.5000    2.4814    4.8540    2.9338
      2.9608    6.5000    3.4716    9.7716    6.5000    0.9029
      3.7118    4.5174    9.7399    0.5945    6.5000    6.5000
   
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
   
