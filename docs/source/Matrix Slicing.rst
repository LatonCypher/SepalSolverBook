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
      0.8136    0.8561    0.1779    0.6937
   
   R1[2] = 0.17786551582800425
   C1 = 
      0.2368
      0.8422
      0.8871
      0.3572
      0.7985
      0.5217
      0.5866
      0.5763
   
   C1[5] = 0.5216947917622591

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
      0.8316    0.2660    0.4642    0.0156    0.2219
      0.6531    0.2697    0.2954    0.0165    0.9521
   

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
   
      0.1874    0.3503    0.0799    0.3474    0.3177    0.1598    0.7827    0.2256
      0.4025    0.5708    0.6026    0.4802    0.6419    0.8681    0.7078    0.1178
      0.5942    0.0477    0.8853    0.9414    0.9100    0.2352    0.0898    0.2471
      0.5919    0.3163    0.2610    0.6857    0.7372    0.9258    0.6897    0.2315
      0.5032    0.0388    0.0412    0.5099    0.7013    0.8988    0.0421    0.4346
      0.5488    0.2810    0.8156    0.7431    0.2691    0.2201    0.3479    0.0948
      0.9265    0.4075    0.0715    0.2727    0.7081    0.8169    0.2587    0.9783
      0.0150    0.6248    0.6573    0.4247    0.4607    0.3585    0.6464    0.0838
   
   B = 
   
      0.4311    0.6478    0.5691    0.8292    0.8578    0.8849    0.2246    0.9335
      0.5728    0.3570    0.5951    0.7117    0.4137    0.4730    0.4835    0.6134
      0.9393    0.0703    0.5090    0.0672    0.7045    0.3639    0.9259    0.7489
      0.8095    0.7726    0.1473    0.0111    0.1659    0.6990    0.9120    0.2086
      0.7399    0.1709    0.9684    0.9713    0.5489    0.7368    0.0582    0.6951
      0.2333    0.8563    0.8348    0.2899    0.7088    0.1579    0.0886    0.3906
      0.8957    0.5493    0.8718    0.6263    0.2204    0.3461    0.1915    0.8044
      0.3477    0.9687    0.4427    0.2707    0.1906    0.2323    0.4547    0.8608
   
   C = 
   
      1.6894    1.3601    1.6301    1.3201    0.9227    1.1860    0.8874    1.6291
      2.8074    2.2338    2.9616    2.1361    2.2316    2.0634    1.6656    2.7333
      2.7717    1.8372    2.2212    1.6718    2.0424    2.3247    2.0382    2.4527
      2.6963    2.5664    2.9494    2.2201    2.1932    2.2296    1.5153    2.7133
      1.6080    2.0703    2.0641    1.5390    1.6754    1.6092    0.9611    1.8773
      2.3602    1.6047    1.7938    1.2869    1.6834    1.8103    1.8369    2.0850
      2.2071    2.8716    2.8726    2.4175    2.2702    2.1968    1.3280    3.0868
      2.3581    1.4291    2.1235    1.4850    1.4703    1.4841    1.5219    2.0305
   
   D = 
   
      1.6894    1.3601    1.6301    1.3201    0.9227    1.1860    0.8874    1.6291
      2.8074    2.2338    2.9616    2.1361    2.2316    2.0634    1.6656    2.7333
      2.7717    1.8372    2.2212    1.6718    2.0424    2.3247    2.0382    2.4527
      2.6963    2.5664    2.9494    2.2201    2.1932    2.2296    1.5153    2.7133
      1.6080    2.0703    2.0641    1.5390    1.6754    1.6092    0.9611    1.8773
      2.3602    1.6047    1.7938    1.2869    1.6834    1.8103    1.8369    2.0850
      2.2071    2.8716    2.8726    2.4175    2.2702    2.1968    1.3280    3.0868
      2.3581    1.4291    2.1235    1.4850    1.4703    1.4841    1.5219    2.0305
   


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

   
      0.6379    0.0408    0.0934    0.1534    0.5581    0.9227
      0.2018    0.1970    0.4506    0.7071    0.4604    0.7262
      0.3422    0.9613    0.7086    0.5101    0.2322    0.0092
      0.8982    0.3597    0.4698    0.3668    0.2803    0.3098
      0.2920    0.2994    0.4879    0.1068    0.8773    0.5835
   
   
      0.6379
      0.8982
      0.9613
      0.7086
      0.7071
      0.5101
      0.5581
      0.8773
      0.9227
      0.7262
      0.5835
   

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

   
      6.9625    9.8214    1.3085    1.3557    8.0570    5.1529
      2.1217    8.6669    0.3156    9.0728    3.6212    9.8471
      7.7744    8.6580    5.8235    5.1398    3.8644    5.9596
      7.8509    6.2140    5.7375    5.7036    0.8115    8.0300
      9.1345    5.9879    9.3098    5.1757    1.7975    1.4236
   
   
      6.9625    9.8214    0.0000    0.0000    8.0570    5.1529
      0.0000    8.6669    0.0000    9.0728    0.0000    9.8471
      7.7744    8.6580    5.8235    5.1398    0.0000    5.9596
      7.8509    6.2140    5.7375    5.7036    0.0000    8.0300
      9.1345    5.9879    9.3098    5.1757    0.0000    0.0000
   
   
      6.9625       NaN    0.0000    0.0000    8.0570    5.1529
      0.0000    8.6669    0.0000       NaN    0.0000       NaN
      7.7744    8.6580    5.8235    5.1398    0.0000    5.9596
      7.8509    6.2140    5.7375    5.7036    0.0000    8.0300
         NaN    5.9879       NaN    5.1757    0.0000    0.0000
   

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

   
      6.5000    6.5000    1.9425    6.5000    4.5667    0.1453
      3.1063    9.6713    6.5000    3.5206    9.7966    0.7001
      6.5000    6.5000    8.7327    6.5000    6.5000    2.9662
      3.7626    1.0075    6.5000    8.3223    6.5000    8.5017
      1.7239    3.9558    4.3580    2.9092    2.6135    6.5000
   
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
   
