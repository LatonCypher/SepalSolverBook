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
      0.9310    0.3338    0.5520    0.6802
   
   R1[2] = 0.551992250852842
   C1 = 
      0.9110
      0.9183
      0.4405
      0.1022
      0.9667
      0.8538
      0.4279
      0.6010
   
   C1[5] = 0.8537889770107233

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
      0.2257    0.3777    0.3886    0.1374    0.5284
      0.6728    0.2714    0.4434    0.8769    0.3521
   

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
   
      0.5194    0.5316    0.2577    0.8177    0.1316    0.9968    0.1169    0.5330
      0.9523    0.8104    0.1481    0.4611    0.5603    0.5385    0.9441    0.0097
      0.2124    0.7168    0.3472    0.8304    0.5382    0.9093    0.4783    0.7027
      0.4507    0.0751    0.6056    0.5707    0.3864    0.1920    0.0406    0.8277
      0.3871    0.6235    0.3865    0.4439    0.7778    0.7518    0.7296    0.1437
      0.1612    0.4568    0.1621    0.9245    0.4721    0.4866    0.8245    0.2712
      0.4951    0.9755    0.4461    0.6174    0.1847    0.1786    0.1494    0.9156
      0.7319    0.5307    0.6349    0.8578    0.4380    0.9413    0.0996    0.9360
   
   B = 
   
      0.7780    0.5918    0.1035    0.7317    0.4402    0.3335    0.7459    0.5757
      0.5221    0.9792    0.5850    0.4676    0.9653    0.4225    0.4772    0.6268
      0.6979    0.2424    0.4942    0.1676    0.5739    0.3385    0.3160    0.7117
      0.2120    0.8525    0.4652    0.8711    0.7783    0.4311    0.8259    0.8897
      0.8886    0.0644    0.1189    0.2810    0.6553    0.2275    0.6654    0.9603
      0.9868    0.2517    0.2248    0.6672    0.2315    0.3558    0.4055    0.8459
      0.8859    0.5446    0.5769    0.5209    0.2924    0.0189    0.7549    0.5502
      0.4087    0.2561    0.1391    0.0066    0.1667    0.2886    0.1860    0.1062
   
   C = 
   
      2.4569    2.0470    1.2538    2.1506    1.9662    1.3783    2.0771    2.6336
      3.2348    2.4742    1.5939    2.5108    2.4148    1.2486    2.8303    3.0857
      3.0444    2.3235    1.6412    2.2838    2.4512    1.5071    2.5146    3.1812
      1.8406    1.2808    0.8830    1.2268    1.5101    1.0290    1.5543    1.8890
      3.1286    1.9850    1.5046    2.1274    2.2605    1.2145    2.4749    3.0831
      2.4141    2.0414    1.4730    2.0526    2.0328    1.0746    2.3373    2.6644
      2.1838    2.2553    1.4052    1.6858    2.2548    1.3672    1.9641    2.2711
      3.2605    2.3969    1.5504    2.4465    2.5566    1.7597    2.6307    3.3401
   
   D = 
   
      2.4569    2.0470    1.2538    2.1506    1.9662    1.3783    2.0771    2.6336
      3.2348    2.4742    1.5939    2.5108    2.4148    1.2486    2.8303    3.0857
      3.0444    2.3235    1.6412    2.2838    2.4512    1.5071    2.5146    3.1812
      1.8406    1.2808    0.8830    1.2268    1.5101    1.0290    1.5543    1.8890
      3.1286    1.9850    1.5046    2.1274    2.2605    1.2145    2.4749    3.0831
      2.4141    2.0414    1.4730    2.0526    2.0328    1.0746    2.3373    2.6644
      2.1838    2.2553    1.4052    1.6858    2.2548    1.3672    1.9641    2.2711
      3.2605    2.3969    1.5504    2.4465    2.5566    1.7597    2.6307    3.3401
   


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

   
      0.7795    0.7015    0.7382    0.5188    0.6188    0.3700
      0.9080    0.8009    0.6336    0.4729    0.8109    0.9974
      0.8784    0.9786    0.0601    0.9263    0.2566    0.4701
      0.7106    0.1606    0.0632    0.4865    0.7952    0.6419
      0.6536    0.6254    0.8611    0.5827    0.9839    0.8689
   
   
      0.7795
      0.9080
      0.8784
      0.7106
      0.6536
      0.7015
      0.8009
      0.9786
      0.6254
      0.7382
      0.6336
      0.8611
      0.5188
      0.9263
      0.5827
      0.6188
      0.8109
      0.7952
      0.9839
      0.9974
      0.6419
      0.8689
   

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

   
      0.6876    0.8098    2.8411    7.5018    2.2580    0.6906
      4.9660    6.8871    8.3791    2.7632    4.1937    2.6047
      1.2247    1.0100    7.3214    6.8857    8.1348    9.9865
      4.1706    3.0125    6.0000    2.5543    3.6296    5.1613
      2.5453    7.4404    0.8465    5.2564    6.0349    4.4405
   
   
      0.0000    0.0000    0.0000    7.5018    0.0000    0.0000
      0.0000    6.8871    8.3791    0.0000    0.0000    0.0000
      0.0000    0.0000    7.3214    6.8857    8.1348    9.9865
      0.0000    0.0000    6.0000    0.0000    0.0000    5.1613
      0.0000    7.4404    0.0000    5.2564    6.0349    0.0000
   
   
      0.0000    0.0000    0.0000    7.5018    0.0000    0.0000
      0.0000    6.8871    8.3791    0.0000    0.0000    0.0000
      0.0000    0.0000    7.3214    6.8857    8.1348       NaN
      0.0000    0.0000    6.0000    0.0000    0.0000    5.1613
      0.0000    7.4404    0.0000    5.2564    6.0349    0.0000
   

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

   
      0.3225    0.1183    8.8469    0.4510    0.3348    6.5000
      3.4057    0.1500    0.4922    6.5000    9.8302    8.6372
      8.5499    4.6464    4.1283    6.5000    6.5000    2.7957
      4.6582    9.7673    6.5000    6.5000    8.9226    3.6499
      2.5904    6.5000    6.5000    1.1750    3.6297    4.8734
   
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
   
