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
      0.6431    0.7788    0.0097    0.7023
   
   R1[2] = 0.009742879950750627
   C1 = 
      0.8799
      0.7405
      0.5810
      0.0863
      0.9636
      0.5778
      0.9320
      0.9935
   
   C1[5] = 0.5777556053711285

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
      0.8494    0.6154    0.6671    0.2285    0.9780
      0.4532    0.1315    0.8957    0.7954    0.9354
   

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
   
      0.6521    0.8788    0.1524    0.1815    0.6861    0.0934    0.3134    0.5650
      0.6946    0.2450    0.8489    0.3480    0.5059    0.5505    0.2054    0.5270
      0.4450    0.2571    0.0577    0.8171    0.7602    0.5524    0.3634    0.3350
      0.1070    0.8901    0.9084    0.7298    0.5760    0.4522    0.0802    0.2093
      0.2460    0.4307    0.7940    0.8756    0.2492    0.6316    0.9186    0.3450
      0.4626    0.9183    0.0628    0.5038    0.8234    0.1447    0.6854    0.5747
      0.3610    0.8761    0.6376    0.7344    0.7903    0.1623    0.4649    0.5813
      0.0994    0.7320    0.1389    0.3591    0.2325    0.3135    0.4363    0.5773
   
   B = 
   
      0.5204    0.3318    0.1684    0.7071    0.7501    0.1310    0.2127    0.7363
      0.1165    0.4501    0.8507    0.1735    0.0224    0.7562    0.6465    0.2605
      0.4327    0.9426    0.3441    0.8546    0.1610    0.7660    0.6277    0.4985
      0.4245    0.7845    0.2412    0.4037    0.9876    0.7376    0.2880    0.9472
      0.5227    0.2129    0.7011    0.0917    0.6178    0.0737    0.7558    0.5128
      0.8341    0.0615    0.4737    0.5542    0.7307    0.5545    0.3872    0.3700
      0.3245    0.0493    0.9117    0.2095    0.0173    0.6770    0.7795    0.3453
      0.0290    0.0607    0.4536    0.7626    0.2348    0.2833    0.7382    0.8727
   
   C = 
   
      1.1393    1.0993    2.0209    1.4283    1.3428    1.4751    2.0708    1.9445
      1.7107    1.5975    1.7433    2.1961    1.8490    1.8141    2.0839    2.3220
      1.6192    1.1927    1.7885    1.4458    2.1140    1.6028    1.8514    2.2093
      1.5726    2.0319    2.0500    1.7809    1.7039    2.3280    2.2061    2.1278
      1.8587    1.8688    2.3601    2.1091    1.8991    2.7001    2.4849    2.4982
      1.3789    1.2740    2.5336    1.4810    1.6365    1.9425    2.5134    2.3022
      1.5938    1.9275    2.5210    1.9517    1.8697    2.3674    2.7065    2.6405
      0.8909    0.9003    1.7450    1.1877    0.9838    1.5877    1.7483    1.5629
   
   D = 
   
      1.1393    1.0993    2.0209    1.4283    1.3428    1.4751    2.0708    1.9445
      1.7107    1.5975    1.7433    2.1961    1.8490    1.8141    2.0839    2.3220
      1.6192    1.1927    1.7885    1.4458    2.1140    1.6028    1.8514    2.2093
      1.5726    2.0319    2.0500    1.7809    1.7039    2.3280    2.2061    2.1278
      1.8587    1.8688    2.3601    2.1091    1.8991    2.7001    2.4849    2.4982
      1.3789    1.2740    2.5336    1.4810    1.6365    1.9425    2.5134    2.3022
      1.5938    1.9275    2.5210    1.9517    1.8697    2.3674    2.7065    2.6405
      0.8909    0.9003    1.7450    1.1877    0.9838    1.5877    1.7483    1.5629
   


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

   
      0.3998    0.7568    0.1542    0.9999    0.1914    0.5744
      0.3148    0.3840    0.5955    0.3440    0.2593    0.2281
      0.1135    0.6448    0.8764    0.3444    0.1097    0.6199
      0.7601    0.1911    0.6475    0.5352    0.3366    0.1980
      0.6015    0.3917    0.1964    0.2710    0.6094    0.4197
   
   
      0.7601
      0.6015
      0.7568
      0.6448
      0.5955
      0.8764
      0.6475
      0.9999
      0.5352
      0.6094
      0.5744
      0.6199
   

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

   
      9.9992    7.2708    6.0881    9.2643    8.2474    0.8778
      0.0707    6.5217    7.3741    6.2474    0.7989    0.8814
      2.0921    6.6414    0.2566    4.2591    8.3845    3.6161
      7.5158    0.0229    7.7459    9.6978    6.3509    0.7316
      5.5097    1.6798    4.7330    5.6450    2.3339    6.8083
   
   
      9.9992    7.2708    6.0881    9.2643    8.2474    0.0000
      0.0000    6.5217    7.3741    6.2474    0.0000    0.0000
      0.0000    6.6414    0.0000    0.0000    8.3845    0.0000
      7.5158    0.0000    7.7459    9.6978    6.3509    0.0000
      5.5097    0.0000    0.0000    5.6450    0.0000    6.8083
   
   
         NaN    7.2708    6.0881       NaN    8.2474    0.0000
      0.0000    6.5217    7.3741    6.2474    0.0000    0.0000
      0.0000    6.6414    0.0000    0.0000    8.3845    0.0000
      7.5158    0.0000    7.7459       NaN    6.3509    0.0000
      5.5097    0.0000    0.0000    5.6450    0.0000    6.8083
   

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

   
      1.9854    9.8886    6.5000    1.8892    1.0904    8.3998
      2.4130    3.4225    9.6998    4.5676    9.3877    6.5000
      2.5743    2.1569    6.5000    2.1337    6.5000    8.3555
      6.5000    3.5664    1.9364    6.5000    6.5000    3.4177
      3.0203    0.2856    0.1700    6.5000    6.5000    6.5000
   
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
   
