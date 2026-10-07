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
      0.3267    0.8743    0.3585    0.1011
   
   R1[2] = 0.3584604858731507
   C1 = 
      0.6778
      0.4317
      0.1309
      0.6493
      0.2920
      0.7545
      0.3786
      0.7151
   
   C1[5] = 0.7544782919661415

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
      0.8407    0.6880    0.3603    0.1482    0.7168
      0.3211    0.8171    0.1601    0.7024    0.4407
   

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
   
      0.8499    0.2603    0.9246    0.9927    0.2551    0.3631    0.9880    0.8016
      0.4328    0.0354    0.9944    0.3455    0.7684    0.6490    0.9383    0.7189
      0.2985    0.3895    0.7977    0.4707    0.7379    0.1802    0.3051    0.2218
      0.1818    0.6763    0.1166    0.8698    0.9054    0.1637    0.2057    0.7857
      0.1063    0.4441    0.0734    0.4247    0.0565    0.7030    0.9386    0.0109
      0.1439    0.0662    0.5895    0.8397    0.5076    0.9690    0.7190    0.3734
      0.4170    0.1369    0.2678    0.0692    0.4943    0.6140    0.8105    0.5177
      0.4332    0.3004    0.9104    0.8121    0.0155    0.7743    0.0871    0.1363
   
   B = 
   
      0.1740    0.5608    0.4071    0.1520    0.3532    0.0529    0.9680    0.3357
      0.0666    0.9320    0.3688    0.1058    0.7731    0.9423    0.2772    0.5251
      0.9854    0.0836    0.4113    0.1867    0.7582    0.6874    0.6063    0.2101
      0.8630    0.6094    0.8187    0.5776    0.7208    0.3359    0.3009    0.0574
      0.9773    0.8853    0.4458    0.6864    0.5542    0.7849    0.7737    0.7282
      0.9152    0.4955    0.7088    0.1718    0.3347    0.2117    0.0739    0.2572
      0.3868    0.5558    0.7792    0.3612    0.0226    0.1360    0.6100    0.8430
      0.7223    0.3296    0.2031    0.8844    0.2424    0.7750    0.8601    0.5735
   
   C = 
   
      3.4759    2.6207    2.9388    2.2061    2.3976    2.2920    3.2705    2.2450
      3.5828    2.3297    2.5608    2.0684    2.0218    2.2811    2.9687    2.3224
      2.4343    1.8692    1.7180    1.3512    1.8806    1.9200    1.9832    1.4674
      2.6239    2.5280    1.9230    2.0421    2.0541    2.4014    2.2097    1.8160
      1.5565    1.6622    1.8421    0.8304    1.0333    0.9465    1.0760    1.3282
      3.2657    2.1556    2.5621    1.7288    1.8666    1.7480    1.9918    1.6942
      2.1377    1.7890    1.7793    1.3632    1.1293    1.3878    1.9923    1.7701
      2.5493    1.5847    1.9778    1.0324    1.9637    1.4982    1.5384    0.9030
   
   D = 
   
      3.4759    2.6207    2.9388    2.2061    2.3976    2.2920    3.2705    2.2450
      3.5828    2.3297    2.5608    2.0684    2.0218    2.2811    2.9687    2.3224
      2.4343    1.8692    1.7180    1.3512    1.8806    1.9200    1.9832    1.4674
      2.6239    2.5280    1.9230    2.0421    2.0541    2.4014    2.2097    1.8160
      1.5565    1.6622    1.8421    0.8304    1.0333    0.9465    1.0760    1.3282
      3.2657    2.1556    2.5621    1.7288    1.8666    1.7480    1.9918    1.6942
      2.1377    1.7890    1.7793    1.3632    1.1293    1.3878    1.9923    1.7701
      2.5493    1.5847    1.9778    1.0324    1.9637    1.4982    1.5384    0.9030
   


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

   
      0.1117    0.8089    0.7407    0.6162    0.3571    0.0146
      0.7572    0.3468    0.9984    0.2686    0.1125    0.9765
      0.3071    0.8671    0.4561    0.7825    0.4297    0.4335
      0.8892    0.2681    0.1174    0.6479    0.6105    0.0211
      0.4912    0.8482    0.0008    0.6385    0.7422    0.4263
   
   
      0.7572
      0.8892
      0.8089
      0.8671
      0.8482
      0.7407
      0.9984
      0.6162
      0.7825
      0.6479
      0.6385
      0.6105
      0.7422
      0.9765
   

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

   
      3.1268    3.3436    9.0492    0.1926    5.5323    0.8430
      5.5983    5.7345    2.9425    0.1788    1.4055    3.3915
      9.1497    1.3142    1.7399    2.5188    9.1827    4.5602
      4.3020    4.5666    7.5268    0.9683    7.6487    3.6782
      7.6211    3.1623    1.3816    0.6419    1.2474    0.9962
   
   
      0.0000    0.0000    9.0492    0.0000    5.5323    0.0000
      5.5983    5.7345    0.0000    0.0000    0.0000    0.0000
      9.1497    0.0000    0.0000    0.0000    9.1827    0.0000
      0.0000    0.0000    7.5268    0.0000    7.6487    0.0000
      7.6211    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000       NaN    0.0000    5.5323    0.0000
      5.5983    5.7345    0.0000    0.0000    0.0000    0.0000
         NaN    0.0000    0.0000    0.0000       NaN    0.0000
      0.0000    0.0000    7.5268    0.0000    7.6487    0.0000
      7.6211    0.0000    0.0000    0.0000    0.0000    0.0000
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      1.4329    3.0589    1.7005    9.6128    6.5000    6.5000
      6.5000    6.5000    8.4630    9.2668    6.5000    1.0998
      3.9934    2.2244    8.2328    4.4202    6.5000    3.5201
      9.9842    2.0565    9.0211    1.5676    6.5000    4.7958
      1.7182    2.9158    1.7103    6.5000    3.6136    1.7512
   
Advantages
^^^^^^^^^^


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
   
