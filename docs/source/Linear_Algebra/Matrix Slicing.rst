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
      0.3031    0.1943    0.8903    0.2956
   
   R1[2] = 0.8902758917727546
   C1 = 
      0.1099
      0.8407
      0.6982
      0.6329
      0.9275
      0.4799
      0.7319
      0.7055
   
   C1[5] = 0.479935675861088

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
      0.3384    0.1147    0.9883    0.9887    0.1896
      0.5700    0.9119    0.5641    0.3937    0.5165
   

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
   
      0.1185    0.3772    0.6252    0.4670    0.4274    0.7388    0.0692    0.3234
      0.4945    0.9794    0.9611    0.1319    0.4094    0.2773    0.3451    0.4165
      0.8132    0.4334    0.5832    0.9073    0.3014    0.7066    0.8988    0.3252
      0.9506    0.1185    0.1481    0.3584    0.8196    0.7004    0.2637    0.2742
      0.8282    0.8086    0.1329    0.7927    0.7787    0.9976    0.9126    0.0414
      0.8850    0.8772    0.9326    0.9465    0.4398    0.2979    0.0717    0.3541
      0.9689    0.6990    0.9616    0.4955    0.3640    0.8859    0.3555    0.3784
      0.0150    0.0429    0.5504    0.9240    0.4956    0.5792    0.5229    0.2238
   
   B = 
   
      0.6399    0.8833    0.6949    0.2581    0.4153    0.6263    0.2113    0.6103
      0.4320    0.5832    0.2654    0.3011    0.1237    0.9157    0.7092    0.5376
      0.9544    0.8811    0.9540    0.6835    0.2904    0.1057    0.2761    0.3116
      0.3222    0.7445    0.6583    0.2937    0.0921    0.6534    0.9271    0.0243
      0.7549    0.0126    0.1428    0.8083    0.8019    0.6228    0.2355    0.6128
      0.9479    0.1858    0.9199    0.1674    0.4315    0.9864    0.5863    0.8466
      0.1192    0.6997    0.8236    0.7632    0.6977    0.4814    0.2573    0.0549
      0.0984    0.3777    0.2695    0.9631    0.6523    0.5805    0.7430    0.6221
   
   C = 
   
      2.0490    1.5365    1.9711    1.5421    1.2412    2.0069    1.6901    1.5737
      2.3532    2.4083    2.3172    2.1600    1.5782    2.3307    1.8439    1.8947
      2.5930    3.0471    3.3546    2.3665    2.0300    3.0668    2.4396    1.9677
      2.2573    1.7346    2.1216    1.7326    1.8078    2.4411    1.5334    1.9791
      2.9078    2.7597    3.2303    2.3137    2.2742    3.7235    2.5540    2.3986
      2.7981    3.0644    2.8519    2.2091    1.5963    2.8824    2.5039    2.0713
      3.1935    3.0404    3.3640    2.3416    1.9827    3.1637    2.4031    2.5068
      1.8587    1.7753    2.2496    1.7766    1.4146    1.9723    1.7994    1.1883
   
   D = 
   
      2.0490    1.5365    1.9711    1.5421    1.2412    2.0069    1.6901    1.5737
      2.3532    2.4083    2.3172    2.1600    1.5782    2.3307    1.8439    1.8947
      2.5930    3.0471    3.3546    2.3665    2.0300    3.0668    2.4396    1.9677
      2.2573    1.7346    2.1216    1.7326    1.8078    2.4411    1.5334    1.9791
      2.9078    2.7597    3.2303    2.3137    2.2742    3.7235    2.5540    2.3986
      2.7981    3.0644    2.8519    2.2091    1.5963    2.8824    2.5039    2.0713
      3.1935    3.0404    3.3640    2.3416    1.9827    3.1637    2.4031    2.5068
      1.8587    1.7753    2.2496    1.7766    1.4146    1.9723    1.7994    1.1883
   


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

   
      0.6517    0.5418    0.6085    0.9995    0.4373    0.6239
      0.3057    0.5990    0.3100    0.7228    0.1114    0.5201
      0.3675    0.8728    0.2893    0.1239    0.7049    0.1322
      0.0119    0.0556    0.8169    0.2373    0.3775    0.7972
      0.6887    0.2488    0.0711    0.3952    0.2365    0.0784
   
   
      0.6517
      0.6887
      0.5418
      0.5990
      0.8728
      0.6085
      0.8169
      0.9995
      0.7228
      0.7049
      0.6239
      0.5201
      0.7972
   

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

   
      5.7390    6.2677    0.1156    3.7661    0.0153    8.6609
      8.6929    4.1938    9.5636    4.0282    6.4944    0.8161
      7.0790    3.5443    3.3833    6.7747    5.5419    0.3585
      8.5030    7.1639    0.3796    1.2600    6.9668    5.0404
      5.5794    9.5369    9.7703    6.8094    8.7686    1.6236
   
   
      5.7390    6.2677    0.0000    0.0000    0.0000    8.6609
      8.6929    0.0000    9.5636    0.0000    6.4944    0.0000
      7.0790    0.0000    0.0000    6.7747    5.5419    0.0000
      8.5030    7.1639    0.0000    0.0000    6.9668    5.0404
      5.5794    9.5369    9.7703    6.8094    8.7686    0.0000
   
   
      5.7390    6.2677    0.0000    0.0000    0.0000    8.6609
      8.6929    0.0000       NaN    0.0000    6.4944    0.0000
      7.0790    0.0000    0.0000    6.7747    5.5419    0.0000
      8.5030    7.1639    0.0000    0.0000    6.9668    5.0404
      5.5794       NaN       NaN    6.8094    8.7686    0.0000
   

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

   
      6.5000    1.4025    1.2889    4.0192    6.5000    0.5226
      6.5000    9.5654    0.4945    9.0950    1.6538    9.7190
      2.4068    9.0166    8.1033    6.5000    6.5000    3.4094
      6.5000    0.6488    9.5429    0.0546    3.4947    0.3820
      6.5000    6.5000    6.5000    4.7922    8.1338    6.5000
   
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
   
