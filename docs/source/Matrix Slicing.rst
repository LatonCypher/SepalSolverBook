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
      0.8290    0.3446    0.1768    0.3000
   
   R1[2] = 0.17684151575448603
   C1 = 
      0.1358
      0.0291
      0.3669
      0.7641
      0.1068
      0.8089
      0.2903
      0.1258
   
   C1[5] = 0.8088742410029837

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
      0.6548    0.6319    0.7291    0.3691    0.2361
      0.9318    0.1169    0.2048    0.0020    0.6805
   

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
   
      0.8819    0.3552    0.3421    0.5397    0.8648    0.8076    0.0315    0.9027
      0.5436    0.1530    0.8417    0.7334    0.7369    0.5626    0.0123    0.7313
      0.6626    0.7581    0.5612    0.4316    0.2835    0.5009    0.7421    0.2512
      0.2942    0.4305    0.4784    0.4070    0.0042    0.2112    0.4486    0.7158
      0.6953    0.3864    0.8009    0.1187    0.2526    0.8261    0.9790    0.5430
      0.9448    0.1341    0.0708    0.6653    0.2259    0.8169    0.0634    0.7049
      0.3119    0.7082    0.9612    0.0939    0.6860    0.3498    0.2379    0.2993
      0.1618    0.6338    0.6585    0.3039    0.1796    0.0799    0.6707    0.7230
   
   B = 
   
      0.0886    0.1093    0.4689    0.7934    0.5575    0.5314    0.6580    0.3174
      0.3798    0.1275    0.9653    0.4331    0.5200    0.7654    0.2730    0.0662
      0.9944    0.3669    0.3246    0.3851    0.5577    0.2971    0.9515    0.2460
      0.1191    0.8532    0.6508    0.9731    0.3932    0.0501    0.0614    0.6625
      0.0845    0.6744    0.9943    0.0106    0.9772    0.0229    0.7245    0.7790
      0.8560    0.0577    0.6621    0.5945    0.0334    0.6883    0.0576    0.0218
      0.2062    0.5553    0.6211    0.8416    0.0520    0.4468    0.2332    0.8204
      0.9361    0.5771    0.5208    0.8880    0.0408    0.4281    0.9620    0.4080
   
   C = 
   
      2.2333    1.8959    3.1028    2.8279    1.9897    1.8455    2.5846    1.8305
      2.2615    1.9718    2.6468    2.5374    1.9097    1.4156    2.5180    1.7703
      1.7970    1.5203    2.7108    2.6386    1.5888    1.9111    1.8523    1.6275
      1.6574    1.2870    1.7691    2.1389    0.8783    1.3007    1.5996    1.1771
      2.4575    1.5953    2.7252    2.9429    1.4293    2.1535    2.3135    1.7613
      1.6756    1.3554    2.2004    2.6498    1.1776    1.5568    1.6701    1.3003
      1.9502    1.3448    2.4203    1.6970    1.8217    1.4891    2.1794    1.3038
      1.8447    1.5148    2.1239    2.2081    1.1490    1.4503    1.9113    1.4435
   
   D = 
   
      2.2333    1.8959    3.1028    2.8279    1.9897    1.8455    2.5846    1.8305
      2.2615    1.9718    2.6468    2.5374    1.9097    1.4156    2.5180    1.7703
      1.7970    1.5203    2.7108    2.6386    1.5888    1.9111    1.8523    1.6275
      1.6574    1.2870    1.7691    2.1389    0.8783    1.3007    1.5996    1.1771
      2.4575    1.5953    2.7252    2.9429    1.4293    2.1535    2.3135    1.7613
      1.6756    1.3554    2.2004    2.6498    1.1776    1.5568    1.6701    1.3003
      1.9502    1.3448    2.4203    1.6970    1.8217    1.4891    2.1794    1.3038
      1.8447    1.5148    2.1239    2.2081    1.1490    1.4503    1.9113    1.4435
   


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

   
      0.7694    0.7515    0.3347    0.7967    0.9891    0.7013
      0.3460    0.4526    0.5522    0.6655    0.2687    0.4176
      0.1441    0.3804    0.5550    0.2526    0.7962    0.8143
      0.5793    0.3104    0.9655    0.0359    0.1421    0.7425
      0.3190    0.1869    0.9352    0.2775    0.8138    0.7879
   
   
      0.7694
      0.5793
      0.7515
      0.5522
      0.5550
      0.9655
      0.9352
      0.7967
      0.6655
      0.9891
      0.7962
      0.8138
      0.7013
      0.8143
      0.7425
      0.7879
   

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

   
      4.7763    6.7277    5.1898    1.6525    1.3861    1.6476
      8.7703    6.0001    0.6331    4.9851    6.8470    1.3912
      7.0535    2.1583    0.9011    7.3711    6.8642    3.6615
      0.3322    0.7668    1.5227    4.6072    8.6349    1.6593
      3.4549    0.4813    0.2889    6.8141    1.4010    7.2561
   
   
      0.0000    6.7277    5.1898    0.0000    0.0000    0.0000
      8.7703    6.0001    0.0000    0.0000    6.8470    0.0000
      7.0535    0.0000    0.0000    7.3711    6.8642    0.0000
      0.0000    0.0000    0.0000    0.0000    8.6349    0.0000
      0.0000    0.0000    0.0000    6.8141    0.0000    7.2561
   
   
      0.0000    6.7277    5.1898    0.0000    0.0000    0.0000
      8.7703    6.0001    0.0000    0.0000    6.8470    0.0000
      7.0535    0.0000    0.0000    7.3711    6.8642    0.0000
      0.0000    0.0000    0.0000    0.0000    8.6349    0.0000
      0.0000    0.0000    0.0000    6.8141    0.0000    7.2561
   

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

   
      1.4214    6.5000    6.5000    2.3214    9.9431    6.5000
      1.3676    1.4659    2.0702    3.2609    3.6707    2.2156
      6.5000    6.5000    6.5000    8.4685    0.6615    8.3264
      6.5000    3.5095    1.5946    6.5000    4.1756    9.6456
      4.3050    8.0318    4.8778    2.0523    0.4310    8.2170
   
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
   
