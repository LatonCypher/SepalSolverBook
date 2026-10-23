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
      0.0891    0.8555    0.6959    0.5467
   
   R1[2] = 0.6959025360539989
   C1 = 
      0.0459
      0.9847
      0.4866
      0.4476
      0.8827
      0.8260
      0.4473
      0.5380
   
   C1[5] = 0.8260192665616164

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
   A[2..5] = new double[] { 10, 15, 20 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a row
   A[1, 2..4] = new double[] { 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a col
   A[0..3, 3] = new double[] { 100, 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set submatrix elements
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
      0.9674    0.8829    0.3309    0.4275    0.1717
      0.4548    0.5639    0.0309    0.9146    0.6027
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.2346    0.3155    0.4418    0.2048    0.1512    0.3599    0.7332    0.1118
      0.1534    0.2963    0.7125    0.5495    0.8292    0.3750    0.5826    0.9223
      0.6742    0.9642    0.3134    0.7325    0.6568    0.2356    0.8547    0.2567
      0.3728    0.2369    0.4278    0.3668    0.7502    0.7342    0.2312    0.5574
      0.6858    0.2159    0.6479    0.8408    0.8142    0.3061    0.2662    0.8685
      0.3409    0.1165    0.6733    0.3370    0.8875    0.7693    0.3235    0.1901
      0.2027    0.5836    0.8002    0.0260    0.7262    0.9394    0.4514    0.1214
      0.4435    0.0929    0.7004    0.2290    0.2211    0.2706    0.6067    0.3853
   
   B = 
   
      0.8276    0.7783    0.9077    0.1164    0.6355    0.9151    0.0117    0.4461
      0.5289    0.3900    0.6477    0.5842    0.7851    0.2133    0.6776    0.3281
      0.0045    0.3482    0.7898    0.4284    0.0488    0.2328    0.2322    0.2533
      0.5980    0.5487    0.4480    0.7922    0.7273    0.8132    0.5339    0.0853
      0.1648    0.0001    0.0275    0.1039    0.9980    0.7418    0.7523    0.1071
      0.7890    0.8044    0.2489    0.0751    0.9494    0.2738    0.2335    0.6030
      0.8594    0.5915    0.3187    0.8522    0.9258    0.4543    0.9313    0.7357
      0.5671    0.4180    0.8621    0.5992    0.3231    0.7943    0.0027    0.0916
   
   C = 
   
      1.4880    1.3419    1.2818    1.2978    1.7749    1.1840    1.3094    1.1205
      2.0718    1.8165    2.2370    2.0950    2.7856    2.5314    1.9179    1.2211
      2.6815    2.2143    2.3825    2.3244    3.4868    2.6351    2.4709    1.6237
      1.8729    1.6932    1.7516    1.3198    2.5505    2.0949    1.4127    1.1279
      2.2845    2.0718    2.5828    2.0044    2.8788    3.0068    1.6879    1.1599
      1.6875    1.6199    1.5505    1.2028    2.5631    1.9346    1.5683    1.2040
      1.8132    1.7517    1.7079    1.3313    2.7186    1.6147    1.7839    1.4742
      1.5461    1.4886    1.7175    1.3786    1.7193    1.5948    1.1487    1.0938
   
   D = 
   
      1.4880    1.3419    1.2818    1.2978    1.7749    1.1840    1.3094    1.1205
      2.0718    1.8165    2.2370    2.0950    2.7856    2.5314    1.9179    1.2211
      2.6815    2.2143    2.3825    2.3244    3.4868    2.6351    2.4709    1.6237
      1.8729    1.6932    1.7516    1.3198    2.5505    2.0949    1.4127    1.1279
      2.2845    2.0718    2.5828    2.0044    2.8788    3.0068    1.6879    1.1599
      1.6875    1.6199    1.5505    1.2028    2.5631    1.9346    1.5683    1.2040
      1.8132    1.7517    1.7079    1.3313    2.7186    1.6147    1.7839    1.4742
      1.5461    1.4886    1.7175    1.3786    1.7193    1.5948    1.1487    1.0938
   


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

   
      0.0746    0.1278    0.7067    0.9583    0.4060    0.6268
      0.9466    0.6197    0.9632    0.1104    0.2554    0.1827
      0.7872    0.7801    0.3777    0.0859    0.7060    0.9806
      0.2614    0.7281    0.7713    0.4832    0.6384    0.9710
      0.8852    0.5694    0.5649    0.7200    0.1208    0.3162
   
   
      0.9466
      0.7872
      0.8852
      0.6197
      0.7801
      0.7281
      0.5694
      0.7067
      0.9632
      0.7713
      0.5649
      0.9583
      0.7200
      0.7060
      0.6384
      0.6268
      0.9806
      0.9710
   

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

   
      3.3448    4.4004    8.7995    9.0460    4.5467    7.0355
      7.4096    6.3955    2.1681    9.1338    3.3227    5.4947
      5.9413    1.7606    9.5672    9.6798    3.1785    6.4789
      9.1788    6.2803    3.8882    2.7259    6.2288    8.7818
      6.7934    4.2987    9.5676    9.4011    0.2187    9.4161
   
   
      0.0000    0.0000    8.7995    9.0460    0.0000    7.0355
      7.4096    6.3955    0.0000    9.1338    0.0000    5.4947
      5.9413    0.0000    9.5672    9.6798    0.0000    6.4789
      9.1788    6.2803    0.0000    0.0000    6.2288    8.7818
      6.7934    0.0000    9.5676    9.4011    0.0000    9.4161
   
   
      0.0000    0.0000    8.7995       NaN    0.0000    7.0355
      7.4096    6.3955    0.0000       NaN    0.0000    5.4947
      5.9413    0.0000       NaN       NaN    0.0000    6.4789
         NaN    6.2803    0.0000    0.0000    6.2288    8.7818
      6.7934    0.0000       NaN       NaN    0.0000       NaN
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      3.3751    0.0279    6.5000    6.5000    0.9843    8.1656
      8.2988    6.5000    6.5000    4.1306    4.7932    9.5458
      6.5000    2.8997    6.5000    4.0081    4.9658    3.1302
      6.5000    1.0552    8.1546    8.8470    6.5000    2.5925
      2.2883    3.3498    6.5000    8.4749    6.5000    6.5000
   
Advantages
^^^^^^^^^^


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
   
