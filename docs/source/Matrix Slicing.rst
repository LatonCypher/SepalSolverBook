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
      0.0878    0.4871    0.1575    0.7985
   
   R1[2] = 0.15747363450005392
   C1 = 
      0.9465
      0.8240
      0.4410
      0.7265
      0.1562
      0.6034
      0.4357
      0.2130
   
   C1[5] = 0.603367441385726

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
      0.8706    0.1636    0.7701    0.6612    0.7894
      0.5346    0.0879    0.6758    0.0126    0.7980
   

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
   
      0.8750    0.5478    0.7171    0.6390    0.4580    0.3701    0.2957    0.2152
      0.9921    0.4173    0.5559    0.4998    0.0661    0.5783    0.0341    0.5867
      0.2182    0.9803    0.1415    0.8202    0.0976    0.9235    0.1266    0.8315
      0.3178    0.7908    0.4375    0.4681    0.2559    0.9401    0.1968    0.8088
      0.5813    0.2825    0.9066    0.1514    0.3259    0.6152    0.5474    0.5426
      0.0428    0.9199    0.9925    0.1081    0.8191    0.0979    0.2583    0.9603
      0.6113    0.6832    0.6326    0.1882    0.9014    0.5004    0.9449    0.2419
      0.1415    0.8000    0.1071    0.5981    0.6730    0.0363    0.2898    0.8441
   
   B = 
   
      0.7939    0.9980    0.6084    0.3413    0.1883    0.4408    0.6557    0.9714
      0.7792    0.2728    0.1254    0.6220    0.7198    0.9386    0.8002    0.1123
      0.5694    0.0593    0.3987    0.6602    0.6681    0.0770    0.4060    0.7172
      0.1874    0.7408    0.9336    0.4273    0.2459    0.0164    0.2796    0.8868
      0.4637    0.8882    0.5849    0.1994    0.4731    0.2401    0.9655    0.2877
      0.0062    0.2940    0.5067    0.8220    0.5164    0.1267    0.7732    0.2409
      0.9011    0.2158    0.6063    0.1670    0.6490    0.6833    0.7492    0.1218
      0.9722    0.2756    0.2894    0.5974    0.2210    0.2096    0.8498    0.6096
   
   C = 
   
      2.3398    2.1773    2.1806    1.9593    1.8425    1.3695    2.6146    2.3806
      2.1583    1.9050    1.8664    2.0234    1.4631    1.1154    2.3849    2.3727
      2.1448    1.7158    1.9203    2.4246    1.8319    1.4418    2.8240    1.9238
      2.2934    1.6746    1.8834    2.4290    1.9494    1.4082    2.9583    1.9436
      2.4019    1.5611    1.8832    2.0235    1.9028    1.2376    2.6791    2.0202
      2.8830    1.5093    1.6014    2.1489    2.1778    1.5473    3.0735    1.8286
      2.9208    2.1916    2.3093    2.0250    2.4273    1.9387    3.4277    1.9335
      2.3028    1.7124    1.6195    1.5889    1.5329    1.3724    2.5559    1.5866
   
   D = 
   
      2.3398    2.1773    2.1806    1.9593    1.8425    1.3695    2.6146    2.3806
      2.1583    1.9050    1.8664    2.0234    1.4631    1.1154    2.3849    2.3727
      2.1448    1.7158    1.9203    2.4246    1.8319    1.4418    2.8240    1.9238
      2.2934    1.6746    1.8834    2.4290    1.9494    1.4082    2.9583    1.9436
      2.4019    1.5611    1.8832    2.0235    1.9028    1.2376    2.6791    2.0202
      2.8830    1.5093    1.6014    2.1489    2.1778    1.5473    3.0735    1.8286
      2.9208    2.1916    2.3093    2.0250    2.4273    1.9387    3.4277    1.9335
      2.3028    1.7124    1.6195    1.5889    1.5329    1.3724    2.5559    1.5866
   


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

   
      0.1613    0.3920    0.5838    0.0794    0.7524    0.8751
      0.0318    0.0281    0.6778    0.8927    0.5171    0.3525
      0.8494    0.5349    0.3936    0.4548    0.0653    0.2176
      0.6289    0.7452    0.8781    0.2292    0.6497    0.8281
      0.2498    0.0816    0.1728    0.7829    0.0178    0.1981
   
   
      0.8494
      0.6289
      0.5349
      0.7452
      0.5838
      0.6778
      0.8781
      0.8927
      0.7829
      0.7524
      0.5171
      0.6497
      0.8751
      0.8281
   

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

   
      8.5680    2.0879    1.0660    7.6553    7.5168    5.3025
      2.3044    6.1846    8.3133    0.0190    4.4516    0.4440
      4.2606    0.6140    6.9000    6.2488    0.3561    8.0259
      8.9963    8.5514    1.0549    0.6987    0.8710    5.9725
      9.0355    7.6435    1.4642    2.7946    0.0992    6.2863
   
   
      8.5680    0.0000    0.0000    7.6553    7.5168    5.3025
      0.0000    6.1846    8.3133    0.0000    0.0000    0.0000
      0.0000    0.0000    6.9000    6.2488    0.0000    8.0259
      8.9963    8.5514    0.0000    0.0000    0.0000    5.9725
      9.0355    7.6435    0.0000    0.0000    0.0000    6.2863
   
   
      8.5680    0.0000    0.0000    7.6553    7.5168    5.3025
      0.0000    6.1846    8.3133    0.0000    0.0000    0.0000
      0.0000    0.0000    6.9000    6.2488    0.0000    8.0259
      8.9963    8.5514    0.0000    0.0000    0.0000    5.9725
         NaN    7.6435    0.0000    0.0000    0.0000    6.2863
   

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

   
      9.7953    0.7471    3.0472    9.0685    6.5000    1.7580
      6.5000    6.5000    8.0291    1.8194    6.5000    6.5000
      6.5000    6.5000    1.1482    6.5000    1.3497    9.4397
      1.7685    0.9504    9.3820    6.5000    3.7438    3.2970
      2.3585    3.3458    6.5000    6.5000    6.5000    1.8336
   
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
   
