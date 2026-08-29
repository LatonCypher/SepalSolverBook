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
      0.8667    0.9075    0.4060    0.6930
   
   R1[2] = 0.4059697286767914
   C1 = 
      0.2725
      0.5950
      0.7712
      0.3304
      0.5033
      0.4374
      0.0373
      0.0552
   
   C1[5] = 0.43738567409405127

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
      0.6987    0.5659    0.9360    0.0948    0.5505
      0.9247    0.3188    0.4381    0.4729    0.0892
   

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
   
      0.5879    0.9589    0.4676    0.0340    0.8288    0.5962    0.2407    0.5710
      0.3189    0.5579    0.0465    0.8121    0.2268    0.1593    0.6769    0.2333
      0.3482    0.7985    0.3640    0.8693    0.2406    0.7139    0.6478    0.8046
      0.4936    0.7671    0.7757    0.0188    0.7199    0.7571    0.4891    0.8033
      0.5466    0.0894    0.0774    0.5115    0.5652    0.9300    0.3941    0.7744
      0.1933    0.7797    0.5759    0.1194    0.7123    0.9245    0.6772    0.5394
      0.3285    0.9258    0.0816    0.1046    0.4892    0.2025    0.5623    0.0808
      0.5531    0.5169    0.3638    0.2902    0.5179    0.8746    0.0204    0.0755
   
   B = 
   
      0.5702    0.8602    0.5403    0.1926    0.7785    0.3888    0.6943    0.6452
      0.6094    0.5931    0.3733    0.0587    0.9447    0.2181    0.5867    0.4330
      0.0975    0.9282    0.2168    0.5936    0.0256    0.3985    0.1159    0.9890
      0.7611    0.5207    0.9817    0.3547    0.7989    0.2676    0.6634    0.4986
      0.3711    0.3112    0.9368    0.0148    0.9863    0.5639    0.8439    0.7486
      0.5189    0.3909    0.6722    0.1411    0.4396    0.3570    0.2950    0.9116
      0.7142    0.1418    0.0577    0.4119    0.3220    0.4279    0.2575    0.1776
      0.6182    0.5134    0.8928    0.7396    0.0852    0.4045    0.6435    0.4942
   
   C = 
   
      2.1328    2.3444    2.5111    1.0771    2.6083    1.6472    2.3522    2.7627
      1.9390    1.4199    1.7549    0.8871    1.9569    1.0503    1.6558    1.4488
      2.8021    2.4225    2.8795    1.6047    2.5575    1.6804    2.4273    2.7074
      2.3448    2.6112    2.6683    1.5205    2.4126    1.8838    2.3689    3.1401
      2.2155    1.8543    2.7163    1.2126    2.0798    1.5323    2.1314    2.4465
      2.2936    2.1816    2.4470    1.2862    2.3703    1.7463    2.1330    2.8543
      1.5772    1.3145    1.3424    0.5304    1.9754    1.0115    1.5194    1.4363
      1.5940    1.8160    1.9973    0.6512    2.0685    1.1940    1.6709    2.3112
   
   D = 
   
      2.1328    2.3444    2.5111    1.0771    2.6083    1.6472    2.3522    2.7627
      1.9390    1.4199    1.7549    0.8871    1.9569    1.0503    1.6558    1.4488
      2.8021    2.4225    2.8795    1.6047    2.5575    1.6804    2.4273    2.7074
      2.3448    2.6112    2.6683    1.5205    2.4126    1.8838    2.3689    3.1401
      2.2155    1.8543    2.7163    1.2126    2.0798    1.5323    2.1314    2.4465
      2.2936    2.1816    2.4470    1.2862    2.3703    1.7463    2.1330    2.8543
      1.5772    1.3145    1.3424    0.5304    1.9754    1.0115    1.5194    1.4363
      1.5940    1.8160    1.9973    0.6512    2.0685    1.1940    1.6709    2.3112
   


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

   
      0.7406    0.0723    0.7590    0.0137    0.5286    0.2533
      0.9683    0.1012    0.2333    0.8158    0.2136    0.1449
      0.5394    0.5873    0.8122    0.8003    0.4657    0.5240
      0.9986    0.9471    0.1823    0.4435    0.5286    0.2098
      0.5920    0.3885    0.9154    0.2844    0.0158    0.1698
   
   
      0.7406
      0.9683
      0.5394
      0.9986
      0.5920
      0.5873
      0.9471
      0.7590
      0.8122
      0.9154
      0.8158
      0.8003
      0.5286
      0.5286
      0.5240
   

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

   
      1.7075    4.1219    4.9485    6.8118    5.7601    9.6158
      4.1533    7.8091    5.5157    4.5236    7.7340    4.3964
      7.6305    0.0991    7.9713    0.1709    6.5618    3.5876
      2.4304    5.2999    0.2394    2.0757    5.6616    4.9349
      4.6604    5.1629    1.3518    5.7356    5.2577    2.4674
   
   
      0.0000    0.0000    0.0000    6.8118    5.7601    9.6158
      0.0000    7.8091    5.5157    0.0000    7.7340    0.0000
      7.6305    0.0000    7.9713    0.0000    6.5618    0.0000
      0.0000    5.2999    0.0000    0.0000    5.6616    0.0000
      0.0000    5.1629    0.0000    5.7356    5.2577    0.0000
   
   
      0.0000    0.0000    0.0000    6.8118    5.7601       NaN
      0.0000    7.8091    5.5157    0.0000    7.7340    0.0000
      7.6305    0.0000    7.9713    0.0000    6.5618    0.0000
      0.0000    5.2999    0.0000    0.0000    5.6616    0.0000
      0.0000    5.1629    0.0000    5.7356    5.2577    0.0000
   

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

   
      4.0254    2.5725    1.9236    8.4692    1.0301    2.5793
      2.2943    0.5269    6.5000    8.0856    0.2879    1.9201
      6.5000    2.0594    2.8409    3.4825    4.3354    4.2051
      6.5000    8.3443    9.8479    1.9982    0.0965    2.1235
      4.5390    6.5000    8.2317    9.0745    6.5000    9.3108
   
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
   
