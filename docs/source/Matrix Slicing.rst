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
      0.0110    0.2937    0.4431    0.8631
   
   R1[2] = 0.4430908983140026
   C1 = 
      0.3160
      0.9588
      0.7246
      0.2023
      0.0927
      0.7938
      0.9674
      0.5489
   
   C1[5] = 0.7937502546052027

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
      0.7190    0.5942    0.3237    0.0995    0.1423
      0.0383    0.5681    0.3314    0.9551    0.7331
   

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
   
      0.7993    0.8482    0.7898    0.2228    0.6349    0.6346    0.3876    0.5526
      0.1715    0.3911    0.5543    0.1086    0.1030    0.3159    0.5015    0.8775
      0.3580    0.5204    0.1068    0.1055    0.9140    0.9466    0.5375    0.1974
      0.7351    0.6823    0.1937    0.5024    0.4404    0.3842    0.4937    0.0542
      0.2366    0.4041    0.4876    0.8231    0.0824    0.4969    0.9259    0.6273
      0.8563    0.3864    0.8657    0.6202    0.8093    0.3260    0.4109    0.2460
      0.9640    0.9707    0.4470    0.1245    0.9162    0.4178    0.6329    0.6844
      0.3656    0.8582    0.6388    0.9156    0.1139    0.9274    0.6124    0.0572
   
   B = 
   
      0.3983    0.3076    0.7306    0.1511    0.3662    0.3254    0.1043    0.6899
      0.3854    0.2306    0.6175    0.6899    0.5442    0.3477    0.3758    0.4287
      0.3473    0.6224    0.7827    0.3463    0.3409    0.9216    0.7327    0.9064
      0.9931    0.6613    0.1160    0.4800    0.2194    0.4979    0.8507    0.2593
      0.9402    0.0143    0.3449    0.5710    0.1902    0.1070    0.5389    0.4743
      0.6849    0.9560    0.6916    0.7707    0.9984    0.8863    0.5750    0.3211
      0.7707    0.9232    0.1792    0.3083    0.7040    0.1239    0.7408    0.1091
      0.8069    0.2744    0.9132    0.2511    0.6147    0.3752    0.4861    0.4225
   
   C = 
   
      2.9169    2.2055    2.9837    2.1963    2.4394    2.2795    2.4331    2.4693
      1.9271    1.5670    1.9585    1.2170    1.7159    1.4391    1.6986    1.3923
      2.5660    1.8346    1.9252    1.9674    2.0925    1.5258    1.9318    1.4736
      2.2233    1.6804    1.7239    1.6034    1.6650    1.3743    1.7528    1.5145
      2.8743    2.5169    2.0104    1.7514    2.2026    1.8760    2.5550    1.5565
      2.9058    2.0714    2.4166    1.8952    1.8750    2.0384    2.4440    2.3392
      3.2244    2.0655    3.0114    2.2421    2.5187    1.9286    2.4343    2.4455
      2.8678    2.7826    2.2458    2.2909    2.4335    2.3934    2.6837    1.8793
   
   D = 
   
      2.9169    2.2055    2.9837    2.1963    2.4394    2.2795    2.4331    2.4693
      1.9271    1.5670    1.9585    1.2170    1.7159    1.4391    1.6986    1.3923
      2.5660    1.8346    1.9252    1.9674    2.0925    1.5258    1.9318    1.4736
      2.2233    1.6804    1.7239    1.6034    1.6650    1.3743    1.7528    1.5145
      2.8743    2.5169    2.0104    1.7514    2.2026    1.8760    2.5550    1.5565
      2.9058    2.0714    2.4166    1.8952    1.8750    2.0384    2.4440    2.3392
      3.2244    2.0655    3.0114    2.2421    2.5187    1.9286    2.4343    2.4455
      2.8678    2.7826    2.2458    2.2909    2.4335    2.3934    2.6837    1.8793
   


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

   
      0.5907    0.0366    0.9907    0.6449    0.1252    0.1743
      0.3340    0.2035    0.0411    0.5785    0.6388    0.3190
      0.2439    0.0591    0.6924    0.3806    0.6573    0.0622
      0.6781    0.5999    0.6210    0.3132    0.8889    0.1982
      0.3279    0.1676    0.4798    0.6358    0.9866    0.6941
   
   
      0.5907
      0.6781
      0.5999
      0.9907
      0.6924
      0.6210
      0.6449
      0.5785
      0.6358
      0.6388
      0.6573
      0.8889
      0.9866
      0.6941
   

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

   
      8.2552    9.8063    9.4505    4.4400    8.6725    4.7513
      2.3285    2.6125    4.2754    3.6733    4.4617    6.9485
      9.9751    6.8084    6.2922    3.7749    9.4160    9.9250
      7.4297    7.5102    4.9618    5.8531    2.4321    8.2677
      7.4289    3.7076    1.5326    0.2040    2.9442    7.8167
   
   
      8.2552    9.8063    9.4505    0.0000    8.6725    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    6.9485
      9.9751    6.8084    6.2922    0.0000    9.4160    9.9250
      7.4297    7.5102    0.0000    5.8531    0.0000    8.2677
      7.4289    0.0000    0.0000    0.0000    0.0000    7.8167
   
   
      8.2552       NaN       NaN    0.0000    8.6725    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    6.9485
         NaN    6.8084    6.2922    0.0000       NaN       NaN
      7.4297    7.5102    0.0000    5.8531    0.0000    8.2677
      7.4289    0.0000    0.0000    0.0000    0.0000    7.8167
   

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

   
      8.1270    2.6125    1.7399    6.5000    3.0020    2.4003
      4.9430    6.5000    6.5000    9.2135    6.5000    1.6671
      2.3141    2.8366    9.3981    6.5000    2.2972    6.5000
      4.1268    9.9591    2.6834    6.5000    4.9839    3.9117
      4.7976    0.0883    3.2998    1.7405    8.1187    4.5338
   
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
   
