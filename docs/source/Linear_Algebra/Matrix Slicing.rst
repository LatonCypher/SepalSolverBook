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
      0.7269    0.9657    0.0326    0.8729
   
   R1[2] = 0.03263365827961373
   C1 = 
      0.3062
      0.2819
      0.1064
      0.8519
      0.6450
      0.7957
      0.1213
      0.5544
   
   C1[5] = 0.7956880626372389

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
      0.1467    0.3890    0.0223    0.3769    0.4244
      0.6510    0.3957    0.8376    0.3795    0.0018
   

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
   
      0.6517    0.3292    0.4881    0.7565    0.4915    0.0587    0.1327    0.7782
      0.8716    0.6736    0.2554    0.0503    0.3237    0.0708    0.9982    0.0787
      0.5291    0.3306    0.0899    0.5933    0.8339    0.1209    0.3530    0.8044
      0.8590    0.8796    0.2322    0.7948    0.4071    0.8251    0.5164    0.6577
      0.7943    0.5446    0.4573    0.9923    0.3175    0.7446    0.9989    0.2847
      0.0225    0.9022    0.1671    0.6534    0.5713    0.7879    0.7229    0.1486
      0.8221    0.3317    0.6212    0.7666    0.5772    0.5804    0.5167    0.6369
      0.1368    0.1197    0.1360    0.1337    0.4858    0.0329    0.9015    0.4371
   
   B = 
   
      0.7176    0.8648    0.7868    0.6834    0.1230    0.1224    0.4470    0.9755
      0.0107    0.3708    0.4891    0.6550    0.5633    0.0757    0.0967    0.3515
      0.1775    0.1736    0.7318    0.2747    0.0141    0.0471    0.4330    0.0918
      0.4293    0.5636    0.6942    0.5024    0.2444    0.6325    0.9881    0.3616
      0.4790    0.5471    0.4620    0.2472    0.6319    0.9794    0.8177    0.7364
      0.0969    0.8514    0.1350    0.5121    0.0077    0.8790    0.5415    0.5127
      0.1030    0.8409    0.3058    0.9189    0.7549    0.6066    0.8701    0.9186
      0.7261    0.0132    0.1554    0.6199    0.5636    0.3233    0.0320    0.3850
   
   C = 
   
      1.7025    1.6375    1.9527    1.9312    1.3073    1.4713    1.8561    1.8835
      1.0215    2.1538    1.7135    2.2146    1.5056    1.2117    1.7891    2.3505
      1.6856    1.7967    1.6902    1.9920    1.6454    1.8666    1.9740    2.1652
      1.8140    2.9253    2.3873    3.0317    1.8228    2.3353    2.6049    2.9062
      1.6168    3.1787    2.5117    3.0779    1.7744    2.4511    3.1273    3.0098
      0.8683    2.3444    1.6492    2.2816    1.6696    2.2309    2.3428    2.1366
      1.8813    2.6265    2.3978    2.6444    1.6024    2.2347    2.6822    2.6952
      0.8272    1.3193    0.9311    1.5128    1.3531    1.3097    1.4773    1.6074
   
   D = 
   
      1.7025    1.6375    1.9527    1.9312    1.3073    1.4713    1.8561    1.8835
      1.0215    2.1538    1.7135    2.2146    1.5056    1.2117    1.7891    2.3505
      1.6856    1.7967    1.6902    1.9920    1.6454    1.8666    1.9740    2.1652
      1.8140    2.9253    2.3873    3.0317    1.8228    2.3353    2.6049    2.9062
      1.6168    3.1787    2.5117    3.0779    1.7744    2.4511    3.1273    3.0098
      0.8683    2.3444    1.6492    2.2816    1.6696    2.2309    2.3428    2.1366
      1.8813    2.6265    2.3978    2.6444    1.6024    2.2347    2.6822    2.6952
      0.8272    1.3193    0.9311    1.5128    1.3531    1.3097    1.4773    1.6074
   


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

   
      0.7697    0.7519    0.1552    0.7895    0.8562    0.5225
      0.6474    0.9680    0.5310    0.2809    0.0135    0.5034
      0.0754    0.6566    0.8522    0.4108    0.8179    0.7576
      0.8678    0.4218    0.0609    0.5328    0.8154    0.9040
      0.3886    0.9530    0.6442    0.6634    0.0366    0.5977
   
   
      0.7697
      0.6474
      0.8678
      0.7519
      0.9680
      0.6566
      0.9530
      0.5310
      0.8522
      0.6442
      0.7895
      0.5328
      0.6634
      0.8562
      0.8179
      0.8154
      0.5225
      0.5034
      0.7576
      0.9040
      0.5977
   

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

   
      7.8761    2.1047    9.6295    4.9676    8.6468    9.7031
      4.7493    7.0843    8.7380    0.9212    5.1365    7.9049
      9.2372    7.1966    7.5050    5.9079    3.7878    7.1747
      8.8970    3.1160    4.2726    0.2851    5.4541    5.3459
      4.5864    8.2642    8.7491    4.6743    9.6560    8.1598
   
   
      7.8761    0.0000    9.6295    0.0000    8.6468    9.7031
      0.0000    7.0843    8.7380    0.0000    5.1365    7.9049
      9.2372    7.1966    7.5050    5.9079    0.0000    7.1747
      8.8970    0.0000    0.0000    0.0000    5.4541    5.3459
      0.0000    8.2642    8.7491    0.0000    9.6560    8.1598
   
   
      7.8761    0.0000       NaN    0.0000    8.6468       NaN
      0.0000    7.0843    8.7380    0.0000    5.1365    7.9049
         NaN    7.1966    7.5050    5.9079    0.0000    7.1747
      8.8970    0.0000    0.0000    0.0000    5.4541    5.3459
      0.0000    8.2642    8.7491    0.0000       NaN    8.1598
   

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

   
      2.6061    1.5675    6.5000    6.5000    4.8910    6.5000
      9.6988    3.9588    8.8068    4.5600    6.5000    9.2307
      4.9583    1.4166    9.1952    6.5000    1.7342    6.5000
      6.5000    4.7888    8.0342    8.4624    3.4827    1.2351
      3.6293    9.4220    3.5881    6.5000    2.8507    6.5000
   
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
   
