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
      0.2168    0.3653    0.5537    0.1626
   
   R1[2] = 0.5537485200896755
   C1 = 
      0.2784
      0.5087
      0.2405
      0.9068
      0.2641
      0.6305
      0.9863
      0.4599
   
   C1[5] = 0.6305451072557685

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
      0.9642    0.5407    0.6873    0.8256    0.3846
      0.3779    0.8691    0.9894    0.0596    0.2160
   

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
   
      0.0710    0.0155    0.4590    0.9562    0.5945    0.8464    0.7641    0.1848
      0.0412    0.9541    0.7380    0.3087    0.0932    0.1279    0.6119    0.1938
      0.9785    0.4928    0.0347    0.9293    0.3202    0.6445    0.0464    0.4189
      0.1043    0.3599    0.9376    0.4541    0.8449    0.9921    0.3715    0.0104
      0.9602    0.5460    0.7692    0.2260    0.8176    0.2134    0.6598    0.1122
      0.7958    0.6900    0.4582    0.2691    0.2637    0.0188    0.2872    0.6565
      0.1686    0.8958    0.3288    0.9950    0.5013    0.1617    0.8936    0.5273
      0.1876    0.3722    0.0423    0.2576    0.6168    0.6882    0.4673    0.2804
   
   B = 
   
      0.9924    0.0821    0.5306    0.0104    0.9563    0.7751    0.1713    0.4193
      0.8042    0.2578    0.2133    0.8730    0.9686    0.8862    0.2528    0.3994
      0.9437    0.1068    0.1206    0.0409    0.3845    0.3499    0.7681    0.0369
      0.2472    0.3084    0.4017    0.6684    0.8873    0.4711    0.6534    0.8596
      0.0158    0.2998    0.0770    0.7293    0.6444    0.8381    0.4036    0.5513
      0.4152    0.2092    0.6635    0.5583    0.6007    0.6275    0.0836    0.3114
      0.7358    0.6792    0.3595    0.5253    0.8740    0.3420    0.1178    0.9793
      0.6109    0.8577    0.5116    0.4995    0.4592    0.4943    0.8788    0.2074
   
   C = 
   
      1.7884    1.3865    1.4571    2.0721    2.7521    2.0619    1.5565    2.2528
      2.2042    1.0600    0.8495    1.6275    2.2819    1.7445    1.3074    1.4216
      2.1926    1.1194    1.6851    1.8900    3.0774    2.5408    1.4827    1.9169
      2.0949    1.0637    1.2898    2.0275    2.6816    2.4046    1.6025    1.7531
      2.8293    1.2056    1.3086    1.7873    3.2265    2.7038    1.5650    2.0300
      2.4679    1.2163    1.2046    1.4909    2.5779    2.1708    1.5572    1.4262
      2.4986    1.8301    1.4567    2.6509    3.4813    2.5960    1.9424    2.6071
      1.3996    1.0821    1.1030    1.7203    2.1327    1.8585    0.9348    1.5203
   
   D = 
   
      1.7884    1.3865    1.4571    2.0721    2.7521    2.0619    1.5565    2.2528
      2.2042    1.0600    0.8495    1.6275    2.2819    1.7445    1.3074    1.4216
      2.1926    1.1194    1.6851    1.8900    3.0774    2.5408    1.4827    1.9169
      2.0949    1.0637    1.2898    2.0275    2.6816    2.4046    1.6025    1.7531
      2.8293    1.2056    1.3086    1.7873    3.2265    2.7038    1.5650    2.0300
      2.4679    1.2163    1.2046    1.4909    2.5779    2.1708    1.5572    1.4262
      2.4986    1.8301    1.4567    2.6509    3.4813    2.5960    1.9424    2.6071
      1.3996    1.0821    1.1030    1.7203    2.1327    1.8585    0.9348    1.5203
   


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

   
      0.8508    0.4445    0.5057    0.2681    0.5844    0.3070
      0.8490    0.0784    0.5864    0.7117    0.3003    0.3211
      0.5737    0.8577    0.0635    0.6594    0.2915    0.9251
      0.4555    0.7964    0.3202    0.4630    0.1028    0.1240
      0.8264    0.4593    0.6099    0.4517    0.7299    0.1712
   
   
      0.8508
      0.8490
      0.5737
      0.8264
      0.8577
      0.7964
      0.5057
      0.5864
      0.6099
      0.7117
      0.6594
      0.5844
      0.7299
      0.9251
   

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

   
      7.3829    5.1217    0.9998    2.7334    7.1432    2.0206
      1.4013    1.9112    4.1193    1.4572    1.1230    6.0330
      9.4386    4.9887    8.5050    3.0878    8.2407    0.3396
      9.0441    0.7323    1.7362    4.6628    6.7224    9.2347
      8.4432    6.0088    4.9511    8.1208    5.5288    6.8720
   
   
      7.3829    5.1217    0.0000    0.0000    7.1432    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    6.0330
      9.4386    0.0000    8.5050    0.0000    8.2407    0.0000
      9.0441    0.0000    0.0000    0.0000    6.7224    9.2347
      8.4432    6.0088    0.0000    8.1208    5.5288    6.8720
   
   
      7.3829    5.1217    0.0000    0.0000    7.1432    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    6.0330
         NaN    0.0000    8.5050    0.0000    8.2407    0.0000
         NaN    0.0000    0.0000    0.0000    6.7224       NaN
      8.4432    6.0088    0.0000    8.1208    5.5288    6.8720
   

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

   
      2.9608    1.0255    8.8646    6.5000    9.0191    4.5113
      0.3197    6.5000    4.1605    0.4141    2.5209    6.5000
      0.4336    6.5000    0.7760    6.5000    9.2631    9.3757
      1.6454    6.5000    2.1788    6.5000    4.0052    3.4905
      9.1807    8.4901    0.9477    6.5000    6.5000    3.4242
   
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
   
