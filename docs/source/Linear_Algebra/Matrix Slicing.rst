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
      0.4950    0.0763    0.8144    0.8322
   
   R1[2] = 0.8143773317416334
   C1 = 
      0.2372
      0.7898
      0.3795
      0.4281
      0.4347
      0.7892
      0.3133
      0.3645
   
   C1[5] = 0.789157379393543

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
      0.1566    0.8739    0.9286    0.9753    0.6468
      0.6030    0.3543    0.5364    0.6363    0.2747
   

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
   
      0.1247    0.1510    0.4861    0.2990    0.4106    0.0153    0.2932    0.8620
      0.3549    0.7415    0.2269    0.6696    0.3686    0.3043    0.5666    0.7075
      0.6240    0.4061    0.4812    0.0102    0.3368    0.3034    0.8629    0.5811
      0.5779    0.5055    0.0118    0.0638    0.3604    0.6994    0.1697    0.0877
      0.4534    0.5776    0.5034    0.6768    0.1569    0.4388    0.9197    0.3493
      0.8636    0.9212    0.4762    0.9884    0.2623    0.9668    0.1920    0.2543
      0.4931    0.4666    0.2135    0.3137    0.6567    0.8047    0.0136    0.6015
      0.2915    0.8362    0.0225    0.2194    0.9925    0.3048    0.9514    0.9748
   
   B = 
   
      0.1080    0.3538    0.4008    0.6287    0.6004    0.1130    0.2263    0.4852
      0.6854    0.0046    0.2530    0.9447    0.1337    0.2194    0.1733    0.8341
      0.6494    0.6179    0.2257    0.2601    0.7139    0.5481    0.0065    0.5113
      0.4910    0.7658    0.5295    0.0181    0.8635    0.0019    0.7767    0.2009
      0.5220    0.9227    0.9670    0.7448    0.9471    0.9563    0.2823    0.6826
      0.1160    0.7788    0.5476    0.1676    0.6815    0.2789    0.7947    0.4844
      0.7431    0.0658    0.7844    0.1589    0.9364    0.1783    0.1831    0.5274
      0.7663    0.5172    0.0834    0.3965    0.6472    0.7854    0.8798    0.2381
   
   C = 
   
      1.6739    1.4300    1.0634    1.0496    1.9319    1.4404    1.2299    1.1425
      2.2136    1.7622    1.7620    1.6909    2.5973    1.4224    1.8025    1.9075
      1.9608    1.4322    1.6840    1.5706    2.4913    1.4403    1.2281    1.8600
      0.9104    1.1966    1.2678    1.2925    1.5117    0.8215    1.0338    1.4161
      2.1880    1.7202    1.9424    1.4490    2.8283    1.1660    1.6005    1.9830
      2.1059    2.5001    2.1649    2.0438    3.0868    1.3171    2.2272    2.4387
      1.5729    2.0933    1.6664    1.6767    2.3541    1.6029    1.7942    1.7891
      2.7343    2.0087    2.4037    2.3110    3.1618    2.1985    1.9357    2.4534
   
   D = 
   
      1.6739    1.4300    1.0634    1.0496    1.9319    1.4404    1.2299    1.1425
      2.2136    1.7622    1.7620    1.6909    2.5973    1.4224    1.8025    1.9075
      1.9608    1.4322    1.6840    1.5706    2.4913    1.4403    1.2281    1.8600
      0.9104    1.1966    1.2678    1.2925    1.5117    0.8215    1.0338    1.4161
      2.1880    1.7202    1.9424    1.4490    2.8283    1.1660    1.6005    1.9830
      2.1059    2.5001    2.1649    2.0438    3.0868    1.3171    2.2272    2.4387
      1.5729    2.0933    1.6664    1.6767    2.3541    1.6029    1.7942    1.7891
      2.7343    2.0087    2.4037    2.3110    3.1618    2.1985    1.9357    2.4534
   


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

   
      0.8077    0.6145    0.2855    0.0864    0.4095    0.5863
      0.8435    0.4419    0.0969    0.6494    0.4914    0.1676
      0.6323    0.3838    0.8485    0.4569    0.2668    0.0511
      0.7610    0.2382    0.0198    0.2111    0.9676    0.7044
      0.0301    0.1222    0.7160    0.0302    0.9395    0.6403
   
   
      0.8077
      0.8435
      0.6323
      0.7610
      0.6145
      0.8485
      0.7160
      0.6494
      0.9676
      0.9395
      0.5863
      0.7044
      0.6403
   

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

   
      0.9987    5.9295    9.8950    7.4687    0.7195    7.5104
      4.5336    5.8733    5.6834    4.1728    5.7176    9.0369
      3.0197    3.1213    0.2143    1.8686    5.6293    4.0144
      2.0292    4.0349    1.7728    2.3918    2.9566    2.2068
      9.4147    8.9169    3.3349    7.3533    8.8218    7.3622
   
   
      0.0000    5.9295    9.8950    7.4687    0.0000    7.5104
      0.0000    5.8733    5.6834    0.0000    5.7176    9.0369
      0.0000    0.0000    0.0000    0.0000    5.6293    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      9.4147    8.9169    0.0000    7.3533    8.8218    7.3622
   
   
      0.0000    5.9295       NaN    7.4687    0.0000    7.5104
      0.0000    5.8733    5.6834    0.0000    5.7176       NaN
      0.0000    0.0000    0.0000    0.0000    5.6293    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
         NaN    8.9169    0.0000    7.3533    8.8218    7.3622
   

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

   
      6.5000    6.5000    2.9262    6.5000    0.4528    0.7752
      0.2316    6.5000    9.9154    6.5000    2.2232    0.7751
      9.7042    9.7625    1.9182    9.2283    6.5000    4.8627
      3.1158    3.7291    3.7456    3.1645    6.5000    4.2881
      4.4706    3.2717    2.7704    6.5000    1.8206    8.4237
   
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
   
