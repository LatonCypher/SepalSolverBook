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
      0.8578    0.7317    0.2557    0.0638
   
   R1[2] = 0.2556686472196009
   C1 = 
      0.4495
      0.1859
      0.0003
      0.7225
      0.0891
      0.9071
      0.5926
      0.5238
   
   C1[5] = 0.9070760197760672

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
      0.9652    0.8258    0.8610    0.9291    0.9850
      0.5505    0.4281    0.8428    0.8657    0.8929
   

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
   
      0.1087    0.4880    0.9375    0.4102    0.0497    0.6403    0.3439    0.0399
      0.6139    0.0708    0.5333    0.4879    0.7867    0.3401    0.4323    0.9012
      0.8942    0.0298    0.0534    0.6057    0.9947    0.1714    0.5933    0.6143
      0.7450    0.1478    0.1146    0.4505    0.9336    0.2982    0.2969    0.2097
      0.2009    0.5163    0.6980    0.3869    0.0416    0.0171    0.7281    0.2567
      0.5881    0.4863    0.9890    0.0288    0.8364    0.5965    0.6581    0.5800
      0.4734    0.4111    0.0003    0.9051    0.5239    0.1892    0.1806    0.4470
      0.0991    0.8186    0.1255    0.6934    0.6175    0.5161    0.7923    0.1731
   
   B = 
   
      0.2303    0.9344    0.4214    0.4013    0.8264    0.2230    0.3266    0.6432
      0.2816    0.0965    0.8498    0.5128    0.0267    0.5592    0.4872    0.0570
      0.8118    0.7279    0.5904    0.1560    0.6346    0.2240    0.4900    0.2936
      0.1661    0.9491    0.1252    0.7503    0.3474    0.7182    0.5715    0.5348
      0.0494    0.5618    0.7287    0.8220    0.9235    0.1928    0.5464    0.1822
      0.6289    0.7794    0.3757    0.8686    0.7261    0.3783    0.7652    0.9608
      0.3579    0.7797    0.4010    0.0525    0.1829    0.6989    0.8952    0.7587
      0.0316    0.7141    0.6728    0.9276    0.0887    0.7387    0.4902    0.0968
   
   C = 
   
      1.5212    2.0441    1.5070    1.4001    1.4176    1.3235    1.8117    1.4815
      1.1114    3.1195    2.1757    2.5328    2.1497    1.8946    2.2941    1.7019
      0.7471    3.0460    1.9502    2.4046    2.1903    1.7882    2.1859    1.7720
      0.7276    2.3595    1.6161    1.9672    2.0004    1.2532    1.7358    1.4643
      1.1042    1.9007    1.4855    1.0701    0.9641    1.4809    1.6938    1.1719
      1.7504    3.2058    2.7364    2.4395    2.5140    1.9207    2.7170    1.9928
      0.5991    2.2432    1.4885    2.0991    1.4108    1.6146    1.6842    1.2696
      1.1146    2.4116    1.9765    2.1574    1.5295    2.0019    2.4154    1.7443
   
   D = 
   
      1.5212    2.0441    1.5070    1.4001    1.4176    1.3235    1.8117    1.4815
      1.1114    3.1195    2.1757    2.5328    2.1497    1.8946    2.2941    1.7019
      0.7471    3.0460    1.9502    2.4046    2.1903    1.7882    2.1859    1.7720
      0.7276    2.3595    1.6161    1.9672    2.0004    1.2532    1.7358    1.4643
      1.1042    1.9007    1.4855    1.0701    0.9641    1.4809    1.6938    1.1719
      1.7504    3.2058    2.7364    2.4395    2.5140    1.9207    2.7170    1.9928
      0.5991    2.2432    1.4885    2.0991    1.4108    1.6146    1.6842    1.2696
      1.1146    2.4116    1.9765    2.1574    1.5295    2.0019    2.4154    1.7443
   


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

   
      0.7875    0.4654    0.7802    0.9196    0.8362    0.1805
      0.2097    0.2170    0.6003    0.7906    0.4768    0.5516
      0.1118    0.3999    0.5905    0.9289    0.2196    0.5518
      0.6115    0.8349    0.3000    0.3160    0.7020    0.3218
      0.7540    0.7067    0.2096    0.8947    0.1569    0.7855
   
   
      0.7875
      0.6115
      0.7540
      0.8349
      0.7067
      0.7802
      0.6003
      0.5905
      0.9196
      0.7906
      0.9289
      0.8947
      0.8362
      0.7020
      0.5516
      0.5518
      0.7855
   

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

   
      4.4524    8.5063    0.1727    6.0232    9.3409    0.6593
      9.5437    6.2564    1.1843    6.1001    6.0978    7.4870
      7.3509    4.0989    6.0100    0.3602    4.7564    5.9688
      6.9755    3.8925    2.5045    8.6859    0.7049    0.5757
      9.9075    1.7509    8.5490    6.8160    6.4809    0.2371
   
   
      0.0000    8.5063    0.0000    6.0232    9.3409    0.0000
      9.5437    6.2564    0.0000    6.1001    6.0978    7.4870
      7.3509    0.0000    6.0100    0.0000    0.0000    5.9688
      6.9755    0.0000    0.0000    8.6859    0.0000    0.0000
      9.9075    0.0000    8.5490    6.8160    6.4809    0.0000
   
   
      0.0000    8.5063    0.0000    6.0232       NaN    0.0000
         NaN    6.2564    0.0000    6.1001    6.0978    7.4870
      7.3509    0.0000    6.0100    0.0000    0.0000    5.9688
      6.9755    0.0000    0.0000    8.6859    0.0000    0.0000
         NaN    0.0000    8.5490    6.8160    6.4809    0.0000
   

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

   
      9.3071    8.6298    6.5000    3.6694    6.5000    6.5000
      6.5000    1.4786    6.5000    0.9784    4.5422    2.9554
      6.5000    6.5000    6.5000    0.7562    4.8809    0.5370
      6.5000    6.5000    6.5000    2.9823    2.1682    9.1091
      9.9917    4.7569    8.7530    8.6650    6.5000    6.5000
   
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
   
