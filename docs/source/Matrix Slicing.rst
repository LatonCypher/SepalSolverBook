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
      0.6910    0.1491    0.2526    0.7038
   
   R1[2] = 0.25258918673400943
   C1 = 
      0.5815
      0.1006
      0.4320
      0.9551
      0.1742
      0.2117
      0.6506
      0.4558
   
   C1[5] = 0.21173832728381536

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
      0.9084    0.5515    0.8485    0.8953    0.7464
      0.8767    0.9915    0.2255    0.5792    0.4795
   

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
   
      0.3235    0.3896    0.4509    0.2894    0.5666    0.9861    0.5258    0.6400
      0.3929    0.8322    0.0804    0.0282    0.4395    0.8967    0.1636    0.4431
      0.7422    0.5376    0.4961    0.3420    0.0469    0.0429    0.2680    0.2517
      0.7590    0.4582    0.4154    0.7836    0.3961    0.7889    0.9450    0.4868
      0.9290    0.0492    0.6443    0.5137    0.3530    0.6845    0.5300    0.5733
      0.9814    0.9193    0.2793    0.0467    0.0556    0.3711    0.7489    0.7164
      0.1756    0.3025    0.3498    0.9482    0.9273    0.5749    0.2506    0.2393
      0.2144    0.4506    0.9004    0.9263    0.4069    0.2120    0.0473    0.0216
   
   B = 
   
      0.8155    0.0518    0.8727    0.5952    0.2019    0.7480    0.5331    0.8503
      0.9452    0.0797    0.3885    0.6164    0.1379    0.1937    0.0821    0.2068
      0.7883    0.3375    0.5494    0.9326    0.0494    0.2585    0.6859    0.5835
      0.3995    0.6515    0.8335    0.9580    0.5478    0.5849    0.0989    0.8597
      0.1940    0.5912    0.1579    0.2329    0.3579    0.6963    0.0723    0.8605
      0.9588    0.5851    0.7653    0.3063    0.3273    0.9834    0.2499    0.1576
      0.0471    0.8327    0.4683    0.4031    0.2480    0.8388    0.1676    0.6581
      0.5760    0.5780    0.4814    0.0010    0.0115    0.1910    0.0266    0.0397
   
   C = 
   
      2.5518    2.1083    2.3210    1.7771    0.9631    2.5307    0.9349    1.8819
      2.3896    1.3092    1.7795    1.2923    0.7100    1.9021    0.6309    1.2222
      1.8489    0.8931    1.7011    1.6958    0.5361    1.3353    0.8797    1.5592
      2.8507    2.4906    3.0648    2.5877    1.3061    3.1594    1.2018    2.7625
      2.5971    1.9860    2.7156    2.1824    0.9960    2.6444    1.2927    2.4008
      2.7224    1.5364    2.3942    1.8852    0.6996    2.1802    1.0362    1.8552
      1.9645    2.0007    2.0722    2.0189    1.1988    2.3019    0.7112    2.2941
      1.9775    1.3709    1.8879    2.3113    0.8843    1.5578    0.9514    2.0127
   
   D = 
   
      2.5518    2.1083    2.3210    1.7771    0.9631    2.5307    0.9349    1.8819
      2.3896    1.3092    1.7795    1.2923    0.7100    1.9021    0.6309    1.2222
      1.8489    0.8931    1.7011    1.6958    0.5361    1.3353    0.8797    1.5592
      2.8507    2.4906    3.0648    2.5877    1.3061    3.1594    1.2018    2.7625
      2.5971    1.9860    2.7156    2.1824    0.9960    2.6444    1.2927    2.4008
      2.7224    1.5364    2.3942    1.8852    0.6996    2.1802    1.0362    1.8552
      1.9645    2.0007    2.0722    2.0189    1.1988    2.3019    0.7112    2.2941
      1.9775    1.3709    1.8879    2.3113    0.8843    1.5578    0.9514    2.0127
   


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

   
      0.3845    0.4664    0.2243    0.9665    0.8180    0.4504
      0.6782    0.2042    0.9143    0.5757    0.8522    0.4398
      0.6948    0.1170    0.1731    0.2410    0.6543    0.8716
      0.0805    0.6743    0.3685    0.5553    0.6717    0.6730
      0.9854    0.7060    0.3610    0.3859    0.8887    0.9982
   
   
      0.6782
      0.6948
      0.9854
      0.6743
      0.7060
      0.9143
      0.9665
      0.5757
      0.5553
      0.8180
      0.8522
      0.6543
      0.6717
      0.8887
      0.8716
      0.6730
      0.9982
   

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

   
      3.6453    5.3253    0.4054    5.6754    8.0833    4.3804
      3.1009    1.5446    2.8997    5.0754    6.1012    3.5845
      4.1078    1.6036    4.6351    8.7879    1.0997    0.5866
      1.0943    9.5522    2.0530    4.0450    4.7618    9.0840
      7.2587    7.8663    4.4230    0.9137    4.8862    5.5430
   
   
      0.0000    5.3253    0.0000    5.6754    8.0833    0.0000
      0.0000    0.0000    0.0000    5.0754    6.1012    0.0000
      0.0000    0.0000    0.0000    8.7879    0.0000    0.0000
      0.0000    9.5522    0.0000    0.0000    0.0000    9.0840
      7.2587    7.8663    0.0000    0.0000    0.0000    5.5430
   
   
      0.0000    5.3253    0.0000    5.6754    8.0833    0.0000
      0.0000    0.0000    0.0000    5.0754    6.1012    0.0000
      0.0000    0.0000    0.0000    8.7879    0.0000    0.0000
      0.0000       NaN    0.0000    0.0000    0.0000       NaN
      7.2587    7.8663    0.0000    0.0000    0.0000    5.5430
   

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

   
      8.1372    6.5000    1.6178    6.5000    4.1571    8.9647
      4.7307    1.1832    1.3336    3.2589    6.5000    8.7764
      0.8092    9.1975    6.5000    6.5000    2.6540    1.2022
      8.4676    2.5801    6.5000    6.5000    9.4646    1.7220
      1.0893    6.5000    1.8548    6.5000    3.6431    2.8084
   
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
   
