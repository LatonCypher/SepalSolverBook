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
      0.3559    0.1017    0.4315    0.2897
   
   R1[2] = 0.43153858404198886
   C1 = 
      0.7895
      0.5347
      0.2524
      0.9103
      0.1536
      0.2782
      0.2333
      0.7669
   
   C1[5] = 0.27817535549751327

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
      0.2295    0.3979    0.7998    0.0905    0.5769
      0.3363    0.5868    0.8352    0.1791    0.3105
   

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
   
      0.6775    0.5508    0.3701    0.8998    0.2011    0.0700    0.4262    0.3105
      0.0938    0.8549    0.4016    0.0099    0.7146    0.8300    0.7204    0.3214
      0.4304    0.2454    0.5184    0.9207    0.7254    0.7913    0.6569    0.3751
      0.4577    0.5056    0.7066    0.4698    0.9420    0.7112    0.6719    0.4701
      0.0435    0.8861    0.8842    0.9271    0.1367    0.9646    0.6513    0.8261
      0.5764    0.0715    0.6099    0.6379    0.3835    0.0639    0.8055    0.4118
      0.2138    0.2787    0.9005    0.8842    0.9614    0.1630    0.6815    0.7294
      0.4106    0.5633    0.2227    0.5132    0.9010    0.1075    0.8836    0.2851
   
   B = 
   
      0.7334    0.2254    0.5735    0.9263    0.8848    0.2779    0.2702    0.2032
      0.7508    0.3618    0.9434    0.2416    0.3692    0.5699    0.2711    0.4450
      0.3268    0.7520    0.6113    0.6466    0.7899    0.6742    0.4966    0.0649
      0.8883    0.3942    0.3554    0.1726    0.7692    0.6210    0.0019    0.8114
      0.1026    0.3387    0.4622    0.4490    0.4565    0.5143    0.7960    0.5012
      0.5039    0.9351    0.6302    0.1018    0.0804    0.0798    0.7361    0.7948
      0.3765    0.6221    0.2868    0.8436    0.3866    0.6702    0.2182    0.1346
      0.5286    0.3075    0.3478    0.1036    0.9558    0.5951    0.6500    0.8520
   
   C = 
   
      2.2111    1.4792    1.8215    1.6444    2.3462    1.8899    1.0243    1.6153
      1.7834    2.2016    2.2810    1.6012    1.7021    1.8981    2.0024    1.8222
      2.4059    2.4482    2.2753    1.9514    2.5963    2.2805    1.9890    2.3779
      2.3199    2.5492    2.5781    2.1950    2.7074    2.4548    2.3380    2.2734
      2.9915    2.9683    2.8759    1.7806    2.9584    2.7643    2.1907    2.8395
      1.8349    1.6832    1.5893    1.9565    2.3941    1.9955    1.2749    1.4085
      2.2687    2.3010    2.2465    2.0991    3.0961    2.7728    2.0903    2.2680
      1.8827    1.7091    1.9222    1.9394    2.1759    2.1378    1.5496    1.6638
   
   D = 
   
      2.2111    1.4792    1.8215    1.6444    2.3462    1.8899    1.0243    1.6153
      1.7834    2.2016    2.2810    1.6012    1.7021    1.8981    2.0024    1.8222
      2.4059    2.4482    2.2753    1.9514    2.5963    2.2805    1.9890    2.3779
      2.3199    2.5492    2.5781    2.1950    2.7074    2.4548    2.3380    2.2734
      2.9915    2.9683    2.8759    1.7806    2.9584    2.7643    2.1907    2.8395
      1.8349    1.6832    1.5893    1.9565    2.3941    1.9955    1.2749    1.4085
      2.2687    2.3010    2.2465    2.0991    3.0961    2.7728    2.0903    2.2680
      1.8827    1.7091    1.9222    1.9394    2.1759    2.1378    1.5496    1.6638
   


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

   
      0.1258    0.1646    0.7054    0.5074    0.7877    0.5696
      0.1809    0.8586    0.9420    0.6758    0.6693    0.5126
      0.2945    0.9956    0.4546    0.3775    0.4542    0.5971
      0.7321    0.5053    0.6221    0.7602    0.5488    0.4918
      0.6600    0.1851    0.7370    0.8168    0.0306    0.8388
   
   
      0.7321
      0.6600
      0.8586
      0.9956
      0.5053
      0.7054
      0.9420
      0.6221
      0.7370
      0.5074
      0.6758
      0.7602
      0.8168
      0.7877
      0.6693
      0.5488
      0.5696
      0.5126
      0.5971
      0.8388
   

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

   
      2.5711    9.5375    0.3730    9.9309    3.2502    0.0245
      4.9054    1.3629    9.0705    6.3296    8.7314    8.9833
      8.7906    5.1701    0.4429    6.2591    0.4686    3.2461
      1.0690    7.0904    0.1556    4.7037    5.4852    3.6752
      0.4566    1.8582    5.3464    1.0724    3.0126    3.4240
   
   
      0.0000    9.5375    0.0000    9.9309    0.0000    0.0000
      0.0000    0.0000    9.0705    6.3296    8.7314    8.9833
      8.7906    5.1701    0.0000    6.2591    0.0000    0.0000
      0.0000    7.0904    0.0000    0.0000    5.4852    0.0000
      0.0000    0.0000    5.3464    0.0000    0.0000    0.0000
   
   
      0.0000       NaN    0.0000       NaN    0.0000    0.0000
      0.0000    0.0000       NaN    6.3296    8.7314    8.9833
      8.7906    5.1701    0.0000    6.2591    0.0000    0.0000
      0.0000    7.0904    0.0000    0.0000    5.4852    0.0000
      0.0000    0.0000    5.3464    0.0000    0.0000    0.0000
   

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

   
      3.9604    3.2651    6.5000    6.5000    0.3272    4.3235
      1.6176    8.3390    3.4382    0.5700    3.7397    6.5000
      6.5000    6.5000    8.2949    0.2065    2.4396    0.4028
      9.3946    3.3444    9.2303    6.5000    8.9923    6.5000
      1.4819    9.5813    6.5000    8.3147    6.5000    2.7571
   
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
   
