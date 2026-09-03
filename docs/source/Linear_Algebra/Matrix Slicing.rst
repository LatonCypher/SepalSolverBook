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
      0.8097    0.9489    0.4682    0.7121
   
   R1[2] = 0.46819697208412125
   C1 = 
      0.1722
      0.4425
      0.5829
      0.0343
      0.0478
      0.4696
      0.5715
      0.4268
   
   C1[5] = 0.4695644854490908

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
      0.7095    0.3406    0.9167    0.5105    0.0737
      0.8722    0.6608    0.4442    0.5604    0.0371
   

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
   
      0.7199    0.5161    0.9905    0.3432    0.3462    0.2076    0.5984    0.3924
      0.9863    0.1967    0.4985    0.2964    0.8520    0.6977    0.1631    0.1471
      0.5197    0.8486    0.4046    0.2371    0.7831    0.5973    0.4064    0.5814
      0.8127    0.4672    0.2480    0.9376    0.4811    0.2692    0.7942    0.6214
      0.3514    0.5325    0.6816    0.0674    0.3459    0.7810    0.3278    0.0134
      0.4899    0.9324    0.6789    0.3785    0.0584    0.6731    0.0451    0.1909
      0.5445    0.0621    0.0998    0.7969    0.2836    0.8097    0.6555    0.5528
      0.2515    0.4246    0.9138    0.5895    0.3646    0.0046    0.0624    0.7409
   
   B = 
   
      0.6270    0.5783    0.8921    0.3444    0.4857    0.3221    0.5126    0.5796
      0.7379    0.3753    0.0513    0.2155    0.2651    0.8118    0.3934    0.3086
      0.2175    0.9848    0.5792    0.8097    0.0694    0.1139    0.4380    0.0550
      0.4519    0.5163    0.6024    0.4092    0.8135    0.4636    0.5317    0.0229
      0.9969    0.2719    0.3894    0.6612    0.7279    0.7541    0.8777    0.3593
      0.6199    0.6087    0.9459    0.0044    0.1212    0.0014    0.1677    0.2986
      0.5199    0.2958    0.2464    0.9664    0.5880    0.5188    0.5976    0.8241
      0.9969    0.7685    0.8736    0.2909    0.8648    0.0326    0.7286    0.0121
   
   C = 
   
      2.3790    2.4617    2.2706    2.2239    1.8028    1.5075    2.1706    1.3231
      2.5193    2.1058    2.5177    1.6738    1.7348    1.4045    2.0284    1.3171
      3.0891    2.2834    2.3623    1.8687    2.0825    1.8335    2.3575    1.3924
      3.0108    2.3808    2.6380    2.2325    2.6857    1.8994    2.6023    1.5655
      1.8048    1.7858    1.7421    1.3680    0.9647    1.0868    1.3642    1.0348
      2.0032    2.0829    1.9434    1.2149    1.1558    1.2421    1.4467    0.8791
      2.4457    2.0367    2.5477    1.5933    2.1043    1.1798    1.9503    1.2490
      2.0735    2.1988    1.9395    1.6761    1.7210    1.1345    1.9075    0.5333
   
   D = 
   
      2.3790    2.4617    2.2706    2.2239    1.8028    1.5075    2.1706    1.3231
      2.5193    2.1058    2.5177    1.6738    1.7348    1.4045    2.0284    1.3171
      3.0891    2.2834    2.3623    1.8687    2.0825    1.8335    2.3575    1.3924
      3.0108    2.3808    2.6380    2.2325    2.6857    1.8994    2.6023    1.5655
      1.8048    1.7858    1.7421    1.3680    0.9647    1.0868    1.3642    1.0348
      2.0032    2.0829    1.9434    1.2149    1.1558    1.2421    1.4467    0.8791
      2.4457    2.0367    2.5477    1.5933    2.1043    1.1798    1.9503    1.2490
      2.0735    2.1988    1.9395    1.6761    1.7210    1.1345    1.9075    0.5333
   


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

   
      0.6670    0.2486    0.1737    0.2646    0.9716    0.0363
      0.4997    0.3962    0.4776    0.6147    0.6261    0.6817
      0.0687    0.1300    0.9800    0.5359    0.7748    0.0665
      0.6482    0.6818    0.1488    0.7904    0.0892    0.7755
      0.5266    0.6539    0.3259    0.8525    0.7091    0.6484
   
   
      0.6670
      0.6482
      0.5266
      0.6818
      0.6539
      0.9800
      0.6147
      0.5359
      0.7904
      0.8525
      0.9716
      0.6261
      0.7748
      0.7091
      0.6817
      0.7755
      0.6484
   

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

   
      3.8658    3.3392    9.0760    4.1424    3.3775    4.6762
      4.6474    5.6032    3.9996    0.6457    1.4675    9.3124
      4.3439    1.3993    8.0559    3.8267    9.5623    1.8576
      5.6732    4.3797    0.5299    1.0126    5.5264    1.1361
      1.0581    4.3228    2.0955    9.6083    3.7434    8.3081
   
   
      0.0000    0.0000    9.0760    0.0000    0.0000    0.0000
      0.0000    5.6032    0.0000    0.0000    0.0000    9.3124
      0.0000    0.0000    8.0559    0.0000    9.5623    0.0000
      5.6732    0.0000    0.0000    0.0000    5.5264    0.0000
      0.0000    0.0000    0.0000    9.6083    0.0000    8.3081
   
   
      0.0000    0.0000       NaN    0.0000    0.0000    0.0000
      0.0000    5.6032    0.0000    0.0000    0.0000       NaN
      0.0000    0.0000    8.0559    0.0000       NaN    0.0000
      5.6732    0.0000    0.0000    0.0000    5.5264    0.0000
      0.0000    0.0000    0.0000       NaN    0.0000    8.3081
   

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

   
      9.6696    6.5000    6.5000    4.4647    6.5000    4.9944
      3.8287    9.1597    3.5441    6.5000    0.6598    2.8188
      3.1913    2.9070    6.5000    2.0941    6.5000    6.5000
      6.5000    0.7509    2.5420    1.7258    2.9982    4.1826
      4.3901    8.1505    9.5532    9.8172    6.5000    6.5000
   
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
   
