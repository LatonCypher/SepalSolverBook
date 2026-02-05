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
      0.0945    0.9457    0.7116    0.3280
   
   R1[2] = 0.7116186470362507
   C1 = 
      0.5687
      0.4243
      0.1880
      0.3074
      0.0499
      0.6652
      0.9964
      0.9133
   
   C1[5] = 0.6652415997931809

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
      0.4604    0.0213    0.2715    0.6210    0.5783
      0.1862    0.2954    0.6463    0.0438    0.3420
   

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
   
      0.7482    0.0184    0.3367    0.6799    0.0121    0.6596    0.7021    0.9219
      0.3512    0.4516    0.6831    0.0127    0.3578    0.8621    0.5367    0.7791
      0.2301    0.3488    0.3750    0.1937    0.1310    0.6839    0.1361    0.8585
      0.1100    0.1441    0.5410    0.6840    0.8439    0.2431    0.7782    0.8159
      0.0122    0.5233    0.0303    0.1606    0.5028    0.8171    0.6993    0.2363
      0.2821    0.0386    0.4067    0.5222    0.2333    0.0868    0.8392    0.7736
      0.2263    0.9224    0.0805    0.8760    0.4971    0.0436    0.6957    0.3867
      0.6666    0.9210    0.3576    0.7561    0.7047    0.5125    0.0706    0.6885
   
   B = 
   
      0.2883    0.3569    0.8447    0.3172    0.7783    0.6457    0.1224    0.0040
      0.8330    0.8977    0.3340    0.6243    0.5333    0.1050    0.8850    0.1955
      0.0364    0.3215    0.1922    0.1587    0.2461    0.8574    0.7786    0.6344
      0.4120    0.2844    0.2909    0.5801    0.6358    0.0078    0.2988    0.1795
      0.9961    0.5467    0.9553    0.5996    0.0260    0.2746    0.9757    0.8015
      0.2936    0.7535    0.7183    0.5785    0.2054    0.9701    0.8385    0.7478
      0.8135    0.7789    0.6858    0.7387    0.4270    0.4327    0.9017    0.7322
      0.7010    0.9703    0.1121    0.1557    0.9849    0.4359    0.4915    0.7390
   
   C = 
   
      1.9465    2.5301    1.9708    1.7476    2.4508    2.1278    2.2242    2.0405
      2.0998    2.7732    1.9989    1.7401    1.8732    2.3664    2.9171    2.4255
      1.4942    2.0969    1.2453    1.1711    1.6280    1.6408    1.9327    1.6924
      2.5702    2.5794    2.0499    1.9560    1.9383    1.7153    2.8965    2.5255
      1.9820    2.1940    1.8109    1.7560    1.1103    1.4263    2.4585    1.8510
      1.8263    2.0118    1.4287    1.4115    1.8164    1.3876    1.9787    1.7985
      2.5424    2.4055    1.7962    2.0660    1.9446    0.9672    2.5073    1.6157
      2.6764    2.8891    2.3263    2.1602    2.4105    1.8609    2.9203    2.0538
   
   D = 
   
      1.9465    2.5301    1.9708    1.7476    2.4508    2.1278    2.2242    2.0405
      2.0998    2.7732    1.9989    1.7401    1.8732    2.3664    2.9171    2.4255
      1.4942    2.0969    1.2453    1.1711    1.6280    1.6408    1.9327    1.6924
      2.5702    2.5794    2.0499    1.9560    1.9383    1.7153    2.8965    2.5255
      1.9820    2.1940    1.8109    1.7560    1.1103    1.4263    2.4585    1.8510
      1.8263    2.0118    1.4287    1.4115    1.8164    1.3876    1.9787    1.7985
      2.5424    2.4055    1.7962    2.0660    1.9446    0.9672    2.5073    1.6157
      2.6764    2.8891    2.3263    2.1602    2.4105    1.8609    2.9203    2.0538
   


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

   
      0.1854    0.1359    0.9009    0.7239    0.4864    0.8415
      0.7229    0.4850    0.6496    0.2021    0.5114    0.0898
      0.6602    0.8509    0.7399    0.9137    0.6318    0.0292
      0.0429    0.3788    0.9147    0.9229    0.3227    0.1184
      0.7998    0.3993    0.4710    0.7336    0.8676    0.6595
   
   
      0.7229
      0.6602
      0.7998
      0.8509
      0.9009
      0.6496
      0.7399
      0.9147
      0.7239
      0.9137
      0.9229
      0.7336
      0.5114
      0.6318
      0.8676
      0.8415
      0.6595
   

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

   
      3.9372    1.6578    2.7299    0.8348    0.0949    3.4707
      9.1698    5.0047    8.5705    9.3375    2.8984    6.2689
      4.6367    2.8490    6.5175    5.4581    8.7411    5.4424
      9.7314    5.3248    5.1826    8.8630    1.4844    2.9935
      4.4832    4.3527    5.2019    1.7372    3.1690    5.5935
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      9.1698    5.0047    8.5705    9.3375    0.0000    6.2689
      0.0000    0.0000    6.5175    5.4581    8.7411    5.4424
      9.7314    5.3248    5.1826    8.8630    0.0000    0.0000
      0.0000    0.0000    5.2019    0.0000    0.0000    5.5935
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
         NaN    5.0047    8.5705       NaN    0.0000    6.2689
      0.0000    0.0000    6.5175    5.4581    8.7411    5.4424
         NaN    5.3248    5.1826    8.8630    0.0000    0.0000
      0.0000    0.0000    5.2019    0.0000    0.0000    5.5935
   

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

   
      4.5219    6.5000    6.5000    4.8503    8.1886    4.1687
      6.5000    9.5902    6.5000    6.5000    1.4790    6.5000
      9.1373    6.5000    6.5000    4.7687    8.3397    9.5324
      6.5000    1.0584    8.9100    6.5000    0.1039    8.0966
      4.3558    6.5000    2.7067    3.1318    6.5000    6.5000
   
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
   
