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
      0.9792    0.5660    0.0239    0.9470
   
   R1[2] = 0.02391988250774435
   C1 = 
      0.2047
      0.1137
      0.9170
      0.1656
      0.7524
      0.0116
      0.1561
      0.6091
   
   C1[5] = 0.011601050465395168

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
      0.6361    0.2157    0.2619    0.6062    0.3634
      0.1226    0.7152    0.7988    0.2588    0.0517
   

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
   
      0.0492    0.7054    0.8794    0.8277    0.2473    0.6091    0.4770    0.9954
      0.5905    0.4922    0.9797    0.9145    0.3089    0.5184    0.1540    0.1781
      0.9198    0.3045    0.0557    0.7601    0.1073    0.8711    0.4158    0.9974
      0.3194    0.6971    0.3342    0.1152    0.3661    0.7149    0.7434    0.2744
      0.6566    0.8861    0.0513    0.0315    0.1743    0.5910    0.2974    0.3848
      0.8248    0.1133    0.9328    0.3095    0.1366    0.6940    0.5020    0.3454
      0.2757    0.2744    0.6420    0.2532    0.8488    0.4908    0.2840    0.4993
      0.6814    0.5496    0.6101    0.5860    0.7625    0.2817    0.4063    0.6743
   
   B = 
   
      0.3549    0.8173    0.0419    0.6131    0.8260    0.2666    0.4761    0.0454
      0.4273    0.3463    0.5685    0.7725    0.0312    0.0101    0.1194    0.4667
      0.0463    0.1394    0.2290    0.7573    0.6111    0.0494    0.9779    0.2136
      0.6310    0.3426    0.1177    0.2697    0.8521    0.4856    0.3072    0.0605
      0.2160    0.6957    0.2617    0.1303    0.5334    0.5789    0.9739    0.7304
      0.7547    0.9864    0.8999    0.0803    0.6786    0.6818    0.1913    0.8402
      0.8462    0.6407    0.6201    0.1259    0.3088    0.4627    0.3835    0.3099
      0.3555    0.5358    0.8680    0.6419    0.8735    0.9451    0.0305    0.0215
   
   C = 
   
      2.1524    2.3025    2.4745    2.2445    2.8673    2.1855    1.7927    1.4310
      1.6938    2.0233    1.4339    1.9465    2.6007    1.4267    2.0434    1.2338
      2.3257    2.8602    2.2495    1.8229    3.0989    2.4112    1.2233    1.2023
      1.8446    2.1717    1.9380    1.3934    1.7375    1.4672    1.3842    1.5225
      1.5060    1.9624    1.6427    1.4891    1.5501    1.2070    0.8868    1.1805
      1.6804    2.2358    1.6204    1.7417    2.5192    1.5284    1.8824    1.1541
      1.3760    2.0207    1.6176    1.4418    2.1539    1.6601    1.9143    1.4240
      1.8355    2.4631    1.8400    2.0685    2.7644    1.9607    2.1396    1.3872
   
   D = 
   
      2.1524    2.3025    2.4745    2.2445    2.8673    2.1855    1.7927    1.4310
      1.6938    2.0233    1.4339    1.9465    2.6007    1.4267    2.0434    1.2338
      2.3257    2.8602    2.2495    1.8229    3.0989    2.4112    1.2233    1.2023
      1.8446    2.1717    1.9380    1.3934    1.7375    1.4672    1.3842    1.5225
      1.5060    1.9624    1.6427    1.4891    1.5501    1.2070    0.8868    1.1805
      1.6804    2.2358    1.6204    1.7417    2.5192    1.5284    1.8824    1.1541
      1.3760    2.0207    1.6176    1.4418    2.1539    1.6601    1.9143    1.4240
      1.8355    2.4631    1.8400    2.0685    2.7644    1.9607    2.1396    1.3872
   


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

   
      0.3756    0.4580    0.1604    0.1453    0.0658    0.5420
      0.1550    0.9096    0.0084    0.6429    0.8485    0.5809
      0.9750    0.8326    0.7432    0.9374    0.7562    0.7516
      0.0703    0.2784    0.2805    0.0558    0.6910    0.7871
      0.8485    0.3005    0.5590    0.1898    0.9965    0.6264
   
   
      0.9750
      0.8485
      0.9096
      0.8326
      0.7432
      0.5590
      0.6429
      0.9374
      0.8485
      0.7562
      0.6910
      0.9965
      0.5420
      0.5809
      0.7516
      0.7871
      0.6264
   

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

   
      8.3814    2.7321    9.6527    5.7901    7.5569    8.9149
      9.1310    1.2766    0.4808    7.4369    7.4238    8.3593
      7.4518    1.9563    0.5360    2.2139    9.4143    1.2045
      1.3851    7.2794    2.3628    8.2128    4.2704    2.6721
      1.6643    8.0072    9.0374    7.6584    0.9752    3.8336
   
   
      8.3814    0.0000    9.6527    5.7901    7.5569    8.9149
      9.1310    0.0000    0.0000    7.4369    7.4238    8.3593
      7.4518    0.0000    0.0000    0.0000    9.4143    0.0000
      0.0000    7.2794    0.0000    8.2128    0.0000    0.0000
      0.0000    8.0072    9.0374    7.6584    0.0000    0.0000
   
   
      8.3814    0.0000       NaN    5.7901    7.5569    8.9149
         NaN    0.0000    0.0000    7.4369    7.4238    8.3593
      7.4518    0.0000    0.0000    0.0000       NaN    0.0000
      0.0000    7.2794    0.0000    8.2128    0.0000    0.0000
      0.0000    8.0072       NaN    7.6584    0.0000    0.0000
   

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

   
      6.5000    9.1330    1.9443    8.1118    6.5000    0.2707
      9.8311    9.5657    2.1549    2.9163    0.4079    0.9702
      6.5000    4.6989    0.8238    0.1165    3.8420    4.0102
      0.1744    9.3918    4.1559    2.7963    6.5000    3.7469
      3.8110    0.6983    6.5000    1.0049    1.1495    6.5000
   
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
   
