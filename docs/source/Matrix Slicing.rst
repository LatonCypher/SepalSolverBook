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
      0.0157    0.0281    0.0536    0.9319
   
   R1[2] = 0.053618758352904305
   C1 = 
      0.6669
      0.7785
      0.6477
      0.4163
      0.0108
      0.9613
      0.9970
      0.6611
   
   C1[5] = 0.9613291751889895

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
      0.1954    0.8832    0.4948    0.8289    0.9350
      0.9096    0.2282    0.9092    0.4027    0.3949
   

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
   
      0.0034    0.2915    0.4810    0.3052    0.1389    0.2257    0.3467    0.2848
      0.0666    0.5933    0.0470    0.2859    0.0233    0.5026    0.7822    0.0237
      0.8209    0.9970    0.3306    0.9444    0.2240    0.9622    0.3154    0.9061
      0.5654    0.8397    0.0692    0.5676    0.3133    0.2607    0.9959    0.5764
      0.1700    0.2884    0.1704    0.4770    0.8657    0.0167    0.4270    0.8535
      0.8592    0.3521    0.3120    0.7022    0.7238    0.4377    0.1281    0.7594
      0.5192    0.3773    0.5758    0.7612    0.1048    0.2722    0.1037    0.7013
      0.0922    0.1728    0.5015    0.0461    0.1156    0.6253    0.9841    0.9404
   
   B = 
   
      0.3626    0.1991    0.4934    0.5147    0.3867    0.2897    0.1172    0.3733
      0.1906    0.4740    0.2465    0.5218    0.5854    0.1495    0.2738    0.0201
      0.4400    0.9799    0.6827    0.7029    0.0955    0.2107    0.5600    0.4362
      0.1664    0.5015    0.0956    0.3984    0.9960    0.6251    0.7074    0.5098
      0.9865    0.5929    0.9536    0.3206    0.6847    0.4635    0.2763    0.8046
      0.8408    0.8183    0.1205    0.1820    0.7956    0.0364    0.3325    0.0890
      0.5738    0.0220    0.3557    0.8584    0.8345    0.2803    0.6966    0.0750
      0.5266    0.0103    0.8204    0.7331    0.7539    0.1469    0.7407    0.3472
   
   C = 
   
      0.9949    1.0408    0.9478    1.2055    1.3006    0.5483    1.1314    0.6293
      1.1124    0.9265    0.6191    1.2785    1.7487    0.5484    1.1348    0.3334
      2.4783    2.3701    2.1518    2.7332    3.7385    1.4072    2.4951    1.5562
      1.8932    1.2900    1.7448    2.4293    2.9698    1.1772    2.0304    1.0975
      1.8334    1.1219    1.9965    1.8206    2.3318    1.0733    1.7061    1.4131
      2.1881    1.7938    2.2024    2.1033    2.7907    1.3051    1.8657    1.7164
      1.4011    1.5226    1.5599    1.8582    2.1383    0.9945    1.7362    1.2004
      1.9944    1.2265    1.7421    2.1935    2.3376    0.6774    1.9936    0.8291
   
   D = 
   
      0.9949    1.0408    0.9478    1.2055    1.3006    0.5483    1.1314    0.6293
      1.1124    0.9265    0.6191    1.2785    1.7487    0.5484    1.1348    0.3334
      2.4783    2.3701    2.1518    2.7332    3.7385    1.4072    2.4951    1.5562
      1.8932    1.2900    1.7448    2.4293    2.9698    1.1772    2.0304    1.0975
      1.8334    1.1219    1.9965    1.8206    2.3318    1.0733    1.7061    1.4131
      2.1881    1.7938    2.2024    2.1033    2.7907    1.3051    1.8657    1.7164
      1.4011    1.5226    1.5599    1.8582    2.1383    0.9945    1.7362    1.2004
      1.9944    1.2265    1.7421    2.1935    2.3376    0.6774    1.9936    0.8291
   


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

   
      0.2176    0.5895    0.9683    0.5609    0.9007    0.7726
      0.4255    0.6615    0.7342    0.5959    0.6463    0.2132
      0.9404    0.6945    0.3410    0.0618    0.2799    0.5672
      0.0237    0.0422    0.3379    0.0977    0.8787    0.3750
      0.3833    0.2683    0.8842    0.6172    0.4241    0.8867
   
   
      0.9404
      0.5895
      0.6615
      0.6945
      0.9683
      0.7342
      0.8842
      0.5609
      0.5959
      0.6172
      0.9007
      0.6463
      0.8787
      0.7726
      0.5672
      0.8867
   

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

   
      9.9008    9.6739    9.8802    8.8081    4.7060    6.3823
      1.2647    7.1989    1.9163    6.2076    5.8742    0.0218
      7.1057    9.8729    8.6722    1.8897    9.4167    8.6128
      1.1707    7.1840    0.6328    3.3481    0.3434    4.8074
      8.2777    9.9790    8.4028    7.2710    9.4393    9.1829
   
   
      9.9008    9.6739    9.8802    8.8081    0.0000    6.3823
      0.0000    7.1989    0.0000    6.2076    5.8742    0.0000
      7.1057    9.8729    8.6722    0.0000    9.4167    8.6128
      0.0000    7.1840    0.0000    0.0000    0.0000    0.0000
      8.2777    9.9790    8.4028    7.2710    9.4393    9.1829
   
   
         NaN       NaN       NaN    8.8081    0.0000    6.3823
      0.0000    7.1989    0.0000    6.2076    5.8742    0.0000
      7.1057       NaN    8.6722    0.0000       NaN    8.6128
      0.0000    7.1840    0.0000    0.0000    0.0000    0.0000
      8.2777       NaN    8.4028    7.2710       NaN       NaN
   

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

   
      1.3680    8.7396    4.1725    1.4353    6.5000    8.4047
      2.4554    3.4705    3.4717    6.5000    2.5646    6.5000
      0.3598    9.8866    2.0810    3.5619    6.5000    6.5000
      6.5000    0.7713    9.0440    6.5000    6.5000    6.5000
      4.5261    6.5000    6.5000    0.0564    0.5181    2.4191
   
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
   
