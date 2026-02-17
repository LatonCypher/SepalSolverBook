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
      0.2415    0.2132    0.6574    0.3988
   
   R1[2] = 0.6574168055947687
   C1 = 
      0.5762
      0.2565
      0.7415
      0.4133
      0.0661
      0.6501
      0.7164
      0.2065
   
   C1[5] = 0.6500524561347477

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
      0.6586    0.9362    0.6964    0.6669    0.7154
      0.6111    0.1163    0.9569    0.8086    0.4361
   

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
   
      0.0453    0.6288    0.1830    0.3597    0.7028    0.2136    0.7252    0.1032
      0.9701    0.2986    0.0790    0.0565    0.4955    0.4758    0.9196    0.0378
      0.9452    0.4614    0.9862    0.7113    0.1769    0.1763    0.5871    0.7949
      0.3640    0.1886    0.9325    0.4057    0.2848    0.5184    0.2443    0.8835
      0.7653    0.4909    0.8111    0.2382    0.2683    0.6502    0.8902    0.2632
      0.9332    0.0153    0.4415    0.5816    0.4916    0.3190    0.0224    0.7900
      0.0924    0.7766    0.4466    0.4697    0.5405    0.6125    0.2777    0.4061
      0.9348    0.2715    0.6581    0.2308    0.1107    0.1859    0.8239    0.9748
   
   B = 
   
      0.7358    0.8609    0.6148    0.3977    0.1414    0.2877    0.3580    0.1893
      0.3932    0.9880    0.9363    0.7249    0.5058    0.9616    0.7824    0.8188
      0.8912    0.8801    0.7348    0.0070    0.9834    0.5154    0.1890    0.0502
      0.4403    0.6121    0.2538    0.5813    0.2463    0.0254    0.6270    0.4682
      0.8586    0.4158    0.3646    0.5325    0.5701    0.5596    0.7328    0.4610
      0.9962    0.3762    0.4740    0.6749    0.7639    0.2033    0.0952    0.8060
      0.4460    0.3682    0.6319    0.5617    0.0804    0.4564    0.7369    0.4720
      0.2909    0.3916    0.5616    0.3667    0.8347    0.4625    0.8852    0.4068
   
   C = 
   
      1.7717    1.7215    1.7161    1.6478    1.3013    1.5366    1.9294    1.5814
      2.2471    1.9727    1.9569    1.7510    1.1312    1.4196    1.7508    1.5198
      2.8896    3.2402    2.8837    1.9652    2.4582    2.0124    2.6145    1.7633
      2.4786    2.3180    2.1886    1.4865    2.4792    1.5619    1.9286    1.4838
      2.9355    2.7904    2.7028    1.9825    2.1536    1.9266    2.1081    1.8740
      2.3219    2.2051    1.8483    1.5028    1.9022    1.2410    1.9006    1.2993
      2.2944    2.2438    2.1223    1.8819    2.0980    1.7571    2.0381    1.9348
      2.4140    2.5945    2.5676    1.7121    2.0586    1.8017    2.3851    1.5267
   
   D = 
   
      1.7717    1.7215    1.7161    1.6478    1.3013    1.5366    1.9294    1.5814
      2.2471    1.9727    1.9569    1.7510    1.1312    1.4196    1.7508    1.5198
      2.8896    3.2402    2.8837    1.9652    2.4582    2.0124    2.6145    1.7633
      2.4786    2.3180    2.1886    1.4865    2.4792    1.5619    1.9286    1.4838
      2.9355    2.7904    2.7028    1.9825    2.1536    1.9266    2.1081    1.8740
      2.3219    2.2051    1.8483    1.5028    1.9022    1.2410    1.9006    1.2993
      2.2944    2.2438    2.1223    1.8819    2.0980    1.7571    2.0381    1.9348
      2.4140    2.5945    2.5676    1.7121    2.0586    1.8017    2.3851    1.5267
   


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

   
      0.8213    0.4395    0.7518    0.1662    0.2591    0.3939
      0.9905    0.2547    0.3735    0.9433    0.0337    0.1300
      0.7834    0.6898    0.6240    0.2488    0.8060    0.9849
      0.9514    0.5418    0.7358    0.7604    0.8500    0.0275
      0.6772    0.9443    0.8347    0.4828    0.6907    0.9034
   
   
      0.8213
      0.9905
      0.7834
      0.9514
      0.6772
      0.6898
      0.5418
      0.9443
      0.7518
      0.6240
      0.7358
      0.8347
      0.9433
      0.7604
      0.8060
      0.8500
      0.6907
      0.9849
      0.9034
   

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

   
      7.7126    8.1093    4.2266    5.9404    0.4044    7.5038
      4.3176    9.0383    1.7303    2.4376    3.9105    5.0098
      6.1888    4.2534    1.7378    8.3126    7.8961    0.0472
      2.6290    3.0862    8.8508    2.0854    5.7035    5.2062
      9.1462    5.8643    2.3280    6.9031    1.7216    8.1800
   
   
      7.7126    8.1093    0.0000    5.9404    0.0000    7.5038
      0.0000    9.0383    0.0000    0.0000    0.0000    5.0098
      6.1888    0.0000    0.0000    8.3126    7.8961    0.0000
      0.0000    0.0000    8.8508    0.0000    5.7035    5.2062
      9.1462    5.8643    0.0000    6.9031    0.0000    8.1800
   
   
      7.7126    8.1093    0.0000    5.9404    0.0000    7.5038
      0.0000       NaN    0.0000    0.0000    0.0000    5.0098
      6.1888    0.0000    0.0000    8.3126    7.8961    0.0000
      0.0000    0.0000    8.8508    0.0000    5.7035    5.2062
         NaN    5.8643    0.0000    6.9031    0.0000    8.1800
   

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

   
      2.3109    1.4179    6.5000    8.1082    3.0595    3.0604
      2.3058    8.0042    8.5762    9.7299    6.5000    6.5000
      6.5000    3.5134    6.5000    4.7584    4.8324    3.5006
      0.9108    3.7515    6.5000    8.4177    3.9851    8.5456
      2.8489    6.5000    2.4077    2.7452    9.1690    3.6758
   
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
   
