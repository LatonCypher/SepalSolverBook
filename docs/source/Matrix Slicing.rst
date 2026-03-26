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
      0.1901    0.0796    0.2111    0.7587
   
   R1[2] = 0.21106485859980884
   C1 = 
      0.5269
      0.2835
      0.9735
      0.2043
      0.0543
      0.5068
      0.3207
      0.6013
   
   C1[5] = 0.5068483423076804

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
      0.0750    0.2302    0.2353    0.3562    0.4804
      0.2553    0.1492    0.1113    0.0369    0.7868
   

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
   
      0.0664    0.0318    0.8640    0.2049    0.6333    0.8719    0.7317    0.0448
      0.0399    0.9697    0.8103    0.3317    0.3477    0.6524    0.9175    0.6499
      0.5880    0.2870    0.0030    0.4269    0.5303    0.0076    0.2386    0.2425
      0.8068    0.7816    0.6278    0.7668    0.4081    0.8697    0.0030    0.3511
      0.3111    0.1418    0.0415    0.0364    0.0679    0.0100    0.5137    0.6067
      0.3346    0.9497    0.6171    0.8960    0.2871    0.8225    0.9720    0.9585
      0.7234    0.2600    0.5616    0.1992    0.1248    0.6710    0.1188    0.7754
      0.4743    0.4881    0.7243    0.9056    0.3347    0.3656    0.5053    0.7767
   
   B = 
   
      0.6472    0.8869    0.8565    0.8820    0.1436    0.2521    0.6998    0.1165
      0.3815    0.4230    0.5166    0.0002    0.9757    0.1484    0.3023    0.5665
      0.9506    0.1489    0.8079    0.8566    0.2989    0.4089    0.2878    0.0559
      0.2941    0.8056    0.7600    0.8839    0.3783    0.3558    0.7305    0.9687
      0.8476    0.3423    0.4058    0.8165    0.0734    0.7445    0.3549    0.4037
      0.6073    0.9443    0.7280    0.7691    0.7289    0.7897    0.1067    0.3276
      0.3957    0.2802    0.0488    0.6123    0.8065    0.5405    0.5790    0.4169
      0.9551    0.6566    0.1262    0.8435    0.0005    0.2498    0.5375    0.1263
   
   C = 
   
      2.3353    1.6406    1.8601    2.6533    1.6485    2.0143    1.2199    1.1246
      2.9383    2.2523    2.1846    2.9183    2.5609    2.0356    1.8701    1.7394
      1.3986    1.4020    1.2417    1.6881    0.7639    0.9343    1.2685    0.9915
      2.8534    2.9498    3.0281    3.2274    2.0226    1.9289    1.9698    1.8099
      1.1520    0.9464    0.5374    1.2316    0.6361    0.6171    0.9478    0.4756
      3.4720    3.2889    2.8404    3.8866    2.9031    2.4245    2.6210    2.3911
      2.4606    2.2144    2.0018    2.6399    1.1951    1.4021    1.4934    0.8735
      2.8955    2.5759    2.4564    3.3584    1.8024    1.8154    2.2173    1.8132
   
   D = 
   
      2.3353    1.6406    1.8601    2.6533    1.6485    2.0143    1.2199    1.1246
      2.9383    2.2523    2.1846    2.9183    2.5609    2.0356    1.8701    1.7394
      1.3986    1.4020    1.2417    1.6881    0.7639    0.9343    1.2685    0.9915
      2.8534    2.9498    3.0281    3.2274    2.0226    1.9289    1.9698    1.8099
      1.1520    0.9464    0.5374    1.2316    0.6361    0.6171    0.9478    0.4756
      3.4720    3.2889    2.8404    3.8866    2.9031    2.4245    2.6210    2.3911
      2.4606    2.2144    2.0018    2.6399    1.1951    1.4021    1.4934    0.8735
      2.8955    2.5759    2.4564    3.3584    1.8024    1.8154    2.2173    1.8132
   


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

   
      0.0003    0.4407    0.0409    0.2231    0.2752    0.3830
      0.9403    0.8472    0.0797    0.0216    0.8998    0.1009
      0.5180    0.4162    0.4395    0.3898    0.8344    0.7503
      0.6919    0.5616    0.8552    0.3800    0.3391    0.6621
      0.2495    0.3611    0.1567    0.2055    0.0562    0.7875
   
   
      0.9403
      0.5180
      0.6919
      0.8472
      0.5616
      0.8552
      0.8998
      0.8344
      0.7503
      0.6621
      0.7875
   

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

   
      8.8477    4.9031    2.8296    3.4272    4.0542    6.4740
      7.1761    3.1064    7.4137    7.4618    9.5428    1.0683
      2.2206    3.6389    6.2494    1.0067    8.7883    3.0069
      1.5482    0.2033    1.5852    9.0422    6.0840    5.9093
      4.1043    1.4420    5.2278    6.9194    7.2497    1.3445
   
   
      8.8477    0.0000    0.0000    0.0000    0.0000    6.4740
      7.1761    0.0000    7.4137    7.4618    9.5428    0.0000
      0.0000    0.0000    6.2494    0.0000    8.7883    0.0000
      0.0000    0.0000    0.0000    9.0422    6.0840    5.9093
      0.0000    0.0000    5.2278    6.9194    7.2497    0.0000
   
   
      8.8477    0.0000    0.0000    0.0000    0.0000    6.4740
      7.1761    0.0000    7.4137    7.4618       NaN    0.0000
      0.0000    0.0000    6.2494    0.0000    8.7883    0.0000
      0.0000    0.0000    0.0000       NaN    6.0840    5.9093
      0.0000    0.0000    5.2278    6.9194    7.2497    0.0000
   

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

   
      4.5117    0.7018    2.0941    9.3546    4.8329    1.7649
      0.6328    3.6960    0.6833    6.5000    6.5000    6.5000
      8.6291    4.8941    1.4247    6.5000    8.9792    9.3473
      6.5000    0.8561    6.5000    4.0404    1.1322    9.0955
      4.4053    1.8484    6.5000    3.2365    0.2847    6.5000
   
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
   
