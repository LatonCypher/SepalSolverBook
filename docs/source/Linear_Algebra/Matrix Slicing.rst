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
      0.1780    0.6076    0.1077    0.0425
   
   R1[2] = 0.10772505015553391
   C1 = 
      0.6496
      0.4179
      0.3669
      0.3010
      0.2850
      0.2509
      0.4659
      0.7088
   
   C1[5] = 0.25092915309941943

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
      0.1732    0.9815    0.8187    0.5201    0.9744
      0.8459    0.8411    0.4793    0.2215    0.9658
   

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
   
      0.1068    0.1820    0.5237    0.2470    0.0945    0.3870    0.3350    0.9412
      0.4429    0.4650    0.6493    0.8531    0.6285    0.5083    0.7913    0.7073
      0.5771    0.1418    0.2465    0.1401    0.2237    0.1723    0.5019    0.9803
      0.9571    0.3971    0.5530    0.1833    0.4587    0.8649    0.0294    0.6168
      0.3076    0.3781    0.9237    0.2744    0.3797    0.2166    0.4406    0.3143
      0.6163    0.1445    0.1570    0.2372    0.9627    0.3898    0.5820    0.2183
      0.1439    0.2538    0.4161    0.6703    0.6221    0.8166    0.9030    0.7807
      0.3771    0.7053    0.3812    0.8829    0.1965    0.7846    0.1407    0.7919
   
   B = 
   
      0.5801    0.0234    0.2223    0.3338    0.5666    0.9722    0.6338    0.5529
      0.6472    0.5221    0.7109    0.3949    0.6086    0.9086    0.1310    0.2455
      0.1652    0.6589    0.6373    0.0861    0.8888    0.5318    0.0336    0.1314
      0.6717    0.4614    0.0330    0.1251    0.2857    0.9537    0.6458    0.5562
      0.7530    0.3752    0.0128    0.8415    0.1562    0.4154    0.9057    0.9296
      0.7805    0.1483    0.7413    0.7128    0.0055    0.2122    0.7199    0.3974
      0.4353    0.2952    0.9991    0.5272    0.9494    0.4774    0.2693    0.6292
      0.9531    0.2029    0.3656    0.0683    0.3774    0.8630    0.5591    0.8857
   
   C = 
   
      1.8484    0.9393    1.4620    0.7798    1.3975    1.8769    1.2493    1.5961
      3.1267    1.7628    2.3049    1.8508    2.4739    3.3690    2.4580    2.8295
      2.0171    0.7711    1.3812    0.9300    1.5548    2.1697    1.4930    1.9247
      2.6478    1.1128    1.7553    1.6069    1.6649    2.6806    2.1863    2.1362
      1.7064    1.3083    1.6554    1.0935    1.9013    2.0809    1.2470    1.5317
      2.1269    0.9379    1.3102    1.7157    1.4320    1.9892    1.9993    2.1383
      3.0097    1.4988    2.3006    1.9029    2.0510    2.7676    2.4023    2.7319
      2.9076    1.4278    1.8715    1.4005    1.7014    3.0509    2.1378    2.2072
   
   D = 
   
      1.8484    0.9393    1.4620    0.7798    1.3975    1.8769    1.2493    1.5961
      3.1267    1.7628    2.3049    1.8508    2.4739    3.3690    2.4580    2.8295
      2.0171    0.7711    1.3812    0.9300    1.5548    2.1697    1.4930    1.9247
      2.6478    1.1128    1.7553    1.6069    1.6649    2.6806    2.1863    2.1362
      1.7064    1.3083    1.6554    1.0935    1.9013    2.0809    1.2470    1.5317
      2.1269    0.9379    1.3102    1.7157    1.4320    1.9892    1.9993    2.1383
      3.0097    1.4988    2.3006    1.9029    2.0510    2.7676    2.4023    2.7319
      2.9076    1.4278    1.8715    1.4005    1.7014    3.0509    2.1378    2.2072
   


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

   
      0.7560    0.9986    0.0612    0.2322    0.2323    0.3190
      0.3322    0.5623    0.1269    0.3794    0.7019    0.0796
      0.6612    0.6869    0.0335    0.3387    0.3569    0.1405
      0.5106    0.5392    0.7184    0.2397    0.1320    0.3004
      0.7386    0.0279    0.9462    0.6633    0.3598    0.0080
   
   
      0.7560
      0.6612
      0.5106
      0.7386
      0.9986
      0.5623
      0.6869
      0.5392
      0.7184
      0.9462
      0.6633
      0.7019
   

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

   
      2.2688    5.1560    8.7644    2.7806    6.2976    4.9249
      9.6428    6.4519    7.2733    2.8188    3.7830    0.6726
      2.2970    1.4991    8.1823    0.8330    1.1774    2.8076
      0.2014    2.6018    5.7684    9.4175    4.6142    5.6969
      0.5517    5.4721    5.6554    6.0286    2.4724    2.4294
   
   
      0.0000    5.1560    8.7644    0.0000    6.2976    0.0000
      9.6428    6.4519    7.2733    0.0000    0.0000    0.0000
      0.0000    0.0000    8.1823    0.0000    0.0000    0.0000
      0.0000    0.0000    5.7684    9.4175    0.0000    5.6969
      0.0000    5.4721    5.6554    6.0286    0.0000    0.0000
   
   
      0.0000    5.1560    8.7644    0.0000    6.2976    0.0000
         NaN    6.4519    7.2733    0.0000    0.0000    0.0000
      0.0000    0.0000    8.1823    0.0000    0.0000    0.0000
      0.0000    0.0000    5.7684       NaN    0.0000    5.6969
      0.0000    5.4721    5.6554    6.0286    0.0000    0.0000
   

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

   
      9.6047    6.5000    8.6926    8.6353    6.5000    9.7366
      4.1353    3.2821    4.1406    6.5000    6.5000    3.5660
      6.5000    6.5000    0.2163    3.1760    6.5000    8.4354
      8.5414    1.1633    2.7842    6.5000    6.5000    1.6982
      4.0518    6.5000    6.5000    8.2824    0.7952    8.0356
   
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
   
