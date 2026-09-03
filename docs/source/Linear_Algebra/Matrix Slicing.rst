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
      0.8187    0.6417    0.4740    0.9850
   
   R1[2] = 0.4740028851313761
   C1 = 
      0.9681
      0.7132
      0.9598
      0.8108
      0.6318
      0.7919
      0.1188
      0.8694
   
   C1[5] = 0.7918649638711646

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
      0.1589    0.4619    0.7817    0.3156    0.5623
      0.7546    0.5129    0.9589    0.1814    0.8918
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.8874    0.9156    0.8664    0.5266    0.8983    0.5426    0.4959    0.1633
      0.6485    0.6224    0.6656    0.3464    0.0612    0.5886    0.9769    0.9153
      0.9250    0.4936    0.6511    0.3656    0.7685    0.2743    0.3600    0.9293
      0.1305    0.8328    0.6538    0.6622    0.4783    0.9769    0.0919    0.1474
      0.5910    0.3368    0.7169    0.1188    0.6559    0.2090    0.4922    0.1459
      0.4172    0.6228    0.6062    0.5557    0.8565    0.8192    0.4364    0.9087
      0.8772    0.5462    0.5247    0.8869    0.4742    0.5366    0.3232    0.1469
      0.5028    0.6647    0.3239    0.5534    0.6985    0.9613    0.6276    0.8697
   
   B = 
   
      0.2125    0.1540    0.5559    0.9812    0.6162    0.7938    0.9238    0.6983
      0.0505    0.7926    0.3175    0.4039    0.5138    0.2670    0.3848    0.0851
      0.6288    0.7262    0.9141    0.1309    0.0546    0.6603    0.7293    0.1673
      0.7045    0.0743    0.7223    0.9708    0.3644    0.4710    0.7386    0.7824
      0.7158    0.7510    0.7451    0.2292    0.5960    0.5989    0.2957    0.2495
      0.6111    0.1156    0.6022    0.3726    0.4548    0.5895    0.8751    0.9449
      0.2338    0.8757    0.5359    0.6100    0.1830    0.7265    0.6726    0.6239
      0.3198    0.9914    0.4038    0.0218    0.6592    0.3389    0.6614    0.1402
   
   C = 
   
      2.2934    2.8642    3.2841    2.5792    2.2369    3.0423    3.3748    2.3236
      1.7565    2.9792    2.7100    2.1604    1.9683    2.6872    3.3755    2.1975
      1.9876    2.8791    2.8362    2.0653    2.2536    2.6666    3.1134    1.8887
      1.9554    1.9029    2.4664    1.7257    1.6286    2.0485    2.5625    1.9098
      1.4360    1.9798    2.1138    1.4567    1.2918    2.0114    2.0905    1.3429
      2.3991    3.0603    3.1174    2.0672    2.3745    2.7802    3.3422    2.2679
      1.9587    1.8618    2.6903    2.5200    1.8556    2.4912    2.9826    2.2883
      2.2463    2.9281    2.9732    2.2615    2.4123    2.7868    3.4104    2.4909
   
   D = 
   
      2.2934    2.8642    3.2841    2.5792    2.2369    3.0423    3.3748    2.3236
      1.7565    2.9792    2.7100    2.1604    1.9683    2.6872    3.3755    2.1975
      1.9876    2.8791    2.8362    2.0653    2.2536    2.6666    3.1134    1.8887
      1.9554    1.9029    2.4664    1.7257    1.6286    2.0485    2.5625    1.9098
      1.4360    1.9798    2.1138    1.4567    1.2918    2.0114    2.0905    1.3429
      2.3991    3.0603    3.1174    2.0672    2.3745    2.7802    3.3422    2.2679
      1.9587    1.8618    2.6903    2.5200    1.8556    2.4912    2.9826    2.2883
      2.2463    2.9281    2.9732    2.2615    2.4123    2.7868    3.4104    2.4909
   


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

   
      0.7527    0.4496    0.0752    0.9681    0.8256    0.0603
      0.9069    0.2307    0.1838    0.6063    0.3710    0.8382
      0.8373    0.1424    0.3247    0.7271    0.0672    0.4672
      0.9019    0.5153    0.4365    0.2876    0.1261    0.3918
      0.2162    0.7154    0.6749    0.8769    0.5483    0.6015
   
   
      0.7527
      0.9069
      0.8373
      0.9019
      0.5153
      0.7154
      0.6749
      0.9681
      0.6063
      0.7271
      0.8769
      0.8256
      0.5483
      0.8382
      0.6015
   

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

   
      9.1964    2.4545    3.9811    8.8448    3.8608    1.5399
      0.1504    3.9998    3.1198    4.5004    9.0950    9.2415
      5.2191    2.4438    4.9805    3.8194    0.9226    4.1469
      9.0668    5.6951    4.6845    3.9707    4.6879    1.1810
      8.3072    9.3376    7.4959    8.2685    4.7910    1.1932
   
   
      9.1964    0.0000    0.0000    8.8448    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    9.0950    9.2415
      5.2191    0.0000    0.0000    0.0000    0.0000    0.0000
      9.0668    5.6951    0.0000    0.0000    0.0000    0.0000
      8.3072    9.3376    7.4959    8.2685    0.0000    0.0000
   
   
         NaN    0.0000    0.0000    8.8448    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN       NaN
      5.2191    0.0000    0.0000    0.0000    0.0000    0.0000
         NaN    5.6951    0.0000    0.0000    0.0000    0.0000
      8.3072       NaN    7.4959    8.2685    0.0000    0.0000
   

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

   
      0.8484    3.6067    1.4396    9.8196    4.9816    1.9022
      8.8452    9.7583    6.5000    6.5000    2.5668    4.3581
      8.8794    4.7591    9.1164    6.5000    4.3589    2.8325
      2.2025    6.5000    8.4814    0.5536    2.8546    8.7274
      6.5000    2.1884    6.5000    8.4176    3.0712    6.5000
   
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
   
