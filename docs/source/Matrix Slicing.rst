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
      0.8730    0.9838    0.5551    0.0660
   
   R1[2] = 0.5550974682731578
   C1 = 
      0.0572
      0.6788
      0.4290
      0.2013
      0.2801
      0.3314
      0.2854
      0.2534
   
   C1[5] = 0.3314319860376569

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
      0.5925    0.7917    0.1318    0.3829    0.2126
      0.3482    0.3182    0.8685    0.8466    0.1901
   

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
   
      0.7045    0.7983    0.9364    0.6114    0.2520    0.6551    0.2105    0.6683
      0.3132    0.7740    0.1881    0.8246    0.6525    0.4483    0.6418    0.0331
      0.1554    0.3238    0.5761    0.7455    0.5512    0.8178    0.0120    0.4176
      0.4648    0.6496    0.9312    0.1032    0.5469    0.9176    0.1179    0.1076
      0.7837    0.3154    0.5962    0.2381    0.8213    0.0179    0.2663    0.4585
      0.8601    0.7600    0.1515    0.5143    0.5003    0.0561    0.7992    0.2372
      0.4121    0.2500    0.5888    0.5798    0.7185    0.9864    0.7101    0.6063
      0.3287    0.6306    0.8876    0.9696    0.7188    0.9531    0.1783    0.6028
   
   B = 
   
      0.7346    0.6210    0.9807    0.0676    0.8837    0.7250    0.2645    0.5612
      0.0026    0.8122    0.0859    0.6861    0.6753    0.4485    0.3461    0.3207
      0.4434    0.9829    0.3171    0.8515    0.0412    0.6525    0.6903    0.8440
      0.1920    0.6134    0.1778    0.3933    0.5562    0.7879    0.7440    0.6324
      0.2888    0.9137    0.9715    0.7461    0.2680    0.5653    0.9896    0.8350
      0.5235    0.0521    0.8588    0.6103    0.5870    0.5584    0.9225    0.7883
      0.1662    0.7033    0.5404    0.8368    0.7476    0.9551    0.3772    0.1655
      0.0828    0.6254    0.2584    0.1334    0.2835    0.4212    0.5749    0.2340
   
   C = 
   
      1.5581    3.2116    2.2591    2.4863    2.3392    2.9523    2.8812    2.7463
      1.0063    2.6055    1.9543    2.3386    2.1931    2.5928    2.4145    2.1165
      1.1374    2.1989    1.8477    1.9926    1.5494    2.1768    2.6500    2.3534
      1.4426    2.4925    2.2362    2.3917    1.7490    2.2967    2.5614    2.5450
      1.2153    2.7002    2.1027    1.7784    1.6222    2.2080    2.0985    2.0459
      1.1260    2.7862    2.0757    2.0188    2.3973    2.6459    1.9625    1.8293
      1.5678    2.9798    2.8010    2.7419    2.3540    3.1424    3.2707    2.8119
      1.6088    3.3922    2.5994    2.9396    2.3485    3.2269    3.6435    3.2710
   
   D = 
   
      1.5581    3.2116    2.2591    2.4863    2.3392    2.9523    2.8812    2.7463
      1.0063    2.6055    1.9543    2.3386    2.1931    2.5928    2.4145    2.1165
      1.1374    2.1989    1.8477    1.9926    1.5494    2.1768    2.6500    2.3534
      1.4426    2.4925    2.2362    2.3917    1.7490    2.2967    2.5614    2.5450
      1.2153    2.7002    2.1027    1.7784    1.6222    2.2080    2.0985    2.0459
      1.1260    2.7862    2.0757    2.0188    2.3973    2.6459    1.9625    1.8293
      1.5678    2.9798    2.8010    2.7419    2.3540    3.1424    3.2707    2.8119
      1.6088    3.3922    2.5994    2.9396    2.3485    3.2269    3.6435    3.2710
   


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

   
      0.9367    0.8350    0.0685    0.5295    0.7749    0.3409
      0.3024    0.4500    0.6353    0.1884    0.9767    0.2642
      0.6278    0.5822    0.4145    0.0932    0.6846    0.4174
      0.6312    0.7763    0.3577    0.2542    0.2315    0.1637
      0.8575    0.7163    0.8615    0.0724    0.9429    0.8462
   
   
      0.9367
      0.6278
      0.6312
      0.8575
      0.8350
      0.5822
      0.7763
      0.7163
      0.6353
      0.8615
      0.5295
      0.7749
      0.9767
      0.6846
      0.9429
      0.8462
   

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

   
      4.6148    0.0860    3.3453    9.7925    9.9651    5.3911
      6.7180    2.0819    9.8006    6.8625    2.3140    8.1953
      9.5746    1.0498    2.2261    8.2900    2.7703    0.3776
      9.2707    5.3527    8.9267    6.2102    1.6035    7.0151
      0.4778    5.8316    1.1411    8.5855    5.1220    7.4141
   
   
      0.0000    0.0000    0.0000    9.7925    9.9651    5.3911
      6.7180    0.0000    9.8006    6.8625    0.0000    8.1953
      9.5746    0.0000    0.0000    8.2900    0.0000    0.0000
      9.2707    5.3527    8.9267    6.2102    0.0000    7.0151
      0.0000    5.8316    0.0000    8.5855    5.1220    7.4141
   
   
      0.0000    0.0000    0.0000       NaN       NaN    5.3911
      6.7180    0.0000       NaN    6.8625    0.0000    8.1953
         NaN    0.0000    0.0000    8.2900    0.0000    0.0000
         NaN    5.3527    8.9267    6.2102    0.0000    7.0151
      0.0000    5.8316    0.0000    8.5855    5.1220    7.4141
   

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

   
      3.6411    2.7473    6.5000    6.5000    6.5000    1.9704
      8.3392    8.4060    0.4337    9.6573    4.8863    4.0805
      4.0167    1.3286    6.5000    1.4470    2.8023    0.1739
      1.1938    2.5006    0.5625    3.2893    3.8166    6.5000
      6.5000    0.6424    6.5000    8.0887    2.2628    3.4474
   
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
   
