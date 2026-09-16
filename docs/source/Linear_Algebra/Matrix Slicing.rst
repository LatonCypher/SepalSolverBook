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
      0.8420    0.7814    0.0622    0.3698
   
   R1[2] = 0.06217261084940773
   C1 = 
      0.7743
      0.8562
      0.0471
      0.4656
      0.8969
      0.6127
      0.8902
      0.6962
   
   C1[5] = 0.6126511404148696

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
      0.4271    0.6174    0.9967    0.1889    0.9964
      0.8860    0.8838    0.4318    0.2191    0.7931
   

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
   
      0.9291    0.5044    0.0229    0.9428    0.5907    0.5532    0.4980    0.9333
      0.2057    0.1809    0.3585    0.0658    0.1218    0.0575    0.4338    0.4537
      0.2323    0.5872    0.7463    0.2690    0.7788    0.5966    0.5769    0.1617
      0.1329    0.1593    0.5099    0.2692    0.9813    0.8984    0.9450    0.9805
      0.4218    0.8875    0.4986    0.5025    0.5886    0.3566    0.0559    0.6709
      0.7475    0.5019    0.2790    0.3356    0.6368    0.4623    0.7011    0.1529
      0.2910    0.4245    0.4013    0.3594    0.9666    0.3601    0.4168    0.8296
      0.7319    0.1008    0.2729    0.5467    0.4069    0.5636    0.5457    0.1908
   
   B = 
   
      0.8286    0.6279    0.0766    0.2517    0.1364    0.6702    0.3503    0.1205
      0.3157    0.4289    0.9922    0.4251    0.9439    0.2471    0.7123    0.6485
      0.3962    0.1451    0.8299    0.3655    0.1641    0.2089    0.0328    0.8341
      0.8517    0.4751    0.3261    0.1678    0.3863    0.9499    0.2972    0.3951
      0.5689    0.7868    0.5597    0.9177    0.0249    0.3873    0.7184    0.3254
      0.3602    0.5055    0.1736    0.5317    0.5835    0.2432    0.5507    0.4041
      0.3785    0.0514    0.7173    0.8994    0.9930    0.9725    0.6108    0.0701
      0.3559    0.4093    0.8379    0.9205    0.7792    0.1718    0.0076    0.2324
   
   C = 
   
      2.7970    2.4029    2.4639    2.7580    2.5300    2.6556    2.0059    1.4981
      0.8413    0.6230    1.2837    1.2209    1.1040    0.8810    0.6198    0.6659
      1.8365    1.6441    2.3963    2.3256    1.8788    1.7479    1.8457    1.7101
      2.1801    2.0298    2.8837    3.4633    2.6073    2.1767    2.0410    1.6274
      1.9785    1.8775    2.4842    2.1476    1.9722    1.5679    1.6041    1.7363
      2.0227    1.7179    1.9639    2.1612    1.8521    2.0692    1.8697    1.2595
      1.9728    1.8973    2.4912    2.6776    1.9395    1.7349    1.6778    1.4691
      1.9209    1.5134    1.4378    1.7581    1.4805    1.9499    1.4371    1.0399
   
   D = 
   
      2.7970    2.4029    2.4639    2.7580    2.5300    2.6556    2.0059    1.4981
      0.8413    0.6230    1.2837    1.2209    1.1040    0.8810    0.6198    0.6659
      1.8365    1.6441    2.3963    2.3256    1.8788    1.7479    1.8457    1.7101
      2.1801    2.0298    2.8837    3.4633    2.6073    2.1767    2.0410    1.6274
      1.9785    1.8775    2.4842    2.1476    1.9722    1.5679    1.6041    1.7363
      2.0227    1.7179    1.9639    2.1612    1.8521    2.0692    1.8697    1.2595
      1.9728    1.8973    2.4912    2.6776    1.9395    1.7349    1.6778    1.4691
      1.9209    1.5134    1.4378    1.7581    1.4805    1.9499    1.4371    1.0399
   


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

   
      0.1513    0.6870    0.0708    0.9356    0.7814    0.6586
      0.9638    0.6696    0.9371    0.4387    0.1324    0.7471
      0.7090    0.8780    0.5989    0.4592    0.8625    0.4499
      0.3645    0.5022    0.5792    0.5423    0.1455    0.1249
      0.7581    0.9310    0.7267    0.2869    0.1956    0.8935
   
   
      0.9638
      0.7090
      0.7581
      0.6870
      0.6696
      0.8780
      0.5022
      0.9310
      0.9371
      0.5989
      0.5792
      0.7267
      0.9356
      0.5423
      0.7814
      0.8625
      0.6586
      0.7471
      0.8935
   

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

   
      9.1869    0.3112    9.3345    0.2748    9.6309    8.1712
      2.5494    7.7952    2.1950    2.4492    0.2705    3.2884
      5.1022    9.5041    1.0869    3.4160    7.9021    3.5318
      7.1129    5.5923    4.7585    6.9109    0.8549    4.1338
      6.8583    4.5989    5.5409    4.5108    1.8583    8.1718
   
   
      9.1869    0.0000    9.3345    0.0000    9.6309    8.1712
      0.0000    7.7952    0.0000    0.0000    0.0000    0.0000
      5.1022    9.5041    0.0000    0.0000    7.9021    0.0000
      7.1129    5.5923    0.0000    6.9109    0.0000    0.0000
      6.8583    0.0000    5.5409    0.0000    0.0000    8.1718
   
   
         NaN    0.0000       NaN    0.0000       NaN    8.1712
      0.0000    7.7952    0.0000    0.0000    0.0000    0.0000
      5.1022       NaN    0.0000    0.0000    7.9021    0.0000
      7.1129    5.5923    0.0000    6.9109    0.0000    0.0000
      6.8583    0.0000    5.5409    0.0000    0.0000    8.1718
   

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

   
      2.9430    1.8535    3.1795    6.5000    1.2065    2.7989
      6.5000    9.0757    6.5000    8.7210    0.1672    6.5000
      8.3114    9.7765    9.9209    4.4093    1.5065    2.2929
      2.0283    9.3843    8.6263    9.0421    4.0232    8.0507
      6.5000    1.2166    0.7254    4.1961    4.8540    9.0961
   
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
   
