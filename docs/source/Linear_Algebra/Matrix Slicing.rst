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
      0.6652    0.1607    0.9110    0.0108
   
   R1[2] = 0.9109893386146012
   C1 = 
      0.1090
      0.2016
      0.9138
      0.7131
      0.8132
      0.7034
      0.2622
      0.9179
   
   C1[5] = 0.7034076854360759

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
   A[2..5] = new double[] { 10, 15, 20 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a row
   A[1, 2..4] = new double[] { 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set multiple elements using subscript along a col
   A[0..3, 3] = new double[] { 100, 150, 200 };
   Console.WriteLine($"A = {A}");

   //  set submatrix elements
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
      0.5097    0.7849    0.8110    0.2480    0.9495
      0.8598    0.3106    0.6198    0.3393    0.9733
   

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
   
      0.3773    0.8183    0.9033    0.5011    0.3067    0.3546    0.8393    0.6487
      0.7244    0.2942    0.0620    0.8626    0.1979    0.4413    0.8396    0.6724
      0.1773    0.5979    0.0276    0.8229    0.6880    0.6106    0.5053    0.0837
      0.4889    0.2915    0.9271    0.0489    0.9243    0.2336    0.4347    0.6832
      0.3083    0.2454    0.6030    0.1012    0.9311    0.9057    0.3336    0.8133
      0.0282    0.8713    0.6040    0.2911    0.1844    0.5512    0.4390    0.7800
      0.4980    0.3581    0.8006    0.2723    0.0370    0.7490    0.4617    0.7979
      0.9074    0.8147    0.5202    0.0720    0.9355    0.5634    0.9681    0.7482
   
   B = 
   
      0.2164    0.5773    0.5394    0.1398    0.1841    0.9077    0.0597    0.8436
      0.6405    0.5689    0.6154    0.0970    0.1845    0.6727    0.9479    0.2905
      0.8427    0.6585    0.6131    0.8292    0.0165    0.5010    0.6215    0.6179
      0.0921    0.0968    0.7048    0.8948    0.6023    0.3977    0.2421    0.7434
      0.9868    0.1028    0.3203    0.4350    0.9099    0.8459    0.0711    0.7567
      0.5107    0.5755    0.8415    0.5818    0.3143    0.2780    0.2967    0.0508
      0.8443    0.7711    0.9999    0.8384    0.4501    0.9574    0.8005    0.2745
      0.8281    0.4575    0.9523    0.6018    0.8533    0.8079    0.4237    0.6978
   
   C = 
   
      3.1426    2.5062    3.4677    2.7633    1.8589    3.2305    2.5547    2.4197
      2.1631    1.9392    3.1322    2.4045    1.9786    2.8668    1.6715    2.2479
      2.0070    1.3904    2.3795    1.9706    1.7558    2.2074    1.4637    1.7008
      3.0423    1.9405    2.6239    2.2227    1.8816    2.9388    1.6660    2.4136
      3.0778    1.9708    2.9268    2.3586    2.1489    2.8032    1.5969    2.1888
      2.5800    1.9694    2.8316    2.0882    1.5555    2.3898    2.1320    1.6988
      2.5065    2.2007    3.0354    2.3311    1.4927    2.5286    1.8652    1.9708
      3.8109    2.8460    3.8147    2.6983    2.4717    4.1403    2.4928    2.9014
   
   D = 
   
      3.1426    2.5062    3.4677    2.7633    1.8589    3.2305    2.5547    2.4197
      2.1631    1.9392    3.1322    2.4045    1.9786    2.8668    1.6715    2.2479
      2.0070    1.3904    2.3795    1.9706    1.7558    2.2074    1.4637    1.7008
      3.0423    1.9405    2.6239    2.2227    1.8816    2.9388    1.6660    2.4136
      3.0778    1.9708    2.9268    2.3586    2.1489    2.8032    1.5969    2.1888
      2.5800    1.9694    2.8316    2.0882    1.5555    2.3898    2.1320    1.6988
      2.5065    2.2007    3.0354    2.3311    1.4927    2.5286    1.8652    1.9708
      3.8109    2.8460    3.8147    2.6983    2.4717    4.1403    2.4928    2.9014
   


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

   
      0.0107    0.8997    0.4787    0.2310    0.1517    0.4215
      0.0401    0.2287    0.1109    0.7768    0.3852    0.0113
      0.2293    0.3019    0.4644    0.4082    0.4763    0.5214
      0.6013    0.6580    0.5852    0.3110    0.8082    0.5637
      0.4818    0.4477    0.3899    0.6048    0.1115    0.0044
   
   
      0.6013
      0.8997
      0.6580
      0.5852
      0.7768
      0.6048
      0.8082
      0.5214
      0.5637
   

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

   
      9.1582    6.4643    8.1891    8.8074    2.1823    9.0569
      3.3905    8.1390    9.2136    6.6682    2.6658    0.7853
      9.4479    2.9234    3.7176    8.0683    7.1752    0.2040
      8.7094    2.7429    3.6308    2.0164    0.0122    2.9282
      3.6707    9.8529    0.3797    3.8302    2.1106    5.7930
   
   
      9.1582    6.4643    8.1891    8.8074    0.0000    9.0569
      0.0000    8.1390    9.2136    6.6682    0.0000    0.0000
      9.4479    0.0000    0.0000    8.0683    7.1752    0.0000
      8.7094    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    9.8529    0.0000    0.0000    0.0000    5.7930
   
   
         NaN    6.4643    8.1891    8.8074    0.0000       NaN
      0.0000    8.1390       NaN    6.6682    0.0000    0.0000
         NaN    0.0000    0.0000    8.0683    7.1752    0.0000
      8.7094    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    0.0000    0.0000    0.0000    5.7930
   

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

   
      3.0157    3.6398    8.9031    3.9776    6.5000    3.7460
      2.4358    4.2488    6.5000    0.5489    6.5000    4.8985
      4.8436    8.6048    4.7250    6.5000    3.7860    0.0966
      1.8885    6.5000    8.1587    0.9568    0.8816    3.2209
      1.6846    4.0147    1.3334    2.3255    2.5283    0.0685
   
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
   
