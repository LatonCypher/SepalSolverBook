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
      0.5834    0.8582    0.7388    0.6921
   
   R1[2] = 0.7388103989689018
   C1 = 
      0.9259
      0.5813
      0.9384
      0.4231
      0.6959
      0.0455
      0.1409
      0.4875
   
   C1[5] = 0.04554263867275565

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
      0.3925    0.5989    0.4253    0.7644    0.4554
      0.3704    0.2660    0.1097    0.8354    0.2157
   

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
   
      0.1833    0.8891    0.4556    0.4717    0.3712    0.3742    0.4104    0.3467
      0.3920    0.9908    0.1744    0.2739    0.8604    0.9968    0.9427    0.9857
      0.2660    0.9959    0.5783    0.6677    0.0243    0.9730    0.7570    0.5134
      0.1549    0.4402    0.0333    0.6131    0.1555    0.6638    0.6040    0.9616
      0.2403    0.2611    0.2622    0.7303    0.0471    0.7407    0.8812    0.3840
      0.5718    0.3858    0.7185    0.5999    0.1902    0.0844    0.7605    0.7501
      0.1117    0.1879    0.1898    0.7023    0.2567    0.7578    0.5847    0.0110
      0.2238    0.8240    0.7961    0.5543    0.9150    0.2100    0.8124    0.4447
   
   B = 
   
      0.3101    0.7811    0.9563    0.6535    0.4127    0.3841    0.1857    0.4923
      0.1291    0.0330    0.6073    0.4279    0.3257    1.0000    0.7550    0.6243
      0.8664    0.5067    0.6934    0.2442    0.8990    0.4097    0.3908    0.9892
      0.8775    0.3166    0.9576    0.3866    0.6296    0.9089    0.6580    0.1193
      0.6032    0.7636    0.2600    0.6700    0.6796    0.4113    0.3843    0.7293
      0.8042    0.7912    0.2101    0.3047    0.3398    0.4067    0.3887    0.7032
      0.1216    0.9034    0.4280    0.1324    0.2781    0.0541    0.4885    0.0902
      0.7601    0.7024    0.3432    0.5317    0.5273    0.1463    0.6627    0.6766
   
   C = 
   
      1.8185    1.7464    1.9526    1.3953    1.7482    1.9526    1.9120    1.9577
      2.8253    3.5035    2.5346    2.3578    2.5191    2.4160    2.9010    3.0971
      2.5773    2.5778    2.6105    1.6854    2.2028    2.4635    2.5641    2.5219
      2.1036    2.2115    1.7940    1.4325    1.6295    1.5777    2.0276    1.7424
      1.9993    2.2483    1.9464    1.1934    1.6111    1.5491    1.8158    1.5224
      2.2213    2.4393    2.5039    1.5989    2.1503    1.7084    2.0474    2.0789
      1.6832    1.7434    1.5050    0.9575    1.3204    1.3938    1.3850    1.2241
      2.5095    2.6920    2.5796    1.9287    2.5791    2.3105    2.4643    2.6673
   
   D = 
   
      1.8185    1.7464    1.9526    1.3953    1.7482    1.9526    1.9120    1.9577
      2.8253    3.5035    2.5346    2.3578    2.5191    2.4160    2.9010    3.0971
      2.5773    2.5778    2.6105    1.6854    2.2028    2.4635    2.5641    2.5219
      2.1036    2.2115    1.7940    1.4325    1.6295    1.5777    2.0276    1.7424
      1.9993    2.2483    1.9464    1.1934    1.6111    1.5491    1.8158    1.5224
      2.2213    2.4393    2.5039    1.5989    2.1503    1.7084    2.0474    2.0789
      1.6832    1.7434    1.5050    0.9575    1.3204    1.3938    1.3850    1.2241
      2.5095    2.6920    2.5796    1.9287    2.5791    2.3105    2.4643    2.6673
   


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

   
      0.4861    0.5767    0.3340    0.0995    0.6180    0.2853
      0.1681    0.8046    0.4611    0.8911    0.6311    0.5029
      0.4116    0.2382    0.7566    0.8702    0.8673    0.8206
      0.2984    0.8755    0.8263    0.4133    0.1563    0.3032
      0.8197    0.6023    0.2975    0.9694    0.1267    0.4337
   
   
      0.8197
      0.5767
      0.8046
      0.8755
      0.6023
      0.7566
      0.8263
      0.8911
      0.8702
      0.9694
      0.6180
      0.6311
      0.8673
      0.5029
      0.8206
   

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

   
      3.2435    1.3730    9.9058    6.1347    9.9545    3.3342
      8.9256    1.4890    4.7438    3.6868    1.2676    8.0263
      7.8986    9.9720    0.9015    5.7094    8.3484    2.1478
      1.9220    8.4146    8.2692    1.9822    2.3925    6.9557
      0.7347    6.6944    1.3805    1.1229    4.5569    3.9288
   
   
      0.0000    0.0000    9.9058    6.1347    9.9545    0.0000
      8.9256    0.0000    0.0000    0.0000    0.0000    8.0263
      7.8986    9.9720    0.0000    5.7094    8.3484    0.0000
      0.0000    8.4146    8.2692    0.0000    0.0000    6.9557
      0.0000    6.6944    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000       NaN    6.1347       NaN    0.0000
      8.9256    0.0000    0.0000    0.0000    0.0000    8.0263
      7.8986       NaN    0.0000    5.7094    8.3484    0.0000
      0.0000    8.4146    8.2692    0.0000    0.0000    6.9557
      0.0000    6.6944    0.0000    0.0000    0.0000    0.0000
   

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

   
      6.5000    1.6786    8.7686    1.8176    0.0100    8.4814
      6.5000    8.9599    6.5000    6.5000    8.4999    3.6391
      9.7153    1.0897    0.2646    6.5000    6.5000    3.0688
      4.8024    3.1578    3.8331    4.7431    4.8186    4.7984
      6.5000    8.0255    4.0886    6.5000    9.0718    0.5684
   
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
   
