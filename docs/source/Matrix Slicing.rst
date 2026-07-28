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
      0.9624    0.5469    0.0285    0.7164
   
   R1[2] = 0.02850585512292636
   C1 = 
      0.5397
      0.2347
      0.6898
      0.2082
      0.5971
      0.4343
      0.7260
      0.4305
   
   C1[5] = 0.43432028850800464

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
      0.6947    0.3684    0.2271    0.6338    0.6104
      0.7987    0.1003    0.2555    0.2853    0.2518
   

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
   
      0.5703    0.6022    0.0152    0.3618    0.7165    0.6348    0.4249    0.4882
      0.4991    0.2407    0.4576    0.3419    0.8183    0.2234    0.4112    0.8053
      0.7085    0.1364    0.5546    0.5256    0.1726    0.2377    0.0658    0.6922
      0.7995    0.6401    0.8775    0.2646    0.3607    0.3061    0.5037    0.0377
      0.4110    0.0798    0.3699    0.2591    0.0308    0.0452    0.3727    0.9886
      0.0162    0.2537    0.9341    0.8767    0.8397    0.1305    0.3936    0.4634
      0.1099    0.6143    0.3571    0.8994    0.4230    0.0963    0.4675    0.0165
      0.8010    0.5301    0.9127    0.9801    0.5239    0.4774    0.3104    0.5783
   
   B = 
   
      0.4754    0.1736    0.3284    0.1403    0.3839    0.9406    0.0702    0.6700
      0.6799    0.0848    0.9438    0.5127    0.8500    0.8934    0.7729    0.8138
      0.8307    0.0985    0.8414    0.1566    0.5314    0.8106    0.9176    0.5643
      0.2828    0.1984    0.3915    0.9639    0.9115    0.2994    0.9497    0.2335
      0.0286    0.2838    0.3273    0.0232    0.5363    0.8760    0.6431    0.1280
      0.6158    0.4168    0.3096    0.5517    0.4521    0.8336    0.6952    0.2897
      0.6912    0.9635    0.7504    0.7668    0.2910    0.5642    0.1540    0.1615
      0.7404    0.9299    0.2361    0.3607    0.7418    0.0139    0.0459    0.8233
   
   C = 
   
      1.8622    1.5546    1.7754    1.6087    2.2258    2.5985    1.8531    1.7114
      1.9192    1.6903    1.7457    1.3426    2.2079    2.3040    1.7476    1.7671
      1.7483    1.1485    1.3768    1.1981    1.8944    1.7913    1.4815    1.6929
      2.1941    1.0824    2.3085    1.4099    2.0649    2.9704    2.1316    1.8609
      1.6485    1.4719    1.1602    1.0742    1.5371    1.1240    0.8301    1.5007
      1.9236    1.3930    2.0940    1.6840    2.4850    2.3345    2.5996    1.5394
      1.4277    0.9107    1.7915    1.6806    1.9926    1.9259    2.0761    1.1562
      2.7283    1.6527    2.6038    2.1939    3.1525    3.3003    2.9774    2.4435
   
   D = 
   
      1.8622    1.5546    1.7754    1.6087    2.2258    2.5985    1.8531    1.7114
      1.9192    1.6903    1.7457    1.3426    2.2079    2.3040    1.7476    1.7671
      1.7483    1.1485    1.3768    1.1981    1.8944    1.7913    1.4815    1.6929
      2.1941    1.0824    2.3085    1.4099    2.0649    2.9704    2.1316    1.8609
      1.6485    1.4719    1.1602    1.0742    1.5371    1.1240    0.8301    1.5007
      1.9236    1.3930    2.0940    1.6840    2.4850    2.3345    2.5996    1.5394
      1.4277    0.9107    1.7915    1.6806    1.9926    1.9259    2.0761    1.1562
      2.7283    1.6527    2.6038    2.1939    3.1525    3.3003    2.9774    2.4435
   


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

   
      0.9194    0.7050    0.5379    0.0877    0.5365    0.0351
      0.8102    0.6970    0.6625    0.6519    0.6535    0.7933
      0.1560    0.2888    0.8013    0.9573    0.2558    0.7406
      0.1009    0.9320    0.2341    0.8634    0.7991    0.7870
      0.2190    0.3736    0.2262    0.2485    0.5521    0.9258
   
   
      0.9194
      0.8102
      0.7050
      0.6970
      0.9320
      0.5379
      0.6625
      0.8013
      0.6519
      0.9573
      0.8634
      0.5365
      0.6535
      0.7991
      0.5521
      0.7933
      0.7406
      0.7870
      0.9258
   

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

   
      9.2128    4.2822    2.0317    8.4167    3.4549    9.8738
      5.8845    9.9846    3.1350    3.8530    0.5440    1.4328
      8.2372    7.7007    4.0541    7.8159    8.7199    2.5986
      4.6853    0.9809    1.1564    8.9917    1.8854    2.5496
      1.0263    4.9849    2.8067    7.0643    9.8433    6.8895
   
   
      9.2128    0.0000    0.0000    8.4167    0.0000    9.8738
      5.8845    9.9846    0.0000    0.0000    0.0000    0.0000
      8.2372    7.7007    0.0000    7.8159    8.7199    0.0000
      0.0000    0.0000    0.0000    8.9917    0.0000    0.0000
      0.0000    0.0000    0.0000    7.0643    9.8433    6.8895
   
   
         NaN    0.0000    0.0000    8.4167    0.0000       NaN
      5.8845       NaN    0.0000    0.0000    0.0000    0.0000
      8.2372    7.7007    0.0000    7.8159    8.7199    0.0000
      0.0000    0.0000    0.0000    8.9917    0.0000    0.0000
      0.0000    0.0000    0.0000    7.0643       NaN    6.8895
   

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

   
      0.3472    6.5000    8.6542    2.7797    2.8105    1.2369
      1.8349    4.8109    3.4308    1.4364    4.5265    0.8889
      6.5000    1.9542    0.0573    8.9771    8.7189    6.5000
      8.5974    0.5990    6.5000    6.5000    6.5000    8.2573
      6.5000    6.5000    0.4974    3.2488    3.8349    1.2613
   
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
   
