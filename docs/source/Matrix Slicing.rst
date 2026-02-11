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
      0.7302    0.0392    0.0587    0.7499
   
   R1[2] = 0.058742977122774986
   C1 = 
      0.5086
      0.2758
      0.6100
      0.4553
      0.9314
      0.9803
      0.8447
      0.1759
   
   C1[5] = 0.9802640013170668

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
      0.1943    0.2858    0.6681    0.6529    0.4986
      0.3550    0.7938    0.5556    0.1688    0.6041
   

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
   
      0.3443    0.8777    0.3596    0.0307    0.8637    0.2452    0.2219    0.5212
      0.9142    0.8465    0.5523    0.2211    0.0959    0.9088    0.3342    0.5846
      0.7454    0.3414    0.3956    0.9399    0.1776    0.7190    0.4965    0.5718
      0.7546    0.1600    0.2422    0.9955    0.7982    0.3967    0.2720    0.3204
      0.7987    0.2950    0.4410    0.8868    0.2105    0.8611    0.4909    0.5952
      0.8239    0.4987    0.5171    0.7284    0.0535    0.4315    0.2019    0.0271
      0.6895    0.0321    0.2675    0.9156    0.6747    0.1004    0.8428    0.2614
      0.6883    0.2189    0.9599    0.6456    0.4170    0.2079    0.9024    0.3460
   
   B = 
   
      0.7308    0.9394    0.1115    0.0452    0.9478    0.2098    0.2241    0.2701
      0.1098    0.4285    0.3542    0.0729    0.0373    0.8259    0.3182    0.8529
      0.3545    0.0892    0.1847    0.3040    0.6803    0.3802    0.9370    0.5898
      0.2104    0.2614    0.4537    0.4118    0.6440    0.3434    0.1425    0.2282
      0.4527    0.9258    0.6920    0.6323    0.4127    0.9780    0.2149    0.7009
      0.6700    0.2493    0.7595    0.7351    0.3866    0.4350    0.2640    0.4220
      0.2196    0.2337    0.8670    0.5795    0.1797    0.3866    0.6642    0.5848
      0.9158    0.0381    0.1504    0.7521    0.2355    0.3623    0.2677    0.7229
   
   C = 
   
      1.5633    1.6721    1.4843    1.4484    1.2372    2.1704    1.2350    2.2760
      2.2644    1.7443    1.7384    1.7240    2.0048    2.0069    1.6623    2.4138
      2.1149    1.6091    1.8889    1.9244    2.1687    1.7970    1.4911    2.0718
      1.8445    1.9730    1.7747    1.7241    2.1341    1.8987    1.1315    1.8278
      2.2840    1.6949    1.9920    2.0550    2.2873    1.8691    1.5704    2.1580
      1.3759    1.4297    1.2384    1.0192    1.8520    1.3595    1.1985    1.4764
      1.5920    1.7813    1.8661    1.6772    1.9566    1.7113    1.3472    1.7774
      1.8462    1.6568    1.9053    1.8043    2.2254    1.8843    2.0519    2.2439
   
   D = 
   
      1.5633    1.6721    1.4843    1.4484    1.2372    2.1704    1.2350    2.2760
      2.2644    1.7443    1.7384    1.7240    2.0048    2.0069    1.6623    2.4138
      2.1149    1.6091    1.8889    1.9244    2.1687    1.7970    1.4911    2.0718
      1.8445    1.9730    1.7747    1.7241    2.1341    1.8987    1.1315    1.8278
      2.2840    1.6949    1.9920    2.0550    2.2873    1.8691    1.5704    2.1580
      1.3759    1.4297    1.2384    1.0192    1.8520    1.3595    1.1985    1.4764
      1.5920    1.7813    1.8661    1.6772    1.9566    1.7113    1.3472    1.7774
      1.8462    1.6568    1.9053    1.8043    2.2254    1.8843    2.0519    2.2439
   


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

   
      0.1808    0.6735    0.7906    0.2900    0.9686    0.4855
      0.4181    0.9645    0.0181    0.1971    0.8675    0.4814
      0.2983    0.9900    0.5706    0.5222    0.8883    0.9037
      0.8150    0.8776    0.8902    0.9105    0.4568    0.7047
      0.4184    0.4617    0.6055    0.1755    0.9421    0.3423
   
   
      0.8150
      0.6735
      0.9645
      0.9900
      0.8776
      0.7906
      0.5706
      0.8902
      0.6055
      0.5222
      0.9105
      0.9686
      0.8675
      0.8883
      0.9421
      0.9037
      0.7047
   

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

   
      9.6253    7.1546    9.5776    2.7863    2.3931    4.5149
      6.6772    3.5587    8.2520    9.6352    6.9335    5.7240
      4.7813    9.0466    7.4439    0.1605    8.5976    7.1441
      1.5553    1.1146    6.8221    4.8872    0.8629    2.4888
      0.5460    9.0634    4.4854    5.9938    7.6509    5.4812
   
   
      9.6253    7.1546    9.5776    0.0000    0.0000    0.0000
      6.6772    0.0000    8.2520    9.6352    6.9335    5.7240
      0.0000    9.0466    7.4439    0.0000    8.5976    7.1441
      0.0000    0.0000    6.8221    0.0000    0.0000    0.0000
      0.0000    9.0634    0.0000    5.9938    7.6509    5.4812
   
   
         NaN    7.1546       NaN    0.0000    0.0000    0.0000
      6.6772    0.0000    8.2520       NaN    6.9335    5.7240
      0.0000       NaN    7.4439    0.0000    8.5976    7.1441
      0.0000    0.0000    6.8221    0.0000    0.0000    0.0000
      0.0000       NaN    0.0000    5.9938    7.6509    5.4812
   

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

   
      4.2486    3.3329    6.5000    1.6148    1.3084    6.5000
      1.2190    6.5000    4.0829    1.4179    0.0636    0.6069
      6.5000    3.3050    8.1926    6.5000    1.2020    1.6971
      4.1104    6.5000    6.5000    2.5712    6.5000    2.5957
      2.9542    2.0867    6.5000    8.4868    3.2695    4.5422
   
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
   
