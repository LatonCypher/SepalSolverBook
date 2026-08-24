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
      0.3958    0.5344    0.2734    0.9826
   
   R1[2] = 0.2734187672605888
   C1 = 
      0.8064
      0.4077
      0.6253
      0.0982
      0.8833
      0.2766
      0.2679
      0.0847
   
   C1[5] = 0.27660831580770373

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
      0.2334    0.4751    0.9407    0.5086    0.5701
      0.5576    0.0667    0.8545    0.5057    0.1026
   

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
   
      0.4052    0.2335    0.5064    0.2682    0.6116    0.8410    0.8414    0.3695
      0.4222    0.2994    0.2743    0.3936    0.5817    0.3006    0.5754    0.8681
      0.1062    0.7735    0.7894    0.5690    0.5454    0.6004    0.2087    0.8185
      0.8847    0.0001    0.6520    0.7561    0.1281    0.8985    0.3001    0.7789
      0.6920    0.6608    0.4617    0.5726    0.1679    0.6482    0.6576    0.2223
      0.2573    0.0615    0.2599    0.3919    0.3127    0.2822    0.3827    0.0110
      0.7651    0.6679    0.2529    0.2745    0.8203    0.2230    0.1940    0.8728
      0.6946    0.3653    0.3285    0.5892    0.0181    0.5491    0.5459    0.0465
   
   B = 
   
      0.9274    0.6444    0.0608    0.8729    0.3270    0.8641    0.1966    0.7292
      0.0022    0.2057    0.2174    0.0898    0.8114    0.1868    0.9180    0.6471
      0.4286    0.6228    0.7362    0.6378    0.9801    0.1246    0.3592    0.2026
      0.4713    0.7500    0.5771    0.4519    0.0641    0.2865    0.0088    0.5266
      0.2539    0.9579    0.1282    0.9142    0.8573    0.1574    0.9607    0.3905
      0.5735    0.1643    0.6372    0.6969    0.6069    0.8667    0.2393    0.7297
      0.9757    0.9731    0.2429    0.4795    0.9467    0.7684    0.7895    0.1661
      0.4587    0.2587    0.6947    0.5658    0.1946    0.1130    0.6254    0.1353
   
   C = 
   
      2.3478    2.4641    1.6785    2.5767    2.7388    2.0473    2.1625    1.7327
      1.9749    2.1908    1.5289    2.2567    2.0700    1.4601    2.0878    1.4240
      1.7685    2.1818    2.1559    2.4030    2.6613    1.3568    2.3637    1.8339
      2.6540    2.3070    2.1731    2.8576    2.0677    2.1799    1.4769    2.0363
      2.2690    2.2637    1.6049    2.2631    2.4549    2.0618    1.8882    2.0052
      1.1546    1.3555    0.7671    1.2453    1.2178    0.9677    0.8808    0.8796
      1.8745    2.2307    1.4370    2.5054    2.2498    1.4662    2.3971    1.8192
      1.9369    1.8200    1.2206    1.8022    1.7580    1.7817    1.2040    1.6243
   
   D = 
   
      2.3478    2.4641    1.6785    2.5767    2.7388    2.0473    2.1625    1.7327
      1.9749    2.1908    1.5289    2.2567    2.0700    1.4601    2.0878    1.4240
      1.7685    2.1818    2.1559    2.4030    2.6613    1.3568    2.3637    1.8339
      2.6540    2.3070    2.1731    2.8576    2.0677    2.1799    1.4769    2.0363
      2.2690    2.2637    1.6049    2.2631    2.4549    2.0618    1.8882    2.0052
      1.1546    1.3555    0.7671    1.2453    1.2178    0.9677    0.8808    0.8796
      1.8745    2.2307    1.4370    2.5054    2.2498    1.4662    2.3971    1.8192
      1.9369    1.8200    1.2206    1.8022    1.7580    1.7817    1.2040    1.6243
   


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

   
      0.0172    0.1229    0.4268    0.8017    0.2148    0.1779
      0.7250    0.8473    0.9396    0.8456    0.7910    0.7065
      0.8607    0.8403    0.3736    0.8742    0.5133    0.5228
      0.9014    0.7150    0.5187    0.8986    0.6647    0.5126
      0.5135    0.7850    0.7022    0.4034    0.9598    0.2359
   
   
      0.7250
      0.8607
      0.9014
      0.5135
      0.8473
      0.8403
      0.7150
      0.7850
      0.9396
      0.5187
      0.7022
      0.8017
      0.8456
      0.8742
      0.8986
      0.7910
      0.5133
      0.6647
      0.9598
      0.7065
      0.5228
      0.5126
   

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

   
      5.7564    3.9848    0.2610    1.0314    0.5183    8.5955
      6.0128    9.4270    0.7677    6.5870    6.9658    9.8604
      9.1070    0.6195    2.4845    8.1202    7.0596    3.5858
      5.6703    6.4982    2.7525    6.6282    5.5920    6.2325
      3.5725    5.1954    7.8000    6.9730    2.5941    8.5788
   
   
      5.7564    0.0000    0.0000    0.0000    0.0000    8.5955
      6.0128    9.4270    0.0000    6.5870    6.9658    9.8604
      9.1070    0.0000    0.0000    8.1202    7.0596    0.0000
      5.6703    6.4982    0.0000    6.6282    5.5920    6.2325
      0.0000    5.1954    7.8000    6.9730    0.0000    8.5788
   
   
      5.7564    0.0000    0.0000    0.0000    0.0000    8.5955
      6.0128       NaN    0.0000    6.5870    6.9658       NaN
         NaN    0.0000    0.0000    8.1202    7.0596    0.0000
      5.6703    6.4982    0.0000    6.6282    5.5920    6.2325
      0.0000    5.1954    7.8000    6.9730    0.0000    8.5788
   

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

   
      3.4027    3.7814    8.1454    3.8358    4.9197    6.5000
      3.6173    6.5000    9.8598    2.8702    1.7947    6.5000
      8.7903    0.6140    4.7268    4.8078    6.5000    2.6555
      6.5000    9.3521    4.7081    3.9536    6.5000    4.7840
      6.5000    6.5000    6.5000    1.2675    6.5000    9.7058
   
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
   
