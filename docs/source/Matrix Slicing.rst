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
      0.5300    0.4366    0.1373    0.3516
   
   R1[2] = 0.13727127470611555
   C1 = 
      0.6309
      0.0186
      0.5375
      0.4474
      0.3695
      0.0013
      0.6458
      0.1374
   
   C1[5] = 0.0012534637562905404

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
      0.1878    0.3219    0.0624    0.8151    0.0885
      0.9053    0.3460    0.8636    0.7709    0.4428
   

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
   
      0.7849    0.4237    0.2278    0.6385    0.3124    0.4279    0.9752    0.7367
      0.2229    0.8219    0.3907    0.4125    0.0084    0.4101    0.7346    0.0647
      0.7159    0.4868    0.4520    0.5089    0.5709    0.4605    0.1412    0.9472
      0.3290    0.0868    0.4223    0.9708    0.8267    0.1099    0.4662    0.9014
      0.1734    0.2416    0.9548    0.5713    0.6523    0.7604    0.0345    0.6969
      0.3144    0.6621    0.4206    0.9592    0.7987    0.4073    0.1256    0.3426
      0.5914    0.5287    0.5011    0.3017    0.7297    0.0242    0.3120    0.2795
      0.0079    0.8635    0.5318    0.9970    0.8929    0.4056    0.6701    0.0520
   
   B = 
   
      0.7675    0.4086    0.3109    0.6757    0.7725    0.8368    0.3126    0.6683
      0.1457    0.4870    0.7664    0.9810    0.1372    0.9713    0.9020    0.6004
      0.8321    0.6188    0.8806    0.9517    0.1442    0.2161    0.5016    0.3750
      0.9521    0.9856    0.4214    0.8361    0.1358    0.4459    0.2876    0.8771
      0.1700    0.6039    0.4787    0.5824    0.9494    0.7781    0.8321    0.8085
      0.4586    0.6288    0.6830    0.4315    0.2758    0.4042    0.5847    0.7982
      0.3872    0.0435    0.2099    0.7203    0.3660    0.1633    0.3980    0.0169
      0.8692    0.8504    0.8181    0.7461    0.7390    0.4095    0.7534    0.4682
   
   C = 
   
      2.7289    2.4240    2.2876    3.3153    2.0999    2.2793    2.3789    2.3799
      1.5389    1.4896    1.7083    2.4329    0.8350    1.5719    1.7135    1.5275
      2.6672    2.7568    2.6005    3.1566    2.1747    2.4377    2.5501    2.6615
      2.6957    2.7499    2.2558    3.0582    2.1106    2.0166    2.2890    2.4677
      2.5855    2.8087    2.7297    2.9936    1.7390    1.9468    2.4417    2.5814
      2.2699    2.6917    2.3469    3.0513    1.6942    2.3717    2.3932    2.7400
      1.7342    1.8138    1.8175    2.5162    1.6628    1.9942    1.9561    1.9107
      2.1661    2.6031    2.4403    3.4087    1.5798    2.3942    2.6208    2.6789
   
   D = 
   
      2.7289    2.4240    2.2876    3.3153    2.0999    2.2793    2.3789    2.3799
      1.5389    1.4896    1.7083    2.4329    0.8350    1.5719    1.7135    1.5275
      2.6672    2.7568    2.6005    3.1566    2.1747    2.4377    2.5501    2.6615
      2.6957    2.7499    2.2558    3.0582    2.1106    2.0166    2.2890    2.4677
      2.5855    2.8087    2.7297    2.9936    1.7390    1.9468    2.4417    2.5814
      2.2699    2.6917    2.3469    3.0513    1.6942    2.3717    2.3932    2.7400
      1.7342    1.8138    1.8175    2.5162    1.6628    1.9942    1.9561    1.9107
      2.1661    2.6031    2.4403    3.4087    1.5798    2.3942    2.6208    2.6789
   


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

   
      0.9505    0.3272    0.2673    0.5034    0.0826    0.0245
      0.9653    0.5618    0.9705    0.8864    0.7288    0.7586
      0.1021    0.6323    0.3217    0.1797    0.4835    0.6075
      0.5703    0.9250    0.1205    0.1793    0.4421    0.4918
      0.9222    0.0806    0.7392    0.7877    0.6120    0.5437
   
   
      0.9505
      0.9653
      0.5703
      0.9222
      0.5618
      0.6323
      0.9250
      0.9705
      0.7392
      0.5034
      0.8864
      0.7877
      0.7288
      0.6120
      0.7586
      0.6075
      0.5437
   

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

   
      6.4303    9.2682    9.7323    1.9886    6.5000    4.6683
      4.5877    0.5574    6.7259    5.0457    9.5334    3.0171
      7.5228    7.7142    2.0058    9.3432    5.1573    9.5219
      8.9299    7.2270    8.8846    0.3123    0.4344    2.0315
      5.3106    9.5277    4.2296    9.1799    0.2399    8.6159
   
   
      6.4303    9.2682    9.7323    0.0000    6.5000    0.0000
      0.0000    0.0000    6.7259    5.0457    9.5334    0.0000
      7.5228    7.7142    0.0000    9.3432    5.1573    9.5219
      8.9299    7.2270    8.8846    0.0000    0.0000    0.0000
      5.3106    9.5277    0.0000    9.1799    0.0000    8.6159
   
   
      6.4303       NaN       NaN    0.0000    6.5000    0.0000
      0.0000    0.0000    6.7259    5.0457       NaN    0.0000
      7.5228    7.7142    0.0000       NaN    5.1573       NaN
      8.9299    7.2270    8.8846    0.0000    0.0000    0.0000
      5.3106       NaN    0.0000       NaN    0.0000    8.6159
   

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

   
      4.2009    6.5000    6.5000    6.5000    8.3840    2.2266
      2.0340    6.5000    6.5000    0.6268    9.5493    6.5000
      6.5000    8.8468    0.3064    1.5290    1.4937    4.3227
      0.9325    9.4532    2.4692    0.1540    9.2095    6.5000
      4.4185    3.0169    8.2821    0.2825    8.6368    8.9309
   
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
   
