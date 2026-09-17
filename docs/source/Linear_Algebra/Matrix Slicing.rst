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
      0.3118    0.4415    0.5068    0.3140
   
   R1[2] = 0.5067978511387569
   C1 = 
      0.8260
      0.8475
      0.6264
      0.8939
      0.4754
      0.1026
      0.5083
      0.8129
   
   C1[5] = 0.10256154604513501

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
      0.9677    0.9457    0.4663    0.2488    0.6265
      0.3740    0.7356    0.1198    0.9563    0.4734
   

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
   
      0.0911    0.8443    0.4493    0.5802    0.3401    0.8076    0.4734    0.8938
      0.4267    0.4821    0.4851    0.9212    0.3856    0.0090    0.1101    0.2148
      0.4631    0.3882    0.4377    0.1581    0.6664    0.2110    0.7347    0.8008
      0.9274    0.8634    0.0330    0.1325    0.9005    0.2517    0.6615    0.9449
      0.5664    0.9737    0.5177    0.6281    0.4382    0.9025    0.3984    0.9377
      0.3994    0.6582    0.1794    0.1068    0.4182    0.6594    0.1193    0.2722
      0.9595    0.6173    0.3590    0.4685    0.7682    0.6193    0.6543    0.0882
      0.5703    0.2368    0.0519    0.9099    0.9995    0.6236    0.3217    0.9132
   
   B = 
   
      0.5775    0.7770    0.0491    0.8337    0.7840    0.5278    0.6322    0.5085
      0.6788    0.0788    0.2758    0.4309    0.3409    0.4738    0.8715    0.4741
      0.7807    0.2867    0.9780    0.7461    0.8681    0.4732    0.0114    0.9626
      0.3060    0.1011    0.3728    0.5335    0.4871    0.8561    0.5010    0.3988
      0.3776    0.1438    0.4926    0.6788    0.4860    0.1095    0.3127    0.0981
      0.7626    0.8686    0.9814    0.8597    0.0476    0.7069    0.8738    0.2673
      0.6824    0.7399    0.5319    0.9487    0.9442    0.0599    0.1850    0.1843
      0.1375    0.4178    0.6338    0.7479    0.9532    0.2155    0.4885    0.8054
   
   C = 
   
      2.3444    1.7989    2.6715    3.1274    2.5347    1.9866    2.4254    2.1669
      1.4915    0.8363    1.3652    1.9515    1.8654    1.5733    1.4107    1.5134
      1.9451    1.6892    2.0505    2.8940    2.7434    1.2095    1.6350    1.8060
      2.3014    2.0439    2.0066    3.4025    3.0895    1.5474    2.4909    2.0040
      2.8391    2.2623    2.9446    3.7659    3.0573    2.4549    2.9846    2.6113
      1.6298    1.2593    1.5055    1.9749    1.3520    1.2767    1.7436    1.1889
      2.6179    2.1139    2.1334    3.3244    2.6074    1.9500    2.3289    1.7456
      2.0072    1.8736    2.3375    3.3045    2.7058    1.9830    2.3863    1.8746
   
   D = 
   
      2.3444    1.7989    2.6715    3.1274    2.5347    1.9866    2.4254    2.1669
      1.4915    0.8363    1.3652    1.9515    1.8654    1.5733    1.4107    1.5134
      1.9451    1.6892    2.0505    2.8940    2.7434    1.2095    1.6350    1.8060
      2.3014    2.0439    2.0066    3.4025    3.0895    1.5474    2.4909    2.0040
      2.8391    2.2623    2.9446    3.7659    3.0573    2.4549    2.9846    2.6113
      1.6298    1.2593    1.5055    1.9749    1.3520    1.2767    1.7436    1.1889
      2.6179    2.1139    2.1334    3.3244    2.6074    1.9500    2.3289    1.7456
      2.0072    1.8736    2.3375    3.3045    2.7058    1.9830    2.3863    1.8746
   


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

   
      0.5561    0.3759    0.9504    0.5224    0.1322    0.9936
      0.0511    0.7414    0.4782    0.5450    0.3214    0.9487
      0.5432    0.3655    0.6463    0.0812    0.6983    0.1719
      0.6073    0.0691    0.7244    0.4833    0.3813    0.3882
      0.2967    0.5301    0.8738    0.9726    0.2405    0.5562
   
   
      0.5561
      0.5432
      0.6073
      0.7414
      0.5301
      0.9504
      0.6463
      0.7244
      0.8738
      0.5224
      0.5450
      0.9726
      0.6983
      0.9936
      0.9487
      0.5562
   

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

   
      5.5288    6.8921    6.2057    6.5106    1.5941    1.7515
      8.8752    9.8047    2.5334    7.6925    4.3887    7.1806
      9.0191    7.5164    2.1729    0.9317    5.9173    3.5852
      7.1069    3.9996    4.3763    5.9857    6.3902    7.8206
      0.7306    9.2806    1.5343    9.9152    1.3707    4.6864
   
   
      5.5288    6.8921    6.2057    6.5106    0.0000    0.0000
      8.8752    9.8047    0.0000    7.6925    0.0000    7.1806
      9.0191    7.5164    0.0000    0.0000    5.9173    0.0000
      7.1069    0.0000    0.0000    5.9857    6.3902    7.8206
      0.0000    9.2806    0.0000    9.9152    0.0000    0.0000
   
   
      5.5288    6.8921    6.2057    6.5106    0.0000    0.0000
      8.8752       NaN    0.0000    7.6925    0.0000    7.1806
         NaN    7.5164    0.0000    0.0000    5.9173    0.0000
      7.1069    0.0000    0.0000    5.9857    6.3902    7.8206
      0.0000       NaN    0.0000       NaN    0.0000    0.0000
   

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

   
      6.5000    3.6270    8.5174    9.9005    0.4798    6.5000
      8.8442    6.5000    8.2711    8.5344    3.2171    1.2234
      8.7544    6.5000    9.0125    6.5000    9.2617    9.0794
      3.6889    3.3186    0.6261    6.5000    4.2805    4.4754
      3.6649    6.5000    1.4993    6.5000    2.6251    1.3820
   
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
   
