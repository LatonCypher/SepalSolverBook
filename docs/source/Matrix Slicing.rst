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
      0.8400    0.6745    0.6768    0.2439
   
   R1[2] = 0.6768448703052663
   C1 = 
      0.8541
      0.6037
      0.1782
      0.1092
      0.5811
      0.4307
      0.9736
      0.5108
   
   C1[5] = 0.43066868121141755

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
      0.5901    0.7498    0.2052    0.5342    0.6419
      0.8553    0.9934    0.6796    0.5451    0.9719
   

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
   
      0.1029    0.1023    0.0413    0.1993    0.3081    0.2096    0.4807    0.9859
      0.4076    0.7758    0.4418    0.9547    0.7781    0.3920    0.1558    0.3708
      0.8713    0.6902    0.5521    0.4385    0.6532    0.2096    0.4949    0.5050
      0.3133    0.6446    0.7785    0.8373    0.9779    0.4148    0.0067    0.5348
      0.1480    0.1289    0.3128    0.1065    0.3007    0.9059    0.1763    0.3323
      0.2892    0.4075    0.2708    0.3664    0.2981    0.3869    0.9320    0.9854
      0.8382    0.6073    0.7212    0.5556    0.6809    0.8608    0.3239    0.5260
      0.4199    0.3865    0.9343    0.5972    0.0817    0.6853    0.2704    0.0710
   
   B = 
   
      0.0705    0.7336    0.1339    0.2343    0.3092    0.5487    0.8373    0.5182
      0.7294    0.6113    0.8518    0.6149    0.4816    0.4875    0.8265    0.0271
      0.4209    0.8784    0.8281    0.5145    0.6959    0.0103    0.2717    0.0526
      0.6725    0.0247    0.8740    0.6238    0.0568    0.5218    0.2709    0.8971
      0.4310    0.3365    0.1492    0.7847    0.7726    0.5716    0.1124    0.1359
      0.8515    0.5703    0.7100    0.8474    0.1561    0.8354    0.4078    0.5487
      0.3484    0.7544    0.8597    0.2728    0.5893    0.4484    0.9019    0.6484
      0.6370    0.2190    0.4090    0.5739    0.6621    0.5551    0.1388    0.6438
   
   C = 
   
      1.3400    0.9809    1.3205    1.3488    1.3279    1.3247    0.9264    1.3402
      2.3824    1.8691    2.5957    2.5933    1.8609    2.1525    1.8005    1.7725
      2.0464    2.3802    2.4233    2.3010    2.1743    2.0997    2.2441    1.7423
      2.5007    2.0162    2.6324    2.8200    2.1749    2.1364    1.5928    1.6810
      1.4818    1.2883    1.4572    1.5836    1.0292    1.3949    0.9526    1.0582
      2.0884    1.9480    2.4538    2.0677    1.9873    2.0098    1.9205    1.9953
      2.6536    2.7129    2.9186    2.9412    2.2846    2.5988    2.3429    2.1006
      1.8647    2.0176    2.4414    1.9486    1.3765    1.5199    1.6290    1.4211
   
   D = 
   
      1.3400    0.9809    1.3205    1.3488    1.3279    1.3247    0.9264    1.3402
      2.3824    1.8691    2.5957    2.5933    1.8609    2.1525    1.8005    1.7725
      2.0464    2.3802    2.4233    2.3010    2.1743    2.0997    2.2441    1.7423
      2.5007    2.0162    2.6324    2.8200    2.1749    2.1364    1.5928    1.6810
      1.4818    1.2883    1.4572    1.5836    1.0292    1.3949    0.9526    1.0582
      2.0884    1.9480    2.4538    2.0677    1.9873    2.0098    1.9205    1.9953
      2.6536    2.7129    2.9186    2.9412    2.2846    2.5988    2.3429    2.1006
      1.8647    2.0176    2.4414    1.9486    1.3765    1.5199    1.6290    1.4211
   


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

   
      0.6366    0.7359    0.3628    0.4067    0.1604    0.6181
      0.9193    0.0703    0.7402    0.1924    0.6933    0.0829
      0.1341    0.6583    0.6054    0.5808    0.5240    0.4881
      0.1851    0.7066    0.1029    0.8155    0.5735    0.8365
      0.8967    0.6580    0.2945    0.0142    0.4920    0.0461
   
   
      0.6366
      0.9193
      0.8967
      0.7359
      0.6583
      0.7066
      0.6580
      0.7402
      0.6054
      0.5808
      0.8155
      0.6933
      0.5240
      0.5735
      0.6181
      0.8365
   

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

   
      8.8171    9.6689    0.3582    4.4388    1.2508    5.9005
      7.9998    8.0978    0.7004    8.7303    2.7203    3.2261
      1.9731    9.4479    6.1196    9.4186    3.4568    0.7132
      7.4991    3.0987    2.0858    4.6268    6.6429    3.3343
      1.8999    0.9192    3.5203    6.3544    8.9603    1.3103
   
   
      8.8171    9.6689    0.0000    0.0000    0.0000    5.9005
      7.9998    8.0978    0.0000    8.7303    0.0000    0.0000
      0.0000    9.4479    6.1196    9.4186    0.0000    0.0000
      7.4991    0.0000    0.0000    0.0000    6.6429    0.0000
      0.0000    0.0000    0.0000    6.3544    8.9603    0.0000
   
   
      8.8171       NaN    0.0000    0.0000    0.0000    5.9005
      7.9998    8.0978    0.0000    8.7303    0.0000    0.0000
      0.0000       NaN    6.1196       NaN    0.0000    0.0000
      7.4991    0.0000    0.0000    0.0000    6.6429    0.0000
      0.0000    0.0000    0.0000    6.3544    8.9603    0.0000
   

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

   
      8.2094    1.8505    0.0070    6.5000    2.9179    6.5000
      1.7824    6.5000    3.2840    3.1477    2.4783    6.5000
      6.5000    4.6754    1.6502    1.5490    6.5000    8.1451
      6.5000    9.9092    2.3283    4.2633    1.2807    8.0933
      1.8269    9.4934    3.7399    3.3676    2.3341    6.5000
   
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
   
