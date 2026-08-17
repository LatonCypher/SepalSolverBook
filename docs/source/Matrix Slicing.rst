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
      0.4877    0.4556    0.4526    0.8755
   
   R1[2] = 0.4526435073406415
   C1 = 
      0.4144
      0.4223
      0.8627
      0.9511
      0.3213
      0.7038
      0.9296
      0.0758
   
   C1[5] = 0.7037683902534903

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
      0.5711    0.3149    0.2075    0.4048    0.5530
      0.5750    0.9273    0.3021    0.3702    0.2237
   

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
   
      0.3786    0.0725    0.0903    0.6211    0.0759    0.7432    0.8891    0.7236
      0.2495    0.6981    0.2629    0.8257    0.3180    0.5454    0.3882    0.7120
      0.9394    0.0213    0.4974    0.9961    0.0817    0.4872    0.9141    0.6719
      0.3533    0.6830    0.2687    0.9562    0.8470    0.7812    0.6386    0.8239
      0.4410    0.5668    0.0973    0.0504    0.9643    0.7007    0.7995    0.6723
      0.8921    0.9014    0.8340    0.6424    0.3618    0.0039    0.2500    0.8648
      0.0253    0.2444    0.3148    0.9444    0.4540    0.0510    0.5604    0.3957
      0.6174    0.5638    0.8992    0.7244    0.1758    0.2118    0.0225    0.9098
   
   B = 
   
      0.8783    0.5021    0.3910    0.0922    0.4127    0.2585    0.4988    0.3354
      0.9817    0.1017    0.4515    0.0250    0.6552    0.2675    0.6596    0.8112
      0.2290    0.1677    0.4153    0.2197    0.0286    0.5614    0.5082    0.1017
      0.0701    0.2950    0.0075    0.7557    0.9979    0.4417    0.4911    0.8334
      0.8606    0.2229    0.8935    0.2797    0.0394    0.4179    0.5023    0.3994
      0.2174    0.0856    0.9624    0.4688    0.6455    0.5577    0.4545    0.8518
      0.9759    0.0465    0.3142    0.3176    0.6123    0.8817    0.7016    0.3737
      0.9597    0.3171    0.1920    0.2454    0.0726    0.5404    0.2701    0.2511
   
   C = 
   
      2.2570    0.7472    1.4243    1.3556    1.9058    2.0635    1.7828    1.8899
      2.4769    0.8453    1.5958    1.3649    2.0458    1.9277    1.9963    2.2802
      2.7429    1.1667    1.5491    1.6558    2.3361    2.4428    2.3100    2.1710
      3.4221    1.1206    2.4327    1.8395    2.5436    2.6453    2.6840    2.9459
      3.3772    0.8355    2.3854    1.1315    1.6350    2.2045    2.2133    2.1090
      3.2906    1.2358    1.6785    1.1682    1.8562    2.0649    2.3713    2.1089
      1.7289    0.6261    0.9648    1.2173    1.5446    1.5920    1.5488    1.5592
      2.4449    1.0788    1.4175    1.1950    1.5963    1.8384    1.9387    1.8470
   
   D = 
   
      2.2570    0.7472    1.4243    1.3556    1.9058    2.0635    1.7828    1.8899
      2.4769    0.8453    1.5958    1.3649    2.0458    1.9277    1.9963    2.2802
      2.7429    1.1667    1.5491    1.6558    2.3361    2.4428    2.3100    2.1710
      3.4221    1.1206    2.4327    1.8395    2.5436    2.6453    2.6840    2.9459
      3.3772    0.8355    2.3854    1.1315    1.6350    2.2045    2.2133    2.1090
      3.2906    1.2358    1.6785    1.1682    1.8562    2.0649    2.3713    2.1089
      1.7289    0.6261    0.9648    1.2173    1.5446    1.5920    1.5488    1.5592
      2.4449    1.0788    1.4175    1.1950    1.5963    1.8384    1.9387    1.8470
   


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

   
      0.6598    0.9389    0.4695    0.0317    0.2119    0.3872
      0.4362    0.2737    0.1417    0.2362    0.2570    0.8254
      0.8746    0.8644    0.8711    0.4586    0.4912    0.8827
      0.3318    0.2261    0.0988    0.0638    0.0028    0.1170
      0.6904    0.3142    0.8039    0.2232    0.3031    0.4798
   
   
      0.6598
      0.8746
      0.6904
      0.9389
      0.8644
      0.8711
      0.8039
      0.8254
      0.8827
   

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

   
      4.7301    2.3469    2.6687    9.4427    2.0204    1.6984
      3.5060    1.6082    3.1892    8.7384    9.2386    7.1625
      2.3444    3.5424    6.9684    2.5509    3.5459    6.3753
      4.6138    1.7928    4.4578    7.4756    7.8929    2.7537
      9.3584    5.6297    1.4318    2.2424    5.7877    2.8355
   
   
      0.0000    0.0000    0.0000    9.4427    0.0000    0.0000
      0.0000    0.0000    0.0000    8.7384    9.2386    7.1625
      0.0000    0.0000    6.9684    0.0000    0.0000    6.3753
      0.0000    0.0000    0.0000    7.4756    7.8929    0.0000
      9.3584    5.6297    0.0000    0.0000    5.7877    0.0000
   
   
      0.0000    0.0000    0.0000       NaN    0.0000    0.0000
      0.0000    0.0000    0.0000    8.7384       NaN    7.1625
      0.0000    0.0000    6.9684    0.0000    0.0000    6.3753
      0.0000    0.0000    0.0000    7.4756    7.8929    0.0000
         NaN    5.6297    0.0000    0.0000    5.7877    0.0000
   

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

   
      6.5000    1.1698    4.0700    6.5000    0.5990    6.5000
      4.3965    6.5000    9.7011    6.5000    9.8771    3.7733
      3.4543    3.7167    9.6270    6.5000    3.0725    6.5000
      8.4794    6.5000    4.2145    3.8929    8.8677    6.5000
      0.3955    2.5184    4.3365    6.5000    3.7868    8.8185
   
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
   
