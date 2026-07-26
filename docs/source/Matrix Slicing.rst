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
      0.8415    0.3840    0.2322    0.9784
   
   R1[2] = 0.23216057039688665
   C1 = 
      0.5416
      0.3287
      0.1275
      0.4725
      0.5018
      0.2726
      0.4814
      0.2757
   
   C1[5] = 0.2725771444889563

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
      0.4917    0.8710    0.5596    0.4828    0.9919
      0.4538    0.1503    0.1859    0.7177    0.5106
   

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
   
      0.7831    0.9777    0.5112    0.1600    0.6609    0.5022    0.5867    0.7487
      0.3362    0.8044    0.2173    0.6624    0.8241    0.5834    0.3300    0.2811
      0.5165    0.6547    0.5484    0.6806    0.0073    0.7246    0.8919    0.7772
      0.3745    0.5918    0.0537    0.9504    0.6773    0.9182    0.9828    0.2782
      0.8313    0.3934    0.2179    0.0539    0.5389    0.2796    0.2850    0.0521
      0.3379    0.8311    0.5190    0.5086    0.0125    0.9601    0.8257    0.2967
      0.6438    0.1099    0.5810    0.7257    0.0525    0.0350    0.4901    0.9476
      0.0102    0.6028    0.5500    0.8716    0.9901    0.8168    0.1047    0.5003
   
   B = 
   
      0.1056    0.4194    0.1917    0.2954    0.0556    0.8661    0.4684    0.9259
      0.2888    0.2891    0.9389    0.6806    0.2052    0.3041    0.4699    0.9829
      0.3573    0.3599    0.9946    0.5057    0.1465    0.4779    0.6625    0.8708
      0.2383    0.7921    0.6704    0.9803    0.5319    0.4278    0.0890    0.1884
      0.1322    0.8752    0.6343    0.5672    0.1592    0.5560    0.3279    0.7520
      0.8954    0.5912    0.5122    0.2478    0.2899    0.9091    0.1840    0.9100
      0.6234    0.7742    0.3711    0.0589    0.7315    0.8520    0.1556    0.9724
      0.7623    0.1462    0.3827    0.5946    0.7907    0.4889    0.5652    0.0306
   
   C = 
   
      2.0593    2.3608    2.8645    2.2912    1.6762    2.9783    2.0028    3.7088
      1.5546    2.3392    2.5315    2.2046    1.3319    2.3302    1.3262    2.8959
      2.4000    2.3813    2.7197    2.2411    2.0836    3.0025    1.6874    3.2837
      2.1925    3.0374    2.6890    2.3072    1.9686    3.1213    1.2749    3.4632
      0.8311    1.4488    1.3923    1.0991    0.6041    1.7890    1.0254    2.2947
      2.1845    2.2328    2.6219    1.8965    1.6548    2.7394    1.4149    3.3726
      1.5464    1.6704    1.8868    1.9010    1.6558    2.1212    1.4383    1.9238
      1.8882    2.5704    2.9759    2.6134    1.5350    2.4547    1.5040    2.8500
   
   D = 
   
      2.0593    2.3608    2.8645    2.2912    1.6762    2.9783    2.0028    3.7088
      1.5546    2.3392    2.5315    2.2046    1.3319    2.3302    1.3262    2.8959
      2.4000    2.3813    2.7197    2.2411    2.0836    3.0025    1.6874    3.2837
      2.1925    3.0374    2.6890    2.3072    1.9686    3.1213    1.2749    3.4632
      0.8311    1.4488    1.3923    1.0991    0.6041    1.7890    1.0254    2.2947
      2.1845    2.2328    2.6219    1.8965    1.6548    2.7394    1.4149    3.3726
      1.5464    1.6704    1.8868    1.9010    1.6558    2.1212    1.4383    1.9238
      1.8882    2.5704    2.9759    2.6134    1.5350    2.4547    1.5040    2.8500
   


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

   
      0.8011    0.7076    0.7225    0.6070    0.3190    0.8719
      0.8504    0.2515    0.8153    0.9652    0.1361    0.7340
      0.5882    0.6931    0.0535    0.3223    0.6065    0.1595
      0.1841    0.7858    0.0104    0.0978    0.8407    0.9863
      0.4783    0.1344    0.8068    0.2204    0.9033    0.8204
   
   
      0.8011
      0.8504
      0.5882
      0.7076
      0.6931
      0.7858
      0.7225
      0.8153
      0.8068
      0.6070
      0.9652
      0.6065
      0.8407
      0.9033
      0.8719
      0.7340
      0.9863
      0.8204
   

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

   
      3.6012    1.4398    0.3751    3.0548    6.5073    4.5116
      8.1856    2.0285    9.1480    6.9043    8.8937    8.1857
      3.0951    9.0341    9.7289    8.8986    0.3477    5.6218
      1.5900    7.1489    2.8302    3.2642    6.3917    7.7599
      0.3950    6.5778    5.1965    6.0519    0.4815    2.7694
   
   
      0.0000    0.0000    0.0000    0.0000    6.5073    0.0000
      8.1856    0.0000    9.1480    6.9043    8.8937    8.1857
      0.0000    9.0341    9.7289    8.8986    0.0000    5.6218
      0.0000    7.1489    0.0000    0.0000    6.3917    7.7599
      0.0000    6.5778    5.1965    6.0519    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    6.5073    0.0000
      8.1856    0.0000       NaN    6.9043    8.8937    8.1857
      0.0000       NaN       NaN    8.8986    0.0000    5.6218
      0.0000    7.1489    0.0000    0.0000    6.3917    7.7599
      0.0000    6.5778    5.1965    6.0519    0.0000    0.0000
   

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

   
      6.5000    1.2664    3.1042    0.2983    6.5000    4.9698
      2.6393    2.1999    2.4438    0.0538    9.8885    0.4061
      6.5000    8.0058    0.7963    3.5744    1.1492    6.5000
      6.5000    6.5000    4.6313    6.5000    0.3350    6.5000
      0.6223    4.9747    0.0784    8.3376    2.4406    4.6837
   
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
   
