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
      0.8622    0.9347    0.8913    0.5413
   
   R1[2] = 0.8913055791267875
   C1 = 
      0.7933
      0.0843
      0.2501
      0.2885
      0.1416
      0.7271
      0.1590
      0.2324
   
   C1[5] = 0.7271448111826996

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
      0.3252    0.9103    0.6406    0.0407    0.1790
      0.0640    0.2875    0.1154    0.0536    0.3983
   

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
   
      0.3440    0.0150    0.2008    0.4610    0.8264    0.0407    0.3147    0.9544
      0.6808    0.4821    0.1905    0.8984    0.8374    0.8121    0.8447    0.0057
      0.4582    0.9034    0.3482    0.4557    0.6552    0.8881    0.3976    0.1144
      0.8715    0.8005    0.6727    0.9693    0.5108    0.4272    0.2705    0.2103
      0.1498    0.6211    0.8457    0.0999    0.2619    0.5623    0.8914    0.0217
      0.6324    0.0153    0.8985    0.4097    0.5990    0.3449    0.9022    0.5758
      0.3511    0.2115    0.3375    0.7877    0.6164    0.1056    0.4088    0.8716
      0.1695    0.5814    0.8960    0.9738    0.6443    0.9835    0.6201    0.9535
   
   B = 
   
      0.7686    0.5995    0.7552    0.4908    0.7594    0.5925    0.3793    0.7616
      0.7214    0.7645    0.8701    0.1084    0.9589    0.4548    0.7728    0.3058
      0.9228    0.9311    0.7565    0.0977    0.9722    0.2203    0.6559    0.8279
      0.1772    0.1112    0.6280    0.7969    0.1918    0.0432    0.8581    0.9831
      0.6232    0.8161    0.4569    0.4539    0.8544    0.2268    0.9813    0.9513
      0.8983    0.7953    0.7189    0.7179    0.1269    0.7545    0.0837    0.4090
      0.2639    0.0668    0.1172    0.3682    0.2070    0.4850    0.9575    0.8175
      0.9908    0.9274    0.3126    0.0575    0.9845    0.1785    0.8911    0.5936
   
   C = 
   
      2.1224    2.0688    1.4562    1.1325    2.2751    0.8159    2.6355    2.5125
      2.6859    2.4449    2.7090    2.3954    2.3357    1.9167    3.2302    3.5295
      2.8304    2.7140    2.7019    1.8080    2.5076    1.8107    2.6914    2.7410
      3.0216    2.8384    3.1102    2.0030    3.0229    1.6774    3.2055    3.4249
      2.2864    2.1038    1.9912    1.1552    2.0517    1.4817    2.3542    2.3231
      2.8907    2.6305    2.2353    1.6112    2.7562    1.5338    3.1863    3.4233
      2.3239    2.1967    1.8770    1.4122    2.4313    0.9859    2.9758    2.8670
      3.9424    3.7223    3.2952    2.2915    3.4863    1.9634    4.0949    4.0941
   
   D = 
   
      2.1224    2.0688    1.4562    1.1325    2.2751    0.8159    2.6355    2.5125
      2.6859    2.4449    2.7090    2.3954    2.3357    1.9167    3.2302    3.5295
      2.8304    2.7140    2.7019    1.8080    2.5076    1.8107    2.6914    2.7410
      3.0216    2.8384    3.1102    2.0030    3.0229    1.6774    3.2055    3.4249
      2.2864    2.1038    1.9912    1.1552    2.0517    1.4817    2.3542    2.3231
      2.8907    2.6305    2.2353    1.6112    2.7562    1.5338    3.1863    3.4233
      2.3239    2.1967    1.8770    1.4122    2.4313    0.9859    2.9758    2.8670
      3.9424    3.7223    3.2952    2.2915    3.4863    1.9634    4.0949    4.0941
   


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

   
      0.3607    0.0969    0.3633    0.0293    0.3021    0.4622
      0.1778    0.8536    0.5506    0.0008    0.0526    0.0813
      0.8394    0.7662    0.8183    0.9220    0.1328    0.8169
      0.5395    0.0941    0.1087    0.2530    0.8857    0.6859
      0.9661    0.9276    0.1078    0.5596    0.1076    0.7265
   
   
      0.8394
      0.5395
      0.9661
      0.8536
      0.7662
      0.9276
      0.5506
      0.8183
      0.9220
      0.5596
      0.8857
      0.8169
      0.6859
      0.7265
   

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

   
      8.9687    4.0269    0.6572    1.9358    6.1332    3.6364
      1.5753    8.2985    7.2424    4.3700    3.2380    5.6980
      3.2998    8.6037    5.9756    3.9173    9.2330    9.1287
      4.7521    9.4871    0.2352    3.3531    2.0350    0.8201
      1.4976    0.4222    6.3588    4.5455    5.5834    1.9807
   
   
      8.9687    0.0000    0.0000    0.0000    6.1332    0.0000
      0.0000    8.2985    7.2424    0.0000    0.0000    5.6980
      0.0000    8.6037    5.9756    0.0000    9.2330    9.1287
      0.0000    9.4871    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.3588    0.0000    5.5834    0.0000
   
   
      8.9687    0.0000    0.0000    0.0000    6.1332    0.0000
      0.0000    8.2985    7.2424    0.0000    0.0000    5.6980
      0.0000    8.6037    5.9756    0.0000       NaN       NaN
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.3588    0.0000    5.5834    0.0000
   

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

   
      6.5000    9.7661    6.5000    6.5000    3.7784    4.9587
      6.5000    6.5000    6.5000    9.2163    6.5000    3.8556
      1.9154    1.9612    2.5097    2.7279    2.8562    1.5920
      9.4930    6.5000    6.5000    1.1359    2.4732    0.3358
      6.5000    8.5053    6.5000    1.1370    9.8648    1.1109
   
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
   
