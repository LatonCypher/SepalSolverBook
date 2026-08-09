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
      0.1551    0.4881    0.3364    0.6822
   
   R1[2] = 0.3363889748151052
   C1 = 
      0.1352
      0.1376
      0.4724
      0.9511
      0.1415
      0.3907
      0.4359
      0.1018
   
   C1[5] = 0.39068338418197135

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
      0.5805    0.6394    0.9185    0.8135    0.0058
      0.6137    0.8397    0.7712    0.2054    0.9248
   

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
   
      0.3125    0.7136    0.3430    0.7092    0.8741    0.5620    0.6859    0.6475
      0.4715    0.6342    0.7867    0.6502    0.2185    0.6404    0.2225    0.4839
      0.6280    0.1207    0.7703    0.3667    0.6794    0.8458    0.4960    0.6921
      0.5568    0.5033    0.5113    0.4301    0.7429    0.3484    0.7177    0.4517
      0.2734    0.6317    0.7755    0.9249    0.2055    0.7480    0.4075    0.3355
      0.6770    0.8513    0.9493    0.3368    0.8709    0.9730    0.0883    0.1608
      0.3587    0.6921    0.7885    0.8663    0.6196    0.9798    0.6422    0.4507
      0.5870    0.8692    0.8249    0.6122    0.5340    0.0331    0.9986    0.9965
   
   B = 
   
      0.9994    0.5801    0.2561    0.8443    0.2864    0.2625    0.7413    0.0446
      0.7621    0.3728    0.5539    0.6797    0.3682    0.5904    0.3608    0.4000
      0.7079    0.6959    0.3321    0.8590    0.1542    0.8118    0.8428    0.9121
      0.9557    0.9431    0.1125    0.1671    0.0964    0.1035    0.0408    0.8557
      0.5639    0.2074    0.8931    0.5143    0.9046    0.5325    0.4032    0.6536
      0.9744    0.4242    0.1246    0.9939    0.7587    0.5189    0.9301    0.6634
      0.0719    0.2114    0.2447    0.3930    0.3861    0.0062    0.5465    0.4237
      0.2751    0.1888    0.2983    0.4713    0.7155    0.8266    0.4315    0.0536
   
   C = 
   
      3.0448    2.0418    1.8806    2.7449    2.4187    2.1518    2.3365    2.4887
      3.0291    2.1260    1.2801    2.6779    1.6682    2.0542    2.2820    2.2365
      3.0486    2.0264    1.5647    3.0463    2.3213    2.2753    2.8035    2.3451
      2.6473    1.8109    1.6567    2.5465    2.0017    1.8571    2.2534    2.1055
      3.1539    2.3155    1.2580    2.6483    1.6705    1.9475    2.2680    2.5849
      3.8091    2.3308    1.9667    3.5474    2.3613    2.5880    2.9966    2.7856
      3.7463    2.5968    1.8015    3.3525    2.4368    2.4474    2.9220    3.1046
      3.0974    2.3400    1.9972    3.0669    2.2811    2.5318    2.6907    2.4976
   
   D = 
   
      3.0448    2.0418    1.8806    2.7449    2.4187    2.1518    2.3365    2.4887
      3.0291    2.1260    1.2801    2.6779    1.6682    2.0542    2.2820    2.2365
      3.0486    2.0264    1.5647    3.0463    2.3213    2.2753    2.8035    2.3451
      2.6473    1.8109    1.6567    2.5465    2.0017    1.8571    2.2534    2.1055
      3.1539    2.3155    1.2580    2.6483    1.6705    1.9475    2.2680    2.5849
      3.8091    2.3308    1.9667    3.5474    2.3613    2.5880    2.9966    2.7856
      3.7463    2.5968    1.8015    3.3525    2.4368    2.4474    2.9220    3.1046
      3.0974    2.3400    1.9972    3.0669    2.2811    2.5318    2.6907    2.4976
   


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

   
      0.3710    0.2127    0.8192    0.9923    0.1220    0.4760
      0.0108    0.3163    0.5877    0.6050    0.8053    0.4238
      0.5504    0.9620    0.3247    0.3058    0.5822    0.2526
      0.7077    0.3533    0.8790    0.7859    0.1334    0.1810
      0.0652    0.3582    0.0640    0.8064    0.9413    0.7745
   
   
      0.5504
      0.7077
      0.9620
      0.8192
      0.5877
      0.8790
      0.9923
      0.6050
      0.7859
      0.8064
      0.8053
      0.5822
      0.9413
      0.7745
   

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

   
      7.2319    8.8450    9.8428    1.7949    6.2754    0.5144
      5.7903    6.8011    0.6957    5.1418    5.3407    2.0438
      7.0910    4.6059    7.1493    6.1314    1.2229    7.0316
      6.4549    3.6882    5.0105    3.2526    6.9598    0.3828
      7.3994    4.9454    0.3009    9.4642    5.6226    6.5428
   
   
      7.2319    8.8450    9.8428    0.0000    6.2754    0.0000
      5.7903    6.8011    0.0000    5.1418    5.3407    0.0000
      7.0910    0.0000    7.1493    6.1314    0.0000    7.0316
      6.4549    0.0000    5.0105    0.0000    6.9598    0.0000
      7.3994    0.0000    0.0000    9.4642    5.6226    6.5428
   
   
      7.2319    8.8450       NaN    0.0000    6.2754    0.0000
      5.7903    6.8011    0.0000    5.1418    5.3407    0.0000
      7.0910    0.0000    7.1493    6.1314    0.0000    7.0316
      6.4549    0.0000    5.0105    0.0000    6.9598    0.0000
      7.3994    0.0000    0.0000       NaN    5.6226    6.5428
   

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

   
      2.7457    0.8527    4.4672    3.1280    6.5000    6.5000
      8.5111    8.4110    6.5000    6.5000    9.3835    3.9913
      4.6768    6.5000    6.5000    2.2901    0.5816    1.6027
      6.5000    6.5000    9.5510    2.3656    8.4602    4.0465
      6.5000    6.5000    1.7376    6.5000    6.5000    9.8499
   
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
   
