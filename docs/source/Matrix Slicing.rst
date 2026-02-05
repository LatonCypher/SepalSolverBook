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
      0.3786    0.4868    0.0304    0.8764
   
   R1[2] = 0.030362619958937076
   C1 = 
      0.8790
      0.6885
      0.7088
      0.6350
      0.4680
      0.0405
      0.4682
      0.0371
   
   C1[5] = 0.040452585536449925

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
      0.1236    0.9768    0.9647    0.2383    0.4845
      0.8075    0.5397    0.0595    0.1490    0.2009
   

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
   
      0.5307    0.7479    0.2944    0.8430    0.4615    0.1174    0.4224    0.6945
      0.5434    0.5349    0.9650    0.9803    0.0446    0.0008    0.9448    0.3207
      0.7975    0.7841    0.0051    0.6752    0.6119    0.5460    0.4716    0.0098
      0.9301    0.1817    0.7918    0.6167    0.2002    0.3460    0.2130    0.1756
      0.4631    0.2339    0.9755    0.3758    0.4408    0.5477    0.8342    0.3824
      0.7269    0.5479    0.6826    0.4979    0.5644    0.9995    0.8434    0.7438
      0.1444    0.3279    0.4547    0.3236    0.6480    0.2801    0.1838    0.2004
      0.7266    0.4237    0.9942    0.6155    0.8696    0.3386    0.8622    0.5969
   
   B = 
   
      0.7213    0.6725    0.9733    0.1638    0.6041    0.2353    0.2026    0.2772
      0.4975    0.6217    0.8131    0.3827    0.8128    0.9162    0.1662    0.9673
      0.0087    0.6240    0.0357    0.4534    0.7077    0.9109    0.9747    0.6369
      0.8411    0.0739    0.9459    0.1858    0.1510    0.8185    0.6879    0.3128
      0.9867    0.5672    0.5141    0.5394    0.6965    0.1038    0.9637    0.2617
      0.0470    0.5384    0.2575    0.4513    0.1495    0.9476    0.1588    0.3682
      0.0179    0.9227    0.8228    0.9116    0.2318    0.3910    0.8632    0.1775
      0.0983    0.6686    0.3803    0.9632    0.9044    0.1101    0.8608    0.2885
   
   C = 
   
      2.0032    2.2469    2.8116    2.0192    2.3292    2.1689    2.5245    1.7610
      1.5836    2.4844    2.8480    2.1080    2.1342    2.7094    2.9486    1.8614
      2.1722    2.1595    2.8995    1.5743    1.8507    2.2297    1.8532    1.6416
      1.5217    1.8919    2.0985    1.3229    1.7624    2.0626    1.9975    1.3989
      1.2883    2.6636    2.2307    2.2912    2.1452    2.4525    2.9030    1.6688
      1.9137    3.4259    3.1725    2.9715    2.8535    3.1197    3.3171    2.2019
      1.2191    1.4306    1.3623    1.2520    1.4416    1.4402    1.7497    1.1112
      2.2093    3.2881    3.1400    2.8292    2.9760    2.7825    3.7600    2.1145
   
   D = 
   
      2.0032    2.2469    2.8116    2.0192    2.3292    2.1689    2.5245    1.7610
      1.5836    2.4844    2.8480    2.1080    2.1342    2.7094    2.9486    1.8614
      2.1722    2.1595    2.8995    1.5743    1.8507    2.2297    1.8532    1.6416
      1.5217    1.8919    2.0985    1.3229    1.7624    2.0626    1.9975    1.3989
      1.2883    2.6636    2.2307    2.2912    2.1452    2.4525    2.9030    1.6688
      1.9137    3.4259    3.1725    2.9715    2.8535    3.1197    3.3171    2.2019
      1.2191    1.4306    1.3623    1.2520    1.4416    1.4402    1.7497    1.1112
      2.2093    3.2881    3.1400    2.8292    2.9760    2.7825    3.7600    2.1145
   


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

   
      0.6096    0.0646    0.4390    0.6271    0.8245    0.2801
      0.8849    0.3460    0.4511    0.8907    0.7107    0.5406
      0.7560    0.4786    0.3132    0.2557    0.0936    0.5589
      0.7593    0.5097    0.1039    0.4998    0.9231    0.3449
      0.3436    0.0632    0.7588    0.9966    0.3889    0.0896
   
   
      0.6096
      0.8849
      0.7560
      0.7593
      0.5097
      0.7588
      0.6271
      0.8907
      0.9966
      0.8245
      0.7107
      0.9231
      0.5406
      0.5589
   

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

   
      0.8535    0.0722    6.9263    7.6240    0.2267    6.7102
      0.9781    1.0807    1.3558    4.1144    6.1021    8.4908
      5.1335    2.2004    4.5100    1.0069    4.0731    0.2270
      1.3895    1.8677    6.1627    5.6764    1.5396    4.8760
      5.8708    7.6207    5.6107    3.0919    4.8376    0.2734
   
   
      0.0000    0.0000    6.9263    7.6240    0.0000    6.7102
      0.0000    0.0000    0.0000    0.0000    6.1021    8.4908
      5.1335    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.1627    5.6764    0.0000    0.0000
      5.8708    7.6207    5.6107    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    6.9263    7.6240    0.0000    6.7102
      0.0000    0.0000    0.0000    0.0000    6.1021    8.4908
      5.1335    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.1627    5.6764    0.0000    0.0000
      5.8708    7.6207    5.6107    0.0000    0.0000    0.0000
   

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

   
      8.8509    9.7839    6.5000    3.3563    2.2763    3.9730
      6.5000    6.5000    4.0176    1.0960    9.6811    1.8085
      6.5000    6.5000    1.1037    2.4221    6.5000    6.5000
      3.3146    4.9848    9.7744    6.5000    6.5000    1.5581
      9.3735    0.1457    1.3576    8.3089    3.5600    2.6873
   
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
   
