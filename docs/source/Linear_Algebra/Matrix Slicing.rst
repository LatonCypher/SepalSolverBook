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
      0.7911    0.1797    0.7038    0.3474
   
   R1[2] = 0.7038219921224361
   C1 = 
      0.4477
      0.4313
      0.3426
      0.9386
      0.2438
      0.5860
      0.8828
      0.3891
   
   C1[5] = 0.5859678288541426

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
      0.0594    0.0799    0.0084    0.7631    0.7297
      0.0334    0.0942    0.8640    0.8858    0.2674
   

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
   
      0.0797    0.6713    0.9464    0.4160    0.3301    0.3177    0.2661    0.2010
      0.8116    0.8676    0.2066    0.5893    0.9461    0.7202    0.6919    0.4730
      0.2547    0.8739    0.2812    0.0482    0.8321    0.0038    0.0579    0.5115
      0.5692    0.4320    0.9679    0.0853    0.0525    0.2894    0.7386    0.5216
      0.4975    0.7375    0.1404    0.3223    0.9930    0.5501    0.4936    0.2143
      0.8235    0.9441    0.7306    0.1076    0.6879    0.6426    0.3835    0.8285
      0.1832    0.0332    0.7555    0.8880    0.8731    0.9344    0.0461    0.5591
      0.9472    0.9369    0.7598    0.1390    0.6263    0.4752    0.0135    0.6527
   
   B = 
   
      0.7507    0.5448    0.2202    0.7541    0.9189    0.8165    0.4588    0.7650
      0.3282    0.0851    0.2898    0.3981    0.9383    0.4836    0.0911    0.5584
      0.2597    0.2387    0.1566    0.8320    0.5149    0.8831    0.6496    0.0693
      0.7381    0.4571    0.9560    0.0782    0.9153    0.6896    0.9482    0.2515
      0.1276    0.6635    0.1008    0.1729    0.9175    0.6943    0.1548    0.0023
      0.2354    0.1953    0.8916    0.4446    0.5553    0.6175    0.6373    0.6971
      0.9176    0.7918    0.5272    0.7888    0.3707    0.2774    0.6813    0.0989
      0.8040    0.6145    0.6883    0.9303    0.9179    0.5430    0.9176    0.2071
   
   C = 
   
      1.3556    1.1318    1.3531    1.7425    2.3335    2.1206    1.7262    0.8962
      2.6880    2.4414    2.4538    2.6450    4.1642    3.2214    2.6553    1.9385
      1.1580    1.2151    0.8692    1.4448    2.4994    1.7859    1.0648    0.8307
      2.0554    1.6136    1.4954    2.6188    2.4662    2.2906    2.1846    1.1481
      1.7713    1.8033    1.6517    1.8158    3.1127    2.3915    1.7295    1.3622
      2.4543    2.1471    2.0870    3.0911    4.0079    3.2790    2.5781    1.8940
      1.8232    1.8306    2.3474    1.9722    3.2515    2.9447    2.6950    1.2081
      2.0474    1.7607    1.6752    2.6677    3.7105    3.0797    2.1533    1.8046
   
   D = 
   
      1.3556    1.1318    1.3531    1.7425    2.3335    2.1206    1.7262    0.8962
      2.6880    2.4414    2.4538    2.6450    4.1642    3.2214    2.6553    1.9385
      1.1580    1.2151    0.8692    1.4448    2.4994    1.7859    1.0648    0.8307
      2.0554    1.6136    1.4954    2.6188    2.4662    2.2906    2.1846    1.1481
      1.7713    1.8033    1.6517    1.8158    3.1127    2.3915    1.7295    1.3622
      2.4543    2.1471    2.0870    3.0911    4.0079    3.2790    2.5781    1.8940
      1.8232    1.8306    2.3474    1.9722    3.2515    2.9447    2.6950    1.2081
      2.0474    1.7607    1.6752    2.6677    3.7105    3.0797    2.1533    1.8046
   


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

   
      0.2109    0.6205    0.0800    0.6151    0.7566    0.6665
      0.7225    0.3073    0.2122    0.9427    0.3915    0.0566
      0.5747    0.6843    0.9306    0.8306    0.9060    0.3381
      0.9320    0.8268    0.7956    0.7195    0.7183    0.3672
      0.4067    0.3117    0.2855    0.2938    0.9531    0.1615
   
   
      0.7225
      0.5747
      0.9320
      0.6205
      0.6843
      0.8268
      0.9306
      0.7956
      0.6151
      0.9427
      0.8306
      0.7195
      0.7566
      0.9060
      0.7183
      0.9531
      0.6665
   

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

   
      8.6277    0.4700    6.3168    7.6074    7.5804    5.2927
      0.8157    2.8765    3.6126    5.8098    6.8060    4.5087
      7.6955    8.2586    4.3403    2.9740    6.0954    5.6633
      4.1024    6.9923    9.1177    4.5950    3.8817    1.1723
      9.6494    5.5942    9.8841    7.6091    3.1704    8.9173
   
   
      8.6277    0.0000    6.3168    7.6074    7.5804    5.2927
      0.0000    0.0000    0.0000    5.8098    6.8060    0.0000
      7.6955    8.2586    0.0000    0.0000    6.0954    5.6633
      0.0000    6.9923    9.1177    0.0000    0.0000    0.0000
      9.6494    5.5942    9.8841    7.6091    0.0000    8.9173
   
   
      8.6277    0.0000    6.3168    7.6074    7.5804    5.2927
      0.0000    0.0000    0.0000    5.8098    6.8060    0.0000
      7.6955    8.2586    0.0000    0.0000    6.0954    5.6633
      0.0000    6.9923       NaN    0.0000    0.0000    0.0000
         NaN    5.5942       NaN    7.6091    0.0000    8.9173
   

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

   
      1.9106    6.5000    3.1969    6.5000    4.4803    4.1778
      8.4242    1.9883    4.2220    9.3119    9.9948    4.5052
      6.5000    9.2849    3.6900    8.0698    6.5000    1.0615
      0.3641    3.4476    1.9219    9.1839    1.6063    6.5000
      6.5000    2.9069    3.3007    6.5000    6.5000    8.9384
   
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
   
