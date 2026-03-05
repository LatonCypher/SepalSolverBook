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
      0.8540    0.0616    0.5046    0.8042
   
   R1[2] = 0.5046086063886557
   C1 = 
      0.1280
      0.0439
      0.8864
      0.1683
      0.6107
      0.3754
      0.3250
      0.6978
   
   C1[5] = 0.3753877288617459

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
      0.6891    0.5541    0.2876    0.4455    0.1599
      0.5825    0.3456    0.7402    0.1894    0.4599
   

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
   
      0.2237    0.3832    0.0726    0.8931    0.3889    0.8242    0.8390    0.7182
      0.9519    0.2979    0.9434    0.1694    0.6381    0.2866    0.5294    0.4447
      0.6753    0.1638    0.9097    0.3180    0.5121    0.6990    0.1987    0.8529
      0.2280    0.2811    0.6827    0.5992    0.4210    0.8637    0.0220    0.7698
      0.0806    0.1224    0.6647    0.8179    0.3993    0.2106    0.3558    0.3222
      0.4993    0.6163    0.3167    0.9665    0.3784    0.5131    0.0276    0.1046
      0.9736    0.8789    0.6853    0.1406    0.4234    0.5728    0.0143    0.1331
      0.7991    0.6162    0.0347    0.5700    0.2996    0.0522    0.8925    0.4274
   
   B = 
   
      0.3938    0.1088    0.6632    0.1474    0.4109    0.3295    0.5865    0.3553
      0.0987    0.7754    0.2537    0.8304    0.2210    0.8158    0.0359    0.7912
      0.6351    0.6284    0.4952    0.0053    0.9033    0.2420    0.6365    0.0435
      0.0287    0.4142    0.9318    0.6298    0.9191    0.1353    0.1256    0.2874
      0.8377    0.1238    0.0535    0.0973    0.2095    0.9348    0.3495    0.6963
      0.7263    0.1619    0.4135    0.3985    0.8187    0.7375    0.8926    0.6975
      0.1848    0.3905    0.4367    0.7305    0.9167    0.9473    0.8926    0.8499
      0.3526    0.0227    0.1960    0.5253    0.3944    0.0628    0.6770    0.0310
   
   C = 
   
      1.5303    1.2626    1.9824    2.2704    2.8716    2.3360    2.4100    2.2234
      2.0057    1.3400    1.8030    1.2961    2.4940    2.1452    2.4432    1.7716
      2.1430    1.1775    1.8066    1.3622    2.6258    1.8552    2.5786    1.5399
      1.8238    1.1381    1.6592    1.4536    2.4424    1.6504    2.1125    1.4435
      1.1563    1.0899    1.5027    1.1840    2.1216    1.2838    1.4407    1.1268
      1.2181    1.2747    1.8099    1.5121    2.0817    1.6395    1.3236    1.6046
      1.7297    1.4302    1.6309    1.3154    1.9658    2.0627    1.8186    1.8221
      1.0184    1.2265    1.7458    1.9152    2.1120    2.0424    1.8217    1.9536
   
   D = 
   
      1.5303    1.2626    1.9824    2.2704    2.8716    2.3360    2.4100    2.2234
      2.0057    1.3400    1.8030    1.2961    2.4940    2.1452    2.4432    1.7716
      2.1430    1.1775    1.8066    1.3622    2.6258    1.8552    2.5786    1.5399
      1.8238    1.1381    1.6592    1.4536    2.4424    1.6504    2.1125    1.4435
      1.1563    1.0899    1.5027    1.1840    2.1216    1.2838    1.4407    1.1268
      1.2181    1.2747    1.8099    1.5121    2.0817    1.6395    1.3236    1.6046
      1.7297    1.4302    1.6309    1.3154    1.9658    2.0627    1.8186    1.8221
      1.0184    1.2265    1.7458    1.9152    2.1120    2.0424    1.8217    1.9536
   


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

   
      0.7742    0.6893    0.9548    0.3536    0.8868    0.9718
      0.7355    0.7853    0.3721    0.2932    0.5820    0.2914
      0.0381    0.9839    0.9866    0.5943    0.3642    0.7044
      0.9119    0.9577    0.7043    0.8944    0.8037    0.0679
      0.6974    0.4265    0.7664    0.8612    0.9578    0.1705
   
   
      0.7742
      0.7355
      0.9119
      0.6974
      0.6893
      0.7853
      0.9839
      0.9577
      0.9548
      0.9866
      0.7043
      0.7664
      0.5943
      0.8944
      0.8612
      0.8868
      0.5820
      0.8037
      0.9578
      0.9718
      0.7044
   

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

   
      8.5359    8.6371    3.3162    6.6539    3.7686    9.9419
      1.1709    6.1962    5.1031    7.0558    9.6950    9.7843
      0.7287    7.7482    0.8386    4.9240    0.8608    8.9391
      1.9285    5.3202    3.1219    8.6881    7.4529    9.0073
      7.4320    6.6975    7.3790    7.8236    3.6677    2.2363
   
   
      8.5359    8.6371    0.0000    6.6539    0.0000    9.9419
      0.0000    6.1962    5.1031    7.0558    9.6950    9.7843
      0.0000    7.7482    0.0000    0.0000    0.0000    8.9391
      0.0000    5.3202    0.0000    8.6881    7.4529    9.0073
      7.4320    6.6975    7.3790    7.8236    0.0000    0.0000
   
   
      8.5359    8.6371    0.0000    6.6539    0.0000       NaN
      0.0000    6.1962    5.1031    7.0558       NaN       NaN
      0.0000    7.7482    0.0000    0.0000    0.0000    8.9391
      0.0000    5.3202    0.0000    8.6881    7.4529       NaN
      7.4320    6.6975    7.3790    7.8236    0.0000    0.0000
   

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

   
      6.5000    9.1157    1.1543    3.5592    6.5000    9.1761
      6.5000    4.0979    0.3996    0.3983    6.5000    6.5000
      4.0705    6.5000    6.5000    3.5141    4.7925    2.6146
      6.5000    8.9784    4.0959    6.5000    9.7032    6.5000
      6.5000    2.7098    8.6237    6.5000    6.5000    2.2152
   
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
   
