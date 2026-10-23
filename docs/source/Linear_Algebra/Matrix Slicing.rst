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
      0.2245    0.8556    0.9918    0.4059
   
   R1[2] = 0.9918030700790724
   C1 = 
      0.4456
      0.7320
      0.8689
      0.1308
      0.6281
      0.5244
      0.4495
      0.0272
   
   C1[5] = 0.5244336668963152

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
      0.5332    0.2798    0.8565    0.7321    0.9578
      0.3806    0.9456    0.1569    0.9541    0.0215
   

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
     - :math:`O(n^3)`
     - :math:`O(n^{\log_2 ^7}) \approx O(n^{2.81})`
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


4. **Return the result**

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
   
      0.6961    0.8175    0.7326    0.1849    0.2216    0.3932    0.7912    0.3677
      0.3298    0.5656    0.8262    0.4129    0.7108    0.0698    0.5701    0.1058
      0.0848    0.1157    0.3550    0.9338    0.9898    0.5638    0.1240    0.7172
      0.6171    0.5757    0.1261    0.9301    0.3955    0.4773    0.7622    0.8663
      0.1813    0.9977    0.4975    0.1223    0.1924    0.6020    0.7476    0.8000
      0.4407    0.1118    0.1231    0.4949    0.9610    0.3711    0.1210    0.6926
      0.3134    0.0453    0.4603    0.0079    0.8458    0.2242    0.6204    0.2418
      0.5985    0.9007    0.5725    0.7291    0.0874    0.1036    0.0069    0.8589
   
   B = 
   
      0.1141    0.9861    0.7068    0.9819    0.2589    0.3480    0.0960    0.9600
      0.3798    0.9214    0.5507    0.5369    0.4382    0.9258    0.1541    0.7592
      0.7218    0.4313    0.5035    0.2107    0.8725    0.2325    0.1908    0.2877
      0.5572    0.2981    0.9740    0.3369    0.5444    0.2465    0.6694    0.7574
      0.1397    0.8609    0.5665    0.7498    0.5285    0.0818    0.0801    0.9005
      0.4706    0.8565    0.4401    0.9074    0.3013    0.9557    0.6108    0.5986
      0.6590    0.5650    0.9474    0.2472    0.8377    0.7703    0.6171    0.3675
      0.2842    0.4276    0.9518    0.5254    0.9498    0.0350    0.1226    0.9256
   
   C = 
   
      1.8636    2.9426    2.8894    2.2509    2.5260    2.2312    1.2477    2.7058
      1.6168    2.3650    2.4371    1.7337    2.2538    1.5001    1.0173    2.2859
      1.5192    2.3333    2.8207    2.1959    2.3687    1.1898    1.3069    2.9167
      1.9267    3.0210    3.7034    2.6283    2.8426    2.1123    1.6944    3.4941
      1.8570    2.7949    2.8909    2.1555    2.6540    2.3276    1.2905    2.7162
      1.0428    2.2479    2.3987    2.1366    1.9188    0.9583    0.8775    2.6913
      1.0908    1.9258    1.8817    1.5498    1.7709    1.0298    0.7474    1.8213
      1.5394    2.4194    2.8364    2.0500    2.3450    1.4965    0.9734    2.9135
   
   D = 
   
      1.8636    2.9426    2.8894    2.2509    2.5260    2.2312    1.2477    2.7058
      1.6168    2.3650    2.4371    1.7337    2.2538    1.5001    1.0173    2.2859
      1.5192    2.3333    2.8207    2.1959    2.3687    1.1898    1.3069    2.9167
      1.9267    3.0210    3.7034    2.6283    2.8426    2.1123    1.6944    3.4941
      1.8570    2.7949    2.8909    2.1555    2.6540    2.3276    1.2905    2.7162
      1.0428    2.2479    2.3987    2.1366    1.9188    0.9583    0.8775    2.6913
      1.0908    1.9258    1.8817    1.5498    1.7709    1.0298    0.7474    1.8213
      1.5394    2.4194    2.8364    2.0500    2.3450    1.4965    0.9734    2.9135
   


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

   
      0.3450    0.1813    0.3322    0.7832    0.2497    0.5061
      0.9492    0.8535    0.8034    0.1405    0.7502    0.7681
      0.2186    0.1594    0.4918    0.4270    0.2721    0.2674
      0.1497    0.3612    0.6850    0.2425    0.9082    0.5599
      0.7917    0.7704    0.5103    0.4018    0.2942    0.5551
   
   
      0.9492
      0.7917
      0.8535
      0.7704
      0.8034
      0.6850
      0.5103
      0.7832
      0.7502
      0.9082
      0.5061
      0.7681
      0.5599
      0.5551
   

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

   
      8.1925    0.4335    6.5082    9.9673    7.6607    5.7220
      5.6144    3.8323    7.2422    0.6570    5.4919    5.3737
      7.9581    2.7309    0.5486    2.7176    9.3618    2.2798
      1.5890    9.0339    0.1272    4.2322    3.3589    5.7937
      7.5558    1.5311    5.2523    6.4509    0.2622    5.5649
   
   
      8.1925    0.0000    6.5082    9.9673    7.6607    5.7220
      5.6144    0.0000    7.2422    0.0000    5.4919    5.3737
      7.9581    0.0000    0.0000    0.0000    9.3618    0.0000
      0.0000    9.0339    0.0000    0.0000    0.0000    5.7937
      7.5558    0.0000    5.2523    6.4509    0.0000    5.5649
   
   
      8.1925    0.0000    6.5082       NaN    7.6607    5.7220
      5.6144    0.0000    7.2422    0.0000    5.4919    5.3737
      7.9581    0.0000    0.0000    0.0000       NaN    0.0000
      0.0000       NaN    0.0000    0.0000    0.0000    5.7937
      7.5558    0.0000    5.2523    6.4509    0.0000    5.5649
   

Complex Conditions
^^^^^^^^^^^^^^^^^^
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

   
      9.0179    1.1929    8.9651    6.5000    9.7261    8.0499
      9.0610    2.6104    0.7479    2.1498    0.1112    6.5000
      6.5000    2.9746    6.5000    2.3937    4.4181    6.5000
      3.1983    3.3331    6.5000    6.5000    6.5000    6.5000
      3.1467    4.3413    4.2057    8.7828    6.5000    0.5867
   
Advantages
^^^^^^^^^^


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
   
