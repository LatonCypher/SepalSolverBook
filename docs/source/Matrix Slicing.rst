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
      0.5659    0.4977    0.1826    0.2979
   
   R1[2] = 0.18264768934955644
   C1 = 
      0.4913
      0.3565
      0.2991
      0.4086
      0.4850
      0.4630
      0.4703
      0.2649
   
   C1[5] = 0.46301821813655686

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
      0.2816    0.3789    0.6907    0.6257    0.1411
      0.4587    0.3073    0.8370    0.1847    0.4289
   

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
   
      0.7275    0.0193    0.6323    0.1975    0.6162    0.8032    0.0953    0.3401
      0.5016    0.8561    0.2352    0.7490    0.7606    0.3850    0.9416    0.1841
      0.2455    0.5243    0.8726    0.6723    0.8585    0.3073    0.2172    0.2276
      0.9869    0.8242    0.0655    0.4768    0.3278    0.3887    0.2244    0.0109
      0.9285    0.1949    0.9256    0.0271    0.4348    0.6108    0.0616    0.0661
      0.0197    0.5983    0.9870    0.3190    0.0718    0.6403    0.5237    0.2342
      0.9923    0.1202    0.1461    0.7212    0.1788    0.3662    0.8869    0.2505
      0.6540    0.4193    0.4639    0.5645    0.7275    0.2150    0.0072    0.3058
   
   B = 
   
      0.7521    0.0095    0.1560    0.6844    0.7381    0.2688    0.1222    0.0205
      0.2360    0.1695    0.0247    0.1692    0.9868    0.0229    0.1363    0.1076
      0.8312    0.9561    0.2745    0.7904    0.4992    0.6809    0.8512    0.6006
      0.8789    0.1559    0.3784    0.4965    0.2040    0.8874    0.2149    0.4726
      0.6391    0.4442    0.9890    0.8743    0.2938    0.2761    0.3220    0.8975
      0.6750    0.8048    0.9688    0.1339    0.0105    0.1999    0.7374    0.0383
      0.4555    0.3892    0.4610    0.3473    0.9715    0.9198    0.2102    0.8202
      0.4196    0.1049    0.0120    0.8215    0.8455    0.6224    0.5393    0.9138
   
   C = 
   
      2.3729    1.6384    1.7979    2.0578    1.4816    1.4318    1.6663    1.4629
      2.6851    1.5249    2.0088    2.2406    2.7831    2.2468    1.3651    2.2355
      2.5751    1.7674    1.7948    2.3343    1.9303    1.9087    1.6601    2.0718
      1.9888    0.8329    1.1770    1.5288    1.9992    1.1332    0.8363    0.8766
      2.2834    1.6467    1.4650    1.9511    1.5951    1.2482    1.5727    1.1333
      2.0717    1.8707    1.3451    1.5760    1.8974    1.7493    1.7243    1.5409
      2.4004    1.0274    1.4142    1.8921    2.2008    2.1032    1.0660    1.5926
      2.2142    1.1399    1.3882    2.0840    1.7248    1.4430    1.2125    1.5505
   
   D = 
   
      2.3729    1.6384    1.7979    2.0578    1.4816    1.4318    1.6663    1.4629
      2.6851    1.5249    2.0088    2.2406    2.7831    2.2468    1.3651    2.2355
      2.5751    1.7674    1.7948    2.3343    1.9303    1.9087    1.6601    2.0718
      1.9888    0.8329    1.1770    1.5288    1.9992    1.1332    0.8363    0.8766
      2.2834    1.6467    1.4650    1.9511    1.5951    1.2482    1.5727    1.1333
      2.0717    1.8707    1.3451    1.5760    1.8974    1.7493    1.7243    1.5409
      2.4004    1.0274    1.4142    1.8921    2.2008    2.1032    1.0660    1.5926
      2.2142    1.1399    1.3882    2.0840    1.7248    1.4430    1.2125    1.5505
   


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

   
      0.6666    0.1060    0.0821    0.1971    0.4388    0.0341
      0.9764    0.7214    0.0373    0.6661    0.8161    0.4760
      0.8599    0.4299    0.0900    0.0172    0.0343    0.2445
      0.9468    0.5626    0.0178    0.6235    0.2140    0.0500
      0.7131    0.6888    0.1375    0.4972    0.9743    0.4379
   
   
      0.6666
      0.9764
      0.8599
      0.9468
      0.7131
      0.7214
      0.5626
      0.6888
      0.6661
      0.6235
      0.8161
      0.9743
   

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

   
      5.4744    1.5750    1.3199    1.0647    6.1577    2.5397
      4.6014    3.0098    3.9256    6.4109    9.4626    2.3178
      7.8098    3.7260    9.2396    8.8506    0.3764    7.8681
      6.8882    5.2443    5.1671    5.3047    2.1725    2.6446
      0.6707    8.9516    6.0327    7.8213    1.3813    2.0253
   
   
      5.4744    0.0000    0.0000    0.0000    6.1577    0.0000
      0.0000    0.0000    0.0000    6.4109    9.4626    0.0000
      7.8098    0.0000    9.2396    8.8506    0.0000    7.8681
      6.8882    5.2443    5.1671    5.3047    0.0000    0.0000
      0.0000    8.9516    6.0327    7.8213    0.0000    0.0000
   
   
      5.4744    0.0000    0.0000    0.0000    6.1577    0.0000
      0.0000    0.0000    0.0000    6.4109       NaN    0.0000
      7.8098    0.0000       NaN    8.8506    0.0000    7.8681
      6.8882    5.2443    5.1671    5.3047    0.0000    0.0000
      0.0000    8.9516    6.0327    7.8213    0.0000    0.0000
   

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

   
      6.5000    8.3305    6.5000    6.5000    2.8207    8.9010
      6.5000    3.1874    6.5000    8.7951    2.0505    9.7941
      8.7038    4.2713    9.1899    6.5000    4.9314    6.5000
      8.3596    3.2084    6.5000    3.5754    3.6251    9.4721
      2.6246    2.8001    0.2460    1.6716    8.9955    3.9028
   
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
   
