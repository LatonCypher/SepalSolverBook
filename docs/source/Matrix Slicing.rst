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
      0.1229    0.1460    0.7968    0.6646
   
   R1[2] = 0.796776663911757
   C1 = 
      0.5109
      0.8841
      0.7258
      0.2909
      0.0927
      0.1574
      0.9797
      0.6549
   
   C1[5] = 0.1574458914705834

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
      0.9398    0.1867    0.0012    0.2928    0.0347
      0.7207    0.0826    0.7580    0.9333    0.7013
   

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
   
      0.4825    0.1061    0.9164    0.7828    0.9560    0.3303    0.0457    0.9513
      0.9095    0.6738    0.1263    0.0849    0.9177    0.5205    0.5506    0.2844
      0.3313    0.0087    0.8069    0.6992    0.5831    0.6743    0.4319    0.3543
      0.6642    0.4670    0.7763    0.7687    0.2123    0.3885    0.7320    0.7231
      0.2913    0.7306    0.8391    0.1810    0.6982    0.0018    0.1959    0.7439
      0.7918    0.5691    0.2446    0.0546    0.2796    0.0326    0.6591    0.0084
      0.6958    0.6316    0.2490    0.7579    0.4878    0.8911    0.3915    0.3494
      0.4909    0.8238    0.5254    0.3506    0.5088    0.3545    0.5273    0.7637
   
   B = 
   
      0.7170    0.2546    0.6773    0.2884    0.7044    0.7669    0.2114    0.9053
      0.3666    0.9130    0.7354    0.9740    0.7283    0.1668    0.6999    0.2251
      0.0002    0.2102    0.9650    0.6609    0.9694    0.2255    0.3252    0.8348
      0.9558    0.4230    0.7057    0.0109    0.3260    0.5026    0.9897    0.9684
      0.4521    0.6702    0.9232    0.7792    0.7286    0.1990    0.8749    0.2262
      0.4845    0.3112    0.9339    0.7586    0.2855    0.0192    0.1940    0.7649
      0.1033    0.7743    0.2478    0.3525    0.5122    0.6269    0.0716    0.0239
      0.6161    0.9558    0.7445    0.0769    0.5439    0.9599    0.9634    0.4323
   
   C = 
   
      2.3163    2.4316    3.7522    1.9413    2.8924    2.1261    3.0692    2.8649
      1.8795    2.3843    2.9748    2.3288    2.5354    1.6917    2.0061    1.9045
      1.7624    1.8313    3.0417    1.7902    2.2811    1.5286    2.0436    2.4637
      2.1877    2.6050    3.3636    1.9417    2.8451    2.3514    2.4906    2.7742
      1.4449    2.3252    2.9206    2.0237    2.6239    1.6012    2.3667    1.7895
      1.0440    1.5116    1.6876    1.4205    1.7823    1.2622    1.0054    1.2096
      2.3629    2.3681    3.3506    2.2096    2.4389    1.7709    2.3845    2.6661
      1.9160    2.7254    3.1929    2.2050    2.7268    1.9802    2.4856    2.1370
   
   D = 
   
      2.3163    2.4316    3.7522    1.9413    2.8924    2.1261    3.0692    2.8649
      1.8795    2.3843    2.9748    2.3288    2.5354    1.6917    2.0061    1.9045
      1.7624    1.8313    3.0417    1.7902    2.2811    1.5286    2.0436    2.4637
      2.1877    2.6050    3.3636    1.9417    2.8451    2.3514    2.4906    2.7742
      1.4449    2.3252    2.9206    2.0237    2.6239    1.6012    2.3667    1.7895
      1.0440    1.5116    1.6876    1.4205    1.7823    1.2622    1.0054    1.2096
      2.3629    2.3681    3.3506    2.2096    2.4389    1.7709    2.3845    2.6661
      1.9160    2.7254    3.1929    2.2050    2.7268    1.9802    2.4856    2.1370
   


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

   
      0.9948    0.6381    0.9200    0.8539    0.0889    0.0128
      0.9412    0.1886    0.1003    0.7973    0.7861    0.2627
      0.5223    0.2894    0.7632    0.5614    0.3937    0.9962
      0.3946    0.0077    0.3857    0.2861    0.1279    0.4376
      0.5499    0.2407    0.9882    0.7781    0.7178    0.5554
   
   
      0.9948
      0.9412
      0.5223
      0.5499
      0.6381
      0.9200
      0.7632
      0.9882
      0.8539
      0.7973
      0.5614
      0.7781
      0.7861
      0.7178
      0.9962
      0.5554
   

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

   
      1.6255    0.6214    4.9173    0.7931    6.4114    1.5727
      8.8584    3.6667    6.2405    9.5319    9.8857    9.1448
      1.7108    2.6419    4.6371    8.5681    0.8092    9.3148
      0.4151    0.5855    8.4187    0.3951    2.3150    9.5571
      6.7483    5.4394    5.0685    0.0922    7.1350    6.9415
   
   
      0.0000    0.0000    0.0000    0.0000    6.4114    0.0000
      8.8584    0.0000    6.2405    9.5319    9.8857    9.1448
      0.0000    0.0000    0.0000    8.5681    0.0000    9.3148
      0.0000    0.0000    8.4187    0.0000    0.0000    9.5571
      6.7483    5.4394    5.0685    0.0000    7.1350    6.9415
   
   
      0.0000    0.0000    0.0000    0.0000    6.4114    0.0000
      8.8584    0.0000    6.2405       NaN       NaN       NaN
      0.0000    0.0000    0.0000    8.5681    0.0000       NaN
      0.0000    0.0000    8.4187    0.0000    0.0000       NaN
      6.7483    5.4394    5.0685    0.0000    7.1350    6.9415
   

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

   
      1.4230    6.5000    3.0904    6.5000    0.5888    6.5000
      9.8179    8.2075    0.1808    6.5000    3.6349    6.5000
      1.6981    3.6609    0.7150    3.6657    6.5000    0.8476
      3.3711    6.5000    4.3553    0.6490    2.4049    6.5000
      6.5000    3.8818    3.2498    4.7211    6.5000    3.6732
   
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
   
