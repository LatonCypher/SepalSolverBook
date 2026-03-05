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
      0.0197    0.5120    0.6978    0.7396
   
   R1[2] = 0.6977783266155996
   C1 = 
      0.2307
      0.3255
      0.6720
      0.6640
      0.0198
      0.2382
      0.2802
      0.5535
   
   C1[5] = 0.23824535480921505

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
      0.4336    0.6278    0.9284    0.6250    0.5893
      0.6785    0.8182    0.6072    0.2845    0.8746
   

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
   
      0.4346    0.9032    0.8566    0.0952    0.4750    0.1280    0.5822    0.4330
      0.9064    0.5824    0.1287    0.7830    0.4251    0.7382    0.7397    0.1024
      0.0295    0.8051    0.9216    0.6453    0.7288    0.7258    0.8999    0.7752
      0.5075    0.5525    0.3445    0.7149    0.4809    0.7208    0.4350    0.3993
      0.6449    0.1607    0.9644    0.2116    0.7241    0.7376    0.1328    0.7480
      0.1708    0.7479    0.2985    0.3140    0.1322    0.4667    0.2967    0.7169
      0.6471    0.1617    0.5488    0.3805    0.7684    0.4706    0.8507    0.3554
      0.2235    0.0171    0.4597    0.4787    0.2948    0.4093    0.3784    0.6561
   
   B = 
   
      0.8904    0.3988    0.4411    0.4957    0.0670    0.9433    0.6193    0.2893
      0.7549    0.6783    0.9473    0.3993    0.8123    0.9873    0.0185    0.0862
      0.1489    0.6366    0.7069    0.0226    0.1468    0.1214    0.4087    0.9200
      0.1463    0.5353    0.0732    0.3658    0.5624    0.2094    0.9206    0.3455
      0.5202    0.0621    0.9438    0.0942    0.5692    0.3662    0.6061    0.7918
      0.6488    0.0922    0.2320    0.6493    0.8539    0.4215    0.9271    0.7759
      0.5620    0.9316    0.8600    0.7224    0.9736    0.5012    0.6941    0.9177
      0.3235    0.4475    0.1039    0.0337    0.2862    0.8650    0.1101    0.6199
   
   C = 
   
      2.0077    2.1598    2.6835    1.1934    2.0125    2.3199    1.5819    2.3026
      2.5293    2.0871    2.3191    2.0285    2.6148    2.5358    2.8122    2.3531
      2.4721    2.7874    3.1852    1.8093    3.2868    2.7640    2.8284    3.5953
      2.1162    1.8594    2.0798    1.5826    2.3625    2.2591    2.4291    2.3452
      2.0419    1.6649    2.1805    1.1514    1.8198    2.2180    2.2885    2.8921
      1.5772    1.5820    1.5805    1.0590    1.8071    2.0155    1.3284    1.6805
      2.1337    1.9635    2.4573    1.5415    2.2384    2.1299    2.5098    2.8120
      1.1942    1.3519    1.2417    0.8922    1.4392    1.4215    1.6604    1.9595
   
   D = 
   
      2.0077    2.1598    2.6835    1.1934    2.0125    2.3199    1.5819    2.3026
      2.5293    2.0871    2.3191    2.0285    2.6148    2.5358    2.8122    2.3531
      2.4721    2.7874    3.1852    1.8093    3.2868    2.7640    2.8284    3.5953
      2.1162    1.8594    2.0798    1.5826    2.3625    2.2591    2.4291    2.3452
      2.0419    1.6649    2.1805    1.1514    1.8198    2.2180    2.2885    2.8921
      1.5772    1.5820    1.5805    1.0590    1.8071    2.0155    1.3284    1.6805
      2.1337    1.9635    2.4573    1.5415    2.2384    2.1299    2.5098    2.8120
      1.1942    1.3519    1.2417    0.8922    1.4392    1.4215    1.6604    1.9595
   


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

   
      0.1312    0.1597    0.9743    0.3377    0.4763    0.7582
      0.8797    0.6459    0.8773    0.2543    0.7048    0.0743
      0.3295    0.3895    0.9705    0.1555    0.5972    0.1138
      0.6446    0.9083    0.8280    0.1400    0.0290    0.5613
      0.8413    0.0612    0.1185    0.8238    0.9821    0.3280
   
   
      0.8797
      0.6446
      0.8413
      0.6459
      0.9083
      0.9743
      0.8773
      0.9705
      0.8280
      0.8238
      0.7048
      0.5972
      0.9821
      0.7582
      0.5613
   

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

   
      7.9144    5.1833    2.7321    7.2274    8.4159    6.3241
      3.9843    7.3412    6.3418    1.8197    4.0111    3.1093
      9.4530    0.0632    5.0120    2.5066    6.2237    9.5678
      5.9374    1.4567    7.6918    8.1267    1.8452    3.1228
      6.8526    6.9134    3.0127    6.2942    8.5004    1.2579
   
   
      7.9144    5.1833    0.0000    7.2274    8.4159    6.3241
      0.0000    7.3412    6.3418    0.0000    0.0000    0.0000
      9.4530    0.0000    5.0120    0.0000    6.2237    9.5678
      5.9374    0.0000    7.6918    8.1267    0.0000    0.0000
      6.8526    6.9134    0.0000    6.2942    8.5004    0.0000
   
   
      7.9144    5.1833    0.0000    7.2274    8.4159    6.3241
      0.0000    7.3412    6.3418    0.0000    0.0000    0.0000
         NaN    0.0000    5.0120    0.0000    6.2237       NaN
      5.9374    0.0000    7.6918    8.1267    0.0000    0.0000
      6.8526    6.9134    0.0000    6.2942    8.5004    0.0000
   

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

   
      1.9346    6.5000    0.1929    1.3355    6.5000    8.6276
      4.3173    3.6598    6.5000    9.2403    0.6751    2.6599
      4.1934    9.8819    4.6921    3.3263    6.5000    4.0667
      6.5000    2.8500    9.8384    3.3159    4.2687    1.1302
      8.8201    6.5000    8.0564    9.6511    1.3456    1.4853
   
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
   
