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
      0.8687    0.0136    0.0278    0.9141
   
   R1[2] = 0.02782010744007468
   C1 = 
      0.5221
      0.7702
      0.0808
      0.9021
      0.2576
      0.0573
      0.9812
      0.2687
   
   C1[5] = 0.05731878727920048

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
      0.9217    0.6960    0.3830    0.2857    0.8188
      0.6117    0.1144    0.0898    0.5655    0.5016
   

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
   
      0.1164    0.9862    0.6618    0.6080    0.5685    0.7336    0.6456    0.8144
      0.8087    0.9669    0.5594    0.7727    0.6914    0.2504    0.2274    0.7609
      0.5719    0.9570    0.2056    0.1767    0.3567    0.9456    0.2724    0.3372
      0.6612    0.5103    0.4875    0.7375    0.4906    0.3796    0.1873    0.2288
      0.1222    0.6874    0.6376    0.4477    0.0874    0.5439    0.9747    0.7774
      0.8166    0.9678    0.7627    0.2866    0.6530    0.7930    0.2290    0.3336
      0.2122    0.2831    0.9705    0.4980    0.2712    0.8875    0.5268    0.1113
      0.2786    0.5658    0.5893    0.4561    0.5090    0.4513    0.9229    0.5459
   
   B = 
   
      0.8900    0.4813    0.0898    0.2123    0.6013    0.2810    0.0062    0.1650
      0.8831    0.5245    0.2622    0.9836    0.9258    0.1815    0.3537    0.7453
      0.0299    0.8449    0.7603    0.6472    0.5156    0.7791    0.0557    0.6645
      0.4156    0.3755    0.3619    0.5933    0.1882    0.7098    0.3361    0.0576
      0.0547    0.1555    0.8748    0.8782    0.8597    0.3641    0.7801    0.7261
      0.6741    0.3357    0.6693    0.9505    0.4866    0.3790    0.9173    0.7359
      0.8180    0.4695    0.5376    0.1076    0.6783    0.7515    0.9660    0.3583
      0.1704    0.9140    0.6953    0.5160    0.7984    0.0941    0.8552    0.9533
   
   C = 
   
      2.4394    2.7429    2.8939    3.4701    3.3725    2.2057    3.0273    3.1893
      2.4338    2.6530    2.4549    3.2056    3.2934    1.9762    2.2773    2.7635
      2.3709    1.8263    1.8484    2.7159    2.5899    1.3447    2.1100    2.3283
      1.8351    1.7754    1.7737    2.3252    2.1764    1.6664    1.5671    1.7766
      2.2221    2.4906    2.3432    2.4802    2.7446    2.0173    2.6035    2.5361
      2.5378    2.4328    2.4677    3.3130    3.2032    1.9445    2.2295    2.8371
      1.7377    1.9467    2.2032    2.4429    2.0951    2.0620    1.9526    2.0645
      2.1347    2.2628    2.4095    2.5246    2.8001    2.0650    2.5574    2.4383
   
   D = 
   
      2.4394    2.7429    2.8939    3.4701    3.3725    2.2057    3.0273    3.1893
      2.4338    2.6530    2.4549    3.2056    3.2934    1.9762    2.2773    2.7635
      2.3709    1.8263    1.8484    2.7159    2.5899    1.3447    2.1100    2.3283
      1.8351    1.7754    1.7737    2.3252    2.1764    1.6664    1.5671    1.7766
      2.2221    2.4906    2.3432    2.4802    2.7446    2.0173    2.6035    2.5361
      2.5378    2.4328    2.4677    3.3130    3.2032    1.9445    2.2295    2.8371
      1.7377    1.9467    2.2032    2.4429    2.0951    2.0620    1.9526    2.0645
      2.1347    2.2628    2.4095    2.5246    2.8001    2.0650    2.5574    2.4383
   


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

   
      0.2685    0.3483    0.8138    0.8492    0.6081    0.7658
      0.7279    0.0797    0.5346    0.3716    0.5269    0.5294
      0.8605    0.0429    0.5889    0.6745    0.2606    0.2298
      0.3301    0.1558    0.9312    0.0817    0.8782    0.8433
      0.8137    0.0656    0.7202    0.0588    0.5992    0.4444
   
   
      0.7279
      0.8605
      0.8137
      0.8138
      0.5346
      0.5889
      0.9312
      0.7202
      0.8492
      0.6745
      0.6081
      0.5269
      0.8782
      0.5992
      0.7658
      0.5294
      0.8433
   

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

   
      6.4053    0.1087    3.0938    2.9178    3.1447    7.4175
      5.8211    4.5789    9.6185    7.6616    3.1329    0.7567
      5.3572    7.2501    4.7527    1.7491    4.0570    4.3431
      4.1911    6.3242    1.4198    0.5925    2.0538    7.6602
      4.4090    6.9353    7.9022    1.7148    4.6106    6.9270
   
   
      6.4053    0.0000    0.0000    0.0000    0.0000    7.4175
      5.8211    0.0000    9.6185    7.6616    0.0000    0.0000
      5.3572    7.2501    0.0000    0.0000    0.0000    0.0000
      0.0000    6.3242    0.0000    0.0000    0.0000    7.6602
      0.0000    6.9353    7.9022    0.0000    0.0000    6.9270
   
   
      6.4053    0.0000    0.0000    0.0000    0.0000    7.4175
      5.8211    0.0000       NaN    7.6616    0.0000    0.0000
      5.3572    7.2501    0.0000    0.0000    0.0000    0.0000
      0.0000    6.3242    0.0000    0.0000    0.0000    7.6602
      0.0000    6.9353    7.9022    0.0000    0.0000    6.9270
   

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

   
      2.5594    3.8452    6.5000    6.5000    8.3826    9.6639
      0.1848    0.7324    3.2488    3.4536    3.4202    6.5000
      9.3664    6.5000    4.4536    9.2886    2.1236    6.5000
      6.5000    1.5364    2.1281    9.5148    6.5000    1.8258
      6.5000    9.7683    3.9532    9.2324    8.3819    3.1276
   
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
   
