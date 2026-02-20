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
      0.9548    0.3354    0.5262    0.4196
   
   R1[2] = 0.5261767758240379
   C1 = 
      0.4000
      0.2393
      0.8881
      0.6746
      0.2264
      0.3417
      0.4663
      0.6510
   
   C1[5] = 0.3416916916783186

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
      0.1730    0.7800    0.3065    0.7586    0.5216
      0.3826    0.4354    0.6357    0.3308    0.3882
   

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
   
      0.5981    0.4837    0.3902    0.0107    0.3337    0.6263    0.8257    0.4916
      0.8767    0.0340    0.1952    0.2719    0.4881    0.3374    0.3854    0.1290
      0.0308    0.5291    0.3203    0.9893    0.1125    0.8504    0.8912    0.2114
      0.5686    0.7289    0.0240    0.0434    0.0966    0.7234    0.8316    0.3941
      0.8935    0.5992    0.4223    0.1993    0.6018    0.5405    0.2354    0.3870
      0.1221    0.8738    0.4904    0.3399    0.7356    0.8363    0.9649    0.4946
      0.0885    0.2567    0.3767    0.9097    0.1328    0.0980    0.5067    0.9929
      0.6781    0.7035    0.1354    0.9044    0.0953    0.4678    0.8400    0.3650
   
   B = 
   
      0.8962    0.5010    0.1619    0.1764    0.7149    0.2119    0.4679    0.8198
      0.1344    0.1048    0.3023    0.5895    0.2983    0.8424    0.6985    0.2130
      0.0223    0.7228    0.1901    0.6595    0.9277    0.0660    0.8940    0.8224
      0.4714    0.7879    0.4336    0.2138    0.2714    0.1877    0.5833    0.8446
      0.8523    0.7428    0.4143    0.3789    0.4171    0.3312    0.3056    0.8292
      0.5850    0.5513    0.9690    0.2243    0.0941    0.4536    0.4362    0.1778
      0.3772    0.7955    0.6697    0.6212    0.8810    0.5185    0.6863    0.6339
      0.5303    0.9980    0.5005    0.8650    0.3936    0.7974    0.3401    0.2697
   
   C = 
   
      1.8376    2.3814    1.8660    1.8553    2.0558    1.7767    2.0819    1.9673
      1.7499    1.7820    1.1590    0.9731    1.5174    0.8957    1.3718    1.8599
      1.6137    2.5541    2.2281    1.7099    1.7408    1.7127    2.3363    2.1032
      1.6566    1.9381    1.8309    1.6114    1.6540    1.8498    1.8718    1.5198
      2.1078    2.2912    1.6169    1.6622    1.9250    1.6345    2.0433    2.2244
      2.1404    3.0437    2.5335    2.4263    2.3254    2.3759    2.7243    2.4803
      1.4392    2.6070    1.5442    1.8558    1.6380    1.5736    1.8569    1.9218
      1.9967    2.5850    1.9782    1.7955    2.0332    1.8853    2.3911    2.3739
   
   D = 
   
      1.8376    2.3814    1.8660    1.8553    2.0558    1.7767    2.0819    1.9673
      1.7499    1.7820    1.1590    0.9731    1.5174    0.8957    1.3718    1.8599
      1.6137    2.5541    2.2281    1.7099    1.7408    1.7127    2.3363    2.1032
      1.6566    1.9381    1.8309    1.6114    1.6540    1.8498    1.8718    1.5198
      2.1078    2.2912    1.6169    1.6622    1.9250    1.6345    2.0433    2.2244
      2.1404    3.0437    2.5335    2.4263    2.3254    2.3759    2.7243    2.4803
      1.4392    2.6070    1.5442    1.8558    1.6380    1.5736    1.8569    1.9218
      1.9967    2.5850    1.9782    1.7955    2.0332    1.8853    2.3911    2.3739
   


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

   
      0.4377    0.2805    0.7200    0.9856    0.5973    0.9001
      0.1057    0.0217    0.5407    0.8785    0.4940    0.0380
      0.8895    0.9105    0.2725    0.8768    0.8146    0.5135
      0.2296    0.1573    0.1341    0.4554    0.8684    0.1107
      0.8750    0.0029    0.4255    0.3422    0.1994    0.2105
   
   
      0.8895
      0.8750
      0.9105
      0.7200
      0.5407
      0.9856
      0.8785
      0.8768
      0.5973
      0.8146
      0.8684
      0.9001
      0.5135
   

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

   
      5.0514    1.1713    5.9096    4.5294    0.1383    2.3160
      6.5351    1.9964    3.6602    6.6189    1.8668    2.9284
      4.1320    8.5433    1.4512    0.2271    9.1051    0.9739
      5.8768    3.9414    3.3562    6.2827    8.5454    3.1572
      4.1646    3.0908    7.9556    1.6376    2.4723    1.5277
   
   
      5.0514    0.0000    5.9096    0.0000    0.0000    0.0000
      6.5351    0.0000    0.0000    6.6189    0.0000    0.0000
      0.0000    8.5433    0.0000    0.0000    9.1051    0.0000
      5.8768    0.0000    0.0000    6.2827    8.5454    0.0000
      0.0000    0.0000    7.9556    0.0000    0.0000    0.0000
   
   
      5.0514    0.0000    5.9096    0.0000    0.0000    0.0000
      6.5351    0.0000    0.0000    6.6189    0.0000    0.0000
      0.0000    8.5433    0.0000    0.0000       NaN    0.0000
      5.8768    0.0000    0.0000    6.2827    8.5454    0.0000
      0.0000    0.0000    7.9556    0.0000    0.0000    0.0000
   

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

   
      6.5000    6.5000    4.4137    3.7452    8.7649    6.5000
      0.4815    8.2965    4.6295    9.5366    9.7211    3.8613
      1.0341    3.9289    4.7080    3.7412    3.2323    6.5000
      2.7976    4.5062    6.5000    4.3080    8.6684    0.6773
      8.9601    6.5000    3.0710    6.5000    4.2267    8.0113
   
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
   
