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
      0.4173    0.9742    0.4909    0.8340
   
   R1[2] = 0.49087184544899154
   C1 = 
      0.9047
      0.2645
      0.3233
      0.5523
      0.5461
      0.2990
      0.0526
      0.5385
   
   C1[5] = 0.29901272234395626

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
      0.3728    0.8982    0.0499    0.8520    0.8101
      0.3856    0.6909    0.3148    0.1217    0.8433
   

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
   
      0.4984    0.9135    0.8822    0.8320    0.5382    0.3807    0.3642    0.5589
      0.4428    0.7748    0.3260    0.4910    0.7089    0.0086    0.6513    0.4130
      0.5617    0.9865    0.6662    0.3179    0.5008    0.3380    0.3177    0.1244
      0.3014    0.9474    0.4445    0.6432    0.9691    0.3602    0.5210    0.6779
      0.4985    0.3791    0.0674    0.7653    0.2691    0.4803    0.9165    0.5053
      0.3041    0.8640    0.5546    0.0338    0.3092    0.5071    0.1740    0.1234
      0.7108    0.4412    0.2373    0.2255    0.9641    0.8011    0.8658    0.8904
      0.0590    0.5025    0.4160    0.9534    0.8393    0.8044    0.8398    0.0505
   
   B = 
   
      0.7623    0.1827    0.9772    0.9893    0.4438    0.7771    0.9232    0.5754
      0.0786    0.4439    0.2785    0.0376    0.1243    0.1419    0.0475    0.9525
      0.1143    0.9082    0.3354    0.5749    0.1583    0.3723    0.2353    0.4553
      0.7785    0.8384    0.1777    0.8692    0.5511    0.7550    0.5004    0.1607
      0.8939    0.0719    0.1109    0.6963    0.1960    0.5891    0.8672    0.6214
      0.4709    0.6762    0.3232    0.1646    0.5600    0.1416    0.9109    0.2217
      0.9044    0.6240    0.5091    0.9609    0.0438    0.8394    0.0595    0.3108
      0.1354    0.8485    0.1300    0.6573    0.5389    0.5707    0.6932    0.8920
   
   C = 
   
      2.2658    2.9930    1.6260    2.9126    1.5687    2.4692    2.3501    2.7229
      2.1006    1.9462    1.3118    2.4738    1.0098    2.1474    1.7156    2.2333
      1.7404    1.9804    1.4462    2.0434    1.0209    1.7450    1.7285    2.2130
      2.4547    2.6321    1.3991    2.8288    1.4561    2.4659    2.4191    2.8303
      2.3773    2.3070    1.4686    2.6906    1.3349    2.3282    1.9527    1.8108
      1.0787    1.5496    1.0326    1.2286    0.7675    1.0613    1.2951    1.7246
      2.9219    2.6371    1.8596    3.2727    1.6875    2.7900    3.0805    2.8136
      2.7696    2.5821    1.2937    2.7021    1.3589    2.3338    2.1989    1.8611
   
   D = 
   
      2.2658    2.9930    1.6260    2.9126    1.5687    2.4692    2.3501    2.7229
      2.1006    1.9462    1.3118    2.4738    1.0098    2.1474    1.7156    2.2333
      1.7404    1.9804    1.4462    2.0434    1.0209    1.7450    1.7285    2.2130
      2.4547    2.6321    1.3991    2.8288    1.4561    2.4659    2.4191    2.8303
      2.3773    2.3070    1.4686    2.6906    1.3349    2.3282    1.9527    1.8108
      1.0787    1.5496    1.0326    1.2286    0.7675    1.0613    1.2951    1.7246
      2.9219    2.6371    1.8596    3.2727    1.6875    2.7900    3.0805    2.8136
      2.7696    2.5821    1.2937    2.7021    1.3589    2.3338    2.1989    1.8611
   


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

   
      0.9829    0.1911    0.3025    0.8067    0.9086    0.1037
      0.5012    0.2860    0.7246    0.9106    0.3299    0.5467
      0.6856    0.7589    0.6135    0.1724    0.6868    0.5967
      0.6849    0.0977    0.9274    0.7059    0.3571    0.4005
      0.4382    0.4448    0.9085    0.6400    0.2607    0.5902
   
   
      0.9829
      0.5012
      0.6856
      0.6849
      0.7589
      0.7246
      0.6135
      0.9274
      0.9085
      0.8067
      0.9106
      0.7059
      0.6400
      0.9086
      0.6868
      0.5467
      0.5967
      0.5902
   

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

   
      3.5317    3.3916    7.6469    4.1551    8.1718    5.0205
      0.9587    6.0008    9.6821    0.1031    9.8218    1.3619
      9.2281    4.4042    0.2980    9.1259    6.6814    0.0910
      4.7472    8.7376    8.4513    4.2055    6.7370    8.7428
      7.0741    8.5621    1.6847    1.9303    1.8592    7.0529
   
   
      0.0000    0.0000    7.6469    0.0000    8.1718    5.0205
      0.0000    6.0008    9.6821    0.0000    9.8218    0.0000
      9.2281    0.0000    0.0000    9.1259    6.6814    0.0000
      0.0000    8.7376    8.4513    0.0000    6.7370    8.7428
      7.0741    8.5621    0.0000    0.0000    0.0000    7.0529
   
   
      0.0000    0.0000    7.6469    0.0000    8.1718    5.0205
      0.0000    6.0008       NaN    0.0000       NaN    0.0000
         NaN    0.0000    0.0000       NaN    6.6814    0.0000
      0.0000    8.7376    8.4513    0.0000    6.7370    8.7428
      7.0741    8.5621    0.0000    0.0000    0.0000    7.0529
   

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

   
      0.6983    8.6533    9.8573    3.6122    2.6383    1.5891
      9.1453    3.5701    6.5000    4.5172    6.5000    1.5152
      2.0414    6.5000    1.7831    9.2566    9.0682    8.6222
      9.9110    6.5000    3.8238    3.3559    0.1831    6.5000
      9.9865    6.5000    6.5000    6.5000    6.5000    2.2643
   
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
   
