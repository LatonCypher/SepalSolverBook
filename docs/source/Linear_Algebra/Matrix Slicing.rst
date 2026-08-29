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
      0.7695    0.1794    0.6845    0.4596
   
   R1[2] = 0.6845197232007845
   C1 = 
      0.4143
      0.8354
      0.8073
      0.7734
      0.8494
      0.2042
      0.5738
      0.1005
   
   C1[5] = 0.20424531317682848

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
      0.9560    0.7714    0.2415    0.3860    0.6528
      0.3037    0.3904    0.3262    0.7529    0.7818
   

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
   
      0.3259    0.6355    0.1431    0.4565    0.6108    0.1360    0.7575    0.7263
      0.8420    0.8030    0.8247    0.9978    0.1356    0.1818    0.7169    0.7076
      0.6217    0.0827    0.9530    0.9598    0.9733    0.2976    0.7168    0.9024
      0.6697    0.8186    0.5823    0.5704    0.0572    0.7785    0.3966    0.3358
      0.4148    0.8818    0.5559    0.1548    0.7651    0.7057    0.1640    0.4433
      0.7529    0.6760    0.8521    0.0641    0.2699    0.4984    0.6835    0.4472
      0.4606    0.1282    0.2611    0.1444    0.4607    0.0598    0.9531    0.2233
      0.9855    0.2432    0.2431    0.2446    0.8213    0.1736    0.2542    0.4929
   
   B = 
   
      0.5814    0.9253    0.6505    0.7203    0.4610    0.0923    0.3118    0.3155
      0.7456    0.3279    0.0080    0.2817    0.7549    0.9484    0.6328    0.9089
      0.1863    0.9202    0.9387    0.1574    0.6730    0.5423    0.1467    0.6313
      0.8741    0.5593    0.9815    0.5466    0.8815    0.5257    0.3465    0.1594
      0.2221    0.4482    0.6677    0.1595    0.7016    0.8492    0.9765    0.8208
      0.7962    0.1031    0.1518    0.1516    0.7522    0.6113    0.4146    0.9736
      0.9278    0.0811    0.4431    0.1006    0.9613    0.9978    0.7163    0.1741
      0.2488    0.9462    0.4466    0.0513    0.0404    0.6435    0.6778    0.3285
   
   C = 
   
      2.2164    1.9334    1.8880    0.9173    2.4171    2.7755    2.3707    1.8477
      3.1301    3.1666    3.0595    1.6654    3.3785    3.2080    2.4382    2.3206
      2.7822    3.3951    3.6574    1.4644    3.4687    3.4615    2.9174    2.5357
      2.6909    2.1988    2.0307    1.3006    2.8419    2.5901    1.9002    2.3980
      2.1317    2.1195    1.8392    0.9875    2.6106    2.7874    2.2801    2.7971
      2.3587    2.3892    2.1164    1.1121    2.7267    2.7100    2.0726    2.3726
      1.6280    1.2905    1.5262    0.6777    1.9056    1.9041    1.6219    1.1254
      1.6925    2.2251    2.0187    1.1585    1.9881    1.9564    1.9718    1.7737
   
   D = 
   
      2.2164    1.9334    1.8880    0.9173    2.4171    2.7755    2.3707    1.8477
      3.1301    3.1666    3.0595    1.6654    3.3785    3.2080    2.4382    2.3206
      2.7822    3.3951    3.6574    1.4644    3.4687    3.4615    2.9174    2.5357
      2.6909    2.1988    2.0307    1.3006    2.8419    2.5901    1.9002    2.3980
      2.1317    2.1195    1.8392    0.9875    2.6106    2.7874    2.2801    2.7971
      2.3587    2.3892    2.1164    1.1121    2.7267    2.7100    2.0726    2.3726
      1.6280    1.2905    1.5262    0.6777    1.9056    1.9041    1.6219    1.1254
      1.6925    2.2251    2.0187    1.1585    1.9881    1.9564    1.9718    1.7737
   


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

   
      0.8610    0.0600    0.8593    0.2426    0.8613    0.6061
      0.7390    0.8354    0.8666    0.1761    0.0091    0.7415
      0.6440    0.5000    0.1954    0.2320    0.1028    0.0006
      0.1379    0.8006    0.2087    0.8856    0.0828    0.3784
      0.8525    0.7200    0.7367    0.4502    0.9543    0.6571
   
   
      0.8610
      0.7390
      0.6440
      0.8525
      0.8354
      0.5000
      0.8006
      0.7200
      0.8593
      0.8666
      0.7367
      0.8856
      0.8613
      0.9543
      0.6061
      0.7415
      0.6571
   

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

   
      9.9257    4.1895    9.8231    4.7716    8.6489    3.5236
      4.8038    4.2376    1.9792    6.8915    8.7430    2.8520
      3.1780    5.4145    4.7510    0.9521    7.6699    9.5085
      0.1990    8.3392    9.3622    2.6355    9.6351    7.9939
      1.6476    5.2391    4.6518    9.5010    2.8245    5.8092
   
   
      9.9257    0.0000    9.8231    0.0000    8.6489    0.0000
      0.0000    0.0000    0.0000    6.8915    8.7430    0.0000
      0.0000    5.4145    0.0000    0.0000    7.6699    9.5085
      0.0000    8.3392    9.3622    0.0000    9.6351    7.9939
      0.0000    5.2391    0.0000    9.5010    0.0000    5.8092
   
   
         NaN    0.0000       NaN    0.0000    8.6489    0.0000
      0.0000    0.0000    0.0000    6.8915    8.7430    0.0000
      0.0000    5.4145    0.0000    0.0000    7.6699       NaN
      0.0000    8.3392       NaN    0.0000       NaN    7.9939
      0.0000    5.2391    0.0000       NaN    0.0000    5.8092
   

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

   
      6.5000    4.7019    3.0197    6.5000    2.8110    8.6631
      4.7245    6.5000    6.5000    2.4439    6.5000    9.7154
      3.9422    1.5314    3.9213    6.5000    1.8392    3.4266
      3.2927    9.5726    0.2037    1.9440    2.7372    3.0997
      6.5000    2.9020    2.8237    8.6068    6.5000    6.5000
   
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
   
