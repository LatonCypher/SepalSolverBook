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
      0.3016    0.6240    0.7932    0.8516
   
   R1[2] = 0.7932068715288371
   C1 = 
      0.3061
      0.5751
      0.0672
      0.4903
      0.8746
      0.6970
      0.2029
      0.5616
   
   C1[5] = 0.6970010208883679

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
      0.8722    0.7102    0.0517    0.4550    0.7977
      0.7772    0.6344    0.6734    0.5753    0.3407
   

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
   
      0.3216    0.9498    0.4218    0.6500    0.7622    0.3232    0.1981    0.1283
      0.4962    0.7141    0.4410    0.3894    0.6613    0.5959    0.0937    0.6581
      0.8944    0.2976    0.0871    0.0566    0.0663    0.6014    0.0064    0.1336
      0.8346    0.7058    0.5741    0.5867    0.9436    0.8785    0.9691    0.5312
      0.8076    0.5199    0.2238    0.4318    0.9884    0.6565    0.8368    0.1469
      0.0840    0.6618    0.8151    0.5975    0.8370    0.9669    0.9841    0.2912
      0.8188    0.8133    0.3864    0.3871    0.3028    0.3688    0.2168    0.0476
      0.4182    0.5597    0.8070    0.0460    0.6045    0.7859    0.4364    0.5317
   
   B = 
   
      0.0202    0.6268    0.5575    0.6565    0.8417    0.8955    0.1246    0.6573
      0.0017    0.9939    0.7593    0.8595    0.7191    0.8666    0.2975    0.5346
      0.5985    0.9456    0.6459    0.8248    0.0083    0.9726    0.5705    0.7918
      0.3323    0.9538    0.8484    0.1871    0.0362    0.4166    0.6357    0.6040
      0.3953    0.6849    0.1656    0.1870    0.8774    0.2016    0.8453    0.0814
      0.4685    0.4562    0.7424    0.7821    0.4196    0.7978    0.4272    0.8175
      0.9699    0.2648    0.9038    0.9391    0.1157    0.1893    0.3999    0.0582
      0.8140    0.0514    0.9527    0.5618    0.8995    0.1314    0.0663    0.3371
   
   C = 
   
      1.2259    2.8930    2.3919    2.1507    1.9235    2.2581    1.8466    1.8269
      1.5716    2.5925    2.6975    2.4235    2.3819    2.3672    1.6680    2.0605
      0.5124    1.3211    1.4195    1.4894    1.4011    1.6792    0.6100    1.3927
      2.7136    3.6581    4.0603    3.8101    3.0225    3.3062    2.6102    2.7654
      1.9241    2.8520    2.9032    2.8092    2.4427    2.4721    2.1178    1.9622
      2.6646    3.3409    3.6061    3.4087    2.0909    2.8551    2.5857    2.4294
      0.9193    2.4917    2.2173    2.2032    1.7795    2.3777    1.3139    1.8676
      1.9708    2.5408    2.8029    2.8661    2.1517    2.5650    1.7648    2.1372
   
   D = 
   
      1.2259    2.8930    2.3919    2.1507    1.9235    2.2581    1.8466    1.8269
      1.5716    2.5925    2.6975    2.4235    2.3819    2.3672    1.6680    2.0605
      0.5124    1.3211    1.4195    1.4894    1.4011    1.6792    0.6100    1.3927
      2.7136    3.6581    4.0603    3.8101    3.0225    3.3062    2.6102    2.7654
      1.9241    2.8520    2.9032    2.8092    2.4427    2.4721    2.1178    1.9622
      2.6646    3.3409    3.6061    3.4087    2.0909    2.8551    2.5857    2.4294
      0.9193    2.4917    2.2173    2.2032    1.7795    2.3777    1.3139    1.8676
      1.9708    2.5408    2.8029    2.8661    2.1517    2.5650    1.7648    2.1372
   


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

   
      0.1142    0.0591    0.3594    0.2360    0.0286    0.9428
      0.3632    0.2686    0.7876    0.1962    0.8063    0.9662
      0.0913    0.4967    0.9783    0.0152    0.0776    0.4706
      0.6171    0.2734    0.4572    0.0669    0.7840    0.4175
      0.0196    0.9603    0.4048    0.0472    0.0875    0.8112
   
   
      0.6171
      0.9603
      0.7876
      0.9783
      0.8063
      0.7840
      0.9428
      0.9662
      0.8112
   

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

   
      5.0632    3.9064    1.0438    8.4604    1.6358    9.3921
      2.7422    4.5000    4.9592    7.9670    1.7452    2.2894
      8.4389    7.1812    3.6545    8.4954    2.4993    9.9366
      0.7943    7.8718    1.6230    7.2333    9.2441    6.5607
      2.6948    5.8018    5.9404    7.1086    8.4807    8.3430
   
   
      5.0632    0.0000    0.0000    8.4604    0.0000    9.3921
      0.0000    0.0000    0.0000    7.9670    0.0000    0.0000
      8.4389    7.1812    0.0000    8.4954    0.0000    9.9366
      0.0000    7.8718    0.0000    7.2333    9.2441    6.5607
      0.0000    5.8018    5.9404    7.1086    8.4807    8.3430
   
   
      5.0632    0.0000    0.0000    8.4604    0.0000       NaN
      0.0000    0.0000    0.0000    7.9670    0.0000    0.0000
      8.4389    7.1812    0.0000    8.4954    0.0000       NaN
      0.0000    7.8718    0.0000    7.2333       NaN    6.5607
      0.0000    5.8018    5.9404    7.1086    8.4807    8.3430
   

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

   
      1.0023    0.8748    9.4276    6.5000    8.3798    8.1830
      2.1334    3.8853    4.7494    6.5000    6.5000    9.0497
      8.5125    0.1319    4.2727    3.3251    3.3801    6.5000
      2.7522    2.6571    1.6410    1.7189    4.7882    3.3120
      6.5000    1.5641    4.5248    4.9329    0.7058    6.5000
   
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
   
