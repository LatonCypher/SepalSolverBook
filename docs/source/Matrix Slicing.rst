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
      0.3219    0.2930    0.9686    0.5566
   
   R1[2] = 0.9685778035681621
   C1 = 
      0.0217
      0.9337
      0.0883
      0.9940
      0.6228
      0.6298
      0.7513
      0.9742
   
   C1[5] = 0.6297920821663664

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
      0.5643    0.1809    0.0591    0.8619    0.7102
      0.0863    0.1942    0.8113    0.0881    0.4032
   

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
   
      0.1417    0.8550    0.3495    0.6812    0.7871    0.1122    0.7087    0.7949
      0.4093    0.4286    0.2221    0.1955    0.6583    0.6859    0.2316    0.1879
      0.0012    0.5932    0.1988    0.1864    0.6552    0.5688    0.7031    0.9361
      0.1142    0.8943    0.6268    0.7063    0.5090    0.2716    0.5946    0.2091
      0.2882    0.4714    0.7089    0.1389    0.4469    0.4785    0.2248    0.9525
      0.4191    0.5252    0.8247    0.5795    0.1055    0.0539    0.7666    0.6705
      0.4289    0.2128    0.2860    0.5942    0.6355    0.2540    0.4058    0.2350
      0.4020    0.3504    0.2700    0.3254    0.2649    0.3840    0.1625    0.9291
   
   B = 
   
      0.2644    0.8702    0.8131    0.0220    0.3447    0.0714    0.4017    0.6030
      0.4569    0.2773    0.8774    0.4056    0.5063    0.8102    0.1808    0.6557
      0.7338    0.5517    0.5418    0.4185    0.7298    0.9864    0.3042    0.6526
      0.2617    0.5559    0.1477    0.7001    0.6565    0.6950    0.4629    0.8777
      0.5241    0.2717    0.3143    0.8025    0.4429    0.3884    0.4738    0.9417
      0.2839    0.9353    0.5208    0.1773    0.4865    0.7160    0.5266    0.6909
      0.1609    0.0519    0.4468    0.9353    0.7419    0.3713    0.3366    0.3743
      0.5864    0.4176    0.9313    0.8365    0.1861    0.7928    0.2054    0.7804
   
   C = 
   
      1.8875    1.6195    2.5180    2.9524    2.2610    2.8005    1.4670    3.1764
      1.2054    1.6171    1.7005    1.4363    1.4806    1.7131    1.1896    2.1714
      1.6329    1.5162    2.3447    2.5215    1.8309    2.4712    1.2933    2.6866
      1.6458    1.5964    2.0832    2.3097    2.2509    2.6206    1.3525    2.7369
      1.8129    1.8282    2.4296    2.0419    1.7213    2.5531    1.2160    2.6463
      1.6948    1.6863    2.3621    2.3452    2.1591    2.5674    1.2565    2.5907
      1.1842    1.4497    1.5102    1.7626    1.6042    1.6637    1.1925    2.2155
      1.3685    1.6043    2.0497    1.7016    1.3242    1.9798    1.0308    2.2346
   
   D = 
   
      1.8875    1.6195    2.5180    2.9524    2.2610    2.8005    1.4670    3.1764
      1.2054    1.6171    1.7005    1.4363    1.4806    1.7131    1.1896    2.1714
      1.6329    1.5162    2.3447    2.5215    1.8309    2.4712    1.2933    2.6866
      1.6458    1.5964    2.0832    2.3097    2.2509    2.6206    1.3525    2.7369
      1.8129    1.8282    2.4296    2.0419    1.7213    2.5531    1.2160    2.6463
      1.6948    1.6863    2.3621    2.3452    2.1591    2.5674    1.2565    2.5907
      1.1842    1.4497    1.5102    1.7626    1.6042    1.6637    1.1925    2.2155
      1.3685    1.6043    2.0497    1.7016    1.3242    1.9798    1.0308    2.2346
   


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

   
      0.0274    0.4252    0.7309    0.2370    0.3115    0.3552
      0.9611    0.1097    0.8719    0.2065    0.8886    0.9156
      0.3853    0.1310    0.7375    0.5735    0.8507    0.3668
      0.2127    0.5299    0.0039    0.7199    0.9188    0.5144
      0.6578    0.8945    0.7598    0.3136    0.6135    0.7318
   
   
      0.9611
      0.6578
      0.5299
      0.8945
      0.7309
      0.8719
      0.7375
      0.7598
      0.5735
      0.7199
      0.8886
      0.8507
      0.9188
      0.6135
      0.9156
      0.5144
      0.7318
   

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

   
      8.9048    9.6440    9.9420    2.0936    6.1767    8.5305
      2.3793    7.1972    5.1206    7.9117    1.4482    8.9895
      2.6173    3.2122    0.4531    5.9545    5.5772    5.2350
      2.8397    0.7186    2.7696    4.7397    7.2050    8.8408
      2.9287    4.7305    6.9260    7.1993    2.9152    5.9424
   
   
      8.9048    9.6440    9.9420    0.0000    6.1767    8.5305
      0.0000    7.1972    5.1206    7.9117    0.0000    8.9895
      0.0000    0.0000    0.0000    5.9545    5.5772    5.2350
      0.0000    0.0000    0.0000    0.0000    7.2050    8.8408
      0.0000    0.0000    6.9260    7.1993    0.0000    5.9424
   
   
      8.9048       NaN       NaN    0.0000    6.1767    8.5305
      0.0000    7.1972    5.1206    7.9117    0.0000    8.9895
      0.0000    0.0000    0.0000    5.9545    5.5772    5.2350
      0.0000    0.0000    0.0000    0.0000    7.2050    8.8408
      0.0000    0.0000    6.9260    7.1993    0.0000    5.9424
   

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

   
      0.1737    1.6156    9.9501    6.5000    1.4809    6.5000
      6.5000    8.3984    6.5000    4.0073    8.2824    6.5000
      9.5795    0.2896    2.5078    6.5000    6.5000    1.0725
      8.7848    1.0488    6.5000    0.6447    6.5000    8.1102
      3.0910    3.2732    2.8101    0.7045    6.5000    6.5000
   
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
   
