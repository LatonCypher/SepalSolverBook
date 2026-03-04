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
      0.4873    0.7839    0.8235    0.3997
   
   R1[2] = 0.8234749387350146
   C1 = 
      0.6213
      0.3154
      0.0412
      0.4852
      0.2604
      0.5226
      0.3599
      0.4605
   
   C1[5] = 0.5225992198399546

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
      0.5810    0.4536    0.1266    0.6221    0.6650
      0.8682    0.4288    0.5693    0.1146    0.8417
   

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
   
      0.9490    0.7050    0.1154    0.1200    0.3880    0.6753    0.6449    0.4696
      0.7626    0.3414    0.1182    0.9693    0.1055    0.1853    0.5198    0.5091
      0.8468    0.4804    0.4027    0.4881    0.9688    0.4842    0.1642    0.9601
      0.3431    0.2212    0.5240    0.0703    0.0014    0.7251    0.1217    0.7496
      0.1599    0.3721    0.0959    0.3714    0.9569    0.3749    0.1964    0.1064
      0.1783    0.6755    0.9513    0.5522    0.0107    0.6258    0.4249    0.1044
      0.3507    0.3795    0.2924    0.6803    0.2618    0.5070    0.0218    0.6096
      0.8403    0.5254    0.8544    0.5817    0.5502    0.8909    0.4320    0.2499
   
   B = 
   
      0.7094    0.6376    0.8525    0.0610    0.6044    0.4054    0.8147    0.2653
      0.8161    0.9202    0.2564    0.6196    0.1250    0.6473    0.6898    0.0220
      0.9164    0.1218    0.1941    0.0655    0.3990    0.3617    0.2027    0.3737
      0.9092    0.6407    0.2777    0.3238    0.8135    0.6159    0.4827    0.3439
      0.3560    0.3240    0.0537    0.2854    0.6374    0.2769    0.7663    0.0834
      0.0069    0.7501    0.7902    0.5961    0.7803    0.0504    0.1255    0.9973
      0.1789    0.2057    0.4539    0.8132    0.8710    0.6340    0.7930    0.1972
      0.4143    0.9057    0.1284    0.6062    0.0311    0.3117    0.8983    0.5375
   
   C = 
   
      1.9162    2.5349    1.9529    1.8634    2.1559    1.6534    2.6559    1.4371
      2.1520    2.1771    1.4831    1.4516    2.0198    1.6967    2.3222    1.1572
      2.5810    2.9242    1.6912    1.8144    2.2978    1.7966    3.1342    1.6658
      1.3059    1.7794    1.1949    1.2010    1.1972    0.8628    1.4341    1.4660
      1.2651    1.4220    0.8039    1.0876    1.5604    1.0106    1.6172    0.7639
      2.1790    1.8598    1.3645    1.4554    1.8895    1.5302    1.5881    1.3724
      1.7982    2.0661    1.1448    1.2601    1.5300    1.2145    1.7643    1.3040
      2.7197    2.6579    2.1401    1.8120    2.8176    1.8972    2.6015    1.9078
   
   D = 
   
      1.9162    2.5349    1.9529    1.8634    2.1559    1.6534    2.6559    1.4371
      2.1520    2.1771    1.4831    1.4516    2.0198    1.6967    2.3222    1.1572
      2.5810    2.9242    1.6912    1.8144    2.2978    1.7966    3.1342    1.6658
      1.3059    1.7794    1.1949    1.2010    1.1972    0.8628    1.4341    1.4660
      1.2651    1.4220    0.8039    1.0876    1.5604    1.0106    1.6172    0.7639
      2.1790    1.8598    1.3645    1.4554    1.8895    1.5302    1.5881    1.3724
      1.7982    2.0661    1.1448    1.2601    1.5300    1.2145    1.7643    1.3040
      2.7197    2.6579    2.1401    1.8120    2.8176    1.8972    2.6015    1.9078
   


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

   
      0.5643    0.6641    0.9964    0.7912    0.0640    0.8430
      0.3789    0.5529    0.5412    0.7120    0.1844    0.7468
      0.6555    0.0246    0.2041    0.6205    0.8609    0.8425
      0.7067    0.8370    0.1842    0.0542    0.8569    0.2607
      0.6908    0.1218    0.2968    0.3916    0.3259    0.2680
   
   
      0.5643
      0.6555
      0.7067
      0.6908
      0.6641
      0.5529
      0.8370
      0.9964
      0.5412
      0.7912
      0.7120
      0.6205
      0.8609
      0.8569
      0.8430
      0.7468
      0.8425
   

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

   
      8.1762    3.1411    6.0838    8.2603    7.0403    9.6482
      6.3499    1.6564    2.2915    3.0066    3.2926    5.8956
      4.6964    4.0221    4.1292    4.8582    1.1933    5.0873
      6.3467    1.7008    9.6404    4.1019    8.0142    1.5698
      3.6581    3.1196    1.2462    6.9725    5.0123    2.2096
   
   
      8.1762    0.0000    6.0838    8.2603    7.0403    9.6482
      6.3499    0.0000    0.0000    0.0000    0.0000    5.8956
      0.0000    0.0000    0.0000    0.0000    0.0000    5.0873
      6.3467    0.0000    9.6404    0.0000    8.0142    0.0000
      0.0000    0.0000    0.0000    6.9725    5.0123    0.0000
   
   
      8.1762    0.0000    6.0838    8.2603    7.0403       NaN
      6.3499    0.0000    0.0000    0.0000    0.0000    5.8956
      0.0000    0.0000    0.0000    0.0000    0.0000    5.0873
      6.3467    0.0000       NaN    0.0000    8.0142    0.0000
      0.0000    0.0000    0.0000    6.9725    5.0123    0.0000
   

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

   
      4.8170    3.6358    4.6082    4.1872    1.7544    0.0434
      1.5976    9.4350    3.5075    3.1406    4.1560    9.1688
      2.5154    6.5000    9.4670    9.8354    6.5000    6.5000
      4.8384    2.2000    2.2721    0.9665    6.5000    0.9671
      8.2273    3.7397    2.0026    4.1351    8.3286    6.5000
   
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
   
