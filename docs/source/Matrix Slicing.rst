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
      0.9692    0.3769    0.9213    0.2255
   
   R1[2] = 0.9213380313488344
   C1 = 
      0.0754
      0.9211
      0.3441
      0.3620
      0.4519
      0.5286
      0.3877
      0.8580
   
   C1[5] = 0.5286209525631832

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
      0.2324    0.2564    0.0939    0.7400    0.3532
      0.0442    0.9426    0.4152    0.0501    0.0416
   

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
   
      0.5000    0.8388    0.9328    0.9671    0.2220    0.7621    0.3518    0.9440
      0.8187    0.2004    0.3484    0.0291    0.9909    0.9310    0.3951    0.0733
      0.5042    0.9785    0.5481    0.1161    0.2255    0.5578    0.3100    0.0732
      0.5394    0.6612    0.7393    0.0000    0.1633    0.2171    0.6675    0.8365
      0.4557    0.7008    0.0394    0.6287    0.5568    0.2577    0.8802    0.8262
      0.1417    0.5563    0.6213    0.7937    0.2558    0.2094    0.2850    0.4967
      0.4678    0.6786    0.2486    0.4565    0.9528    0.1898    0.1564    0.8514
      0.3080    0.5869    0.8339    0.0512    0.7365    0.7987    0.6314    0.7127
   
   B = 
   
      0.4167    0.0081    0.7616    0.3027    0.1679    0.2368    0.4910    0.8849
      0.8132    0.9962    0.4480    0.6316    0.3614    0.2526    0.4175    0.5922
      0.6941    0.0751    0.2033    0.7817    0.6043    0.8672    0.2308    0.2179
      0.2269    0.0271    0.2221    0.7593    0.2647    0.1246    0.2344    0.9523
      0.4677    0.2564    0.8451    0.2522    0.8458    0.3736    0.2400    0.9279
      0.4137    0.8126    0.5957    0.4864    0.3591    0.1991    0.8407    0.7981
      0.9488    0.8640    0.5556    0.6363    0.8706    0.9944    0.9127    0.5819
      0.3872    0.4972    0.9963    0.5178    0.6450    0.1955    0.7651    0.1016
   
   C = 
   
      2.8757    2.3855    2.9386    3.2839    2.5834    2.0288    2.7750    3.1782
      2.0043    1.6216    2.4750    1.6608    1.9918    1.5131    2.0100    2.8465
      2.0713    1.8386    1.7276    1.8506    1.5084    1.3743    1.6718    2.0979
      2.3989    1.9295    2.3289    2.1634    2.1130    1.8674    2.1824    1.8281
      2.4517    2.2453    2.7449    2.3423    2.3827    1.6935    2.4585    2.7440
      1.7917    1.3525    1.6539    2.0875    1.6705    1.3296    1.5089    1.9669
      2.0251    1.6679    2.6657    1.9841    2.1542    1.2705    1.8598    2.5178
      2.7459    2.3890    2.8375    2.4996    2.7004    2.1521    2.5705    2.6112
   
   D = 
   
      2.8757    2.3855    2.9386    3.2839    2.5834    2.0288    2.7750    3.1782
      2.0043    1.6216    2.4750    1.6608    1.9918    1.5131    2.0100    2.8465
      2.0713    1.8386    1.7276    1.8506    1.5084    1.3743    1.6718    2.0979
      2.3989    1.9295    2.3289    2.1634    2.1130    1.8674    2.1824    1.8281
      2.4517    2.2453    2.7449    2.3423    2.3827    1.6935    2.4585    2.7440
      1.7917    1.3525    1.6539    2.0875    1.6705    1.3296    1.5089    1.9669
      2.0251    1.6679    2.6657    1.9841    2.1542    1.2705    1.8598    2.5178
      2.7459    2.3890    2.8375    2.4996    2.7004    2.1521    2.5705    2.6112
   


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

   
      0.1736    0.8773    0.1374    0.4976    0.7833    0.0388
      0.9989    0.9035    0.9168    0.6433    0.1385    0.8717
      0.2488    0.3310    0.5174    0.3172    0.7011    0.3585
      0.2306    0.4864    0.3699    0.2404    0.3076    0.3734
      0.4924    0.4729    0.8565    0.5013    0.3210    0.8453
   
   
      0.9989
      0.8773
      0.9035
      0.9168
      0.5174
      0.8565
      0.6433
      0.5013
      0.7833
      0.7011
      0.8717
      0.8453
   

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

   
      4.9189    2.4117    2.4679    1.5215    2.6718    7.2291
      9.7145    8.8024    0.7602    2.1185    0.8334    1.6320
      1.0828    7.8585    3.4470    8.8299    5.0790    9.8295
      9.0419    7.4608    3.7789    7.8486    2.2111    7.3990
      3.4784    0.8671    1.4965    7.1826    7.2176    9.0324
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    7.2291
      9.7145    8.8024    0.0000    0.0000    0.0000    0.0000
      0.0000    7.8585    0.0000    8.8299    5.0790    9.8295
      9.0419    7.4608    0.0000    7.8486    0.0000    7.3990
      0.0000    0.0000    0.0000    7.1826    7.2176    9.0324
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    7.2291
         NaN    8.8024    0.0000    0.0000    0.0000    0.0000
      0.0000    7.8585    0.0000    8.8299    5.0790       NaN
         NaN    7.4608    0.0000    7.8486    0.0000    7.3990
      0.0000    0.0000    0.0000    7.1826    7.2176       NaN
   

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

   
      6.5000    4.5623    4.3749    6.5000    1.8686    3.0410
      6.5000    8.3266    3.0708    2.8043    2.9232    6.5000
      6.5000    6.5000    9.4126    1.2510    6.5000    9.6236
      6.5000    6.5000    8.1116    6.5000    6.5000    3.8222
      6.5000    3.2687    1.9882    6.5000    6.5000    9.2298
   
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
   
