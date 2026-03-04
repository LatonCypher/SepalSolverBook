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
      0.1829    0.5482    0.7072    0.0312
   
   R1[2] = 0.7071541994923342
   C1 = 
      0.2727
      0.7870
      0.1717
      0.7605
      0.0177
      0.2702
      0.6039
      0.2163
   
   C1[5] = 0.27018948007364174

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
      0.4189    0.5379    0.3651    0.1288    0.7749
      0.7551    0.5470    0.3069    0.5390    0.8798
   

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
   
      0.9539    0.8916    0.5279    0.8154    0.2239    0.3002    0.1432    0.3475
      0.6901    0.8382    0.5807    0.9618    0.1947    0.8945    0.0949    0.7495
      0.9101    0.5839    0.6268    0.9397    0.0532    0.8659    0.7624    0.7578
      0.6521    0.6584    0.2280    0.1347    0.2075    0.8546    0.4173    0.0326
      0.1179    0.5961    0.6906    0.4166    0.2509    0.5496    0.9749    0.2041
      0.8546    0.6458    0.4940    0.8356    0.2004    0.1218    0.0095    0.8419
      0.9603    0.4909    0.6125    0.2751    0.4863    0.0794    0.9708    0.9485
      0.2629    0.8508    0.3881    0.4561    0.9757    0.6182    0.7816    0.0899
   
   B = 
   
      0.2217    0.2435    0.7519    0.6315    0.4637    0.3218    0.2743    0.1344
      0.6835    0.2166    0.7227    0.0065    0.0349    0.8984    0.6644    0.2665
      0.5460    0.3432    0.3353    0.3985    0.4203    0.3254    0.0203    0.6326
      0.5989    0.9393    0.1418    0.4048    0.5196    0.8465    0.2225    0.0662
      0.0476    0.4384    0.0497    0.1024    0.9108    0.3756    0.0810    0.5801
      0.1266    0.8796    0.7614    0.9091    0.3656    0.2716    0.4475    0.9823
      0.5161    0.0255    0.5347    0.4093    0.3898    0.6989    0.5130    0.2438
      0.6477    0.5345    0.0990    0.3722    0.2144    0.0691    0.5020    0.5048
   
   C = 
   
      1.9451    1.9240    2.0048    1.6324    1.5630    2.2596    1.4465    1.3889
      2.2759    2.7274    2.2714    2.2130    1.7951    2.4123    1.8129    2.1404
      2.5023    2.6552    2.5942    2.5954    2.0187    2.6571    2.0226    2.1865
      1.1543    1.3768    1.9490    1.5425    1.1622    1.5934    1.2806    1.4943
      1.7769    1.5136    1.7824    1.5225    1.4354    2.0897    1.4039    1.6654
      1.9762    1.9475    1.5845    1.5275    1.4718    1.8964    1.3577    1.3180
      2.1962    1.6236    2.0188    1.8375    1.9165    2.1305    1.7123    1.7413
      1.7111    1.8493    1.9532    1.5261    1.9903    2.4481    1.5485    1.9471
   
   D = 
   
      1.9451    1.9240    2.0048    1.6324    1.5630    2.2596    1.4465    1.3889
      2.2759    2.7274    2.2714    2.2130    1.7951    2.4123    1.8129    2.1404
      2.5023    2.6552    2.5942    2.5954    2.0187    2.6571    2.0226    2.1865
      1.1543    1.3768    1.9490    1.5425    1.1622    1.5934    1.2806    1.4943
      1.7769    1.5136    1.7824    1.5225    1.4354    2.0897    1.4039    1.6654
      1.9762    1.9475    1.5845    1.5275    1.4718    1.8964    1.3577    1.3180
      2.1962    1.6236    2.0188    1.8375    1.9165    2.1305    1.7123    1.7413
      1.7111    1.8493    1.9532    1.5261    1.9903    2.4481    1.5485    1.9471
   


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

   
      0.1118    0.9958    0.2622    0.9340    0.2121    0.0246
      0.1357    0.4268    0.1475    0.3818    0.7866    0.9795
      0.6460    0.6091    0.4803    0.9343    0.0861    0.6470
      0.5609    0.8739    0.1973    0.8919    0.9377    0.3657
      0.0085    0.9214    0.5657    0.9882    0.6241    0.2951
   
   
      0.6460
      0.5609
      0.9958
      0.6091
      0.8739
      0.9214
      0.5657
      0.9340
      0.9343
      0.8919
      0.9882
      0.7866
      0.9377
      0.6241
      0.9795
      0.6470
   

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

   
      0.1501    0.4245    0.0744    7.1273    1.7973    6.2534
      4.3205    3.7026    3.0238    3.8968    1.6379    2.2471
      7.1053    9.9255    2.7755    8.1850    0.3134    3.9112
      3.7597    1.7542    6.5446    2.1967    5.6787    1.9580
      4.2974    8.4623    5.7870    4.5329    4.6479    3.8379
   
   
      0.0000    0.0000    0.0000    7.1273    0.0000    6.2534
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.1053    9.9255    0.0000    8.1850    0.0000    0.0000
      0.0000    0.0000    6.5446    0.0000    5.6787    0.0000
      0.0000    8.4623    5.7870    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    7.1273    0.0000    6.2534
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.1053       NaN    0.0000    8.1850    0.0000    0.0000
      0.0000    0.0000    6.5446    0.0000    5.6787    0.0000
      0.0000    8.4623    5.7870    0.0000    0.0000    0.0000
   

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

   
      4.5923    6.5000    2.3523    4.1444    3.0370    9.5717
      8.2738    6.5000    0.0594    1.7404    3.5790    0.7314
      2.2905    4.9454    6.5000    0.5653    1.7349    8.1170
      6.5000    0.4502    8.3731    1.2060    6.5000    9.5440
      1.2755    6.5000    6.5000    9.4072    6.5000    4.0946
   
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
   
