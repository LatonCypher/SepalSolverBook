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
      0.8890    0.6097    0.6545    0.7093
   
   R1[2] = 0.6544647529428257
   C1 = 
      0.1312
      0.4163
      0.5894
      0.4601
      0.7889
      0.9894
      0.9106
      0.0524
   
   C1[5] = 0.9894266534946603

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
      0.9979    0.3488    0.8235    0.2756    0.0338
      0.1093    0.2060    0.0527    0.8559    0.7922
   

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
   
      0.9088    0.1423    0.9076    0.8916    0.9641    0.7591    0.9523    0.7449
      0.0570    0.2463    0.9437    0.9854    0.8918    0.0859    0.8095    0.8280
      0.5050    0.3388    0.8173    0.9675    0.3387    0.1416    0.0710    0.2940
      0.5380    0.7908    0.1167    0.3866    0.7837    0.9506    0.0774    0.2856
      0.9439    0.6488    0.5005    0.9342    0.0438    0.2064    0.9838    0.7150
      0.8852    0.4240    0.9880    0.7513    0.8462    0.8452    0.8416    0.6088
      0.3098    0.1630    0.7421    0.8753    0.7128    0.7169    0.4520    0.8017
      0.7499    0.1446    0.7525    0.4424    0.7672    0.9836    0.5720    0.8345
   
   B = 
   
      0.0135    0.4667    0.5751    0.1524    0.3329    0.7679    0.3952    0.5396
      0.4446    0.3954    0.1868    0.2228    0.7200    0.7893    0.3039    0.1031
      0.3629    0.5393    0.2692    0.3185    0.3881    0.5368    0.2550    0.2272
      0.4238    0.9531    0.7231    0.1671    0.9421    0.2777    0.4169    0.0919
      0.7310    0.7498    0.9814    0.5952    0.9208    0.7213    0.0798    0.7646
      0.1508    0.0034    0.0935    0.5882    0.0528    0.1108    0.1567    0.3158
      0.0368    0.0666    0.2304    0.3030    0.5301    0.6258    0.3106    0.0241
      0.0365    0.2035    0.0595    0.0199    0.1298    0.6502    0.6925    0.5120
   
   C = 
   
      1.6642    2.7602    2.7193    1.9320    3.1266    3.4048    2.0130    2.1744
      1.5952    2.4635    2.1645    1.3718    2.8532    2.7160    1.6582    1.5135
      1.1463    2.0516    1.6530    0.8867    2.0360    1.8583    1.1892    1.0380
      1.2945    1.6494    1.6610    1.4147    2.0080    2.1122    1.0773    1.4814
      1.0043    2.1021    1.8060    1.0639    2.5214    2.9001    1.9239    1.2642
      1.6766    2.6470    2.5372    1.9375    3.0402    3.3800    1.9268    2.0608
      1.3920    2.1737    1.9599    1.4650    2.3710    2.4057    1.5909    1.6258
      1.2956    2.0212    2.0073    1.6853    2.2326    2.7798    1.6876    1.9695
   
   D = 
   
      1.6642    2.7602    2.7193    1.9320    3.1266    3.4048    2.0130    2.1744
      1.5952    2.4635    2.1645    1.3718    2.8532    2.7160    1.6582    1.5135
      1.1463    2.0516    1.6530    0.8867    2.0360    1.8583    1.1892    1.0380
      1.2945    1.6494    1.6610    1.4147    2.0080    2.1122    1.0773    1.4814
      1.0043    2.1021    1.8060    1.0639    2.5214    2.9001    1.9239    1.2642
      1.6766    2.6470    2.5372    1.9375    3.0402    3.3800    1.9268    2.0608
      1.3920    2.1737    1.9599    1.4650    2.3710    2.4057    1.5909    1.6258
      1.2956    2.0212    2.0073    1.6853    2.2326    2.7798    1.6876    1.9695
   


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

   
      0.9120    0.5533    0.3816    0.8434    0.5309    0.9799
      0.7264    0.0057    0.3057    0.4536    0.6413    0.5425
      0.2474    0.6041    0.2392    0.8984    0.3930    0.0907
      0.4811    0.0517    0.9576    0.4875    0.0756    0.8579
      0.0433    0.9815    0.2723    0.6579    0.9931    0.3028
   
   
      0.9120
      0.7264
      0.5533
      0.6041
      0.9815
      0.9576
      0.8434
      0.8984
      0.6579
      0.5309
      0.6413
      0.9931
      0.9799
      0.5425
      0.8579
   

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

   
      2.3269    2.9807    0.8413    0.2554    6.3420    6.9061
      2.1183    7.9284    9.7828    9.6881    7.3177    0.2514
      9.8359    2.1316    8.6038    4.5236    4.6512    5.0777
      3.5850    8.8214    1.2601    9.2786    3.3398    6.8939
      6.7854    8.3508    7.0425    7.3297    1.5787    1.0109
   
   
      0.0000    0.0000    0.0000    0.0000    6.3420    6.9061
      0.0000    7.9284    9.7828    9.6881    7.3177    0.0000
      9.8359    0.0000    8.6038    0.0000    0.0000    5.0777
      0.0000    8.8214    0.0000    9.2786    0.0000    6.8939
      6.7854    8.3508    7.0425    7.3297    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    6.3420    6.9061
      0.0000    7.9284       NaN       NaN    7.3177    0.0000
         NaN    0.0000    8.6038    0.0000    0.0000    5.0777
      0.0000    8.8214    0.0000       NaN    0.0000    6.8939
      6.7854    8.3508    7.0425    7.3297    0.0000    0.0000
   

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

   
      4.9428    6.5000    2.1005    2.4381    1.9368    0.6742
      1.1186    4.4555    6.5000    6.5000    2.7195    6.5000
      6.5000    2.4777    6.5000    6.5000    1.0423    1.3382
      6.5000    8.2026    0.1444    4.9718    4.7640    0.1082
      0.2735    9.9029    6.5000    2.1708    0.8178    8.1997
   
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
   
