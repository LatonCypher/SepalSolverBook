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
      0.2006    0.8367    0.0138    0.9180
   
   R1[2] = 0.013752402937567187
   C1 = 
      0.8693
      0.5904
      0.9082
      0.8395
      0.1128
      0.2472
      0.0597
      0.0053
   
   C1[5] = 0.2471661435951048

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
      0.2668    0.0275    0.2854    0.2631    0.2192
      0.3811    0.2801    0.6094    0.6469    0.6045
   

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
   
      0.9971    0.1367    0.7637    0.9546    0.5072    0.8595    0.3334    0.5987
      0.5239    0.0218    0.1570    0.6689    0.4350    0.2428    0.5109    0.5249
      0.4014    0.6318    0.4941    0.8292    0.1690    0.2051    0.6886    0.9904
      0.3842    0.5395    0.0658    0.3385    0.5181    0.2888    0.6226    0.8878
      0.8465    0.3248    0.6218    0.1258    0.4219    0.8254    0.8841    0.9329
      0.7892    0.5671    0.3084    0.8399    0.0543    0.7432    0.1688    0.0523
      0.8380    0.5707    0.9731    0.4636    0.9012    0.3511    0.5523    0.2914
      0.6141    0.5906    0.4765    0.4394    0.1247    0.4453    0.8969    0.4795
   
   B = 
   
      0.4347    0.6755    0.6289    0.0909    0.0521    0.8406    0.8681    0.3887
      0.1791    0.8139    0.9369    0.0549    0.4457    0.4558    0.5085    0.4934
      0.5668    0.1572    0.5072    0.0383    0.4499    0.4834    0.2655    0.1280
      0.9665    0.7247    0.6730    0.3709    0.9593    0.8550    0.5684    0.7608
      0.9365    0.3412    0.1692    0.2075    0.4500    0.9381    0.9537    0.7195
      0.1695    0.2649    0.4875    0.3576    0.3421    0.6700    0.8059    0.1268
      0.6750    0.2761    0.7255    0.0153    0.8884    0.3244    0.7447    0.5839
      0.5707    0.1277    0.9962    0.8877    0.4793    0.3617    0.0510    0.3231
   
   C = 
   
      3.0008    2.1659    3.1281    1.4308    2.4776    3.4622    3.1357    2.1410
      2.0601    1.3020    1.9653    0.9538    1.7336    2.0245    1.9056    1.5551
      2.5922    1.8925    3.2678    1.3959    2.5529    2.4507    2.1619    2.0314
      2.0892    1.4928    2.5728    1.2011    1.9254    2.0926    2.0534    1.7412
      2.5644    1.7509    3.2813    1.3897    2.2941    2.8408    2.9101    1.8904
      1.7519    1.9206    2.2954    0.7523    1.6921    2.4118    2.3119    1.5138
      2.9087    2.1097    2.8818    0.8964    2.3366    3.1965    3.1081    2.1941
      2.1388    1.7581    2.8434    0.8940    2.1661    2.2711    2.3797    1.7501
   
   D = 
   
      3.0008    2.1659    3.1281    1.4308    2.4776    3.4622    3.1357    2.1410
      2.0601    1.3020    1.9653    0.9538    1.7336    2.0245    1.9056    1.5551
      2.5922    1.8925    3.2678    1.3959    2.5529    2.4507    2.1619    2.0314
      2.0892    1.4928    2.5728    1.2011    1.9254    2.0926    2.0534    1.7412
      2.5644    1.7509    3.2813    1.3897    2.2941    2.8408    2.9101    1.8904
      1.7519    1.9206    2.2954    0.7523    1.6921    2.4118    2.3119    1.5138
      2.9087    2.1097    2.8818    0.8964    2.3366    3.1965    3.1081    2.1941
      2.1388    1.7581    2.8434    0.8940    2.1661    2.2711    2.3797    1.7501
   


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

   
      0.2620    0.6398    0.1806    0.1801    0.9683    0.8402
      0.6221    0.3415    0.8540    0.9258    0.2293    0.3699
      0.8214    0.9719    0.6405    0.8944    0.2538    0.4818
      0.2055    0.0013    0.1153    0.3678    0.8597    0.9513
      0.9127    0.0672    0.6345    0.8481    0.4872    0.0595
   
   
      0.6221
      0.8214
      0.9127
      0.6398
      0.9719
      0.8540
      0.6405
      0.6345
      0.9258
      0.8944
      0.8481
      0.9683
      0.8597
      0.8402
      0.9513
   

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

   
      3.5553    2.6015    6.5500    9.3732    0.2263    6.2052
      6.3069    3.4486    6.7731    5.7941    3.6768    0.5779
      6.4123    1.0646    7.7411    8.2135    2.6735    1.5324
      8.1129    3.4542    3.0533    3.1194    8.6510    2.1666
      9.3815    1.6032    2.5368    2.9606    0.8785    2.2053
   
   
      0.0000    0.0000    6.5500    9.3732    0.0000    6.2052
      6.3069    0.0000    6.7731    5.7941    0.0000    0.0000
      6.4123    0.0000    7.7411    8.2135    0.0000    0.0000
      8.1129    0.0000    0.0000    0.0000    8.6510    0.0000
      9.3815    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    6.5500       NaN    0.0000    6.2052
      6.3069    0.0000    6.7731    5.7941    0.0000    0.0000
      6.4123    0.0000    7.7411    8.2135    0.0000    0.0000
      8.1129    0.0000    0.0000    0.0000    8.6510    0.0000
         NaN    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      2.7580    8.7475    6.5000    6.5000    6.5000    0.9407
      4.5560    8.7622    2.2920    6.5000    4.7368    9.7774
      9.6790    1.5823    0.5932    6.5000    6.5000    8.5519
      0.3249    9.6176    1.7050    2.5644    4.1727    0.6072
      6.5000    9.7533    6.5000    0.7636    8.6281    2.8423
   
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
   
