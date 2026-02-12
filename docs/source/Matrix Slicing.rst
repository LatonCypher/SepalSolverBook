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
      0.3596    0.9546    0.3054    0.0636
   
   R1[2] = 0.30535517145671687
   C1 = 
      0.2846
      0.9347
      0.6894
      0.7140
      0.0712
      0.4397
      0.6890
      0.9869
   
   C1[5] = 0.43972358439149684

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
      0.3271    0.7220    0.7911    0.4355    0.3343
      0.7527    0.9108    0.2505    0.2405    0.0083
   

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
   
      0.5122    0.1425    0.6885    0.1207    0.4547    0.5732    0.3687    0.2755
      0.0966    0.0557    0.3264    0.1104    0.7404    0.4173    0.7664    0.7605
      0.5166    0.3320    0.2127    0.2103    0.3824    0.2773    0.4558    0.0783
      0.6342    0.8945    0.1748    0.7600    0.9449    0.4213    0.6966    0.0015
      0.7119    0.7449    0.1677    0.9186    0.5244    0.3039    0.9781    0.7684
      0.4682    0.4332    0.2883    0.2879    0.0527    0.9785    0.7166    0.7637
      0.5739    0.7929    0.0783    0.2075    0.1094    0.7041    0.8169    0.2108
      0.1891    0.8112    0.9305    0.4219    0.1222    0.2353    0.4674    0.5604
   
   B = 
   
      0.5487    0.8609    0.1827    0.2328    0.6844    0.1252    0.6177    0.7922
      0.0485    0.1190    0.2943    0.3131    0.7716    0.5510    0.1191    0.9503
      0.5502    0.4827    0.3427    0.7752    0.3130    0.7634    0.6848    0.6196
      0.1809    0.7679    0.3081    0.5601    0.3608    0.7436    0.3520    0.7990
      0.5174    0.3001    0.2770    0.9002    0.7160    0.9874    0.5754    0.4911
      0.0248    0.5364    0.8385    0.4376    0.8178    0.6670    0.3152    0.5548
      0.5635    0.4196    0.8049    0.3467    0.1711    0.5766    0.8394    0.5429
      0.4173    0.4458    0.2752    0.3693    0.5918    0.4497    0.7053    0.2434
   
   C = 
   
      1.2609    1.6044    1.3877    1.6549    1.7400    1.9258    1.7935    1.8728
      1.3979    1.4387    1.5609    1.7505    1.7036    2.1672    2.0659    1.6162
      0.9488    1.2380    1.0566    1.1594    1.3771    1.4268    1.3235    1.6327
      1.5175    2.1228    1.8491    2.2658    2.5943    2.8870    2.1476    3.1444
      1.8358    2.5612    2.0884    2.2713    2.6920    2.9405    2.7270    3.2539
      1.2626    1.9966    2.0224    1.6355    2.2613    2.1927    2.1184    2.3349
      1.0563    1.6328    1.7651    1.3264    2.0226    1.8662    1.6948    2.3617
      1.2976    1.6412    1.4836    1.8376    1.8903    2.2937    1.9311    2.4150
   
   D = 
   
      1.2609    1.6044    1.3877    1.6549    1.7400    1.9258    1.7935    1.8728
      1.3979    1.4387    1.5609    1.7505    1.7036    2.1672    2.0659    1.6162
      0.9488    1.2380    1.0566    1.1594    1.3771    1.4268    1.3235    1.6327
      1.5175    2.1228    1.8491    2.2658    2.5943    2.8870    2.1476    3.1444
      1.8358    2.5612    2.0884    2.2713    2.6920    2.9405    2.7270    3.2539
      1.2626    1.9966    2.0224    1.6355    2.2613    2.1927    2.1184    2.3349
      1.0563    1.6328    1.7651    1.3264    2.0226    1.8662    1.6948    2.3617
      1.2976    1.6412    1.4836    1.8376    1.8903    2.2937    1.9311    2.4150
   


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

   
      0.1354    0.3266    0.8401    0.5922    0.9776    0.3645
      0.5698    0.8337    0.5698    0.4756    0.1572    0.6468
      0.4057    0.1902    0.3854    0.2667    0.4260    0.5167
      0.9652    0.3960    0.5048    0.7975    0.5965    0.6794
      0.7454    0.2636    0.5013    0.0095    0.3036    0.3210
   
   
      0.5698
      0.9652
      0.7454
      0.8337
      0.8401
      0.5698
      0.5048
      0.5013
      0.5922
      0.7975
      0.9776
      0.5965
      0.6468
      0.5167
      0.6794
   

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

   
      8.2326    4.9289    3.8341    1.0055    7.3038    8.7591
      4.8069    4.5253    2.8274    4.6074    7.2823    5.6105
      1.3494    8.4297    1.7137    4.5434    2.6040    6.5623
      3.2759    0.3380    7.8264    6.7650    3.5310    4.0581
      0.8692    9.4988    2.9461    9.4191    5.1430    8.8580
   
   
      8.2326    0.0000    0.0000    0.0000    7.3038    8.7591
      0.0000    0.0000    0.0000    0.0000    7.2823    5.6105
      0.0000    8.4297    0.0000    0.0000    0.0000    6.5623
      0.0000    0.0000    7.8264    6.7650    0.0000    0.0000
      0.0000    9.4988    0.0000    9.4191    5.1430    8.8580
   
   
      8.2326    0.0000    0.0000    0.0000    7.3038    8.7591
      0.0000    0.0000    0.0000    0.0000    7.2823    5.6105
      0.0000    8.4297    0.0000    0.0000    0.0000    6.5623
      0.0000    0.0000    7.8264    6.7650    0.0000    0.0000
      0.0000       NaN    0.0000       NaN    5.1430    8.8580
   

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

   
      9.3516    9.5342    4.4582    9.6698    1.1020    4.7464
      6.5000    6.5000    4.0740    3.0046    6.5000    1.4867
      0.8221    6.5000    6.5000    6.5000    8.1109    6.5000
      3.4827    1.0198    3.9091    2.2274    6.5000    9.8806
      9.1915    6.5000    8.7277    4.3043    2.0584    6.5000
   
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
   
