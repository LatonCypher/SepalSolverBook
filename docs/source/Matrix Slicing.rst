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
      0.7757    0.3875    0.6433    0.9236
   
   R1[2] = 0.6432596714614692
   C1 = 
      0.8368
      0.4012
      0.5848
      0.5216
      0.2396
      0.8820
      0.1756
      0.3950
   
   C1[5] = 0.8820350783326989

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
      0.5466    0.2987    0.7372    0.0468    0.5891
      0.5755    0.2798    0.7875    0.6855    0.0739
   

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
   
      0.2080    0.8902    0.4338    0.2141    0.1801    0.9316    0.6242    0.9538
      0.0136    0.8215    0.2987    0.6899    0.8436    0.0433    0.9244    0.0368
      0.7847    0.8327    0.9543    0.9706    0.1467    0.8969    0.2623    0.9577
      0.7699    0.6899    0.8613    0.2622    0.4174    0.6820    0.6740    0.9235
      0.4569    0.0125    0.0675    0.1070    0.7902    0.5559    0.4304    0.4969
      0.6627    0.9008    0.5152    0.5694    0.8553    0.7586    0.1726    0.5120
      0.1875    0.1534    0.9699    0.3463    0.3264    0.3714    0.0526    0.1406
      0.6450    0.8701    0.0012    0.2518    0.5773    0.2512    0.9739    0.0620
   
   B = 
   
      0.5420    0.6330    0.6958    0.9032    0.1794    0.3153    0.1466    0.4983
      0.4579    0.4349    0.3199    0.8955    0.5997    0.1767    0.2055    0.7708
      0.1693    0.2840    0.1307    0.3002    0.1917    0.6551    0.7849    0.7441
      0.4280    0.1142    0.6135    0.4833    0.8437    0.2704    0.0924    0.5304
      0.5045    0.9381    0.1475    0.8903    0.6126    0.8166    0.2014    0.0891
      0.7885    0.4908    0.8925    0.6413    0.8089    0.3384    0.6647    0.2946
      0.5792    0.8619    0.8007    0.6513    0.5034    0.4837    0.4294    0.8575
      0.5540    0.4628    0.9599    0.6571    0.4499    0.6837    0.1279    0.8778
   
   C = 
   
      2.4009    2.2721    2.8909    3.0098    2.4423    1.9813    1.6194    2.8892
      1.7448    2.1558    1.6732    2.5761    2.1682    1.7074    1.0694    2.1410
      2.8473    2.4877    3.4840    3.7159    3.0203    2.4870    1.9859    3.6006
      2.6415    2.7964    3.1261    3.5531    2.5003    2.5287    1.8999    3.3220
      1.6721    1.9410    1.8306    2.1626    1.5666    1.6006    0.9095    1.3837
      2.5158    2.5829    2.5988    3.5318    2.6931    2.2482    1.5555    2.6070
      1.0502    1.0993    1.0755    1.4208    1.1939    1.3291    1.2056    1.4241
      1.9437    2.3486    2.0304    2.8338    1.9252    1.4957    1.0070    2.1414
   
   D = 
   
      2.4009    2.2721    2.8909    3.0098    2.4423    1.9813    1.6194    2.8892
      1.7448    2.1558    1.6732    2.5761    2.1682    1.7074    1.0694    2.1410
      2.8473    2.4877    3.4840    3.7159    3.0203    2.4870    1.9859    3.6006
      2.6415    2.7964    3.1261    3.5531    2.5003    2.5287    1.8999    3.3220
      1.6721    1.9410    1.8306    2.1626    1.5666    1.6006    0.9095    1.3837
      2.5158    2.5829    2.5988    3.5318    2.6931    2.2482    1.5555    2.6070
      1.0502    1.0993    1.0755    1.4208    1.1939    1.3291    1.2056    1.4241
      1.9437    2.3486    2.0304    2.8338    1.9252    1.4957    1.0070    2.1414
   


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

   
      0.1710    0.7285    0.4498    0.6150    0.4397    0.3778
      0.7723    0.2485    0.1928    0.0382    0.8108    0.3497
      0.2813    0.4218    0.1068    0.2619    0.8217    0.4668
      0.7960    0.6672    0.4570    0.4044    0.2159    0.9349
      0.8073    0.8537    0.0696    0.6709    0.5088    0.8670
   
   
      0.7723
      0.7960
      0.8073
      0.7285
      0.6672
      0.8537
      0.6150
      0.6709
      0.8108
      0.8217
      0.5088
      0.9349
      0.8670
   

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

   
      9.5961    6.4517    2.8658    2.8959    3.5528    8.7091
      0.8668    4.9671    7.2586    5.5918    1.6166    2.2503
      6.2751    7.8762    2.0621    0.7813    1.6635    2.5498
      6.1109    0.6164    3.9175    6.3190    1.0513    8.2080
      2.6889    4.8080    1.4949    5.5354    9.9949    0.8462
   
   
      9.5961    6.4517    0.0000    0.0000    0.0000    8.7091
      0.0000    0.0000    7.2586    5.5918    0.0000    0.0000
      6.2751    7.8762    0.0000    0.0000    0.0000    0.0000
      6.1109    0.0000    0.0000    6.3190    0.0000    8.2080
      0.0000    0.0000    0.0000    5.5354    9.9949    0.0000
   
   
         NaN    6.4517    0.0000    0.0000    0.0000    8.7091
      0.0000    0.0000    7.2586    5.5918    0.0000    0.0000
      6.2751    7.8762    0.0000    0.0000    0.0000    0.0000
      6.1109    0.0000    0.0000    6.3190    0.0000    8.2080
      0.0000    0.0000    0.0000    5.5354       NaN    0.0000
   

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

   
      3.1248    6.5000    4.5092    0.6849    9.8684    6.5000
      0.8828    8.0156    9.9210    2.0110    1.5253    4.3899
      6.5000    2.4120    6.5000    9.0743    2.2902    1.7127
      6.5000    6.5000    6.5000    8.7474    6.5000    8.5478
      9.1783    9.2793    2.7478    2.4100    8.3859    8.7573
   
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
   
