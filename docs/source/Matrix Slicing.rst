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
      0.0834    0.7467    0.5567    0.3624
   
   R1[2] = 0.5567282529826864
   C1 = 
      0.5312
      0.9580
      0.8227
      0.2113
      0.0625
      0.6996
      0.8991
      0.9807
   
   C1[5] = 0.6995920412261087

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
      0.0475    0.2446    0.4258    0.6733    0.7700
      0.4970    0.1112    0.5509    0.8274    0.4786
   

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
   
      0.4373    0.1242    0.6150    0.5245    0.4824    0.1432    0.8795    0.3296
      0.1410    0.0479    0.1175    0.6869    0.5930    0.7611    0.0755    0.6926
      0.1208    0.5979    0.1257    0.2511    0.7416    0.6510    0.4833    0.1819
      0.8656    0.5946    0.1095    0.4015    0.0845    0.3507    0.9007    0.9125
      0.0459    0.1170    0.3768    0.5193    0.2302    0.6737    0.3476    0.1163
      0.9281    0.5498    0.6243    0.1921    0.5784    0.4484    0.3043    0.7738
      0.0013    0.3382    0.7307    0.7301    0.5257    0.2565    0.1299    0.8834
      0.6995    0.1964    0.5606    0.2573    0.2171    0.3713    0.0860    0.3018
   
   B = 
   
      0.8091    0.3059    0.6904    0.1328    0.4992    0.2216    0.5144    0.8426
      0.7607    0.5455    0.4088    0.8254    0.4601    0.2092    0.7087    0.2994
      0.4793    0.6731    0.1028    0.3897    0.3377    0.0624    0.7158    0.1726
      0.4746    0.1739    0.7135    0.6736    0.0486    0.1131    0.6524    0.5367
      0.5377    0.2241    0.2600    0.0234    0.1765    0.7586    0.0092    0.4673
      0.7736    0.8178    0.2221    0.8896    0.5745    0.9323    0.0192    0.2998
      0.6170    0.1189    0.9316    0.3465    0.9579    0.4708    0.6923    0.5527
      0.1661    0.5939    0.5914    0.0433    0.1467    0.7846    0.9783    0.5339
   
   C = 
   
      1.9595    1.2321    1.9615    1.2112    1.5669    1.3927    2.0338    1.7237
      1.6021    1.4434    1.4223    1.3138    0.8814    1.8647    1.3886    1.4388
      1.9627    1.3554    1.4151    1.4994    1.3846    1.7278    1.2715    1.3431
      2.4198    1.6875    2.6170    1.5845    1.9753    1.8996    2.7308    2.2714
      1.4321    1.1348    1.0909    1.3296    1.0070    1.1746    1.0846    0.9813
      2.5337    2.0295    2.0578    1.5011    1.7011    1.9886    2.4208    2.1435
      1.6630    1.6713    1.5723    1.3796    0.9326    1.5914    2.2036    1.4863
      1.6133    1.2849    1.2019    1.0250    1.0196    1.0482    1.4321    1.3044
   
   D = 
   
      1.9595    1.2321    1.9615    1.2112    1.5669    1.3927    2.0338    1.7237
      1.6021    1.4434    1.4223    1.3138    0.8814    1.8647    1.3886    1.4388
      1.9627    1.3554    1.4151    1.4994    1.3846    1.7278    1.2715    1.3431
      2.4198    1.6875    2.6170    1.5845    1.9753    1.8996    2.7308    2.2714
      1.4321    1.1348    1.0909    1.3296    1.0070    1.1746    1.0846    0.9813
      2.5337    2.0295    2.0578    1.5011    1.7011    1.9886    2.4208    2.1435
      1.6630    1.6713    1.5723    1.3796    0.9326    1.5914    2.2036    1.4863
      1.6133    1.2849    1.2019    1.0250    1.0196    1.0482    1.4321    1.3044
   


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

   
      0.0474    0.3896    0.4536    0.6389    0.2841    0.3876
      0.5865    0.7431    0.0770    0.9973    0.8534    0.6143
      0.0072    0.3208    0.2279    0.9591    0.9954    0.7882
      0.8441    0.3020    0.2582    0.9663    0.5842    0.3736
      0.2845    0.0096    0.2537    0.4602    0.1402    0.1219
   
   
      0.5865
      0.8441
      0.7431
      0.6389
      0.9973
      0.9591
      0.9663
      0.8534
      0.9954
      0.5842
      0.6143
      0.7882
   

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

   
      1.6609    9.3417    5.6976    5.5898    6.6092    0.7529
      2.1376    5.4649    1.3905    9.9157    5.7886    5.1241
      7.7846    1.9371    0.8927    0.0024    7.5631    5.6878
      4.6429    6.9934    2.3196    7.3368    7.7996    2.1629
      3.2195    4.6997    0.9895    6.3958    5.3986    0.6521
   
   
      0.0000    9.3417    5.6976    5.5898    6.6092    0.0000
      0.0000    5.4649    0.0000    9.9157    5.7886    5.1241
      7.7846    0.0000    0.0000    0.0000    7.5631    5.6878
      0.0000    6.9934    0.0000    7.3368    7.7996    0.0000
      0.0000    0.0000    0.0000    6.3958    5.3986    0.0000
   
   
      0.0000       NaN    5.6976    5.5898    6.6092    0.0000
      0.0000    5.4649    0.0000       NaN    5.7886    5.1241
      7.7846    0.0000    0.0000    0.0000    7.5631    5.6878
      0.0000    6.9934    0.0000    7.3368    7.7996    0.0000
      0.0000    0.0000    0.0000    6.3958    5.3986    0.0000
   

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

   
      6.5000    4.5934    3.7735    2.3238    8.5509    2.2166
      6.5000    3.1963    6.5000    0.1091    2.5632    4.0231
      1.0023    6.5000    6.5000    8.5291    6.5000    2.3574
      1.6155    4.6860    2.3597    9.8442    9.7188    6.5000
      0.0201    1.0635    6.5000    8.2317    6.5000    4.4697
   
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
   
