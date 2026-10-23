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
      0.7971    0.2973    0.8667    0.1262
   
   R1[2] = 0.8667049174648936
   C1 = 
      0.3088
      0.1286
      0.3315
      0.3323
      0.2788
      0.8614
      0.3354
      0.6228
   
   C1[5] = 0.8613749730199741

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
      0.0454    0.5879    0.7376    0.9618    0.2245
      0.1415    0.5039    0.7995    0.1343    0.9255
   

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
   
      0.4018    0.7633    0.8223    0.1024    0.0381    0.4910    0.8449    0.1569
      0.5767    0.1980    0.8190    0.8291    0.0819    0.3812    0.1253    0.6072
      0.2318    0.4162    0.3853    0.8912    0.3814    0.9803    0.1524    0.6843
      0.1158    0.8790    0.1831    0.9502    0.2521    0.9746    0.1868    0.0702
      0.2285    0.2944    0.0931    0.1553    0.7237    0.1099    0.3328    0.3698
      0.0073    0.6295    0.5876    0.4685    0.4715    0.4242    0.6300    0.5758
      0.6550    0.4635    0.4588    0.8903    0.8110    0.2489    0.4613    0.9295
      0.9583    0.9764    0.9106    0.6521    0.3550    0.6692    0.4643    0.3699
   
   B = 
   
      0.9982    0.1822    0.5940    0.6362    0.2483    0.1988    0.2248    0.0148
      0.9604    0.9009    0.0010    0.5895    0.3663    0.1137    0.4961    0.4875
      0.5700    0.7567    0.3791    0.5400    0.0652    0.0085    0.7549    0.6305
      0.2183    0.1404    0.0890    0.7920    0.8925    0.4708    0.9483    0.0961
      0.3294    0.3875    0.0564    0.7687    0.4854    0.1295    0.8094    0.1553
      0.1281    0.6837    0.2168    0.1499    0.0331    0.4575    0.6459    0.6818
      0.6941    0.7889    0.7268    0.9698    0.8928    0.9965    0.7777    0.0344
      0.4077    0.8911    0.2938    0.6125    0.7081    0.2283    0.6586    0.5341
   
   C = 
   
      2.3512    2.5543    1.3291    2.2492    1.4247    1.3292    2.2954    1.3598
      1.8240    1.9518    1.0838    2.1961    1.6034    0.9830    2.4423    1.3022
      1.6813    2.3819    0.9093    2.3138    1.8688    1.3222    2.9058    1.6332
      1.6377    2.0589    0.6055    2.0073    1.5819    1.2525    2.5265    1.3844
      1.2321    1.3467    0.6003    1.6142    1.2232    0.7129    1.5741    0.6167
      1.9308    2.5616    1.0150    2.4538    1.9021    1.3130    2.7267    1.4140
      2.5531    2.6857    1.3508    3.3203    2.6290    1.4968    3.3560    1.4183
      3.2314    3.1258    1.5846    3.2435    2.1080    1.5155    3.3300    1.8518
   
   D = 
   
      2.3512    2.5543    1.3291    2.2492    1.4247    1.3292    2.2954    1.3598
      1.8240    1.9518    1.0838    2.1961    1.6034    0.9830    2.4423    1.3022
      1.6813    2.3819    0.9093    2.3138    1.8688    1.3222    2.9058    1.6332
      1.6377    2.0589    0.6055    2.0073    1.5819    1.2525    2.5265    1.3844
      1.2321    1.3467    0.6003    1.6142    1.2232    0.7129    1.5741    0.6167
      1.9308    2.5616    1.0150    2.4538    1.9021    1.3130    2.7267    1.4140
      2.5531    2.6857    1.3508    3.3203    2.6290    1.4968    3.3560    1.4183
      3.2314    3.1258    1.5846    3.2435    2.1080    1.5155    3.3300    1.8518
   


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

   
      0.1869    0.8956    0.7977    0.1405    0.0718    0.9706
      0.6239    0.7487    0.5246    0.3060    0.7228    0.0278
      0.8246    0.8661    0.8411    0.9069    0.7511    0.3446
      0.5558    0.8118    0.4337    0.0813    0.7620    0.1269
      0.8654    0.5309    0.3858    0.9152    0.1727    0.2735
   
   
      0.6239
      0.8246
      0.5558
      0.8654
      0.8956
      0.7487
      0.8661
      0.8118
      0.5309
      0.7977
      0.5246
      0.8411
      0.9069
      0.9152
      0.7228
      0.7511
      0.7620
      0.9706
   

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

   
      2.4172    0.1390    1.3621    0.8637    1.3811    3.1916
      3.5478    9.2176    5.6531    3.8845    9.4286    1.4900
      9.1920    9.6366    6.0161    3.8032    2.6130    0.0840
      0.5996    1.5660    0.8355    9.3197    8.3700    8.3543
      0.1178    9.9044    1.4962    0.4809    4.4653    3.5655
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    9.2176    5.6531    0.0000    9.4286    0.0000
      9.1920    9.6366    6.0161    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    9.3197    8.3700    8.3543
      0.0000    9.9044    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    5.6531    0.0000       NaN    0.0000
         NaN       NaN    6.0161    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000       NaN    8.3700    8.3543
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
   

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

   
      9.4814    8.2431    6.5000    1.9104    9.3839    0.6909
      9.0122    6.5000    8.1423    9.2476    2.7932    6.5000
      6.5000    2.8468    1.9771    8.4340    6.5000    1.9735
      2.8353    0.3197    8.7923    8.7230    9.2090    0.1147
      2.2911    4.6723    1.3668    6.5000    6.5000    9.9997
   
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
   
