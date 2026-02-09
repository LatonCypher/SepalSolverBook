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
      0.1825    0.5281    0.1838    0.1243
   
   R1[2] = 0.18381518937449703
   C1 = 
      0.9146
      0.4245
      0.9123
      0.7823
      0.3355
      0.4486
      0.3894
      0.0733
   
   C1[5] = 0.4486018885559523

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
      0.2087    0.2901    0.7729    0.2836    0.1650
      0.3305    0.1939    0.5699    0.2373    0.8363
   

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
   
      0.0058    0.1456    0.1264    0.8576    0.1609    0.0688    0.2881    0.4004
      0.7672    0.9623    0.6652    0.5208    0.0654    0.4548    0.7235    0.1902
      0.5259    0.0341    0.9729    0.7870    0.8002    0.4403    0.7927    0.7670
      0.7689    0.1724    0.5618    0.5997    0.2549    0.9243    0.3285    0.3554
      0.2754    0.3710    0.6340    0.6989    0.0768    0.0430    0.6860    0.5721
      0.3370    0.6007    0.5610    0.8686    0.9484    0.5951    0.5470    0.5779
      0.7217    0.1006    0.8087    0.4486    0.3916    0.8632    0.7150    0.3547
      0.4686    0.5291    0.1400    0.6253    0.3976    0.1226    0.4034    0.1668
   
   B = 
   
      0.1797    0.9465    0.2608    0.6085    0.5138    0.1782    0.8802    0.6323
      0.9318    0.4539    0.0668    0.4519    0.6715    0.1916    0.5878    0.5502
      0.2098    0.8503    0.3898    0.0313    0.8930    0.1444    0.3722    0.7281
      0.0553    0.7532    0.7250    0.5267    0.8702    0.3669    0.2397    0.7648
      0.4155    0.4536    0.5074    0.3844    0.0314    0.7134    0.2054    0.7639
      0.7091    0.8880    0.7488    0.8089    0.2532    0.5955    0.1031    0.3775
      0.8618    0.2942    0.3264    0.9870    0.0518    0.1419    0.4105    0.5076
      0.1786    0.5856    0.5073    0.6281    0.7758    0.5808    0.4034    0.4225
   
   C = 
   
      0.6461    1.2783    1.1125    1.1783    1.3080    0.7910    0.6632    1.2960
      2.2100    2.8787    1.6075    2.4235    2.3899    1.1388    2.0474    2.5664
      1.8388    3.3696    2.4727    2.7083    2.6194    1.9206    1.8782    3.1654
      1.5578    2.9766    1.9747    2.2723    2.0692    1.4565    1.5568    2.3092
      1.3226    2.1044    1.4357    1.8239    2.0577    0.9781    1.3965    2.0392
      2.1766    3.1809    2.3749    2.7003    2.4905    2.0191    1.7805    3.0872
      1.8722    3.1164    2.0936    2.5234    2.0940    1.5301    1.7089    2.5812
      1.2707    1.7793    1.1752    1.6129    1.4591    0.9453    1.2526    1.7928
   
   D = 
   
      0.6461    1.2783    1.1125    1.1783    1.3080    0.7910    0.6632    1.2960
      2.2100    2.8787    1.6075    2.4235    2.3899    1.1388    2.0474    2.5664
      1.8388    3.3696    2.4727    2.7083    2.6194    1.9206    1.8782    3.1654
      1.5578    2.9766    1.9747    2.2723    2.0692    1.4565    1.5568    2.3092
      1.3226    2.1044    1.4357    1.8239    2.0577    0.9781    1.3965    2.0392
      2.1766    3.1809    2.3749    2.7003    2.4905    2.0191    1.7805    3.0872
      1.8722    3.1164    2.0936    2.5234    2.0940    1.5301    1.7089    2.5812
      1.2707    1.7793    1.1752    1.6129    1.4591    0.9453    1.2526    1.7928
   


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

   
      0.2644    0.8513    0.1779    0.6564    0.1793    0.5382
      0.4621    0.0753    0.3522    0.5526    0.3875    0.1055
      0.7270    0.2871    0.6672    0.4763    0.0350    0.4050
      0.4199    0.8260    0.3187    0.3116    0.9362    0.0925
      0.6089    0.1935    0.5498    0.0936    0.4260    0.8276
   
   
      0.7270
      0.6089
      0.8513
      0.8260
      0.6672
      0.5498
      0.6564
      0.5526
      0.9362
      0.5382
      0.8276
   

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

   
      9.1110    0.0075    4.3950    0.5684    3.7213    9.7076
      1.2296    5.3777    4.4655    5.7937    0.9573    7.6243
      2.2404    6.7104    8.4686    9.9763    0.9448    5.1287
      3.8087    8.2240    4.0401    4.1279    1.8663    5.7403
      2.6768    0.5016    0.0308    4.2563    5.8498    4.3617
   
   
      9.1110    0.0000    0.0000    0.0000    0.0000    9.7076
      0.0000    5.3777    0.0000    5.7937    0.0000    7.6243
      0.0000    6.7104    8.4686    9.9763    0.0000    5.1287
      0.0000    8.2240    0.0000    0.0000    0.0000    5.7403
      0.0000    0.0000    0.0000    0.0000    5.8498    0.0000
   
   
         NaN    0.0000    0.0000    0.0000    0.0000       NaN
      0.0000    5.3777    0.0000    5.7937    0.0000    7.6243
      0.0000    6.7104    8.4686       NaN    0.0000    5.1287
      0.0000    8.2240    0.0000    0.0000    0.0000    5.7403
      0.0000    0.0000    0.0000    0.0000    5.8498    0.0000
   

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

   
      3.5611    8.4673    1.6213    2.7165    6.5000    1.8157
      9.1665    6.5000    2.0147    8.4489    2.4308    1.1232
      9.5811    2.1769    6.5000    2.1921    0.3445    4.7817
      8.2714    6.5000    6.5000    8.5289    2.5153    9.8365
      8.6173    6.5000    6.5000    0.5158    2.2429    2.8077
   
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
   
