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
      0.4378    0.9088    0.2640    0.6119
   
   R1[2] = 0.2639650058317673
   C1 = 
      0.4419
      0.7975
      0.2395
      0.5542
      0.2647
      0.8274
      0.5584
      0.3280
   
   C1[5] = 0.82741849131836

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
      0.9587    0.0059    0.4666    0.7040    0.2852
      0.3752    0.1719    0.5251    0.6934    0.7771
   

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
   
      0.9105    0.5885    0.8467    0.1659    0.1747    0.1427    0.5289    0.5312
      0.0094    0.6895    0.2689    0.2187    0.7344    0.9519    0.4204    0.9441
      0.1548    0.0429    0.8681    0.8162    0.6830    0.3631    0.4560    0.3569
      0.7739    0.3870    0.3689    0.6150    0.7932    0.6203    0.4094    0.1950
      0.9945    0.5649    0.9714    0.3243    0.4970    0.7591    0.9148    0.2690
      0.6706    0.6330    0.1717    0.1082    0.4850    0.4481    0.0626    0.5053
      0.1632    0.2653    0.2050    0.9327    0.8005    0.7936    0.1844    0.5477
      0.9227    0.8044    0.5882    0.2923    0.5170    0.6354    0.6048    0.7208
   
   B = 
   
      0.6535    0.6785    0.5705    0.6879    0.8974    0.0813    0.5251    0.8326
      0.1699    0.8108    0.7397    0.6978    0.6475    0.2785    0.1019    0.2238
      0.8179    0.7107    0.0575    0.8518    0.9315    0.0067    0.4663    0.4752
      0.0132    0.0517    0.1876    0.4618    0.3439    0.7686    0.8665    0.5363
      0.3426    0.2892    0.6795    0.2156    0.0201    0.7958    0.0283    0.2100
      0.0444    0.0056    0.6850    0.3030    0.5511    0.3439    0.0109    0.7353
      0.5048    0.0910    0.8363    0.6795    0.8378    0.8327    0.8606    0.3140
      0.0827    0.9140    0.3106    0.8078    0.3356    0.5570    0.9953    0.8187
   
   C = 
   
      1.7669    2.2904    1.8583    2.7042    2.7475    1.2955    2.0670    2.1238
      0.9303    1.8867    2.3677    2.3126    1.9890    2.1503    1.7227    2.1662
      1.3392    1.3663    1.5281    2.1082    1.9717    1.9046    1.9686    1.8346
      1.4035    1.5812    2.2312    2.1953    2.2665    1.9400    1.7263    2.1470
      2.2327    2.3170    2.8081    3.2314    3.5596    2.0620    2.3909    2.7599
      0.9471    1.7062    1.7268    1.7903    1.6877    1.1888    1.1659    1.7044
      0.7796    1.2731    1.8879    1.8835    1.6217    2.1739    1.7516    2.0507
      1.7949    2.5784    2.7263    3.1292    3.1065    2.0626    2.3534    2.7404
   
   D = 
   
      1.7669    2.2904    1.8583    2.7042    2.7475    1.2955    2.0670    2.1238
      0.9303    1.8867    2.3677    2.3126    1.9890    2.1503    1.7227    2.1662
      1.3392    1.3663    1.5281    2.1082    1.9717    1.9046    1.9686    1.8346
      1.4035    1.5812    2.2312    2.1953    2.2665    1.9400    1.7263    2.1470
      2.2327    2.3170    2.8081    3.2314    3.5596    2.0620    2.3909    2.7599
      0.9471    1.7062    1.7268    1.7903    1.6877    1.1888    1.1659    1.7044
      0.7796    1.2731    1.8879    1.8835    1.6217    2.1739    1.7516    2.0507
      1.7949    2.5784    2.7263    3.1292    3.1065    2.0626    2.3534    2.7404
   


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

   
      0.9684    0.0913    0.6916    0.2583    0.4135    0.5589
      0.9789    0.5778    0.9908    0.3408    0.7452    0.0012
      0.8806    0.1357    0.5383    0.6846    0.6185    0.2495
      0.4392    0.2865    0.6987    0.1068    0.2220    0.1088
      0.5676    0.7866    0.1355    0.0409    0.6617    0.6000
   
   
      0.9684
      0.9789
      0.8806
      0.5676
      0.5778
      0.7866
      0.6916
      0.9908
      0.5383
      0.6987
      0.6846
      0.7452
      0.6185
      0.6617
      0.5589
      0.6000
   

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

   
      7.7239    6.2872    9.7795    7.0667    7.3286    6.9224
      5.4771    7.1602    0.3733    2.4322    6.8495    4.5416
      2.6837    3.5438    9.6436    0.5815    6.0194    5.0919
      8.1574    7.1316    8.7148    9.8849    6.9955    3.6724
      1.8461    0.7131    6.0153    6.2643    5.2500    5.3510
   
   
      7.7239    6.2872    9.7795    7.0667    7.3286    6.9224
      5.4771    7.1602    0.0000    0.0000    6.8495    0.0000
      0.0000    0.0000    9.6436    0.0000    6.0194    5.0919
      8.1574    7.1316    8.7148    9.8849    6.9955    0.0000
      0.0000    0.0000    6.0153    6.2643    5.2500    5.3510
   
   
      7.7239    6.2872       NaN    7.0667    7.3286    6.9224
      5.4771    7.1602    0.0000    0.0000    6.8495    0.0000
      0.0000    0.0000       NaN    0.0000    6.0194    5.0919
      8.1574    7.1316    8.7148       NaN    6.9955    0.0000
      0.0000    0.0000    6.0153    6.2643    5.2500    5.3510
   

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

   
      4.2053    2.2466    2.7021    9.4809    6.5000    3.7463
      0.6722    8.3637    2.1304    0.9839    4.6760    0.1144
      6.5000    8.7979    6.5000    0.8020    4.6839    4.7041
      2.2399    9.9542    1.7739    9.1639    9.3178    0.3575
      9.2191    4.6032    6.5000    8.5656    4.1643    1.6010
   
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
   
