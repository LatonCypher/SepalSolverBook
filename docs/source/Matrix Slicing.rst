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
      0.9921    0.8646    0.9330    0.5134
   
   R1[2] = 0.9329926565629473
   C1 = 
      0.2015
      0.4010
      0.6680
      0.8154
      0.1196
      0.4160
      0.1372
      0.7441
   
   C1[5] = 0.41600318861587404

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
      0.6319    0.0588    0.4007    0.2549    0.5849
      0.4720    0.2528    0.9976    0.9621    0.5849
   

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
   
      0.2999    0.4623    0.6371    0.8277    0.9811    0.7375    0.9889    0.2578
      0.2545    0.2119    0.5324    0.2807    0.3169    0.0048    0.1023    0.9236
      0.2404    0.6046    0.6318    0.9250    0.0486    0.5603    0.4169    0.4693
      0.3335    0.3174    0.8771    0.1471    0.6122    0.7746    0.9607    0.9503
      0.2936    0.6378    0.6707    0.3496    0.0261    0.8495    0.0168    0.0027
      0.1663    0.5978    0.7096    0.2277    0.8161    0.4145    0.2610    0.5695
      0.4341    0.2667    0.2321    0.6557    0.7106    0.0466    0.9693    0.4565
      0.5066    0.5268    0.0450    0.1962    0.5443    0.0144    0.8764    0.1677
   
   B = 
   
      0.9987    0.1613    0.2356    0.5212    0.8597    0.8112    0.8537    0.7460
      0.9127    0.2830    0.7351    0.7679    0.1286    0.2884    0.9313    0.9104
      0.3034    0.6486    0.5452    0.7723    0.6117    0.2913    0.2767    0.6281
      0.0508    0.9247    0.2402    0.1676    0.0680    0.8657    0.4765    0.5049
      0.0664    0.6151    0.4358    0.9708    0.8436    0.8621    0.9211    0.5982
      0.6016    0.5574    0.8921    0.8135    0.5834    0.3233    0.6820    0.6903
      0.0002    0.0905    0.0494    0.2352    0.7640    0.1940    0.0638    0.5646
      0.5704    0.0177    0.1826    0.0967    0.5656    0.1391    0.3736    0.7826
   
   C = 
   
      1.6130    2.4664    2.1381    2.9521    2.9226    2.5907    2.8234    3.3188
      1.1742    0.9291    0.8895    1.1786    1.4615    1.0888    1.3425    1.8324
      1.6387    1.8632    1.6950    1.8789    1.6856    1.7234    2.0127    2.6122
      1.9453    1.7607    2.0042    2.6620    3.1140    1.8417    2.4017    3.3501
      1.6110    1.4772    1.7583    1.9401    1.3005    1.2209    1.8023    2.0112
      1.5670    1.6335    1.7625    2.3779    2.1212    1.6785    2.2673    2.5963
      1.1165    1.4612    1.0648    1.7200    2.2195    1.9435    1.9143    2.4052
      1.1511    0.8666    0.9023    1.4988    1.7762    1.4131    1.6589    1.9465
   
   D = 
   
      1.6130    2.4664    2.1381    2.9521    2.9226    2.5907    2.8234    3.3188
      1.1742    0.9291    0.8895    1.1786    1.4615    1.0888    1.3425    1.8324
      1.6387    1.8632    1.6950    1.8789    1.6856    1.7234    2.0127    2.6122
      1.9453    1.7607    2.0042    2.6620    3.1140    1.8417    2.4017    3.3501
      1.6110    1.4772    1.7583    1.9401    1.3005    1.2209    1.8023    2.0112
      1.5670    1.6335    1.7625    2.3779    2.1212    1.6785    2.2673    2.5963
      1.1165    1.4612    1.0648    1.7200    2.2195    1.9435    1.9143    2.4052
      1.1511    0.8666    0.9023    1.4988    1.7762    1.4131    1.6589    1.9465
   


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

   
      0.4294    0.6355    0.4183    0.2150    0.4603    0.4302
      0.3159    0.9352    0.5161    0.1451    0.2521    0.9275
      0.0566    0.9786    0.7895    0.9370    0.7090    0.6389
      0.7258    0.0520    0.2945    0.6536    0.9739    0.3500
      0.5460    0.2162    0.3555    0.4854    0.7790    0.1340
   
   
      0.7258
      0.5460
      0.6355
      0.9352
      0.9786
      0.5161
      0.7895
      0.9370
      0.6536
      0.7090
      0.9739
      0.7790
      0.9275
      0.6389
   

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

   
      6.5286    8.3958    3.1261    6.3305    4.9638    3.6637
      3.9486    7.2475    0.3196    5.6935    8.8629    9.6927
      2.5280    6.6166    6.5961    1.7636    0.9767    3.8136
      1.3241    7.4280    4.5947    3.9296    3.4149    4.9914
      4.3277    9.2826    8.8316    5.5141    6.8124    8.2164
   
   
      6.5286    8.3958    0.0000    6.3305    0.0000    0.0000
      0.0000    7.2475    0.0000    5.6935    8.8629    9.6927
      0.0000    6.6166    6.5961    0.0000    0.0000    0.0000
      0.0000    7.4280    0.0000    0.0000    0.0000    0.0000
      0.0000    9.2826    8.8316    5.5141    6.8124    8.2164
   
   
      6.5286    8.3958    0.0000    6.3305    0.0000    0.0000
      0.0000    7.2475    0.0000    5.6935    8.8629       NaN
      0.0000    6.6166    6.5961    0.0000    0.0000    0.0000
      0.0000    7.4280    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    8.8316    5.5141    6.8124    8.2164
   

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

   
      6.5000    6.5000    8.0293    6.5000    4.9144    6.5000
      0.8349    6.5000    4.2575    4.7793    4.0307    6.5000
      4.4366    3.8535    6.5000    6.5000    6.5000    8.9481
      0.2262    6.5000    6.5000    2.5246    4.0852    6.5000
      3.6242    9.3379    8.1178    6.5000    6.5000    6.5000
   
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
   
