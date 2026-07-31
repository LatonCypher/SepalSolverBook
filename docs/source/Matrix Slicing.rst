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
      0.8054    0.6608    0.5298    0.8691
   
   R1[2] = 0.5298184183960571
   C1 = 
      0.3039
      0.6874
      0.1345
      0.5959
      0.6527
      0.5484
      0.5799
      0.0096
   
   C1[5] = 0.548435097477091

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
      0.1296    0.0584    0.2067    0.1977    0.9301
      0.3116    0.0124    0.3049    0.2589    0.1498
   

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
   
      0.7112    0.5812    0.7672    0.7301    0.5267    0.2506    0.4076    0.4657
      0.7447    0.9686    0.0099    0.7334    0.4168    0.1181    0.2301    0.0681
      0.6712    0.3674    0.5006    0.9312    0.0353    0.0099    0.5685    0.1507
      0.6452    0.7424    0.4480    0.2324    0.1452    0.4186    0.4266    0.9750
      0.9357    0.2871    0.1370    0.7474    0.9169    0.8823    0.5553    0.4250
      0.7234    0.2825    0.4391    0.9543    0.1963    0.1461    0.8971    0.8311
      0.5221    0.9778    0.9582    0.8428    0.6394    0.5472    0.7063    0.9168
      0.5647    0.0863    0.0347    0.4505    0.5959    0.8780    0.7859    0.4354
   
   B = 
   
      0.4278    0.4152    0.8254    0.0108    0.5877    0.6569    0.2196    0.0998
      0.2562    0.2822    0.1231    0.5756    0.3239    0.3877    0.2396    0.1871
      0.3344    0.2033    0.3918    0.8048    0.5762    0.4642    0.4876    0.7022
      0.9314    0.3751    0.3013    0.0927    0.3493    0.5962    0.1443    0.3220
      0.4744    0.6787    0.4385    0.6366    0.2051    0.8969    0.2631    0.6819
      0.5578    0.0181    0.6519    0.3394    0.1473    0.5301    0.6869    0.9632
      0.3842    0.2249    0.5903    0.6528    0.5717    0.6877    0.4042    0.7494
      0.1637    0.0122    0.8431    0.5055    0.6348    0.7985    0.8538    0.5415
   
   C = 
   
      2.0123    1.3485    2.2067    1.9491    1.9769    2.7413    1.6480    2.1116
      1.6164    1.1972    1.4117    1.1316    1.2909    1.9555    0.8482    1.1059
      1.6814    0.9873    1.5605    1.1810    1.5566    1.9191    0.9885    1.3283
      1.4585    0.8696    2.2798    1.8223    1.9132    2.4822    1.9023    1.9426
      2.4259    1.5461    2.7498    1.8153    1.8882    3.2464    1.8835    2.6052
      2.0729    1.1751    2.5030    1.7926    2.2053    2.8916    1.8027    2.1376
      2.6093    1.6175    3.0076    2.9349    2.6675    3.7506    2.5502    3.1680
      1.8406    1.0373    2.2907    1.5359    1.5144    2.5770    1.6759    2.3186
   
   D = 
   
      2.0123    1.3485    2.2067    1.9491    1.9769    2.7413    1.6480    2.1116
      1.6164    1.1972    1.4117    1.1316    1.2909    1.9555    0.8482    1.1059
      1.6814    0.9873    1.5605    1.1810    1.5566    1.9191    0.9885    1.3283
      1.4585    0.8696    2.2798    1.8223    1.9132    2.4822    1.9023    1.9426
      2.4259    1.5461    2.7498    1.8153    1.8882    3.2464    1.8835    2.6052
      2.0729    1.1751    2.5030    1.7926    2.2053    2.8916    1.8027    2.1376
      2.6093    1.6175    3.0076    2.9349    2.6675    3.7506    2.5502    3.1680
      1.8406    1.0373    2.2907    1.5359    1.5144    2.5770    1.6759    2.3186
   


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

   
      0.2185    0.0038    0.6867    0.5119    0.0895    0.5857
      0.3881    0.1589    0.3413    0.7449    0.1755    0.0617
      0.9171    0.4583    0.0067    0.7843    0.4970    0.2124
      0.7548    0.4963    0.2005    0.4011    0.0780    0.3968
      0.5700    0.2458    0.5428    0.9674    0.1107    0.0889
   
   
      0.9171
      0.7548
      0.5700
      0.6867
      0.5428
      0.5119
      0.7449
      0.7843
      0.9674
      0.5857
   

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

   
      0.9414    4.4475    8.7134    0.2232    3.1611    7.4117
      0.4883    7.1164    5.0270    6.5671    4.8842    5.2055
      3.2248    4.6312    2.0828    7.8242    2.1248    2.0824
      4.0245    1.9300    9.4332    5.2815    0.3515    0.1824
      4.4408    7.7518    6.7712    6.9584    9.2175    0.5539
   
   
      0.0000    0.0000    8.7134    0.0000    0.0000    7.4117
      0.0000    7.1164    5.0270    6.5671    0.0000    5.2055
      0.0000    0.0000    0.0000    7.8242    0.0000    0.0000
      0.0000    0.0000    9.4332    5.2815    0.0000    0.0000
      0.0000    7.7518    6.7712    6.9584    9.2175    0.0000
   
   
      0.0000    0.0000    8.7134    0.0000    0.0000    7.4117
      0.0000    7.1164    5.0270    6.5671    0.0000    5.2055
      0.0000    0.0000    0.0000    7.8242    0.0000    0.0000
      0.0000    0.0000       NaN    5.2815    0.0000    0.0000
      0.0000    7.7518    6.7712    6.9584       NaN    0.0000
   

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

   
      8.1287    6.5000    6.5000    6.5000    6.5000    6.5000
      0.4941    3.2898    9.0007    6.5000    4.0096    6.5000
      6.5000    9.1980    6.5000    1.9017    6.5000    3.4051
      6.5000    8.9736    6.5000    4.5762    6.5000    1.1079
      2.6491    1.6075    2.5634    6.5000    6.5000    3.7474
   
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
   
