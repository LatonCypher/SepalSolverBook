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
      0.2979    0.7286    0.8428    0.5045
   
   R1[2] = 0.842786116425452
   C1 = 
      0.3078
      0.9749
      0.3368
      0.7799
      0.2130
      0.8747
      0.9341
      0.7080
   
   C1[5] = 0.8746867802036284

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
      0.9753    0.7203    0.6050    0.6634    0.9551
      0.8355    0.3398    0.5228    0.6888    0.7834
   

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
   
      0.8507    0.9494    0.3347    0.5387    0.8889    0.3178    0.9932    0.3965
      0.1672    0.2117    0.1109    0.4729    0.7156    0.2096    0.7148    0.2216
      0.9194    0.0615    0.4316    0.2149    0.2230    0.9028    0.2513    0.4405
      0.6799    0.8048    0.0091    0.3865    0.7100    0.5331    0.9318    0.2148
      0.8493    0.7431    0.2924    0.6613    0.6834    0.7393    0.5858    0.4974
      0.2370    0.1023    0.6342    0.0400    0.0862    0.6448    0.5575    0.8460
      0.3199    0.7774    0.3871    0.7454    0.1327    0.5551    0.0794    0.2955
      0.7095    0.4769    0.7178    0.0170    0.4615    0.5504    0.4083    0.5422
   
   B = 
   
      0.5357    0.1682    0.2103    0.0708    0.3960    0.4504    0.7681    0.2660
      0.1226    0.3467    0.2631    0.1492    0.2471    0.6887    0.5268    0.1587
      0.4102    0.0366    0.2662    0.0833    0.9997    0.8209    0.9911    0.3323
      0.8380    0.4625    0.2297    0.0813    0.3902    0.6140    0.8353    0.4273
      0.0611    0.7953    0.6599    0.5670    0.1140    0.3825    0.7351    0.5232
      0.9357    0.4093    0.4309    0.2208    0.3072    0.8660    0.9019    0.2488
      0.5138    0.8034    0.4205    0.9886    0.9485    0.9731    0.9772    0.9184
      0.8396    0.4624    0.7871    0.1849    0.7289    0.6760    0.2749    0.2538
   
   C = 
   
      2.3556    2.5518    2.0946    1.9028    2.5462    3.4921    3.9548    2.2752
      1.3505    1.6559    1.2665    1.2907    1.3995    1.9031    2.2194    1.4562
      2.2145    1.2436    1.3623    0.7833    1.7568    2.3521    2.6907    1.1734
      1.9918    2.2032    1.7050    1.6815    1.9131    2.8905    3.2504    1.8911
      2.6723    2.2637    2.0111    1.4708    2.2934    3.3482    3.7643    1.9292
      2.0386    1.2886    1.4898    0.9869    2.1222    2.4282    2.3202    1.2394
      1.8665    1.2153    1.1387    0.5624    1.4729    2.2634    2.4185    1.0110
      1.9555    1.4898    1.6097    1.0696    2.1272    2.6647    2.9055    1.4012
   
   D = 
   
      2.3556    2.5518    2.0946    1.9028    2.5462    3.4921    3.9548    2.2752
      1.3505    1.6559    1.2665    1.2907    1.3995    1.9031    2.2194    1.4562
      2.2145    1.2436    1.3623    0.7833    1.7568    2.3521    2.6907    1.1734
      1.9918    2.2032    1.7050    1.6815    1.9131    2.8905    3.2504    1.8911
      2.6723    2.2637    2.0111    1.4708    2.2934    3.3482    3.7643    1.9292
      2.0386    1.2886    1.4898    0.9869    2.1222    2.4282    2.3202    1.2394
      1.8665    1.2153    1.1387    0.5624    1.4729    2.2634    2.4185    1.0110
      1.9555    1.4898    1.6097    1.0696    2.1272    2.6647    2.9055    1.4012
   


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

   
      0.0242    0.9317    0.7489    0.7442    0.4571    0.9357
      0.3940    0.5726    0.4217    0.9365    0.1467    0.5327
      0.3066    0.5420    0.2387    0.7082    0.3460    0.4482
      0.9812    0.0076    0.7659    0.4714    0.5318    0.9033
      0.9657    0.5140    0.8171    0.0449    0.9038    0.6418
   
   
      0.9812
      0.9657
      0.9317
      0.5726
      0.5420
      0.5140
      0.7489
      0.7659
      0.8171
      0.7442
      0.9365
      0.7082
      0.5318
      0.9038
      0.9357
      0.5327
      0.9033
      0.6418
   

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

   
      2.8782    5.3154    9.7817    6.5314    6.1447    0.7805
      9.7715    8.1748    3.4861    5.4923    3.6766    1.3689
      9.2151    5.3393    7.4521    7.5225    8.8336    5.0049
      7.1817    0.7913    7.4350    4.0666    8.7864    4.8675
      2.9344    4.2636    4.8728    9.2192    6.0438    5.4622
   
   
      0.0000    5.3154    9.7817    6.5314    6.1447    0.0000
      9.7715    8.1748    0.0000    5.4923    0.0000    0.0000
      9.2151    5.3393    7.4521    7.5225    8.8336    5.0049
      7.1817    0.0000    7.4350    0.0000    8.7864    0.0000
      0.0000    0.0000    0.0000    9.2192    6.0438    5.4622
   
   
      0.0000    5.3154       NaN    6.5314    6.1447    0.0000
         NaN    8.1748    0.0000    5.4923    0.0000    0.0000
         NaN    5.3393    7.4521    7.5225    8.8336    5.0049
      7.1817    0.0000    7.4350    0.0000    8.7864    0.0000
      0.0000    0.0000    0.0000       NaN    6.0438    5.4622
   

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

   
      6.5000    6.5000    3.7349    6.5000    0.2331    4.5843
      6.5000    6.5000    9.4001    8.2817    1.1870    9.9237
      6.5000    6.5000    9.5431    0.2605    2.7490    6.5000
      8.8002    6.5000    2.9615    9.0583    0.5668    4.0394
      1.7064    3.0906    2.1278    0.8517    6.5000    3.9200
   
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
   
