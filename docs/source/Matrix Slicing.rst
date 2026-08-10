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
      0.9709    0.9852    0.3553    0.3252
   
   R1[2] = 0.3552644845595726
   C1 = 
      0.5755
      0.8817
      0.7670
      0.9281
      0.2378
      0.1789
      0.2337
      0.5661
   
   C1[5] = 0.17889832355557622

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
      0.9591    0.1304    0.3050    0.0248    0.0268
      0.3183    0.1414    0.7304    0.8880    0.1945
   

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
   
      0.9886    0.8384    0.4926    0.3387    0.1111    0.8931    0.5983    0.8861
      0.9319    0.7608    0.1645    0.5962    0.0728    0.2031    0.1251    0.4932
      0.1119    0.9171    0.3384    0.1550    0.3599    0.7675    0.3686    0.1284
      0.8556    0.3995    0.0846    0.3147    0.2961    0.2238    0.6496    0.6970
      0.9932    0.1908    0.5145    0.1845    0.9999    0.7133    0.3503    0.2564
      0.7331    0.8677    0.8312    0.0912    0.7391    0.8679    0.0200    0.1532
      0.7898    0.8710    0.8121    0.1986    0.4479    0.7360    0.6394    0.8034
      0.4950    0.3444    0.8115    0.3543    0.3785    0.4025    0.3010    0.4200
   
   B = 
   
      0.4733    0.4878    0.1586    0.7784    0.5715    0.4249    0.5651    0.9669
      0.6630    0.0046    0.0093    0.0743    0.2397    0.8692    0.8780    0.2583
      0.5090    0.5226    0.6614    0.0776    0.9128    0.4107    0.0528    0.4069
      0.9622    0.3118    0.3310    0.2676    0.2613    0.3396    0.3065    0.3117
      0.2422    0.7970    0.9907    0.1724    0.1494    0.4142    0.4469    0.2716
      0.5873    0.6750    0.0604    0.9169    0.4615    0.1364    0.5414    0.8673
      0.8435    0.4569    0.0150    0.2301    0.9912    0.1314    0.8323    0.4300
      0.3430    0.7295    0.8109    0.2008    0.2304    0.8582    0.1465    0.3536
   
   C = 
   
      2.9605    2.4604    1.4941    2.1144    2.5301    2.4730    2.5856    2.8539
      2.0145    1.3420    0.9473    1.2809    1.3632    1.8248    1.7049    1.7745
      1.8753    1.3510    0.8140    1.0994    1.4361    1.4487    1.8359    1.4985
      2.0058    1.7538    1.1813    1.3321    1.6962    1.6890    1.8314    1.8633
      2.0805    2.4373    1.8075    1.8351    2.0162    1.6392    1.9747    2.4081
      2.1912    2.1202    1.6135    1.6825    1.9755    1.9966    2.0873    2.3158
      2.9114    2.6076    1.8855    1.8562    2.6788    2.5530    2.5634    2.7000
      1.9427    1.7950    1.4802    1.1567    1.8363    1.5749    1.4327    1.7381
   
   D = 
   
      2.9605    2.4604    1.4941    2.1144    2.5301    2.4730    2.5856    2.8539
      2.0145    1.3420    0.9473    1.2809    1.3632    1.8248    1.7049    1.7745
      1.8753    1.3510    0.8140    1.0994    1.4361    1.4487    1.8359    1.4985
      2.0058    1.7538    1.1813    1.3321    1.6962    1.6890    1.8314    1.8633
      2.0805    2.4373    1.8075    1.8351    2.0162    1.6392    1.9747    2.4081
      2.1912    2.1202    1.6135    1.6825    1.9755    1.9966    2.0873    2.3158
      2.9114    2.6076    1.8855    1.8562    2.6788    2.5530    2.5634    2.7000
      1.9427    1.7950    1.4802    1.1567    1.8363    1.5749    1.4327    1.7381
   


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

   
      0.7772    0.5200    0.5199    0.0708    0.6265    0.0661
      0.0772    0.4483    0.2710    0.3280    0.1707    0.7409
      0.4866    0.5836    0.2871    0.0337    0.7295    0.6800
      0.2492    0.2804    0.8630    0.4254    0.7029    0.8661
      0.6689    0.3102    0.2237    0.4154    0.4094    0.9919
   
   
      0.7772
      0.6689
      0.5200
      0.5836
      0.5199
      0.8630
      0.6265
      0.7295
      0.7029
      0.7409
      0.6800
      0.8661
      0.9919
   

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

   
      1.2872    4.3792    1.4881    6.2954    7.8954    0.2376
      5.9922    1.4825    9.6437    6.9387    0.8282    3.8390
      9.6170    6.4470    5.3318    8.4406    7.3715    1.5101
      1.2617    0.5659    0.9510    0.6551    8.1978    5.2439
      7.1265    0.8200    6.0916    3.1290    8.0747    0.3733
   
   
      0.0000    0.0000    0.0000    6.2954    7.8954    0.0000
      5.9922    0.0000    9.6437    6.9387    0.0000    0.0000
      9.6170    6.4470    5.3318    8.4406    7.3715    0.0000
      0.0000    0.0000    0.0000    0.0000    8.1978    5.2439
      7.1265    0.0000    6.0916    0.0000    8.0747    0.0000
   
   
      0.0000    0.0000    0.0000    6.2954    7.8954    0.0000
      5.9922    0.0000       NaN    6.9387    0.0000    0.0000
         NaN    6.4470    5.3318    8.4406    7.3715    0.0000
      0.0000    0.0000    0.0000    0.0000    8.1978    5.2439
      7.1265    0.0000    6.0916    0.0000    8.0747    0.0000
   

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

   
      6.5000    2.1481    6.5000    4.9902    6.5000    9.1982
      6.5000    1.4690    9.2976    1.5521    6.5000    4.1823
      9.9326    1.5596    1.3893    9.8163    8.3041    4.3696
      2.1109    6.5000    6.5000    2.8782    3.7380    3.8009
      9.0567    8.2804    6.5000    0.6483    6.5000    3.9005
   
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
   
