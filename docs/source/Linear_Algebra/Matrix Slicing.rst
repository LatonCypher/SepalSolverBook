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
      0.6496    0.7268    0.1509    0.3902
   
   R1[2] = 0.15089101748762557
   C1 = 
      0.1193
      0.3162
      0.8579
      0.9362
      0.1858
      0.0162
      0.5623
      0.9150
   
   C1[5] = 0.01624321042096044

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
      0.1513    0.8325    0.6310    0.4611    0.8743
      0.4698    0.6781    0.8803    0.9303    0.9763
   

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
   
      0.0520    0.5548    0.4408    0.7675    0.6896    0.9895    0.9881    0.7757
      0.7081    0.9398    0.3040    0.7266    0.5299    0.3041    0.8619    0.8248
      0.8011    0.0069    0.6429    0.9248    0.5936    0.8969    0.9109    0.6425
      0.8296    0.0729    0.3771    0.1088    0.7631    0.2042    0.6056    0.5212
      0.0471    0.3422    0.3888    0.3701    0.0274    0.0566    0.8899    0.5004
      0.4294    0.3299    0.8794    0.5966    0.3081    0.8959    0.8607    0.7957
      0.7464    0.9233    0.7570    0.4104    0.2145    0.0594    0.2455    0.8754
      0.6043    0.5828    0.4664    0.0556    0.1339    0.5844    0.9686    0.5139
   
   B = 
   
      0.1335    0.9267    0.8978    0.5739    0.2028    0.8086    0.0212    0.1620
      0.4390    0.4041    0.2806    0.1582    0.8934    0.1403    0.9969    0.9250
      0.3740    0.6319    0.0906    0.8061    0.3674    0.3001    0.7443    0.9203
      0.5010    0.0995    0.7288    0.3944    0.8829    0.8190    0.4774    0.4443
      0.6803    0.8874    0.4586    0.8953    0.4336    0.4271    0.9060    0.8397
      0.7150    0.7514    0.3762    0.1793    0.2196    0.5283    0.6878    0.3025
      0.7927    0.5194    0.1059    0.9149    0.4724    0.0929    0.0387    0.8580
      0.3187    0.5290    0.5132    0.8913    0.9929    0.0291    0.6828    0.1837
   
   C = 
   
      3.0069    2.9064    1.9928    3.1658    3.0990    1.8123    3.1219    3.1369
      2.5088    2.8832    2.3285    3.1394    3.2591    1.8818    2.8108    3.0146
      2.7857    3.2572    2.4893    3.4422    2.7440    2.4298    2.5724    2.8081
      1.6495    2.4682    1.6371    2.5730    1.6473    1.3885    1.6340    1.9152
      1.4114    1.2583    0.8282    1.8355    1.7264    0.6448    1.2481    1.7421
      2.6160    2.9609    1.9704    3.1760    2.7586    1.8540    2.7492    2.8635
      1.6556    2.4096    1.8928    2.5540    2.7079    1.4676    2.5379    2.4236
      1.9793    2.4287    1.4364    2.4057    2.0178    1.2268    1.8791    2.3056
   
   D = 
   
      3.0069    2.9064    1.9928    3.1658    3.0990    1.8123    3.1219    3.1369
      2.5088    2.8832    2.3285    3.1394    3.2591    1.8818    2.8108    3.0146
      2.7857    3.2572    2.4893    3.4422    2.7440    2.4298    2.5724    2.8081
      1.6495    2.4682    1.6371    2.5730    1.6473    1.3885    1.6340    1.9152
      1.4114    1.2583    0.8282    1.8355    1.7264    0.6448    1.2481    1.7421
      2.6160    2.9609    1.9704    3.1760    2.7586    1.8540    2.7492    2.8635
      1.6556    2.4096    1.8928    2.5540    2.7079    1.4676    2.5379    2.4236
      1.9793    2.4287    1.4364    2.4057    2.0178    1.2268    1.8791    2.3056
   


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

   
      0.0183    0.5257    0.8296    0.7794    0.4455    0.1504
      0.3251    0.3549    0.7792    0.5768    0.5450    0.8616
      0.6867    0.8779    0.0734    0.8079    0.3386    0.2231
      0.9399    0.9959    0.2753    0.6852    0.2466    0.8824
      0.1546    0.2901    0.4587    0.3698    0.9858    0.8132
   
   
      0.6867
      0.9399
      0.5257
      0.8779
      0.9959
      0.8296
      0.7792
      0.7794
      0.5768
      0.8079
      0.6852
      0.5450
      0.9858
      0.8616
      0.8824
      0.8132
   

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

   
      2.3165    3.0373    2.9463    5.5487    0.6637    3.0935
      3.9767    3.8357    4.8640    1.4962    4.2291    6.0329
      2.0721    5.1607    9.6317    6.4018    0.1176    8.6733
      1.2676    4.1715    5.2572    1.7019    9.3360    5.2352
      3.0341    3.7388    1.0710    1.7874    7.2319    0.4808
   
   
      0.0000    0.0000    0.0000    5.5487    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    6.0329
      0.0000    5.1607    9.6317    6.4018    0.0000    8.6733
      0.0000    0.0000    5.2572    0.0000    9.3360    5.2352
      0.0000    0.0000    0.0000    0.0000    7.2319    0.0000
   
   
      0.0000    0.0000    0.0000    5.5487    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    6.0329
      0.0000    5.1607       NaN    6.4018    0.0000    8.6733
      0.0000    0.0000    5.2572    0.0000       NaN    5.2352
      0.0000    0.0000    0.0000    0.0000    7.2319    0.0000
   

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

   
      2.6297    0.1177    6.5000    9.5909    1.1261    6.5000
      9.1482    3.7547    3.3859    8.8269    9.3385    1.9646
      2.5491    6.5000    6.5000    3.7365    6.5000    1.1677
      6.5000    6.5000    0.1132    6.5000    2.6410    9.6255
      3.0588    4.0714    1.7778    1.3721    0.5308    0.6680
   
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
   
