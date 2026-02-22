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
      0.6913    0.6963    0.4985    0.9480
   
   R1[2] = 0.4984549144352013
   C1 = 
      0.3767
      0.7768
      0.3675
      0.3498
      0.1998
      0.2967
      0.5871
      0.4076
   
   C1[5] = 0.2967316764804119

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
      0.8543    0.7439    0.4180    0.6504    0.3361
      0.1311    0.0052    0.6541    0.3204    0.6269
   

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
   
      0.2680    0.0973    0.6891    0.9242    0.2906    0.2221    0.7532    0.1657
      0.6124    0.3048    0.8316    0.7963    0.9624    0.0778    0.5574    0.6492
      0.0056    0.6435    0.8105    0.9247    0.3028    0.0950    0.9853    0.6437
      0.3237    0.0763    0.9727    0.8431    0.4360    0.7772    0.6197    0.4387
      0.6934    0.6677    0.0000    0.4951    0.5706    0.1667    0.9529    0.6517
      0.6981    0.4873    0.3512    0.5396    0.4342    0.0336    0.3477    0.0670
      0.5747    0.1534    0.1148    0.1994    0.5221    0.2879    0.3916    0.0758
      0.0534    0.3147    0.5152    0.2018    0.0044    0.1862    0.1526    0.8163
   
   B = 
   
      0.6181    0.1963    0.0641    0.4534    0.0417    0.6149    0.7295    0.6214
      0.9510    0.6033    0.9827    0.6724    0.9685    0.0347    0.3700    0.8412
      0.6760    0.7763    0.3801    0.6987    0.5038    0.6840    0.1070    0.5294
      0.8625    0.2939    0.0838    0.1812    0.9136    0.6977    0.8647    0.6582
      0.7884    0.8751    0.9839    0.5053    0.0144    0.8207    0.5716    0.7119
      0.6703    0.9613    0.6943    0.8504    0.6004    0.4618    0.6756    0.0949
      0.6583    0.5404    0.5948    0.4178    0.0058    0.6758    0.5622    0.8609
      0.3956    0.9837    0.2656    0.0377    0.8835    0.9135    0.8507    0.3979
   
   C = 
   
      2.4606    1.9558    1.3843    1.4926    1.5852    2.2859    1.9850    2.1638
      3.3520    3.0405    2.2265    2.0178    2.1045    3.3071    2.8053    3.0320
      3.1666    2.8123    2.1392    1.8388    2.5124    2.7717    2.4672    2.9114
      3.1036    3.0077    1.9898    2.1872    2.2117    2.9918    2.5933    2.4275
      2.9373    2.5000    2.1591    1.7059    1.8175    2.5796    2.7098    2.8201
      2.2180    1.5284    1.3775    1.3832    1.2586    1.7311    1.7171    2.0232
      1.6430    1.3729    1.2145    1.1552    0.6621    1.4717    1.4383    1.4445
      1.4062    1.7278    0.9666    0.8874    1.5848    1.4754    1.2935    1.1804
   
   D = 
   
      2.4606    1.9558    1.3843    1.4926    1.5852    2.2859    1.9850    2.1638
      3.3520    3.0405    2.2265    2.0178    2.1045    3.3071    2.8053    3.0320
      3.1666    2.8123    2.1392    1.8388    2.5124    2.7717    2.4672    2.9114
      3.1036    3.0077    1.9898    2.1872    2.2117    2.9918    2.5933    2.4275
      2.9373    2.5000    2.1591    1.7059    1.8175    2.5796    2.7098    2.8201
      2.2180    1.5284    1.3775    1.3832    1.2586    1.7311    1.7171    2.0232
      1.6430    1.3729    1.2145    1.1552    0.6621    1.4717    1.4383    1.4445
      1.4062    1.7278    0.9666    0.8874    1.5848    1.4754    1.2935    1.1804
   


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

   
      0.3198    0.5796    0.3421    0.9244    0.3040    0.3454
      0.4309    0.4186    0.0054    0.0275    0.9645    0.6427
      0.9321    0.3498    0.3253    0.5143    0.6269    0.6898
      0.2083    0.2269    0.6474    0.8959    0.9467    0.1060
      0.3500    0.3205    0.9660    0.2300    0.6878    0.4206
   
   
      0.9321
      0.5796
      0.6474
      0.9660
      0.9244
      0.5143
      0.8959
      0.9645
      0.6269
      0.9467
      0.6878
      0.6427
      0.6898
   

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

   
      0.2318    8.2145    9.0955    5.8515    9.3930    7.5541
      1.7030    3.2513    8.9651    2.7246    0.9190    8.3309
      1.4429    3.8040    5.4664    1.2717    8.1411    5.6738
      7.5646    9.5482    2.6303    2.1996    1.5713    9.5513
      0.8545    4.8622    1.3555    8.1508    4.2045    2.3906
   
   
      0.0000    8.2145    9.0955    5.8515    9.3930    7.5541
      0.0000    0.0000    8.9651    0.0000    0.0000    8.3309
      0.0000    0.0000    5.4664    0.0000    8.1411    5.6738
      7.5646    9.5482    0.0000    0.0000    0.0000    9.5513
      0.0000    0.0000    0.0000    8.1508    0.0000    0.0000
   
   
      0.0000    8.2145       NaN    5.8515       NaN    7.5541
      0.0000    0.0000    8.9651    0.0000    0.0000    8.3309
      0.0000    0.0000    5.4664    0.0000    8.1411    5.6738
      7.5646       NaN    0.0000    0.0000    0.0000       NaN
      0.0000    0.0000    0.0000    8.1508    0.0000    0.0000
   

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

   
      6.5000    1.8477    0.3733    1.7824    6.5000    0.4114
      1.9529    6.5000    8.6732    1.6191    9.1773    0.5515
      2.1711    3.2326    9.2870    4.7556    6.5000    0.1022
      1.3800    4.8751    3.0527    6.5000    3.6122    2.3263
      1.9038    4.9474    4.0435    0.8454    6.5000    8.5195
   
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
   
