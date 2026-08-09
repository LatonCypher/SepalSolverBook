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
      0.6591    0.0804    0.9311    0.5265
   
   R1[2] = 0.931117730043147
   C1 = 
      0.5194
      0.7228
      0.7920
      0.2353
      0.6737
      0.6143
      0.2416
      0.8677
   
   C1[5] = 0.6143037552437962

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
      0.0352    0.3135    0.0280    0.1135    0.0363
      0.7531    0.0292    0.0868    0.4341    0.5267
   

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
   
      0.1515    0.4799    0.9247    0.8327    0.8673    0.8171    0.9438    0.6156
      0.8402    0.9696    0.2944    0.5326    0.8239    0.6802    0.3362    0.4592
      0.2519    0.0990    0.0652    0.6804    0.1129    0.5251    0.4553    0.8022
      0.5574    0.7128    0.9810    0.3827    0.8816    0.3888    0.6673    0.1035
      0.9572    0.6923    0.9417    0.8432    0.5671    0.5733    0.7834    0.8731
      0.8821    0.8546    0.5440    0.9508    0.4063    0.1648    0.4369    0.3593
      0.3420    0.6334    0.0568    0.0986    0.4688    0.8834    0.5435    0.5139
      0.6967    0.7940    0.7334    0.9598    0.0812    0.8052    0.3842    0.6195
   
   B = 
   
      0.3656    0.1733    0.5850    0.9389    0.9916    0.0880    0.3005    0.6186
      0.6100    0.5077    0.8591    0.8556    0.9514    0.0988    0.1320    0.3216
      0.1629    0.7257    0.9406    0.1327    0.4383    0.2546    0.6622    0.6899
      0.8249    0.8817    0.1145    0.2250    0.3235    0.1208    0.6761    0.0029
      0.6408    0.2185    0.2137    0.2388    0.2304    0.2525    0.2132    0.7147
      0.2874    0.5075    0.9068    0.4595    0.4323    0.7828    0.1904    0.2155
      0.6049    0.4392    0.1830    0.8763    0.8446    0.7205    0.4425    0.6914
      0.3188    0.1364    0.9733    0.4804    0.4364    0.4321    0.2336    0.9144
   
   C = 
   
      2.7433    2.7777    3.1641    2.5682    2.9003    2.2013    2.1860    2.8997
      2.4591    2.0566    2.9638    2.8019    3.0252    1.4902    1.4968    2.4241
      1.4788    1.3417    1.7359    1.5356    1.5803    1.2450    1.1049    1.4769
      2.2274    2.2049    2.6687    2.3731    2.7646    1.4679    1.7514    2.5217
      2.9015    2.8221    3.7711    3.3105    3.7145    2.0279    2.3533    3.3358
      2.4033    2.2331    2.5367    2.5738    2.9245    1.1171    1.7762    2.1552
      1.6488    1.3685    2.3098    2.1337    2.1717    1.5426    0.9193    1.8259
      2.3636    2.5819    3.3103    2.6705    3.0399    1.6378    1.9341    2.2589
   
   D = 
   
      2.7433    2.7777    3.1641    2.5682    2.9003    2.2013    2.1860    2.8997
      2.4591    2.0566    2.9638    2.8019    3.0252    1.4902    1.4968    2.4241
      1.4788    1.3417    1.7359    1.5356    1.5803    1.2450    1.1049    1.4769
      2.2274    2.2049    2.6687    2.3731    2.7646    1.4679    1.7514    2.5217
      2.9015    2.8221    3.7711    3.3105    3.7145    2.0279    2.3533    3.3358
      2.4033    2.2331    2.5367    2.5738    2.9245    1.1171    1.7762    2.1552
      1.6488    1.3685    2.3098    2.1337    2.1717    1.5426    0.9193    1.8259
      2.3636    2.5819    3.3103    2.6705    3.0399    1.6378    1.9341    2.2589
   


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

   
      0.2901    0.1633    0.7875    0.9470    0.4965    0.6829
      0.4285    0.4731    0.5218    0.5319    0.5113    0.9577
      0.5391    0.2435    0.3082    0.0882    0.4021    0.3531
      0.3831    0.5178    0.4896    0.9896    0.6347    0.7331
      0.2235    0.9368    0.9336    0.0478    0.5688    0.7146
   
   
      0.5391
      0.5178
      0.9368
      0.7875
      0.5218
      0.9336
      0.9470
      0.5319
      0.9896
      0.5113
      0.6347
      0.5688
      0.6829
      0.9577
      0.7331
      0.7146
   

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

   
      9.8141    8.0854    8.8463    9.4190    6.6920    1.8920
      1.0532    2.2650    4.3229    6.2299    2.1075    0.1620
      0.7966    2.6564    7.0408    3.1708    1.3231    0.3048
      4.9524    6.2214    4.2247    8.6279    6.9523    3.2374
      2.4905    0.1620    4.8204    4.6620    5.4893    4.0597
   
   
      9.8141    8.0854    8.8463    9.4190    6.6920    0.0000
      0.0000    0.0000    0.0000    6.2299    0.0000    0.0000
      0.0000    0.0000    7.0408    0.0000    0.0000    0.0000
      0.0000    6.2214    0.0000    8.6279    6.9523    0.0000
      0.0000    0.0000    0.0000    0.0000    5.4893    0.0000
   
   
         NaN    8.0854    8.8463       NaN    6.6920    0.0000
      0.0000    0.0000    0.0000    6.2299    0.0000    0.0000
      0.0000    0.0000    7.0408    0.0000    0.0000    0.0000
      0.0000    6.2214    0.0000    8.6279    6.9523    0.0000
      0.0000    0.0000    0.0000    0.0000    5.4893    0.0000
   

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

   
      4.5965    6.5000    8.8331    6.5000    6.5000    9.8660
      9.8898    6.5000    2.3461    2.4520    6.5000    0.7948
      0.5513    0.2748    6.5000    8.3321    6.5000    3.9895
      9.5718    6.5000    8.9128    8.3651    2.1323    8.2906
      8.8928    4.6522    4.3582    9.1332    1.8585    8.6971
   
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
   
