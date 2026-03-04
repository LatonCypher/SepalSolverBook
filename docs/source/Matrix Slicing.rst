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
      0.2352    0.4169    0.2711    0.5649
   
   R1[2] = 0.27114162030683786
   C1 = 
      0.1540
      0.9616
      0.1761
      0.1018
      0.6811
      0.7709
      0.1984
      0.4029
   
   C1[5] = 0.7708644847525824

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
      0.2599    0.3812    0.8954    0.5840    0.3232
      0.2787    0.7053    0.4927    0.7444    0.5138
   

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
   
      0.2191    0.5864    0.2079    0.2574    0.7121    0.9882    0.0502    0.7093
      0.2793    0.9249    0.1256    0.7514    0.6561    0.3003    0.5197    0.9440
      0.5764    0.5691    0.4632    0.3782    0.3376    0.1636    0.0290    0.4365
      0.7467    0.7845    0.6360    0.7812    0.3286    0.2628    0.6093    0.0757
      0.3626    0.5522    0.8359    0.7516    0.6697    0.5515    0.0593    0.3787
      0.5059    0.5628    0.8607    0.7595    0.9525    0.4748    0.5749    0.5693
      0.6228    0.2002    0.9523    0.8372    0.0926    0.4702    0.4105    0.6901
      0.2939    0.2664    0.8947    0.7164    0.4856    0.2928    0.0093    0.5452
   
   B = 
   
      0.6679    0.6734    0.7375    0.4233    0.4873    0.3706    0.6023    0.2117
      0.2645    0.7719    0.9379    0.7530    0.5825    0.6849    0.3264    0.2834
      0.8535    0.4253    0.1401    0.5980    0.2135    0.4061    0.2655    0.9135
      0.3731    0.2978    0.5544    0.2967    0.9740    0.5913    0.7353    0.9536
      0.5244    0.3221    0.3064    0.1016    0.3061    0.8423    0.7612    0.9666
      0.1540    0.0622    0.9235    0.6966    0.6003    0.7891    0.1011    0.3708
      0.0888    0.1298    0.9494    0.5808    0.7119    0.3521    0.5292    0.4667
      0.2638    0.8632    0.8108    0.2498    0.4818    0.7358    0.5153    0.8629
   
   C = 
   
      1.2921    1.6748    2.6368    1.7020    1.9321    2.6386    1.6018    2.3382
      1.5042    2.2916    3.2448    1.9262    2.6395    2.8996    2.3472    2.9553
      1.3919    1.6365    1.8694    1.3359    1.5121    1.7599    1.4478    1.8443
      1.8273    1.8781    2.7919    2.1082    2.4461    2.2887    2.0872    2.4712
      1.9234    1.8344    2.3970    1.8735    2.1696    2.5953    1.9653    2.9200
      2.2785    2.2697    3.1803    2.2817    2.7580    3.1699    2.6460    3.6334
      1.9335    1.9362    2.6564    1.9800    2.3741    2.3510    1.9997    2.9078
      1.7421    1.6439    1.8593    1.4676    1.7810    2.1229    1.7135    2.6911
   
   D = 
   
      1.2921    1.6748    2.6368    1.7020    1.9321    2.6386    1.6018    2.3382
      1.5042    2.2916    3.2448    1.9262    2.6395    2.8996    2.3472    2.9553
      1.3919    1.6365    1.8694    1.3359    1.5121    1.7599    1.4478    1.8443
      1.8273    1.8781    2.7919    2.1082    2.4461    2.2887    2.0872    2.4712
      1.9234    1.8344    2.3970    1.8735    2.1696    2.5953    1.9653    2.9200
      2.2785    2.2697    3.1803    2.2817    2.7580    3.1699    2.6460    3.6334
      1.9335    1.9362    2.6564    1.9800    2.3741    2.3510    1.9997    2.9078
      1.7421    1.6439    1.8593    1.4676    1.7810    2.1229    1.7135    2.6911
   


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

   
      0.5242    0.4700    0.1416    0.0237    0.4425    0.9086
      0.0843    0.1439    0.1356    0.1620    0.7201    0.5348
      0.2264    0.3430    0.1029    0.2629    0.9688    0.5828
      0.9839    0.4440    0.6828    0.4578    0.1175    0.4752
      0.7311    0.5491    0.7685    0.6385    0.5730    0.1693
   
   
      0.5242
      0.9839
      0.7311
      0.5491
      0.6828
      0.7685
      0.6385
      0.7201
      0.9688
      0.5730
      0.9086
      0.5348
      0.5828
   

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

   
      6.7319    5.0501    3.0527    9.1267    0.8298    3.6833
      8.2972    7.0889    5.7430    3.1011    3.2669    5.2946
      6.1097    4.0417    6.8606    4.7654    3.5306    5.9766
      3.9610    7.5526    5.9752    7.7611    1.7666    5.2898
      3.7177    8.0767    6.6037    6.6932    6.6575    5.2725
   
   
      6.7319    5.0501    0.0000    9.1267    0.0000    0.0000
      8.2972    7.0889    5.7430    0.0000    0.0000    5.2946
      6.1097    0.0000    6.8606    0.0000    0.0000    5.9766
      0.0000    7.5526    5.9752    7.7611    0.0000    5.2898
      0.0000    8.0767    6.6037    6.6932    6.6575    5.2725
   
   
      6.7319    5.0501    0.0000       NaN    0.0000    0.0000
      8.2972    7.0889    5.7430    0.0000    0.0000    5.2946
      6.1097    0.0000    6.8606    0.0000    0.0000    5.9766
      0.0000    7.5526    5.9752    7.7611    0.0000    5.2898
      0.0000    8.0767    6.6037    6.6932    6.6575    5.2725
   

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

   
      4.7460    2.9706    1.7526    6.5000    0.2584    9.6034
      6.5000    0.8396    0.4018    6.5000    8.7555    9.3578
      4.7068    0.4148    6.5000    6.5000    3.9802    1.5643
      2.2672    4.9767    1.0866    4.7875    2.7793    0.8151
      4.3738    6.5000    6.5000    6.5000    1.2919    0.2126
   
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
   
