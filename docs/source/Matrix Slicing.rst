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
      0.3787    0.4855    0.9450    0.1202
   
   R1[2] = 0.9449731694438985
   C1 = 
      0.1134
      0.7389
      0.6402
      0.7790
      0.5203
      0.7521
      0.5352
      0.3967
   
   C1[5] = 0.7520753606498132

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
      0.0375    0.1164    0.0814    0.8583    0.4940
      0.7368    0.0990    0.1412    0.5967    0.2281
   

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
   
      0.6782    0.5563    0.3219    0.1359    0.4025    0.8114    0.3441    0.8568
      0.0441    0.7791    0.2342    0.1385    0.9169    0.0233    0.8728    0.8625
      0.1877    0.8464    0.4223    0.8023    0.6284    0.6352    0.4625    0.6127
      0.5349    0.6824    0.7623    0.6217    0.4513    0.9032    0.7405    0.2189
      0.0671    0.5438    0.4483    0.2350    0.5080    0.1448    0.7856    0.3347
      0.4415    0.9649    0.1996    0.8190    0.9807    0.9703    0.7575    0.6146
      0.7771    0.3589    0.1800    0.7304    0.3088    0.5356    0.9199    0.2859
      0.0140    0.0602    0.1221    0.9626    0.0870    0.0724    0.1514    0.2472
   
   B = 
   
      0.4831    0.1256    0.4124    0.5526    0.6711    0.2779    0.7752    0.9055
      0.8652    0.4696    0.1051    0.9964    0.4104    0.4215    0.4714    0.8077
      0.2382    0.9819    0.4940    0.1496    0.2957    0.5750    0.1165    0.3610
      0.7572    0.2646    0.3181    0.3834    0.1079    0.8878    0.7461    0.8709
      0.6086    0.5395    0.2641    0.2570    0.5500    0.2055    0.2009    0.6312
      0.1594    0.9720    0.7232    0.5630    0.8575    0.7638    0.8792    0.8037
      0.9247    0.6643    0.5465    0.2956    0.4491    0.2333    0.8728    0.2578
      0.9351    0.0482    0.6776    0.7646    0.5372    0.1334    0.9491    0.9442
   
   C = 
   
      2.4822    1.9742    2.0021    2.3464    2.3253    1.6258    2.8346    3.1018
      3.0314    1.7767    1.5802    2.0550    1.8131    1.1232    2.3170    2.5111
      3.0154    2.3413    1.9235    2.4422    2.1120    2.1680    2.8622    3.3097
      2.8092    2.9246    2.1921    2.3386    2.4045    2.4112    3.0279    3.2605
      2.1594    1.7191    1.2761    1.4363    1.3624    1.1572    1.7686    1.8221
      3.7425    2.9264    2.4338    3.0414    2.8815    2.5728    3.7260    4.1388
      2.6732    1.9482    1.8448    1.9654    1.9967    1.8445    2.9447    2.8270
      1.2524    0.6345    0.7042    0.7519    0.4848    1.0955    1.2195    1.3292
   
   D = 
   
      2.4822    1.9742    2.0021    2.3464    2.3253    1.6258    2.8346    3.1018
      3.0314    1.7767    1.5802    2.0550    1.8131    1.1232    2.3170    2.5111
      3.0154    2.3413    1.9235    2.4422    2.1120    2.1680    2.8622    3.3097
      2.8092    2.9246    2.1921    2.3386    2.4045    2.4112    3.0279    3.2605
      2.1594    1.7191    1.2761    1.4363    1.3624    1.1572    1.7686    1.8221
      3.7425    2.9264    2.4338    3.0414    2.8815    2.5728    3.7260    4.1388
      2.6732    1.9482    1.8448    1.9654    1.9967    1.8445    2.9447    2.8270
      1.2524    0.6345    0.7042    0.7519    0.4848    1.0955    1.2195    1.3292
   


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

   
      0.4888    0.9350    0.4278    0.2517    0.2814    0.2434
      0.8131    0.9446    0.7211    0.8747    0.9878    0.8785
      0.2815    0.3688    0.6400    0.8775    0.7393    0.4600
      0.9331    0.9386    0.8911    0.7989    0.2605    0.1588
      0.9583    0.5529    0.5599    0.9293    0.3577    0.8637
   
   
      0.8131
      0.9331
      0.9583
      0.9350
      0.9446
      0.9386
      0.5529
      0.7211
      0.6400
      0.8911
      0.5599
      0.8747
      0.8775
      0.7989
      0.9293
      0.9878
      0.7393
      0.8785
      0.8637
   

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

   
      3.5993    0.0456    8.3283    7.8801    1.2911    7.6828
      3.9173    6.4906    0.9375    9.4215    8.7175    0.4135
      9.8283    2.4588    9.2801    9.4490    6.3438    4.3648
      8.5721    2.4089    7.2725    4.0422    7.4066    9.8161
      0.7311    6.1695    8.2183    9.0342    0.2189    0.2796
   
   
      0.0000    0.0000    8.3283    7.8801    0.0000    7.6828
      0.0000    6.4906    0.0000    9.4215    8.7175    0.0000
      9.8283    0.0000    9.2801    9.4490    6.3438    0.0000
      8.5721    0.0000    7.2725    0.0000    7.4066    9.8161
      0.0000    6.1695    8.2183    9.0342    0.0000    0.0000
   
   
      0.0000    0.0000    8.3283    7.8801    0.0000    7.6828
      0.0000    6.4906    0.0000       NaN    8.7175    0.0000
         NaN    0.0000       NaN       NaN    6.3438    0.0000
      8.5721    0.0000    7.2725    0.0000    7.4066       NaN
      0.0000    6.1695    8.2183       NaN    0.0000    0.0000
   

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

   
      6.5000    1.1305    2.3538    2.2089    6.5000    1.1146
      2.3582    6.5000    6.5000    3.2200    2.6092    6.5000
      6.5000    2.8337    0.5226    1.0039    9.3088    0.7274
      3.3210    6.5000    1.4838    8.6870    3.4951    6.5000
      0.3630    3.4867    3.2618    0.0995    9.9693    2.8213
   
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
   
