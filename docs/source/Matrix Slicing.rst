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
      0.6141    0.1289    0.1315    0.7308
   
   R1[2] = 0.13154690543819336
   C1 = 
      0.0765
      0.3764
      0.1998
      0.4379
      0.6642
      0.5713
      0.8134
      0.3019
   
   C1[5] = 0.5712904377824232

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
      0.7977    0.2163    0.4723    0.2167    0.6185
      0.0714    0.5795    0.7812    0.1645    0.7545
   

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
   
      0.1432    0.3381    0.1118    0.2210    0.5665    0.1133    0.7317    0.4486
      0.3754    0.1036    0.6776    0.3156    0.1276    0.2404    0.5182    0.3677
      0.7297    0.8234    0.8630    0.0053    0.0868    0.5139    0.2238    0.4719
      0.7184    0.4386    0.3152    0.1412    0.4472    0.1799    0.8973    0.8494
      0.8516    0.7229    0.5386    0.6231    0.8087    0.1299    0.3780    0.7076
      0.4959    0.6847    0.8273    0.8198    0.7798    0.2079    0.1041    0.3716
      0.8523    0.2608    0.4505    0.3792    0.7403    0.5082    0.9473    0.8442
      0.0089    0.0997    0.0538    0.6133    0.0821    0.9884    0.1393    0.2692
   
   B = 
   
      0.1426    0.8590    0.6119    0.5589    0.1400    0.3960    0.3709    0.0590
      0.3740    0.2280    0.6928    0.6117    0.5049    0.4630    0.2562    0.2427
      0.0889    0.4951    0.3376    0.6815    0.9915    0.2338    0.1415    0.8170
      0.6163    0.2600    0.0295    0.7542    0.0585    0.8586    0.1444    0.1031
      0.1972    0.6716    0.4374    0.8779    0.4701    0.7282    0.5954    0.6595
      0.1064    0.7862    0.3869    0.5764    0.6809    0.9589    0.2506    0.5732
      0.1370    0.9422    0.9504    0.0246    0.4614    0.1771    0.3981    0.7650
      0.9021    0.4957    0.2976    0.9036    0.7113    0.4540    0.0313    0.7216
   
   C = 
   
      0.9217    1.6943    1.4866    1.5158    1.3147    1.2836    0.8585    1.5267
      0.8005    1.7089    1.2903    1.5687    1.5195    1.2082    0.6613    1.5171
      1.0201    2.1503    1.8983    2.3079    2.2035    1.6862    0.8888    1.8120
      1.3781    2.6182    2.2248    2.2770    1.9937    1.7251    1.1390    2.1183
      1.6872    2.6772    2.1958    3.1887    2.2010    2.4344    1.3539    2.1375
      1.4310    2.1744    1.7123    3.0208    2.1039    2.3650    1.1643    1.9371
      1.5843    3.3211    2.5374    2.9579    2.4515    2.4666    1.4731    2.6341
      0.8047    1.3134    0.7416    1.4536    1.1082    1.7434    0.4854    1.0534
   
   D = 
   
      0.9217    1.6943    1.4866    1.5158    1.3147    1.2836    0.8585    1.5267
      0.8005    1.7089    1.2903    1.5687    1.5195    1.2082    0.6613    1.5171
      1.0201    2.1503    1.8983    2.3079    2.2035    1.6862    0.8888    1.8120
      1.3781    2.6182    2.2248    2.2770    1.9937    1.7251    1.1390    2.1183
      1.6872    2.6772    2.1958    3.1887    2.2010    2.4344    1.3539    2.1375
      1.4310    2.1744    1.7123    3.0208    2.1039    2.3650    1.1643    1.9371
      1.5843    3.3211    2.5374    2.9579    2.4515    2.4666    1.4731    2.6341
      0.8047    1.3134    0.7416    1.4536    1.1082    1.7434    0.4854    1.0534
   


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

   
      0.1785    0.4947    0.6310    0.1788    0.3877    0.2497
      0.7515    0.0216    0.5347    0.7130    0.3425    0.8487
      0.8633    0.2700    0.3929    0.8919    0.8832    0.6549
      0.9723    0.7853    0.1182    0.4238    0.7766    0.2342
      0.6254    0.5132    0.0934    0.9670    0.9619    0.3776
   
   
      0.7515
      0.8633
      0.9723
      0.6254
      0.7853
      0.5132
      0.6310
      0.5347
      0.7130
      0.8919
      0.9670
      0.8832
      0.7766
      0.9619
      0.8487
      0.6549
   

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

   
      5.8020    2.6311    7.8354    5.2422    5.8523    3.7497
      0.9043    3.2577    1.0096    0.2791    6.8961    7.1444
      5.8849    3.3773    5.2159    6.7704    3.8630    1.9676
      7.6774    8.3901    5.8978    0.8501    5.2356    5.2742
      2.2530    6.6946    3.7474    2.0695    3.2755    1.8740
   
   
      5.8020    0.0000    7.8354    5.2422    5.8523    0.0000
      0.0000    0.0000    0.0000    0.0000    6.8961    7.1444
      5.8849    0.0000    5.2159    6.7704    0.0000    0.0000
      7.6774    8.3901    5.8978    0.0000    5.2356    5.2742
      0.0000    6.6946    0.0000    0.0000    0.0000    0.0000
   
   
      5.8020    0.0000    7.8354    5.2422    5.8523    0.0000
      0.0000    0.0000    0.0000    0.0000    6.8961    7.1444
      5.8849    0.0000    5.2159    6.7704    0.0000    0.0000
      7.6774    8.3901    5.8978    0.0000    5.2356    5.2742
      0.0000    6.6946    0.0000    0.0000    0.0000    0.0000
   

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

   
      3.8258    1.8047    6.5000    4.8934    4.4738    6.5000
      6.5000    9.5044    8.5050    6.5000    6.5000    4.1226
      6.5000    6.5000    9.5958    1.7140    6.5000    3.6731
      8.7167    1.2149    6.5000    6.5000    9.2559    8.8690
      0.1100    2.0000    2.2927    6.5000    0.3271    6.5000
   
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
   
