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
      0.5791    0.5959    0.5097    0.0119
   
   R1[2] = 0.5096513858569214
   C1 = 
      0.8174
      0.1345
      0.8468
      0.8608
      0.3464
      0.2832
      0.7857
      0.3150
   
   C1[5] = 0.2832303525537052

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
      0.6809    0.8958    0.9477    0.9396    0.3820
      0.1518    0.9589    0.7086    0.6403    0.8915
   

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
   M_3 &=& A_{11}\left(B_{12} - B_{22}\left) \\
   M_4 &=& A_{22}\left(B_{21} - B_{11}\left) \\
   M_5 &=& \left(A_{11} + A_{12}\right)B_{22} \\
   M_6 &=& \left(A_{21} - A_{11}\right)\left(B_{11} + B_{12}\right) \\
   M_7 &=& \left(A_{12} - A_{22}\right)\left(B_{21} + B_{22}\right)
   \end{array}


3. **Combine results** to form the product matrix::

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
   
      0.5204    0.0617    0.4851    0.9039    0.1615    0.2595    0.6559    0.3545
      0.7931    0.6888    0.4081    0.8014    0.4885    0.6368    0.2060    0.4899
      0.7364    0.6487    0.8430    0.3407    0.1815    0.7524    0.6684    0.2263
      0.9227    0.2638    0.0013    0.6239    0.3457    0.9601    0.6855    0.0600
      0.0077    0.6604    0.0721    0.0447    0.0575    0.9974    0.7064    0.0082
      0.3241    0.4882    0.7199    0.7594    0.4908    0.4239    0.8638    0.1371
      0.8524    0.6700    0.6382    0.6194    0.1873    0.9671    0.5189    0.9831
      0.9372    0.9700    0.2507    0.0580    0.5013    0.4465    0.9898    0.9851
   
   B = 
   
      0.0933    0.2001    0.8560    0.0483    0.2226    0.5602    0.0862    0.5246
      0.7568    0.8762    0.2245    0.5517    0.0173    0.9588    0.8446    0.3282
      0.7723    0.8231    0.9504    0.7043    0.8284    0.4018    0.1398    0.2182
      0.4631    0.1270    0.5431    0.2393    0.1862    0.4045    0.3100    0.0407
      0.5284    0.4884    0.6356    0.2438    0.1017    0.4034    0.0516    0.8062
      0.3828    0.8417    0.3477    0.8650    0.8634    0.5158    0.2338    0.6113
      0.3833    0.2002    0.1570    0.8782    0.9318    0.1146    0.0960    0.5469
      0.0735    0.2213    0.8687    0.3306    0.5302    0.2712    0.9121    0.0743
   
   C = 
   
      1.3506    1.1793    2.0151    1.5741    1.7267    1.2816    0.9004    1.1098
      1.8985    2.1241    2.6464    1.9103    1.7270    2.2748    1.5963    1.6959
      2.0253    2.3588    2.4409    2.4257    2.3479    2.1104    1.2907    1.7857
      1.3930    1.6234    1.9024    1.8768    1.8618    1.7521    0.8587    1.8412
      1.2604    1.6560    0.7490    1.9260    1.6107    1.3053    0.8938    1.2813
      2.0702    1.9813    2.1977    2.2640    2.1119    1.7989    1.1088    1.6556
      2.1067    2.5885    3.2138    2.6714    2.7041    2.5274    2.1028    1.9306
      1.9297    2.2879    2.7748    2.4742    2.3249    2.3924    2.0768    2.1588
   
   D = 
   
      1.3506    1.1793    2.0151    1.5741    1.7267    1.2816    0.9004    1.1098
      1.8985    2.1241    2.6464    1.9103    1.7270    2.2748    1.5963    1.6959
      2.0253    2.3588    2.4409    2.4257    2.3479    2.1104    1.2907    1.7857
      1.3930    1.6234    1.9024    1.8768    1.8618    1.7521    0.8587    1.8412
      1.2604    1.6560    0.7490    1.9260    1.6107    1.3053    0.8938    1.2813
      2.0702    1.9813    2.1977    2.2640    2.1119    1.7989    1.1088    1.6556
      2.1067    2.5885    3.2138    2.6714    2.7041    2.5274    2.1028    1.9306
      1.9297    2.2879    2.7748    2.4742    2.3249    2.3924    2.0768    2.1588
   


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

   
      0.1172    0.3821    0.4604    0.5172    0.2899    0.9508
      0.5120    0.3966    0.7819    0.9291    0.6100    0.4373
      0.9701    0.0826    0.9461    0.0252    0.1311    0.8575
      0.3458    0.5831    0.6406    0.0320    0.5439    0.4373
      0.2180    0.3156    0.1818    0.9947    0.7655    0.3196
   
   
      0.5120
      0.9701
      0.5831
      0.7819
      0.9461
      0.6406
      0.5172
      0.9291
      0.9947
      0.6100
      0.5439
      0.7655
      0.9508
      0.8575
   

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

   
      2.3213    7.7893    8.6504    2.7013    0.5068    5.2503
      8.3014    1.0978    3.5593    1.2971    0.5332    9.7910
      9.2238    8.3023    0.2972    8.6695    9.3809    8.6517
      4.5538    9.5656    5.3951    0.7287    0.9569    9.7093
      2.4489    6.7750    9.7152    7.7917    4.2911    1.9021
   
   
      0.0000    7.7893    8.6504    0.0000    0.0000    5.2503
      8.3014    0.0000    0.0000    0.0000    0.0000    9.7910
      9.2238    8.3023    0.0000    8.6695    9.3809    8.6517
      0.0000    9.5656    5.3951    0.0000    0.0000    9.7093
      0.0000    6.7750    9.7152    7.7917    0.0000    0.0000
   
   
      0.0000    7.7893    8.6504    0.0000    0.0000    5.2503
      8.3014    0.0000    0.0000    0.0000    0.0000       NaN
         NaN    8.3023    0.0000    8.6695       NaN    8.6517
      0.0000       NaN    5.3951    0.0000    0.0000       NaN
      0.0000    6.7750       NaN    7.7917    0.0000    0.0000
   

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

   
      9.5426    1.5148    8.9007    9.0927    3.2676    1.8371
      8.6736    2.3284    3.9377    4.6666    6.5000    8.9997
      3.6665    3.2268    6.5000    9.8830    6.5000    4.6627
      9.5217    3.3342    3.0069    9.5195    6.5000    0.6184
      3.1250    9.5399    2.3057    6.5000    2.2732    0.8841
   
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
   
