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
      0.6799    0.5814    0.9144    0.1642
   
   R1[2] = 0.9144448180141724
   C1 = 
      0.3593
      0.3813
      0.6143
      0.8323
      0.7256
      0.8237
      0.1713
      0.3209
   
   C1[5] = 0.8236831378977088

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
      0.3047    0.3056    0.1802    0.7031    0.1249
      0.7520    0.4110    0.4954    0.5765    0.8101
   

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
   
      0.3737    0.8755    0.5063    0.9076    0.8614    0.3373    0.7888    0.0766
      0.1372    0.0418    0.0336    0.6319    0.2810    0.0006    0.9613    0.1136
      0.8274    0.4523    0.0537    0.5579    0.8559    0.5695    0.2669    0.2783
      0.5776    0.5935    0.0731    0.4886    0.1648    0.9086    0.8928    0.4081
      0.0800    0.3942    0.3194    0.1138    0.4260    0.2295    0.5227    0.6533
      0.6085    0.4510    0.5348    0.6337    0.4083    0.1244    0.2292    0.2814
      0.2798    0.1161    0.1985    0.9194    0.5760    0.0293    0.9091    0.1916
      0.9643    0.5355    0.6021    0.4699    0.7155    0.5868    0.5900    0.3807
   
   B = 
   
      0.2135    0.0691    0.8758    0.6337    0.1181    0.0388    0.0073    0.4360
      0.0683    0.0212    0.3776    0.2005    0.8611    0.6746    0.0984    0.6901
      0.7319    0.1341    0.7555    0.9435    0.9128    0.0669    0.5688    0.6657
      0.7352    0.9346    0.6890    0.6590    0.0360    0.2149    0.2671    0.0167
      0.4426    0.9630    0.7405    0.0323    0.4948    0.5579    0.0082    0.8787
      0.4583    0.0187    0.5822    0.1479    0.4983    0.4825    0.3313    0.5869
      0.1994    0.8935    0.5111    0.7956    0.5998    0.5518    0.6456    0.6514
      0.4820    0.6475    0.7584    0.4604    0.9557    0.0219    0.8865    0.0677
   
   C = 
   
      1.9074    2.5507    2.9613    2.2287    2.4336    1.9143    1.3152    2.5932
      0.8924    1.8086    1.3827    1.3698    0.9302    0.8616    0.9168    1.0028
      1.4842    1.8489    2.6332    1.4856    1.6896    1.3663    0.8448    1.9969
      1.4407    1.7567    2.5387    1.9139    2.1235    1.5646    1.4752    2.0055
      1.0742    1.4676    1.7502    1.2704    1.9075    0.9660    1.2475    1.4151
      1.4371    1.4981    2.2493    1.7417    1.6417    0.9203    0.9643    1.5433
      1.4309    2.3992    2.1259    1.8282    1.3753    1.1413    1.1431    1.4780
      1.9154    2.0715    3.2871    2.3507    2.5058    1.5560    1.4464    2.5818
   
   D = 
   
      1.9074    2.5507    2.9613    2.2287    2.4336    1.9143    1.3152    2.5932
      0.8924    1.8086    1.3827    1.3698    0.9302    0.8616    0.9168    1.0028
      1.4842    1.8489    2.6332    1.4856    1.6896    1.3663    0.8448    1.9969
      1.4407    1.7567    2.5387    1.9139    2.1235    1.5646    1.4752    2.0055
      1.0742    1.4676    1.7502    1.2704    1.9075    0.9660    1.2475    1.4151
      1.4371    1.4981    2.2493    1.7417    1.6417    0.9203    0.9643    1.5433
      1.4309    2.3992    2.1259    1.8282    1.3753    1.1413    1.1431    1.4780
      1.9154    2.0715    3.2871    2.3507    2.5058    1.5560    1.4464    2.5818
   


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

   
      0.6977    0.5991    0.3998    0.3957    0.1214    0.6104
      0.0903    0.3490    0.4367    0.8879    0.7494    0.2283
      0.3687    0.6547    0.0705    0.4574    0.6080    0.7333
      0.1479    0.1991    0.1273    0.2153    0.7484    0.6204
      0.2130    0.0380    0.7123    0.8085    0.4785    0.1253
   
   
      0.6977
      0.5991
      0.6547
      0.7123
      0.8879
      0.8085
      0.7494
      0.6080
      0.7484
      0.6104
      0.7333
      0.6204
   

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

   
      9.6994    8.5889    4.2291    3.8546    0.4643    2.4299
      4.2125    8.4384    5.3816    4.3459    8.8685    7.8039
      4.7517    8.5983    5.8038    6.1180    5.3408    3.0324
      3.9466    0.4903    2.1190    5.2224    8.4663    9.1545
      2.3491    9.4682    5.6445    2.5653    5.6169    5.0962
   
   
      9.6994    8.5889    0.0000    0.0000    0.0000    0.0000
      0.0000    8.4384    5.3816    0.0000    8.8685    7.8039
      0.0000    8.5983    5.8038    6.1180    5.3408    0.0000
      0.0000    0.0000    0.0000    5.2224    8.4663    9.1545
      0.0000    9.4682    5.6445    0.0000    5.6169    5.0962
   
   
         NaN    8.5889    0.0000    0.0000    0.0000    0.0000
      0.0000    8.4384    5.3816    0.0000    8.8685    7.8039
      0.0000    8.5983    5.8038    6.1180    5.3408    0.0000
      0.0000    0.0000    0.0000    5.2224    8.4663       NaN
      0.0000       NaN    5.6445    0.0000    5.6169    5.0962
   

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

   
      3.4717    8.8197    2.3908    4.7208    2.2839    1.0254
      6.5000    6.5000    0.0432    1.9311    1.0270    6.5000
      9.6882    9.3784    1.3268    8.2751    0.6155    0.9481
      9.2028    6.5000    4.2972    6.5000    3.3910    8.0093
      6.5000    3.8212    6.5000    9.6325    6.5000    1.9954
   
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
   
