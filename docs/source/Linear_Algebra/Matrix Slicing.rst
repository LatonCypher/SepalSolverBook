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
      0.3926    0.4300    0.8750    0.4794
   
   R1[2] = 0.8749620185791056
   C1 = 
      0.3814
      0.7228
      0.3098
      0.6704
      0.4980
      0.4680
      0.4343
      0.6587
   
   C1[5] = 0.46798099389043546

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
      0.8339    0.6179    0.3588    0.2728    0.4265
      0.8248    0.3047    0.6551    0.5038    0.7526
   

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
   
      0.8327    0.0287    0.0276    0.9157    0.8911    0.7902    0.4997    0.5040
      0.7807    0.0330    0.5865    0.4253    0.3833    0.0888    0.3098    0.6331
      0.1124    0.4121    0.0023    0.6202    0.4704    0.1518    0.8153    0.2158
      0.8164    0.7096    0.0930    0.6465    0.8058    0.3600    0.1566    0.1935
      0.4874    0.6708    0.0970    0.4976    0.5083    0.7753    0.1723    0.0491
      0.0349    0.0275    0.3155    0.3835    0.8454    0.4279    0.8503    0.1734
      0.2618    0.2630    0.4698    0.9701    0.2951    0.2572    0.1099    0.3804
      0.4548    0.2843    0.0605    0.4200    0.6617    0.8013    0.4618    0.2070
   
   B = 
   
      0.0477    0.3803    0.6911    0.0596    0.6681    0.1514    0.4849    0.7524
      0.8429    0.5398    0.8477    0.8089    0.0378    0.9280    0.2626    0.8781
      0.4078    0.8997    0.1283    0.7824    0.2073    0.4826    0.8513    0.4970
      0.3430    0.9711    0.0717    0.1297    0.5557    0.9803    0.7395    0.5555
      0.8538    0.6619    0.0409    0.0243    0.0401    0.3646    0.6312    0.5413
      0.3033    0.9085    0.6710    0.5345    0.2709    0.6399    0.1415    0.9931
      0.0774    0.5941    0.9944    0.0283    0.4443    0.0063    0.9064    0.9927
      0.8480    0.8080    0.9079    0.1102    0.2218    0.8909    0.5954    0.1979
   
   C = 
   
      1.8557    3.2580    2.1900    0.7269    1.6556    2.3462    2.5392    3.0369
      1.3652    2.2855    1.6314    0.7226    1.1983    1.6114    2.1133    1.8726
      1.2602    1.9776    1.5995    0.5617    0.9059    1.4746    1.8092    2.0495
      1.8701    2.5149    1.8299    1.0171    1.1932    2.1583    1.9562    2.6302
      1.5229    2.3007    1.7104    1.1491    0.9655    1.9570    1.4789    2.5061
      1.3495    2.2779    1.4401    0.6133    0.8690    1.3008    2.0445    2.1812
      1.4195    2.4080    1.1729    0.9114    1.0361    2.0731    1.8621    1.7998
      1.4493    2.3963    1.8051    0.8391    1.0552    1.7149    1.7302    2.5085
   
   D = 
   
      1.8557    3.2580    2.1900    0.7269    1.6556    2.3462    2.5392    3.0369
      1.3652    2.2855    1.6314    0.7226    1.1983    1.6114    2.1133    1.8726
      1.2602    1.9776    1.5995    0.5617    0.9059    1.4746    1.8092    2.0495
      1.8701    2.5149    1.8299    1.0171    1.1932    2.1583    1.9562    2.6302
      1.5229    2.3007    1.7104    1.1491    0.9655    1.9570    1.4789    2.5061
      1.3495    2.2779    1.4401    0.6133    0.8690    1.3008    2.0445    2.1812
      1.4195    2.4080    1.1729    0.9114    1.0361    2.0731    1.8621    1.7998
      1.4493    2.3963    1.8051    0.8391    1.0552    1.7149    1.7302    2.5085
   


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

   
      0.1318    0.5608    0.5845    0.7305    0.8065    0.6830
      0.3818    0.3935    0.8970    0.3358    0.2885    0.3353
      0.8548    0.3766    0.5861    0.5610    0.5731    0.9881
      0.4793    0.0929    0.3806    0.9095    0.5251    0.8272
      0.5231    0.7565    0.4114    0.7645    0.7861    0.3876
   
   
      0.8548
      0.5231
      0.5608
      0.7565
      0.5845
      0.8970
      0.5861
      0.7305
      0.5610
      0.9095
      0.7645
      0.8065
      0.5731
      0.5251
      0.7861
      0.6830
      0.9881
      0.8272
   

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

   
      9.3726    8.6462    7.9582    9.6663    1.3534    3.4472
      3.0470    3.4250    1.4694    7.1504    5.0021    4.0502
      5.1344    7.4346    0.5320    7.7508    4.6531    8.4688
      8.5584    4.6687    1.6412    6.4677    3.6339    3.8417
      7.9496    5.2800    5.2414    1.5848    7.2649    4.8258
   
   
      9.3726    8.6462    7.9582    9.6663    0.0000    0.0000
      0.0000    0.0000    0.0000    7.1504    5.0021    0.0000
      5.1344    7.4346    0.0000    7.7508    0.0000    8.4688
      8.5584    0.0000    0.0000    6.4677    0.0000    0.0000
      7.9496    5.2800    5.2414    0.0000    7.2649    0.0000
   
   
         NaN    8.6462    7.9582       NaN    0.0000    0.0000
      0.0000    0.0000    0.0000    7.1504    5.0021    0.0000
      5.1344    7.4346    0.0000    7.7508    0.0000    8.4688
      8.5584    0.0000    0.0000    6.4677    0.0000    0.0000
      7.9496    5.2800    5.2414    0.0000    7.2649    0.0000
   

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

   
      6.5000    8.8878    8.2010    2.8295    6.5000    2.3217
      3.7158    0.6083    8.7144    4.0387    9.7203    6.5000
      8.6851    9.4133    3.9958    2.4855    6.5000    1.5087
      4.0216    0.0401    0.6633    9.2870    6.5000    3.1752
      6.5000    3.3105    3.4081    6.5000    9.8184    0.8677
   
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
   
