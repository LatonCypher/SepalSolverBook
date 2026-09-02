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
      0.7243    0.5296    0.9548    0.7386
   
   R1[2] = 0.954800184008302
   C1 = 
      0.7404
      0.6771
      0.4474
      0.9705
      0.8477
      0.5966
      0.6789
      0.7542
   
   C1[5] = 0.5966084404457374

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
      0.9397    0.1597    0.3988    0.2917    0.7794
      0.6273    0.8705    0.5354    0.9372    0.3334
   

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
   
      0.9327    0.3123    0.9250    0.4061    0.1412    0.7956    0.4125    0.9857
      0.1204    0.7281    0.3462    0.1219    0.7126    0.8154    0.4187    0.1582
      0.0221    0.4006    0.6769    0.7394    0.0636    0.0944    0.7275    0.8976
      0.8912    0.8731    0.6370    0.5815    0.7444    0.3003    0.1276    0.9574
      0.2981    0.9876    0.1883    0.5607    0.6087    0.8438    0.3646    0.9661
      0.6815    0.1808    0.6510    0.1148    0.7650    0.1363    0.5170    0.0612
      0.4344    0.8137    0.6857    0.3337    0.9945    0.3908    0.4498    0.6818
      0.6948    0.1337    0.6020    0.2114    0.2841    0.0612    0.7652    0.7828
   
   B = 
   
      0.3212    0.7142    0.5853    0.6814    0.8560    0.3812    0.9253    0.2445
      0.4595    0.2455    0.4791    0.4237    0.7354    0.4247    0.1729    0.8887
      0.9480    0.3949    0.2840    0.2084    0.7787    0.6033    0.7693    0.5151
      0.8739    0.1731    0.6678    0.0717    0.9745    0.4434    0.2477    0.1424
      0.6609    0.8216    0.2971    0.7756    0.8000    0.4645    0.9138    0.0055
      0.7240    0.2199    0.4870    0.1954    0.4119    0.9942    0.4426    0.1364
      0.7639    0.5878    0.9318    0.9484    0.9888    0.8880    0.8636    0.1737
      0.0641    0.0016    0.7447    0.3704    0.3670    0.1979    0.2402    0.9223
   
   C = 
   
      2.7225    1.7133    2.7772    2.0110    3.3544    2.6443    2.8034    2.1299
      2.1992    1.4337    1.7157    1.6392    2.4049    2.1628    1.9455    1.2059
      2.2027    1.0115    2.3021    1.4691    2.6998    1.8619    1.7374    1.7827
      2.6678    1.9572    2.7084    2.2633    3.6645    2.2998    2.7631    2.3550
      2.5717    1.5283    2.7265    2.0417    3.2241    2.5319    2.2070    2.2002
      2.0225    1.7705    1.5680    1.8178    2.5369    1.7423    2.4116    0.8480
      2.7826    2.0071    2.4744    2.3345    3.4810    2.4577    2.7869    1.9959
      1.9068    1.5013    2.1930    1.9186    2.6643    1.8058    2.3171    1.4937
   
   D = 
   
      2.7225    1.7133    2.7772    2.0110    3.3544    2.6443    2.8034    2.1299
      2.1992    1.4337    1.7157    1.6392    2.4049    2.1628    1.9455    1.2059
      2.2027    1.0115    2.3021    1.4691    2.6998    1.8619    1.7374    1.7827
      2.6678    1.9572    2.7084    2.2633    3.6645    2.2998    2.7631    2.3550
      2.5717    1.5283    2.7265    2.0417    3.2241    2.5319    2.2070    2.2002
      2.0225    1.7705    1.5680    1.8178    2.5369    1.7423    2.4116    0.8480
      2.7826    2.0071    2.4744    2.3345    3.4810    2.4577    2.7869    1.9959
      1.9068    1.5013    2.1930    1.9186    2.6643    1.8058    2.3171    1.4937
   


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

   
      0.4128    0.5856    0.2532    0.9703    0.5000    0.4008
      0.8483    0.1876    0.1008    0.1056    0.6632    0.4205
      0.5494    0.4904    0.2622    0.6608    0.3755    0.4041
      0.9312    0.4139    0.7180    0.8450    0.3213    0.9512
      0.8140    0.8649    0.4508    0.9285    0.0597    0.0912
   
   
      0.8483
      0.5494
      0.9312
      0.8140
      0.5856
      0.8649
      0.7180
      0.9703
      0.6608
      0.8450
      0.9285
      0.6632
      0.9512
   

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

   
      4.3727    1.8488    9.7048    3.3020    0.3025    0.9084
      1.1016    9.7191    5.3922    7.0129    1.8092    5.8691
      3.9281    5.1110    3.1162    7.7819    3.9956    2.2883
      5.5878    5.7387    9.2835    0.3981    6.6964    9.1068
      2.9158    6.8967    6.8895    3.2301    8.7499    8.9701
   
   
      0.0000    0.0000    9.7048    0.0000    0.0000    0.0000
      0.0000    9.7191    5.3922    7.0129    0.0000    5.8691
      0.0000    5.1110    0.0000    7.7819    0.0000    0.0000
      5.5878    5.7387    9.2835    0.0000    6.6964    9.1068
      0.0000    6.8967    6.8895    0.0000    8.7499    8.9701
   
   
      0.0000    0.0000       NaN    0.0000    0.0000    0.0000
      0.0000       NaN    5.3922    7.0129    0.0000    5.8691
      0.0000    5.1110    0.0000    7.7819    0.0000    0.0000
      5.5878    5.7387       NaN    0.0000    6.6964       NaN
      0.0000    6.8967    6.8895    0.0000    8.7499    8.9701
   

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

   
      1.1746    6.5000    6.5000    6.5000    6.5000    6.5000
      9.3295    2.2762    6.5000    6.5000    2.5358    2.8064
      8.0090    4.4723    1.8335    1.8237    6.5000    6.5000
      2.9277    8.0008    8.1610    0.6384    0.2133    6.5000
      6.5000    2.5078    8.1922    6.5000    6.5000    6.5000
   
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
   
