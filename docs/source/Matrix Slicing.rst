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
      0.3801    0.7376    0.9253    0.7107
   
   R1[2] = 0.925305093359384
   C1 = 
      0.9861
      0.0243
      0.5807
      0.4564
      0.1480
      0.7669
      0.4523
      0.0708
   
   C1[5] = 0.7668969721193427

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
      0.8982    0.5388    0.1653    0.2988    0.8054
      0.7801    0.0788    0.6993    0.9312    0.3137
   

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
   
      0.6911    0.9922    0.2429    0.4716    0.6687    0.8094    0.9705    0.9699
      0.1318    0.4313    0.4827    0.3890    0.1062    0.7816    0.5917    0.9156
      0.9297    0.2767    0.5452    0.4308    0.8963    0.8305    0.6898    0.9673
      0.0258    0.8887    0.2537    0.7479    0.9914    0.5321    0.5632    0.9395
      0.0932    0.7018    0.9364    0.4139    0.3912    0.5521    0.5269    0.7543
      0.1185    0.4040    0.5146    0.3894    0.2638    0.1521    0.0463    0.0175
      0.8288    0.3191    0.0488    0.4920    0.3662    0.3212    0.6306    0.8648
      0.7985    0.3971    0.6301    0.3453    0.1451    0.9727    0.8846    0.9371
   
   B = 
   
      0.2758    0.9834    0.5783    0.5890    0.2038    0.6586    0.4031    0.7476
      0.5590    0.2731    0.9903    0.0559    0.0353    0.4697    0.4484    0.2411
      0.1692    0.6474    0.9825    0.5243    0.0922    0.5994    0.0274    0.8047
      0.5442    0.9900    0.4333    0.5431    0.4457    0.4598    0.4126    0.9999
      0.2653    0.2579    0.3452    0.7305    0.1909    0.3997    0.6383    0.1222
      0.5347    0.3692    0.8651    0.7406    0.2795    0.4584    0.2131    0.8122
      0.6462    0.7781    0.1703    0.6201    0.9904    0.6334    0.4003    0.4871
      0.1480    0.8281    0.6753    0.7069    0.2428    0.8658    0.0762    0.6336
   
   C = 
   
      2.4240    3.6043    3.5765    3.2215    1.9590    3.3763    1.9865    3.2493
      1.5348    2.4795    2.5780    2.2367    1.3070    2.3257    0.9613    2.4960
      2.0087    3.6448    3.3326    3.4643    1.7628    3.2805    1.7905    3.3642
      2.0044    2.8411    3.0013    2.7358    1.5172    2.7407    1.7678    2.6085
      1.6528    2.6386    3.0600    2.3645    1.2486    2.5388    1.1845    2.6370
      0.7414    1.1203    1.3853    0.9201    0.4025    0.9750    0.6244    1.1789
      1.4874    2.8407    2.1522    2.3066    1.3982    2.3930    1.3021    2.3886
      2.0057    3.5042    3.2988    3.0478    1.7919    3.1243    1.3852    3.3774
   
   D = 
   
      2.4240    3.6043    3.5765    3.2215    1.9590    3.3763    1.9865    3.2493
      1.5348    2.4795    2.5780    2.2367    1.3070    2.3257    0.9613    2.4960
      2.0087    3.6448    3.3326    3.4643    1.7628    3.2805    1.7905    3.3642
      2.0044    2.8411    3.0013    2.7358    1.5172    2.7407    1.7678    2.6085
      1.6528    2.6386    3.0600    2.3645    1.2486    2.5388    1.1845    2.6370
      0.7414    1.1203    1.3853    0.9201    0.4025    0.9750    0.6244    1.1789
      1.4874    2.8407    2.1522    2.3066    1.3982    2.3930    1.3021    2.3886
      2.0057    3.5042    3.2988    3.0478    1.7919    3.1243    1.3852    3.3774
   


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

   
      0.2674    0.5238    0.5901    0.3029    0.8189    0.0645
      0.1625    0.1899    0.5158    0.5901    0.2863    0.3662
      0.2434    0.4117    0.5993    0.9339    0.0545    0.2330
      0.3183    0.4803    0.2224    0.9478    0.9636    0.8681
      0.0363    0.3024    0.2074    0.9727    0.8158    0.0656
   
   
      0.5238
      0.5901
      0.5158
      0.5993
      0.5901
      0.9339
      0.9478
      0.9727
      0.8189
      0.9636
      0.8158
      0.8681
   

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

   
      8.6834    3.0302    2.6388    4.3187    3.8607    1.9313
      1.0386    6.3690    2.1924    7.4613    0.5650    7.4911
      0.0654    1.4678    6.1924    5.6521    2.3992    9.9379
      7.9475    5.7793    5.3030    9.8466    0.4307    3.0731
      7.7569    1.5161    6.1958    4.2799    2.9020    9.2836
   
   
      8.6834    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    6.3690    0.0000    7.4613    0.0000    7.4911
      0.0000    0.0000    6.1924    5.6521    0.0000    9.9379
      7.9475    5.7793    5.3030    9.8466    0.0000    0.0000
      7.7569    0.0000    6.1958    0.0000    0.0000    9.2836
   
   
      8.6834    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    6.3690    0.0000    7.4613    0.0000    7.4911
      0.0000    0.0000    6.1924    5.6521    0.0000       NaN
      7.9475    5.7793    5.3030       NaN    0.0000    0.0000
      7.7569    0.0000    6.1958    0.0000    0.0000       NaN
   

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

   
      0.1735    6.5000    6.5000    9.7442    6.5000    9.6078
      2.1659    1.0201    6.5000    6.5000    8.9953    9.2607
      1.8491    2.6296    3.9902    9.6719    2.7594    6.5000
      6.5000    6.5000    6.5000    6.5000    6.5000    8.1903
      4.4190    6.5000    0.1518    8.9346    6.5000    6.5000
   
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
   
