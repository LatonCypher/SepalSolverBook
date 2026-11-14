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
      0.5994    0.5689    0.6641    0.1784
   
   R1[2] = 0.664092573721375
   C1 = 
      0.6626
      0.0395
      0.0866
      0.9799
      0.3106
      0.3926
      0.4026
      0.7581
   
   C1[5] = 0.392565969483726

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
      0.6795    0.6698    0.3332    0.4102    0.3825
      0.2458    0.6342    0.8911    0.0231    0.5479
   

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
   
      0.9586    0.4307    0.5159    0.9261    0.4465    0.8575    0.2559    0.5241
      0.0737    0.2215    0.4054    0.2351    0.4668    0.1460    0.0037    0.9477
      0.6651    0.9442    0.9226    0.8097    0.2585    0.8802    0.6863    0.6993
      0.5160    0.4385    0.0908    0.9872    0.5880    0.7403    0.9134    0.6159
      0.6258    0.2135    0.8236    0.4923    0.9943    0.9174    0.0521    0.7461
      0.2123    0.7553    0.8368    0.2741    0.5439    0.5082    0.0959    0.8094
      0.1793    0.3817    0.0599    0.1385    0.1688    0.5840    0.7519    0.0869
      0.9883    0.2006    0.2907    0.3563    0.5584    0.8216    0.2973    0.5132
   
   B = 
   
      0.1917    0.4766    0.2142    0.9215    0.1054    0.7799    0.4000    0.1603
      0.6307    0.2805    0.1749    0.9348    0.8840    0.3436    0.8371    0.6605
      0.6812    0.9345    0.2356    0.7261    0.9503    0.4314    0.7338    0.2435
      0.6438    0.5034    0.0277    0.6875    0.7835    0.8421    0.5927    0.6528
      0.3814    0.5771    0.0173    0.3321    0.8692    0.7204    0.1765    0.6282
      0.0972    0.1408    0.4901    0.8988    0.9204    0.4664    0.1142    0.9395
      0.8772    0.6732    0.2512    0.1953    0.9762    0.7125    0.4030    0.9860
      0.5460    0.1239    0.3767    0.4567    0.9257    0.8435    0.1862    0.7764
   
   C = 
   
      2.1673    2.1417    1.1177    3.5055    3.6100    3.2440    2.0490    2.9136
      1.2943    1.0043    0.5942    1.4507    2.1941    1.7128    0.9289    1.5802
      3.0407    2.6733    1.4191    4.0523    4.7680    3.5985    2.7663    3.6923
      2.5067    2.0856    1.0706    2.9504    3.9562    3.3628    1.8964    3.4826
      2.0540    2.2062    1.1401    3.2183    3.8733    3.1419    1.7654    2.8803
      2.0465    1.7833    0.9698    2.7234    3.4835    2.3968    1.8372    2.4577
      1.2331    1.0147    0.6339    1.4281    2.0204    1.4164    0.9329    1.8493
      1.5772    1.6800    1.0056    2.7705    2.8438    2.6953    1.3955    2.4085
   
   D = 
   
      2.1673    2.1417    1.1177    3.5055    3.6100    3.2440    2.0490    2.9136
      1.2943    1.0043    0.5942    1.4507    2.1941    1.7128    0.9289    1.5802
      3.0407    2.6733    1.4191    4.0523    4.7680    3.5985    2.7663    3.6923
      2.5067    2.0856    1.0706    2.9504    3.9562    3.3628    1.8964    3.4826
      2.0540    2.2062    1.1401    3.2183    3.8733    3.1419    1.7654    2.8803
      2.0465    1.7833    0.9698    2.7234    3.4835    2.3968    1.8372    2.4577
      1.2331    1.0147    0.6339    1.4281    2.0204    1.4164    0.9329    1.8493
      1.5772    1.6800    1.0056    2.7705    2.8438    2.6953    1.3955    2.4085
   


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

   
      0.6848    0.2329    0.3581    0.9868    0.0808    0.7965
      0.6045    0.8957    0.9664    0.7634    0.5545    0.5496
      0.3040    0.8581    0.1943    0.9644    0.3937    0.0384
      0.9145    0.6186    0.8519    0.1239    0.1403    0.0002
      0.3910    0.6638    0.8823    0.0297    0.0455    0.0495
   
   
      0.6848
      0.6045
      0.9145
      0.8957
      0.8581
      0.6186
      0.6638
      0.9664
      0.8519
      0.8823
      0.9868
      0.7634
      0.9644
      0.5545
      0.7965
      0.5496
   

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

   
      0.4720    3.8994    7.7355    9.2210    8.9404    3.9843
      8.3599    2.3663    0.1592    6.2724    0.6242    3.4324
      4.6660    7.7022    1.3651    4.5772    6.2378    9.4168
      1.0693    5.9601    4.8546    8.0188    8.6368    1.0458
      1.2196    9.1632    4.0891    5.2324    9.9951    6.4793
   
   
      0.0000    0.0000    7.7355    9.2210    8.9404    0.0000
      8.3599    0.0000    0.0000    6.2724    0.0000    0.0000
      0.0000    7.7022    0.0000    0.0000    6.2378    9.4168
      0.0000    5.9601    0.0000    8.0188    8.6368    0.0000
      0.0000    9.1632    0.0000    5.2324    9.9951    6.4793
   
   
      0.0000    0.0000    7.7355       NaN    8.9404    0.0000
      8.3599    0.0000    0.0000    6.2724    0.0000    0.0000
      0.0000    7.7022    0.0000    0.0000    6.2378       NaN
      0.0000    5.9601    0.0000    8.0188    8.6368    0.0000
      0.0000       NaN    0.0000    5.2324       NaN    6.4793
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      6.5000    9.2082    6.5000    6.5000    3.9632    4.4047
      1.6516    0.3810    0.1213    3.3683    9.2567    4.6112
      8.1243    1.4413    6.5000    9.7140    6.5000    3.2517
      8.2697    9.4907    3.2639    4.3654    4.8051    2.3785
      1.9637    9.9182    9.4875    4.5241    6.5000    6.5000
   
Advantages
^^^^^^^^^^


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
   
