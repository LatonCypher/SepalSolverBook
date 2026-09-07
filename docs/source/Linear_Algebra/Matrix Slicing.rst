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
      0.5167    0.5695    0.8442    0.8098
   
   R1[2] = 0.844203420501474
   C1 = 
      0.5200
      0.0816
      0.2739
      0.4222
      0.4962
      0.5978
      0.6635
      0.1890
   
   C1[5] = 0.5977851941894634

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
      0.1964    0.3072    0.8228    0.2855    0.2735
      0.0538    0.5818    0.1014    0.0453    0.4964
   

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
   
      0.1156    0.1616    0.0438    0.6758    0.8133    0.8329    0.4516    0.4356
      0.8238    0.0139    0.7110    0.2472    0.6410    0.7628    0.4385    0.6342
      0.7466    0.4464    0.6787    0.6414    0.3610    0.8346    0.6709    0.5720
      0.0939    0.7828    0.4090    0.2973    0.8935    0.8304    0.4442    0.8866
      0.2819    0.5840    0.7980    0.8663    0.6037    0.7706    0.0481    0.2719
      0.1439    0.5363    0.8797    0.8274    0.9575    0.9041    0.1745    0.0940
      0.8192    0.4869    0.4907    0.2273    0.6599    0.0846    0.0068    0.3232
      0.4320    0.1205    0.3634    0.6125    0.6968    0.1665    0.5264    0.1576
   
   B = 
   
      0.5706    0.9988    0.5686    0.3123    0.9827    0.9792    0.3515    0.3079
      0.5995    0.6799    0.4344    0.3271    0.5810    0.8207    0.8860    0.5675
      0.5263    0.3173    0.4416    0.0512    0.0464    0.1926    0.0730    0.0453
      0.7506    0.8525    0.0800    0.8267    0.9386    0.4346    0.0659    0.5812
      0.7509    0.3249    0.5548    0.2205    0.5545    0.3382    0.0095    0.9740
      0.4473    0.5857    0.2767    0.3999    0.5446    0.4488    0.8738    0.5598
      0.7773    0.6072    0.5352    0.9307    0.1549    0.3606    0.5922    0.7948
      0.7425    0.0511    0.8334    0.8477    0.0892    0.2069    0.9823    0.0632
   
   C = 
   
      2.3509    1.8639    1.4958    1.9518    1.8573    1.4499    1.6624    2.1670
      2.6723    2.2222    2.1382    1.8946    1.9780    1.9110    1.9253    1.8773
      3.1227    2.8539    2.2364    2.4667    2.4361    2.3638    2.4416    2.2748
      3.0071    2.1008    2.3000    2.2461    1.9407    1.9609    2.6440    2.4086
      2.6185    2.3609    1.6363    1.7526    2.2526    1.9092    1.7065    2.0327
      2.8165    2.4442    1.7227    1.7641    2.3291    1.9221    1.6393    2.4527
      1.9667    1.7831    1.5747    1.0876    1.7658    1.7254    1.1718    1.3988
      2.0937    1.8025    1.3533    1.5431    1.6590    1.3910    0.9442    1.7741
   
   D = 
   
      2.3509    1.8639    1.4958    1.9518    1.8573    1.4499    1.6624    2.1670
      2.6723    2.2222    2.1382    1.8946    1.9780    1.9110    1.9253    1.8773
      3.1227    2.8539    2.2364    2.4667    2.4361    2.3638    2.4416    2.2748
      3.0071    2.1008    2.3000    2.2461    1.9407    1.9609    2.6440    2.4086
      2.6185    2.3609    1.6363    1.7526    2.2526    1.9092    1.7065    2.0327
      2.8165    2.4442    1.7227    1.7641    2.3291    1.9221    1.6393    2.4527
      1.9667    1.7831    1.5747    1.0876    1.7658    1.7254    1.1718    1.3988
      2.0937    1.8025    1.3533    1.5431    1.6590    1.3910    0.9442    1.7741
   


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

   
      0.7382    0.4265    0.5198    0.6713    0.9705    0.6988
      0.9684    0.9202    0.7882    0.0284    0.9021    0.6388
      0.6017    0.8064    0.2710    0.6435    0.2870    0.7576
      0.5918    0.1978    0.5931    0.9369    0.9336    0.8177
      0.9770    0.1947    0.2885    0.5852    0.2229    0.2405
   
   
      0.7382
      0.9684
      0.6017
      0.5918
      0.9770
      0.9202
      0.8064
      0.5198
      0.7882
      0.5931
      0.6713
      0.6435
      0.9369
      0.5852
      0.9705
      0.9021
      0.9336
      0.6988
      0.6388
      0.7576
      0.8177
   

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

   
      7.1671    2.8453    2.7859    5.8790    8.8124    8.0725
      9.9632    7.4974    6.3551    8.5863    7.5055    1.2756
      1.9705    1.2898    4.1654    6.4764    4.6197    4.5780
      4.2997    3.0067    3.0691    0.6043    1.6868    3.4760
      2.1250    3.0084    9.9242    6.2485    2.2983    2.1142
   
   
      7.1671    0.0000    0.0000    5.8790    8.8124    8.0725
      9.9632    7.4974    6.3551    8.5863    7.5055    0.0000
      0.0000    0.0000    0.0000    6.4764    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    9.9242    6.2485    0.0000    0.0000
   
   
      7.1671    0.0000    0.0000    5.8790    8.8124    8.0725
         NaN    7.4974    6.3551    8.5863    7.5055    0.0000
      0.0000    0.0000    0.0000    6.4764    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000       NaN    6.2485    0.0000    0.0000
   

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

   
      3.7851    4.9770    6.5000    4.8298    6.5000    2.3262
      6.5000    6.5000    3.2925    6.5000    1.4486    6.5000
      6.5000    0.1304    1.3040    3.3911    3.4132    6.5000
      6.5000    3.3812    0.1412    4.6659    6.5000    9.1675
      1.4766    0.1089    6.5000    0.2767    9.0893    1.4923
   
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
   
