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
      0.9908    0.4415    0.9932    0.5392
   
   R1[2] = 0.9931992438966084
   C1 = 
      0.1344
      0.3359
      0.9513
      0.1294
      0.2383
      0.8787
      0.9011
      0.2254
   
   C1[5] = 0.8786592527462038

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
      0.8010    0.5512    0.2249    0.7113    0.1765
      0.3551    0.1592    0.7716    0.5267    0.1877
   

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
   
      0.9048    0.6226    0.6482    0.2450    0.2170    0.9072    0.6883    0.1474
      0.6914    0.0180    0.1805    0.4761    0.7023    0.1046    0.6808    0.2360
      0.6097    0.3177    0.5981    0.0502    0.1440    0.6723    0.2868    0.4511
      0.8828    0.3617    0.0298    0.3680    0.0212    0.9550    0.4496    0.7958
      0.8212    0.0709    0.1356    0.9950    0.5496    0.7560    0.5215    0.5217
      0.0322    0.5903    0.2736    0.9502    0.2954    0.3986    0.6548    0.3899
      0.0545    0.8583    0.6111    0.7713    0.2417    0.9338    0.3954    0.3944
      0.7521    0.4231    0.3669    0.7378    0.7161    0.2288    0.0021    0.7421
   
   B = 
   
      0.4410    0.0763    0.1398    0.9431    0.6066    0.4735    0.7250    0.8601
      0.1257    0.4572    0.3515    0.4726    0.8154    0.3521    0.7887    0.8418
      0.8199    0.7391    0.4217    0.4110    0.6492    0.3892    0.1576    0.0742
      0.2969    0.5712    0.7332    0.4760    0.3473    0.7464    0.1032    0.7087
      0.2242    0.2639    0.2312    0.1141    0.8122    0.1285    0.1558    0.7261
      0.7557    0.7290    0.3073    0.7787    0.5341    0.1488    0.9212    0.7708
      0.7168    0.0666    0.4191    0.3753    0.1323    0.5933    0.5745    0.0388
      0.7354    0.5240    0.8068    0.5542    0.9028    0.0453    0.1317    0.6354
   
   C = 
   
      2.4174    1.8144    1.5346    2.6018    2.4473    1.6607    2.5588    2.5012
      1.4945    0.8969    1.1984    1.5092    1.6460    1.2798    1.2210    1.7275
      1.8917    1.4461    1.2100    1.8924    1.9559    0.9799    1.6579    1.7923
      2.2023    1.6136    1.6617    2.5467    2.2828    1.2792    2.2141    2.6013
      2.2295    1.7678    1.9252    2.4735    2.3796    1.7254    1.9256    2.8146
      1.7183    1.6337    1.8037    1.6800    1.8999    1.5422    1.4708    2.0131
      2.1953    2.2663    1.9593    2.1971    2.5010    1.5641    2.0694    2.5226
      1.7854    1.6882    1.7851    2.0832    2.6698    1.3596    1.4342    2.7211
   
   D = 
   
      2.4174    1.8144    1.5346    2.6018    2.4473    1.6607    2.5588    2.5012
      1.4945    0.8969    1.1984    1.5092    1.6460    1.2798    1.2210    1.7275
      1.8917    1.4461    1.2100    1.8924    1.9559    0.9799    1.6579    1.7923
      2.2023    1.6136    1.6617    2.5467    2.2828    1.2792    2.2141    2.6013
      2.2295    1.7678    1.9252    2.4735    2.3796    1.7254    1.9256    2.8146
      1.7183    1.6337    1.8037    1.6800    1.8999    1.5422    1.4708    2.0131
      2.1953    2.2663    1.9593    2.1971    2.5010    1.5641    2.0694    2.5226
      1.7854    1.6882    1.7851    2.0832    2.6698    1.3596    1.4342    2.7211
   


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

   
      0.7280    0.3291    0.1450    0.4668    0.1637    0.0719
      0.6590    0.8472    0.8267    0.2229    0.8271    0.0164
      0.9800    0.6241    0.1355    0.2922    0.4495    0.9237
      0.9242    0.4474    0.7265    0.4097    0.7097    0.7294
      0.5765    0.4848    0.3145    0.3636    0.9831    0.8491
   
   
      0.7280
      0.6590
      0.9800
      0.9242
      0.5765
      0.8472
      0.6241
      0.8267
      0.7265
      0.8271
      0.7097
      0.9831
      0.9237
      0.7294
      0.8491
   

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

   
      7.8503    2.7730    6.0515    8.8312    4.0019    5.9530
      3.9691    6.7064    2.0603    8.6282    4.2244    5.8329
      2.6519    2.6706    3.3025    0.3450    2.1449    0.0153
      7.0220    7.1009    5.6026    1.4260    7.0767    9.1760
      4.1367    1.3808    9.7742    8.9569    8.0296    3.2683
   
   
      7.8503    0.0000    6.0515    8.8312    0.0000    5.9530
      0.0000    6.7064    0.0000    8.6282    0.0000    5.8329
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.0220    7.1009    5.6026    0.0000    7.0767    9.1760
      0.0000    0.0000    9.7742    8.9569    8.0296    0.0000
   
   
      7.8503    0.0000    6.0515    8.8312    0.0000    5.9530
      0.0000    6.7064    0.0000    8.6282    0.0000    5.8329
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.0220    7.1009    5.6026    0.0000    7.0767       NaN
      0.0000    0.0000       NaN    8.9569    8.0296    0.0000
   

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

   
      0.3185    6.5000    9.1941    1.6471    0.0639    6.5000
      1.7483    0.0132    2.7742    3.3717    0.6476    6.5000
      9.6134    3.3534    3.3553    6.5000    0.9422    0.3445
      4.8565    1.4693    6.5000    4.9495    2.6588    4.7319
      6.5000    6.5000    6.5000    3.8820    0.2679    3.5534
   
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
   
