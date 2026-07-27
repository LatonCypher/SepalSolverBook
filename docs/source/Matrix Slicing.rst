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
      0.3137    0.8333    0.0040    0.9055
   
   R1[2] = 0.0040295553322062805
   C1 = 
      0.0483
      0.6164
      0.3785
      0.6726
      0.7398
      0.5703
      0.6591
      0.4715
   
   C1[5] = 0.5702829454543177

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
      0.6661    0.9276    0.8611    0.6936    0.2690
      0.6423    0.6539    0.3344    0.4014    0.6311
   

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
   
      0.2553    0.3122    0.7505    0.0296    0.2348    0.9027    0.6847    0.3147
      0.5928    0.5236    0.5655    0.9765    0.7870    0.6334    0.0720    0.3960
      0.7784    0.3802    0.5429    0.5521    0.0671    0.9885    0.9392    0.8808
      0.4581    0.9685    0.4579    0.0461    0.7761    0.5390    0.0893    0.7029
      0.0779    0.5332    0.4022    0.5479    0.9719    0.4859    0.7468    0.0033
      0.2208    0.1135    0.4790    0.4985    0.7269    0.2761    0.0109    0.5439
      0.6473    0.7938    0.7451    0.0667    0.5607    0.0023    0.0746    0.9233
      0.2918    0.2424    0.4639    0.1111    0.0736    0.0089    0.1886    0.7782
   
   B = 
   
      0.1881    0.1512    0.3115    0.1907    0.4663    0.3800    0.5154    0.2353
      0.1908    0.3474    0.2879    0.5813    0.3167    0.9398    0.2683    0.6783
      0.6848    0.7399    0.0779    0.7820    0.9866    0.7616    0.7519    0.8821
      0.9249    0.3109    0.6664    0.4141    0.8887    0.9818    0.4535    0.8505
      0.6305    0.4952    0.6671    0.3093    0.5613    0.7553    0.9676    0.3529
      0.6494    0.5959    0.5090    0.6677    0.2077    0.8585    0.8251    0.0953
      0.5391    0.6218    0.1057    0.8458    0.7703    0.3885    0.2529    0.9431
      0.5192    0.9913    0.4984    0.2843    0.1222    0.6955    0.9364    0.5947
   
   C = 
   
      1.9156    2.1035    1.0929    2.1733    1.8698    2.4282    2.2329    1.9608
      2.6538    2.1980    2.0825    2.1039    2.5451    3.5483    2.9872    2.4655
      2.7493    2.9026    1.8484    2.7483    2.5837    3.4854    3.1047    2.9170
      1.8797    2.2168    1.6398    1.9029    1.7153    3.0508    2.7377    2.0353
      2.2312    1.9033    1.5504    2.1238    2.3109    2.8185    2.2673    2.2962
      1.7781    1.6525    1.3686    1.2621    1.5946    2.2133    2.1736    1.5920
      1.7198    2.1865    1.3759    1.6958    1.8331    2.7216    2.5650    2.2223
      1.0795    1.4366    0.7323    1.0148    1.0528    1.4788    1.4696    1.4042
   
   D = 
   
      1.9156    2.1035    1.0929    2.1733    1.8698    2.4282    2.2329    1.9608
      2.6538    2.1980    2.0825    2.1039    2.5451    3.5483    2.9872    2.4655
      2.7493    2.9026    1.8484    2.7483    2.5837    3.4854    3.1047    2.9170
      1.8797    2.2168    1.6398    1.9029    1.7153    3.0508    2.7377    2.0353
      2.2312    1.9033    1.5504    2.1238    2.3109    2.8185    2.2673    2.2962
      1.7781    1.6525    1.3686    1.2621    1.5946    2.2133    2.1736    1.5920
      1.7198    2.1865    1.3759    1.6958    1.8331    2.7216    2.5650    2.2223
      1.0795    1.4366    0.7323    1.0148    1.0528    1.4788    1.4696    1.4042
   


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

   
      0.8092    0.4080    0.3846    0.2647    0.1154    0.3055
      0.6593    0.1662    0.7643    0.7681    0.8855    0.8070
      0.7289    0.9520    0.5528    0.8794    0.5948    0.4135
      0.2807    0.9897    0.9268    0.4358    0.3776    0.7894
      0.8576    0.5422    0.8033    0.6130    0.8956    0.1015
   
   
      0.8092
      0.6593
      0.7289
      0.8576
      0.9520
      0.9897
      0.5422
      0.7643
      0.5528
      0.9268
      0.8033
      0.7681
      0.8794
      0.6130
      0.8855
      0.5948
      0.8956
      0.8070
      0.7894
   

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

   
      7.0007    4.1053    9.6242    7.2267    7.0077    5.6771
      6.3662    3.3423    4.9258    4.5243    3.8322    9.2689
      8.4320    1.2719    5.3450    3.9960    0.5330    2.9739
      6.8342    8.7158    3.8823    5.8152    9.0483    1.7975
      1.0245    3.8310    8.3798    6.2606    1.2246    9.6331
   
   
      7.0007    0.0000    9.6242    7.2267    7.0077    5.6771
      6.3662    0.0000    0.0000    0.0000    0.0000    9.2689
      8.4320    0.0000    5.3450    0.0000    0.0000    0.0000
      6.8342    8.7158    0.0000    5.8152    9.0483    0.0000
      0.0000    0.0000    8.3798    6.2606    0.0000    9.6331
   
   
      7.0007    0.0000       NaN    7.2267    7.0077    5.6771
      6.3662    0.0000    0.0000    0.0000    0.0000       NaN
      8.4320    0.0000    5.3450    0.0000    0.0000    0.0000
      6.8342    8.7158    0.0000    5.8152       NaN    0.0000
      0.0000    0.0000    8.3798    6.2606    0.0000       NaN
   

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

   
      3.0133    1.2758    8.7691    1.6871    6.5000    1.7680
      6.5000    0.8689    1.5625    4.8960    2.7104    3.7156
      4.5301    1.2651    8.8772    6.5000    4.4710    0.7971
      2.7787    1.7243    6.5000    6.5000    3.4814    9.9227
      2.9440    0.6353    6.5000    0.8287    0.5221    6.5000
   
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
   
