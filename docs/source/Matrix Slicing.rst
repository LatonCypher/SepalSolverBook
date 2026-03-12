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
      0.7021    0.5659    0.0570    0.6903
   
   R1[2] = 0.05697364596012655
   C1 = 
      0.8603
      0.6331
      0.9047
      0.1759
      0.8206
      0.2566
      0.7122
      0.8112
   
   C1[5] = 0.2566403413005488

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
      0.6855    0.3742    0.9864    0.4057    0.8635
      0.4307    0.0208    0.1966    0.1631    0.1338
   

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
   
      0.2074    0.5760    0.4354    0.0848    0.0625    0.2957    0.0707    0.9093
      0.3921    0.4765    0.4506    0.2542    0.5863    0.9678    0.8028    0.3349
      0.4899    0.4218    0.4588    0.1151    0.1022    0.3020    0.8952    0.5601
      0.0661    0.9307    0.2135    0.2790    0.6817    0.5993    0.4545    0.0666
      0.6343    0.5737    0.0919    0.6515    0.4668    0.0959    0.0310    0.7814
      0.1696    0.4013    0.1938    0.9195    0.5115    0.1040    0.8150    0.8645
      0.4420    0.3817    0.2687    0.0094    0.3492    0.0562    0.7661    0.8030
      0.6760    0.4334    0.1690    0.3069    0.4130    0.8569    0.0411    0.5923
   
   B = 
   
      0.8300    0.2107    0.2835    0.3443    0.5950    0.2128    0.8176    0.1214
      0.2403    0.3615    0.4975    0.8181    0.7838    0.1958    0.8816    0.6936
      0.4603    0.0491    0.5943    0.6465    0.7093    0.9230    0.1747    0.2364
      0.6888    0.5973    0.2141    0.6230    0.3196    0.5268    0.0898    0.8162
      0.3839    0.4968    0.8738    0.2810    0.8963    0.1934    0.6972    0.3838
      0.8189    0.7181    0.9167    0.3887    0.4735    0.3928    0.9178    0.4867
      0.1139    0.4467    0.3676    0.9031    0.0096    0.0359    0.7685    0.0732
      0.5956    0.4581    0.3566    0.8055    0.3896    0.9895    0.4806    0.9057
   
   C = 
   
      1.3851    1.0155    1.2981    1.8057    1.4617    1.6339    1.5673    1.5934
      2.1310    1.9271    2.4844    2.5103    2.1296    1.5803    2.9172    1.7502
      1.5206    1.2711    1.5410    2.2878    1.4457    1.3957    2.1685    1.3135
      1.4129    1.5301    2.0041    1.9847    1.9344    0.9898    2.3435    1.5786
      1.8820    1.4072    1.4450    1.9788    1.8689    1.5775    1.9117    1.9642
      1.8489    1.8284    1.7098    2.7013    1.6990    1.8024    2.1027    2.1848
      1.3345    1.1739    1.4015    2.1027    1.4156    1.3335    2.0155    1.3344
      2.1721    1.6010    1.9460    1.8512    1.9669    1.5504    2.3825    1.7882
   
   D = 
   
      1.3851    1.0155    1.2981    1.8057    1.4617    1.6339    1.5673    1.5934
      2.1310    1.9271    2.4844    2.5103    2.1296    1.5803    2.9172    1.7502
      1.5206    1.2711    1.5410    2.2878    1.4457    1.3957    2.1685    1.3135
      1.4129    1.5301    2.0041    1.9847    1.9344    0.9898    2.3435    1.5786
      1.8820    1.4072    1.4450    1.9788    1.8689    1.5775    1.9117    1.9642
      1.8489    1.8284    1.7098    2.7013    1.6990    1.8024    2.1027    2.1848
      1.3345    1.1739    1.4015    2.1027    1.4156    1.3335    2.0155    1.3344
      2.1721    1.6010    1.9460    1.8512    1.9669    1.5504    2.3825    1.7882
   


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

   
      0.8011    0.4228    0.2557    0.8414    0.7178    0.6477
      0.6026    0.0854    0.7197    0.3853    0.2454    0.8147
      0.0173    0.0279    0.9544    0.4937    0.9063    0.1925
      0.1352    0.0570    0.7875    0.6821    0.3257    0.3417
      0.6015    0.5376    0.6379    0.7178    0.8718    0.1555
   
   
      0.8011
      0.6026
      0.6015
      0.5376
      0.7197
      0.9544
      0.7875
      0.6379
      0.8414
      0.6821
      0.7178
      0.7178
      0.9063
      0.8718
      0.6477
      0.8147
   

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

   
      1.6864    0.9616    6.7926    9.6473    0.6828    9.6072
      4.4995    4.1307    2.4498    3.6447    6.1796    4.9848
      7.0896    4.7157    3.4065    3.7609    5.7757    5.1741
      5.7539    1.0093    4.5324    7.3328    6.8682    4.0493
      9.6003    2.9732    9.6419    0.9539    3.2045    4.3996
   
   
      0.0000    0.0000    6.7926    9.6473    0.0000    9.6072
      0.0000    0.0000    0.0000    0.0000    6.1796    0.0000
      7.0896    0.0000    0.0000    0.0000    5.7757    5.1741
      5.7539    0.0000    0.0000    7.3328    6.8682    0.0000
      9.6003    0.0000    9.6419    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    6.7926       NaN    0.0000       NaN
      0.0000    0.0000    0.0000    0.0000    6.1796    0.0000
      7.0896    0.0000    0.0000    0.0000    5.7757    5.1741
      5.7539    0.0000    0.0000    7.3328    6.8682    0.0000
         NaN    0.0000       NaN    0.0000    0.0000    0.0000
   

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

   
      6.5000    6.5000    6.5000    3.6123    3.3110    2.0086
      8.7803    4.3512    3.7705    9.7277    4.5795    4.1944
      0.5035    1.0133    4.5572    9.0394    2.6630    1.6379
      6.5000    2.2709    6.5000    9.6500    1.9006    9.4745
      3.4401    0.1543    6.5000    0.9058    3.7600    3.3385
   
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
   
