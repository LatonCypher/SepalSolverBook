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
      0.3272    0.1432    0.4855    0.4548
   
   R1[2] = 0.48549291088445146
   C1 = 
      0.6113
      0.0557
      0.2302
      0.0988
      0.8706
      0.3298
      0.0024
      0.9211
   
   C1[5] = 0.3297542053004837

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
      0.3729    0.1541    0.8633    0.5996    0.8109
      0.4236    0.3430    0.5731    0.6529    0.2625
   

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
   
      0.1645    0.7919    0.2986    0.7465    0.6736    0.9791    0.6494    0.9321
      0.1795    0.8353    0.1846    0.6681    0.2896    0.1220    0.0554    0.7143
      0.8354    0.6175    0.4387    0.0365    0.5895    0.3775    0.8445    0.6630
      0.3718    0.3357    0.5614    0.1202    0.1804    0.9974    0.4849    0.4416
      0.6876    0.6057    0.5343    0.0417    0.1084    0.0111    0.3453    0.1120
      0.7851    0.8627    0.0298    0.8403    0.2147    0.2255    0.5964    0.9407
      0.5889    0.5218    0.9200    0.7702    0.4331    0.4916    0.2939    0.9586
      0.0091    0.2887    0.9505    0.7432    0.6983    0.4211    0.9536    0.6205
   
   B = 
   
      0.8659    0.1795    0.7205    0.5052    0.8837    0.7691    0.8007    0.4109
      0.0329    0.7020    0.3285    0.9265    0.2709    0.9073    0.3422    0.5331
      0.0319    0.1199    0.3923    0.2721    0.7367    0.1969    0.4622    0.2832
      0.2542    0.0140    0.4381    0.3968    0.5781    0.4479    0.8015    0.1829
      0.4224    0.1662    0.6320    0.1617    0.1774    0.9482    0.5282    0.5826
      0.6695    0.6342    0.4876    0.8485    0.5867    0.4666    0.9164    0.2758
      0.1458    0.9134    0.9011    0.5545    0.1109    0.1707    0.7488    0.0638
      0.4691    0.2975    0.4369    0.6428    0.9263    0.2728    0.8884    0.4852
   
   C = 
   
      1.8397    2.2350    2.7185    3.0932    2.6408    2.6988    3.7064    1.8670
      0.9057    1.0387    1.3734    1.8202    1.6978    1.7674    1.9912    1.2460
      1.7029    1.9425    2.6003    2.4382    2.2838    2.3656    2.9910    1.6265
      1.4033    1.6083    1.8815    2.1277    1.9828    1.5947    2.5333    1.1382
      0.7991    0.9871    1.3563    1.3610    1.3573    1.3997    1.4637    0.9070
      1.6926    1.7651    2.4229    2.6988    2.5430    2.4358    3.2133    1.6263
      1.7567    1.5304    2.4912    2.6032    3.0706    2.4040    3.4435    1.7932
      1.2435    1.7674    2.5770    2.2235    2.2675    1.9795    3.1611    1.4475
   
   D = 
   
      1.8397    2.2350    2.7185    3.0932    2.6408    2.6988    3.7064    1.8670
      0.9057    1.0387    1.3734    1.8202    1.6978    1.7674    1.9912    1.2460
      1.7029    1.9425    2.6003    2.4382    2.2838    2.3656    2.9910    1.6265
      1.4033    1.6083    1.8815    2.1277    1.9828    1.5947    2.5333    1.1382
      0.7991    0.9871    1.3563    1.3610    1.3573    1.3997    1.4637    0.9070
      1.6926    1.7651    2.4229    2.6988    2.5430    2.4358    3.2133    1.6263
      1.7567    1.5304    2.4912    2.6032    3.0706    2.4040    3.4435    1.7932
      1.2435    1.7674    2.5770    2.2235    2.2675    1.9795    3.1611    1.4475
   


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

   
      0.0258    0.3981    0.7106    0.0928    0.7004    0.3363
      0.5610    0.0885    0.4866    0.6994    0.2080    0.0237
      0.6247    0.3971    0.1515    0.2363    0.1820    0.8408
      0.5250    0.5848    0.3761    0.8483    0.0818    0.2622
      0.9223    0.7082    0.7299    0.4720    0.0712    0.8610
   
   
      0.5610
      0.6247
      0.5250
      0.9223
      0.5848
      0.7082
      0.7106
      0.7299
      0.6994
      0.8483
      0.7004
      0.8408
      0.8610
   

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

   
      7.4378    3.6816    1.4135    3.4324    2.3755    4.5737
      3.4083    9.8754    5.1543    8.6682    2.2333    2.8907
      5.1920    3.9430    5.0126    6.8827    7.1047    8.8550
      8.2923    8.6894    7.6201    0.8200    0.4636    7.1397
      7.1064    8.7999    7.0075    9.0571    9.6998    5.7790
   
   
      7.4378    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    9.8754    5.1543    8.6682    0.0000    0.0000
      5.1920    0.0000    5.0126    6.8827    7.1047    8.8550
      8.2923    8.6894    7.6201    0.0000    0.0000    7.1397
      7.1064    8.7999    7.0075    9.0571    9.6998    5.7790
   
   
      7.4378    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000       NaN    5.1543    8.6682    0.0000    0.0000
      5.1920    0.0000    5.0126    6.8827    7.1047    8.8550
      8.2923    8.6894    7.6201    0.0000    0.0000    7.1397
      7.1064    8.7999    7.0075       NaN       NaN    5.7790
   

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

   
      6.5000    0.0285    4.5823    6.5000    3.5437    9.7499
      2.8427    0.6000    6.5000    3.1126    8.9175    0.2757
      6.5000    6.5000    4.3633    6.5000    4.8044    3.8921
      4.0074    0.4639    0.3768    6.5000    9.0907    3.4517
      1.6690    2.1871    6.5000    8.8764    2.1497    1.7374
   
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
   
