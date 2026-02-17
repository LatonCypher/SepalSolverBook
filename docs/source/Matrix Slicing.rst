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
      0.8887    0.8787    0.6214    0.8258
   
   R1[2] = 0.6213554418562215
   C1 = 
      0.1507
      0.2934
      0.9705
      0.1521
      0.0348
      0.9545
      0.6489
      0.6747
   
   C1[5] = 0.9544554389433136

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
      0.3384    0.7714    0.4265    0.9841    0.9693
      0.2033    0.2329    0.9793    0.9876    0.1340
   

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
   
      0.8879    0.2067    0.3661    0.4266    0.2682    0.9196    0.0654    0.0335
      0.4644    0.7325    0.0616    0.8641    0.5656    0.7136    0.2973    0.8734
      0.8694    0.4448    0.7045    0.9670    0.0552    0.3642    0.3886    0.4780
      0.8753    0.9321    0.8871    0.6908    0.5706    0.3844    0.4252    0.9207
      0.6582    0.3645    0.6773    0.8480    0.0332    0.8542    0.6043    0.6659
      0.0996    0.9441    0.4844    0.9708    0.4685    0.2391    0.6450    0.5306
      0.0510    0.0489    0.1160    0.5902    0.1934    0.8975    0.3569    0.2392
      0.5051    0.0232    0.6643    0.4724    0.1820    0.9221    0.7797    0.9357
   
   B = 
   
      0.6934    0.5502    0.3348    0.1155    0.3664    0.6560    0.4190    0.0820
      0.1410    0.3615    0.6071    0.6718    0.7320    0.3736    0.1072    0.8914
      0.9078    0.6420    0.7864    0.2317    0.4703    0.0085    0.2995    0.0567
      0.7574    0.7189    0.0939    0.1400    0.7507    0.0375    0.9717    0.3642
      0.0846    0.1439    0.7100    0.5990    0.2617    0.5748    0.8214    0.8634
      0.3564    0.6744    0.3193    0.8785    0.0312    0.7848    0.6422    0.5145
      0.2032    0.4544    0.6594    0.6081    0.0431    0.2424    0.3962    0.7234
      0.6097    0.0721    0.4119    0.0990    0.7164    0.9403    0.7446    0.7378
   
   C = 
   
      1.6845    1.7959    1.2917    1.3976    1.0948    1.6020    1.7800    1.2099
      2.0309    1.9418    1.9150    1.9139    2.1929    2.3897    2.8222    2.7242
      2.5425    2.2512    1.8146    1.3345    2.0864    1.6400    2.3517    1.7287
      2.9001    2.4857    2.8090    2.0588    2.7780    2.5543    2.9732    2.8819
      2.6012    2.4419    2.0230    1.8003    2.0016    2.0676    2.6529    2.1228
      1.9567    1.9649    2.1314    1.8293    2.2221    1.5707    2.4203    2.6163
      1.1493    1.3571    0.9512    1.2933    0.8175    1.2018    1.6896    1.3327
      2.3874    2.1222    2.0731    1.7799    1.6494    2.2605    2.6195    2.1577
   
   D = 
   
      1.6845    1.7959    1.2917    1.3976    1.0948    1.6020    1.7800    1.2099
      2.0309    1.9418    1.9150    1.9139    2.1929    2.3897    2.8222    2.7242
      2.5425    2.2512    1.8146    1.3345    2.0864    1.6400    2.3517    1.7287
      2.9001    2.4857    2.8090    2.0588    2.7780    2.5543    2.9732    2.8819
      2.6012    2.4419    2.0230    1.8003    2.0016    2.0676    2.6529    2.1228
      1.9567    1.9649    2.1314    1.8293    2.2221    1.5707    2.4203    2.6163
      1.1493    1.3571    0.9512    1.2933    0.8175    1.2018    1.6896    1.3327
      2.3874    2.1222    2.0731    1.7799    1.6494    2.2605    2.6195    2.1577
   


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

   
      0.6386    0.8787    0.1377    0.1507    0.0854    0.8954
      0.0019    0.0068    0.7438    0.2557    0.4489    0.6383
      0.9591    0.8829    0.7686    0.9413    0.3160    0.4590
      0.3020    0.0759    0.6494    0.2357    0.1614    0.3497
      0.6368    0.4469    0.5224    0.1267    0.6410    0.5090
   
   
      0.6386
      0.9591
      0.6368
      0.8787
      0.8829
      0.7438
      0.7686
      0.6494
      0.5224
      0.9413
      0.6410
      0.8954
      0.6383
      0.5090
   

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

   
      5.0670    6.5337    3.4011    4.0057    1.5237    9.8870
      9.7957    1.5990    4.6848    6.9138    5.1592    2.4104
      6.2751    5.3105    9.3613    5.7282    1.6082    6.6318
      0.7799    5.7648    5.4673    2.1669    3.5252    2.4382
      2.7133    7.4151    3.7237    6.9056    0.1742    8.5870
   
   
      5.0670    6.5337    0.0000    0.0000    0.0000    9.8870
      9.7957    0.0000    0.0000    6.9138    5.1592    0.0000
      6.2751    5.3105    9.3613    5.7282    0.0000    6.6318
      0.0000    5.7648    5.4673    0.0000    0.0000    0.0000
      0.0000    7.4151    0.0000    6.9056    0.0000    8.5870
   
   
      5.0670    6.5337    0.0000    0.0000    0.0000       NaN
         NaN    0.0000    0.0000    6.9138    5.1592    0.0000
      6.2751    5.3105       NaN    5.7282    0.0000    6.6318
      0.0000    5.7648    5.4673    0.0000    0.0000    0.0000
      0.0000    7.4151    0.0000    6.9056    0.0000    8.5870
   

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

   
      3.3406    9.9431    0.3044    6.5000    6.5000    9.3364
      4.9724    6.5000    1.1657    9.9297    6.5000    9.0645
      8.4568    3.7506    6.5000    3.0614    0.8188    6.5000
      2.1264    8.5357    6.5000    3.9049    2.2687    6.5000
      3.2411    9.4802    4.0938    4.1352    8.3581    9.2275
   
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
   
