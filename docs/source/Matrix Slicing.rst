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
      0.1921    0.0050    0.9904    0.0632
   
   R1[2] = 0.9904286406986933
   C1 = 
      0.0875
      0.1506
      0.8606
      0.7049
      0.7446
      0.3407
      0.6480
      0.7365
   
   C1[5] = 0.34074394967527855

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
      0.9532    0.2261    0.3927    0.1171    0.9148
      0.5330    0.7325    0.0102    0.6568    0.3156
   

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
   
      0.2959    0.0506    0.5308    0.1858    0.0749    0.4807    0.8974    0.7900
      0.6830    0.3447    0.1342    0.4983    0.5470    0.3427    0.1012    0.6755
      0.4146    0.5953    0.6947    0.7003    0.4859    0.9436    0.0673    0.9592
      0.8859    0.3969    0.1391    0.9486    0.0668    0.1958    0.6932    0.8711
      0.2189    0.4038    0.3138    0.6537    0.3899    0.5446    0.8432    0.1056
      0.4076    0.6446    0.5176    0.8636    0.0283    0.5858    0.5558    0.4478
      0.1613    0.0846    0.0208    0.2860    0.1169    0.0892    0.3387    0.0912
      0.1222    0.5871    0.4952    0.7087    0.0853    0.3254    0.5642    0.2031
   
   B = 
   
      0.3008    0.2204    0.5148    0.7169    0.1730    0.5172    0.0761    0.2733
      0.2114    0.4246    0.5411    0.9050    0.1182    0.5244    0.3889    0.9297
      0.7924    0.2827    0.0183    0.0802    0.9076    0.0423    0.5450    0.8948
      0.3717    0.9515    0.1480    0.5691    0.9941    0.3864    0.9907    0.5539
      0.6378    0.7162    0.4621    0.2472    0.6713    0.6260    0.4436    0.8002
      0.8557    0.3097    0.4398    0.4896    0.3850    0.9487    0.9610    0.5775
      0.5974    0.3443    0.5091    0.2595    0.9806    0.7124    0.0099    0.9182
      0.9376    0.4898    0.0298    0.8895    0.7018    0.1026    0.9162    0.4310
   
   C = 
   
      2.3254    1.3120    0.9434    1.5958    2.3934    1.4972    1.7435    2.2078
      1.9059    1.6725    1.0895    2.0261    1.8485    1.5411    1.9448    1.9229
      3.1184    2.3401    1.3543    2.7431    2.8975    2.1723    3.3375    3.0853
      2.2543    2.0795    1.3097    2.6125    2.6807    1.8495    2.2602    2.4395
      1.9603    1.7202    1.2858    1.5953    2.3926    1.9631    1.7938    2.5244
      2.2614    1.9439    1.2630    2.2450    2.5789    1.9199    2.4108    2.7166
      0.6280    0.6221    0.4399    0.5982    0.8501    0.6476    0.5644    0.7951
      1.6771    1.5460    0.9704    1.5695    2.1228    1.4507    1.7519    2.2767
   
   D = 
   
      2.3254    1.3120    0.9434    1.5958    2.3934    1.4972    1.7435    2.2078
      1.9059    1.6725    1.0895    2.0261    1.8485    1.5411    1.9448    1.9229
      3.1184    2.3401    1.3543    2.7431    2.8975    2.1723    3.3375    3.0853
      2.2543    2.0795    1.3097    2.6125    2.6807    1.8495    2.2602    2.4395
      1.9603    1.7202    1.2858    1.5953    2.3926    1.9631    1.7938    2.5244
      2.2614    1.9439    1.2630    2.2450    2.5789    1.9199    2.4108    2.7166
      0.6280    0.6221    0.4399    0.5982    0.8501    0.6476    0.5644    0.7951
      1.6771    1.5460    0.9704    1.5695    2.1228    1.4507    1.7519    2.2767
   


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

   
      0.4861    0.6796    0.2634    0.8453    0.1046    0.5658
      0.0480    0.6813    0.3174    0.2596    0.2290    0.8959
      0.8671    0.0036    0.7744    0.5408    0.3693    0.1326
      0.1589    0.9534    0.3545    0.8522    0.6983    0.3879
      0.6794    0.0584    0.3843    0.7678    0.9692    0.6238
   
   
      0.8671
      0.6794
      0.6796
      0.6813
      0.9534
      0.7744
      0.8453
      0.5408
      0.8522
      0.7678
      0.6983
      0.9692
      0.5658
      0.8959
      0.6238
   

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

   
      1.8337    1.7885    9.9846    3.3959    9.4990    3.2557
      6.4237    1.6161    7.3379    3.2049    7.8356    8.7413
      6.5619    7.1836    6.6633    2.4487    9.5833    4.1605
      7.4735    5.7629    4.9011    7.1249    2.7076    2.1763
      0.1794    9.4883    7.0491    0.2977    2.8308    9.2559
   
   
      0.0000    0.0000    9.9846    0.0000    9.4990    0.0000
      6.4237    0.0000    7.3379    0.0000    7.8356    8.7413
      6.5619    7.1836    6.6633    0.0000    9.5833    0.0000
      7.4735    5.7629    0.0000    7.1249    0.0000    0.0000
      0.0000    9.4883    7.0491    0.0000    0.0000    9.2559
   
   
      0.0000    0.0000       NaN    0.0000       NaN    0.0000
      6.4237    0.0000    7.3379    0.0000    7.8356    8.7413
      6.5619    7.1836    6.6633    0.0000       NaN    0.0000
      7.4735    5.7629    0.0000    7.1249    0.0000    0.0000
      0.0000       NaN    7.0491    0.0000    0.0000       NaN
   

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

   
      6.5000    6.5000    8.4116    6.5000    6.5000    6.5000
      6.5000    9.2662    3.5896    1.1897    6.5000    6.5000
      9.5383    9.5726    1.4926    3.5434    6.5000    8.7500
      6.5000    6.5000    6.5000    2.4740    9.2776    4.6924
      1.1366    8.9319    3.4925    3.1141    0.6192    6.5000
   
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
   
