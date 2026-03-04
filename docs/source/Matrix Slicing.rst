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
      0.0353    0.4327    0.9029    0.1142
   
   R1[2] = 0.9028751950347859
   C1 = 
      0.0332
      0.4111
      0.6012
      0.9191
      0.0294
      0.1803
      0.7095
      0.1611
   
   C1[5] = 0.18025301918679282

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
      0.5865    0.5777    0.8788    0.3069    0.1081
      0.8487    0.6888    0.6468    0.1979    0.7825
   

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
   
      0.0071    0.7687    0.5271    0.6825    0.0595    0.8948    0.3636    0.8315
      0.0163    0.9692    0.2395    0.9735    0.1306    0.9740    0.1519    0.6492
      0.0812    0.3494    0.8207    0.2682    0.3868    0.5695    0.9180    0.9641
      0.0731    0.1541    0.7895    0.1770    0.6137    0.0516    0.3169    0.9945
      0.3849    0.6450    0.4932    0.4466    0.2446    0.1491    0.7452    0.5595
      0.5954    0.3911    0.9798    0.8093    0.0887    0.4906    0.9724    0.1383
      0.0687    0.4456    0.4850    0.3207    0.6156    0.1416    0.4802    0.4707
      0.7314    0.6313    0.1875    0.3207    0.9536    0.7084    0.5564    0.9247
   
   B = 
   
      0.1836    0.8892    0.6261    0.0099    0.0257    0.0062    0.3795    0.4130
      0.0085    0.0214    0.4517    0.6914    0.1307    0.3449    0.6487    0.3902
      0.9884    0.3539    0.4823    0.2790    0.8664    0.2227    0.4018    0.1383
      0.3814    0.0515    0.0050    0.1669    0.3421    0.0317    0.7956    0.0887
      0.3054    0.7096    0.7590    0.7621    0.5103    0.4297    0.5578    0.1980
      0.1615    0.7292    0.0770    0.8866    0.1390    0.9549    0.4786    0.3433
      0.7830    0.4732    0.0487    0.4195    0.8333    0.2612    0.9096    0.3565
      0.6123    0.0065    0.6026    0.3402    0.8421    0.8131    0.5344    0.8711
   
   C = 
   
      1.7456    1.1167    1.2421    2.0666    1.9488    2.0553    2.4926    1.6092
      1.3328    1.0492    1.1411    2.1472    1.5429    1.9723    2.5296    1.4842
      2.4505    1.5143    1.5688    2.0289    2.7039    2.0460    2.6391    1.7462
      1.9154    0.9863    1.5815    1.3418    2.1885    1.4394    1.7725    1.3337
      1.7588    1.1921    1.3429    1.4834    1.9120    1.2459    2.3025    1.3711
      2.3421    1.8080    1.2618    1.6423    2.2325    1.2555    2.7599    1.2588
      1.4932    1.0291    1.2649    1.4539    1.7203    1.1802    1.8646    1.0495
      1.8547    2.2093    2.1976    2.4523    2.2008    2.2576    2.8887    2.0386
   
   D = 
   
      1.7456    1.1167    1.2421    2.0666    1.9488    2.0553    2.4926    1.6092
      1.3328    1.0492    1.1411    2.1472    1.5429    1.9723    2.5296    1.4842
      2.4505    1.5143    1.5688    2.0289    2.7039    2.0460    2.6391    1.7462
      1.9154    0.9863    1.5815    1.3418    2.1885    1.4394    1.7725    1.3337
      1.7588    1.1921    1.3429    1.4834    1.9120    1.2459    2.3025    1.3711
      2.3421    1.8080    1.2618    1.6423    2.2325    1.2555    2.7599    1.2588
      1.4932    1.0291    1.2649    1.4539    1.7203    1.1802    1.8646    1.0495
      1.8547    2.2093    2.1976    2.4523    2.2008    2.2576    2.8887    2.0386
   


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

   
      0.1339    0.4060    0.3224    0.7861    0.4311    0.2097
      0.3081    0.4297    0.2584    0.2635    0.0740    0.7028
      0.7120    0.6502    0.7621    0.3239    0.6031    0.6378
      0.6176    0.2970    0.5384    0.3785    0.1446    0.6109
      0.3327    0.6719    0.7409    0.5400    0.2746    0.8271
   
   
      0.7120
      0.6176
      0.6502
      0.6719
      0.7621
      0.5384
      0.7409
      0.7861
      0.5400
      0.6031
      0.7028
      0.6378
      0.6109
      0.8271
   

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

   
      0.3848    9.4027    5.2179    8.5036    6.4657    5.3651
      1.6628    2.7000    1.5869    5.7854    6.1071    0.4372
      3.7107    9.3631    9.0652    1.0283    3.8620    3.4819
      7.6388    0.0586    8.4488    5.0267    7.3165    0.2295
      2.3817    4.1630    5.0234    4.8964    6.8000    0.9445
   
   
      0.0000    9.4027    5.2179    8.5036    6.4657    5.3651
      0.0000    0.0000    0.0000    5.7854    6.1071    0.0000
      0.0000    9.3631    9.0652    0.0000    0.0000    0.0000
      7.6388    0.0000    8.4488    5.0267    7.3165    0.0000
      0.0000    0.0000    5.0234    0.0000    6.8000    0.0000
   
   
      0.0000       NaN    5.2179    8.5036    6.4657    5.3651
      0.0000    0.0000    0.0000    5.7854    6.1071    0.0000
      0.0000       NaN       NaN    0.0000    0.0000    0.0000
      7.6388    0.0000    8.4488    5.0267    7.3165    0.0000
      0.0000    0.0000    5.0234    0.0000    6.8000    0.0000
   

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

   
      0.7461    3.4271    6.5000    6.5000    6.5000    3.0158
      6.5000    8.3315    0.6192    6.5000    2.9549    1.7850
      6.5000    3.7248    6.5000    6.5000    6.5000    4.2849
      9.4203    6.5000    6.5000    1.6095    4.8953    9.9919
      2.4902    0.7650    0.6919    6.5000    1.0709    6.5000
   
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
   
