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
      0.2967    0.4976    0.9307    0.9922
   
   R1[2] = 0.9306587183717181
   C1 = 
      0.6901
      0.0938
      0.8766
      0.5271
      0.3702
      0.3312
      0.7600
      0.2451
   
   C1[5] = 0.33123497509651445

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
      0.1199    0.2164    0.1430    0.3151    0.8429
      0.3417    0.1127    0.3823    0.0249    0.9970
   

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
   
      0.9984    0.1577    0.5418    0.2436    0.1233    0.8396    0.1425    0.4695
      0.0322    0.4364    0.7562    0.2687    0.9398    0.7619    0.4034    0.2077
      0.9788    0.9814    0.3006    0.8415    0.3119    0.4900    0.9081    0.8950
      0.3541    0.5017    0.7875    0.0412    0.9408    0.4144    0.0205    0.7932
      0.6832    0.0996    0.3868    0.8907    0.9501    0.7538    0.0871    0.8619
      0.2631    0.8529    0.6445    0.6940    0.8758    0.3025    0.7826    0.9679
      0.5843    0.2574    0.9147    0.7788    0.4972    0.4085    0.8830    0.6857
      0.0275    0.8140    0.3198    0.8549    0.6389    0.6054    0.3296    0.1772
   
   B = 
   
      0.6872    0.0601    0.8109    0.1125    0.1392    0.9583    0.5805    0.2290
      0.9582    0.4968    0.8094    0.5932    0.2944    0.5399    0.6827    0.6732
      0.7104    0.6654    0.3151    0.1631    0.2651    0.5461    0.1874    0.1859
      0.9331    0.2533    0.7487    0.3363    0.1270    0.6591    0.9171    0.7224
      0.2051    0.6672    0.0610    0.8715    0.6704    0.9562    0.4970    0.6778
      0.2001    0.3110    0.5492    0.9345    0.1038    0.7840    0.5084    0.9443
      0.3933    0.5472    0.3569    0.0365    0.8470    0.8101    0.9897    0.5874
      0.6453    0.0426    0.7046    0.3164    0.1601    0.0599    0.5776    0.4783
   
   C = 
   
      2.0018    1.0019    2.1407    1.4220    0.7257    2.4181    1.9125    1.7962
      1.8660    1.8835    1.5847    2.0876    1.4516    2.6917    2.0783    2.3286
      3.7085    1.8550    3.5556    2.0704    1.7841    3.6582    3.8859    3.1842
      2.1178    1.6065    1.8234    1.9386    1.2291    2.3560    1.8901    2.0155
      2.6070    1.5262    2.5338    2.3069    1.2671    3.1285    2.7933    2.7582
      3.2761    2.1920    2.8073    2.2547    1.9831    3.2885    3.4149    3.0575
      2.9982    1.9401    2.6065    1.6939    1.7319    3.2639    3.1253    2.6092
      2.3200    1.6378    2.0360    2.0163    1.2356    2.5671    2.4695    2.5145
   
   D = 
   
      2.0018    1.0019    2.1407    1.4220    0.7257    2.4181    1.9125    1.7962
      1.8660    1.8835    1.5847    2.0876    1.4516    2.6917    2.0783    2.3286
      3.7085    1.8550    3.5556    2.0704    1.7841    3.6582    3.8859    3.1842
      2.1178    1.6065    1.8234    1.9386    1.2291    2.3560    1.8901    2.0155
      2.6070    1.5262    2.5338    2.3069    1.2671    3.1285    2.7933    2.7582
      3.2761    2.1920    2.8073    2.2547    1.9831    3.2885    3.4149    3.0575
      2.9982    1.9401    2.6065    1.6939    1.7319    3.2639    3.1253    2.6092
      2.3200    1.6378    2.0360    2.0163    1.2356    2.5671    2.4695    2.5145
   


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

   
      0.3657    0.2838    0.9644    0.9063    0.6120    0.7403
      0.0130    0.6940    0.8147    0.6639    0.8816    0.6130
      0.3653    0.9687    0.9198    0.8649    0.2290    0.1077
      0.4747    0.7223    0.4386    0.2242    0.1406    0.4715
      0.2625    0.2374    0.7834    0.9019    0.3136    0.1320
   
   
      0.6940
      0.9687
      0.7223
      0.9644
      0.8147
      0.9198
      0.7834
      0.9063
      0.6639
      0.8649
      0.9019
      0.6120
      0.8816
      0.7403
      0.6130
   

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

   
      0.5620    7.6703    7.1221    4.3774    0.7606    5.9375
      5.3864    0.3522    4.7455    0.4750    1.7922    2.0797
      4.3716    3.2664    9.4960    3.7781    9.3450    7.9301
      5.0549    8.0817    6.9407    9.6468    6.9890    7.9017
      4.3268    7.4167    2.5431    6.3453    4.7392    9.9767
   
   
      0.0000    7.6703    7.1221    0.0000    0.0000    5.9375
      5.3864    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    9.4960    0.0000    9.3450    7.9301
      5.0549    8.0817    6.9407    9.6468    6.9890    7.9017
      0.0000    7.4167    0.0000    6.3453    0.0000    9.9767
   
   
      0.0000    7.6703    7.1221    0.0000    0.0000    5.9375
      5.3864    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000       NaN    0.0000       NaN    7.9301
      5.0549    8.0817    6.9407       NaN    6.9890    7.9017
      0.0000    7.4167    0.0000    6.3453    0.0000       NaN
   

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

   
      1.8775    8.6492    4.4638    9.6889    8.8040    2.6210
      6.5000    4.6508    3.9962    9.0482    1.2816    4.3873
      0.9069    8.2709    1.4492    8.1886    1.2234    3.9225
      3.6637    3.2582    3.7170    3.0821    6.5000    1.6500
      6.5000    8.9961    9.9818    0.8019    6.5000    2.7892
   
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
   
