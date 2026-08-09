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
      0.2798    0.5613    0.2897    0.4546
   
   R1[2] = 0.28970727100108684
   C1 = 
      0.6139
      0.1423
      0.5456
      0.4281
      0.1434
      0.7123
      0.4453
      0.8337
   
   C1[5] = 0.7123147538383577

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
      0.2431    0.7471    0.8296    0.6910    0.3991
      0.9763    0.5082    0.6592    0.0986    0.6564
   

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
   
      0.7751    0.0829    0.8488    0.7902    0.0204    0.5932    0.6549    0.1630
      0.4708    0.2341    0.9709    0.3475    0.0290    0.3205    0.6510    0.4699
      0.4696    0.4983    0.0332    0.9464    0.5265    0.8399    0.8244    0.7496
      0.5211    0.7894    0.2609    0.0906    0.4480    0.8888    0.5057    0.5689
      0.5303    0.7863    0.9530    0.3805    0.2756    0.3924    0.4492    0.5401
      0.9217    0.1638    0.4274    0.2854    0.5953    0.1405    0.8087    0.4225
      0.6681    0.6221    0.5069    0.4443    0.2557    0.1437    0.5975    0.0228
      0.7031    0.3697    0.2459    0.1340    0.0139    0.7209    0.0718    0.1721
   
   B = 
   
      0.9579    0.0566    0.4242    0.4337    0.9867    0.5359    0.9933    0.1060
      0.2735    0.7591    0.5696    0.0120    0.0357    0.9416    0.4685    0.1161
      0.5360    0.0029    0.2910    0.8293    0.2663    0.7643    0.1459    0.5251
      0.8009    0.1337    0.2653    0.7975    0.9205    0.2538    0.3792    0.6112
      0.2852    0.9396    0.8269    0.8514    0.3066    0.7730    0.1235    0.5667
      0.7512    0.5615    0.9976    0.8338    0.7680    0.7357    0.5922    0.3655
      0.4025    0.6350    0.7969    0.2176    0.3427    0.8734    0.9308    0.7384
      0.1463    0.9474    0.9752    0.5015    0.0424    0.0583    0.1874    0.9996
   
   C = 
   
      2.5918    1.1374    2.1221    2.4075    2.4143    2.3764    2.2262    1.8953
      1.8935    1.3194    2.0285    1.9585    1.5494    2.1573    1.7382    1.8833
      2.5844    2.7314    3.4050    2.6958    2.4819    2.7751    2.5338    2.6669
      2.0096    2.4217    2.9855    2.0419    1.7126    2.7200    2.1188    1.8600
      2.1718    1.9568    2.5552    2.2637    1.7181    2.7751    1.9642    2.0517
      2.0480    1.7679    2.3732    1.9956    1.8773    2.3417    2.1515    1.9237
      1.8624    1.2929    1.7564    1.5510    1.6199    2.2706    1.8747    1.3423
      1.6134    0.9656    1.5717    1.3351    1.4857    1.5608    1.4861    0.8250
   
   D = 
   
      2.5918    1.1374    2.1221    2.4075    2.4143    2.3764    2.2262    1.8953
      1.8935    1.3194    2.0285    1.9585    1.5494    2.1573    1.7382    1.8833
      2.5844    2.7314    3.4050    2.6958    2.4819    2.7751    2.5338    2.6669
      2.0096    2.4217    2.9855    2.0419    1.7126    2.7200    2.1188    1.8600
      2.1718    1.9568    2.5552    2.2637    1.7181    2.7751    1.9642    2.0517
      2.0480    1.7679    2.3732    1.9956    1.8773    2.3417    2.1515    1.9237
      1.8624    1.2929    1.7564    1.5510    1.6199    2.2706    1.8747    1.3423
      1.6134    0.9656    1.5717    1.3351    1.4857    1.5608    1.4861    0.8250
   


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

   
      0.1571    0.9936    0.9437    0.7795    0.0741    0.1083
      0.8263    0.2963    0.2925    0.3667    0.1508    0.1451
      0.8775    0.3657    0.6232    0.1613    0.3478    0.1968
      0.8767    0.0935    0.4464    0.8090    0.2277    0.6228
      0.6663    0.4327    0.0178    0.6965    0.2620    0.5861
   
   
      0.8263
      0.8775
      0.8767
      0.6663
      0.9936
      0.9437
      0.6232
      0.7795
      0.8090
      0.6965
      0.6228
      0.5861
   

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

   
      3.0245    7.2300    9.6572    5.6481    4.6597    1.2742
      2.3296    2.6186    8.0335    8.9442    5.5359    4.5274
      7.5116    3.2632    3.4099    2.3540    2.9493    0.1586
      1.4352    0.4900    6.9175    0.1548    1.9819    4.3634
      6.9999    7.8999    7.3356    8.3476    5.7659    8.3180
   
   
      0.0000    7.2300    9.6572    5.6481    0.0000    0.0000
      0.0000    0.0000    8.0335    8.9442    5.5359    0.0000
      7.5116    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.9175    0.0000    0.0000    0.0000
      6.9999    7.8999    7.3356    8.3476    5.7659    8.3180
   
   
      0.0000    7.2300       NaN    5.6481    0.0000    0.0000
      0.0000    0.0000    8.0335    8.9442    5.5359    0.0000
      7.5116    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.9175    0.0000    0.0000    0.0000
      6.9999    7.8999    7.3356    8.3476    5.7659    8.3180
   

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

   
      6.5000    6.5000    9.2280    8.9274    9.9189    9.8456
      8.1698    8.7436    6.5000    9.7850    6.5000    6.5000
      8.8318    0.9661    2.5963    6.5000    9.2767    3.0646
      1.7285    1.9721    3.8581    8.3068    0.6395    3.4722
      8.9815    9.9428    2.3851    8.7423    6.5000    9.5216
   
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
   
