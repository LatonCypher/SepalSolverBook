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
      0.2114    0.8083    0.5662    0.7653
   
   R1[2] = 0.5661811724912957
   C1 = 
      0.6137
      0.7319
      0.2326
      0.4794
      0.9469
      0.0449
      0.7717
      0.3610
   
   C1[5] = 0.04490587140893443

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
      0.7149    0.7930    0.0680    0.6406    0.2994
      0.5983    0.5430    0.0292    0.9463    0.1350
   

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
   
      0.6128    0.9085    0.0313    0.5690    0.0313    0.7750    0.3460    0.3402
      0.1429    0.7924    0.6346    0.9452    0.2062    0.8358    0.4668    0.9705
      0.0088    0.8355    0.4142    0.1473    0.6152    0.5257    0.6414    0.3286
      0.6880    0.7207    0.0337    0.5177    0.9569    0.5720    0.4115    0.2677
      0.9336    0.1753    0.0173    0.6042    0.9655    0.1175    0.3587    0.9781
      0.5202    0.1032    0.5222    0.2667    0.1859    0.0207    0.4961    0.1083
      0.2373    0.9733    0.5110    0.6431    0.0384    0.9324    0.9952    0.4924
      0.0609    0.8827    0.2531    0.9965    0.2578    0.6714    0.7278    0.2431
   
   B = 
   
      0.8690    0.6549    0.5581    0.9539    0.1851    0.9298    0.7616    0.7046
      0.0979    0.6205    0.8092    0.1085    0.5282    0.4300    0.1813    0.4938
      0.4615    0.1755    0.3896    0.0923    0.4443    0.7363    0.1901    0.7809
      0.7616    0.1179    0.5651    0.3195    0.4697    0.3467    0.8381    0.6577
      0.6087    0.7816    0.6006    0.8718    0.7574    0.2855    0.9661    0.0319
      0.4630    0.2810    0.5899    0.8117    0.3668    0.4996    0.5803    0.7838
      0.7685    0.9630    0.1545    0.9277    0.2233    0.8920    0.0863    0.3993
      0.2191    0.4046    0.6701    0.1436    0.7076    0.3037    0.0287    0.6894
   
   C = 
   
      1.7876    1.7507    2.1683    1.8940    1.5004    1.9889    1.6339    2.2602
      2.2984    2.0464    2.8416    2.0134    2.4246    2.4562    1.9176    3.1263
      1.5756    1.9935    1.9244    1.7896    1.7306    1.8337    1.3245    1.7533
      2.3005    2.3780    2.4279    2.6217    1.9820    2.1614    2.3945    2.0352
      2.4288    2.3235    2.3711    2.5146    2.1035    2.1173    2.3125    2.0958
      1.4340    1.2006    1.0010    1.2955    0.8439    1.5439    0.9753    1.2956
      2.3549    2.3745    2.5393    2.3689    2.0288    2.7525    1.6715    2.9387
      2.0955    1.9390    2.2362    1.9755    1.8341    2.1002    1.7982    2.3245
   
   D = 
   
      1.7876    1.7507    2.1683    1.8940    1.5004    1.9889    1.6339    2.2602
      2.2984    2.0464    2.8416    2.0134    2.4246    2.4562    1.9176    3.1263
      1.5756    1.9935    1.9244    1.7896    1.7306    1.8337    1.3245    1.7533
      2.3005    2.3780    2.4279    2.6217    1.9820    2.1614    2.3945    2.0352
      2.4288    2.3235    2.3711    2.5146    2.1035    2.1173    2.3125    2.0958
      1.4340    1.2006    1.0010    1.2955    0.8439    1.5439    0.9753    1.2956
      2.3549    2.3745    2.5393    2.3689    2.0288    2.7525    1.6715    2.9387
      2.0955    1.9390    2.2362    1.9755    1.8341    2.1002    1.7982    2.3245
   


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

   
      0.1583    0.3462    0.2589    0.6744    0.9169    0.9214
      0.1602    0.6860    0.8087    0.9719    0.4359    0.5500
      0.1227    0.2640    0.8531    0.7299    0.3340    0.8254
      0.4396    0.6161    0.1951    0.3166    0.7972    0.1915
      0.4365    0.4411    0.9615    0.0817    0.9779    0.6898
   
   
      0.6860
      0.6161
      0.8087
      0.8531
      0.9615
      0.6744
      0.9719
      0.7299
      0.9169
      0.7972
      0.9779
      0.9214
      0.5500
      0.8254
      0.6898
   

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

   
      0.9515    9.5321    8.6386    2.3838    0.9566    0.6205
      5.0825    0.6436    4.1263    5.1345    1.7688    9.7290
      7.6973    8.1089    6.0085    7.6360    8.8355    2.4878
      4.7708    8.1917    2.6207    2.6011    5.4190    6.4925
      2.7922    2.5705    0.2005    7.7355    4.4810    4.5206
   
   
      0.0000    9.5321    8.6386    0.0000    0.0000    0.0000
      5.0825    0.0000    0.0000    5.1345    0.0000    9.7290
      7.6973    8.1089    6.0085    7.6360    8.8355    0.0000
      0.0000    8.1917    0.0000    0.0000    5.4190    6.4925
      0.0000    0.0000    0.0000    7.7355    0.0000    0.0000
   
   
      0.0000       NaN    8.6386    0.0000    0.0000    0.0000
      5.0825    0.0000    0.0000    5.1345    0.0000       NaN
      7.6973    8.1089    6.0085    7.6360    8.8355    0.0000
      0.0000    8.1917    0.0000    0.0000    5.4190    6.4925
      0.0000    0.0000    0.0000    7.7355    0.0000    0.0000
   

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

   
      6.5000    9.5272    6.5000    1.2581    3.2783    4.1226
      6.5000    4.9349    9.9851    4.7399    6.5000    1.4007
      9.1740    4.9429    6.5000    1.1754    2.5896    9.7001
      8.0268    0.9464    2.0170    2.4216    6.5000    6.5000
      8.7505    6.5000    6.5000    8.6360    0.6354    1.5276
   
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
   
