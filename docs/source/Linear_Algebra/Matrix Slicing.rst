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
      0.0153    0.1410    0.4232    0.4100
   
   R1[2] = 0.42318912107611906
   C1 = 
      0.8393
      0.0975
      0.8639
      0.4581
      0.3158
      0.0312
      0.4070
      0.2590
   
   C1[5] = 0.03123303251928078

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
      0.2566    0.6372    0.6917    0.9292    0.4675
      0.7510    0.0161    0.9660    0.4583    0.2070
   

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
   
      0.6462    0.2332    0.1015    0.5274    0.9854    0.4359    0.9227    0.8561
      0.2281    0.0322    0.5555    0.9145    0.2331    0.4752    0.0561    0.4804
      0.7283    0.9297    0.3616    0.4631    0.0624    0.6011    0.3150    0.1628
      0.1250    0.1935    0.4958    0.2520    0.3089    0.5320    0.3156    0.6544
      0.9642    0.5409    0.3672    0.4758    0.2887    0.8421    0.3225    0.9921
      0.5053    0.5244    0.4172    0.6433    0.8727    0.9754    0.9200    0.1545
      0.2955    0.1227    0.0767    0.0098    0.8491    0.9384    0.3439    0.7878
      0.4859    0.5999    0.3328    0.4103    0.1880    0.3621    0.7707    0.8268
   
   B = 
   
      0.2431    0.8400    0.2427    0.7354    0.6143    0.7393    0.3470    0.4687
      0.6106    0.3466    0.8002    0.8080    0.7670    0.8018    0.3215    0.3017
      0.1401    0.5259    0.5893    0.2083    0.1395    0.9719    0.4378    0.9058
      0.5893    0.9300    0.4278    0.4649    0.6813    0.0581    0.4125    0.2719
      0.4464    0.6369    0.5154    0.8558    0.3748    0.9351    0.9088    0.4240
      0.9498    0.4639    0.9846    0.3058    0.9991    0.5105    0.8184    0.4240
      0.2458    0.3393    0.8330    0.8155    0.1199    0.4030    0.4785    0.3993
      0.4123    0.2047    0.3516    0.1870    0.5742    0.3424    0.4869    0.3959
   
   C = 
   
      2.0581    2.4856    2.6354    2.8191    2.3562    2.6029    2.6717    1.9186
      1.4591    1.8316    1.6034    1.2150    1.7099    1.4351    1.5713    1.3814
      1.8116    2.0137    2.2756    2.1019    2.2816    2.2101    1.6796    1.5469
      1.3570    1.3517    1.7611    1.2754    1.5268    1.6557    1.6123    1.3762
      2.3136    2.5201    2.6823    2.3970    2.9405    2.7007    2.4544    2.0782
      2.4864    2.7760    3.2942    3.0055    2.7096    2.9746    2.8987    2.1601
      1.8430    1.5943    2.1444    1.7784    2.0424    2.0734    2.2673    1.4548
      1.7310    1.8912    2.3558    2.1569    2.0839    2.1418    1.9150    1.6901
   
   D = 
   
      2.0581    2.4856    2.6354    2.8191    2.3562    2.6029    2.6717    1.9186
      1.4591    1.8316    1.6034    1.2150    1.7099    1.4351    1.5713    1.3814
      1.8116    2.0137    2.2756    2.1019    2.2816    2.2101    1.6796    1.5469
      1.3570    1.3517    1.7611    1.2754    1.5268    1.6557    1.6123    1.3762
      2.3136    2.5201    2.6823    2.3970    2.9405    2.7007    2.4544    2.0782
      2.4864    2.7760    3.2942    3.0055    2.7096    2.9746    2.8987    2.1601
      1.8430    1.5943    2.1444    1.7784    2.0424    2.0734    2.2673    1.4548
      1.7310    1.8912    2.3558    2.1569    2.0839    2.1418    1.9150    1.6901
   


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

   
      0.7505    0.6675    0.8338    0.4248    0.8065    0.0297
      0.3025    0.7607    0.4342    0.1188    0.5108    0.0694
      0.1606    0.2700    0.7909    0.5238    0.3278    0.7067
      0.7009    0.4771    0.1102    0.9915    0.9423    0.2069
      0.3235    0.9035    0.7083    0.2743    0.5691    0.6875
   
   
      0.7505
      0.7009
      0.6675
      0.7607
      0.9035
      0.8338
      0.7909
      0.7083
      0.5238
      0.9915
      0.8065
      0.5108
      0.9423
      0.5691
      0.7067
      0.6875
   

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

   
      8.9036    8.3633    8.9828    3.2811    3.8967    0.5498
      0.5598    7.7050    1.4232    9.4479    9.5318    6.9198
      9.7455    2.3820    6.6042    5.4824    2.7036    2.6899
      7.7698    5.7406    1.4034    7.2529    0.2394    5.4541
      0.9566    0.1449    0.3855    4.9027    1.3724    5.3750
   
   
      8.9036    8.3633    8.9828    0.0000    0.0000    0.0000
      0.0000    7.7050    0.0000    9.4479    9.5318    6.9198
      9.7455    0.0000    6.6042    5.4824    0.0000    0.0000
      7.7698    5.7406    0.0000    7.2529    0.0000    5.4541
      0.0000    0.0000    0.0000    0.0000    0.0000    5.3750
   
   
      8.9036    8.3633    8.9828    0.0000    0.0000    0.0000
      0.0000    7.7050    0.0000       NaN       NaN    6.9198
         NaN    0.0000    6.6042    5.4824    0.0000    0.0000
      7.7698    5.7406    0.0000    7.2529    0.0000    5.4541
      0.0000    0.0000    0.0000    0.0000    0.0000    5.3750
   

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

   
      6.5000    6.5000    6.5000    6.5000    2.5125    6.5000
      0.8045    0.6701    6.5000    2.3023    1.4888    4.8569
      3.2939    6.5000    3.8557    1.6035    6.5000    1.9180
      9.5826    6.5000    1.3809    6.5000    0.4550    8.6185
      8.3290    8.5088    1.6028    2.5585    6.5000    0.6822
   
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
   
