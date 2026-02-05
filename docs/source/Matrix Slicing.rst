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
      0.9998    0.2919    0.6360    0.5452
   
   R1[2] = 0.6360035978046013
   C1 = 
      0.4922
      0.6226
      0.3064
      0.3420
      0.7495
      0.0417
      0.0503
      0.1514
   
   C1[5] = 0.041749248394589045

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
      0.3168    0.9812    0.8061    0.3700    0.3838
      0.1444    0.8753    0.0504    0.7148    0.8189
   

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
   
      0.7019    0.3740    0.3244    0.3962    0.7150    0.0064    0.3028    0.1618
      0.6529    0.3855    0.9075    0.0374    0.9300    0.9289    0.3689    0.9510
      0.5034    0.0155    0.3442    0.1281    0.1177    0.1341    0.2680    0.9437
      0.7389    0.8105    0.6558    0.1270    0.6510    0.1805    0.6207    0.2286
      0.8543    0.8222    0.9636    0.5552    0.3851    0.4404    0.4371    0.9216
      0.7531    0.8009    0.9584    0.0751    0.3911    0.0453    0.9290    0.4551
      0.7474    0.7109    0.4772    0.9834    0.7089    0.6719    0.0643    0.9063
      0.9609    0.2877    0.7261    0.4171    0.2859    0.4953    0.5444    0.6939
   
   B = 
   
      0.4347    0.5254    0.4292    0.0413    0.3536    0.3665    0.9193    0.7937
      0.0416    0.6966    0.1933    0.6785    0.2061    0.9140    0.6652    0.9592
      0.6699    0.8475    0.6545    0.3231    0.5199    0.8453    0.1030    0.4153
      0.2656    0.5012    0.2044    0.5708    0.5400    0.5211    0.2683    0.0901
      0.4806    0.9813    0.8596    0.9046    0.8705    0.1992    0.1776    0.7069
      0.6679    0.2262    0.3411    0.4299    0.6650    0.1456    0.4273    0.4856
      0.6090    0.5459    0.8611    0.6220    0.6550    0.3208    0.6243    0.9799
      0.2899    0.4772    0.8282    0.9164    0.9778    0.9297    0.6874    0.9817
   
   C = 
   
      1.2224    2.0484    1.6784    1.5999    1.6911    1.4707    1.4638    2.0504
      2.4854    3.1773    3.1779    2.9447    3.4012    2.7013    2.4063    3.6719
      1.0670    1.3737    1.6299    1.4113    1.7193    1.5627    1.4372    1.9064
      1.7056    2.6998    2.2740    2.1269    2.1547    2.1997    2.0574    3.0282
      2.2111    3.2725    2.8907    2.8756    3.0880    3.3064    2.7435    3.7362
      1.9386    2.9219    2.6493    2.2953    2.3944    2.6630    2.3257    3.4265
      2.0266    3.1003    2.6160    3.0293    3.1822    2.9417    2.5492    3.3420
      2.0277    2.5506    2.4867    2.1536    2.6152    2.3951    2.3407    3.0351
   
   D = 
   
      1.2224    2.0484    1.6784    1.5999    1.6911    1.4707    1.4638    2.0504
      2.4854    3.1773    3.1779    2.9447    3.4012    2.7013    2.4063    3.6719
      1.0670    1.3737    1.6299    1.4113    1.7193    1.5627    1.4372    1.9064
      1.7056    2.6998    2.2740    2.1269    2.1547    2.1997    2.0574    3.0282
      2.2111    3.2725    2.8907    2.8756    3.0880    3.3064    2.7435    3.7362
      1.9386    2.9219    2.6493    2.2953    2.3944    2.6630    2.3257    3.4265
      2.0266    3.1003    2.6160    3.0293    3.1822    2.9417    2.5492    3.3420
      2.0277    2.5506    2.4867    2.1536    2.6152    2.3951    2.3407    3.0351
   


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

   
      0.4475    0.8751    0.4117    0.6784    0.0619    0.3389
      0.7793    0.3836    0.6818    0.3406    0.7365    0.4706
      0.2920    0.6831    0.7558    0.0317    0.2236    0.5256
      0.9738    0.0331    0.1000    0.6102    0.7183    0.8500
      0.2413    0.6586    0.0248    0.0910    0.9187    0.3866
   
   
      0.7793
      0.9738
      0.8751
      0.6831
      0.6586
      0.6818
      0.7558
      0.6784
      0.6102
      0.7365
      0.7183
      0.9187
      0.5256
      0.8500
   

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

   
      8.4671    0.5478    6.5368    7.9511    5.3551    4.4509
      7.6180    6.7529    1.6547    2.0864    7.5963    6.2060
      4.0601    0.0578    6.3322    9.6614    8.4027    0.7997
      0.1365    8.8526    6.6755    1.6432    1.8150    1.3175
      8.5780    2.7567    7.6715    5.7625    6.0429    3.1556
   
   
      8.4671    0.0000    6.5368    7.9511    5.3551    0.0000
      7.6180    6.7529    0.0000    0.0000    7.5963    6.2060
      0.0000    0.0000    6.3322    9.6614    8.4027    0.0000
      0.0000    8.8526    6.6755    0.0000    0.0000    0.0000
      8.5780    0.0000    7.6715    5.7625    6.0429    0.0000
   
   
      8.4671    0.0000    6.5368    7.9511    5.3551    0.0000
      7.6180    6.7529    0.0000    0.0000    7.5963    6.2060
      0.0000    0.0000    6.3322       NaN    8.4027    0.0000
      0.0000    8.8526    6.6755    0.0000    0.0000    0.0000
      8.5780    0.0000    7.6715    5.7625    6.0429    0.0000
   

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

   
      9.2885    3.7611    2.4613    1.4284    2.1089    4.2453
      1.6116    6.5000    9.8486    2.4021    4.5061    6.5000
      4.0088    1.5645    1.1427    0.5293    3.4332    2.7318
      0.2252    1.7085    0.5246    1.0685    0.4131    1.5005
      6.5000    6.5000    6.5000    4.6261    9.0112    1.8403
   
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
   
