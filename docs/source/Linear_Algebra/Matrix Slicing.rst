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
      0.6549    0.2552    0.6870    0.8190
   
   R1[2] = 0.6870467440897792
   C1 = 
      0.4625
      0.6366
      0.0319
      0.9559
      0.1550
      0.0241
      0.5777
      0.5379
   
   C1[5] = 0.024088247917200878

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
      0.2318    0.3068    0.8153    0.6323    0.4201
      0.8702    0.9480    0.9404    0.3349    0.9526
   

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
   
      0.8600    0.4282    0.8122    0.7951    0.1993    0.2170    0.6910    0.7362
      0.8031    0.8607    0.6319    0.0983    0.1241    0.8583    0.8397    0.1255
      0.0451    0.7695    0.6477    0.3033    0.5944    0.0593    0.1406    0.7338
      0.4396    0.6423    0.6994    0.7389    0.3066    0.1700    0.1888    0.0619
      0.5526    0.9435    0.5345    0.7417    0.8055    0.0528    0.9041    0.5798
      0.3564    0.1502    0.5022    0.1257    0.3732    0.8250    0.8488    0.8926
      0.5614    0.3174    0.1195    0.7100    0.2689    0.2810    0.0855    0.4242
      0.0273    0.7900    0.1004    0.9265    0.5284    0.4880    0.7343    0.3108
   
   B = 
   
      0.3465    0.6456    0.9618    0.1603    0.5198    0.8221    0.7605    0.8447
      0.9025    0.1570    0.4997    0.2376    0.3280    0.2564    0.0703    0.0959
      0.8001    0.5490    0.7877    0.2719    0.9500    0.8972    0.9067    0.1000
      0.8791    0.5273    0.8141    0.8292    0.7504    0.6341    0.6335    0.4438
      0.8135    0.5024    0.4835    0.4969    0.5359    0.0035    0.0375    0.5431
      0.2249    0.7373    0.3288    0.2137    0.0059    0.3880    0.6575    0.5658
      0.4334    0.2257    0.7599    0.9499    0.6122    0.4652    0.5896    0.5328
      0.9047    0.0928    0.1643    0.0423    0.2585    0.6279    0.7471    0.6557
   
   C = 
   
      3.2097    1.9719    3.1419    1.9526    2.6770    2.9183    3.0317    2.2834
      2.4186    1.9487    2.7812    1.6346    1.9918    2.3131    2.4642    1.9504
      2.7166    1.1075    1.7192    1.0902    1.7133    1.5590    1.5600    1.2236
      2.3666    1.4859    2.2539    1.3965    1.9549    1.8158    1.7624    1.2346
      3.7063    1.8909    3.2170    2.3680    2.7964    2.4541    2.4722    2.2691
      2.4359    1.6658    2.1591    1.5394    1.7611    2.1386    2.5402    2.1284
      1.9035    1.2531    1.7277    1.0794    1.3499    1.5164    1.5695    1.4604
      2.7563    1.5051    2.2794    2.0652    1.8798    1.6306    1.7600    1.6781
   
   D = 
   
      3.2097    1.9719    3.1419    1.9526    2.6770    2.9183    3.0317    2.2834
      2.4186    1.9487    2.7812    1.6346    1.9918    2.3131    2.4642    1.9504
      2.7166    1.1075    1.7192    1.0902    1.7133    1.5590    1.5600    1.2236
      2.3666    1.4859    2.2539    1.3965    1.9549    1.8158    1.7624    1.2346
      3.7063    1.8909    3.2170    2.3680    2.7964    2.4541    2.4722    2.2691
      2.4359    1.6658    2.1591    1.5394    1.7611    2.1386    2.5402    2.1284
      1.9035    1.2531    1.7277    1.0794    1.3499    1.5164    1.5695    1.4604
      2.7563    1.5051    2.2794    2.0652    1.8798    1.6306    1.7600    1.6781
   


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

   
      0.5824    0.1763    0.6230    0.5967    0.9679    0.2657
      0.1591    0.7437    0.7487    0.7498    0.9256    0.1049
      0.3997    0.6955    0.9871    0.4668    0.2918    0.5304
      0.4917    0.4549    0.1752    0.3379    0.4678    0.9348
      0.6280    0.7681    0.6128    0.3064    0.2995    0.4424
   
   
      0.5824
      0.6280
      0.7437
      0.6955
      0.7681
      0.6230
      0.7487
      0.9871
      0.6128
      0.5967
      0.7498
      0.9679
      0.9256
      0.5304
      0.9348
   

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

   
      7.2093    4.1439    0.4939    6.8253    4.4568    7.4833
      1.1062    1.4389    9.8558    7.0960    5.9558    9.6171
      9.7595    8.3458    6.8778    8.5058    3.0170    7.7363
      6.5909    8.3966    2.7256    4.9924    6.5169    2.5594
      6.5340    9.0064    7.8575    4.1560    9.7363    9.9736
   
   
      7.2093    0.0000    0.0000    6.8253    0.0000    7.4833
      0.0000    0.0000    9.8558    7.0960    5.9558    9.6171
      9.7595    8.3458    6.8778    8.5058    0.0000    7.7363
      6.5909    8.3966    0.0000    0.0000    6.5169    0.0000
      6.5340    9.0064    7.8575    0.0000    9.7363    9.9736
   
   
      7.2093    0.0000    0.0000    6.8253    0.0000    7.4833
      0.0000    0.0000       NaN    7.0960    5.9558       NaN
         NaN    8.3458    6.8778    8.5058    0.0000    7.7363
      6.5909    8.3966    0.0000    0.0000    6.5169    0.0000
      6.5340       NaN    7.8575    0.0000       NaN       NaN
   

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

   
      6.5000    9.3707    0.9065    4.2593    6.5000    3.1138
      6.5000    3.7160    9.0069    1.9742    4.1884    8.8623
      8.2206    6.5000    6.5000    8.0568    6.5000    1.3340
      6.5000    8.2335    1.8423    0.2336    0.1853    6.5000
      3.3767    4.4235    0.6368    8.1926    2.3555    3.9923
   
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
   
