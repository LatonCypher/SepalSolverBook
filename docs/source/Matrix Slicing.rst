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
      0.4600    0.2895    0.9683    0.8245
   
   R1[2] = 0.9683155317394051
   C1 = 
      0.4310
      0.6010
      0.6427
      0.8732
      0.0201
      0.2607
      0.3756
      0.7338
   
   C1[5] = 0.26070334142205087

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
      0.0481    0.2881    0.9830    0.9406    0.6585
      0.9676    0.5975    0.7885    0.4802    0.2526
   

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
   
      0.7723    0.5775    0.1041    0.2085    0.1988    0.4287    0.3572    0.2838
      0.6199    0.6237    0.7687    0.7416    0.8137    0.0498    0.0598    0.3478
      0.3150    0.7892    0.8497    0.0419    0.3288    0.7017    0.3462    0.6562
      0.5732    0.7888    0.8371    0.6582    0.1160    0.9395    0.6884    0.8335
      0.0984    0.1327    0.8761    0.2616    0.1031    0.1314    0.6764    0.5234
      0.1775    0.0816    0.1394    0.4772    0.9589    0.8979    0.3657    0.8659
      0.7378    0.6013    0.8809    0.8889    0.7413    0.0305    0.1554    0.3779
      0.1205    0.1469    0.2414    0.5081    0.9597    0.8011    0.1083    0.4215
   
   B = 
   
      0.4712    0.5789    0.1088    0.3723    0.9054    0.0014    0.9354    0.8551
      0.0463    0.5629    0.1966    0.2989    0.2936    0.7855    0.4141    0.9360
      0.4067    0.0812    0.6758    0.6431    0.3492    0.0082    0.0015    0.6906
      0.1095    0.0454    0.0773    0.6548    0.0428    0.5573    0.1486    0.6748
      0.8307    0.4108    0.9405    0.9462    0.4158    0.1790    0.1288    0.5346
      0.0329    0.2798    0.1430    0.0727    0.1251    0.7888    0.4980    0.1176
      0.3191    0.6258    0.8533    0.5995    0.6725    0.4510    0.9399    0.3407
      0.6133    0.7868    0.0025    0.4768    0.6312    0.5428    0.9138    0.3246
   
   C = 
   
      0.9231    1.4385    0.8378    1.2323    1.4696    1.2606    1.8268    1.7840
      1.6248    1.4654    1.5913    2.3724    1.6488    1.3112    1.4531    2.7193
      1.3443    1.7620    1.4736    1.8096    1.6869    1.7755    1.9459    2.2124
      1.5772    2.2708    1.6669    2.4066    2.2256    2.5189    2.8533    2.9282
      1.0643    1.1289    1.3434    1.5732    1.2896    0.9686    1.3798    1.4608
      1.6702    1.7370    1.5109    2.0973    1.5572    1.8464    1.9766    1.6702
      1.7293    1.5852    1.6978    2.5801    1.8453    1.4080    1.6747    2.9774
      1.3340    1.2130    1.3550    1.8090    1.0964    1.4821    1.2589    1.5310
   
   D = 
   
      0.9231    1.4385    0.8378    1.2323    1.4696    1.2606    1.8268    1.7840
      1.6248    1.4654    1.5913    2.3724    1.6488    1.3112    1.4531    2.7193
      1.3443    1.7620    1.4736    1.8096    1.6869    1.7755    1.9459    2.2124
      1.5772    2.2708    1.6669    2.4066    2.2256    2.5189    2.8533    2.9282
      1.0643    1.1289    1.3434    1.5732    1.2896    0.9686    1.3798    1.4608
      1.6702    1.7370    1.5109    2.0973    1.5572    1.8464    1.9766    1.6702
      1.7293    1.5852    1.6978    2.5801    1.8453    1.4080    1.6747    2.9774
      1.3340    1.2130    1.3550    1.8090    1.0964    1.4821    1.2589    1.5310
   


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

   
      0.4006    0.0055    0.7244    0.2921    0.5599    0.8833
      0.5159    0.4225    0.2574    0.2600    0.6466    0.7252
      0.2267    0.0001    0.8355    0.5406    0.7577    0.0869
      0.5417    0.9370    0.3654    0.2413    0.2472    0.5963
      0.3842    0.2354    0.4526    0.0570    0.1635    0.0568
   
   
      0.5159
      0.5417
      0.9370
      0.7244
      0.8355
      0.5406
      0.5599
      0.6466
      0.7577
      0.8833
      0.7252
      0.5963
   

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

   
      5.9989    2.7448    5.7247    3.6777    1.6424    4.9967
      4.7656    5.5232    1.7462    3.3774    1.7722    2.2923
      3.0924    1.4970    0.5673    6.6730    6.1766    7.5365
      8.0330    3.3035    4.4436    8.4154    1.3063    4.0542
      0.4337    7.3768    3.4927    0.1027    1.7307    6.6967
   
   
      5.9989    0.0000    5.7247    0.0000    0.0000    0.0000
      0.0000    5.5232    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    6.6730    6.1766    7.5365
      8.0330    0.0000    0.0000    8.4154    0.0000    0.0000
      0.0000    7.3768    0.0000    0.0000    0.0000    6.6967
   
   
      5.9989    0.0000    5.7247    0.0000    0.0000    0.0000
      0.0000    5.5232    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    6.6730    6.1766    7.5365
      8.0330    0.0000    0.0000    8.4154    0.0000    0.0000
      0.0000    7.3768    0.0000    0.0000    0.0000    6.6967
   

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

   
      4.0575    6.5000    0.0127    1.2177    8.9724    9.9731
      6.5000    4.8642    3.5517    4.6933    0.9579    8.2173
      8.3946    2.2074    6.5000    0.5022    6.5000    3.3484
      0.7637    2.3398    3.6700    1.5430    3.3393    2.2964
      4.3532    2.0646    6.5000    9.0772    6.5000    9.2893
   
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
   
