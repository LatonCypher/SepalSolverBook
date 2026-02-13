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
      0.0884    0.6070    0.0012    0.8432
   
   R1[2] = 0.0012369655036573723
   C1 = 
      0.4695
      0.9195
      0.4757
      0.0130
      0.9985
      0.4067
      0.9174
      0.3437
   
   C1[5] = 0.4066536875047235

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
      0.2433    0.2722    0.2802    0.1280    0.5476
      0.7157    0.4039    0.1837    0.7044    0.2830
   

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
   
      0.6621    0.1615    0.5520    0.5341    0.6312    0.6336    0.1379    0.1173
      0.0917    0.6997    0.4030    0.3863    0.9467    0.1407    0.8446    0.6526
      0.9332    0.3805    0.9595    0.3051    0.0721    0.0133    0.5576    0.4423
      0.8241    0.9991    0.1738    0.5496    0.5324    0.3536    0.5435    0.3026
      0.2645    0.2791    0.6148    0.2476    0.2868    0.8026    0.5723    0.0961
      0.8089    0.0631    0.9479    0.1484    0.6811    0.0490    0.7316    0.3842
      0.2161    0.9687    0.4438    0.8960    0.6137    0.5131    0.0359    0.8879
      0.5479    0.0135    0.2964    0.5465    0.6357    0.2733    0.5740    0.0143
   
   B = 
   
      0.4912    0.8901    0.7052    0.8895    0.0791    0.0009    0.9786    0.5678
      0.5041    0.8141    0.0892    0.3184    0.8094    0.7968    0.7049    0.2355
      0.7044    0.8348    0.3353    0.4356    0.9353    0.1252    0.1409    0.4710
      0.2245    0.8749    0.7134    0.2567    0.0551    0.5902    0.4996    0.8627
      0.5179    0.2573    0.9115    0.6895    0.9690    0.5497    0.9695    0.8410
      0.1329    0.5016    0.9034    0.0420    0.2416    0.0135    0.6206    0.7489
      0.6141    0.3446    0.4640    0.7372    0.3138    0.5509    0.3145    0.7027
      0.2800    0.2287    0.4339    0.6366    0.4178    0.6949    0.6463    0.4592
   
   C = 
   
      1.4440    2.2035    2.3100    1.6561    1.5858    1.0266    2.2306    2.2908
      1.9787    2.0801    2.2029    2.2759    2.4608    2.2771    2.5253    2.5347
      1.8999    2.5268    1.7597    2.1904    1.7289    1.2585    2.0084    2.0002
      1.8954    2.7435    2.3088    2.2432    1.9649    1.9500    2.9118    2.4926
      1.3928    1.8881    1.8879    1.3701    1.5271    0.9962    1.6841    2.0076
      2.0462    2.2323    2.1710    2.4462    2.0717    1.3023    2.2129    2.3487
      1.7649    2.7660    2.4516    1.9604    2.3665    2.3375    2.9030    2.6661
      1.3294    1.7260    1.9757    1.6432    1.2297    1.0502    1.8361    2.0746
   
   D = 
   
      1.4440    2.2035    2.3100    1.6561    1.5858    1.0266    2.2306    2.2908
      1.9787    2.0801    2.2029    2.2759    2.4608    2.2771    2.5253    2.5347
      1.8999    2.5268    1.7597    2.1904    1.7289    1.2585    2.0084    2.0002
      1.8954    2.7435    2.3088    2.2432    1.9649    1.9500    2.9118    2.4926
      1.3928    1.8881    1.8879    1.3701    1.5271    0.9962    1.6841    2.0076
      2.0462    2.2323    2.1710    2.4462    2.0717    1.3023    2.2129    2.3487
      1.7649    2.7660    2.4516    1.9604    2.3665    2.3375    2.9030    2.6661
      1.3294    1.7260    1.9757    1.6432    1.2297    1.0502    1.8361    2.0746
   


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

   
      0.5779    0.0087    0.7667    0.0677    0.9225    0.4350
      0.8025    0.3728    0.3855    0.4285    0.5458    0.6916
      0.4739    0.0079    0.2913    0.4046    0.8827    0.3826
      0.6450    0.4708    0.3487    0.6023    0.9815    0.6855
      0.7940    0.2834    0.5058    0.6783    0.5693    0.7178
   
   
      0.5779
      0.8025
      0.6450
      0.7940
      0.7667
      0.5058
      0.6023
      0.6783
      0.9225
      0.5458
      0.8827
      0.9815
      0.5693
      0.6916
      0.6855
      0.7178
   

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

   
      0.5775    0.5182    4.0358    1.0946    6.5428    8.5075
      2.6515    9.8492    5.9239    5.9660    1.7847    1.6593
      1.3468    2.4800    4.4471    0.5831    8.0023    0.7601
      2.9969    7.7370    8.7836    0.1300    8.8544    2.1219
      9.9041    1.3425    2.8555    3.0116    0.1234    6.9226
   
   
      0.0000    0.0000    0.0000    0.0000    6.5428    8.5075
      0.0000    9.8492    5.9239    5.9660    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    8.0023    0.0000
      0.0000    7.7370    8.7836    0.0000    8.8544    0.0000
      9.9041    0.0000    0.0000    0.0000    0.0000    6.9226
   
   
      0.0000    0.0000    0.0000    0.0000    6.5428    8.5075
      0.0000       NaN    5.9239    5.9660    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    8.0023    0.0000
      0.0000    7.7370    8.7836    0.0000    8.8544    0.0000
         NaN    0.0000    0.0000    0.0000    0.0000    6.9226
   

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

   
      6.5000    1.1364    3.9323    6.5000    6.5000    9.4444
      0.3111    2.4777    0.2454    8.6731    6.5000    4.7234
      2.6880    6.5000    1.9327    6.5000    6.5000    1.2306
      6.5000    6.5000    4.9671    9.0446    6.5000    8.2823
      3.9220    3.8057    6.5000    6.5000    1.7498    6.5000
   
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
   
