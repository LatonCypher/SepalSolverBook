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
      0.6614    0.8329    0.3093    0.5045
   
   R1[2] = 0.309344827816473
   C1 = 
      0.4554
      0.6667
      0.1546
      0.7838
      0.7121
      0.1243
      0.0573
      0.4657
   
   C1[5] = 0.12429172307540048

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
      0.9538    0.1504    0.8404    0.9782    0.1785
      0.5003    0.3746    0.2006    0.2111    0.2577
   

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
   
      0.2842    0.5283    0.5151    0.4010    0.1952    0.4260    0.0086    0.3590
      0.4465    0.2519    0.6801    0.1066    0.2302    0.9349    0.1020    0.0143
      0.8578    0.4143    0.3994    0.2315    0.7617    0.2132    0.5264    0.8893
      0.2315    0.4713    0.4912    0.5733    0.8757    0.0743    0.7846    0.6260
      0.8831    0.1233    0.3536    0.0655    0.6154    0.8986    0.9581    0.3989
      0.3969    0.6297    0.8527    0.5463    0.6417    0.2641    0.0030    0.6957
      0.5793    0.9118    0.6607    0.9675    0.0660    0.1711    0.9657    0.7825
      0.1497    0.3238    0.4176    0.6029    0.7526    0.7011    0.4970    0.6641
   
   B = 
   
      0.1854    0.4747    0.2573    0.6425    0.0521    0.3908    0.8074    0.9530
      0.2579    0.6725    0.2152    0.7970    0.9594    0.6839    0.5171    0.2731
      0.1226    0.0138    0.5386    0.6702    0.0567    0.0471    0.6387    0.0260
      0.2320    0.4317    0.1596    0.8200    0.5195    0.9374    0.1284    0.6216
      0.4237    0.8519    0.7404    0.5605    0.8250    0.2069    0.6050    0.5061
      0.4996    0.7014    0.0898    0.8898    0.9525    0.3551    0.6109    0.6137
      0.5476    0.1150    0.8190    0.9622    0.0739    0.0833    0.3821    0.0260
      0.1930    0.2899    0.2911    0.2483    0.4006    0.0603    0.8039    0.7850
   
   C = 
   
      0.7147    1.2406    0.8226    1.8636    1.4705    1.0866    1.5533    1.3201
      0.8792    1.3046    0.8945    2.0935    1.4526    0.8678    1.6997    1.2824
      1.2577    1.9080    1.8351    2.6827    1.8118    1.1851    2.6987    2.3131
      1.3163    1.7509    1.9968    2.7909    1.8920    1.2840    2.1961    1.7192
      1.5654    1.9157    1.8918    3.1218    1.8127    1.0577    2.6190    2.1261
      1.0070    1.7934    1.4879    2.5465    2.0168    1.4068    2.3708    1.9451
      1.4413    1.8291    1.9385    3.6477    2.0473    1.9900    2.6278    2.1975
      1.3719    1.9375    1.6500    2.8173    2.2469    1.3509    2.2399    1.9622
   
   D = 
   
      0.7147    1.2406    0.8226    1.8636    1.4705    1.0866    1.5533    1.3201
      0.8792    1.3046    0.8945    2.0935    1.4526    0.8678    1.6997    1.2824
      1.2577    1.9080    1.8351    2.6827    1.8118    1.1851    2.6987    2.3131
      1.3163    1.7509    1.9968    2.7909    1.8920    1.2840    2.1961    1.7192
      1.5654    1.9157    1.8918    3.1218    1.8127    1.0577    2.6190    2.1261
      1.0070    1.7934    1.4879    2.5465    2.0168    1.4068    2.3708    1.9451
      1.4413    1.8291    1.9385    3.6477    2.0473    1.9900    2.6278    2.1975
      1.3719    1.9375    1.6500    2.8173    2.2469    1.3509    2.2399    1.9622
   


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

   
      0.0794    0.1088    0.6698    0.5035    0.3919    0.5597
      0.1998    0.2415    0.4009    0.6120    0.8287    0.5913
      0.2733    0.4441    0.4048    0.3036    0.6281    0.5138
      0.7294    0.8485    0.8735    0.1757    0.4460    0.8073
      0.3951    0.1278    0.3738    0.2920    0.6349    0.0065
   
   
      0.7294
      0.8485
      0.6698
      0.8735
      0.5035
      0.6120
      0.8287
      0.6281
      0.6349
      0.5597
      0.5913
      0.5138
      0.8073
   

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

   
      0.2920    1.6295    6.0436    2.6184    6.3052    3.0167
      1.6400    4.7110    0.9863    3.1135    0.7535    7.1732
      2.1383    8.5019    3.6105    7.0940    1.7073    3.6174
      3.9922    4.4973    2.5784    4.7597    5.8389    4.4279
      0.7590    9.4650    9.2752    0.9638    1.0314    2.4795
   
   
      0.0000    0.0000    6.0436    0.0000    6.3052    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    7.1732
      0.0000    8.5019    0.0000    7.0940    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    5.8389    0.0000
      0.0000    9.4650    9.2752    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    6.0436    0.0000    6.3052    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    7.1732
      0.0000    8.5019    0.0000    7.0940    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    5.8389    0.0000
      0.0000       NaN       NaN    0.0000    0.0000    0.0000
   

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

   
      2.2910    4.1498    6.5000    6.5000    1.8365    4.2791
      8.7954    6.5000    9.9904    8.3836    0.7885    4.1079
      0.0997    3.8917    8.9077    1.6201    2.1184    8.0660
      6.5000    0.7818    6.5000    4.8111    3.1190    2.5786
      4.9552    2.0990    6.5000    6.5000    4.1327    8.4430
   
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
   
