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
      0.0284    0.4199    0.2546    0.6886
   
   R1[2] = 0.25463727163177974
   C1 = 
      0.6670
      0.8242
      0.0975
      0.6869
      0.5293
      0.6453
      0.5601
      0.3276
   
   C1[5] = 0.6453199593975646

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
      0.2217    0.6435    0.4565    0.9750    0.3292
      0.6974    0.5614    0.6032    0.6217    0.4338
   

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
   
      0.9685    0.1468    0.2764    0.2684    0.5928    0.7608    0.3537    0.6393
      0.5262    0.8917    0.0302    0.9704    0.3257    0.8501    0.3914    0.3531
      0.8159    0.3049    0.0375    0.9641    0.6815    0.5837    0.7510    0.5640
      0.8685    0.8774    0.1767    0.2794    0.3384    0.6748    0.9896    0.6044
      0.8512    0.2728    0.6143    0.8387    0.4595    0.3685    0.0484    0.0892
      0.1552    0.2683    0.5546    0.2721    0.7848    0.5197    0.3472    0.5993
      0.3752    0.8727    0.8858    0.9836    0.1201    0.9158    0.3662    0.5562
      0.3257    0.8343    0.3081    0.9143    0.4511    0.4051    0.1225    0.1838
   
   B = 
   
      0.6564    0.6920    0.9753    0.8274    0.3693    0.7934    0.6646    0.1710
      0.4055    0.4716    0.1689    0.1495    0.5436    0.7149    0.3349    0.7495
      0.3451    0.5060    0.6268    0.5359    0.0878    0.9197    0.6983    0.1099
      0.5671    0.1825    0.6847    0.4896    0.3361    0.7038    0.2406    0.0636
      0.9305    0.8990    0.2683    0.1784    0.2922    0.6916    0.9276    0.4946
      0.9259    0.9492    0.7831    0.1196    0.3642    0.8691    0.3539    0.3320
      0.5237    0.7790    0.6020    0.3985    0.3229    0.3031    0.6367    0.5579
      0.9168    0.6576    0.1616    0.5684    0.1726    0.3512    0.4838    0.3637
   
   C = 
   
      2.9703    2.8793    2.3975    1.8040    1.2269    2.7194    2.3041    1.2987
      2.8866    2.6138    2.3929    1.5765    1.6000    2.9723    1.9259    1.6134
      3.3037    3.0258    2.7139    2.0240    1.5459    2.9824    2.4922    1.5883
      3.1574    3.2683    2.6100    1.9603    1.6757    3.0081    2.5369    2.0065
      2.2327    2.0407    2.2909    1.6809    1.0980    2.7096    1.9184    0.8798
      2.4991    2.4276    1.6540    1.2802    0.9775    2.3266    2.0687    1.2782
      3.1251    2.9271    2.8017    1.9902    1.6042    3.6137    2.3347    1.6482
      2.2044    1.9480    1.8194    1.2892    1.2588    2.5473    1.6598    1.2658
   
   D = 
   
      2.9703    2.8793    2.3975    1.8040    1.2269    2.7194    2.3041    1.2987
      2.8866    2.6138    2.3929    1.5765    1.6000    2.9723    1.9259    1.6134
      3.3037    3.0258    2.7139    2.0240    1.5459    2.9824    2.4922    1.5883
      3.1574    3.2683    2.6100    1.9603    1.6757    3.0081    2.5369    2.0065
      2.2327    2.0407    2.2909    1.6809    1.0980    2.7096    1.9184    0.8798
      2.4991    2.4276    1.6540    1.2802    0.9775    2.3266    2.0687    1.2782
      3.1251    2.9271    2.8017    1.9902    1.6042    3.6137    2.3347    1.6482
      2.2044    1.9480    1.8194    1.2892    1.2588    2.5473    1.6598    1.2658
   


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

   
      0.4971    0.7513    0.9833    0.3580    0.3058    0.8325
      0.2340    0.5818    0.7932    0.3543    0.8023    0.5782
      0.2786    0.8022    0.3360    0.2716    0.3667    0.8022
      0.5853    0.7771    0.0536    0.2744    0.9146    0.1817
      0.5676    0.9287    0.1125    0.9390    0.2866    0.4103
   
   
      0.5853
      0.5676
      0.7513
      0.5818
      0.8022
      0.7771
      0.9287
      0.9833
      0.7932
      0.9390
      0.8023
      0.9146
      0.8325
      0.5782
      0.8022
   

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

   
      3.2237    4.7719    7.8219    9.3853    3.3738    9.8147
      3.8502    1.6868    9.9619    4.7816    0.9537    6.3355
      2.1670    7.0382    1.7118    6.9136    8.6256    7.3417
      5.7184    3.7506    1.4270    2.9668    0.2790    8.0262
      2.2022    6.2648    2.8965    7.9864    0.2321    0.4796
   
   
      0.0000    0.0000    7.8219    9.3853    0.0000    9.8147
      0.0000    0.0000    9.9619    0.0000    0.0000    6.3355
      0.0000    7.0382    0.0000    6.9136    8.6256    7.3417
      5.7184    0.0000    0.0000    0.0000    0.0000    8.0262
      0.0000    6.2648    0.0000    7.9864    0.0000    0.0000
   
   
      0.0000    0.0000    7.8219       NaN    0.0000       NaN
      0.0000    0.0000       NaN    0.0000    0.0000    6.3355
      0.0000    7.0382    0.0000    6.9136    8.6256    7.3417
      5.7184    0.0000    0.0000    0.0000    0.0000    8.0262
      0.0000    6.2648    0.0000    7.9864    0.0000    0.0000
   

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

   
      0.7103    1.6562    4.1411    6.5000    1.8736    9.8135
      1.6870    6.5000    6.5000    2.8306    0.3350    1.3989
      9.0829    3.1645    8.7128    2.4776    2.8526    4.5144
      6.5000    6.5000    0.1972    8.2481    0.7942    3.2527
      2.3261    6.5000    4.8190    2.4470    1.9169    8.9876
   
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
   
