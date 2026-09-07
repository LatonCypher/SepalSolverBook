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
      0.1398    0.9779    0.4046    0.1775
   
   R1[2] = 0.40463975326255086
   C1 = 
      0.6019
      0.1102
      0.1085
      0.1462
      0.5477
      0.3351
      0.1981
      0.0946
   
   C1[5] = 0.33508951129231346

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
      0.8883    0.8651    0.3062    0.7221    0.4069
      0.4911    0.7998    0.3578    0.7508    0.7410
   

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
   
      0.5944    0.2939    0.1654    0.9918    0.5466    0.8103    0.8517    0.2081
      0.9838    0.0184    0.5054    0.9602    0.5142    0.7438    0.7582    0.6941
      0.2645    0.8837    0.0151    0.2636    0.1516    0.5476    0.7847    0.2209
      0.6988    0.2160    0.8656    0.8237    0.3058    0.9482    0.7993    0.1466
      0.5432    0.9288    0.9438    0.4352    0.8680    0.3341    0.2734    0.3057
      0.1477    0.6413    0.4251    0.1091    0.5564    0.3848    0.8491    0.0900
      0.0059    0.4744    0.6432    0.4474    0.9747    0.2034    0.4697    0.4725
      0.1033    0.5476    0.9177    0.1283    0.9210    0.0577    0.2324    0.3119
   
   B = 
   
      0.8625    0.6512    0.5476    0.0642    0.8764    0.8992    0.0879    0.6444
      0.1968    0.6208    0.1614    0.4267    0.4287    0.4530    0.9425    0.3436
      0.6223    0.6523    0.5431    0.4019    0.1720    0.7957    0.7667    0.8744
      0.7285    0.1694    0.3587    0.1007    0.9610    0.0785    0.6317    0.1441
      0.1424    0.7681    0.2595    0.1074    0.4807    0.3844    0.0042    0.1792
      0.9114    0.8359    0.3935    0.4968    0.5134    0.3770    0.6886    0.5711
      0.5613    0.2529    0.8474    0.3985    0.5789    0.1947    0.0197    0.6656
      0.6020    0.6136    0.9166    0.2138    0.3314    0.2625    0.2730    0.1292
   
   C = 
   
      2.8158    2.2858    2.1917    1.1751    2.8694    1.6131    1.7164    1.9261
      3.4608    2.7787    2.8654    1.2461    3.1778    2.1783    1.8166    2.3318
      1.6976    1.6836    1.5125    1.0749    1.7481    1.1464    1.4877    1.4161
      3.2286    2.6129    2.4472    1.4216    2.7906    2.1487    2.1590    2.5474
      2.3213    2.8224    1.9846    1.2878    2.3031    2.2873    2.2442    2.1250
      1.5584    1.8090    1.5522    1.0735    1.5688    1.3180    1.3209    1.5992
      1.6970    2.1211    1.7538    1.0003    1.7507    1.4340    1.5058    1.4583
      1.3634    2.0333    1.4340    0.9089    1.3167    1.5841    1.4432    1.4687
   
   D = 
   
      2.8158    2.2858    2.1917    1.1751    2.8694    1.6131    1.7164    1.9261
      3.4608    2.7787    2.8654    1.2461    3.1778    2.1783    1.8166    2.3318
      1.6976    1.6836    1.5125    1.0749    1.7481    1.1464    1.4877    1.4161
      3.2286    2.6129    2.4472    1.4216    2.7906    2.1487    2.1590    2.5474
      2.3213    2.8224    1.9846    1.2878    2.3031    2.2873    2.2442    2.1250
      1.5584    1.8090    1.5522    1.0735    1.5688    1.3180    1.3209    1.5992
      1.6970    2.1211    1.7538    1.0003    1.7507    1.4340    1.5058    1.4583
      1.3634    2.0333    1.4340    0.9089    1.3167    1.5841    1.4432    1.4687
   


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

   
      0.9242    0.5059    0.8688    0.3639    0.8322    0.8531
      0.4513    0.9051    0.5494    0.9635    0.3355    0.1604
      0.4269    0.5035    0.6043    0.0462    0.5953    0.2538
      0.3129    0.6551    0.8609    0.3154    0.8741    0.4852
      0.6920    0.0322    0.1031    0.2102    0.6701    0.9814
   
   
      0.9242
      0.6920
      0.5059
      0.9051
      0.5035
      0.6551
      0.8688
      0.5494
      0.6043
      0.8609
      0.9635
      0.8322
      0.5953
      0.8741
      0.6701
      0.8531
      0.9814
   

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

   
      7.2420    4.5731    5.7366    1.9816    2.9304    7.3908
      9.8942    1.1610    1.7139    1.5190    0.6225    7.0224
      3.3208    0.3728    1.8893    8.0092    9.9499    7.9303
      5.3588    3.3109    2.4458    6.7417    5.3648    8.0497
      6.8482    8.1909    9.5202    1.8310    4.7533    0.6374
   
   
      7.2420    0.0000    5.7366    0.0000    0.0000    7.3908
      9.8942    0.0000    0.0000    0.0000    0.0000    7.0224
      0.0000    0.0000    0.0000    8.0092    9.9499    7.9303
      5.3588    0.0000    0.0000    6.7417    5.3648    8.0497
      6.8482    8.1909    9.5202    0.0000    0.0000    0.0000
   
   
      7.2420    0.0000    5.7366    0.0000    0.0000    7.3908
         NaN    0.0000    0.0000    0.0000    0.0000    7.0224
      0.0000    0.0000    0.0000    8.0092       NaN    7.9303
      5.3588    0.0000    0.0000    6.7417    5.3648    8.0497
      6.8482    8.1909       NaN    0.0000    0.0000    0.0000
   

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

   
      6.5000    0.1466    2.4226    1.9567    8.2675    9.9334
      6.5000    3.7916    3.5302    6.5000    6.5000    6.5000
      8.1147    6.5000    9.2118    2.3433    6.5000    0.7574
      6.5000    8.0123    0.4933    1.2865    4.8884    8.0793
      0.3224    6.5000    0.2146    4.2179    6.5000    2.0379
   
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
   
