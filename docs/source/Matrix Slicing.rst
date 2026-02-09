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
      0.3930    0.3385    0.8597    0.3409
   
   R1[2] = 0.8597451757544005
   C1 = 
      0.9983
      0.3630
      0.1664
      0.5073
      0.2250
      0.0268
      0.4104
      0.2571
   
   C1[5] = 0.026811133582633873

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
      0.0634    0.2130    0.1004    0.7724    0.4118
      0.2413    0.3061    0.3743    0.0231    0.5364
   

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
   
      0.4248    0.7698    0.8099    0.2983    0.9309    0.0933    0.2859    0.9067
      0.3239    0.6549    0.0129    0.1277    0.5753    0.8251    0.0462    0.9974
      0.4001    0.1819    0.9578    0.6637    0.4782    0.7543    0.1338    0.5248
      0.8614    0.3047    0.6010    0.1891    0.4885    0.8706    0.4607    0.8866
      0.1665    0.5845    0.3962    0.8332    0.6613    0.3207    0.7172    0.1245
      0.6626    0.6663    0.1901    0.4395    0.2932    0.3703    0.5926    0.5495
      0.6323    0.1628    0.9378    0.4209    0.4390    0.9789    0.0359    0.0436
      0.4872    0.6678    0.3463    0.3764    0.8224    0.8140    0.8590    0.8718
   
   B = 
   
      0.1318    0.3171    0.7154    0.2266    0.3769    0.6448    0.8508    0.2278
      0.9501    0.6897    0.8365    0.8605    0.5654    0.3063    0.5571    0.2891
      0.8101    0.1274    0.4552    0.9166    0.2050    0.1175    0.2507    0.2422
      0.9053    0.8182    0.8363    0.7411    0.8862    0.1879    0.3090    0.2621
      0.8095    0.6426    0.6123    0.2045    0.1229    0.5873    0.1784    0.3824
      0.1685    0.3694    0.8704    0.1788    0.1771    0.1855    0.7837    0.5332
      0.2136    0.9653    0.8816    0.4449    0.4182    0.2234    0.6289    0.7265
      0.5994    0.4013    0.0314    0.6624    0.2702    0.6542    0.8089    0.0477
   
   C = 
   
      3.0874    2.2854    2.4977    2.6569    1.5212    1.8820    2.2380    1.2503
      2.0035    1.7799    2.0347    1.6898    1.1139    1.5887    2.2682    1.0408
      2.4597    1.8430    2.5131    2.2568    1.4283    1.3449    2.0721    1.2569
      2.2331    2.1505    2.7936    2.1962    1.4341    1.8862    2.8881    1.5074
      2.4699    2.4739    2.8055    2.1155    1.6845    1.1791    1.7452    1.4721
      2.0280    2.1712    2.5270    1.9772    1.5528    1.4690    2.2781    1.2711
      1.9328    1.4724    2.5211    1.7645    1.1497    1.1227    1.8971    1.2464
      2.8290    2.9754    3.3763    2.5547    1.8057    2.0263    3.0198    1.9007
   
   D = 
   
      3.0874    2.2854    2.4977    2.6569    1.5212    1.8820    2.2380    1.2503
      2.0035    1.7799    2.0347    1.6898    1.1139    1.5887    2.2682    1.0408
      2.4597    1.8430    2.5131    2.2568    1.4283    1.3449    2.0721    1.2569
      2.2331    2.1505    2.7936    2.1962    1.4341    1.8862    2.8881    1.5074
      2.4699    2.4739    2.8055    2.1155    1.6845    1.1791    1.7452    1.4721
      2.0280    2.1712    2.5270    1.9772    1.5528    1.4690    2.2781    1.2711
      1.9328    1.4724    2.5211    1.7645    1.1497    1.1227    1.8971    1.2464
      2.8290    2.9754    3.3763    2.5547    1.8057    2.0263    3.0198    1.9007
   


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

   
      0.7573    0.5857    0.8444    0.5863    0.7913    0.7087
      0.9239    0.7639    0.7397    0.5844    0.6051    0.5663
      0.4514    0.4565    0.6683    0.9820    0.5972    0.8912
      0.1183    0.8755    0.8032    0.8879    0.5114    0.6027
      0.7389    0.3386    0.7767    0.7412    0.8182    0.3204
   
   
      0.7573
      0.9239
      0.7389
      0.5857
      0.7639
      0.8755
      0.8444
      0.7397
      0.6683
      0.8032
      0.7767
      0.5863
      0.5844
      0.9820
      0.8879
      0.7412
      0.7913
      0.6051
      0.5972
      0.5114
      0.8182
      0.7087
      0.5663
      0.8912
      0.6027
   

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

   
      3.2703    2.1176    3.0429    1.6984    0.5394    9.2411
      8.1864    6.6320    0.0326    3.1653    5.8498    7.6436
      7.4085    3.8508    5.5793    4.6812    9.6894    7.1906
      4.9731    9.6359    0.0828    6.5881    5.1487    7.5518
      0.1355    5.9167    9.1892    2.0411    5.1493    2.7549
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    9.2411
      8.1864    6.6320    0.0000    0.0000    5.8498    7.6436
      7.4085    0.0000    5.5793    0.0000    9.6894    7.1906
      0.0000    9.6359    0.0000    6.5881    5.1487    7.5518
      0.0000    5.9167    9.1892    0.0000    5.1493    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      8.1864    6.6320    0.0000    0.0000    5.8498    7.6436
      7.4085    0.0000    5.5793    0.0000       NaN    7.1906
      0.0000       NaN    0.0000    6.5881    5.1487    7.5518
      0.0000    5.9167       NaN    0.0000    5.1493    0.0000
   

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

   
      3.6601    1.1912    8.6198    1.1629    1.3600    2.6930
      1.6612    1.8520    6.5000    6.5000    6.5000    9.6179
      8.8805    9.4170    0.6439    6.5000    6.5000    6.5000
      8.1337    4.4316    6.5000    8.2489    3.9509    6.5000
      6.5000    0.3343    6.5000    3.5126    6.5000    4.7266
   
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
   
