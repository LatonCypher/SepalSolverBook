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
      0.0850    0.5089    0.5378    0.8654
   
   R1[2] = 0.5378328090994895
   C1 = 
      0.0137
      0.2891
      0.2254
      0.9131
      0.8191
      0.0955
      0.9372
      0.2637
   
   C1[5] = 0.09553612588836424

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
      0.6033    0.9063    0.0120    0.9511    0.3128
      0.3860    0.2226    0.7424    0.7283    0.1665
   

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
   
      0.8647    0.2288    0.2058    0.9474    0.3917    0.8911    0.1442    0.9563
      0.2685    0.6815    0.3658    0.0934    0.4474    0.3427    0.6720    0.0003
      0.5962    0.7433    0.4202    0.5071    0.3317    0.2293    0.3508    0.7919
      0.6765    0.9714    0.3910    0.3911    0.7196    0.2691    0.8638    0.3843
      0.5656    0.4431    0.7481    0.4832    0.7987    0.4786    0.1220    0.5419
      0.6201    0.4549    0.2283    0.8834    0.8254    0.9203    0.8230    0.8770
      0.9512    0.9530    0.2322    0.1700    0.0493    0.5100    0.4648    0.0113
      0.8911    0.9379    0.8683    0.7276    0.1193    0.9557    0.1720    0.4782
   
   B = 
   
      0.5313    0.8300    0.2795    0.7474    0.3119    0.6554    0.7045    0.2675
      0.5402    0.2519    0.8587    0.1804    0.7961    0.9113    0.6125    0.1511
      0.8389    0.3225    0.0231    0.9087    0.3667    0.3600    0.4880    0.9574
      0.2836    0.8811    0.0252    0.0886    0.0650    0.5295    0.3642    0.6810
      0.4566    0.3428    0.4131    0.1403    0.7981    0.8867    0.8389    0.0076
      0.9078    0.6684    0.8128    0.4490    0.5857    0.3406    0.4286    0.0411
      0.2805    0.3264    0.4506    0.1347    0.8550    0.1587    0.7803    0.9560
      0.5759    0.9466    0.4264    0.3103    0.0328    0.3941    0.1137    0.1514
   
   C = 
   
      2.6033    3.3587    1.8257    1.7297    1.5781    2.4015    2.1266    1.4304
      1.5483    1.1969    1.4375    0.9715    1.8989    1.5984    1.8657    1.2485
      2.1287    2.3956    1.6467    1.4489    1.6898    2.2279    2.0054    1.4867
      2.3596    2.3491    2.1113    1.5281    2.6362    2.6947    2.8416    1.8690
      2.4500    2.3946    1.5731    1.7368    1.8747    2.4038    2.2429    1.4880
      2.9655    3.4781    2.4253    1.7432    2.6269    2.8925    2.9774    2.0185
      1.8855    1.7743    1.7430    1.4109    1.8873    1.9611    2.0529    1.2039
      3.1606    3.0856    2.2004    2.3061    2.2080    2.7837    2.5893    1.9840
   
   D = 
   
      2.6033    3.3587    1.8257    1.7297    1.5781    2.4015    2.1266    1.4304
      1.5483    1.1969    1.4375    0.9715    1.8989    1.5984    1.8657    1.2485
      2.1287    2.3956    1.6467    1.4489    1.6898    2.2279    2.0054    1.4867
      2.3596    2.3491    2.1113    1.5281    2.6362    2.6947    2.8416    1.8690
      2.4500    2.3946    1.5731    1.7368    1.8747    2.4038    2.2429    1.4880
      2.9655    3.4781    2.4253    1.7432    2.6269    2.8925    2.9774    2.0185
      1.8855    1.7743    1.7430    1.4109    1.8873    1.9611    2.0529    1.2039
      3.1606    3.0856    2.2004    2.3061    2.2080    2.7837    2.5893    1.9840
   


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

   
      0.7116    0.9575    0.3750    0.2995    0.1977    0.5268
      0.0138    0.8165    0.4249    0.6935    0.3934    0.5913
      0.0177    0.1498    0.5419    0.7173    0.0158    0.1925
      0.9900    0.6270    0.0686    0.8637    0.3370    0.0803
      0.7029    0.3091    0.4712    0.2340    0.4530    0.1633
   
   
      0.7116
      0.9900
      0.7029
      0.9575
      0.8165
      0.6270
      0.5419
      0.6935
      0.7173
      0.8637
      0.5268
      0.5913
   

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

   
      8.0596    2.4645    8.0682    6.1907    0.6521    2.3098
      7.0593    9.4736    1.8010    8.4884    2.8126    6.3149
      2.4315    9.7052    7.4067    1.8656    7.0242    9.2869
      4.4460    9.4446    4.2111    2.6968    2.5414    0.9529
      2.7558    0.4611    1.7539    3.3114    9.0698    9.3156
   
   
      8.0596    0.0000    8.0682    6.1907    0.0000    0.0000
      7.0593    9.4736    0.0000    8.4884    0.0000    6.3149
      0.0000    9.7052    7.4067    0.0000    7.0242    9.2869
      0.0000    9.4446    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    9.0698    9.3156
   
   
      8.0596    0.0000    8.0682    6.1907    0.0000    0.0000
      7.0593       NaN    0.0000    8.4884    0.0000    6.3149
      0.0000       NaN    7.4067    0.0000    7.0242       NaN
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN       NaN
   

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

   
      4.2313    0.5794    0.1992    1.0514    6.5000    9.7747
      6.5000    9.9466    4.6717    2.6459    9.6435    4.2868
      6.5000    9.3403    9.5450    2.1010    2.6431    3.0339
      6.5000    6.5000    4.3837    6.5000    0.2129    6.5000
      0.0890    0.5250    3.0559    6.5000    6.5000    4.4044
   
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
   
