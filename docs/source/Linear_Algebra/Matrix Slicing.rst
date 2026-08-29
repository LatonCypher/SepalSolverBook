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
      0.0725    0.9739    0.7154    0.4064
   
   R1[2] = 0.7153720808733416
   C1 = 
      0.9126
      0.3541
      0.5094
      0.6992
      0.6128
      0.1355
      0.5531
      0.6012
   
   C1[5] = 0.13548633594977244

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
      0.1445    0.2543    0.1940    0.5110    0.5604
      0.1154    0.1179    0.6403    0.1771    0.6093
   

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
   
      0.7418    0.1807    0.6539    0.3467    0.9662    0.8362    0.9857    0.1297
      0.4058    0.7683    0.9699    0.4192    0.0923    0.9048    0.9868    0.8950
      0.4599    0.0952    0.0785    0.9934    0.1073    0.9536    0.6814    0.7141
      0.0180    0.3889    0.6373    0.2170    0.9324    0.5626    0.4987    0.7543
      0.5829    0.8270    0.0355    0.4587    0.8508    0.6612    0.4678    0.4623
      0.9356    0.2156    0.4917    0.4930    0.2693    0.6913    0.6560    0.0267
      0.3174    0.6315    0.8713    0.7186    0.5917    0.0456    0.9567    0.5340
      0.7587    0.2086    0.4158    0.8714    0.4940    0.6564    0.4172    0.0066
   
   B = 
   
      0.5722    0.7471    0.0752    0.9817    0.9199    0.2575    0.2655    0.0801
      0.8658    0.2946    0.4198    0.9747    0.7835    0.1492    0.8566    0.5298
      0.1449    0.2003    0.4418    0.5166    0.3360    0.9341    0.5169    0.8240
      0.8545    0.1719    0.1050    0.6715    0.9551    0.6180    0.1760    0.4005
      0.2197    0.7532    0.6094    0.2606    0.2746    0.6527    0.8902    0.4851
      0.1955    0.7883    0.5657    0.2908    0.0133    0.9033    0.7528    0.4435
      0.5420    0.1949    0.4342    0.0852    0.5979    0.5399    0.3881    0.8063
      0.6287    0.6395    0.6008    0.5741    0.5015    0.9637    0.6864    0.1159
   
   C = 
   
      1.9634    2.4599    2.0247    2.1284    2.3057    3.0863    2.7119    2.4822
      2.6906    2.3433    2.3598    2.8148    2.7777    3.6572    3.1016    2.7520
      2.2340    1.9801    1.5433    2.0253    2.2806    2.8076    1.9871    1.6570
      1.6840    2.0183    2.0250    1.7537    1.6826    2.9050    2.6703    2.0108
      2.3069    2.3140    1.8282    2.4239    2.3881    2.4408    2.7162    1.8345
      1.7810    1.8384    1.2859    2.0561    2.1544    2.2172    1.8071    1.7610
      2.4617    1.7307    1.8720    2.4153    2.7687    2.8925    2.5009    2.5063
      1.8866    1.8364    1.2773    2.1071    2.2305    2.3005    1.8487    1.7308
   
   D = 
   
      1.9634    2.4599    2.0247    2.1284    2.3057    3.0863    2.7119    2.4822
      2.6906    2.3433    2.3598    2.8148    2.7777    3.6572    3.1016    2.7520
      2.2340    1.9801    1.5433    2.0253    2.2806    2.8076    1.9871    1.6570
      1.6840    2.0183    2.0250    1.7537    1.6826    2.9050    2.6703    2.0108
      2.3069    2.3140    1.8282    2.4239    2.3881    2.4408    2.7162    1.8345
      1.7810    1.8384    1.2859    2.0561    2.1544    2.2172    1.8071    1.7610
      2.4617    1.7307    1.8720    2.4153    2.7687    2.8925    2.5009    2.5063
      1.8866    1.8364    1.2773    2.1071    2.2305    2.3005    1.8487    1.7308
   


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

   
      0.6979    0.0051    0.4229    0.6518    0.9566    0.0817
      0.2353    0.7583    0.4207    0.3514    0.1240    0.9405
      0.3210    0.0158    0.0373    0.7010    0.1738    0.1056
      0.8149    0.7076    0.7617    0.2220    0.4255    0.3479
      0.1297    0.7759    0.2493    0.2851    0.3188    0.6757
   
   
      0.6979
      0.8149
      0.7583
      0.7076
      0.7759
      0.7617
      0.6518
      0.7010
      0.9566
      0.9405
      0.6757
   

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

   
      7.3070    7.5636    8.9266    5.3350    7.3932    1.4719
      4.8035    9.8025    0.4963    7.2294    7.2654    9.5252
      4.9189    8.3283    0.9157    9.5397    3.5355    5.3487
      4.3164    3.0581    9.6378    1.2521    3.9044    0.5285
      7.0354    8.3435    2.4663    0.8673    3.6245    7.5867
   
   
      7.3070    7.5636    8.9266    5.3350    7.3932    0.0000
      0.0000    9.8025    0.0000    7.2294    7.2654    9.5252
      0.0000    8.3283    0.0000    9.5397    0.0000    5.3487
      0.0000    0.0000    9.6378    0.0000    0.0000    0.0000
      7.0354    8.3435    0.0000    0.0000    0.0000    7.5867
   
   
      7.3070    7.5636    8.9266    5.3350    7.3932    0.0000
      0.0000       NaN    0.0000    7.2294    7.2654       NaN
      0.0000    8.3283    0.0000       NaN    0.0000    5.3487
      0.0000    0.0000       NaN    0.0000    0.0000    0.0000
      7.0354    8.3435    0.0000    0.0000    0.0000    7.5867
   

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

   
      4.2707    9.6650    3.3779    1.2597    4.9240    6.5000
      3.9018    4.1653    4.0735    2.1641    6.5000    1.2832
      1.3315    9.1070    9.1976    1.1353    0.4782    3.2892
      9.4484    8.8998    3.5491    6.5000    0.3167    6.5000
      6.5000    4.6170    6.5000    0.9768    6.5000    6.5000
   
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
   
