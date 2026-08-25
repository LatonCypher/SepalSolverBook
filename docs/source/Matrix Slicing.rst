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
      0.6429    0.5873    0.0330    0.5410
   
   R1[2] = 0.03297095574353337
   C1 = 
      0.9681
      0.3174
      0.7500
      0.3780
      0.6497
      0.0971
      0.7473
      0.7374
   
   C1[5] = 0.09709867535788053

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
      0.9466    0.9542    0.1384    0.5348    0.5603
      0.9441    0.0868    0.9712    0.1417    0.9876
   

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
   
      0.3284    0.0640    0.7972    0.7104    0.8877    0.3495    0.6033    0.5170
      0.6243    0.7767    0.0999    0.8862    0.6270    0.1609    0.0597    0.7842
      0.9310    0.9927    0.6061    0.8088    0.5084    0.3752    0.9206    0.4246
      0.8741    0.5739    0.4809    0.9920    0.8520    0.5101    0.9732    0.1901
      0.8232    0.8923    0.3169    0.6006    0.7986    0.3163    0.8649    0.3189
      0.7448    0.3753    0.7075    0.9959    0.1068    0.3316    0.3473    0.1528
      0.1571    0.7443    0.7635    0.0218    0.3167    0.8196    0.6367    0.0282
      0.3514    0.7619    0.4512    0.1394    0.5027    0.2125    0.5237    0.2987
   
   B = 
   
      0.6468    0.0901    0.0648    0.3618    0.2597    0.4792    0.4206    0.6420
      0.4959    0.7846    0.4977    0.1769    0.3076    0.5150    0.4453    0.4288
      0.8396    0.4834    0.4268    0.7348    0.1086    0.1553    0.1071    0.3911
      0.1331    0.1868    0.5624    0.3647    0.4913    0.7722    0.3824    0.0851
      0.1107    0.0220    0.5593    0.0374    0.3813    0.0405    0.6976    0.8856
      0.2236    0.9717    0.1923    0.7054    0.6272    0.1418    0.0571    0.0194
      0.6430    0.8171    0.7752    0.4023    0.7916    0.6483    0.5613    0.3517
      0.6516    0.4615    0.0338    0.8144    0.1433    0.1714    0.7777    0.6391
   
   C = 
   
      1.9093    1.6886    1.8418    1.9185    1.6500    1.4280    1.9036    1.9462
      1.6456    1.4603    1.4225    1.5596    1.3469    1.6203    2.0479    1.9289
      2.7198    2.6307    2.3524    2.2527    2.2291    2.4194    2.4307    2.3820
      2.3439    2.3442    2.4409    2.0711    2.3856    2.3256    2.3716    2.3081
      2.2441    2.2185    2.1592    1.7684    2.0510    2.0596    2.3157    2.3076
      1.8034    1.5685    1.4950    1.7211    1.4205    1.7318    1.3442    1.3214
      1.7608    2.3080    1.5480    1.6267    1.5063    1.1407    1.1346    1.2589
      1.6371    1.6569    1.4111    1.2670    1.2255    1.1797    1.4777    1.5651
   
   D = 
   
      1.9093    1.6886    1.8418    1.9185    1.6500    1.4280    1.9036    1.9462
      1.6456    1.4603    1.4225    1.5596    1.3469    1.6203    2.0479    1.9289
      2.7198    2.6307    2.3524    2.2527    2.2291    2.4194    2.4307    2.3820
      2.3439    2.3442    2.4409    2.0711    2.3856    2.3256    2.3716    2.3081
      2.2441    2.2185    2.1592    1.7684    2.0510    2.0596    2.3157    2.3076
      1.8034    1.5685    1.4950    1.7211    1.4205    1.7318    1.3442    1.3214
      1.7608    2.3080    1.5480    1.6267    1.5063    1.1407    1.1346    1.2589
      1.6371    1.6569    1.4111    1.2670    1.2255    1.1797    1.4777    1.5651
   


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

   
      0.8802    0.0323    0.6241    0.8978    0.9885    0.3048
      0.7091    0.9522    0.3024    0.2742    0.7776    0.9636
      0.2192    0.3046    0.4315    0.8873    0.4923    0.1426
      0.2265    0.9498    0.0055    0.2513    0.2890    0.3819
      0.4990    0.6347    0.9975    0.1709    0.3954    0.2239
   
   
      0.8802
      0.7091
      0.9522
      0.9498
      0.6347
      0.6241
      0.9975
      0.8978
      0.8873
      0.9885
      0.7776
      0.9636
   

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

   
      1.1627    6.3595    9.2826    0.7274    4.8880    2.5143
      2.7143    2.7355    1.5021    9.2209    7.1249    9.7887
      9.2334    9.9457    0.6524    0.7107    8.4066    4.4029
      5.0048    7.8599    2.4531    0.3689    8.0287    7.9595
      0.5270    5.1319    2.5092    3.7237    5.8998    0.6140
   
   
      0.0000    6.3595    9.2826    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    9.2209    7.1249    9.7887
      9.2334    9.9457    0.0000    0.0000    8.4066    0.0000
      5.0048    7.8599    0.0000    0.0000    8.0287    7.9595
      0.0000    5.1319    0.0000    0.0000    5.8998    0.0000
   
   
      0.0000    6.3595       NaN    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000       NaN    7.1249       NaN
         NaN       NaN    0.0000    0.0000    8.4066    0.0000
      5.0048    7.8599    0.0000    0.0000    8.0287    7.9595
      0.0000    5.1319    0.0000    0.0000    5.8998    0.0000
   

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

   
      4.0304    8.9479    3.5502    6.5000    6.5000    0.8694
      6.5000    8.5367    0.9844    4.4209    1.2278    6.5000
      9.4595    6.5000    9.4116    0.6593    8.5084    6.5000
      0.3045    9.3271    6.5000    6.5000    4.3119    6.5000
      1.6240    0.1332    6.5000    9.9744    3.3282    3.7860
   
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
   
