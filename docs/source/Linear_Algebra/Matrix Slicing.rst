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
      0.0656    0.3371    0.4777    0.2224
   
   R1[2] = 0.4777278245499641
   C1 = 
      0.6693
      0.4538
      0.9516
      0.2723
      0.1517
      0.2864
      0.1473
      0.7471
   
   C1[5] = 0.28635537795752586

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
      0.3345    0.1692    0.4219    0.1381    0.3617
      0.5085    0.3593    0.1725    0.4626    0.1357
   

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
   
      0.4297    0.0357    0.8736    0.0767    0.4322    0.4610    0.9897    0.9639
      0.2864    0.5974    0.8646    0.6713    0.3695    0.2004    0.1232    0.5327
      0.0042    0.5443    0.6146    0.5391    0.2591    0.2415    0.5073    0.3021
      0.2946    0.8571    0.9347    0.0460    0.5385    0.9777    0.4137    0.2388
      0.5439    0.4382    0.6612    0.4860    0.4022    0.7215    0.4163    0.8199
      0.0342    0.3002    0.3600    0.7751    0.6788    0.6501    0.1824    0.8369
      0.1167    0.0867    0.9597    0.9954    0.9938    0.8307    0.6904    0.1838
      0.7983    0.5472    0.3073    0.2968    0.3130    0.1652    0.5757    0.9812
   
   B = 
   
      0.4478    0.7490    0.9777    0.7280    0.4267    0.5947    0.9936    0.6310
      0.8550    0.7062    0.8360    0.7626    0.9752    0.5407    0.8350    0.7994
      0.9866    0.8424    0.8464    0.7051    0.3313    0.0748    0.7566    0.9949
      0.9361    0.0235    0.7050    0.9458    0.0184    0.1719    0.8774    0.0210
      0.5898    0.6431    0.7640    0.7408    0.4862    0.5677    0.0101    0.0714
      0.2994    0.4057    0.9899    0.3420    0.1636    0.9804    0.9559    0.9470
      0.4613    0.8031    0.6058    0.1961    0.7440    0.8338    0.1369    0.9252
      0.4448    0.0099    0.2889    0.7371    0.2516    0.4720    0.2453    0.1714
   
   C = 
   
      2.4349    2.3542    2.9080    2.4111    1.7734    2.3310    2.0020    2.7187
      2.6922    1.8037    2.6937    2.6677    1.4417    1.4338    2.3694    1.9540
      2.1718    1.5930    2.1909    1.9581    1.3651    1.3848    1.7736    1.8288
      2.7374    2.6921    3.5270    2.5612    2.0618    2.4383    2.8112    3.1900
      2.7355    2.1791    3.3109    2.8868    1.7170    2.3632    2.7849    2.5992
      2.4041    1.4142    2.6498    2.6186    1.2235    1.9126    2.0957    1.6123
      3.2403    2.5131    3.7535    3.0605    1.6497    2.4003    2.7316    2.6467
      2.3425    1.9906    2.7421    2.6204    1.8360    2.1275    2.2236    2.1328
   
   D = 
   
      2.4349    2.3542    2.9080    2.4111    1.7734    2.3310    2.0020    2.7187
      2.6922    1.8037    2.6937    2.6677    1.4417    1.4338    2.3694    1.9540
      2.1718    1.5930    2.1909    1.9581    1.3651    1.3848    1.7736    1.8288
      2.7374    2.6921    3.5270    2.5612    2.0618    2.4383    2.8112    3.1900
      2.7355    2.1791    3.3109    2.8868    1.7170    2.3632    2.7849    2.5992
      2.4041    1.4142    2.6498    2.6186    1.2235    1.9126    2.0957    1.6123
      3.2403    2.5131    3.7535    3.0605    1.6497    2.4003    2.7316    2.6467
      2.3425    1.9906    2.7421    2.6204    1.8360    2.1275    2.2236    2.1328
   


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

   
      0.2247    0.5033    0.1966    0.3400    0.9899    0.7827
      0.6591    0.6372    0.8104    0.7908    0.2692    0.6545
      0.4843    0.2998    0.6073    0.2335    0.9636    0.0625
      0.3120    0.8465    0.4112    0.3828    0.6181    0.5305
      0.8137    0.6265    0.9169    0.7030    0.6672    0.0413
   
   
      0.6591
      0.8137
      0.5033
      0.6372
      0.8465
      0.6265
      0.8104
      0.6073
      0.9169
      0.7908
      0.7030
      0.9899
      0.9636
      0.6181
      0.6672
      0.7827
      0.6545
      0.5305
   

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

   
      8.2015    4.6416    3.0498    9.8328    2.5407    0.7428
      8.3319    5.8427    7.8402    8.9109    2.6821    3.5117
      0.1117    7.8090    4.7576    2.9407    9.2930    4.4551
      2.1910    0.8549    4.9753    0.9274    2.5403    9.7681
      9.3329    5.2334    7.6387    9.8325    6.1319    9.0884
   
   
      8.2015    0.0000    0.0000    9.8328    0.0000    0.0000
      8.3319    5.8427    7.8402    8.9109    0.0000    0.0000
      0.0000    7.8090    0.0000    0.0000    9.2930    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    9.7681
      9.3329    5.2334    7.6387    9.8325    6.1319    9.0884
   
   
      8.2015    0.0000    0.0000       NaN    0.0000    0.0000
      8.3319    5.8427    7.8402    8.9109    0.0000    0.0000
      0.0000    7.8090    0.0000    0.0000       NaN    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000       NaN
         NaN    5.2334    7.6387       NaN    6.1319       NaN
   

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

   
      1.3707    6.5000    0.3242    1.6909    1.5120    9.0036
      6.5000    8.3518    6.5000    6.5000    6.5000    6.5000
      9.7397    2.1373    4.6513    4.3264    8.9655    3.6165
      1.9355    8.6850    8.4350    0.6081    4.3098    9.4301
      3.4739    9.1054    1.5902    6.5000    8.3720    6.5000
   
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
   
