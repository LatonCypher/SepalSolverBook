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
      0.1359    0.9831    0.7331    0.8582
   
   R1[2] = 0.7331233306301037
   C1 = 
      0.4274
      0.5099
      0.6174
      0.8424
      0.5798
      0.5597
      0.5360
      0.4831
   
   C1[5] = 0.5597120188335151

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
      0.8921    0.7884    0.3790    0.9645    0.7083
      0.1904    0.3417    0.3296    0.4857    0.6064
   

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

   * - +--------------------+----------------------+--------------------+
   * - 
     - Feature
     - Standard Algorithm
     - Strassen Algorithm
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Approach
     - Direct row-by-column
     - Divide-and-conquer
     - 
   * - 
     - 
     - multiplication
     - with recursive
     - 
   * - 
     - 
     - 
     - submatrices
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Multiplications
     - 8
     - 7
     - 
   * - 
     - for 2×2 matrices
     - 
     - 
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Additions/
     - 4
     - 18
     - 
   * - 
     - Subtractions
     - 
     - 
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Time Complexity
     - O(n^3)
     - O(n^(log2 7))
     - 
   * - 
     - 
     - 
     - ≈ O(n^2.81)
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Best Use Case
     - Small matrices
     - Large matrices
     - 
   * - +--------------------+----------------------+--------------------+

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
   M_3 &=& A_{11}\left(B_{12} - B_{22}\left) \\
   M_4 &=& A_{22}\left(B_{21} - B_{11}\left) \\
   M_5 &=& \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 &=& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 &=& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix::

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
   
      0.5649    0.0631    0.4386    0.4371    0.5844    0.6606    0.6400    0.6880
      0.8840    0.0606    0.7746    0.6656    0.5283    0.3636    0.8999    0.4199
      0.8731    0.1104    0.2018    0.3585    0.0138    0.2354    0.0447    0.7327
      0.2970    0.7486    0.9462    0.7680    0.8093    0.0761    0.4499    0.9474
      0.8300    0.9465    0.9680    0.5106    0.9926    0.3327    0.9321    0.5826
      0.6314    0.5553    0.3818    0.8064    0.1607    0.1280    0.0922    0.5772
      0.8125    0.4631    0.6722    0.1213    0.9756    0.7194    0.1326    0.9805
      0.5727    0.6891    0.3953    0.8942    0.1734    0.0903    0.8775    0.1274
   
   B = 
   
      0.5608    0.7738    0.1798    0.9486    0.0720    0.8553    0.6064    0.1351
      0.4677    0.6610    0.6609    0.9511    0.7166    0.7506    0.6102    0.9387
      0.9260    0.0178    0.4971    0.2982    0.7424    0.8450    0.4161    0.9868
      0.0584    0.3992    0.3628    0.2575    0.3274    0.0655    0.4754    0.0957
      0.3101    0.5765    0.1181    0.6393    0.3731    0.4431    0.2530    0.0320
      0.1846    0.1514    0.4503    0.4683    0.2874    0.2767    0.5492    0.0990
      0.9275    0.4912    0.2398    0.2389    0.8858    0.1350    0.1830    0.5419
      0.8848    0.9107    0.1615    0.2454    0.0021    0.9184    0.1320    0.6717
   
   C = 
   
      2.2835    2.0389    1.1509    1.8439    1.5309    2.0897    1.4898    1.5033
      2.7173    2.1877    1.3353    2.1247    1.9997    2.3415    1.7652    1.8270
      1.4866    1.6281    0.6970    1.3952    0.5231    1.7738    1.0888    0.9953
      2.9582    2.6099    1.6879    2.3666    2.2361    2.9762    1.8495    2.6637
      3.5835    3.0999    2.0259    3.2637    2.9167    3.4645    2.4076    2.9659
      1.6842    1.8673    1.1548    1.7750    1.1705    1.9815    1.4680    1.5161
      2.7277    2.6246    1.4597    2.6757    1.6194    3.1682    1.9081    2.0524
      2.0588    1.9233    1.3714    1.9408    1.9894    1.7369    1.6281    1.7754
   
   D = 
   
      2.2835    2.0389    1.1509    1.8439    1.5309    2.0897    1.4898    1.5033
      2.7173    2.1877    1.3353    2.1247    1.9997    2.3415    1.7652    1.8270
      1.4866    1.6281    0.6970    1.3952    0.5231    1.7738    1.0888    0.9953
      2.9582    2.6099    1.6879    2.3666    2.2361    2.9762    1.8495    2.6637
      3.5835    3.0999    2.0259    3.2637    2.9167    3.4645    2.4076    2.9659
      1.6842    1.8673    1.1548    1.7750    1.1705    1.9815    1.4680    1.5161
      2.7277    2.6246    1.4597    2.6757    1.6194    3.1682    1.9081    2.0524
      2.0588    1.9233    1.3714    1.9408    1.9894    1.7369    1.6281    1.7754
   


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

   
      0.1454    0.3481    0.5332    0.9430    0.2904    0.5544
      0.0781    0.2845    0.4527    0.9184    0.3093    0.4397
      0.2318    0.3740    0.1734    0.9121    0.1993    0.2193
      0.3558    0.9706    0.9510    0.8492    0.1753    0.5213
      0.8922    0.3166    0.1446    0.3582    0.0775    0.7285
   
   
      0.8922
      0.9706
      0.5332
      0.9510
      0.9430
      0.9184
      0.9121
      0.8492
      0.5544
      0.5213
      0.7285
   

Logical indexing is most effective when performing bulk updates. You can set values for specific elements without affecting the rest of the matrix.


.. code-block:: csharp

   Matrix A = Rand(5, 6);
   A *= 10;
   Console.WriteLine(A);

   // Extract all values greater than 0.5
   var L = A[A > 0.5];
   Console.WriteLine(L);

   // Set all elements less than 5 to zero
   A[A < 5] = 0;

   // Replace specific "masquerading" integers or outliers
   A[A == -999] = double.NaN;


Ouput

.. terminal::

   
      2.9702    9.5316    9.8916    9.5428    9.7016    7.9527
      0.0852    6.8738    8.3929    5.1712    7.1842    5.7856
      5.9462    0.2440    9.1322    0.4630    6.5774    3.5572
      0.3170    9.4203    9.5505    1.1127    3.7847    4.4556
      8.8279    7.5435    6.8016    0.9499    1.0173    9.4106
   
   
      2.9702
      5.9462
      8.8279
      9.5316
      6.8738
      9.4203
      7.5435
      9.8916
      8.3929
      9.1322
      9.5505
      6.8016
      9.5428
      5.1712
      1.1127
      0.9499
      9.7016
      7.1842
      6.5774
      3.7847
      1.0173
      7.9527
      5.7856
      3.5572
      4.4556
      9.4106
   

Complex Conditions
~~~~~~~~~~~~~~~~~~
You can combine multiple conditions using logical operators. This allows for precise data "clipping" or windowing.
* Use ``&`` for **AND**
* Use ``|`` for **OR**

.. code-block:: csharp

   Matrix A = Rand(5, 6);
   A *= 10;
   // Set values within the range (5, 8) to a new value
   A[(A > 5).And(A < 8)] = 6.5;

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
   
