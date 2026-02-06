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
      0.4856    0.8768    0.8538    0.6059
   
   R1[2] = 0.8537831834453171
   C1 = 
      0.0278
      0.5693
      0.8673
      0.3995
      0.6178
      0.3662
      0.3848
      0.6576
   
   C1[5] = 0.366156414643132

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
      0.5342    0.7362    0.4668    0.8945    0.4631
      0.4489    0.2475    0.1765    0.2736    0.9518
   

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
   
      0.7892    0.3485    0.3442    0.4939    0.4008    0.0795    0.8127    0.4064
      0.7624    0.4204    0.4322    0.0664    0.2267    0.5010    0.1037    0.4572
      0.4894    0.8760    0.3105    0.4877    0.1968    0.3942    0.1895    0.5797
      0.6036    0.3337    0.7708    0.5282    0.8985    0.4504    0.7234    0.6013
      0.0015    0.7856    0.0320    0.0743    0.4073    0.5681    0.0646    0.4459
      0.8504    0.8466    0.8285    0.5380    0.4760    0.6731    0.3395    0.9351
      0.9041    0.4445    0.0717    0.0623    0.0196    0.5775    0.8064    0.0746
      0.2052    0.4922    0.3987    0.1268    0.4159    0.7302    0.6186    0.3443
   
   B = 
   
      0.6279    0.6245    0.8212    0.7197    0.1316    0.0220    0.8012    0.6319
      0.1189    0.0288    0.7919    0.5548    0.2225    0.6502    0.0714    0.2121
      0.7513    0.7201    0.2994    0.4743    0.3937    0.7430    0.9159    0.8933
      0.0781    0.7495    0.0631    0.1483    0.7917    0.1293    0.2255    0.7831
      0.4916    0.8518    0.9495    0.8672    0.3755    0.8904    0.4251    0.5423
      0.6686    0.5971    0.2281    0.5014    0.0088    0.8372    0.8550    0.7691
      0.6792    0.7556    0.7785    0.8236    0.6210    0.6353    0.3805    0.3885
      0.9992    0.1797    0.6558    0.3475    0.6012    0.0411    0.9827    0.3413
   
   C = 
   
      2.0424    2.1969    2.3563    2.1959    1.6082    1.5201    2.0308    1.9998
      1.8322    1.5019    1.8026    1.6888    0.8455    1.3258    2.0651    1.7136
      1.7510    1.5703    2.0239    1.7837    1.3112    1.5235    1.9115    1.8357
      2.8740    3.0264    2.9374    2.8733    2.0275    2.5327    2.9657    2.8742
      1.1937    0.9174    1.4967    1.3094    0.7125    1.4419    1.2250    1.0894
      3.1481    2.7871    3.1339    2.9094    2.0101    2.4958    3.4479    3.1052
      1.6972    1.6598    1.9468    1.9371    0.8534    1.3865    1.7180    1.5719
      1.9536    1.8439    1.9545    1.9846    1.1476    2.0259    1.9681    1.8344
   
   D = 
   
      2.0424    2.1969    2.3563    2.1959    1.6082    1.5201    2.0308    1.9998
      1.8322    1.5019    1.8026    1.6888    0.8455    1.3258    2.0651    1.7136
      1.7510    1.5703    2.0239    1.7837    1.3112    1.5235    1.9115    1.8357
      2.8740    3.0264    2.9374    2.8733    2.0275    2.5327    2.9657    2.8742
      1.1937    0.9174    1.4967    1.3094    0.7125    1.4419    1.2250    1.0894
      3.1481    2.7871    3.1339    2.9094    2.0101    2.4958    3.4479    3.1052
      1.6972    1.6598    1.9468    1.9371    0.8534    1.3865    1.7180    1.5719
      1.9536    1.8439    1.9545    1.9846    1.1476    2.0259    1.9681    1.8344
   


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

   
      0.0835    0.6124    0.2798    0.1365    0.3233    0.7548
      0.1590    0.5191    0.0417    0.7902    0.6706    0.0329
      0.8174    0.0691    0.1532    0.6722    0.2038    0.6784
      0.8975    0.0118    0.6511    0.2261    0.1103    0.5487
      0.6981    0.8035    0.3449    0.8822    0.3947    0.2403
   
   
      0.8174
      0.8975
      0.6981
      0.6124
      0.5191
      0.8035
      0.6511
      0.7902
      0.6722
      0.8822
      0.6706
      0.7548
      0.6784
      0.5487
   

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

   
      3.1028    9.8627    9.4422    8.4497    9.7396    6.2654
      8.7435    6.6663    8.7718    7.4920    4.4233    9.9910
      0.7255    4.7076    6.2719    8.1094    4.4010    6.8899
      7.0178    1.3951    8.8788    5.3426    2.7623    8.8721
      7.9824    6.6366    1.0009    7.8453    9.1245    4.3025
   
   
      0.0000    9.8627    9.4422    8.4497    9.7396    6.2654
      8.7435    6.6663    8.7718    7.4920    0.0000    9.9910
      0.0000    0.0000    6.2719    8.1094    0.0000    6.8899
      7.0178    0.0000    8.8788    5.3426    0.0000    8.8721
      7.9824    6.6366    0.0000    7.8453    9.1245    0.0000
   
   
      0.0000       NaN       NaN    8.4497       NaN    6.2654
      8.7435    6.6663    8.7718    7.4920    0.0000       NaN
      0.0000    0.0000    6.2719    8.1094    0.0000    6.8899
      7.0178    0.0000    8.8788    5.3426    0.0000    8.8721
      7.9824    6.6366    0.0000    7.8453       NaN    0.0000
   

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

   
      0.0700    4.9217    9.9197    6.5000    6.5000    9.1625
      1.7033    6.5000    2.9526    0.6948    1.3258    6.5000
      1.5394    8.5847    3.8455    6.5000    4.7351    6.5000
      6.5000    4.6742    4.2491    6.5000    6.5000    4.7521
      1.0599    1.3382    6.5000    4.8023    3.4075    2.1424
   
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
   
