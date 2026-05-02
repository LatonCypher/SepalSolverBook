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
      0.5067    0.8999    0.9127    0.7961
   
   R1[2] = 0.9126994194080679
   C1 = 
      0.0515
      0.4048
      0.0404
      0.8342
      0.0124
      0.7309
      0.1965
      0.2803
   
   C1[5] = 0.7309371142110244

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
      0.9137    0.3025    0.5612    0.1864    0.2194
      0.6551    0.6395    0.9707    0.1455    0.3626
   

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
   
      0.3477    0.7852    0.0502    0.9811    0.7389    0.9543    0.8427    0.1191
      0.2439    0.4843    0.4602    0.6754    0.2440    0.9177    0.7923    0.5747
      0.8588    0.9699    0.6107    0.8070    0.8572    0.1578    0.7471    0.0450
      0.1786    0.9794    0.8701    0.9057    0.1545    0.1490    0.5040    0.4096
      0.5383    0.4950    0.8489    0.1705    0.0679    0.9540    0.6445    0.0146
      0.9076    0.2025    0.3893    0.3592    0.3864    0.6930    0.4789    0.4942
      0.9208    0.0059    0.9300    0.2163    0.8037    0.0905    0.4509    0.1192
      0.9477    0.4773    0.4944    0.2656    0.1678    0.6146    0.0253    0.2401
   
   B = 
   
      0.3801    0.3358    0.1907    0.2128    0.1308    0.8689    0.2999    0.3294
      0.4029    0.1433    0.5136    0.8193    0.6610    0.8051    0.2220    0.3520
      0.5213    0.4926    0.6627    0.6841    0.2785    0.2725    0.9712    0.6526
      0.9142    0.7177    0.8074    0.1048    0.3530    0.3150    0.9294    0.7489
      0.0612    0.5196    0.1354    0.8970    0.0946    0.1851    0.6280    0.4953
      0.5258    0.8605    0.3871    0.4797    0.6921    0.1680    0.9860    0.2696
      0.6474    0.2233    0.9007    0.0361    0.9790    0.1831    0.3369    0.6836
      0.4823    0.9911    0.7052    0.1966    0.6045    0.8995    0.4986    0.2211
   
   C = 
   
      2.5216    2.4694    2.6075    2.0289    2.5521    1.8154    2.9875    2.3840
      2.4328    2.5258    2.6528    1.6351    2.5000    1.8014    2.8670    2.0939
      2.4140    2.0999    2.5999    2.3602    2.1572    2.3101    2.7841    2.6149
      2.3556    2.0059    2.6663    1.8394    2.0915    1.9804    2.5758    2.2014
      1.9326    1.8069    2.0266    1.6635    2.0007    1.4553    2.4621    1.7679
      1.8943    2.1771    1.9255    1.4565    1.7715    1.8910    2.3628    1.7081
      1.4811    1.6376    1.6037    1.6638    1.1119    1.4802    2.1871    1.8314
      1.5187    1.6804    1.4207    1.4523    1.2820    1.7810    1.9568    1.3209
   
   D = 
   
      2.5216    2.4694    2.6075    2.0289    2.5521    1.8154    2.9875    2.3840
      2.4328    2.5258    2.6528    1.6351    2.5000    1.8014    2.8670    2.0939
      2.4140    2.0999    2.5999    2.3602    2.1572    2.3101    2.7841    2.6149
      2.3556    2.0059    2.6663    1.8394    2.0915    1.9804    2.5758    2.2014
      1.9326    1.8069    2.0266    1.6635    2.0007    1.4553    2.4621    1.7679
      1.8943    2.1771    1.9255    1.4565    1.7715    1.8910    2.3628    1.7081
      1.4811    1.6376    1.6037    1.6638    1.1119    1.4802    2.1871    1.8314
      1.5187    1.6804    1.4207    1.4523    1.2820    1.7810    1.9568    1.3209
   


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

   
      0.1859    0.9098    0.9565    0.8107    0.5965    0.5183
      0.0789    0.0375    0.2376    0.1530    0.4053    0.9309
      0.1900    0.5221    0.1039    0.7656    0.5278    0.1963
      0.1958    0.4202    0.3584    0.4075    0.4817    0.4545
      0.5690    0.4724    0.2803    0.9249    0.0919    0.5312
   
   
      0.5690
      0.9098
      0.5221
      0.9565
      0.8107
      0.7656
      0.9249
      0.5965
      0.5278
      0.5183
      0.9309
      0.5312
   

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

   
      8.3781    7.0366    5.5391    0.4142    8.8563    3.1145
      0.9352    1.7807    5.9802    8.6521    2.1587    2.1257
      0.5877    8.4042    0.8732    9.3079    2.8436    5.4397
      9.4084    5.6270    9.9617    4.3552    4.7944    3.3051
      7.1663    2.9634    4.9207    7.0397    1.3858    0.0353
   
   
      8.3781    7.0366    5.5391    0.0000    8.8563    0.0000
      0.0000    0.0000    5.9802    8.6521    0.0000    0.0000
      0.0000    8.4042    0.0000    9.3079    0.0000    5.4397
      9.4084    5.6270    9.9617    0.0000    0.0000    0.0000
      7.1663    0.0000    0.0000    7.0397    0.0000    0.0000
   
   
      8.3781    7.0366    5.5391    0.0000    8.8563    0.0000
      0.0000    0.0000    5.9802    8.6521    0.0000    0.0000
      0.0000    8.4042    0.0000       NaN    0.0000    5.4397
         NaN    5.6270       NaN    0.0000    0.0000    0.0000
      7.1663    0.0000    0.0000    7.0397    0.0000    0.0000
   

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

   
      6.5000    6.5000    6.5000    0.7604    1.4837    9.7772
      8.0978    4.7599    8.8270    2.6865    0.5197    8.8629
      9.8336    2.0439    6.5000    6.5000    4.7945    3.8646
      2.1950    6.5000    0.4182    3.6127    3.5994    8.0244
      3.1536    0.5981    4.8161    4.2782    9.1067    1.9198
   
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
   
