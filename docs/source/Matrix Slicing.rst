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
      0.9530    0.5496    0.8299    0.8481
   
   R1[2] = 0.8299188477896312
   C1 = 
      0.8227
      0.2249
      0.8749
      0.0409
      0.1325
      0.4627
      0.5653
      0.8313
   
   C1[5] = 0.4626854924709147

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
      0.3322    0.9678    0.0585    0.9456    0.5337
      0.1812    0.1379    0.5774    0.1853    0.6698
   

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
   
      0.6999    0.1837    0.4268    0.9997    0.7417    0.9569    0.8889    0.9110
      0.1367    0.8285    0.8599    0.1488    0.5765    0.1115    0.3553    0.3734
      0.0668    0.9533    0.0813    0.1277    0.6206    0.6789    0.5593    0.6871
      0.3570    0.3461    0.2836    0.7466    0.7312    0.5906    0.4332    0.7067
      0.3516    0.4235    0.4019    0.5047    0.1210    0.3495    0.1727    0.8381
      0.5123    0.9595    0.2730    0.6886    0.4409    0.6235    0.9970    0.1456
      0.5961    0.0857    0.1254    0.0163    0.3435    0.9551    0.0434    0.2620
      0.2434    0.9545    0.1272    0.4128    0.3534    0.4233    0.9744    0.5330
   
   B = 
   
      0.4642    0.0939    0.0743    0.2928    0.4030    0.6959    0.5391    0.1044
      0.5284    0.1513    0.2839    0.1554    0.0045    0.9604    0.1948    0.7769
      0.5545    0.6000    0.5575    0.7109    0.4244    0.8125    0.4871    0.6454
      0.5785    0.3847    0.8851    0.4045    0.6741    0.2809    0.8540    0.5811
      0.9081    0.0254    0.2185    0.9801    0.4042    0.7051    0.5248    0.5136
      0.0267    0.8141    0.5853    0.6722    0.3284    0.3698    0.1479    0.0518
      0.7403    0.7561    0.6070    0.8226    0.9023    0.7109    0.1070    0.9619
      0.1179    0.7588    0.5189    0.3210    0.9257    0.4054    0.1608    0.6500
   
   C = 
   
      2.7015    2.8954    2.9613    3.3350    3.3972    3.1692    2.2470    2.9497
      1.8977    1.3688    1.4572    1.8924    1.4600    2.4830    1.1982    2.1857
      1.7305    1.7611    1.6630    2.0224    1.7663    2.4289    0.9668    2.2128
      2.0216    1.9064    2.0788    2.3587    2.3036    2.3495    1.6667    2.2052
      1.2477    1.5865    1.5879    1.4232    1.7497    1.7969    1.1672    1.7095
      2.4669    2.0051    2.2142    2.4898    2.2082    3.0026    1.6379    2.6876
      0.8015    1.1683    0.9493    1.3820    1.0389    1.3362    0.7813    0.6572
      2.0431    1.8972    1.9185    2.0806    2.0892    2.6200    1.1698    2.5761
   
   D = 
   
      2.7015    2.8954    2.9613    3.3350    3.3972    3.1692    2.2470    2.9497
      1.8977    1.3688    1.4572    1.8924    1.4600    2.4830    1.1982    2.1857
      1.7305    1.7611    1.6630    2.0224    1.7663    2.4289    0.9668    2.2128
      2.0216    1.9064    2.0788    2.3587    2.3036    2.3495    1.6667    2.2052
      1.2477    1.5865    1.5879    1.4232    1.7497    1.7969    1.1672    1.7095
      2.4669    2.0051    2.2142    2.4898    2.2082    3.0026    1.6379    2.6876
      0.8015    1.1683    0.9493    1.3820    1.0389    1.3362    0.7813    0.6572
      2.0431    1.8972    1.9185    2.0806    2.0892    2.6200    1.1698    2.5761
   


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

   
      0.8631    0.7359    0.1568    0.9310    0.0467    0.2333
      0.5813    0.3912    0.7490    0.2190    0.0344    0.1032
      0.3422    0.5012    0.6570    0.5176    0.2140    0.1495
      0.0001    0.4759    0.3579    0.2996    0.1441    0.1420
      0.9353    0.3019    0.3749    0.9352    0.2406    0.8136
   
   
      0.8631
      0.5813
      0.9353
      0.7359
      0.5012
      0.7490
      0.6570
      0.9310
      0.5176
      0.9352
      0.8136
   

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

   
      0.8973    8.4910    8.5269    3.8770    3.3834    1.7023
      0.4514    9.2687    0.5554    2.1961    6.4066    8.6792
      7.0767    3.3780    3.8762    8.9018    1.6924    7.7848
      6.2618    8.1060    8.6756    4.7948    1.4369    5.2589
      0.7972    4.7133    9.5248    3.9096    1.8139    0.0511
   
   
      0.0000    8.4910    8.5269    0.0000    0.0000    0.0000
      0.0000    9.2687    0.0000    0.0000    6.4066    8.6792
      7.0767    0.0000    0.0000    8.9018    0.0000    7.7848
      6.2618    8.1060    8.6756    0.0000    0.0000    5.2589
      0.0000    0.0000    9.5248    0.0000    0.0000    0.0000
   
   
      0.0000    8.4910    8.5269    0.0000    0.0000    0.0000
      0.0000       NaN    0.0000    0.0000    6.4066    8.6792
      7.0767    0.0000    0.0000    8.9018    0.0000    7.7848
      6.2618    8.1060    8.6756    0.0000    0.0000    5.2589
      0.0000    0.0000       NaN    0.0000    0.0000    0.0000
   

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

   
      3.1531    6.5000    1.0151    9.2294    6.5000    2.7241
      6.5000    1.1197    0.4410    3.9270    6.5000    2.8025
      9.8988    2.3886    8.5936    1.4718    4.8914    3.1607
      1.2029    6.5000    6.5000    6.5000    8.4143    9.8558
      6.5000    6.5000    6.5000    8.1044    6.5000    6.5000
   
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
   
