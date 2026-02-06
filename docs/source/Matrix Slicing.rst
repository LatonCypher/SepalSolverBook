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
      0.0894    0.1421    0.4308    0.4237
   
   R1[2] = 0.43079418723833074
   C1 = 
      0.3930
      0.0032
      0.3713
      0.6972
      0.8856
      0.7824
      0.8087
      0.9042
   
   C1[5] = 0.782357718465637

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
      0.6359    0.0536    0.1140    0.8415    0.5717
      0.6149    0.1537    0.3257    0.5228    0.2414
   

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
   
      0.3454    0.7173    0.0982    0.1399    0.8295    0.4170    0.1523    0.4121
      0.2838    0.3899    0.0633    0.3126    0.5571    0.7927    0.7752    0.5268
      0.6416    0.9530    0.0840    0.3721    0.8084    0.3468    0.4236    0.8201
      0.9764    0.9896    0.0779    0.1418    0.0386    0.9083    0.6321    0.9768
      0.3661    0.0394    0.4366    0.9050    0.3811    0.2150    0.9843    0.7258
      0.8130    0.4667    0.1927    0.4855    0.8177    0.9800    0.2408    0.5991
      0.6974    0.3674    0.9093    0.6474    0.7525    0.9946    0.2111    0.5566
      0.2883    0.9977    0.4181    0.9182    0.0234    0.8941    0.9201    0.6993
   
   B = 
   
      0.2020    0.6199    0.6730    0.4445    0.6034    0.2045    0.5813    0.8034
      0.0910    0.7252    0.2475    0.4674    0.7762    0.2929    0.3702    0.6733
      0.2492    0.1850    0.4676    0.0595    0.1518    0.5834    0.2017    0.3769
      0.5565    0.4255    0.7069    0.1236    0.4310    0.1490    0.4847    0.3206
      0.2460    0.0877    0.6000    0.1880    0.3202    0.6918    0.3305    0.8599
      0.1524    0.8565    0.8929    0.4034    0.2820    0.5612    0.7728    0.5245
      0.3429    0.2018    0.5756    0.4498    0.4307    0.3489    0.3174    0.0453
      0.3340    0.0026    0.6968    0.7807    0.2152    0.1621    0.9262    0.6993
   
   C = 
   
      0.6949    1.2736    1.7997    1.2263    1.3779    1.2866    1.5804    2.0695
      0.9822    1.4891    2.3936    1.5353    1.4676    1.4419    2.0043    1.9130
      1.1152    1.7183    2.5800    1.9042    2.0156    1.5494    2.3523    2.7778
      1.0766    2.3092    2.9175    2.3393    2.1813    1.4714    2.8385    2.7470
      1.3964    1.1394    2.5930    1.4867    1.4706    1.3213    2.0309    1.7680
      1.1581    2.0459    3.0177    1.7760    1.8622    1.7845    2.5785    2.8427
      1.3562    2.1044    3.2921    1.6880    1.8552    2.1198    2.6385    2.9256
      1.4554    2.3257    3.1148    2.0577    2.2141    1.6842    2.7048    2.3751
   
   D = 
   
      0.6949    1.2736    1.7997    1.2263    1.3779    1.2866    1.5804    2.0695
      0.9822    1.4891    2.3936    1.5353    1.4676    1.4419    2.0043    1.9130
      1.1152    1.7183    2.5800    1.9042    2.0156    1.5494    2.3523    2.7778
      1.0766    2.3092    2.9175    2.3393    2.1813    1.4714    2.8385    2.7470
      1.3964    1.1394    2.5930    1.4867    1.4706    1.3213    2.0309    1.7680
      1.1581    2.0459    3.0177    1.7760    1.8622    1.7845    2.5785    2.8427
      1.3562    2.1044    3.2921    1.6880    1.8552    2.1198    2.6385    2.9256
      1.4554    2.3257    3.1148    2.0577    2.2141    1.6842    2.7048    2.3751
   


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

   
      0.0851    0.9589    0.8325    0.9422    0.9903    0.9291
      0.5400    0.3605    0.5706    0.1692    0.1062    0.4067
      0.2975    0.7490    0.9953    0.6697    0.3266    0.3554
      0.2978    0.7487    0.1567    0.1730    0.1229    0.0372
      0.0678    0.1272    0.2532    0.3099    0.8703    0.2515
   
   
      0.5400
      0.9589
      0.7490
      0.7487
      0.8325
      0.5706
      0.9953
      0.9422
      0.6697
      0.9903
      0.8703
      0.9291
   

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

   
      1.3486    2.3771    9.7074    2.3075    1.0938    1.8298
      4.8588    0.6706    0.0144    5.5373    2.1511    2.0815
      6.8439    4.3882    2.0944    0.8092    9.9870    3.8741
      6.5416    0.6111    3.9369    9.6889    6.9691    6.3291
      5.7519    8.1158    4.1264    9.7458    1.7572    3.8537
   
   
      0.0000    0.0000    9.7074    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    5.5373    0.0000    0.0000
      6.8439    0.0000    0.0000    0.0000    9.9870    0.0000
      6.5416    0.0000    0.0000    9.6889    6.9691    6.3291
      5.7519    8.1158    0.0000    9.7458    0.0000    0.0000
   
   
      0.0000    0.0000       NaN    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    5.5373    0.0000    0.0000
      6.8439    0.0000    0.0000    0.0000       NaN    0.0000
      6.5416    0.0000    0.0000       NaN    6.9691    6.3291
      5.7519    8.1158    0.0000       NaN    0.0000    0.0000
   

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

   
      1.0159    4.0751    3.1203    6.5000    6.5000    6.5000
      4.6990    6.5000    6.5000    6.5000    6.5000    3.2015
      1.2210    2.6342    1.5830    3.6527    6.5000    6.5000
      8.5840    3.9212    6.5000    6.5000    6.5000    3.9892
      6.5000    6.5000    1.6366    9.9753    4.9866    4.8287
   
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
   
