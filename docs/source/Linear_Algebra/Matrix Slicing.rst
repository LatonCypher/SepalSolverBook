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
      0.0270    0.7748    0.5673    0.2529
   
   R1[2] = 0.5672908720750476
   C1 = 
      0.9052
      0.6169
      0.6533
      0.2844
      0.1989
      0.2706
      0.4738
      0.1205
   
   C1[5] = 0.2706095223541487

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
      0.0586    0.2188    0.8893    0.4518    0.6481
      0.0122    0.7927    0.1620    0.1371    0.1169
   

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
   
      0.0357    0.4765    0.9320    0.2433    0.3313    0.0002    0.4693    0.3921
      0.5035    0.9645    0.8293    0.8864    0.3389    0.1738    0.8588    0.4800
      0.8709    0.4662    0.9578    0.1222    0.0903    0.5453    0.5006    0.6111
      0.3662    0.6796    0.4126    0.2472    0.1766    0.0520    0.3749    0.8796
      0.0866    0.7058    0.9223    0.7977    0.1088    0.7063    0.2560    0.8336
      0.9391    0.1733    0.7917    0.7635    0.1249    0.0527    0.2429    0.0389
      0.3511    0.4476    0.3023    0.1635    0.9000    0.4079    0.3485    0.2025
      0.0111    0.5810    0.7485    0.6947    0.2200    0.1307    0.3026    0.6741
   
   B = 
   
      0.4237    0.4068    0.4617    0.5145    0.0371    0.5378    0.8960    0.0441
      0.7988    0.9987    0.0427    0.1383    0.1448    0.2650    0.4831    0.3544
      0.6056    0.0601    0.1508    0.4276    0.9628    0.3669    0.2021    0.9846
      0.0170    0.3801    0.0511    0.0376    0.5355    0.9864    0.9628    0.1140
      0.6648    0.3447    0.8267    0.7430    0.0396    0.5783    0.1831    0.3567
      0.4513    0.1713    0.6202    0.9889    0.3552    0.9082    0.2974    0.7865
      0.2865    0.8928    0.2003    0.0378    0.9100    0.0241    0.6152    0.8027
      0.5430    0.5730    0.6786    0.0936    0.8970    0.6830    0.8266    0.7629
   
   C = 
   
      1.5320    1.3968    0.8239    0.7927    1.8899    1.1983    1.3584    1.9099
      2.3116    2.7432    1.3297    1.2814    2.7187    2.4073    2.9769    2.5947
      2.1049    1.8454    1.5004    1.6090    2.2883    2.0407    2.3085    2.4896
      1.6780    1.8549    1.1232    0.7472    1.7972    1.5314    1.9833    1.7672
      2.0897    1.9634    1.3949    1.4335    2.6564    2.6387    2.4494    2.6887
      1.2263    1.1841    0.8103    1.0321    1.5106    1.7471    2.0404    1.2800
      1.6844    1.4775    1.4394    1.4821    1.1358    1.6173    1.4173    1.5665
      1.5919    1.6484    0.9593    0.7994    2.1124    1.8334    1.9332    1.9610
   
   D = 
   
      1.5320    1.3968    0.8239    0.7927    1.8899    1.1983    1.3584    1.9099
      2.3116    2.7432    1.3297    1.2814    2.7187    2.4073    2.9769    2.5947
      2.1049    1.8454    1.5004    1.6090    2.2883    2.0407    2.3085    2.4896
      1.6780    1.8549    1.1232    0.7472    1.7972    1.5314    1.9833    1.7672
      2.0897    1.9634    1.3949    1.4335    2.6564    2.6387    2.4494    2.6887
      1.2263    1.1841    0.8103    1.0321    1.5106    1.7471    2.0404    1.2800
      1.6844    1.4775    1.4394    1.4821    1.1358    1.6173    1.4173    1.5665
      1.5919    1.6484    0.9593    0.7994    2.1124    1.8334    1.9332    1.9610
   


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

   
      0.3755    0.8509    0.8332    0.1051    0.9415    0.8563
      0.7651    0.0331    0.6640    0.8864    0.5706    0.1605
      0.0141    0.2674    0.4281    0.5249    0.4334    0.8480
      0.6944    0.9948    0.4388    0.7880    0.7312    0.8425
      0.5118    0.4353    0.9818    0.9618    0.1856    0.7733
   
   
      0.7651
      0.6944
      0.5118
      0.8509
      0.9948
      0.8332
      0.6640
      0.9818
      0.8864
      0.5249
      0.7880
      0.9618
      0.9415
      0.5706
      0.7312
      0.8563
      0.8480
      0.8425
      0.7733
   

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

   
      3.1314    7.1966    3.0043    8.9936    0.5230    6.4438
      7.6207    2.5658    6.1840    4.7366    5.8908    1.4244
      7.4323    0.8030    0.1988    4.7775    1.9131    7.1420
      6.4473    6.7566    2.7406    6.8450    5.4313    5.5850
      1.1172    7.7978    4.4077    8.9797    3.5398    9.6968
   
   
      0.0000    7.1966    0.0000    8.9936    0.0000    6.4438
      7.6207    0.0000    6.1840    0.0000    5.8908    0.0000
      7.4323    0.0000    0.0000    0.0000    0.0000    7.1420
      6.4473    6.7566    0.0000    6.8450    5.4313    5.5850
      0.0000    7.7978    0.0000    8.9797    0.0000    9.6968
   
   
      0.0000    7.1966    0.0000    8.9936    0.0000    6.4438
      7.6207    0.0000    6.1840    0.0000    5.8908    0.0000
      7.4323    0.0000    0.0000    0.0000    0.0000    7.1420
      6.4473    6.7566    0.0000    6.8450    5.4313    5.5850
      0.0000    7.7978    0.0000    8.9797    0.0000       NaN
   

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

   
      1.3843    3.4843    6.5000    0.2614    9.4465    6.5000
      9.3692    8.6689    1.1620    6.5000    0.3731    6.5000
      1.8087    6.5000    2.9588    9.9405    4.8635    6.5000
      9.5861    1.3685    8.9850    6.5000    8.4204    4.9799
      4.0394    3.6601    6.5000    4.6123    3.9850    6.5000
   
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
   
