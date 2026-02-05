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
      0.3958    0.8750    0.3750    0.9176
   
   R1[2] = 0.3749629222945915
   C1 = 
      0.7518
      0.7596
      0.2398
      0.2428
      0.1628
      0.5001
      0.2472
      0.9652
   
   C1[5] = 0.5000790179256513

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
      0.1706    0.9132    0.3971    0.5261    0.5664
      0.5231    0.4847    0.5137    0.7627    0.3979
   

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
   
      0.1711    0.7527    0.2406    0.7689    0.0421    0.2336    0.2836    0.1479
      0.5835    0.4921    0.1272    0.0469    0.3140    0.4384    0.1788    0.5644
      0.1704    0.6739    0.2654    0.1555    0.5867    0.8397    0.9451    0.4938
      0.7810    0.8315    0.8378    0.6753    0.2554    0.4838    0.6740    0.7190
      0.2783    0.5785    0.1289    0.2421    0.7841    0.7905    0.2173    0.2361
      0.5452    0.8197    0.6711    0.2675    0.4804    0.5426    0.8729    0.0411
      0.6880    0.6531    0.6389    0.2170    0.2981    0.5599    0.8770    0.8359
      0.6838    0.1976    0.1291    0.0310    0.9889    0.1625    0.5322    0.5649
   
   B = 
   
      0.0996    0.3568    0.7534    0.4169    0.2486    0.7810    0.5613    0.5580
      0.3378    0.1229    0.3813    0.3061    0.5835    0.5875    0.7977    0.3585
      0.0391    0.6283    0.1807    0.5886    0.2163    0.6053    0.1701    0.1379
      0.4007    0.9837    0.0206    0.5874    0.9815    0.9923    0.4434    0.6561
      0.2184    0.3309    0.5565    0.3174    0.9452    0.5941    0.8859    0.0519
      0.7665    0.2139    0.4904    0.3756    0.0325    0.2455    0.9676    0.2973
      0.7227    0.9001    0.5674    0.3728    0.8774    0.6743    0.2235    0.5920
      0.4648    0.0047    0.2091    0.7902    0.0641    0.1481    0.6512    0.7951
   
   C = 
   
      1.0509    1.3809    0.8051    1.2187    1.5941    1.7800    1.5014    1.2601
      1.0443    0.7560    1.2604    1.2734    1.0099    1.3668    1.8724    1.2516
      2.0017    1.6901    1.8143    1.7691    2.0883    2.1092    2.6124    1.7076
      1.9100    2.3695    1.9829    2.5522    2.4178    3.1072    2.8567    2.4203
      1.3692    1.1147    1.4552    1.3245    1.6448    1.7169    2.4087    1.1314
      1.6355    2.0409    1.8874    1.7445    2.2617    2.5924    2.3651    1.6018
      1.9177    1.9523    2.0001    2.2827    2.0262    2.5528    2.6581    2.2143
      1.1401    1.2236    1.6646    1.4595    1.7868    1.8290    2.0973    1.3544
   
   D = 
   
      1.0509    1.3809    0.8051    1.2187    1.5941    1.7800    1.5014    1.2601
      1.0443    0.7560    1.2604    1.2734    1.0099    1.3668    1.8724    1.2516
      2.0017    1.6901    1.8143    1.7691    2.0883    2.1092    2.6124    1.7076
      1.9100    2.3695    1.9829    2.5522    2.4178    3.1072    2.8567    2.4203
      1.3692    1.1147    1.4552    1.3245    1.6448    1.7169    2.4087    1.1314
      1.6355    2.0409    1.8874    1.7445    2.2617    2.5924    2.3651    1.6018
      1.9177    1.9523    2.0001    2.2827    2.0262    2.5528    2.6581    2.2143
      1.1401    1.2236    1.6646    1.4595    1.7868    1.8290    2.0973    1.3544
   


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

   
      0.0658    0.8355    0.2921    0.2628    0.4531    0.5381
      0.7405    0.2204    0.5979    0.8584    0.4382    0.7507
      0.5868    0.8909    0.9197    0.5441    0.0592    0.1050
      0.0709    0.6705    0.8872    0.1494    0.0649    0.5239
      0.2934    0.6230    0.7278    0.3465    0.0504    0.8811
   
   
      0.7405
      0.5868
      0.8355
      0.8909
      0.6705
      0.6230
      0.5979
      0.9197
      0.8872
      0.7278
      0.8584
      0.5441
      0.5381
      0.7507
      0.5239
      0.8811
   

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

   
      7.5332    3.9435    4.1875    0.5741    7.5252    6.4042
      8.9375    5.9963    0.2485    8.9942    1.3757    1.8606
      7.9915    2.9234    4.3394    8.2988    9.0205    2.6674
      6.2346    1.2022    4.2455    7.9642    5.2299    3.6464
      0.2992    4.2200    3.7362    1.6057    0.6935    4.6339
   
   
      7.5332    0.0000    0.0000    0.0000    7.5252    6.4042
      8.9375    5.9963    0.0000    8.9942    0.0000    0.0000
      7.9915    0.0000    0.0000    8.2988    9.0205    0.0000
      6.2346    0.0000    0.0000    7.9642    5.2299    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      7.5332    0.0000    0.0000    0.0000    7.5252    6.4042
      8.9375    5.9963    0.0000    8.9942    0.0000    0.0000
      7.9915    0.0000    0.0000    8.2988       NaN    0.0000
      6.2346    0.0000    0.0000    7.9642    5.2299    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      0.0207    0.3233    4.7467    0.2719    8.8280    6.5000
      2.4053    4.0283    2.5410    0.7534    4.2944    6.5000
      6.5000    6.5000    6.5000    3.2289    6.5000    8.7060
      0.8308    4.4922    1.0554    6.5000    0.5125    6.5000
      2.6028    8.4631    4.1076    1.6452    6.5000    8.5120
   
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
   
