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
      0.0860    0.3553    0.0635    0.6955
   
   R1[2] = 0.06352737472985492
   C1 = 
      0.3972
      0.9435
      0.3733
      0.7061
      0.4480
      0.2645
      0.8760
      0.0319
   
   C1[5] = 0.2644851890547262

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
      0.8668    0.8180    0.4488    0.7492    0.5916
      0.9964    0.9249    0.3222    0.6961    0.2116
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.8483    0.7484    0.3672    0.7027    0.7133    0.2297    0.1148    0.4842
      0.1928    0.3737    0.8928    0.2544    0.6295    0.8508    0.9972    0.2381
      0.8776    0.4096    0.2605    0.0600    0.8341    0.3975    0.7644    0.4374
      0.0648    0.5732    0.4251    0.2940    0.1718    0.9154    0.9413    0.8832
      0.9901    0.6683    0.6220    0.3755    0.9916    0.7214    0.6066    0.4533
      0.5460    0.7575    0.9488    0.0546    0.1220    0.2813    0.9458    0.9643
      0.5458    0.8809    0.3034    0.8303    0.6728    0.2857    0.7068    0.3088
      0.1652    0.0654    0.3224    0.9823    0.8238    0.3125    0.0522    0.6979
   
   B = 
   
      0.4761    0.2921    0.2129    0.5806    0.0871    0.4189    0.1487    0.5800
      0.7934    0.0853    0.4110    0.8156    0.0044    0.7002    0.5793    0.3396
      0.3868    0.3545    0.8059    0.4758    0.0734    0.7408    0.9650    0.2276
      0.1802    0.0173    0.8538    0.5909    0.0494    0.3424    0.7789    0.3115
      0.0442    0.9659    0.2099    0.8046    0.0263    0.7563    0.5059    0.4510
      0.2998    0.4505    0.2502    0.7590    0.3442    0.7433    0.7957    0.5710
      0.9557    0.9539    0.8724    0.8328    0.5532    0.3190    0.7024    0.2512
      0.2814    0.1063    0.2223    0.9807    0.9740    0.9299    0.2511    0.3592
   
   C = 
   
      1.6126    1.4074    1.7991    3.0115    0.7719    2.5890    2.2073    1.7043
      2.0822    2.3769    2.3992    3.2079    1.1895    2.7388    3.0605    1.6268
      1.8639    2.1450    1.6550    3.0413    1.1081    2.4447    2.0509    1.6783
      2.1332    1.7937    2.1256    3.3642    1.7545    2.7759    2.6791    1.5744
      2.2771    2.4828    2.3260    3.9328    1.2049    3.3732    3.0427    2.2340
      2.5028    1.8107    2.3744    3.4636    1.6858    2.9805    2.6703    1.6065
      2.1034    1.8421    2.3297    3.3200    0.9226    2.5883    2.6728    1.6985
      0.8086    1.2456    1.6124    2.5109    0.9248    2.2111    2.0161    1.3112
   
   D = 
   
      1.6126    1.4074    1.7991    3.0115    0.7719    2.5890    2.2073    1.7043
      2.0822    2.3769    2.3992    3.2079    1.1895    2.7388    3.0605    1.6268
      1.8639    2.1450    1.6550    3.0413    1.1081    2.4447    2.0509    1.6783
      2.1332    1.7937    2.1256    3.3642    1.7545    2.7759    2.6791    1.5744
      2.2771    2.4828    2.3260    3.9328    1.2049    3.3732    3.0427    2.2340
      2.5028    1.8107    2.3744    3.4636    1.6858    2.9805    2.6703    1.6065
      2.1034    1.8421    2.3297    3.3200    0.9226    2.5883    2.6728    1.6985
      0.8086    1.2456    1.6124    2.5109    0.9248    2.2111    2.0161    1.3112
   


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

   
      0.5843    0.0597    0.3843    0.8522    0.2884    0.1476
      0.7504    0.9060    0.6129    0.3659    0.5676    0.4917
      0.7225    0.3848    0.9059    0.9203    0.2556    0.5717
      0.6891    0.4966    0.0195    0.7244    0.2745    0.1578
      0.4364    0.9287    0.9262    0.7816    0.5482    0.7534
   
   
      0.5843
      0.7504
      0.7225
      0.6891
      0.9060
      0.9287
      0.6129
      0.9059
      0.9262
      0.8522
      0.9203
      0.7244
      0.7816
      0.5676
      0.5482
      0.5717
      0.7534
   

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

   
      9.0223    5.1096    6.6799    0.4089    5.8326    9.9863
      9.3809    4.0634    7.5318    3.9998    8.4905    5.7666
      0.2186    4.8254    0.5585    1.7644    0.0064    7.9907
      3.5087    8.4400    3.3734    4.3563    8.0139    0.8906
      3.8270    1.2915    9.3887    8.3550    0.1873    9.0042
   
   
      9.0223    5.1096    6.6799    0.0000    5.8326    9.9863
      9.3809    0.0000    7.5318    0.0000    8.4905    5.7666
      0.0000    0.0000    0.0000    0.0000    0.0000    7.9907
      0.0000    8.4400    0.0000    0.0000    8.0139    0.0000
      0.0000    0.0000    9.3887    8.3550    0.0000    9.0042
   
   
         NaN    5.1096    6.6799    0.0000    5.8326       NaN
         NaN    0.0000    7.5318    0.0000    8.4905    5.7666
      0.0000    0.0000    0.0000    0.0000    0.0000    7.9907
      0.0000    8.4400    0.0000    0.0000    8.0139    0.0000
      0.0000    0.0000       NaN    8.3550    0.0000       NaN
   

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

   
      6.5000    3.1164    2.2474    6.5000    4.3499    1.3460
      4.3368    8.9840    6.5000    4.8888    3.7045    6.5000
      6.5000    4.4082    4.7789    6.5000    9.8985    6.5000
      6.5000    6.5000    2.7230    4.2238    2.3344    4.0686
      1.5799    6.5000    6.5000    6.5000    8.2568    1.6655
   
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
   
