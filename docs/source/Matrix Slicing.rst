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
      0.1773    0.6741    0.3299    0.4411
   
   R1[2] = 0.3299192155347582
   C1 = 
      0.9253
      0.4768
      0.8076
      0.5957
      0.9908
      0.9560
      0.7329
      0.4900
   
   C1[5] = 0.9560433520211636

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
      0.1746    0.1067    0.0845    0.5679    0.8898
      0.6356    0.2590    0.5901    0.4348    0.8392
   

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
   
      0.7725    0.4067    0.0931    0.8299    0.3145    0.9389    0.0900    0.4463
      0.3595    0.9157    0.2267    0.8299    0.6782    0.8300    0.8503    0.4735
      0.9015    0.6755    0.7841    0.2484    0.1171    0.8587    0.5732    0.8371
      0.9099    0.9910    0.0798    0.0831    0.4799    0.6993    0.6166    0.3253
      0.4414    0.9078    0.5234    0.4664    0.8019    0.4688    0.6092    0.7682
      0.9869    0.5520    0.6453    0.8168    0.6611    0.5161    0.8653    0.3426
      0.2831    0.9935    0.7546    0.9817    0.6336    0.7898    0.6582    0.1026
      0.8146    0.2336    0.9099    0.6957    0.4804    0.2405    0.0127    0.8939
   
   B = 
   
      0.4088    0.8070    0.4051    0.6111    0.7798    0.1044    0.5503    0.6864
      0.8937    0.9930    0.5325    0.9250    0.1351    0.8392    0.9717    0.1960
      0.1150    0.7539    0.9499    0.8683    0.8704    0.7803    0.3281    0.4336
      0.6297    0.7595    0.9643    0.1824    0.2136    0.8177    0.1876    0.2750
      0.7953    0.4215    0.7593    0.9765    0.8289    0.6935    0.3516    0.6169
      0.0465    0.5763    0.4466    0.9833    0.3110    0.4648    0.1703    0.1493
      0.6326    0.1994    0.5220    0.5793    0.9056    0.0798    0.3689    0.9368
      0.7761    0.2441    0.5190    0.8409    0.6458    0.7874    0.7314    0.2749
   
   C = 
   
      1.9097    2.5284    2.3549    2.7382    1.8380    2.1863    1.6366    1.4197
      2.9973    3.0500    3.2241    3.7841    2.6747    2.9583    2.3575    2.2218
      2.3641    3.0410    2.9152    3.8965    2.9536    2.6611    2.4673    2.1271
      2.3758    2.6493    2.2196    3.3441    2.3143    2.0200    2.2586    1.9438
      2.9868    2.9238    3.1442    3.8920    2.8804    3.0253    2.5327    2.1828
      2.8484    3.2838    3.4564    3.7655    3.2938    2.7749    2.3345    2.6799
      2.7451    3.4081    3.5379    3.7892    2.6547    3.1947    2.2280    2.1399
      2.1795    2.6657    2.9323    3.0955    2.6692    2.7098    1.9726    1.7808
   
   D = 
   
      1.9097    2.5284    2.3549    2.7382    1.8380    2.1863    1.6366    1.4197
      2.9973    3.0500    3.2241    3.7841    2.6747    2.9583    2.3575    2.2218
      2.3641    3.0410    2.9152    3.8965    2.9536    2.6611    2.4673    2.1271
      2.3758    2.6493    2.2196    3.3441    2.3143    2.0200    2.2586    1.9438
      2.9868    2.9238    3.1442    3.8920    2.8804    3.0253    2.5327    2.1828
      2.8484    3.2838    3.4564    3.7655    3.2938    2.7749    2.3345    2.6799
      2.7451    3.4081    3.5379    3.7892    2.6547    3.1947    2.2280    2.1399
      2.1795    2.6657    2.9323    3.0955    2.6692    2.7098    1.9726    1.7808
   


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

   
      0.3283    0.0345    0.4237    0.1184    0.7621    0.0448
      0.1986    0.2548    0.9753    0.0404    0.6519    0.5535
      0.4887    0.9841    0.9192    0.3503    0.0385    0.6886
      0.3416    0.2919    0.2076    0.1256    0.8337    0.9603
      0.4365    0.1462    0.3324    0.8248    0.3546    0.4482
   
   
      0.9841
      0.9753
      0.9192
      0.8248
      0.7621
      0.6519
      0.8337
      0.5535
      0.6886
      0.9603
   

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

   
      1.1042    0.8356    2.5223    2.0363    9.7593    6.7827
      7.0918    4.4288    9.6000    4.9030    9.2604    0.3061
      9.9702    3.3069    9.6130    9.9819    3.3765    4.9093
      9.1933    3.6814    3.7842    7.4521    2.1194    3.6591
      4.8207    3.9039    6.3102    5.8288    0.9023    4.5796
   
   
      0.0000    0.0000    0.0000    0.0000    9.7593    6.7827
      7.0918    0.0000    9.6000    0.0000    9.2604    0.0000
      9.9702    0.0000    9.6130    9.9819    0.0000    0.0000
      9.1933    0.0000    0.0000    7.4521    0.0000    0.0000
      0.0000    0.0000    6.3102    5.8288    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000       NaN    6.7827
      7.0918    0.0000       NaN    0.0000       NaN    0.0000
         NaN    0.0000       NaN       NaN    0.0000    0.0000
         NaN    0.0000    0.0000    7.4521    0.0000    0.0000
      0.0000    0.0000    6.3102    5.8288    0.0000    0.0000
   

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

   
      9.9343    1.4735    1.6914    4.1412    3.4066    3.0555
      0.4145    6.5000    6.5000    4.1064    3.1822    0.4829
      1.1815    0.1946    2.9643    0.1149    9.7232    9.5716
      8.7611    8.0152    6.5000    9.2327    1.4092    4.4588
      1.2991    0.1231    3.3533    0.9230    6.5000    9.7393
   
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
   
