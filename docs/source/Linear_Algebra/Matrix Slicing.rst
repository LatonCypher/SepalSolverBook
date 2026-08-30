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
      0.1300    0.5802    0.5549    0.2427
   
   R1[2] = 0.5548582327637569
   C1 = 
      0.8278
      0.6321
      0.2743
      0.8663
      0.7124
      0.6981
      0.1268
      0.9819
   
   C1[5] = 0.6980977725646703

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
      0.1161    0.2773    0.5317    0.7500    0.2479
      0.9643    0.4807    0.7262    0.5139    0.0839
   

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
   
      0.4644    0.0349    0.8316    0.3803    0.7983    0.1752    0.3334    0.0303
      0.8161    0.4411    0.7684    0.9064    0.1979    0.0768    0.3412    0.7698
      0.2561    0.3872    0.0175    0.9690    0.5459    0.6942    0.4323    0.7071
      0.5228    0.4381    0.0430    0.7182    0.1888    0.0870    0.0840    0.0945
      0.9188    0.4368    0.9140    0.7582    0.7925    0.8674    0.3467    0.8563
      0.2109    0.0694    0.4496    0.2151    0.4808    0.5136    0.2197    0.2336
      0.6133    0.1903    0.6304    0.8660    0.5647    0.8445    0.4436    0.1710
      0.4588    0.2897    0.1379    0.8883    0.2690    0.4355    0.5783    0.5606
   
   B = 
   
      0.9332    0.9772    0.8941    0.9576    0.6042    0.6376    0.8698    0.8214
      0.1369    0.4472    0.7623    0.9889    0.4612    0.7697    0.6261    0.7604
      0.6841    0.5723    0.8070    0.5356    0.6563    0.0009    0.7855    0.0667
      0.3116    0.8136    0.4112    0.3738    0.0150    0.2178    0.7047    0.8715
      0.6188    0.3613    0.7384    0.5884    0.3417    0.5890    0.4870    0.1741
      0.4072    0.9707    0.0943    0.7896    0.5693    0.8479    0.7709    0.4902
      0.4313    0.0959    0.9956    0.8263    0.4044    0.1092    0.6110    0.9040
      0.5707    0.2484    0.1260    0.3368    0.5640    0.1891    0.7060    0.4942
   
   C = 
   
      1.8521    1.7528    2.2111    1.9606    1.3726    1.0674    2.0960    1.3362
      2.3703    2.5418    2.6487    2.6862    1.8977    1.4223    3.1357    2.6079
      1.8165    2.3100    1.9247    2.4644    1.5146    1.7634    2.7261    2.5260
      1.0435    1.4999    1.3747    1.5064    0.7582    1.0390    1.5460    1.5895
      3.2607    3.6075    3.3239    3.8109    2.7555    2.4900    4.1960    3.1084
      1.3157    1.4208    1.3444    1.5405    1.1350    1.0220    1.6609    1.0931
      2.2818    2.8585    2.5180    2.8600    1.8345    1.8561    3.0758    2.4430
      1.7521    2.0941    1.9936    2.3005    1.4044    1.4059    2.5305    2.4406
   
   D = 
   
      1.8521    1.7528    2.2111    1.9606    1.3726    1.0674    2.0960    1.3362
      2.3703    2.5418    2.6487    2.6862    1.8977    1.4223    3.1357    2.6079
      1.8165    2.3100    1.9247    2.4644    1.5146    1.7634    2.7261    2.5260
      1.0435    1.4999    1.3747    1.5064    0.7582    1.0390    1.5460    1.5895
      3.2607    3.6075    3.3239    3.8109    2.7555    2.4900    4.1960    3.1084
      1.3157    1.4208    1.3444    1.5405    1.1350    1.0220    1.6609    1.0931
      2.2818    2.8585    2.5180    2.8600    1.8345    1.8561    3.0758    2.4430
      1.7521    2.0941    1.9936    2.3005    1.4044    1.4059    2.5305    2.4406
   


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

   
      0.7965    0.0290    0.9985    0.5489    0.7533    0.0104
      0.4531    0.9681    0.0072    0.5505    0.9610    0.2595
      0.6227    0.3207    0.5640    0.5643    0.0835    0.6868
      0.2139    0.2533    0.1569    0.5222    0.7358    0.1028
      0.8584    0.1677    0.6127    0.7635    0.6161    0.0094
   
   
      0.7965
      0.6227
      0.8584
      0.9681
      0.9985
      0.5640
      0.6127
      0.5489
      0.5505
      0.5643
      0.5222
      0.7635
      0.7533
      0.9610
      0.7358
      0.6161
      0.6868
   

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

   
      6.2958    1.8284    8.9238    0.1506    7.3222    5.4158
      4.6169    1.2015    0.7999    3.7847    0.3959    4.9877
      3.2193    0.6530    0.3373    7.7988    3.9453    0.1031
      3.9553    4.3391    5.4731    4.6829    9.0512    1.6845
      4.8014    3.8949    7.8893    6.0944    3.0994    1.9574
   
   
      6.2958    0.0000    8.9238    0.0000    7.3222    5.4158
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.7988    0.0000    0.0000
      0.0000    0.0000    5.4731    0.0000    9.0512    0.0000
      0.0000    0.0000    7.8893    6.0944    0.0000    0.0000
   
   
      6.2958    0.0000    8.9238    0.0000    7.3222    5.4158
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.7988    0.0000    0.0000
      0.0000    0.0000    5.4731    0.0000       NaN    0.0000
      0.0000    0.0000    7.8893    6.0944    0.0000    0.0000
   

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

   
      3.1819    9.2010    6.5000    6.5000    4.1499    3.8253
      0.1680    2.8223    0.8511    6.5000    6.5000    1.5907
      4.8015    9.0263    3.2934    6.5000    2.5173    3.5543
      6.5000    6.5000    2.5222    4.2630    9.8646    4.6210
      6.5000    3.9098    0.7212    1.9413    2.8816    6.5000
   
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
   
