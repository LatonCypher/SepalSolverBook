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
      0.8418    0.1637    0.1819    0.7219
   
   R1[2] = 0.18193067132678287
   C1 = 
      0.7776
      0.7088
      0.1093
      0.5554
      0.9314
      0.2938
      0.8085
      0.8073
   
   C1[5] = 0.2937845752031699

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
      0.7271    0.3220    0.3290    0.7225    0.2975
      0.4617    0.5675    0.7552    0.3486    0.8900
   

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
   
      0.0395    0.4451    0.1852    0.6158    0.6715    0.8442    0.8185    0.7043
      0.0462    0.1951    0.9601    0.3793    0.5533    0.4404    0.7347    0.9163
      0.5445    0.0612    0.1111    0.1407    0.7184    0.5626    0.3403    0.0396
      0.5283    0.1055    0.0188    0.7362    0.5363    0.5704    0.2855    0.4437
      0.1382    0.3310    0.0655    0.6447    0.0460    0.6175    0.8224    0.0316
      0.7539    0.2124    0.6018    0.7799    0.9420    0.2412    0.4983    0.3369
      0.8496    0.3284    0.2265    0.3196    0.3625    0.6201    0.4730    0.1388
      0.0547    0.9789    0.7462    0.6324    0.2965    0.9591    0.8294    0.9887
   
   B = 
   
      0.3078    0.1003    0.7861    0.5032    0.8267    0.6872    0.3555    0.6434
      0.7475    0.3899    0.4598    0.5691    0.3490    0.3543    0.1500    0.6849
      0.6290    0.5535    0.9484    0.9810    0.0888    0.9884    0.3988    0.9211
      0.8193    0.2178    0.8848    0.5535    0.9229    0.8823    0.3348    0.6065
      0.2637    0.8508    0.0898    0.5422    0.2771    0.1248    0.8986    0.7504
      0.3239    0.7286    0.6610    0.3608    0.1094    0.4671    0.0084    0.9529
      0.2342    0.4774    0.6048    0.2351    0.0363    0.7191    0.5999    0.5588
      0.8779    0.3539    0.9840    0.6357    0.3503    0.1823    0.6861    0.4706
   
   C = 
   
      2.2264    2.2405    2.7626    2.1045    1.3277    2.1064    1.9456    2.9715
      2.3398    2.1613    3.0591    2.5002    1.0908    2.3547    2.1259    2.9544
      0.8847    1.3682    1.3672    1.1934    0.8981    1.2342    1.1758    1.8639
      1.6391    1.4301    2.1675    1.5976    1.5315    1.6881    1.4200    2.1903
      1.2919    1.2123    1.8341    1.1402    0.9518    1.7371    0.9022    1.8643
      2.1473    1.9957    2.8281    2.4514    1.8942    2.5263    2.1796    3.0316
      1.4404    1.4434    2.1816    1.6332    1.3662    1.9064    1.2586    2.3666
      3.1872    2.6348    3.8954    2.9970    1.6004    2.9415    2.1260    3.8416
   
   D = 
   
      2.2264    2.2405    2.7626    2.1045    1.3277    2.1064    1.9456    2.9715
      2.3398    2.1613    3.0591    2.5002    1.0908    2.3547    2.1259    2.9544
      0.8847    1.3682    1.3672    1.1934    0.8981    1.2342    1.1758    1.8639
      1.6391    1.4301    2.1675    1.5976    1.5315    1.6881    1.4200    2.1903
      1.2919    1.2123    1.8341    1.1402    0.9518    1.7371    0.9022    1.8643
      2.1473    1.9957    2.8281    2.4514    1.8942    2.5263    2.1796    3.0316
      1.4404    1.4434    2.1816    1.6332    1.3662    1.9064    1.2586    2.3666
      3.1872    2.6348    3.8954    2.9970    1.6004    2.9415    2.1260    3.8416
   


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

   
      0.4297    0.1804    0.3648    0.2384    0.9892    0.5970
      0.1174    0.6013    0.2871    0.6440    0.3860    0.6087
      0.4518    0.1787    0.2692    0.0747    0.7488    0.3654
      0.7758    0.5592    0.4441    0.5891    0.7915    0.7284
      0.1101    0.2536    0.5348    0.8960    0.4265    0.6784
   
   
      0.7758
      0.6013
      0.5592
      0.5348
      0.6440
      0.5891
      0.8960
      0.9892
      0.7488
      0.7915
      0.5970
      0.6087
      0.7284
      0.6784
   

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

   
      8.5580    9.1602    7.1289    4.8196    8.7667    4.6841
      6.7803    5.5288    4.9206    4.0946    6.9762    9.9160
      8.2185    1.1963    0.8769    7.8005    0.2900    1.4610
      4.0682    2.1021    6.5223    0.2262    0.5765    1.9725
      9.2000    6.7453    5.0045    2.0536    5.6501    0.1694
   
   
      8.5580    9.1602    7.1289    0.0000    8.7667    0.0000
      6.7803    5.5288    0.0000    0.0000    6.9762    9.9160
      8.2185    0.0000    0.0000    7.8005    0.0000    0.0000
      0.0000    0.0000    6.5223    0.0000    0.0000    0.0000
      9.2000    6.7453    5.0045    0.0000    5.6501    0.0000
   
   
      8.5580       NaN    7.1289    0.0000    8.7667    0.0000
      6.7803    5.5288    0.0000    0.0000    6.9762       NaN
      8.2185    0.0000    0.0000    7.8005    0.0000    0.0000
      0.0000    0.0000    6.5223    0.0000    0.0000    0.0000
         NaN    6.7453    5.0045    0.0000    5.6501    0.0000
   

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

   
      4.5708    6.5000    3.9071    6.5000    0.8813    8.8084
      6.5000    8.3399    2.6801    0.5268    1.1339    3.8561
      6.5000    3.0569    2.2741    9.1494    3.9144    1.2264
      9.6581    3.6796    6.5000    8.8088    0.4702    4.0753
      9.7314    6.5000    3.7022    3.2433    9.6233    6.5000
   
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
   
