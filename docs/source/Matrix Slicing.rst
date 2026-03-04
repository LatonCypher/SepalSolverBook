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
      0.1406    0.1397    0.9350    0.5736
   
   R1[2] = 0.9350425189820333
   C1 = 
      0.9107
      0.4737
      0.9321
      0.9005
      0.6302
      0.5601
      0.5305
      0.1857
   
   C1[5] = 0.5601206248903188

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
      0.7774    0.0875    0.4153    0.3196    0.0523
      0.9972    0.8927    0.8365    0.4667    0.6042
   

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
   
      0.5294    0.3222    0.3472    0.9798    0.5586    0.1566    0.0209    0.2393
      0.7773    0.6017    0.8900    0.7302    0.3131    0.6174    0.6042    0.9221
      0.8976    0.8635    0.3036    0.5805    0.2701    0.4499    0.6103    0.8733
      0.3331    0.9035    0.8543    0.0466    0.4472    0.3987    0.4027    0.9686
      0.9675    0.0423    0.8980    0.6302    0.5885    0.5768    0.1787    0.4503
      0.6033    0.9974    0.6537    0.0792    0.4864    0.7189    0.6643    0.7754
      0.1170    0.2508    0.7338    0.4168    0.1500    0.5538    0.3176    0.5437
      0.3732    0.4715    0.3981    0.2833    0.6010    0.6746    0.6896    0.9445
   
   B = 
   
      0.8118    0.2049    0.0367    0.6904    0.5227    0.3777    0.2667    0.2516
      0.9244    0.8682    0.5797    0.1349    0.9804    0.6925    0.5190    0.4389
      0.4817    0.9068    0.3390    0.1685    0.3566    0.0585    0.2253    0.7623
      0.1207    0.5048    0.8691    0.6504    0.1252    0.6363    0.7129    0.3984
      0.0057    0.3064    0.1472    0.6967    0.2437    0.1672    0.8465    0.6575
      0.9915    0.9813    0.6157    0.9268    0.2645    0.6191    0.8934    0.6625
      0.2753    0.9346    0.4782    0.0457    0.6732    0.8519    0.2248    0.2378
      0.0541    0.1757    0.5083    0.3267    0.6217    0.1061    0.3700    0.7874
   
   C = 
   
      1.1902    1.5841    1.4857    1.7181    1.1795    1.3004    1.7911    1.5940
      2.5342    3.2858    2.4975    2.3620    2.6246    2.2741    2.5344    2.9137
      2.4060    2.7500    2.1934    2.0833    2.6353    2.2603    2.2607    2.3760
      2.0838    2.7257    1.8623    1.5420    2.4581    1.5985    1.9672    2.5668
      1.9820    2.3599    1.6682    2.3347    1.6426    1.5037    2.1518    2.3637
      2.6765    3.2339    2.1168    2.0015    2.7742    2.1817    2.3726    2.6841
      1.3974    2.0993    1.5520    1.3192    1.3558    1.2222    1.5182    1.8341
      1.8779    2.6463    1.9818    1.9566    2.2110    1.8768    2.2518    2.4668
   
   D = 
   
      1.1902    1.5841    1.4857    1.7181    1.1795    1.3004    1.7911    1.5940
      2.5342    3.2858    2.4975    2.3620    2.6246    2.2741    2.5344    2.9137
      2.4060    2.7500    2.1934    2.0833    2.6353    2.2603    2.2607    2.3760
      2.0838    2.7257    1.8623    1.5420    2.4581    1.5985    1.9672    2.5668
      1.9820    2.3599    1.6682    2.3347    1.6426    1.5037    2.1518    2.3637
      2.6765    3.2339    2.1168    2.0015    2.7742    2.1817    2.3726    2.6841
      1.3974    2.0993    1.5520    1.3192    1.3558    1.2222    1.5182    1.8341
      1.8779    2.6463    1.9818    1.9566    2.2110    1.8768    2.2518    2.4668
   


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

   
      0.7001    0.1846    0.9558    0.8981    0.5358    0.3753
      0.4527    0.6798    0.2366    0.2525    0.0038    0.0567
      0.6797    0.2074    0.4252    0.9309    0.8163    0.9307
      0.5911    0.1953    0.5752    0.7369    0.3476    0.9207
      0.6307    0.0743    0.8150    0.8173    0.4132    0.9528
   
   
      0.7001
      0.6797
      0.5911
      0.6307
      0.6798
      0.9558
      0.5752
      0.8150
      0.8981
      0.9309
      0.7369
      0.8173
      0.5358
      0.8163
      0.9307
      0.9207
      0.9528
   

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

   
      7.1467    8.6772    3.4241    1.8923    1.9722    3.1967
      0.0551    4.6176    7.4146    4.8035    1.9979    9.2422
      9.2858    4.4591    0.5906    1.9582    6.2410    8.8515
      0.4267    9.2237    9.0771    9.3455    9.9091    8.5789
      3.3149    5.0373    4.4614    4.4839    1.7601    0.4568
   
   
      7.1467    8.6772    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    7.4146    0.0000    0.0000    9.2422
      9.2858    0.0000    0.0000    0.0000    6.2410    8.8515
      0.0000    9.2237    9.0771    9.3455    9.9091    8.5789
      0.0000    5.0373    0.0000    0.0000    0.0000    0.0000
   
   
      7.1467    8.6772    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    7.4146    0.0000    0.0000       NaN
         NaN    0.0000    0.0000    0.0000    6.2410    8.8515
      0.0000       NaN       NaN       NaN       NaN    8.5789
      0.0000    5.0373    0.0000    0.0000    0.0000    0.0000
   

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

   
      9.2213    1.6253    9.7159    6.5000    6.5000    1.5510
      6.5000    1.5249    8.7024    1.3248    2.6453    9.5208
      4.1899    9.1415    4.9834    0.8430    6.5000    6.5000
      3.8667    2.8606    4.3483    6.5000    3.0969    2.7895
      6.5000    2.4645    3.1156    2.2115    0.2039    6.5000
   
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
   
