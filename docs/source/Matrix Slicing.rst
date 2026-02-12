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
      0.1833    0.3300    0.8747    0.8059
   
   R1[2] = 0.8747213568060567
   C1 = 
      0.1725
      0.8181
      0.6986
      0.4615
      0.2088
      0.1571
      0.1751
      0.3822
   
   C1[5] = 0.15712516391702602

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
      0.9073    0.0496    0.6640    0.7201    0.1699
      0.4574    0.6163    0.3785    0.3630    0.0367
   

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
   
      0.5323    0.1442    0.3558    0.1729    0.2339    0.9139    0.3871    0.0332
      0.4438    0.6747    0.3469    0.6040    0.8897    0.5293    0.8506    0.8094
      0.8780    0.9088    0.8865    0.1353    0.6505    0.7222    0.1971    0.1184
      0.0686    0.4559    0.0164    0.3473    0.7942    0.7519    0.8522    0.4948
      0.2856    0.4092    0.1894    0.9672    0.6560    0.2043    0.0350    0.9335
      0.9903    0.6669    0.9782    0.7815    0.8213    0.5144    0.3504    0.3128
      0.6545    0.8940    0.0267    0.0029    0.5400    0.7850    0.1895    0.6207
      0.3391    0.1675    0.3721    0.5787    0.4853    0.3159    0.7009    0.1070
   
   B = 
   
      0.9870    0.9240    0.6782    0.4629    0.0038    0.2267    0.4983    0.4553
      0.1051    0.8291    0.8437    0.5437    0.5503    0.1099    0.4520    0.5287
      0.7871    0.4068    0.1571    0.2010    0.3318    0.9443    0.4179    0.2669
      0.7911    0.2147    0.1969    0.9865    0.4774    0.4475    0.5199    0.6864
      0.6726    0.0667    0.4574    0.5886    0.0244    0.5713    0.0309    0.3079
      0.6543    0.2701    0.0311    0.8325    0.1294    0.8168    0.1102    0.0611
      0.9671    0.6969    0.8360    0.1451    0.5987    0.8785    0.2082    0.5197
      0.4804    0.9045    0.3301    0.0845    0.1863    0.2931    0.1203    0.7773
   
   C = 
   
      2.1031    1.3556    1.0427    1.5244    0.6439    1.7799    0.7615    0.8872
      3.4161    2.7675    2.4454    2.3939    1.5266    2.6977    1.3453    2.4436
      2.9246    2.4374    2.0520    2.2350    1.1116    2.3661    1.4440    1.6487
      2.4913    1.8201    1.7645    1.8844    1.1414    2.1980    0.7719    1.6331
      2.2963    1.8556    1.4029    1.9870    0.9881    1.5674    1.0716    2.0192
      3.8139    2.7546    2.3297    2.7775    1.4231    2.8601    1.8026    2.3107
      2.1216    2.2990    1.8376    1.8485    0.8485    1.5713    0.9601    1.5750
      2.3654    1.4307    1.3968    1.5531    0.9855    1.8880    0.9096    1.3557
   
   D = 
   
      2.1031    1.3556    1.0427    1.5244    0.6439    1.7799    0.7615    0.8872
      3.4161    2.7675    2.4454    2.3939    1.5266    2.6977    1.3453    2.4436
      2.9246    2.4374    2.0520    2.2350    1.1116    2.3661    1.4440    1.6487
      2.4913    1.8201    1.7645    1.8844    1.1414    2.1980    0.7719    1.6331
      2.2963    1.8556    1.4029    1.9870    0.9881    1.5674    1.0716    2.0192
      3.8139    2.7546    2.3297    2.7775    1.4231    2.8601    1.8026    2.3107
      2.1216    2.2990    1.8376    1.8485    0.8485    1.5713    0.9601    1.5750
      2.3654    1.4307    1.3968    1.5531    0.9855    1.8880    0.9096    1.3557
   


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

   
      0.4209    0.9409    0.6761    0.0136    0.4485    0.9502
      0.9762    0.4814    0.6814    0.8952    0.7437    0.5665
      0.7808    0.0037    0.8172    0.3610    0.1061    0.7181
      0.7147    0.4543    0.6539    0.9360    0.3314    0.1930
      0.4056    0.4112    0.9237    0.4445    0.4897    0.7572
   
   
      0.9762
      0.7808
      0.7147
      0.9409
      0.6761
      0.6814
      0.8172
      0.6539
      0.9237
      0.8952
      0.9360
      0.7437
      0.9502
      0.5665
      0.7181
      0.7572
   

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

   
      3.8760    2.4722    1.2925    2.3557    1.5576    5.2240
      5.0781    8.9970    1.1133    3.1392    4.6659    0.6744
      4.8904    1.9973    4.2284    7.9620    9.3985    5.1724
      3.6541    9.8806    8.8015    8.3163    3.6629    9.1238
      0.8071    8.1440    0.1697    4.8718    6.8769    7.8684
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    5.2240
      5.0781    8.9970    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.9620    9.3985    5.1724
      0.0000    9.8806    8.8015    8.3163    0.0000    9.1238
      0.0000    8.1440    0.0000    0.0000    6.8769    7.8684
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    5.2240
      5.0781    8.9970    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.9620       NaN    5.1724
      0.0000       NaN    8.8015    8.3163    0.0000       NaN
      0.0000    8.1440    0.0000    0.0000    6.8769    7.8684
   

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

   
      2.5846    9.2468    6.5000    6.5000    3.8566    6.5000
      2.1118    6.5000    8.7520    6.5000    0.6650    4.4272
      6.5000    6.5000    1.0986    2.7199    1.6317    0.4227
      8.7264    4.7472    6.5000    6.5000    9.8756    6.5000
      6.5000    6.5000    1.6341    6.5000    1.3888    8.9077
   
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
   
