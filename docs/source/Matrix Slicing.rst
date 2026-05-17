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
      0.6101    0.2624    0.5934    0.2917
   
   R1[2] = 0.5934211063140448
   C1 = 
      0.0203
      0.8464
      0.8927
      0.5296
      0.1157
      0.8461
      0.0052
      0.0303
   
   C1[5] = 0.8461125105197019

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
      0.6966    0.6182    0.8175    0.6807    0.1420
      0.4268    0.7315    0.7878    0.9613    0.5538
   

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
   
      0.8839    0.7641    0.5702    0.7626    0.4483    0.7004    0.2154    0.0656
      0.7984    0.0812    0.8509    0.5595    0.5520    0.7924    0.1532    0.7032
      0.6139    0.8412    0.5487    0.5531    0.4775    0.6884    0.4546    0.6187
      0.9744    0.0016    0.2262    0.2911    0.9404    0.9404    0.7957    0.1752
      0.7995    0.1130    0.2070    0.7442    0.9375    0.5496    0.9865    0.2307
      0.7290    0.0865    0.8212    0.1263    0.5410    0.2072    0.1253    0.2361
      0.3186    0.9223    0.0318    0.4450    0.7310    0.7903    0.9932    0.6844
      0.3274    0.2117    0.8262    0.1324    0.6103    0.6928    0.7552    0.3124
   
   B = 
   
      0.0466    0.6668    0.8503    0.5785    0.5843    0.3644    0.4449    0.7652
      0.2332    0.3216    0.3984    0.7952    0.3773    0.4904    0.2687    0.0696
      0.1730    0.5850    0.0928    0.2556    0.7053    0.1374    0.8529    0.0900
      0.9815    0.2466    0.2016    0.4254    0.3379    0.8938    0.7303    0.8364
      0.6252    0.4769    0.7578    0.8699    0.5785    0.6805    0.9948    0.1881
      0.2913    0.1628    0.9972    0.8719    0.0715    0.4451    0.1745    0.4288
      0.8328    0.4346    0.3502    0.4073    0.9281    0.5159    0.0095    0.1408
      0.5653    0.5774    0.0125    0.5687    0.8525    0.8207    0.4439    0.2132
   
   C = 
   
      1.7672    1.8161    2.3770    2.7147    2.0300    2.2385    2.2411    1.8476
      1.8535    2.0591    2.1739    2.6152    2.4040    2.3322    2.5123    1.7763
      2.0900    2.0320    2.2348    2.9522    2.5249    2.5797    2.2451    1.6213
      1.9941    1.9029    2.8400    2.8082    2.3269    2.2598    2.0243    1.7390
      2.5280    1.9727    2.5009    2.7496    2.6012    2.6213    2.2464    1.8609
      0.9566    1.5080    1.4194    1.5907    1.7259    1.2526    1.8205    1.0020
      2.5735    1.9416    2.4294    3.2339    2.6916    2.8938    1.9200    1.4453
      1.7263    1.7149    1.8878    2.2454    2.2685    1.8246    1.8778    1.0352
   
   D = 
   
      1.7672    1.8161    2.3770    2.7147    2.0300    2.2385    2.2411    1.8476
      1.8535    2.0591    2.1739    2.6152    2.4040    2.3322    2.5123    1.7763
      2.0900    2.0320    2.2348    2.9522    2.5249    2.5797    2.2451    1.6213
      1.9941    1.9029    2.8400    2.8082    2.3269    2.2598    2.0243    1.7390
      2.5280    1.9727    2.5009    2.7496    2.6012    2.6213    2.2464    1.8609
      0.9566    1.5080    1.4194    1.5907    1.7259    1.2526    1.8205    1.0020
      2.5735    1.9416    2.4294    3.2339    2.6916    2.8938    1.9200    1.4453
      1.7263    1.7149    1.8878    2.2454    2.2685    1.8246    1.8778    1.0352
   


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

   
      0.0582    0.4340    0.3553    0.7183    0.1752    0.1588
      0.6048    0.7098    0.7288    0.6846    0.7890    0.5814
      0.3696    0.1447    0.5415    0.1784    0.0254    0.5026
      0.7942    0.3547    0.1016    0.4599    0.7950    0.6230
      0.9701    0.0593    0.1765    0.6953    0.4774    0.5905
   
   
      0.6048
      0.7942
      0.9701
      0.7098
      0.7288
      0.5415
      0.7183
      0.6846
      0.6953
      0.7890
      0.7950
      0.5814
      0.5026
      0.6230
      0.5905
   

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

   
      0.3157    6.9009    2.7361    7.5278    8.8312    1.7940
      3.2904    0.7512    7.6752    1.2288    5.5011    7.1961
      3.5678    5.6637    9.2504    4.4044    1.0342    4.5853
      3.9530    6.5408    7.1609    0.8664    8.3373    7.2224
      3.0346    7.3378    8.3331    3.6896    7.3221    0.5614
   
   
      0.0000    6.9009    0.0000    7.5278    8.8312    0.0000
      0.0000    0.0000    7.6752    0.0000    5.5011    7.1961
      0.0000    5.6637    9.2504    0.0000    0.0000    0.0000
      0.0000    6.5408    7.1609    0.0000    8.3373    7.2224
      0.0000    7.3378    8.3331    0.0000    7.3221    0.0000
   
   
      0.0000    6.9009    0.0000    7.5278    8.8312    0.0000
      0.0000    0.0000    7.6752    0.0000    5.5011    7.1961
      0.0000    5.6637       NaN    0.0000    0.0000    0.0000
      0.0000    6.5408    7.1609    0.0000    8.3373    7.2224
      0.0000    7.3378    8.3331    0.0000    7.3221    0.0000
   

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

   
      4.3522    0.3577    6.5000    9.6065    8.5499    4.3567
      2.6798    6.5000    3.6096    2.5198    6.5000    2.6803
      6.5000    4.0221    6.5000    0.0462    3.6443    9.5927
      0.4729    9.7893    1.5422    8.3177    6.5000    6.5000
      3.4186    8.7653    6.5000    1.9461    8.6309    1.3130
   
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
   
