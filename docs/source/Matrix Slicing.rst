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
      0.9592    0.1090    0.4302    0.1560
   
   R1[2] = 0.43021228938242684
   C1 = 
      0.0178
      0.9682
      0.9927
      0.6542
      0.8536
      0.9931
      0.8000
      0.4141
   
   C1[5] = 0.9930828769930773

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
      0.3830    0.4107    0.7117    0.1614    0.9614
      0.8375    0.4130    0.8720    0.8783    0.8658
   

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
   
      0.1833    0.9484    0.8434    0.1672    0.6726    0.2947    0.0449    0.0891
      0.3732    0.0236    0.2514    0.6020    0.5048    0.2282    0.0188    0.1097
      0.1538    0.5950    0.0833    0.0058    0.7489    0.6852    0.0928    0.2410
      0.9366    0.2829    0.6314    0.5893    0.3818    0.6145    0.2927    0.5063
      0.2560    0.0929    0.2386    0.8380    0.1193    0.3730    0.9596    0.2118
      0.8830    0.0410    0.9526    0.7869    0.2932    0.8001    0.4953    0.9342
      0.9197    0.9776    0.4449    0.9528    0.8089    0.6304    0.4342    0.6281
      0.5821    0.8564    0.6048    0.6176    0.6124    0.2116    0.8628    0.8690
   
   B = 
   
      0.2110    0.5482    0.4700    0.8952    0.8688    0.9936    0.9274    0.7604
      0.9668    0.9729    0.5615    0.6054    0.7104    0.1274    0.4364    0.0395
      0.2701    0.9593    0.0371    0.9522    0.1348    0.9384    0.6582    0.2826
      0.9402    0.8840    0.5660    0.2052    0.4942    0.2876    0.9553    0.1091
      0.3059    0.5808    0.7444    0.6819    0.5313    0.7033    0.0618    0.5555
      0.6099    0.6825    0.6277    0.6390    0.5997    0.9897    0.2067    0.2744
      0.8137    0.7811    0.8327    0.4537    0.6021    0.1547    0.8078    0.9924
      0.8418    0.1306    0.3958    0.7575    0.1332    0.5392    0.6503    0.9053
   
   C = 
   
      1.8377    2.6185    1.5030    2.3106    1.6023    1.9621    1.4953    1.0131
      1.1366    1.4786    1.1167    1.2929    1.1032    1.4257    1.2617    0.8823
      1.5611    1.7548    1.5730    1.7516    1.4671    1.6576    0.8822    1.0788
      2.3517    2.8512    2.0701    2.9012    2.2060    2.9236    2.6868    2.0956
      2.2193    2.3015    1.8614    1.6001    1.6276    1.4468    2.2328    1.6698
      2.9903    3.3586    2.4214    3.5274    2.3717    3.5812    3.4065    2.7478
      3.6693    4.0456    3.1450    3.6616    3.1772    3.3285    3.4220    2.5894
      3.4449    3.5661    2.7776    3.3448    2.5883    2.6748    3.2453    2.7559
   
   D = 
   
      1.8377    2.6185    1.5030    2.3106    1.6023    1.9621    1.4953    1.0131
      1.1366    1.4786    1.1167    1.2929    1.1032    1.4257    1.2617    0.8823
      1.5611    1.7548    1.5730    1.7516    1.4671    1.6576    0.8822    1.0788
      2.3517    2.8512    2.0701    2.9012    2.2060    2.9236    2.6868    2.0956
      2.2193    2.3015    1.8614    1.6001    1.6276    1.4468    2.2328    1.6698
      2.9903    3.3586    2.4214    3.5274    2.3717    3.5812    3.4065    2.7478
      3.6693    4.0456    3.1450    3.6616    3.1772    3.3285    3.4220    2.5894
      3.4449    3.5661    2.7776    3.3448    2.5883    2.6748    3.2453    2.7559
   


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

   
      0.4992    0.6676    0.9135    0.8387    0.5594    0.3197
      0.1739    0.0594    0.2405    0.4713    0.6649    0.6908
      0.5096    0.6276    0.2659    0.2108    0.9877    0.8465
      0.5801    0.1879    0.2163    0.8777    0.2751    0.1942
      0.8238    0.5917    0.2757    0.8235    0.3770    0.2637
   
   
      0.5096
      0.5801
      0.8238
      0.6676
      0.6276
      0.5917
      0.9135
      0.8387
      0.8777
      0.8235
      0.5594
      0.6649
      0.9877
      0.6908
      0.8465
   

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

   
      9.3868    1.9306    9.3912    5.0101    9.4830    6.0111
      7.2473    2.8165    1.7456    5.5013    6.8638    4.1755
      8.4428    4.9120    9.7331    4.3377    6.6096    6.2656
      2.5960    1.4325    8.0630    0.2007    1.2167    5.7596
      1.0763    3.2403    7.8030    4.7637    5.8058    4.2285
   
   
      9.3868    0.0000    9.3912    5.0101    9.4830    6.0111
      7.2473    0.0000    0.0000    5.5013    6.8638    0.0000
      8.4428    0.0000    9.7331    0.0000    6.6096    6.2656
      0.0000    0.0000    8.0630    0.0000    0.0000    5.7596
      0.0000    0.0000    7.8030    0.0000    5.8058    0.0000
   
   
         NaN    0.0000       NaN    5.0101       NaN    6.0111
      7.2473    0.0000    0.0000    5.5013    6.8638    0.0000
      8.4428    0.0000       NaN    0.0000    6.6096    6.2656
      0.0000    0.0000    8.0630    0.0000    0.0000    5.7596
      0.0000    0.0000    7.8030    0.0000    5.8058    0.0000
   

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

   
      3.7752    9.8723    0.9188    3.5913    6.5000    8.6199
      4.9923    6.5000    9.6569    3.0132    6.5000    9.9584
      0.0895    1.7325    1.9912    2.1251    6.5000    6.5000
      6.5000    0.1389    6.5000    8.5625    4.0021    9.1396
      1.0061    0.0554    9.0717    6.5000    6.5000    4.8316
   
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
   
