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
      0.1329    0.8486    0.0002    0.9675
   
   R1[2] = 0.0001882381674179756
   C1 = 
      0.4998
      0.7374
      0.8957
      0.7980
      0.1003
      0.0088
      0.2908
      0.2204
   
   C1[5] = 0.008758349133844212

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
      0.3159    0.1030    0.3725    0.4339    0.3440
      0.8806    0.9547    0.8101    0.6797    0.1441
   

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
   
      0.0249    0.7415    0.9930    0.1808    0.1062    0.2885    0.3112    0.6462
      0.3087    0.7682    0.1303    0.6692    0.6665    0.9475    0.3102    0.8590
      0.1117    0.8293    0.1203    0.1808    0.1376    0.6177    0.0246    0.8075
      0.1361    0.8759    0.7522    0.8492    0.3477    0.3459    0.4550    0.7354
      0.2965    0.7222    0.8173    0.3854    0.3717    0.7721    0.6555    0.2290
      0.4002    0.7866    0.6738    0.6899    0.5646    0.4773    0.3408    0.3178
      0.3735    0.6237    0.7563    0.0588    0.3003    0.4614    0.0461    0.6176
      0.0741    0.1349    0.6144    0.0764    0.8816    0.3441    0.1858    0.3617
   
   B = 
   
      0.0614    0.1731    0.6674    0.9582    0.9954    0.2555    0.3472    0.6819
      0.4769    0.7506    0.2862    0.7469    0.5137    0.6037    0.5536    0.1594
      0.5721    0.6952    0.9644    0.7771    0.5257    0.1516    0.4059    0.3301
      0.3354    0.2939    0.1898    0.5563    0.4579    0.1989    0.6415    0.3520
      0.7313    0.0204    0.8276    0.4172    0.5244    0.8751    0.1871    0.3075
      0.1248    0.7600    0.8536    0.1338    0.0270    0.8453    0.2671    0.6848
      0.7896    0.9719    0.0600    0.5021    0.9973    0.5822    0.7675    0.1614
      0.8627    0.0686    0.8843    0.1457    0.5105    0.4188    0.4907    0.2179
   
   C = 
   
      1.9007    1.8726    2.1450    1.7832    1.7143    1.4292    1.5911    0.9479
      2.2759    2.0115    2.8171    2.0288    2.1999    2.6200    2.0521    1.7027
      1.4255    1.3301    1.8189    1.1905    1.2088    1.5784    1.2685    0.9570
      2.4322    2.2161    2.4886    2.3684    2.3906    2.0160    2.2497    1.3571
      2.0426    2.5220    2.4743    2.2938    2.2586    2.1677    1.9729    1.5215
      2.0323    2.0583    2.4491    2.3952    2.2836    2.0455    1.9411    1.4883
      1.6191    1.5199    2.3595    1.7443    1.6479    1.5366    1.3375    1.1748
      1.5923    1.0485    2.0494    1.2515    1.3425    1.5307    0.9757    0.9172
   
   D = 
   
      1.9007    1.8726    2.1450    1.7832    1.7143    1.4292    1.5911    0.9479
      2.2759    2.0115    2.8171    2.0288    2.1999    2.6200    2.0521    1.7027
      1.4255    1.3301    1.8189    1.1905    1.2088    1.5784    1.2685    0.9570
      2.4322    2.2161    2.4886    2.3684    2.3906    2.0160    2.2497    1.3571
      2.0426    2.5220    2.4743    2.2938    2.2586    2.1677    1.9729    1.5215
      2.0323    2.0583    2.4491    2.3952    2.2836    2.0455    1.9411    1.4883
      1.6191    1.5199    2.3595    1.7443    1.6479    1.5366    1.3375    1.1748
      1.5923    1.0485    2.0494    1.2515    1.3425    1.5307    0.9757    0.9172
   


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

   
      0.5007    0.6112    0.3004    0.7879    0.9607    0.5006
      0.2356    0.6460    0.6064    0.6812    0.7427    0.9191
      0.7281    0.0531    0.9372    0.4408    0.0123    0.3602
      0.6299    0.0003    0.3713    0.0437    0.2530    0.8560
      0.9772    0.1069    0.8566    0.8202    0.7852    0.8600
   
   
      0.5007
      0.7281
      0.6299
      0.9772
      0.6112
      0.6460
      0.6064
      0.9372
      0.8566
      0.7879
      0.6812
      0.8202
      0.9607
      0.7427
      0.7852
      0.5006
      0.9191
      0.8560
      0.8600
   

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

   
      5.0527    4.4694    8.9274    1.7909    4.3272    1.6477
      1.0092    6.5333    8.0202    7.4271    4.6345    8.3484
      6.7106    6.9302    0.0374    5.4256    8.2815    7.9753
      9.5535    4.1358    6.9204    8.7307    2.7520    2.7119
      8.0368    2.0221    1.9962    1.2989    2.9164    7.6053
   
   
      5.0527    0.0000    8.9274    0.0000    0.0000    0.0000
      0.0000    6.5333    8.0202    7.4271    0.0000    8.3484
      6.7106    6.9302    0.0000    5.4256    8.2815    7.9753
      9.5535    0.0000    6.9204    8.7307    0.0000    0.0000
      8.0368    0.0000    0.0000    0.0000    0.0000    7.6053
   
   
      5.0527    0.0000    8.9274    0.0000    0.0000    0.0000
      0.0000    6.5333    8.0202    7.4271    0.0000    8.3484
      6.7106    6.9302    0.0000    5.4256    8.2815    7.9753
         NaN    0.0000    6.9204    8.7307    0.0000    0.0000
      8.0368    0.0000    0.0000    0.0000    0.0000    7.6053
   

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

   
      4.2260    6.5000    6.5000    6.5000    9.3755    6.5000
      6.5000    0.3893    3.5748    3.0031    6.5000    9.9061
      6.5000    0.8392    3.3923    9.5080    6.5000    3.2897
      6.5000    3.7196    0.7986    4.2367    4.4359    3.2387
      9.5020    8.0740    6.5000    2.2749    1.3071    0.5928
   
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
   
