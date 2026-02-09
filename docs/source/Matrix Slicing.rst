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
      0.4173    0.5227    0.0590    0.3838
   
   R1[2] = 0.05902708644274435
   C1 = 
      0.4802
      0.7553
      0.1898
      0.6545
      0.8788
      0.8259
      0.3899
      0.6646
   
   C1[5] = 0.825899254188682

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
      0.6182    0.5316    0.0765    0.6149    0.0552
      0.4542    0.9300    0.3981    0.2446    0.1152
   

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
   
      0.6194    0.1602    0.6944    0.3951    0.1925    0.0734    0.5620    0.7461
      0.9800    0.8024    0.0316    0.8716    0.9736    0.2839    0.8697    0.0774
      0.5102    0.1283    0.3163    0.5906    0.2469    0.3958    0.8254    0.2127
      0.9590    0.0699    0.1814    0.9862    0.2285    0.0593    0.6755    0.3893
      0.7098    0.5681    0.6299    0.6795    0.3557    0.8011    0.9697    0.0001
      0.1130    0.7538    0.5015    0.4896    0.3560    0.8195    0.8887    0.3056
      0.8730    0.7787    0.5860    0.7772    0.7441    0.4123    0.7026    0.0722
      0.5057    0.5130    0.1913    0.4752    0.3713    0.3107    0.5650    0.7895
   
   B = 
   
      0.5470    0.6364    0.4654    0.1337    0.5268    0.5649    0.2044    0.0339
      0.6879    0.9387    0.4113    0.1811    0.0874    0.8180    0.7363    0.9660
      0.5360    0.9904    0.0104    0.2562    0.5528    0.6450    0.7273    0.8538
      0.2554    0.3269    0.0793    0.6302    0.7525    0.3457    0.2651    0.7279
      0.1787    0.1720    0.0590    0.6391    0.7256    0.7898    0.4519    0.1912
      0.6668    0.1303    0.0933    0.8864    0.7243    0.4487    0.4049    0.7580
      0.8372    0.3870    0.5217    0.7206    0.6391    0.1289    0.4424    0.4280
      0.3373    0.3133    0.4171    0.0108    0.3415    0.8165    0.5321    0.0597
   
   C = 
   
      1.7276    1.8553    1.0153    1.1399    1.8283    1.9319    1.6167    1.4337
      2.4451    2.2583    1.4255    2.3351    2.7541    2.6032    2.0260    2.2479
      1.7586    1.4316    0.9112    1.6504    1.9652    1.4540    1.3356    1.5544
      1.6990    1.6083    1.0890    1.4983    2.1270    1.6687    1.2741    1.3736
      2.6999    2.3719    1.2261    2.4237    2.7413    2.2723    2.1159    2.6956
      2.4314    2.0440    1.0952    2.1864    2.2953    2.1862    2.1212    2.6045
      2.5463    2.5971    1.2733    2.2458    2.7490    2.6989    2.2365    2.6076
      1.8663    1.7185    1.1609    1.4374    1.8997    2.1430    1.7099    1.6174
   
   D = 
   
      1.7276    1.8553    1.0153    1.1399    1.8283    1.9319    1.6167    1.4337
      2.4451    2.2583    1.4255    2.3351    2.7541    2.6032    2.0260    2.2479
      1.7586    1.4316    0.9112    1.6504    1.9652    1.4540    1.3356    1.5544
      1.6990    1.6083    1.0890    1.4983    2.1270    1.6687    1.2741    1.3736
      2.6999    2.3719    1.2261    2.4237    2.7413    2.2723    2.1159    2.6956
      2.4314    2.0440    1.0952    2.1864    2.2953    2.1862    2.1212    2.6045
      2.5463    2.5971    1.2733    2.2458    2.7490    2.6989    2.2365    2.6076
      1.8663    1.7185    1.1609    1.4374    1.8997    2.1430    1.7099    1.6174
   


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

   
      0.0143    0.8010    0.6103    0.2517    0.8282    0.6101
      0.4899    0.2609    0.9290    0.6061    0.3878    0.8892
      0.6399    0.4221    0.6488    0.7311    0.5885    0.4647
      0.4082    0.5758    0.9786    0.5653    0.0935    0.0626
      0.1162    0.8528    0.2746    0.8950    0.8697    0.1886
   
   
      0.6399
      0.8010
      0.5758
      0.8528
      0.6103
      0.9290
      0.6488
      0.9786
      0.6061
      0.7311
      0.5653
      0.8950
      0.8282
      0.5885
      0.8697
      0.6101
      0.8892
   

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

   
      5.2502    6.9940    7.3823    9.8492    8.3523    7.5424
      4.5479    2.2784    5.8074    0.8616    0.0388    0.5291
      4.2572    5.0690    1.2232    4.4284    3.6607    3.8839
      9.2545    4.3665    7.6217    1.3442    1.4970    3.9901
      7.5687    9.0155    9.4095    8.2287    5.2618    5.8840
   
   
      5.2502    6.9940    7.3823    9.8492    8.3523    7.5424
      0.0000    0.0000    5.8074    0.0000    0.0000    0.0000
      0.0000    5.0690    0.0000    0.0000    0.0000    0.0000
      9.2545    0.0000    7.6217    0.0000    0.0000    0.0000
      7.5687    9.0155    9.4095    8.2287    5.2618    5.8840
   
   
      5.2502    6.9940    7.3823       NaN    8.3523    7.5424
      0.0000    0.0000    5.8074    0.0000    0.0000    0.0000
      0.0000    5.0690    0.0000    0.0000    0.0000    0.0000
         NaN    0.0000    7.6217    0.0000    0.0000    0.0000
      7.5687       NaN       NaN    8.2287    5.2618    5.8840
   

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

   
      2.9207    3.4940    6.5000    2.8149    9.6540    9.9092
      9.3635    6.5000    6.5000    8.0319    8.9256    1.6322
      9.1484    6.5000    1.3699    2.6731    1.8549    4.5860
      6.5000    6.5000    9.7233    6.5000    1.9953    0.3977
      6.5000    4.7530    4.3243    6.5000    3.3741    6.5000
   
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
   
