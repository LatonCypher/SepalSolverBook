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
      0.6752    0.5811    0.6866    0.0178
   
   R1[2] = 0.6866435055610357
   C1 = 
      0.6275
      0.4036
      0.9232
      0.9503
      0.4573
      0.5638
      0.7387
      0.0571
   
   C1[5] = 0.5638485578278397

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
      0.1124    0.8162    0.9714    0.9581    0.0610
      0.5075    0.9438    0.3657    0.3492    0.9474
   

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
   
      0.6096    0.1978    0.9901    0.2900    0.5195    0.3223    0.1975    0.4753
      0.4456    0.5466    0.1559    0.3913    0.2685    0.1994    0.0733    0.2302
      0.1543    0.8857    0.9989    0.1663    0.2307    0.0726    0.4696    0.1848
      0.3459    0.5317    0.8397    0.3802    0.8286    0.9743    0.7811    0.5777
      0.2926    0.5279    0.9274    0.5268    0.8127    0.8335    0.0709    0.5871
      0.8635    0.3485    0.8512    0.8444    0.7756    0.0464    0.0061    0.5845
      0.1667    0.7945    0.1403    0.8504    0.1604    0.1834    0.4189    0.7844
      0.7681    0.1551    0.2075    0.8857    0.7182    0.5578    0.2584    0.3010
   
   B = 
   
      0.6071    0.7329    0.3879    0.4188    0.1308    0.1861    0.2798    0.3617
      0.8179    0.2550    0.0731    0.2356    0.3614    0.5633    0.3496    0.3334
      0.2585    0.4861    0.5639    0.3648    0.3887    0.0058    0.8127    0.9112
      0.3893    0.3436    0.3479    0.0359    0.0852    0.1916    0.7872    0.7289
      0.8226    0.1251    0.3095    0.2088    0.0264    0.7364    0.9676    0.0535
      0.6152    0.3777    0.0418    0.2010    0.8522    0.1928    0.3073    0.3066
      0.3202    0.2228    0.0470    0.1750    0.6029    0.0285    0.9059    0.7581
      0.2528    0.3597    0.4157    0.6346    0.7679    0.0391    0.6782    0.1710
   
   C = 
   
      1.7096    1.4798    1.2913    1.1829    1.3331    0.7550    2.3755    1.7575
      1.3354    0.8842    0.6275    0.6413    0.7477    0.7139    1.2941    0.9411
      1.5726    1.1091    0.9191    0.9059    1.2357    0.7698    2.0919    1.8047
      2.6871    1.7815    1.3530    1.4623    2.3629    1.2845    3.4651    2.3787
      2.4067    1.6244    1.3923    1.3264    1.8598    1.2422    2.9398    1.9641
      2.1743    1.7518    1.6196    1.3278    1.1542    1.1269    2.8866    1.9798
      1.6956    1.1501    0.9009    0.9802    1.4514    0.8384    2.2310    1.5895
      2.0844    1.4741    1.1174    0.9640    1.1939    1.0568    2.4396    1.6210
   
   D = 
   
      1.7096    1.4798    1.2913    1.1829    1.3331    0.7550    2.3755    1.7575
      1.3354    0.8842    0.6275    0.6413    0.7477    0.7139    1.2941    0.9411
      1.5726    1.1091    0.9191    0.9059    1.2357    0.7698    2.0919    1.8047
      2.6871    1.7815    1.3530    1.4623    2.3629    1.2845    3.4651    2.3787
      2.4067    1.6244    1.3923    1.3264    1.8598    1.2422    2.9398    1.9641
      2.1743    1.7518    1.6196    1.3278    1.1542    1.1269    2.8866    1.9798
      1.6956    1.1501    0.9009    0.9802    1.4514    0.8384    2.2310    1.5895
      2.0844    1.4741    1.1174    0.9640    1.1939    1.0568    2.4396    1.6210
   


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

   
      0.3078    0.9311    0.8416    0.3095    0.5603    0.1851
      0.6959    0.6051    0.3396    0.4765    0.6391    0.4262
      0.3064    0.7185    0.3180    0.7350    0.3696    0.2024
      0.2813    0.3370    0.6592    0.0816    0.6224    0.8114
      0.9023    0.1207    0.3393    0.2130    0.9847    0.2320
   
   
      0.6959
      0.9023
      0.9311
      0.6051
      0.7185
      0.8416
      0.6592
      0.7350
      0.5603
      0.6391
      0.6224
      0.9847
      0.8114
   

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

   
      3.0581    2.8019    9.0172    7.8376    3.5494    0.5491
      1.3299    2.9188    8.3102    8.9959    7.5199    5.6759
      4.9268    1.5628    9.1422    8.8163    3.3718    8.4312
      4.5335    1.4299    9.3185    3.3251    8.8459    2.4795
      4.1878    5.3992    0.1259    7.0615    4.4719    4.6877
   
   
      0.0000    0.0000    9.0172    7.8376    0.0000    0.0000
      0.0000    0.0000    8.3102    8.9959    7.5199    5.6759
      0.0000    0.0000    9.1422    8.8163    0.0000    8.4312
      0.0000    0.0000    9.3185    0.0000    8.8459    0.0000
      0.0000    5.3992    0.0000    7.0615    0.0000    0.0000
   
   
      0.0000    0.0000       NaN    7.8376    0.0000    0.0000
      0.0000    0.0000    8.3102    8.9959    7.5199    5.6759
      0.0000    0.0000       NaN    8.8163    0.0000    8.4312
      0.0000    0.0000       NaN    0.0000    8.8459    0.0000
      0.0000    5.3992    0.0000    7.0615    0.0000    0.0000
   

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

   
      2.5974    9.3185    6.5000    0.6525    0.6633    3.3454
      9.1077    6.5000    2.4633    9.8778    6.5000    8.3569
      2.3912    3.0329    6.5000    6.5000    0.1788    9.9157
      6.5000    2.2598    3.5812    6.5000    6.5000    8.6181
      6.5000    8.5566    6.5000    6.5000    3.0304    6.5000
   
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
   
