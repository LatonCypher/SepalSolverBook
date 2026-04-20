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
      0.5901    0.4933    0.7368    0.9069
   
   R1[2] = 0.736844074957812
   C1 = 
      0.5681
      0.2198
      0.5824
      0.3128
      0.3860
      0.9967
      0.5573
      0.7794
   
   C1[5] = 0.9967294051030927

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
      0.4385    0.6144    0.2345    0.4479    0.1692
      0.0174    0.3267    0.7269    0.0939    0.1747
   

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
   
      0.0737    0.8594    0.9251    0.5430    0.3736    0.9469    0.8720    0.1202
      0.2358    0.7869    0.0580    0.8417    0.6940    0.5730    0.7869    0.5451
      0.9361    0.8947    0.5482    0.7918    0.7317    0.5854    0.1493    0.1978
      0.7260    0.8195    0.9266    0.9364    0.1415    0.4523    0.7055    0.6129
      0.8489    0.4821    0.3227    0.5417    0.5721    0.5238    0.4295    0.3122
      0.2813    0.3466    0.1022    0.5449    0.7894    0.8474    0.7457    0.6512
      0.7698    0.3317    0.8738    0.8984    0.2161    0.3317    0.8809    0.7029
      0.5434    0.5769    0.6613    0.5955    0.5286    0.2367    0.3523    0.3742
   
   B = 
   
      0.6654    0.1256    0.9659    0.1002    0.3923    0.2803    0.6831    0.8385
      0.3248    0.6720    0.9933    0.9293    0.7348    0.0672    0.6755    0.6836
      0.9343    0.1135    0.3389    0.1458    0.4181    0.1399    0.7102    0.6170
      0.8217    0.7179    0.3360    0.8565    0.8741    0.6690    0.4157    0.3141
      0.9954    0.0162    0.3860    0.0440    0.0769    0.5755    0.3731    0.5316
      0.1743    0.1158    0.0117    0.9765    0.1990    0.2232    0.3817    0.7685
      0.2353    0.7640    0.0018    0.9536    0.9207    0.2860    0.2064    0.9301
      0.9034    0.6951    0.5219    0.5346    0.5394    0.7008    0.2678    0.4433
   
   C = 
   
      2.4894    1.9470    1.6404    3.2428    2.6067    1.3311    2.2266    3.1814
      2.6265    2.2269    1.8723    3.1161    2.6166    1.8245    1.8697    2.8187
      3.1206    1.6807    2.6376    2.5354    2.3630    1.6620    2.5427    3.0490
      3.3239    2.4390    2.5250    3.2197    3.1740    1.8284    2.6321    3.3856
      2.5121    1.4712    1.9810    2.1573    2.0079    1.4657    1.9457    2.6553
      2.5402    1.8044    1.4895    2.7534    2.1513    1.7941    1.6717    2.7603
      3.2897    2.2672    2.1267    2.8315    2.9692    1.9042    2.3213    3.1943
      2.6446    1.5237    1.9249    1.9874    2.0480    1.4021    1.9387    2.4016
   
   D = 
   
      2.4894    1.9470    1.6404    3.2428    2.6067    1.3311    2.2266    3.1814
      2.6265    2.2269    1.8723    3.1161    2.6166    1.8245    1.8697    2.8187
      3.1206    1.6807    2.6376    2.5354    2.3630    1.6620    2.5427    3.0490
      3.3239    2.4390    2.5250    3.2197    3.1740    1.8284    2.6321    3.3856
      2.5121    1.4712    1.9810    2.1573    2.0079    1.4657    1.9457    2.6553
      2.5402    1.8044    1.4895    2.7534    2.1513    1.7941    1.6717    2.7603
      3.2897    2.2672    2.1267    2.8315    2.9692    1.9042    2.3213    3.1943
      2.6446    1.5237    1.9249    1.9874    2.0480    1.4021    1.9387    2.4016
   


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

   
      0.4811    0.3229    0.9615    0.8015    0.1708    0.1121
      0.8265    0.0645    0.8688    0.6204    0.0344    0.2009
      0.3534    0.7723    0.4859    0.9256    0.2942    0.0149
      0.1431    0.0302    0.1314    0.0488    0.7515    0.8970
      0.1328    0.7428    0.7082    0.8082    0.4953    0.5667
   
   
      0.8265
      0.7723
      0.7428
      0.9615
      0.8688
      0.7082
      0.8015
      0.6204
      0.9256
      0.8082
      0.7515
      0.8970
      0.5667
   

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

   
      0.6935    8.2649    6.9002    3.5844    3.4570    8.7928
      0.7106    4.8084    8.7465    5.7051    0.4980    4.4558
      2.3319    4.8287    9.9666    4.1506    8.5173    5.8329
      7.5266    4.4543    2.9412    6.2410    7.8650    0.1529
      3.9233    2.3918    2.4885    6.3993    1.5797    1.8204
   
   
      0.0000    8.2649    6.9002    0.0000    0.0000    8.7928
      0.0000    0.0000    8.7465    5.7051    0.0000    0.0000
      0.0000    0.0000    9.9666    0.0000    8.5173    5.8329
      7.5266    0.0000    0.0000    6.2410    7.8650    0.0000
      0.0000    0.0000    0.0000    6.3993    0.0000    0.0000
   
   
      0.0000    8.2649    6.9002    0.0000    0.0000    8.7928
      0.0000    0.0000    8.7465    5.7051    0.0000    0.0000
      0.0000    0.0000       NaN    0.0000    8.5173    5.8329
      7.5266    0.0000    0.0000    6.2410    7.8650    0.0000
      0.0000    0.0000    0.0000    6.3993    0.0000    0.0000
   

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

   
      3.5727    1.3058    1.1617    6.5000    1.0466    1.5537
      3.3684    8.5601    2.6265    4.5524    1.9118    4.7973
      6.5000    0.7418    6.5000    8.9336    4.7699    3.8065
      0.4389    1.5355    9.2786    4.2673    1.3764    6.5000
      2.1515    6.5000    3.7341    6.5000    4.3419    6.5000
   
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
   
