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
      0.3231    0.1188    0.0950    0.9824
   
   R1[2] = 0.09495038185214799
   C1 = 
      0.6313
      0.8485
      0.2692
      0.7831
      0.4036
      0.0630
      0.3868
      0.2912
   
   C1[5] = 0.0630181819423844

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
      0.4707    0.6319    0.3531    0.2995    0.6283
      0.6291    0.5245    0.1306    0.1473    0.9753
   

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
   
      0.4422    0.5531    0.9489    0.2228    0.0967    0.5708    0.4712    0.1550
      0.0690    0.7385    0.4413    0.7238    0.3925    0.5901    0.6776    0.6849
      0.8489    0.6108    0.2697    0.0600    0.9023    0.9221    0.6771    0.2743
      0.4446    0.7853    0.8808    0.8505    0.4616    0.7108    0.3490    0.8976
      0.5934    0.3401    0.5210    0.3240    0.5597    0.4632    0.2974    0.8630
      0.1325    0.8465    0.1330    0.2757    0.6638    0.8792    0.7678    0.1163
      0.0420    0.6955    0.9124    0.9245    0.2860    0.8701    0.1966    0.6246
      0.6680    0.1600    0.4623    0.9651    0.9441    0.6709    0.0260    0.7677
   
   B = 
   
      0.0837    0.6941    0.4627    0.4865    0.6819    0.9853    0.0430    0.7998
      0.1063    0.2881    0.8195    0.0797    0.2170    0.9554    0.5233    0.5897
      0.0209    0.8247    0.8536    0.9745    0.6247    0.7507    0.6948    0.6643
      0.3637    0.0620    0.9088    0.1675    0.5162    0.5710    0.1331    0.7560
      0.9242    0.2520    0.3192    0.6579    0.5419    0.0242    0.6499    0.3206
      0.1969    0.4930    0.4841    0.0003    0.9739    0.6455    0.7343    0.0940
      0.3626    0.9851    0.9682    0.0476    0.2341    0.4593    0.3246    0.5957
      0.3256    0.5748    0.4029    0.6665    0.9109    0.4390    0.4517    0.1704
   
   C = 
   
      0.6198    2.1217    2.4963    1.4108    1.9893    2.4590    1.7024    1.8704
      1.3045    2.1205    3.0147    1.3909    2.4265    2.5205    2.0101    2.0327
      1.5138    2.4979    2.6786    1.5435    2.7062    2.7051    2.1586    2.0897
      1.4339    2.6403    3.5650    2.1984    3.3045    3.3594    2.4959    2.6223
      1.2119    2.1180    2.3311    1.8355    2.5813    2.3137    1.7988    1.8134
      1.3071    1.8865    2.5469    0.8587    2.0011    2.1839    1.9565    1.6746
      1.1432    2.0930    3.1630    1.7339    2.8441    2.8518    2.2935    2.1458
      1.6977    1.9863    2.6726    2.0841    3.1475    2.5139    2.0234    2.1773
   
   D = 
   
      0.6198    2.1217    2.4963    1.4108    1.9893    2.4590    1.7024    1.8704
      1.3045    2.1205    3.0147    1.3909    2.4265    2.5205    2.0101    2.0327
      1.5138    2.4979    2.6786    1.5435    2.7062    2.7051    2.1586    2.0897
      1.4339    2.6403    3.5650    2.1984    3.3045    3.3594    2.4959    2.6223
      1.2119    2.1180    2.3311    1.8355    2.5813    2.3137    1.7988    1.8134
      1.3071    1.8865    2.5469    0.8587    2.0011    2.1839    1.9565    1.6746
      1.1432    2.0930    3.1630    1.7339    2.8441    2.8518    2.2935    2.1458
      1.6977    1.9863    2.6726    2.0841    3.1475    2.5139    2.0234    2.1773
   


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

   
      0.9245    0.1823    0.2147    0.5539    0.5520    0.8368
      0.7676    0.2726    0.1457    0.4480    0.8575    0.6933
      0.6860    0.7119    0.6893    0.9540    0.6641    0.1759
      0.0854    0.1852    0.5215    0.5593    0.8979    0.1191
      0.3287    0.6203    0.6129    0.3123    0.2570    0.0181
   
   
      0.9245
      0.7676
      0.6860
      0.7119
      0.6203
      0.6893
      0.5215
      0.6129
      0.5539
      0.9540
      0.5593
      0.5520
      0.8575
      0.6641
      0.8979
      0.8368
      0.6933
   

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

   
      5.6057    8.5667    0.7486    9.0970    1.5859    3.8970
      5.9841    7.3770    2.8104    0.4128    7.7883    7.1404
      4.3740    7.7655    2.9763    5.1099    1.5542    1.5060
      2.9106    2.7524    0.1974    1.7421    2.1725    9.3897
      4.6194    4.8773    6.4881    8.8138    0.2674    6.4714
   
   
      5.6057    8.5667    0.0000    9.0970    0.0000    0.0000
      5.9841    7.3770    0.0000    0.0000    7.7883    7.1404
      0.0000    7.7655    0.0000    5.1099    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    9.3897
      0.0000    0.0000    6.4881    8.8138    0.0000    6.4714
   
   
      5.6057    8.5667    0.0000       NaN    0.0000    0.0000
      5.9841    7.3770    0.0000    0.0000    7.7883    7.1404
      0.0000    7.7655    0.0000    5.1099    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      0.0000    0.0000    6.4881    8.8138    0.0000    6.4714
   

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

   
      4.7801    6.5000    8.4367    4.1350    6.5000    8.6947
      1.4602    4.0735    3.0330    0.8844    3.6377    0.3936
      3.1235    0.6056    9.1237    1.6419    8.2697    4.7260
      1.0942    8.7885    4.3356    0.7754    3.0250    6.5000
      4.9615    6.5000    6.5000    6.5000    0.8346    9.2230
   
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
   
