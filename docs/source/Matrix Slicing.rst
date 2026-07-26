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
      0.7370    0.9563    0.9574    0.7536
   
   R1[2] = 0.9574000309081978
   C1 = 
      0.1313
      0.7563
      0.3734
      0.7809
      0.3510
      0.8361
      0.7598
      0.0694
   
   C1[5] = 0.8361482385977359

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
      0.0299    0.3465    0.9955    0.2772    0.4905
      0.3467    0.8505    0.4777    0.2956    0.2977
   

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
   
      0.3555    0.2161    0.5665    0.9170    0.1127    0.8396    0.6698    0.7098
      0.0034    0.1212    0.6919    0.7098    0.6961    0.0635    0.0618    0.5548
      0.3200    0.4143    0.5144    0.1597    0.4355    0.2524    0.6125    0.3181
      0.1902    0.7926    0.7586    0.7528    0.4793    0.1921    0.4639    0.5790
      0.6660    0.2765    0.4840    0.6293    0.6069    0.1789    0.5156    0.1277
      0.6635    0.1432    0.1300    0.4018    0.1855    0.6685    0.1501    0.3812
      0.4944    0.7193    0.4005    0.2136    0.0366    0.0448    0.0647    0.5837
      0.9344    0.3166    0.8419    0.4756    0.5513    0.1948    0.2000    0.7651
   
   B = 
   
      0.0930    0.8077    0.0425    0.8763    0.3129    0.6054    0.8363    0.4126
      0.4067    0.2976    0.7991    0.4939    0.5510    0.5351    0.2241    0.8324
      0.2184    0.5180    0.0442    0.3836    0.6950    0.8073    0.9505    0.4791
      0.4941    0.3544    0.1414    0.9110    0.3290    0.8513    0.5488    0.2070
      0.0552    0.2811    0.3575    0.5385    0.2868    0.5795    0.6519    0.4636
      0.4380    0.8437    0.9871    0.2805    0.2624    0.7037    0.4810    0.9813
      0.1471    0.0423    0.8988    0.1449    0.6673    0.1619    0.2264    0.2597
      0.7056    0.8993    0.0353    0.1473    0.5858    0.7259    0.8119    0.0735
   
   C = 
   
      1.6710    2.3766    1.8386    1.9687    2.0411    2.8486    2.5927    1.8901
      1.0182    1.3996    0.6145    1.4581    1.3648    2.0905    2.0259    1.0225
      0.8387    1.3522    1.3566    1.2689    1.5247    1.7267    1.7393    1.3885
      1.4650    1.8864    1.5798    1.9994    2.1078    2.7008    2.4508    1.8312
      0.8689    1.5522    1.2212    1.9497    1.5441    2.1313    2.1265    1.4674
      0.9408    1.7535    1.0797    1.4333    1.0611    1.8041    1.7169    1.3474
      0.9746    1.4724    0.7798    1.2645    1.3071    1.6764    1.6066    1.1595
      1.3195    2.4694    0.9935    2.2246    1.9993    2.8639    3.0332    1.7059
   
   D = 
   
      1.6710    2.3766    1.8386    1.9687    2.0411    2.8486    2.5927    1.8901
      1.0182    1.3996    0.6145    1.4581    1.3648    2.0905    2.0259    1.0225
      0.8387    1.3522    1.3566    1.2689    1.5247    1.7267    1.7393    1.3885
      1.4650    1.8864    1.5798    1.9994    2.1078    2.7008    2.4508    1.8312
      0.8689    1.5522    1.2212    1.9497    1.5441    2.1313    2.1265    1.4674
      0.9408    1.7535    1.0797    1.4333    1.0611    1.8041    1.7169    1.3474
      0.9746    1.4724    0.7798    1.2645    1.3071    1.6764    1.6066    1.1595
      1.3195    2.4694    0.9935    2.2246    1.9993    2.8639    3.0332    1.7059
   


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

   
      0.0643    0.6504    0.4096    0.9365    0.1671    0.1886
      0.4261    0.4423    0.8944    0.9987    0.0637    0.7117
      0.6130    0.0645    0.1474    0.0226    0.2517    0.2451
      0.6230    0.3218    0.7303    0.0364    0.9471    0.3974
      0.7327    0.9903    0.5424    0.1028    0.7051    0.3734
   
   
      0.6130
      0.6230
      0.7327
      0.6504
      0.9903
      0.8944
      0.7303
      0.5424
      0.9365
      0.9987
      0.9471
      0.7051
      0.7117
   

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

   
      8.9091    9.5631    7.1553    4.8924    6.6551    3.3485
      8.0673    4.1175    7.0359    9.0820    0.1469    4.0925
      6.3438    7.0770    6.3627    7.9586    2.0053    3.5998
      3.3106    0.2716    3.1115    8.2933    8.2205    2.3997
      7.9627    4.7168    1.1136    1.5530    9.4718    9.1739
   
   
      8.9091    9.5631    7.1553    0.0000    6.6551    0.0000
      8.0673    0.0000    7.0359    9.0820    0.0000    0.0000
      6.3438    7.0770    6.3627    7.9586    0.0000    0.0000
      0.0000    0.0000    0.0000    8.2933    8.2205    0.0000
      7.9627    0.0000    0.0000    0.0000    9.4718    9.1739
   
   
      8.9091       NaN    7.1553    0.0000    6.6551    0.0000
      8.0673    0.0000    7.0359       NaN    0.0000    0.0000
      6.3438    7.0770    6.3627    7.9586    0.0000    0.0000
      0.0000    0.0000    0.0000    8.2933    8.2205    0.0000
      7.9627    0.0000    0.0000    0.0000       NaN       NaN
   

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

   
      3.2530    6.5000    4.5807    6.5000    3.2736    4.3684
      8.4837    6.5000    2.8644    6.5000    1.4219    6.5000
      1.5071    6.5000    6.5000    4.2482    6.5000    1.3917
      6.5000    4.3944    0.6631    6.5000    0.1972    6.5000
      3.5046    2.5233    1.3908    0.2450    6.5000    2.7279
   
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
   
