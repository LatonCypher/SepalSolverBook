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
      0.2619    0.3136    0.7556    0.3238
   
   R1[2] = 0.7556102562390599
   C1 = 
      0.9912
      0.9796
      0.1810
      0.3783
      0.6890
      0.2431
      0.9632
      0.2760
   
   C1[5] = 0.24311798462717116

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
      0.6693    0.0362    0.7549    0.5258    0.9290
      0.3278    0.5518    0.6758    0.2565    0.1787
   

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
   
      0.7615    0.6795    0.7909    0.2104    0.0524    0.0328    0.1709    0.7811
      0.7294    0.8645    0.6075    0.1014    0.1692    0.7699    0.5273    0.1622
      0.2848    0.8944    0.9231    0.0565    0.9513    0.2221    0.6276    0.9362
      0.9619    0.7682    0.8580    0.9930    0.9530    0.2675    0.0091    0.8361
      0.3773    0.2088    0.6855    0.7504    0.1615    0.5053    0.2182    0.7160
      0.6212    0.9518    0.4807    0.5292    0.9361    0.1089    0.5040    0.8305
      0.6317    0.8387    0.8990    0.5645    0.1388    0.1966    0.4880    0.5974
      0.7429    0.1497    0.0504    0.9820    0.1771    0.9463    0.2046    0.1187
   
   B = 
   
      0.4389    0.8314    0.6968    0.1265    0.0646    0.8743    0.4724    0.2300
      0.7086    0.9744    0.6786    0.4791    0.1701    0.5447    0.8896    0.5232
      0.7549    0.3859    0.6527    0.8985    0.8856    0.8750    0.7930    0.3005
      0.3020    0.5201    0.8665    0.8796    0.9218    0.2969    0.0689    0.5660
      0.6852    0.9929    0.9505    0.8074    0.9085    0.4802    0.5257    0.7739
      0.8859    0.4597    0.4711    0.3937    0.2506    0.1644    0.0734    0.7231
      0.9916    0.0336    0.6384    0.4789    0.3538    0.1754    0.6162    0.9089
      0.9807    0.5257    0.0269    0.0208    0.4976    0.0540    0.7348    0.5447
   
   C = 
   
      2.4768    2.1934    1.8857    1.4709    1.5641    1.8931    2.3152    1.5324
      2.9018    2.3609    2.4438    1.8370    1.4395    1.9793    2.1920    2.1152
      3.8617    3.0539    2.8917    2.5192    2.6478    2.2146    3.2573    2.8199
      3.6330    3.9050    3.6722    3.0305    3.2200    2.8533    3.0273    2.8375
      2.5344    1.9484    2.0524    1.8724    2.0655    1.5037    1.7419    1.9054
      3.5221    3.3379    3.1364    2.4893    2.5848    2.2400    2.9790    2.7985
      3.0598    2.5417    2.6375    2.2216    2.1452    2.1802    2.6236    2.1922
      2.0457    1.9739    2.2511    1.6907    1.5528    1.3497    0.9677    1.8921
   
   D = 
   
      2.4768    2.1934    1.8857    1.4709    1.5641    1.8931    2.3152    1.5324
      2.9018    2.3609    2.4438    1.8370    1.4395    1.9793    2.1920    2.1152
      3.8617    3.0539    2.8917    2.5192    2.6478    2.2146    3.2573    2.8199
      3.6330    3.9050    3.6722    3.0305    3.2200    2.8533    3.0273    2.8375
      2.5344    1.9484    2.0524    1.8724    2.0655    1.5037    1.7419    1.9054
      3.5221    3.3379    3.1364    2.4893    2.5848    2.2400    2.9790    2.7985
      3.0598    2.5417    2.6375    2.2216    2.1452    2.1802    2.6236    2.1922
      2.0457    1.9739    2.2511    1.6907    1.5528    1.3497    0.9677    1.8921
   


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

   
      0.3874    0.4805    0.3994    0.4990    0.6584    0.9370
      0.0270    0.6666    0.7808    0.8766    0.6374    0.7077
      0.5978    0.0565    0.0840    0.0437    0.9125    0.8102
      0.4138    0.4188    0.8908    0.8787    0.8802    0.2225
      0.5538    0.3245    0.2695    0.6383    0.7043    0.4407
   
   
      0.5978
      0.5538
      0.6666
      0.7808
      0.8908
      0.8766
      0.8787
      0.6383
      0.6584
      0.6374
      0.9125
      0.8802
      0.7043
      0.9370
      0.7077
      0.8102
   

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

   
      6.5690    1.2767    1.1000    6.8966    2.8550    7.5570
      4.1291    6.2570    2.1834    1.7299    5.0532    9.6451
      0.1269    1.8705    0.0752    3.1662    0.3190    9.3080
      6.4845    6.6791    1.4702    4.1612    4.4837    9.3269
      4.7880    7.1848    1.0751    9.3953    6.2297    7.4284
   
   
      6.5690    0.0000    0.0000    6.8966    0.0000    7.5570
      0.0000    6.2570    0.0000    0.0000    5.0532    9.6451
      0.0000    0.0000    0.0000    0.0000    0.0000    9.3080
      6.4845    6.6791    0.0000    0.0000    0.0000    9.3269
      0.0000    7.1848    0.0000    9.3953    6.2297    7.4284
   
   
      6.5690    0.0000    0.0000    6.8966    0.0000    7.5570
      0.0000    6.2570    0.0000    0.0000    5.0532       NaN
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      6.4845    6.6791    0.0000    0.0000    0.0000       NaN
      0.0000    7.1848    0.0000       NaN    6.2297    7.4284
   

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

   
      6.5000    1.9404    6.5000    0.6303    4.5254    8.4863
      2.3023    8.1069    4.7290    0.9348    6.5000    6.5000
      6.5000    6.5000    6.5000    3.6006    9.1990    3.2490
      1.6460    2.1819    6.5000    1.2145    4.2237    6.5000
      0.9205    2.1215    0.3965    0.2307    0.8464    6.5000
   
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
   
