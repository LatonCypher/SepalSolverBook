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
      0.7246    0.8211    0.1295    0.9341
   
   R1[2] = 0.129529955094407
   C1 = 
      0.2395
      0.3688
      0.1772
      0.2629
      0.3472
      0.7541
      0.6349
      0.4413
   
   C1[5] = 0.7541244311416478

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
      0.3088    0.1834    0.9900    0.7088    0.7746
      0.0402    0.9492    0.0300    0.8998    0.8835
   

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
   
      0.0509    0.6504    0.4308    0.4526    0.2230    0.2672    0.2246    0.1878
      0.0553    0.9472    0.9452    0.2319    0.6243    0.2172    0.7597    0.7141
      0.0087    0.5964    0.0156    0.1412    0.2695    0.2984    0.5797    0.8031
      0.4072    0.1253    0.0404    0.7594    0.5300    0.3997    0.9205    0.7304
      0.5593    0.2539    0.2282    0.1110    0.2648    0.1789    0.7174    0.0832
      0.7189    0.2792    0.4101    0.5131    0.8909    0.6843    0.1375    0.6433
      0.3606    0.1988    0.8261    0.2150    0.4110    0.9831    0.5813    0.3646
      0.0041    0.1885    0.5999    0.6162    0.1505    0.0711    0.6727    0.5934
   
   B = 
   
      0.0144    0.8567    0.4206    0.7542    0.6843    0.6386    0.9525    0.9727
      0.1272    0.6575    0.0819    0.1207    0.8570    0.1999    0.6650    0.8758
      0.4564    0.7300    0.6061    0.5442    0.0600    0.3214    0.8977    0.0872
      0.7506    0.5834    0.9046    0.8976    0.8199    0.0095    0.6173    0.7236
      0.1624    0.5509    0.0803    0.3440    0.0199    0.3360    0.9796    0.4224
      0.0050    0.2921    0.6493    0.0881    0.2720    0.2330    0.2655    0.0575
      0.3383    0.5747    0.9451    0.3966    0.6009    0.1192    0.3136    0.0245
      0.8848    0.1120    0.5529    0.7175    0.8198    0.4327    0.2375    0.0102
   
   C = 
   
      0.8995    1.4009    1.2527    1.0817    1.3552    0.5505    1.5516    1.1012
      1.7181    2.4194    2.1874    1.9261    2.2098    1.1905    2.7512    1.4356
      1.1411    1.1521    1.3971    1.1390    1.7271    0.7078    1.2218    0.7878
      1.6560    1.9233    2.4688    2.1326    2.2822    1.0023    2.0637    1.3358
      0.5880    1.4971    1.3561    1.1273    1.2582    0.7345    1.5266    1.0072
      1.3820    2.2398    2.0393    2.1425    1.9902    1.4051    2.8056    1.7764
      1.1598    2.0567    2.2854    1.6587    1.5667    1.1318    2.2826    1.0006
      1.5378    1.4816    1.9603    1.6560    1.6185    0.6430    1.5663    0.7574
   
   D = 
   
      0.8995    1.4009    1.2527    1.0817    1.3552    0.5505    1.5516    1.1012
      1.7181    2.4194    2.1874    1.9261    2.2098    1.1905    2.7512    1.4356
      1.1411    1.1521    1.3971    1.1390    1.7271    0.7078    1.2218    0.7878
      1.6560    1.9233    2.4688    2.1326    2.2822    1.0023    2.0637    1.3358
      0.5880    1.4971    1.3561    1.1273    1.2582    0.7345    1.5266    1.0072
      1.3820    2.2398    2.0393    2.1425    1.9902    1.4051    2.8056    1.7764
      1.1598    2.0567    2.2854    1.6587    1.5667    1.1318    2.2826    1.0006
      1.5378    1.4816    1.9603    1.6560    1.6185    0.6430    1.5663    0.7574
   


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

   
      0.5427    0.3257    0.2867    0.8365    0.6126    0.9674
      0.9834    0.6398    0.7999    0.4032    0.2834    0.3888
      0.5639    0.0684    0.0426    0.9426    0.4159    0.6298
      0.8003    0.3579    0.4516    0.3033    0.5569    0.3096
      0.7914    0.5231    0.2775    0.6865    0.8236    0.4291
   
   
      0.5427
      0.9834
      0.5639
      0.8003
      0.7914
      0.6398
      0.5231
      0.7999
      0.8365
      0.9426
      0.6865
      0.6126
      0.5569
      0.8236
      0.9674
      0.6298
   

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

   
      1.1141    3.1744    2.7270    6.2630    2.9188    4.8399
      1.9907    4.1148    3.3772    1.4266    9.6902    1.7721
      0.4735    8.2043    0.4872    1.1594    6.2332    5.9127
      6.9448    5.7319    5.2499    0.2810    6.9896    9.5845
      1.1627    7.3782    5.9088    8.5723    1.2012    3.6725
   
   
      0.0000    0.0000    0.0000    6.2630    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    9.6902    0.0000
      0.0000    8.2043    0.0000    0.0000    6.2332    5.9127
      6.9448    5.7319    5.2499    0.0000    6.9896    9.5845
      0.0000    7.3782    5.9088    8.5723    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    6.2630    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN    0.0000
      0.0000    8.2043    0.0000    0.0000    6.2332    5.9127
      6.9448    5.7319    5.2499    0.0000    6.9896       NaN
      0.0000    7.3782    5.9088    8.5723    0.0000    0.0000
   

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

   
      9.9879    4.6352    6.5000    0.8421    6.5000    9.1806
      1.0268    2.3376    6.5000    1.3106    6.5000    9.1621
      4.8266    6.5000    4.1162    3.3623    6.5000    6.5000
      1.2456    4.8502    2.5042    0.8852    2.4261    2.4339
      1.8456    8.6303    9.2593    2.8700    6.5000    3.5917
   
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
   
