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
      0.5320    0.5032    0.6690    0.0141
   
   R1[2] = 0.6690059943339328
   C1 = 
      0.8983
      0.3492
      0.2708
      0.6027
      0.2608
      0.3799
      0.9780
      0.1252
   
   C1[5] = 0.37988239717099204

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
      0.7459    0.1297    0.5760    0.8687    0.9162
      0.5753    0.3162    0.2371    0.4688    0.4149
   

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
   
      0.9710    0.3538    0.1772    0.0980    0.5942    0.6601    0.5705    0.0018
      0.0169    0.2875    0.7749    0.6532    0.3291    0.5697    0.3589    0.9848
      0.9496    0.3162    0.5043    0.5619    0.8626    0.7508    0.4356    0.0662
      0.4951    0.0191    0.6879    0.2320    0.5081    0.0209    0.5107    0.2632
      0.7928    0.6603    0.8152    0.4354    0.0917    0.6194    0.5965    0.1822
      0.6908    0.2813    0.5929    0.2574    0.6320    0.2569    0.5122    0.0779
      0.1131    0.9861    0.7937    0.4636    0.3226    0.3114    0.5618    0.3308
      0.5203    0.8021    0.0381    0.5193    0.1454    0.5236    0.4207    0.8277
   
   B = 
   
      0.3410    0.8237    0.7420    0.1041    0.6916    0.3882    0.5505    0.0638
      0.8547    0.5665    0.0830    0.4099    0.4981    0.5165    0.7784    0.9542
      0.8477    0.7138    0.2000    0.5283    0.1575    0.9243    0.3365    0.1880
      0.9234    0.4194    0.1419    0.2322    0.2773    0.0489    0.8977    0.6472
      0.1754    0.7580    0.6301    0.1429    0.7741    0.2569    0.4238    0.2529
      0.7952    0.9222    0.1946    0.0430    0.7568    0.5611    0.9144    0.7370
      0.0900    0.2351    0.9236    0.5140    0.6894    0.9430    0.5805    0.7390
      0.9375    0.7838    0.2855    0.6079    0.9298    0.3089    0.9618    0.9520
   
   C = 
   
      1.5564    2.3625    1.8294    0.7701    2.2574    1.7899    2.1459    1.5564
      2.9779    2.6349    1.2148    1.5352    2.3071    1.9501    2.8962    2.5497
      2.3900    3.0574    2.0222    1.0450    2.6473    2.0997    2.8115    1.9771
      1.3810    1.7378    1.4104    0.9726    1.5305    1.5544    1.5112    1.1011
      2.6610    2.7153    1.6490    1.3420    2.2468    2.4136    2.7424    2.2096
      1.6507    2.1570    1.6345    0.9722    1.8919    1.7877    1.9049    1.3924
      2.6471    2.3357    1.2674    1.4922    2.0033    2.1991    2.5788    2.4385
      2.6305    2.4687    1.3523    1.2863    2.4779    1.6605    2.9706    2.6633
   
   D = 
   
      1.5564    2.3625    1.8294    0.7701    2.2574    1.7899    2.1459    1.5564
      2.9779    2.6349    1.2148    1.5352    2.3071    1.9501    2.8962    2.5497
      2.3900    3.0574    2.0222    1.0450    2.6473    2.0997    2.8115    1.9771
      1.3810    1.7378    1.4104    0.9726    1.5305    1.5544    1.5112    1.1011
      2.6610    2.7153    1.6490    1.3420    2.2468    2.4136    2.7424    2.2096
      1.6507    2.1570    1.6345    0.9722    1.8919    1.7877    1.9049    1.3924
      2.6471    2.3357    1.2674    1.4922    2.0033    2.1991    2.5788    2.4385
      2.6305    2.4687    1.3523    1.2863    2.4779    1.6605    2.9706    2.6633
   


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

   
      0.9243    0.6470    0.1089    0.9653    0.0076    0.4084
      0.8949    0.6846    0.6785    0.9084    0.1440    0.1480
      0.5471    0.8426    0.0880    0.7783    0.3050    0.3512
      0.2620    0.1648    0.8894    0.4194    0.7472    0.5831
      0.6358    0.4270    0.1757    0.1336    0.4131    0.7746
   
   
      0.9243
      0.8949
      0.5471
      0.6358
      0.6470
      0.6846
      0.8426
      0.6785
      0.8894
      0.9653
      0.9084
      0.7783
      0.7472
      0.5831
      0.7746
   

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

   
      5.9729    0.8828    3.4055    0.3385    0.4604    0.0773
      8.3201    1.3740    6.2669    5.7951    0.3253    4.4568
      8.4575    9.2358    5.8017    7.4926    0.3480    6.8763
      9.1907    9.8803    5.2445    3.4716    8.5567    4.8336
      3.7301    8.7031    0.3504    0.1438    0.8065    3.2238
   
   
      5.9729    0.0000    0.0000    0.0000    0.0000    0.0000
      8.3201    0.0000    6.2669    5.7951    0.0000    0.0000
      8.4575    9.2358    5.8017    7.4926    0.0000    6.8763
      9.1907    9.8803    5.2445    0.0000    8.5567    0.0000
      0.0000    8.7031    0.0000    0.0000    0.0000    0.0000
   
   
      5.9729    0.0000    0.0000    0.0000    0.0000    0.0000
      8.3201    0.0000    6.2669    5.7951    0.0000    0.0000
      8.4575       NaN    5.8017    7.4926    0.0000    6.8763
         NaN       NaN    5.2445    0.0000    8.5567    0.0000
      0.0000    8.7031    0.0000    0.0000    0.0000    0.0000
   

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

   
      4.6161    9.7887    0.7527    6.5000    3.9697    3.9451
      3.9975    4.5116    6.5000    6.5000    6.5000    8.7097
      3.8990    0.0023    6.5000    9.5332    6.5000    6.5000
      6.5000    1.2147    6.5000    2.7336    4.2533    8.7722
      6.5000    3.3591    4.8706    6.5000    3.0819    2.0831
   
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
   
