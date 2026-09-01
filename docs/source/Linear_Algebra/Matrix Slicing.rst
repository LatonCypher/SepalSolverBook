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
      0.5093    0.7140    0.7359    0.1201
   
   R1[2] = 0.7358885745073721
   C1 = 
      0.0213
      0.3944
      0.9743
      0.9049
      0.9415
      0.0864
      0.1061
      0.8494
   
   C1[5] = 0.08644035840857012

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
      0.4146    0.7281    0.4364    0.3837    0.2449
      0.4880    0.6550    0.7839    0.4235    0.6799
   

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
   
      0.9904    0.2755    0.6562    0.6915    0.3136    0.7594    0.9875    0.1625
      0.5489    0.5623    0.5506    0.1978    0.0952    0.3414    0.4802    0.9507
      0.6426    0.4433    0.6269    0.7481    0.4538    0.4660    0.1832    0.8506
      0.4072    0.4189    0.9437    0.4443    0.6848    0.5147    0.1069    0.9493
      0.8204    0.5217    0.8185    0.0615    0.5459    0.7673    0.3891    0.1785
      0.9998    0.4289    0.1238    0.0468    0.4298    0.1411    0.2008    0.3699
      0.6511    0.7381    0.5876    0.7970    0.1182    0.7043    0.6715    0.8401
      0.8244    0.0091    0.3545    0.1621    0.0498    0.0467    0.0413    0.5633
   
   B = 
   
      0.8966    0.3694    0.0400    0.1138    0.8110    0.8140    0.7547    0.4584
      0.3399    0.8419    0.0329    0.0914    0.1277    0.7181    0.3721    0.9221
      0.9256    0.0071    0.5808    0.6964    0.0269    0.0178    0.6934    0.4766
      0.6813    0.3957    0.1116    0.9461    0.2637    0.0306    0.2314    0.9043
      0.1705    0.5136    0.8273    0.6665    0.4172    0.8515    0.5915    0.5899
      0.8150    0.5191    0.2376    0.6141    0.5374    0.5014    0.5056    0.8221
      0.0715    0.9942    0.8435    0.2488    0.7215    0.1883    0.5534    0.5251
      0.4337    0.5939    0.1581    0.9147    0.8927    0.4183    0.0579    0.6827
   
   C = 
   
      2.8737    2.5097    1.8056    2.3188    2.4349    1.9385    2.5904    3.0849
      2.0688    2.0264    1.0975    1.9465    2.0022    1.6068    1.6007    2.4494
      2.6560    2.0734    1.2630    2.6702    2.1232    1.8858    1.9124    3.0064
      2.6393    1.9746    1.5569    2.8296    2.0132    1.9210    1.9997    2.9558
      2.5361    1.9440    1.5226    1.8644    1.8504    2.0563    2.3315    2.5819
      1.5518    1.4631    0.7482    1.0450    1.6115    1.7547    1.4689    1.6827
      2.9280    2.7743    1.4449    2.7515    2.5104    2.0263    2.2041    3.5547
      1.4747    0.8043    0.4335    1.0823    1.3006    0.9980    1.0175    1.1759
   
   D = 
   
      2.8737    2.5097    1.8056    2.3188    2.4349    1.9385    2.5904    3.0849
      2.0688    2.0264    1.0975    1.9465    2.0022    1.6068    1.6007    2.4494
      2.6560    2.0734    1.2630    2.6702    2.1232    1.8858    1.9124    3.0064
      2.6393    1.9746    1.5569    2.8296    2.0132    1.9210    1.9997    2.9558
      2.5361    1.9440    1.5226    1.8644    1.8504    2.0563    2.3315    2.5819
      1.5518    1.4631    0.7482    1.0450    1.6115    1.7547    1.4689    1.6827
      2.9280    2.7743    1.4449    2.7515    2.5104    2.0263    2.2041    3.5547
      1.4747    0.8043    0.4335    1.0823    1.3006    0.9980    1.0175    1.1759
   


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

   
      0.4649    0.1409    0.9126    0.2760    0.7549    0.2166
      0.7038    0.5843    0.8852    0.4419    0.6593    0.6209
      0.7985    0.8355    0.8563    0.9018    0.4772    0.0529
      0.5706    0.9113    0.7395    0.8013    0.3227    0.0861
      0.8320    0.8495    0.1329    0.3792    0.2740    0.9169
   
   
      0.7038
      0.7985
      0.5706
      0.8320
      0.5843
      0.8355
      0.9113
      0.8495
      0.9126
      0.8852
      0.8563
      0.7395
      0.9018
      0.8013
      0.7549
      0.6593
      0.6209
      0.9169
   

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

   
      4.1493    7.6723    0.1491    2.1773    8.2572    5.7825
      8.5015    2.7098    5.8379    5.4277    1.3583    7.2843
      9.2340    2.0258    2.3324    2.6854    0.6203    7.9020
      6.7943    5.5029    2.6375    0.1732    4.5828    5.7729
      3.0649    3.0711    3.4516    6.3589    7.7172    9.5993
   
   
      0.0000    7.6723    0.0000    0.0000    8.2572    5.7825
      8.5015    0.0000    5.8379    5.4277    0.0000    7.2843
      9.2340    0.0000    0.0000    0.0000    0.0000    7.9020
      6.7943    5.5029    0.0000    0.0000    0.0000    5.7729
      0.0000    0.0000    0.0000    6.3589    7.7172    9.5993
   
   
      0.0000    7.6723    0.0000    0.0000    8.2572    5.7825
      8.5015    0.0000    5.8379    5.4277    0.0000    7.2843
         NaN    0.0000    0.0000    0.0000    0.0000    7.9020
      6.7943    5.5029    0.0000    0.0000    0.0000    5.7729
      0.0000    0.0000    0.0000    6.3589    7.7172       NaN
   

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

   
      1.2495    8.6350    6.5000    4.8662    4.9878    1.6750
      2.7971    1.2876    4.2251    1.1320    4.7803    9.1516
      6.5000    1.6646    0.7675    6.5000    6.5000    6.5000
      6.5000    0.4085    4.7965    2.1341    6.5000    2.4876
      3.4528    2.9411    0.2630    2.7823    6.5000    3.5495
   
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
   
