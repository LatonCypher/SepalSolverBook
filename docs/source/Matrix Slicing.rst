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
      0.4890    0.7311    0.2570    0.9375
   
   R1[2] = 0.25696441904054523
   C1 = 
      0.0766
      0.4517
      0.0911
      0.4287
      0.0827
      0.4785
      0.7220
      0.9032
   
   C1[5] = 0.4785164713471727

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
      0.5623    0.9998    0.6670    0.8909    0.9853
      0.3743    0.2004    0.0971    0.6141    0.1881
   

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
   
      0.0643    0.0886    0.3645    0.5106    0.4959    0.2642    0.8623    0.2560
      0.8966    0.3740    0.7080    0.1077    0.3326    0.0490    0.6021    0.7935
      0.2978    0.5613    0.0680    0.7937    0.8851    0.2897    0.4499    0.5028
      0.0140    0.0694    0.4661    0.9119    0.1129    0.2019    0.4573    0.8074
      0.7526    0.2108    0.8329    0.4107    0.9819    0.5212    0.5820    0.9438
      0.9180    0.1778    0.1544    0.5637    0.2741    0.6700    0.9964    0.3534
      0.1394    0.6643    0.3574    0.6370    0.8621    0.6710    0.3246    0.4419
      0.4023    0.8655    0.7870    0.6073    0.4876    0.5648    0.1389    0.2646
   
   B = 
   
      0.7472    0.3675    0.6188    0.2055    0.3449    0.3370    0.7537    0.2689
      0.2141    0.7690    0.7666    0.8187    0.4427    0.2850    0.2197    0.2191
      0.0113    0.8498    0.3407    0.9990    0.3536    0.3891    0.2985    0.1782
      0.5685    0.2193    0.9855    0.5377    0.4302    0.6260    0.0461    0.3020
      0.8043    0.4020    0.7725    0.8752    0.4004    0.4407    0.3026    0.2897
      0.7334    0.3235    0.1232    0.4334    0.9413    0.9238    0.3002    0.0405
      0.0204    0.3435    0.2585    0.3953    0.6620    0.6123    0.4614    0.9031
      0.0079    0.5695    0.9433    0.2294    0.0639    0.9997    0.7320    0.2345
   
   C = 
   
      0.9737    1.2403    1.6151    1.6725    1.4444    1.7549    1.0150    1.2489
      1.1411    2.0507    2.3559    1.9880    1.4000    2.1053    1.9483    1.3098
      1.7321    1.6633    2.7299    2.2087    1.6737    2.2195    1.3352    1.2472
      0.8036    1.3823    2.1113    1.5680    1.1823    2.1002    1.1038    1.0206
      2.0417    2.5373    3.1792    2.9120    2.1534    3.1089    2.2942    1.5734
      1.7811    1.5993    2.1976    1.7967    2.1150    2.4761    1.8056    1.5727
      1.8082    1.9322    2.5944    2.5470    1.9625    2.4145    1.3226    1.1127
      1.6513    2.1925    2.5108    2.6911    1.8972    2.1549    1.3312    0.9729
   
   D = 
   
      0.9737    1.2403    1.6151    1.6725    1.4444    1.7549    1.0150    1.2489
      1.1411    2.0507    2.3559    1.9880    1.4000    2.1053    1.9483    1.3098
      1.7321    1.6633    2.7299    2.2087    1.6737    2.2195    1.3352    1.2472
      0.8036    1.3823    2.1113    1.5680    1.1823    2.1002    1.1038    1.0206
      2.0417    2.5373    3.1792    2.9120    2.1534    3.1089    2.2942    1.5734
      1.7811    1.5993    2.1976    1.7967    2.1150    2.4761    1.8056    1.5727
      1.8082    1.9322    2.5944    2.5470    1.9625    2.4145    1.3226    1.1127
      1.6513    2.1925    2.5108    2.6911    1.8972    2.1549    1.3312    0.9729
   


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

   
      0.2259    0.7698    0.4157    0.3916    0.0169    0.7954
      0.2942    0.0260    0.7168    0.2754    0.5425    0.7737
      0.8984    0.2891    0.6693    0.3139    0.7114    0.2710
      0.3273    0.8872    0.1257    0.9217    0.1889    0.3514
      0.1976    0.7740    0.0379    0.7679    0.7854    0.7395
   
   
      0.8984
      0.7698
      0.8872
      0.7740
      0.7168
      0.6693
      0.9217
      0.7679
      0.5425
      0.7114
      0.7854
      0.7954
      0.7737
      0.7395
   

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

   
      7.6161    6.1335    9.2642    3.5684    9.4092    0.5256
      2.1031    8.8007    3.0213    6.1041    7.7771    6.0665
      1.0019    3.8571    0.9023    1.9833    2.5685    2.0091
      7.1141    8.3688    0.4379    5.4137    4.7906    5.4385
      1.0053    1.6437    3.6248    8.8285    6.7360    0.1244
   
   
      7.6161    6.1335    9.2642    0.0000    9.4092    0.0000
      0.0000    8.8007    0.0000    6.1041    7.7771    6.0665
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.1141    8.3688    0.0000    5.4137    0.0000    5.4385
      0.0000    0.0000    0.0000    8.8285    6.7360    0.0000
   
   
      7.6161    6.1335       NaN    0.0000       NaN    0.0000
      0.0000    8.8007    0.0000    6.1041    7.7771    6.0665
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.1141    8.3688    0.0000    5.4137    0.0000    5.4385
      0.0000    0.0000    0.0000    8.8285    6.7360    0.0000
   

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

   
      6.5000    2.1016    8.2477    0.6068    9.1741    6.5000
      6.5000    2.7523    4.9245    6.5000    9.4251    9.0647
      4.8814    6.5000    6.5000    3.3983    8.0139    6.5000
      1.2364    8.2627    1.8787    6.5000    1.1455    8.6946
      0.7579    6.5000    9.1573    6.5000    3.9502    1.3393
   
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
   
