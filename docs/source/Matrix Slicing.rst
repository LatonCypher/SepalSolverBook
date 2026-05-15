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
      0.6118    0.7097    0.0248    0.8196
   
   R1[2] = 0.02475670969760213
   C1 = 
      0.8251
      0.2328
      0.8743
      0.3505
      0.6923
      0.9590
      0.5918
      0.3820
   
   C1[5] = 0.9589815608583715

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
      0.5401    0.4368    0.2590    0.5556    0.2825
      0.8901    0.2738    0.3083    0.7220    0.0253
   

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
   
      0.1853    0.5427    0.9962    0.5833    0.0081    0.1629    0.8666    0.1123
      0.4739    0.8417    0.9361    0.2607    0.7720    0.3240    0.1358    0.1383
      0.2173    0.4214    0.9277    0.1849    0.5546    0.4434    0.7701    0.9067
      0.3845    0.7706    0.4214    0.9246    0.0776    0.2659    0.3526    0.7324
      0.8585    0.0945    0.9777    0.8566    0.4241    0.4688    0.2961    0.3888
      0.6202    0.6604    0.1457    0.0896    0.0911    0.9545    0.4963    0.8278
      0.3060    0.2229    0.4424    0.9511    0.1722    0.1832    0.8557    0.8460
      0.2560    0.9819    0.8604    0.9090    0.0749    0.4370    0.3295    0.4992
   
   B = 
   
      0.6416    0.7071    0.2784    0.8613    0.6239    0.2025    0.6478    0.3993
      0.3355    0.6756    0.7444    0.0776    0.3498    0.6358    0.6181    0.9742
      0.4135    0.8162    0.6957    0.7027    0.5806    0.9969    0.1258    0.4714
      0.9043    0.2890    0.1454    0.3163    0.7784    0.7906    0.5273    0.4835
      0.4324    0.2099    0.5249    0.9638    0.6069    0.5535    0.8831    0.3576
      0.8510    0.5290    0.4033    0.6652    0.4655    0.5530    0.6962    0.3018
      0.7081    0.2181    0.4518    0.9256    0.6437    0.6219    0.5354    0.3684
      0.1566    0.9687    0.4862    0.8010    0.9589    0.0115    0.2241    0.0303
   
   C = 
   
      2.0138    1.8649    1.7495    2.0944    2.0841    2.4717    1.4981    1.7290
      1.9366    2.2401    2.1121    2.4097    2.1758    2.4629    2.0933    2.0046
      2.1361    2.6462    2.3052    3.1988    2.8737    2.4245    2.0293    1.6673
      2.1398    2.3469    1.7716    2.1441    2.5739    2.1365    1.8724    1.8100
      2.6143    2.4946    1.8486    3.0108    2.8421    2.5685    2.1357    1.7240
      2.0935    2.4637    1.8381    2.5614    2.3852    1.6580    2.0717    1.5316
      2.2829    2.1421    1.6593    2.6501    2.8179    2.1351    1.8205    1.4655
      2.3873    2.6116    2.1401    2.2565    2.6499    2.7463    2.0189    2.1991
   
   D = 
   
      2.0138    1.8649    1.7495    2.0944    2.0841    2.4717    1.4981    1.7290
      1.9366    2.2401    2.1121    2.4097    2.1758    2.4629    2.0933    2.0046
      2.1361    2.6462    2.3052    3.1988    2.8737    2.4245    2.0293    1.6673
      2.1398    2.3469    1.7716    2.1441    2.5739    2.1365    1.8724    1.8100
      2.6143    2.4946    1.8486    3.0108    2.8421    2.5685    2.1357    1.7240
      2.0935    2.4637    1.8381    2.5614    2.3852    1.6580    2.0717    1.5316
      2.2829    2.1421    1.6593    2.6501    2.8179    2.1351    1.8205    1.4655
      2.3873    2.6116    2.1401    2.2565    2.6499    2.7463    2.0189    2.1991
   


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

   
      0.8670    0.5715    0.5935    0.4840    0.5365    0.4902
      0.8448    0.8176    0.7562    0.2602    0.0745    0.4653
      0.3843    0.9303    0.9849    0.0219    0.5107    0.2712
      0.8180    0.9785    0.5319    0.8034    0.4131    0.9758
      0.8171    0.6286    0.1281    0.3446    0.4507    0.8972
   
   
      0.8670
      0.8448
      0.8180
      0.8171
      0.5715
      0.8176
      0.9303
      0.9785
      0.6286
      0.5935
      0.7562
      0.9849
      0.5319
      0.8034
      0.5365
      0.5107
      0.9758
      0.8972
   

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

   
      2.8226    6.4943    2.5198    9.4534    7.7237    3.0802
      4.4495    5.9948    2.1453    1.8352    3.3766    0.9407
      0.9772    9.7101    6.5199    5.2796    4.0474    9.4177
      5.2559    7.3092    9.0023    2.2758    0.8812    4.3950
      6.1608    0.3968    3.0429    5.0102    2.0646    4.6802
   
   
      0.0000    6.4943    0.0000    9.4534    7.7237    0.0000
      0.0000    5.9948    0.0000    0.0000    0.0000    0.0000
      0.0000    9.7101    6.5199    5.2796    0.0000    9.4177
      5.2559    7.3092    9.0023    0.0000    0.0000    0.0000
      6.1608    0.0000    0.0000    5.0102    0.0000    0.0000
   
   
      0.0000    6.4943    0.0000       NaN    7.7237    0.0000
      0.0000    5.9948    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    6.5199    5.2796    0.0000       NaN
      5.2559    7.3092       NaN    0.0000    0.0000    0.0000
      6.1608    0.0000    0.0000    5.0102    0.0000    0.0000
   

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

   
      3.6966    2.8180    3.7833    6.5000    6.5000    1.2364
      6.5000    4.9279    0.7759    0.7926    8.9680    3.6339
      4.8210    2.9823    3.6444    8.5144    6.5000    2.2826
      6.5000    6.5000    1.4013    6.5000    3.6010    1.5210
      3.5909    6.5000    9.2435    4.7790    8.3625    3.6372
   
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
   
