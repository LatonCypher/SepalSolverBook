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
      0.7278    0.7634    0.7403    0.8216
   
   R1[2] = 0.7402876773074181
   C1 = 
      0.8493
      0.7295
      0.9678
      0.6198
      0.9231
      0.7746
      0.1944
      0.8565
   
   C1[5] = 0.7745873304056112

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
      0.6454    0.9535    0.3143    0.5107    0.6204
      0.3258    0.0360    0.4416    0.3796    0.7079
   

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
   
      0.4329    0.2305    0.7978    0.9544    0.7849    0.5750    0.0836    0.9027
      0.0225    0.3695    0.0617    0.5911    0.8018    0.7719    0.6343    0.0787
      0.3089    0.5338    0.6875    0.9020    0.1516    0.1758    0.4375    0.7519
      0.8594    0.7688    0.9607    0.8015    0.6264    0.2084    0.0201    0.4066
      0.3468    0.7607    0.0659    0.8101    0.3760    0.2175    0.6506    0.1289
      0.1509    0.5389    0.6427    0.9702    0.3192    0.5840    0.1168    0.7072
      0.6400    0.4252    0.6437    0.3884    0.8125    0.5761    0.6199    0.7112
      0.9445    0.5581    0.6209    0.0623    0.8780    0.1493    0.8808    0.1265
   
   B = 
   
      0.3172    0.6943    0.5332    0.0864    0.7796    0.9290    0.0603    0.5482
      0.0613    0.7202    0.9752    0.1772    0.3029    0.2770    0.9965    0.0329
      0.0303    0.2764    0.1649    0.1819    0.5747    0.1635    0.1393    0.0791
      0.9745    0.7292    0.6524    0.0233    0.0645    0.2079    0.2486    0.9934
      0.0882    0.7735    0.8535    0.9994    0.6467    0.0613    0.7236    0.7196
      0.0418    0.5900    0.9117    0.7978    0.3917    0.5314    0.2436    0.3126
      0.9182    0.7101    0.6252    0.1326    0.7214    0.5316    0.4217    0.9722
      0.4210    0.6354    0.2091    0.6215    0.2720    0.3390    0.7847    0.3592
   
   C = 
   
      1.6557    2.9622    2.6447    2.0609    1.9659    1.4989    2.0556    2.4061
      1.3263    2.3059    2.5692    1.6426    1.5029    1.0794    1.6225    2.0798
      1.7696    2.4562    2.1075    1.0846    1.5429    1.3250    1.7976    1.9969
      1.3836    2.8805    2.7114    1.4517    2.1184    1.6327    1.9825    2.0502
      1.6420    2.3606    2.4189    0.9113    1.4236    1.2402    1.6901    2.0425
      1.5035    2.5018    2.3707    1.4880    1.4246    1.2280    1.8542    1.8950
      1.5915    3.0722    2.8703    2.0527    2.4144    1.8249    2.1962    2.4245
      1.3591    2.7479    2.6535    1.4870    2.5623    1.7908    1.8575    2.2273
   
   D = 
   
      1.6557    2.9622    2.6447    2.0609    1.9659    1.4989    2.0556    2.4061
      1.3263    2.3059    2.5692    1.6426    1.5029    1.0794    1.6225    2.0798
      1.7696    2.4562    2.1075    1.0846    1.5429    1.3250    1.7976    1.9969
      1.3836    2.8805    2.7114    1.4517    2.1184    1.6327    1.9825    2.0502
      1.6420    2.3606    2.4189    0.9113    1.4236    1.2402    1.6901    2.0425
      1.5035    2.5018    2.3707    1.4880    1.4246    1.2280    1.8542    1.8950
      1.5915    3.0722    2.8703    2.0527    2.4144    1.8249    2.1962    2.4245
      1.3591    2.7479    2.6535    1.4870    2.5623    1.7908    1.8575    2.2273
   


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

   
      0.0055    0.5172    0.6610    0.5481    0.2808    0.7884
      0.9038    0.8961    0.2261    0.3764    0.7474    0.5460
      0.5292    0.5255    0.4492    0.0623    0.2006    0.8285
      0.9387    0.9587    0.8042    0.9798    0.8359    0.3273
      0.3368    0.0811    0.1547    0.3820    0.5446    0.8497
   
   
      0.9038
      0.5292
      0.9387
      0.5172
      0.8961
      0.5255
      0.9587
      0.6610
      0.8042
      0.5481
      0.9798
      0.7474
      0.8359
      0.5446
      0.7884
      0.5460
      0.8285
      0.8497
   

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

   
      2.3103    0.1388    5.0931    1.0664    5.6038    1.6241
      1.2583    8.6246    0.7538    0.4628    4.9029    4.0233
      6.5655    0.2092    3.6815    3.3755    6.5108    7.9621
      8.0181    4.5960    3.7569    3.9424    5.6462    4.7587
      5.4161    7.4053    8.2089    6.8052    3.4690    4.0939
   
   
      0.0000    0.0000    5.0931    0.0000    5.6038    0.0000
      0.0000    8.6246    0.0000    0.0000    0.0000    0.0000
      6.5655    0.0000    0.0000    0.0000    6.5108    7.9621
      8.0181    0.0000    0.0000    0.0000    5.6462    0.0000
      5.4161    7.4053    8.2089    6.8052    0.0000    0.0000
   
   ⚠️ Runtime Error: Sequence contains no elements

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      0.5688    8.5217    6.5000    8.4146    2.1608    6.5000
      6.5000    4.4511    3.8829    4.6062    6.5000    9.3452
      3.8460    0.2751    8.2038    6.5000    3.9190    6.5000
      1.5124    6.5000    6.5000    9.2032    1.6906    9.1029
      6.5000    3.7806    6.5000    6.5000    6.5000    1.6762
   
Advantages
^^^^^^^^^^


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
   
