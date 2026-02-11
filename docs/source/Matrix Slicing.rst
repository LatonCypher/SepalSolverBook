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
      0.0168    0.1165    0.7259    0.1121
   
   R1[2] = 0.7259380121784631
   C1 = 
      0.6771
      0.2565
      0.4893
      0.2185
      0.1478
      0.4991
      0.5575
      0.2758
   
   C1[5] = 0.4990791048280142

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
      0.0131    0.3457    0.6811    0.2815    0.2690
      0.7942    0.6630    0.4370    0.5654    0.3706
   

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
   
      0.3143    0.6773    0.8305    0.9144    0.5404    0.5189    0.2341    0.4926
      0.2933    0.2749    0.3267    0.5165    0.7956    0.6088    0.4699    0.0683
      0.1867    0.4961    0.8493    0.8920    0.8906    0.3958    0.1336    0.5604
      0.9953    0.2979    0.9317    0.7948    0.8555    0.8667    0.8220    0.3913
      0.9534    0.8314    0.0244    0.5351    0.7966    0.2001    0.1800    0.2686
      0.6481    0.0709    0.3137    0.5303    0.7478    0.0889    0.7592    0.5222
      0.0968    0.9574    0.0126    0.5098    0.1136    0.4434    0.3707    0.1390
      0.7150    0.9919    0.4527    0.8715    0.2389    0.2551    0.9048    0.5506
   
   B = 
   
      0.8737    0.8532    0.9289    0.8061    0.6996    0.8792    0.0725    0.3065
      0.7476    0.3901    0.4790    0.2823    0.0893    0.8944    0.5530    0.9287
      0.2087    0.6322    0.7736    0.2637    0.6493    0.4873    0.6217    0.2279
      0.8141    0.4945    0.5810    0.1518    0.7509    0.0335    0.0081    0.1078
      0.6449    0.4385    0.9371    0.6401    0.2034    0.9290    0.1469    0.5804
      0.2256    0.6768    0.6150    0.8827    0.2629    0.6748    0.2823    0.1162
      0.9640    0.4344    0.7044    0.7074    0.4452    0.8558    0.1222    0.6608
      0.6594    0.6832    0.6715    0.9082    0.1596    0.8527    0.8991    0.4510
   
   C = 
   
      2.7147    2.5360    3.1112    2.2193    1.9354    2.7899    1.6185    1.7640
      2.0988    1.8311    2.4537    1.9196    1.3717    2.2905    0.7882    1.3491
      2.5994    2.4302    3.1346    2.1729    1.8303    2.7382    1.5859    1.7116
      3.7315    3.5337    4.4262    3.5023    2.7550    4.0389    1.6452    2.1966
      2.8047    2.1643    2.7903    2.1488    1.4965    2.8699    0.9855    1.8533
      2.6949    2.1158    2.8275    2.2741    1.6584    2.6535    0.9828    1.5747
      1.8402    1.3221    1.5881    1.2816    0.8711    1.8051    0.8606    1.4017
      3.6171    2.7609    3.3837    2.6264    2.1435    3.4036    1.6017    2.3520
   
   D = 
   
      2.7147    2.5360    3.1112    2.2193    1.9354    2.7899    1.6185    1.7640
      2.0988    1.8311    2.4537    1.9196    1.3717    2.2905    0.7882    1.3491
      2.5994    2.4302    3.1346    2.1729    1.8303    2.7382    1.5859    1.7116
      3.7315    3.5337    4.4262    3.5023    2.7550    4.0389    1.6452    2.1966
      2.8047    2.1643    2.7903    2.1488    1.4965    2.8699    0.9855    1.8533
      2.6949    2.1158    2.8275    2.2741    1.6584    2.6535    0.9828    1.5747
      1.8402    1.3221    1.5881    1.2816    0.8711    1.8051    0.8606    1.4017
      3.6171    2.7609    3.3837    2.6264    2.1435    3.4036    1.6017    2.3520
   


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

   
      0.7549    0.5944    0.9823    0.6806    0.3798    0.5050
      0.3565    0.4748    0.4091    0.5604    0.1352    0.4192
      0.2024    0.6508    0.2776    0.7213    0.1855    0.6932
      0.6203    0.3281    0.1765    0.7535    0.9186    0.7235
      0.4340    0.2187    0.4445    0.4057    0.5257    0.8065
   
   
      0.7549
      0.6203
      0.5944
      0.6508
      0.9823
      0.6806
      0.5604
      0.7213
      0.7535
      0.9186
      0.5257
      0.5050
      0.6932
      0.7235
      0.8065
   

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

   
      1.2746    1.9051    0.4141    4.2886    3.3858    1.5905
      1.0546    9.7818    7.0469    2.6703    4.0450    1.4491
      6.5095    2.3070    8.8053    7.1583    5.0823    3.8363
      0.8587    2.2142    9.2297    5.2911    7.7721    4.4879
      5.3956    5.9109    7.6493    1.7653    2.3707    0.7505
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    9.7818    7.0469    0.0000    0.0000    0.0000
      6.5095    0.0000    8.8053    7.1583    5.0823    0.0000
      0.0000    0.0000    9.2297    5.2911    7.7721    0.0000
      5.3956    5.9109    7.6493    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    7.0469    0.0000    0.0000    0.0000
      6.5095    0.0000    8.8053    7.1583    5.0823    0.0000
      0.0000    0.0000       NaN    5.2911    7.7721    0.0000
      5.3956    5.9109    7.6493    0.0000    0.0000    0.0000
   

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

   
      0.9222    6.5000    6.5000    4.4143    1.6059    8.8982
      4.4956    4.8568    2.2387    6.5000    4.7135    0.1049
      0.2110    4.9035    1.9459    3.4002    9.9933    9.1518
      3.1657    4.2973    4.3822    1.2861    6.5000    6.5000
      8.8891    2.7301    2.8604    4.1541    1.9665    0.6087
   
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
   
