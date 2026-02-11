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
      0.5679    0.3260    0.2359    0.4374
   
   R1[2] = 0.23590233708487307
   C1 = 
      0.7170
      0.2063
      0.0681
      0.7687
      0.9937
      0.0562
      0.4945
      0.8788
   
   C1[5] = 0.05620071483039779

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
      0.0092    0.0201    0.2127    0.2297    0.8577
      0.1618    0.4980    0.7740    0.9790    0.7304
   

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
   
      0.4447    0.3217    0.0924    0.3405    0.8179    0.9799    0.3298    0.6046
      0.4736    0.2851    0.2680    0.7571    0.2968    0.9221    0.3250    0.0210
      0.7519    0.0245    0.2915    0.8747    0.2557    0.2999    0.4077    0.0709
      0.6085    0.6730    0.8395    0.7454    0.3747    0.4214    0.6015    0.9592
      0.5983    0.7864    0.9422    0.6864    0.4750    0.3609    0.2801    0.2650
      0.2278    0.8955    0.7276    0.3197    0.1304    0.1484    0.0958    0.7669
      0.6966    0.0171    0.4429    0.3320    0.8638    0.2984    0.9320    0.4064
      0.0297    0.0710    0.7135    0.7506    0.3616    0.8210    0.5446    0.5894
   
   B = 
   
      0.1217    0.9500    0.3111    0.4535    0.6537    0.2206    0.0079    0.8456
      0.2293    0.1572    0.7655    0.4274    0.1999    0.7815    0.9504    0.4740
      0.7614    0.1337    0.2861    0.5117    0.5017    0.5775    0.7914    0.2940
      0.5888    0.4730    0.9560    0.6356    0.3032    0.8276    0.1267    0.6467
      0.9727    0.4597    0.3595    0.9027    0.9540    0.0159    0.1890    0.4037
      0.9057    0.1839    0.3585    0.3525    0.0799    0.1250    0.8324    0.5129
      0.4414    0.3065    0.3676    0.1927    0.8633    0.0162    0.1400    0.2332
      0.1411    0.8124    0.5448    0.4536    0.1337    0.2530    0.0805    0.7539
   
   C = 
   
      2.3126    1.7950    1.8325    2.0244    1.7288    0.9785    1.4907    2.1414
      2.0431    1.3114    1.7342    1.6201    1.3709    1.2393    1.4536    1.7884
      1.5444    1.5262    1.5602    1.5039    1.5373    1.1434    0.7315    1.7044
      2.4534    2.3621    2.6867    2.5047    2.2182    2.0730    1.9863    2.7933
      2.3246    1.7286    2.2614    2.2560    1.9884    1.9830    2.0355    2.2416
      1.3870    1.3453    1.8233    1.5979    1.1114    1.6511    1.6925    1.7670
      2.2006    1.9486    1.6555    2.0099    2.4886    0.8666    0.9892    1.9677
      2.4239    1.4528    1.9309    1.8741    1.5787    1.3616    1.6029    1.8924
   
   D = 
   
      2.3126    1.7950    1.8325    2.0244    1.7288    0.9785    1.4907    2.1414
      2.0431    1.3114    1.7342    1.6201    1.3709    1.2393    1.4536    1.7884
      1.5444    1.5262    1.5602    1.5039    1.5373    1.1434    0.7315    1.7044
      2.4534    2.3621    2.6867    2.5047    2.2182    2.0730    1.9863    2.7933
      2.3246    1.7286    2.2614    2.2560    1.9884    1.9830    2.0355    2.2416
      1.3870    1.3453    1.8233    1.5979    1.1114    1.6511    1.6925    1.7670
      2.2006    1.9486    1.6555    2.0099    2.4886    0.8666    0.9892    1.9677
      2.4239    1.4528    1.9309    1.8741    1.5787    1.3616    1.6029    1.8924
   


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

   
      0.8740    0.4347    0.0930    0.3997    0.8124    0.2836
      0.3535    0.0032    0.6393    0.0190    0.0530    0.1902
      0.6930    0.5095    0.4169    0.3343    0.2856    0.1427
      0.8663    0.7815    0.2594    0.9691    0.5707    0.3023
      0.3937    0.3244    0.8283    0.3051    0.0308    0.8039
   
   
      0.8740
      0.6930
      0.8663
      0.5095
      0.7815
      0.6393
      0.8283
      0.9691
      0.8124
      0.5707
      0.8039
   

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

   
      0.7253    3.5893    1.2423    9.2511    0.0781    7.9848
      8.4240    7.5213    4.4181    6.0828    8.1716    2.4718
      3.1034    4.3424    2.2006    5.1823    6.2228    7.7831
      1.2926    1.2213    9.1502    0.4276    7.0062    4.1821
      0.9469    8.1498    7.5390    2.2556    1.7966    9.9942
   
   
      0.0000    0.0000    0.0000    9.2511    0.0000    7.9848
      8.4240    7.5213    0.0000    6.0828    8.1716    0.0000
      0.0000    0.0000    0.0000    5.1823    6.2228    7.7831
      0.0000    0.0000    9.1502    0.0000    7.0062    0.0000
      0.0000    8.1498    7.5390    0.0000    0.0000    9.9942
   
   
      0.0000    0.0000    0.0000       NaN    0.0000    7.9848
      8.4240    7.5213    0.0000    6.0828    8.1716    0.0000
      0.0000    0.0000    0.0000    5.1823    6.2228    7.7831
      0.0000    0.0000       NaN    0.0000    7.0062    0.0000
      0.0000    8.1498    7.5390    0.0000    0.0000       NaN
   

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

   
      4.7838    0.3968    0.5603    2.6293    6.5000    2.9165
      9.5474    0.1443    6.5000    6.5000    1.4470    4.2298
      1.7423    6.5000    3.9284    1.4486    6.5000    8.4360
      1.3708    1.4489    9.1271    9.4249    6.5000    9.5775
      1.7393    2.7383    6.5000    3.6874    6.5000    8.9591
   
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
   
