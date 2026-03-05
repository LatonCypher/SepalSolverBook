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
      0.3934    0.8655    0.1415    0.9226
   
   R1[2] = 0.1414788664200083
   C1 = 
      0.8349
      0.3518
      0.0817
      0.2421
      0.6608
      0.3174
      0.2794
      0.5752
   
   C1[5] = 0.3173619616314236

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
      0.8909    0.1638    0.4403    0.7610    0.1189
      0.8956    0.8747    0.8731    0.2488    0.8314
   

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
   
      0.3791    0.8209    0.2385    0.0531    0.9553    0.9561    0.4328    0.5223
      0.6724    0.6523    0.1857    0.0019    0.9843    0.0183    0.3823    0.8623
      0.2293    0.4889    0.7435    0.5289    0.9135    0.0098    0.1809    0.0020
      0.6352    0.7606    0.0322    0.6651    0.9084    0.3790    0.0282    0.1423
      0.2348    0.6490    0.5426    0.9374    0.7260    0.0689    0.3152    0.9369
      0.1233    0.9276    0.4381    0.6093    0.6021    0.8020    0.9667    0.4548
      0.3053    0.4175    0.1950    0.4716    0.5500    0.1394    0.7255    0.8584
      0.1678    0.7879    0.4865    0.5510    0.2517    0.4850    0.4496    0.1018
   
   B = 
   
      0.1117    0.2044    0.7184    0.0404    0.8086    0.8019    0.9797    0.6316
      0.3645    0.1080    0.6866    0.4105    0.2964    0.0369    0.6231    0.8002
      0.4608    0.9125    0.7608    0.0470    0.3891    0.3731    0.6604    0.4599
      0.4655    0.4338    0.1212    0.2010    0.6363    0.5142    0.8025    0.9462
      0.2747    0.5193    0.0091    0.1706    0.4984    0.7581    0.1944    0.0145
      0.6334    0.4960    0.2423    0.9215    0.4740    0.3395    0.0378    0.0569
      0.8181    0.7482    0.7333    0.7702    0.7222    0.6443    0.0104    0.2794
      0.8999    0.6922    0.1087    0.7962    0.9865    0.6829    0.6262    0.2469
   
   C = 
   
      2.1683    2.0625    1.6385    2.1673    2.4336    2.1350    1.6365    1.3744
      1.7701    1.7813    1.4600    1.4697    2.4366    2.2212    1.9254    1.3689
      1.1995    1.6234    1.2737    0.6569    1.5487    1.5650    1.6258    1.4432
      1.3134    1.3093    1.2200    1.1123    1.9680    1.8242    1.9316    1.7317
      2.2932    2.3153    1.4970    1.6658    2.7362    2.3133    2.4788    2.1379
      2.7109    2.5380    2.0908    2.4771    2.7599    2.2720    1.9194    2.0349
      2.1010    1.9817    1.3756    1.7521    2.4575    2.0934    1.7238    1.4933
      1.6223    1.5805    1.5590    1.3809    1.6895    1.3430    1.5545    1.6635
   
   D = 
   
      2.1683    2.0625    1.6385    2.1673    2.4336    2.1350    1.6365    1.3744
      1.7701    1.7813    1.4600    1.4697    2.4366    2.2212    1.9254    1.3689
      1.1995    1.6234    1.2737    0.6569    1.5487    1.5650    1.6258    1.4432
      1.3134    1.3093    1.2200    1.1123    1.9680    1.8242    1.9316    1.7317
      2.2932    2.3153    1.4970    1.6658    2.7362    2.3133    2.4788    2.1379
      2.7109    2.5380    2.0908    2.4771    2.7599    2.2720    1.9194    2.0349
      2.1010    1.9817    1.3756    1.7521    2.4575    2.0934    1.7238    1.4933
      1.6223    1.5805    1.5590    1.3809    1.6895    1.3430    1.5545    1.6635
   


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

   
      0.7702    0.7179    0.6877    0.5279    0.2681    0.4423
      0.2253    0.2711    0.0632    0.0797    0.8463    0.9823
      0.6583    0.3659    0.5594    0.9655    0.4440    0.7236
      0.6530    0.0437    0.9399    0.8764    0.1338    0.7492
      0.0195    0.8210    0.2247    0.2352    0.6173    0.6439
   
   
      0.7702
      0.6583
      0.6530
      0.7179
      0.8210
      0.6877
      0.5594
      0.9399
      0.5279
      0.9655
      0.8764
      0.8463
      0.6173
      0.9823
      0.7236
      0.7492
      0.6439
   

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

   
      0.6148    6.6544    9.2418    0.0589    1.4031    1.2778
      1.8199    1.2172    1.8267    7.9239    8.5653    2.1042
      2.1159    1.2773    9.6766    9.1129    8.0727    2.5442
      8.8310    3.6040    6.0634    3.3232    8.2336    9.3682
      4.5486    7.7166    5.0703    1.9087    4.6744    5.1742
   
   
      0.0000    6.6544    9.2418    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.9239    8.5653    0.0000
      0.0000    0.0000    9.6766    9.1129    8.0727    0.0000
      8.8310    0.0000    6.0634    0.0000    8.2336    9.3682
      0.0000    7.7166    5.0703    0.0000    0.0000    5.1742
   
   
      0.0000    6.6544       NaN    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.9239    8.5653    0.0000
      0.0000    0.0000       NaN       NaN    8.0727    0.0000
      8.8310    0.0000    6.0634    0.0000    8.2336       NaN
      0.0000    7.7166    5.0703    0.0000    0.0000    5.1742
   

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

   
      8.7524    6.5000    6.5000    0.1971    4.3033    6.5000
      6.5000    4.7531    6.5000    9.4946    6.5000    0.2191
      6.5000    1.4452    3.8430    0.9169    9.7804    6.5000
      4.4836    8.3818    8.6669    6.5000    6.5000    3.8783
      8.2409    1.5227    8.4597    6.5000    4.3921    8.9591
   
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
   
