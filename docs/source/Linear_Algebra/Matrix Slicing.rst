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
      0.9754    0.8340    0.9871    0.4687
   
   R1[2] = 0.9871003443698994
   C1 = 
      0.0095
      0.5150
      0.5678
      0.6041
      0.6362
      0.8139
      0.7363
      0.2650
   
   C1[5] = 0.813852622444335

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
      0.3384    0.5287    0.5499    0.2491    0.5505
      0.4118    0.6334    0.3347    0.8277    0.3182
   

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
   
      0.9295    0.5358    0.4910    0.2924    0.4182    0.5935    0.3001    0.1256
      0.7423    0.4467    0.2342    0.9008    0.6799    0.4417    0.7706    0.7585
      0.3335    0.9183    0.4125    0.2022    0.1488    0.2042    0.7940    0.2067
      0.3688    0.4429    0.1504    0.4502    0.1694    0.9268    0.9978    0.8173
      0.4003    0.4743    0.2689    0.4685    0.4837    0.1535    0.1259    0.9350
      0.1387    0.3229    0.0091    0.1045    0.6895    0.2777    0.1410    0.6940
      0.0860    0.5682    0.6203    0.4056    0.1057    0.4501    0.3838    0.4391
      0.7181    0.3736    0.7354    0.7282    0.4674    0.2463    0.1890    0.8421
   
   B = 
   
      0.6499    0.3010    0.3827    0.8236    0.5168    0.4244    0.4151    0.5979
      0.8591    0.5153    0.7038    0.6231    0.7518    0.3841    0.4455    0.8035
      0.0954    0.8017    0.3274    0.0896    0.9010    0.5474    0.8360    0.8905
      0.2401    0.5580    0.0821    0.2287    0.5485    0.0661    0.0780    0.2726
      0.6179    0.4983    0.3479    0.0724    0.5962    0.5713    0.7644    0.9716
      0.1154    0.3319    0.8509    0.9476    0.5969    0.0635    0.6935    0.3803
      0.0264    0.4352    0.3147    0.9525    0.7763    0.5930    0.0555    0.8803
      0.2764    0.1458    0.1668    0.2337    0.8069    0.2958    0.6368    0.8785
   
   C = 
   
      1.5509    1.6669    1.6834    2.1180    2.4239    1.3801    1.8858    2.5098
      1.8058    2.0754    1.7304    2.4956    3.3038    1.7721    2.1250    3.4302
      1.2872    1.5347    1.4354    1.9389    2.3391    1.3633    1.3392    2.4625
      1.2065    1.6565    1.8369    2.7281    2.9944    1.4278    1.8593    2.9465
      1.3841    1.3250    1.1079    1.2753    2.2949    1.1675    1.7174    2.4482
      1.0471    0.8720    0.9281    0.9496    1.6262    0.8951    1.3865    1.8882
      0.9494    1.4755    1.2830    1.4757    2.2371    1.0676    1.5329    2.1685
      1.5876    1.9243    1.4104    1.7008    2.9661    1.5428    2.2110    3.0371
   
   D = 
   
      1.5509    1.6669    1.6834    2.1180    2.4239    1.3801    1.8858    2.5098
      1.8058    2.0754    1.7304    2.4956    3.3038    1.7721    2.1250    3.4302
      1.2872    1.5347    1.4354    1.9389    2.3391    1.3633    1.3392    2.4625
      1.2065    1.6565    1.8369    2.7281    2.9944    1.4278    1.8593    2.9465
      1.3841    1.3250    1.1079    1.2753    2.2949    1.1675    1.7174    2.4482
      1.0471    0.8720    0.9281    0.9496    1.6262    0.8951    1.3865    1.8882
      0.9494    1.4755    1.2830    1.4757    2.2371    1.0676    1.5329    2.1685
      1.5876    1.9243    1.4104    1.7008    2.9661    1.5428    2.2110    3.0371
   


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

   
      0.9809    0.1818    0.1008    0.7003    0.8433    0.4283
      0.7262    0.7813    0.3724    0.7952    0.3004    0.5412
      0.0092    0.1775    0.5410    0.6984    0.5379    0.9646
      0.6360    0.0096    0.7075    0.2891    0.7374    0.3583
      0.8237    0.7687    0.0455    0.9187    0.1376    0.1551
   
   
      0.9809
      0.7262
      0.6360
      0.8237
      0.7813
      0.7687
      0.5410
      0.7075
      0.7003
      0.7952
      0.6984
      0.9187
      0.8433
      0.5379
      0.7374
      0.5412
      0.9646
   

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

   
      9.2651    3.3318    2.2990    9.4557    5.7522    3.9116
      8.3983    7.7524    5.6784    3.3511    0.0363    4.3370
      9.9744    7.6600    4.3169    1.2863    6.2114    6.0082
      5.3515    3.1345    5.7221    7.7793    0.0336    3.1671
      0.3468    3.5890    7.3332    0.1445    1.3931    9.7182
   
   
      9.2651    0.0000    0.0000    9.4557    5.7522    0.0000
      8.3983    7.7524    5.6784    0.0000    0.0000    0.0000
      9.9744    7.6600    0.0000    0.0000    6.2114    6.0082
      5.3515    0.0000    5.7221    7.7793    0.0000    0.0000
      0.0000    0.0000    7.3332    0.0000    0.0000    9.7182
   
   
         NaN    0.0000    0.0000       NaN    5.7522    0.0000
      8.3983    7.7524    5.6784    0.0000    0.0000    0.0000
         NaN    7.6600    0.0000    0.0000    6.2114    6.0082
      5.3515    0.0000    5.7221    7.7793    0.0000    0.0000
      0.0000    0.0000    7.3332    0.0000    0.0000       NaN
   

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

   
      3.0164    2.2538    4.6489    6.5000    4.8478    6.5000
      4.2371    8.7998    2.9648    0.7413    3.9297    9.5374
      2.3281    3.3093    0.8448    6.5000    1.6359    4.5028
      1.4707    1.3851    1.8793    6.5000    6.5000    6.5000
      6.5000    2.5298    3.5661    1.7697    9.6637    6.5000
   
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
   
