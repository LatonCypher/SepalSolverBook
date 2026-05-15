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
      0.2388    0.9698    0.2082    0.2678
   
   R1[2] = 0.20821535160165172
   C1 = 
      0.8220
      0.9075
      0.2309
      0.1571
      0.9224
      0.6040
      0.8433
      0.3446
   
   C1[5] = 0.6040020865680423

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
      0.3743    0.9189    0.7889    0.4094    0.4266
      0.6045    0.1019    0.3266    0.6393    0.0404
   

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
   
      0.0322    0.1629    0.6881    0.2919    0.8411    0.1815    0.3533    0.3858
      0.2202    0.9631    0.3656    0.9901    0.5399    0.9658    0.9602    0.3516
      0.7031    0.1435    0.2773    0.6906    0.9429    0.7564    0.1638    0.2933
      0.0386    0.1244    0.3877    0.7455    0.2239    0.4968    0.8306    0.1439
      0.3920    0.7037    0.1373    0.9469    0.2280    0.0718    0.5222    0.1272
      0.3548    0.1003    0.4234    0.3023    0.3746    0.0423    0.1221    0.3087
      0.2998    0.2332    0.2308    0.0384    0.1296    0.1683    0.2406    0.9875
      0.2031    0.7804    0.2439    0.9136    0.8087    0.7782    0.5467    0.2942
   
   B = 
   
      0.5206    0.3782    0.0399    0.6304    0.1488    0.5716    0.8099    0.3040
      0.3551    0.2892    0.2060    0.4394    0.2157    0.9552    0.9437    0.7111
      0.9507    0.8778    0.8000    0.6639    0.2615    0.1966    0.2693    0.6517
      0.8388    0.3831    0.1014    0.3034    0.2879    0.4787    0.8829    0.1664
      0.9224    0.2354    0.6838    0.4238    0.0081    0.0151    0.7145    0.1103
      0.7736    0.3977    0.5676    0.0615    0.3213    0.5908    0.1028    0.9837
      0.7303    0.0817    0.7505    0.2054    0.7663    0.5921    0.1192    0.6072
      0.4051    0.5932    0.8265    0.8645    0.5456    0.1550    0.4032    0.1140
   
   C = 
   
      2.3042    1.3030    1.8771    1.4109    0.8502    0.8379    1.4402    1.1524
      3.7236    1.8603    2.5287    1.8946    1.8635    2.7934    2.8010    2.7876
      2.9533    1.5255    1.7890    1.6333    0.9431    1.5276    2.2785    1.5924
      2.3139    1.0801    1.5903    0.9831    1.2250    1.3849    1.2794    1.5110
      2.0776    1.0355    1.0602    1.2531    1.0132    1.7513    2.1384    1.2940
      1.4689    0.9488    1.0311    1.0938    0.5508    0.6773    1.1739    0.6977
      1.3160    1.1010    1.4295    1.4248    0.9447    0.8548    1.0957    0.8523
      3.2474    1.5857    2.1047    1.6673    1.3614    2.1881    2.6148    2.1479
   
   D = 
   
      2.3042    1.3030    1.8771    1.4109    0.8502    0.8379    1.4402    1.1524
      3.7236    1.8603    2.5287    1.8946    1.8635    2.7934    2.8010    2.7876
      2.9533    1.5255    1.7890    1.6333    0.9431    1.5276    2.2785    1.5924
      2.3139    1.0801    1.5903    0.9831    1.2250    1.3849    1.2794    1.5110
      2.0776    1.0355    1.0602    1.2531    1.0132    1.7513    2.1384    1.2940
      1.4689    0.9488    1.0311    1.0938    0.5508    0.6773    1.1739    0.6977
      1.3160    1.1010    1.4295    1.4248    0.9447    0.8548    1.0957    0.8523
      3.2474    1.5857    2.1047    1.6673    1.3614    2.1881    2.6148    2.1479
   


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

   
      0.7271    0.3992    0.5992    0.3554    0.3969    0.5616
      0.1380    0.3563    0.8207    0.5609    0.1857    0.6340
      0.9809    0.2396    0.8127    0.3787    0.3071    0.4016
      0.2079    0.9946    0.5761    0.7132    0.2421    0.4505
      0.8685    0.4375    0.0073    0.2241    0.0306    0.1235
   
   
      0.7271
      0.9809
      0.8685
      0.9946
      0.5992
      0.8207
      0.8127
      0.5761
      0.5609
      0.7132
      0.5616
      0.6340
   

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

   
      8.2701    0.2137    8.7322    5.4351    2.2949    4.2570
      4.4690    1.9618    0.6747    5.2682    3.4418    5.0157
      8.7660    2.0879    6.5975    3.4922    6.3372    6.1821
      1.8636    5.1374    7.0037    9.2873    0.7326    6.2713
      9.7271    9.2580    8.8756    0.3434    8.6061    1.0816
   
   
      8.2701    0.0000    8.7322    5.4351    0.0000    0.0000
      0.0000    0.0000    0.0000    5.2682    0.0000    5.0157
      8.7660    0.0000    6.5975    0.0000    6.3372    6.1821
      0.0000    5.1374    7.0037    9.2873    0.0000    6.2713
      9.7271    9.2580    8.8756    0.0000    8.6061    0.0000
   
   
      8.2701    0.0000    8.7322    5.4351    0.0000    0.0000
      0.0000    0.0000    0.0000    5.2682    0.0000    5.0157
      8.7660    0.0000    6.5975    0.0000    6.3372    6.1821
      0.0000    5.1374    7.0037       NaN    0.0000    6.2713
         NaN       NaN    8.8756    0.0000    8.6061    0.0000
   

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

   
      4.9132    2.2905    6.5000    4.2430    1.6108    6.5000
      6.5000    8.4850    6.5000    6.5000    3.4988    2.8489
      6.5000    8.9791    6.5000    2.4619    0.0289    0.0810
      6.5000    9.0294    8.6139    6.5000    4.9166    6.5000
      9.9146    9.1277    8.1010    0.2676    6.5000    9.9166
   
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
   
