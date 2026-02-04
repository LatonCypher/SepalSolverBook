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
      0.3147    0.8070    0.8868    0.5085
   
   R1[2] = 0.8867761596357169
   C1 = 
      0.4854
      0.2345
      0.5864
      0.5616
      0.3838
      0.3558
      0.0317
      0.0858
   
   C1[5] = 0.35584722903973287

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
      0.1732    0.3021    0.7650    0.7856    0.4320
      0.3777    0.4858    0.0245    0.7781    0.7653
   

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

   * - +--------------------+----------------------+--------------------+
   * - 
     - Feature
     - Standard Algorithm
     - Strassen Algorithm
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Approach
     - Direct row-by-column
     - Divide-and-conquer
     - 
   * - 
     - 
     - multiplication
     - with recursive
     - 
   * - 
     - 
     - 
     - submatrices
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Multiplications
     - 8
     - 7
     - 
   * - 
     - for 2×2 matrices
     - 
     - 
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Additions/
     - 4
     - 18
     - 
   * - 
     - Subtractions
     - 
     - 
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Time Complexity
     - O(n^3)
     - O(n^(log2 7))
     - 
   * - 
     - 
     - 
     - ≈ O(n^2.81)
     - 
   * - +--------------------+----------------------+--------------------+
   * - 
     - Best Use Case
     - Small matrices
     - Large matrices
     - 
   * - +--------------------+----------------------+--------------------+

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
   M_3 &=& A_{11}\left(B_{12} - B_{22}\left) \\
   M_4 &=& A_{22}\left(B_{21} - B_{11}\left) \\
   M_5 &=& \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 &=& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 &=& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix::

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
   
      0.8821    0.3372    0.7875    0.3099    0.6514    0.6971    0.9929    0.9834
      0.8563    0.6494    0.1135    0.4406    0.7650    0.2146    0.3355    0.4240
      0.1109    0.8708    0.9889    0.2894    0.9034    0.9069    0.3296    0.3213
      0.4413    0.6808    0.4827    0.1739    0.6311    0.4447    0.4971    0.2528
      0.4498    0.9854    0.1784    0.7835    0.1872    0.1656    0.2549    0.9291
      0.4187    0.5162    0.2403    0.2609    0.6691    0.4782    0.9080    0.2445
      0.8905    0.4833    0.5093    0.5635    0.7721    0.9725    0.4983    0.2447
      0.0243    0.5023    0.5025    0.2473    0.1554    0.1679    0.2060    0.0048
   
   B = 
   
      0.8396    0.4240    0.1791    0.1283    0.9136    0.8913    0.8981    0.3106
      0.1736    0.9327    0.0869    0.9625    0.4456    0.4933    0.5217    0.3039
      0.8140    0.8528    0.7092    0.7514    0.2220    0.7126    0.8249    0.1070
      0.5981    0.5384    0.7803    0.3946    0.3048    0.8623    0.6303    0.9639
      0.5474    0.7641    0.1422    0.5482    0.6492    0.6998    0.2293    0.2190
      0.5288    0.7614    0.3209    0.4119    0.2477    0.1849    0.9950    0.3081
      0.3811    0.9498    0.7322    0.1659    0.0423    0.9956    0.6383    0.2989
      0.7041    0.4664    0.4302    0.0682    0.6238    0.6146    0.7547    0.7396
   
   C = 
   
      3.4216    3.9572    2.4541    2.0278    2.4766    3.9588    4.0320    2.1410
      2.1462    2.5670    1.2398    1.5864    2.0598    2.7140    2.4022    1.5476
      2.5481    3.7019    1.8217    2.6549    1.8226    2.8081    3.1142    1.4972
      1.9337    2.7383    1.3215    1.7721    1.5654    2.3973    2.3546    1.1740
      2.1039    2.6283    1.5702    1.7259    1.8814    2.6762    2.6306    2.0689
      1.9301    2.8560    1.5125    1.5651    1.4892    2.6349    2.4012    1.3101
      2.8821    3.4837    1.8943    2.1076    2.2296    3.2474    3.4744    1.8196
      0.9203    1.4850    0.8262    1.1506    0.5872    1.1886    1.1921    0.6032
   
   D = 
   
      3.4216    3.9572    2.4541    2.0278    2.4766    3.9588    4.0320    2.1410
      2.1462    2.5670    1.2398    1.5864    2.0598    2.7140    2.4022    1.5476
      2.5481    3.7019    1.8217    2.6549    1.8226    2.8081    3.1142    1.4972
      1.9337    2.7383    1.3215    1.7721    1.5654    2.3973    2.3546    1.1740
      2.1039    2.6283    1.5702    1.7259    1.8814    2.6762    2.6306    2.0689
      1.9301    2.8560    1.5125    1.5651    1.4892    2.6349    2.4012    1.3101
      2.8821    3.4837    1.8943    2.1076    2.2296    3.2474    3.4744    1.8196
      0.9203    1.4850    0.8262    1.1506    0.5872    1.1886    1.1921    0.6032
   


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

   
      0.0986    0.9660    0.9266    0.5426    0.7310    0.3456
      0.3111    0.5671    0.4252    0.6611    0.7631    0.9504
      0.4917    0.3764    0.8008    0.2951    0.5881    0.7623
      0.2876    0.6790    0.5466    0.7388    0.0258    0.8360
      0.7707    0.7406    0.2922    0.9509    0.8901    0.7575
   
   
      0.7707
      0.9660
      0.5671
      0.6790
      0.7406
      0.9266
      0.8008
      0.5466
      0.5426
      0.6611
      0.7388
      0.9509
      0.7310
      0.7631
      0.5881
      0.8901
      0.9504
      0.7623
      0.8360
      0.7575
   

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

   
      7.6958    5.5297    6.0861    4.6100    9.2724    6.5486
      5.0979    7.4173    9.7052    2.5924    6.1396    7.0555
      3.8226    7.9330    8.8940    0.1777    6.9315    3.6345
      0.9733    1.8023    7.6440    5.8706    4.6458    4.5318
      9.3793    3.0459    0.8669    2.6817    7.9241    0.6015
   
   
      7.6958    5.5297    6.0861    0.0000    9.2724    6.5486
      5.0979    7.4173    9.7052    0.0000    6.1396    7.0555
      0.0000    7.9330    8.8940    0.0000    6.9315    0.0000
      0.0000    0.0000    7.6440    5.8706    0.0000    0.0000
      9.3793    0.0000    0.0000    0.0000    7.9241    0.0000
   
   
      7.6958    5.5297    6.0861    0.0000       NaN    6.5486
      5.0979    7.4173       NaN    0.0000    6.1396    7.0555
      0.0000    7.9330    8.8940    0.0000    6.9315    0.0000
      0.0000    0.0000    7.6440    5.8706    0.0000    0.0000
         NaN    0.0000    0.0000    0.0000    7.9241    0.0000
   

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

   
      8.0322    8.3458    8.2735    2.3421    6.5000    6.5000
      2.9320    6.5000    3.9124    4.6089    8.2248    2.1989
      6.5000    0.0851    6.5000    0.9729    6.5000    1.0332
      2.4396    4.9592    9.7873    1.5928    2.6134    3.9670
      0.9049    8.0418    6.5000    9.9324    2.6029    6.5000
   
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
   
