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
      0.8441    0.9883    0.2043    0.3130
   
   R1[2] = 0.20426788697508091
   C1 = 
      0.7904
      0.5473
      0.9662
      0.4637
      0.3429
      0.4702
      0.8287
      0.0253
   
   C1[5] = 0.47023825477064873

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
      0.7717    0.7016    0.3762    0.6850    0.8533
      0.4467    0.5738    0.2661    0.9360    0.9209
   

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
   
      0.3254    0.7551    0.2258    0.5935    0.6695    0.8763    0.9755    0.1443
      0.5586    0.1495    0.5725    0.8630    0.3075    0.9731    0.8481    0.0768
      0.7177    0.5133    0.0575    0.5310    0.5669    0.1567    0.8433    0.1374
      0.5760    0.5750    0.2641    0.5609    0.9456    0.0813    0.4057    0.1486
      0.2684    0.2003    0.1605    0.0046    0.0484    0.8285    0.3726    0.2124
      0.8630    0.4886    0.2589    0.3328    0.8811    0.4175    0.9690    0.8970
      0.3908    0.8739    0.5312    0.8678    0.7735    0.6732    0.0911    0.7892
      0.4206    0.5657    0.7600    0.8675    0.5569    0.1800    0.6561    0.6849
   
   B = 
   
      0.2359    0.5783    0.9636    0.4866    0.4540    0.0624    0.6778    0.1980
      0.2637    0.7814    0.7010    0.2814    0.4157    0.3533    0.9259    0.4175
      0.6013    0.0613    0.8892    0.2048    0.7520    0.3750    0.6193    0.6682
      0.5354    0.8743    0.6850    0.8457    0.2845    0.3349    0.8650    0.0576
      0.9703    0.3092    0.8719    0.0296    0.9687    0.7665    0.3968    0.1620
      0.1363    0.1710    0.5029    0.2528    0.2741    0.5509    0.0386    0.2272
      0.2999    0.1950    0.3129    0.1222    0.2703    0.7867    0.2258    0.4372
      0.1546    0.4778    0.2782    0.2659    0.7381    0.4178    0.9793    0.5968
   
   C = 
   
      1.8134    1.9270    2.8201    1.3179    2.0592    2.3941    2.2340    1.3849
      1.6748    1.6931    2.7876    1.5402    1.8423    2.0624    2.0443    1.2928
      1.4692    1.7162    2.3415    1.1506    1.6551    1.6673    2.0127    1.0036
      1.8199    1.7454    2.6108    1.1082    2.0163    1.6767    2.1870    1.0006
      0.5196    0.6564    1.1795    0.5366    0.8586    1.0246    0.8141    0.7300
      2.0075    2.1492    3.1634    1.3805    2.7763    2.4775    2.9486    1.7637
      2.0983    2.4494    3.3170    1.6928    2.7279    2.1880    3.2800    1.6362
      2.0374    2.1485    3.0437    1.5774    2.5160    2.1299    3.0766    1.7039
   
   D = 
   
      1.8134    1.9270    2.8201    1.3179    2.0592    2.3941    2.2340    1.3849
      1.6748    1.6931    2.7876    1.5402    1.8423    2.0624    2.0443    1.2928
      1.4692    1.7162    2.3415    1.1506    1.6551    1.6673    2.0127    1.0036
      1.8199    1.7454    2.6108    1.1082    2.0163    1.6767    2.1870    1.0006
      0.5196    0.6564    1.1795    0.5366    0.8586    1.0246    0.8141    0.7300
      2.0075    2.1492    3.1634    1.3805    2.7763    2.4775    2.9486    1.7637
      2.0983    2.4494    3.3170    1.6928    2.7279    2.1880    3.2800    1.6362
      2.0374    2.1485    3.0437    1.5774    2.5160    2.1299    3.0766    1.7039
   


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

   
      0.3821    0.8399    0.8647    0.6749    0.1376    0.9433
      0.9810    0.1036    0.1898    0.7493    0.4432    0.2430
      0.6882    0.1220    0.8249    0.0257    0.5405    0.6450
      0.2175    0.0459    0.3891    0.0723    0.0838    0.3338
      0.0026    0.1210    0.8296    0.3216    0.7081    0.4994
   
   
      0.9810
      0.6882
      0.8399
      0.8647
      0.8249
      0.8296
      0.6749
      0.7493
      0.5405
      0.7081
      0.9433
      0.6450
   

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

   
      6.5617    8.9783    9.0099    2.7926    7.7139    5.1577
      4.8770    0.9713    2.1454    9.9914    4.8233    8.7751
      7.9982    8.6443    8.7471    9.1071    6.7112    7.9468
      0.1015    3.8729    3.5197    9.7932    7.2119    7.5737
      6.1181    8.3840    6.0907    1.2770    1.1466    8.2214
   
   
      6.5617    8.9783    9.0099    0.0000    7.7139    5.1577
      0.0000    0.0000    0.0000    9.9914    0.0000    8.7751
      7.9982    8.6443    8.7471    9.1071    6.7112    7.9468
      0.0000    0.0000    0.0000    9.7932    7.2119    7.5737
      6.1181    8.3840    6.0907    0.0000    0.0000    8.2214
   
   
      6.5617    8.9783       NaN    0.0000    7.7139    5.1577
      0.0000    0.0000    0.0000       NaN    0.0000    8.7751
      7.9982    8.6443    8.7471       NaN    6.7112    7.9468
      0.0000    0.0000    0.0000       NaN    7.2119    7.5737
      6.1181    8.3840    6.0907    0.0000    0.0000    8.2214
   

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

   
      9.2833    1.1988    6.5000    6.5000    1.7217    4.7274
      4.4283    6.5000    9.1513    4.5172    6.5000    6.5000
      6.5000    1.6804    4.8426    2.8514    4.1816    3.8442
      1.6441    6.5000    1.2861    4.6613    4.0487    3.5686
      6.5000    1.5043    6.5000    2.1367    4.8498    3.6205
   
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
   
