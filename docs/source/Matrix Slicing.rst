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
      0.0696    0.4643    0.4028    0.7308
   
   R1[2] = 0.40283356603032416
   C1 = 
      0.5544
      0.9972
      0.4634
      0.4405
      0.6184
      0.1525
      0.3726
      0.8908
   
   C1[5] = 0.1525171683138119

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
      0.6205    0.5878    0.1434    0.0640    0.5708
      0.6971    0.3067    0.7315    0.7706    0.6402
   

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
   
      0.8851    0.9707    0.2564    0.9000    0.9235    0.1933    0.5655    0.8450
      0.9960    0.5814    0.3335    0.9061    0.0058    0.0680    0.6570    0.3027
      0.0081    0.0108    0.2840    0.2436    0.4256    0.6717    0.3533    0.1640
      0.9108    0.9095    0.2624    0.4470    0.8158    0.7046    0.1550    0.3287
      0.7808    0.1502    0.2955    0.9529    0.6142    0.4523    0.2248    0.2601
      0.1548    0.9107    0.7287    0.6353    0.3074    0.9580    0.0974    0.8336
      0.5603    0.0933    0.6705    0.4220    0.9998    0.8036    0.8880    0.6395
      0.9344    0.5662    0.8704    0.7015    0.2749    0.5264    0.7366    0.1684
   
   B = 
   
      0.7368    0.5437    0.0210    0.8514    0.8586    0.4479    0.7738    0.1332
      0.3568    0.0746    0.0106    0.7598    0.0631    0.5532    0.0896    0.0567
      0.4252    0.5815    0.2433    0.4882    0.8791    0.3056    0.0454    0.9484
      0.0330    0.7660    0.9549    0.6074    0.1764    0.6188    0.2422    0.6820
      0.6679    0.5885    0.9697    0.2411    0.5479    0.5424    0.4582    0.1547
      0.9479    0.4363    0.5194    0.8522    0.5388    0.2599    0.4061    0.3147
      0.8305    0.3658    0.9026    0.3873    0.6645    0.5630    0.6614    0.8794
      0.9538    0.8925    0.9867    0.3642    0.9038    0.5153    0.8444    0.3126
   
   C = 
   
      3.2128    2.9811    3.2907    3.0772    2.9551    2.8736    2.5906    1.9950
      2.0157    2.0165    1.9061    2.4271    2.0949    1.9770    1.7777    1.7945
      1.5093    1.1761    1.5442    1.1733    1.2783    0.9359    0.9190    1.0763
      2.7770    2.1958    2.1408    2.8432    2.3758    2.1502    1.9465    1.3136
      2.0596    2.2107    2.2899    2.2173    2.0724    1.8240    1.6952    1.5589
      2.7592    2.4412    2.5030    2.7973    2.4458    2.0890    1.6866    1.8919
      3.5220    2.8594    3.3983    2.6344    3.2997    2.3492    2.4862    2.3920
      2.7386    2.4050    2.2783    2.9383    2.8029    2.2191    1.9522    2.3690
   
   D = 
   
      3.2128    2.9811    3.2907    3.0772    2.9551    2.8736    2.5906    1.9950
      2.0157    2.0165    1.9061    2.4271    2.0949    1.9770    1.7777    1.7945
      1.5093    1.1761    1.5442    1.1733    1.2783    0.9359    0.9190    1.0763
      2.7770    2.1958    2.1408    2.8432    2.3758    2.1502    1.9465    1.3136
      2.0596    2.2107    2.2899    2.2173    2.0724    1.8240    1.6952    1.5589
      2.7592    2.4412    2.5030    2.7973    2.4458    2.0890    1.6866    1.8919
      3.5220    2.8594    3.3983    2.6344    3.2997    2.3492    2.4862    2.3920
      2.7386    2.4050    2.2783    2.9383    2.8029    2.2191    1.9522    2.3690
   


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

   
      0.8777    0.9917    0.5153    0.4063    0.0141    0.2349
      0.6153    0.8006    0.4687    0.5437    0.3280    0.7773
      0.4305    0.4781    0.9763    0.0417    0.3960    0.7125
      0.1261    0.9657    0.9759    0.2634    0.5299    0.7787
      0.6987    0.7929    0.6013    0.7583    0.8174    0.0631
   
   
      0.8777
      0.6153
      0.6987
      0.9917
      0.8006
      0.9657
      0.7929
      0.5153
      0.9763
      0.9759
      0.6013
      0.5437
      0.7583
      0.5299
      0.8174
      0.7773
      0.7125
      0.7787
   

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

   
      9.6050    9.1412    6.0841    7.9936    3.6676    1.5206
      9.7567    6.6475    3.3858    1.4182    8.6662    7.8760
      9.4152    6.3494    3.9426    5.4864    1.9792    5.2236
      7.2708    1.8131    3.3414    6.4284    8.8416    6.2577
      9.3315    7.9810    1.7294    6.7743    7.0341    1.4062
   
   
      9.6050    9.1412    6.0841    7.9936    0.0000    0.0000
      9.7567    6.6475    0.0000    0.0000    8.6662    7.8760
      9.4152    6.3494    0.0000    5.4864    0.0000    5.2236
      7.2708    0.0000    0.0000    6.4284    8.8416    6.2577
      9.3315    7.9810    0.0000    6.7743    7.0341    0.0000
   
   
         NaN       NaN    6.0841    7.9936    0.0000    0.0000
         NaN    6.6475    0.0000    0.0000    8.6662    7.8760
         NaN    6.3494    0.0000    5.4864    0.0000    5.2236
      7.2708    0.0000    0.0000    6.4284    8.8416    6.2577
         NaN    7.9810    0.0000    6.7743    7.0341    0.0000
   

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

   
      4.0275    1.7479    8.9723    4.5382    0.9753    3.1030
      9.6036    1.4689    6.5000    3.4694    6.5000    6.5000
      6.5000    0.2058    4.9778    6.5000    3.6796    6.5000
      3.6414    6.5000    1.7240    6.5000    2.1745    6.5000
      8.0388    1.9303    0.1247    4.9367    6.5000    8.8186
   
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
   
