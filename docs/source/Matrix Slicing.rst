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
      0.5608    0.5352    0.5775    0.7983
   
   R1[2] = 0.5775257468962742
   C1 = 
      0.3450
      0.9770
      0.1335
      0.0581
      0.7369
      0.1471
      0.5677
      0.8282
   
   C1[5] = 0.1471392765252948

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
      0.1629    0.1479    0.4517    0.7831    0.2814
      0.4208    0.6485    0.8282    0.2978    0.6740
   

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
   
      0.2843    0.0504    0.9341    0.6330    0.9462    0.1067    0.2397    0.5272
      0.6004    0.5241    0.3599    0.7301    0.8842    0.3429    0.9862    0.5604
      0.5601    0.4281    0.9643    0.2447    0.6978    0.4238    0.6834    0.9128
      0.3402    0.3113    0.7621    0.4087    0.0995    0.0234    0.8825    0.9077
      0.7466    0.2411    0.7280    0.6897    0.2261    0.9507    0.6843    0.9733
      0.8110    0.2630    0.2511    0.0015    0.7486    0.2855    0.5323    0.1953
      0.3866    0.1217    0.3539    0.1129    0.3457    0.3478    0.1869    0.3423
      0.8845    0.0470    0.9484    0.6417    0.7954    0.2918    0.9312    0.0631
   
   B = 
   
      0.6706    0.9508    0.4443    0.5892    0.4951    0.5782    0.1285    0.4140
      0.0652    0.8692    0.8356    0.5107    0.5088    0.5412    0.2559    0.3618
      0.6677    0.5527    0.7404    0.0680    0.1329    0.6142    0.0234    0.9915
      0.5669    0.5626    0.2157    0.5044    0.6268    0.9730    0.1061    0.0959
      0.2217    0.6265    0.0780    0.8072    0.7720    0.0358    0.8703    0.4283
      0.5071    0.2163    0.5537    0.0249    0.3268    0.1766    0.2996    0.9301
      0.4100    0.2132    0.7323    0.9908    0.3496    0.8025    0.4191    0.0209
      0.5193    0.2662    0.6526    0.1449    0.8042    0.3735    0.4602    0.4780
   
   C = 
   
      1.8124    1.9938    1.6489    1.6563    1.9604    1.8231    1.3369    1.8843
      2.1563    2.6237    2.4754    2.7948    2.6596    2.6552    1.8407    1.8512
      2.3099    2.4927    2.7585    2.1208    2.4269    2.3750    1.6708    2.5100
      1.8560    1.7423    2.3229    1.7041    1.8073    2.2858    1.0657    1.5650
      2.7116    2.4620    2.9013    1.9855    2.5286    2.7689    1.4642    2.6449
      1.3599    1.8355    1.5000    1.7971    1.5840    1.3442    1.2275    1.3706
      1.0749    1.1552    1.1396    0.8934    1.0923    0.9682    0.7420    1.2049
      2.3321    2.5437    2.2195    2.5144    2.0759    2.5945    1.4149    2.0468
   
   D = 
   
      1.8124    1.9938    1.6489    1.6563    1.9604    1.8231    1.3369    1.8843
      2.1563    2.6237    2.4754    2.7948    2.6596    2.6552    1.8407    1.8512
      2.3099    2.4927    2.7585    2.1208    2.4269    2.3750    1.6708    2.5100
      1.8560    1.7423    2.3229    1.7041    1.8073    2.2858    1.0657    1.5650
      2.7116    2.4620    2.9013    1.9855    2.5286    2.7689    1.4642    2.6449
      1.3599    1.8355    1.5000    1.7971    1.5840    1.3442    1.2275    1.3706
      1.0749    1.1552    1.1396    0.8934    1.0923    0.9682    0.7420    1.2049
      2.3321    2.5437    2.2195    2.5144    2.0759    2.5945    1.4149    2.0468
   


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

   
      0.5082    0.0133    0.6611    0.4333    0.2045    0.5531
      0.3176    0.4944    0.8323    0.8278    0.7973    0.1504
      0.9438    0.4653    0.0930    0.3279    0.0807    0.4524
      0.2232    0.8777    0.2308    0.3166    0.9540    0.3657
      0.4111    0.6858    0.0873    0.8483    0.6867    0.8122
   
   
      0.5082
      0.9438
      0.8777
      0.6858
      0.6611
      0.8323
      0.8278
      0.8483
      0.7973
      0.9540
      0.6867
      0.5531
      0.8122
   

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

   
      5.0698    3.6628    3.6096    6.7232    0.1202    7.2918
      5.1421    3.6061    8.7365    2.6644    9.7254    8.2616
      2.1768    2.6283    9.5077    4.6211    2.3500    9.5369
      1.7320    7.0779    2.6782    9.9212    3.7973    6.7642
      3.3521    1.3580    5.9816    8.7143    8.7190    6.2738
   
   
      5.0698    0.0000    0.0000    6.7232    0.0000    7.2918
      5.1421    0.0000    8.7365    0.0000    9.7254    8.2616
      0.0000    0.0000    9.5077    0.0000    0.0000    9.5369
      0.0000    7.0779    0.0000    9.9212    0.0000    6.7642
      0.0000    0.0000    5.9816    8.7143    8.7190    6.2738
   
   
      5.0698    0.0000    0.0000    6.7232    0.0000    7.2918
      5.1421    0.0000    8.7365    0.0000       NaN    8.2616
      0.0000    0.0000       NaN    0.0000    0.0000       NaN
      0.0000    7.0779    0.0000       NaN    0.0000    6.7642
      0.0000    0.0000    5.9816    8.7143    8.7190    6.2738
   

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

   
      4.9700    8.7881    6.5000    0.9537    0.7589    9.7188
      4.6925    3.3385    6.5000    6.5000    6.5000    6.5000
      9.5765    9.6180    4.4205    9.4179    0.4334    2.5501
      6.5000    6.5000    0.0478    0.4328    6.5000    2.5867
      0.0676    2.3379    0.0520    0.2838    6.5000    6.5000
   
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
   
