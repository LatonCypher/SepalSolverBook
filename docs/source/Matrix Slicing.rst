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
      0.1328    0.7171    0.1459    0.3433
   
   R1[2] = 0.14589561067938606
   C1 = 
      0.4730
      0.4776
      0.4806
      0.7829
      0.5526
      0.7226
      0.3734
      0.6191
   
   C1[5] = 0.7226424677971774

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
      0.6657    0.2431    0.4228    0.7151    0.1823
      0.3241    0.9174    0.3021    0.4614    0.2002
   

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
   
      0.5763    0.6289    0.7661    0.2584    0.5178    0.9465    0.5319    0.3568
      0.5744    0.2269    0.2890    0.3487    0.6407    0.3120    0.8902    0.5562
      0.9354    0.4537    0.9321    0.3418    0.3090    0.1379    0.9758    0.8201
      0.7755    0.9994    0.6993    0.0097    0.8510    0.7638    0.8211    0.2417
      0.2207    0.4979    0.4895    0.7205    0.2753    0.0748    0.1899    0.0764
      0.9071    0.2068    0.0260    0.1510    0.1710    0.2119    0.0771    0.3114
      0.1156    0.1721    0.6197    0.9198    0.4867    0.1140    0.4922    0.6198
      0.4573    0.0225    0.3259    0.2689    0.6954    0.1485    0.7562    0.5989
   
   B = 
   
      0.1801    0.5576    0.1492    0.1662    0.1676    0.0796    0.6113    0.2596
      0.6158    0.2502    0.3031    0.2848    0.0834    0.1484    0.0381    0.7933
      0.8125    0.5561    0.0728    0.7453    0.6619    0.6586    0.6419    0.2252
      0.2627    0.6267    0.6734    0.0397    0.1558    0.4466    0.7649    0.8614
      0.2443    0.5331    0.5066    0.3040    0.1826    0.3483    0.7634    0.3364
      0.9198    0.7506    0.0743    0.6384    0.2501    0.3680    0.3858    0.9042
      0.2016    0.7917    0.9242    0.4272    0.4463    0.9358    0.9924    0.3542
      0.2738    0.9978    0.0420    0.4277    0.1069    0.8544    0.9098    0.2830
   
   C = 
   
      2.3834    2.8303    1.3455    1.9976    1.3033    2.0904    2.6786    2.3630
      1.3449    2.5918    1.6042    1.4014    1.0127    2.0717    2.8110    1.6650
      1.9186    3.2267    1.6782    1.9424    1.4790    2.6805    3.4525    1.9134
      2.4679    2.9957    1.7329    2.1355    1.4166    2.2273    2.9476    2.4961
      1.1287    1.4009    1.0283    0.8172    0.6767    1.1020    1.5162    1.4322
      0.6889    1.2885    0.4883    0.5883    0.3619    0.6632    1.2667    0.9000
      1.3647    2.3817    1.4698    1.2627    0.9906    2.0552    2.6464    1.7149
      1.0546    2.2888    1.3672    1.2213    0.9018    1.8906    2.5787    1.2472
   
   D = 
   
      2.3834    2.8303    1.3455    1.9976    1.3033    2.0904    2.6786    2.3630
      1.3449    2.5918    1.6042    1.4014    1.0127    2.0717    2.8110    1.6650
      1.9186    3.2267    1.6782    1.9424    1.4790    2.6805    3.4525    1.9134
      2.4679    2.9957    1.7329    2.1355    1.4166    2.2273    2.9476    2.4961
      1.1287    1.4009    1.0283    0.8172    0.6767    1.1020    1.5162    1.4322
      0.6889    1.2885    0.4883    0.5883    0.3619    0.6632    1.2667    0.9000
      1.3647    2.3817    1.4698    1.2627    0.9906    2.0552    2.6464    1.7149
      1.0546    2.2888    1.3672    1.2213    0.9018    1.8906    2.5787    1.2472
   


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

   
      0.9668    0.8224    0.6632    0.6199    0.4381    0.7580
      0.7659    0.8100    0.0585    0.3073    0.4613    0.0456
      0.6856    0.8218    0.9997    0.3796    0.3357    0.7482
      0.3264    0.8805    0.7817    0.4675    0.7952    0.3711
      0.8591    0.6459    0.9962    0.4706    0.2285    0.9479
   
   
      0.9668
      0.7659
      0.6856
      0.8591
      0.8224
      0.8100
      0.8218
      0.8805
      0.6459
      0.6632
      0.9997
      0.7817
      0.9962
      0.6199
      0.7952
      0.7580
      0.7482
      0.9479
   

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

   
      0.0837    1.9341    3.0463    8.2981    5.0480    3.7859
      6.6646    2.8741    9.9383    7.1321    0.7375    5.1896
      2.7527    1.2660    1.7701    5.8045    3.5446    6.4939
      9.2290    7.7271    9.5512    1.9593    5.5620    3.7660
      8.2832    7.9523    2.1403    4.5495    4.0305    1.6607
   
   
      0.0000    0.0000    0.0000    8.2981    5.0480    0.0000
      6.6646    0.0000    9.9383    7.1321    0.0000    5.1896
      0.0000    0.0000    0.0000    5.8045    0.0000    6.4939
      9.2290    7.7271    9.5512    0.0000    5.5620    0.0000
      8.2832    7.9523    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    8.2981    5.0480    0.0000
      6.6646    0.0000       NaN    7.1321    0.0000    5.1896
      0.0000    0.0000    0.0000    5.8045    0.0000    6.4939
         NaN    7.7271       NaN    0.0000    5.5620    0.0000
      8.2832    7.9523    0.0000    0.0000    0.0000    0.0000
   

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

   
      3.7759    6.5000    1.8642    4.5391    0.3788    8.8520
      4.4424    1.8942    3.0558    2.9497    4.8414    9.4409
      0.5954    6.5000    9.9552    6.5000    8.2378    6.5000
      3.6304    0.2499    6.5000    1.2167    1.6621    4.8533
      1.9965    3.9619    6.5000    2.2221    3.5871    9.5614
   
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
   
