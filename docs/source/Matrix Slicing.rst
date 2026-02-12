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
      0.1844    0.5221    0.5724    0.1367
   
   R1[2] = 0.5723803838891971
   C1 = 
      0.3763
      0.2991
      0.0930
      0.7778
      0.9318
      0.0869
      0.2669
      0.1614
   
   C1[5] = 0.08686726641721187

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
      0.9768    0.7544    0.2976    0.3599    0.2823
      0.3473    0.4587    0.0450    0.5418    0.5980
   

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
   
      0.3912    0.5729    0.7242    0.2613    0.5357    0.5384    0.8284    0.6937
      0.3261    0.1719    0.5898    0.2425    0.5515    0.5626    0.1302    0.0851
      0.3689    0.7845    0.2437    0.5840    0.7978    0.4013    0.0320    0.9944
      0.1832    0.4002    0.1556    0.7779    0.6339    0.7261    0.3867    0.4448
      0.8181    0.3431    0.7762    0.7537    0.5272    0.5759    0.5518    0.4834
      0.5155    0.9239    0.8372    0.6707    0.6755    0.0861    0.8155    0.4652
      0.7611    0.4354    0.1944    0.7288    0.0492    0.8581    0.9586    0.9427
      0.5977    0.3307    0.1643    0.2275    0.9624    0.6231    0.3800    0.3621
   
   B = 
   
      0.8634    0.5389    0.4336    0.8495    0.7329    0.1499    0.6049    0.1778
      0.0404    0.3143    0.0170    0.5058    0.2136    0.9817    0.1043    0.9083
      0.0982    0.7058    0.1160    0.4466    0.6070    0.9684    0.5297    0.1362
      0.7313    0.3865    0.0692    0.6718    0.3597    0.9614    0.6281    0.9695
      0.0152    0.1485    0.5843    0.1692    0.3908    0.7467    0.7498    0.1450
      0.0425    0.8676    0.1053    0.5995    0.5601    0.9146    0.0547    0.0767
      0.0566    0.5676    0.1288    0.8583    0.5803    0.8783    0.1475    0.7277
      0.3237    0.3381    0.4150    0.8516    0.1862    0.5883    0.7581    0.0202
   
   C = 
   
      0.9255    2.2545    1.0457    2.8363    2.0635    3.6018    1.9233    1.6778
      0.5909    1.4124    0.6631    1.4050    1.3429    2.1127    1.2078    0.7492
      1.1540    1.6641    1.1672    2.4611    1.5362    3.1988    2.1795    1.5673
      0.9648    1.7289    0.8393    2.2033    1.5556    3.0576    1.6327    1.6095
      1.5677    2.4424    1.1431    3.0411    2.3539    3.6251    2.2897    1.8254
      1.2657    2.2134    1.0846    2.9917    2.1966    4.0129    2.2569    2.4025
      1.6233    2.5805    1.0442    3.5915    2.2627    3.6485    2.0063    2.0534
      0.8918    1.6516    1.1268    2.0721    1.7033    2.6274    1.7121    1.1208
   
   D = 
   
      0.9255    2.2545    1.0457    2.8363    2.0635    3.6018    1.9233    1.6778
      0.5909    1.4124    0.6631    1.4050    1.3429    2.1127    1.2078    0.7492
      1.1540    1.6641    1.1672    2.4611    1.5362    3.1988    2.1795    1.5673
      0.9648    1.7289    0.8393    2.2033    1.5556    3.0576    1.6327    1.6095
      1.5677    2.4424    1.1431    3.0411    2.3539    3.6251    2.2897    1.8254
      1.2657    2.2134    1.0846    2.9917    2.1966    4.0129    2.2569    2.4025
      1.6233    2.5805    1.0442    3.5915    2.2627    3.6485    2.0063    2.0534
      0.8918    1.6516    1.1268    2.0721    1.7033    2.6274    1.7121    1.1208
   


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

   
      0.4454    0.9414    0.1154    0.1440    0.9383    0.0404
      0.0217    0.6196    0.9040    0.3040    0.7865    0.3890
      0.3720    0.4284    0.0624    0.7607    0.7966    0.9741
      0.9250    0.8806    0.2558    0.7069    0.5928    0.4913
      0.3646    0.0355    0.1683    0.6905    0.3559    0.4456
   
   
      0.9250
      0.9414
      0.6196
      0.8806
      0.9040
      0.7607
      0.7069
      0.6905
      0.9383
      0.7865
      0.7966
      0.5928
      0.9741
   

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

   
      0.2864    3.1420    4.6921    9.8175    3.4254    1.9121
      5.6739    3.9774    3.3671    1.0367    1.3451    7.5091
      7.6365    5.1032    8.3722    8.7860    2.7333    1.8337
      9.4790    5.5319    7.1374    7.4983    3.1224    7.8074
      4.3691    0.1849    7.1543    7.4215    2.6732    7.1734
   
   
      0.0000    0.0000    0.0000    9.8175    0.0000    0.0000
      5.6739    0.0000    0.0000    0.0000    0.0000    7.5091
      7.6365    5.1032    8.3722    8.7860    0.0000    0.0000
      9.4790    5.5319    7.1374    7.4983    0.0000    7.8074
      0.0000    0.0000    7.1543    7.4215    0.0000    7.1734
   
   
      0.0000    0.0000    0.0000       NaN    0.0000    0.0000
      5.6739    0.0000    0.0000    0.0000    0.0000    7.5091
      7.6365    5.1032    8.3722    8.7860    0.0000    0.0000
         NaN    5.5319    7.1374    7.4983    0.0000    7.8074
      0.0000    0.0000    7.1543    7.4215    0.0000    7.1734
   

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

   
      4.1511    6.5000    8.9242    6.5000    0.4325    6.5000
      4.9681    4.4735    6.5000    6.5000    1.4715    4.1383
      9.7239    9.6464    6.5000    3.8027    4.3989    2.3051
      3.4172    3.8383    1.8602    2.6229    9.6513    1.9523
      8.7740    3.8247    4.3405    6.5000    6.5000    1.1570
   
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
   
