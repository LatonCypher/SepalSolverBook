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
      0.5509    0.4181    0.3393    0.3972
   
   R1[2] = 0.3392524550957352
   C1 = 
      0.3404
      0.5023
      0.2019
      0.5488
      0.3000
      0.3570
      0.7791
      0.2305
   
   C1[5] = 0.3570464160014991

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
      0.9694    0.5792    0.6806    0.9522    0.2468
      0.4467    0.3265    0.5407    0.1564    0.3297
   

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
   
      0.7046    0.0978    0.5274    0.6969    0.9648    0.9612    0.1158    0.5717
      0.6980    0.7425    0.9256    0.7213    0.2269    0.0754    0.6428    0.4344
      0.0272    0.9364    0.7795    0.7720    0.1400    0.4475    0.0030    0.8552
      0.7794    0.3425    0.7647    0.5540    0.7626    0.2150    0.9583    0.3379
      0.1714    0.3831    0.3537    0.0021    0.6057    0.5076    0.1376    0.8859
      0.7792    0.9674    0.2855    0.5129    0.3846    0.2494    0.0213    0.7460
      0.1578    0.1671    0.1858    0.9039    0.7887    0.3484    0.2256    0.0234
      0.1912    0.9866    0.3049    0.4336    0.5797    0.2278    0.7830    0.3104
   
   B = 
   
      0.4408    0.1229    0.5679    0.9020    0.7350    0.4595    0.5878    0.2036
      0.7580    0.0356    0.8199    0.9200    0.9842    0.1116    0.0349    0.6830
      0.4194    0.2398    0.9985    0.8419    0.0748    0.4312    0.7968    0.7348
      0.6519    0.2175    0.6973    0.8082    0.5468    0.7112    0.2759    0.7380
      0.7371    0.6350    0.3427    0.8313    0.9922    0.7579    0.4274    0.1198
      0.9504    0.6813    0.3000    0.5317    0.7883    0.1955    0.3246    0.5288
      0.2698    0.6369    0.6054    0.1153    0.7397    0.6800    0.4855    0.2302
      0.3215    0.3420    0.3729    0.6054    0.1573    0.9402    0.8279    0.7590
   
   C = 
   
      2.9000    1.9050    2.3952    3.4053    2.9253    2.5931    2.2840    2.1966
      2.2809    1.2445    3.0839    3.2406    2.5357    2.3480    2.1660    2.4065
      2.3562    1.0797    2.6029    3.0385    2.0504    2.0019    1.7974    2.6909
      2.4187    1.7685    2.9053    3.1729    2.9581    2.7094    2.3730    2.0455
      1.7665    1.2412    1.5397    2.1322    1.7729    1.7603    1.6205    1.6031
      2.2970    0.9931    2.3762    3.1541    2.5379    2.0101    1.7341    2.1569
      1.8444    1.1564    1.5626    2.0642    2.0164    1.6554    1.0752    1.2984
      2.1975    1.3543    2.3811    2.5684    2.7542    1.9460    1.4682    1.8625
   
   D = 
   
      2.9000    1.9050    2.3952    3.4053    2.9253    2.5931    2.2840    2.1966
      2.2809    1.2445    3.0839    3.2406    2.5357    2.3480    2.1660    2.4065
      2.3562    1.0797    2.6029    3.0385    2.0504    2.0019    1.7974    2.6909
      2.4187    1.7685    2.9053    3.1729    2.9581    2.7094    2.3730    2.0455
      1.7665    1.2412    1.5397    2.1322    1.7729    1.7603    1.6205    1.6031
      2.2970    0.9931    2.3762    3.1541    2.5379    2.0101    1.7341    2.1569
      1.8444    1.1564    1.5626    2.0642    2.0164    1.6554    1.0752    1.2984
      2.1975    1.3543    2.3811    2.5684    2.7542    1.9460    1.4682    1.8625
   


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

   
      0.4589    0.0157    0.3865    0.6546    0.4592    0.4890
      0.2166    0.1128    0.0197    0.9667    0.3809    0.7495
      0.5489    0.2874    0.5059    0.4558    0.3182    0.5957
      0.4211    0.8958    0.9043    0.1235    0.7874    0.0810
      0.1652    0.0138    0.2937    0.3797    0.4582    0.3151
   
   
      0.5489
      0.8958
      0.5059
      0.9043
      0.6546
      0.9667
      0.7874
      0.7495
      0.5957
   

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

   
      6.6128    3.8198    7.9351    0.1529    1.8314    1.0573
      1.6847    1.5638    6.3519    2.2721    7.7019    2.8102
      5.0658    7.1877    5.7139    2.4958    3.4811    5.6825
      0.7337    9.0450    4.9133    9.3890    0.7641    3.8307
      3.2783    4.1923    2.7254    5.1821    2.4254    8.6608
   
   
      6.6128    0.0000    7.9351    0.0000    0.0000    0.0000
      0.0000    0.0000    6.3519    0.0000    7.7019    0.0000
      5.0658    7.1877    5.7139    0.0000    0.0000    5.6825
      0.0000    9.0450    0.0000    9.3890    0.0000    0.0000
      0.0000    0.0000    0.0000    5.1821    0.0000    8.6608
   
   
      6.6128    0.0000    7.9351    0.0000    0.0000    0.0000
      0.0000    0.0000    6.3519    0.0000    7.7019    0.0000
      5.0658    7.1877    5.7139    0.0000    0.0000    5.6825
      0.0000       NaN    0.0000       NaN    0.0000    0.0000
      0.0000    0.0000    0.0000    5.1821    0.0000    8.6608
   

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

   
      8.2700    2.7291    0.2167    0.5923    6.5000    0.4967
      4.2540    6.5000    6.5000    6.5000    6.5000    3.2805
      1.8027    6.5000    6.5000    4.5339    9.9526    6.5000
      4.9867    6.5000    8.0954    2.4552    0.2800    4.8365
      3.7550    4.2201    3.1686    8.4186    2.2009    2.7403
   
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
   
