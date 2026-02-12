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
      0.0354    0.4111    0.2321    0.0049
   
   R1[2] = 0.23213450212353404
   C1 = 
      0.8816
      0.9947
      0.8228
      0.4834
      0.8760
      0.5334
      0.3625
      0.3019
   
   C1[5] = 0.533425930783772

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
      0.2000    0.2802    0.6475    0.4745    0.1062
      0.1826    0.7929    0.0732    0.0485    0.2345
   

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
   
      0.0564    0.9804    0.5995    0.3634    0.4922    0.7939    0.5656    0.4189
      0.1413    0.7362    0.4795    0.5616    0.6066    0.9395    0.2018    0.1366
      0.3428    0.1072    0.3531    0.8108    0.8874    0.3383    0.4050    0.1141
      0.3213    0.9713    0.8382    0.0000    0.4817    0.3186    0.6099    0.3500
      0.1645    0.6387    0.5996    0.6960    0.9062    0.9471    0.5901    0.5363
      0.5917    0.6110    0.0709    0.4357    0.6909    0.6128    0.0279    0.7185
      0.8636    0.2659    0.1692    0.4337    0.1466    0.2425    0.4334    0.2449
      0.4605    0.1924    0.1552    0.9955    0.2815    0.8282    0.4880    0.1659
   
   B = 
   
      0.9006    0.0661    0.0460    0.6095    0.8961    0.9987    0.5502    0.9920
      0.8130    0.1742    0.6712    0.4634    0.5729    0.5733    0.0771    0.2407
      0.1388    0.1213    0.7342    0.1582    0.5864    0.8600    0.9298    0.9329
      0.7558    0.6053    0.0461    0.7146    0.6378    0.1760    0.6817    0.4890
      0.7787    0.5370    0.7416    0.9434    0.1605    0.9768    0.4149    0.9129
      0.7875    0.2679    0.3501    0.8566    0.8203    0.6923    0.6722    0.2976
      0.4899    0.9536    0.3081    0.2004    0.5039    0.3311    0.6857    0.1772
      0.6010    0.7285    0.1482    0.4730    0.3257    0.0883    0.0548    0.1351
   
   C = 
   
      2.7431    1.7887    1.9968    2.2990    2.3472    2.4525    2.0604    1.8713
      2.6100    1.4050    1.7398    2.3865    2.2020    2.3963    1.9923    1.9270
      2.2821    1.6115    1.3026    2.1559    1.7539    2.0953    1.9575    2.0898
      2.3306    1.4727    1.9907    1.7936    2.0960    2.5225    1.8825    2.0245
      3.3396    2.3099    2.1736    3.0265    2.7032    2.9522    2.6183    2.5028
      2.8348    1.5030    1.3515    2.4884    2.0614    2.2507    1.4927    1.9284
      2.0099    1.1218    0.7259    1.5350    1.8227    1.7130    1.4832    1.6067
      2.5554    1.6447    0.9839    2.2570    2.2733    1.9035    2.1084    1.7472
   
   D = 
   
      2.7431    1.7887    1.9968    2.2990    2.3472    2.4525    2.0604    1.8713
      2.6100    1.4050    1.7398    2.3865    2.2020    2.3963    1.9923    1.9270
      2.2821    1.6115    1.3026    2.1559    1.7539    2.0953    1.9575    2.0898
      2.3306    1.4727    1.9907    1.7936    2.0960    2.5225    1.8825    2.0245
      3.3396    2.3099    2.1736    3.0265    2.7032    2.9522    2.6183    2.5028
      2.8348    1.5030    1.3515    2.4884    2.0614    2.2507    1.4927    1.9284
      2.0099    1.1218    0.7259    1.5350    1.8227    1.7130    1.4832    1.6067
      2.5554    1.6447    0.9839    2.2570    2.2733    1.9035    2.1084    1.7472
   


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

   
      0.8987    0.0349    0.3679    0.8269    0.0594    0.3712
      0.5375    0.1324    0.5149    0.5881    0.6742    0.3247
      0.6731    0.9689    0.5059    0.3493    0.5734    0.4186
      0.1819    0.6893    0.4012    0.2151    0.2657    0.5636
      0.8151    0.7062    0.7954    0.2023    0.9070    0.4429
   
   
      0.8987
      0.5375
      0.6731
      0.8151
      0.9689
      0.6893
      0.7062
      0.5149
      0.5059
      0.7954
      0.8269
      0.5881
      0.6742
      0.5734
      0.9070
      0.5636
   

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

   
      2.1429    4.6213    0.0376    0.0191    4.4036    9.7541
      7.4995    7.5342    1.1255    6.5875    3.3005    9.7630
      8.0941    5.1432    2.2870    6.2446    5.5701    0.1019
      5.7891    9.8333    3.7149    0.2977    8.2684    4.9858
      7.1937    0.9481    9.5931    3.8501    9.9671    8.3358
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    9.7541
      7.4995    7.5342    0.0000    6.5875    0.0000    9.7630
      8.0941    5.1432    0.0000    6.2446    5.5701    0.0000
      5.7891    9.8333    0.0000    0.0000    8.2684    0.0000
      7.1937    0.0000    9.5931    0.0000    9.9671    8.3358
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      7.4995    7.5342    0.0000    6.5875    0.0000       NaN
      8.0941    5.1432    0.0000    6.2446    5.5701    0.0000
      5.7891       NaN    0.0000    0.0000    8.2684    0.0000
      7.1937    0.0000       NaN    0.0000       NaN    8.3358
   

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

   
      6.5000    8.1836    4.9240    0.3838    8.1974    6.5000
      9.3453    6.5000    8.5638    4.1713    9.7108    9.7217
      6.5000    1.2941    8.1509    6.5000    3.7385    6.5000
      6.5000    9.9811    0.5883    6.5000    9.7948    6.5000
      1.4664    6.5000    6.5000    3.8729    6.5000    9.0963
   
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
   
