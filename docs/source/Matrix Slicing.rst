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
      0.5145    0.0361    0.9324    0.9017
   
   R1[2] = 0.9324116531586316
   C1 = 
      0.1859
      0.7882
      0.8332
      0.6817
      0.0108
      0.1399
      0.0500
      0.9133
   
   C1[5] = 0.1398587253729182

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
      0.3553    0.8850    0.2308    0.8496    0.9818
      0.7325    0.3786    0.6900    0.4254    0.1344
   

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
   
      0.6632    0.3026    0.2613    0.3178    0.3902    0.1551    0.5918    0.3384
      0.6751    0.3583    0.6917    0.9019    0.0915    0.3828    0.3482    0.3945
      0.2524    0.9314    0.7749    0.2249    0.7066    0.3861    0.3500    0.2261
      0.1300    0.9400    0.4177    0.4505    0.2122    0.9490    0.4814    0.2269
      0.8674    0.2831    0.6269    0.1701    0.5856    0.9989    0.0963    0.0848
      0.9591    0.6001    0.5933    0.1176    0.4757    0.4133    0.3833    0.9040
      0.5733    0.2109    0.6976    0.1976    0.2906    0.3248    0.9539    0.2537
      0.1185    0.2506    0.0516    0.2294    0.3108    0.4021    0.0475    0.9207
   
   B = 
   
      0.8990    0.6270    0.6666    0.8459    0.3115    0.5845    0.4383    0.5671
      0.6172    0.4212    0.8686    0.5363    0.5188    0.9016    0.2254    0.8726
      0.6394    0.2133    0.9008    0.3337    0.1029    0.2819    0.3144    0.5693
      0.5318    0.4858    0.8175    0.0987    0.8847    0.9634    0.6682    0.6663
      0.8415    0.2764    0.8726    0.4163    0.1466    0.1869    0.1820    0.4658
      0.2330    0.2877    0.2314    0.4935    0.3610    0.3290    0.3133    0.4195
      0.7106    0.9389    0.0068    0.0823    0.6713    0.7940    0.5590    0.4506
      0.8316    0.6370    0.6245    0.9721    0.4107    0.1372    0.6699    0.0438
   
   C = 
   
      2.1856    1.6771    1.7920    1.4586    1.3210    1.6806    1.3306    1.5291
      2.4918    1.8737    2.5389    1.7224    1.8127    2.2553    1.7925    2.0677
      2.5382    1.6043    2.7087    1.7272    1.4114    1.9904    1.3113    2.2057
      2.1342    1.7136    2.1975    1.6149    1.7597    2.2405    1.4583    2.1558
      2.3103    1.4733    2.3236    1.9388    1.1780    1.6290    1.2850    1.9481
      3.1953    2.2240    2.8691    2.6551    1.6227    2.0355    1.8568    2.0910
      2.4058    1.9242    1.8490    1.4569    1.4390    1.8656    1.5081    1.7505
      1.5708    1.1350    1.4701    1.5012    0.9758    0.8851    1.1038    0.8432
   
   D = 
   
      2.1856    1.6771    1.7920    1.4586    1.3210    1.6806    1.3306    1.5291
      2.4918    1.8737    2.5389    1.7224    1.8127    2.2553    1.7925    2.0677
      2.5382    1.6043    2.7087    1.7272    1.4114    1.9904    1.3113    2.2057
      2.1342    1.7136    2.1975    1.6149    1.7597    2.2405    1.4583    2.1558
      2.3103    1.4733    2.3236    1.9388    1.1780    1.6290    1.2850    1.9481
      3.1953    2.2240    2.8691    2.6551    1.6227    2.0355    1.8568    2.0910
      2.4058    1.9242    1.8490    1.4569    1.4390    1.8656    1.5081    1.7505
      1.5708    1.1350    1.4701    1.5012    0.9758    0.8851    1.1038    0.8432
   


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

   
      0.8339    0.9931    0.1994    0.4633    0.0844    0.9505
      0.6716    0.4283    0.8352    0.5160    0.3274    0.4742
      0.4188    0.8708    0.9797    0.4837    0.8028    0.6191
      0.9739    0.1855    0.2663    0.5136    0.1633    0.1047
      0.8982    0.8766    0.7838    0.4610    0.6635    0.1538
   
   
      0.8339
      0.6716
      0.9739
      0.8982
      0.9931
      0.8708
      0.8766
      0.8352
      0.9797
      0.7838
      0.5160
      0.5136
      0.8028
      0.6635
      0.9505
      0.6191
   

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

   
      9.9662    8.8752    7.3432    3.4267    1.5013    7.2216
      8.0460    6.9057    9.1454    8.5218    9.0207    8.4175
      3.0659    8.2519    3.1597    7.2507    8.8597    6.5775
      4.7908    5.5464    1.0360    8.4413    7.1820    1.3229
      1.8268    8.4844    8.6282    2.1169    5.9802    7.9228
   
   
      9.9662    8.8752    7.3432    0.0000    0.0000    7.2216
      8.0460    6.9057    9.1454    8.5218    9.0207    8.4175
      0.0000    8.2519    0.0000    7.2507    8.8597    6.5775
      0.0000    5.5464    0.0000    8.4413    7.1820    0.0000
      0.0000    8.4844    8.6282    0.0000    5.9802    7.9228
   
   
         NaN    8.8752    7.3432    0.0000    0.0000    7.2216
      8.0460    6.9057       NaN    8.5218       NaN    8.4175
      0.0000    8.2519    0.0000    7.2507    8.8597    6.5775
      0.0000    5.5464    0.0000    8.4413    7.1820    0.0000
      0.0000    8.4844    8.6282    0.0000    5.9802    7.9228
   

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

   
      3.3822    9.0934    0.5941    0.2755    4.6640    6.5000
      0.5491    8.2109    6.5000    1.1533    0.9229    6.5000
      0.7052    9.5903    9.9755    6.5000    0.0271    9.1821
      4.3847    8.6358    6.5000    9.5831    0.4491    3.1669
      6.5000    9.8963    3.6157    1.3778    4.6547    6.5000
   
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
   
