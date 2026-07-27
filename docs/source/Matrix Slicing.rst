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
      0.6721    0.6320    0.2663    0.1903
   
   R1[2] = 0.2663177208805343
   C1 = 
      0.7071
      0.2815
      0.2736
      0.0748
      0.6996
      0.2375
      0.0339
      0.7520
   
   C1[5] = 0.23748613036334876

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
      0.0632    0.4749    0.6562    0.6420    0.7181
      0.7667    0.9332    0.3468    0.8456    0.2769
   

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
   
      0.3618    0.5548    0.6730    0.8051    0.8291    0.5186    0.6001    0.3801
      0.9901    0.9426    0.8303    0.2207    0.7789    0.9685    0.8313    0.4926
      0.5878    0.8444    0.4174    0.0600    0.1700    0.0019    0.6718    0.6154
      0.5392    0.0295    0.3987    0.5281    0.1232    0.9160    0.1033    0.6636
      0.2083    0.1388    0.7702    0.4324    0.1864    0.4247    0.0702    0.6634
      0.7994    0.3318    0.6363    0.4873    0.8662    0.9516    0.3361    0.4097
      0.2695    0.7606    0.9501    0.0201    0.2165    0.8753    0.4053    0.9355
      0.4318    0.9703    0.0729    0.3416    0.1559    0.7171    0.1730    0.1479
   
   B = 
   
      0.2754    0.9214    0.1983    0.6508    0.1351    0.4950    0.8740    0.3002
      0.8742    0.6120    0.1634    0.2741    0.3803    0.3203    0.5339    0.8068
      0.8922    0.3220    0.2065    0.9523    0.1344    0.8802    0.1216    0.8429
      0.8112    0.2252    0.2140    0.3619    0.0929    0.2734    0.8305    0.8377
      0.5192    0.7324    0.6911    0.9767    0.8196    0.6640    0.4868    0.8716
      0.4967    0.1129    0.1220    0.9238    0.1434    0.2273    0.6459    0.9383
      0.1732    0.9493    0.3337    0.6810    0.7974    0.2987    0.6306    0.1044
      0.8196    0.1764    0.3405    0.1245    0.6923    0.2179    0.3593    0.6976
   
   C = 
   
      2.9417    2.3735    1.4394    3.0648    1.9206    2.0998    2.6165    3.3350
      3.4498    3.3622    1.6705    4.0563    2.4056    2.6762    3.3589    3.9606
      2.0311    2.0774    0.9049    1.7352    1.5635    1.3934    1.7939    1.9092
      2.0391    1.1710    0.7643    2.0493    0.9608    1.2371    1.9294    2.4047
      2.0803    0.9905    0.7454    1.7685    0.9535    1.3296    1.3567    2.2171
      2.7897    2.3876    1.4147    3.3987    1.7630    2.1765    2.7535    3.4211
      2.9872    1.8314    1.0883    2.7085    1.7290    1.8864    2.0363    3.2171
      1.8976    1.4775    0.6355    1.6908    0.9398    1.0325    1.8893    2.1900
   
   D = 
   
      2.9417    2.3735    1.4394    3.0648    1.9206    2.0998    2.6165    3.3350
      3.4498    3.3622    1.6705    4.0563    2.4056    2.6762    3.3589    3.9606
      2.0311    2.0774    0.9049    1.7352    1.5635    1.3934    1.7939    1.9092
      2.0391    1.1710    0.7643    2.0493    0.9608    1.2371    1.9294    2.4047
      2.0803    0.9905    0.7454    1.7685    0.9535    1.3296    1.3567    2.2171
      2.7897    2.3876    1.4147    3.3987    1.7630    2.1765    2.7535    3.4211
      2.9872    1.8314    1.0883    2.7085    1.7290    1.8864    2.0363    3.2171
      1.8976    1.4775    0.6355    1.6908    0.9398    1.0325    1.8893    2.1900
   


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

   
      0.3432    0.8987    0.0365    0.2676    0.0152    0.4130
      0.0673    0.0496    0.3019    0.5966    0.1392    0.0579
      0.3729    0.2730    0.9756    0.1963    0.9581    0.7147
      0.2421    0.1074    0.5031    0.9118    0.5064    0.0513
      0.9728    0.2320    0.2640    0.2683    0.3682    0.4523
   
   
      0.9728
      0.8987
      0.9756
      0.5031
      0.5966
      0.9118
      0.9581
      0.5064
      0.7147
   

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

   
      9.5825    9.4894    3.4118    2.1754    3.8589    6.2098
      7.8370    7.0043    1.4237    4.4851    9.9639    6.6520
      2.6596    6.1436    1.3991    6.5830    1.7397    8.6702
      3.3849    7.4507    0.4369    0.8784    1.7848    2.4097
      8.1931    3.9490    0.3154    9.5909    9.7033    7.3371
   
   
      9.5825    9.4894    0.0000    0.0000    0.0000    6.2098
      7.8370    7.0043    0.0000    0.0000    9.9639    6.6520
      0.0000    6.1436    0.0000    6.5830    0.0000    8.6702
      0.0000    7.4507    0.0000    0.0000    0.0000    0.0000
      8.1931    0.0000    0.0000    9.5909    9.7033    7.3371
   
   
         NaN       NaN    0.0000    0.0000    0.0000    6.2098
      7.8370    7.0043    0.0000    0.0000       NaN    6.6520
      0.0000    6.1436    0.0000    6.5830    0.0000    8.6702
      0.0000    7.4507    0.0000    0.0000    0.0000    0.0000
      8.1931    0.0000    0.0000       NaN       NaN    7.3371
   

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

   
      9.9698    8.8475    1.9293    9.7763    4.7565    6.5000
      8.8223    8.8705    1.1759    6.5000    9.7354    6.5000
      8.7926    9.7750    6.5000    3.7105    4.5905    6.5000
      3.2609    6.5000    9.6811    9.6984    1.8532    6.5000
      9.4052    6.5000    1.9506    1.9649    8.0679    2.4554
   
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
   
