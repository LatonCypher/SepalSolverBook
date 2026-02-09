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
      0.0958    0.9521    0.7005    0.3783
   
   R1[2] = 0.7004673721757262
   C1 = 
      0.2841
      0.7631
      0.6691
      0.5026
      0.3811
      0.6899
      0.0084
      0.1723
   
   C1[5] = 0.6899233246038575

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
      0.2516    0.3171    0.7506    0.5819    0.3570
      0.7622    0.5776    0.5340    0.4383    0.9095
   

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
   
      0.9142    0.0213    0.2059    0.0683    0.7353    0.3599    0.4235    0.3338
      0.4564    0.6499    0.6843    0.1055    0.2267    0.7834    0.9147    0.7199
      0.2703    0.9729    0.4733    0.6126    0.6095    0.2982    0.7004    0.4109
      0.4736    0.8568    0.2113    0.2224    0.2780    0.1125    0.0597    0.7382
      0.7660    0.2912    0.0288    0.7661    0.6449    0.2451    0.5706    0.7948
      0.4283    0.9701    0.6465    0.3552    0.3907    0.7012    0.9303    0.8688
      0.0294    0.6300    0.7410    0.3250    0.1869    0.3715    0.5608    0.5711
      0.9413    0.3836    0.1539    0.2981    0.2013    0.4026    0.2996    0.0677
   
   B = 
   
      0.9146    0.3296    0.4211    0.2347    0.9341    0.4225    0.2860    0.3002
      0.6125    0.8721    0.4696    0.8854    0.0418    0.8068    0.1583    0.1117
      0.3637    0.7729    0.5071    0.7320    0.8408    0.7938    0.1732    0.0485
      0.1875    0.5289    0.7780    0.1658    0.8035    0.9613    0.2034    0.7833
      0.1517    0.5997    0.9396    0.3367    0.0122    0.3906    0.3509    0.3064
      0.6517    0.9464    0.3772    0.9874    0.0759    0.3040    0.1958    0.2471
      0.6251    0.2369    0.8216    0.4755    0.4449    0.8713    0.2102    0.7336
      0.6435    0.2496    0.5992    0.7089    0.3539    0.1545    0.7118    0.2185
   
   C = 
   
      1.7625    1.4803    1.9271    1.4365    1.4258    1.4497    0.9695    1.0381
      2.6641    2.5757    2.6178    2.9961    1.8376    2.5967    1.3110    1.4168
      2.1191    2.5435    2.7940    2.4968    1.6704    2.8661    1.1498    1.5566
      1.7043    1.6557    1.6768    1.8176    1.1343    1.5817    1.0104    0.7403
      2.1587    1.8860    2.7132    1.8796    1.9290    2.2642    1.3859    1.7143
      2.9445    3.0100    3.1566    3.3739    2.0491    3.1289    1.5486    1.7118
      1.7318    2.0429    2.0556    2.2624    1.4201    2.1842    0.9653    1.0550
      1.7315    1.5110    1.5141    1.3784    1.4544    1.5884    0.6778    0.9621
   
   D = 
   
      1.7625    1.4803    1.9271    1.4365    1.4258    1.4497    0.9695    1.0381
      2.6641    2.5757    2.6178    2.9961    1.8376    2.5967    1.3110    1.4168
      2.1191    2.5435    2.7940    2.4968    1.6704    2.8661    1.1498    1.5566
      1.7043    1.6557    1.6768    1.8176    1.1343    1.5817    1.0104    0.7403
      2.1587    1.8860    2.7132    1.8796    1.9290    2.2642    1.3859    1.7143
      2.9445    3.0100    3.1566    3.3739    2.0491    3.1289    1.5486    1.7118
      1.7318    2.0429    2.0556    2.2624    1.4201    2.1842    0.9653    1.0550
      1.7315    1.5110    1.5141    1.3784    1.4544    1.5884    0.6778    0.9621
   


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

   
      0.0421    0.9233    0.5476    0.7376    0.9279    0.7329
      0.6967    0.2907    0.8913    0.1687    0.2162    0.0496
      0.3962    0.6001    0.8618    0.0578    0.9254    0.4503
      0.5250    0.6234    0.1703    0.8590    0.4336    0.1379
      0.1641    0.3309    0.1660    0.4369    0.4778    0.2882
   
   
      0.6967
      0.5250
      0.9233
      0.6001
      0.6234
      0.5476
      0.8913
      0.8618
      0.7376
      0.8590
      0.9279
      0.9254
      0.7329
   

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

   
      7.2692    8.1262    4.3699    8.3578    5.9160    8.6791
      1.8348    8.5187    5.0917    4.1434    0.5738    1.4365
      5.1965    0.8780    2.4818    7.6426    3.9512    1.8593
      1.5304    7.0105    2.4409    7.5809    3.9606    3.2653
      4.4181    5.3903    4.6697    9.6024    0.8535    2.1813
   
   
      7.2692    8.1262    0.0000    8.3578    5.9160    8.6791
      0.0000    8.5187    5.0917    0.0000    0.0000    0.0000
      5.1965    0.0000    0.0000    7.6426    0.0000    0.0000
      0.0000    7.0105    0.0000    7.5809    0.0000    0.0000
      0.0000    5.3903    0.0000    9.6024    0.0000    0.0000
   
   
      7.2692    8.1262    0.0000    8.3578    5.9160    8.6791
      0.0000    8.5187    5.0917    0.0000    0.0000    0.0000
      5.1965    0.0000    0.0000    7.6426    0.0000    0.0000
      0.0000    7.0105    0.0000    7.5809    0.0000    0.0000
      0.0000    5.3903    0.0000       NaN    0.0000    0.0000
   

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

   
      2.5494    6.5000    6.5000    1.2691    8.1389    0.4740
      1.7634    1.5699    0.3075    0.3842    2.7344    3.3467
      2.8650    6.5000    0.3180    6.5000    4.9438    6.5000
      4.1419    6.5000    2.6076    6.5000    6.5000    9.5094
      9.0657    6.5000    6.5000    9.9622    1.9182    3.2227
   
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
   
