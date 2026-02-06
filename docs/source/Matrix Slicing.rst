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
      0.4992    0.3663    0.8808    0.2921
   
   R1[2] = 0.8807584992760976
   C1 = 
      0.0248
      0.9156
      0.7261
      0.0460
      0.2571
      0.2972
      0.4544
      0.7564
   
   C1[5] = 0.29715523054092485

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
      0.2629    0.5161    0.1771    0.4674    0.0522
      0.5873    0.7500    0.4914    0.2200    0.6750
   

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
   
      0.6922    0.1590    0.6984    0.9260    0.3774    0.3953    0.3036    0.1013
      0.7241    0.2003    0.6369    0.3246    0.1973    0.9198    0.0289    0.2478
      0.5831    0.2749    0.7972    0.0442    0.8387    0.8981    0.8900    0.3957
      0.8281    0.8886    0.4076    0.4312    0.5836    0.4271    0.9539    0.9308
      0.4612    0.5114    0.5637    0.3394    0.2261    0.9924    0.3229    0.0393
      0.8424    0.0116    0.4266    0.5261    0.7378    0.2799    0.0553    0.8417
      0.3233    0.4051    0.7867    0.8295    0.9235    0.6107    0.4216    0.4469
      0.6700    0.2734    0.1213    0.3899    0.0823    0.1955    0.5047    0.4457
   
   B = 
   
      0.2320    0.2631    0.6756    0.2536    0.0329    0.2150    0.3168    0.0207
      0.5293    0.1538    0.6662    0.3783    0.8727    0.9528    0.0974    0.4653
      0.2669    0.4032    0.4190    0.4532    0.8370    0.0157    0.6464    0.6106
      0.0346    0.5325    0.5697    0.3600    0.3562    0.8917    0.0344    0.7954
      0.3676    0.3726    0.7869    0.8497    0.5270    0.1007    0.4811    0.0731
      0.1645    0.8864    0.1141    0.8496    0.0128    0.4370    0.4730    0.1569
      0.1546    0.2910    0.3044    0.3698    0.2416    0.1770    0.1404    0.2290
      0.6268    0.4991    0.9860    0.2776    0.3814    0.2367    0.5796    0.5611
   
   C = 
   
      0.7775    1.6113    1.9282    1.6826    1.3919    1.4255    1.1880    1.4673
      0.8389    1.6720    1.5878    1.6935    1.0646    1.1315    1.3495    1.0597
      1.3368    2.1058    2.3599    2.5437    1.7616    1.1673    1.9109    1.2899
      1.8019    2.0867    3.2839    2.3560    2.1960    2.0501    1.7833    1.8727
      0.8608    1.6855    1.5100    1.8536    1.2789    1.4208    1.2182    1.1298
      1.1870    1.6348    2.5145    1.7195    1.3091    1.0736    1.5447    1.2844
      1.3134    2.1374    2.6559    2.4739    2.0849    1.7477    1.7303    1.8459
      0.7658    1.0481    1.5879    1.0150    0.8388    1.0427    0.7919    0.9275
   
   D = 
   
      0.7775    1.6113    1.9282    1.6826    1.3919    1.4255    1.1880    1.4673
      0.8389    1.6720    1.5878    1.6935    1.0646    1.1315    1.3495    1.0597
      1.3368    2.1058    2.3599    2.5437    1.7616    1.1673    1.9109    1.2899
      1.8019    2.0867    3.2839    2.3560    2.1960    2.0501    1.7833    1.8727
      0.8608    1.6855    1.5100    1.8536    1.2789    1.4208    1.2182    1.1298
      1.1870    1.6348    2.5145    1.7195    1.3091    1.0736    1.5447    1.2844
      1.3134    2.1374    2.6559    2.4739    2.0849    1.7477    1.7303    1.8459
      0.7658    1.0481    1.5879    1.0150    0.8388    1.0427    0.7919    0.9275
   


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

   
      0.0808    0.0021    0.5746    0.7096    0.0089    0.3313
      0.5579    0.2469    0.0637    0.7456    0.8701    0.0257
      0.3916    0.3874    0.0067    0.6272    0.3722    0.9366
      0.8965    0.6112    0.3557    0.1250    0.1116    0.4922
      0.5371    0.6110    0.1501    0.8375    0.4134    0.1439
   
   
      0.5579
      0.8965
      0.5371
      0.6112
      0.6110
      0.5746
      0.7096
      0.7456
      0.6272
      0.8375
      0.8701
      0.9366
   

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

   
      4.3751    0.8421    6.1113    6.7717    3.5744    5.2319
      7.1775    6.4557    3.2163    5.2484    8.3423    4.7257
      6.6858    1.9578    8.1813    7.1208    5.2208    0.4911
      5.6012    7.2466    1.5817    3.3188    6.7564    9.6315
      5.1335    3.7370    2.4795    0.0344    3.4544    4.5932
   
   
      0.0000    0.0000    6.1113    6.7717    0.0000    5.2319
      7.1775    6.4557    0.0000    5.2484    8.3423    0.0000
      6.6858    0.0000    8.1813    7.1208    5.2208    0.0000
      5.6012    7.2466    0.0000    0.0000    6.7564    9.6315
      5.1335    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    6.1113    6.7717    0.0000    5.2319
      7.1775    6.4557    0.0000    5.2484    8.3423    0.0000
      6.6858    0.0000    8.1813    7.1208    5.2208    0.0000
      5.6012    7.2466    0.0000    0.0000    6.7564       NaN
      5.1335    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      9.9121    0.2405    1.1013    8.5666    8.6452    2.2532
      2.4626    9.8993    2.8454    4.4828    0.2082    9.9897
      4.3437    6.5000    4.5699    2.3417    6.5000    6.5000
      3.2236    3.7622    1.4667    4.3834    8.6303    2.2912
      6.5000    9.0827    6.5000    6.5000    2.7363    9.6143
   
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
   
