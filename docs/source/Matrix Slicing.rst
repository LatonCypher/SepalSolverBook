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
      0.2254    0.7883    0.8872    0.1930
   
   R1[2] = 0.8872306777841691
   C1 = 
      0.7259
      0.0778
      0.7382
      0.9600
      0.3407
      0.6299
      0.6482
      0.4832
   
   C1[5] = 0.6299400290003239

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
      0.8268    0.6213    0.3292    0.7160    0.0588
      0.0901    0.9854    0.2084    0.4721    0.1005
   

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
   
      0.7691    0.1712    0.2310    0.4200    0.3558    0.4928    0.9161    0.6746
      0.7212    0.1330    0.6913    0.7931    0.8827    0.6131    0.7317    0.6979
      0.3178    0.7026    0.3202    0.4036    0.6263    0.1709    0.6098    0.4081
      0.3394    0.7011    0.9502    0.7197    0.3166    0.7455    0.9732    0.9351
      0.9155    0.1947    0.1472    0.8599    0.3065    0.6216    0.1076    0.7394
      0.9247    0.4065    0.0475    0.0409    0.0309    0.7330    0.0708    0.2257
      0.9951    0.6251    0.6018    0.1383    0.5993    0.3239    0.9412    0.8469
      0.4012    0.7013    0.6496    0.6228    0.2568    0.8635    0.8406    0.7763
   
   B = 
   
      0.1612    0.6586    0.5290    0.6805    0.9559    0.8722    0.5844    0.7626
      0.7825    0.9977    0.9731    0.5398    0.7795    0.0623    0.3150    0.4246
      0.3851    0.5987    0.6722    0.1468    0.7192    0.8325    0.4016    0.7091
      0.2349    0.5261    0.1509    0.1164    0.6164    0.7460    0.7249    0.2540
      0.1622    0.8664    0.9586    0.4853    0.8177    0.3380    0.8999    0.5165
      0.4488    0.5996    0.0569    0.9167    0.4894    0.2938    0.0826    0.6401
      0.4852    0.2136    0.5523    0.5609    0.7761    0.9170    0.3737    0.2824
      0.7807    0.0264    0.9072    0.3680    0.3006    0.4829    0.1366    0.3939
   
   C = 
   
      1.6956    1.8537    2.2791    2.0850    2.7395    2.6179    1.6959    1.9533
      1.9911    2.7458    3.0136    2.4140    3.5787    3.2909    2.5297    2.6281
      1.6119    2.1004    2.4450    1.6424    2.5223    1.9068    1.6896    1.6361
      2.7265    2.8243    3.3407    2.5597    3.6581    3.3376    2.1604    2.6970
      1.5168    2.0184    1.9618    1.9008    2.5235    2.3166    1.7471    1.9815
      1.0396    1.5518    1.2380    1.6702    1.7670    1.3017    0.8628    1.5159
      2.2743    2.6489    3.4412    2.5464    3.5903    3.0808    2.1539    2.6025
      2.4530    2.6207    2.8891    2.4928    3.2997    2.8853    1.8903    2.4512
   
   D = 
   
      1.6956    1.8537    2.2791    2.0850    2.7395    2.6179    1.6959    1.9533
      1.9911    2.7458    3.0136    2.4140    3.5787    3.2909    2.5297    2.6281
      1.6119    2.1004    2.4450    1.6424    2.5223    1.9068    1.6896    1.6361
      2.7265    2.8243    3.3407    2.5597    3.6581    3.3376    2.1604    2.6970
      1.5168    2.0184    1.9618    1.9008    2.5235    2.3166    1.7471    1.9815
      1.0396    1.5518    1.2380    1.6702    1.7670    1.3017    0.8628    1.5159
      2.2743    2.6489    3.4412    2.5464    3.5903    3.0808    2.1539    2.6025
      2.4530    2.6207    2.8891    2.4928    3.2997    2.8853    1.8903    2.4512
   


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

   
      0.1651    0.2366    0.4759    0.7248    0.3618    0.7661
      0.1119    0.5684    0.3656    0.5042    0.2088    0.4833
      0.2655    0.5204    0.1428    0.2086    0.9816    0.0395
      0.9408    0.9972    0.6495    0.1065    0.1379    0.4916
      0.9316    0.0096    0.6294    0.6556    0.9324    0.4463
   
   
      0.9408
      0.9316
      0.5684
      0.5204
      0.9972
      0.6495
      0.6294
      0.7248
      0.5042
      0.6556
      0.9816
      0.9324
      0.7661
   

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

   
      6.5145    1.0187    9.6428    5.1678    6.6616    8.4655
      4.6531    6.5862    6.1932    6.4108    1.6183    3.2743
      8.0895    5.8908    8.6358    2.2901    2.0125    5.8180
      5.1049    3.9968    4.9337    8.7409    1.9853    9.3607
      5.2452    7.4072    2.6137    0.0570    9.7882    8.2230
   
   
      6.5145    0.0000    9.6428    5.1678    6.6616    8.4655
      0.0000    6.5862    6.1932    6.4108    0.0000    0.0000
      8.0895    5.8908    8.6358    0.0000    0.0000    5.8180
      5.1049    0.0000    0.0000    8.7409    0.0000    9.3607
      5.2452    7.4072    0.0000    0.0000    9.7882    8.2230
   
   
      6.5145    0.0000       NaN    5.1678    6.6616    8.4655
      0.0000    6.5862    6.1932    6.4108    0.0000    0.0000
      8.0895    5.8908    8.6358    0.0000    0.0000    5.8180
      5.1049    0.0000    0.0000    8.7409    0.0000       NaN
      5.2452    7.4072    0.0000    0.0000       NaN    8.2230
   

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

   
      3.0618    4.4200    3.6363    6.5000    6.5000    8.8014
      3.3186    3.0925    0.0273    9.7703    4.8238    0.3102
      4.8748    4.7701    4.3411    9.6966    9.4160    2.7880
      6.5000    9.3466    0.3461    0.0678    8.6911    6.5000
      6.5000    4.7036    8.7251    4.2835    3.9148    8.2782
   
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
   
