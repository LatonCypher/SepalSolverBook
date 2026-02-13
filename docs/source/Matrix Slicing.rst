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
      0.9048    0.7747    0.5758    0.8096
   
   R1[2] = 0.5757875735969594
   C1 = 
      0.9822
      0.2046
      0.7089
      0.3263
      0.3996
      0.6980
      0.5315
      0.6875
   
   C1[5] = 0.69802251522841

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
      0.3118    0.1448    0.4829    0.9161    0.1417
      0.3120    0.0926    0.9723    0.8696    0.5411
   

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
   
      0.8438    0.4474    0.0705    0.5733    0.5021    0.8048    0.9054    0.3423
      0.8195    0.6055    0.4076    0.6712    0.9945    0.9931    0.5984    0.4987
      0.3952    0.6184    0.2806    0.4273    0.0351    0.4701    0.6396    0.8833
      0.8261    0.9566    0.7810    0.0853    0.5451    0.4439    0.4551    0.4757
      0.0678    0.2095    0.7702    0.4939    0.0949    0.2566    0.3457    0.3909
      0.0687    0.9185    0.4137    0.0385    0.7548    0.6842    0.9721    0.9082
      0.1325    0.1320    0.4519    0.5177    0.3120    0.5816    0.2954    0.4122
      0.3297    0.5203    0.2698    0.8767    0.2601    0.4736    0.1088    0.4660
   
   B = 
   
      0.1830    0.7525    0.4166    0.4754    0.2242    0.1822    0.8366    0.9855
      0.6019    0.3904    0.0053    0.0028    0.0665    0.4629    0.1015    0.7323
      0.9475    0.9560    0.4407    0.6908    0.4461    0.3760    0.9858    0.5700
      0.1063    0.8476    0.1066    0.2630    0.4616    0.8483    0.6448    0.4890
      0.0865    0.9176    0.6419    0.4470    0.8412    0.7403    0.1164    0.9529
      0.8779    0.6552    0.0348    0.7608    0.4348    0.5687    0.5639    0.3405
      0.5760    0.0092    0.3591    0.8245    0.5225    0.9053    0.0529    0.8898
      0.9015    0.8860    0.1876    0.4736    0.5776    0.0620    0.2357    0.9918
   
   C = 
   
      2.1315    2.6625    1.1857    2.3471    1.9580    2.5440    1.8313    3.3773
      2.7242    3.8222    1.5772    2.7790    2.5848    3.0260    2.4066    4.1245
      2.3363    2.2980    0.7715    1.8148    1.5305    1.7534    1.4567    2.8500
      2.6038    3.0307    1.3205    2.1392    1.8007    2.0567    2.0631    3.5488
      1.7058    1.8925    0.6887    1.4026    1.1985    1.3713    1.4219    1.7739
      3.0061    2.7929    1.2477    2.4206    2.2437    2.5101    1.3225    3.7129
      1.6662    2.0573    0.7142    1.5324    1.3868    1.5491    1.3805    1.9050
      1.6434    2.4151    0.6625    1.3621    1.3844    1.7355    1.5729    2.2567
   
   D = 
   
      2.1315    2.6625    1.1857    2.3471    1.9580    2.5440    1.8313    3.3773
      2.7242    3.8222    1.5772    2.7790    2.5848    3.0260    2.4066    4.1245
      2.3363    2.2980    0.7715    1.8148    1.5305    1.7534    1.4567    2.8500
      2.6038    3.0307    1.3205    2.1392    1.8007    2.0567    2.0631    3.5488
      1.7058    1.8925    0.6887    1.4026    1.1985    1.3713    1.4219    1.7739
      3.0061    2.7929    1.2477    2.4206    2.2437    2.5101    1.3225    3.7129
      1.6662    2.0573    0.7142    1.5324    1.3868    1.5491    1.3805    1.9050
      1.6434    2.4151    0.6625    1.3621    1.3844    1.7355    1.5729    2.2567
   


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

   
      0.3597    0.9365    0.9747    0.2719    0.5549    0.6652
      0.1435    0.5264    0.1283    0.2232    0.8408    0.4058
      0.8962    0.1858    0.1248    0.0223    0.7192    0.7703
      0.5107    0.6572    0.0209    0.0997    0.4886    0.7889
      0.8972    0.4954    0.8119    0.5825    0.8011    0.2733
   
   
      0.8962
      0.5107
      0.8972
      0.9365
      0.5264
      0.6572
      0.9747
      0.8119
      0.5825
      0.5549
      0.8408
      0.7192
      0.8011
      0.6652
      0.7703
      0.7889
   

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

   
      1.9382    4.9089    2.1097    5.9691    9.5656    7.1606
      2.9782    9.5453    8.7506    1.8607    0.2742    6.5678
      3.3217    7.1623    2.1468    4.9619    4.9990    7.8895
      3.6817    8.5180    3.8413    0.5324    8.6443    6.6494
      6.4512    5.1011    2.5969    2.2179    5.7788    2.1211
   
   
      0.0000    0.0000    0.0000    5.9691    9.5656    7.1606
      0.0000    9.5453    8.7506    0.0000    0.0000    6.5678
      0.0000    7.1623    0.0000    0.0000    0.0000    7.8895
      0.0000    8.5180    0.0000    0.0000    8.6443    6.6494
      6.4512    5.1011    0.0000    0.0000    5.7788    0.0000
   
   
      0.0000    0.0000    0.0000    5.9691       NaN    7.1606
      0.0000       NaN    8.7506    0.0000    0.0000    6.5678
      0.0000    7.1623    0.0000    0.0000    0.0000    7.8895
      0.0000    8.5180    0.0000    0.0000    8.6443    6.6494
      6.4512    5.1011    0.0000    0.0000    5.7788    0.0000
   

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

   
      4.6213    4.3781    0.0324    8.2608    0.2866    6.5000
      6.5000    8.5399    6.5000    2.1934    8.9922    4.2487
      0.8009    6.5000    6.5000    4.5798    9.2894    6.5000
      2.4476    0.6917    0.8142    3.0682    0.8581    8.4470
      6.5000    2.9815    6.5000    9.6127    3.5966    4.1623
   
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
   
