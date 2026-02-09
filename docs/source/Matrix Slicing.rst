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
      0.1888    0.8348    0.6319    0.6782
   
   R1[2] = 0.6318803773067209
   C1 = 
      0.7549
      0.4054
      0.0786
      0.8269
      0.5273
      0.0199
      0.2573
      0.6234
   
   C1[5] = 0.019926815533422904

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
      0.5386    0.8847    0.1414    0.7221    0.1494
      0.2046    0.0377    0.1582    0.1228    0.1510
   

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
   
      0.8059    0.2692    0.0132    0.0619    0.4622    0.4072    0.3840    0.3009
      0.7576    0.7747    0.0977    0.6541    0.5607    0.2936    0.0739    0.0467
      0.7708    0.7111    0.8216    0.8688    0.8954    0.5027    0.2319    0.5477
      0.3924    0.7334    0.1972    0.0878    0.5757    0.6935    0.0425    0.7050
      0.4525    0.3698    0.4062    0.5351    0.4716    0.1234    0.7719    0.8277
      0.6807    0.3737    0.6801    0.7083    0.0892    0.8019    0.8986    0.1128
      0.1676    0.2097    0.1770    0.3585    0.5110    0.7921    0.5862    0.2732
      0.5679    0.6148    0.6855    0.0927    0.4421    0.2494    0.1898    0.7217
   
   B = 
   
      0.1981    0.2020    0.4640    0.7951    0.5554    0.2546    0.1177    0.7887
      0.5625    0.6247    0.8106    0.7353    0.1459    0.4041    0.1915    0.6885
      0.9955    0.0381    0.7789    0.7721    0.5763    0.2548    0.2334    0.5480
      0.5996    0.7855    0.5613    0.5162    0.3214    0.2706    0.8068    0.9853
      0.4074    0.1338    0.7748    0.9259    0.7522    0.9965    0.7516    0.7792
      0.7453    0.1971    0.6445    0.2247    0.2215    0.6035    0.7186    0.7616
      0.3713    0.7514    0.9747    0.9171    0.5841    0.2614    0.6829    0.2721
      0.0458    0.6133    0.9022    0.9141    0.1570    0.4318    0.3375    0.7561
   
   C = 
   
      1.0095    0.9953    1.9035    2.0276    1.2237    1.2707    1.2033    1.8915
      1.5522    1.3716    2.1607    2.2808    1.3376    1.4833    1.4868    2.5449
      2.7422    2.0427    3.7996    3.8740    2.2908    2.4208    2.4971    3.9616
      1.5388    1.2919    2.5499    2.4212    1.1890    1.7781    1.5016    2.5304
      1.6314    1.9332    3.0705    3.1504    1.6742    1.6165    1.9002    2.6586
      2.4196    1.8676    3.1096    2.8967    1.8394    1.5457    2.1770    2.8748
      1.5711    1.2857    2.3111    2.0477    1.2860    1.5280    1.8363    2.0944
      1.6658    1.2913    2.6872    2.7799    1.4419    1.5451    1.3041    2.4699
   
   D = 
   
      1.0095    0.9953    1.9035    2.0276    1.2237    1.2707    1.2033    1.8915
      1.5522    1.3716    2.1607    2.2808    1.3376    1.4833    1.4868    2.5449
      2.7422    2.0427    3.7996    3.8740    2.2908    2.4208    2.4971    3.9616
      1.5388    1.2919    2.5499    2.4212    1.1890    1.7781    1.5016    2.5304
      1.6314    1.9332    3.0705    3.1504    1.6742    1.6165    1.9002    2.6586
      2.4196    1.8676    3.1096    2.8967    1.8394    1.5457    2.1770    2.8748
      1.5711    1.2857    2.3111    2.0477    1.2860    1.5280    1.8363    2.0944
      1.6658    1.2913    2.6872    2.7799    1.4419    1.5451    1.3041    2.4699
   


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

   
      0.1438    0.4656    0.1945    0.7502    0.6096    0.3203
      0.9986    0.6040    0.6985    0.0553    0.5933    0.5230
      0.7635    0.1516    0.1128    0.0134    0.1922    0.4224
      0.2529    0.0805    0.8865    0.4667    0.2431    0.2625
      0.1389    0.0324    0.3298    0.0130    0.8688    0.8197
   
   
      0.9986
      0.7635
      0.6040
      0.6985
      0.8865
      0.7502
      0.6096
      0.5933
      0.8688
      0.5230
      0.8197
   

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

   
      8.4369    1.6633    4.8067    5.4284    0.3276    6.8261
      5.4433    9.7328    8.6641    6.8911    8.0314    3.5433
      4.2027    6.3079    0.2600    8.9287    7.1208    0.4859
      4.5424    5.5252    0.5421    3.9245    9.8627    9.4877
      3.3875    3.3940    5.0588    4.8861    2.8860    8.8445
   
   
      8.4369    0.0000    0.0000    5.4284    0.0000    6.8261
      5.4433    9.7328    8.6641    6.8911    8.0314    0.0000
      0.0000    6.3079    0.0000    8.9287    7.1208    0.0000
      0.0000    5.5252    0.0000    0.0000    9.8627    9.4877
      0.0000    0.0000    5.0588    0.0000    0.0000    8.8445
   
   
      8.4369    0.0000    0.0000    5.4284    0.0000    6.8261
      5.4433       NaN    8.6641    6.8911    8.0314    0.0000
      0.0000    6.3079    0.0000    8.9287    7.1208    0.0000
      0.0000    5.5252    0.0000    0.0000       NaN       NaN
      0.0000    0.0000    5.0588    0.0000    0.0000    8.8445
   

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

   
      6.5000    4.8354    2.5430    3.0803    8.8551    9.6424
      9.8714    0.7142    3.1951    1.6121    6.5000    0.6722
      2.9359    3.4686    2.2435    0.3970    3.0298    4.7948
      6.5000    6.5000    2.0213    9.2153    6.5000    4.1850
      6.5000    2.0445    6.5000    6.5000    1.0450    0.8294
   
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
   
