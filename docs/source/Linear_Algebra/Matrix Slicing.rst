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
      0.2693    0.4863    0.6483    0.8798
   
   R1[2] = 0.6483482784182707
   C1 = 
      0.8055
      0.3102
      0.1506
      0.9640
      0.8480
      0.7553
      0.1745
      0.9582
   
   C1[5] = 0.7553389757846533

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
      0.0523    0.9615    0.9155    0.0410    0.6344
      0.8145    0.3308    0.5284    0.5196    0.9003
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.5052    0.9339    0.5106    0.8236    0.9680    0.2644    0.9492    0.9595
      0.3249    0.0605    0.3652    0.3576    0.4189    0.5511    0.5035    0.1718
      0.5948    0.3329    0.0830    0.8238    0.1791    0.3011    0.9239    0.2060
      0.8774    0.0775    0.9060    0.9407    0.9751    0.8476    0.0430    0.8282
      0.7152    0.6292    0.0477    0.4217    0.3996    0.8414    0.2204    0.1074
      0.1433    0.0652    0.8637    0.9757    0.2159    0.0649    0.7328    0.7513
      0.5932    0.2809    0.0426    0.6008    0.8924    0.3221    0.9406    0.1519
      0.5429    0.1325    0.4128    0.3617    0.6572    0.6893    0.5302    0.5594
   
   B = 
   
      0.3646    0.8816    0.6403    0.9567    0.3669    0.4792    0.9401    0.6191
      0.1229    0.1518    0.2608    0.1223    0.0459    0.5107    0.9307    0.5865
      0.7076    0.7949    0.9072    0.0588    0.2139    0.0393    0.0714    0.2920
      0.7268    0.9716    0.3719    0.1352    0.8915    0.7144    0.9451    0.3839
      0.9365    0.5969    0.7705    0.3384    0.9066    0.4961    0.0474    0.7151
      0.1696    0.6781    0.5127    0.4160    0.7989    0.6312    0.9869    0.4425
      0.1392    0.2726    0.9118    0.2663    0.1336    0.4396    0.9277    0.8453
      0.9482    0.5773    0.3848    0.6713    0.1665    0.8994    0.6623    0.6424
   
   C = 
   
      3.2521    3.3630    3.4527    2.0733    2.4469    3.2549    3.9818    3.5536
      1.3629    1.7935    1.8186    1.0084    1.4347    1.3879    1.8704    1.5599
      1.4579    2.1231    2.0634    1.2962    1.5463    1.9171    2.9526    2.0785
      3.5023    4.0660    3.2976    2.2793    3.0626    2.9502    3.3218    2.8550
      1.3278    2.1048    1.8037    1.4371    1.7592    1.8901    2.7850    1.9011
      2.4082    2.5773    2.4122    1.1274    1.5808    1.9790    2.4309    2.0389
      1.8829    2.2783    2.4840    1.4740    1.9925    2.0548    2.7234    2.4484
      2.1058    2.5055    2.4497    1.6346    1.9264    2.0999    2.5789    2.2557
   
   D = 
   
      3.2521    3.3630    3.4527    2.0733    2.4469    3.2549    3.9818    3.5536
      1.3629    1.7935    1.8186    1.0084    1.4347    1.3879    1.8704    1.5599
      1.4579    2.1231    2.0634    1.2962    1.5463    1.9171    2.9526    2.0785
      3.5023    4.0660    3.2976    2.2793    3.0626    2.9502    3.3218    2.8550
      1.3278    2.1048    1.8037    1.4371    1.7592    1.8901    2.7850    1.9011
      2.4082    2.5773    2.4122    1.1274    1.5808    1.9790    2.4309    2.0389
      1.8829    2.2783    2.4840    1.4740    1.9925    2.0548    2.7234    2.4484
      2.1058    2.5055    2.4497    1.6346    1.9264    2.0999    2.5789    2.2557
   


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

   
      0.7934    0.3082    0.8281    0.0712    0.7556    0.0656
      0.5181    0.0503    0.4377    0.6229    0.0825    0.8301
      0.7199    0.7907    0.8222    0.1240    0.0230    0.8782
      0.6090    0.0993    0.8792    0.8873    0.9817    0.8358
      0.3623    0.1597    0.3176    0.6958    0.6656    0.3238
   
   
      0.7934
      0.5181
      0.7199
      0.6090
      0.7907
      0.8281
      0.8222
      0.8792
      0.6229
      0.8873
      0.6958
      0.7556
      0.9817
      0.6656
      0.8301
      0.8782
      0.8358
   

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

   
      1.8572    2.4440    5.0441    8.4468    4.4309    2.1679
      2.4268    0.8894    1.3605    4.7508    3.0092    5.4508
      8.5968    1.8819    1.6651    9.5743    2.6741    9.9187
      5.6188    2.3823    2.5808    1.3744    6.8498    0.6979
      9.0161    1.1915    8.7349    9.1937    8.4936    6.5273
   
   
      0.0000    0.0000    5.0441    8.4468    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    5.4508
      8.5968    0.0000    0.0000    9.5743    0.0000    9.9187
      5.6188    0.0000    0.0000    0.0000    6.8498    0.0000
      9.0161    0.0000    8.7349    9.1937    8.4936    6.5273
   
   
      0.0000    0.0000    5.0441    8.4468    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    5.4508
      8.5968    0.0000    0.0000       NaN    0.0000       NaN
      5.6188    0.0000    0.0000    0.0000    6.8498    0.0000
         NaN    0.0000    8.7349       NaN    8.4936    6.5273
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      6.5000    3.3176    6.5000    0.7933    6.5000    0.4489
      8.4288    0.7942    0.4366    6.5000    4.4026    9.6624
      1.4027    4.5796    1.3903    2.4815    9.0973    3.4406
      6.5000    4.5995    9.1260    8.6113    6.5000    3.3646
      6.5000    1.1007    0.3860    6.5000    2.7534    1.9744
   
Advantages
^^^^^^^^^^


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
   
