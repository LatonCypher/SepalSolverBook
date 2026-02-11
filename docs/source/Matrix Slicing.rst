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
      0.8079    0.9809    0.8761    0.5220
   
   R1[2] = 0.876066870567701
   C1 = 
      0.5616
      0.7052
      0.0891
      0.4331
      0.0606
      0.8419
      0.6442
      0.5186
   
   C1[5] = 0.8418584066731102

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
      0.3269    0.1698    0.0201    0.9812    0.1966
      0.7122    0.8217    0.1779    0.0677    0.2859
   

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
   
      0.6051    0.6740    0.3111    0.1117    0.5799    0.6446    0.5646    0.4714
      0.4337    0.2663    0.2744    0.3130    0.0180    0.3009    0.0677    0.9754
      0.3590    0.6304    0.6360    0.5151    0.6058    0.2308    0.6604    0.7198
      0.3493    0.2445    0.5476    0.6427    0.4075    0.2382    0.6125    0.2076
      0.1105    0.2803    0.1649    0.7561    0.9921    0.5661    0.3668    0.9112
      0.7383    0.0021    0.1354    0.0618    0.2145    0.6467    0.7306    0.3625
      0.0426    0.2223    0.9734    0.4761    0.0595    0.0632    0.6980    0.9994
      0.2801    0.7452    0.5631    0.1569    0.9064    0.8224    0.4796    0.3051
   
   B = 
   
      0.4358    0.5335    0.3018    0.2919    0.1331    0.9205    0.4208    0.0865
      0.2795    0.6989    0.2128    0.7147    0.4609    0.4248    0.4881    0.4024
      0.9938    0.0508    0.6628    0.9513    0.4872    0.8997    0.8367    0.7603
      0.2128    0.1376    0.5019    0.2639    0.2332    0.5317    0.8196    0.4252
      0.3095    0.0915    0.4940    0.8300    0.6218    0.1965    0.2753    0.8191
      0.7435    0.9260    0.5923    0.3061    0.8797    0.5224    0.9765    0.3783
      0.7256    0.1547    0.8416    0.5461    0.9574    0.1055    0.9898    0.6424
      0.3446    0.9866    0.9533    0.5782    0.2311    0.6342    0.0961    0.4007
   
   C = 
   
      2.0159    2.0275    2.1811    2.2433    2.1460    1.9919    2.3289    1.8781
      1.2172    1.7276    1.7005    1.3686    0.9533    1.7122    1.2582    1.0493
      2.1605    1.8167    2.6004    2.6464    2.1466    2.2101    2.5281    2.2834
      1.7207    1.0308    1.8987    1.8329    1.6732    1.6606    2.2224    1.7188
      1.7594    1.9380    2.5846    2.3129    2.0773    1.8785    2.2177    2.1969
      1.6723    1.4999    1.7935    1.3467    1.6651    1.5222    1.9243    1.2289
      2.0656    1.4510    2.5513    2.2506    1.6851    2.0147    2.1962    1.9571
      2.2684    1.9401    2.3244    2.6338    2.5084    2.0163    2.6381    2.3029
   
   D = 
   
      2.0159    2.0275    2.1811    2.2433    2.1460    1.9919    2.3289    1.8781
      1.2172    1.7276    1.7005    1.3686    0.9533    1.7122    1.2582    1.0493
      2.1605    1.8167    2.6004    2.6464    2.1466    2.2101    2.5281    2.2834
      1.7207    1.0308    1.8987    1.8329    1.6732    1.6606    2.2224    1.7188
      1.7594    1.9380    2.5846    2.3129    2.0773    1.8785    2.2177    2.1969
      1.6723    1.4999    1.7935    1.3467    1.6651    1.5222    1.9243    1.2289
      2.0656    1.4510    2.5513    2.2506    1.6851    2.0147    2.1962    1.9571
      2.2684    1.9401    2.3244    2.6338    2.5084    2.0163    2.6381    2.3029
   


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

   
      0.3604    0.9760    0.5385    0.9740    0.7021    0.4599
      0.8590    0.3974    0.5809    0.0385    0.1560    0.1597
      0.2548    0.4708    0.0006    0.5261    0.7756    0.1140
      0.9096    0.3674    0.9514    0.8734    0.4107    0.5667
      0.4489    0.1085    0.1646    0.2826    0.6050    0.4430
   
   
      0.8590
      0.9096
      0.9760
      0.5385
      0.5809
      0.9514
      0.9740
      0.5261
      0.8734
      0.7021
      0.7756
      0.6050
      0.5667
   

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

   
      9.7817    4.2802    0.9558    9.5009    3.4211    1.9838
      7.7887    7.8846    0.0437    9.9630    2.7760    6.5697
      1.0262    0.0695    4.7474    0.7996    6.7295    8.7244
      9.8009    8.0108    1.2867    9.8563    3.9640    9.2553
      6.4406    3.1225    7.4465    2.4828    5.2881    8.4676
   
   
      9.7817    0.0000    0.0000    9.5009    0.0000    0.0000
      7.7887    7.8846    0.0000    9.9630    0.0000    6.5697
      0.0000    0.0000    0.0000    0.0000    6.7295    8.7244
      9.8009    8.0108    0.0000    9.8563    0.0000    9.2553
      6.4406    0.0000    7.4465    0.0000    5.2881    8.4676
   
   
         NaN    0.0000    0.0000       NaN    0.0000    0.0000
      7.7887    7.8846    0.0000       NaN    0.0000    6.5697
      0.0000    0.0000    0.0000    0.0000    6.7295    8.7244
         NaN    8.0108    0.0000       NaN    0.0000       NaN
      6.4406    0.0000    7.4465    0.0000    5.2881    8.4676
   

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

   
      0.5821    1.7138    6.5000    6.5000    3.1022    4.5530
      2.3501    6.5000    9.4406    6.5000    6.5000    2.3385
      6.5000    1.4828    9.2287    2.4623    8.8091    1.9250
      4.1394    6.5000    9.7861    2.5436    8.0841    8.0937
      0.7005    3.3295    1.6575    6.5000    3.2499    1.3280
   
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
   
