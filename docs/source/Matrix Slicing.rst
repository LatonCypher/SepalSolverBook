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
      0.0012    0.3187    0.1483    0.5385
   
   R1[2] = 0.14828721066841755
   C1 = 
      0.1548
      0.7960
      0.2814
      0.4242
      0.8452
      0.0153
      0.1756
      0.6302
   
   C1[5] = 0.015338329225760394

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
      0.1655    0.4948    0.5975    0.7637    0.0186
      0.1562    0.8805    0.5501    0.1654    0.7836
   

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
   
      0.2241    0.3575    0.1265    0.7326    0.3543    0.6947    0.5992    0.4091
      0.3413    0.6506    0.4002    0.1162    0.8110    0.0113    0.0575    0.7112
      0.0594    0.5879    0.0792    0.1642    0.1615    0.7204    0.7027    0.9380
      0.2848    0.5543    0.7368    0.2225    0.6513    0.4264    0.6374    0.9522
      0.9165    0.0577    0.7136    0.2021    0.3894    0.1953    0.2227    0.7911
      0.9803    0.6088    0.4550    0.8131    0.8465    0.4952    0.1999    0.7857
      0.2524    0.9080    0.1474    0.8264    0.7787    0.2896    0.5286    0.6955
      0.0324    0.7577    0.1292    0.5476    0.7359    0.9504    0.3068    0.5915
   
   B = 
   
      0.1908    0.3420    0.4990    0.2877    0.1560    0.1665    0.9208    0.9501
      0.1573    0.5131    0.9855    0.0860    0.7660    0.6974    0.9326    0.2797
      0.9960    0.5584    0.3628    0.0938    0.2091    0.0968    0.1234    0.0543
      0.8299    0.5309    0.0174    0.3741    0.3606    0.8522    0.5740    0.9417
      0.1159    0.0825    0.8598    0.3849    0.8930    0.9989    0.3533    0.9923
      0.5908    0.2778    0.0870    0.8601    0.3124    0.8392    0.7888    0.9331
      0.3613    0.8191    0.9279    0.3340    0.1380    0.4435    0.2362    0.4763
      0.0815    0.6633    0.2530    0.9188    0.4964    0.0199    0.9383    0.9095
   
   C = 
   
      1.5343    1.7040    1.5474    1.6910    1.4186    2.1340    2.1744    2.6669
      0.8419    1.3246    1.8903    1.2297    1.7659    1.5076    2.0135    2.1270
      1.0935    1.8645    1.7315    1.9148    1.4672    1.6636    2.3784    2.4000
      1.6954    2.2373    2.3891    1.9872    1.9788    2.0050    2.6084    2.8889
      1.3679    1.6423    1.5353    1.5303    1.2414    1.1016    2.1884    2.5102
      1.9378    2.2256    2.4235    2.2215    2.3358    2.6902    3.4674    4.0038
      1.5327    2.1123    2.4499    1.8381    2.2678    2.6630    2.8529    3.2072
      1.5142    1.7310    1.9690    2.0380    2.1000    2.6934    2.7039    3.0665
   
   D = 
   
      1.5343    1.7040    1.5474    1.6910    1.4186    2.1340    2.1744    2.6669
      0.8419    1.3246    1.8903    1.2297    1.7659    1.5076    2.0135    2.1270
      1.0935    1.8645    1.7315    1.9148    1.4672    1.6636    2.3784    2.4000
      1.6954    2.2373    2.3891    1.9872    1.9788    2.0050    2.6084    2.8889
      1.3679    1.6423    1.5353    1.5303    1.2414    1.1016    2.1884    2.5102
      1.9378    2.2256    2.4235    2.2215    2.3358    2.6902    3.4674    4.0038
      1.5327    2.1123    2.4499    1.8381    2.2678    2.6630    2.8529    3.2072
      1.5142    1.7310    1.9690    2.0380    2.1000    2.6934    2.7039    3.0665
   


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

   
      0.4917    0.9425    0.4669    0.8814    0.7562    0.3925
      0.1099    0.0645    0.7920    0.5552    0.2452    0.1677
      0.4166    0.8273    0.8290    0.0873    0.2623    0.0931
      0.4488    0.6475    0.9163    0.1570    0.5695    0.3563
      0.5321    0.1233    0.4126    0.9561    0.4809    0.6710
   
   
      0.5321
      0.9425
      0.8273
      0.6475
      0.7920
      0.8290
      0.9163
      0.8814
      0.5552
      0.9561
      0.7562
      0.5695
      0.6710
   

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

   
      5.7126    7.5393    1.7705    6.6971    4.2479    6.7710
      9.5945    4.8345    2.9393    1.6132    4.8056    5.2159
      9.9076    8.4693    8.6503    4.7254    6.9780    3.0520
      0.5676    7.2247    6.7345    3.1767    0.9855    8.8494
      8.9965    5.4324    1.6268    6.4680    7.2276    3.3535
   
   
      5.7126    7.5393    0.0000    6.6971    0.0000    6.7710
      9.5945    0.0000    0.0000    0.0000    0.0000    5.2159
      9.9076    8.4693    8.6503    0.0000    6.9780    0.0000
      0.0000    7.2247    6.7345    0.0000    0.0000    8.8494
      8.9965    5.4324    0.0000    6.4680    7.2276    0.0000
   
   
      5.7126    7.5393    0.0000    6.6971    0.0000    6.7710
         NaN    0.0000    0.0000    0.0000    0.0000    5.2159
         NaN    8.4693    8.6503    0.0000    6.9780    0.0000
      0.0000    7.2247    6.7345    0.0000    0.0000    8.8494
      8.9965    5.4324    0.0000    6.4680    7.2276    0.0000
   

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

   
      9.3705    6.5000    4.2534    0.2535    6.5000    2.6172
      0.3209    6.5000    9.0972    4.8906    6.5000    0.8712
      8.3377    6.5000    9.7220    6.5000    6.5000    2.5640
      4.2244    6.5000    9.0430    0.8872    6.5000    1.2933
      0.4622    1.4702    3.2684    4.9347    3.9279    0.3611
   
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
   
