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
      0.9665    0.0578    0.6801    0.9389
   
   R1[2] = 0.680145914219527
   C1 = 
      0.8225
      0.5052
      0.3140
      0.7793
      0.8732
      0.7430
      0.1001
      0.4349
   
   C1[5] = 0.7429761605383457

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
      0.6708    0.8187    0.4577    0.3138    0.5026
      0.9758    0.7959    0.3418    0.0125    0.1043
   

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
   
      0.2238    0.9477    0.4246    0.8865    0.2997    0.7626    0.2187    0.8644
      0.2688    0.2734    0.2634    0.2401    0.9108    0.2370    0.4597    0.9903
      0.7803    0.9356    0.1448    0.3753    0.3300    0.3418    0.0597    0.9282
      0.4487    0.7904    0.1518    0.3138    0.5775    0.9236    0.9285    0.4800
      0.9704    0.6730    0.4612    0.3756    0.1864    0.4772    0.9841    0.1818
      0.7291    0.0991    0.0516    0.4735    0.9533    0.5265    0.7273    0.7002
      0.4082    0.7692    0.2173    0.1307    0.5806    0.1269    0.5094    0.1279
      0.6764    0.4818    0.3235    0.4842    0.4507    0.5426    0.6406    0.7729
   
   B = 
   
      0.0977    0.0629    0.3849    0.0880    0.5940    0.6689    0.2668    0.2181
      0.5026    0.5173    0.8172    0.3633    0.5407    0.7384    0.0610    0.4130
      0.9810    0.9516    0.0893    0.1120    0.3324    0.4309    0.6751    0.2667
      0.5192    0.8325    0.5513    0.5308    0.3626    0.4843    0.1545    0.8746
      0.2785    0.9101    0.8854    0.9883    0.0852    0.8072    0.1960    0.6848
      0.7712    0.1384    0.5756    0.5478    0.3418    0.0624    0.7976    0.7560
      0.5899    0.4064    0.2362    0.0062    0.2240    0.3287    0.2324    0.2739
      0.1105    0.4966    0.0870    0.5858    0.9626    0.2809    0.7536    0.6773
   
   C = 
   
      2.2711    2.5429    2.2185    2.1039    2.2752    2.0660    1.9103    2.7560
      1.3638    2.1492    1.6204    1.8930    1.6971    1.7907    1.5239    2.0514
      1.3766    1.8160    1.8685    1.6815    2.2054    2.0249    1.4716    2.0529
      2.2268    2.1119    2.3092    1.8738    1.8931    2.0650    1.7462    2.4124
      2.1010    1.8866    1.8597    1.1391    1.8042    2.0814    1.4521    1.8222
      1.5954    2.1240    2.0070    2.0025    1.7736    2.0505    1.6119    2.3521
      1.2817    1.5556    1.5957    1.1305    1.1081    1.6778    0.7525    1.2987
      1.8843    2.1321    1.8799    1.7272    2.0568    2.0074    1.7554    2.2741
   
   D = 
   
      2.2711    2.5429    2.2185    2.1039    2.2752    2.0660    1.9103    2.7560
      1.3638    2.1492    1.6204    1.8930    1.6971    1.7907    1.5239    2.0514
      1.3766    1.8160    1.8685    1.6815    2.2054    2.0249    1.4716    2.0529
      2.2268    2.1119    2.3092    1.8738    1.8931    2.0650    1.7462    2.4124
      2.1010    1.8866    1.8597    1.1391    1.8042    2.0814    1.4521    1.8222
      1.5954    2.1240    2.0070    2.0025    1.7736    2.0505    1.6119    2.3521
      1.2817    1.5556    1.5957    1.1305    1.1081    1.6778    0.7525    1.2987
      1.8843    2.1321    1.8799    1.7272    2.0568    2.0074    1.7554    2.2741
   


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

   
      0.1284    0.4690    0.6601    0.7846    0.4363    0.0455
      0.0473    0.1498    0.8728    0.1969    0.5058    0.4883
      0.7881    0.0829    0.8341    0.4984    0.8687    0.9181
      0.1544    0.9849    0.5336    0.7305    0.3670    0.3640
      0.3867    0.6450    0.5904    0.9106    0.5840    0.4635
   
   
      0.7881
      0.9849
      0.6450
      0.6601
      0.8728
      0.8341
      0.5336
      0.5904
      0.7846
      0.7305
      0.9106
      0.5058
      0.8687
      0.5840
      0.9181
   

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

   
      2.3477    3.0705    3.2893    1.7929    9.4500    6.8400
      4.0620    3.7026    7.9427    7.7527    1.7560    1.8893
      9.4047    7.1665    7.5405    3.7144    4.9653    0.2323
      3.5895    2.4924    4.7572    0.5120    9.8884    8.7010
      5.1180    9.5833    0.4237    0.3947    9.0711    3.8149
   
   
      0.0000    0.0000    0.0000    0.0000    9.4500    6.8400
      0.0000    0.0000    7.9427    7.7527    0.0000    0.0000
      9.4047    7.1665    7.5405    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    9.8884    8.7010
      5.1180    9.5833    0.0000    0.0000    9.0711    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000       NaN    6.8400
      0.0000    0.0000    7.9427    7.7527    0.0000    0.0000
         NaN    7.1665    7.5405    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN    8.7010
      5.1180       NaN    0.0000    0.0000       NaN    0.0000
   

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

   
      9.1212    2.4734    0.0685    3.0002    8.5601    0.0170
      2.1274    6.5000    0.4975    2.4690    1.4872    6.5000
      9.3442    0.4954    4.8911    0.0011    6.5000    6.5000
      6.5000    6.5000    3.0795    2.9359    6.5000    8.9101
      9.7870    6.5000    9.5814    1.1909    6.5000    4.3219
   
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
   
