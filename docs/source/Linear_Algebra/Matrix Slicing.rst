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
      0.4817    0.1923    0.4672    0.3193
   
   R1[2] = 0.46716164784189773
   C1 = 
      0.9781
      0.0793
      0.6094
      0.3765
      0.6950
      0.4824
      0.7464
      0.4623
   
   C1[5] = 0.48241951999993493

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
      0.6118    0.1338    0.5942    0.6443    0.1617
      0.0842    0.1807    0.5131    0.0102    0.2734
   

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
   
      0.8775    0.1361    0.2350    0.8038    0.7314    0.0159    0.8501    0.8454
      0.6354    0.5859    0.1253    0.9212    0.3081    0.9031    0.2757    0.2006
      0.7572    0.9369    0.5811    0.4753    0.9289    0.0331    0.6627    0.5766
      0.6459    0.2946    0.5440    0.1050    0.3555    0.7583    0.9303    0.4882
      0.1843    0.6718    0.3222    0.0935    0.5749    0.3634    0.0072    0.3919
      0.8538    0.3669    0.3879    0.2522    0.3279    0.0755    0.6463    0.3930
      0.7546    0.0375    0.2100    0.2807    0.6663    0.8751    0.0586    0.8502
      0.3285    0.1904    0.2384    0.9539    0.1363    0.9473    0.1291    0.3321
   
   B = 
   
      0.2727    0.7077    0.8255    0.1252    0.2732    0.2217    0.8574    0.8986
      0.8106    0.8751    0.5505    0.0410    0.4188    0.5287    0.6793    0.8514
      0.1550    0.1241    0.8231    0.1289    0.1135    0.6806    0.1837    0.9391
      0.7122    0.9946    0.3281    0.3338    0.3681    0.5866    0.7561    0.7608
      0.4163    0.5772    0.9203    0.1532    0.9815    0.7109    0.3538    0.6789
      0.6744    0.3553    0.9729    0.0128    0.9521    0.1063    0.1483    0.8672
      0.8599    0.1058    0.9638    0.7186    0.9203    0.3961    0.1006    0.2277
      0.2516    0.0079    0.7868    0.5812    0.8516    0.0711    0.0627    0.6103
   
   C = 
   
      2.2174    2.0933    3.4296    1.6285    2.8545    1.8164    1.8955    2.9565
      2.3484    2.4237    2.8381    0.8006    2.3590    1.5148    1.9456    3.0658
      2.5184    2.5233    3.7546    1.3208    2.8843    2.3049    2.1883    3.5475
      2.1562    1.4637    3.5233    1.2146    2.7431    1.4673    1.2957    2.8306
      1.3004    1.3160    2.0156    0.4490    1.6532    1.1481    1.0268    2.0574
      1.6120    1.5118    2.6162    1.0002    1.8470    1.3202    1.4601    2.3107
      1.6004    1.5805    3.0986    0.8663    2.6141    1.1451    1.3481    2.8643
      1.8504    1.8090    2.3181    0.7169    1.9851    1.1677    1.3986    2.5530
   
   D = 
   
      2.2174    2.0933    3.4296    1.6285    2.8545    1.8164    1.8955    2.9565
      2.3484    2.4237    2.8381    0.8006    2.3590    1.5148    1.9456    3.0658
      2.5184    2.5233    3.7546    1.3208    2.8843    2.3049    2.1883    3.5475
      2.1562    1.4637    3.5233    1.2146    2.7431    1.4673    1.2957    2.8306
      1.3004    1.3160    2.0156    0.4490    1.6532    1.1481    1.0268    2.0574
      1.6120    1.5118    2.6162    1.0002    1.8470    1.3202    1.4601    2.3107
      1.6004    1.5805    3.0986    0.8663    2.6141    1.1451    1.3481    2.8643
      1.8504    1.8090    2.3181    0.7169    1.9851    1.1677    1.3986    2.5530
   


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

   
      0.7730    0.9821    0.8137    0.9192    0.8976    0.0684
      0.0635    0.3844    0.0949    0.5366    0.6742    0.8354
      0.7141    0.0753    0.8311    0.9285    0.3304    0.5986
      0.1044    0.6429    0.0911    0.9139    0.1990    0.8609
      0.6741    0.8652    0.6287    0.1692    0.7142    0.3094
   
   
      0.7730
      0.7141
      0.6741
      0.9821
      0.6429
      0.8652
      0.8137
      0.8311
      0.6287
      0.9192
      0.5366
      0.9285
      0.9139
      0.8976
      0.6742
      0.7142
      0.8354
      0.5986
      0.8609
   

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

   
      2.6706    4.0107    2.7887    8.1319    8.0836    4.3055
      6.3988    0.9037    3.0524    5.8184    7.4614    3.6791
      3.1208    7.9691    2.5729    0.3183    0.6293    0.8325
      3.6499    2.5567    9.8919    4.2078    5.0349    3.1567
      9.4160    4.4408    3.1906    2.2176    3.1373    3.6535
   
   
      0.0000    0.0000    0.0000    8.1319    8.0836    0.0000
      6.3988    0.0000    0.0000    5.8184    7.4614    0.0000
      0.0000    7.9691    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    9.8919    0.0000    5.0349    0.0000
      9.4160    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    8.1319    8.0836    0.0000
      6.3988    0.0000    0.0000    5.8184    7.4614    0.0000
      0.0000    7.9691    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000       NaN    0.0000    5.0349    0.0000
         NaN    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      6.5000    4.3107    1.1964    9.8902    0.5787    6.5000
      6.5000    8.6072    8.8994    1.1935    8.3808    1.5576
      3.8549    4.7615    4.8557    8.6839    2.9932    6.5000
      8.8860    1.9890    3.7035    4.1013    6.5000    8.4711
      4.0258    4.9307    9.6371    6.5000    2.1922    9.2224
   
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
   
