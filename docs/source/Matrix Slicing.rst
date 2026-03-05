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
      0.4843    0.5375    0.4339    0.8101
   
   R1[2] = 0.43394730682364135
   C1 = 
      0.9128
      0.0557
      0.0672
      0.0950
      0.6954
      0.2671
      0.8604
      0.2383
   
   C1[5] = 0.2670705029625491

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
      0.3982    0.5184    0.8745    0.6429    0.5307
      0.2389    0.1741    0.3497    0.9181    0.1585
   

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
   
      0.4613    0.7224    0.2753    0.8275    0.3660    0.0234    0.5117    0.6574
      0.8826    0.2014    0.0149    0.9451    0.2975    0.6536    0.5044    0.0171
      0.9258    0.9386    0.4329    0.1854    0.5918    0.9795    0.1966    0.8462
      0.3135    0.8396    0.3374    0.5944    0.2418    0.3082    0.6049    0.1436
      0.5970    0.0438    0.8925    0.8213    0.2806    0.3598    0.6463    0.5663
      0.7542    0.5669    0.2682    0.5705    0.9861    0.0129    0.6735    0.2741
      0.3322    0.6084    0.8922    0.6440    0.6465    0.2391    0.3324    0.4456
      0.4809    0.4350    0.2172    0.5447    0.1397    0.1170    0.7109    0.4181
   
   B = 
   
      0.3020    0.4465    0.9329    0.0662    0.9049    0.3463    0.6766    0.7892
      0.6941    0.1980    0.9428    0.2633    0.7465    0.0689    0.6878    0.1649
      0.5287    0.4486    0.5333    0.2735    0.9638    0.3021    0.6865    0.4155
      0.5273    0.6260    0.6051    0.0580    0.5069    0.4187    0.6396    0.5853
      0.2692    0.5862    0.0380    0.6832    0.1506    0.6825    0.0921    0.2644
      0.6329    0.5739    0.2892    0.7323    0.5143    0.5587    0.3091    0.3414
      0.8935    0.8467    0.8049    0.8186    0.0729    0.2916    0.0815    0.7933
      0.4040    0.5187    0.4629    0.8366    0.6684    0.1633    0.3972    0.5800
   
   C = 
   
      2.0588    1.9927    2.4958    1.5799    2.1854    1.1586    1.8711    1.9738
      1.8639    2.0177    2.2073    1.2794    1.8716    1.4378    1.6277    2.0009
      2.5545    2.4240    2.9474    2.4280    3.2225    1.7403    2.3972    2.3114
      2.0279    1.7350    2.2753    1.3747    1.8719    1.0542    1.6252    1.6062
      2.2252    2.4016    2.4683    1.8006    2.5024    1.4967    1.9867    2.3680
      2.0501    2.2243    2.4365    1.7693    2.0408    1.5415    1.7078    2.0923
      2.1362    2.1009    2.3165    1.7251    2.4835    1.4406    2.0051    1.8847
      1.7650    1.7071    2.1090    1.3501    1.6578    0.9264    1.3951    1.7436
   
   D = 
   
      2.0588    1.9927    2.4958    1.5799    2.1854    1.1586    1.8711    1.9738
      1.8639    2.0177    2.2073    1.2794    1.8716    1.4378    1.6277    2.0009
      2.5545    2.4240    2.9474    2.4280    3.2225    1.7403    2.3972    2.3114
      2.0279    1.7350    2.2753    1.3747    1.8719    1.0542    1.6252    1.6062
      2.2252    2.4016    2.4683    1.8006    2.5024    1.4967    1.9867    2.3680
      2.0501    2.2243    2.4365    1.7693    2.0408    1.5415    1.7078    2.0923
      2.1362    2.1009    2.3165    1.7251    2.4835    1.4406    2.0051    1.8847
      1.7650    1.7071    2.1090    1.3501    1.6578    0.9264    1.3951    1.7436
   


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

   
      0.7459    0.7705    0.4285    0.0289    0.6224    0.9574
      0.4179    0.8367    0.6430    0.1766    0.6671    0.0323
      0.4556    0.3802    0.5300    0.5359    0.6658    0.7940
      0.9110    0.6636    0.5031    0.5457    0.4493    0.3737
      0.3910    0.1014    0.3571    0.3275    0.0997    0.3217
   
   
      0.7459
      0.9110
      0.7705
      0.8367
      0.6636
      0.6430
      0.5300
      0.5031
      0.5359
      0.5457
      0.6224
      0.6671
      0.6658
      0.9574
      0.7940
   

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

   
      0.0371    5.9877    4.2841    8.6141    6.9347    4.8723
      8.2010    6.9093    4.1063    2.5818    9.2197    2.6969
      6.8768    3.8822    6.0378    7.1139    6.8551    6.6163
      3.5146    5.4852    2.1711    9.8356    7.7349    6.7855
      1.1359    5.6368    1.7979    6.1919    3.0552    3.3965
   
   
      0.0000    5.9877    0.0000    8.6141    6.9347    0.0000
      8.2010    6.9093    0.0000    0.0000    9.2197    0.0000
      6.8768    0.0000    6.0378    7.1139    6.8551    6.6163
      0.0000    5.4852    0.0000    9.8356    7.7349    6.7855
      0.0000    5.6368    0.0000    6.1919    0.0000    0.0000
   
   
      0.0000    5.9877    0.0000    8.6141    6.9347    0.0000
      8.2010    6.9093    0.0000    0.0000       NaN    0.0000
      6.8768    0.0000    6.0378    7.1139    6.8551    6.6163
      0.0000    5.4852    0.0000       NaN    7.7349    6.7855
      0.0000    5.6368    0.0000    6.1919    0.0000    0.0000
   

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

   
      3.5959    1.3984    6.5000    2.5366    6.5000    1.2495
      8.6867    4.8143    4.0785    9.8012    1.9167    8.5300
      6.5000    6.5000    0.2155    2.6547    8.6891    4.3987
      8.7518    8.2163    2.8222    1.0091    3.9481    6.5000
      6.5000    2.9708    6.5000    3.1115    6.5000    8.9241
   
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
   
