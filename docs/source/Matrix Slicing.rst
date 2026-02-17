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
      0.0588    0.9728    0.2548    0.4397
   
   R1[2] = 0.2548134216589196
   C1 = 
      0.3710
      0.9313
      0.1765
      0.2987
      0.6969
      0.7866
      0.0652
      0.2145
   
   C1[5] = 0.7865779829372798

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
      0.9074    0.1041    0.1410    0.3885    0.5306
      0.3525    0.3772    0.2995    0.2210    0.0659
   

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
   
      0.0633    0.1846    0.2356    0.4370    0.6510    0.7644    0.8183    0.4747
      0.5837    0.7784    0.4403    0.5592    0.6519    0.4463    0.0213    0.9685
      0.2827    0.2310    0.9459    0.5827    0.8242    0.9490    0.0294    0.6864
      0.5418    0.2787    0.8739    0.8813    0.1904    0.2614    0.8281    0.2892
      0.5103    0.0009    0.3277    0.6566    0.0342    0.3130    0.7123    0.2721
      0.1264    0.9866    0.0294    0.0509    0.3702    0.5265    0.1821    0.4505
      0.1269    0.0856    0.2832    0.9461    0.1080    0.6581    0.6951    0.3949
      0.6605    0.3294    0.6935    0.6475    0.8435    0.8044    0.2363    0.9646
   
   B = 
   
      0.5572    0.0390    0.7883    0.7311    0.3703    0.8733    0.0135    0.3104
      0.9713    0.0336    0.4210    0.1377    0.0959    0.3491    0.2924    0.6536
      0.0478    0.4469    0.2434    0.5119    0.1193    0.3023    0.9572    0.6738
      0.7966    0.2283    0.1878    0.0958    0.9127    0.6604    0.8471    0.5201
      0.1259    0.9169    0.5923    0.3483    0.0969    0.6384    0.7300    0.9490
      0.3163    0.0988    0.8157    0.6790    0.0501    0.9013    0.8340    0.8401
      0.2218    0.0106    0.8744    0.1393    0.8466    0.0506    0.6660    0.3768
      0.3050    0.0379    0.9621    0.9842    0.6850    0.0164    0.8920    0.0181
   
   C = 
   
      1.2240    0.9128    2.4484    1.5612    1.5875    1.6334    2.7316    2.1031
      2.0713    1.0522    2.7008    2.2992    1.6208    2.1195    2.8569    2.2967
      1.5111    1.4504    2.6080    2.3896    1.3940    2.3926    3.4952    2.7819
      1.6949    0.8424    2.2510    1.6101    2.0667    1.8205    2.8383    2.1150
      1.1682    0.3964    1.7659    1.1953    1.6359    1.3232    1.8801    1.2900
      1.4617    0.4734    1.7733    1.2035    0.7165    1.2249    1.5939    1.6008
      1.4174    0.5368    1.9713    1.3102    1.8549    1.5550    2.5421    1.7026
      1.9442    1.3866    3.2402    2.7676    1.9327    2.6205    3.6219    2.8071
   
   D = 
   
      1.2240    0.9128    2.4484    1.5612    1.5875    1.6334    2.7316    2.1031
      2.0713    1.0522    2.7008    2.2992    1.6208    2.1195    2.8569    2.2967
      1.5111    1.4504    2.6080    2.3896    1.3940    2.3926    3.4952    2.7819
      1.6949    0.8424    2.2510    1.6101    2.0667    1.8205    2.8383    2.1150
      1.1682    0.3964    1.7659    1.1953    1.6359    1.3232    1.8801    1.2900
      1.4617    0.4734    1.7733    1.2035    0.7165    1.2249    1.5939    1.6008
      1.4174    0.5368    1.9713    1.3102    1.8549    1.5550    2.5421    1.7026
      1.9442    1.3866    3.2402    2.7676    1.9327    2.6205    3.6219    2.8071
   


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

   
      0.1613    0.3584    0.5290    0.4032    0.6835    0.3815
      0.0743    0.3179    0.7383    0.0371    0.2778    0.6979
      0.1653    0.0015    0.6101    0.1756    0.7318    0.4031
      0.4767    0.8493    0.6418    0.9888    0.0322    0.9969
      0.0796    0.7027    0.5198    0.0665    0.5572    0.5150
   
   
      0.8493
      0.7027
      0.5290
      0.7383
      0.6101
      0.6418
      0.5198
      0.9888
      0.6835
      0.7318
      0.5572
      0.6979
      0.9969
      0.5150
   

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

   
      5.2226    6.8761    1.3846    0.2983    8.7197    4.0934
      3.6653    5.2039    4.4334    3.1093    5.4388    6.9378
      6.3987    2.2042    9.5328    8.7660    8.8149    3.8152
      7.8688    2.2962    5.2705    6.3572    5.1769    1.0800
      6.6787    0.8444    4.0719    1.6606    7.3495    1.6052
   
   
      5.2226    6.8761    0.0000    0.0000    8.7197    0.0000
      0.0000    5.2039    0.0000    0.0000    5.4388    6.9378
      6.3987    0.0000    9.5328    8.7660    8.8149    0.0000
      7.8688    0.0000    5.2705    6.3572    5.1769    0.0000
      6.6787    0.0000    0.0000    0.0000    7.3495    0.0000
   
   
      5.2226    6.8761    0.0000    0.0000    8.7197    0.0000
      0.0000    5.2039    0.0000    0.0000    5.4388    6.9378
      6.3987    0.0000       NaN    8.7660    8.8149    0.0000
      7.8688    0.0000    5.2705    6.3572    5.1769    0.0000
      6.6787    0.0000    0.0000    0.0000    7.3495    0.0000
   

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

   
      6.5000    9.7448    6.5000    6.5000    3.9742    4.2611
      6.5000    6.5000    4.8465    3.2509    6.5000    0.4057
      6.5000    1.1418    6.5000    9.4035    1.6373    9.1191
      1.8720    6.5000    9.4539    9.4201    1.9998    9.3487
      1.6628    4.8636    4.1153    4.6311    6.5000    6.5000
   
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
   
