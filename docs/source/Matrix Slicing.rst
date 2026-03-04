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
      0.8529    0.2242    0.1486    0.0223
   
   R1[2] = 0.14859225757682515
   C1 = 
      0.5876
      0.7717
      0.8033
      0.1289
      0.6325
      0.1325
      0.1622
      0.5135
   
   C1[5] = 0.1325121461674026

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
      0.3352    0.2642    0.8709    0.6010    0.1966
      0.4457    0.3133    0.0110    0.6965    0.8147
   

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
   
      0.2366    0.7294    0.9086    0.3098    0.0196    0.3583    0.3317    0.0863
      0.7747    0.3038    0.4681    0.1720    0.3484    0.5891    0.7041    0.5672
      0.6100    0.0945    0.5775    0.6813    0.3167    0.2236    0.9181    0.6106
      0.0623    0.4462    0.7157    0.8855    0.0375    0.2748    0.6792    0.3700
      0.8940    0.8622    0.2022    0.6903    0.7749    0.0479    0.9275    0.0186
      0.8212    0.0472    0.9324    0.5217    0.9828    0.2981    0.6045    0.5784
      0.6768    0.7115    0.7590    0.7758    0.1020    0.5766    0.9001    0.1192
      0.7735    0.2774    0.8720    0.5866    0.7576    0.6290    0.3395    0.5895
   
   B = 
   
      0.3011    0.5686    0.3570    0.6887    0.9163    0.5813    0.1374    0.4160
      0.7706    0.0988    0.7433    0.1215    0.4064    0.2778    0.1928    0.7091
      0.7115    0.8002    0.1372    0.4235    0.6269    0.5971    0.0846    0.6870
      0.6395    0.5608    0.2708    0.9177    0.2482    0.9354    0.9468    0.7589
      0.2316    0.0550    0.5045    0.9941    0.9130    0.9465    0.9963    0.1394
      0.9096    0.0841    0.4636    0.1743    0.4687    0.3076    0.9485    0.2356
      0.8647    0.4047    0.0783    0.6053    0.5987    0.4868    0.1735    0.0342
      0.6732    0.1873    0.3831    0.3399    0.8655    0.6995    0.3358    0.1794
   
   C = 
   
      2.1533    1.2891    1.0703    1.2327    1.6189    1.5232    0.9892    1.5890
      2.5176    1.4014    1.3345    1.9945    2.6761    2.2256    1.5860    1.3029
      2.5848    1.7225    1.1209    2.4183    2.6005    2.6056    1.6878    1.4722
      2.5331    1.5181    1.0331    1.8348    1.7967    2.1253    1.5335    1.6655
      2.5565    1.5680    1.6676    2.7859    2.7690    2.7385    1.9444    1.8005
      2.6916    1.9424    1.5004    3.0363    3.3852    3.2561    2.2558    1.7432
      3.1949    1.9382    1.5196    2.3731    2.5831    2.5655    1.8735    2.0985
      2.8803    1.8364    1.6871    2.7426    3.2137    3.0844    2.3972    1.9339
   
   D = 
   
      2.1533    1.2891    1.0703    1.2327    1.6189    1.5232    0.9892    1.5890
      2.5176    1.4014    1.3345    1.9945    2.6761    2.2256    1.5860    1.3029
      2.5848    1.7225    1.1209    2.4183    2.6005    2.6056    1.6878    1.4722
      2.5331    1.5181    1.0331    1.8348    1.7967    2.1253    1.5335    1.6655
      2.5565    1.5680    1.6676    2.7859    2.7690    2.7385    1.9444    1.8005
      2.6916    1.9424    1.5004    3.0363    3.3852    3.2561    2.2558    1.7432
      3.1949    1.9382    1.5196    2.3731    2.5831    2.5655    1.8735    2.0985
      2.8803    1.8364    1.6871    2.7426    3.2137    3.0844    2.3972    1.9339
   


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

   
      0.2043    0.1696    0.9348    0.1414    0.2974    0.5879
      0.8038    0.2091    0.3259    0.0892    0.7873    0.8560
      0.7257    0.4984    0.1686    0.1981    0.9608    0.8763
      0.3517    0.7697    0.2923    0.9680    0.8359    0.2977
      0.8048    0.9100    0.5966    0.3702    0.4689    0.7214
   
   
      0.8038
      0.7257
      0.8048
      0.7697
      0.9100
      0.9348
      0.5966
      0.9680
      0.7873
      0.9608
      0.8359
      0.5879
      0.8560
      0.8763
      0.7214
   

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

   
      9.9592    7.1768    1.5616    1.4171    0.2913    1.5073
      8.1002    4.2336    2.7951    7.6557    4.2211    2.9877
      3.7603    4.6974    5.0851    1.7828    0.4975    7.5363
      7.2498    8.0268    0.4998    6.0991    9.7236    5.1709
      1.5147    7.3665    8.1936    1.7642    8.0158    6.5148
   
   
      9.9592    7.1768    0.0000    0.0000    0.0000    0.0000
      8.1002    0.0000    0.0000    7.6557    0.0000    0.0000
      0.0000    0.0000    5.0851    0.0000    0.0000    7.5363
      7.2498    8.0268    0.0000    6.0991    9.7236    5.1709
      0.0000    7.3665    8.1936    0.0000    8.0158    6.5148
   
   
         NaN    7.1768    0.0000    0.0000    0.0000    0.0000
      8.1002    0.0000    0.0000    7.6557    0.0000    0.0000
      0.0000    0.0000    5.0851    0.0000    0.0000    7.5363
      7.2498    8.0268    0.0000    6.0991       NaN    5.1709
      0.0000    7.3665    8.1936    0.0000    8.0158    6.5148
   

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

   
      1.7224    4.5047    6.5000    6.5000    6.5000    2.8793
      8.4033    6.5000    8.2282    1.1354    9.3738    2.1397
      6.5000    3.0638    1.3561    6.5000    6.5000    6.5000
      0.4200    6.5000    6.5000    2.8183    2.8839    0.0488
      6.5000    3.7312    6.5000    4.5870    6.5000    1.6264
   
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
   
