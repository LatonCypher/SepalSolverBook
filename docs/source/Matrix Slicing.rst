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
      0.9715    0.5974    0.1762    0.8489
   
   R1[2] = 0.17615244726398138
   C1 = 
      0.0650
      0.7330
      0.7209
      0.8721
      0.2223
      0.7370
      0.9251
      0.9255
   
   C1[5] = 0.7369999638960296

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
      0.8450    0.9305    0.1826    0.2191    0.4080
      0.4393    0.1282    0.1376    0.2398    0.4176
   

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
   
      0.3956    0.6723    0.6998    0.4773    0.2478    0.5516    0.0258    0.6517
      0.6693    0.5965    0.0805    0.4390    0.2958    0.1403    0.6397    0.1711
      0.5316    0.8456    0.4791    0.3964    0.0193    0.1381    0.9992    0.8531
      0.6924    0.4833    0.5290    0.0963    0.3658    0.2070    0.5064    0.6590
      0.2711    0.4889    0.4097    0.7576    0.5792    0.1167    0.9604    0.2146
      0.0533    0.9986    0.8152    0.4464    0.8940    0.4344    0.4898    0.4388
      0.7521    0.9829    0.0017    0.7245    0.6055    0.2796    0.2210    0.3047
      0.4701    0.6472    0.1926    0.4081    0.5101    0.9821    0.1753    0.6519
   
   B = 
   
      0.8987    0.7733    0.4619    0.5766    0.1172    0.0460    0.7621    0.6326
      0.4269    0.4154    0.8557    0.1921    0.4702    0.7564    0.0351    0.2856
      0.3230    0.8365    0.4899    0.4959    0.6830    0.8689    0.3885    0.4450
      0.9936    0.4628    0.8965    0.3850    0.2539    0.5834    0.7606    0.4751
      0.0258    0.9081    0.7700    0.6742    0.0568    0.6167    0.5450    0.3053
      0.2044    0.2875    0.0890    0.8254    0.0554    0.5579    0.5056    0.3534
      0.7175    0.8835    0.1342    0.4468    0.3962    0.3519    0.7684    0.6874
      0.6664    0.0922    0.5744    0.9465    0.9341    0.0992    0.3190    0.2506
   
   C = 
   
      1.9147    1.8581    2.1464    2.1388    1.6253    1.9476    1.6016    1.4321
      1.9276    1.9258    1.6769    1.4724    0.9632    1.3108    1.6744    1.4606
      2.7015    2.3653    2.2105    2.2400    2.0893    1.8368    2.0426    1.9346
      1.9494    2.1232    1.8254    2.0589    1.5426    1.4980    1.7267    1.5553
      2.2083    2.5339    2.1320    1.8641    1.3542    1.9617    2.1402    1.7854
      1.9366    2.7545    2.7234    2.3940    1.8247    2.7361    1.9550    1.7668
      2.2504    2.1803    2.5346    1.9284    1.1575    1.8397    1.8977    1.6134
      1.9407    1.9431    2.1094    2.4980    1.3564    1.9056    1.8834    1.5486
   
   D = 
   
      1.9147    1.8581    2.1464    2.1388    1.6253    1.9476    1.6016    1.4321
      1.9276    1.9258    1.6769    1.4724    0.9632    1.3108    1.6744    1.4606
      2.7015    2.3653    2.2105    2.2400    2.0893    1.8368    2.0426    1.9346
      1.9494    2.1232    1.8254    2.0589    1.5426    1.4980    1.7267    1.5553
      2.2083    2.5339    2.1320    1.8641    1.3542    1.9617    2.1402    1.7854
      1.9366    2.7545    2.7234    2.3940    1.8247    2.7361    1.9550    1.7668
      2.2504    2.1803    2.5346    1.9284    1.1575    1.8397    1.8977    1.6134
      1.9407    1.9431    2.1094    2.4980    1.3564    1.9056    1.8834    1.5486
   


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

   
      0.3964    0.1850    0.1134    0.9924    0.5884    0.9719
      0.6203    0.6259    0.7235    0.6290    0.7175    0.3639
      0.2686    0.8544    0.3761    0.7676    0.6432    0.4722
      0.3439    0.7961    0.3101    0.0094    0.1050    0.8028
      0.4541    0.6389    0.0481    0.7723    0.9136    0.9784
   
   
      0.6203
      0.6259
      0.8544
      0.7961
      0.6389
      0.7235
      0.9924
      0.6290
      0.7676
      0.7723
      0.5884
      0.7175
      0.6432
      0.9136
      0.9719
      0.8028
      0.9784
   

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

   
      0.0808    2.1624    8.0890    5.7717    2.2339    8.3697
      4.8778    8.7707    7.9215    7.3754    0.2919    6.1935
      6.0711    2.6904    3.0525    5.1931    4.3531    7.8762
      1.4293    4.5668    4.8261    3.4919    8.2616    9.8066
      8.6063    1.2251    7.4090    4.1656    0.6379    4.9129
   
   
      0.0000    0.0000    8.0890    5.7717    0.0000    8.3697
      0.0000    8.7707    7.9215    7.3754    0.0000    6.1935
      6.0711    0.0000    0.0000    5.1931    0.0000    7.8762
      0.0000    0.0000    0.0000    0.0000    8.2616    9.8066
      8.6063    0.0000    7.4090    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    8.0890    5.7717    0.0000    8.3697
      0.0000    8.7707    7.9215    7.3754    0.0000    6.1935
      6.0711    0.0000    0.0000    5.1931    0.0000    7.8762
      0.0000    0.0000    0.0000    0.0000    8.2616       NaN
      8.6063    0.0000    7.4090    0.0000    0.0000    0.0000
   

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

   
      4.5194    3.3467    3.7339    8.1214    6.5000    1.9187
      6.5000    2.0057    0.5503    1.5774    6.5000    4.5928
      2.9018    6.5000    8.2077    3.2753    6.5000    9.6302
      0.0950    3.0713    6.5000    4.8300    1.5773    2.1436
      6.5000    6.5000    6.5000    3.6460    6.5000    1.8140
   
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
   
