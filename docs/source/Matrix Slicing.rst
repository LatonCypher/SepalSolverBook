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
      0.2181    0.9912    0.1673    0.6963
   
   R1[2] = 0.16728458333780927
   C1 = 
      0.1103
      0.0914
      0.9444
      0.0045
      0.7238
      0.6406
      0.2238
      0.1249
   
   C1[5] = 0.6405757346744329

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
      0.0601    0.4786    0.9318    0.3273    0.8224
      0.7680    0.0618    0.6536    0.7765    0.6106
   

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
   
      0.6552    0.6712    0.3296    0.9368    0.9945    0.3385    0.0438    0.8634
      0.8094    0.7174    0.1970    0.0997    0.7589    0.3427    0.5699    0.4633
      0.7211    0.2036    0.0380    0.9675    0.8024    0.2038    0.4645    0.9043
      0.0409    0.2535    0.3450    0.1266    0.2303    0.1511    0.7730    0.2323
      0.1838    0.5933    0.7888    0.6460    0.1499    0.9424    0.2586    0.3078
      0.6908    0.9897    0.3446    0.1904    0.4669    0.5431    0.7569    0.2172
      0.7083    0.6833    0.8616    0.3475    0.4869    0.6475    0.8437    0.1286
      0.0330    0.1410    0.4630    0.1800    0.7308    0.6282    0.5693    0.2066
   
   B = 
   
      0.6577    0.8762    0.9529    0.4570    0.9684    0.2742    0.1694    0.8975
      0.3892    0.3080    0.9929    0.4191    0.0744    0.5577    0.4938    0.7978
      0.0140    0.8076    0.5374    0.3891    0.5275    0.6101    0.6059    0.9029
      0.2478    0.5964    0.2686    0.2193    0.1929    0.0911    0.2591    0.6376
      0.7135    0.8291    0.6206    0.2330    0.1957    0.0250    0.2509    0.1094
      0.6727    0.8273    0.5605    0.7101    0.5425    0.9609    0.7724    0.5959
      0.5847    0.5022    0.3812    0.3120    0.0374    0.0914    0.4637    0.5121
      0.2162    0.7820    0.0121    0.2493    0.1645    0.3684    0.4559    0.9748
   
   C = 
   
      2.0785    3.4075    2.5535    1.6154    1.5610    1.5126    1.8098    3.1930
      2.0444    2.7098    2.5020    1.4825    1.3922    1.3223    1.5671    2.5708
      1.9705    3.0765    1.9697    1.3438    1.3539    1.0141    1.4828    2.7894
      0.9300    1.3539    1.0352    0.7470    0.4591    0.6818    1.0128    1.3688
      1.4816    2.6405    2.0850    1.6428    1.3637    1.9677    1.9952    2.7729
      2.0796    2.6882    2.7624    1.6910    1.4110    1.6516    1.8504    2.8161
      2.1341    3.1977    2.8979    1.8900    1.7575    1.8914    2.1416    3.1768
      1.4492    2.1266    1.4937    1.1392    0.8605    1.1365    1.4291    1.6221
   
   D = 
   
      2.0785    3.4075    2.5535    1.6154    1.5610    1.5126    1.8098    3.1930
      2.0444    2.7098    2.5020    1.4825    1.3922    1.3223    1.5671    2.5708
      1.9705    3.0765    1.9697    1.3438    1.3539    1.0141    1.4828    2.7894
      0.9300    1.3539    1.0352    0.7470    0.4591    0.6818    1.0128    1.3688
      1.4816    2.6405    2.0850    1.6428    1.3637    1.9677    1.9952    2.7729
      2.0796    2.6882    2.7624    1.6910    1.4110    1.6516    1.8504    2.8161
      2.1341    3.1977    2.8979    1.8900    1.7575    1.8914    2.1416    3.1768
      1.4492    2.1266    1.4937    1.1392    0.8605    1.1365    1.4291    1.6221
   


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

   
      0.3995    0.2896    0.7765    0.7110    0.0666    0.0342
      0.4438    0.7974    0.8792    0.1456    0.7551    0.6982
      0.9424    0.7967    0.7792    0.7857    0.4094    0.4617
      0.2714    0.3139    0.7352    0.2776    0.5169    0.7231
      0.3069    0.8232    0.8720    0.6988    0.6189    0.4490
   
   
      0.9424
      0.7974
      0.7967
      0.8232
      0.7765
      0.8792
      0.7792
      0.7352
      0.8720
      0.7110
      0.7857
      0.6988
      0.7551
      0.5169
      0.6189
      0.6982
      0.7231
   

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

   
      3.4756    0.5909    0.1480    4.2391    5.7564    8.2501
      3.6488    8.5738    9.9871    1.8879    8.5386    9.6885
      7.1470    5.2152    2.8592    0.9468    4.1106    0.5329
      9.6291    7.9369    3.0927    5.4247    3.2674    0.1576
      9.9707    1.3292    0.0796    5.5802    6.6933    0.2236
   
   
      0.0000    0.0000    0.0000    0.0000    5.7564    8.2501
      0.0000    8.5738    9.9871    0.0000    8.5386    9.6885
      7.1470    5.2152    0.0000    0.0000    0.0000    0.0000
      9.6291    7.9369    0.0000    5.4247    0.0000    0.0000
      9.9707    0.0000    0.0000    5.5802    6.6933    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    5.7564    8.2501
      0.0000    8.5738       NaN    0.0000    8.5386       NaN
      7.1470    5.2152    0.0000    0.0000    0.0000    0.0000
         NaN    7.9369    0.0000    5.4247    0.0000    0.0000
         NaN    0.0000    0.0000    5.5802    6.6933    0.0000
   

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

   
      2.0557    1.7239    6.5000    9.7207    6.5000    6.5000
      6.5000    6.5000    8.0516    6.5000    6.5000    0.5765
      6.5000    1.2428    3.0746    3.2277    6.5000    6.5000
      2.5698    6.5000    2.6814    4.9274    6.5000    1.5724
      2.4377    6.5000    6.5000    6.5000    0.0941    6.5000
   
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
   
