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
      0.1011    0.2492    0.5623    0.7842
   
   R1[2] = 0.5623112492498379
   C1 = 
      0.2286
      0.5096
      0.1423
      0.5458
      0.7904
      0.2574
      0.1957
      0.2879
   
   C1[5] = 0.2573538013880827

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
      0.9407    0.5701    0.2663    0.0439    0.3458
      0.1900    0.9158    0.8120    0.0529    0.8908
   

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
   
      0.2917    0.4060    0.5907    0.6597    0.5462    0.5919    0.0011    0.6166
      0.2035    0.8469    0.5152    0.8676    0.2120    0.4105    0.9998    0.1988
      0.9817    0.4590    0.7853    0.8118    0.7657    0.6036    0.6784    0.2793
      0.8341    0.5568    0.0471    0.0401    0.9022    0.5329    0.0105    0.1277
      0.2993    0.5414    0.4455    0.5157    0.1832    0.7988    0.1779    0.8980
      0.3416    0.1457    0.0254    0.5072    0.2778    0.2896    0.2968    0.8374
      0.1326    0.0618    0.0657    0.7119    0.1344    0.0503    0.7744    0.9870
      0.3398    0.4948    0.1876    0.6188    0.0148    0.7265    0.8752    0.7124
   
   B = 
   
      0.4317    0.1864    0.9130    0.4939    0.1052    0.2309    0.8900    0.6439
      0.1078    0.7485    0.7838    0.3679    0.5801    0.9057    0.5670    0.6767
      0.8868    0.8581    0.2824    0.5945    0.4087    0.9617    0.8243    0.0156
      0.3194    0.1741    0.4030    0.2083    0.2388    0.1297    0.5794    0.4772
      0.7088    0.3189    0.4896    0.9288    0.7708    0.2162    0.6207    0.2714
      0.1702    0.3638    0.2665    0.6657    0.6007    0.0223    0.2176    0.5553
      0.1596    0.3529    0.2634    0.7748    0.7468    0.7892    0.9766    0.2291
      0.7441    0.3915    0.0447    0.5047    0.0874    0.6832    0.2651    0.4176
   
   C = 
   
      1.8511    1.6113    1.4702    1.9954    1.4964    1.6421    1.9913    1.5212
      1.4408    1.9126    1.8302    2.2443    2.1044    2.4018    2.8388    1.7238
      2.3906    2.1543    2.5319    3.0694    2.3681    2.4081    3.5949    2.1574
      1.3015    1.1549    1.8196    1.9184    1.4740    1.0498    1.8401    1.5302
      1.7099    1.6965    1.4208    2.0123    1.4832    1.8661    1.9388    1.7212
      1.2644    0.9093    0.9664    1.4464    0.9348    1.1738    1.4487    1.2149
      1.3114    0.9721    0.8023    1.5320    1.0450    1.5577    1.7321    1.1220
      1.3680    1.5592    1.4637    2.1254    1.7109    1.9841    2.3071    1.7574
   
   D = 
   
      1.8511    1.6113    1.4702    1.9954    1.4964    1.6421    1.9913    1.5212
      1.4408    1.9126    1.8302    2.2443    2.1044    2.4018    2.8388    1.7238
      2.3906    2.1543    2.5319    3.0694    2.3681    2.4081    3.5949    2.1574
      1.3015    1.1549    1.8196    1.9184    1.4740    1.0498    1.8401    1.5302
      1.7099    1.6965    1.4208    2.0123    1.4832    1.8661    1.9388    1.7212
      1.2644    0.9093    0.9664    1.4464    0.9348    1.1738    1.4487    1.2149
      1.3114    0.9721    0.8023    1.5320    1.0450    1.5577    1.7321    1.1220
      1.3680    1.5592    1.4637    2.1254    1.7109    1.9841    2.3071    1.7574
   


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

   
      0.6201    0.0563    0.3569    0.1237    0.6958    0.9831
      0.9040    0.2373    0.9250    0.4765    0.8590    0.4709
      0.8993    0.1606    0.8474    0.8274    0.8690    0.8927
      0.0333    0.1803    0.7927    0.5752    0.3900    0.3778
      0.8848    0.7107    0.6983    0.1731    0.1078    0.2580
   
   
      0.6201
      0.9040
      0.8993
      0.8848
      0.7107
      0.9250
      0.8474
      0.7927
      0.6983
      0.8274
      0.5752
      0.6958
      0.8590
      0.8690
      0.9831
      0.8927
   

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

   
      3.3556    7.7543    3.7447    7.8364    2.7776    3.3984
      4.6266    2.7548    7.8997    2.7946    2.4596    5.0709
      6.9352    2.7818    0.2158    8.8317    3.1979    5.1144
      8.2685    2.1917    9.2769    1.2038    9.6011    1.2316
      9.6985    0.8896    8.5693    7.4581    0.1771    0.4790
   
   
      0.0000    7.7543    0.0000    7.8364    0.0000    0.0000
      0.0000    0.0000    7.8997    0.0000    0.0000    5.0709
      6.9352    0.0000    0.0000    8.8317    0.0000    5.1144
      8.2685    0.0000    9.2769    0.0000    9.6011    0.0000
      9.6985    0.0000    8.5693    7.4581    0.0000    0.0000
   
   
      0.0000    7.7543    0.0000    7.8364    0.0000    0.0000
      0.0000    0.0000    7.8997    0.0000    0.0000    5.0709
      6.9352    0.0000    0.0000    8.8317    0.0000    5.1144
      8.2685    0.0000       NaN    0.0000       NaN    0.0000
         NaN    0.0000    8.5693    7.4581    0.0000    0.0000
   

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

   
      6.5000    4.4843    4.4737    9.6807    8.8459    6.5000
      9.2021    0.0627    1.2594    6.5000    2.4241    0.7591
      6.5000    6.5000    9.5458    9.2063    8.9900    2.4314
      8.2845    2.4420    3.8053    6.5000    0.0100    4.4889
      6.5000    2.8531    2.7444    6.5000    3.5198    9.4274
   
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
   
