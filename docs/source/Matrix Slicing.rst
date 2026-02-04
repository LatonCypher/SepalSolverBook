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
      0.0879    0.1550    0.8248    0.5640
   
   R1[2] = 0.8247647349659196
   C1 = 
      0.9014
      0.0905
      0.2684
      0.3347
      0.9588
      0.6659
      0.4907
      0.4848
   
   C1[5] = 0.665850014524206

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
      0.3138    0.7530    0.2868    0.0160    0.1957
      0.4025    0.1703    0.8770    0.9131    0.4940
   

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
   
      0.8106    0.3523    0.9854    0.6745    0.5037    0.9800    0.2740    0.2942
      0.5354    0.5665    0.2453    0.6583    0.7419    0.5426    0.9818    0.1806
      0.0759    0.5101    0.4233    0.3104    0.1053    0.2368    0.9996    0.6881
      0.2692    0.3501    0.0164    0.9410    0.6593    0.1814    0.2344    0.9318
      0.5620    0.0372    0.2059    0.0751    0.7030    0.4370    0.1565    0.3371
      0.8256    0.7749    0.4913    0.9111    0.6361    0.9869    0.5702    0.8196
      0.7022    0.3488    0.7256    0.9805    0.6477    0.7304    0.1870    0.1099
      0.1229    0.0980    0.2612    0.7111    0.0331    0.9496    0.5955    0.6496
   
   B = 
   
      0.2796    0.3439    0.8568    0.5955    0.4128    0.7692    0.4561    0.8980
      0.1258    0.8493    0.2502    0.4740    0.8236    0.0388    0.0905    0.6791
      0.7073    0.9806    0.2476    0.5310    0.9421    0.4743    0.5959    0.6504
      0.6924    0.6310    0.6816    0.6051    0.9755    0.7373    0.9830    0.7499
      0.0175    0.0216    0.3879    0.8121    0.5482    0.6890    0.8909    0.3346
      0.9256    0.9815    0.5666    0.9612    0.9774    0.1844    0.3962    0.6993
      0.8794    0.2975    0.9309    0.4871    0.4486    0.3392    0.6399    0.3228
      0.7034    0.0554    0.7243    0.8872    0.1839    0.3878    0.0795    0.8599
   
   C = 
   
      2.7987    3.0403    2.7052    3.3266    3.6222    2.3367    2.6876    3.3093
      2.3560    2.1719    2.7499    2.8786    2.9717    2.0500    2.6075    2.6187
      2.1838    1.6405    2.1129    2.1101    2.0172    1.2300    1.5202    2.0379
      1.8235    1.3133    2.2153    2.5550    2.1481    1.8508    1.9725    2.4203
      1.1510    0.9834    1.5030    1.8733    1.4746    1.3355    1.3825    1.6014
      3.3091    3.1959    3.5741    4.1411    4.0504    2.7014    3.0228    4.0622
      2.3615    2.6604    2.4556    2.9787    3.3903    2.3078    2.7429    2.9571
      2.5839    1.9762    2.2548    2.4946    2.4042    1.3983    1.7581    2.3061
   
   D = 
   
      2.7987    3.0403    2.7052    3.3266    3.6222    2.3367    2.6876    3.3093
      2.3560    2.1719    2.7499    2.8786    2.9717    2.0500    2.6075    2.6187
      2.1838    1.6405    2.1129    2.1101    2.0172    1.2300    1.5202    2.0379
      1.8235    1.3133    2.2153    2.5550    2.1481    1.8508    1.9725    2.4203
      1.1510    0.9834    1.5030    1.8733    1.4746    1.3355    1.3825    1.6014
      3.3091    3.1959    3.5741    4.1411    4.0504    2.7014    3.0228    4.0622
      2.3615    2.6604    2.4556    2.9787    3.3903    2.3078    2.7429    2.9571
      2.5839    1.9762    2.2548    2.4946    2.4042    1.3983    1.7581    2.3061
   


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

   
      0.3672    0.2249    0.1376    0.5827    0.1699    0.7180
      0.9745    0.4691    0.9463    0.0031    0.6261    0.1581
      0.3744    0.4544    0.6017    0.3398    0.8562    0.1118
      0.5134    0.2723    0.2442    0.6842    0.3677    0.7187
      0.5545    0.2566    0.3805    0.5354    0.9891    0.2063
   
   
      0.9745
      0.5134
      0.5545
      0.9463
      0.6017
      0.5827
      0.6842
      0.5354
      0.6261
      0.8562
      0.9891
      0.7180
      0.7187
   

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

   
      7.7524    5.6072    4.4926    0.6516    7.2339    3.5444
      1.0266    2.5840    0.4908    7.2337    0.5717    3.8365
      7.6777    0.0782    4.0711    0.0430    2.7940    1.3316
      5.9127    0.9048    2.7117    3.4272    5.1166    7.8850
      5.9698    8.0769    0.8640    4.4235    6.2437    7.0130
   
   
      7.7524    5.6072    0.0000    0.0000    7.2339    0.0000
      0.0000    0.0000    0.0000    7.2337    0.0000    0.0000
      7.6777    0.0000    0.0000    0.0000    0.0000    0.0000
      5.9127    0.0000    0.0000    0.0000    5.1166    7.8850
      5.9698    8.0769    0.0000    0.0000    6.2437    7.0130
   
   
      7.7524    5.6072    0.0000    0.0000    7.2339    0.0000
      0.0000    0.0000    0.0000    7.2337    0.0000    0.0000
      7.6777    0.0000    0.0000    0.0000    0.0000    0.0000
      5.9127    0.0000    0.0000    0.0000    5.1166    7.8850
      5.9698    8.0769    0.0000    0.0000    6.2437    7.0130
   

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

   
      1.4634    3.2016    6.5000    8.5926    1.8207    0.2115
      4.0804    3.8641    4.1542    8.2212    3.1214    1.8790
      9.0978    6.5000    6.5000    1.7351    8.1554    6.5000
      6.5000    0.4570    6.5000    8.9885    6.5000    4.0016
      8.1380    6.5000    4.2905    6.5000    4.0267    6.5000
   
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
   
