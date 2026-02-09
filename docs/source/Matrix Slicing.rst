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
      0.8504    0.6074    0.2621    0.6207
   
   R1[2] = 0.2621459985762451
   C1 = 
      0.1089
      0.3987
      0.4724
      0.6106
      0.0164
      0.2521
      0.5373
      0.4243
   
   C1[5] = 0.25208040772341356

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
      0.5465    0.1074    0.1870    0.9293    0.3652
      0.5907    0.8050    0.3227    0.7268    0.2297
   

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
   
      0.4022    0.3254    0.9188    0.9202    0.8460    0.2316    0.1320    0.0462
      0.6343    0.7374    0.6344    0.1742    0.0375    0.0646    0.4424    0.3463
      0.3325    0.2091    0.5705    0.4242    0.1723    0.8607    0.9325    0.7832
      0.3186    0.0579    0.4158    0.6961    0.8297    0.4716    0.9737    0.9317
      0.2564    0.5306    0.2058    0.1779    0.9536    0.8674    0.6345    0.6432
      0.5097    0.5611    0.5383    0.2280    0.5629    0.0726    0.7313    0.4436
      0.1554    0.6940    0.6479    0.2287    0.7391    0.4343    0.9395    0.2260
      0.3753    0.6147    0.6973    0.0436    0.3513    0.7261    0.3103    0.7933
   
   B = 
   
      0.7608    0.6082    0.5218    0.4422    0.5163    0.6391    0.1568    0.1369
      0.2409    0.4838    0.0208    0.9167    0.9188    0.0448    0.0817    0.2394
      0.4220    0.6208    0.4894    0.9978    0.6505    0.1250    0.7776    0.1313
      0.5098    0.9766    0.5178    0.3654    0.1213    0.8524    0.4665    0.9836
      0.4991    0.8994    0.9269    0.5623    0.8455    0.6433    0.2387    0.9800
      0.3498    0.2064    0.2480    0.9225    0.9949    0.9205    0.2898    0.8645
      0.7759    0.5590    0.7033    0.3300    0.5859    0.2762    0.4221    0.1770
      0.1422    0.3547    0.3951    0.3204    0.5196    0.3470    0.0283    0.7389
   
   C = 
   
      1.8535    2.7701    2.0955    2.4769    2.2631    1.9807    1.5595    2.2456
      1.4505    1.7237    1.2457    1.9907    1.9740    0.9921    0.9585    0.9448
      1.9823    2.2036    2.0150    2.5126    2.7418    2.0872    1.4172    2.2444
      2.2538    2.8782    2.6703    2.3848    2.7980    2.4116    1.4750    2.8784
      1.8635    2.3337    2.1370    2.6221    3.1507    2.1753    1.0917    2.6361
      1.8032    2.2257    1.8885    2.1272    2.3638    1.3973    1.1274    1.5707
      1.9573    2.4157    2.0738    2.6337    2.8920    1.6194    1.3969    1.9306
      1.5330    1.9218    1.6098    2.6652    2.8310    1.6471    1.1194    1.9461
   
   D = 
   
      1.8535    2.7701    2.0955    2.4769    2.2631    1.9807    1.5595    2.2456
      1.4505    1.7237    1.2457    1.9907    1.9740    0.9921    0.9585    0.9448
      1.9823    2.2036    2.0150    2.5126    2.7418    2.0872    1.4172    2.2444
      2.2538    2.8782    2.6703    2.3848    2.7980    2.4116    1.4750    2.8784
      1.8635    2.3337    2.1370    2.6221    3.1507    2.1753    1.0917    2.6361
      1.8032    2.2257    1.8885    2.1272    2.3638    1.3973    1.1274    1.5707
      1.9573    2.4157    2.0738    2.6337    2.8920    1.6194    1.3969    1.9306
      1.5330    1.9218    1.6098    2.6652    2.8310    1.6471    1.1194    1.9461
   


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

   
      0.8651    0.3390    0.5176    0.8748    0.8880    0.8017
      0.1693    0.6653    0.6257    0.8724    0.7655    0.7072
      0.9584    0.5692    0.4338    0.8116    0.1390    0.4324
      0.6521    0.4296    0.4645    0.9099    0.2524    0.1212
      0.2099    0.5935    0.2327    0.4044    0.7731    0.9183
   
   
      0.8651
      0.9584
      0.6521
      0.6653
      0.5692
      0.5935
      0.5176
      0.6257
      0.8748
      0.8724
      0.8116
      0.9099
      0.8880
      0.7655
      0.7731
      0.8017
      0.7072
      0.9183
   

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

   
      1.3359    1.9433    6.8306    1.5662    9.2055    1.6370
      2.1585    8.9347    3.9873    4.1852    4.1669    1.6876
      3.8495    9.8598    7.5888    4.8356    0.5764    5.3612
      8.8355    7.2927    3.1074    3.2681    0.6541    3.7253
      9.3712    3.5475    3.9018    7.1264    0.1445    3.9417
   
   
      0.0000    0.0000    6.8306    0.0000    9.2055    0.0000
      0.0000    8.9347    0.0000    0.0000    0.0000    0.0000
      0.0000    9.8598    7.5888    0.0000    0.0000    5.3612
      8.8355    7.2927    0.0000    0.0000    0.0000    0.0000
      9.3712    0.0000    0.0000    7.1264    0.0000    0.0000
   
   
      0.0000    0.0000    6.8306    0.0000       NaN    0.0000
      0.0000    8.9347    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    7.5888    0.0000    0.0000    5.3612
      8.8355    7.2927    0.0000    0.0000    0.0000    0.0000
         NaN    0.0000    0.0000    7.1264    0.0000    0.0000
   

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

   
      0.6666    2.8061    6.5000    0.9945    2.7736    8.4709
      4.5178    9.4148    6.5000    1.3108    0.6345    9.8978
      4.2211    6.5000    8.5003    6.5000    6.5000    2.8330
      8.3107    6.5000    9.2951    1.6872    6.5000    2.9662
      6.5000    6.5000    9.2270    1.6909    6.5000    0.7734
   
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
   
