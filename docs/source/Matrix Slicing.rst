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
      0.8139    0.8099    0.3516    0.3163
   
   R1[2] = 0.3515983896333421
   C1 = 
      0.6567
      0.0492
      0.0335
      0.5612
      0.0125
      0.7069
      0.7853
      0.4184
   
   C1[5] = 0.7068976831340411

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
      0.9117    0.8336    0.7692    0.1097    0.0659
      0.5026    0.9194    0.7230    0.6536    0.8743
   

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
   
      0.8412    0.4476    0.7627    0.0851    0.2281    0.3305    0.5975    0.4231
      0.9210    0.7463    0.7294    0.4581    0.6243    0.0170    0.8515    0.0520
      0.0251    0.9317    0.0786    0.5360    0.9650    0.4129    0.2142    0.1192
      0.1248    0.4952    0.6249    0.1923    0.5702    0.8174    0.6106    0.3971
      0.5878    0.5026    0.3266    0.9150    0.7778    0.4644    0.7764    0.3917
      0.0356    0.9455    0.4890    0.5124    0.5235    0.6890    0.4429    0.8564
      0.4073    0.0938    0.1645    0.0528    0.4555    0.5364    0.3679    0.0725
      0.8972    0.6344    0.8369    0.1856    0.5021    0.0611    0.3966    0.5374
   
   B = 
   
      0.7449    0.9743    0.5850    0.6584    0.0740    0.5118    0.8893    0.0768
      0.6023    0.6555    0.0427    0.7007    0.3012    0.9928    0.7474    0.1082
      0.7951    0.7377    0.2856    0.1831    0.8192    0.2663    0.1494    0.7615
      0.8972    0.8689    0.8253    0.1900    0.1316    0.4920    0.5910    0.8100
      0.1450    0.7094    0.8019    0.8307    0.9301    0.1388    0.1209    0.8993
      0.4100    0.8333    0.8204    0.9265    0.5256    0.5131    0.9674    0.7991
      0.3211    0.9171    0.5591    0.0257    0.2329    0.6968    0.8084    0.6748
      0.0431    0.4473    0.8974    0.9973    0.3575    0.3465    0.8596    0.9944
   
   C = 
   
      1.9577    2.9240    1.9670    1.9563    1.5093    1.8840    2.4409    2.0559
      2.4996    3.5838    2.1942    1.9578    1.7572    2.3384    2.5814    2.2793
      1.5064    2.4374    1.8586    2.0941    1.6245    1.7588    1.8389    2.0577
      1.6916    2.8975    2.2568    2.2226    1.9397    1.8781    2.3827    2.6677
      2.3905    3.7641    3.0039    2.4597    1.8712    2.3598    2.9955    3.0734
      1.9821    3.1953    2.6249    2.8115    2.0138    2.3708    2.9382    3.0644
      0.9453    1.7656    1.4090    1.3311    1.0173    0.9913    1.4218    1.3682
      2.1307    3.0798    2.1009    2.2436    1.7511    1.9667    2.4091    2.2277
   
   D = 
   
      1.9577    2.9240    1.9670    1.9563    1.5093    1.8840    2.4409    2.0559
      2.4996    3.5838    2.1942    1.9578    1.7572    2.3384    2.5814    2.2793
      1.5064    2.4374    1.8586    2.0941    1.6245    1.7588    1.8389    2.0577
      1.6916    2.8975    2.2568    2.2226    1.9397    1.8781    2.3827    2.6677
      2.3905    3.7641    3.0039    2.4597    1.8712    2.3598    2.9955    3.0734
      1.9821    3.1953    2.6249    2.8115    2.0138    2.3708    2.9382    3.0644
      0.9453    1.7656    1.4090    1.3311    1.0173    0.9913    1.4218    1.3682
      2.1307    3.0798    2.1009    2.2436    1.7511    1.9667    2.4091    2.2277
   


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

   
      0.5031    0.8893    0.0041    0.3700    0.8271    0.7596
      0.3514    0.2635    0.3030    0.8532    0.2044    0.4827
      0.8103    0.6732    0.0289    0.3764    0.5524    0.7415
      0.4440    0.6601    0.1246    0.2051    0.6935    0.4690
      0.5298    0.9194    0.1210    0.8688    0.8761    0.2888
   
   
      0.5031
      0.8103
      0.5298
      0.8893
      0.6732
      0.6601
      0.9194
      0.8532
      0.8688
      0.8271
      0.5524
      0.6935
      0.8761
      0.7596
      0.7415
   

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

   
      3.4263    4.1290    1.2086    9.9933    6.8274    1.5264
      3.8647    0.9914    7.5415    2.3562    3.9257    3.9090
      4.7958    2.7117    0.1886    6.3825    0.9354    1.0022
      6.6448    7.3839    0.2812    1.8687    1.3655    0.4776
      3.0036    7.8006    4.3311    4.7135    1.0470    3.8707
   
   
      0.0000    0.0000    0.0000    9.9933    6.8274    0.0000
      0.0000    0.0000    7.5415    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    6.3825    0.0000    0.0000
      6.6448    7.3839    0.0000    0.0000    0.0000    0.0000
      0.0000    7.8006    0.0000    0.0000    0.0000    0.0000
   
   
      0.0000    0.0000    0.0000       NaN    6.8274    0.0000
      0.0000    0.0000    7.5415    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    6.3825    0.0000    0.0000
      6.6448    7.3839    0.0000    0.0000    0.0000    0.0000
      0.0000    7.8006    0.0000    0.0000    0.0000    0.0000
   

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

   
      2.9735    3.6799    9.8414    0.7286    6.5000    9.9024
      4.3855    6.5000    1.3330    0.2721    6.5000    9.9509
      3.9615    6.5000    9.5303    3.0869    2.4818    6.5000
      9.1146    3.9870    6.5000    1.7157    6.5000    3.6763
      3.3922    1.5580    9.6956    6.5000    8.0394    6.5000
   
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
   
