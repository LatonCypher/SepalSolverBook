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
      0.9518    0.1906    0.8267    0.6522
   
   R1[2] = 0.8266917077928168
   C1 = 
      0.9102
      0.8731
      0.3247
      0.0428
      0.5309
      0.3669
      0.8530
      0.9167
   
   C1[5] = 0.3669071763480076

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
      0.8663    0.2256    0.7217    0.9083    0.7648
      0.0276    0.3859    0.7069    0.1963    0.0469
   

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
   
      0.8551    0.0761    0.6293    0.8522    0.1360    0.1410    0.9612    0.4351
      0.5188    0.9232    0.1056    0.3502    0.6191    0.5604    0.6181    0.1282
      0.2026    0.6585    0.9655    0.3201    0.4366    0.1065    0.3530    0.9697
      0.7568    0.8777    0.0226    0.0781    0.5772    0.4979    0.0289    0.5157
      0.1843    0.0033    0.0405    0.6260    0.6016    0.5676    0.5422    0.8922
      0.2991    0.0837    0.7107    0.9758    0.7412    0.2176    0.5318    0.2823
      0.8130    0.1465    0.3146    0.6608    0.8578    0.0898    0.3640    0.5442
      0.0546    0.0234    0.4066    0.7796    0.7708    0.9663    0.8884    0.8534
   
   B = 
   
      0.4354    0.0794    0.2684    0.7701    0.9507    0.7255    0.1332    0.0873
      0.8542    0.1516    0.8962    0.3462    0.2801    0.3123    0.7365    0.2326
      0.0454    0.1743    0.7443    0.6874    0.3462    0.5870    0.7376    0.2719
      0.7976    0.4776    0.9089    0.4019    0.6211    0.9526    0.0127    0.7700
      0.6325    0.4533    0.7268    0.0069    0.5870    0.5497    0.6882    0.8470
      0.8527    0.1538    0.1206    0.6187    0.3027    0.8421    0.9231    0.3175
      0.0911    0.2423    0.8355    0.3444    0.6806    0.4479    0.3959    0.1105
      0.1765    0.9434    0.1896    0.1956    0.5383    0.3211    0.6507    0.0593
   
   C = 
   
      1.5163    1.3230    2.5422    1.9643    2.5924    2.5891    1.5324    1.2117
      2.2469    1.0044    2.4218    1.5214    2.0287    2.1906    2.1028    1.3366
      1.5201    1.6518    2.4631    1.5566    1.9610    2.0235    2.3977    1.1800
      2.0258    1.0662    1.6790    1.3564    1.8085    1.8259    1.9688    1.0173
      1.6555    1.6542    1.7794    1.1390    1.9532    2.0928    1.7979    1.3125
      1.7649    1.3910    2.6339    1.5181    2.1749    2.5094    1.7433    1.7626
      1.7688    1.4614    2.2258    1.4519    2.4047    2.3346    1.6280    1.5270
      2.2271    1.9695    2.6278    1.7190    2.4925    2.9377    2.6638    1.8296
   
   D = 
   
      1.5163    1.3230    2.5422    1.9643    2.5924    2.5891    1.5324    1.2117
      2.2469    1.0044    2.4218    1.5214    2.0287    2.1906    2.1028    1.3366
      1.5201    1.6518    2.4631    1.5566    1.9610    2.0235    2.3977    1.1800
      2.0258    1.0662    1.6790    1.3564    1.8085    1.8259    1.9688    1.0173
      1.6555    1.6542    1.7794    1.1390    1.9532    2.0928    1.7979    1.3125
      1.7649    1.3910    2.6339    1.5181    2.1749    2.5094    1.7433    1.7626
      1.7688    1.4614    2.2258    1.4519    2.4047    2.3346    1.6280    1.5270
      2.2271    1.9695    2.6278    1.7190    2.4925    2.9377    2.6638    1.8296
   


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

   
      0.1573    0.0484    0.9536    0.8788    0.0536    0.3103
      0.5380    0.7337    0.7128    0.2398    0.4030    0.6069
      0.3058    0.9932    0.3045    0.2565    0.4780    0.9891
      0.2721    0.7950    0.9860    0.8909    0.6327    0.8077
      0.6512    0.2460    0.6701    0.9169    0.1332    0.8211
   
   
      0.5380
      0.6512
      0.7337
      0.9932
      0.7950
      0.9536
      0.7128
      0.9860
      0.6701
      0.8788
      0.8909
      0.9169
      0.6327
      0.6069
      0.9891
      0.8077
      0.8211
   

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

   
      6.9142    2.5118    4.1813    6.9709    8.3109    2.1042
      4.0185    8.6737    6.0564    0.1855    4.7163    5.1295
      7.7126    2.1238    7.6275    2.0300    4.7269    2.1328
      1.8453    5.8209    3.9955    6.2463    7.1404    8.0415
      0.0665    7.3222    8.8660    2.0110    9.1468    1.3297
   
   
      6.9142    0.0000    0.0000    6.9709    8.3109    0.0000
      0.0000    8.6737    6.0564    0.0000    0.0000    5.1295
      7.7126    0.0000    7.6275    0.0000    0.0000    0.0000
      0.0000    5.8209    0.0000    6.2463    7.1404    8.0415
      0.0000    7.3222    8.8660    0.0000    9.1468    0.0000
   
   
      6.9142    0.0000    0.0000    6.9709    8.3109    0.0000
      0.0000    8.6737    6.0564    0.0000    0.0000    5.1295
      7.7126    0.0000    7.6275    0.0000    0.0000    0.0000
      0.0000    5.8209    0.0000    6.2463    7.1404    8.0415
      0.0000    7.3222    8.8660    0.0000       NaN    0.0000
   

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

   
      0.9839    0.4510    4.9214    3.6995    3.0930    8.0061
      0.7973    6.5000    8.5254    3.8259    3.9841    6.5000
      0.1173    1.7342    9.5077    3.5693    9.2449    6.5000
      6.5000    6.5000    2.9765    3.5312    1.1246    3.0823
      4.0813    3.9659    8.7259    0.5937    9.6858    3.3963
   
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
   
