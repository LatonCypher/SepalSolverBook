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
      0.4106    0.4472    0.1806    0.0454
   
   R1[2] = 0.1805697628362588
   C1 = 
      0.0596
      0.1743
      0.1073
      0.4696
      0.0087
      0.0447
      0.1095
      0.7048
   
   C1[5] = 0.044733346056564316

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
      0.6370    0.9990    0.2535    0.6455    0.6424
      0.1976    0.4863    0.7880    0.9286    0.7965
   

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
   
      0.4414    0.9073    0.2115    0.6670    0.4868    0.5432    0.5562    0.9421
      0.4563    0.6064    0.9928    0.1273    0.6444    0.7997    0.9081    0.9102
      0.4897    0.1615    0.0526    0.9458    0.6694    0.7636    0.4255    0.5767
      0.5343    0.4498    0.4515    0.9968    0.0901    0.2800    0.2172    0.4205
      0.9451    0.1970    0.5939    0.9237    0.1663    0.7421    0.3000    0.8463
      0.5065    0.0274    0.6459    0.3751    0.1736    0.9610    0.2772    0.2957
      0.3394    0.2587    0.5516    0.9641    0.8959    0.5005    0.0706    0.6453
      0.9828    0.6880    0.2751    0.2380    0.0731    0.5468    0.4107    0.2882
   
   B = 
   
      0.6829    0.7265    0.7963    0.6803    0.7384    0.7187    0.9883    0.8707
      0.1916    0.0972    0.5181    0.5376    0.8696    0.8081    0.8986    0.7762
      0.4978    0.9699    0.7021    0.4533    0.9668    0.8761    0.7963    0.7684
      0.7401    0.8201    0.6675    0.7188    0.4451    0.1177    0.7151    0.1297
      0.5928    0.4408    0.1764    0.8774    0.7709    0.6589    0.7960    0.5016
      0.2636    0.2051    0.8015    0.2636    0.9408    0.8133    0.2696    0.0060
      0.9552    0.9060    0.2892    0.9984    0.5043    0.5055    0.0759    0.4442
      0.7039    0.5132    0.8618    0.9565    0.7917    0.7619    0.9951    0.0909
   
   C = 
   
      2.7004    2.4744    2.9094    3.3901    3.5288    3.0757    3.4105    1.9178
      3.1171    3.1957    3.2613    3.7314    4.3083    3.9303    3.5806    2.4615
      2.5020    2.3313    2.4921    2.8887    2.8793    2.3565    2.6921    1.2966
      2.0442    2.1971    2.3065    2.2984    2.4411    1.9776    2.5868    1.4722
      2.8389    2.9710    3.3286    3.1325    3.5021    2.9770    3.4419    1.8501
      1.7794    1.9813    2.2573    1.8869    2.6010    2.2575    2.0203    1.2501
      2.4541    2.4900    2.5708    2.9186    3.1458    2.5744    3.1920    1.5876
      1.8987    1.9074    2.3094    2.2282    2.7019    2.4514    2.5024    1.8807
   
   D = 
   
      2.7004    2.4744    2.9094    3.3901    3.5288    3.0757    3.4105    1.9178
      3.1171    3.1957    3.2613    3.7314    4.3083    3.9303    3.5806    2.4615
      2.5020    2.3313    2.4921    2.8887    2.8793    2.3565    2.6921    1.2966
      2.0442    2.1971    2.3065    2.2984    2.4411    1.9776    2.5868    1.4722
      2.8389    2.9710    3.3286    3.1325    3.5021    2.9770    3.4419    1.8501
      1.7794    1.9813    2.2573    1.8869    2.6010    2.2575    2.0203    1.2501
      2.4541    2.4900    2.5708    2.9186    3.1458    2.5744    3.1920    1.5876
      1.8987    1.9074    2.3094    2.2282    2.7019    2.4514    2.5024    1.8807
   


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

   
      0.8788    0.2696    0.2032    0.4402    0.0308    0.4992
      0.5052    0.1178    0.4136    0.1340    0.1349    0.3886
      0.0239    0.9416    0.6210    0.0544    0.7756    0.7989
      0.6692    0.7562    0.4339    0.9740    0.6613    0.1377
      0.5142    0.6776    0.2541    0.9576    0.3981    0.7834
   
   
      0.8788
      0.5052
      0.6692
      0.5142
      0.9416
      0.7562
      0.6776
      0.6210
      0.9740
      0.9576
      0.7756
      0.6613
      0.7989
      0.7834
   

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

   
      9.8186    0.3493    5.6893    5.6766    8.7211    5.4656
      8.3727    6.3062    9.9265    6.3526    0.3675    1.6911
      3.3460    9.9525    3.7850    2.7170    0.6352    2.0922
      4.9696    2.3474    5.6963    0.1194    6.7381    1.8795
      6.9599    0.3419    0.3619    0.4943    8.3971    1.3212
   
   
      9.8186    0.0000    5.6893    5.6766    8.7211    5.4656
      8.3727    6.3062    9.9265    6.3526    0.0000    0.0000
      0.0000    9.9525    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    5.6963    0.0000    6.7381    0.0000
      6.9599    0.0000    0.0000    0.0000    8.3971    0.0000
   
   
         NaN    0.0000    5.6893    5.6766    8.7211    5.4656
      8.3727    6.3062       NaN    6.3526    0.0000    0.0000
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    5.6963    0.0000    6.7381    0.0000
      6.9599    0.0000    0.0000    0.0000    8.3971    0.0000
   

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

   
      4.6722    6.5000    6.5000    1.2188    3.5309    6.5000
      6.5000    0.2300    0.8716    6.5000    6.5000    2.8121
      3.2433    6.5000    6.5000    9.3521    4.1890    1.1564
      6.5000    3.7194    6.5000    3.6198    9.4234    4.2303
      4.3555    3.9710    3.1432    6.5000    6.5000    0.1896
   
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
   
