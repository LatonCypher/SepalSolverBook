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
      0.8697    0.7498    0.6249    0.1501
   
   R1[2] = 0.6248658742808629
   C1 = 
      0.2658
      0.0342
      0.2149
      0.6172
      0.9224
      0.7290
      0.4305
      0.4410
   
   C1[5] = 0.7290367187793091

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
      0.0749    0.9096    0.7160    0.2377    0.3279
      0.6622    0.3578    0.0384    0.0005    0.5862
   

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
   
      0.4793    0.3034    0.9228    0.9224    0.6350    0.2196    0.4025    0.7876
      0.6932    0.0043    0.7775    0.5076    0.4594    0.0880    0.8183    0.5576
      0.6023    0.6906    0.0860    0.5836    0.2906    0.1580    0.5955    0.2058
      0.4880    0.5137    0.0714    0.0849    0.7065    0.3271    0.8177    0.5534
      0.9359    0.1198    0.4692    0.5999    0.4321    0.6425    0.2940    0.2293
      0.9669    0.0220    0.2114    0.8668    0.1340    0.5637    0.9423    0.5731
      0.5266    0.5863    0.5100    0.5809    0.0648    0.5806    0.9896    0.2058
      0.9239    0.8589    0.4164    0.2946    0.0528    0.8255    0.3010    0.9136
   
   B = 
   
      0.0814    0.2082    0.5095    0.1647    0.6004    0.1471    0.5977    0.6377
      0.7059    0.9891    0.6777    0.8019    0.5564    0.1208    0.9853    0.6513
      0.8688    0.8526    0.2692    0.4417    0.7344    0.0199    0.3310    0.4909
      0.6994    0.8381    0.1375    0.9215    0.5568    0.1202    0.2485    0.0489
      0.1032    0.1648    0.5136    0.4384    0.1923    0.9014    0.6722    0.5176
      0.9421    0.3766    0.5861    0.4562    0.3480    0.8536    0.6164    0.6038
      0.5081    0.8149    0.4989    0.8564    0.8966    0.6981    0.4293    0.9447
      0.5479    0.4238    0.5013    0.0799    0.0093    0.9192    0.8781    0.6543
   
   C = 
   
      2.6085    2.8088    1.8756    2.3660    2.2146    2.0013    2.5466    2.3582
      1.9416    2.2488    1.6106    1.9156    2.1301    1.7521    2.0060    2.2802
      1.6136    2.0509    1.5204    1.9547    1.7807    1.2456    1.9430    1.8477
      1.6235    1.8822    1.8676    1.8055    1.6665    2.1411    2.3561    2.3828
      1.9130    1.8662    1.6271    1.7630    1.8797    1.5877    1.9959    1.9740
      2.2218    2.3749    1.8403    2.2377    2.3029    2.0399    2.2302    2.4521
      2.4753    2.7342    1.8533    2.4747    2.4441    1.6623    2.2125    2.4501
      2.6860    2.5959    2.3245    2.0268    2.0782    2.0856    3.0852    2.7754
   
   D = 
   
      2.6085    2.8088    1.8756    2.3660    2.2146    2.0013    2.5466    2.3582
      1.9416    2.2488    1.6106    1.9156    2.1301    1.7521    2.0060    2.2802
      1.6136    2.0509    1.5204    1.9547    1.7807    1.2456    1.9430    1.8477
      1.6235    1.8822    1.8676    1.8055    1.6665    2.1411    2.3561    2.3828
      1.9130    1.8662    1.6271    1.7630    1.8797    1.5877    1.9959    1.9740
      2.2218    2.3749    1.8403    2.2377    2.3029    2.0399    2.2302    2.4521
      2.4753    2.7342    1.8533    2.4747    2.4441    1.6623    2.2125    2.4501
      2.6860    2.5959    2.3245    2.0268    2.0782    2.0856    3.0852    2.7754
   


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

   
      0.6956    0.3277    0.9457    0.4054    0.9475    0.5117
      0.7169    0.4615    0.4558    0.2720    0.0819    0.3170
      0.9285    0.7651    0.2675    0.5146    0.4710    0.4857
      0.3862    0.9303    0.8472    0.1541    0.8113    0.5020
      0.1195    0.7689    0.6668    0.8704    0.3910    0.5366
   
   
      0.6956
      0.7169
      0.9285
      0.7651
      0.9303
      0.7689
      0.9457
      0.8472
      0.6668
      0.5146
      0.8704
      0.9475
      0.8113
      0.5117
      0.5020
      0.5366
   

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

   
      7.8772    0.1959    4.5786    3.7387    1.0394    6.0566
      0.1134    8.0603    8.4003    9.8060    2.2638    3.4397
      5.4179    0.7433    2.0461    1.8643    0.0879    1.7239
      2.0849    6.4405    0.9162    6.8594    1.3848    1.0539
      8.6230    1.5457    6.6441    0.3412    8.2481    5.2528
   
   
      7.8772    0.0000    0.0000    0.0000    0.0000    6.0566
      0.0000    8.0603    8.4003    9.8060    0.0000    0.0000
      5.4179    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    6.4405    0.0000    6.8594    0.0000    0.0000
      8.6230    0.0000    6.6441    0.0000    8.2481    5.2528
   
   
      7.8772    0.0000    0.0000    0.0000    0.0000    6.0566
      0.0000    8.0603    8.4003       NaN    0.0000    0.0000
      5.4179    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    6.4405    0.0000    6.8594    0.0000    0.0000
      8.6230    0.0000    6.6441    0.0000    8.2481    5.2528
   

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

   
      8.6455    6.5000    6.5000    9.5875    1.3409    6.5000
      4.3374    3.5859    0.0954    9.9431    6.5000    6.5000
      8.7042    8.4061    6.5000    6.5000    6.5000    6.5000
      9.2409    3.6238    6.5000    4.9218    1.0172    4.4728
      6.5000    6.5000    8.3183    0.0631    0.9703    3.7685
   
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
   
