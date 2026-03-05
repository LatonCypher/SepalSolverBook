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
      0.0264    0.8618    0.1942    0.0660
   
   R1[2] = 0.19420573708713318
   C1 = 
      0.3372
      0.0900
      0.1073
      0.9627
      0.2101
      0.9841
      0.0025
      0.3638
   
   C1[5] = 0.9841218984026776

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
      0.8552    0.3055    0.9607    0.6002    0.3377
      0.5057    0.4512    0.4687    0.1750    0.8151
   

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
   
      0.9509    0.6123    0.2834    0.2026    0.0359    0.9209    0.8760    0.2992
      0.3211    0.6315    0.5154    0.8025    0.2408    0.6456    0.7360    0.5199
      0.3247    0.9679    0.4674    0.3651    0.3880    0.7475    0.7000    0.6292
      0.9447    0.1816    0.6324    0.3379    0.4559    0.2435    0.8619    0.2055
      0.5932    0.3646    0.3464    0.2201    0.2786    0.0828    0.4731    0.3184
      0.7453    0.1958    0.9928    0.7832    0.4965    0.1176    0.4559    0.3033
      0.5224    0.1226    0.8471    0.6099    0.2949    0.4687    0.1951    0.1648
      0.0102    0.0359    0.1173    0.8060    0.6522    0.0668    0.5405    0.6524
   
   B = 
   
      0.4017    0.2874    0.1087    0.6710    0.6461    0.3681    0.5642    0.1723
      0.1919    0.7610    0.9766    0.7852    0.9903    0.7124    0.1845    0.8064
      0.9788    0.5505    0.3722    0.7848    0.9613    0.2126    0.9267    0.5346
      0.3829    0.4389    0.7027    0.8301    0.8162    0.8183    0.9668    0.0654
      0.2193    0.1110    0.1144    0.9552    0.4049    0.5288    0.5603    0.4951
      0.0507    0.7923    0.2180    0.3491    0.6582    0.1750    0.6839    0.3339
      0.8203    0.8133    0.7435    0.0085    0.9557    0.9496    0.7630    0.6533
      0.9811    0.4167    0.0653    0.2816    0.1077    0.7564    0.2043    0.7685
   
   C = 
   
      1.9211    2.5549    1.8248    1.9568    3.1485    2.2505    2.4873    1.9499
      2.2612    2.5623    2.1569    2.3900    3.2651    2.6668    2.7953    2.1077
      2.2280    2.7143    2.1800    2.4625    3.3015    2.6839    2.5392    2.4928
      2.1836    1.9362    1.5123    2.1390    2.8646    2.1456    2.6008    1.6974
      1.4973    1.3492    1.1266    1.5276    1.9107    1.5838    1.5744    1.3152
      2.3949    1.8990    1.6334    2.6878    3.0159    2.2114    2.9023    1.6844
      1.7062    1.6089    1.2122    2.1113    2.4032    1.5068    2.3604    1.2383
      1.6643    1.2854    1.1799    1.6308    1.7076    2.0772    1.8571    1.3459
   
   D = 
   
      1.9211    2.5549    1.8248    1.9568    3.1485    2.2505    2.4873    1.9499
      2.2612    2.5623    2.1569    2.3900    3.2651    2.6668    2.7953    2.1077
      2.2280    2.7143    2.1800    2.4625    3.3015    2.6839    2.5392    2.4928
      2.1836    1.9362    1.5123    2.1390    2.8646    2.1456    2.6008    1.6974
      1.4973    1.3492    1.1266    1.5276    1.9107    1.5838    1.5744    1.3152
      2.3949    1.8990    1.6334    2.6878    3.0159    2.2114    2.9023    1.6844
      1.7062    1.6089    1.2122    2.1113    2.4032    1.5068    2.3604    1.2383
      1.6643    1.2854    1.1799    1.6308    1.7076    2.0772    1.8571    1.3459
   


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

   
      0.4063    0.0501    0.9547    0.8315    0.9281    0.4409
      0.4786    0.0567    0.5089    0.8428    0.2695    0.9200
      0.6174    0.0833    0.8666    0.6170    0.9509    0.8725
      0.1326    0.5065    0.1079    0.1678    0.2541    0.2874
      0.4975    0.7640    0.2824    0.9045    0.0732    0.2554
   
   
      0.6174
      0.5065
      0.7640
      0.9547
      0.5089
      0.8666
      0.8315
      0.8428
      0.6170
      0.9045
      0.9281
      0.9509
      0.9200
      0.8725
   

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

   
      7.7194    7.6054    7.5398    7.5413    9.9096    4.8646
      4.0628    2.2794    9.2451    7.9780    3.8371    5.0270
      9.3352    9.2720    2.7188    0.3801    9.8135    3.9715
      0.6119    7.6172    0.1730    9.8115    9.0568    0.5463
      6.5369    8.8074    5.9693    4.5845    0.0226    7.1851
   
   
      7.7194    7.6054    7.5398    7.5413    9.9096    0.0000
      0.0000    0.0000    9.2451    7.9780    0.0000    5.0270
      9.3352    9.2720    0.0000    0.0000    9.8135    0.0000
      0.0000    7.6172    0.0000    9.8115    9.0568    0.0000
      6.5369    8.8074    5.9693    0.0000    0.0000    7.1851
   
   
      7.7194    7.6054    7.5398    7.5413       NaN    0.0000
      0.0000    0.0000       NaN    7.9780    0.0000    5.0270
         NaN       NaN    0.0000    0.0000       NaN    0.0000
      0.0000    7.6172    0.0000       NaN       NaN    0.0000
      6.5369    8.8074    5.9693    0.0000    0.0000    7.1851
   

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

   
      8.3481    3.4475    2.1489    9.7783    8.7507    6.5000
      4.9354    4.9914    9.5378    0.3166    6.5000    9.4139
      6.5000    9.5810    2.5439    9.8132    8.0906    4.8755
      3.3007    3.6508    8.3946    4.1565    9.4365    6.5000
      1.9498    6.5000    3.6692    6.5000    4.0736    2.3963
   
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
   
