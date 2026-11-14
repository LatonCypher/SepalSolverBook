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
      0.7778    0.4959    0.1231    0.7013
   
   R1[2] = 0.12310741732873431
   C1 = 
      0.9084
      0.9824
      0.1740
      0.8739
      0.9251
      0.7708
      0.1564
      0.2580
   
   C1[5] = 0.7707741092172222

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
      0.1378    0.3894    0.2434    0.4248    0.4697
      0.2706    0.6806    0.7517    0.7311    0.8356
   

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
   
      0.1807    0.7046    0.0942    0.3091    0.5880    0.2189    0.5108    0.0204
      0.4244    0.0770    0.2269    0.4327    0.4061    0.9262    0.9695    0.6457
      0.8771    0.2814    0.7518    0.1209    0.9010    0.9491    0.5919    0.7244
      0.6205    0.9873    0.5675    0.4593    0.0824    0.9275    0.8071    0.9671
      0.7785    0.6300    0.2976    0.0891    0.7156    0.5344    0.0810    0.5221
      0.1864    0.4445    0.3287    0.8343    0.0599    0.9981    0.3263    0.7518
      0.3701    0.0478    0.7617    0.3432    0.2374    0.6967    0.5862    0.6836
      0.5308    0.2914    0.7073    0.2027    0.4049    0.1580    0.2418    0.0392
   
   B = 
   
      0.7643    0.4746    0.1001    0.9973    0.7453    0.0114    0.9357    0.3558
      0.2507    0.9535    0.8411    0.9485    0.3214    0.5339    0.3618    0.6142
      0.7826    0.8846    0.4311    0.5717    0.1763    0.2817    0.3258    0.1770
      0.0760    0.3276    0.1989    0.2259    0.6105    0.2913    0.9449    0.6688
      0.4832    0.5185    0.4724    0.1401    0.9571    0.8043    0.4919    0.3452
      0.8946    0.4288    0.4300    0.5342    0.4489    0.9828    0.5746    0.9100
      0.8676    0.5227    0.8916    0.1641    0.4212    0.4732    0.7918    0.1219
      0.8531    0.8233    0.9842    0.2537    0.9452    0.8110    0.4101    0.9943
   
   C = 
   
      1.3525    1.6248    1.5602    1.2606    1.4619    1.4411    1.5746    1.2053
      2.9709    2.2634    2.3812    1.5983    2.4684    2.4553    2.6723    2.2711
      3.7544    3.1692    2.7471    2.5128    3.1729    2.9323    3.0360    2.6660
      3.5955    3.5468    3.3376    2.8682    2.9094    2.9718    3.1659    3.1673
      2.3322    2.3350    1.9077    2.0956    2.3420    2.0175    2.0749    2.0386
      2.4208    2.3249    2.1884    1.7697    2.2024    2.3682    2.4004    2.6717
      2.7469    2.2986    2.0812    1.6025    2.0681    2.0518    2.1978    1.9926
      1.6280    1.6582    1.1569    1.4467    1.3351    1.0471    1.5216    0.9807
   
   D = 
   
      1.3525    1.6248    1.5602    1.2606    1.4619    1.4411    1.5746    1.2053
      2.9709    2.2634    2.3812    1.5983    2.4684    2.4553    2.6723    2.2711
      3.7544    3.1692    2.7471    2.5128    3.1729    2.9323    3.0360    2.6660
      3.5955    3.5468    3.3376    2.8682    2.9094    2.9718    3.1659    3.1673
      2.3322    2.3350    1.9077    2.0956    2.3420    2.0175    2.0749    2.0386
      2.4208    2.3249    2.1884    1.7697    2.2024    2.3682    2.4004    2.6717
      2.7469    2.2986    2.0812    1.6025    2.0681    2.0518    2.1978    1.9926
      1.6280    1.6582    1.1569    1.4467    1.3351    1.0471    1.5216    0.9807
   


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

   
      0.2743    0.1629    0.1790    0.9236    0.8856    0.1789
      0.9469    0.3191    0.9451    0.6478    0.1003    0.1075
      0.1633    0.6545    0.6456    0.8128    0.2854    0.4294
      0.3326    0.4738    0.8123    0.5517    0.3443    0.3340
      0.1423    0.7684    0.1522    0.3864    0.1397    0.4576
   
   
      0.9469
      0.6545
      0.7684
      0.9451
      0.6456
      0.8123
      0.9236
      0.6478
      0.8128
      0.5517
      0.8856
   

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

   
      5.8000    9.3717    0.9053    6.2468    2.9396    9.5436
      5.7241    7.3012    2.6318    4.5765    0.3374    0.1745
      0.5949    7.1756    5.7923    5.2835    1.1214    8.5109
      0.7736    6.5293    8.4779    8.1480    5.0080    4.1103
      9.8862    8.2276    6.2005    4.4071    1.1439    0.1724
   
   
      5.8000    9.3717    0.0000    6.2468    0.0000    9.5436
      5.7241    7.3012    0.0000    0.0000    0.0000    0.0000
      0.0000    7.1756    5.7923    5.2835    0.0000    8.5109
      0.0000    6.5293    8.4779    8.1480    5.0080    0.0000
      9.8862    8.2276    6.2005    0.0000    0.0000    0.0000
   
   
      5.8000       NaN    0.0000    6.2468    0.0000       NaN
      5.7241    7.3012    0.0000    0.0000    0.0000    0.0000
      0.0000    7.1756    5.7923    5.2835    0.0000    8.5109
      0.0000    6.5293    8.4779    8.1480    5.0080    0.0000
         NaN    8.2276    6.2005    0.0000    0.0000    0.0000
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      8.7740    6.5000    6.5000    6.5000    6.5000    6.5000
      1.4659    2.1630    2.9926    2.4046    4.8779    0.2165
      9.0574    0.4185    9.0893    4.2436    6.5000    8.7648
      2.7123    0.9803    2.8958    2.3559    9.4397    9.2158
      6.5000    6.5000    3.5631    9.7468    0.5549    6.5000
   
Advantages
^^^^^^^^^^


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
   
