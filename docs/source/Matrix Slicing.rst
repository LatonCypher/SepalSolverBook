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
      0.0781    0.4421    0.6622    0.2131
   
   R1[2] = 0.6621757056074802
   C1 = 
      0.9000
      0.4412
      0.3867
      0.9693
      0.8567
      0.1720
      0.4521
      0.7632
   
   C1[5] = 0.17198960605055869

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
      0.1681    0.2362    0.4213    0.0334    0.0415
      0.1672    0.9050    0.3985    0.5656    0.0791
   

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
   
      0.2961    0.6422    0.7846    0.7937    0.1169    0.0153    0.5137    0.2133
      0.0132    0.2429    0.6374    0.2497    0.4198    0.7891    0.2209    0.4063
      0.7850    0.1736    0.4317    0.7864    0.7895    0.1303    0.4985    0.2787
      0.2628    0.7565    0.5727    0.0995    0.5522    0.0683    0.7295    0.8729
      0.1978    0.1597    0.6431    0.3011    0.0566    0.7514    0.4572    0.5470
      0.0773    0.7739    0.9342    0.2752    0.8800    0.3773    0.1298    0.5061
      0.8793    0.6438    0.2954    0.5815    0.3278    0.0020    0.7136    0.2644
      0.6042    0.1096    0.7024    0.5019    0.8319    0.2374    0.5928    0.4926
   
   B = 
   
      0.4039    0.1124    0.7885    0.9340    0.6360    0.8977    0.9323    0.7988
      0.3845    0.6842    0.6032    0.7774    0.7648    0.6065    0.9426    0.1509
      0.1094    0.8045    0.4869    0.2181    0.6748    0.9985    0.6166    0.2377
      0.3440    0.5141    0.7609    0.9946    0.8412    0.8446    0.1111    0.4442
      0.6518    0.4445    0.6299    0.8539    0.3105    0.7623    0.6322    0.5187
      0.4834    0.2561    0.5254    0.8462    0.7529    0.1331    0.0181    0.5784
      0.1613    0.9893    0.5523    0.2509    0.5925    0.0037    0.3655    0.3622
      0.9414    0.9501    0.1482    0.3587    0.0028    0.7674    0.0578    0.6141
   
   C = 
   
      1.0925    2.2785    2.0037    2.0544    2.2293    2.3657    1.7276    1.2590
      1.3275    1.8020    1.5185    1.8159    1.6908    1.7440    1.0459    1.3133
      1.6219    2.1008    2.4147    2.7539    2.2243    2.7400    1.9488    1.9418
      1.8263    2.8730    1.9341    2.0829    1.8735    2.4532    1.9897    1.6307
      1.3040    1.9932    1.5585    1.7437    1.7909    1.7354    1.0130    1.4342
      1.7791    2.5283    2.0914    2.4363    2.1387    2.8140    2.0481    1.5555
      1.4137    2.1793    2.3089    2.5200    2.2671    2.4217    2.1568    1.7201
      1.7520    2.4511    2.3156    2.5386    2.1540    2.7800    1.9309    1.9751
   
   D = 
   
      1.0925    2.2785    2.0037    2.0544    2.2293    2.3657    1.7276    1.2590
      1.3275    1.8020    1.5185    1.8159    1.6908    1.7440    1.0459    1.3133
      1.6219    2.1008    2.4147    2.7539    2.2243    2.7400    1.9488    1.9418
      1.8263    2.8730    1.9341    2.0829    1.8735    2.4532    1.9897    1.6307
      1.3040    1.9932    1.5585    1.7437    1.7909    1.7354    1.0130    1.4342
      1.7791    2.5283    2.0914    2.4363    2.1387    2.8140    2.0481    1.5555
      1.4137    2.1793    2.3089    2.5200    2.2671    2.4217    2.1568    1.7201
      1.7520    2.4511    2.3156    2.5386    2.1540    2.7800    1.9309    1.9751
   


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

   
      0.7519    0.2723    0.6543    0.3500    0.9568    0.2591
      0.7276    0.4046    0.9880    0.6115    0.7713    0.2393
      0.9800    0.9176    0.2353    0.5325    0.3711    0.1331
      0.4943    0.3314    0.0128    0.7051    0.5433    0.3637
      0.6974    0.0874    0.1730    0.9125    0.3801    0.0538
   
   
      0.7519
      0.7276
      0.9800
      0.6974
      0.9176
      0.6543
      0.9880
      0.6115
      0.5325
      0.7051
      0.9125
      0.9568
      0.7713
      0.5433
   

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

   
      7.8469    0.2624    0.8971    3.1993    8.4557    2.7920
      4.7671    0.1921    6.6329    2.0985    8.7362    4.3611
      9.1555    1.3394    7.0958    7.7360    4.8002    2.5681
      4.9886    9.6668    7.9042    0.3443    2.2223    5.0507
      2.3961    8.6771    6.9913    8.8631    6.9461    7.9013
   
   
      7.8469    0.0000    0.0000    0.0000    8.4557    0.0000
      0.0000    0.0000    6.6329    0.0000    8.7362    0.0000
      9.1555    0.0000    7.0958    7.7360    0.0000    0.0000
      0.0000    9.6668    7.9042    0.0000    0.0000    5.0507
      0.0000    8.6771    6.9913    8.8631    6.9461    7.9013
   
   
      7.8469    0.0000    0.0000    0.0000    8.4557    0.0000
      0.0000    0.0000    6.6329    0.0000    8.7362    0.0000
         NaN    0.0000    7.0958    7.7360    0.0000    0.0000
      0.0000       NaN    7.9042    0.0000    0.0000    5.0507
      0.0000    8.6771    6.9913    8.8631    6.9461    7.9013
   

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

   
      2.6174    8.6942    3.9264    1.7313    9.4548    3.8380
      6.5000    9.7995    6.5000    1.5246    2.3884    1.2075
      6.5000    2.6417    1.9314    6.5000    6.5000    9.1253
      6.5000    1.5051    6.5000    6.5000    6.5000    6.5000
      8.4138    8.3942    9.2549    6.5000    6.5000    8.7033
   
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
   
