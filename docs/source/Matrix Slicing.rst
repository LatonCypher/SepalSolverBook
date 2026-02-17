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
      0.7624    0.4558    0.9505    0.4516
   
   R1[2] = 0.9504852929700193
   C1 = 
      0.0531
      0.7811
      0.3713
      0.8668
      0.4476
      0.3066
      0.0472
      0.8816
   
   C1[5] = 0.3066436316354839

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
      0.1745    0.1385    0.9646    0.1221    0.8713
      0.0990    0.3446    0.9997    0.4519    0.4331
   

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
   
      0.6828    0.2802    0.1158    0.8383    0.6395    0.0904    0.9398    0.1606
      0.6221    0.0219    0.5885    0.6141    0.2295    0.3633    0.1921    0.3916
      0.6668    0.8315    0.4255    0.2051    0.6233    0.7988    0.1643    0.8386
      0.5696    0.7004    0.1100    0.6827    0.7256    0.8950    0.7543    0.5946
      0.1440    0.0584    0.9770    0.8605    0.8745    0.5090    0.1416    0.4496
      0.4790    0.2495    0.9190    0.4788    0.9650    0.9947    0.2364    0.2491
      0.7878    0.4734    0.0202    0.9978    0.3623    0.7045    0.6907    0.4820
      0.2788    0.5271    0.3129    0.0509    0.8050    0.5134    0.5237    0.3598
   
   B = 
   
      0.1314    0.9547    0.4243    0.1710    0.1138    0.0189    0.9108    0.8193
      0.8544    0.9286    0.6593    0.8570    0.3719    0.4384    0.5373    0.5668
      0.1127    0.5604    0.2061    0.6802    0.3862    0.8049    0.8453    0.9317
      0.1423    0.5073    0.5196    0.9168    0.8854    0.6654    0.5282    0.0849
      0.8255    0.5129    0.3867    0.2260    0.0708    0.3909    0.7218    0.4903
      0.3184    0.4001    0.0653    0.1704    0.4669    0.3906    0.8039    0.2721
      0.1958    0.9068    0.4700    0.6668    0.7259    0.0991    0.3351    0.5725
      0.1596    0.5816    0.5909    0.8025    0.6756    0.5059    0.7273    0.0042
   
   C = 
   
      1.2279    2.7121    1.7237    2.1198    1.8471    1.2464    2.2792    1.7742
      0.6595    1.9208    1.1530    1.6447    1.4399    1.3524    2.2073    1.4457
      1.8101    3.0274    1.8913    2.3636    1.8339    1.8522    3.2792    2.0520
      1.9094    3.3624    2.1257    2.6951    2.3908    1.8694    3.2773    2.0578
      1.2850    2.2179    1.4517    2.2678    1.8835    2.1694    2.8579    1.7847
      1.6472    2.6990    1.5022    2.1048    1.7987    2.0918    3.3566    2.3109
      1.3879    3.0837    1.9645    2.5182    2.3386    1.6316    2.9259    1.7842
      1.5175    2.2593    1.3602    1.6662    1.3135    1.2711    2.2594    1.6588
   
   D = 
   
      1.2279    2.7121    1.7237    2.1198    1.8471    1.2464    2.2792    1.7742
      0.6595    1.9208    1.1530    1.6447    1.4399    1.3524    2.2073    1.4457
      1.8101    3.0274    1.8913    2.3636    1.8339    1.8522    3.2792    2.0520
      1.9094    3.3624    2.1257    2.6951    2.3908    1.8694    3.2773    2.0578
      1.2850    2.2179    1.4517    2.2678    1.8835    2.1694    2.8579    1.7847
      1.6472    2.6990    1.5022    2.1048    1.7987    2.0918    3.3566    2.3109
      1.3879    3.0837    1.9645    2.5182    2.3386    1.6316    2.9259    1.7842
      1.5175    2.2593    1.3602    1.6662    1.3135    1.2711    2.2594    1.6588
   


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

   
      0.9226    0.7638    0.2415    0.0160    0.8851    0.6226
      0.2620    0.2693    0.7012    0.0269    0.8637    0.8298
      0.6174    0.6884    0.8354    0.5164    0.0941    0.7544
      0.3907    0.8146    0.4969    0.6873    0.4842    0.7111
      0.8580    0.4973    0.6846    0.0727    0.7848    0.2974
   
   
      0.9226
      0.6174
      0.8580
      0.7638
      0.6884
      0.8146
      0.7012
      0.8354
      0.6846
      0.5164
      0.6873
      0.8851
      0.8637
      0.7848
      0.6226
      0.8298
      0.7544
      0.7111
   

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

   
      3.3156    5.4555    7.5070    3.8241    1.4511    1.8059
      4.3842    4.9653    3.6364    4.3402    6.0985    3.8737
      2.2418    0.7649    7.4046    5.4186    9.2888    9.6013
      1.1156    4.1049    4.5295    0.2927    4.9477    8.6158
      2.2804    2.8149    5.1206    5.9151    1.1480    4.7826
   
   
      0.0000    5.4555    7.5070    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    6.0985    0.0000
      0.0000    0.0000    7.4046    5.4186    9.2888    9.6013
      0.0000    0.0000    0.0000    0.0000    0.0000    8.6158
      0.0000    0.0000    5.1206    5.9151    0.0000    0.0000
   
   
      0.0000    5.4555    7.5070    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    6.0985    0.0000
      0.0000    0.0000    7.4046    5.4186       NaN       NaN
      0.0000    0.0000    0.0000    0.0000    0.0000    8.6158
      0.0000    0.0000    5.1206    5.9151    0.0000    0.0000
   

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

   
      2.0205    3.2191    4.8443    4.1033    1.4843    3.0390
      3.5108    6.5000    4.1634    4.5139    6.5000    4.7009
      1.7383    6.5000    6.5000    3.8065    1.4138    9.0135
      6.5000    4.8270    3.0693    6.5000    0.8503    6.5000
      4.3674    4.7832    3.6120    6.5000    6.5000    2.4720
   
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
   
