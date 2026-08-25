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
      0.1578    0.2750    0.5708    0.3189
   
   R1[2] = 0.5708332234219833
   C1 = 
      0.4780
      0.4339
      0.0078
      0.7274
      0.3646
      0.0610
      0.7637
      0.0187
   
   C1[5] = 0.06096057993286974

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
      0.5734    0.6882    0.6142    0.1510    0.1577
      0.8291    0.9939    0.1867    0.5407    0.5087
   

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
   
      0.1338    0.8088    0.0949    0.3795    0.1595    0.2963    0.4888    0.5253
      0.1469    0.4006    0.5608    0.0331    0.4229    0.9749    0.6876    0.3325
      0.4846    0.4978    0.0267    0.6108    0.2254    0.9190    0.6975    0.2454
      0.9211    0.5315    0.1630    0.4113    0.1766    0.6443    0.7568    0.6931
      0.0150    0.8511    0.3186    0.2218    0.2489    0.8364    0.1570    0.3834
      0.9271    0.5516    0.4037    0.5534    0.9381    0.5943    0.8849    0.6036
      0.6442    0.6104    0.3978    0.7053    0.9795    0.6330    0.0025    0.7170
      0.0520    0.7451    0.8028    0.8686    0.5764    0.8553    0.3175    0.8226
   
   B = 
   
      0.6678    0.4110    0.2755    0.9361    0.5965    0.0719    0.5040    0.1815
      0.2841    0.4669    0.6567    0.9077    0.2564    0.1139    0.3639    0.3080
      0.0862    0.6273    0.3951    0.9959    0.9114    0.1358    0.8047    0.1636
      0.5103    0.2637    0.6256    0.7160    0.7334    0.3293    0.7615    0.2596
      0.8926    0.7354    0.8153    0.6560    0.7230    0.4262    0.3515    0.6870
      0.9734    0.5243    0.6534    0.9894    0.3789    0.1626    0.9712    0.4525
      0.6399    0.8718    0.4324    0.8568    0.3287    0.2633    0.1472    0.4221
      0.7408    0.8639    0.1064    0.0824    0.0561    0.0545    0.1616    0.6650
   
   C = 
   
      1.6537    1.7448    1.4338    2.0856    1.0698    0.5131    1.2278    1.1867
      2.2898    2.3168    1.8603    2.9419    1.6456    0.6811    1.9467    1.4934
      2.5029    2.0771    1.9651    3.0445    1.6433    0.7388    2.0262    1.4325
      2.7724    2.5636    1.8903    3.2603    1.7953    0.7013    2.0132    1.6574
      1.8132    1.7516    1.6858    2.4196    1.2502    0.5186    1.7274    1.2453
      3.5221    3.3320    2.7232    4.1780    2.6963    1.1287    2.5489    2.2361
      3.0210    2.6590    2.4662    3.3883    2.4096    0.9622    2.4792    1.9903
      2.9184    2.9617    2.6178    3.7107    2.4821    0.9968    2.8177    2.0598
   
   D = 
   
      1.6537    1.7448    1.4338    2.0856    1.0698    0.5131    1.2278    1.1867
      2.2898    2.3168    1.8603    2.9419    1.6456    0.6811    1.9467    1.4934
      2.5029    2.0771    1.9651    3.0445    1.6433    0.7388    2.0262    1.4325
      2.7724    2.5636    1.8903    3.2603    1.7953    0.7013    2.0132    1.6574
      1.8132    1.7516    1.6858    2.4196    1.2502    0.5186    1.7274    1.2453
      3.5221    3.3320    2.7232    4.1780    2.6963    1.1287    2.5489    2.2361
      3.0210    2.6590    2.4662    3.3883    2.4096    0.9622    2.4792    1.9903
      2.9184    2.9617    2.6178    3.7107    2.4821    0.9968    2.8177    2.0598
   


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

   
      0.4356    0.2893    0.7786    0.0979    0.8253    0.9601
      0.5398    0.1962    0.2369    0.0451    0.4004    0.4912
      0.0299    0.4109    0.3781    0.4652    0.6226    0.8821
      0.8783    0.6183    0.8070    0.3022    0.7418    0.3118
      0.7831    0.6419    0.0295    0.2273    0.1898    0.5455
   
   
      0.5398
      0.8783
      0.7831
      0.6183
      0.6419
      0.7786
      0.8070
      0.8253
      0.6226
      0.7418
      0.9601
      0.8821
      0.5455
   

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

   
      9.3626    8.2182    7.0572    0.4490    0.8099    1.5211
      5.1969    6.8245    1.0678    0.3503    0.8199    7.2801
      6.4301    3.8693    7.1631    4.4045    1.9940    4.8772
      3.1269    7.3413    6.9312    2.1679    9.6547    4.7713
      7.8993    7.6356    6.2717    1.9996    8.5704    7.8727
   
   
      9.3626    8.2182    7.0572    0.0000    0.0000    0.0000
      5.1969    6.8245    0.0000    0.0000    0.0000    7.2801
      6.4301    0.0000    7.1631    0.0000    0.0000    0.0000
      0.0000    7.3413    6.9312    0.0000    9.6547    0.0000
      7.8993    7.6356    6.2717    0.0000    8.5704    7.8727
   
   
         NaN    8.2182    7.0572    0.0000    0.0000    0.0000
      5.1969    6.8245    0.0000    0.0000    0.0000    7.2801
      6.4301    0.0000    7.1631    0.0000    0.0000    0.0000
      0.0000    7.3413    6.9312    0.0000       NaN    0.0000
      7.8993    7.6356    6.2717    0.0000    8.5704    7.8727
   

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

   
      4.3456    9.0606    2.3738    1.3424    4.0139    2.7939
      6.5000    0.5327    0.0155    0.6419    6.5000    0.8633
      4.5883    9.8025    4.6921    3.5721    0.9601    6.5000
      6.5000    6.5000    0.8853    6.5000    8.2342    6.5000
      8.1441    3.0104    0.2090    2.6219    3.8659    6.5000
   
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
   
