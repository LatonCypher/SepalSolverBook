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
      0.6603    0.6283    0.7595    0.5579
   
   R1[2] = 0.7595319878359205
   C1 = 
      0.4591
      0.9392
      0.2739
      0.0592
      0.0792
      0.4279
      0.5845
      0.3285
   
   C1[5] = 0.4278812781993152

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
      0.9885    0.8505    0.7890    0.4519    0.6026
      0.7652    0.4190    0.9114    0.1529    0.2757
   

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
   
      0.6575    0.4269    0.0874    0.5283    0.3177    0.5351    0.6626    0.0768
      0.1718    0.4327    0.4708    0.6574    0.5878    0.7056    0.0782    0.4865
      0.5576    0.1654    0.7535    0.6495    0.2539    0.8944    0.4027    0.8863
      0.3272    0.1985    0.6732    0.1013    0.1885    0.4497    0.7703    0.3089
      0.4581    0.9220    0.8639    0.9291    0.3633    0.5820    0.7061    0.8441
      0.7246    0.9384    0.5346    0.5204    0.7575    0.4948    0.8634    0.4580
      0.3664    0.2336    0.2052    0.7212    0.9810    0.0141    0.7258    0.3030
      0.0541    0.5734    0.5052    0.9738    0.3129    0.4923    0.0977    0.4562
   
   B = 
   
      0.9620    0.1059    0.3465    0.9785    0.5711    0.4219    0.5393    0.6729
      0.9217    0.3824    0.0782    0.7185    0.0474    0.5138    0.9529    0.4421
      0.7394    0.5914    0.7885    0.2311    0.8366    0.8620    0.9580    0.6455
      0.4564    0.6874    0.2280    0.8706    0.0721    0.1653    0.5306    0.8095
      0.8001    0.2455    0.7494    0.1704    0.0300    0.9920    0.2296    0.1417
      0.8779    0.3259    0.8480    0.1908    0.1673    0.3282    0.0005    0.5444
      0.4512    0.6254    0.7894    0.6891    0.8637    0.2468    0.3653    0.4374
      0.1303    0.4630    0.3121    0.4546    0.8789    0.4798    0.4374    0.6939
   
   C = 
   
      2.3645    1.3500    1.6894    2.0779    1.2458    1.3506    1.4742    1.7946
      2.4006    1.5624    1.8669    1.6700    1.1907    1.8768    1.6815    1.9821
      2.8279    2.0304    2.4916    2.2984    2.2875    2.1472    2.1183    2.7744
      1.9750    1.3959    1.9098    1.4958    1.7844    1.5100    1.5242    1.6472
      3.5835    2.6620    2.7105    3.1625    2.5551    2.6961    3.1572    3.2885
      3.6844    2.2087    2.6764    2.9866    2.1969    2.6815    2.7632    2.7409
      2.2128    1.5849    1.8861    2.0094    1.3689    1.8731    1.6224    1.7404
      2.1846    1.7027    1.5555    1.8513    1.1280    1.6289    1.8835    2.0758
   
   D = 
   
      2.3645    1.3500    1.6894    2.0779    1.2458    1.3506    1.4742    1.7946
      2.4006    1.5624    1.8669    1.6700    1.1907    1.8768    1.6815    1.9821
      2.8279    2.0304    2.4916    2.2984    2.2875    2.1472    2.1183    2.7744
      1.9750    1.3959    1.9098    1.4958    1.7844    1.5100    1.5242    1.6472
      3.5835    2.6620    2.7105    3.1625    2.5551    2.6961    3.1572    3.2885
      3.6844    2.2087    2.6764    2.9866    2.1969    2.6815    2.7632    2.7409
      2.2128    1.5849    1.8861    2.0094    1.3689    1.8731    1.6224    1.7404
      2.1846    1.7027    1.5555    1.8513    1.1280    1.6289    1.8835    2.0758
   


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

   
      0.0587    0.7343    0.6386    0.6546    0.3978    0.5056
      0.8329    0.2782    0.0652    0.7045    0.0507    0.1559
      0.7030    0.7096    0.4776    0.0783    0.5423    0.2568
      0.5466    0.0443    0.2185    0.0881    0.1004    0.4716
      0.9083    0.8900    0.5668    0.8791    0.7025    0.5539
   
   
      0.8329
      0.7030
      0.5466
      0.9083
      0.7343
      0.7096
      0.8900
      0.6386
      0.5668
      0.6546
      0.7045
      0.8791
      0.5423
      0.7025
      0.5056
      0.5539
   

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

   
      9.6357    9.9237    2.4221    5.7583    9.1850    0.6427
      6.2140    3.8634    3.2906    3.6699    4.5932    7.9078
      7.1371    1.7693    6.7233    5.6220    0.4566    5.8820
      4.4451    4.7491    2.0169    2.8700    4.6499    3.8127
      8.3206    8.7446    8.9700    6.0236    0.9532    1.6939
   
   
      9.6357    9.9237    0.0000    5.7583    9.1850    0.0000
      6.2140    0.0000    0.0000    0.0000    0.0000    7.9078
      7.1371    0.0000    6.7233    5.6220    0.0000    5.8820
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.3206    8.7446    8.9700    6.0236    0.0000    0.0000
   
   
         NaN       NaN    0.0000    5.7583       NaN    0.0000
      6.2140    0.0000    0.0000    0.0000    0.0000    7.9078
      7.1371    0.0000    6.7233    5.6220    0.0000    5.8820
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.3206    8.7446    8.9700    6.0236    0.0000    0.0000
   

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

   
      2.7153    0.1993    6.5000    6.5000    6.5000    6.5000
      8.5327    8.7684    6.5000    9.0982    9.4957    1.2701
      6.5000    1.2988    1.6309    6.5000    8.9617    3.1653
      0.6365    4.4130    0.3879    8.0695    0.1412    6.5000
      9.6159    6.5000    4.7251    3.9695    4.1717    8.8417
   
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
   
