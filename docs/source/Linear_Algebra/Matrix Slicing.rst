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
      0.0091    0.8757    0.6947    0.5576
   
   R1[2] = 0.6946763433590589
   C1 = 
      0.5501
      0.9672
      0.2063
      0.3440
      0.4443
      0.2405
      0.7769
      0.7811
   
   C1[5] = 0.24045154926330314

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
      0.8268    0.2310    0.3294    0.2715    0.2929
      0.3471    0.6257    0.8554    0.2119    0.1568
   

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
   
      0.8684    0.3417    0.3015    0.6170    0.3594    0.0593    0.0974    0.2532
      0.3092    0.7480    0.6144    0.6456    0.5159    0.2101    0.8883    0.5817
      0.0157    0.1974    0.3069    0.8192    0.6802    0.4503    0.0376    0.1733
      0.6671    0.9635    0.6972    0.0425    0.7194    0.2766    0.4467    0.3784
      0.4693    0.3133    0.4412    0.1239    0.6773    0.5591    0.3579    0.4162
      0.9175    0.9346    0.9388    0.6462    0.6646    0.9360    0.2925    0.4401
      0.5143    0.3136    0.3602    0.6559    0.3791    0.5499    0.1436    0.1091
      0.5620    0.7068    0.0051    0.1637    0.0306    0.5209    0.4938    0.7614
   
   B = 
   
      0.8070    0.5410    0.1146    0.3071    0.3494    0.2288    0.4035    0.9104
      0.8121    0.5118    0.2978    0.2988    0.6440    0.2811    0.2242    0.3647
      0.1276    0.7867    0.7762    0.5492    0.7543    0.3357    0.4528    0.8366
      0.9182    0.2355    0.8191    0.4336    0.7710    0.7659    0.4863    0.3557
      0.3591    0.0363    0.2500    0.9327    0.1101    0.1964    0.9937    0.3968
      0.9642    0.1706    0.2711    0.9585    0.7498    0.3560    0.5614    0.1082
      0.0635    0.3593    0.2198    0.4796    0.9805    0.8668    0.1495    0.3769
      0.4408    0.5780    0.5459    0.6882    0.4363    0.7780    0.2162    0.0161
   
   C = 
   
      1.8873    1.2317    1.2063    1.4149    1.5166    1.2417    1.3233    1.5767
      2.2288    1.8954    1.9627    2.4447    2.8901    2.3804    1.7738    1.8696
      1.7216    0.7591    1.3649    1.7910    1.5208    1.2509    1.5598    0.9701
      2.1689    1.8649    1.4989    2.3048    2.3018    1.6114    1.8403    2.0469
      1.7916    1.2795    1.2178    2.1595    1.8203    1.4045    1.6500    1.4254
      3.5661    2.4085    2.3658    3.3169    3.3825    2.3423    2.6438    2.6736
      2.0415    1.0988    1.3042    1.7585    1.8014    1.3087    1.4904    1.3830
      2.0586    1.4157    1.0859    1.7461    1.9918    1.6662    1.0284    1.0987
   
   D = 
   
      1.8873    1.2317    1.2063    1.4149    1.5166    1.2417    1.3233    1.5767
      2.2288    1.8954    1.9627    2.4447    2.8901    2.3804    1.7738    1.8696
      1.7216    0.7591    1.3649    1.7910    1.5208    1.2509    1.5598    0.9701
      2.1689    1.8649    1.4989    2.3048    2.3018    1.6114    1.8403    2.0469
      1.7916    1.2795    1.2178    2.1595    1.8203    1.4045    1.6500    1.4254
      3.5661    2.4085    2.3658    3.3169    3.3825    2.3423    2.6438    2.6736
      2.0415    1.0988    1.3042    1.7585    1.8014    1.3087    1.4904    1.3830
      2.0586    1.4157    1.0859    1.7461    1.9918    1.6662    1.0284    1.0987
   


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

   
      0.6793    0.7029    0.6928    0.0607    0.1495    0.9105
      0.1160    0.3462    0.2607    0.8655    0.9432    0.2834
      0.5577    0.1651    0.7411    0.6287    0.6358    0.0752
      0.5409    0.3961    0.9275    0.6720    0.2133    0.0935
      0.1045    0.7344    0.1697    0.8492    0.4876    0.8891
   
   
      0.6793
      0.5577
      0.5409
      0.7029
      0.7344
      0.6928
      0.7411
      0.9275
      0.8655
      0.6287
      0.6720
      0.8492
      0.9432
      0.6358
      0.9105
      0.8891
   

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

   
      8.1774    4.2759    1.0062    5.6463    2.9464    1.1948
      7.5242    7.9331    1.8645    9.9029    6.8363    1.8695
      4.6504    5.3604    7.8645    4.9743    0.4626    3.7116
      5.1472    5.2655    6.1530    5.3192    0.1466    1.2868
      6.8135    0.5835    2.2207    4.0732    1.8567    4.4322
   
   
      8.1774    0.0000    0.0000    5.6463    0.0000    0.0000
      7.5242    7.9331    0.0000    9.9029    6.8363    0.0000
      0.0000    5.3604    7.8645    0.0000    0.0000    0.0000
      5.1472    5.2655    6.1530    5.3192    0.0000    0.0000
      6.8135    0.0000    0.0000    0.0000    0.0000    0.0000
   
   
      8.1774    0.0000    0.0000    5.6463    0.0000    0.0000
      7.5242    7.9331    0.0000       NaN    6.8363    0.0000
      0.0000    5.3604    7.8645    0.0000    0.0000    0.0000
      5.1472    5.2655    6.1530    5.3192    0.0000    0.0000
      6.8135    0.0000    0.0000    0.0000    0.0000    0.0000
   

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

   
      6.5000    1.8542    6.5000    4.4446    6.5000    9.6889
      9.1710    2.6262    0.8203    6.5000    1.7727    3.7934
      6.5000    0.7982    4.7851    0.3314    9.3477    6.5000
      6.5000    6.5000    9.0918    8.6501    1.4259    9.6746
      6.5000    8.5130    0.7564    0.3539    2.0815    6.5000
   
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
   
