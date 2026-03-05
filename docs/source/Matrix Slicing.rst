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
      0.2605    0.0315    0.6069    0.2848
   
   R1[2] = 0.606942959685257
   C1 = 
      0.5126
      0.0633
      0.3609
      0.3916
      0.0746
      0.0634
      0.8738
      0.9357
   
   C1[5] = 0.06335613711016441

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
      0.8934    0.2327    0.0213    0.4606    0.6676
      0.0695    0.7268    0.2155    0.8144    0.0108
   

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
   
      0.5112    0.4979    0.8290    0.1540    0.8209    0.9448    0.8578    0.7309
      0.5589    0.1372    0.0689    0.6937    0.4103    0.3945    0.6802    0.4512
      0.4680    0.2074    0.9579    0.3860    0.4638    0.8541    0.0896    0.2562
      0.7558    0.0331    0.2096    0.1171    0.9642    0.8311    0.0534    0.0459
      0.4999    0.8145    0.5360    0.7964    0.8855    0.5609    0.4253    0.4683
      0.3669    0.1391    0.2637    0.8593    0.8939    0.6506    0.8596    0.0664
      0.9862    0.7788    0.3881    0.8211    0.7169    0.5346    0.4201    0.7440
      0.5202    0.3700    0.3118    0.9508    0.6307    0.4698    0.8206    0.3061
   
   B = 
   
      0.7992    0.3324    0.9059    0.1576    0.5315    0.6824    0.8714    0.7515
      0.8777    0.4024    0.6350    0.6502    0.7503    0.8766    0.9994    0.4899
      0.8680    0.3113    0.2460    0.5187    0.3161    0.4086    0.2235    0.1057
      0.9235    0.5998    0.2079    0.9985    0.7552    0.2936    0.6393    0.1026
      0.3708    0.4928    0.2747    0.6790    0.9864    0.3947    0.2106    0.1243
      0.3313    0.7212    0.2890    0.9716    0.4529    0.8325    0.5407    0.8144
      0.7943    0.6073    0.3928    0.8843    0.0342    0.3742    0.4969    0.9983
      0.8127    0.3563    0.7512    0.5925    0.6324    0.5534    0.9079    0.3700
   
   C = 
   
      3.6000    2.5879    2.3998    3.6551    2.7528    3.0052    3.0004    2.7298
      2.4573    1.7390    1.5875    2.4364    1.8376    1.7280    2.1304    1.7839
      2.4782    1.7589    1.4734    2.4667    2.0080    2.0752    1.9126    1.5316
      1.6357    1.5233    1.3421    1.9028    1.9396    1.7826    1.5341    1.4853
      3.5476    2.4044    2.1917    3.4814    3.0859    2.7427    3.0049    2.0778
      2.7214    2.2308    1.4854    3.1816    2.2795    1.9851    2.0945    1.9835
      3.9479    2.5134    2.7294    3.5014    3.2854    3.0521    3.5736    2.4670
      3.1793    2.2462    1.8420    3.2254    2.4273    2.2024    2.5733    2.0962
   
   D = 
   
      3.6000    2.5879    2.3998    3.6551    2.7528    3.0052    3.0004    2.7298
      2.4573    1.7390    1.5875    2.4364    1.8376    1.7280    2.1304    1.7839
      2.4782    1.7589    1.4734    2.4667    2.0080    2.0752    1.9126    1.5316
      1.6357    1.5233    1.3421    1.9028    1.9396    1.7826    1.5341    1.4853
      3.5476    2.4044    2.1917    3.4814    3.0859    2.7427    3.0049    2.0778
      2.7214    2.2308    1.4854    3.1816    2.2795    1.9851    2.0945    1.9835
      3.9479    2.5134    2.7294    3.5014    3.2854    3.0521    3.5736    2.4670
      3.1793    2.2462    1.8420    3.2254    2.4273    2.2024    2.5733    2.0962
   


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

   
      0.3202    0.3571    0.7506    0.8294    0.9299    0.1740
      0.8673    0.4961    0.3713    0.4844    0.3646    0.8577
      0.4728    0.4981    0.5374    0.9044    0.3268    0.6414
      0.2857    0.1380    0.1053    0.7577    0.7279    0.3438
      0.0812    0.7715    0.3515    0.5225    0.4203    0.1299
   
   
      0.8673
      0.7715
      0.7506
      0.5374
      0.8294
      0.9044
      0.7577
      0.5225
      0.9299
      0.7279
      0.8577
      0.6414
   

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

   
      2.3513    8.8051    6.1524    3.5704    2.8096    3.1228
      2.9900    5.5399    8.6495    6.8901    9.5108    6.0469
      2.7229    9.5424    4.1725    3.0841    4.7600    2.8761
      5.9170    2.8147    5.1556    4.7693    4.9897    7.3494
      5.5607    4.6599    2.2920    9.3841    3.1534    0.9342
   
   
      0.0000    8.8051    6.1524    0.0000    0.0000    0.0000
      0.0000    5.5399    8.6495    6.8901    9.5108    6.0469
      0.0000    9.5424    0.0000    0.0000    0.0000    0.0000
      5.9170    0.0000    5.1556    0.0000    0.0000    7.3494
      5.5607    0.0000    0.0000    9.3841    0.0000    0.0000
   
   
      0.0000    8.8051    6.1524    0.0000    0.0000    0.0000
      0.0000    5.5399    8.6495    6.8901       NaN    6.0469
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
      5.9170    0.0000    5.1556    0.0000    0.0000    7.3494
      5.5607    0.0000    0.0000       NaN    0.0000    0.0000
   

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

   
      6.5000    2.2729    1.1847    0.4281    1.1338    1.4023
      0.5154    9.1469    9.8646    8.7010    2.9477    4.4312
      6.5000    0.7031    3.1681    9.5633    3.7266    1.7760
      2.8713    6.5000    2.7466    4.1042    6.5000    8.8384
      3.4320    6.5000    3.8574    6.5000    6.5000    6.5000
   
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
   
