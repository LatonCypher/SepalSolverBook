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
      0.6775    0.5607    0.5124    0.0544
   
   R1[2] = 0.5124368276674799
   C1 = 
      0.8142
      0.2677
      0.1031
      0.2889
      0.8066
      0.1178
      0.1914
      0.0839
   
   C1[5] = 0.1177864609311502

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
      0.9626    0.8834    0.2545    0.7300    0.5894
      0.2779    0.4984    0.5817    0.8159    0.7439
   

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
   
      0.7143    0.1866    0.0659    0.9383    0.4519    0.9932    0.6448    0.4886
      0.0725    0.2136    0.0610    0.4718    0.0184    0.2831    0.2571    0.9769
      0.3258    0.0817    0.4341    0.5184    0.6805    0.2071    0.2835    0.1016
      0.4483    0.5809    0.9538    0.4290    0.7188    0.8322    0.6522    0.8249
      0.9167    0.8460    0.4284    0.4025    0.3711    0.4495    0.2835    0.0278
      0.9872    0.1425    0.2626    0.9587    0.6726    0.6064    0.7506    0.2880
      0.1593    0.2084    0.3358    0.8166    0.6643    0.9600    0.9959    0.5493
      0.4483    0.6049    0.5812    0.9057    0.3721    0.7226    0.1908    0.8936
   
   B = 
   
      0.2652    0.9525    0.1934    0.0385    0.3303    0.9187    0.1324    0.3510
      0.9392    0.9136    0.9798    0.1002    0.7709    0.0356    0.2043    0.5007
      0.4602    0.7447    0.6334    0.0228    0.9005    0.8921    0.7625    0.0884
      0.1013    0.6511    0.5202    0.2324    0.8259    0.6446    0.7751    0.8751
      0.0522    0.2928    0.7755    0.8133    0.2577    0.9917    0.8432    0.7386
      0.8462    0.7884    0.9412    0.9341    0.6896    0.1473    0.5478    0.4632
      0.8023    0.3596    0.4936    0.7339    0.6716    0.7904    0.4412    0.5208
      0.0438    0.5735    0.5830    0.8100    0.8782    0.0454    0.1950    0.1326
   
   C = 
   
      1.8928    2.9383    2.7391    2.4299    2.8776    2.4527    2.2151    2.3654
      0.7852    1.4981    1.4845    1.3946    1.8638    0.7403    0.9400    0.9588
      0.8580    1.5684    1.6094    1.1883    1.5874    1.9577    1.6247    1.4069
      2.4480    3.5216    3.6269    2.7058    3.7308    2.9484    2.7485    2.2734
      1.9040    2.8081    2.3540    1.1757    2.2937    2.1735    1.6225    1.7690
      1.7767    3.0003    2.6264    2.1787    2.8127    3.1270    2.3898    2.4868
      2.1453    2.7485    3.1031    2.8373    3.1746    2.5921    2.5852    2.4314
      1.8692    3.2619    3.1025    2.1431    3.3929    2.2025    2.2961    2.1316
   
   D = 
   
      1.8928    2.9383    2.7391    2.4299    2.8776    2.4527    2.2151    2.3654
      0.7852    1.4981    1.4845    1.3946    1.8638    0.7403    0.9400    0.9588
      0.8580    1.5684    1.6094    1.1883    1.5874    1.9577    1.6247    1.4069
      2.4480    3.5216    3.6269    2.7058    3.7308    2.9484    2.7485    2.2734
      1.9040    2.8081    2.3540    1.1757    2.2937    2.1735    1.6225    1.7690
      1.7767    3.0003    2.6264    2.1787    2.8127    3.1270    2.3898    2.4868
      2.1453    2.7485    3.1031    2.8373    3.1746    2.5921    2.5852    2.4314
      1.8692    3.2619    3.1025    2.1431    3.3929    2.2025    2.2961    2.1316
   


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

   
      0.1107    0.8832    0.2611    0.8894    0.9018    0.3888
      0.5244    0.4394    0.7168    0.5507    0.3926    0.7351
      0.9957    0.0010    0.3463    0.3894    0.0108    0.6976
      0.8087    0.9279    0.7823    0.1874    0.4105    0.6037
      0.4847    0.0856    0.5079    0.3506    0.8501    0.7610
   
   
      0.5244
      0.9957
      0.8087
      0.8832
      0.9279
      0.7168
      0.7823
      0.5079
      0.8894
      0.5507
      0.9018
      0.8501
      0.7351
      0.6976
      0.6037
      0.7610
   

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

   
      9.2071    7.2732    1.4114    5.2690    5.5570    0.8350
      3.6621    5.7679    3.9327    8.9221    8.1028    4.8975
      5.5506    3.8917    5.8516    9.3145    4.0160    3.5769
      5.3656    5.3342    6.2596    0.7909    5.4188    4.9607
      5.6352    4.7425    1.1463    6.5218    1.0804    5.7905
   
   
      9.2071    7.2732    0.0000    5.2690    5.5570    0.0000
      0.0000    5.7679    0.0000    8.9221    8.1028    0.0000
      5.5506    0.0000    5.8516    9.3145    0.0000    0.0000
      5.3656    5.3342    6.2596    0.0000    5.4188    0.0000
      5.6352    0.0000    0.0000    6.5218    0.0000    5.7905
   
   
         NaN    7.2732    0.0000    5.2690    5.5570    0.0000
      0.0000    5.7679    0.0000    8.9221    8.1028    0.0000
      5.5506    0.0000    5.8516       NaN    0.0000    0.0000
      5.3656    5.3342    6.2596    0.0000    5.4188    0.0000
      5.6352    0.0000    0.0000    6.5218    0.0000    5.7905
   

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

   
      8.7037    1.8742    0.9115    0.1815    0.3959    2.2552
      0.2049    6.5000    6.5000    1.7474    9.9645    1.6745
      6.5000    4.0016    0.6338    9.2319    8.4734    9.9734
      8.3971    6.5000    6.5000    0.1413    6.5000    3.9313
      4.6979    8.3929    6.5000    2.4702    8.3327    6.5000
   
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
   
