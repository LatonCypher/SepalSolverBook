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
      0.1157    0.1015    0.0402    0.3192
   
   R1[2] = 0.04024089310851975
   C1 = 
      0.5878
      0.2661
      0.2364
      0.2745
      0.3079
      0.4855
      0.2488
      0.7267
   
   C1[5] = 0.48545822077768275

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
      0.1964    0.9255    0.6304    0.1342    0.2399
      0.2644    0.7743    0.0219    0.9775    0.6202
   

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
   
      0.5359    0.7353    0.6694    0.2503    0.1173    0.6259    0.2619    0.5033
      0.2810    0.7941    0.3772    0.2112    0.6596    0.6839    0.0695    0.2094
      0.2142    0.7772    0.3835    0.2213    0.8864    0.2363    0.6247    0.7145
      0.1803    0.8285    0.0221    0.5453    0.7841    0.8661    0.7271    0.8430
      0.6831    0.9639    0.1029    0.5540    0.8915    0.8128    0.6898    0.5545
      0.7704    0.0679    0.3958    0.0432    0.3724    0.7888    0.5798    0.4788
      0.2877    0.8601    0.6649    0.0834    0.3187    0.7665    0.9855    0.6188
      0.0119    0.9898    0.3103    0.0099    0.5127    0.9803    0.8822    0.7018
   
   B = 
   
      0.3853    0.6700    0.6335    0.4128    0.0310    0.5292    0.4481    0.3082
      0.7284    0.4431    0.8179    0.7342    0.7546    0.0585    0.4771    0.7945
      0.1495    0.2329    0.0909    0.7638    0.5608    0.5271    0.7173    0.1483
      0.2654    0.0696    0.9347    0.4721    0.4229    0.8266    0.7734    0.1802
      0.6132    0.3651    0.9898    0.6998    0.6643    0.8275    0.7407    0.8583
      0.6055    0.3094    0.0767    0.8109    0.7448    0.4329    0.5250    0.5498
      0.4061    0.1167    0.3576    0.1502    0.0583    0.9794    0.4941    0.3213
      0.9127    0.9524    0.0819    0.8321    0.0246    0.2014    0.7545    0.1391
   
   C = 
   
      1.9253    1.6046    1.5346    2.4384    1.6245    1.6123    2.1893    1.4926
      1.8371    1.3027    1.8065    2.2878    1.8656    1.5207    1.9786    1.8051
      2.3572    1.7428    2.1903    2.5568    1.7206    2.1354    2.5413    1.9710
      2.8909    1.9730    2.4750    3.0188    2.1029    2.5119    2.9444    2.3153
      2.9528    2.1329    2.9851    3.1779    2.2920    2.8070    3.1145    2.6022
      1.7953    1.5450    1.2955    2.0763    1.1958    1.9698    2.0325    1.3640
      2.4834    1.7925    1.8013    2.8051    1.9214    2.3073    2.6729    1.9833
      2.6813    1.7814    1.8100    2.8436    2.0649    2.0899    2.5676    2.1979
   
   D = 
   
      1.9253    1.6046    1.5346    2.4384    1.6245    1.6123    2.1893    1.4926
      1.8371    1.3027    1.8065    2.2878    1.8656    1.5207    1.9786    1.8051
      2.3572    1.7428    2.1903    2.5568    1.7206    2.1354    2.5413    1.9710
      2.8909    1.9730    2.4750    3.0188    2.1029    2.5119    2.9444    2.3153
      2.9528    2.1329    2.9851    3.1779    2.2920    2.8070    3.1145    2.6022
      1.7953    1.5450    1.2955    2.0763    1.1958    1.9698    2.0325    1.3640
      2.4834    1.7925    1.8013    2.8051    1.9214    2.3073    2.6729    1.9833
      2.6813    1.7814    1.8100    2.8436    2.0649    2.0899    2.5676    2.1979
   


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

   
      0.6556    0.9377    0.8977    0.7058    0.5296    0.1086
      0.6121    0.2831    0.5973    0.7613    0.2180    0.3422
      0.0121    0.4345    0.4470    0.7634    0.3026    0.5181
      0.9166    0.0189    0.6069    0.6029    0.5702    0.9847
      0.7996    0.6490    0.7118    0.5697    0.4825    0.3514
   
   
      0.6556
      0.6121
      0.9166
      0.7996
      0.9377
      0.6490
      0.8977
      0.5973
      0.6069
      0.7118
      0.7058
      0.7613
      0.7634
      0.6029
      0.5697
      0.5296
      0.5702
      0.5181
      0.9847
   

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

   
      3.4608    5.9426    0.5118    8.5083    6.4509    3.6611
      7.6507    0.9873    0.3118    1.3545    3.2855    6.4950
      5.5808    4.2649    0.8531    5.3486    8.8776    8.3685
      5.0558    9.8118    2.6277    9.7630    2.5833    8.0461
      7.8468    0.8618    5.6335    8.0870    7.0460    9.7162
   
   
      0.0000    5.9426    0.0000    8.5083    6.4509    0.0000
      7.6507    0.0000    0.0000    0.0000    0.0000    6.4950
      5.5808    0.0000    0.0000    5.3486    8.8776    8.3685
      5.0558    9.8118    0.0000    9.7630    0.0000    8.0461
      7.8468    0.0000    5.6335    8.0870    7.0460    9.7162
   
   
      0.0000    5.9426    0.0000    8.5083    6.4509    0.0000
      7.6507    0.0000    0.0000    0.0000    0.0000    6.4950
      5.5808    0.0000    0.0000    5.3486    8.8776    8.3685
      5.0558       NaN    0.0000       NaN    0.0000    8.0461
      7.8468    0.0000    5.6335    8.0870    7.0460       NaN
   

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

   
      2.1130    1.0030    6.5000    8.4332    4.4660    6.5000
      4.1662    3.1712    2.0004    0.1434    8.4977    4.2129
      8.1773    1.1490    4.2138    0.0437    0.5581    2.3332
      6.5000    2.4601    4.4137    2.3599    6.5000    9.7968
      8.4545    4.2459    0.1939    6.5000    3.8889    6.5000
   
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
   
