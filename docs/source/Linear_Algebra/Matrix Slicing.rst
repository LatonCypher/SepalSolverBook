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
      0.1060    0.2513    0.6087    0.6757
   
   R1[2] = 0.6087010548987789
   C1 = 
      0.6297
      0.9949
      0.7656
      0.4778
      0.2850
      0.6917
      0.2439
      0.7930
   
   C1[5] = 0.6917162884475766

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
      0.5217    0.7704    0.5628    0.6120    0.3659
      0.2439    0.7115    0.8774    0.8908    0.3279
   

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
   
      0.9100    0.7610    0.5911    0.2079    0.8794    0.9345    0.9852    0.3014
      0.2655    0.0949    0.2003    0.8239    0.2475    0.8958    0.6918    0.1218
      0.6948    0.2767    0.9712    0.0316    0.0023    0.4692    0.6893    0.8831
      0.9822    0.2833    0.7188    0.6414    0.7906    0.0461    0.9738    0.9668
      0.9696    0.5920    0.2068    0.7232    0.7108    0.4053    0.4624    0.0186
      0.9846    0.7033    0.3792    0.8810    0.6316    0.1771    0.3611    0.1637
      0.7203    0.7540    0.7001    0.7762    0.7545    0.3440    0.3936    0.0823
      0.0219    0.4332    0.0219    0.3594    0.9980    0.0243    0.6977    0.3788
   
   B = 
   
      0.9883    0.8739    0.9182    0.3322    0.6956    0.3923    0.5816    0.9284
      0.1782    0.8327    0.1793    0.1957    0.3858    0.7270    0.3345    0.8236
      0.7896    0.0466    0.3839    0.1183    0.3229    0.1541    0.3085    0.9017
      0.9109    0.9626    0.9217    0.9575    0.8306    0.6764    0.1260    0.2930
      0.4331    0.3019    0.1308    0.4289    0.4971    0.5215    0.0796    0.4361
      0.9912    0.8530    0.7753    0.0730    0.0792    0.5399    0.1761    0.8689
      0.0534    0.7784    0.5547    0.3253    0.6224    0.8310    0.3205    0.3656
      0.2188    0.7924    0.7154    0.4088    0.2010    0.2212    0.7194    0.3647
   
   C = 
   
      3.1168    3.7249    2.9921    1.6094    2.4750    2.9904    1.7594    3.7312
      2.2466    2.5873    2.2948    1.3658    1.6193    1.9758    0.8385    1.9303
      2.2277    2.5505    2.4677    1.0506    1.5746    1.6674    1.7393    2.7408
      2.8246    3.5473    3.1907    2.1354    2.7543    2.5961    2.0472    3.0748
      2.6241    2.9811    2.4194    1.6474    2.2472    2.3097    1.2064    2.6241
      2.7044    3.0642    2.5249    1.8213    2.3959    2.3131    1.3508    2.7143
      2.8127    2.9298    2.4231    1.7233    2.3264    2.3881    1.2909    2.9503
      1.0199    1.8921    1.2448    1.2505    1.4963    1.7671    0.7895    1.3518
   
   D = 
   
      3.1168    3.7249    2.9921    1.6094    2.4750    2.9904    1.7594    3.7312
      2.2466    2.5873    2.2948    1.3658    1.6193    1.9758    0.8385    1.9303
      2.2277    2.5505    2.4677    1.0506    1.5746    1.6674    1.7393    2.7408
      2.8246    3.5473    3.1907    2.1354    2.7543    2.5961    2.0472    3.0748
      2.6241    2.9811    2.4194    1.6474    2.2472    2.3097    1.2064    2.6241
      2.7044    3.0642    2.5249    1.8213    2.3959    2.3131    1.3508    2.7143
      2.8127    2.9298    2.4231    1.7233    2.3264    2.3881    1.2909    2.9503
      1.0199    1.8921    1.2448    1.2505    1.4963    1.7671    0.7895    1.3518
   


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

   
      0.8962    0.6893    0.9643    0.9597    0.5079    0.1388
      0.4265    0.7898    0.5166    0.9989    0.4785    0.6378
      0.9435    0.9928    0.1783    0.8665    0.8152    0.6638
      0.2949    0.8141    0.4231    0.9479    0.9107    0.2928
      0.4379    0.9774    0.2331    0.3004    0.3399    0.5649
   
   
      0.8962
      0.9435
      0.6893
      0.7898
      0.9928
      0.8141
      0.9774
      0.9643
      0.5166
      0.9597
      0.9989
      0.8665
      0.9479
      0.5079
      0.8152
      0.9107
      0.6378
      0.6638
      0.5649
   

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

   
      4.9267    3.8796    9.7745    3.2079    2.5303    4.5163
      3.4353    8.8409    1.7673    8.2346    4.2671    2.9057
      4.6173    4.3824    0.5791    0.8029    2.9056    0.8952
      8.4838    9.1582    4.9505    8.0087    7.6633    6.4878
      8.8166    1.2752    2.9392    6.9264    8.8491    8.7010
   
   
      0.0000    0.0000    9.7745    0.0000    0.0000    0.0000
      0.0000    8.8409    0.0000    8.2346    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.4838    9.1582    0.0000    8.0087    7.6633    6.4878
      8.8166    0.0000    0.0000    6.9264    8.8491    8.7010
   
   
      0.0000    0.0000       NaN    0.0000    0.0000    0.0000
      0.0000    8.8409    0.0000    8.2346    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      8.4838       NaN    0.0000    8.0087    7.6633    6.4878
      8.8166    0.0000    0.0000    6.9264    8.8491    8.7010
   

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

   
      9.4427    6.5000    6.5000    4.6272    4.4309    1.2212
      6.5000    9.7214    2.1585    9.9106    8.2453    1.7754
      6.5000    1.8922    6.5000    6.5000    6.5000    9.3255
      1.6754    4.2323    0.1796    6.5000    4.2119    9.6567
      4.3736    6.5000    1.0940    1.6221    2.9746    4.0411
   
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
   
