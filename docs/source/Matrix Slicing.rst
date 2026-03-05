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
      0.0149    0.3142    0.8595    0.9133
   
   R1[2] = 0.8595376800980746
   C1 = 
      0.8566
      0.0523
      0.1222
      0.0667
      0.9971
      0.7867
      0.1634
      0.0778
   
   C1[5] = 0.7866731677416353

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
      0.2361    0.1245    0.5070    0.2874    0.0597
      0.9518    0.5932    0.3708    0.1080    0.5780
   

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
   
      0.1456    0.8999    0.9854    0.6758    0.3484    0.5334    0.3860    0.6120
      0.0194    0.1847    0.7037    0.6370    0.1264    0.9127    0.1621    0.5947
      0.8034    0.8086    0.9499    0.3056    0.1596    0.7117    0.2944    0.0208
      0.4905    0.9932    0.9030    0.9397    0.2703    0.1681    0.8135    0.5168
      0.9153    0.1352    0.9950    0.4911    0.3445    0.6831    0.0188    0.7077
      0.8525    0.8724    0.3483    0.6564    0.7390    0.7184    0.4929    0.1360
      0.9670    0.4940    0.5203    0.4372    0.9879    0.0555    0.6314    0.4768
      0.4488    0.2285    0.0870    0.2375    0.2360    0.0163    0.9550    0.1654
   
   B = 
   
      0.9084    0.9426    0.2832    0.5752    0.2683    0.1563    0.5991    0.9668
      0.8804    0.4281    0.6854    0.7740    0.2358    0.7883    0.7248    0.6490
      0.1702    0.7832    0.4128    0.0214    0.6883    0.2242    0.9657    0.8002
      0.7928    0.1070    0.5426    0.2383    0.8226    0.6658    0.9125    0.4101
      0.4358    0.4974    0.5813    0.1534    0.2591    0.5183    0.4062    0.5250
      0.9915    0.5068    0.2390    0.2420    0.9546    0.6111    0.6615    0.0787
      0.8777    0.5445    0.2717    0.0631    0.5227    0.9263    0.1141    0.4129
      0.6936    0.4013    0.4803    0.7795    0.3774    0.9246    0.7639    0.9357
   
   C = 
   
      3.0720    2.2659    2.1603    1.6463    2.5175    2.8330    3.3138    2.7473
      2.3199    1.5691    1.3895    1.0350    2.2704    2.0539    2.5344    1.7246
      2.8937    2.4888    1.6925    1.4126    2.1939    1.9891    2.8489    2.4678
      3.5757    2.5652    2.3688    1.8304    2.6111    3.1620    3.4520    3.2013
      2.8441    2.5642    1.7376    1.5404    2.3846    2.0675    3.1901    2.8748
      3.6836    2.5747    2.1398    1.7539    2.4003    2.7405    3.0141    2.7136
      3.1191    2.6318    2.0525    1.6304    1.9126    2.5201    2.7132    3.0810
      1.8841    1.3265    0.9285    0.7229    1.0679    1.5979    1.0773    1.4235
   
   D = 
   
      3.0720    2.2659    2.1603    1.6463    2.5175    2.8330    3.3138    2.7473
      2.3199    1.5691    1.3895    1.0350    2.2704    2.0539    2.5344    1.7246
      2.8937    2.4888    1.6925    1.4126    2.1939    1.9891    2.8489    2.4678
      3.5757    2.5652    2.3688    1.8304    2.6111    3.1620    3.4520    3.2013
      2.8441    2.5642    1.7376    1.5404    2.3846    2.0675    3.1901    2.8748
      3.6836    2.5747    2.1398    1.7539    2.4003    2.7405    3.0141    2.7136
      3.1191    2.6318    2.0525    1.6304    1.9126    2.5201    2.7132    3.0810
      1.8841    1.3265    0.9285    0.7229    1.0679    1.5979    1.0773    1.4235
   


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

   
      0.8696    0.4189    0.4946    0.1271    0.5070    0.5324
      0.5564    0.4792    0.0932    0.2837    0.6378    0.8020
      0.9137    0.3495    0.4961    0.5490    0.7342    0.5178
      0.4429    0.7478    0.2227    0.7539    0.4003    0.4542
      0.0585    0.5715    0.5896    0.8444    0.7209    0.7812
   
   
      0.8696
      0.5564
      0.9137
      0.7478
      0.5715
      0.5896
      0.5490
      0.7539
      0.8444
      0.5070
      0.6378
      0.7342
      0.7209
      0.5324
      0.8020
      0.5178
      0.7812
   

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

   
      8.1333    3.3906    1.3579    6.7551    9.6465    1.4270
      5.5793    7.9413    4.8032    5.9051    3.3899    7.4813
      5.8922    1.8912    2.5372    4.2874    1.2857    6.9217
      7.6733    6.9039    9.5850    3.7758    1.4910    9.2178
      6.2528    8.3965    1.7362    7.2600    9.4997    9.0621
   
   
      8.1333    0.0000    0.0000    6.7551    9.6465    0.0000
      5.5793    7.9413    0.0000    5.9051    0.0000    7.4813
      5.8922    0.0000    0.0000    0.0000    0.0000    6.9217
      7.6733    6.9039    9.5850    0.0000    0.0000    9.2178
      6.2528    8.3965    0.0000    7.2600    9.4997    9.0621
   
   
      8.1333    0.0000    0.0000    6.7551       NaN    0.0000
      5.5793    7.9413    0.0000    5.9051    0.0000    7.4813
      5.8922    0.0000    0.0000    0.0000    0.0000    6.9217
      7.6733    6.9039       NaN    0.0000    0.0000       NaN
      6.2528    8.3965    0.0000    7.2600       NaN       NaN
   

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

   
      8.6489    9.1970    6.5000    6.5000    8.9641    1.4889
      6.5000    8.3935    6.5000    9.5938    8.2770    1.2555
      0.3661    6.5000    6.5000    6.5000    0.6228    6.5000
      8.1809    6.5000    1.4706    0.4568    8.3305    4.3790
      8.5989    1.8986    4.8346    3.9517    1.5461    0.0972
   
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
   
