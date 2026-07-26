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
      0.6237    0.2492    0.7730    0.2709
   
   R1[2] = 0.7729759650222183
   C1 = 
      0.4654
      0.6696
      0.0052
      0.0937
      0.9876
      0.7160
      0.0805
      0.9943
   
   C1[5] = 0.7159993914842806

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
      0.4477    0.4770    0.3238    0.2572    0.9838
      0.4165    0.5872    0.3027    0.7154    0.8506
   

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
   
      0.3411    0.4498    0.1795    0.3367    0.9228    0.3803    0.3122    0.3417
      0.6175    0.5621    0.7584    0.0681    0.1352    0.0696    0.1816    0.6206
      0.4094    0.2790    0.6578    0.5098    0.9093    0.5431    0.7448    0.5131
      0.0476    0.9634    0.4967    0.2158    0.9951    0.6845    0.1955    0.5875
      0.2857    0.9867    0.5270    0.2787    0.3316    0.7458    0.3324    0.3906
      0.5355    0.2359    0.0329    0.5559    0.5122    0.3434    0.8721    0.0469
      0.8885    0.7693    0.7661    0.3090    0.3175    0.4508    0.1848    0.5698
      0.1301    0.8287    0.4432    0.1193    0.3501    0.5034    0.6408    0.0618
   
   B = 
   
      0.1316    0.9608    0.1564    0.8966    0.5068    0.9588    0.1641    0.2141
      0.2406    0.9260    0.7673    0.4980    0.6104    0.8499    0.5116    0.7123
      0.8508    0.9903    0.8958    0.4119    0.3846    0.7915    0.7467    0.2215
      0.6625    0.5745    0.6350    0.9902    0.7927    0.4183    0.2976    0.4444
      0.3509    0.6111    0.3697    0.1346    0.1314    0.7819    0.4509    0.7030
      0.5275    0.2032    0.3508    0.0040    0.5645    0.7226    0.5099    0.8637
      0.4446    0.4402    0.8047    0.1269    0.1314    0.5704    0.5958    0.1665
      0.8656    0.3049    0.6099    0.1853    0.0468    0.0812    0.0423    0.7127
   
   C = 
   
      1.4880    1.9983    1.7073    1.1659    1.1764    2.1945    1.3309    1.8555
      1.6089    2.2699    1.8495    1.3699    1.1118    2.0085    1.2064    1.3585
      2.3992    2.7463    2.6300    1.5959    1.5829    2.9335    2.0053    2.2566
      2.1093    2.5660    2.4523    1.2110    1.5446    2.7797    1.8749    2.6444
      1.9036    2.4899    2.3408    1.4028    1.6973    2.6658    1.7726    2.2156
      1.3126    1.8659    1.6874    1.3514    1.2467    2.1223    1.3262    1.3723
      2.0832    3.0430    2.3836    1.9749    1.8067    2.9671    1.7108    2.0947
      1.3994    2.0170    1.9884    0.9719    1.2541    2.2378    1.6108    1.6010
   
   D = 
   
      1.4880    1.9983    1.7073    1.1659    1.1764    2.1945    1.3309    1.8555
      1.6089    2.2699    1.8495    1.3699    1.1118    2.0085    1.2064    1.3585
      2.3992    2.7463    2.6300    1.5959    1.5829    2.9335    2.0053    2.2566
      2.1093    2.5660    2.4523    1.2110    1.5446    2.7797    1.8749    2.6444
      1.9036    2.4899    2.3408    1.4028    1.6973    2.6658    1.7726    2.2156
      1.3126    1.8659    1.6874    1.3514    1.2467    2.1223    1.3262    1.3723
      2.0832    3.0430    2.3836    1.9749    1.8067    2.9671    1.7108    2.0947
      1.3994    2.0170    1.9884    0.9719    1.2541    2.2378    1.6108    1.6010
   


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

   
      0.6564    0.7828    0.9793    0.4901    0.8736    0.6699
      0.3371    0.8163    0.2836    0.2883    0.4199    0.1455
      0.5728    0.4218    0.8608    0.0140    0.0465    0.2705
      0.4179    0.8320    0.5880    0.1522    0.9347    0.3667
      0.2179    0.0760    0.5102    0.5851    0.4448    0.0122
   
   
      0.6564
      0.5728
      0.7828
      0.8163
      0.8320
      0.9793
      0.8608
      0.5880
      0.5102
      0.5851
      0.8736
      0.9347
      0.6699
   

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

   
      6.7680    6.5544    4.6522    3.0087    8.8792    2.0661
      5.3278    5.0510    6.4320    7.3939    3.3568    3.8329
      8.7555    5.1066    3.6021    6.0753    3.3129    6.1843
      8.0217    8.6685    4.0220    1.3313    8.7951    0.4003
      1.9598    9.6026    7.1955    5.1186    7.1864    8.0272
   
   
      6.7680    6.5544    0.0000    0.0000    8.8792    0.0000
      5.3278    5.0510    6.4320    7.3939    0.0000    0.0000
      8.7555    5.1066    0.0000    6.0753    0.0000    6.1843
      8.0217    8.6685    0.0000    0.0000    8.7951    0.0000
      0.0000    9.6026    7.1955    5.1186    7.1864    8.0272
   
   
      6.7680    6.5544    0.0000    0.0000    8.8792    0.0000
      5.3278    5.0510    6.4320    7.3939    0.0000    0.0000
      8.7555    5.1066    0.0000    6.0753    0.0000    6.1843
      8.0217    8.6685    0.0000    0.0000    8.7951    0.0000
      0.0000       NaN    7.1955    5.1186    7.1864    8.0272
   

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

   
      9.0227    2.6032    2.2980    1.1484    8.3524    1.5719
      6.5000    8.8614    2.2515    1.9824    6.5000    9.4520
      9.7208    3.9146    6.5000    2.6901    2.1650    6.5000
      3.3734    2.9263    0.8300    6.5000    6.5000    6.5000
      9.3085    2.4587    2.5294    0.9167    9.6948    3.0003
   
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
   
