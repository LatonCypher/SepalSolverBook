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
      0.2100    0.6079    0.2647    0.3242
   
   R1[2] = 0.26472094753333264
   C1 = 
      0.9155
      0.8623
      0.7734
      0.4193
      0.7719
      0.2946
      0.4298
      0.8953
   
   C1[5] = 0.29464535922832624

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
      0.0643    0.9169    0.4894    0.7186    0.6817
      0.7454    0.9728    0.8897    0.8705    0.6502
   

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
   
      0.0079    0.0783    0.5043    0.4519    0.0992    0.7358    0.6756    0.3211
      0.8457    0.7103    0.7549    0.5701    0.6984    0.8703    0.2529    0.1951
      0.6601    0.5781    0.5205    0.8174    0.3960    0.9067    0.6267    0.4322
      0.1496    0.2966    0.8561    0.3765    0.6803    0.8569    0.0933    0.4447
      0.1649    0.4127    0.3550    0.4349    0.4920    0.4311    0.8504    0.9577
      0.2184    0.6552    0.8327    0.1193    0.1695    0.6915    0.9633    0.7636
      0.4439    0.4854    0.6397    0.9590    0.9533    0.2373    0.3497    0.6355
      0.0300    0.4848    0.3668    0.6790    0.5949    0.6272    0.3149    0.2405
   
   B = 
   
      0.0314    0.6097    0.0804    0.9582    0.9489    0.9091    0.3836    0.3252
      0.6374    0.3769    0.2430    0.5302    0.6103    0.3816    0.7345    0.0233
      0.8448    0.2754    0.5312    0.1194    0.9859    0.3796    0.7701    0.8655
      0.4886    0.2831    0.7472    0.1994    0.5193    0.6072    0.3384    0.2605
      0.7100    0.4993    0.3246    0.3976    0.1918    0.6241    0.6892    0.1117
      0.3450    0.3541    0.6913    0.5781    0.7780    0.2316    0.8514    0.8754
      0.7958    0.9324    0.5388    0.5943    0.8447    0.2027    0.0650    0.0796
      0.3294    0.9214    0.9227    0.5115    0.2320    0.8821    0.3215    0.7322
   
   C = 
   
      1.6647    1.5371    1.8264    1.2300    2.0237    1.1555    1.4438    1.5028
      2.4573    2.2252    2.2122    2.4216    3.3462    2.5334    2.9219    2.0964
      2.4634    2.4966    2.5726    2.4393    3.3278    2.4801    2.5798    2.0960
      2.1003    1.6852    2.0940    1.5266    2.3415    1.8370    2.4093    2.0537
      2.2709    2.5507    2.4266    1.9460    2.3544    2.1303    1.8562    1.6851
      2.5632    2.5744    2.4648    2.1100    3.0512    1.9719    2.2602    2.0982
      2.5786    2.3729    2.4583    1.9993    2.6563    2.6952    2.4300    1.7666
      1.9202    1.5287    1.8407    1.3742    1.9624    1.5564    1.9216    1.3321
   
   D = 
   
      1.6647    1.5371    1.8264    1.2300    2.0237    1.1555    1.4438    1.5028
      2.4573    2.2252    2.2122    2.4216    3.3462    2.5334    2.9219    2.0964
      2.4634    2.4966    2.5726    2.4393    3.3278    2.4801    2.5798    2.0960
      2.1003    1.6852    2.0940    1.5266    2.3415    1.8370    2.4093    2.0537
      2.2709    2.5507    2.4266    1.9460    2.3544    2.1303    1.8562    1.6851
      2.5632    2.5744    2.4648    2.1100    3.0512    1.9719    2.2602    2.0982
      2.5786    2.3729    2.4583    1.9993    2.6563    2.6952    2.4300    1.7666
      1.9202    1.5287    1.8407    1.3742    1.9624    1.5564    1.9216    1.3321
   


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

   
      0.3746    0.4513    0.3984    0.7964    0.2840    0.5682
      0.7802    0.5058    0.4155    0.5134    0.9873    0.6462
      0.9558    0.0934    0.7694    0.2924    0.3007    0.6745
      0.5672    0.3700    0.6120    0.1252    0.8770    0.9179
      0.1470    0.4557    0.0709    0.4133    0.5077    0.2564
   
   
      0.7802
      0.9558
      0.5672
      0.5058
      0.7694
      0.6120
      0.7964
      0.5134
      0.9873
      0.8770
      0.5077
      0.5682
      0.6462
      0.6745
      0.9179
   

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

   
      9.4996    6.2996    4.4425    8.2549    1.3124    2.0995
      4.9292    6.4901    9.5625    8.0748    2.6477    5.3857
      4.8598    3.9307    2.0702    5.3706    7.8677    6.2647
      5.5443    9.5837    3.5625    3.7968    1.4432    0.0214
      9.2401    8.7429    6.3490    0.0219    4.2122    7.8125
   
   
      9.4996    6.2996    0.0000    8.2549    0.0000    0.0000
      0.0000    6.4901    9.5625    8.0748    0.0000    5.3857
      0.0000    0.0000    0.0000    5.3706    7.8677    6.2647
      5.5443    9.5837    0.0000    0.0000    0.0000    0.0000
      9.2401    8.7429    6.3490    0.0000    0.0000    7.8125
   
   
         NaN    6.2996    0.0000    8.2549    0.0000    0.0000
      0.0000    6.4901       NaN    8.0748    0.0000    5.3857
      0.0000    0.0000    0.0000    5.3706    7.8677    6.2647
      5.5443       NaN    0.0000    0.0000    0.0000    0.0000
         NaN    8.7429    6.3490    0.0000    0.0000    7.8125
   

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

   
      6.5000    6.5000    0.4634    8.1162    3.2823    0.6345
      6.5000    9.1332    9.2544    3.3874    0.7396    2.6765
      1.1048    1.2075    6.5000    4.9860    6.5000    6.5000
      9.8183    8.7963    1.9476    2.4847    1.1538    8.0984
      4.8798    6.5000    6.5000    8.2596    4.1471    8.1983
   
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
   
