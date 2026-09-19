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
      0.7287    0.0370    0.8440    0.2924
   
   R1[2] = 0.8440354673833251
   C1 = 
      0.2656
      0.3372
      0.1929
      0.3001
      0.6012
      0.4068
      0.2432
      0.8863
   
   C1[5] = 0.4067726598580147

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
      0.5736    0.8867    0.7138    0.3007    0.4050
      0.1683    0.6577    0.2161    0.9375    0.8652
   

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
   
      0.2029    0.5278    0.0677    0.7563    0.5072    0.7664    0.2355    0.0068
      0.8668    0.7251    0.1733    0.7040    0.5218    0.4016    0.5849    0.7960
      0.7680    0.4866    0.9376    0.7792    0.7703    0.9648    0.2555    0.7578
      0.9604    0.8052    0.0630    0.1476    0.2190    0.0850    0.7680    0.9077
      0.5601    0.1843    0.1669    0.7874    0.8708    0.9915    0.5559    0.9406
      0.0448    0.0097    0.3945    0.5775    0.7140    0.5815    0.5015    0.5814
      0.2792    0.1949    0.0502    0.8256    0.9127    0.1137    0.8555    0.7201
      0.8833    0.0786    0.5188    0.5122    0.6091    0.8451    0.5294    0.7465
   
   B = 
   
      0.8418    0.6685    0.0631    0.0668    0.2207    0.5822    0.7564    0.1044
      0.8688    0.6965    0.4181    0.3300    0.6165    0.0553    0.9562    0.4100
      0.6981    0.9558    0.0930    0.7997    0.7915    0.2651    0.3276    0.9128
      0.8296    0.1166    0.0957    0.3750    0.3351    0.9354    0.8604    0.8772
      0.9922    0.4131    0.1866    0.5958    0.4437    0.6734    0.9037    0.6999
      0.8245    0.9929    0.3498    0.2312    0.9675    0.8050    0.0223    0.3032
      0.2067    0.5465    0.9877    0.2722    0.0124    0.6998    0.3828    0.1935
      0.4502    0.4246    0.1832    0.8877    0.5018    0.7412    0.1846    0.9519
   
   C = 
   
      2.4909    1.7582    0.9087    1.0751    1.6501    2.0010    1.8979    1.6022
      3.3926    2.6040    1.4028    1.9694    2.0382    2.9232    2.8628    2.5214
      4.3240    3.5769    1.2862    2.6783    3.1315    3.4874    2.9791    3.4216
      2.5291    2.2602    1.4128    1.6006    1.4521    2.1846    2.3054    1.8091
      3.6212    2.8014    1.4339    2.2614    2.4578    3.5878    2.5275    2.8901
      2.3538    1.8743    1.0373    1.7507    1.6990    2.4022    1.6267    2.2018
      2.6245    1.7297    1.3697    1.8750    1.3851    2.7974    2.4120    2.4034
      3.3454    2.8977    1.2548    2.0570    2.2947    3.1495    2.2636    2.5428
   
   D = 
   
      2.4909    1.7582    0.9087    1.0751    1.6501    2.0010    1.8979    1.6022
      3.3926    2.6040    1.4028    1.9694    2.0382    2.9232    2.8628    2.5214
      4.3240    3.5769    1.2862    2.6783    3.1315    3.4874    2.9791    3.4216
      2.5291    2.2602    1.4128    1.6006    1.4521    2.1846    2.3054    1.8091
      3.6212    2.8014    1.4339    2.2614    2.4578    3.5878    2.5275    2.8901
      2.3538    1.8743    1.0373    1.7507    1.6990    2.4022    1.6267    2.2018
      2.6245    1.7297    1.3697    1.8750    1.3851    2.7974    2.4120    2.4034
      3.3454    2.8977    1.2548    2.0570    2.2947    3.1495    2.2636    2.5428
   


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

   
      0.7260    0.1833    0.6552    0.2221    0.9938    0.6811
      0.1628    0.0420    0.1721    0.0224    0.6280    0.7116
      0.4767    0.3047    0.7408    0.7234    0.6599    0.1816
      0.1844    0.7031    0.2367    0.6640    0.9251    0.0163
      0.9418    0.6609    0.9367    0.7336    0.2220    0.1719
   
   
      0.7260
      0.9418
      0.7031
      0.6609
      0.6552
      0.7408
      0.9367
      0.7234
      0.6640
      0.7336
      0.9938
      0.6280
      0.6599
      0.9251
      0.6811
      0.7116
   

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

   
      9.9333    0.1647    8.3835    1.6981    1.2471    9.2927
      3.7259    4.2359    2.5430    6.4640    5.3972    0.1820
      6.0442    1.7526    9.6381    1.6217    3.5241    5.1180
      3.4707    2.4800    3.0934    7.1813    5.1441    8.1182
      8.8776    3.0511    1.9749    7.8284    8.1412    0.9029
   
   
      9.9333    0.0000    8.3835    0.0000    0.0000    9.2927
      0.0000    0.0000    0.0000    6.4640    5.3972    0.0000
      6.0442    0.0000    9.6381    0.0000    0.0000    5.1180
      0.0000    0.0000    0.0000    7.1813    5.1441    8.1182
      8.8776    0.0000    0.0000    7.8284    8.1412    0.0000
   
   
         NaN    0.0000    8.3835    0.0000    0.0000       NaN
      0.0000    0.0000    0.0000    6.4640    5.3972    0.0000
      6.0442    0.0000       NaN    0.0000    0.0000    5.1180
      0.0000    0.0000    0.0000    7.1813    5.1441    8.1182
      8.8776    0.0000    0.0000    7.8284    8.1412    0.0000
   

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

   
      3.0518    1.7749    2.4799    2.6769    6.5000    2.8042
      0.8492    4.4319    6.5000    8.0124    8.7138    4.4640
      6.5000    6.5000    6.5000    1.9272    3.4788    6.5000
      6.5000    6.5000    3.4610    6.5000    4.8521    3.8559
      1.6728    9.5026    1.5823    6.5000    4.1517    8.2420
   
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
   
