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
      0.4138    0.2715    0.3185    0.9550
   
   R1[2] = 0.31848498091362143
   C1 = 
      0.2169
      0.3868
      0.9559
      0.7766
      0.5169
      0.8273
      0.5998
      0.2846
   
   C1[5] = 0.8272793623608173

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
      0.6303    0.9538    0.7543    0.4289    0.2463
      0.4069    0.0247    0.2826    0.8996    0.0460
   

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
   
      0.5189    0.2815    0.5075    0.3824    0.2514    0.0750    0.2359    0.3244
      0.6046    0.6175    0.1630    0.6953    0.2664    0.2664    0.1085    0.5324
      0.1861    0.9679    0.4052    0.4418    0.3054    0.9751    0.5581    0.5818
      0.6316    0.0792    0.7076    0.6067    0.3972    0.8560    0.7319    0.1655
      0.9834    0.1908    0.1877    0.8259    0.9171    0.7932    0.3867    0.8186
      0.9943    0.2114    0.0314    0.7095    0.3249    0.2049    0.0681    0.2011
      0.3958    0.7339    0.2110    0.0707    0.1253    0.6956    0.1020    0.1727
      0.1366    0.3406    0.1473    0.2191    0.1350    0.7740    0.5543    0.5653
   
   B = 
   
      0.7011    0.9523    0.3676    0.3821    0.5058    0.0879    0.8171    0.5943
      0.4805    0.3038    0.7764    0.1777    0.0446    0.9410    0.1926    0.6416
      0.2936    0.1805    0.7826    0.3420    0.5130    0.4951    0.8974    0.9693
      0.5595    0.9993    0.7816    0.2496    0.2957    0.5910    0.8039    0.4058
      0.5133    0.2941    0.1361    0.8898    0.6636    0.2929    0.1186    0.0649
      0.1827    0.6855    0.8984    0.0519    0.8541    0.6814    0.3877    0.7273
      0.2303    0.6447    0.6998    0.5985    0.8558    0.3555    0.3710    0.2973
      0.1854    0.6524    0.5535    0.3684    0.3079    0.3003    0.1940    0.3969
   
   C = 
   
      1.1192    1.5424    1.5515    1.0056    1.1810    1.0937    1.4503    1.4057
      1.4665    2.1658    2.0189    1.0820    1.2837    1.5838    1.5966    1.6502
      1.5330    2.4836    3.1125    1.3627    2.1681    2.5160    1.7914    2.4295
      1.5876    2.6431    2.7486    1.5458    2.5375    1.8486    2.3368    2.2899
      2.1548    3.4506    2.8632    2.0703    2.7155    2.0394    2.3913    2.3002
      1.4621    2.1370    1.4961    1.0200    1.2492    1.0406    1.6340    1.3151
      0.9785    1.4007    1.7446    0.6438    1.1799    1.4704    1.0668    1.5521
      0.8684    1.7756    2.0157    0.9181    1.6237    1.4687    1.1169    1.4922
   
   D = 
   
      1.1192    1.5424    1.5515    1.0056    1.1810    1.0937    1.4503    1.4057
      1.4665    2.1658    2.0189    1.0820    1.2837    1.5838    1.5966    1.6502
      1.5330    2.4836    3.1125    1.3627    2.1681    2.5160    1.7914    2.4295
      1.5876    2.6431    2.7486    1.5458    2.5375    1.8486    2.3368    2.2899
      2.1548    3.4506    2.8632    2.0703    2.7155    2.0394    2.3913    2.3002
      1.4621    2.1370    1.4961    1.0200    1.2492    1.0406    1.6340    1.3151
      0.9785    1.4007    1.7446    0.6438    1.1799    1.4704    1.0668    1.5521
      0.8684    1.7756    2.0157    0.9181    1.6237    1.4687    1.1169    1.4922
   


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

   
      0.8243    0.3324    0.4173    0.4759    0.4361    0.4955
      0.4099    0.2027    0.9922    0.9888    0.6467    0.3956
      0.4377    0.7233    0.1945    0.2267    0.4900    0.4128
      0.8504    0.0237    0.4602    0.9331    0.4284    0.4986
      0.8366    0.2844    0.6776    0.1411    0.1604    0.6858
   
   
      0.8243
      0.8504
      0.8366
      0.7233
      0.9922
      0.6776
      0.9888
      0.9331
      0.6467
      0.6858
   

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

   
      1.7962    2.1152    6.7885    1.8670    0.4910    2.9887
      5.4402    7.0462    2.7266    4.1034    5.2073    8.6612
      0.3205    0.5048    8.8228    7.5601    9.5115    9.4817
      3.3053    1.9826    1.1957    1.7460    9.2513    0.5896
      7.4237    8.5283    2.4710    9.0970    4.7404    4.6248
   
   
      0.0000    0.0000    6.7885    0.0000    0.0000    0.0000
      5.4402    7.0462    0.0000    0.0000    5.2073    8.6612
      0.0000    0.0000    8.8228    7.5601    9.5115    9.4817
      0.0000    0.0000    0.0000    0.0000    9.2513    0.0000
      7.4237    8.5283    0.0000    9.0970    0.0000    0.0000
   
   
      0.0000    0.0000    6.7885    0.0000    0.0000    0.0000
      5.4402    7.0462    0.0000    0.0000    5.2073    8.6612
      0.0000    0.0000    8.8228    7.5601       NaN       NaN
      0.0000    0.0000    0.0000    0.0000       NaN    0.0000
      7.4237    8.5283    0.0000       NaN    0.0000    0.0000
   

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

   
      6.5000    0.5909    0.0263    6.5000    9.5007    6.5000
      2.1528    9.3775    3.5657    1.6918    6.5000    3.6628
      1.7992    2.7759    1.6779    3.5108    4.3330    2.8837
      9.3239    8.9575    0.1815    8.0897    8.9792    4.9825
      1.2605    8.5144    1.0798    2.1805    9.9755    9.4320
   
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
   
