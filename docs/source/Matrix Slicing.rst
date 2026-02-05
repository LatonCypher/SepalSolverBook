Matrix Slicing
##############

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
      0.2151    0.0855    0.9365    0.5321
   
   R1[2] = 0.9364796050315887
   C1 = 
      0.2095
      0.5585
      0.4533
      0.3823
      0.9188
      0.9322
      0.8654
      0.5219
   
   C1[5] = 0.9322302252621041

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
      0.4557    0.3073    0.1167    0.0719    0.5929
      0.3211    0.2856    0.2099    0.8613    0.2778
   

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
   
      0.0432    0.0319    0.1207    0.1131    0.2973    0.3475    0.0110    0.7994
      0.9386    0.8776    0.6677    0.9333    0.9039    0.7614    0.5938    0.6740
      0.4965    0.4353    0.2662    0.2633    0.5367    0.3616    0.2921    0.4725
      0.7436    0.2430    0.5677    0.9492    0.8384    0.6390    0.2509    0.3783
      0.5751    0.5568    0.2760    0.1986    0.0459    0.2858    0.7274    0.5774
      0.2384    0.0294    0.1341    0.0077    0.6547    0.8800    0.6053    0.7511
      0.3679    0.5439    0.5287    0.9555    0.2042    0.9057    0.6958    0.2215
      0.9464    0.9730    0.9299    0.1310    0.2169    0.1416    0.1962    0.8640
   
   B = 
   
      0.0650    0.6575    0.4229    0.8846    0.9218    0.6330    0.7265    0.6524
      0.2020    0.2453    0.5204    0.8804    0.7180    0.0999    0.9100    0.7753
      0.8661    0.8181    0.6676    0.1740    0.3987    0.1753    0.0683    0.6860
      0.5146    0.8166    0.5585    0.1635    0.5951    0.0796    0.1388    0.3692
      0.9092    0.2021    0.0957    0.5461    0.7486    0.3907    0.0989    0.6418
      0.0063    0.1599    0.8850    0.0182    0.0447    0.4630    0.2740    0.0165
      0.3299    0.3568    0.5166    0.2555    0.6250    0.6457    0.6885    0.9358
      0.9335    0.4542    0.6873    0.9344    0.0962    0.8421    0.0873    0.4078
   
   C = 
   
      1.1943    0.7099    1.0697    1.0242    0.5000    1.0180    0.2863    0.7103
      2.9485    2.9631    3.3510    3.1605    3.4635    2.5298    2.4213    3.5186
      1.5139    1.3511    1.6083    1.7275    1.6790    1.3890    1.2061    1.7577
      2.2798    2.3210    2.3854    2.0127    2.5005    1.7740    1.3957    2.3511
      1.3136    1.4795    1.8581    1.8350    1.7154    1.5900    1.6050    2.0199
      1.6431    1.1100    1.8803    1.4914    1.2790    1.8646    0.9984    1.5806
      1.7110    2.1230    2.6580    1.5650    2.1585    1.5909    1.6977    2.2646
      2.2000    2.2575    2.4418    2.8555    2.3941    1.8742    1.9255    2.7356
   
   D = 
   
      1.1943    0.7099    1.0697    1.0242    0.5000    1.0180    0.2863    0.7103
      2.9485    2.9631    3.3510    3.1605    3.4635    2.5298    2.4213    3.5186
      1.5139    1.3511    1.6083    1.7275    1.6790    1.3890    1.2061    1.7577
      2.2798    2.3210    2.3854    2.0127    2.5005    1.7740    1.3957    2.3511
      1.3136    1.4795    1.8581    1.8350    1.7154    1.5900    1.6050    2.0199
      1.6431    1.1100    1.8803    1.4914    1.2790    1.8646    0.9984    1.5806
      1.7110    2.1230    2.6580    1.5650    2.1585    1.5909    1.6977    2.2646
      2.2000    2.2575    2.4418    2.8555    2.3941    1.8742    1.9255    2.7356
   


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

   
      0.0721    0.2119    0.1647    0.0309    0.1211    0.4134
      0.2945    0.8479    0.3581    0.9761    0.1403    0.0352
      0.5792    0.5911    0.6657    0.2087    0.4396    0.0996
      0.6062    0.5684    0.8089    0.5303    0.7735    0.2972
      0.2544    0.0466    0.7946    0.0163    0.2352    0.0987
   
   
      0.5792
      0.6062
      0.8479
      0.5911
      0.5684
      0.6657
      0.8089
      0.7946
      0.9761
      0.5303
      0.7735
   

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

   
      2.5075    4.8109    7.3790    1.8968    6.9592    5.5200
      5.6926    5.9411    2.3317    9.2553    3.3618    9.4873
      1.9446    5.1811    3.3691    3.4217    1.3556    8.4913
      4.4850    8.1128    3.2076    2.8172    5.8349    0.2098
      5.5122    7.1505    1.6578    2.5219    5.2950    2.4643
   
   
      0.0000    0.0000    7.3790    0.0000    6.9592    5.5200
      5.6926    5.9411    0.0000    9.2553    0.0000    9.4873
      0.0000    5.1811    0.0000    0.0000    0.0000    8.4913
      0.0000    8.1128    0.0000    0.0000    5.8349    0.0000
      5.5122    7.1505    0.0000    0.0000    5.2950    0.0000
   
   
      0.0000    0.0000    7.3790    0.0000    6.9592    5.5200
      5.6926    5.9411    0.0000       NaN    0.0000       NaN
      0.0000    5.1811    0.0000    0.0000    0.0000    8.4913
      0.0000    8.1128    0.0000    0.0000    5.8349    0.0000
      5.5122    7.1505    0.0000    0.0000    5.2950    0.0000
   

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

   
      6.5000    8.1244    8.3608    6.5000    8.7551    4.0219
      1.8204    4.3011    1.5659    6.5000    9.6563    0.9880
      2.9895    2.7434    1.1340    1.4870    9.4896    4.8063
      6.5000    4.4904    8.0544    8.9374    2.3253    6.5000
      9.4774    8.2612    6.5000    9.7273    9.2026    2.6231
   
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
   
