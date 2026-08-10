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
      0.8278    0.4383    0.2643    0.4138
   
   R1[2] = 0.2642638549330285
   C1 = 
      0.7751
      0.3900
      0.8204
      0.8582
      0.5169
      0.0349
      0.8561
      0.8398
   
   C1[5] = 0.03486557719543493

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
      0.6776    0.7503    0.1497    0.2096    0.9377
      0.6108    0.0830    0.5026    0.1142    0.5474
   

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
   
      0.5203    0.1833    0.6659    0.3087    0.3334    0.5707    0.6018    0.7090
      0.2090    0.1491    0.5176    0.7325    0.1798    0.2224    0.3319    0.2477
      0.5437    0.7520    0.3413    0.3632    0.7489    0.6803    0.2593    0.0733
      0.3269    0.8582    0.2666    0.1888    0.3415    0.0956    0.5699    0.3654
      0.3481    0.0087    0.3521    0.6039    0.5378    0.0755    0.5649    0.6875
      0.4104    0.8726    0.8603    0.9158    0.8386    0.2633    0.2742    0.2165
      0.4438    0.8800    0.8548    0.4520    0.4969    0.3561    0.4771    0.0843
      0.8488    0.6195    0.1424    0.7732    0.7438    0.0377    0.4288    0.5597
   
   B = 
   
      0.2855    0.0890    0.8127    0.5076    0.5661    0.5139    0.6681    0.7782
      0.5431    0.5939    0.3007    0.2015    0.9125    0.5615    0.1959    0.3742
      0.1578    0.9691    0.4399    0.3711    0.8154    0.2507    0.4285    0.7271
      0.1900    0.0204    0.5208    0.8929    0.0338    0.0098    0.8766    0.0336
      0.9662    0.9481    0.8102    0.3695    0.5419    0.7997    0.6362    0.0410
      0.9295    0.5978    0.5167    0.9764    0.2130    0.5000    0.9676    0.2464
      0.7083    0.9412    0.6989    0.0099    0.7382    0.7055    0.8151    0.4065
      0.4765    0.6683    0.9189    0.3558    0.1654    0.3576    0.0381    0.3724
   
   C = 
   
      2.0284    2.5042    2.5688    1.7624    1.8789    1.7704    2.2213    1.6310
      1.0950    1.4050    1.5440    1.3573    1.1319    0.9058    1.6422    0.9087
      2.2611    2.2430    2.2142    1.8481    2.0389    1.9391    2.3241    1.2959
      1.6340    1.9626    1.7996    0.9615    1.8785    1.5722    1.4545    1.1810
      1.5919    1.9359    2.2564    1.3710    1.3508    1.3903    1.8164    1.0768
      2.2531    2.7623    2.6574    2.1678    2.5097    1.9987    2.6365    1.5935
      2.0146    2.5891    2.2342    1.6894    2.4778    1.8831    2.2842    1.6448
      2.0723    2.1026    2.7776    1.8139    2.0083    1.9437    2.3078    1.4444
   
   D = 
   
      2.0284    2.5042    2.5688    1.7624    1.8789    1.7704    2.2213    1.6310
      1.0950    1.4050    1.5440    1.3573    1.1319    0.9058    1.6422    0.9087
      2.2611    2.2430    2.2142    1.8481    2.0389    1.9391    2.3241    1.2959
      1.6340    1.9626    1.7996    0.9615    1.8785    1.5722    1.4545    1.1810
      1.5919    1.9359    2.2564    1.3710    1.3508    1.3903    1.8164    1.0768
      2.2531    2.7623    2.6574    2.1678    2.5097    1.9987    2.6365    1.5935
      2.0146    2.5891    2.2342    1.6894    2.4778    1.8831    2.2842    1.6448
      2.0723    2.1026    2.7776    1.8139    2.0083    1.9437    2.3078    1.4444
   


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

   
      0.8274    0.0052    0.5943    0.5418    0.0629    0.5215
      0.5821    0.8042    0.9152    0.8373    0.2123    0.3578
      0.5351    0.7244    0.6752    0.0020    0.7816    0.9769
      0.8355    0.1283    0.4408    0.0642    0.3969    0.4225
      0.2797    0.5106    0.8607    0.9107    0.3166    0.9899
   
   
      0.8274
      0.5821
      0.5351
      0.8355
      0.8042
      0.7244
      0.5106
      0.5943
      0.9152
      0.6752
      0.8607
      0.5418
      0.8373
      0.9107
      0.7816
      0.5215
      0.9769
      0.9899
   

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

   
      8.7979    6.0517    0.8507    0.0320    6.1428    7.0077
      3.7618    0.3656    4.8315    5.8527    9.0726    3.0933
      7.4230    9.3521    3.0874    0.4481    4.2918    3.5185
      5.5214    5.5569    6.4986    0.5265    6.1530    3.1958
      9.4701    8.5281    1.4940    2.6194    8.3193    8.3462
   
   
      8.7979    6.0517    0.0000    0.0000    6.1428    7.0077
      0.0000    0.0000    0.0000    5.8527    9.0726    0.0000
      7.4230    9.3521    0.0000    0.0000    0.0000    0.0000
      5.5214    5.5569    6.4986    0.0000    6.1530    0.0000
      9.4701    8.5281    0.0000    0.0000    8.3193    8.3462
   
   
      8.7979    6.0517    0.0000    0.0000    6.1428    7.0077
      0.0000    0.0000    0.0000    5.8527       NaN    0.0000
      7.4230       NaN    0.0000    0.0000    0.0000    0.0000
      5.5214    5.5569    6.4986    0.0000    6.1530    0.0000
         NaN    8.5281    0.0000    0.0000    8.3193    8.3462
   

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

   
      2.0566    6.5000    6.5000    6.5000    0.5056    3.0993
      3.8201    6.5000    2.6241    3.4575    0.7636    2.1810
      9.5896    0.1565    2.5031    1.0670    1.6499    6.5000
      6.5000    2.9841    9.8023    4.5383    9.8396    4.7792
      3.0657    4.9493    6.5000    9.4212    2.3045    6.5000
   
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
   
