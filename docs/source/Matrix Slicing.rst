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
      0.8082    0.0885    0.2390    0.1201
   
   R1[2] = 0.23897943144884293
   C1 = 
      0.4931
      0.1158
      0.6453
      0.9583
      0.2055
      0.5817
      0.2579
      0.8045
   
   C1[5] = 0.5817215821557646

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
      0.4757    0.3266    0.4895    0.7323    0.8955
      0.3443    0.3387    0.1721    0.7581    0.4805
   

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
   
      0.2795    0.9158    0.5324    0.5168    0.2421    0.3688    0.9981    0.2234
      0.3690    0.1921    0.6251    0.5558    0.1157    0.9122    0.6000    0.1712
      0.0647    0.4790    0.6713    0.4417    0.1974    0.5577    0.7709    0.2340
      0.7195    0.7200    0.6980    0.8534    0.5830    0.2687    0.2210    0.4996
      0.4715    0.7833    0.8330    0.8847    0.1130    0.6000    0.9090    0.8126
      0.1860    0.3492    0.3408    0.9993    0.5227    0.0304    0.2513    0.4517
      0.7880    0.6310    0.7887    0.9901    0.4974    0.8113    0.2850    0.9733
      0.0192    0.3119    0.6898    0.1127    0.8149    0.3478    0.9054    0.0367
   
   B = 
   
      0.5296    0.2668    0.1767    0.4469    0.5364    0.2091    0.1440    0.5324
      0.0307    0.4232    0.2493    0.2573    0.5434    0.7872    0.1360    0.0359
      0.2542    0.5193    0.8390    0.6202    0.7996    0.6920    0.4646    0.8799
      0.4763    0.3100    0.9503    0.9721    0.3592    0.2480    0.6464    0.4474
      0.9040    0.1476    0.4363    0.5145    0.7196    0.1576    0.4096    0.1485
      0.0894    0.8366    0.3110    0.9388    0.6330    0.0380    0.6529    0.9799
      0.2610    0.4474    0.2107    0.6437    0.0207    0.5631    0.4293    0.3251
      0.4149    0.6239    0.5616    0.8104    0.3943    0.7446    0.7034    0.3834
   
   C = 
   
      1.1627    1.8290    1.7716    2.4874    1.7752    2.0565    1.6718    1.6888
      1.0388    1.8322    1.7226    2.5832    1.7424    1.3170    1.7500    2.1738
      0.9566    1.6921    1.6672    2.3088    1.5936    1.6253    1.6124    1.7560
      1.8031    1.8452    2.3683    2.8686    2.4330    2.0104    1.9380    2.0182
      1.6371    2.5964    2.7020    3.6539    2.4626    2.6685    2.5332    2.6196
      1.4001    1.1810    1.8997    2.1810    1.4997    1.3588    1.5381    1.2208
      2.1092    2.6808    2.9752    3.9559    3.0129    2.4472    2.7462    2.9138
      1.2681    1.3695    1.4421    1.9845    1.6116    1.4336    1.4139    1.4491
   
   D = 
   
      1.1627    1.8290    1.7716    2.4874    1.7752    2.0565    1.6718    1.6888
      1.0388    1.8322    1.7226    2.5832    1.7424    1.3170    1.7500    2.1738
      0.9566    1.6921    1.6672    2.3088    1.5936    1.6253    1.6124    1.7560
      1.8031    1.8452    2.3683    2.8686    2.4330    2.0104    1.9380    2.0182
      1.6371    2.5964    2.7020    3.6539    2.4626    2.6685    2.5332    2.6196
      1.4001    1.1810    1.8997    2.1810    1.4997    1.3588    1.5381    1.2208
      2.1092    2.6808    2.9752    3.9559    3.0129    2.4472    2.7462    2.9138
      1.2681    1.3695    1.4421    1.9845    1.6116    1.4336    1.4139    1.4491
   


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

   
      0.0408    0.1117    0.7908    0.7053    0.2585    0.4472
      0.4631    0.3213    0.5682    0.4042    0.4040    0.6120
      0.1259    0.4396    0.4162    0.3731    0.8819    0.9298
      0.7303    0.4250    0.8057    0.8145    0.6409    0.2649
      0.5526    0.0797    0.8232    0.1478    0.3495    0.7873
   
   
      0.7303
      0.5526
      0.7908
      0.5682
      0.8057
      0.8232
      0.7053
      0.8145
      0.8819
      0.6409
      0.6120
      0.9298
      0.7873
   

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

   
      9.2106    4.5566    6.5698    3.2728    2.6008    3.7138
      4.7944    2.1225    7.8965    6.8633    2.0735    3.8259
      5.6668    5.9341    7.0238    8.0418    6.4264    7.9240
      1.7705    3.2245    4.0419    3.9027    6.4672    2.9474
      0.1180    9.6893    9.2832    6.3520    9.2167    1.9812
   
   
      9.2106    0.0000    6.5698    0.0000    0.0000    0.0000
      0.0000    0.0000    7.8965    6.8633    0.0000    0.0000
      5.6668    5.9341    7.0238    8.0418    6.4264    7.9240
      0.0000    0.0000    0.0000    0.0000    6.4672    0.0000
      0.0000    9.6893    9.2832    6.3520    9.2167    0.0000
   
   
         NaN    0.0000    6.5698    0.0000    0.0000    0.0000
      0.0000    0.0000    7.8965    6.8633    0.0000    0.0000
      5.6668    5.9341    7.0238    8.0418    6.4264    7.9240
      0.0000    0.0000    0.0000    0.0000    6.4672    0.0000
      0.0000       NaN       NaN    6.3520       NaN    0.0000
   

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

   
      0.1973    2.7469    0.8126    1.2102    8.2730    6.5000
      1.4220    6.5000    8.6703    8.2011    6.5000    6.5000
      6.5000    6.5000    8.5153    3.1641    1.8598    3.5928
      0.0425    6.5000    6.5000    1.6518    3.2264    1.2649
      0.1026    6.5000    8.3459    0.9350    3.8869    3.5156
   
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
   
