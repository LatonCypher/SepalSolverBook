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
      0.8897    0.8451    0.4244    0.6283
   
   R1[2] = 0.42444991083087746
   C1 = 
      0.6205
      0.2052
      0.9471
      0.3334
      0.0382
      0.3620
      0.6584
      0.6572
   
   C1[5] = 0.36203070425807016

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
      0.8661    0.6595    0.3179    0.3018    0.3337
      0.0789    0.8212    0.7717    0.3255    0.0934
   

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
   
      0.3685    0.1982    0.5797    0.6334    0.3617    0.9382    0.3416    0.4695
      0.8290    0.1495    0.5692    0.4177    0.1752    0.9510    0.4949    0.8888
      0.3378    0.5665    0.6523    0.7119    0.2740    0.9266    0.6068    0.3110
      0.4197    0.0523    0.9784    0.2032    0.7046    0.4803    0.7040    0.1442
      0.7964    0.5054    0.0520    0.8400    0.0988    0.5368    0.8234    0.6211
      0.1934    0.2195    0.6423    0.8216    0.2466    0.1661    0.9278    0.6160
      0.8596    0.7272    0.4953    0.4569    0.7387    0.1442    0.0759    0.1911
      0.1876    0.1918    0.0567    0.2519    0.6824    0.4052    0.0795    0.4456
   
   B = 
   
      0.7905    0.7662    0.6767    0.6669    0.5785    0.8637    0.7034    0.7937
      0.1339    0.2753    0.0550    0.4115    0.4336    0.7567    0.3147    0.6036
      0.1466    0.0377    0.9452    0.1876    0.3593    0.7219    0.4655    0.4499
      0.5405    0.5599    0.6412    0.6446    0.4390    0.3178    0.3978    0.0415
      0.9087    0.0203    0.2063    0.3996    0.8169    0.9722    0.5268    0.3614
      0.3570    0.0966    0.4503    0.4323    0.7579    0.1791    0.1986    0.1570
      0.3687    0.5426    0.2056    0.2824    0.9110    0.7651    0.8401    0.4722
      0.3477    0.9829    0.9124    0.2807    0.2936    0.7606    0.8236    0.3109
   
   C = 
   
      1.6980    1.4583    2.2101    1.6227    2.2412    2.2263    1.8940    1.2846
      1.9747    2.1692    2.7521    1.8607    2.5080    2.7682    2.4902    1.7444
      1.7349    1.5680    2.2151    1.8084    2.5582    2.5508    2.0970    1.5609
      1.7134    1.0711    1.9798    1.3444    2.3295    2.5922    2.0246    1.5206
      1.9597    2.3327    2.1526    1.9687    2.4873    2.6694    2.4395    1.6974
      1.5601    1.8228    2.1553    1.4746    2.1519    2.5058    2.2806    1.3539
      1.9135    1.3913    1.7901    1.6925    2.0291    2.7429    1.8847    1.7479
      1.2674    0.8739    1.0988    0.9724    1.3905    1.5640    1.1927    0.7870
   
   D = 
   
      1.6980    1.4583    2.2101    1.6227    2.2412    2.2263    1.8940    1.2846
      1.9747    2.1692    2.7521    1.8607    2.5080    2.7682    2.4902    1.7444
      1.7349    1.5680    2.2151    1.8084    2.5582    2.5508    2.0970    1.5609
      1.7134    1.0711    1.9798    1.3444    2.3295    2.5922    2.0246    1.5206
      1.9597    2.3327    2.1526    1.9687    2.4873    2.6694    2.4395    1.6974
      1.5601    1.8228    2.1553    1.4746    2.1519    2.5058    2.2806    1.3539
      1.9135    1.3913    1.7901    1.6925    2.0291    2.7429    1.8847    1.7479
      1.2674    0.8739    1.0988    0.9724    1.3905    1.5640    1.1927    0.7870
   


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

   
      0.7214    0.8100    0.0953    0.6948    0.6681    0.9161
      0.8784    0.7296    0.4955    0.1315    0.2982    0.7786
      0.4056    0.2619    0.4690    0.7749    0.0364    0.2928
      0.4161    0.8759    0.1299    0.7427    0.5662    0.9031
      0.0004    0.5202    0.2086    0.7145    0.2047    0.0311
   
   
      0.7214
      0.8784
      0.8100
      0.7296
      0.8759
      0.5202
      0.6948
      0.7749
      0.7427
      0.7145
      0.6681
      0.5662
      0.9161
      0.7786
      0.9031
   

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

   
      5.3162    3.1705    0.9313    5.0348    5.8626    4.0359
      8.8966    1.3023    5.5471    7.1190    9.4883    0.7151
      1.4076    1.9743    6.5511    7.7875    4.8506    2.9930
      4.1822    2.3544    2.8303    1.9497    0.2251    9.4530
      2.2940    5.6450    9.3308    8.6139    6.2192    6.7029
   
   
      5.3162    0.0000    0.0000    5.0348    5.8626    0.0000
      8.8966    0.0000    5.5471    7.1190    9.4883    0.0000
      0.0000    0.0000    6.5511    7.7875    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    9.4530
      0.0000    5.6450    9.3308    8.6139    6.2192    6.7029
   
   
      5.3162    0.0000    0.0000    5.0348    5.8626    0.0000
      8.8966    0.0000    5.5471    7.1190       NaN    0.0000
      0.0000    0.0000    6.5511    7.7875    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      0.0000    5.6450       NaN    8.6139    6.2192    6.7029
   

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

   
      4.6539    6.5000    1.3135    6.5000    1.1480    4.6950
      1.4451    9.7427    6.5000    9.5132    6.5000    6.5000
      9.5127    9.5281    4.6423    1.1740    6.5000    4.7174
      3.1523    2.0366    6.5000    0.8149    1.2714    4.9006
      6.5000    0.0629    1.1715    9.7503    4.7216    4.0341
   
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
   
