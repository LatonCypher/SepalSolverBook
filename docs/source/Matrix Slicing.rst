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
      0.9013    0.3997    0.4970    0.3574
   
   R1[2] = 0.4970369010728861
   C1 = 
      0.8217
      0.4137
      0.1803
      0.1641
      0.1924
      0.2524
      0.4544
      0.2079
   
   C1[5] = 0.2523531627724588

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
      0.6716    0.3913    0.5837    0.3219    0.9088
      0.3137    0.0455    0.8988    0.6859    0.9538
   

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
   
      0.7048    0.5552    0.3973    0.0149    0.6189    0.6407    0.3756    0.5413
      0.0244    0.8218    0.7397    0.9351    0.4140    0.2617    0.8553    0.4234
      0.8815    0.1349    0.6339    0.0755    0.7709    0.0704    0.0374    0.5689
      0.6846    0.8467    0.1848    0.3222    0.9768    0.2501    0.7501    0.6786
      0.3904    0.7324    0.3975    0.7490    0.2502    0.4665    0.4182    0.0033
      0.3446    0.4997    0.3985    0.6179    0.9601    0.0847    0.4911    0.0439
      0.9719    0.7226    0.8232    0.5974    0.0296    0.8249    0.3530    0.5876
      0.4690    0.2646    0.6244    0.2846    0.1224    0.0026    0.1906    0.7400
   
   B = 
   
      0.9555    0.8674    0.1397    0.7064    0.0497    0.3579    0.2748    0.5539
      0.8924    0.6368    0.2375    0.4709    0.5309    0.4403    0.6376    0.1741
      0.2604    0.8767    0.0074    0.8953    0.2670    0.7152    0.3999    0.4229
      0.7214    0.5919    0.5630    0.2744    0.6970    0.1911    0.8608    0.8943
      0.4910    0.2084    0.6734    0.2121    0.9861    0.6915    0.4223    0.8769
      0.9218    0.4639    0.7574    0.6507    0.6041    0.2806    0.5736    0.7457
      0.9760    0.9251    0.1471    0.5376    0.9958    0.9965    0.8345    0.8513
      0.7149    0.0856    0.7645    0.9303    0.6941    0.9779    0.2036    0.3738
   
   C = 
   
      2.9310    2.1421    1.6127    2.3727    2.1933    2.2950    1.7719    2.2109
      3.2059    2.7817    1.6570    2.4349    2.9987    2.7044    2.7563    2.7503
      2.0688    1.7276    1.2152    2.0331    1.5721    1.9891    1.1597    1.8203
      3.6176    2.5574    1.9558    2.5406    3.0897    2.9682    2.3993    2.8281
      2.6338    2.2526    1.2390    1.7666    1.9838    1.6135    2.1009    2.1062
      2.3849    2.0297    1.3342    1.5687    2.3369    1.9664    1.9773    2.3386
      3.7583    3.1442    1.7956    3.2072    2.3550    2.5472    2.4710    2.7077
      1.8297    1.5576    0.9713    1.9115    1.3545    1.7842    1.1551    1.3725
   
   D = 
   
      2.9310    2.1421    1.6127    2.3727    2.1933    2.2950    1.7719    2.2109
      3.2059    2.7817    1.6570    2.4349    2.9987    2.7044    2.7563    2.7503
      2.0688    1.7276    1.2152    2.0331    1.5721    1.9891    1.1597    1.8203
      3.6176    2.5574    1.9558    2.5406    3.0897    2.9682    2.3993    2.8281
      2.6338    2.2526    1.2390    1.7666    1.9838    1.6135    2.1009    2.1062
      2.3849    2.0297    1.3342    1.5687    2.3369    1.9664    1.9773    2.3386
      3.7583    3.1442    1.7956    3.2072    2.3550    2.5472    2.4710    2.7077
      1.8297    1.5576    0.9713    1.9115    1.3545    1.7842    1.1551    1.3725
   


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

   
      0.1087    0.0671    0.3505    0.3564    0.1335    0.1108
      0.2126    0.6292    0.5345    0.6203    0.3118    0.7186
      0.6068    0.9756    0.7069    0.9944    0.0259    0.6604
      0.3138    0.2059    0.4385    0.7387    0.5710    0.9010
      0.8909    0.8716    0.6830    0.3387    0.2078    0.8077
   
   
      0.6068
      0.8909
      0.6292
      0.9756
      0.8716
      0.5345
      0.7069
      0.6830
      0.6203
      0.9944
      0.7387
      0.5710
      0.7186
      0.6604
      0.9010
      0.8077
   

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

   
      2.5923    2.2618    8.5279    9.1328    7.8913    0.9607
      2.9415    1.9367    2.3600    5.4299    7.1120    2.9366
      4.6526    6.3944    9.5660    5.7256    0.1900    5.0948
      3.8040    0.5917    2.0199    0.7638    5.8415    4.5608
      6.4427    1.6230    4.5192    4.2456    3.9308    6.7181
   
   
      0.0000    0.0000    8.5279    9.1328    7.8913    0.0000
      0.0000    0.0000    0.0000    5.4299    7.1120    0.0000
      0.0000    6.3944    9.5660    5.7256    0.0000    5.0948
      0.0000    0.0000    0.0000    0.0000    5.8415    0.0000
      6.4427    0.0000    0.0000    0.0000    0.0000    6.7181
   
   
      0.0000    0.0000    8.5279       NaN    7.8913    0.0000
      0.0000    0.0000    0.0000    5.4299    7.1120    0.0000
      0.0000    6.3944       NaN    5.7256    0.0000    5.0948
      0.0000    0.0000    0.0000    0.0000    5.8415    0.0000
      6.4427    0.0000    0.0000    0.0000    0.0000    6.7181
   

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

   
      8.5827    9.7516    8.8931    8.7420    6.5000    6.5000
      6.5000    9.7851    4.1336    6.5000    3.5988    6.5000
      2.8045    4.2386    6.5000    3.6014    6.5000    8.1752
      6.5000    3.9730    9.0126    6.5000    6.5000    1.4619
      9.5296    0.5893    8.1436    2.1366    1.1727    8.1637
   
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
   
