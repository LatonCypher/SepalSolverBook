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
      0.9076    0.4384    0.0768    0.4151
   
   R1[2] = 0.07677028074351078
   C1 = 
      0.1763
      0.3249
      0.1605
      0.3628
      0.7034
      0.8061
      0.8705
      0.4428
   
   C1[5] = 0.8061446896754276

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
      0.5392    0.7369    0.7852    0.2595    0.3713
      0.8355    0.0081    0.2785    0.9509    0.8250
   

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
   
      0.3674    0.6520    0.2019    0.6199    0.1824    0.1780    0.2358    0.2581
      0.2140    0.6571    0.6132    0.6593    0.2943    0.5497    0.7834    0.4478
      0.3078    0.9513    0.6897    0.5448    0.3452    0.6059    0.2957    0.8542
      0.1632    0.6693    0.7478    0.2202    0.2262    0.3122    0.3458    0.2148
      0.3587    0.5208    0.1066    0.3421    0.7231    0.2720    0.3265    0.4013
      0.9571    0.1328    0.2061    0.5255    0.1017    0.4512    0.8238    0.0601
      0.0768    0.6893    0.1018    0.6608    0.7829    0.4068    0.4178    0.1989
      0.5487    0.9603    0.9746    0.4826    0.0022    0.8313    0.0369    0.4661
   
   B = 
   
      0.4704    0.7862    0.5462    0.2921    0.4794    0.8939    0.7010    0.7936
      0.9558    0.3997    0.9679    0.4039    0.1785    0.3344    0.6347    0.7642
      0.9577    0.2849    0.8100    0.1525    0.7465    0.2452    0.7247    0.7160
      0.3118    0.4803    0.4077    0.7859    0.3459    0.7462    0.7230    0.2306
      0.8228    0.0742    0.7738    0.8241    0.4968    0.1726    0.9479    0.7749
      0.5512    0.5330    0.3650    0.4568    0.2216    0.6088    0.9864    0.1040
      0.1157    0.7519    0.3552    0.8328    0.5701    0.2068    0.5362    0.2742
      0.7382    0.4087    0.0001    0.1016    0.5779    0.4638    0.7396    0.1834
   
   C = 
   
      1.6487    1.2959    1.5379    1.3428    1.0713    1.3668    1.9318    1.3492
      2.4880    2.0092    2.2251    2.1311    1.8792    1.8084    3.0607    1.8452
      3.1673    2.0004    2.4631    1.9018    1.9886    2.0546    3.4285    2.1589
      2.0581    1.2456    1.8442    1.2438    1.3349    1.1175    2.1070    1.5691
      1.9542    1.2929    1.7007    1.6330    1.3004    1.3202    2.3322    1.5896
      1.4105    2.0087    1.5685    1.7598    1.4731    1.8331    2.3123    1.4924
      2.0620    1.3526    1.9637    2.0348    1.2966    1.3786    2.5574    1.6130
      3.0681    1.9861    2.5335    1.5356    1.8045    2.1408    3.2358    2.1621
   
   D = 
   
      1.6487    1.2959    1.5379    1.3428    1.0713    1.3668    1.9318    1.3492
      2.4880    2.0092    2.2251    2.1311    1.8792    1.8084    3.0607    1.8452
      3.1673    2.0004    2.4631    1.9018    1.9886    2.0546    3.4285    2.1589
      2.0581    1.2456    1.8442    1.2438    1.3349    1.1175    2.1070    1.5691
      1.9542    1.2929    1.7007    1.6330    1.3004    1.3202    2.3322    1.5896
      1.4105    2.0087    1.5685    1.7598    1.4731    1.8331    2.3123    1.4924
      2.0620    1.3526    1.9637    2.0348    1.2966    1.3786    2.5574    1.6130
      3.0681    1.9861    2.5335    1.5356    1.8045    2.1408    3.2358    2.1621
   


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

   
      0.2818    0.0087    0.3819    0.1723    0.1814    0.5912
      0.4709    0.3724    0.6291    0.1796    0.1817    0.7736
      0.4160    0.2853    0.0181    0.4532    0.9635    0.1596
      0.2509    0.0249    0.1889    0.7362    0.4741    0.7059
      0.3750    0.9264    0.7498    0.5078    0.1473    0.8923
   
   
      0.9264
      0.6291
      0.7498
      0.7362
      0.5078
      0.9635
      0.5912
      0.7736
      0.7059
      0.8923
   

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

   
      8.3963    5.5967    2.9682    6.1489    4.9440    4.4776
      4.7663    3.1434    3.5176    1.1467    5.7616    1.5761
      1.7229    1.6215    1.8272    0.7420    5.9935    4.5873
      9.4251    9.4479    4.4300    7.1094    0.6715    1.8265
      4.0227    8.1454    5.7981    4.3572    4.5727    3.2931
   
   
      8.3963    5.5967    0.0000    6.1489    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    5.7616    0.0000
      0.0000    0.0000    0.0000    0.0000    5.9935    0.0000
      9.4251    9.4479    0.0000    7.1094    0.0000    0.0000
      0.0000    8.1454    5.7981    0.0000    0.0000    0.0000
   
   
      8.3963    5.5967    0.0000    6.1489    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    5.7616    0.0000
      0.0000    0.0000    0.0000    0.0000    5.9935    0.0000
         NaN       NaN    0.0000    7.1094    0.0000    0.0000
      0.0000    8.1454    5.7981    0.0000    0.0000    0.0000
   

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

   
      9.0131    4.3213    4.6800    1.3226    1.3788    6.5000
      6.5000    6.5000    1.6363    0.0220    0.8049    6.5000
      3.8519    3.8559    2.2963    0.2774    1.3311    0.3526
      4.4224    6.5000    3.5495    0.0446    2.2964    6.5000
      6.5000    1.2866    2.3093    8.8066    8.7245    9.8967
   
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
   
