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
      0.8958    0.0026    0.4835    0.1549
   
   R1[2] = 0.48352071565083243
   C1 = 
      0.8803
      0.4195
      0.1148
      0.2774
      0.8569
      0.1765
      0.4810
      0.8602
   
   C1[5] = 0.17647034775776682

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
      0.5642    0.7092    0.0437    0.9594    0.0595
      0.9479    0.3823    0.2974    0.7033    0.0419
   

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
   
      0.4263    0.2070    0.1329    0.1005    0.6011    0.5398    0.0303    0.8758
      0.3925    0.1092    0.7553    0.0803    0.9010    0.1363    0.2180    0.0336
      0.2028    0.3682    0.6404    0.9335    0.6290    0.2261    0.7829    0.5936
      0.3707    0.0852    0.6400    0.9329    0.3538    0.3437    0.5850    0.5780
      0.9279    0.3154    0.8973    0.5229    0.5377    0.3650    0.9535    0.0585
      0.8749    0.7266    0.3613    0.7506    0.0642    0.3829    0.6441    0.7981
      0.2675    0.2230    0.3575    0.5600    0.9680    0.9957    0.7557    0.6735
      0.4100    0.1039    0.1549    0.7623    0.0723    0.1854    0.6962    0.1248
   
   B = 
   
      0.6779    0.5948    0.0186    0.3381    0.2336    0.1211    0.1096    0.2317
      0.9656    0.2266    0.1771    0.4149    0.5415    0.8490    0.4263    0.9003
      0.8642    0.9583    0.7032    0.7349    0.0358    0.6537    0.0748    0.2188
      0.7085    0.3847    0.6472    0.0895    0.7666    0.5997    0.9688    0.4548
      0.7521    0.7055    0.3514    0.9924    0.8601    0.2149    0.3707    0.3424
      0.4751    0.3843    0.7114    0.9872    0.2961    0.0865    0.6891    0.0422
      0.0921    0.5646    0.2973    0.5424    0.5907    0.5497    0.6232    0.1018
      0.4733    0.9829    0.4122    0.9432    0.3645    0.9636    0.0041    0.4972
   
   C = 
   
      1.8008    1.9761    1.1684    2.3087    1.3075    1.4110    0.8596    1.0271
      1.8595    1.8571    1.1020    1.9189    1.1958    1.0398    0.7878    0.7442
      2.6413    2.7330    1.9826    2.6074    2.2720    2.4726    2.0106    1.5429
      2.3044    2.4922    1.8568    2.2674    1.8331    2.0794    1.7635    1.2095
      2.7727    2.7998    1.7987    2.6170    1.9756    2.0078    1.8551    1.2587
      2.8060    2.6606    1.7002    2.4739    2.0261    2.5791    1.8521    1.7778
      2.6918    2.9221    2.2090    3.4847    2.4447    2.1498    2.2123    1.3809
      1.3178    1.3471    1.0440    1.1138    1.3157    1.2307    1.4282    0.7346
   
   D = 
   
      1.8008    1.9761    1.1684    2.3087    1.3075    1.4110    0.8596    1.0271
      1.8595    1.8571    1.1020    1.9189    1.1958    1.0398    0.7878    0.7442
      2.6413    2.7330    1.9826    2.6074    2.2720    2.4726    2.0106    1.5429
      2.3044    2.4922    1.8568    2.2674    1.8331    2.0794    1.7635    1.2095
      2.7727    2.7998    1.7987    2.6170    1.9756    2.0078    1.8551    1.2587
      2.8060    2.6606    1.7002    2.4739    2.0261    2.5791    1.8521    1.7778
      2.6918    2.9221    2.2090    3.4847    2.4447    2.1498    2.2123    1.3809
      1.3178    1.3471    1.0440    1.1138    1.3157    1.2307    1.4282    0.7346
   


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

   
      0.2051    0.8548    0.2130    0.8717    0.8228    0.4528
      0.2976    0.3369    0.1024    0.0853    0.0882    0.2046
      0.2607    0.4334    0.3093    0.5641    0.9298    0.5782
      0.0799    0.3908    0.3183    0.3255    0.6867    0.4552
      0.9190    0.2460    0.6527    0.3957    0.6677    0.7234
   
   
      0.9190
      0.8548
      0.6527
      0.8717
      0.5641
      0.8228
      0.9298
      0.6867
      0.6677
      0.5782
      0.7234
   

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

   
      1.6358    6.2743    5.7102    2.0108    3.0654    2.7660
      0.5536    1.1528    7.3919    0.3313    2.8606    1.1816
      1.8730    2.7239    1.8425    8.7562    7.2878    1.0728
      9.1219    4.9626    3.6001    9.2572    4.2846    9.1335
      1.8031    3.4081    6.5353    6.6582    0.7483    0.6454
   
   
      0.0000    6.2743    5.7102    0.0000    0.0000    0.0000
      0.0000    0.0000    7.3919    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.7562    7.2878    0.0000
      9.1219    0.0000    0.0000    9.2572    0.0000    9.1335
      0.0000    0.0000    6.5353    6.6582    0.0000    0.0000
   
   
      0.0000    6.2743    5.7102    0.0000    0.0000    0.0000
      0.0000    0.0000    7.3919    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.7562    7.2878    0.0000
         NaN    0.0000    0.0000       NaN    0.0000       NaN
      0.0000    0.0000    6.5353    6.6582    0.0000    0.0000
   

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

   
      8.0123    4.3957    6.5000    0.9485    9.3768    9.9564
      2.3565    9.8081    2.6488    0.2022    4.8430    8.4598
      4.6876    6.5000    2.7423    6.5000    6.5000    6.5000
      8.0161    8.0525    8.0557    1.8230    6.5000    6.5000
      1.3545    6.5000    8.3462    8.7771    0.0160    3.0244
   
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
   
