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
      0.2153    0.1721    0.4768    0.8514
   
   R1[2] = 0.47683882575175884
   C1 = 
      0.2259
      0.9033
      0.6552
      0.5444
      0.9944
      0.3665
      0.5167
      0.0820
   
   C1[5] = 0.36651343190773134

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
      0.6787    0.2650    0.1819    0.4086    0.6840
      0.8931    0.9258    0.4927    0.4357    0.2993
   

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
   
      0.4502    0.5812    0.1775    0.8070    0.0890    0.8125    0.4391    0.9455
      0.8063    0.6095    0.6890    0.8883    0.0237    0.5319    0.2032    0.1309
      0.2093    0.0544    0.6764    0.2731    0.2314    0.9798    0.8678    0.3982
      0.8442    0.1577    0.0504    0.5610    0.1873    0.9275    0.3834    0.0146
      0.1581    0.5381    0.5637    0.9219    0.5794    0.6845    0.3110    0.6015
      0.4195    0.7450    0.9814    0.6951    0.0564    0.2492    0.4111    0.1832
      0.5000    0.8371    0.9477    0.3647    0.2877    0.0033    0.4563    0.7718
      0.7111    0.0458    0.0665    0.1700    0.5764    0.9290    0.4188    0.1184
   
   B = 
   
      0.0993    0.1922    0.6965    0.7149    0.1922    0.2768    0.5751    0.5552
      0.4131    0.8024    0.7472    0.2725    0.3332    0.1038    0.5316    0.1283
      0.0956    0.8796    0.3787    0.2986    0.1630    0.0046    0.3262    0.5127
      0.9432    0.0311    0.4251    0.7908    0.7057    0.2630    0.6908    0.1124
      0.6386    0.8208    0.3274    0.5002    0.1133    0.4796    0.0176    0.8613
      0.6645    0.6900    0.2266    0.0555    0.3693    0.0515    0.9452    0.3923
      0.2571    0.5181    0.2765    0.2887    0.0438    0.7167    0.0566    0.9766
      0.3607    0.4558    0.5603    0.0954    0.6556    0.9419    0.3176    0.1935
   
   C = 
   
      2.1136    2.0263    2.0226    1.4780    1.8280    1.6878    2.2779    1.5134
      1.7035    1.8293    1.9134    1.7633    1.3912    0.8310    2.1823    1.4318
      1.5310    2.1844    1.3195    1.0410    1.0486    1.2969    1.6643    2.0087
      1.5227    1.3497    1.3491    1.3625    1.0091    0.8240    1.8797    1.4802
      2.2831    2.3699    1.8854    1.6320    1.6787    1.4473    2.0635    1.7371
      1.4722    2.0781    1.8073    1.5240    1.2160    0.8879    1.7557    1.4931
      1.4116    2.4394    2.1412    1.5065    1.3467    1.5177    1.5728    1.7560
      1.3920    1.6223    1.2084    1.1473    0.7872    0.9826    1.5220    1.7467
   
   D = 
   
      2.1136    2.0263    2.0226    1.4780    1.8280    1.6878    2.2779    1.5134
      1.7035    1.8293    1.9134    1.7633    1.3912    0.8310    2.1823    1.4318
      1.5310    2.1844    1.3195    1.0410    1.0486    1.2969    1.6643    2.0087
      1.5227    1.3497    1.3491    1.3625    1.0091    0.8240    1.8797    1.4802
      2.2831    2.3699    1.8854    1.6320    1.6787    1.4473    2.0635    1.7371
      1.4722    2.0781    1.8073    1.5240    1.2160    0.8879    1.7557    1.4931
      1.4116    2.4394    2.1412    1.5065    1.3467    1.5177    1.5728    1.7560
      1.3920    1.6223    1.2084    1.1473    0.7872    0.9826    1.5220    1.7467
   


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

   
      0.6985    0.3650    0.5173    0.6824    0.0181    0.5382
      0.7394    0.3190    0.0886    0.5504    0.5487    0.1713
      0.9397    0.0641    0.1072    0.9860    0.8907    0.0209
      0.5666    0.9775    0.0669    0.5394    0.1909    0.8874
      0.4708    0.9037    0.7875    0.6290    0.0773    0.9341
   
   
      0.6985
      0.7394
      0.9397
      0.5666
      0.9775
      0.9037
      0.5173
      0.7875
      0.6824
      0.5504
      0.9860
      0.5394
      0.6290
      0.5487
      0.8907
      0.5382
      0.8874
      0.9341
   

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

   
      1.1637    3.7218    9.6838    3.7785    5.8203    5.0984
      4.3799    0.8208    4.0777    1.4882    6.2454    0.5441
      2.9636    4.3520    6.8991    1.9904    1.8632    4.8406
      8.3556    1.8593    2.4514    6.0068    0.1090    1.3207
      0.4768    4.6502    7.5690    9.9649    5.7060    7.0870
   
   
      0.0000    0.0000    9.6838    0.0000    5.8203    5.0984
      0.0000    0.0000    0.0000    0.0000    6.2454    0.0000
      0.0000    0.0000    6.8991    0.0000    0.0000    0.0000
      8.3556    0.0000    0.0000    6.0068    0.0000    0.0000
      0.0000    0.0000    7.5690    9.9649    5.7060    7.0870
   
   
      0.0000    0.0000       NaN    0.0000    5.8203    5.0984
      0.0000    0.0000    0.0000    0.0000    6.2454    0.0000
      0.0000    0.0000    6.8991    0.0000    0.0000    0.0000
      8.3556    0.0000    0.0000    6.0068    0.0000    0.0000
      0.0000    0.0000    7.5690       NaN    5.7060    7.0870
   

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

   
      0.1197    4.1893    6.5000    4.7984    3.2531    0.8376
      3.6314    3.8672    1.3594    0.0042    6.5000    6.5000
      6.5000    1.7160    6.5000    0.4073    6.5000    9.1981
      2.2135    9.4641    2.9135    3.1538    4.1771    6.5000
      6.5000    2.4995    6.5000    3.6778    6.5000    1.4659
   
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
   
