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
      0.2701    0.8020    0.8599    0.0521
   
   R1[2] = 0.8599438171344937
   C1 = 
      0.2575
      0.5119
      0.5055
      0.2409
      0.4558
      0.9634
      0.1101
      0.8519
   
   C1[5] = 0.9634274911249974

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
      0.2511    0.2770    0.2474    0.2733    0.9248
      0.0118    0.7753    0.9518    0.8247    0.0337
   

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
   
      0.6398    0.7343    0.5525    0.9953    0.3322    0.7379    0.3144    0.9551
      0.1140    0.4672    0.2580    0.5687    0.8055    0.3316    0.4513    0.6844
      0.9210    0.8396    0.0439    0.4152    0.1451    0.2581    0.3445    0.2090
      0.1925    0.1152    0.0981    0.1756    0.1535    0.7041    0.2138    0.2808
      0.4508    0.9122    0.9019    0.0623    0.9037    0.5588    0.6879    0.4762
      0.5313    0.0714    0.8476    0.4270    0.4703    0.9399    0.4882    0.8064
      0.0119    0.9915    0.1660    0.7649    0.2855    0.6185    0.2568    0.4103
      0.2061    0.5705    0.0533    0.2790    0.1074    0.9954    0.8495    0.9512
   
   B = 
   
      0.7019    0.1397    0.0726    0.7457    0.6768    0.9889    0.3095    0.6872
      0.2523    0.4120    0.5559    0.4755    0.3383    0.9620    0.7603    0.1613
      0.2763    0.3707    0.0371    0.3680    0.6740    0.1156    0.7382    0.4212
      0.2604    0.8738    0.5742    0.8420    0.9288    0.9027    0.6765    0.2454
      0.5145    0.0254    0.2709    0.0229    0.5246    0.4465    0.5880    0.5950
      0.0676    0.8025    0.2365    0.6736    0.2805    0.6391    0.9368    0.9395
      0.1913    0.3472    0.6117    0.2493    0.3824    0.2080    0.9790    0.8798
      0.9279    0.8937    0.0135    0.4927    0.9818    0.9568    0.0373    0.3875
   
   C = 
   
      2.2135    3.0298    1.5164    2.9213    3.4175    3.9008    3.0675    2.5727
      1.5755    1.8559    1.1860    1.5725    2.2974    2.4257    2.2173    1.8550
      1.3305    1.3708    1.0876    1.8179    1.8080    2.5997    1.9089    1.6016
      0.6651    1.1583    0.5252    1.0518    1.0340    1.3028    1.3081    1.2850
      1.8882    1.9634    1.4133    1.9575    2.6409    2.8433    3.2871    2.7045
      1.8834    2.4472    1.0141    2.2644    2.8403    2.7612    2.7982    2.7432
      1.1221    2.0994    1.3836    1.8747    1.9900    2.6440    2.4115    1.5616
      1.5437    2.4739    1.2913    2.0329    2.2218    2.7816    2.5884    2.4397
   
   D = 
   
      2.2135    3.0298    1.5164    2.9213    3.4175    3.9008    3.0675    2.5727
      1.5755    1.8559    1.1860    1.5725    2.2974    2.4257    2.2173    1.8550
      1.3305    1.3708    1.0876    1.8179    1.8080    2.5997    1.9089    1.6016
      0.6651    1.1583    0.5252    1.0518    1.0340    1.3028    1.3081    1.2850
      1.8882    1.9634    1.4133    1.9575    2.6409    2.8433    3.2871    2.7045
      1.8834    2.4472    1.0141    2.2644    2.8403    2.7612    2.7982    2.7432
      1.1221    2.0994    1.3836    1.8747    1.9900    2.6440    2.4115    1.5616
      1.5437    2.4739    1.2913    2.0329    2.2218    2.7816    2.5884    2.4397
   


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

   
      0.2644    0.2284    0.2957    0.9119    0.3521    0.4757
      0.5073    0.0237    0.0569    0.0368    0.1247    0.8289
      0.8062    0.2856    0.5302    0.0783    0.1148    0.3703
      0.2993    0.5981    0.3959    0.1324    0.4732    0.8369
      0.3189    0.2352    0.9129    0.2005    0.0078    0.9077
   
   
      0.5073
      0.8062
      0.5981
      0.5302
      0.9129
      0.9119
      0.8289
      0.8369
      0.9077
   

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

   
      9.4923    9.4803    3.5367    3.4651    1.4717    4.6343
      5.7174    5.7762    9.1589    4.9375    6.9592    8.7184
      9.6808    1.0541    7.6250    0.9211    4.7059    3.0179
      5.9818    8.5345    9.8397    7.6247    0.0340    7.6732
      2.3738    7.0818    0.5653    2.8898    2.3060    6.4921
   
   
      9.4923    9.4803    0.0000    0.0000    0.0000    0.0000
      5.7174    5.7762    9.1589    0.0000    6.9592    8.7184
      9.6808    0.0000    7.6250    0.0000    0.0000    0.0000
      5.9818    8.5345    9.8397    7.6247    0.0000    7.6732
      0.0000    7.0818    0.0000    0.0000    0.0000    6.4921
   
   
         NaN       NaN    0.0000    0.0000    0.0000    0.0000
      5.7174    5.7762       NaN    0.0000    6.9592    8.7184
         NaN    0.0000    7.6250    0.0000    0.0000    0.0000
      5.9818    8.5345       NaN    7.6247    0.0000    7.6732
      0.0000    7.0818    0.0000    0.0000    0.0000    6.4921
   

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

   
      6.5000    8.3746    3.6911    8.1568    6.5000    6.5000
      6.5000    6.5000    6.5000    8.4033    8.7299    6.5000
      1.1401    1.7223    8.2719    3.6445    8.8714    6.5000
      0.5485    0.7255    9.5554    2.7922    6.5000    6.5000
      6.5000    0.9291    2.3450    8.8805    6.5000    6.5000
   
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
   
