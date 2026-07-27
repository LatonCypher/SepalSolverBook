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
      0.1403    0.7555    0.7649    0.1609
   
   R1[2] = 0.7648687591376403
   C1 = 
      0.0777
      0.8496
      0.4480
      0.4179
      0.9714
      0.3122
      0.2993
      0.5208
   
   C1[5] = 0.3122467193082017

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
      0.2905    0.0188    0.5776    0.7117    0.1068
      0.5128    0.2004    0.0162    0.0012    0.5661
   

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
   
      0.7744    0.6908    0.4157    0.0252    0.6366    0.2684    0.4742    0.9085
      0.2759    0.8044    0.5336    0.0181    0.8406    0.2457    0.3837    0.3037
      0.8859    0.7414    0.1308    0.5610    0.6309    0.7024    0.8882    0.0160
      0.1221    0.2358    0.7449    0.3139    0.1857    0.5093    0.7275    0.8506
      0.0050    0.1242    0.8839    0.0591    0.5670    0.4703    0.7267    0.2323
      0.9704    0.9966    0.0420    0.4352    0.1844    0.6938    0.7297    0.7118
      0.6057    0.5017    0.9388    0.5870    0.6960    0.0460    0.9666    0.3243
      0.8659    0.5148    0.7171    0.9789    0.3646    0.6166    0.3221    0.9040
   
   B = 
   
      0.1451    0.9724    0.6072    0.3204    0.2442    0.3690    0.2058    0.4419
      0.8293    0.9314    0.1885    0.9141    0.1138    0.5149    0.6612    0.9201
      0.0316    0.1708    0.8256    0.7211    0.9651    0.6330    0.2387    0.9938
      0.3138    0.4532    0.1687    0.1239    0.9406    0.6241    0.8003    0.0850
      0.2127    0.8556    0.6455    0.2542    0.1978    0.0325    0.6791    0.6438
      0.9088    0.4614    0.8906    0.0479    0.6127    0.2027    0.0233    0.0398
      0.4519    0.1792    0.2516    0.7224    0.6443    0.2151    0.9807    0.5476
      0.4193    0.6233    0.1102    0.6028    0.6222    0.8828    0.0129    0.2893
   
   C = 
   
      1.6808    2.7986    1.8172    2.2473    1.8537    1.8994    1.6507    2.3360
      1.4324    2.2075    1.6542    1.8965    1.4438    1.2928    1.6872    2.2428
      2.1041    2.8616    2.1383    1.9707    2.0919    1.5095    2.4687    2.1764
      1.5230    1.6622    1.6367    1.9407    2.4176    1.8506    1.4724    1.8222
      1.1240    1.2755    1.7594    1.5916    1.9371    1.1374    1.4531    1.8489
      2.4031    3.1285    1.8841    2.3425    2.1749    2.1013    2.0831    2.1763
      1.4804    2.4747    2.1057    2.4754    2.6537    1.9686    2.5761    2.7852
      2.0449    3.1052    2.3451    2.2863    3.1028    2.6536    2.0626    2.3493
   
   D = 
   
      1.6808    2.7986    1.8172    2.2473    1.8537    1.8994    1.6507    2.3360
      1.4324    2.2075    1.6542    1.8965    1.4438    1.2928    1.6872    2.2428
      2.1041    2.8616    2.1383    1.9707    2.0919    1.5095    2.4687    2.1764
      1.5230    1.6622    1.6367    1.9407    2.4176    1.8506    1.4724    1.8222
      1.1240    1.2755    1.7594    1.5916    1.9371    1.1374    1.4531    1.8489
      2.4031    3.1285    1.8841    2.3425    2.1749    2.1013    2.0831    2.1763
      1.4804    2.4747    2.1057    2.4754    2.6537    1.9686    2.5761    2.7852
      2.0449    3.1052    2.3451    2.2863    3.1028    2.6536    2.0626    2.3493
   


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

   
      0.0320    0.1233    0.0083    0.8658    0.2948    0.7225
      0.4358    0.0534    0.7332    0.8366    0.3999    0.8961
      0.6863    0.9721    0.5163    0.8476    0.7393    0.0356
      0.5204    0.7657    0.7817    0.2243    0.3340    0.3237
      0.4216    0.2293    0.7821    0.6628    0.0637    0.5854
   
   
      0.6863
      0.5204
      0.9721
      0.7657
      0.7332
      0.5163
      0.7817
      0.7821
      0.8658
      0.8366
      0.8476
      0.6628
      0.7393
      0.7225
      0.8961
      0.5854
   

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

   
      6.9758    6.1192    3.6667    6.3781    0.9927    8.9406
      2.5308    8.2759    3.1891    2.1104    1.5405    6.0834
      9.4853    4.4536    0.2806    9.2892    6.8857    3.3083
      8.5900    8.4826    2.5436    9.5640    5.3409    5.1355
      1.8706    1.6060    1.8831    8.8006    6.3395    7.4537
   
   
      6.9758    6.1192    0.0000    6.3781    0.0000    8.9406
      0.0000    8.2759    0.0000    0.0000    0.0000    6.0834
      9.4853    0.0000    0.0000    9.2892    6.8857    0.0000
      8.5900    8.4826    0.0000    9.5640    5.3409    5.1355
      0.0000    0.0000    0.0000    8.8006    6.3395    7.4537
   
   
      6.9758    6.1192    0.0000    6.3781    0.0000    8.9406
      0.0000    8.2759    0.0000    0.0000    0.0000    6.0834
         NaN    0.0000    0.0000       NaN    6.8857    0.0000
      8.5900    8.4826    0.0000       NaN    5.3409    5.1355
      0.0000    0.0000    0.0000    8.8006    6.3395    7.4537
   

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

   
      6.5000    4.6034    9.3661    3.3372    6.5000    8.2378
      2.2421    9.3841    6.5000    6.5000    3.7919    4.5802
      8.0234    1.5102    4.1590    9.3792    3.8646    3.6136
      0.4240    2.6047    6.5000    6.5000    4.8129    1.6044
      8.1105    2.9991    3.7554    1.6085    9.0808    6.5000
   
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
   
