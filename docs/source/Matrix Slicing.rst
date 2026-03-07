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
      0.7514    0.7209    0.2819    0.0391
   
   R1[2] = 0.2819054379985225
   C1 = 
      0.2981
      0.1303
      0.4199
      0.7835
      0.2465
      0.9684
      0.5820
      0.8506
   
   C1[5] = 0.9684248075014388

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
      0.4264    0.9080    0.7554    0.8712    0.5665
      0.8956    0.5134    0.3893    0.2564    0.3989
   

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
   
      0.1375    0.9809    0.0107    0.9818    0.1772    0.7721    0.4391    0.8027
      0.6270    0.1329    0.2144    0.6825    0.1353    0.9352    0.5312    0.9971
      0.6032    0.3419    0.7055    0.3987    0.6265    0.0039    0.4151    0.4361
      0.6687    0.2193    0.5176    0.7372    0.6638    0.3771    0.4357    0.6817
      0.9083    0.0612    0.2902    0.9988    0.6803    0.9571    0.6690    0.5085
      0.5616    0.2794    0.0693    0.3223    0.2363    0.5828    0.5899    0.1611
      0.4728    0.0356    0.4177    0.2569    0.1842    0.9871    0.2877    0.0155
      0.0185    0.3611    0.8749    0.4596    0.1969    0.5736    0.2541    0.7981
   
   B = 
   
      0.1280    0.7859    0.3520    0.2089    0.2858    0.0681    0.2994    0.6084
      0.5351    0.1704    0.8499    0.6261    0.6908    0.2651    0.0319    0.2840
      0.9082    0.4994    0.9674    0.7013    0.8231    0.9654    0.6623    0.9524
      0.1202    0.4161    0.4805    0.0683    0.2660    0.2798    0.3258    0.0053
      0.5433    0.5797    0.1994    0.1381    0.5975    0.3816    0.8246    0.6528
      0.1827    0.7634    0.4624    0.4108    0.3232    0.8062    0.8962    0.4092
      0.2825    0.3754    0.6837    0.1623    0.4680    0.3754    0.5452    0.8698
      0.2178    0.7486    0.3197    0.9567    0.8029    0.3407    0.5989    0.7880
   
   C = 
   
      1.2064    2.1470    2.3133    1.8982    2.1923    1.6828    1.9576    1.8238
      1.0396    2.6447    2.0104    1.8541    2.0614    1.8206    2.3928    2.3458
      1.5021    1.8991    1.9269    1.4347    2.0153    1.4709    1.7962    2.2534
      1.4627    2.4748    2.0994    1.6599    2.2346    1.7628    2.3214    2.4699
      1.3767    3.0416    2.3305    1.5819    2.2436    2.0932    2.8795    2.6700
      0.7596    1.5817    1.4285    0.8847    1.2313    1.1058    1.4632    1.5216
      0.8548    1.6730    1.4190    0.9240    1.1481    1.4961    1.7057    1.4836
      1.5027    1.9491    2.1140    1.9425    2.1599    1.9751    2.0392    2.1627
   
   D = 
   
      1.2064    2.1470    2.3133    1.8982    2.1923    1.6828    1.9576    1.8238
      1.0396    2.6447    2.0104    1.8541    2.0614    1.8206    2.3928    2.3458
      1.5021    1.8991    1.9269    1.4347    2.0153    1.4709    1.7962    2.2534
      1.4627    2.4748    2.0994    1.6599    2.2346    1.7628    2.3214    2.4699
      1.3767    3.0416    2.3305    1.5819    2.2436    2.0932    2.8795    2.6700
      0.7596    1.5817    1.4285    0.8847    1.2313    1.1058    1.4632    1.5216
      0.8548    1.6730    1.4190    0.9240    1.1481    1.4961    1.7057    1.4836
      1.5027    1.9491    2.1140    1.9425    2.1599    1.9751    2.0392    2.1627
   


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

   
      0.9227    0.3788    0.8914    0.5895    0.7591    0.2284
      0.1929    0.1388    0.0963    0.8000    0.4641    0.5596
      0.6296    0.6516    0.8046    0.1208    0.0423    0.3767
      0.9389    0.3767    0.2657    0.9999    0.0668    0.6183
      0.1458    0.8095    0.6765    0.1671    0.1390    0.2294
   
   
      0.9227
      0.6296
      0.9389
      0.6516
      0.8095
      0.8914
      0.8046
      0.6765
      0.5895
      0.8000
      0.9999
      0.7591
      0.5596
      0.6183
   

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

   
      7.4248    3.9503    6.4218    4.7344    2.7007    4.0344
      1.5626    7.4861    1.5082    9.2537    0.1859    2.9431
      4.3768    6.2492    3.9448    3.5713    8.9276    4.1308
      0.7013    5.4799    3.0487    4.7874    9.4657    5.0949
      1.5833    1.9974    3.0104    3.5663    8.4084    7.6981
   
   
      7.4248    0.0000    6.4218    0.0000    0.0000    0.0000
      0.0000    7.4861    0.0000    9.2537    0.0000    0.0000
      0.0000    6.2492    0.0000    0.0000    8.9276    0.0000
      0.0000    5.4799    0.0000    0.0000    9.4657    5.0949
      0.0000    0.0000    0.0000    0.0000    8.4084    7.6981
   
   
      7.4248    0.0000    6.4218    0.0000    0.0000    0.0000
      0.0000    7.4861    0.0000       NaN    0.0000    0.0000
      0.0000    6.2492    0.0000    0.0000    8.9276    0.0000
      0.0000    5.4799    0.0000    0.0000       NaN    5.0949
      0.0000    0.0000    0.0000    0.0000    8.4084    7.6981
   

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

   
      6.5000    9.9057    0.3938    8.4330    3.6113    4.8145
      6.5000    6.5000    9.6406    3.1199    4.8902    6.5000
      9.4533    4.1513    8.2336    6.5000    8.7622    3.6011
      0.8940    6.5000    1.3653    6.5000    8.2370    8.3801
      0.9959    6.5000    9.9981    1.3565    8.0308    4.0623
   
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
   
