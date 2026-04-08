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
      0.8717    0.0977    0.6505    0.4833
   
   R1[2] = 0.6504864151385857
   C1 = 
      0.7938
      0.2702
      0.7565
      0.2496
      0.9996
      0.4808
      0.0693
      0.1103
   
   C1[5] = 0.480803780592042

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
      0.9035    0.9094    0.7853    0.0790    0.1459
      0.6023    0.7927    0.5402    0.4959    0.6813
   

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
   
      0.1884    0.0218    0.1281    0.0827    0.9696    0.1402    0.2870    0.8662
      0.0808    0.7342    0.8046    0.2537    0.8262    0.5910    0.1944    0.6193
      0.1824    0.6661    0.2342    0.9991    0.5198    0.9482    0.8482    0.0120
      0.8535    0.8662    0.5243    0.6322    0.7308    0.1725    0.6975    0.8841
      0.7676    0.4614    0.1126    0.2388    0.0225    0.5988    0.4192    0.5908
      0.4615    0.2726    0.1265    0.7869    0.7548    0.0000    0.5727    0.8823
      0.8486    0.6329    0.2482    0.6158    0.6735    0.5379    0.2174    0.1476
      0.4098    0.4286    0.6953    0.6901    0.9153    0.8561    0.6204    0.2662
   
   B = 
   
      0.6459    0.8446    0.9810    0.0167    0.8227    0.1865    0.5782    0.2115
      0.7282    0.4795    0.7518    0.5670    0.5291    0.0857    0.0686    0.1572
      0.5247    0.1222    0.7350    0.6539    0.2653    0.2068    0.9962    0.1094
      0.7243    0.6453    0.4218    0.8128    0.8786    0.6311    0.4269    0.7291
      0.4690    0.7569    0.0040    0.1194    0.3460    0.4636    0.5761    0.9425
      0.0519    0.7788    0.3935    0.5417    0.2315    0.3293    0.8920    0.9121
      0.2199    0.5948    0.6417    0.9601    0.4540    0.6116    0.7024    0.3970
      0.2427    0.2628    0.6562    0.4874    0.2602    0.4999    0.0556    0.0914
   
   C = 
   
      1.0001    1.4799    1.1418    1.0558    0.9967    1.2199    1.2066    1.3524
      1.8039    2.0463    2.0965    2.0571    1.5632    1.4106    2.1810    1.8570
      1.9319    2.7865    2.2007    2.7419    2.2300    1.8481    2.5528    2.5900
      2.6347    2.9430    3.2389    2.6433    2.6944    2.0050    2.4590    2.0389
      1.3411    1.9255    2.1758    1.5597    1.6058    1.1161    1.5641    1.2092
      1.8270    2.1876    2.0320    1.9546    1.9995    1.7735    1.6336    1.7475
      2.0127    2.5448    2.2011    1.6883    2.1345    1.3486    2.0730    1.9804
      2.1162    2.8804    2.4396    2.5637    2.2203    1.9112    2.9951    2.6474
   
   D = 
   
      1.0001    1.4799    1.1418    1.0558    0.9967    1.2199    1.2066    1.3524
      1.8039    2.0463    2.0965    2.0571    1.5632    1.4106    2.1810    1.8570
      1.9319    2.7865    2.2007    2.7419    2.2300    1.8481    2.5528    2.5900
      2.6347    2.9430    3.2389    2.6433    2.6944    2.0050    2.4590    2.0389
      1.3411    1.9255    2.1758    1.5597    1.6058    1.1161    1.5641    1.2092
      1.8270    2.1876    2.0320    1.9546    1.9995    1.7735    1.6336    1.7475
      2.0127    2.5448    2.2011    1.6883    2.1345    1.3486    2.0730    1.9804
      2.1162    2.8804    2.4396    2.5637    2.2203    1.9112    2.9951    2.6474
   


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

   
      0.2454    0.6382    0.6375    0.1662    0.2299    0.3252
      0.9948    0.7752    0.3698    0.9347    0.6578    0.2300
      0.6629    0.2445    0.1063    0.6589    0.3455    0.2362
      0.5388    0.9726    0.4876    0.5656    0.2420    0.3383
      0.5273    0.8767    0.1227    0.7688    0.4271    0.2286
   
   
      0.9948
      0.6629
      0.5388
      0.5273
      0.6382
      0.7752
      0.9726
      0.8767
      0.6375
      0.9347
      0.6589
      0.5656
      0.7688
      0.6578
   

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

   
      6.6315    1.3138    6.9536    4.2224    5.4059    6.1059
      0.6893    5.1844    2.1266    5.0158    4.4400    3.2893
      2.1956    1.5051    4.4658    5.7959    5.3476    2.8440
      4.8511    8.3903    2.1774    1.7992    7.8925    9.3555
      6.7046    7.8695    5.7008    1.4038    4.4060    3.3302
   
   
      6.6315    0.0000    6.9536    0.0000    5.4059    6.1059
      0.0000    5.1844    0.0000    5.0158    0.0000    0.0000
      0.0000    0.0000    0.0000    5.7959    5.3476    0.0000
      0.0000    8.3903    0.0000    0.0000    7.8925    9.3555
      6.7046    7.8695    5.7008    0.0000    0.0000    0.0000
   
   
      6.6315    0.0000    6.9536    0.0000    5.4059    6.1059
      0.0000    5.1844    0.0000    5.0158    0.0000    0.0000
      0.0000    0.0000    0.0000    5.7959    5.3476    0.0000
      0.0000    8.3903    0.0000    0.0000    7.8925       NaN
      6.7046    7.8695    5.7008    0.0000    0.0000    0.0000
   

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

   
      1.1358    6.5000    0.7850    9.9286    6.5000    6.5000
      0.9053    6.5000    1.2310    2.5676    1.0602    6.5000
      1.9415    3.9311    0.3267    6.5000    6.5000    0.5997
      2.1503    6.5000    6.5000    1.6655    6.5000    9.3302
      2.4504    1.0566    6.5000    1.3635    1.1518    6.5000
   
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
   
