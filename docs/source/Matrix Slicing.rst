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
      0.1873    0.7640    0.2212    0.8355
   
   R1[2] = 0.22124947132236839
   C1 = 
      0.3854
      0.2468
      0.1089
      0.5886
      0.0924
      0.4238
      0.1423
      0.9821
   
   C1[5] = 0.42377015534836926

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
      0.0611    0.9317    0.4898    0.4598    0.2889
      0.5444    0.3192    0.3306    0.2755    0.1010
   

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
   
      0.8619    0.0491    0.9791    0.5185    0.8857    0.7109    0.8024    0.3883
      0.0101    0.8590    0.7734    0.2088    0.4274    0.5767    0.6738    0.0714
      0.5166    0.7258    0.4304    0.8153    0.2181    0.3998    0.9239    0.1051
      0.1607    0.0704    0.2974    0.8774    0.2517    0.2878    0.5983    0.6323
      0.6030    0.8430    0.2341    0.6052    0.0203    0.0165    0.7861    0.3184
      0.5642    0.7910    0.5532    0.5338    0.7857    0.7234    0.8263    0.6342
      0.2842    0.9728    0.8560    0.7022    0.4585    0.2673    0.6397    0.0380
      0.6861    0.4514    0.4548    0.0582    0.9886    0.3947    0.6853    0.1130
   
   B = 
   
      0.7321    0.2601    0.8986    0.5159    0.2762    0.4486    0.7015    0.7787
      0.0799    0.9291    0.9206    0.7084    0.8876    0.9405    0.4711    0.3140
      0.3157    0.5674    0.8781    0.8289    0.4194    0.0491    0.2135    0.6063
      0.8853    0.4774    0.4587    0.6400    0.2801    0.3548    0.1904    0.7937
      0.1243    0.7018    0.3949    0.4084    0.4140    0.9446    0.1136    0.8821
      0.7757    0.9194    0.3685    0.0906    0.4640    0.2820    0.1548    0.6676
      0.0717    0.7278    0.1950    0.4029    0.2706    0.6713    0.5345    0.1564
      0.2946    0.7647    0.8536    0.0670    0.8888    0.6017    0.7508    0.3418
   
   C = 
   
      2.2365    3.2289    3.0169    2.3983    2.0963    2.4742    1.8666    3.2059
      1.0749    2.7143    2.1485    1.8916    1.8384    1.9861    1.1682    1.8042
      1.7283    2.7155    2.3877    2.1639    1.8150    2.2269    1.6109    2.1780
      1.4777    2.0550    1.7347    1.3531    1.4390    1.5652    1.2441    1.7479
      1.2840    2.2067    2.2402    1.8375    1.6942    2.0326    1.6494    1.6172
      2.0284    3.7532    3.2452    2.4135    2.6877    3.0960    2.1072    2.9690
      1.4991    2.8608    2.6614    2.4665    2.0183    2.2944    1.4381    2.2991
      1.2449    2.5255    2.2241    1.8111    1.6755    2.3484    1.4266    2.2793
   
   D = 
   
      2.2365    3.2289    3.0169    2.3983    2.0963    2.4742    1.8666    3.2059
      1.0749    2.7143    2.1485    1.8916    1.8384    1.9861    1.1682    1.8042
      1.7283    2.7155    2.3877    2.1639    1.8150    2.2269    1.6109    2.1780
      1.4777    2.0550    1.7347    1.3531    1.4390    1.5652    1.2441    1.7479
      1.2840    2.2067    2.2402    1.8375    1.6942    2.0326    1.6494    1.6172
      2.0284    3.7532    3.2452    2.4135    2.6877    3.0960    2.1072    2.9690
      1.4991    2.8608    2.6614    2.4665    2.0183    2.2944    1.4381    2.2991
      1.2449    2.5255    2.2241    1.8111    1.6755    2.3484    1.4266    2.2793
   


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

   
      0.6113    0.3128    0.3816    0.0245    0.4417    0.9274
      0.0318    0.8825    0.2217    0.8367    0.5634    0.1078
      0.6380    0.8323    0.4667    0.0656    0.7342    0.1121
      0.9805    0.3870    0.8079    0.6394    0.2569    0.4398
      0.1042    0.8377    0.0619    0.3466    0.1315    0.1714
   
   
      0.6113
      0.6380
      0.9805
      0.8825
      0.8323
      0.8377
      0.8079
      0.8367
      0.6394
      0.5634
      0.7342
      0.9274
   

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

   
      3.3902    3.2444    4.1497    3.4672    7.2175    6.4110
      0.7654    5.7211    3.6890    1.1423    2.3449    7.4804
      7.1561    4.8873    0.0883    0.0435    8.8953    6.7754
      0.5469    5.5512    1.2408    5.9591    9.1268    5.0726
      7.3246    0.6758    4.2653    1.7983    0.8059    3.3087
   
   
      0.0000    0.0000    0.0000    0.0000    7.2175    6.4110
      0.0000    5.7211    0.0000    0.0000    0.0000    7.4804
      7.1561    0.0000    0.0000    0.0000    8.8953    6.7754
      0.0000    5.5512    0.0000    5.9591    9.1268    5.0726
      7.3246    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    7.2175    6.4110
      0.0000    5.7211    0.0000    0.0000    0.0000    7.4804
      7.1561    0.0000    0.0000    0.0000    8.8953    6.7754
      0.0000    5.5512    0.0000    5.9591       NaN    5.0726
      7.3246    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      9.4034    6.5000    2.7640    9.9533    9.4143    1.0073
      6.5000    0.6895    4.6908    1.2884    9.2034    6.5000
      3.6195    2.7926    6.5000    9.6115    6.5000    0.5727
      3.2869    8.6874    9.5462    6.5000    6.5000    1.9502
      9.6115    6.5000    6.5000    1.4431    9.7291    9.1176
   
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
   
