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
      0.0882    0.5194    0.0480    0.9720
   
   R1[2] = 0.04804477787083772
   C1 = 
      0.2456
      0.9637
      0.7787
      0.1624
      0.3454
      0.3522
      0.7744
      0.5434
   
   C1[5] = 0.35224797350086656

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
      0.2897    0.8920    0.7541    0.4292    0.9276
      0.0945    0.7467    0.3277    0.6574    0.3976
   

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
   
      0.7107    0.4325    0.9631    0.0145    0.7846    0.4625    0.4422    0.4277
      0.7004    0.4866    0.2803    0.9645    0.7361    0.6353    0.3969    0.7617
      0.6167    0.5799    0.2728    0.8895    0.6351    0.5042    0.2204    0.4341
      0.4125    0.8982    0.0025    0.1862    0.4583    0.7331    0.2524    0.8020
      0.1210    0.7521    0.3781    0.9980    0.5451    0.7534    0.7995    0.3038
      0.6796    0.2851    0.8339    0.5553    0.8076    0.2642    0.5887    0.4045
      0.3129    0.8746    0.4187    0.8742    0.5871    0.1133    0.9363    0.7389
      0.7496    0.4670    0.9162    0.7361    0.2522    0.1719    0.9726    0.3125
   
   B = 
   
      0.1601    0.7234    0.0889    0.9750    0.5512    0.3158    0.9674    0.0672
      0.8304    0.7306    0.5151    0.8325    0.8424    0.6279    0.4291    0.9522
      0.4075    0.6231    0.4402    0.4577    0.8826    0.6305    0.4339    0.7574
      0.7401    0.7144    0.4078    0.9701    0.7781    0.3289    0.6460    0.5202
      0.0789    0.8714    0.3017    0.4881    0.8257    0.1072    0.9407    0.6852
      0.0791    0.0197    0.6777    0.4160    0.3305    0.6755    0.1602    0.6643
      0.6292    0.5968    0.5441    0.3845    0.0510    0.4834    0.9433    0.2338
      0.3432    0.0428    0.9702    0.3756    0.7503    0.4894    0.2117    0.0895
   
   C = 
   
      1.3997    2.4156    1.9216    2.4139    2.7616    1.9277    2.6202    2.1831
      1.9637    2.6493    2.4372    3.2142    3.2034    2.0934    2.9610    2.3118
      1.7274    2.3887    1.9108    2.8393    2.7893    1.7511    2.5164    2.1234
      1.4789    1.6879    2.1268    2.2586    2.3665    1.8159    1.8622    1.9135
      2.2466    2.5655    2.3765    2.8864    2.7784    2.1797    2.7005    2.6179
      1.6901    2.6937    1.9362    2.7026    2.8704    1.8496    2.9432    2.1402
      2.4919    2.8549    2.4993    3.0440    3.0833    2.1531    3.0344    2.3882
      2.1787    2.7972    2.0357    2.9391    2.7372    2.1160    3.0470    2.1143
   
   D = 
   
      1.3997    2.4156    1.9216    2.4139    2.7616    1.9277    2.6202    2.1831
      1.9637    2.6493    2.4372    3.2142    3.2034    2.0934    2.9610    2.3118
      1.7274    2.3887    1.9108    2.8393    2.7893    1.7511    2.5164    2.1234
      1.4789    1.6879    2.1268    2.2586    2.3665    1.8159    1.8622    1.9135
      2.2466    2.5655    2.3765    2.8864    2.7784    2.1797    2.7005    2.6179
      1.6901    2.6937    1.9362    2.7026    2.8704    1.8496    2.9432    2.1402
      2.4919    2.8549    2.4993    3.0440    3.0833    2.1531    3.0344    2.3882
      2.1787    2.7972    2.0357    2.9391    2.7372    2.1160    3.0470    2.1143
   


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

   
      0.2884    0.5356    0.0943    0.2700    0.7024    0.5900
      0.9968    0.6764    0.7984    0.7579    0.2641    0.1167
      0.3691    0.6395    0.9665    0.3557    0.3313    0.0373
      0.7996    0.8846    0.1041    0.4958    0.0822    0.2291
      0.9411    0.4551    0.5283    0.5696    0.6134    0.2202
   
   
      0.9968
      0.7996
      0.9411
      0.5356
      0.6764
      0.6395
      0.8846
      0.7984
      0.9665
      0.5283
      0.7579
      0.5696
      0.7024
      0.6134
      0.5900
   

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

   
      3.9587    5.5981    1.0928    1.9401    5.3474    3.9074
      8.2994    6.5814    2.1750    7.1105    8.9891    2.9016
      1.0708    1.5380    5.8491    4.5976    3.1809    9.9234
      1.7940    7.1572    3.2949    3.0022    3.1855    9.9077
      8.7129    7.2665    0.1636    5.7764    1.8724    4.7243
   
   
      0.0000    5.5981    0.0000    0.0000    5.3474    0.0000
      8.2994    6.5814    0.0000    7.1105    8.9891    0.0000
      0.0000    0.0000    5.8491    0.0000    0.0000    9.9234
      0.0000    7.1572    0.0000    0.0000    0.0000    9.9077
      8.7129    7.2665    0.0000    5.7764    0.0000    0.0000
   
   
      0.0000    5.5981    0.0000    0.0000    5.3474    0.0000
      8.2994    6.5814    0.0000    7.1105    8.9891    0.0000
      0.0000    0.0000    5.8491    0.0000    0.0000       NaN
      0.0000    7.1572    0.0000    0.0000    0.0000       NaN
      8.7129    7.2665    0.0000    5.7764    0.0000    0.0000
   

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

   
      8.4038    4.5599    1.0009    1.5554    0.2959    4.3113
      6.5000    8.6049    2.2160    2.4019    3.4583    8.8208
      6.5000    4.0714    8.5539    6.5000    0.0228    9.7701
      6.5000    9.1838    2.2734    4.1743    6.5000    6.5000
      3.7057    8.3635    8.5183    3.5132    6.5000    9.3500
   
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
   
