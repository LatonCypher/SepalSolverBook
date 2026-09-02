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
      0.8104    0.7348    0.6592    0.7118
   
   R1[2] = 0.6592104628253749
   C1 = 
      0.3191
      0.7322
      0.6845
      0.2348
      0.7342
      0.8234
      0.4350
      0.4705
   
   C1[5] = 0.8234386542224492

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
      0.2563    0.1255    0.2245    0.8221    0.9284
      0.6458    0.6717    0.2345    0.2339    0.0442
   

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
   
      0.3804    0.2718    0.4584    0.2218    0.5638    0.1993    0.3225    0.6158
      0.2196    0.6815    0.6162    0.1667    0.2021    0.5164    0.0998    0.8186
      0.8298    0.7216    0.5885    0.0442    0.8207    0.2887    0.6993    0.9715
      0.9132    0.0915    0.1603    0.5046    0.2352    0.7732    0.9114    0.3305
      0.5045    0.7202    0.9421    0.9142    0.1569    0.0242    0.0937    0.4101
      0.6798    0.2746    0.1881    0.5534    0.6212    0.1746    0.9957    0.5481
      0.6851    0.2449    0.0011    0.5165    0.6783    0.8965    0.6773    0.7435
      0.7410    0.3384    0.0271    0.1701    0.4961    0.0347    0.8885    0.9244
   
   B = 
   
      0.3855    0.4045    0.8538    0.5860    0.8530    0.2063    0.2132    0.4307
      0.3579    0.9596    0.0246    0.2095    0.3959    0.6015    0.5194    0.6889
      0.0171    0.5579    0.0557    0.2261    0.2029    0.6210    0.2372    0.1144
      0.6527    0.4025    0.1335    0.5131    0.3457    0.4082    0.0186    0.5094
      0.3885    0.9595    0.7908    0.5674    0.3512    0.8005    0.6864    0.0413
      0.1448    0.0409    0.9149    0.3132    0.0641    0.6543    0.0289    0.7409
      0.8040    0.0294    0.8094    0.3423    0.5661    0.9904    0.2149    0.1598
      0.4714    0.7196    0.0752    0.1828    0.2469    0.7306    0.0480    0.1919
   
   C = 
   
      1.1941    1.7614    1.3221    1.1026    1.1471    1.9682    0.8267    0.8571
      1.0674    1.9607    1.0355    0.9566    1.0024    2.1026    0.7644    1.2835
      1.9980    2.8931    2.3172    1.7662    2.0706    3.2370    1.4606    1.4903
      1.8088    1.2717    2.5142    1.5974    1.7518    2.3873    0.6850    1.5234
      1.3983    2.2381    0.8759    1.3323    1.4336    2.0294    0.8702    1.4050
      2.0503    1.8930    2.1697    1.6305    1.8463    2.6462    1.0141    1.2046
      1.9773    1.9631    2.6207    1.7514    1.7227    2.8437    0.9558    1.6701
      1.8662    1.8768    1.8779    1.3640    1.7379    2.4178    0.9201    1.0075
   
   D = 
   
      1.1941    1.7614    1.3221    1.1026    1.1471    1.9682    0.8267    0.8571
      1.0674    1.9607    1.0355    0.9566    1.0024    2.1026    0.7644    1.2835
      1.9980    2.8931    2.3172    1.7662    2.0706    3.2370    1.4606    1.4903
      1.8088    1.2717    2.5142    1.5974    1.7518    2.3873    0.6850    1.5234
      1.3983    2.2381    0.8759    1.3323    1.4336    2.0294    0.8702    1.4050
      2.0503    1.8930    2.1697    1.6305    1.8463    2.6462    1.0141    1.2046
      1.9773    1.9631    2.6207    1.7514    1.7227    2.8437    0.9558    1.6701
      1.8662    1.8768    1.8779    1.3640    1.7379    2.4178    0.9201    1.0075
   


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

   
      0.6884    0.0197    0.3017    0.8379    0.2086    0.3047
      0.4823    0.4600    0.1167    0.6326    0.8295    0.9897
      0.8786    0.1555    0.8897    0.2536    0.2537    0.3667
      0.5961    0.2069    0.5596    0.8508    0.1823    0.6838
      0.1246    0.5189    0.7633    0.9308    0.2081    0.8727
   
   
      0.6884
      0.8786
      0.5961
      0.5189
      0.8897
      0.5596
      0.7633
      0.8379
      0.6326
      0.8508
      0.9308
      0.8295
      0.9897
      0.6838
      0.8727
   

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

   
      0.4021    6.9226    2.1663    9.9164    4.4022    0.6647
      1.8765    8.3907    0.6771    3.5320    2.9286    6.9448
      1.5327    9.5204    6.2044    3.8501    2.3423    7.8375
      7.5894    5.8545    9.7803    7.5108    8.0394    4.3874
      1.1137    5.2027    0.6531    5.0350    4.1505    9.7981
   
   
      0.0000    6.9226    0.0000    9.9164    0.0000    0.0000
      0.0000    8.3907    0.0000    0.0000    0.0000    6.9448
      0.0000    9.5204    6.2044    0.0000    0.0000    7.8375
      7.5894    5.8545    9.7803    7.5108    8.0394    0.0000
      0.0000    5.2027    0.0000    5.0350    0.0000    9.7981
   
   
      0.0000    6.9226    0.0000       NaN    0.0000    0.0000
      0.0000    8.3907    0.0000    0.0000    0.0000    6.9448
      0.0000       NaN    6.2044    0.0000    0.0000    7.8375
      7.5894    5.8545       NaN    7.5108    8.0394    0.0000
      0.0000    5.2027    0.0000    5.0350    0.0000       NaN
   

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

   
      4.8664    2.3289    8.2590    4.5073    8.9820    4.2702
      8.8466    8.8105    9.5956    6.5000    1.5393    3.2043
      2.9839    6.5000    3.6221    2.5278    3.5625    8.5322
      4.3965    9.6123    6.5000    6.5000    6.5000    6.5000
      0.0608    3.6377    0.0120    0.1608    6.5000    3.6793
   
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
   
