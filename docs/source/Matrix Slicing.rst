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
      0.7434    0.2128    0.2569    0.0547
   
   R1[2] = 0.25689567760466414
   C1 = 
      0.3638
      0.7450
      0.0766
      0.9717
      0.4434
      0.5217
      0.9174
      0.3393
   
   C1[5] = 0.521699503910018

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
      0.2758    0.1839    0.7889    0.6450    0.2289
      0.0873    0.0901    0.2161    0.1765    0.6210
   

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
   
      0.8568    0.2964    0.0106    0.6987    0.8561    0.7536    0.9599    0.7721
      0.0268    0.0218    0.9247    0.2740    0.8882    0.2666    0.6530    0.5926
      0.9884    0.2616    0.0910    0.9700    0.9189    0.4679    0.9283    0.4257
      0.4134    0.4448    0.0999    0.9054    0.3176    0.9789    0.3773    0.5500
      0.5225    0.1168    0.9893    0.7319    0.8490    0.3543    0.2945    0.2141
      0.6924    0.9799    0.7518    0.8711    0.9413    0.3743    0.1456    0.6079
      0.0808    0.3184    0.1241    0.9797    0.1161    0.9503    0.5835    0.5970
      0.5156    0.1793    0.6591    0.7471    0.6724    0.4037    0.1830    0.9220
   
   B = 
   
      0.9583    0.6994    0.4820    0.5776    0.2750    0.5637    0.5999    0.7235
      0.4251    0.0984    0.0607    0.4358    0.5690    0.6717    0.2647    0.8552
      0.7261    0.0471    0.0675    0.5786    0.0936    0.7947    0.8863    0.4666
      0.3198    0.4297    0.8979    0.6888    0.3700    0.9107    0.1209    0.8078
      0.6809    0.6782    0.8855    0.7196    0.9465    0.6329    0.2726    0.7764
      0.1876    0.6269    0.5028    0.7483    0.4371    0.4667    0.8486    0.3598
      0.6881    0.4177    0.5311    0.0897    0.8082    0.2003    0.5113    0.3695
      0.9578    0.4836    0.4550    0.1264    0.6090    0.7193    0.6632    0.3974
   
   C = 
   
      3.3025    2.7565    3.0573    2.4752    3.0495    2.9681    2.5620    3.0401
      2.4658    1.5110    1.8598    1.7209    2.0535    2.2578    2.0698    1.9532
      3.1947    2.6482    3.1053    2.5543    2.8718    2.9808    2.2646    3.1589
      2.1336    1.9793    2.2702    2.1785    2.0795    2.5648    2.0387    2.4143
      2.5550    1.7624    2.1668    2.3588    1.9003    2.7414    2.1343    2.5113
      3.2982    2.2182    2.6019    2.9093    2.6830    3.6761    2.4982    3.5543
      1.8468    1.7215    2.1086    1.8546    1.9379    2.3136    1.8934    2.0651
      2.8304    1.9617    2.2899    2.1908    2.1041    2.9291    2.2621    2.5387
   
   D = 
   
      3.3025    2.7565    3.0573    2.4752    3.0495    2.9681    2.5620    3.0401
      2.4658    1.5110    1.8598    1.7209    2.0535    2.2578    2.0698    1.9532
      3.1947    2.6482    3.1053    2.5543    2.8718    2.9808    2.2646    3.1589
      2.1336    1.9793    2.2702    2.1785    2.0795    2.5648    2.0387    2.4143
      2.5550    1.7624    2.1668    2.3588    1.9003    2.7414    2.1343    2.5113
      3.2982    2.2182    2.6019    2.9093    2.6830    3.6761    2.4982    3.5543
      1.8468    1.7215    2.1086    1.8546    1.9379    2.3136    1.8934    2.0651
      2.8304    1.9617    2.2899    2.1908    2.1041    2.9291    2.2621    2.5387
   


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

   
      0.4301    0.4546    0.4650    0.5800    0.2198    0.0941
      0.6595    0.2414    0.7450    0.0407    0.6817    0.8202
      0.4076    0.1234    0.4787    0.5003    0.1750    0.6266
      0.6808    0.6695    0.3323    0.4604    0.7937    0.1579
      0.5122    0.4967    0.8472    0.9770    0.2612    0.1564
   
   
      0.6595
      0.6808
      0.5122
      0.6695
      0.7450
      0.8472
      0.5800
      0.5003
      0.9770
      0.6817
      0.7937
      0.8202
      0.6266
   

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

   
      2.2360    8.1093    5.5717    0.7941    6.9285    7.4829
      6.0399    2.8702    6.2799    8.2306    9.6888    8.5053
      3.6653    3.3974    8.5774    8.3345    3.7295    7.7747
      9.0349    3.3609    8.1943    8.6181    7.9376    7.5853
      1.0501    0.3997    4.9774    5.3877    1.0482    2.8855
   
   
      0.0000    8.1093    5.5717    0.0000    6.9285    7.4829
      6.0399    0.0000    6.2799    8.2306    9.6888    8.5053
      0.0000    0.0000    8.5774    8.3345    0.0000    7.7747
      9.0349    0.0000    8.1943    8.6181    7.9376    7.5853
      0.0000    0.0000    0.0000    5.3877    0.0000    0.0000
   
   
      0.0000    8.1093    5.5717    0.0000    6.9285    7.4829
      6.0399    0.0000    6.2799    8.2306       NaN    8.5053
      0.0000    0.0000    8.5774    8.3345    0.0000    7.7747
         NaN    0.0000    8.1943    8.6181    7.9376    7.5853
      0.0000    0.0000    0.0000    5.3877    0.0000    0.0000
   

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

   
      2.4229    0.2675    6.5000    6.5000    6.5000    2.6421
      6.5000    6.5000    0.8026    6.5000    9.3652    3.1324
      6.5000    6.5000    4.6570    6.5000    1.8734    0.5686
      1.6739    1.9256    8.9387    2.0212    0.1409    6.5000
      8.3678    4.6105    2.6560    0.1287    3.4477    6.5000
   
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
   
