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
      0.1238    0.3249    0.0410    0.3995
   
   R1[2] = 0.041045134919993065
   C1 = 
      0.8307
      0.9437
      0.4104
      0.2458
      0.4338
      0.0334
      0.1155
      0.2446
   
   C1[5] = 0.03335403145098714

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
      0.1611    0.8288    0.2661    0.0300    0.3275
      0.0478    0.4095    0.9111    0.1723    0.0746
   

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
   
      0.5038    0.8509    0.3524    0.7337    0.4974    0.6755    0.0988    0.7740
      0.3142    0.0205    0.7707    0.6984    0.4267    0.6795    0.7939    0.6990
      0.7312    0.1503    0.7438    0.3203    0.9168    0.6335    0.3198    0.8354
      0.3103    0.4149    0.5602    0.6602    0.3182    0.6023    0.6949    0.9470
      0.9473    0.4264    0.0197    0.0314    0.2249    0.7476    0.2868    0.4682
      0.5804    0.2207    0.6382    0.1317    0.7086    0.4633    0.9242    0.3911
      0.3625    0.4124    0.3091    0.4574    0.8581    0.0253    0.1710    0.1479
      0.1617    0.1069    0.5540    0.9440    0.7867    0.8655    0.6395    0.5821
   
   B = 
   
      0.5980    0.9417    0.5116    0.9028    0.7432    0.9953    0.6035    0.2475
      0.4355    0.5190    0.4135    0.3605    0.8910    0.4008    0.7512    0.8053
      0.0271    0.7505    0.3133    0.4328    0.0999    0.4292    0.7116    0.6332
      0.6766    0.4174    0.8247    0.4615    0.8789    0.3232    0.8579    0.7024
      0.4914    0.1262    0.3251    0.8782    0.4765    0.2648    0.9249    0.1645
      0.8817    0.4544    0.3975    0.3009    0.0516    0.2424    0.9648    0.5378
      0.2327    0.6930    0.6419    0.8546    0.8387    0.3542    0.0670    0.0585
      0.3556    0.7647    0.6262    0.7028    0.2846    0.8168    0.9740    0.2993
   
   C = 
   
      2.3160    2.5168    2.3034    2.5212    2.3876    2.1935    3.6957    2.2309
      1.9324    2.6238    2.3427    2.6958    2.0457    2.0073    3.1368    1.7640
      2.1201    2.7225    2.2117    3.0402    2.0086    2.4027    3.6525    1.7581
      2.0140    2.7231    2.4322    2.6970    2.2715    2.1789    3.3084    1.9297
      1.7769    2.0662    1.5407    2.0286    1.6331    1.8572    2.3376    1.2083
      1.6606    2.4346    1.9495    2.7670    2.0556    1.9295    2.6285    1.3548
      1.2506    1.3297    1.3215    1.8321    1.6655    1.2213    2.1145    1.1479
      2.3025    2.3984    2.4538    2.7669    2.2220    1.8668    3.5544    1.9464
   
   D = 
   
      2.3160    2.5168    2.3034    2.5212    2.3876    2.1935    3.6957    2.2309
      1.9324    2.6238    2.3427    2.6958    2.0457    2.0073    3.1368    1.7640
      2.1201    2.7225    2.2117    3.0402    2.0086    2.4027    3.6525    1.7581
      2.0140    2.7231    2.4322    2.6970    2.2715    2.1789    3.3084    1.9297
      1.7769    2.0662    1.5407    2.0286    1.6331    1.8572    2.3376    1.2083
      1.6606    2.4346    1.9495    2.7670    2.0556    1.9295    2.6285    1.3548
      1.2506    1.3297    1.3215    1.8321    1.6655    1.2213    2.1145    1.1479
      2.3025    2.3984    2.4538    2.7669    2.2220    1.8668    3.5544    1.9464
   


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

   
      0.8823    0.0752    0.5399    0.5674    0.6418    0.0069
      0.0137    0.8707    0.7684    0.8117    0.3060    0.9891
      0.0955    0.0520    0.6381    0.0631    0.4352    0.8332
      0.5149    0.7194    0.6944    0.0727    0.6358    0.4100
      0.8722    0.5623    0.5886    0.7036    0.3614    0.0237
   
   
      0.8823
      0.5149
      0.8722
      0.8707
      0.7194
      0.5623
      0.5399
      0.7684
      0.6381
      0.6944
      0.5886
      0.5674
      0.8117
      0.7036
      0.6418
      0.6358
      0.9891
      0.8332
   

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

   
      5.3678    0.4216    8.4581    3.7541    8.1731    0.6917
      5.5219    6.5865    3.6755    4.9774    2.6418    0.5455
      0.5274    2.4616    3.4259    7.2212    4.1008    8.4527
      3.5822    3.7402    6.7824    1.8103    7.2280    3.0938
      8.6021    9.0409    5.0735    8.7078    8.7476    7.0697
   
   
      5.3678    0.0000    8.4581    0.0000    8.1731    0.0000
      5.5219    6.5865    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.2212    0.0000    8.4527
      0.0000    0.0000    6.7824    0.0000    7.2280    0.0000
      8.6021    9.0409    5.0735    8.7078    8.7476    7.0697
   
   
      5.3678    0.0000    8.4581    0.0000    8.1731    0.0000
      5.5219    6.5865    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.2212    0.0000    8.4527
      0.0000    0.0000    6.7824    0.0000    7.2280    0.0000
      8.6021       NaN    5.0735    8.7078    8.7476    7.0697
   

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

   
      0.2367    0.9089    8.6675    4.0638    1.6512    6.5000
      1.2158    1.2367    2.3009    6.5000    4.7354    6.5000
      3.3269    4.6776    2.6447    1.7592    0.4018    9.3928
      6.5000    3.8850    4.3025    9.9952    6.5000    6.5000
      1.5149    8.8366    3.5287    9.6791    2.3067    2.0306
   
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
   
