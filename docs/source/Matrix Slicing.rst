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
      0.4665    0.7801    0.7782    0.7896
   
   R1[2] = 0.7781592301775857
   C1 = 
      0.1281
      0.0493
      0.6064
      0.4421
      0.9405
      0.0884
      0.0180
      0.7932
   
   C1[5] = 0.08842050338142027

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
      0.9785    0.0402    0.4833    0.3131    0.5591
      0.3558    0.6663    0.9845    0.4419    0.1173
   

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
   
      0.7125    0.6767    0.9252    0.0192    0.3924    0.9942    0.6935    0.3023
      0.8828    0.4204    0.6094    0.7067    0.9319    0.0612    0.9207    0.0740
      0.7968    0.9769    0.4117    0.4779    0.0759    0.7374    0.6821    0.1145
      0.4843    0.0866    0.0471    0.4834    0.3985    0.8201    0.1531    0.1477
      0.6449    0.7524    0.6710    0.6762    0.4565    0.0838    0.8668    0.8838
      0.1692    0.9010    0.3970    0.4868    0.3526    0.7505    0.2936    0.5168
      0.5817    0.0920    0.6958    0.1406    0.0045    0.5880    0.7253    0.4480
      0.3501    0.1784    0.5686    0.0758    0.9685    0.5842    0.7589    0.1874
   
   B = 
   
      0.2304    0.7532    0.4516    0.2677    0.2807    0.8291    0.3281    0.8265
      0.0683    0.1767    0.5692    0.6886    0.5467    0.6831    0.4341    0.7355
      0.7178    0.0745    0.2044    0.1794    0.4812    0.9942    0.8839    0.4128
      0.4942    0.5976    0.7265    0.1393    0.3879    0.0784    0.5651    0.1200
      0.6232    0.2626    0.8000    0.8586    0.1094    0.4441    0.4526    0.8865
      0.2252    0.4495    0.3670    0.9364    0.2362    0.7872    0.5711    0.6225
      0.1874    0.9748    0.0295    0.8797    0.4469    0.8349    0.8873    0.7472
      0.7216    0.8158    0.0840    0.9717    0.5541    0.8100    0.7386    0.5369
   
   C = 
   
      1.7006    2.2093    1.6346    2.9971    1.7777    3.7551    2.9403    3.1181
      1.8394    2.4371    2.0774    2.4729    1.6138    2.9710    2.7385    2.9672
      1.2059    2.1987    1.7083    2.4933    1.6918    3.0511    2.4647    2.7018
      0.9585    1.4154    1.2654    1.6533    0.7809    1.6153    1.4048    1.5988
      2.1194    2.7963    1.8437    2.9971    2.1242    3.4770    3.1898    3.0236
      1.4428    1.7450    1.6336    2.5708    1.5533    2.5998    2.3031    2.3016
      1.3037    1.9283    0.8378    1.9913    1.3146    2.6812    2.2376    2.0050
      1.5512    1.7926    1.4583    2.5575    1.1858    2.6589    2.3216    2.5545
   
   D = 
   
      1.7006    2.2093    1.6346    2.9971    1.7777    3.7551    2.9403    3.1181
      1.8394    2.4371    2.0774    2.4729    1.6138    2.9710    2.7385    2.9672
      1.2059    2.1987    1.7083    2.4933    1.6918    3.0511    2.4647    2.7018
      0.9585    1.4154    1.2654    1.6533    0.7809    1.6153    1.4048    1.5988
      2.1194    2.7963    1.8437    2.9971    2.1242    3.4770    3.1898    3.0236
      1.4428    1.7450    1.6336    2.5708    1.5533    2.5998    2.3031    2.3016
      1.3037    1.9283    0.8378    1.9913    1.3146    2.6812    2.2376    2.0050
      1.5512    1.7926    1.4583    2.5575    1.1858    2.6589    2.3216    2.5545
   


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

   
      0.4516    0.1222    0.6959    0.3899    0.0443    0.5889
      0.4079    0.1457    0.3800    0.3573    0.5613    0.3020
      0.4365    0.0221    0.3595    0.6829    0.8292    0.2888
      0.1725    0.0362    0.4060    0.5389    0.6911    0.6347
      0.3430    0.3847    0.8175    0.5356    0.3251    0.2163
   
   
      0.6959
      0.8175
      0.6829
      0.5389
      0.5356
      0.5613
      0.8292
      0.6911
      0.5889
      0.6347
   

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

   
      9.4127    4.6754    4.3287    7.6330    2.4849    4.0461
      6.3183    9.7060    6.8455    7.1325    0.2338    7.4829
      8.2655    7.4055    8.6778    1.0320    3.0986    5.9204
      3.4315    1.8051    3.0042    2.5062    1.4766    1.8005
      7.4562    6.5824    5.9311    7.2731    2.7850    9.5775
   
   
      9.4127    0.0000    0.0000    7.6330    0.0000    0.0000
      6.3183    9.7060    6.8455    7.1325    0.0000    7.4829
      8.2655    7.4055    8.6778    0.0000    0.0000    5.9204
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.4562    6.5824    5.9311    7.2731    0.0000    9.5775
   
   
         NaN    0.0000    0.0000    7.6330    0.0000    0.0000
      6.3183       NaN    6.8455    7.1325    0.0000    7.4829
      8.2655    7.4055    8.6778    0.0000    0.0000    5.9204
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      7.4562    6.5824    5.9311    7.2731    0.0000       NaN
   

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

   
      3.2704    1.9510    3.3748    4.8865    8.0585    0.4704
      4.4117    0.8631    6.5000    4.6094    0.7565    9.7401
      4.6008    9.7101    8.9853    9.7106    8.6397    6.5000
      9.7577    4.9101    6.5000    8.3109    0.4894    6.5000
      1.8069    0.5509    0.0953    6.5000    3.5923    6.5000
   
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
   
