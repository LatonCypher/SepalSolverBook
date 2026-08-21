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
      0.6147    0.5414    0.0873    0.9536
   
   R1[2] = 0.08727273663006607
   C1 = 
      0.8257
      0.5172
      0.3663
      0.9818
      0.2493
      0.8163
      0.3737
      0.0614
   
   C1[5] = 0.8163367242925988

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
      0.7699    0.5150    0.8699    0.8448    0.9175
      0.8710    0.7194    0.8260    0.9427    0.8192
   

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
   
      0.1909    0.8479    0.7410    0.7353    0.4133    0.0926    0.7813    0.7669
      0.1337    0.2943    0.8512    0.3497    0.2773    0.5906    0.0716    0.0085
      0.9170    0.3339    0.1703    0.1382    0.6224    0.8087    0.5275    0.9127
      0.0725    0.3162    0.8309    0.4365    0.5689    0.2753    0.3748    0.2581
      0.7281    0.1549    0.2318    0.1122    0.2570    0.5321    0.0483    0.2767
      0.6325    0.6607    0.7512    0.4793    0.8162    0.5475    0.5916    0.4603
      0.5379    0.3124    0.8767    0.5517    0.6926    0.9756    0.4341    0.3960
      0.2357    0.2215    0.6126    0.7527    0.4870    0.8817    0.4594    0.7689
   
   B = 
   
      0.2501    0.4616    0.6159    0.0390    0.8998    0.8279    0.4759    0.5000
      0.7360    0.5167    0.0220    0.7659    0.8053    0.2951    0.7940    0.1618
      0.9851    0.5676    0.7253    0.3824    0.4109    0.6045    0.8775    0.9088
      0.4404    0.9263    0.2089    0.7174    0.6348    0.3934    0.4182    0.4792
      0.5530    0.6763    0.1528    0.6264    0.2551    0.3433    0.3360    0.3478
      0.3991    0.7931    0.8776    0.9042    0.6103    0.5930    0.9100    0.7565
      0.6301    0.4183    0.8323    0.6944    0.0554    0.6138    0.8968    0.7604
      0.3313    0.6063    0.5057    0.6282    0.0685    0.8551    0.0972    0.1607
   
   C = 
   
      2.7374    2.7727    2.0099    2.8346    1.8835    2.4776    2.7201    2.1896
      1.6795    1.7119    1.4038    1.5697    1.3647    1.3463    1.8861    1.6546
      2.0055    2.6569    2.4300    2.5165    1.9957    2.8126    2.4157    2.1095
      2.0077    1.9892    1.5165    1.9035    1.2899    1.6366    2.0000    1.7843
      1.0504    1.4354    1.3298    1.1655    1.3581    1.5028    1.3606    1.2265
      2.7907    3.0165    2.3794    2.8679    2.3206    2.7229    3.0329    2.5573
      2.6482    3.0823    2.6129    2.8574    2.2692    2.7059    3.0524    2.7532
      2.3223    2.9552    2.3710    2.8574    1.8603    2.5565    2.5931    2.3804
   
   D = 
   
      2.7374    2.7727    2.0099    2.8346    1.8835    2.4776    2.7201    2.1896
      1.6795    1.7119    1.4038    1.5697    1.3647    1.3463    1.8861    1.6546
      2.0055    2.6569    2.4300    2.5165    1.9957    2.8126    2.4157    2.1095
      2.0077    1.9892    1.5165    1.9035    1.2899    1.6366    2.0000    1.7843
      1.0504    1.4354    1.3298    1.1655    1.3581    1.5028    1.3606    1.2265
      2.7907    3.0165    2.3794    2.8679    2.3206    2.7229    3.0329    2.5573
      2.6482    3.0823    2.6129    2.8574    2.2692    2.7059    3.0524    2.7532
      2.3223    2.9552    2.3710    2.8574    1.8603    2.5565    2.5931    2.3804
   


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

   
      0.5847    0.4641    0.2050    0.8390    0.5453    0.0342
      0.7397    0.7461    0.4172    0.5985    0.9083    0.1509
      0.7665    0.7112    0.1705    0.3177    0.6091    0.2245
      0.0254    0.2371    0.8024    0.4758    0.8282    0.2488
      0.7135    0.7265    0.9609    0.9309    0.4620    0.6146
   
   
      0.5847
      0.7397
      0.7665
      0.7135
      0.7461
      0.7112
      0.7265
      0.8024
      0.9609
      0.8390
      0.5985
      0.9309
      0.5453
      0.9083
      0.6091
      0.8282
      0.6146
   

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

   
      0.5949    9.3986    8.2080    2.7816    5.6317    3.4184
      1.3473    7.2630    6.1223    9.0401    9.2762    3.1005
      1.6552    8.4686    8.2013    8.9894    2.9871    9.4872
      7.2674    7.5266    1.6631    1.7127    7.1385    2.5908
      5.5130    7.3400    8.4824    7.0705    6.2949    6.2710
   
   
      0.0000    9.3986    8.2080    0.0000    5.6317    0.0000
      0.0000    7.2630    6.1223    9.0401    9.2762    0.0000
      0.0000    8.4686    8.2013    8.9894    0.0000    9.4872
      7.2674    7.5266    0.0000    0.0000    7.1385    0.0000
      5.5130    7.3400    8.4824    7.0705    6.2949    6.2710
   
   
      0.0000       NaN    8.2080    0.0000    5.6317    0.0000
      0.0000    7.2630    6.1223       NaN       NaN    0.0000
      0.0000    8.4686    8.2013    8.9894    0.0000       NaN
      7.2674    7.5266    0.0000    0.0000    7.1385    0.0000
      5.5130    7.3400    8.4824    7.0705    6.2949    6.2710
   

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

   
      2.4313    3.1027    1.8956    8.7816    6.5000    6.5000
      6.5000    9.0612    1.5391    6.5000    2.3374    0.2889
      8.2662    6.5000    0.9288    6.5000    8.2514    6.5000
      8.8537    3.4298    8.0914    1.6008    8.5066    3.5688
      8.6886    2.4490    4.1842    2.4877    8.1083    8.1019
   
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
   
