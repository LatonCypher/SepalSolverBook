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
      0.4546    0.1929    0.5490    0.9862
   
   R1[2] = 0.5489859465705125
   C1 = 
      0.9057
      0.3675
      0.6759
      0.1924
      0.8515
      0.4738
      0.8417
      0.1992
   
   C1[5] = 0.47378867203401853

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
      0.0641    0.8305    0.5910    0.4616    0.8229
      0.3532    0.9381    0.8031    0.9708    0.5830
   

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
   
      0.7768    0.8059    0.4207    0.7992    0.6129    0.5860    0.5602    0.6943
      0.3805    0.7890    0.0945    0.6105    0.6233    0.8779    0.1337    0.7573
      0.8009    0.1931    0.3074    0.1518    0.6263    0.7024    0.3288    0.7376
      0.6785    0.3894    0.1211    0.9482    0.4220    0.8903    0.3040    0.4798
      0.4076    0.0564    0.7944    0.3406    0.4360    0.2322    0.0848    0.8061
      0.2122    0.9229    0.3078    0.1065    0.4542    0.0909    0.2610    0.5174
      0.8167    0.5247    0.8042    0.9888    0.1605    0.3826    0.3535    0.4179
      0.3447    0.5695    0.4973    0.9816    0.9568    0.9937    0.8460    0.7590
   
   B = 
   
      0.2553    0.2408    0.1505    0.5613    0.1306    0.2126    0.4785    0.1837
      0.3143    0.7065    0.0415    0.7730    0.0970    0.0002    0.2002    0.2276
      0.7171    0.5682    0.2764    0.3581    0.7538    0.6650    0.6067    0.7415
      0.5783    0.3144    0.9804    0.5896    0.9265    0.0284    0.4511    0.6149
      0.2251    0.1249    0.6611    0.6853    0.9623    0.1503    0.4357    0.4256
      0.4396    0.6967    0.3003    0.8139    0.1369    0.9365    0.1121    0.6411
      0.6106    0.0151    0.5240    0.0186    0.1180    0.0658    0.3932    0.0861
      0.0841    0.3924    0.7388    0.1689    0.2527    0.8106    0.4107    0.6947
   
   C = 
   
      2.0115    2.0125    2.4378    2.7055    2.1488    1.7083    1.9869    2.2966
      1.4375    1.8834    2.0199    2.4893    1.6903    1.6996    1.4063    2.0606
      1.2859    1.4137    1.7045    1.9300    1.4197    1.7504    1.4607    1.7699
      1.6430    1.6713    2.1415    2.3848    1.7813    1.5578    1.5040    1.9959
      1.2082    1.2302    1.6151    1.3832    1.6381    1.5664    1.4223    1.7879
      0.9717    1.2386    1.1063    1.4831    1.0591    0.8429    1.0443    1.1763
      1.9771    1.7911    2.0513    2.2335    2.0340    1.4808    1.8531    2.1080
      2.4239    2.1990    3.1104    2.9990    2.7330    2.1773    2.1961    2.8097
   
   D = 
   
      2.0115    2.0125    2.4378    2.7055    2.1488    1.7083    1.9869    2.2966
      1.4375    1.8834    2.0199    2.4893    1.6903    1.6996    1.4063    2.0606
      1.2859    1.4137    1.7045    1.9300    1.4197    1.7504    1.4607    1.7699
      1.6430    1.6713    2.1415    2.3848    1.7813    1.5578    1.5040    1.9959
      1.2082    1.2302    1.6151    1.3832    1.6381    1.5664    1.4223    1.7879
      0.9717    1.2386    1.1063    1.4831    1.0591    0.8429    1.0443    1.1763
      1.9771    1.7911    2.0513    2.2335    2.0340    1.4808    1.8531    2.1080
      2.4239    2.1990    3.1104    2.9990    2.7330    2.1773    2.1961    2.8097
   


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

   
      0.0244    0.6766    0.0852    0.4319    0.8922    0.3056
      0.4953    0.0747    0.2264    0.6073    0.2241    0.9118
      0.9878    0.6135    0.0185    0.1822    0.1528    0.9750
      0.9993    0.1510    0.9896    0.6201    0.8724    0.2653
      0.6019    0.9247    0.4936    0.2114    0.2334    0.1584
   
   
      0.9878
      0.9993
      0.6019
      0.6766
      0.6135
      0.9247
      0.9896
      0.6073
      0.6201
      0.8922
      0.8724
      0.9118
      0.9750
   

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

   
      5.0262    4.5895    5.1251    6.9767    2.0939    4.1607
      3.8115    7.9923    1.4529    0.0916    9.4306    4.3021
      4.8545    4.6870    7.3016    0.1623    8.8142    4.8358
      2.4810    4.8840    2.8140    2.2966    6.3246    2.3218
      7.2140    7.5220    3.9810    2.7994    5.9490    2.2438
   
   
      5.0262    0.0000    5.1251    6.9767    0.0000    0.0000
      0.0000    7.9923    0.0000    0.0000    9.4306    0.0000
      0.0000    0.0000    7.3016    0.0000    8.8142    0.0000
      0.0000    0.0000    0.0000    0.0000    6.3246    0.0000
      7.2140    7.5220    0.0000    0.0000    5.9490    0.0000
   
   
      5.0262    0.0000    5.1251    6.9767    0.0000    0.0000
      0.0000    7.9923    0.0000    0.0000       NaN    0.0000
      0.0000    0.0000    7.3016    0.0000    8.8142    0.0000
      0.0000    0.0000    0.0000    0.0000    6.3246    0.0000
      7.2140    7.5220    0.0000    0.0000    5.9490    0.0000
   

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

   
      9.7305    9.5537    6.5000    6.5000    6.5000    9.1976
      6.5000    6.5000    3.1516    4.1777    3.5800    6.5000
      0.5565    6.5000    6.5000    6.5000    9.2084    6.5000
      0.7194    3.3180    6.5000    6.5000    0.2751    8.0681
      1.4344    0.5438    4.4168    6.5000    6.5000    9.0135
   
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
   
