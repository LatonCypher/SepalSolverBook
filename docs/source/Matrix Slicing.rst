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
      0.1314    0.3569    0.6125    0.4919
   
   R1[2] = 0.6125424924728151
   C1 = 
      0.3030
      0.9771
      0.1615
      0.9488
      0.3634
      0.6012
      0.4483
      0.9846
   
   C1[5] = 0.6012059078010592

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
      0.8213    0.2822    0.2546    0.7285    0.9801
      0.8129    0.6266    0.8272    0.4961    0.2501
   

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
   
      0.0876    0.1828    0.2325    0.7727    0.4639    0.5755    0.9509    0.7265
      0.7013    0.0403    0.1194    0.6518    0.7475    0.6743    0.8032    0.3070
      0.8136    0.2536    0.8305    0.2511    0.1861    0.4570    0.5901    0.8500
      0.4831    0.7810    0.7207    0.2238    0.9860    0.5538    0.5879    0.4656
      0.0401    0.5519    0.5831    0.1036    0.1377    0.9829    0.2477    0.3114
      0.6068    0.9497    0.5837    0.8965    0.3496    0.4480    0.2798    0.9095
      0.8658    0.2034    0.3353    0.5209    0.1023    0.5788    0.9461    0.3621
      0.0437    0.7204    0.5956    0.4595    0.1308    0.2367    0.4352    0.0802
   
   B = 
   
      0.4756    0.6258    0.7138    0.1274    0.7759    0.7131    0.9795    0.9374
      0.8709    0.3920    0.2975    0.3162    0.2338    0.4882    0.6837    0.1402
      0.2119    0.2922    0.8515    0.5555    0.0059    0.9553    0.0631    0.4089
      0.4690    0.6565    0.5886    0.8826    0.5100    0.1258    0.3787    0.9377
      0.5665    0.0170    0.5672    0.0822    0.2415    0.3128    0.1389    0.8119
      0.4159    0.0954    0.0117    0.7911    0.8547    0.4923    0.4356    0.7140
      0.1012    0.3227    0.7721    0.7720    0.9051    0.0384    0.7998    0.6041
      0.3654    0.1327    0.1422    0.1759    0.1091    0.2136    0.3403    0.9957
   
   C = 
   
      1.4763    1.1677    1.8770    2.2354    2.0499    1.0911    1.8409    3.0126
      1.5969    1.2944    2.0935    2.0127    2.4039    1.3780    2.1133    3.2023
      1.5673    1.3660    2.1984    1.8488    1.8859    2.0164    2.1040    3.0534
      2.1860    1.2870    2.4083    1.9613    1.9701    2.1454    2.1442    3.0815
      1.2975    0.6971    1.0754    1.6299    1.3479    1.4710    1.2440    1.7239
      2.4048    1.7707    2.2891    2.2522    1.9732    2.1016    2.3969    3.4594
      1.4309    1.4717    2.1173    2.0810    2.4021    1.5332    2.3518    2.8940
      1.2357    0.9612    1.4474    1.5179    1.0767    1.2009    1.2435    1.4343
   
   D = 
   
      1.4763    1.1677    1.8770    2.2354    2.0499    1.0911    1.8409    3.0126
      1.5969    1.2944    2.0935    2.0127    2.4039    1.3780    2.1133    3.2023
      1.5673    1.3660    2.1984    1.8488    1.8859    2.0164    2.1040    3.0534
      2.1860    1.2870    2.4083    1.9613    1.9701    2.1454    2.1442    3.0815
      1.2975    0.6971    1.0754    1.6299    1.3479    1.4710    1.2440    1.7239
      2.4048    1.7707    2.2891    2.2522    1.9732    2.1016    2.3969    3.4594
      1.4309    1.4717    2.1173    2.0810    2.4021    1.5332    2.3518    2.8940
      1.2357    0.9612    1.4474    1.5179    1.0767    1.2009    1.2435    1.4343
   


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

   
      0.7653    0.0761    0.7160    0.4140    0.5633    0.2051
      0.7031    0.2606    0.2803    0.2273    0.7276    0.7370
      0.8443    0.8710    0.4152    0.5195    0.0451    0.7285
      0.3553    0.8841    0.2519    0.5707    0.1301    0.7582
      0.9499    0.8348    0.4023    0.8570    0.9366    0.2841
   
   
      0.7653
      0.7031
      0.8443
      0.9499
      0.8710
      0.8841
      0.8348
      0.7160
      0.5195
      0.5707
      0.8570
      0.5633
      0.7276
      0.9366
      0.7370
      0.7285
      0.7582
   

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

   
      9.2687    4.0863    9.1942    7.4352    9.8735    6.8595
      7.0928    9.9244    7.6509    8.6412    6.1727    9.0061
      9.2007    9.6178    8.5150    1.0362    4.8755    3.8556
      1.3827    4.5592    1.8863    7.1550    0.7218    7.1907
      3.6544    2.7007    6.7230    5.7381    0.4007    9.1101
   
   
      9.2687    0.0000    9.1942    7.4352    9.8735    6.8595
      7.0928    9.9244    7.6509    8.6412    6.1727    9.0061
      9.2007    9.6178    8.5150    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.1550    0.0000    7.1907
      0.0000    0.0000    6.7230    5.7381    0.0000    9.1101
   
   
         NaN    0.0000       NaN    7.4352       NaN    6.8595
      7.0928       NaN    7.6509    8.6412    6.1727       NaN
         NaN       NaN    8.5150    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    7.1550    0.0000    7.1907
      0.0000    0.0000    6.7230    5.7381    0.0000       NaN
   

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

   
      0.8833    6.5000    0.8742    4.1854    9.3706    9.5966
      4.0187    6.5000    0.8306    6.5000    6.5000    2.6365
      2.1623    6.5000    9.3745    1.8571    0.4836    1.9995
      2.0117    4.7755    6.5000    6.5000    8.7234    6.5000
      0.7647    1.6765    8.7782    1.2030    2.6187    2.7373
   
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
   
