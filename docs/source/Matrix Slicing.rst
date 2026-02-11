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
      0.9196    0.3276    0.3897    0.7036
   
   R1[2] = 0.38970705971162845
   C1 = 
      0.7940
      0.3705
      0.7530
      0.2499
      0.1186
      0.4576
      0.3759
      0.2269
   
   C1[5] = 0.4575889783200491

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
      0.5572    0.2000    0.9116    0.6806    0.8430
      0.1825    0.8245    0.2028    0.0812    0.6403
   

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
   
      0.2523    0.6985    0.3862    0.9048    0.2872    0.2342    0.4710    0.5816
      0.7647    0.6936    0.9566    0.6748    0.7900    0.2953    0.8391    0.8965
      0.3325    0.9074    0.8632    0.9397    0.6860    0.4542    0.9074    0.2122
      0.2430    0.7646    0.0140    0.7121    0.0617    0.2707    0.9483    0.4227
      0.0915    0.3953    0.6817    0.0251    0.8015    0.1465    0.0330    0.1966
      0.6466    0.8086    0.6459    0.0578    0.9405    0.8586    0.1374    0.0685
      0.3827    0.0009    0.1365    0.0131    0.0321    0.7098    0.9916    0.5100
      0.4474    0.7172    0.5427    0.1916    0.0704    0.8315    0.3263    0.1817
   
   B = 
   
      0.7836    0.3643    0.7692    0.4037    0.0128    0.7231    0.4314    0.7952
      0.7198    0.9229    0.4621    0.7996    0.6726    0.5871    0.8991    0.9341
      0.5995    0.1522    0.8641    0.3372    0.2239    0.5338    0.5821    0.5326
      0.3756    0.6528    0.0418    0.2990    0.4360    0.0640    0.8907    0.8807
      0.4145    0.7176    0.5511    0.5831    0.5733    0.3436    0.6004    0.1709
      0.4149    0.4300    0.6397    0.8934    0.6447    0.5653    0.4184    0.5500
      0.6295    0.7949    0.5473    0.4640    0.7269    0.5455    0.4497    0.2022
      0.7827    0.9970    0.9579    0.2717    0.1074    0.0837    0.3073    0.2696
   
   C = 
   
      2.2398    2.6469    2.0113    1.8143    1.6745    1.3932    2.4285    2.2855
      3.6053    3.7593    3.7057    2.7449    2.3342    2.4851    3.3621    3.0685
      2.9943    3.3236    2.8287    2.7162    2.5860    2.2993    3.3739    3.0072
      2.0823    2.5969    1.7132    1.7597    1.7755    1.4045    2.1413    2.0072
      1.3420    1.3786    1.5850    1.2573    1.0297    1.0564    1.4319    1.1044
      2.3839    2.3394    2.6399    2.5405    1.9219    2.1800    2.4402    2.3436
      1.7184    1.7944    1.9162    1.4566    1.2933    1.3468    1.1759    1.1232
      1.9860    1.8811    2.0758    1.9790    1.5263    1.7340    1.9172    2.0678
   
   D = 
   
      2.2398    2.6469    2.0113    1.8143    1.6745    1.3932    2.4285    2.2855
      3.6053    3.7593    3.7057    2.7449    2.3342    2.4851    3.3621    3.0685
      2.9943    3.3236    2.8287    2.7162    2.5860    2.2993    3.3739    3.0072
      2.0823    2.5969    1.7132    1.7597    1.7755    1.4045    2.1413    2.0072
      1.3420    1.3786    1.5850    1.2573    1.0297    1.0564    1.4319    1.1044
      2.3839    2.3394    2.6399    2.5405    1.9219    2.1800    2.4402    2.3436
      1.7184    1.7944    1.9162    1.4566    1.2933    1.3468    1.1759    1.1232
      1.9860    1.8811    2.0758    1.9790    1.5263    1.7340    1.9172    2.0678
   


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

   
      0.0967    0.3023    0.8663    0.4344    0.7651    0.2086
      0.2734    0.0639    0.9772    0.2690    0.5095    0.1910
      0.6173    0.8943    0.1213    0.2065    0.3586    0.9830
      0.8242    0.7652    0.5689    0.6801    0.4546    0.8193
      0.6911    0.1805    0.6424    0.3632    0.8426    0.2943
   
   
      0.6173
      0.8242
      0.6911
      0.8943
      0.7652
      0.8663
      0.9772
      0.5689
      0.6424
      0.6801
      0.7651
      0.5095
      0.8426
      0.9830
      0.8193
   

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

   
      0.2912    0.4153    3.4421    0.2032    6.1170    9.7834
      7.8661    7.0364    8.9914    8.9918    2.8732    3.2446
      3.9603    2.9524    0.7978    5.6482    9.2412    9.2701
      1.9384    3.9024    5.9553    4.7786    6.4010    0.4160
      5.5849    7.0006    6.0697    1.9009    5.0233    1.0353
   
   
      0.0000    0.0000    0.0000    0.0000    6.1170    9.7834
      7.8661    7.0364    8.9914    8.9918    0.0000    0.0000
      0.0000    0.0000    0.0000    5.6482    9.2412    9.2701
      0.0000    0.0000    5.9553    0.0000    6.4010    0.0000
      5.5849    7.0006    6.0697    0.0000    5.0233    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    6.1170       NaN
      7.8661    7.0364    8.9914    8.9918    0.0000    0.0000
      0.0000    0.0000    0.0000    5.6482       NaN       NaN
      0.0000    0.0000    5.9553    0.0000    6.4010    0.0000
      5.5849    7.0006    6.0697    0.0000    5.0233    0.0000
   

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

   
      4.7044    8.1433    2.7542    4.7424    8.4074    0.0422
      1.7976    6.5000    1.1353    2.1377    6.5000    4.5521
      6.5000    0.3967    2.6836    2.8273    8.7366    9.7947
      2.7045    3.4607    2.2014    4.9091    9.1546    1.5189
      9.7474    0.4639    1.9181    4.0446    6.5000    8.5822
   
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
   
