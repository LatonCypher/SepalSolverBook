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
      0.6999    0.6078    0.2821    0.8738
   
   R1[2] = 0.2820708462639705
   C1 = 
      0.5573
      0.0393
      0.4908
      0.3944
      0.2727
      0.1232
      0.2937
      0.5589
   
   C1[5] = 0.1231508092336796

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
      0.8622    0.9372    0.7042    0.5865    0.5174
      0.1853    0.4548    0.1491    0.3567    0.6998
   

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
   
      0.9019    0.6199    0.6131    0.1866    0.8004    0.8341    0.7412    0.4680
      0.3825    0.3229    0.5865    0.6930    0.6953    0.9469    0.3638    0.1765
      0.7910    0.5124    0.5283    0.9616    0.4366    0.9174    0.6171    0.1135
      0.4344    0.0651    0.2005    0.6089    0.6658    0.3698    0.9674    0.1917
      0.0587    0.1419    0.8535    0.8164    0.3442    0.4959    0.2209    0.3299
      0.0414    0.4596    0.9818    0.4412    0.8691    0.4482    0.9802    0.6469
      0.4381    0.7011    0.1680    0.7489    0.1985    0.0715    0.4283    0.6500
      0.8788    0.3907    0.6935    0.0787    0.8983    0.2136    0.7792    0.4920
   
   B = 
   
      0.4215    0.7874    0.4467    0.3130    0.3568    0.7217    0.1030    0.4592
      0.2225    0.7489    0.6818    0.3950    0.9246    0.7399    0.0777    0.3100
      0.6382    0.3398    0.6393    0.2945    0.5679    0.4243    0.0114    0.8440
      0.8274    0.8328    0.6469    0.9240    0.4111    0.5815    0.9643    0.5502
      0.4804    0.4652    0.0705    0.9739    0.7861    0.7373    0.7023    0.6996
      0.1681    0.7838    0.9300    0.4591    0.9900    0.0950    0.4987    0.6680
      0.9515    0.9058    0.6123    0.5433    0.1333    0.6544    0.5244    0.3273
      0.0330    0.0024    0.5891    0.0132    0.8803    0.7406    0.3300    0.9912
   
   C = 
   
      2.3091    3.2368    2.8998    2.4515    3.2856    2.9791    1.8490    3.0500
      2.0259    2.7151    2.4705    2.3722    2.7409    2.1381    1.9489    2.5650
      2.5351    3.4685    2.9912    2.6773    2.8850    2.6302    2.1797    2.7298
      2.1381    2.4424    1.8565    2.1297    1.7665    2.1018    1.8617    1.9433
      1.7464    1.8723    2.0119    1.7675    2.0540    1.6741    1.5278    2.2119
      2.5583    2.7231    2.7042    2.4847    3.0058    2.8469    2.0379    3.1023
      1.6038    2.0888    1.9912    1.6229    2.0640    2.2565    1.4378    1.9435
      2.1901    2.5781    2.1820    2.1091    2.5556    2.8201    1.5129    2.6671
   
   D = 
   
      2.3091    3.2368    2.8998    2.4515    3.2856    2.9791    1.8490    3.0500
      2.0259    2.7151    2.4705    2.3722    2.7409    2.1381    1.9489    2.5650
      2.5351    3.4685    2.9912    2.6773    2.8850    2.6302    2.1797    2.7298
      2.1381    2.4424    1.8565    2.1297    1.7665    2.1018    1.8617    1.9433
      1.7464    1.8723    2.0119    1.7675    2.0540    1.6741    1.5278    2.2119
      2.5583    2.7231    2.7042    2.4847    3.0058    2.8469    2.0379    3.1023
      1.6038    2.0888    1.9912    1.6229    2.0640    2.2565    1.4378    1.9435
      2.1901    2.5781    2.1820    2.1091    2.5556    2.8201    1.5129    2.6671
   


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

   
      0.2832    0.0549    0.9953    0.1787    0.9514    0.9778
      0.4942    0.0241    0.5810    0.1422    0.3222    0.6301
      0.2122    0.5797    0.2103    0.7561    0.1922    0.3191
      0.3931    0.1266    0.8271    0.9469    0.2565    0.7775
      0.5341    0.2702    0.7613    0.5714    0.5772    0.9225
   
   
      0.5341
      0.5797
      0.9953
      0.5810
      0.8271
      0.7613
      0.7561
      0.9469
      0.5714
      0.9514
      0.5772
      0.9778
      0.6301
      0.7775
      0.9225
   

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

   
      3.4553    9.0107    3.3600    0.9702    7.6722    4.1795
      4.6606    0.1714    8.1134    3.1744    7.3886    5.7098
      8.0750    1.0430    5.0183    7.5903    3.5257    0.7589
      9.4092    9.7953    3.0966    3.3851    8.2753    7.3851
      5.7905    1.4420    7.3211    4.9622    1.3090    1.5523
   
   
      0.0000    9.0107    0.0000    0.0000    7.6722    0.0000
      0.0000    0.0000    8.1134    0.0000    7.3886    5.7098
      8.0750    0.0000    5.0183    7.5903    0.0000    0.0000
      9.4092    9.7953    0.0000    0.0000    8.2753    7.3851
      5.7905    0.0000    7.3211    0.0000    0.0000    0.0000
   
   
      0.0000       NaN    0.0000    0.0000    7.6722    0.0000
      0.0000    0.0000    8.1134    0.0000    7.3886    5.7098
      8.0750    0.0000    5.0183    7.5903    0.0000    0.0000
         NaN       NaN    0.0000    0.0000    8.2753    7.3851
      5.7905    0.0000    7.3211    0.0000    0.0000    0.0000
   

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

   
      8.2282    1.4309    6.5000    0.0251    0.2155    9.2937
      1.6696    8.1232    0.6046    6.5000    6.5000    8.0305
      2.6218    6.5000    0.7449    2.9537    4.4197    2.6344
      9.8861    6.5000    1.5288    0.7266    2.6099    6.5000
      9.3090    6.5000    6.5000    8.1791    2.1243    4.9983
   
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
   
