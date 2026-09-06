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
      0.1165    0.7036    0.6673    0.4322
   
   R1[2] = 0.6673348961386707
   C1 = 
      0.9448
      0.8821
      0.2233
      0.1331
      0.8984
      0.2617
      0.6999
      0.3558
   
   C1[5] = 0.261684089245075

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
      0.7347    0.6176    0.1580    0.2398    0.5777
      0.4732    0.9418    0.0468    0.0231    0.1290
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.2839    0.0028    0.8148    0.2175    0.0039    0.6797    0.2420    0.0444
      0.4685    0.4997    0.7932    0.7434    0.6112    0.2559    0.6663    0.5475
      0.4060    0.9987    0.6540    0.5399    0.0103    0.1381    0.8386    0.4609
      0.0284    0.5414    0.5619    0.5365    0.2497    0.8875    0.9069    0.7768
      0.8258    0.3017    0.2283    0.9749    0.6309    0.7941    0.1632    0.6643
      0.9954    0.5848    0.0572    0.8729    0.4198    0.4784    0.3883    0.7225
      0.9396    0.7073    0.9720    0.0195    0.1529    0.8257    0.1569    0.5735
      0.0080    0.7580    0.6666    0.3216    0.3097    0.1219    0.2769    0.9352
   
   B = 
   
      0.6740    0.8968    0.2648    0.2390    0.5570    0.2546    0.7395    0.3231
      0.2645    0.5087    0.9823    0.7923    0.5720    0.7450    0.9250    0.8807
      0.6419    0.1445    0.7001    0.7645    0.5761    0.1255    0.1184    0.1780
      0.9357    0.3796    0.9472    0.9088    0.4104    0.5233    0.8993    0.9205
      0.7365    0.9255    0.8995    0.3449    0.3868    0.7999    0.4170    0.7682
      0.4557    0.1571    0.6646    0.4969    0.5553    0.7337    0.0669    0.8776
      0.9950    0.7576    0.0559    0.7967    0.5170    0.2078    0.4605    0.1414
      0.9509    0.2501    0.7576    0.2780    0.2080    0.8333    0.9929    0.8125
   
   C = 
   
      1.5143    0.7612    1.3568    1.4350    1.2317    0.8796    0.7073    1.1093
      3.4030    2.3187    3.0463    2.8109    2.1456    2.2514    2.6935    2.6501
      2.8060    1.9535    2.5549    2.7473    2.0058    1.8799    2.6443    2.2463
      3.2543    1.8376    2.8945    2.8184    2.0891    2.4485    2.4232    2.8099
      3.3157    2.2955    3.2058    2.4238    2.0716    2.6485    2.8444    3.2149
      3.2795    2.4684    2.9695    2.4309    2.0591    2.5226    3.1720    3.0346
      2.6528    1.8837    2.7721    2.2932    2.2139    2.1369    2.2423    2.4477
      2.3832    1.3607    2.6017    2.0524    1.4792    1.9927    2.2686    2.2287
   
   D = 
   
      1.5143    0.7612    1.3568    1.4350    1.2317    0.8796    0.7073    1.1093
      3.4030    2.3187    3.0463    2.8109    2.1456    2.2514    2.6935    2.6501
      2.8060    1.9535    2.5549    2.7473    2.0058    1.8799    2.6443    2.2463
      3.2543    1.8376    2.8945    2.8184    2.0891    2.4485    2.4232    2.8099
      3.3157    2.2955    3.2058    2.4238    2.0716    2.6485    2.8444    3.2149
      3.2795    2.4684    2.9695    2.4309    2.0591    2.5226    3.1720    3.0346
      2.6528    1.8837    2.7721    2.2932    2.2139    2.1369    2.2423    2.4477
      2.3832    1.3607    2.6017    2.0524    1.4792    1.9927    2.2686    2.2287
   


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

   
      0.9029    0.2951    0.8640    0.8168    0.8853    0.5802
      0.4337    0.5592    0.4693    0.9647    0.4976    0.0384
      0.8074    0.8749    0.0788    0.3669    0.7635    0.7287
      0.9325    0.2712    0.6568    0.9184    0.9485    0.8517
      0.6422    0.2219    0.1204    0.5353    0.1555    0.2478
   
   
      0.9029
      0.8074
      0.9325
      0.6422
      0.5592
      0.8749
      0.8640
      0.6568
      0.8168
      0.9647
      0.9184
      0.5353
      0.8853
      0.7635
      0.9485
      0.5802
      0.7287
      0.8517
   

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

   
      6.6213    9.4904    5.9783    8.5067    4.4754    6.8657
      8.9622    9.2049    1.4478    2.6504    4.0688    7.2754
      3.9760    3.7596    9.7459    3.2780    0.6821    6.6378
      6.8017    1.7293    9.2466    6.6048    5.1690    0.5760
      6.1144    5.5498    8.7880    4.9892    6.1621    4.3663
   
   
      6.6213    9.4904    5.9783    8.5067    0.0000    6.8657
      8.9622    9.2049    0.0000    0.0000    0.0000    7.2754
      0.0000    0.0000    9.7459    0.0000    0.0000    6.6378
      6.8017    0.0000    9.2466    6.6048    5.1690    0.0000
      6.1144    5.5498    8.7880    0.0000    6.1621    0.0000
   
   
      6.6213       NaN    5.9783    8.5067    0.0000    6.8657
      8.9622       NaN    0.0000    0.0000    0.0000    7.2754
      0.0000    0.0000       NaN    0.0000    0.0000    6.6378
      6.8017    0.0000       NaN    6.6048    5.1690    0.0000
      6.1144    5.5498    8.7880    0.0000    6.1621    0.0000
   

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

   
      0.5359    9.7974    0.5469    3.1817    8.3570    0.8156
      2.7945    1.5051    1.7808    8.3692    6.5000    0.9926
      6.5000    3.3532    6.5000    9.8834    6.5000    6.5000
      0.4853    9.5988    6.5000    6.5000    1.8735    4.1946
      6.5000    6.5000    8.4595    0.7807    3.0877    9.4358
   
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
   
