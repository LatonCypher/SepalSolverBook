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
      0.1262    0.8547    0.1769    0.1624
   
   R1[2] = 0.17685974981203834
   C1 = 
      0.6461
      0.8219
      0.3192
      0.1568
      0.3120
      0.0259
      0.6531
      0.5084
   
   C1[5] = 0.02592974226127276

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
      0.7005    0.0228    0.5256    0.0836    0.1728
      0.1863    0.9445    0.6271    0.2560    0.4139
   

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
   
      0.4037    0.3531    0.9217    0.9947    0.3288    0.5885    0.1777    0.0042
      0.4626    0.5133    0.7438    0.8359    0.1135    0.3008    0.0772    0.6677
      0.7417    0.9871    0.7685    0.4965    0.1406    0.5968    0.3157    0.2616
      0.7900    0.8641    0.8301    0.8873    0.0051    0.9992    0.6721    0.6236
      0.1683    0.7184    0.8803    0.1897    0.6185    0.4788    0.2252    0.2751
      0.0746    0.1342    0.2001    0.4274    0.9993    0.5070    0.2597    0.6127
      0.8991    0.2516    0.6375    0.1154    0.8045    0.1112    0.3963    0.4528
      0.2860    0.6359    0.9381    0.6288    0.3488    0.1259    0.1293    0.8484
   
   B = 
   
      0.9252    0.0334    0.1750    0.7900    0.6632    0.4794    0.1096    0.0196
      0.2124    0.8012    0.2869    0.6828    0.1903    0.8719    0.7493    0.8130
      0.6180    0.9737    0.4334    0.0603    0.9591    0.0060    0.4166    0.1147
      0.5525    0.3363    0.1528    0.8613    0.7658    0.5940    0.5125    0.4823
      0.5511    0.0648    0.0287    0.5005    0.6077    0.4383    0.4510    0.5644
      0.7949    0.0430    0.6928    0.0456    0.8114    0.7295    0.5828    0.7965
      0.4460    0.3367    0.5716    0.3342    0.2343    0.9206    0.5187    0.1009
      0.6447    0.1190    0.9976    0.3180    0.5987    0.0183    0.2257    0.7153
   
   C = 
   
      2.2987    1.6354    1.2464    1.7244    2.7022    1.8348    1.7870    1.5557
      2.2252    1.5579    1.6003    1.7895    2.4890    1.5228    1.5909    1.7041
      2.5065    1.9032    1.6809    2.0203    2.5973    2.3082    2.0296    1.9184
      3.4166    2.1693    2.5803    2.4994    3.5087    3.0255    2.6086    2.5538
      1.9564    1.6715    1.3987    1.3341    2.2198    1.6577    1.7576    1.7298
      1.9219    0.6956    1.3433    1.3356    2.0406    1.4660    1.4303    1.7721
      2.3435    1.1354    1.3019    1.7041    2.2870    1.5296    1.3472    1.2575
      2.2238    1.8165    1.7526    1.7516    2.5444    1.4500    1.7101    1.8506
   
   D = 
   
      2.2987    1.6354    1.2464    1.7244    2.7022    1.8348    1.7870    1.5557
      2.2252    1.5579    1.6003    1.7895    2.4890    1.5228    1.5909    1.7041
      2.5065    1.9032    1.6809    2.0203    2.5973    2.3082    2.0296    1.9184
      3.4166    2.1693    2.5803    2.4994    3.5087    3.0255    2.6086    2.5538
      1.9564    1.6715    1.3987    1.3341    2.2198    1.6577    1.7576    1.7298
      1.9219    0.6956    1.3433    1.3356    2.0406    1.4660    1.4303    1.7721
      2.3435    1.1354    1.3019    1.7041    2.2870    1.5296    1.3472    1.2575
      2.2238    1.8165    1.7526    1.7516    2.5444    1.4500    1.7101    1.8506
   


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

   
      0.1880    0.3168    0.6409    0.6423    0.8904    0.9606
      0.1466    0.0300    0.3410    0.9275    0.6550    0.8873
      0.8968    0.8855    0.0779    0.8162    0.4700    0.6101
      0.1105    0.4741    0.5468    0.2257    0.5410    0.6023
      0.2202    0.3184    0.8551    0.0674    0.3119    0.9188
   
   
      0.8968
      0.8855
      0.6409
      0.5468
      0.8551
      0.6423
      0.9275
      0.8162
      0.8904
      0.6550
      0.5410
      0.9606
      0.8873
      0.6101
      0.6023
      0.9188
   

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

   
      3.7250    3.2240    3.1278    9.7367    6.6710    9.1916
      7.1693    3.7303    9.0259    0.1200    7.5503    0.7513
      6.2225    8.8161    6.3127    5.7367    9.4836    2.7078
      9.7477    2.9046    5.6043    7.0008    4.6735    5.5764
      1.2343    8.7775    9.9509    5.6260    4.6267    1.8099
   
   
      0.0000    0.0000    0.0000    9.7367    6.6710    9.1916
      7.1693    0.0000    9.0259    0.0000    7.5503    0.0000
      6.2225    8.8161    6.3127    5.7367    9.4836    0.0000
      9.7477    0.0000    5.6043    7.0008    0.0000    5.5764
      0.0000    8.7775    9.9509    5.6260    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000       NaN    6.6710       NaN
      7.1693    0.0000       NaN    0.0000    7.5503    0.0000
      6.2225    8.8161    6.3127    5.7367       NaN    0.0000
         NaN    0.0000    5.6043    7.0008    0.0000    5.5764
      0.0000    8.7775       NaN    5.6260    0.0000    0.0000
   

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

   
      0.5538    3.6789    6.5000    6.5000    6.5000    4.4231
      8.9838    8.4441    3.4292    3.5167    2.1975    6.5000
      9.5453    6.5000    0.2619    6.5000    6.5000    1.4628
      3.5852    2.2797    8.4659    2.8460    0.5374    3.6538
      8.7276    1.2960    2.0640    6.5000    3.3390    3.1546
   
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
   
