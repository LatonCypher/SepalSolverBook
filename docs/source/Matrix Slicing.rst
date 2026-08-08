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
      0.0948    0.3282    0.0180    0.7586
   
   R1[2] = 0.018046679653795694
   C1 = 
      0.3467
      0.3379
      0.8689
      0.6383
      0.7217
      0.8264
      0.3416
      0.9389
   
   C1[5] = 0.8263653571378333

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
      0.3166    0.9774    0.3952    0.1740    0.5586
      0.0480    0.4840    0.0771    0.3178    0.5873
   

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
   
      0.6070    0.5665    0.0381    0.2787    0.2483    0.7705    0.5026    0.4157
      0.4379    0.6038    0.4756    0.6781    0.9365    0.1871    0.8947    0.8918
      0.7766    0.0471    0.1255    0.3466    0.4561    0.8549    0.3474    0.9440
      0.4594    0.2529    0.7898    0.9882    0.0597    0.2646    0.3000    0.1938
      0.6482    0.9619    0.4881    0.5722    0.0967    0.7512    0.3841    0.1812
      0.1776    0.8631    0.2405    0.5127    0.1949    0.9124    0.3936    0.6483
      0.9580    0.1950    0.0733    0.2384    0.5424    0.1934    0.6828    0.8534
      0.7680    0.0889    0.6168    0.7927    0.4208    0.2520    0.1058    0.9841
   
   B = 
   
      0.9406    0.3406    0.1692    0.8187    0.2221    0.2557    0.4427    0.1889
      0.6607    0.8553    0.6580    0.9016    0.5147    0.7404    0.0725    0.7964
      0.7879    0.5131    0.8721    0.6155    0.2415    0.3000    0.0202    0.5078
      0.8742    0.2113    0.3809    0.9942    0.8205    0.3091    0.2171    0.0334
      0.0394    0.5099    0.0901    0.6498    0.9957    0.2513    0.3856    0.2495
      0.9783    0.4821    0.1062    0.3308    0.0393    0.2488    0.4431    0.3494
      0.4919    0.8465    0.8106    0.6623    0.3049    0.4004    0.0709    0.8395
      0.7752    0.8840    0.9073    0.5471    0.5529    0.1409    0.0093    0.1467
   
   C = 
   
      2.5519    2.0608    1.5037    2.2847    1.3248    1.1861    0.8477    1.4085
      3.1297    3.1665    2.7831    3.6207    2.7849    1.6771    0.9102    2.0087
      2.9205    2.2158    1.6739    2.4258    1.6270    0.9776    1.0131    1.1021
      2.6444    1.5702    1.7620    2.5037    1.5022    1.0754    0.6155    1.1099
      3.1980    2.3116    1.9505    2.9320    1.5695    1.5918    0.8899    1.7910
      2.9714    2.4760    2.0247    2.6251    1.6708    1.4400    0.7707    1.6531
      2.5041    2.2834    1.8423    2.5778    1.7541    1.0631    0.8427    1.2827
      3.0380    2.1171    2.0715    2.8418    2.0209    1.0417    0.8216    0.9816
   
   D = 
   
      2.5519    2.0608    1.5037    2.2847    1.3248    1.1861    0.8477    1.4085
      3.1297    3.1665    2.7831    3.6207    2.7849    1.6771    0.9102    2.0087
      2.9205    2.2158    1.6739    2.4258    1.6270    0.9776    1.0131    1.1021
      2.6444    1.5702    1.7620    2.5037    1.5022    1.0754    0.6155    1.1099
      3.1980    2.3116    1.9505    2.9320    1.5695    1.5918    0.8899    1.7910
      2.9714    2.4760    2.0247    2.6251    1.6708    1.4400    0.7707    1.6531
      2.5041    2.2834    1.8423    2.5778    1.7541    1.0631    0.8427    1.2827
      3.0380    2.1171    2.0715    2.8418    2.0209    1.0417    0.8216    0.9816
   


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

   
      0.4340    0.8886    0.2446    0.0592    0.5436    0.4795
      0.8625    0.7772    0.2234    0.6768    0.1166    0.3197
      0.6032    0.3538    0.8420    0.5743    0.1126    0.5866
      0.8242    0.7198    0.4166    0.0742    0.0739    0.5615
      0.2384    0.1607    0.6989    0.7688    0.6820    0.1013
   
   
      0.8625
      0.6032
      0.8242
      0.8886
      0.7772
      0.7198
      0.8420
      0.6989
      0.6768
      0.5743
      0.7688
      0.5436
      0.6820
      0.5866
      0.5615
   

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

   
      2.3413    5.2834    7.8055    3.0805    5.1983    7.3893
      6.8279    5.4326    8.6306    3.0949    8.6058    2.4676
      7.1568    0.2087    6.6090    9.1490    7.7152    7.0696
      8.4926    5.5444    3.0984    6.4870    1.4022    7.9809
      5.7011    8.4839    6.4992    9.2183    7.4225    5.1442
   
   
      0.0000    5.2834    7.8055    0.0000    5.1983    7.3893
      6.8279    5.4326    8.6306    0.0000    8.6058    0.0000
      7.1568    0.0000    6.6090    9.1490    7.7152    7.0696
      8.4926    5.5444    0.0000    6.4870    0.0000    7.9809
      5.7011    8.4839    6.4992    9.2183    7.4225    5.1442
   
   
      0.0000    5.2834    7.8055    0.0000    5.1983    7.3893
      6.8279    5.4326    8.6306    0.0000    8.6058    0.0000
      7.1568    0.0000    6.6090       NaN    7.7152    7.0696
      8.4926    5.5444    0.0000    6.4870    0.0000    7.9809
      5.7011    8.4839    6.4992       NaN    7.4225    5.1442
   

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

   
      0.3766    1.4640    2.6783    6.5000    9.2121    0.6382
      6.5000    6.5000    0.6645    9.1183    6.5000    3.3596
      8.7843    8.9141    0.7035    6.5000    9.7534    6.5000
      8.4078    9.6788    0.1534    2.2453    6.5000    6.5000
      1.2508    6.5000    9.4521    0.2428    4.0977    2.4358
   
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
   
