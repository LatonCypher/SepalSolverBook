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
      0.2717    0.4368    0.5354    0.2097
   
   R1[2] = 0.5354484114852412
   C1 = 
      0.5268
      0.9279
      0.4464
      0.8939
      0.6831
      0.4169
      0.5190
      0.7296
   
   C1[5] = 0.4169378703052461

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
      0.4472    0.2445    0.5377    0.4085    0.3876
      0.2441    0.5348    0.5088    0.5252    0.4041
   

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
   
      0.5435    0.6004    0.6828    0.1336    0.1941    0.6956    0.2183    0.0574
      0.8638    0.9093    0.2301    0.9745    0.9301    0.7149    0.3964    0.7852
      0.7416    0.4346    0.1771    0.9080    0.7679    0.3173    0.1884    0.1602
      0.4434    0.9243    0.7838    0.2776    0.5930    0.5936    0.0650    0.9197
      0.9325    0.5589    0.7832    0.4021    0.8501    0.0859    0.5749    0.2897
      0.9240    0.9094    0.0382    0.9505    0.4872    0.2516    0.3305    0.9312
      0.4718    0.2447    0.6277    0.0374    0.9991    0.6931    0.5145    0.2097
      0.5133    0.8536    0.6660    0.4818    0.8121    0.0862    0.7326    0.5386
   
   B = 
   
      0.1354    0.5094    0.2600    0.7315    0.3795    0.3187    0.0167    0.7566
      0.2316    0.2001    0.2632    0.2234    0.3504    0.5481    0.0769    0.4804
      0.3094    0.9555    0.5514    0.9132    0.4590    0.3622    0.9908    0.6657
      0.9610    0.4319    0.2275    0.2660    0.0040    0.1456    0.1026    0.7502
      0.7406    0.8451    0.8384    0.0077    0.8052    0.5256    0.5209    0.2275
      0.3813    0.7273    0.1236    0.3350    0.3900    0.9402    0.7134    0.1302
      0.3011    0.3146    0.4179    0.9476    0.6903    0.6313    0.5036    0.2740
      0.5580    0.8278    0.5288    0.4368    0.6632    0.3004    0.9952    0.3952
   
   C = 
   
      1.0591    1.8933    1.0766    1.6573    1.3470    1.6802    1.5098    1.4718
      2.8542    3.3433    2.2616    2.2697    2.5781    2.6461    2.3879    2.6983
      1.9643    2.0978    1.4580    1.4036    1.4971    1.5400    1.1952    1.9000
      1.9816    2.9942    1.9379    1.9871    2.2168    2.1593    2.5638    2.1030
      1.8815    2.7105    2.0297    2.3359    2.2179    1.9234    1.9577    2.2738
      2.3368    2.5691    1.7870    1.9748    2.0268    1.9261    1.7471    2.4766
      1.6269    2.5890    1.7909    1.8020    2.1221    2.0818    2.1351    1.4620
      2.0917    2.7021    2.1175    2.2672    2.3521    2.0751    2.1730    2.2129
   
   D = 
   
      1.0591    1.8933    1.0766    1.6573    1.3470    1.6802    1.5098    1.4718
      2.8542    3.3433    2.2616    2.2697    2.5781    2.6461    2.3879    2.6983
      1.9643    2.0978    1.4580    1.4036    1.4971    1.5400    1.1952    1.9000
      1.9816    2.9942    1.9379    1.9871    2.2168    2.1593    2.5638    2.1030
      1.8815    2.7105    2.0297    2.3359    2.2179    1.9234    1.9577    2.2738
      2.3368    2.5691    1.7870    1.9748    2.0268    1.9261    1.7471    2.4766
      1.6269    2.5890    1.7909    1.8020    2.1221    2.0818    2.1351    1.4620
      2.0917    2.7021    2.1175    2.2672    2.3521    2.0751    2.1730    2.2129
   


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

   
      0.7463    0.7393    0.5051    0.5901    0.0913    0.0552
      0.7226    0.2445    0.6020    0.4517    0.4063    0.3123
      0.5802    0.4304    0.1952    0.6815    0.3243    0.0459
      0.1697    0.6213    0.3400    0.5355    0.1982    0.0909
      0.8063    0.5631    0.9856    0.4078    0.5645    0.1450
   
   
      0.7463
      0.7226
      0.5802
      0.8063
      0.7393
      0.6213
      0.5631
      0.5051
      0.6020
      0.9856
      0.5901
      0.6815
      0.5355
      0.5645
   

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

   
      5.5743    2.7885    6.6679    0.2963    1.8262    3.6524
      0.5355    2.3418    5.1101    7.0983    2.9563    3.9443
      7.7290    8.7118    2.3217    5.1900    6.8977    6.9813
      7.2275    8.7150    0.9195    1.4895    1.7124    6.7954
      6.3131    8.0238    5.2401    7.5959    9.9570    8.2760
   
   
      5.5743    0.0000    6.6679    0.0000    0.0000    0.0000
      0.0000    0.0000    5.1101    7.0983    0.0000    0.0000
      7.7290    8.7118    0.0000    5.1900    6.8977    6.9813
      7.2275    8.7150    0.0000    0.0000    0.0000    6.7954
      6.3131    8.0238    5.2401    7.5959    9.9570    8.2760
   
   
      5.5743    0.0000    6.6679    0.0000    0.0000    0.0000
      0.0000    0.0000    5.1101    7.0983    0.0000    0.0000
      7.7290    8.7118    0.0000    5.1900    6.8977    6.9813
      7.2275    8.7150    0.0000    0.0000    0.0000    6.7954
      6.3131    8.0238    5.2401    7.5959       NaN    8.2760
   

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

   
      8.7998    1.8324    8.9853    1.5925    3.1829    0.1282
      9.6363    0.0479    9.8083    0.6493    6.5000    6.5000
      6.5000    6.5000    0.2192    0.3566    1.2368    4.8688
      2.5464    8.0972    6.5000    6.5000    3.3478    9.8487
      2.5438    6.5000    2.5323    6.5000    1.0866    1.4130
   
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
   
