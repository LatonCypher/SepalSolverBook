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
      0.0594    0.6969    0.8481    0.9097
   
   R1[2] = 0.8481142445981795
   C1 = 
      0.1561
      0.3775
      0.6325
      0.1100
      0.5909
      0.0609
      0.3681
      0.1467
   
   C1[5] = 0.0608533537106033

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
      0.0952    0.8822    0.0436    0.3819    0.5710
      0.9009    0.9159    0.1547    0.8140    0.4335
   

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
   
      0.4920    0.7463    0.0052    0.1363    0.4977    0.0208    0.2296    0.8367
      0.8489    0.4078    0.2107    0.7120    0.0901    0.1195    0.9554    0.4114
      0.9984    0.9456    0.5100    0.8236    0.1107    0.8494    0.8838    0.3465
      0.7694    0.3157    0.6256    0.5744    0.6084    0.0691    0.0006    0.7786
      0.8776    0.3828    0.3841    0.1271    0.5060    0.0282    0.8530    0.3879
      0.8484    0.3541    0.8899    0.7249    0.2005    0.5055    0.6228    0.6803
      0.6572    0.8072    0.7254    0.9701    0.0007    0.6014    0.8512    0.1502
      0.9737    0.9615    0.7403    0.6460    0.8101    0.1078    0.0195    0.5389
   
   B = 
   
      0.2218    0.7246    0.0523    0.1380    0.5032    0.2782    0.7960    0.9412
      0.0850    0.8304    0.7773    0.6762    0.0768    0.0585    0.7941    0.0365
      0.7223    0.5975    0.4046    0.2564    0.6292    0.0917    0.2410    0.4522
      0.5734    0.1313    0.7418    0.4604    0.6060    0.7712    0.6948    0.2687
      0.8532    0.7665    0.2679    0.0295    0.8921    0.2105    0.0051    0.1458
      0.7135    0.6098    0.9513    0.4065    0.8506    0.7990    0.9407    0.6950
      0.3268    0.6595    0.3550    0.3627    0.6071    0.3443    0.6602    0.7040
      0.2224    0.7843    0.5669    0.2936    0.2965    0.1439    0.9825    0.3849
   
   C = 
   
      0.9551    2.1990    1.4179    0.9887    1.2400    0.6069    2.0760    1.1000
      1.3492    2.2678    1.6849    1.2932    1.9066    1.3310    2.6929    2.0276
      2.2087    3.3789    2.9522    2.0579    2.8556    2.0710    3.9642    2.7881
      1.7204    2.3884    1.6351    1.0194    1.9859    1.0284    2.2466    1.6098
      1.3943    2.4718    1.2781    0.9866    1.8979    0.8782    2.1568    1.8910
      2.1632    2.9419    2.3586    1.5554    2.6420    1.6558    3.2308    2.4895
      2.0358    2.7537    2.6344    1.8664    2.5105    1.8400    3.2881    2.3118
      2.0971    3.1533    2.2089    1.5048    2.4072    1.2341    2.8138    1.8741
   
   D = 
   
      0.9551    2.1990    1.4179    0.9887    1.2400    0.6069    2.0760    1.1000
      1.3492    2.2678    1.6849    1.2932    1.9066    1.3310    2.6929    2.0276
      2.2087    3.3789    2.9522    2.0579    2.8556    2.0710    3.9642    2.7881
      1.7204    2.3884    1.6351    1.0194    1.9859    1.0284    2.2466    1.6098
      1.3943    2.4718    1.2781    0.9866    1.8979    0.8782    2.1568    1.8910
      2.1632    2.9419    2.3586    1.5554    2.6420    1.6558    3.2308    2.4895
      2.0358    2.7537    2.6344    1.8664    2.5105    1.8400    3.2881    2.3118
      2.0971    3.1533    2.2089    1.5048    2.4072    1.2341    2.8138    1.8741
   


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

   
      0.1422    0.3798    0.3272    0.3359    0.6519    0.4715
      0.0336    0.8489    0.4184    0.9635    0.7002    0.7619
      0.4325    0.0969    0.5343    0.2944    0.8043    0.1966
      0.8591    0.3523    0.4943    0.2390    0.6460    0.3339
      0.8782    0.7295    0.3712    0.2132    0.9944    0.3499
   
   
      0.8591
      0.8782
      0.8489
      0.7295
      0.5343
      0.9635
      0.6519
      0.7002
      0.8043
      0.6460
      0.9944
      0.7619
   

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

   
      1.7360    9.3196    2.0749    6.1729    5.9870    1.7059
      4.2603    1.2477    9.4291    5.9489    7.6192    1.0450
      2.8818    4.7484    0.1675    2.7162    9.4651    5.9868
      5.0612    8.7521    9.2396    9.8223    1.6098    1.6087
      3.6892    7.9557    8.4079    4.9921    6.8300    0.1096
   
   
      0.0000    9.3196    0.0000    6.1729    5.9870    0.0000
      0.0000    0.0000    9.4291    5.9489    7.6192    0.0000
      0.0000    0.0000    0.0000    0.0000    9.4651    5.9868
      5.0612    8.7521    9.2396    9.8223    0.0000    0.0000
      0.0000    7.9557    8.4079    0.0000    6.8300    0.0000
   
   
      0.0000       NaN    0.0000    6.1729    5.9870    0.0000
      0.0000    0.0000       NaN    5.9489    7.6192    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN    5.9868
      5.0612    8.7521       NaN       NaN    0.0000    0.0000
      0.0000    7.9557    8.4079    0.0000    6.8300    0.0000
   

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

   
      9.6644    2.4396    1.9964    6.5000    6.5000    6.5000
      4.8156    2.2309    6.5000    6.5000    0.4579    9.1649
      8.0309    1.8962    6.5000    8.3302    2.3723    6.5000
      3.1891    1.9778    4.7289    4.2285    6.5000    9.4486
      6.5000    0.7632    9.0575    3.8735    6.5000    8.1003
   
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
   
