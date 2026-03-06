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
      0.4139    0.0744    0.1091    0.0822
   
   R1[2] = 0.10914041655758633
   C1 = 
      0.9386
      0.1392
      0.7729
      0.4652
      0.2837
      0.3199
      0.2039
      0.9557
   
   C1[5] = 0.319852767396708

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
      0.6347    0.4622    0.8311    0.6749    0.2465
      0.6424    0.8039    0.5621    0.2173    0.4029
   

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
   
      0.0884    0.2923    0.2619    0.7981    0.6041    0.2623    0.8607    0.9114
      0.6513    0.3487    0.4413    0.4689    0.9963    0.4843    0.6754    0.8529
      0.9136    0.9937    0.1572    0.9871    0.6018    0.1983    0.0185    0.7899
      0.3149    0.2553    0.7271    0.4736    0.7098    0.7151    0.8244    0.1262
      0.1857    0.9280    0.9373    0.8708    0.3721    0.0464    0.5783    0.3962
      0.8596    0.4125    0.6214    0.5871    0.2706    0.1193    0.1760    0.0856
      0.8078    0.3557    0.0435    0.4288    0.7902    0.5700    0.9237    0.4492
      0.0102    0.9282    0.7780    0.1223    0.1839    0.3332    0.2496    0.8773
   
   B = 
   
      0.1293    0.7248    0.5352    0.8990    0.7589    0.7377    0.3365    0.8485
      0.7219    0.2249    0.7865    0.2643    0.8830    0.5973    0.8280    0.2081
      0.2949    0.9202    0.6376    0.0551    0.6821    0.1434    0.6283    0.9756
      0.2873    0.3291    0.6590    0.3093    0.7765    0.3583    0.8222    0.5332
      0.7854    0.0220    0.1838    0.4812    0.6166    0.6456    0.1928    0.7999
      0.4089    0.5576    0.2113    0.9504    0.0900    0.9382    0.6979    0.5698
      0.9515    0.2293    0.7087    0.2095    0.5128    0.8132    0.4781    0.4016
      0.3175    0.1554    0.8567    0.4842    0.0221    0.2975    0.5108    0.0560
   
   C = 
   
      2.2190    1.1319    2.5274    1.5795    1.9811    2.1705    2.2691    1.8463
      2.4947    1.6901    2.7079    2.3411    2.4903    2.8206    2.4593    2.6977
      1.9875    1.6059    2.8636    2.2622    2.8603    2.4683    2.7073    2.3078
      2.2498    1.7334    2.1189    1.7920    2.2556    2.4958    2.2581    2.5956
      2.2079    1.7206    2.8284    1.2693    2.8149    2.0097    2.7188    2.3081
      1.2168    1.6069    1.8406    1.4195    2.1660    1.6352    1.7672    2.0944
      2.3724    1.4634    2.3276    2.2881    2.3119    2.8980    2.1675    2.3835
      1.7328    1.3557    2.3449    1.2174    1.7438    1.6127    2.1969    1.5125
   
   D = 
   
      2.2190    1.1319    2.5274    1.5795    1.9811    2.1705    2.2691    1.8463
      2.4947    1.6901    2.7079    2.3411    2.4903    2.8206    2.4593    2.6977
      1.9875    1.6059    2.8636    2.2622    2.8603    2.4683    2.7073    2.3078
      2.2498    1.7334    2.1189    1.7920    2.2556    2.4958    2.2581    2.5956
      2.2079    1.7206    2.8284    1.2693    2.8149    2.0097    2.7188    2.3081
      1.2168    1.6069    1.8406    1.4195    2.1660    1.6352    1.7672    2.0944
      2.3724    1.4634    2.3276    2.2881    2.3119    2.8980    2.1675    2.3835
      1.7328    1.3557    2.3449    1.2174    1.7438    1.6127    2.1969    1.5125
   


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

   
      0.8947    0.9370    0.7710    0.4386    0.6543    0.9298
      0.8102    0.1926    0.2518    0.8502    0.9051    0.4574
      0.7673    0.0799    0.6510    0.3696    0.7661    0.1865
      0.4508    0.8330    0.0108    0.3909    0.5403    0.7066
      0.5498    0.0921    0.1275    0.0443    0.0206    0.5360
   
   
      0.8947
      0.8102
      0.7673
      0.5498
      0.9370
      0.8330
      0.7710
      0.6510
      0.8502
      0.6543
      0.9051
      0.7661
      0.5403
      0.9298
      0.7066
      0.5360
   

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

   
      2.5475    9.5398    0.2427    4.1438    9.2230    6.4289
      8.6177    8.9178    7.8778    3.1735    4.5486    4.6902
      5.1024    5.0744    4.5371    0.2367    2.3476    9.3063
      6.6834    7.5953    1.1353    4.0200    9.2559    6.6455
      2.7210    2.8495    5.6622    8.8869    4.4589    0.3365
   
   
      0.0000    9.5398    0.0000    0.0000    9.2230    6.4289
      8.6177    8.9178    7.8778    0.0000    0.0000    0.0000
      5.1024    5.0744    0.0000    0.0000    0.0000    9.3063
      6.6834    7.5953    0.0000    0.0000    9.2559    6.6455
      0.0000    0.0000    5.6622    8.8869    0.0000    0.0000
   
   
      0.0000       NaN    0.0000    0.0000       NaN    6.4289
      8.6177    8.9178    7.8778    0.0000    0.0000    0.0000
      5.1024    5.0744    0.0000    0.0000    0.0000       NaN
      6.6834    7.5953    0.0000    0.0000       NaN    6.6455
      0.0000    0.0000    5.6622    8.8869    0.0000    0.0000
   

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

   
      2.1591    0.7262    6.5000    0.7700    4.9131    4.4102
      6.5000    1.5508    8.9261    8.9063    9.7814    0.5960
      1.6369    6.5000    3.9165    6.5000    0.8723    3.7617
      4.9883    6.5000    9.8462    6.5000    1.6314    1.8680
      9.0927    3.0722    1.7891    6.5000    3.9974    2.8077
   
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
   
