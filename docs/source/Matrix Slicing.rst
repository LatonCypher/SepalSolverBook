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
      0.5003    0.7224    0.3022    0.7608
   
   R1[2] = 0.3021670693091889
   C1 = 
      0.2777
      0.1170
      0.8241
      0.5709
      0.9113
      0.4318
      0.9632
      0.5932
   
   C1[5] = 0.43181292498416535

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
      0.2746    0.1972    0.3005    0.4060    0.4584
      0.6531    0.4801    0.6125    0.1809    0.6052
   

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
   
      0.1006    0.6199    0.4956    0.0459    0.2955    0.0766    0.7038    0.1316
      0.0725    0.0833    0.1415    0.6803    0.0329    0.1511    0.7963    0.5357
      0.4810    0.7928    0.0062    0.1101    0.7391    0.5415    0.6285    0.8978
      0.9020    0.3275    0.3808    0.8439    0.7746    0.0874    0.4347    0.5244
      0.4147    0.3743    0.9107    0.9033    0.2919    0.3604    0.5233    0.4335
      0.0641    0.5259    0.0795    0.0977    0.7089    0.3569    0.4032    0.3211
      0.8984    0.8059    0.5631    0.5561    0.1833    0.5534    0.4134    0.0361
      0.7915    0.0323    0.5855    0.4057    0.3515    0.6777    0.3573    0.9178
   
   B = 
   
      0.3152    0.8598    0.7306    0.8150    0.0885    0.1627    0.5912    0.9549
      0.0075    0.2488    0.4722    0.7074    0.6138    0.5978    0.7852    0.4545
      0.0100    0.6882    0.3592    0.9953    0.8786    0.1032    0.8157    0.6483
      0.7018    0.8537    0.8630    0.1597    0.2117    0.3354    0.8036    0.8821
      0.6779    0.5693    0.5937    0.4039    0.2340    0.1679    0.6943    0.9383
      0.8919    0.6947    0.4251    0.0991    0.7057    0.5763    0.7123    0.4506
      0.9130    0.7917    0.4864    0.4841    0.0417    0.9363    0.7595    0.0717
      0.0313    0.7930    0.4494    0.1898    0.8519    0.8375    0.6797    0.0350
   
   C = 
   
      0.9888    1.5040    1.1933    1.5137    1.0992    1.3164    1.8711    1.1064
      1.4033    1.9403    1.4421    0.8830    0.9299    1.5913    1.8698    0.9737
      1.8209    2.7156    2.2012    1.8034    1.9041    2.3664    2.9869    1.9347
      1.8992    3.1012    2.6229    2.1121    1.5020    1.6916    3.0659    2.8172
      1.7872    3.0221    2.3622    2.1426    1.9716    1.7979    3.1593    2.4422
      1.2706    1.5495    1.3211    1.0968    1.1272    1.3370    1.8652    1.3041
      1.6815    2.6800    2.2806    2.2874    1.6682    1.6396    2.9302    2.5321
      1.7381    3.1196    2.2371    1.8718    2.0476    1.8975    2.9190    2.2009
   
   D = 
   
      0.9888    1.5040    1.1933    1.5137    1.0992    1.3164    1.8711    1.1064
      1.4033    1.9403    1.4421    0.8830    0.9299    1.5913    1.8698    0.9737
      1.8209    2.7156    2.2012    1.8034    1.9041    2.3664    2.9869    1.9347
      1.8992    3.1012    2.6229    2.1121    1.5020    1.6916    3.0659    2.8172
      1.7872    3.0221    2.3622    2.1426    1.9716    1.7979    3.1593    2.4422
      1.2706    1.5495    1.3211    1.0968    1.1272    1.3370    1.8652    1.3041
      1.6815    2.6800    2.2806    2.2874    1.6682    1.6396    2.9302    2.5321
      1.7381    3.1196    2.2371    1.8718    2.0476    1.8975    2.9190    2.2009
   


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

   
      0.3983    0.8962    0.6323    0.0716    0.1772    0.3998
      0.9067    0.5064    0.8357    0.7580    0.8540    0.3711
      0.1062    0.7326    0.6779    0.0840    0.7466    0.6719
      0.5054    0.8287    0.9108    0.9017    0.5208    0.4299
      0.3317    0.4293    0.5303    0.7411    0.9338    0.0242
   
   
      0.9067
      0.5054
      0.8962
      0.5064
      0.7326
      0.8287
      0.6323
      0.8357
      0.6779
      0.9108
      0.5303
      0.7580
      0.9017
      0.7411
      0.8540
      0.7466
      0.5208
      0.9338
      0.6719
   

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

   
      7.9439    2.1437    3.1241    6.2240    4.8377    2.7987
      0.9588    4.6020    4.6941    8.9574    1.5302    1.2934
      5.5292    6.7538    8.8606    7.4666    7.9107    8.4147
      0.3810    0.4540    6.4588    2.6475    6.5199    9.5633
      5.8661    9.7520    5.6387    6.6857    4.6891    7.1719
   
   
      7.9439    0.0000    0.0000    6.2240    0.0000    0.0000
      0.0000    0.0000    0.0000    8.9574    0.0000    0.0000
      5.5292    6.7538    8.8606    7.4666    7.9107    8.4147
      0.0000    0.0000    6.4588    0.0000    6.5199    9.5633
      5.8661    9.7520    5.6387    6.6857    0.0000    7.1719
   
   
      7.9439    0.0000    0.0000    6.2240    0.0000    0.0000
      0.0000    0.0000    0.0000    8.9574    0.0000    0.0000
      5.5292    6.7538    8.8606    7.4666    7.9107    8.4147
      0.0000    0.0000    6.4588    0.0000    6.5199       NaN
      5.8661       NaN    5.6387    6.6857    0.0000    7.1719
   

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

   
      4.9814    0.4105    4.7573    6.5000    6.5000    4.3654
      3.6977    6.5000    6.5000    1.0073    6.5000    0.1868
      1.7314    9.2522    0.1402    9.6693    4.9067    8.4720
      3.3906    4.3571    0.2689    2.5997    4.6062    6.5000
      6.5000    8.0326    6.5000    8.0057    6.5000    0.2932
   
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
   
