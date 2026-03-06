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
      0.8839    0.3509    0.2607    0.1276
   
   R1[2] = 0.2606615681088512
   C1 = 
      0.1449
      0.0984
      0.6038
      0.9994
      0.0628
      0.5994
      0.7574
      0.4801
   
   C1[5] = 0.5994100362968423

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
      0.0282    0.7635    0.1489    0.8719    0.2949
      0.3607    0.8253    0.6340    0.2513    0.3769
   

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
   
      0.9356    0.1136    0.2020    0.2450    0.3853    0.5315    0.1653    0.8183
      0.8434    0.2708    0.8930    0.9973    0.4072    0.6559    0.1087    0.4878
      0.5018    0.9112    0.2898    0.8859    0.3159    0.3899    0.1909    0.7876
      0.6733    0.8680    0.5802    0.1210    0.3715    0.1118    0.8653    0.1292
      0.5724    0.6401    0.6582    0.4260    0.0363    0.7519    0.8446    0.7009
      0.3599    0.7020    0.9019    0.7583    0.1163    0.9359    0.0957    0.1705
      0.8426    0.4900    0.1793    0.4148    0.1649    0.4460    0.4134    0.6314
      0.8844    0.0966    0.5836    0.8782    0.3121    0.3780    0.8076    0.1396
   
   B = 
   
      0.1039    0.2588    0.0624    0.3061    0.1940    0.0635    0.4061    0.1851
      0.2853    0.9551    0.2365    0.2267    0.6069    0.9412    0.8909    0.4052
      0.3577    0.5057    0.5225    0.2297    0.7234    0.7544    0.0470    0.0609
      0.1566    0.1473    0.3385    0.5179    0.0982    0.0147    0.8532    0.8293
      0.2509    0.0990    0.9199    0.9750    0.9690    0.8147    0.3782    0.9539
      0.0957    0.7350    0.9274    0.3157    0.2640    0.7989    0.5558    0.6097
      0.9827    0.1020    0.9862    0.1102    0.8348    0.5755    0.2932    0.5437
      0.7069    0.0613    0.7354    0.7038    0.4839    0.6209    0.3659    0.7220
   
   C = 
   
      1.1286    0.9846    1.8858    1.6230    1.4682    1.6640    1.4886    1.8069
      1.2570    1.6388    2.3697    2.0006    1.9665    2.2180    2.2055    2.3469
      1.4154    1.6629    2.1179    1.8920    1.8966    2.2889    2.4653    2.4259
      1.5895    1.5296    1.9851    1.1826    2.2634    2.2693    1.6808    1.5982
      1.9507    1.8404    2.7543    1.5513    2.2955    2.6931    2.1328    2.2169
      1.0123    2.0511    2.1112    1.4086    1.7450    2.3785    2.1155    1.8915
      1.2930    1.2628    1.8398    1.4165    1.5594    1.7766    1.8035    1.8192
      1.4724    1.1453    2.2170    1.4924    1.8827    1.7080    1.8380    2.0348
   
   D = 
   
      1.1286    0.9846    1.8858    1.6230    1.4682    1.6640    1.4886    1.8069
      1.2570    1.6388    2.3697    2.0006    1.9665    2.2180    2.2055    2.3469
      1.4154    1.6629    2.1179    1.8920    1.8966    2.2889    2.4653    2.4259
      1.5895    1.5296    1.9851    1.1826    2.2634    2.2693    1.6808    1.5982
      1.9507    1.8404    2.7543    1.5513    2.2955    2.6931    2.1328    2.2169
      1.0123    2.0511    2.1112    1.4086    1.7450    2.3785    2.1155    1.8915
      1.2930    1.2628    1.8398    1.4165    1.5594    1.7766    1.8035    1.8192
      1.4724    1.1453    2.2170    1.4924    1.8827    1.7080    1.8380    2.0348
   


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

   
      0.6632    0.5931    0.9873    0.7821    0.5001    0.4149
      0.4948    0.5702    0.9808    0.4229    0.8782    0.9891
      0.6740    0.1322    0.1847    0.6675    0.2745    0.0050
      0.7437    0.9345    0.1987    0.5898    0.5062    0.4820
      0.2072    0.8566    0.3798    0.0631    0.5380    0.8942
   
   
      0.6632
      0.6740
      0.7437
      0.5931
      0.5702
      0.9345
      0.8566
      0.9873
      0.9808
      0.7821
      0.6675
      0.5898
      0.5001
      0.8782
      0.5062
      0.5380
      0.9891
      0.8942
   

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

   
      0.4003    1.1752    3.5005    4.0471    0.9480    2.4492
      1.2284    9.0547    9.7413    4.8083    5.3069    5.1489
      6.6823    6.0542    4.8925    1.7304    0.0261    7.3329
      7.8718    8.5208    8.1218    9.3637    9.2842    8.3186
      8.5886    1.8441    8.5733    7.8499    5.0786    6.8525
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    9.0547    9.7413    0.0000    5.3069    5.1489
      6.6823    6.0542    0.0000    0.0000    0.0000    7.3329
      7.8718    8.5208    8.1218    9.3637    9.2842    8.3186
      8.5886    0.0000    8.5733    7.8499    5.0786    6.8525
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN       NaN    0.0000    5.3069    5.1489
      6.6823    6.0542    0.0000    0.0000    0.0000    7.3329
      7.8718    8.5208    8.1218       NaN       NaN    8.3186
      8.5886    0.0000    8.5733    7.8499    5.0786    6.8525
   

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

   
      0.0237    6.5000    6.5000    6.5000    1.0441    6.5000
      2.5594    9.0566    1.6385    6.5000    6.5000    6.5000
      6.5000    0.7852    9.9576    0.3243    6.5000    8.5446
      6.5000    6.5000    3.8454    0.1329    8.1790    2.0408
      6.5000    1.7950    6.5000    2.2398    4.1822    9.9749
   
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
   
