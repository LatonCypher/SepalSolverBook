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
      0.8485    0.5261    0.4627    0.5745
   
   R1[2] = 0.46267069196877375
   C1 = 
      0.0109
      0.7080
      0.9729
      0.6481
      0.5439
      0.3943
      0.5091
      0.3145
   
   C1[5] = 0.3942972407255052

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
      0.5035    0.6597    0.2340    0.1421    0.7687
      0.5297    0.5648    0.9042    0.7047    0.0154
   

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
   
      0.0928    0.3771    0.4756    0.1202    0.7897    0.6688    0.3737    0.6070
      0.4421    0.1570    0.6106    0.2318    0.1311    0.1250    0.9940    0.2705
      0.4298    0.2335    0.4890    0.5982    0.4721    0.2029    0.6479    0.3442
      0.1655    0.4335    0.1762    0.5716    0.5006    0.5762    0.9455    0.2776
      0.1028    0.2463    0.1403    0.8892    0.8273    0.8020    0.5879    0.0338
      0.7793    0.8321    0.7508    0.2699    0.4851    0.5989    0.0952    0.9682
      0.9067    0.6818    0.6774    0.9469    0.5358    0.6309    0.4982    0.9095
      0.7414    0.6052    0.5690    0.0107    0.6191    0.4415    0.6927    0.4358
   
   B = 
   
      0.3517    0.2100    0.7209    0.2179    0.7434    0.2063    0.8086    0.7497
      0.6773    0.4434    0.5181    0.6192    0.9238    0.0447    0.6867    0.3292
      0.2736    0.8354    0.1270    0.4490    0.4104    0.4399    0.3205    0.3396
      0.0001    0.8636    0.8848    0.3067    0.1327    0.1430    0.1041    0.5608
      0.9879    0.3348    0.5414    0.4137    0.2999    0.8156    0.9036    0.9155
      0.2104    0.3591    0.4444    0.7635    0.4156    0.7100    0.6590    0.3738
      0.5447    0.3160    0.5767    0.1543    0.8533    0.7250    0.7341    0.9155
      0.6416    0.6088    0.2971    0.2109    0.0724    0.3449    0.5638    0.7900
   
   C = 
   
      1.9320    1.6799    1.5496    1.5270    1.5060    1.8616    2.2697    2.2172
      1.2997    1.4403    1.4628    0.8988    1.7140    1.4095    1.7681    2.0110
      1.5260    1.7642    1.8440    1.1640    1.6190    1.5173    1.9569    2.2738
      1.7089    1.7101    2.0268    1.4103    1.8881    1.8113    2.2301    2.4057
      1.5694    1.7873    2.1597    1.5630    1.5650    1.9031    2.1165    2.3266
      2.3214    2.3900    2.1985    1.9818    2.2377    1.7907    2.9190    2.7847
      2.4830    2.9934    3.0588    2.1863    2.6213    2.2107    3.2954    3.5663
      2.1877    1.7586    1.9901    1.5870    2.3369    1.9027    2.8031    2.6646
   
   D = 
   
      1.9320    1.6799    1.5496    1.5270    1.5060    1.8616    2.2697    2.2172
      1.2997    1.4403    1.4628    0.8988    1.7140    1.4095    1.7681    2.0110
      1.5260    1.7642    1.8440    1.1640    1.6190    1.5173    1.9569    2.2738
      1.7089    1.7101    2.0268    1.4103    1.8881    1.8113    2.2301    2.4057
      1.5694    1.7873    2.1597    1.5630    1.5650    1.9031    2.1165    2.3266
      2.3214    2.3900    2.1985    1.9818    2.2377    1.7907    2.9190    2.7847
      2.4830    2.9934    3.0588    2.1863    2.6213    2.2107    3.2954    3.5663
      2.1877    1.7586    1.9901    1.5870    2.3369    1.9027    2.8031    2.6646
   


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

   
      0.2764    0.3322    0.4311    0.5830    0.0372    0.0299
      0.4405    0.8635    0.5347    0.5333    0.5460    0.5536
      0.9430    0.2755    0.8428    0.2628    0.3186    0.8577
      0.5355    0.5543    0.5816    0.2652    0.8271    0.2409
      0.5576    0.0338    0.9538    0.6935    0.6612    0.7859
   
   
      0.9430
      0.5355
      0.5576
      0.8635
      0.5543
      0.5347
      0.8428
      0.5816
      0.9538
      0.5830
      0.5333
      0.6935
      0.5460
      0.8271
      0.6612
      0.5536
      0.8577
      0.7859
   

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

   
      1.4618    9.4506    9.1166    9.1620    7.8712    4.3645
      1.9527    7.4319    4.6303    6.7166    6.6290    3.6435
      3.7598    6.1118    9.1513    6.2284    9.8339    0.3251
      3.8356    0.4403    7.4657    4.6262    7.6160    9.1582
      0.0638    1.6694    2.0595    6.0160    1.5253    6.9087
   
   
      0.0000    9.4506    9.1166    9.1620    7.8712    0.0000
      0.0000    7.4319    0.0000    6.7166    6.6290    0.0000
      0.0000    6.1118    9.1513    6.2284    9.8339    0.0000
      0.0000    0.0000    7.4657    0.0000    7.6160    9.1582
      0.0000    0.0000    0.0000    6.0160    0.0000    6.9087
   
   
      0.0000       NaN       NaN       NaN    7.8712    0.0000
      0.0000    7.4319    0.0000    6.7166    6.6290    0.0000
      0.0000    6.1118       NaN    6.2284       NaN    0.0000
      0.0000    0.0000    7.4657    0.0000    7.6160       NaN
      0.0000    0.0000    0.0000    6.0160    0.0000    6.9087
   

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

   
      6.5000    1.7851    2.7854    0.3040    0.9367    9.9285
      8.2167    6.5000    3.4833    8.9164    4.3442    9.3010
      9.0306    8.6167    2.6633    8.9435    6.5000    0.8309
      6.5000    2.6160    8.9653    6.5000    9.6330    2.5941
      6.5000    2.9084    9.6217    9.6871    9.4447    1.6251
   
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
   
