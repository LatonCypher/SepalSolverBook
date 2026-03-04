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
      0.4972    0.9737    0.3866    0.0368
   
   R1[2] = 0.38656822448683537
   C1 = 
      0.2332
      0.4840
      0.2909
      0.8274
      0.6343
      0.1493
      0.8256
      0.3016
   
   C1[5] = 0.1493418610461804

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
      0.1887    0.8052    0.9534    0.7699    0.1670
      0.1991    0.6398    0.9730    0.8898    0.5086
   

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
   
      0.0310    0.4753    0.6025    0.0254    0.9087    0.2834    0.7832    0.6385
      0.6732    0.5472    0.7415    0.0765    0.0216    0.9576    0.0455    0.3879
      0.7508    0.5507    0.6748    0.2723    0.4088    0.6377    0.1099    0.9792
      0.7274    0.1344    0.4057    0.4614    0.4495    0.6025    0.6921    0.5669
      0.9077    0.3354    0.7901    0.0603    0.3231    0.0039    0.6246    0.9913
      0.0092    0.2417    0.6720    0.4119    0.9259    0.4647    0.1075    0.9491
      0.0049    0.7649    0.6311    0.4635    0.8369    0.3415    0.7638    0.9826
      0.5966    0.6969    0.2928    0.6752    0.8928    0.9503    0.2487    0.8896
   
   B = 
   
      0.7926    0.6811    0.4030    0.3452    0.4708    0.6134    0.0777    0.9155
      0.8984    0.4299    0.7714    0.8042    0.2470    0.2973    0.1340    0.4861
      0.5885    0.1946    0.8609    0.5661    0.6119    0.4591    0.6627    0.8716
      0.8204    0.8145    0.1033    0.5269    0.8415    0.3817    0.9632    0.8878
      0.7245    0.1273    0.8328    0.5799    0.0321    0.3643    0.2542    0.9961
      0.2240    0.1073    0.2624    0.3978    0.7369    0.0491    0.0171    0.1450
      0.4598    0.3510    0.3602    0.8807    0.3434    0.1183    0.6442    0.6630
      0.3004    0.2633    0.2590    0.9398    0.9305    0.1478    0.7359    0.1695
   
   C = 
   
      2.1007    0.9524    2.1790    2.6769    1.6231    0.9785    1.7001    2.3808
      1.8919    1.1239    1.7257    1.9307    2.0531    1.0628    1.0273    1.8528
      2.4941    1.5181    2.1374    2.7354    2.5635    1.3759    1.7478    2.5236
      2.2637    1.5220    1.7223    2.4746    2.2357    1.2074    1.7754    2.5846
      2.3551    1.4870    2.0627    2.7328    2.1945    1.3805    1.9112    2.6408
      2.0672    1.0317    1.9887    2.5034    2.1140    1.0563    1.8862    2.2992
      2.7718    1.5024    2.4993    3.4356    2.4225    1.2542    2.4012    2.8940
      3.0665    1.8500    2.4127    3.2389    2.8425    1.4980    2.0422    3.0825
   
   D = 
   
      2.1007    0.9524    2.1790    2.6769    1.6231    0.9785    1.7001    2.3808
      1.8919    1.1239    1.7257    1.9307    2.0531    1.0628    1.0273    1.8528
      2.4941    1.5181    2.1374    2.7354    2.5635    1.3759    1.7478    2.5236
      2.2637    1.5220    1.7223    2.4746    2.2357    1.2074    1.7754    2.5846
      2.3551    1.4870    2.0627    2.7328    2.1945    1.3805    1.9112    2.6408
      2.0672    1.0317    1.9887    2.5034    2.1140    1.0563    1.8862    2.2992
      2.7718    1.5024    2.4993    3.4356    2.4225    1.2542    2.4012    2.8940
      3.0665    1.8500    2.4127    3.2389    2.8425    1.4980    2.0422    3.0825
   


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

   
      0.6272    0.8404    0.0210    0.7374    0.1227    0.4194
      0.5226    0.7209    0.1224    0.3965    0.5778    0.6903
      0.4675    0.3969    0.5663    0.0455    0.1050    0.1459
      0.7542    0.5210    0.8581    0.6157    0.6712    0.9307
      0.5596    0.0692    0.2519    0.4617    0.5999    0.3302
   
   
      0.6272
      0.5226
      0.7542
      0.5596
      0.8404
      0.7209
      0.5210
      0.5663
      0.8581
      0.7374
      0.6157
      0.5778
      0.6712
      0.5999
      0.6903
      0.9307
   

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

   
      1.2152    9.8772    2.5174    3.1212    1.4813    4.8872
      9.2374    3.4117    9.9774    1.4840    0.1375    4.1311
      3.9134    5.9817    8.0768    8.6196    7.4525    4.4841
      4.2000    0.9288    3.6783    0.1832    9.4762    9.1339
      9.9034    9.5078    4.2842    0.3786    6.7210    0.3193
   
   
      0.0000    9.8772    0.0000    0.0000    0.0000    0.0000
      9.2374    0.0000    9.9774    0.0000    0.0000    0.0000
      0.0000    5.9817    8.0768    8.6196    7.4525    0.0000
      0.0000    0.0000    0.0000    0.0000    9.4762    9.1339
      9.9034    9.5078    0.0000    0.0000    6.7210    0.0000
   
   
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
         NaN    0.0000       NaN    0.0000    0.0000    0.0000
      0.0000    5.9817    8.0768    8.6196    7.4525    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN       NaN
         NaN       NaN    0.0000    0.0000    6.7210    0.0000
   

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

   
      6.5000    9.5880    6.5000    1.8114    2.5735    6.5000
      6.5000    9.1768    6.5000    6.5000    8.7079    6.5000
      3.0740    6.5000    0.9318    6.5000    3.0917    4.7952
      9.2312    8.5008    9.2949    6.5000    3.9092    6.5000
      8.2785    9.8507    6.5000    2.9772    4.4407    4.2084
   
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
   
