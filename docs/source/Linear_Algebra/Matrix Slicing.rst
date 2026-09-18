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
      0.0732    0.2607    0.6689    0.5269
   
   R1[2] = 0.6688657421089133
   C1 = 
      0.6497
      0.6838
      0.2021
      0.6205
      0.6216
      0.4874
      0.6221
      0.9055
   
   C1[5] = 0.48742534071963917

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
      0.6765    0.0550    0.0849    0.2822    0.9627
      0.8586    0.7678    0.4671    0.7854    0.1152
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.9592    0.6171    0.9017    0.7131    0.9102    0.3187    0.1063    0.6631
      0.1792    0.9508    0.8564    0.5592    0.5391    0.7917    0.3913    0.6302
      0.0368    0.6779    0.6431    0.2705    0.7743    0.2749    0.8750    0.8953
      0.8234    0.9654    0.5352    0.4870    0.9373    0.1124    0.0610    0.4084
      0.1344    0.0189    0.2664    0.2063    0.4408    0.7466    0.2861    0.0164
      0.5542    0.7867    0.1892    0.2485    0.8207    0.9231    0.7973    0.9903
      0.9820    0.9557    0.8055    0.9386    0.7007    0.0112    0.2425    0.7321
      0.0395    0.2060    0.1595    0.8671    0.5162    0.3013    0.1381    0.4347
   
   B = 
   
      0.3791    0.0709    0.7892    0.2173    0.0664    0.0360    0.8544    0.8814
      0.2969    0.3262    0.6280    0.1529    0.6871    0.5102    0.3917    0.2749
      0.9988    0.3044    0.9986    0.8320    0.5742    0.9894    0.2778    0.9852
      0.2042    0.7163    0.1057    0.0899    0.4075    0.5712    0.2206    0.6239
      0.4834    0.9196    0.9376    0.9219    0.4623    0.6821    0.3178    0.1529
      0.9013    0.7882    0.4513    0.2630    0.3441    0.5552    0.5500    0.3108
      0.8203    0.3602    0.3673    0.8881    0.5493    0.1531    0.7973    0.4683
      0.7702    0.3554    0.1319    0.0802    0.3111    0.6909    0.4467    0.1572
   
   C = 
   
      2.9183    2.4167    3.2441    2.1876    2.0912    2.9211    2.3146    2.7406
      3.1003    2.4688    2.7424    2.0503    2.3174    2.9609    2.0870    2.2227
      2.9422    2.1753    2.4151    2.3060    2.1595    2.5713    2.0301    1.7755
      2.1518    2.0027    2.8478    1.7961    1.8563    2.3231    1.9285    2.0932
      1.4982    1.3473    1.2633    1.1305    0.8819    1.1663    1.0280    0.9509
      3.3289    2.6530    2.7562    2.2073    2.2304    2.6291    2.7357    1.9877
      2.7639    2.2997    3.1265    2.0371    2.2556    2.8832    2.3935    2.8468
      1.3816    1.6560    1.1394    0.9633    1.1425    1.6004    0.9840    1.0951
   
   D = 
   
      2.9183    2.4167    3.2441    2.1876    2.0912    2.9211    2.3146    2.7406
      3.1003    2.4688    2.7424    2.0503    2.3174    2.9609    2.0870    2.2227
      2.9422    2.1753    2.4151    2.3060    2.1595    2.5713    2.0301    1.7755
      2.1518    2.0027    2.8478    1.7961    1.8563    2.3231    1.9285    2.0932
      1.4982    1.3473    1.2633    1.1305    0.8819    1.1663    1.0280    0.9509
      3.3289    2.6530    2.7562    2.2073    2.2304    2.6291    2.7357    1.9877
      2.7639    2.2997    3.1265    2.0371    2.2556    2.8832    2.3935    2.8468
      1.3816    1.6560    1.1394    0.9633    1.1425    1.6004    0.9840    1.0951
   


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

   
      0.7644    0.5102    0.6506    0.0482    0.9062    0.9170
      0.7530    0.1436    0.6214    0.6887    0.4580    0.0456
      0.6496    0.1224    0.6608    0.6766    0.7758    0.9081
      0.2139    0.3557    0.5187    0.8250    0.1485    0.7047
      0.0591    0.1932    0.8635    0.4248    0.3873    0.6843
   
   
      0.7644
      0.7530
      0.6496
      0.5102
      0.6506
      0.6214
      0.6608
      0.5187
      0.8635
      0.6887
      0.6766
      0.8250
      0.9062
      0.7758
      0.9170
      0.9081
      0.7047
      0.6843
   

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

   
      4.2245    8.4795    1.4802    3.3758    6.7270    6.0178
      9.0970    8.3685    5.3682    2.2100    0.8113    8.8557
      4.0947    5.5664    6.5726    4.8482    9.8696    3.4519
      7.4667    1.1160    3.6429    1.8650    8.5644    9.9354
      8.9911    3.6907    6.0244    2.8617    4.2375    9.2212
   
   
      0.0000    8.4795    0.0000    0.0000    6.7270    6.0178
      9.0970    8.3685    5.3682    0.0000    0.0000    8.8557
      0.0000    5.5664    6.5726    0.0000    9.8696    0.0000
      7.4667    0.0000    0.0000    0.0000    8.5644    9.9354
      8.9911    0.0000    6.0244    0.0000    0.0000    9.2212
   
   
      0.0000    8.4795    0.0000    0.0000    6.7270    6.0178
         NaN    8.3685    5.3682    0.0000    0.0000    8.8557
      0.0000    5.5664    6.5726    0.0000       NaN    0.0000
      7.4667    0.0000    0.0000    0.0000    8.5644       NaN
      8.9911    0.0000    6.0244    0.0000    0.0000       NaN
   

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

   
      9.8786    3.2834    4.5002    4.3981    2.8611    4.2109
      1.1786    6.5000    9.4735    6.5000    6.5000    8.3539
      0.2967    6.5000    0.1898    2.0760    8.2149    6.5000
      1.1007    4.6929    4.7457    6.5000    8.6754    6.5000
      4.3936    4.3319    1.9504    6.5000    2.3922    4.3558
   
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
   
