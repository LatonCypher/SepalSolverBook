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
      0.5650    0.5841    0.2147    0.8065
   
   R1[2] = 0.21468226116150546
   C1 = 
      0.9925
      0.5203
      0.7882
      0.6154
      0.8541
      0.2962
      0.1960
      0.0016
   
   C1[5] = 0.2962230730873484

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
      0.0685    0.2965    0.5467    0.0981    0.8680
      0.0526    0.4697    0.0024    0.1620    0.7291
   

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
   
      0.2465    0.7319    0.9170    0.5854    0.5489    0.0192    0.6021    0.9070
      0.9477    0.4078    0.0384    0.5184    0.5984    0.5780    0.2291    0.6607
      0.3452    0.4515    0.9788    0.5040    0.0493    0.0972    0.5122    0.8951
      0.0889    0.9099    0.3502    0.4954    0.1469    0.9438    0.7587    0.4465
      0.0763    0.2578    0.5658    0.5281    0.4065    0.6994    0.1137    0.3650
      0.7666    0.5602    0.3421    0.6341    0.3590    0.9733    0.9275    0.4161
      0.0063    0.5304    0.2210    0.1890    0.1103    0.2988    0.7148    0.2716
      0.8072    0.1228    0.4739    0.7390    0.3609    0.0400    0.9877    0.6938
   
   B = 
   
      0.0826    0.5562    0.8093    0.8941    0.3148    0.4639    0.9968    0.8855
      0.8048    0.7861    0.6088    0.5697    0.4322    0.0611    0.5083    0.7233
      0.6426    0.2205    0.3026    0.1326    0.2223    0.7365    0.4683    0.9381
      0.5935    0.7054    0.0195    0.3554    0.6102    0.4702    0.3389    0.2251
      0.6300    0.4149    0.9240    0.6726    0.1346    0.0696    0.9043    0.1528
      0.7333    0.1100    0.4906    0.1608    0.2892    0.6634    0.9942    0.5474
      0.1195    0.9181    0.9154    0.3177    0.6547    0.7429    0.1212    0.1878
      0.8849    0.5701    0.0329    0.0131    0.7646    0.0944    0.1555    0.7028
   
   C = 
   
      2.7805    2.6272    2.0316    1.5424    2.1221    1.6935    1.9750    2.5845
      2.1517    2.1207    2.1049    1.8459    1.7023    1.3942    2.5919    2.2020
      2.2756    2.1300    1.4519    1.0981    1.8835    1.6786    1.5454    2.4500
      2.5291    2.3072    2.0494    1.3178    1.9322    1.8298    2.1155    2.1723
      1.9963    1.3004    1.2351    0.9046    1.1940    1.3274    1.7846    1.6266
      2.5292    2.7341    2.7492    1.9734    2.2016    2.3389    2.8934    2.6019
      1.2960    1.4922    1.3104    0.7573    1.1726    1.0496    0.9693    1.1447
      1.8974    2.6278    2.1659    1.6892    2.1008    1.9294    1.9331    2.1646
   
   D = 
   
      2.7805    2.6272    2.0316    1.5424    2.1221    1.6935    1.9750    2.5845
      2.1517    2.1207    2.1049    1.8459    1.7023    1.3942    2.5919    2.2020
      2.2756    2.1300    1.4519    1.0981    1.8835    1.6786    1.5454    2.4500
      2.5291    2.3072    2.0494    1.3178    1.9322    1.8298    2.1155    2.1723
      1.9963    1.3004    1.2351    0.9046    1.1940    1.3274    1.7846    1.6266
      2.5292    2.7341    2.7492    1.9734    2.2016    2.3389    2.8934    2.6019
      1.2960    1.4922    1.3104    0.7573    1.1726    1.0496    0.9693    1.1447
      1.8974    2.6278    2.1659    1.6892    2.1008    1.9294    1.9331    2.1646
   


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

   
      0.1121    0.7556    0.5033    0.8801    0.5978    0.7475
      0.5791    0.0735    0.8549    0.7565    0.1749    0.1749
      0.3241    0.3578    0.6247    0.0798    0.8102    0.5021
      0.7587    0.7755    0.5010    0.2880    0.1346    0.2864
      0.0432    0.0649    0.8764    0.1144    0.1006    0.5742
   
   
      0.5791
      0.7587
      0.7556
      0.7755
      0.5033
      0.8549
      0.6247
      0.5010
      0.8764
      0.8801
      0.7565
      0.5978
      0.8102
      0.7475
      0.5021
      0.5742
   

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

   
      9.2303    9.0631    5.8314    5.1565    0.3023    6.3687
      1.5198    3.4051    5.1240    4.6591    9.5210    0.2203
      8.0707    3.5263    0.5886    8.8800    7.7587    8.8009
      4.0488    5.0264    3.1626    6.0469    6.0368    8.1997
      0.5352    9.1509    7.9256    4.1411    8.6064    7.9785
   
   
      9.2303    9.0631    5.8314    5.1565    0.0000    6.3687
      0.0000    0.0000    5.1240    0.0000    9.5210    0.0000
      8.0707    0.0000    0.0000    8.8800    7.7587    8.8009
      0.0000    5.0264    0.0000    6.0469    6.0368    8.1997
      0.0000    9.1509    7.9256    0.0000    8.6064    7.9785
   
   
         NaN       NaN    5.8314    5.1565    0.0000    6.3687
      0.0000    0.0000    5.1240    0.0000       NaN    0.0000
      8.0707    0.0000    0.0000    8.8800    7.7587    8.8009
      0.0000    5.0264    0.0000    6.0469    6.0368    8.1997
      0.0000       NaN    7.9256    0.0000    8.6064    7.9785
   

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

   
      1.2213    6.5000    8.2308    2.7458    1.5830    6.5000
      3.7886    8.7954    3.1355    1.5174    1.8438    3.5465
      6.5000    0.3580    3.2523    2.0059    6.5000    6.5000
      8.5916    4.8487    4.1531    9.6107    4.8279    8.9464
      9.2337    6.5000    6.5000    2.6023    0.9600    8.7342
   
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
   
