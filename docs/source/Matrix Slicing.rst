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
      0.6351    0.1582    0.8113    0.1782
   
   R1[2] = 0.8112665101567662
   C1 = 
      0.7455
      0.8830
      0.9191
      0.3361
      0.9204
      0.9639
      0.6013
      0.8262
   
   C1[5] = 0.9639394218545335

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
      0.7817    0.9507    0.6569    0.8065    0.8943
      0.2193    0.7061    0.0114    0.7753    0.5008
   

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
   
      0.6760    0.1301    0.7963    0.8305    0.0977    0.9018    0.2862    0.6074
      0.8002    0.1755    0.1073    0.6998    0.0837    0.5943    0.4242    0.3982
      0.8928    0.0039    0.8685    0.0234    0.1137    0.3879    0.5562    0.4487
      0.9101    0.8747    0.3602    0.2740    0.2545    0.1254    0.7022    0.4144
      0.8706    0.9388    0.9102    0.7243    0.9768    0.7567    0.0559    0.8372
      0.2591    0.4620    0.4573    0.6625    0.1765    0.2103    0.4681    0.5356
      0.5761    0.9280    0.7097    0.1978    0.2352    0.4199    0.5241    0.4338
      0.3766    0.5683    0.8301    0.4378    0.8039    0.4943    0.1258    0.7078
   
   B = 
   
      0.9582    0.9463    0.5701    0.1807    0.8325    0.8775    0.3584    0.1842
      0.3712    0.2421    0.2729    0.1609    0.7376    0.8186    0.8809    0.9662
      0.3054    0.8235    0.6749    0.0025    0.8712    0.8070    0.2348    0.7453
      0.6035    0.0983    0.1500    0.5880    0.8264    0.9079    0.4767    0.6589
      0.5665    0.0002    0.0128    0.2079    0.2839    0.9465    0.8459    0.2999
      0.2752    0.2644    0.6964    0.5806    0.6118    0.6618    0.4846    0.2583
      0.1083    0.3170    0.5578    0.1409    0.3920    0.4647    0.5105    0.7259
      0.0945    0.0003    0.9372    0.9874    0.6713    0.7193    0.3083    0.1743
   
   C = 
   
      1.8324    1.7379    2.4409    1.8173    3.1382    3.3554    1.7929    1.9668
      1.5815    1.2486    1.7061    1.3999    2.2884    2.5238    1.4983    1.4140
      1.4102    1.8424    2.1020    0.9481    2.3111    2.4544    1.2450    1.4473
      1.7660    1.6525    1.9124    1.1010    2.6455    3.0024    2.0742    2.1525
      2.7446    2.0900    2.8307    2.2134    4.1330    4.9781    3.1777    2.8980
      1.2184    1.0029    1.5935    1.2653    2.2242    2.4851    1.5784    1.8120
      1.5792    1.6510    2.0847    1.1664    2.7662    3.0736    2.0886    2.2972
      1.7616    1.3915    2.0837    1.5896    2.8728    3.5187    2.2413    2.1091
   
   D = 
   
      1.8324    1.7379    2.4409    1.8173    3.1382    3.3554    1.7929    1.9668
      1.5815    1.2486    1.7061    1.3999    2.2884    2.5238    1.4983    1.4140
      1.4102    1.8424    2.1020    0.9481    2.3111    2.4544    1.2450    1.4473
      1.7660    1.6525    1.9124    1.1010    2.6455    3.0024    2.0742    2.1525
      2.7446    2.0900    2.8307    2.2134    4.1330    4.9781    3.1777    2.8980
      1.2184    1.0029    1.5935    1.2653    2.2242    2.4851    1.5784    1.8120
      1.5792    1.6510    2.0847    1.1664    2.7662    3.0736    2.0886    2.2972
      1.7616    1.3915    2.0837    1.5896    2.8728    3.5187    2.2413    2.1091
   


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

   
      0.8319    0.0067    0.3958    0.0214    0.7128    0.4818
      0.8091    0.1132    0.0770    0.5220    0.5389    0.5228
      0.3027    0.1273    0.5049    0.1871    0.9236    0.7880
      0.7725    0.0504    0.6172    0.0450    0.6968    0.5188
      0.3824    0.2491    0.1793    0.8535    0.6642    0.2595
   
   
      0.8319
      0.8091
      0.7725
      0.5049
      0.6172
      0.5220
      0.8535
      0.7128
      0.5389
      0.9236
      0.6968
      0.6642
      0.5228
      0.7880
      0.5188
   

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

   
      8.8169    0.1781    2.0680    7.0436    4.6816    5.1610
      8.5050    4.1982    7.7350    7.2414    9.6366    1.7613
      3.8842    4.0975    5.4420    0.4756    9.0860    5.9859
      8.1892    2.0857    4.3284    4.8396    1.7800    7.5723
      5.5888    9.4693    4.3387    9.6406    1.7114    4.3384
   
   
      8.8169    0.0000    0.0000    7.0436    0.0000    5.1610
      8.5050    0.0000    7.7350    7.2414    9.6366    0.0000
      0.0000    0.0000    5.4420    0.0000    9.0860    5.9859
      8.1892    0.0000    0.0000    0.0000    0.0000    7.5723
      5.5888    9.4693    0.0000    9.6406    0.0000    0.0000
   
   
      8.8169    0.0000    0.0000    7.0436    0.0000    5.1610
      8.5050    0.0000    7.7350    7.2414       NaN    0.0000
      0.0000    0.0000    5.4420    0.0000       NaN    5.9859
      8.1892    0.0000    0.0000    0.0000    0.0000    7.5723
      5.5888       NaN    0.0000       NaN    0.0000    0.0000
   

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

   
      6.5000    4.9557    2.0884    6.5000    8.1279    3.7760
      4.2851    6.5000    6.5000    9.6871    1.4050    1.1721
      2.5776    6.5000    8.4645    6.5000    3.7157    8.0235
      4.0954    4.2901    1.0824    3.9479    0.3169    2.2198
      9.4162    9.6764    2.1741    4.2068    2.9020    4.0248
   
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
   
