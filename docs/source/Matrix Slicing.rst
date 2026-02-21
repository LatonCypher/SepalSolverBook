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
      0.4165    0.7130    0.2107    0.0073
   
   R1[2] = 0.2106916891535786
   C1 = 
      0.9847
      0.5237
      0.9664
      0.8993
      0.5530
      0.9881
      0.1006
      0.4700
   
   C1[5] = 0.9880933162132545

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
      0.7122    0.1958    0.1191    0.9578    0.8391
      0.0399    0.1319    0.5220    0.9787    0.2912
   

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
   
      0.3917    0.5102    0.1069    0.6519    0.0438    0.5035    0.2189    0.6870
      0.8233    0.1486    0.6479    0.0434    0.1583    0.7938    0.2633    0.1785
      0.2872    0.6856    0.0172    0.2455    0.9715    0.0602    0.6064    0.9669
      0.3570    0.5150    0.1805    0.3530    0.6147    0.7020    0.8237    0.7798
      0.0371    0.5011    0.1987    0.6235    0.6451    0.5832    0.5037    0.0966
      0.3074    0.0099    0.2053    0.7800    0.6001    0.7384    0.1162    0.4444
      0.0923    0.8816    0.3919    0.1013    0.2067    0.3496    0.9696    0.9682
      0.4007    0.7216    0.3965    0.7440    0.5413    0.6653    0.7381    0.3864
   
   B = 
   
      0.1808    0.6827    0.4887    0.4636    0.6626    0.8014    0.7830    0.7064
      0.0563    0.2898    0.2550    0.1580    0.4594    0.5030    0.0865    0.7432
      0.9971    0.4684    0.9210    0.5834    0.5237    0.2835    0.9294    0.5422
      0.1866    0.7989    0.6130    0.5083    0.7358    0.0580    0.1791    0.8934
      0.4397    0.0513    0.1055    0.4429    0.0038    0.4171    0.7523    0.9719
      0.8599    0.1531    0.0269    0.8176    0.9455    0.1429    0.0498    0.4904
      0.9787    0.7944    0.3813    0.4797    0.0625    0.0801    0.5806    0.9707
      0.5963    0.4324    0.7093    0.7418    0.6305    0.1746    0.8079    0.9169
   
   C = 
   
      1.4037    1.5364    1.4085    1.7015    1.9526    0.8663    1.3070    2.4279
      1.9277    1.3593    1.3287    1.7831    1.8652    1.1524    1.7232    2.0445
      1.8024    1.5578    1.5027    1.8639    1.4031    1.2253    2.2112    3.3898
      2.4845    1.8901    1.6394    2.3515    2.0368    1.1756    2.1608    3.5044
      1.6851    1.3260    1.0554    1.6051    1.4639    0.7839    1.2536    2.5538
      1.6840    1.3604    1.2627    1.9153    1.8776    0.7975    1.4869    2.4984
      2.3937    1.8361    1.7806    2.0229    1.7484    1.0173    2.0489    3.2244
      2.4102    2.1458    1.8316    2.3337    2.2730    1.2870    2.0591    3.6221
   
   D = 
   
      1.4037    1.5364    1.4085    1.7015    1.9526    0.8663    1.3070    2.4279
      1.9277    1.3593    1.3287    1.7831    1.8652    1.1524    1.7232    2.0445
      1.8024    1.5578    1.5027    1.8639    1.4031    1.2253    2.2112    3.3898
      2.4845    1.8901    1.6394    2.3515    2.0368    1.1756    2.1608    3.5044
      1.6851    1.3260    1.0554    1.6051    1.4639    0.7839    1.2536    2.5538
      1.6840    1.3604    1.2627    1.9153    1.8776    0.7975    1.4869    2.4984
      2.3937    1.8361    1.7806    2.0229    1.7484    1.0173    2.0489    3.2244
      2.4102    2.1458    1.8316    2.3337    2.2730    1.2870    2.0591    3.6221
   


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

   
      0.7015    0.0872    0.8000    0.9729    0.2977    0.7714
      0.4217    0.8096    0.7146    0.9629    0.1759    0.4539
      0.2402    0.8173    0.0454    0.6482    0.0513    0.6511
      0.2570    0.8604    0.2949    0.1451    0.5914    0.8937
      0.5276    0.7120    0.1699    0.4164    0.0481    0.3672
   
   
      0.7015
      0.5276
      0.8096
      0.8173
      0.8604
      0.7120
      0.8000
      0.7146
      0.9729
      0.9629
      0.6482
      0.5914
      0.7714
      0.6511
      0.8937
   

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

   
      5.1811    1.3569    0.7846    7.8295    9.2026    1.7323
      1.2176    2.6752    1.3778    9.5503    8.9997    3.5924
      7.7405    5.7130    1.8791    0.9683    0.8674    6.4009
      2.2715    8.6283    7.9630    4.0396    1.6589    5.2740
      3.2509    1.2361    9.2922    6.1426    6.1225    3.7217
   
   
      5.1811    0.0000    0.0000    7.8295    9.2026    0.0000
      0.0000    0.0000    0.0000    9.5503    8.9997    0.0000
      7.7405    5.7130    0.0000    0.0000    0.0000    6.4009
      0.0000    8.6283    7.9630    0.0000    0.0000    5.2740
      0.0000    0.0000    9.2922    6.1426    6.1225    0.0000
   
   
      5.1811    0.0000    0.0000    7.8295       NaN    0.0000
      0.0000    0.0000    0.0000       NaN    8.9997    0.0000
      7.7405    5.7130    0.0000    0.0000    0.0000    6.4009
      0.0000    8.6283    7.9630    0.0000    0.0000    5.2740
      0.0000    0.0000       NaN    6.1426    6.1225    0.0000
   

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

   
      6.5000    1.4535    6.5000    6.5000    6.5000    6.5000
      6.5000    6.5000    0.4531    6.5000    6.5000    6.5000
      9.9156    0.6636    6.5000    4.7644    4.5064    8.7237
      6.5000    3.4766    6.5000    1.9373    4.9879    6.5000
      8.6853    2.9370    6.5000    6.5000    9.2845    4.9148
   
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
   
