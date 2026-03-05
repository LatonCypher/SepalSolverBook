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
      0.7207    0.9126    0.0720    0.2579
   
   R1[2] = 0.07198621306905117
   C1 = 
      0.6520
      0.9463
      0.4096
      0.9786
      0.5392
      0.7846
      0.7824
      0.7273
   
   C1[5] = 0.7845981127271603

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
      0.8208    0.2685    0.5289    0.1667    0.3644
      0.0171    0.7882    0.7611    0.8588    0.7806
   

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
   
      0.1263    0.8273    0.4295    0.6920    0.3283    0.6010    0.4818    0.7657
      0.4646    0.2183    0.3387    0.9374    0.4212    0.6883    0.8851    0.5464
      0.8813    0.4116    0.3352    0.7942    0.4183    0.7229    0.2609    0.6600
      0.0487    0.0414    0.5220    0.5800    0.6707    0.1579    0.9007    0.2637
      0.1049    0.5089    0.6421    0.8487    0.1112    0.0657    0.3563    0.8779
      0.2725    0.5445    0.9191    0.6165    0.7031    0.5598    0.7736    0.5545
      0.9443    0.9026    0.3496    0.7291    0.3383    0.0961    0.0211    0.9621
      0.3867    0.0865    0.2830    0.4273    0.5447    0.8083    0.8186    0.8201
   
   B = 
   
      0.3314    0.2637    0.0112    0.8173    0.9327    0.5183    0.9206    0.5647
      0.8950    0.8736    0.7860    0.8569    0.0705    0.8925    0.5266    0.8173
      0.5991    0.3473    0.8040    0.2919    0.5686    0.2894    0.5032    0.6999
      0.7867    0.3746    0.7383    0.7216    0.8415    0.2585    0.4017    0.6847
      0.7413    0.8569    0.4858    0.3368    0.4274    0.1268    0.4290    0.4316
      0.7537    0.6463    0.4980    0.1403    0.2889    0.5254    0.6828    0.7718
      0.2093    0.3699    0.3820    0.3192    0.3160    0.5490    0.0140    0.6835
      0.9068    0.0966    0.5535    0.4838    0.8091    0.8241    0.9246    0.9251
   
   C = 
   
      3.0754    2.0863    2.5745    2.1560    2.0884    2.3598    2.3119    3.1651
      2.8015    1.9680    2.3291    2.1274    2.5308    2.1272    2.2579    3.1431
      2.9941    1.9919    2.2175    2.3889    2.7141    2.2465    2.8028    3.1399
      1.8661    1.4830    1.7755    1.3093    1.6635    1.2430    1.2143    2.0947
      2.5451    1.3675    2.2527    1.9067    2.1024    1.8814    1.9378    2.6600
      3.2212    2.4017    2.8480    2.2331    2.4894    2.3176    2.4552    3.4417
      3.1038    1.8848    2.2922    2.7730    2.7142    2.4825    2.9140    3.1395
      2.6394    1.8072    2.0492    1.7361    2.2758    2.0891    2.2710    2.9569
   
   D = 
   
      3.0754    2.0863    2.5745    2.1560    2.0884    2.3598    2.3119    3.1651
      2.8015    1.9680    2.3291    2.1274    2.5308    2.1272    2.2579    3.1431
      2.9941    1.9919    2.2175    2.3889    2.7141    2.2465    2.8028    3.1399
      1.8661    1.4830    1.7755    1.3093    1.6635    1.2430    1.2143    2.0947
      2.5451    1.3675    2.2527    1.9067    2.1024    1.8814    1.9378    2.6600
      3.2212    2.4017    2.8480    2.2331    2.4894    2.3176    2.4552    3.4417
      3.1038    1.8848    2.2922    2.7730    2.7142    2.4825    2.9140    3.1395
      2.6394    1.8072    2.0492    1.7361    2.2758    2.0891    2.2710    2.9569
   


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

   
      0.2186    0.5300    0.3679    0.1570    0.6542    0.5852
      0.2439    0.8109    0.8590    0.0346    0.3685    0.7982
      0.1542    0.5032    0.8024    0.6034    0.6339    0.1191
      0.4461    0.7478    0.2266    0.4620    0.5582    0.9881
      0.0670    0.5733    0.1174    0.0418    0.6148    0.6648
   
   
      0.5300
      0.8109
      0.5032
      0.7478
      0.5733
      0.8590
      0.8024
      0.6034
      0.6542
      0.6339
      0.5582
      0.6148
      0.5852
      0.7982
      0.9881
      0.6648
   

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

   
      2.3215    8.1133    4.6250    0.1266    3.0800    7.1321
      2.2834    3.1318    5.4240    0.1914    3.6108    3.5469
      9.0482    6.9553    9.5772    8.7881    2.3065    0.4634
      5.0750    9.4377    9.9325    6.4923    4.9387    9.7820
      8.3927    0.3670    9.1523    4.0864    4.0812    0.0367
   
   
      0.0000    8.1133    0.0000    0.0000    0.0000    7.1321
      0.0000    0.0000    5.4240    0.0000    0.0000    0.0000
      9.0482    6.9553    9.5772    8.7881    0.0000    0.0000
      5.0750    9.4377    9.9325    6.4923    0.0000    9.7820
      8.3927    0.0000    9.1523    0.0000    0.0000    0.0000
   
   
      0.0000    8.1133    0.0000    0.0000    0.0000    7.1321
      0.0000    0.0000    5.4240    0.0000    0.0000    0.0000
         NaN    6.9553       NaN    8.7881    0.0000    0.0000
      5.0750       NaN       NaN    6.4923    0.0000       NaN
      8.3927    0.0000       NaN    0.0000    0.0000    0.0000
   

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

   
      1.2389    9.2741    2.1342    6.5000    1.9450    8.7232
      6.5000    6.5000    8.5373    4.6530    1.1797    8.8916
      4.5873    1.9872    4.7868    2.1620    1.3618    9.7306
      4.8131    9.5781    9.6660    3.6095    3.9070    6.5000
      1.0825    4.0396    0.7081    0.9122    3.5549    4.0942
   
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
   
