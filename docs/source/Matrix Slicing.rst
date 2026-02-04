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
      0.6145    0.5195    0.8054    0.4455
   
   R1[2] = 0.8054047531626372
   C1 = 
      0.6828
      0.7434
      0.8897
      0.1982
      0.3544
      0.6495
      0.3116
      0.3476
   
   C1[5] = 0.6495046917253431

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
      0.0594    0.5826    0.1093    0.8280    0.2915
      0.5099    0.5248    0.8698    0.6727    0.3300
   

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
   
      0.2893    0.3074    0.9856    0.7299    0.3770    0.9007    0.6270    0.2481
      0.7977    0.7364    0.0401    0.5632    0.6702    0.3794    0.6746    0.7985
      0.7617    0.9060    0.6442    0.2528    0.5964    0.6079    0.4266    0.9579
      0.9717    0.0983    0.3565    0.3783    0.7263    0.1156    0.3525    0.8060
      0.6902    0.9653    0.7152    0.2389    0.3685    0.4503    0.6699    0.8518
      0.6014    0.0384    0.0599    0.8185    0.4615    0.9610    0.6772    0.4999
      0.4657    0.1663    0.3997    0.0291    0.3740    0.9018    0.0358    0.4893
      0.2927    0.7368    0.3037    0.4576    0.6383    0.6911    0.7399    0.8456
   
   B = 
   
      0.3047    0.4675    0.8068    0.7758    0.7456    0.7079    0.1482    0.9380
      0.4256    0.4299    0.0289    0.5814    0.0614    0.6354    0.4265    0.6814
      0.9293    0.7707    0.8719    0.6667    0.4051    0.8956    0.6510    0.3090
      0.7212    0.7428    0.9002    0.5671    0.1099    0.4654    0.1460    0.5694
      0.1055    0.4891    0.5741    0.7851    0.5860    0.0049    0.1411    0.5404
      0.3421    0.6408    0.5330    0.6800    0.6915    0.3885    0.3580    0.0209
      0.4693    0.2606    0.5126    0.2053    0.8965    0.0504    0.6842    0.8731
      0.1897    0.3347    0.4395    0.1215    0.8163    0.8866    0.5462    0.7221
   
   C = 
   
      2.3506    2.5772    2.8857    2.5415    2.3225    2.2259    1.8624    2.1502
      1.6685    2.1528    2.4906    2.4127    2.6298    2.2233    1.6687    3.1189
      2.0515    2.5429    2.7360    2.7760    2.8465    2.9193    2.0724    3.0740
      1.3766    1.8433    2.4517    2.0822    2.3960    2.0265    1.2985    2.5887
      2.1271    2.3949    2.5928    2.5455    2.7132    2.8194    2.1513    3.0708
      1.6357    2.1371    2.6193    2.2084    2.5151    1.7377    1.4096    2.2966
      1.0626    1.5527    1.6840    1.7145    1.7968    1.5946    1.0717    1.2958
      1.8265    2.2585    2.4199    2.3431    2.6423    2.2190    1.9277    2.7470
   
   D = 
   
      2.3506    2.5772    2.8857    2.5415    2.3225    2.2259    1.8624    2.1502
      1.6685    2.1528    2.4906    2.4127    2.6298    2.2233    1.6687    3.1189
      2.0515    2.5429    2.7360    2.7760    2.8465    2.9193    2.0724    3.0740
      1.3766    1.8433    2.4517    2.0822    2.3960    2.0265    1.2985    2.5887
      2.1271    2.3949    2.5928    2.5455    2.7132    2.8194    2.1513    3.0708
      1.6357    2.1371    2.6193    2.2084    2.5151    1.7377    1.4096    2.2966
      1.0626    1.5527    1.6840    1.7145    1.7968    1.5946    1.0717    1.2958
      1.8265    2.2585    2.4199    2.3431    2.6423    2.2190    1.9277    2.7470
   


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

   
      0.4306    0.2945    0.8943    0.9241    0.3641    0.3491
      0.9290    0.4298    0.6343    0.5868    0.1433    0.9558
      0.5972    0.6455    0.4578    0.3865    0.9574    0.3150
      0.7427    0.5513    0.9069    0.3718    0.5505    0.2259
      0.7272    0.7601    0.5265    0.7752    0.3133    0.5244
   
   
      0.9290
      0.5972
      0.7427
      0.7272
      0.6455
      0.5513
      0.7601
      0.8943
      0.6343
      0.9069
      0.5265
      0.9241
      0.5868
      0.7752
      0.9574
      0.5505
      0.9558
      0.5244
   

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

   
      0.2176    6.9754    1.2324    6.8371    0.2246    9.0758
      7.6062    7.0344    3.1872    0.7466    9.8494    5.8162
      9.8918    7.0095    5.6127    5.8415    9.0500    4.3055
      3.7240    5.5932    0.1912    4.2978    2.1025    9.8918
      0.8550    7.4416    1.3478    5.6934    1.8367    4.9953
   
   
      0.0000    6.9754    0.0000    6.8371    0.0000    9.0758
      7.6062    7.0344    0.0000    0.0000    9.8494    5.8162
      9.8918    7.0095    5.6127    5.8415    9.0500    0.0000
      0.0000    5.5932    0.0000    0.0000    0.0000    9.8918
      0.0000    7.4416    0.0000    5.6934    0.0000    0.0000
   
   
      0.0000    6.9754    0.0000    6.8371    0.0000       NaN
      7.6062    7.0344    0.0000    0.0000       NaN    5.8162
         NaN    7.0095    5.6127    5.8415       NaN    0.0000
      0.0000    5.5932    0.0000    0.0000    0.0000       NaN
      0.0000    7.4416    0.0000    5.6934    0.0000    0.0000
   

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

   
      6.5000    6.5000    0.7396    6.5000    6.5000    8.6542
      6.5000    6.5000    6.5000    6.5000    0.1772    1.7971
      0.6551    6.5000    0.0646    3.1076    9.1122    1.1773
      6.5000    6.5000    1.7701    4.5745    9.5158    1.0495
      9.8107    6.5000    6.5000    2.2948    3.1422    6.5000
   
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
   
