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
      0.2021    0.8622    0.8899    0.0243
   
   R1[2] = 0.8898564279742354
   C1 = 
      0.5317
      0.6633
      0.9177
      0.8574
      0.8678
      0.3124
      0.5731
      0.8443
   
   C1[5] = 0.3124105001519496

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
      0.9933    0.1450    0.4367    0.2743    0.5062
      0.3918    0.1089    0.9611    0.2990    0.7636
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.3703    0.2159    0.8163    0.7132    0.5016    0.5220    0.1840    0.3493
      0.3393    0.4591    0.3960    0.3967    0.4481    0.2851    0.4324    0.1260
      0.5450    0.0008    0.9420    0.9902    0.9725    0.4963    0.7931    0.8394
      0.4797    0.8107    0.1531    0.1700    0.4126    0.0825    0.4215    0.6504
      0.5232    0.2873    0.3766    0.9745    0.7152    0.8724    0.3849    0.7336
      0.3991    0.7240    0.6584    0.4086    0.5705    0.6175    0.8525    0.5813
      0.2343    0.8534    0.2620    0.5757    0.4582    0.8744    0.1115    0.7918
      0.5351    0.4823    0.3075    0.7248    0.3831    0.6953    0.9888    0.5323
   
   B = 
   
      0.6732    0.0411    0.2735    0.4520    0.3400    0.1128    0.1372    0.0389
      0.1109    0.5958    0.5319    0.8459    0.6994    0.6455    0.1654    0.0270
      0.8562    0.9215    0.9481    0.5645    0.4534    0.7443    0.3437    0.8223
      0.7586    0.6733    0.0488    0.1576    0.2680    0.7720    0.3029    0.6246
      0.7009    0.0488    0.7920    0.8211    0.7956    0.9892    0.0261    0.9871
      0.6692    0.3941    0.6847    0.4968    0.4397    0.2985    0.1421    0.5180
      0.0490    0.8228    0.7508    0.6956    0.8285    0.4031    0.2678    0.3718
      0.2381    0.6360    0.8223    0.8657    0.6397    0.3155    0.3470    0.8246
   
   C = 
   
      2.3063    1.9801    2.2049    2.0247    1.8427    2.1756    0.8409    2.2589
      1.4754    1.4897    1.7103    1.7472    1.6431    1.6780    0.5905    1.4537
      3.1773    2.9872    3.4868    3.2583    3.0645    3.2222    1.2983    3.6184
      1.1929    1.5715    1.9506    2.2519    1.9750    1.6305    0.6651    1.4157
      2.7244    2.3579    2.7565    2.7690    2.5516    2.6315    1.0442    2.8523
      2.2160    2.6720    3.1312    3.1003    2.8537    2.5935    1.0572    2.5110
      2.0137    2.1095    2.4909    2.6400    2.2976    2.2257    0.8786    2.2068
      2.1358    2.5257    2.6895    2.7462    2.6232    2.3130    1.0368    2.2843
   
   D = 
   
      2.3063    1.9801    2.2049    2.0247    1.8427    2.1756    0.8409    2.2589
      1.4754    1.4897    1.7103    1.7472    1.6431    1.6780    0.5905    1.4537
      3.1773    2.9872    3.4868    3.2583    3.0645    3.2222    1.2983    3.6184
      1.1929    1.5715    1.9506    2.2519    1.9750    1.6305    0.6651    1.4157
      2.7244    2.3579    2.7565    2.7690    2.5516    2.6315    1.0442    2.8523
      2.2160    2.6720    3.1312    3.1003    2.8537    2.5935    1.0572    2.5110
      2.0137    2.1095    2.4909    2.6400    2.2976    2.2257    0.8786    2.2068
      2.1358    2.5257    2.6895    2.7462    2.6232    2.3130    1.0368    2.2843
   


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

   
      0.8344    0.8030    0.8980    0.2747    0.8316    0.5083
      0.8115    0.5944    0.0205    0.6815    0.2045    0.9504
      0.5979    0.1154    0.1475    0.3537    0.3959    0.4395
      0.7068    0.0307    0.1272    0.2674    0.1654    0.6855
      0.4896    0.0014    0.3636    0.9373    0.1222    0.9699
   
   
      0.8344
      0.8115
      0.5979
      0.7068
      0.8030
      0.5944
      0.8980
      0.6815
      0.9373
      0.8316
      0.5083
      0.9504
      0.6855
      0.9699
   

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

   
      8.4695    9.7201    4.7078    6.3300    7.8464    2.0908
      6.7471    7.2997    1.4393    7.7443    9.4064    9.9312
      8.7766    7.1877    2.2399    3.2876    4.5616    6.7043
      6.6365    0.2608    0.9162    2.3362    0.7952    6.3521
      8.6209    6.1129    3.2982    8.9655    4.0607    0.3526
   
   
      8.4695    9.7201    0.0000    6.3300    7.8464    0.0000
      6.7471    7.2997    0.0000    7.7443    9.4064    9.9312
      8.7766    7.1877    0.0000    0.0000    0.0000    6.7043
      6.6365    0.0000    0.0000    0.0000    0.0000    6.3521
      8.6209    6.1129    0.0000    8.9655    0.0000    0.0000
   
   
      8.4695       NaN    0.0000    6.3300    7.8464    0.0000
      6.7471    7.2997    0.0000    7.7443       NaN       NaN
      8.7766    7.1877    0.0000    0.0000    0.0000    6.7043
      6.6365    0.0000    0.0000    0.0000    0.0000    6.3521
      8.6209    6.1129    0.0000    8.9655    0.0000    0.0000
   

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

   
      0.9524    6.5000    9.6008    0.8479    2.3480    8.5476
      9.0526    6.5000    4.7191    9.2084    6.5000    8.6516
      6.5000    3.1161    2.4447    2.0580    9.7699    9.0423
      6.5000    8.7017    2.3197    1.6918    4.0051    2.8536
      6.5000    3.7562    3.5385    0.6433    6.5000    8.8926
   
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
   
