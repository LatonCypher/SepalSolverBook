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
      0.8976    0.4125    0.2159    0.0596
   
   R1[2] = 0.2159400404003452
   C1 = 
      0.5412
      0.5547
      0.8433
      0.2783
      0.8906
      0.7646
      0.9731
      0.7629
   
   C1[5] = 0.7645586369750943

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
      0.8251    0.2648    0.7346    0.2539    0.0385
      0.0063    0.2375    0.3137    0.7397    0.6813
   

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
   
      0.7790    0.1908    0.2205    0.4177    0.7037    0.8755    0.6682    0.2598
      0.6945    0.8701    0.4921    0.1669    0.0083    0.4954    0.4021    0.6887
      0.1258    0.3438    0.7918    0.3885    0.5561    0.8246    0.3953    0.7002
      0.5847    0.9175    0.8170    0.8451    0.6114    0.6164    0.2245    0.0082
      0.9914    0.1720    0.9657    0.8961    0.4779    0.5198    0.8739    0.8143
      0.2117    0.4173    0.4873    0.1464    0.6318    0.5200    0.3863    0.6821
      0.8211    0.5572    0.0281    0.3631    0.1024    0.8225    0.2108    0.0438
      0.9009    0.7754    0.3406    0.7651    0.2558    0.2030    0.2792    0.4214
   
   B = 
   
      0.5792    0.1222    0.9085    0.6969    0.7211    0.0245    0.7426    0.7238
      0.6991    0.1437    0.6318    0.8639    0.0881    0.3595    0.2076    0.0448
      0.8942    0.3478    0.6976    0.8805    0.6165    0.3253    0.2713    0.9011
      0.1584    0.1346    0.7383    0.6729    0.1642    0.5273    0.5895    0.5002
      0.3800    0.2272    0.8213    0.4792    0.4401    0.8860    0.0135    0.4137
      0.2366    0.6371    0.2742    0.7427    0.4924    0.5571    0.0660    0.2327
      0.4398    0.0846    0.3348    0.7034    0.1614    0.3586    0.2683    0.2219
      0.4729    0.0506    0.0227    0.3035    0.4364    0.1268    0.7315    0.8548
   
   C = 
   
      1.7392    1.0428    2.3381    2.7193    1.7451    1.7634    1.3608    1.8452
      2.0999    0.7898    1.9400    2.6451    1.5212    1.0927    1.5728    1.8653
      1.9941    1.1130    2.0017    2.7127    1.6930    1.7716    1.2888    2.1224
      2.3253    1.1523    3.0512    3.3994    1.7573    2.0222    1.4597    2.0765
      2.7739    1.1572    3.1908    3.7697    2.4352    2.0024    2.4332    3.2523
      1.7289    0.8171    1.7103    2.2034    1.4080    1.4654    1.1078    1.7354
      1.2947    0.8063    1.7668    2.1442    1.2213    1.0511    1.0914    1.1442
      1.9570    0.6753    2.4796    2.7101    1.4950    1.3084    1.7735    1.9517
   
   D = 
   
      1.7392    1.0428    2.3381    2.7193    1.7451    1.7634    1.3608    1.8452
      2.0999    0.7898    1.9400    2.6451    1.5212    1.0927    1.5728    1.8653
      1.9941    1.1130    2.0017    2.7127    1.6930    1.7716    1.2888    2.1224
      2.3253    1.1523    3.0512    3.3994    1.7573    2.0222    1.4597    2.0765
      2.7739    1.1572    3.1908    3.7697    2.4352    2.0024    2.4332    3.2523
      1.7289    0.8171    1.7103    2.2034    1.4080    1.4654    1.1078    1.7354
      1.2947    0.8063    1.7668    2.1442    1.2213    1.0511    1.0914    1.1442
      1.9570    0.6753    2.4796    2.7101    1.4950    1.3084    1.7735    1.9517
   


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

   
      0.2955    0.2057    0.7575    0.8178    0.1927    0.2798
      0.2091    0.9302    0.7114    0.2879    0.7081    0.9220
      0.7507    0.0163    0.0356    0.1768    0.2403    0.8799
      0.6256    0.8929    0.2498    0.3255    0.8076    0.3955
      0.6362    0.0699    0.0226    0.4558    0.8396    0.8895
   
   
      0.7507
      0.6256
      0.6362
      0.9302
      0.8929
      0.7575
      0.7114
      0.8178
      0.7081
      0.8076
      0.8396
      0.9220
      0.8799
      0.8895
   

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

   
      7.7742    6.3833    7.1460    9.1868    8.8242    9.7851
      5.2670    5.7235    0.8272    3.7645    4.6892    0.2231
      7.1400    7.4050    2.0223    6.9314    3.4981    6.7276
      2.4976    4.7199    6.6214    7.7103    0.2821    6.1989
      8.8864    7.3119    5.3829    6.1196    3.5957    4.6302
   
   
      7.7742    6.3833    7.1460    9.1868    8.8242    9.7851
      5.2670    5.7235    0.0000    0.0000    0.0000    0.0000
      7.1400    7.4050    0.0000    6.9314    0.0000    6.7276
      0.0000    0.0000    6.6214    7.7103    0.0000    6.1989
      8.8864    7.3119    5.3829    6.1196    0.0000    0.0000
   
   
      7.7742    6.3833    7.1460       NaN    8.8242       NaN
      5.2670    5.7235    0.0000    0.0000    0.0000    0.0000
      7.1400    7.4050    0.0000    6.9314    0.0000    6.7276
      0.0000    0.0000    6.6214    7.7103    0.0000    6.1989
      8.8864    7.3119    5.3829    6.1196    0.0000    0.0000
   

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

   
      1.3718    8.5291    9.0775    2.8762    8.9863    9.0101
      2.5916    6.5000    2.9984    2.1345    2.3135    2.8358
      6.5000    4.8621    6.5000    6.5000    8.4725    4.8007
      3.5384    6.5000    6.5000    4.4167    8.4158    3.7314
      2.7374    4.3984    8.8613    6.5000    6.5000    0.3691
   
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
   
