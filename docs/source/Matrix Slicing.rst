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
      0.5409    0.4809    0.1761    0.0830
   
   R1[2] = 0.17613300757587058
   C1 = 
      0.5158
      0.5632
      0.3983
      0.0800
      0.9243
      0.0128
      0.5623
      0.3936
   
   C1[5] = 0.012767053673302886

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
      0.5082    0.6624    0.8416    0.6779    0.1513
      0.5522    0.0738    0.7041    0.6192    0.9024
   

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
   
      0.0284    0.5424    0.4602    0.1587    0.8226    0.2344    0.9615    0.3081
      0.1945    0.2454    0.2214    0.7986    0.7598    0.0624    0.0257    0.2768
      0.1009    0.3242    0.3283    0.5821    0.7138    0.6793    0.7666    0.8347
      0.5193    0.2212    0.4831    0.1881    0.9201    0.8725    0.3861    0.6794
      0.7072    0.7244    0.0164    0.4426    0.8435    0.6914    0.7697    0.7160
      0.1387    0.5652    0.3409    0.9410    0.7977    0.9622    0.1768    0.0099
      0.1639    0.4744    0.9372    0.0001    0.2967    0.8899    0.2350    0.2933
      0.5390    0.2970    0.8875    0.8932    0.4142    0.0194    0.1800    0.0774
   
   B = 
   
      0.7132    0.4406    0.9922    0.5802    0.9930    0.5203    0.7959    0.8809
      0.2004    0.3859    0.9150    0.7643    0.7863    0.8764    0.4720    0.3826
      0.1455    0.5939    0.4417    0.6649    0.1415    0.2396    0.6474    0.1167
      0.5257    0.7318    0.2031    0.0225    0.2871    0.5046    0.8593    0.9023
      0.9472    0.7968    0.6164    0.2841    0.3267    0.7186    0.7326    0.6398
      0.9776    0.2554    0.1411    0.1535    0.4424    0.0626    0.2961    0.6985
      0.6925    0.0880    0.6363    0.2792    0.8526    0.4043    0.0227    0.5281
      0.3675    0.6495    0.6254    0.8599    0.1945    0.2073    0.4784    0.9127
   
   C = 
   
      2.0667    1.6114    2.1045    1.5438    1.8175    1.7389    1.5542    1.9085
      1.5401    1.6997    1.3441    0.9362    0.9982    1.3899    1.8083    1.8075
      2.6685    2.1424    2.2056    1.7765    1.9183    1.7474    2.0869    2.8743
      2.8254    2.1700    2.3300    1.8831    1.9598    1.6875    2.2584    2.7903
      3.1556    2.3063    3.0168    2.1613    2.7782    2.3392    2.4785    3.3840
      2.5787    2.0737    1.7426    1.1925    1.7394    1.8309    2.2845    2.5121
      1.7699    1.4869    1.6521    1.6196    1.4165    1.1504    1.5878    1.6386
      1.6070    1.9339    1.8009    1.3874    1.4632    1.5917    2.2616    1.9421
   
   D = 
   
      2.0667    1.6114    2.1045    1.5438    1.8175    1.7389    1.5542    1.9085
      1.5401    1.6997    1.3441    0.9362    0.9982    1.3899    1.8083    1.8075
      2.6685    2.1424    2.2056    1.7765    1.9183    1.7474    2.0869    2.8743
      2.8254    2.1700    2.3300    1.8831    1.9598    1.6875    2.2584    2.7903
      3.1556    2.3063    3.0168    2.1613    2.7782    2.3392    2.4785    3.3840
      2.5787    2.0737    1.7426    1.1925    1.7394    1.8309    2.2845    2.5121
      1.7699    1.4869    1.6521    1.6196    1.4165    1.1504    1.5878    1.6386
      1.6070    1.9339    1.8009    1.3874    1.4632    1.5917    2.2616    1.9421
   


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

   
      0.3656    0.7207    0.4996    0.3919    0.9534    0.1469
      0.5578    0.6775    0.9476    0.7484    0.4425    0.1246
      0.0215    0.7041    0.5022    0.7322    0.6266    0.7623
      0.0659    0.9351    0.9176    0.5404    0.6465    0.6945
      0.2312    0.1151    0.1564    0.6477    0.8418    0.6324
   
   
      0.5578
      0.7207
      0.6775
      0.7041
      0.9351
      0.9476
      0.5022
      0.9176
      0.7484
      0.7322
      0.5404
      0.6477
      0.9534
      0.6266
      0.6465
      0.8418
      0.7623
      0.6945
      0.6324
   

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

   
      2.3005    1.0261    4.4059    8.2747    7.8996    9.5965
      9.9383    9.4392    9.0482    8.6161    3.5703    3.6791
      4.4846    5.8089    9.4368    1.1044    1.6609    7.2849
      9.5400    3.8559    6.9859    1.9749    1.8914    5.5631
      3.6927    9.1796    7.9179    5.5038    8.5433    0.1284
   
   
      0.0000    0.0000    0.0000    8.2747    7.8996    9.5965
      9.9383    9.4392    9.0482    8.6161    0.0000    0.0000
      0.0000    5.8089    9.4368    0.0000    0.0000    7.2849
      9.5400    0.0000    6.9859    0.0000    0.0000    5.5631
      0.0000    9.1796    7.9179    5.5038    8.5433    0.0000
   
   
      0.0000    0.0000    0.0000    8.2747    7.8996       NaN
         NaN       NaN       NaN    8.6161    0.0000    0.0000
      0.0000    5.8089       NaN    0.0000    0.0000    7.2849
         NaN    0.0000    6.9859    0.0000    0.0000    5.5631
      0.0000       NaN    7.9179    5.5038    8.5433    0.0000
   

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

   
      9.0392    9.0033    3.9456    3.3393    6.5000    9.7661
      4.8592    2.3983    3.4182    8.7360    4.8130    8.6554
      0.0614    0.0836    3.3617    6.5000    6.5000    4.0100
      4.5696    6.5000    2.8990    9.9131    4.8763    6.5000
      8.2744    2.9760    1.1086    0.5196    9.2535    3.1644
   
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
   
