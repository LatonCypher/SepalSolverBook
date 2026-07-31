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
      0.2859    0.5063    0.2760    0.5713
   
   R1[2] = 0.2759669520066358
   C1 = 
      0.9361
      0.2264
      0.0041
      0.1063
      0.0701
      0.6564
      0.1551
      0.7845
   
   C1[5] = 0.6563845184059525

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
      0.0362    0.2858    0.5307    0.9413    0.2767
      0.8947    0.0741    0.0210    0.4719    0.1608
   

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
   
      0.2632    0.4326    0.9700    0.2914    0.3809    0.9956    0.8124    0.8965
      0.1859    0.4246    0.6878    0.8104    0.8061    0.4775    0.9159    0.0063
      0.7632    0.5961    0.2910    0.9276    0.8277    0.1424    0.7395    0.0959
      0.2857    0.1419    0.4589    0.2159    0.1512    0.9363    0.3190    0.1581
      0.5320    0.1552    0.6511    0.9584    0.5026    0.5876    0.0818    0.6731
      0.7522    0.4072    0.3389    0.3617    0.4379    0.6431    0.9967    0.4586
      0.1109    0.8373    0.1888    0.8651    0.1132    0.6983    0.6632    0.6358
      0.1211    0.6105    0.0640    0.5288    0.7824    0.6425    0.2976    0.8883
   
   B = 
   
      0.4647    0.2599    0.4276    0.5298    0.4029    0.1270    0.6654    0.0668
      0.6436    0.3628    0.2886    0.5332    0.0979    0.5678    0.8364    0.7010
      0.3390    0.7370    0.7657    0.9860    0.8647    0.3245    0.3242    0.4444
      0.7724    0.6714    0.3131    0.0946    0.4158    0.6225    0.9477    0.5307
      0.0916    0.2130    0.2145    0.1996    0.3079    0.2146    0.0005    0.5246
      0.0259    0.4160    0.6101    0.6898    0.1362    0.4021    0.3287    0.2936
      0.0019    0.3676    0.7389    0.4917    0.6744    0.6271    0.5855    0.5493
      0.3170    0.4702    0.4725    0.6640    0.1660    0.8317    0.9189    0.2727
   
   C = 
   
      1.3011    2.3515    2.7843    3.1116    2.0580    2.5124    2.7545    2.0893
      1.3087    1.9634    2.1264    2.0245    1.9802    1.9370    2.1692    2.1136
      1.6647    1.8044    1.8678    1.7877    1.7921    1.8857    2.5482    1.9987
      0.6353    1.2222    1.4962    1.6378    1.0311    1.1406    1.3020    1.0095
      1.5829    2.0160    1.9158    2.0902    1.5928    1.9189    2.4630    1.6069
      1.2099    1.7787    2.2513    2.3097    1.7572    2.0209    2.5103    1.7692
      1.5539    1.9100    1.9452    2.0257    1.3324    2.3391    2.8573    1.9393
      1.2498    1.6163    1.6420    1.8384    1.0604    2.0637    2.3153    1.7498
   
   D = 
   
      1.3011    2.3515    2.7843    3.1116    2.0580    2.5124    2.7545    2.0893
      1.3087    1.9634    2.1264    2.0245    1.9802    1.9370    2.1692    2.1136
      1.6647    1.8044    1.8678    1.7877    1.7921    1.8857    2.5482    1.9987
      0.6353    1.2222    1.4962    1.6378    1.0311    1.1406    1.3020    1.0095
      1.5829    2.0160    1.9158    2.0902    1.5928    1.9189    2.4630    1.6069
      1.2099    1.7787    2.2513    2.3097    1.7572    2.0209    2.5103    1.7692
      1.5539    1.9100    1.9452    2.0257    1.3324    2.3391    2.8573    1.9393
      1.2498    1.6163    1.6420    1.8384    1.0604    2.0637    2.3153    1.7498
   


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

   
      0.3910    0.5229    0.2896    0.5394    0.3766    0.5973
      0.6113    0.4515    0.6170    0.4833    0.7084    0.5319
      0.6423    0.3539    0.8693    0.8986    0.1724    0.8174
      0.8415    0.3130    0.7130    0.0322    0.4335    0.3863
      0.4149    0.6411    0.2916    0.9535    0.7643    0.8082
   
   
      0.6113
      0.6423
      0.8415
      0.5229
      0.6411
      0.6170
      0.8693
      0.7130
      0.5394
      0.8986
      0.9535
      0.7084
      0.7643
      0.5973
      0.5319
      0.8174
      0.8082
   

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

   
      7.0160    5.5380    6.5859    5.1712    3.9633    6.1686
      9.0962    5.5201    4.8503    5.2906    5.6851    8.4223
      5.9949    5.1127    2.2495    2.0334    1.6061    8.6863
      9.0379    9.8118    0.0150    2.2633    4.3944    0.8293
      4.6097    7.4510    2.4840    7.2425    6.8802    9.0402
   
   
      7.0160    5.5380    6.5859    5.1712    0.0000    6.1686
      9.0962    5.5201    0.0000    5.2906    5.6851    8.4223
      5.9949    5.1127    0.0000    0.0000    0.0000    8.6863
      9.0379    9.8118    0.0000    0.0000    0.0000    0.0000
      0.0000    7.4510    0.0000    7.2425    6.8802    9.0402
   
   
      7.0160    5.5380    6.5859    5.1712    0.0000    6.1686
         NaN    5.5201    0.0000    5.2906    5.6851    8.4223
      5.9949    5.1127    0.0000    0.0000    0.0000    8.6863
         NaN       NaN    0.0000    0.0000    0.0000    0.0000
      0.0000    7.4510    0.0000    7.2425    6.8802       NaN
   

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

   
      8.3443    0.3375    9.8564    2.0759    9.9960    8.4840
      8.5184    6.5000    6.5000    3.1074    0.1607    3.5019
      3.4995    3.8520    6.5000    3.1813    6.5000    1.1570
      8.0417    6.5000    3.9265    1.4800    9.3214    3.7116
      3.3860    9.7850    3.0423    2.1318    4.4647    6.5000
   
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
   
