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
      0.1726    0.0352    0.2448    0.7657
   
   R1[2] = 0.2447701907462353
   C1 = 
      0.4517
      0.3804
      0.1065
      0.5969
      0.9866
      0.7670
      0.6071
      0.0052
   
   C1[5] = 0.7670020981608332

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
      0.4112    0.3401    0.3422    0.2815    0.6409
      0.2876    0.6420    0.8982    0.1937    0.2206
   

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
   
      0.4609    0.7607    0.3064    0.7085    0.3102    0.5833    0.7129    0.3711
      0.9687    0.9958    0.6190    0.8410    0.1163    0.7993    0.8790    0.6276
      0.1592    0.6066    0.8648    0.9813    0.5760    0.6096    0.7655    0.1339
      0.8688    0.8116    0.0013    0.4304    0.9474    0.0061    0.1524    0.4489
      0.5279    0.0807    0.3119    0.0624    0.8540    0.3227    0.5092    0.2279
      0.5254    0.2878    0.8200    0.0431    0.6464    0.5495    0.7250    0.4926
      0.6216    0.5735    0.4288    0.2060    0.4765    0.5386    0.6602    0.5341
      0.0368    0.9471    0.0133    0.7387    0.9311    0.5787    0.9107    0.8713
   
   B = 
   
      0.4258    0.3753    0.0004    0.8622    0.3439    0.9445    0.0602    0.9080
      0.8692    0.4470    0.3131    0.5507    0.2036    0.6588    0.5437    0.0720
      0.7918    0.3504    0.6379    0.1327    0.9599    0.8793    0.1598    0.3259
      0.8466    0.7978    0.1477    0.4813    0.2485    0.9998    0.2900    0.7835
      0.5230    0.9588    0.9720    0.0376    0.1707    0.0852    0.3249    0.9660
      0.5881    0.0408    0.5830    0.4137    0.6634    0.9702    0.0007    0.8564
      0.3985    0.3163    0.5533    0.1840    0.2662    0.3706    0.9642    0.5046
      0.1335    0.5601    0.6924    0.3586    0.3949    0.4445    0.1450    0.8584
   
   C = 
   
      2.5388    1.9402    1.8315    1.7151    1.5599    2.9358    1.5381    2.6057
      3.4451    2.4703    2.3312    2.5922    2.3711    4.3462    1.9195    3.5911
      3.0932    2.3110    2.3181    1.5210    2.0116    3.2752    1.7072    2.8185
      2.0605    2.2410    1.6384    1.6304    0.9559    2.1294    1.1384    2.5677
      1.4645    1.5140    1.6914    0.9119    1.0982    1.5642    0.9453    2.1895
      2.1755    1.7949    2.3106    1.3026    1.8995    2.5258    1.3125    2.6824
      2.1775    1.7911    1.9960    1.5615    1.6187    2.5932    1.3467    2.6199
      2.7813    2.7236    2.7638    1.6649    1.5312    2.7745    2.0409    3.2871
   
   D = 
   
      2.5388    1.9402    1.8315    1.7151    1.5599    2.9358    1.5381    2.6057
      3.4451    2.4703    2.3312    2.5922    2.3711    4.3462    1.9195    3.5911
      3.0932    2.3110    2.3181    1.5210    2.0116    3.2752    1.7072    2.8185
      2.0605    2.2410    1.6384    1.6304    0.9559    2.1294    1.1384    2.5677
      1.4645    1.5140    1.6914    0.9119    1.0982    1.5642    0.9453    2.1895
      2.1755    1.7949    2.3106    1.3026    1.8995    2.5258    1.3125    2.6824
      2.1775    1.7911    1.9960    1.5615    1.6187    2.5932    1.3467    2.6199
      2.7813    2.7236    2.7638    1.6649    1.5312    2.7745    2.0409    3.2871
   


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

   
      0.8813    0.1990    0.7879    0.7162    0.0769    0.4427
      0.9341    0.8580    0.2875    0.2335    0.4104    0.0004
      0.5582    0.9522    0.6136    0.5168    0.0715    0.0891
      0.7911    0.4134    0.6817    0.2359    0.9287    0.6372
      0.5117    0.1621    0.0253    0.7466    0.9595    0.9876
   
   
      0.8813
      0.9341
      0.5582
      0.7911
      0.5117
      0.8580
      0.9522
      0.7879
      0.6136
      0.6817
      0.7162
      0.5168
      0.7466
      0.9287
      0.9595
      0.6372
      0.9876
   

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

   
      2.6884    6.8316    6.9885    2.2280    3.3224    6.8808
      6.3932    5.8089    6.0789    1.6291    4.6389    4.5033
      1.3905    7.2164    6.0639    2.4765    5.5676    0.2695
      6.3881    9.1809    9.9088    2.9497    4.1489    7.7840
      2.5899    8.8581    0.9513    0.2138    0.9332    7.7003
   
   
      0.0000    6.8316    6.9885    0.0000    0.0000    6.8808
      6.3932    5.8089    6.0789    0.0000    0.0000    0.0000
      0.0000    7.2164    6.0639    0.0000    5.5676    0.0000
      6.3881    9.1809    9.9088    0.0000    0.0000    7.7840
      0.0000    8.8581    0.0000    0.0000    0.0000    7.7003
   
   
      0.0000    6.8316    6.9885    0.0000    0.0000    6.8808
      6.3932    5.8089    6.0789    0.0000    0.0000    0.0000
      0.0000    7.2164    6.0639    0.0000    5.5676    0.0000
      6.3881       NaN       NaN    0.0000    0.0000    7.7840
      0.0000    8.8581    0.0000    0.0000    0.0000    7.7003
   

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

   
      4.7206    6.5000    9.3652    3.7554    6.5000    6.5000
      3.4532    4.3827    6.5000    8.9000    2.7151    6.5000
      8.6082    2.5660    6.5000    6.5000    4.6802    6.5000
      8.9506    2.5758    1.3292    6.5000    0.2517    1.8137
      2.1814    6.5000    6.5000    6.5000    3.4378    1.4660
   
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
   
