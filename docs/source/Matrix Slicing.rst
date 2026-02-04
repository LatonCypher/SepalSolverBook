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
      0.5703    0.1898    0.6973    0.7573
   
   R1[2] = 0.6973159553724261
   C1 = 
      0.5550
      0.6310
      0.2549
      0.5385
      0.1835
      0.4506
      0.7389
      0.8758
   
   C1[5] = 0.4505739978116309

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
      0.5350    0.8600    0.8561    0.6609    0.3537
      0.2732    0.6521    0.5864    0.0136    0.4459
   

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
   M_3 &=& A_{11}\left(B_{12} - B_{22}\left) \\
   M_4 &=& A_{22}\left(B_{21} - B_{11}\left) \\
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
   
      0.7773    0.4976    0.5907    0.2679    0.7207    0.8744    0.5454    0.7086
      0.8448    0.2282    0.6490    0.8701    0.9258    0.6723    0.5672    0.4577
      0.4528    0.1114    0.3655    0.6524    0.9982    0.0648    0.4349    0.3237
      0.8076    0.7236    0.2052    0.1452    0.6905    0.0112    0.3716    0.6660
      0.9001    0.4218    0.1178    0.1764    0.6050    0.5269    0.1350    0.4716
      0.9336    0.9567    0.8206    0.4524    0.1728    0.5263    0.7998    0.7244
      0.2824    0.7679    0.0244    0.3983    0.9984    0.7065    0.3742    0.3490
      0.1817    0.1064    0.2916    0.2779    0.8581    0.5324    0.4799    0.4388
   
   B = 
   
      0.2176    0.8151    0.2872    0.6991    0.2923    0.9413    0.0802    0.6794
      0.2442    0.6769    0.1495    0.7829    0.5375    0.0020    0.1453    0.1737
      0.0039    0.4563    0.8047    0.6941    0.5470    0.3756    0.3775    0.2550
      0.3273    0.7033    0.0864    0.8496    0.2021    0.7072    0.2528    0.2629
      0.8459    0.5030    0.0354    0.6936    0.9018    0.5062    0.3820    0.7820
      0.5838    0.6826    0.7431    0.2131    0.6362    0.7695    0.0625    0.4807
      0.9663    0.6465    0.1572    0.9353    0.7831    0.5650    0.1704    0.0838
      0.9473    0.9557    0.3010    0.3539    0.7700    0.5368    0.1625    0.6717
   
   C = 
   
      2.6991    3.4177    1.7704    3.0178    3.0509    2.8703    0.9634    2.3412
      2.6842    3.4799    1.6335    3.4370    2.9598    3.2070    1.1325    2.4100
      1.9498    2.2070    0.7465    2.4393    2.0553    1.9998    0.8675    1.6574
      1.9814    2.5757    0.8094    2.4615    2.2002    1.8671    0.7201    1.7888
      1.7535    2.3990    1.0077    2.0162    1.9395    2.0582    0.5862    1.8156
      2.5006    3.7568    1.8516    3.5921    3.0023    2.8421    0.9910    2.0704
      2.3286    2.6011    0.9742    2.4706    2.5007    2.0060    0.7902    1.8223
      2.0736    2.0733    0.9603    1.9617    2.1522    1.8280    0.7245    1.5512
   
   D = 
   
      2.6991    3.4177    1.7704    3.0178    3.0509    2.8703    0.9634    2.3412
      2.6842    3.4799    1.6335    3.4370    2.9598    3.2070    1.1325    2.4100
      1.9498    2.2070    0.7465    2.4393    2.0553    1.9998    0.8675    1.6574
      1.9814    2.5757    0.8094    2.4615    2.2002    1.8671    0.7201    1.7888
      1.7535    2.3990    1.0077    2.0162    1.9395    2.0582    0.5862    1.8156
      2.5006    3.7568    1.8516    3.5921    3.0023    2.8421    0.9910    2.0704
      2.3286    2.6011    0.9742    2.4706    2.5007    2.0060    0.7902    1.8223
      2.0736    2.0733    0.9603    1.9617    2.1522    1.8280    0.7245    1.5512
   


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

   
      0.7475    0.0006    0.6844    0.1498    0.7809    0.5438
      0.4568    0.4856    0.9155    0.6795    0.1138    0.0003
      0.0460    0.3151    0.0075    0.5414    0.2718    0.3984
      0.8972    0.2184    0.8185    0.8429    0.6186    0.8186
      0.3772    0.1365    0.2258    0.5712    0.0245    0.3303
   
   
      0.7475
      0.8972
      0.6844
      0.9155
      0.8185
      0.6795
      0.5414
      0.8429
      0.5712
      0.7809
      0.6186
      0.5438
      0.8186
   

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

   
      6.4195    1.6214    6.4035    2.7023    7.7816    2.5255
      4.5074    2.4004    6.8992    2.4241    1.8175    3.1434
      4.3840    6.6195    9.4099    1.5870    3.4386    8.7312
      3.0836    7.6022    1.1040    9.6586    6.9247    1.7276
      5.3287    2.4950    8.7713    4.1648    1.4281    5.3238
   
   
      6.4195    0.0000    6.4035    0.0000    7.7816    0.0000
      0.0000    0.0000    6.8992    0.0000    0.0000    0.0000
      0.0000    6.6195    9.4099    0.0000    0.0000    8.7312
      0.0000    7.6022    0.0000    9.6586    6.9247    0.0000
      5.3287    0.0000    8.7713    0.0000    0.0000    5.3238
   
   
      6.4195    0.0000    6.4035    0.0000    7.7816    0.0000
      0.0000    0.0000    6.8992    0.0000    0.0000    0.0000
      0.0000    6.6195       NaN    0.0000    0.0000    8.7312
      0.0000    7.6022    0.0000       NaN    6.9247    0.0000
      5.3287    0.0000    8.7713    0.0000    0.0000    5.3238
   

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

   
      3.5540    4.1176    6.5000    6.5000    8.5709    4.0724
      3.8443    6.5000    6.5000    6.5000    6.5000    1.5054
      1.0290    1.0518    3.0942    6.5000    0.5377    4.3253
      4.3339    8.5062    3.3675    4.5884    6.5000    6.5000
      6.5000    6.5000    9.5858    1.5363    2.0282    0.0456
   
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
   
