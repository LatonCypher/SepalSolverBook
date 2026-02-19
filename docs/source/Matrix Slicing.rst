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
      0.1291    0.8382    0.5592    0.6474
   
   R1[2] = 0.5591882769426381
   C1 = 
      0.2897
      0.9202
      0.6265
      0.1018
      0.2700
      0.6560
      0.4802
      0.0182
   
   C1[5] = 0.6560277924293572

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
      0.5454    0.4890    0.7459    0.8848    0.5486
      0.0544    0.9414    0.7098    0.5263    0.4553
   

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
   
      0.6755    0.4254    0.3889    0.1488    0.6006    0.2852    0.1796    0.6654
      0.1521    0.4262    0.3174    0.0865    0.1569    0.6246    0.8080    0.6836
      0.2155    0.5847    0.2131    0.9910    0.0205    0.3530    0.2991    0.8128
      0.4361    0.8800    0.5278    0.6631    0.5878    0.6097    0.5053    0.8959
      0.8552    0.0687    0.7911    0.7499    0.9565    0.2599    0.4158    0.6931
      0.9220    0.8597    0.5338    0.3037    0.9042    0.1676    0.3638    0.3966
      0.0235    0.8373    0.2705    0.2417    0.5166    0.0813    0.5826    0.3251
      0.7054    0.5150    0.5644    0.9472    0.1563    0.0275    0.3511    0.9964
   
   B = 
   
      0.6637    0.6403    0.2555    0.4481    0.4489    0.0763    0.1235    0.5868
      0.0448    0.2352    0.5982    0.7430    0.1843    0.9236    0.3959    0.2219
      0.7845    0.1954    0.3829    0.7814    0.9976    0.1390    0.7111    0.6740
      0.5616    0.2864    0.5422    0.3533    0.8741    0.8126    0.3356    0.6101
      0.8061    0.6819    0.3761    0.2135    0.1242    0.4972    0.2912    0.4335
      0.3166    0.4474    0.7993    0.5455    0.1963    0.1535    0.2275    0.1526
      0.5780    0.7443    0.3065    0.1850    0.9073    0.2696    0.8051    0.1992
      0.6880    0.1232    0.0046    0.3287    0.7140    0.3915    0.8319    0.8847
   
   C = 
   
      1.9921    1.4040    1.1686    1.5110    1.6683    1.2707    1.5162    1.7720
      1.6792    1.3564    1.2713    1.4118    1.9024    1.1789    1.8492    1.3796
      1.7533    1.0955    1.4090    1.5671    2.2069    1.8546    1.7455    1.8459
      2.6906    1.9393    2.0668    2.3421    2.7550    2.3310    2.4621    2.4527
      3.1830    2.0964    1.6673    1.9682    2.8832    1.7469    2.1960    2.6582
      2.5049    1.9951    1.7063    2.0585    2.1288    1.9139    1.8599    2.1178
      1.4036    1.1964    1.1808    1.2987    1.4868    1.5627    1.5163    1.1694
      2.4892    1.4573    1.4110    1.9153    2.8574    1.9443    2.1735    2.5100
   
   D = 
   
      1.9921    1.4040    1.1686    1.5110    1.6683    1.2707    1.5162    1.7720
      1.6792    1.3564    1.2713    1.4118    1.9024    1.1789    1.8492    1.3796
      1.7533    1.0955    1.4090    1.5671    2.2069    1.8546    1.7455    1.8459
      2.6906    1.9393    2.0668    2.3421    2.7550    2.3310    2.4621    2.4527
      3.1830    2.0964    1.6673    1.9682    2.8832    1.7469    2.1960    2.6582
      2.5049    1.9951    1.7063    2.0585    2.1288    1.9139    1.8599    2.1178
      1.4036    1.1964    1.1808    1.2987    1.4868    1.5627    1.5163    1.1694
      2.4892    1.4573    1.4110    1.9153    2.8574    1.9443    2.1735    2.5100
   


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

   
      0.8032    0.2085    0.6983    0.9596    0.8401    0.9463
      0.9178    0.2797    0.5557    0.2590    0.3346    0.1733
      0.4073    0.0087    0.7441    0.3117    0.1914    0.9761
      0.4330    0.0027    0.8302    0.2307    0.8084    0.5807
      0.1382    0.1301    0.0799    0.7646    0.9734    0.1944
   
   
      0.8032
      0.9178
      0.6983
      0.5557
      0.7441
      0.8302
      0.9596
      0.7646
      0.8401
      0.8084
      0.9734
      0.9463
      0.9761
      0.5807
   

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

   
      3.9497    3.7676    4.1091    3.1837    7.0585    1.4171
      2.0880    6.5487    6.0835    4.8490    0.4009    2.8191
      5.7720    6.2366    2.0710    1.2496    8.0083    6.0485
      9.8313    3.7818    0.3385    7.7898    6.5143    5.0916
      3.4266    0.3330    3.2582    9.6790    3.6884    0.9357
   
   
      0.0000    0.0000    0.0000    0.0000    7.0585    0.0000
      0.0000    6.5487    6.0835    0.0000    0.0000    0.0000
      5.7720    6.2366    0.0000    0.0000    8.0083    6.0485
      9.8313    0.0000    0.0000    7.7898    6.5143    5.0916
      0.0000    0.0000    0.0000    9.6790    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    7.0585    0.0000
      0.0000    6.5487    6.0835    0.0000    0.0000    0.0000
      5.7720    6.2366    0.0000    0.0000    8.0083    6.0485
         NaN    0.0000    0.0000    7.7898    6.5143    5.0916
      0.0000    0.0000    0.0000       NaN    0.0000    0.0000
   

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

   
      6.5000    9.3550    3.0736    4.9389    6.5000    6.5000
      6.5000    4.2372    6.5000    9.2345    0.8887    0.0651
      2.2569    2.7257    8.1644    6.5000    6.5000    1.6616
      6.5000    2.9893    9.0207    9.6573    1.8515    1.0054
      0.9483    2.3511    0.7654    3.3480    6.5000    6.5000
   
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
   
