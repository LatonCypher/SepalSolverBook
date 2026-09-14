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
      0.1836    0.2394    0.5652    0.9590
   
   R1[2] = 0.5652476519893075
   C1 = 
      0.6772
      0.2608
      0.0460
      0.2339
      0.1752
      0.1229
      0.7995
      0.3947
   
   C1[5] = 0.12293792182753982

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
      0.7716    0.7020    0.4912    0.3249    0.6287
      0.9534    0.3781    0.9792    0.0316    0.9631
   

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
   
      0.5550    0.9895    0.2670    0.5890    0.6832    0.4107    0.5522    0.1429
      0.2372    0.0528    0.1763    0.5225    0.9268    0.9704    0.4984    0.1916
      0.2674    0.7606    0.7043    0.6808    0.7681    0.2959    0.1968    0.4905
      0.9487    0.6141    0.4047    0.8818    0.9037    0.6094    0.3128    0.1282
      0.2761    0.3807    0.0687    0.4100    0.7908    0.9889    0.1589    0.5185
      0.6407    0.8355    0.8684    0.0741    0.7468    0.1840    0.2333    0.9402
      0.4050    0.5194    0.3871    0.6599    0.0125    0.5170    0.3774    0.9547
      0.6695    0.9492    0.2356    0.0914    0.3544    0.1537    0.3646    0.5161
   
   B = 
   
      0.5978    0.7586    0.0690    0.7548    0.5534    0.3097    0.5000    0.8344
      0.3918    0.5746    0.5642    0.7312    0.9133    0.1799    0.5399    0.4380
      0.0736    0.0310    0.5505    0.1306    0.1048    0.2734    0.8094    0.3461
      0.8435    0.5013    0.5240    0.5879    0.1064    0.0051    0.2772    0.6036
      0.7279    0.4961    0.8788    0.9722    0.7291    0.5110    0.9752    0.1109
      0.7661    0.7963    0.4431    0.0284    0.3601    0.9783    0.2075    0.5685
      0.4410    0.0993    0.5146    0.9345    0.2137    0.4920    0.2834    0.2618
      0.8740    0.1653    0.2656    0.6463    0.1907    0.2782    0.9739    0.6592
   
   C = 
   
      2.4163    2.0375    2.1566    2.8078    2.0928    1.4882    2.2383    1.8925
      2.4215    1.7913    1.9688    2.0660    1.4218    1.8553    1.8677    1.5087
      2.3853    1.7203    2.2297    2.5063    1.7911    1.3309    2.6471    1.8392
      2.9560    2.5129    2.3560    3.0075    2.1918    1.7670    2.5992    2.3460
      2.5216    1.9170    1.8392    2.0173    1.6168    1.7688    2.0393    1.7017
      2.4461    1.7258    2.1401    2.8085    2.0565    1.5245    3.2432    2.1142
      2.4367    1.5615    1.5676    2.1204    1.2672    1.2914    2.1353    2.1211
      1.8541    1.5261    1.4636    2.3071    1.7619    1.0974    2.0467    1.6734
   
   D = 
   
      2.4163    2.0375    2.1566    2.8078    2.0928    1.4882    2.2383    1.8925
      2.4215    1.7913    1.9688    2.0660    1.4218    1.8553    1.8677    1.5087
      2.3853    1.7203    2.2297    2.5063    1.7911    1.3309    2.6471    1.8392
      2.9560    2.5129    2.3560    3.0075    2.1918    1.7670    2.5992    2.3460
      2.5216    1.9170    1.8392    2.0173    1.6168    1.7688    2.0393    1.7017
      2.4461    1.7258    2.1401    2.8085    2.0565    1.5245    3.2432    2.1142
      2.4367    1.5615    1.5676    2.1204    1.2672    1.2914    2.1353    2.1211
      1.8541    1.5261    1.4636    2.3071    1.7619    1.0974    2.0467    1.6734
   


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

   
      0.3291    0.8794    0.4626    0.3088    0.3967    0.8359
      0.0789    0.8784    0.5472    0.0243    0.8145    0.7739
      0.6238    0.6937    0.3844    0.6151    0.4654    0.7493
      0.6992    0.7699    0.1399    0.8825    0.0823    0.8282
      0.2568    0.7412    0.1528    0.1347    0.4707    0.6160
   
   
      0.6238
      0.6992
      0.8794
      0.8784
      0.6937
      0.7699
      0.7412
      0.5472
      0.6151
      0.8825
      0.8145
      0.8359
      0.7739
      0.7493
      0.8282
      0.6160
   

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

   
      6.6761    3.5072    2.6542    5.6527    9.7136    0.4383
      6.9843    7.7073    7.4229    1.3140    0.2692    0.2266
      4.7510    0.9664    6.8181    0.0789    1.2507    0.6041
      2.5294    9.7845    2.4447    9.1725    2.7710    7.0468
      3.9723    7.4510    8.6355    4.9975    6.3967    7.2017
   
   
      6.6761    0.0000    0.0000    5.6527    9.7136    0.0000
      6.9843    7.7073    7.4229    0.0000    0.0000    0.0000
      0.0000    0.0000    6.8181    0.0000    0.0000    0.0000
      0.0000    9.7845    0.0000    9.1725    0.0000    7.0468
      0.0000    7.4510    8.6355    0.0000    6.3967    7.2017
   
   
      6.6761    0.0000    0.0000    5.6527       NaN    0.0000
      6.9843    7.7073    7.4229    0.0000    0.0000    0.0000
      0.0000    0.0000    6.8181    0.0000    0.0000    0.0000
      0.0000       NaN    0.0000       NaN    0.0000    7.0468
      0.0000    7.4510    8.6355    0.0000    6.3967    7.2017
   

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

   
      2.4514    0.6557    1.3915    6.5000    9.8662    4.4960
      6.5000    3.2721    6.5000    4.1620    9.8564    0.9065
      6.5000    1.2431    6.5000    1.4833    2.1580    6.5000
      3.3256    4.3052    6.5000    6.5000    6.5000    8.2415
      9.6521    8.4968    8.7432    2.1303    6.5000    6.5000
   
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
   
