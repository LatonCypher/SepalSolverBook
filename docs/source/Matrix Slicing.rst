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
      0.4104    0.9450    0.9745    0.2437
   
   R1[2] = 0.9745262059516179
   C1 = 
      0.7259
      0.5572
      0.2806
      0.7889
      0.7708
      0.1218
      0.9747
      0.8696
   
   C1[5] = 0.12182452493417739

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
      0.0592    0.7813    0.0437    0.3069    0.5763
      0.3034    0.0920    0.5998    0.9731    0.4861
   

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
   
      0.8647    0.7039    0.6417    0.4163    0.6416    0.7016    0.9514    0.0396
      0.9616    0.2011    0.0172    0.1068    0.5759    0.0298    0.5187    0.4893
      0.2573    0.8182    0.4757    0.6491    0.4999    0.4321    0.0589    0.8702
      0.2752    0.7730    0.5101    0.4117    0.0701    0.5527    0.2288    0.8060
      0.0149    0.5264    0.5128    0.0378    0.5839    0.4861    0.5777    0.3430
      0.2560    0.9102    0.5165    0.8370    0.9986    0.8646    0.9070    0.7546
      0.5560    0.4754    0.6652    0.6061    0.9357    0.9575    0.5981    0.9220
      0.9647    0.4068    0.2227    0.9024    0.3994    0.0938    0.4422    0.2928
   
   B = 
   
      0.2195    0.9553    0.8162    0.1204    0.6637    0.7091    0.8869    0.0531
      0.5816    0.6719    0.4652    0.0457    0.3622    0.4900    0.7756    0.2916
      0.8530    0.4311    0.4032    0.4969    0.0717    0.0971    0.4321    0.9873
      0.2900    0.4148    0.4088    0.8539    0.3859    0.2134    0.1523    0.1431
      0.1574    0.9554    0.4546    0.8322    0.5464    0.3529    0.5530    0.3967
      0.2476    0.3522    0.7279    0.4496    0.9627    0.1840    0.9743    0.9800
      0.0993    0.2785    0.0732    0.6850    0.9386    0.8961    0.3951    0.6614
      0.6879    0.9220    0.0155    0.7132    0.1950    0.6995    0.6653    0.4306
   
   C = 
   
      1.6637    2.9099    2.3347    2.3400    2.9622    2.3449    3.0942    2.5327
      0.8599    2.2618    1.2581    1.4218    1.6792    1.8207    1.9107    0.9536
      1.9166    2.7184    1.6074    2.1302    1.6658    1.6855    2.4669    1.8503
      1.7896    2.2415    1.4216    1.7119    1.6003    1.6066    2.3307    1.8704
      1.2634    1.8109    1.1461    1.6578    1.6481    1.3795    1.9018    1.9038
      2.2493    3.6329    2.3441    3.4230    3.2360    2.7083    3.5386    3.0770
      2.2197    3.6367    2.3714    3.2132    2.9970    2.5085    3.5417    3.0138
      1.2315    2.4730    1.7221    1.9023    1.9325    1.8569    2.0866    1.1879
   
   D = 
   
      1.6637    2.9099    2.3347    2.3400    2.9622    2.3449    3.0942    2.5327
      0.8599    2.2618    1.2581    1.4218    1.6792    1.8207    1.9107    0.9536
      1.9166    2.7184    1.6074    2.1302    1.6658    1.6855    2.4669    1.8503
      1.7896    2.2415    1.4216    1.7119    1.6003    1.6066    2.3307    1.8704
      1.2634    1.8109    1.1461    1.6578    1.6481    1.3795    1.9018    1.9038
      2.2493    3.6329    2.3441    3.4230    3.2360    2.7083    3.5386    3.0770
      2.2197    3.6367    2.3714    3.2132    2.9970    2.5085    3.5417    3.0138
      1.2315    2.4730    1.7221    1.9023    1.9325    1.8569    2.0866    1.1879
   


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

   
      0.2986    0.4462    0.4853    0.9592    0.6109    0.8704
      0.2687    0.5320    0.6369    0.6449    0.3247    0.9868
      0.2074    0.0631    0.8801    0.6600    0.5723    0.0826
      0.8203    0.7693    0.5938    0.7504    0.9493    0.3913
      0.2478    0.2007    0.5207    0.7557    0.8342    0.0385
   
   
      0.8203
      0.5320
      0.7693
      0.6369
      0.8801
      0.5938
      0.5207
      0.9592
      0.6449
      0.6600
      0.7504
      0.7557
      0.6109
      0.5723
      0.9493
      0.8342
      0.8704
      0.9868
   

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

   
      2.7041    8.8380    5.8730    0.6820    3.8523    4.6560
      9.0462    9.4006    2.9242    2.0486    1.2812    0.2848
      6.2751    2.4109    1.1654    8.3658    2.1040    0.8757
      3.0244    7.6560    4.4698    7.0233    3.4109    1.8381
      0.6525    0.8938    3.3802    3.2127    4.0872    4.1670
   
   
      0.0000    8.8380    5.8730    0.0000    0.0000    0.0000
      9.0462    9.4006    0.0000    0.0000    0.0000    0.0000
      6.2751    0.0000    0.0000    8.3658    0.0000    0.0000
      0.0000    7.6560    0.0000    7.0233    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    8.8380    5.8730    0.0000    0.0000    0.0000
         NaN       NaN    0.0000    0.0000    0.0000    0.0000
      6.2751    0.0000    0.0000    8.3658    0.0000    0.0000
      0.0000    7.6560    0.0000    7.0233    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      0.5337    6.5000    8.6843    0.4827    6.5000    6.5000
      6.5000    6.5000    8.6863    3.3498    6.5000    1.6096
      2.3976    2.6470    8.9311    6.5000    9.7199    6.5000
      9.7317    1.0929    6.5000    9.7910    4.8371    9.7540
      6.5000    6.5000    9.7747    4.4498    9.8481    2.6867
   
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
   
