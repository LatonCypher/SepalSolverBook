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
      0.1396    0.3334    0.5552    0.7020
   
   R1[2] = 0.5551893132682657
   C1 = 
      0.5365
      0.8168
      0.2075
      0.9706
      0.2376
      0.5753
      0.5922
      0.6630
   
   C1[5] = 0.5753447134189117

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
      0.4864    0.8108    0.6018    0.1336    0.4529
      0.2501    0.4270    0.9994    0.8125    0.7225
   

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
   
      0.0780    0.2477    0.9064    0.5361    0.4906    0.7396    0.2224    0.4496
      0.8705    0.1286    0.5876    0.5709    0.6151    0.9054    0.5289    0.2128
      0.1835    0.0957    0.9827    0.4721    0.2170    0.1030    0.9207    0.2407
      0.3782    0.4535    0.1016    0.0616    0.0510    0.3447    0.6951    0.0108
      0.3550    0.4838    0.7132    0.3258    0.1034    0.7898    0.4387    0.2596
      0.3971    0.1955    0.0698    0.2689    0.6537    0.1447    0.1013    0.4382
      0.4407    0.3071    0.6783    0.5376    0.0173    0.0347    0.3142    0.6113
      0.3379    0.4667    0.7696    0.8613    0.5654    0.3762    0.0263    0.5158
   
   B = 
   
      0.4435    0.1682    0.5319    0.3948    0.5702    0.1170    0.8931    0.0066
      0.7258    0.1164    0.8029    0.8719    0.6224    0.2398    0.8052    0.7643
      0.6898    0.0950    0.3302    0.2001    0.1413    0.5878    0.5721    0.1357
      0.3643    0.2340    0.4035    0.3665    0.0795    0.8829    0.5371    0.1023
      0.5653    0.1494    0.9070    0.3471    0.7901    0.3571    0.0602    0.5644
      0.2965    0.8083    0.2869    0.7850    0.4205    0.1520    0.5026    0.3322
      0.8933    0.6906    0.8563    0.0020    0.1222    0.7274    0.1495    0.4291
      0.4415    0.1665    0.5569    0.2804    0.3972    0.8781    0.1982    0.7455
   
   C = 
   
      1.9288    1.1530    1.8539    1.5020    1.2737    1.9188    1.5992    1.3209
      2.2754    1.5751    2.3797    1.7676    1.7207    1.9110    2.1371    1.2757
      2.0826    1.0373    1.8381    0.7509    0.7634    2.0131    1.3068    0.9871
      1.3462    0.9085    1.3701    0.8803    0.7917    0.8528    1.0765    0.8189
      1.9184    1.2600    1.7848    1.5536    1.2007    1.5685    1.8098    1.2047
      1.1606    0.5166    1.4650    0.9033    1.1431    1.0857    0.9106    0.9762
      1.6527    0.6495    1.5570    0.9798    0.8904    1.7753    1.5043    0.9965
      2.0158    0.8784    2.0867    1.6463    1.4734    2.0955    1.9098    1.3914
   
   D = 
   
      1.9288    1.1530    1.8539    1.5020    1.2737    1.9188    1.5992    1.3209
      2.2754    1.5751    2.3797    1.7676    1.7207    1.9110    2.1371    1.2757
      2.0826    1.0373    1.8381    0.7509    0.7634    2.0131    1.3068    0.9871
      1.3462    0.9085    1.3701    0.8803    0.7917    0.8528    1.0765    0.8189
      1.9184    1.2600    1.7848    1.5536    1.2007    1.5685    1.8098    1.2047
      1.1606    0.5166    1.4650    0.9033    1.1431    1.0857    0.9106    0.9762
      1.6527    0.6495    1.5570    0.9798    0.8904    1.7753    1.5043    0.9965
      2.0158    0.8784    2.0867    1.6463    1.4734    2.0955    1.9098    1.3914
   


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

   
      0.9129    0.5929    0.2263    0.0857    0.3840    0.8783
      0.9674    0.7231    0.6309    0.7932    0.2915    0.5878
      0.9412    0.2815    0.3508    0.6716    0.8453    0.4393
      0.2534    0.4425    0.2687    0.3302    0.9096    0.0238
      0.4030    0.2986    0.2571    0.1199    0.6779    0.1380
   
   
      0.9129
      0.9674
      0.9412
      0.5929
      0.7231
      0.6309
      0.7932
      0.6716
      0.8453
      0.9096
      0.6779
      0.8783
      0.5878
   

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

   
      5.8392    4.9000    7.6613    3.2015    7.9773    5.0597
      7.1960    8.1474    6.0141    2.8397    5.2624    0.9452
      1.5766    4.5019    2.5566    2.0150    2.8695    0.4998
      0.5971    5.0649    5.1139    2.2752    3.2184    7.5768
      2.2509    0.1840    2.9779    7.7071    9.7643    6.7128
   
   
      5.8392    0.0000    7.6613    0.0000    7.9773    5.0597
      7.1960    8.1474    6.0141    0.0000    5.2624    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    5.0649    5.1139    0.0000    0.0000    7.5768
      0.0000    0.0000    0.0000    7.7071    9.7643    6.7128
   
   
      5.8392    0.0000    7.6613    0.0000    7.9773    5.0597
      7.1960    8.1474    6.0141    0.0000    5.2624    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    5.0649    5.1139    0.0000    0.0000    7.5768
      0.0000    0.0000    0.0000    7.7071       NaN    6.7128
   

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

   
      9.2558    6.5000    3.3554    0.6084    0.7097    9.5423
      6.5000    3.7032    0.6627    1.6969    0.3062    3.4945
      8.2598    6.5000    6.5000    6.5000    6.5000    6.5000
      6.5000    6.5000    0.1956    9.6449    9.4010    6.5000
      3.4415    9.6458    3.9739    6.5000    8.3538    3.8088
   
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
   
