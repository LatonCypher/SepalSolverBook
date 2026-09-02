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
      0.6185    0.5708    0.0361    0.6452
   
   R1[2] = 0.03606294367227447
   C1 = 
      0.8329
      0.2518
      0.6957
      0.4849
      0.2881
      0.6690
      0.7618
      0.6862
   
   C1[5] = 0.6689502675211993

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
      0.3686    0.7703    0.7939    0.2185    0.7713
      0.0135    0.0921    0.5663    0.1007    0.4163
   

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
   
      0.1527    0.2249    0.6194    0.4472    0.0699    0.4911    0.6580    0.8083
      0.5412    0.6428    0.5173    0.6850    0.4052    0.8246    0.4058    0.9323
      0.7825    0.6847    0.4822    0.2121    0.4374    0.3068    0.9355    0.5125
      0.7019    0.4556    0.2492    0.6110    0.3214    0.5494    0.2628    0.7120
      0.9728    0.4640    0.2837    0.0782    0.8375    0.1689    0.2700    0.6804
      0.8230    0.0524    0.4756    0.3640    0.6800    0.1427    0.8647    0.1174
      0.3883    0.0137    0.5983    0.4033    0.7742    0.3654    0.9205    0.4305
      0.5335    0.6809    0.0789    0.2262    0.8176    0.7472    0.7802    0.3191
   
   B = 
   
      0.8508    0.6623    0.5674    0.6104    0.5770    0.8678    0.0984    0.6320
      0.3059    0.1949    0.6974    0.1425    0.7396    0.1494    0.3494    0.1983
      0.5966    0.9505    0.1541    0.8663    0.8075    0.0261    0.9065    0.5752
      0.8086    0.1139    0.6178    0.0074    0.1242    0.6286    0.0466    0.5656
      0.1517    0.1922    0.9212    0.3001    0.7633    0.0491    0.8326    0.0305
      0.3716    0.2789    0.3646    0.7511    0.4474    0.4085    0.9409    0.1850
      0.3540    0.8303    0.1775    0.0712    0.5969    0.6153    0.0607    0.5810
      0.6311    0.5256    0.3260    0.6761    0.2789    0.7382    0.9222    0.7411
   
   C = 
   
      1.8660    1.9062    1.2390    1.6483    1.7014    1.6690    1.9816    1.8246
      2.6196    2.1884    2.3082    2.2754    2.4710    2.3046    2.7765    2.2461
      2.1693    2.3500    1.9748    1.7692    2.5461    2.0280    1.9455    2.0210
      2.1745    1.6676    1.9070    1.7229    1.8656    1.9953    1.9397    1.8146
      1.9168    1.8031    2.0703    1.7636    2.2090    1.7486    2.0187    1.6320
      1.8307    1.9990    1.6719    1.3767    2.0747    1.6737    1.4085    1.6465
      1.8685    2.1158    1.7215    1.6237    2.1914    1.6798    2.0455    1.7655
      1.7715    1.7680    2.1977    1.5706    2.4164    1.7700    2.0979    1.4985
   
   D = 
   
      1.8660    1.9062    1.2390    1.6483    1.7014    1.6690    1.9816    1.8246
      2.6196    2.1884    2.3082    2.2754    2.4710    2.3046    2.7765    2.2461
      2.1693    2.3500    1.9748    1.7692    2.5461    2.0280    1.9455    2.0210
      2.1745    1.6676    1.9070    1.7229    1.8656    1.9953    1.9397    1.8146
      1.9168    1.8031    2.0703    1.7636    2.2090    1.7486    2.0187    1.6320
      1.8307    1.9990    1.6719    1.3767    2.0747    1.6737    1.4085    1.6465
      1.8685    2.1158    1.7215    1.6237    2.1914    1.6798    2.0455    1.7655
      1.7715    1.7680    2.1977    1.5706    2.4164    1.7700    2.0979    1.4985
   


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

   
      0.2633    0.8338    0.7469    0.9795    0.2413    0.9881
      0.0207    0.0732    0.4685    0.3427    0.5423    0.0589
      0.7473    0.1961    0.7073    0.9718    0.7850    0.1839
      0.0573    0.9909    0.4915    0.1125    0.3740    0.7513
      0.1438    0.6749    0.6645    0.5570    0.7879    0.4961
   
   
      0.7473
      0.8338
      0.9909
      0.6749
      0.7469
      0.7073
      0.6645
      0.9795
      0.9718
      0.5570
      0.5423
      0.7850
      0.7879
      0.9881
      0.7513
   

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

   
      8.3282    2.2836    5.7062    5.3305    4.6378    7.7971
      7.0210    6.0040    6.0838    0.7596    2.4879    8.8355
      4.1388    6.7454    7.0052    0.7921    7.3049    8.9294
      4.5468    8.0381    2.1306    6.2564    2.6068    9.3543
      0.0872    2.0372    6.2486    9.2690    5.3962    9.7588
   
   
      8.3282    0.0000    5.7062    5.3305    0.0000    7.7971
      7.0210    6.0040    6.0838    0.0000    0.0000    8.8355
      0.0000    6.7454    7.0052    0.0000    7.3049    8.9294
      0.0000    8.0381    0.0000    6.2564    0.0000    9.3543
      0.0000    0.0000    6.2486    9.2690    5.3962    9.7588
   
   
      8.3282    0.0000    5.7062    5.3305    0.0000    7.7971
      7.0210    6.0040    6.0838    0.0000    0.0000    8.8355
      0.0000    6.7454    7.0052    0.0000    7.3049    8.9294
      0.0000    8.0381    0.0000    6.2564    0.0000       NaN
      0.0000    0.0000    6.2486       NaN    5.3962       NaN
   

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

   
      3.6205    2.4490    9.9083    3.2408    9.1703    1.0858
      2.9857    8.9289    4.7750    1.8272    8.0466    0.5550
      2.1269    2.9286    4.9186    9.9891    2.8856    9.8749
      0.5347    6.5000    6.5000    1.9590    9.1922    4.2491
      0.4417    3.3485    4.1585    6.5000    6.5000    1.8089
   
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
   
