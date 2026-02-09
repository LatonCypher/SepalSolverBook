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
      0.5822    0.2976    0.2661    0.9738
   
   R1[2] = 0.2661431420215806
   C1 = 
      0.0774
      0.9283
      0.8398
      0.8007
      0.7451
      0.7737
      0.1958
      0.3805
   
   C1[5] = 0.773732723900332

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
      0.8868    0.8934    0.6163    0.6635    0.8715
      0.3890    0.9282    0.2766    0.5231    0.2384
   

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
   
      0.2050    0.4653    0.6644    0.6721    0.4760    0.5543    0.4820    0.6316
      0.6623    0.0099    0.5942    0.1022    0.3434    0.1588    0.8431    0.1467
      0.5570    0.7520    0.4065    0.4065    0.9592    0.2091    0.5788    0.4827
      0.2324    0.7904    0.6463    0.6400    0.0196    0.3651    0.7855    0.1079
      0.8208    0.4110    0.4803    0.7183    0.0919    0.8630    0.7537    0.3134
      0.7928    0.3816    0.6638    0.6726    0.5085    0.2975    0.9348    0.2504
      0.4976    0.5201    0.9381    0.0012    0.1913    0.2808    0.5611    0.1096
      0.0017    0.3293    0.9945    0.1381    0.8296    0.1278    0.3560    0.9967
   
   B = 
   
      0.6849    0.3743    0.1608    0.2698    0.4321    0.9324    0.2374    0.5895
      0.6656    0.2076    0.6148    0.9803    0.0469    0.4553    0.4187    0.6008
      0.8609    0.8033    0.4160    0.2483    0.4145    0.2073    0.1801    0.4319
      0.6541    0.6528    0.9978    0.2459    0.8111    0.3242    0.1694    0.5517
      0.2560    0.1690    0.5118    0.8624    0.6203    0.5149    0.9679    0.1434
      0.2193    0.1142    0.1078    0.6695    0.6893    0.2665    0.9651    0.6370
      0.1965    0.3191    0.0537    0.6353    0.7498    0.5412    0.3330    0.4863
      0.3013    0.0580    0.2733    0.3938    0.7398    0.8428    0.8866    0.1386
   
   C = 
   
      1.9901    1.4801    1.7679    2.1784    2.4371    1.9447    2.1932    1.8014
      1.3712    1.1477    0.7400    1.3570    1.6791    1.5774    1.1822    1.2901
      2.0485    1.3553    1.8031    2.6135    2.3045    2.3475    2.3401    1.7991
      1.9322    1.4901    1.5518    1.9584    1.8573    1.5415    1.3395    1.8764
      2.1742    1.6201    1.5673    2.1796    2.6045    2.2342    2.0258    2.3073
      2.2628    1.7810    1.7202    2.2485    2.5878    2.3267    1.8941    2.1060
      1.7492    1.2985    0.9795    1.6301    1.4433    1.4650    1.2453    1.5060
      1.7776    1.2843    1.4842    2.0241    2.1475    1.8964    2.2694    1.2161
   
   D = 
   
      1.9901    1.4801    1.7679    2.1784    2.4371    1.9447    2.1932    1.8014
      1.3712    1.1477    0.7400    1.3570    1.6791    1.5774    1.1822    1.2901
      2.0485    1.3553    1.8031    2.6135    2.3045    2.3475    2.3401    1.7991
      1.9322    1.4901    1.5518    1.9584    1.8573    1.5415    1.3395    1.8764
      2.1742    1.6201    1.5673    2.1796    2.6045    2.2342    2.0258    2.3073
      2.2628    1.7810    1.7202    2.2485    2.5878    2.3267    1.8941    2.1060
      1.7492    1.2985    0.9795    1.6301    1.4433    1.4650    1.2453    1.5060
      1.7776    1.2843    1.4842    2.0241    2.1475    1.8964    2.2694    1.2161
   


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

   
      0.8496    0.1253    0.8453    0.3201    0.4703    0.2463
      0.8162    0.0179    0.9216    0.4799    0.0086    0.0196
      0.5077    0.7652    0.2609    0.6497    0.5242    0.7624
      0.6959    0.5586    0.8349    0.0906    0.2873    0.6006
      0.9415    0.9178    0.7982    0.2274    0.1154    0.6984
   
   
      0.8496
      0.8162
      0.5077
      0.6959
      0.9415
      0.7652
      0.5586
      0.9178
      0.8453
      0.9216
      0.8349
      0.7982
      0.6497
      0.5242
      0.7624
      0.6006
      0.6984
   

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

   
      3.6544    8.3607    9.1153    9.5441    0.1380    3.4350
      4.6268    5.9194    8.3577    8.3337    7.9341    0.4567
      7.3904    0.1461    3.1147    3.5714    5.5854    3.8899
      0.8074    9.7482    6.8449    3.7164    0.5286    1.2580
      0.9083    0.4742    9.2597    9.6438    1.2208    0.2427
   
   
      0.0000    8.3607    9.1153    9.5441    0.0000    0.0000
      0.0000    5.9194    8.3577    8.3337    7.9341    0.0000
      7.3904    0.0000    0.0000    0.0000    5.5854    0.0000
      0.0000    9.7482    6.8449    0.0000    0.0000    0.0000
      0.0000    0.0000    9.2597    9.6438    0.0000    0.0000
   
   
      0.0000    8.3607       NaN       NaN    0.0000    0.0000
      0.0000    5.9194    8.3577    8.3337    7.9341    0.0000
      7.3904    0.0000    0.0000    0.0000    5.5854    0.0000
      0.0000       NaN    6.8449    0.0000    0.0000    0.0000
      0.0000    0.0000       NaN       NaN    0.0000    0.0000
   

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

   
      0.8952    1.7212    1.0423    9.5766    2.0136    6.5000
      6.5000    2.3930    6.5000    9.0005    2.1094    2.0871
      0.0697    4.8161    1.3642    9.3165    4.7915    6.5000
      3.8769    2.0481    1.4263    0.9023    8.1667    1.0346
      6.5000    1.9450    1.2214    4.9519    3.8831    6.5000
   
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
   
