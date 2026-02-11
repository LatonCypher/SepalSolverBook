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
      0.2713    0.9917    0.3578    0.1071
   
   R1[2] = 0.35782787048479736
   C1 = 
      0.4996
      0.2484
      0.7445
      0.2570
      0.7545
      0.0895
      0.1544
      0.2544
   
   C1[5] = 0.08948913942867054

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
      0.6649    0.7446    0.6530    0.0225    0.4180
      0.9279    0.0967    0.1884    0.8317    0.2573
   

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
   
      0.4425    0.5697    0.7400    0.1868    0.3408    0.5037    0.5908    0.8811
      0.2866    0.5054    0.1873    0.6896    0.2307    0.2312    0.5298    0.6809
      0.5667    0.9221    0.1666    0.1660    0.6246    0.1530    0.6728    0.4995
      0.3353    0.4168    0.8756    0.8016    0.0148    0.5612    0.6792    0.3650
      0.0721    0.3802    0.6121    0.3963    0.2027    0.2422    0.6721    0.9127
      0.4699    0.1928    0.1228    0.4364    0.5755    0.9986    0.3379    0.6449
      0.9473    0.5543    0.6881    0.4935    0.7528    0.3239    0.2709    0.0639
      0.3471    0.5866    0.3851    0.1616    0.5074    0.5039    0.4838    0.7339
   
   B = 
   
      0.6323    0.6837    0.0454    0.3010    0.6971    0.2744    0.9560    0.7027
      0.9659    0.8103    0.2577    0.3473    0.3780    0.1600    0.4067    0.4456
      0.8848    0.8829    0.1354    0.5103    0.1117    0.4070    0.2596    0.2867
      0.0439    0.8865    0.0459    0.1604    0.1381    0.5355    0.9905    0.1504
      0.7667    0.2585    0.8583    0.8468    0.3954    0.1998    0.6346    0.8467
      0.0454    0.1367    0.5363    0.9563    0.6194    0.0132    0.5252    0.1559
      0.2468    0.1047    0.9051    0.2573    0.9623    0.0909    0.2964    0.7776
      0.5479    0.9487    0.9881    0.3850    0.2307    0.9319    0.5226    0.2919
   
   C = 
   
      2.4058    2.6378    2.2438    2.0002    1.8509    1.5633    2.1483    1.8888
      1.5565    2.1748    1.6745    1.2828    1.4083    1.3368    1.9920    1.4261
      2.3292    2.1554    2.0142    1.6430    1.8895    1.1131    2.0616    2.1035
      1.8290    2.5486    1.5670    1.6856    1.6911    1.3566    2.2076    1.5276
      1.8043    2.2709    2.0165    1.4573    1.4046    1.4973    1.7071    1.4537
      1.5345    1.9052    2.0803    2.1185    1.7945    1.2033    2.3186    1.6108
      2.4585    2.4696    1.4298    1.9494    1.7887    1.1318    2.5600    2.1013
      2.0673    2.1428    2.0953    1.8492    1.6767    1.2683    1.9440    1.7386
   
   D = 
   
      2.4058    2.6378    2.2438    2.0002    1.8509    1.5633    2.1483    1.8888
      1.5565    2.1748    1.6745    1.2828    1.4083    1.3368    1.9920    1.4261
      2.3292    2.1554    2.0142    1.6430    1.8895    1.1131    2.0616    2.1035
      1.8290    2.5486    1.5670    1.6856    1.6911    1.3566    2.2076    1.5276
      1.8043    2.2709    2.0165    1.4573    1.4046    1.4973    1.7071    1.4537
      1.5345    1.9052    2.0803    2.1185    1.7945    1.2033    2.3186    1.6108
      2.4585    2.4696    1.4298    1.9494    1.7887    1.1318    2.5600    2.1013
      2.0673    2.1428    2.0953    1.8492    1.6767    1.2683    1.9440    1.7386
   


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

   
      0.5102    0.6934    0.2169    0.5715    0.7675    0.6029
      0.8342    0.9872    0.3396    0.3309    0.5019    0.9577
      0.6093    0.1588    0.7949    0.4681    0.5251    0.1297
      0.9544    0.1409    0.6779    0.3667    0.7910    0.4113
      0.2968    0.5714    0.4756    0.8512    0.9256    0.2673
   
   
      0.5102
      0.8342
      0.6093
      0.9544
      0.6934
      0.9872
      0.5714
      0.7949
      0.6779
      0.5715
      0.8512
      0.7675
      0.5019
      0.5251
      0.7910
      0.9256
      0.6029
      0.9577
   

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

   
      2.8433    9.9680    4.5675    5.3837    7.0503    0.5893
      3.4808    4.8469    1.3180    2.5837    8.5109    3.5220
      5.7058    0.6137    1.5155    1.7496    0.2683    9.9865
      0.7884    0.2183    2.6527    9.8330    4.4935    8.7108
      4.1866    4.9800    8.7507    9.3981    7.4537    6.3866
   
   
      0.0000    9.9680    0.0000    5.3837    7.0503    0.0000
      0.0000    0.0000    0.0000    0.0000    8.5109    0.0000
      5.7058    0.0000    0.0000    0.0000    0.0000    9.9865
      0.0000    0.0000    0.0000    9.8330    0.0000    8.7108
      0.0000    0.0000    8.7507    9.3981    7.4537    6.3866
   
   
      0.0000       NaN    0.0000    5.3837    7.0503    0.0000
      0.0000    0.0000    0.0000    0.0000    8.5109    0.0000
      5.7058    0.0000    0.0000    0.0000    0.0000       NaN
      0.0000    0.0000    0.0000       NaN    0.0000    8.7108
      0.0000    0.0000    8.7507       NaN    7.4537    6.3866
   

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

   
      9.6778    6.5000    9.7263    6.5000    8.8898    6.5000
      2.6269    3.7827    9.1740    6.5000    8.0043    8.0664
      6.5000    6.5000    8.9905    6.5000    0.3146    2.4849
      0.3652    0.7770    2.6592    8.9337    3.5262    6.5000
      2.7891    4.9494    6.5000    1.9154    0.8917    3.0709
   
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
   
