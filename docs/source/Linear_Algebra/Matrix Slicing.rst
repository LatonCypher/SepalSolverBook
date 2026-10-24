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
      0.5793    0.8094    0.6800    0.9798
   
   R1[2] = 0.6800120585384601
   C1 = 
      0.4296
      0.4836
      0.3675
      0.4457
      0.5594
      0.6701
      0.8492
      0.8964
   
   C1[5] = 0.6701084708591769

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
      0.8000    0.1754    0.5007    0.9985    0.9660
      0.9117    0.3438    0.8796    0.0872    0.4754
   

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
   
      0.9366    0.0457    0.3205    0.9223    0.6195    0.0527    0.7793    0.1599
      0.0204    0.9884    0.7779    0.8532    0.2745    0.2477    0.2579    0.4650
      0.3640    0.2025    0.6685    0.8121    0.9484    0.4779    0.7225    0.8990
      0.9131    0.5668    0.1283    0.0342    0.8116    0.4639    0.0197    0.2517
      0.9312    0.7520    0.6916    0.2562    0.9239    0.9251    0.2793    0.6902
      0.9716    0.6832    0.7309    0.7666    0.5455    0.2737    0.9642    0.9555
      0.1531    0.0095    0.8073    0.1763    0.2073    0.2736    0.2225    0.8767
      0.4201    0.9402    0.7497    0.8749    0.8007    0.6800    0.9903    0.5253
   
   B = 
   
      0.5465    0.9031    0.6092    0.8041    0.4517    0.2549    0.9442    0.5250
      0.8613    0.7671    0.4692    0.7905    0.8074    0.4218    0.4450    0.8138
      0.8208    0.4165    0.8484    0.4998    0.1200    0.4405    0.7147    0.8955
      0.4483    0.7483    0.4812    0.9081    0.6139    0.6983    0.3480    0.4707
      0.5516    0.1095    0.9070    0.6136    0.9348    0.7079    0.1775    0.9786
      0.8337    0.3734    0.7922    0.5446    0.9444    0.3725    0.1847    0.9689
      0.6455    0.1305    0.4658    0.8593    0.9696    0.5541    0.1674    0.2615
      0.9596    0.8487    0.6355    0.7401    0.6577    0.7941    0.3966    0.3250
   
   C = 
   
      2.2697    2.0293    2.3759    2.9837    2.5542    2.0601    1.7683    2.1632
      2.8540    2.2899    2.4077    2.8305    2.4709    2.1594    1.6341    2.6407
      3.5366    2.5097    3.4214    3.6527    3.5363    3.0034    1.9283    3.2092
      2.1965    1.8167    2.2203    2.2312    2.2879    1.5103    1.5508    2.4023
      3.9625    2.9664    3.7698    3.7430    3.7301    2.7396    2.4527    3.9387
      4.1314    3.3783    3.6695    4.4023    3.8806    3.1743    2.6984    3.4433
      2.1608    1.5116    1.9332    1.8104    1.5265    1.5899    1.2594    1.7053
      4.1987    2.9841    3.8142    4.3515    4.2722    3.2308    2.2972    3.9411
   
   D = 
   
      2.2697    2.0293    2.3759    2.9837    2.5542    2.0601    1.7683    2.1632
      2.8540    2.2899    2.4077    2.8305    2.4709    2.1594    1.6341    2.6407
      3.5366    2.5097    3.4214    3.6527    3.5363    3.0034    1.9283    3.2092
      2.1965    1.8167    2.2203    2.2312    2.2879    1.5103    1.5508    2.4023
      3.9625    2.9664    3.7698    3.7430    3.7301    2.7396    2.4527    3.9387
      4.1314    3.3783    3.6695    4.4023    3.8806    3.1743    2.6984    3.4433
      2.1608    1.5116    1.9332    1.8104    1.5265    1.5899    1.2594    1.7053
      4.1987    2.9841    3.8142    4.3515    4.2722    3.2308    2.2972    3.9411
   


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

   
      0.2310    0.0953    0.0522    0.2399    0.8612    0.3127
      0.1720    0.5887    0.6950    0.4332    0.3617    0.9952
      0.8032    0.9244    0.4851    0.5864    0.3018    0.8284
      0.7356    0.5293    0.3026    0.4925    0.6911    0.1639
      0.7125    0.2851    0.5280    0.2478    0.8380    0.0792
   
   
      0.8032
      0.7356
      0.7125
      0.5887
      0.9244
      0.5293
      0.6950
      0.5280
      0.5864
      0.8612
      0.6911
      0.8380
      0.9952
      0.8284
   

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

   
      5.4771    8.0175    6.0580    7.4054    9.5807    5.4350
      6.6006    0.0848    7.4219    9.5467    3.0932    0.1904
      6.5920    8.5956    2.5881    1.7356    3.4687    8.7368
      0.4178    8.0863    1.1776    0.2195    8.1173    7.1048
      8.1578    1.8912    1.5429    4.7660    8.0762    3.3955
   
   
      5.4771    8.0175    6.0580    7.4054    9.5807    5.4350
      6.6006    0.0000    7.4219    9.5467    0.0000    0.0000
      6.5920    8.5956    0.0000    0.0000    0.0000    8.7368
      0.0000    8.0863    0.0000    0.0000    8.1173    7.1048
      8.1578    0.0000    0.0000    0.0000    8.0762    0.0000
   
   
      5.4771    8.0175    6.0580    7.4054       NaN    5.4350
      6.6006    0.0000    7.4219       NaN    0.0000    0.0000
      6.5920    8.5956    0.0000    0.0000    0.0000    8.7368
      0.0000    8.0863    0.0000    0.0000    8.1173    7.1048
      8.1578    0.0000    0.0000    0.0000    8.0762    0.0000
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      9.6064    6.5000    0.1248    6.5000    8.1650    1.3675
      2.7110    9.1126    4.5363    1.7569    2.3478    6.5000
      8.7120    1.3540    6.5000    6.5000    8.5403    9.1824
      6.5000    3.6099    6.5000    1.5753    4.5976    6.5000
      6.5000    9.1729    1.9751    6.5000    3.0385    0.1229
   
Advantages
^^^^^^^^^^


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
   
