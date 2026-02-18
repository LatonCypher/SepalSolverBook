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
      0.0950    0.2093    0.2711    0.4390
   
   R1[2] = 0.271128143476563
   C1 = 
      0.0494
      0.8013
      0.1395
      0.4029
      0.3973
      0.0917
      0.1831
      0.6317
   
   C1[5] = 0.09172757241955287

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
      0.6449    0.8776    0.7591    0.9860    0.5626
      0.7687    0.5736    0.4423    0.6840    0.8746
   

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
   
      0.8117    0.0136    0.3074    0.8776    0.0508    0.3784    0.6649    0.8334
      0.6410    0.7811    0.2652    0.7391    0.7853    0.6827    0.0945    0.8697
      0.7726    0.5793    0.8234    0.5620    0.8409    0.0750    0.4716    0.8035
      0.7846    0.0741    0.9233    0.7676    0.7080    0.8346    0.3173    0.2549
      0.2807    0.3069    0.0346    0.7278    0.8198    0.4528    0.7337    0.1974
      0.6234    0.0867    0.0512    0.2333    0.2894    0.0250    0.2655    0.9986
      0.0728    0.7547    0.7610    0.8755    0.5310    0.7462    0.2904    0.9278
      0.5444    0.8234    0.2753    0.9987    0.6489    0.8835    0.0136    0.4115
   
   B = 
   
      0.9935    0.6383    0.9415    0.3523    0.0275    0.5752    0.6553    0.8544
      0.5912    0.4137    0.7146    0.5323    0.6191    0.7001    0.1238    0.0583
      0.5392    0.0160    0.1521    0.7838    0.6930    0.8704    0.9849    0.0601
      0.2162    0.5252    0.1760    0.4113    0.1786    0.8736    0.8594    0.1174
      0.5832    0.6975    0.4483    0.4125    0.9723    0.9780    0.0418    0.5762
      0.3644    0.4125    0.2413    0.7849    0.6384    0.9213    0.7318    0.2126
      0.7774    0.6459    0.6188    0.9444    0.4722    0.3997    0.0911    0.8823
      0.7052    0.4964    0.1199    0.1438    0.7743    0.3931    0.0648    0.4060
   
   C = 
   
      2.4421    2.0242    1.6005    1.9609    1.6508    2.5024    1.9842    1.8505
      2.7949    2.4468    2.0116    2.2276    2.7345    3.5687    2.0106    1.7300
      3.1265    2.3620    2.1487    2.4237    2.7612    3.4534    2.0569    2.0521
      2.6305    2.1189    1.8129    2.6387    2.4129    3.6657    2.7780    1.7891
      1.9890    2.0194    1.5714    2.0036    1.9372    2.6321    1.3267    1.6413
      1.8371    1.4364    1.1174    0.9352    1.3439    1.4724    0.7894    1.3799
      2.5797    2.1569    1.5867    2.5964    3.0012    3.6851    2.2979    1.3522
      2.3931    2.2471    1.8804    2.2897    2.4139    3.6174    2.2896    1.3876
   
   D = 
   
      2.4421    2.0242    1.6005    1.9609    1.6508    2.5024    1.9842    1.8505
      2.7949    2.4468    2.0116    2.2276    2.7345    3.5687    2.0106    1.7300
      3.1265    2.3620    2.1487    2.4237    2.7612    3.4534    2.0569    2.0521
      2.6305    2.1189    1.8129    2.6387    2.4129    3.6657    2.7780    1.7891
      1.9890    2.0194    1.5714    2.0036    1.9372    2.6321    1.3267    1.6413
      1.8371    1.4364    1.1174    0.9352    1.3439    1.4724    0.7894    1.3799
      2.5797    2.1569    1.5867    2.5964    3.0012    3.6851    2.2979    1.3522
      2.3931    2.2471    1.8804    2.2897    2.4139    3.6174    2.2896    1.3876
   


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

   
      0.5515    0.1572    0.5075    0.3882    0.6192    0.5423
      0.7879    0.1081    0.1751    0.3494    0.7843    0.0285
      0.8282    0.4052    0.6062    0.9709    0.6715    0.3665
      0.7306    0.9606    0.1391    0.2396    0.9754    0.1994
      0.2259    0.9538    0.3620    0.9708    0.0950    0.9755
   
   
      0.5515
      0.7879
      0.8282
      0.7306
      0.9606
      0.9538
      0.5075
      0.6062
      0.9709
      0.9708
      0.6192
      0.7843
      0.6715
      0.9754
      0.5423
      0.9755
   

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

   
      3.1972    4.4427    0.5198    4.4743    2.3619    7.3224
      2.1842    2.6553    0.8685    0.9145    4.7168    2.7183
      9.1783    8.0778    5.6508    2.7499    5.8349    2.5089
      5.3318    6.8049    8.8263    1.6550    0.9898    9.3406
      6.4501    0.1539    9.4764    9.2656    7.3877    4.4635
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    7.3224
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      9.1783    8.0778    5.6508    0.0000    5.8349    0.0000
      5.3318    6.8049    8.8263    0.0000    0.0000    9.3406
      6.4501    0.0000    9.4764    9.2656    7.3877    0.0000
   
   
      0.0000    0.0000    0.0000    0.0000    0.0000    7.3224
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
         NaN    8.0778    5.6508    0.0000    5.8349    0.0000
      5.3318    6.8049    8.8263    0.0000    0.0000       NaN
      6.4501    0.0000       NaN       NaN    7.3877    0.0000
   

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

   
      2.6706    1.0683    6.5000    1.2900    0.2253    6.5000
      6.5000    4.3981    4.6668    3.4183    6.5000    3.2597
      9.4096    4.3388    2.7997    1.2955    2.0697    6.5000
      0.3050    2.7643    1.2901    6.5000    3.5353    6.5000
      6.5000    4.6349    1.0430    4.0694    4.1231    6.5000
   
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
   
