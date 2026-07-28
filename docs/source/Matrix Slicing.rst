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
      0.5263    0.7156    0.8998    0.5509
   
   R1[2] = 0.8998152236038522
   C1 = 
      0.4133
      0.0784
      0.5506
      0.5911
      0.9171
      0.3416
      0.3117
      0.0941
   
   C1[5] = 0.34161882334108284

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
      0.6233    0.2560    0.7328    0.1131    0.6427
      0.8200    0.1368    0.5714    0.8802    0.1272
   

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
   
      0.6154    0.7390    0.1396    0.4551    0.1574    0.5978    0.9898    0.1298
      0.1007    0.9320    0.3189    0.7512    0.0473    0.1618    0.6975    0.4942
      0.3367    0.8530    0.9922    0.0380    0.8432    0.3478    0.0294    0.4757
      0.7107    0.9143    0.3701    0.3881    0.0521    0.3330    0.1526    0.7878
      0.1092    0.2045    0.0319    0.2011    0.3935    0.3890    0.3646    0.5137
      0.7197    0.4796    0.1107    0.5141    0.9578    0.3673    0.4382    0.3280
      0.9044    0.1353    0.1622    0.8440    0.5370    0.9954    0.4668    0.4601
      0.1335    0.3957    0.4390    0.9288    0.0628    0.8807    0.5221    0.9210
   
   B = 
   
      0.9438    0.2720    0.0434    0.5419    0.4255    0.5212    0.3122    0.6254
      0.3418    0.1908    0.4201    0.3181    0.8141    0.3345    0.2681    0.7540
      0.9866    0.2877    0.6831    0.8010    0.0349    0.9256    0.5621    0.7596
      0.9211    0.4711    0.3018    0.3066    0.8212    0.1001    0.2000    0.4598
      0.8906    0.9448    0.5173    0.5411    0.5752    0.8799    0.5584    0.4623
      0.0084    0.5625    0.2139    0.2971    0.9541    0.6213    0.4736    0.1307
      0.2379    0.7408    0.4871    0.3593    0.8085    0.2555    0.4549    0.0595
      0.1433    0.7274    0.8471    0.4753    0.3451    0.5627    0.1026    0.1689
   
   C = 
   
      1.7897    1.8757    1.3714    1.5001    2.7481    1.5786    1.3944    1.4891
      1.7003    1.6626    1.6579    1.3959    2.3456    1.3329    1.0818    1.5212
      2.4522    1.9177    1.9900    2.0564    1.9084    2.6163    1.5968    2.1422
      1.9042    1.5796    1.6248    1.6478    2.1213    1.7927    1.0896    1.8032
      0.9038    1.4070    1.0727    0.9152    1.4488    1.1451    0.7696    0.6804
      2.4332    2.2360    1.5288    1.7296    2.4916    2.0569    1.4600    1.7044
      2.5009    2.4638    1.5695    1.8947    2.9885    2.2204    1.6094    1.6628
      1.8694    2.2871    2.0076    1.7557    2.7735    1.9553    1.3644    1.4731
   
   D = 
   
      1.7897    1.8757    1.3714    1.5001    2.7481    1.5786    1.3944    1.4891
      1.7003    1.6626    1.6579    1.3959    2.3456    1.3329    1.0818    1.5212
      2.4522    1.9177    1.9900    2.0564    1.9084    2.6163    1.5968    2.1422
      1.9042    1.5796    1.6248    1.6478    2.1213    1.7927    1.0896    1.8032
      0.9038    1.4070    1.0727    0.9152    1.4488    1.1451    0.7696    0.6804
      2.4332    2.2360    1.5288    1.7296    2.4916    2.0569    1.4600    1.7044
      2.5009    2.4638    1.5695    1.8947    2.9885    2.2204    1.6094    1.6628
      1.8694    2.2871    2.0076    1.7557    2.7735    1.9553    1.3644    1.4731
   


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

   
      0.4736    0.4673    0.1006    0.0838    0.4375    0.3801
      0.2500    0.7402    0.1989    0.5605    0.4662    0.9292
      0.1201    0.9983    0.0569    0.0335    0.1066    0.3242
      0.1577    0.9836    0.8425    0.2659    0.0989    0.5751
      0.3705    0.5269    0.6490    0.9072    0.7204    0.9007
   
   
      0.7402
      0.9983
      0.9836
      0.5269
      0.8425
      0.6490
      0.5605
      0.9072
      0.7204
      0.9292
      0.5751
      0.9007
   

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

   
      0.0076    4.4513    1.1777    4.9475    5.5423    8.7012
      4.7843    2.0358    0.8226    7.6503    7.9370    9.3023
      8.9258    8.1221    8.6390    9.9847    3.1332    4.1051
      1.1176    0.4827    8.3504    6.7750    9.9695    9.4823
      8.5619    7.7971    4.9765    6.0983    5.4496    4.6378
   
   
      0.0000    0.0000    0.0000    0.0000    5.5423    8.7012
      0.0000    0.0000    0.0000    7.6503    7.9370    9.3023
      8.9258    8.1221    8.6390    9.9847    0.0000    0.0000
      0.0000    0.0000    8.3504    6.7750    9.9695    9.4823
      8.5619    7.7971    0.0000    6.0983    5.4496    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    5.5423    8.7012
      0.0000    0.0000    0.0000    7.6503    7.9370       NaN
      8.9258    8.1221    8.6390       NaN    0.0000    0.0000
      0.0000    0.0000    8.3504    6.7750       NaN       NaN
      8.5619    7.7971    0.0000    6.0983    5.4496    0.0000
   

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

   
      3.6575    2.9881    4.6765    2.1879    1.5866    9.9991
      6.5000    6.5000    4.8615    3.1168    1.1620    0.3442
      3.5976    3.9717    6.5000    2.0411    2.0103    8.3375
      6.5000    3.6453    9.7772    3.7613    6.5000    2.0782
      6.5000    9.7481    4.2372    6.5000    8.2631    6.5000
   
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
   
