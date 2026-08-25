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
      0.4203    0.4699    0.5968    0.3228
   
   R1[2] = 0.5967743104976662
   C1 = 
      0.0176
      0.9551
      0.0655
      0.5024
      0.8318
      0.3870
      0.3450
      0.1736
   
   C1[5] = 0.3870495578434986

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
      0.2480    0.4548    0.6299    0.2096    0.6053
      0.5376    0.6945    0.1286    0.7429    0.0550
   

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
   
      0.5511    0.5105    0.7032    0.0720    0.6711    0.6033    0.4134    0.0618
      0.3592    0.7943    0.1941    0.6661    0.4989    0.1500    0.7551    0.0083
      0.6023    0.5572    0.5184    0.1291    0.2563    0.4509    0.8537    0.0485
      0.8978    0.6060    0.3193    0.9656    0.8662    0.8891    0.4646    0.0598
      0.5348    0.0240    0.4061    0.1439    0.0720    0.7726    0.2886    0.1177
      0.6994    0.8922    0.9609    0.1518    0.5834    0.7943    0.8615    0.8030
      0.9496    0.7526    0.1482    0.1161    0.8724    0.5093    0.5698    0.0252
      0.6855    0.0374    0.6828    0.1155    0.2203    0.6266    0.6854    0.3909
   
   B = 
   
      0.9375    0.1479    0.3168    0.8184    0.2964    0.6736    0.0298    0.4714
      0.4171    0.2084    0.8607    0.6139    0.9791    0.3532    0.7932    0.5707
      0.7134    0.1574    0.5426    0.0492    0.1772    0.3528    0.4393    0.2316
      0.2356    0.2000    0.5726    0.5371    0.4870    0.4492    0.9425    0.3695
      0.1324    0.3352    0.7640    0.6850    0.9999    0.7329    0.5669    0.6146
      0.4952    0.9534    0.3590    0.4563    0.9583    0.9128    0.1687    0.2716
      0.8751    0.7308    0.7206    0.5562    0.7065    0.5080    0.2109    0.4059
      0.0977    0.7514    0.4850    0.6086    0.1277    0.2883    0.4396    0.8336
   
   C = 
   
      2.0035    1.4616    2.0938    1.8403    2.3719    2.1024    1.3947    1.5363
      1.7654    1.2507    2.2673    1.9842    2.4201    1.7788    1.8249    1.5746
      2.2064    1.4887    2.0220    1.8156    2.1765    1.8906    1.2321    1.4367
      2.5170    2.0249    2.8768    2.9350    3.4402    3.0649    2.3231    2.2121
      1.4911    1.2368    1.0902    1.1837    1.3553    1.5152    0.6328    0.8823
      3.0520    2.6566    3.3388    2.9789    3.3807    3.0152    2.2932    2.7109
      2.2061    1.5572    2.3677    2.4712    2.8673    2.4111    1.5116    1.8813
      2.1500    1.7055    1.7627    1.7356    1.7718    1.9620    1.0059    1.4550
   
   D = 
   
      2.0035    1.4616    2.0938    1.8403    2.3719    2.1024    1.3947    1.5363
      1.7654    1.2507    2.2673    1.9842    2.4201    1.7788    1.8249    1.5746
      2.2064    1.4887    2.0220    1.8156    2.1765    1.8906    1.2321    1.4367
      2.5170    2.0249    2.8768    2.9350    3.4402    3.0649    2.3231    2.2121
      1.4911    1.2368    1.0902    1.1837    1.3553    1.5152    0.6328    0.8823
      3.0520    2.6566    3.3388    2.9789    3.3807    3.0152    2.2932    2.7109
      2.2061    1.5572    2.3677    2.4712    2.8673    2.4111    1.5116    1.8813
      2.1500    1.7055    1.7627    1.7356    1.7718    1.9620    1.0059    1.4550
   


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

   
      0.1900    0.8248    0.5931    0.8155    0.5363    0.9257
      0.1758    0.5584    0.3710    0.2909    0.2719    0.2972
      0.9804    0.9180    0.2738    0.7483    0.5641    0.6071
      0.4391    0.4721    0.6988    0.8720    0.9661    0.2488
      0.7023    0.1583    0.4738    0.1974    0.6144    0.3586
   
   
      0.9804
      0.7023
      0.8248
      0.5584
      0.9180
      0.5931
      0.6988
      0.8155
      0.7483
      0.8720
      0.5363
      0.5641
      0.9661
      0.6144
      0.9257
      0.6071
   

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

   
      3.7128    3.6438    1.6708    8.2117    5.3028    3.0349
      2.9498    0.1172    5.2866    2.8917    7.8863    9.5842
      8.2678    9.1791    8.1863    3.4030    5.1080    6.4991
      7.9517    2.1816    2.8709    9.1697    9.4502    4.7944
      0.1759    8.7026    2.3973    9.6214    5.1017    3.9330
   
   
      0.0000    0.0000    0.0000    8.2117    5.3028    0.0000
      0.0000    0.0000    5.2866    0.0000    7.8863    9.5842
      8.2678    9.1791    8.1863    0.0000    5.1080    6.4991
      7.9517    0.0000    0.0000    9.1697    9.4502    0.0000
      0.0000    8.7026    0.0000    9.6214    5.1017    0.0000
   
   
      0.0000    0.0000    0.0000    8.2117    5.3028    0.0000
      0.0000    0.0000    5.2866    0.0000    7.8863       NaN
      8.2678       NaN    8.1863    0.0000    5.1080    6.4991
      7.9517    0.0000    0.0000       NaN       NaN    0.0000
      0.0000    8.7026    0.0000       NaN    5.1017    0.0000
   

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

   
      6.5000    4.6877    9.3710    6.5000    6.5000    2.9401
      6.5000    6.5000    2.4218    8.8815    9.7754    2.3291
      3.7847    3.8894    4.8461    6.5000    4.9758    8.9848
      1.0441    6.5000    6.5000    2.1630    2.8608    8.8521
      9.9474    3.9593    1.1063    4.0595    6.5000    0.5933
   
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
   
