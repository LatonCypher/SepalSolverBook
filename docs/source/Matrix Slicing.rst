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
      0.1080    0.3500    0.3234    0.3715
   
   R1[2] = 0.3234101599585887
   C1 = 
      0.1644
      0.2009
      0.1801
      0.4868
      0.4337
      0.0534
      0.2877
      0.1062
   
   C1[5] = 0.05336893319303471

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
      0.7616    0.8374    0.4426    0.2209    0.5294
      0.1630    0.4014    0.5275    0.7541    0.5841
   

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
   
      0.7207    0.7117    0.9693    0.2884    0.7072    0.8005    0.1334    0.5219
      0.3542    0.1837    0.3237    0.6855    0.0186    0.1061    0.7697    0.3230
      0.7407    0.2236    0.4014    0.0734    0.7545    0.1520    0.2004    0.0852
      0.9231    0.2369    0.4828    0.8236    0.6294    0.1365    0.2607    0.7286
      0.9004    0.2326    0.1079    0.4171    0.9653    0.5032    0.5004    0.8478
      0.8175    0.7015    0.2690    0.1423    0.3397    0.5406    0.8332    0.3389
      0.0585    0.4535    0.9670    0.4273    0.7993    0.3488    0.5717    0.3627
      0.2302    0.0702    0.3630    0.4542    0.1131    0.5079    0.8381    0.3881
   
   B = 
   
      0.7480    0.7573    0.5444    0.4013    0.3280    0.6615    0.6703    0.6301
      0.1407    0.4929    0.9441    0.0797    0.4109    0.0204    0.0054    0.1296
      0.5521    0.9363    0.7611    0.9511    0.1719    0.4046    0.2987    0.6885
      0.3508    0.0072    0.5986    0.1762    0.9112    0.3210    0.7938    0.5037
      0.5107    0.2274    0.7759    0.1898    0.9955    0.7704    0.9865    0.1325
      0.9332    0.8233    0.6209    0.9620    0.9770    0.3851    0.3760    0.3598
      0.3167    0.7282    0.5846    0.9655    0.6432    0.2274    0.4869    0.9415
      0.5037    0.5672    0.4906    0.7953    0.0680    0.1213    0.5670    0.9742
   
   C = 
   
      2.6887    3.0191    3.3544    2.7668    2.5656    1.9227    2.3648    2.3747
      1.2249    1.5020    1.7117    1.6911    1.5111    0.8584    1.4954    1.8952
      1.4663    1.5384    1.8025    1.2604    1.5050    1.3762    1.6232    1.2354
      2.1776    2.1324    2.6696    2.0757    2.2108    1.7603    2.6303    2.4474
      2.4601    2.3796    2.8115    2.3808    2.6216    1.9319    2.8339    2.4879
      2.0211    2.5390    2.6499    2.3236    2.1576    1.4101    1.8813    2.2171
      1.8886    2.2672    2.8007    2.3823    2.2899    1.5004    2.0733    2.0996
      1.5344    1.8264    1.8233    2.1513    1.7549    0.9667    1.5544    1.9978
   
   D = 
   
      2.6887    3.0191    3.3544    2.7668    2.5656    1.9227    2.3648    2.3747
      1.2249    1.5020    1.7117    1.6911    1.5111    0.8584    1.4954    1.8952
      1.4663    1.5384    1.8025    1.2604    1.5050    1.3762    1.6232    1.2354
      2.1776    2.1324    2.6696    2.0757    2.2108    1.7603    2.6303    2.4474
      2.4601    2.3796    2.8115    2.3808    2.6216    1.9319    2.8339    2.4879
      2.0211    2.5390    2.6499    2.3236    2.1576    1.4101    1.8813    2.2171
      1.8886    2.2672    2.8007    2.3823    2.2899    1.5004    2.0733    2.0996
      1.5344    1.8264    1.8233    2.1513    1.7549    0.9667    1.5544    1.9978
   


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

   
      0.6467    0.4901    0.6622    0.1791    0.5240    0.3554
      0.4031    0.1564    0.7592    0.1899    0.2113    0.9850
      0.7522    0.2198    0.9831    0.5252    0.9150    0.0175
      0.6276    0.7020    0.1507    0.6718    0.4907    0.0610
      0.4652    0.8747    0.3420    0.6940    0.1464    0.9451
   
   
      0.6467
      0.7522
      0.6276
      0.7020
      0.8747
      0.6622
      0.7592
      0.9831
      0.5252
      0.6718
      0.6940
      0.5240
      0.9150
      0.9850
      0.9451
   

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

   
      5.2291    2.1041    3.5579    2.9348    5.6986    9.0333
      5.5214    8.3459    2.4603    5.1635    5.2560    9.9637
      8.6516    2.4375    7.2419    3.4865    2.3138    4.1215
      3.8207    5.0344    3.9437    5.2912    0.3711    8.2828
      3.0489    4.3891    6.5263    8.6726    0.3945    0.1574
   
   
      5.2291    0.0000    0.0000    0.0000    5.6986    9.0333
      5.5214    8.3459    0.0000    5.1635    5.2560    9.9637
      8.6516    0.0000    7.2419    0.0000    0.0000    0.0000
      0.0000    5.0344    0.0000    5.2912    0.0000    8.2828
      0.0000    0.0000    6.5263    8.6726    0.0000    0.0000
   
   
      5.2291    0.0000    0.0000    0.0000    5.6986       NaN
      5.5214    8.3459    0.0000    5.1635    5.2560       NaN
      8.6516    0.0000    7.2419    0.0000    0.0000    0.0000
      0.0000    5.0344    0.0000    5.2912    0.0000    8.2828
      0.0000    0.0000    6.5263    8.6726    0.0000    0.0000
   

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

   
      9.7892    6.5000    2.4225    3.5137    6.5000    3.2666
      2.5548    3.8786    6.5000    6.5000    6.5000    9.7438
      8.7890    4.9128    6.5000    3.0709    3.8787    6.5000
      6.5000    6.5000    4.9923    4.9669    4.5578    6.5000
      3.2438    6.5000    9.7667    2.0303    8.3323    6.5000
   
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
   
