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
      0.3179    0.9648    0.5962    0.2325
   
   R1[2] = 0.5962154759538475
   C1 = 
      0.7944
      0.1733
      0.0077
      0.7755
      0.2340
      0.6518
      0.3039
      0.1093
   
   C1[5] = 0.651771904077101

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
      0.3409    0.2476    0.7216    0.5036    0.2271
      0.6496    0.5328    0.1893    0.8388    0.5767
   

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
   
      0.0479    0.9333    0.7224    0.6465    0.5699    0.8184    0.7807    0.9996
      0.0139    0.3879    0.6389    0.2598    0.9248    0.4104    0.1130    0.3767
      0.3147    0.5868    0.7193    0.4458    0.4499    0.9355    0.1522    0.3330
      0.6008    0.4434    0.0227    0.0285    0.2767    0.1934    0.7563    0.6386
      0.4308    0.4830    0.9143    0.4912    0.1154    0.1748    0.2846    0.4823
      0.9762    0.0285    0.5780    0.6880    0.7443    0.6092    0.4046    0.3779
      0.0972    0.9018    0.8705    0.1018    0.9450    0.4007    0.8926    0.5350
      0.4192    0.1972    0.9008    0.9561    0.9418    0.8623    0.1011    0.3467
   
   B = 
   
      0.8460    0.1368    0.7734    0.6438    0.7244    0.4948    0.0558    0.9211
      0.5854    0.3860    0.2468    0.2182    0.9399    0.4069    0.7898    0.2841
      0.3619    0.5110    0.7390    0.7240    0.8601    0.1111    0.0144    0.2862
      0.2360    0.7908    0.7387    0.6371    0.1903    0.1580    0.4844    0.2232
      0.4549    0.5948    0.0808    0.0545    0.0239    0.6266    0.2081    0.3133
      0.9675    0.4314    0.7809    0.3383    0.2628    0.8512    0.9164    0.1077
      0.9274    0.3454    0.8924    0.7778    0.4945    0.5958    0.2052    0.3972
      0.8129    0.7286    0.5610    0.3563    0.2484    0.9756    0.7647    0.4538
   
   C = 
   
      3.5885    2.9371    3.2213    2.4407    2.5193    3.0800    2.8565    1.6907
      1.7602    1.7241    1.4778    1.1330    1.2529    1.6405    1.3220    0.9136
      2.4968    1.9559    2.3385    1.7134    1.8975    2.0386    1.9441    1.2152
      2.3164    1.2619    1.8184    1.4145    1.4670    1.8964    1.2762    1.3899
      1.9715    1.6947    2.1612    1.8162    1.9550    1.4500    1.2679    1.2921
      2.8246    2.1046    2.8063    2.1876    1.8338    2.2622    1.5037    1.8572
      3.0295    2.3197    2.5021    2.0264    2.3882    2.5149    1.9357    1.5541
      2.6600    2.5696    2.7790    2.1194    1.8309    2.2615    1.9273    1.4986
   
   D = 
   
      3.5885    2.9371    3.2213    2.4407    2.5193    3.0800    2.8565    1.6907
      1.7602    1.7241    1.4778    1.1330    1.2529    1.6405    1.3220    0.9136
      2.4968    1.9559    2.3385    1.7134    1.8975    2.0386    1.9441    1.2152
      2.3164    1.2619    1.8184    1.4145    1.4670    1.8964    1.2762    1.3899
      1.9715    1.6947    2.1612    1.8162    1.9550    1.4500    1.2679    1.2921
      2.8246    2.1046    2.8063    2.1876    1.8338    2.2622    1.5037    1.8572
      3.0295    2.3197    2.5021    2.0264    2.3882    2.5149    1.9357    1.5541
      2.6600    2.5696    2.7790    2.1194    1.8309    2.2615    1.9273    1.4986
   


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

   
      0.1451    0.5713    0.6820    0.6641    0.0137    0.2629
      0.6688    0.4922    0.6524    0.0768    0.3033    0.4091
      0.7512    0.0305    0.4104    0.9388    0.0645    0.9594
      0.0490    0.3329    0.3390    0.7831    0.6840    0.1453
      0.2972    0.8742    0.2619    0.1217    0.9814    0.8934
   
   
      0.6688
      0.7512
      0.5713
      0.8742
      0.6820
      0.6524
      0.6641
      0.9388
      0.7831
      0.6840
      0.9814
      0.9594
      0.8934
   

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

   
      0.7308    8.5049    4.6860    3.9468    2.3277    7.9338
      0.0118    0.3039    7.1327    4.5323    4.7575    5.6414
      7.6616    2.0773    0.9894    9.4648    8.2311    6.2150
      8.6285    3.9839    4.8379    3.8570    3.0322    2.3470
      7.5724    4.2869    9.9801    0.0260    5.4746    6.6082
   
   
      0.0000    8.5049    0.0000    0.0000    0.0000    7.9338
      0.0000    0.0000    7.1327    0.0000    0.0000    5.6414
      7.6616    0.0000    0.0000    9.4648    8.2311    6.2150
      8.6285    0.0000    0.0000    0.0000    0.0000    0.0000
      7.5724    0.0000    9.9801    0.0000    5.4746    6.6082
   
   
      0.0000    8.5049    0.0000    0.0000    0.0000    7.9338
      0.0000    0.0000    7.1327    0.0000    0.0000    5.6414
      7.6616    0.0000    0.0000       NaN    8.2311    6.2150
      8.6285    0.0000    0.0000    0.0000    0.0000    0.0000
      7.5724    0.0000       NaN    0.0000    5.4746    6.6082
   

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

   
      3.0523    6.5000    6.5000    8.5524    8.0597    3.9363
      2.7031    0.8487    9.4061    4.5559    2.1027    6.5000
      9.5623    6.5000    8.9891    9.2845    3.2490    4.9598
      4.2743    6.5000    8.7934    6.5000    9.2737    4.0306
      6.5000    2.3760    0.7840    0.7669    9.6749    6.5000
   
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
   
