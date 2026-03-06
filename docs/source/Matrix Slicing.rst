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
      0.3180    0.8479    0.9939    0.4984
   
   R1[2] = 0.9938753351059959
   C1 = 
      0.8277
      0.4474
      0.5243
      0.2801
      0.1101
      0.3195
      0.2536
      0.1955
   
   C1[5] = 0.3195476993900185

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
      0.8862    0.6755    0.0287    0.3293    0.3375
      0.9956    0.2258    0.2087    0.0795    0.8264
   

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
   
      0.4373    0.5422    0.1028    0.5542    0.1619    0.2196    0.8915    0.3691
      0.4690    0.3089    0.1139    0.2647    0.3955    0.7757    0.5851    0.3416
      0.4286    0.2419    0.2576    0.0496    0.1566    0.2956    0.3901    0.8571
      0.0394    0.9223    0.6316    0.2114    0.2839    0.2691    0.6987    0.0860
      0.1528    0.3851    0.6625    0.4383    0.1215    0.3798    0.3913    0.4747
      0.2679    0.7981    0.2743    0.1189    0.1462    0.9363    0.3402    0.8066
      0.9254    0.2520    0.0926    0.4283    0.9160    0.0574    0.3106    0.1852
      0.8911    0.7820    0.5528    0.9702    0.8185    0.2273    0.7517    0.3369
   
   B = 
   
      0.9474    0.9281    0.1253    0.4287    0.0669    0.3438    0.4497    0.0546
      0.0381    0.5524    0.9449    0.4872    0.3591    0.4665    0.2833    0.2226
      0.4730    0.1462    0.0296    0.5436    0.9469    0.7976    0.2356    0.6923
      0.6923    0.7539    0.9250    0.6296    0.8411    0.1654    0.6987    0.4685
      0.8482    0.3534    0.1841    0.3949    0.2917    0.3105    0.6939    0.1205
      0.0802    0.2554    0.9649    0.2370    0.0077    0.2852    0.6305    0.2045
      0.0394    0.2126    0.8622    0.1356    0.0918    0.7708    0.4851    0.8228
      0.4468    0.3141    0.3956    0.7452    0.5685    0.5463    0.7533    0.6051
   
   C = 
   
      1.2223    1.5570    2.2393    1.3684    1.1281    1.5787    1.7230    1.4968
      1.2667    1.3918    2.0598    1.2541    0.8421    1.4216    1.8149    1.1917
      1.1265    1.0896    1.3254    1.2965    0.9723    1.3758    1.4866    1.1978
      0.8460    1.1424    2.0390    1.2774    1.3076    1.7330    1.3459    1.4600
      1.1373    1.1541    1.7221    1.4340    1.4887    1.5401    1.5113    1.4596
      1.0692    1.4357    2.4486    1.6545    1.2039    1.7181    1.9585    1.4149
      2.1033    1.7971    1.3182    1.3950    1.0018    1.2218    1.7707    0.8611
      2.6998    2.6842    2.9158    2.4043    2.1808    2.3549    2.7600    2.0273
   
   D = 
   
      1.2223    1.5570    2.2393    1.3684    1.1281    1.5787    1.7230    1.4968
      1.2667    1.3918    2.0598    1.2541    0.8421    1.4216    1.8149    1.1917
      1.1265    1.0896    1.3254    1.2965    0.9723    1.3758    1.4866    1.1978
      0.8460    1.1424    2.0390    1.2774    1.3076    1.7330    1.3459    1.4600
      1.1373    1.1541    1.7221    1.4340    1.4887    1.5401    1.5113    1.4596
      1.0692    1.4357    2.4486    1.6545    1.2039    1.7181    1.9585    1.4149
      2.1033    1.7971    1.3182    1.3950    1.0018    1.2218    1.7707    0.8611
      2.6998    2.6842    2.9158    2.4043    2.1808    2.3549    2.7600    2.0273
   


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

   
      0.9018    0.0528    0.5438    0.2417    0.3846    0.5253
      0.0557    0.0121    0.7545    0.4636    0.1933    0.2474
      0.7000    0.2320    0.7331    0.5307    0.7814    0.1776
      0.8053    0.7491    0.4182    0.0493    0.2067    0.2508
      0.0299    0.8243    0.4248    0.8621    0.3276    0.4897
   
   
      0.9018
      0.7000
      0.8053
      0.7491
      0.8243
      0.5438
      0.7545
      0.7331
      0.5307
      0.8621
      0.7814
      0.5253
   

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

   
      6.0164    8.4960    5.5203    8.8621    1.1027    2.6486
      8.3240    7.5139    2.6294    4.0117    4.0406    4.7958
      0.5715    2.0029    8.0142    6.4519    3.1295    9.0253
      0.0434    9.4374    1.0797    0.9875    4.9890    2.4842
      4.9602    1.1147    5.8992    4.6021    4.2094    8.8523
   
   
      6.0164    8.4960    5.5203    8.8621    0.0000    0.0000
      8.3240    7.5139    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    8.0142    6.4519    0.0000    9.0253
      0.0000    9.4374    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    5.8992    0.0000    0.0000    8.8523
   
   
      6.0164    8.4960    5.5203    8.8621    0.0000    0.0000
      8.3240    7.5139    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    8.0142    6.4519    0.0000       NaN
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    5.8992    0.0000    0.0000    8.8523
   

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

   
      3.6725    6.5000    0.3048    3.8895    8.2071    6.5000
      6.5000    6.5000    6.5000    1.6517    3.1075    6.5000
      0.0100    6.5000    9.5657    6.5000    9.0135    6.5000
      9.3050    1.1835    8.2295    0.3509    6.5000    3.3095
      6.5000    1.6640    4.6399    6.5000    2.5612    6.5000
   
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
   
