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
      0.8045    0.2765    0.8030    0.0713
   
   R1[2] = 0.8030017641531733
   C1 = 
      0.4791
      0.8427
      0.9148
      0.2511
      0.7383
      0.3820
      0.4658
      0.8977
   
   C1[5] = 0.3820438989880226

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
      0.3386    0.3127    0.3439    0.7998    0.0660
      0.4957    0.3886    0.4111    0.8242    0.4339
   

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
   
      0.6730    0.8189    0.9743    0.1614    0.5260    0.3992    0.5423    0.6598
      0.4845    0.8196    0.2243    0.8156    0.3164    0.3512    0.1424    0.2258
      0.8476    0.3049    0.2507    0.0902    0.7444    0.6948    0.8849    0.3608
      0.5207    0.4771    0.7727    0.3826    0.7620    0.1448    0.5493    0.5834
      0.9760    0.8196    0.4149    0.7622    0.0233    0.1371    0.5563    0.1561
      0.5204    0.3814    0.1617    0.6934    0.9897    0.4472    0.8420    0.4184
      0.6274    0.8320    0.1229    0.2211    0.2879    0.2846    0.6127    0.4672
      0.5113    0.7309    0.8216    0.6786    0.0398    0.4213    0.1197    0.1518
   
   B = 
   
      0.7571    0.0421    0.0555    0.5596    0.8414    0.3426    0.7644    0.7208
      0.4126    0.5822    0.3724    0.6793    0.1534    0.9239    0.8803    0.5459
      0.6922    0.2537    0.3639    0.7424    0.7295    0.4267    0.1581    0.3008
      0.3621    0.1327    0.3911    0.8592    0.7740    0.2364    0.2453    0.8310
      0.4006    0.8630    0.4206    0.4762    0.0127    0.1427    0.0444    0.3983
      0.9639    0.3447    0.8507    0.9966    0.8672    0.0680    0.0094    0.8775
      0.8358    0.4989    0.6247    0.0945    0.9681    0.1962    0.7934    0.1833
      0.6148    0.2345    0.4351    0.9421    0.9336    0.2920    0.0789    0.7933
   
   C = 
   
      3.0346    1.7906    1.9467    3.1160    3.0213    1.8423    1.9385    2.5420
      1.8787    1.1809    1.3517    2.4220    1.9854    1.3747    1.4755    2.1814
      2.9031    1.6969    1.9011    2.4155    2.8181    1.1327    1.7483    2.2824
      2.5270    1.6649    1.6781    2.6265    2.5827    1.4360    1.5511    2.1802
      2.3426    1.1062    1.3503    2.4132    2.6429    1.6162    2.1762    2.2646
      2.7029    1.9035    2.0057    2.6567    2.7571    1.2225    1.6784    2.4810
      2.1724    1.3332    1.4251    2.1162    2.1960    1.4053    1.8241    1.9745
      2.1185    1.0205    1.3809    2.5687    2.2902    1.4635    1.4433    2.1065
   
   D = 
   
      3.0346    1.7906    1.9467    3.1160    3.0213    1.8423    1.9385    2.5420
      1.8787    1.1809    1.3517    2.4220    1.9854    1.3747    1.4755    2.1814
      2.9031    1.6969    1.9011    2.4155    2.8181    1.1327    1.7483    2.2824
      2.5270    1.6649    1.6781    2.6265    2.5827    1.4360    1.5511    2.1802
      2.3426    1.1062    1.3503    2.4132    2.6429    1.6162    2.1762    2.2646
      2.7029    1.9035    2.0057    2.6567    2.7571    1.2225    1.6784    2.4810
      2.1724    1.3332    1.4251    2.1162    2.1960    1.4053    1.8241    1.9745
      2.1185    1.0205    1.3809    2.5687    2.2902    1.4635    1.4433    2.1065
   


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

   
      0.5171    0.4639    0.0678    0.8538    0.8367    0.0354
      0.3891    0.2953    0.4675    0.7577    0.9183    0.9784
      0.9944    0.5290    0.6413    0.8853    0.4618    0.3506
      0.8501    0.8038    0.1692    0.7582    0.2213    0.2787
      0.2980    0.4842    0.4121    0.1440    0.0969    0.0555
   
   
      0.5171
      0.9944
      0.8501
      0.5290
      0.8038
      0.6413
      0.8538
      0.7577
      0.8853
      0.7582
      0.8367
      0.9183
      0.9784
   

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

   
      7.2908    5.3535    6.8967    0.4860    1.9661    9.6425
      9.1065    9.6363    0.7562    8.2159    3.2504    1.2770
      8.6352    2.6251    8.6393    2.1714    7.9367    0.0195
      0.3275    1.7994    7.5747    5.4697    4.4825    9.0416
      8.0229    5.0789    0.9899    8.3817    9.1599    3.6504
   
   
      7.2908    5.3535    6.8967    0.0000    0.0000    9.6425
      9.1065    9.6363    0.0000    8.2159    0.0000    0.0000
      8.6352    0.0000    8.6393    0.0000    7.9367    0.0000
      0.0000    0.0000    7.5747    5.4697    0.0000    9.0416
      8.0229    5.0789    0.0000    8.3817    9.1599    0.0000
   
   
      7.2908    5.3535    6.8967    0.0000    0.0000       NaN
         NaN       NaN    0.0000    8.2159    0.0000    0.0000
      8.6352    0.0000    8.6393    0.0000    7.9367    0.0000
      0.0000    0.0000    7.5747    5.4697    0.0000       NaN
      8.0229    5.0789    0.0000    8.3817       NaN    0.0000
   

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

   
      8.3607    6.5000    2.4672    9.8477    6.5000    8.7899
      1.1470    9.5444    6.5000    6.5000    4.9754    9.4630
      4.4691    2.6599    6.5000    6.5000    6.5000    1.4724
      1.5875    9.9465    6.5000    2.6716    6.5000    0.6046
      2.5390    3.7570    6.5000    0.6949    3.5871    0.6769
   
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
   
