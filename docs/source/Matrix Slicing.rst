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
      0.1802    0.1538    0.4423    0.8002
   
   R1[2] = 0.4423124052060595
   C1 = 
      0.2920
      0.1866
      0.1017
      0.9388
      0.9319
      0.6433
      0.1811
      0.3974
   
   C1[5] = 0.6433405760132319

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
      0.2977    0.3614    0.5572    0.0305    0.9437
      0.3614    0.8701    0.1307    0.8874    0.2028
   

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
   
      0.2669    0.7188    0.0572    0.1586    0.5656    0.1150    0.9518    0.6937
      0.3413    0.4270    0.2556    0.5471    0.9314    0.4128    0.3631    0.2277
      0.5881    0.9504    0.3068    0.5716    0.2609    0.8278    0.0463    0.9337
      0.5418    0.1088    0.9413    0.5036    0.1765    0.4806    0.7116    0.2014
      0.9544    0.8885    0.2437    0.9752    0.3862    0.1792    0.0110    0.0065
      0.2330    0.3323    0.0838    0.4881    0.7027    0.9995    0.9512    0.2974
      0.8687    0.6724    0.5759    0.8388    0.5126    0.8083    0.3554    0.7703
      0.8325    0.3946    0.1751    0.5613    0.5193    0.9997    0.1192    0.7433
   
   B = 
   
      0.2862    0.1326    0.7313    0.0104    0.1831    0.2182    0.4300    0.1828
      0.9984    0.5174    0.6820    0.5382    0.1543    0.9154    0.1667    0.2336
      0.1479    0.2970    0.3295    0.0032    0.7710    0.4054    0.7169    0.1041
      0.1767    0.2062    0.6016    0.3061    0.5111    0.5581    0.6640    0.7687
      0.8262    0.5729    0.9155    0.8201    0.8707    0.6513    0.2796    0.9185
      0.8634    0.1375    0.2525    0.8183    0.8634    0.7572    0.4914    0.0117
      0.3474    0.1533    0.1523    0.3278    0.0844    0.1528    0.6485    0.1692
      0.5390    0.8456    0.3236    0.8238    0.1117    0.2476    0.6855    0.8629
   
   C = 
   
      2.1017    1.5294    1.7159    1.8798    1.0345    1.6006    1.6883    1.6251
      2.0334    1.2935    2.0401    1.8099    1.8286    1.9054    1.6192    1.7274
      2.7133    1.8387    2.2804    2.3693    1.8332    2.4768    2.1606    1.8637
      1.4084    0.9581    1.5400    1.1586    1.7503    1.5179    2.1453    1.0713
      1.8497    1.1128    2.3735    1.2597    1.4909    2.0551    1.5882    1.5212
      2.4315    1.2657    1.8551    2.2819    1.9963    2.0948    2.0481    1.5787
      2.8134    1.9178    2.7650    2.4624    2.3959    2.6976    2.7544    2.2255
      2.4917    1.5642    2.2600    2.2888    2.0437    2.2246    2.1453    1.8443
   
   D = 
   
      2.1017    1.5294    1.7159    1.8798    1.0345    1.6006    1.6883    1.6251
      2.0334    1.2935    2.0401    1.8099    1.8286    1.9054    1.6192    1.7274
      2.7133    1.8387    2.2804    2.3693    1.8332    2.4768    2.1606    1.8637
      1.4084    0.9581    1.5400    1.1586    1.7503    1.5179    2.1453    1.0713
      1.8497    1.1128    2.3735    1.2597    1.4909    2.0551    1.5882    1.5212
      2.4315    1.2657    1.8551    2.2819    1.9963    2.0948    2.0481    1.5787
      2.8134    1.9178    2.7650    2.4624    2.3959    2.6976    2.7544    2.2255
      2.4917    1.5642    2.2600    2.2888    2.0437    2.2246    2.1453    1.8443
   


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

   
      0.0811    0.7270    0.4377    0.0150    0.2701    0.5910
      0.2395    0.3637    0.7124    0.6231    0.5989    0.4560
      0.7616    0.3000    0.0617    0.2964    0.3799    0.8934
      0.4937    0.4636    0.3853    0.1994    0.8883    0.0672
      0.2632    0.0785    0.9521    0.9895    0.9653    0.8538
   
   
      0.7616
      0.7270
      0.7124
      0.9521
      0.6231
      0.9895
      0.5989
      0.8883
      0.9653
      0.5910
      0.8934
      0.8538
   

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

   
      8.4013    2.1964    2.8434    8.0888    0.8743    1.2608
      5.4847    5.3548    7.8509    8.1479    6.4126    6.7595
      0.3669    8.1753    2.4138    1.1805    2.7224    3.8990
      0.5602    7.0085    0.9734    8.9960    3.5370    1.3392
      5.5636    4.9658    4.0164    5.2318    2.0886    6.6725
   
   
      8.4013    0.0000    0.0000    8.0888    0.0000    0.0000
      5.4847    5.3548    7.8509    8.1479    6.4126    6.7595
      0.0000    8.1753    0.0000    0.0000    0.0000    0.0000
      0.0000    7.0085    0.0000    8.9960    0.0000    0.0000
      5.5636    0.0000    0.0000    5.2318    0.0000    6.6725
   
   
      8.4013    0.0000    0.0000    8.0888    0.0000    0.0000
      5.4847    5.3548    7.8509    8.1479    6.4126    6.7595
      0.0000    8.1753    0.0000    0.0000    0.0000    0.0000
      0.0000    7.0085    0.0000    8.9960    0.0000    0.0000
      5.5636    0.0000    0.0000    5.2318    0.0000    6.6725
   

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

   
      3.2707    6.5000    4.9958    2.0824    6.5000    1.0092
      1.3329    6.5000    1.9488    8.5604    2.1542    3.1807
      2.1349    9.1787    0.0394    6.5000    1.3493    6.5000
      2.3205    3.1776    1.0146    1.8494    2.2054    0.1430
      0.3135    0.4695    6.5000    6.5000    4.8401    6.5000
   
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
   
