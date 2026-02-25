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
      0.3618    0.6875    0.9009    0.2787
   
   R1[2] = 0.9008541304061312
   C1 = 
      0.0368
      0.8921
      0.3539
      0.5075
      0.0178
      0.2588
      0.9763
      0.3814
   
   C1[5] = 0.25876746552480734

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
      0.1845    0.0285    0.2152    0.9381    0.3818
      0.1902    0.7564    0.0574    0.4734    0.9687
   

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
   
      0.1037    0.6589    0.9161    0.8389    0.9447    0.2805    0.7456    0.2906
      0.6351    0.3630    0.6367    0.0237    0.4491    0.6169    0.9623    0.6765
      0.4595    0.2082    0.0620    0.3698    0.4910    0.7412    0.9269    0.1296
      0.9618    0.9351    0.4469    0.5097    0.4389    0.8131    0.8857    0.9419
      0.4323    0.7592    0.3893    0.2713    0.4199    0.1485    0.0985    0.4887
      0.6538    0.8570    0.5925    0.2278    0.1272    0.7807    0.4112    0.1465
      0.7331    0.1348    0.6559    0.4710    0.2926    0.4914    0.8407    0.4456
      0.8721    0.0342    0.3521    0.6980    0.3076    0.1196    0.4458    0.8749
   
   B = 
   
      0.4004    0.0865    0.2512    0.3138    0.6897    0.0440    0.5830    0.6735
      0.3838    0.4959    0.9350    0.0598    0.3563    0.3847    0.7756    0.0778
      0.7989    0.8172    0.3603    0.0421    0.6974    0.0818    0.8516    0.7515
      0.9644    0.3803    0.7021    0.2680    0.7549    0.7888    0.0885    0.1679
      0.9680    0.1081    0.8435    0.2282    0.7447    0.5550    0.8612    0.0490
      0.1403    0.0446    0.0872    0.8568    0.4085    0.0319    0.3336    0.4073
      0.2802    0.1608    0.3529    0.7948    0.7321    0.8908    0.7491    0.6997
      0.0524    0.2628    0.0799    0.7209    0.2383    0.6657    0.0475    0.3526
   
   C = 
   
      3.0131    1.7143    2.6688    1.5932    3.0116    2.3855    2.9053    1.7350
      1.7514    1.1729    1.5713    2.1376    2.4815    1.8148    2.5417    2.1236
      1.5158    0.6036    1.4083    1.8355    2.0914    1.6051    1.8854    1.4545
      2.4289    1.5797    2.4637    2.6929    3.2250    2.5262    3.0691    2.4465
      1.5176    1.0315    1.5902    0.9238    1.6070    1.2079    1.7046    1.0107
      1.6393    1.2056    1.6712    1.4727    2.0911    1.1460    2.2556    1.6541
      1.9346    1.1514    1.4992    1.8692    2.5071    1.7329    2.1991    2.0362
      1.8021    0.9858    1.3652    1.6353    2.1991    1.7851    1.5769    1.6560
   
   D = 
   
      3.0131    1.7143    2.6688    1.5932    3.0116    2.3855    2.9053    1.7350
      1.7514    1.1729    1.5713    2.1376    2.4815    1.8148    2.5417    2.1236
      1.5158    0.6036    1.4083    1.8355    2.0914    1.6051    1.8854    1.4545
      2.4289    1.5797    2.4637    2.6929    3.2250    2.5262    3.0691    2.4465
      1.5176    1.0315    1.5902    0.9238    1.6070    1.2079    1.7046    1.0107
      1.6393    1.2056    1.6712    1.4727    2.0911    1.1460    2.2556    1.6541
      1.9346    1.1514    1.4992    1.8692    2.5071    1.7329    2.1991    2.0362
      1.8021    0.9858    1.3652    1.6353    2.1991    1.7851    1.5769    1.6560
   


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

   
      0.5167    0.1348    0.4448    0.1420    0.5857    0.1558
      0.7106    0.1163    0.3225    0.6682    0.0294    0.4142
      0.8618    0.4355    0.5731    0.9757    0.6395    0.3600
      0.9291    0.9190    0.9808    0.9937    0.8510    0.0896
      0.5931    0.0345    0.2808    0.8731    0.3491    0.2005
   
   
      0.5167
      0.7106
      0.8618
      0.9291
      0.5931
      0.9190
      0.5731
      0.9808
      0.6682
      0.9757
      0.9937
      0.8731
      0.5857
      0.6395
      0.8510
   

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

   
      6.7146    7.6679    0.2892    2.0373    4.3338    8.3150
      1.9683    3.3510    4.0034    4.4596    5.0432    4.7460
      4.9821    3.9843    3.4806    4.9266    5.8931    5.7790
      8.2752    3.3097    1.2025    7.3927    3.7219    0.6415
      7.9346    5.0965    6.6011    3.6904    7.8206    7.0861
   
   
      6.7146    7.6679    0.0000    0.0000    0.0000    8.3150
      0.0000    0.0000    0.0000    0.0000    5.0432    0.0000
      0.0000    0.0000    0.0000    0.0000    5.8931    5.7790
      8.2752    0.0000    0.0000    7.3927    0.0000    0.0000
      7.9346    5.0965    6.6011    0.0000    7.8206    7.0861
   
   
      6.7146    7.6679    0.0000    0.0000    0.0000    8.3150
      0.0000    0.0000    0.0000    0.0000    5.0432    0.0000
      0.0000    0.0000    0.0000    0.0000    5.8931    5.7790
      8.2752    0.0000    0.0000    7.3927    0.0000    0.0000
      7.9346    5.0965    6.6011    0.0000    7.8206    7.0861
   

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

   
      6.5000    6.5000    3.8312    6.5000    2.4041    2.0863
      1.4955    9.9111    0.5712    6.5000    3.8819    8.2921
      0.4821    0.5003    9.6262    4.1089    6.5000    0.9143
      6.5000    6.5000    6.5000    4.4753    6.5000    9.8427
      9.5180    6.5000    8.7960    3.7144    2.9358    2.7665
   
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
   
