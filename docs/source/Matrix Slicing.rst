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
      0.5140    0.1119    0.4795    0.9544
   
   R1[2] = 0.4794661186222108
   C1 = 
      0.3825
      0.1671
      0.5880
      0.6633
      0.4921
      0.3622
      0.2923
      0.0243
   
   C1[5] = 0.3621984884228885

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
      0.4567    0.7896    0.3693    0.8296    0.1111
      0.5845    0.8962    0.1568    0.8979    0.4749
   

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
   
      0.8675    0.0472    0.6295    0.0226    0.1866    0.4033    0.0856    0.4145
      0.6219    0.8863    0.8110    0.4832    0.1764    0.3217    0.0576    0.5590
      0.9474    0.0095    0.1252    0.2247    0.1814    0.9000    0.4339    0.7625
      0.2671    0.2489    0.2842    0.6053    0.0416    0.1388    0.0565    0.1911
      0.0696    0.6372    0.4560    0.9423    0.5425    0.2478    0.8440    0.5018
      0.9273    0.5957    0.4603    0.6310    0.9422    0.4810    0.4178    0.2344
      0.8092    0.3536    0.3445    0.2149    0.8067    0.3659    0.6539    0.7786
      0.9468    0.8397    0.9153    0.7511    0.6382    0.8831    0.7924    0.5958
   
   B = 
   
      0.8690    0.5487    0.2260    0.3886    0.1846    0.3121    0.5921    0.3316
      0.1144    0.0700    0.8452    0.9138    0.5723    0.5503    0.3425    0.1530
      0.7437    0.4354    0.9735    0.1508    0.2486    0.7476    0.1216    0.0763
      0.9472    0.6114    0.2319    0.6390    0.3605    0.6403    0.9357    0.4837
      0.6486    0.6576    0.8745    0.2491    0.9836    0.9417    0.9147    0.5440
      0.5117    0.4543    0.6326    0.5372    0.4179    0.0086    0.6242    0.6570
      0.2870    0.0045    0.5155    0.6232    0.6422    0.6356    0.2907    0.2927
      0.2757    0.7825    0.8750    0.7909    0.8387    0.5809    0.1542    0.8265
   
   C = 
   
      1.7150    1.3979    1.6790    1.1338    1.1065    1.2561    1.1387    1.0880
      2.1522    1.7517    2.6677    2.1773    1.8115    2.1278    1.6875    1.4236
      2.0433    1.8393    2.0150    1.9415    1.7652    1.4358    1.7611    1.8812
      1.2122    0.8980    1.0083    1.0322    0.7761    1.0077    1.0150    0.7294
      2.2243    1.7233    2.7220    2.4713    2.4307    2.6577    2.1703    1.7309
      2.8557    2.1602    2.8561    2.3162    2.4467    2.6586    2.7191    1.8836
      2.3162    2.0592    2.8222    2.2478    2.5342    2.4731    2.1196    1.9669
      3.5684    2.7267    4.0353    3.3516    3.1590    3.3812    3.1195    2.5274
   
   D = 
   
      1.7150    1.3979    1.6790    1.1338    1.1065    1.2561    1.1387    1.0880
      2.1522    1.7517    2.6677    2.1773    1.8115    2.1278    1.6875    1.4236
      2.0433    1.8393    2.0150    1.9415    1.7652    1.4358    1.7611    1.8812
      1.2122    0.8980    1.0083    1.0322    0.7761    1.0077    1.0150    0.7294
      2.2243    1.7233    2.7220    2.4713    2.4307    2.6577    2.1703    1.7309
      2.8557    2.1602    2.8561    2.3162    2.4467    2.6586    2.7191    1.8836
      2.3162    2.0592    2.8222    2.2478    2.5342    2.4731    2.1196    1.9669
      3.5684    2.7267    4.0353    3.3516    3.1590    3.3812    3.1195    2.5274
   


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

   
      0.0874    0.4365    0.9381    0.4141    0.5911    0.6772
      0.3007    0.8770    0.8158    0.1049    0.1208    0.5101
      0.9992    0.6652    0.5893    0.6585    0.9244    0.2908
      0.1113    0.7509    0.8105    0.5211    0.6527    0.8984
      0.1615    0.3100    0.4109    0.7328    0.0273    0.4788
   
   
      0.9992
      0.8770
      0.6652
      0.7509
      0.9381
      0.8158
      0.5893
      0.8105
      0.6585
      0.5211
      0.7328
      0.5911
      0.9244
      0.6527
      0.6772
      0.5101
      0.8984
   

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

   
      6.2589    4.1678    9.8165    6.4637    0.3040    9.6251
      6.5362    5.4965    3.5801    9.4855    7.9993    8.3931
      3.5972    7.9708    9.1154    5.8255    4.8903    5.5626
      9.9664    9.3261    8.1159    3.0770    3.3570    7.0185
      4.9171    1.7012    3.4569    5.1755    7.7909    3.8213
   
   
      6.2589    0.0000    9.8165    6.4637    0.0000    9.6251
      6.5362    5.4965    0.0000    9.4855    7.9993    8.3931
      0.0000    7.9708    9.1154    5.8255    0.0000    5.5626
      9.9664    9.3261    8.1159    0.0000    0.0000    7.0185
      0.0000    0.0000    0.0000    5.1755    7.7909    0.0000
   
   
      6.2589    0.0000       NaN    6.4637    0.0000       NaN
      6.5362    5.4965    0.0000       NaN    7.9993    8.3931
      0.0000    7.9708       NaN    5.8255    0.0000    5.5626
         NaN       NaN    8.1159    0.0000    0.0000    7.0185
      0.0000    0.0000    0.0000    5.1755    7.7909    0.0000
   

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

   
      6.5000    8.1711    0.9522    6.5000    4.5880    0.1417
      9.8543    8.0675    2.9539    0.4889    4.4365    6.5000
      6.5000    6.5000    0.3311    8.5751    6.5000    4.2673
      8.5816    9.6858    6.5000    8.8905    2.4791    6.5000
      0.4989    1.6313    6.5000    3.8279    6.5000    6.5000
   
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
   
