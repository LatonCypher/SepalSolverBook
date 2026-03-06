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
      0.6296    0.6861    0.0231    0.6124
   
   R1[2] = 0.02305543873020921
   C1 = 
      0.1413
      0.3660
      0.0162
      0.5008
      0.8535
      0.9701
      0.6437
      0.0754
   
   C1[5] = 0.9700880315473591

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
      0.0483    0.7610    0.2683    0.9841    0.6708
      0.4631    0.0139    0.8405    0.9134    0.6411
   

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
   
      0.8477    0.6918    0.2492    0.3960    0.4706    0.7687    0.6799    0.0421
      0.2228    0.3057    0.5190    0.0231    0.1750    0.2945    0.0185    0.8125
      0.1610    0.9841    0.2486    0.0461    0.3461    0.6120    0.5980    0.9784
      0.0379    0.1230    0.3696    0.1749    0.4805    0.0091    0.7286    0.7752
      0.2260    0.5363    0.3792    0.4896    0.7131    0.7781    0.6112    0.1400
      0.2567    0.2283    0.8449    0.5308    0.0521    0.5528    0.8270    0.4751
      0.0999    0.5860    0.6710    0.2677    0.0282    0.3714    0.6438    0.6932
      0.0429    0.0743    0.1441    0.4147    0.9009    0.9321    0.4006    0.1892
   
   B = 
   
      0.2249    0.1401    0.8940    0.4262    0.0791    0.2307    0.2000    0.4357
      0.2872    0.9249    0.5942    0.7660    0.5769    0.2179    0.3103    0.6747
      0.9182    0.6677    0.0753    0.8400    0.5981    0.3586    0.0206    0.6599
      0.4396    0.9994    0.6459    0.6796    0.4694    0.7126    0.3375    0.8501
      0.2298    0.5627    0.5263    0.2012    0.4689    0.3756    0.0376    0.0513
      0.1336    0.7699    0.9708    0.1686    0.2975    0.7497    0.2763    0.5950
      0.7054    0.7174    0.2476    0.6794    0.8916    0.9986    0.1723    0.9066
      0.5756    0.5136    0.9567    0.8886    0.8624    0.4770    0.6980    0.6040
   
   C = 
   
      1.5069    2.6868    2.6460    2.0932    1.8930    2.1700    0.8996    2.4606
      1.1849    1.4393    1.5948    1.6001    1.4021    1.0131    0.8162    1.3572
      1.7135    2.7422    2.6375    2.5109    2.4720    2.0262    1.3263    2.4523
      1.5319    1.7390    1.4317    1.8218    1.9234    1.5775    0.7997    1.6511
      1.5477    2.7808    2.2815    1.9726    2.0154    2.1822    0.8295    2.2649
      2.0751    2.6340    1.9950    2.4425    2.2425    2.2768    0.9476    2.6430
      1.8338    2.3912    1.8589    2.3586    2.1685    1.8447    1.0045    2.3340
      1.0686    2.1945    2.0204    1.2567    1.5472    1.9008    0.6671    1.5947
   
   D = 
   
      1.5069    2.6868    2.6460    2.0932    1.8930    2.1700    0.8996    2.4606
      1.1849    1.4393    1.5948    1.6001    1.4021    1.0131    0.8162    1.3572
      1.7135    2.7422    2.6375    2.5109    2.4720    2.0262    1.3263    2.4523
      1.5319    1.7390    1.4317    1.8218    1.9234    1.5775    0.7997    1.6511
      1.5477    2.7808    2.2815    1.9726    2.0154    2.1822    0.8295    2.2649
      2.0751    2.6340    1.9950    2.4425    2.2425    2.2768    0.9476    2.6430
      1.8338    2.3912    1.8589    2.3586    2.1685    1.8447    1.0045    2.3340
      1.0686    2.1945    2.0204    1.2567    1.5472    1.9008    0.6671    1.5947
   


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

   
      0.8519    0.7743    0.5340    0.6565    0.2657    0.7706
      0.7184    0.4381    0.4193    0.8876    0.3535    0.1190
      0.1505    0.5380    0.9234    0.1245    0.6359    0.8787
      0.1774    0.8124    0.1161    0.3147    0.7748    0.4299
      0.9202    0.0229    0.6187    0.6511    0.3398    0.7440
   
   
      0.8519
      0.7184
      0.9202
      0.7743
      0.5380
      0.8124
      0.5340
      0.9234
      0.6187
      0.6565
      0.8876
      0.6511
      0.6359
      0.7748
      0.7706
      0.8787
      0.7440
   

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

   
      1.4687    3.4022    9.3489    7.5960    7.2058    9.4986
      8.3186    6.7911    3.2297    3.2355    7.8680    8.7697
      3.6238    0.0291    1.1785    1.3606    4.7970    3.6132
      5.1015    0.8732    4.4582    2.0746    7.4964    7.9442
      0.7389    9.3304    2.6157    5.0944    4.5744    7.9358
   
   
      0.0000    0.0000    9.3489    7.5960    7.2058    9.4986
      8.3186    6.7911    0.0000    0.0000    7.8680    8.7697
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      5.1015    0.0000    0.0000    0.0000    7.4964    7.9442
      0.0000    9.3304    0.0000    5.0944    0.0000    7.9358
   
   
      0.0000    0.0000       NaN    7.5960    7.2058       NaN
      8.3186    6.7911    0.0000    0.0000    7.8680    8.7697
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      5.1015    0.0000    0.0000    0.0000    7.4964    7.9442
      0.0000       NaN    0.0000    5.0944    0.0000    7.9358
   

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

   
      1.7535    3.1985    6.5000    9.9159    4.0106    8.6455
      1.9196    9.7926    6.5000    9.4847    2.2620    2.0004
      8.3869    6.5000    2.9373    6.5000    6.5000    6.5000
      6.5000    6.5000    6.5000    6.5000    2.5362    2.0231
      6.5000    2.6237    6.5000    0.1672    0.3852    2.2076
   
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
   
