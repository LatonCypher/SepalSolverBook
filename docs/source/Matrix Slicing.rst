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
      0.9100    0.1856    0.0135    0.0197
   
   R1[2] = 0.013530950765343164
   C1 = 
      0.5969
      0.6003
      0.2679
      0.5049
      0.8945
      0.5619
      0.6768
      0.7376
   
   C1[5] = 0.5619436198160287

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
      0.6103    0.1673    0.4553    0.2954    0.0147
      0.0300    0.0094    0.5359    0.0426    0.5649
   

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
   
      0.3764    0.7767    0.6258    0.7110    0.6655    0.6741    0.3820    0.6556
      0.7531    0.6950    0.9297    0.1922    0.2328    0.3286    0.8547    0.6634
      0.7982    0.1885    0.0932    0.4880    0.9639    0.8874    0.5413    0.7652
      0.4812    0.8860    0.9906    0.2578    0.4692    0.6620    0.8909    0.7395
      0.8252    0.7598    0.5183    0.3070    0.8376    0.8093    0.1114    0.0491
      0.0927    0.7704    0.6954    0.3016    0.3653    0.8553    0.2917    0.9256
      0.5311    0.9129    0.3442    0.5658    0.2128    0.9434    0.1587    0.8106
      0.9373    0.2286    0.4027    0.5206    0.6610    0.8274    0.4276    0.1299
   
   B = 
   
      0.3852    0.0991    0.0401    0.8057    0.8717    0.6416    0.5453    0.0776
      0.6146    0.3760    0.1593    0.0491    0.1968    0.8744    0.4942    0.5143
      0.9417    0.9039    0.1360    0.2494    0.6008    0.2657    0.5025    0.3305
      0.1283    0.1520    0.6443    0.5354    0.7070    0.3857    0.5392    0.9743
      0.0460    0.1163    0.9144    0.4639    0.5945    0.0324    0.8388    0.0002
      0.2313    0.3505    0.0768    0.6602    0.6581    0.6924    0.5929    0.8269
      0.9329    0.4354    0.1405    0.0308    0.7044    0.1068    0.0871    0.3521
      0.7866    0.8777    0.4819    0.7817    0.0023    0.2757    0.5306    0.1853
   
   C = 
   
      2.3616    2.0585    1.7119    2.1562    2.4694    2.0710    2.6260    2.1419
      3.0233    2.3021    1.0690    1.8454    2.4458    1.9212    2.1414    1.6060
      1.9301    1.6388    1.7834    2.5847    2.6739    1.8043    2.6262    1.7315
      3.2834    2.6390    1.4227    2.0765    2.7151    2.2187    2.5932    2.0699
      1.6806    1.3553    1.2898    1.9606    2.5064    2.0630    2.4697    1.6430
      2.4176    2.2549    1.3020    1.9140    1.8510    1.9245    2.2734    1.9086
      2.1761    1.9290    1.2580    2.2216    2.1105    2.3491    2.4006    2.1620
      1.6704    1.2890    1.2547    2.1131    2.7109    1.7848    2.2583    1.6895
   
   D = 
   
      2.3616    2.0585    1.7119    2.1562    2.4694    2.0710    2.6260    2.1419
      3.0233    2.3021    1.0690    1.8454    2.4458    1.9212    2.1414    1.6060
      1.9301    1.6388    1.7834    2.5847    2.6739    1.8043    2.6262    1.7315
      3.2834    2.6390    1.4227    2.0765    2.7151    2.2187    2.5932    2.0699
      1.6806    1.3553    1.2898    1.9606    2.5064    2.0630    2.4697    1.6430
      2.4176    2.2549    1.3020    1.9140    1.8510    1.9245    2.2734    1.9086
      2.1761    1.9290    1.2580    2.2216    2.1105    2.3491    2.4006    2.1620
      1.6704    1.2890    1.2547    2.1131    2.7109    1.7848    2.2583    1.6895
   


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

   
      0.3358    0.2289    0.5410    0.0851    0.2711    0.9722
      0.2137    0.7113    0.3662    0.8121    0.9415    0.5223
      0.8663    0.4360    0.2276    0.0447    0.2413    0.3163
      0.0614    0.5856    0.6276    0.8676    0.8635    0.7784
      0.1315    0.7510    0.0141    0.7171    0.8342    0.9057
   
   
      0.8663
      0.7113
      0.5856
      0.7510
      0.5410
      0.6276
      0.8121
      0.8676
      0.7171
      0.9415
      0.8635
      0.8342
      0.9722
      0.5223
      0.7784
      0.9057
   

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

   
      2.5300    9.7815    4.4954    5.3953    2.2814    8.3761
      7.5355    8.2092    1.3636    5.6829    6.1682    9.9505
      0.5584    2.7537    4.2077    9.9856    2.5199    0.3033
      2.7743    2.7853    9.4775    3.1920    6.1217    5.6930
      8.6209    9.3667    0.1530    6.1354    9.8650    5.9822
   
   
      0.0000    9.7815    0.0000    5.3953    0.0000    8.3761
      7.5355    8.2092    0.0000    5.6829    6.1682    9.9505
      0.0000    0.0000    0.0000    9.9856    0.0000    0.0000
      0.0000    0.0000    9.4775    0.0000    6.1217    5.6930
      8.6209    9.3667    0.0000    6.1354    9.8650    5.9822
   
   
      0.0000       NaN    0.0000    5.3953    0.0000    8.3761
      7.5355    8.2092    0.0000    5.6829    6.1682       NaN
      0.0000    0.0000    0.0000       NaN    0.0000    0.0000
      0.0000    0.0000       NaN    0.0000    6.1217    5.6930
      8.6209       NaN    0.0000    6.1354       NaN    5.9822
   

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

   
      6.5000    3.7413    6.5000    1.7159    6.5000    0.9814
      0.2188    3.9464    9.4685    6.5000    8.1616    6.5000
      3.3441    6.5000    9.6940    6.5000    6.5000    6.5000
      8.5220    6.5000    6.5000    4.2715    1.4700    2.8750
      1.2678    8.5110    8.6957    9.5584    9.1221    6.5000
   
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
   
