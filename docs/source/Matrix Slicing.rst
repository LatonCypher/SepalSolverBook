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
      0.2073    0.5641    0.9796    0.1391
   
   R1[2] = 0.9795809089467413
   C1 = 
      0.4083
      0.5651
      0.8108
      0.9047
      0.5880
      0.3212
      0.5058
      0.7297
   
   C1[5] = 0.32122657536624255

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
      0.8729    0.0964    0.6774    0.8761    0.1477
      0.5091    0.4508    0.7422    0.6534    0.5311
   

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
   
      0.7708    0.0893    0.8092    0.1150    0.2785    0.3544    0.1331    0.0109
      0.1792    0.5416    0.2906    0.4576    0.3109    0.0620    0.0380    0.2632
      0.8579    0.6112    0.0874    0.1351    0.8478    0.6290    0.5396    0.9251
      0.7061    0.7220    0.5868    0.1162    0.2785    0.8505    0.9133    0.8876
      0.2962    0.5646    0.6975    0.6271    0.9180    0.9458    0.8168    0.0749
      0.4262    0.0956    0.4142    0.2903    0.0901    0.0023    0.2164    0.8782
      0.2545    0.1737    0.9308    0.1719    0.2534    0.8003    0.0510    0.7984
      0.3080    0.3921    0.9863    0.2683    0.7315    0.8558    0.3117    0.8376
   
   B = 
   
      0.8621    0.7982    0.6926    0.9661    0.7304    0.1746    0.4892    0.0357
      0.8527    0.9349    0.8708    0.6416    0.6189    0.6338    0.1691    0.6722
      0.3682    0.4282    0.8693    0.6560    0.3102    0.5831    0.4336    0.7755
      0.0267    0.5832    0.5788    0.9774    0.9019    0.3707    0.5086    0.3177
      0.1570    0.1919    0.9060    0.0284    0.5782    0.5353    0.8238    0.2067
      0.1256    0.7767    0.6581    0.7274    0.2101    0.5499    0.7403    0.1359
      0.7527    0.2641    0.3132    0.7297    0.9879    0.7383    0.7873    0.1125
      0.5537    0.5148    0.3223    0.5822    0.7356    0.2639    0.0446    0.6792
   
   C = 
   
      1.2361    1.4818    1.9125    1.8145    1.3481    1.1508    1.3986    0.8798
      0.9664    1.2940    1.5324    1.3934    1.3929    1.0117    0.8817    0.9969
      2.4270    2.6424    2.9298    2.8244    2.9897    2.0805    2.2598    1.5020
      2.7729    2.9698    3.0792    3.4537    3.1443    2.4915    2.3987    1.8812
      1.9294    2.5936    3.4001    3.0725    2.9392    2.6830    2.9645    1.5913
      1.2727    1.3046    1.3405    1.7020    1.6731    0.9252    0.8374    1.1326
      1.3355    1.9591    2.2657    2.2274    1.6894    1.5851    1.5219    1.6114
      1.8909    2.5099    3.1610    2.8169    2.5424    2.2903    2.3000    1.9961
   
   D = 
   
      1.2361    1.4818    1.9125    1.8145    1.3481    1.1508    1.3986    0.8798
      0.9664    1.2940    1.5324    1.3934    1.3929    1.0117    0.8817    0.9969
      2.4270    2.6424    2.9298    2.8244    2.9897    2.0805    2.2598    1.5020
      2.7729    2.9698    3.0792    3.4537    3.1443    2.4915    2.3987    1.8812
      1.9294    2.5936    3.4001    3.0725    2.9392    2.6830    2.9645    1.5913
      1.2727    1.3046    1.3405    1.7020    1.6731    0.9252    0.8374    1.1326
      1.3355    1.9591    2.2657    2.2274    1.6894    1.5851    1.5219    1.6114
      1.8909    2.5099    3.1610    2.8169    2.5424    2.2903    2.3000    1.9961
   


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

   
      0.7096    0.2763    0.1634    0.5238    0.1371    0.8684
      0.8972    0.7749    0.4125    0.1933    0.8611    0.6644
      0.1423    0.7967    0.5978    0.4776    0.6929    0.8244
      0.4623    0.7536    0.9718    0.4025    0.4739    0.7602
      0.6785    0.2716    0.6035    0.7889    0.2330    0.5007
   
   
      0.7096
      0.8972
      0.6785
      0.7749
      0.7967
      0.7536
      0.5978
      0.9718
      0.6035
      0.5238
      0.7889
      0.8611
      0.6929
      0.8684
      0.6644
      0.8244
      0.7602
      0.5007
   

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

   
      0.6034    6.8250    0.0437    1.6410    5.3832    5.2297
      8.1053    3.1158    4.1653    2.6592    9.8855    7.9630
      5.3567    3.4055    7.6927    4.6667    7.9686    9.0165
      1.8205    3.2906    8.7889    8.3158    1.0556    0.0088
      7.3715    2.3338    3.6243    5.5297    0.2867    7.6418
   
   
      0.0000    6.8250    0.0000    0.0000    5.3832    5.2297
      8.1053    0.0000    0.0000    0.0000    9.8855    7.9630
      5.3567    0.0000    7.6927    0.0000    7.9686    9.0165
      0.0000    0.0000    8.7889    8.3158    0.0000    0.0000
      7.3715    0.0000    0.0000    5.5297    0.0000    7.6418
   
   
      0.0000    6.8250    0.0000    0.0000    5.3832    5.2297
      8.1053    0.0000    0.0000    0.0000       NaN    7.9630
      5.3567    0.0000    7.6927    0.0000    7.9686       NaN
      0.0000    0.0000    8.7889    8.3158    0.0000    0.0000
      7.3715    0.0000    0.0000    5.5297    0.0000    7.6418
   

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

   
      9.8428    0.0423    0.4659    1.7338    6.5000    4.7507
      0.6954    6.5000    3.1519    8.0159    1.0197    6.5000
      6.5000    8.2597    6.5000    9.7930    0.3482    6.5000
      2.5185    9.8110    2.4114    6.5000    9.4972    8.0609
      3.9659    3.4707    0.1191    3.1243    4.3728    2.1533
   
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
   
