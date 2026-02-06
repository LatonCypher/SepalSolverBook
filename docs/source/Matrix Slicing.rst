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
      0.6946    0.5450    0.0487    0.4370
   
   R1[2] = 0.04870555250570141
   C1 = 
      0.8590
      0.0532
      0.9344
      0.1279
      0.7836
      0.4296
      0.8693
      0.6735
   
   C1[5] = 0.42957628521498237

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
      0.2591    0.2266    0.1204    0.2006    0.1219
      0.6844    0.4409    0.7257    0.4203    0.0952
   

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
   
      0.6393    0.2602    0.3975    0.4905    0.2510    0.6323    0.2197    0.6298
      0.2182    0.4388    0.9536    0.0211    0.1873    0.1663    0.6237    0.7122
      0.7962    0.3342    0.0908    0.3467    0.6959    0.7720    0.1257    0.8927
      0.3816    0.0791    0.1309    0.3580    0.2742    0.9946    0.2324    0.7877
      0.9263    0.9745    0.3276    0.4670    0.5827    0.2341    0.3515    0.7586
      0.4251    0.0994    0.4108    0.0523    0.5887    0.1970    0.2507    0.7866
      0.6852    0.0570    0.9898    0.2706    0.0078    0.9379    0.5489    0.9096
      0.2242    0.2766    0.6466    0.3529    0.4876    0.4140    0.0861    0.1612
   
   B = 
   
      0.7552    0.8496    0.9345    0.2614    0.2122    0.9056    0.5918    0.3610
      0.0017    0.4151    0.4799    0.4994    0.8312    0.6968    0.8520    0.1185
      0.9430    0.8571    0.4802    0.1935    0.1967    0.8052    0.4107    0.5005
      0.1377    0.9606    0.1374    0.6006    0.0201    0.7301    0.9101    0.4259
      0.2987    0.8812    0.3370    0.0757    0.1634    0.5754    0.8415    0.7276
      0.7722    0.4817    0.6861    0.2406    0.2873    0.1125    0.9508    0.5534
      0.4479    0.4801    0.1295    0.8612    0.3573    0.6105    0.2590    0.7830
      0.3730    0.9074    0.5091    0.2302    0.0490    0.0150    0.1854    0.1566
   
   C = 
   
      1.8222    2.6658    1.8481    1.1739    0.7720    1.7976    2.1959    1.4727
      1.7971    2.3960    1.4959    1.2286    0.9351    1.8047    1.5232    1.4454
      1.9285    3.0816    2.2306    1.1530    0.8957    1.8575    2.6265    1.6920
      1.7089    2.3601    1.7125    1.0211    0.6318    1.1908    2.0554    1.4203
      1.8697    3.4043    2.3436    1.6505    1.4057    2.7102    2.8827    1.7604
      1.4494    2.2525    1.4159    0.7605    0.5355    1.3489    1.4460    1.2499
      2.7999    3.2616    2.3604    1.4699    0.9043    2.1135    2.3159    1.9617
      1.3921    2.0151    1.2428    0.7815    0.6490    1.5561    1.8112    1.2641
   
   D = 
   
      1.8222    2.6658    1.8481    1.1739    0.7720    1.7976    2.1959    1.4727
      1.7971    2.3960    1.4959    1.2286    0.9351    1.8047    1.5232    1.4454
      1.9285    3.0816    2.2306    1.1530    0.8957    1.8575    2.6265    1.6920
      1.7089    2.3601    1.7125    1.0211    0.6318    1.1908    2.0554    1.4203
      1.8697    3.4043    2.3436    1.6505    1.4057    2.7102    2.8827    1.7604
      1.4494    2.2525    1.4159    0.7605    0.5355    1.3489    1.4460    1.2499
      2.7999    3.2616    2.3604    1.4699    0.9043    2.1135    2.3159    1.9617
      1.3921    2.0151    1.2428    0.7815    0.6490    1.5561    1.8112    1.2641
   


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

   
      0.4023    0.8977    0.3278    0.8970    0.7272    0.2225
      0.7570    0.6674    0.6114    0.1404    0.9935    0.8625
      0.5329    0.2121    0.4660    0.4040    0.0257    0.4200
      0.0835    0.0527    0.6145    0.2944    0.0163    0.1144
      0.4813    0.1960    0.9388    0.7323    0.7302    0.2589
   
   
      0.7570
      0.5329
      0.8977
      0.6674
      0.6114
      0.6145
      0.9388
      0.8970
      0.7323
      0.7272
      0.9935
      0.7302
      0.8625
   

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

   
      1.4519    5.5086    1.5743    1.1224    9.0468    9.5540
      0.9917    0.9398    2.0046    5.5205    1.4098    7.3520
      9.5229    5.3492    5.3440    7.8476    6.5700    1.9018
      1.8486    0.9196    1.5459    0.2181    3.3859    9.2748
      3.1068    3.2189    3.3251    0.6843    7.9190    5.6253
   
   
      0.0000    5.5086    0.0000    0.0000    9.0468    9.5540
      0.0000    0.0000    0.0000    5.5205    0.0000    7.3520
      9.5229    5.3492    5.3440    7.8476    6.5700    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    9.2748
      0.0000    0.0000    0.0000    0.0000    7.9190    5.6253
   
   
      0.0000    5.5086    0.0000    0.0000       NaN       NaN
      0.0000    0.0000    0.0000    5.5205    0.0000    7.3520
         NaN    5.3492    5.3440    7.8476    6.5700    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
      0.0000    0.0000    0.0000    0.0000    7.9190    5.6253
   

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

   
      0.1030    0.8338    3.1116    2.2851    2.3925    9.8712
      2.1169    6.5000    8.9516    0.0322    6.5000    6.5000
      0.7612    9.9208    6.5000    6.5000    2.8497    8.5118
      8.4130    0.6608    3.4751    0.6300    4.4455    6.5000
      1.3006    9.7597    6.5000    9.3341    6.5000    4.2665
   
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
   
