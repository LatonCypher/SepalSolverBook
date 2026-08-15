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
      0.6620    0.8647    0.6727    0.0517
   
   R1[2] = 0.6727466709495317
   C1 = 
      0.3381
      0.4421
      0.7707
      0.5601
      0.4074
      0.7407
      0.3423
      0.0648
   
   C1[5] = 0.740684154903723

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
      0.6949    0.8590    0.8173    0.9842    0.3828
      0.2060    0.5279    0.5925    0.9280    0.1168
   

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
   
      0.2106    0.3659    0.9889    0.9996    0.5451    0.3514    0.5333    0.7698
      0.9922    0.8541    0.6265    0.1026    0.7698    0.2532    0.7658    0.0811
      0.4175    0.1250    0.1953    0.5206    0.6938    0.0970    0.1956    0.7537
      0.2776    0.5671    0.1357    0.0100    0.6192    0.2434    0.6730    0.9168
      0.3801    0.6035    0.3928    0.0302    0.5585    0.1625    0.5937    0.0837
      0.7058    0.2368    0.7512    0.9229    0.8821    0.9485    0.8966    0.9097
      0.6150    0.7012    0.2967    0.9455    0.7711    0.1710    0.3859    0.0051
      0.1294    0.0601    0.7554    0.1467    0.0856    0.3986    0.6135    0.0288
   
   B = 
   
      0.1415    0.2247    0.7423    0.3689    0.1146    0.9400    0.9075    0.3579
      0.7748    0.0517    0.3530    0.2062    0.1089    0.2137    0.5372    0.5798
      0.8214    0.0208    0.5157    0.4786    0.3134    0.3440    0.5499    0.8659
      0.6582    0.2833    0.6547    0.0034    0.2215    0.6937    0.2422    0.8697
      0.6451    0.6741    0.2171    0.9130    0.4793    0.6932    0.7726    0.3574
      0.3359    0.2185    0.1036    0.1087    0.5286    0.5804    0.1973    0.9687
      0.1368    0.1307    0.6094    0.2315    0.4456    0.2066    0.6064    0.1914
      0.9480    0.0604    0.7374    0.9961    0.5161    0.5981    0.4174    0.7780
   
   C = 
   
      3.0559    0.9304    2.4973    2.0560    1.6773    2.4622    2.3088    3.2494
      2.1477    0.9884    2.1481    1.8307    1.3117    2.2892    2.8715    2.2122
      1.8805    0.8118    1.6313    1.7151    1.0981    1.8760    1.6680    1.8096
      2.0391    0.7112    1.7286    1.9451    1.3368    1.6936    1.9509    1.8535
      1.4394    0.6280    1.2792    1.2011    0.9005    1.2965    1.7510    1.3881
      3.3806    1.4218    3.1062    2.6940    2.3398    3.5040    3.1965    3.9565
      2.1090    1.0564    1.9002    1.3337    1.0839    2.2025    2.1927    2.2251
      1.0822    0.3162    1.0578    0.7143    0.8306    0.9307    1.1295    1.4194
   
   D = 
   
      3.0559    0.9304    2.4973    2.0560    1.6773    2.4622    2.3088    3.2494
      2.1477    0.9884    2.1481    1.8307    1.3117    2.2892    2.8715    2.2122
      1.8805    0.8118    1.6313    1.7151    1.0981    1.8760    1.6680    1.8096
      2.0391    0.7112    1.7286    1.9451    1.3368    1.6936    1.9509    1.8535
      1.4394    0.6280    1.2792    1.2011    0.9005    1.2965    1.7510    1.3881
      3.3806    1.4218    3.1062    2.6940    2.3398    3.5040    3.1965    3.9565
      2.1090    1.0564    1.9002    1.3337    1.0839    2.2025    2.1927    2.2251
      1.0822    0.3162    1.0578    0.7143    0.8306    0.9307    1.1295    1.4194
   


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

   
      0.6604    0.0301    0.3271    0.9324    0.6491    0.0259
      0.9836    0.5239    0.8536    0.2661    0.8121    0.3836
      0.9081    0.8156    0.4547    0.5875    0.5720    0.7401
      0.6714    0.3707    0.2803    0.2705    0.4099    0.0355
      0.4251    0.4228    0.5594    0.8325    0.0885    0.0254
   
   
      0.6604
      0.9836
      0.9081
      0.6714
      0.5239
      0.8156
      0.8536
      0.5594
      0.9324
      0.5875
      0.8325
      0.6491
      0.8121
      0.5720
      0.7401
   

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

   
      5.7878    1.4438    3.8614    2.0833    3.1323    8.6076
      4.8736    2.9212    5.5587    4.9448    9.2830    7.4830
      7.0050    2.2256    3.8181    0.6348    3.6000    6.5344
      0.0639    1.1573    3.3912    9.7813    5.0021    7.8419
      2.1251    9.4896    5.3103    4.0169    9.6488    1.8924
   
   
      5.7878    0.0000    0.0000    0.0000    0.0000    8.6076
      0.0000    0.0000    5.5587    0.0000    9.2830    7.4830
      7.0050    0.0000    0.0000    0.0000    0.0000    6.5344
      0.0000    0.0000    0.0000    9.7813    5.0021    7.8419
      0.0000    9.4896    5.3103    0.0000    9.6488    0.0000
   
   
      5.7878    0.0000    0.0000    0.0000    0.0000    8.6076
      0.0000    0.0000    5.5587    0.0000       NaN    7.4830
      7.0050    0.0000    0.0000    0.0000    0.0000    6.5344
      0.0000    0.0000    0.0000       NaN    5.0021    7.8419
      0.0000       NaN    5.3103    0.0000       NaN    0.0000
   

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

   
      9.6919    6.5000    3.9213    9.8753    8.7210    6.5000
      3.8719    6.5000    6.5000    6.5000    6.5000    6.5000
      6.5000    3.6662    6.5000    6.5000    6.5000    0.6151
      6.5000    4.0102    2.3337    6.5000    9.0721    6.5000
      9.6405    6.5000    3.7772    6.5000    1.4692    9.3919
   
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
   
