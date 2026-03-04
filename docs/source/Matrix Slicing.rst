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
      0.5463    0.9654    0.1275    0.4654
   
   R1[2] = 0.12745814225350638
   C1 = 
      0.6269
      0.9252
      0.6982
      0.8109
      0.7095
      0.0994
      0.7139
      0.0900
   
   C1[5] = 0.09936128166766278

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
      0.8300    0.2915    0.2275    0.5901    0.1350
      0.0233    0.6836    0.5405    0.5938    0.4188
   

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
   
      0.6357    0.2173    0.6489    0.5559    0.9919    0.6294    0.2195    0.5902
      0.6218    0.1036    0.3239    0.3207    0.6351    0.1871    0.3370    0.6060
      0.5152    0.3552    0.5039    0.3729    0.0095    0.4267    0.0057    0.8356
      0.7910    0.4053    0.4464    0.5186    0.7491    0.3545    0.8741    0.3307
      0.8620    0.3793    0.1217    0.3448    0.9782    0.8074    0.6019    0.3507
      0.8409    0.5478    0.3192    0.6210    0.3249    0.7838    0.8094    0.7562
      0.5913    0.0582    0.5315    0.4658    0.8708    0.2192    0.5738    0.5072
      0.6866    0.5070    0.4820    0.1885    0.0888    0.1883    0.9987    0.6289
   
   B = 
   
      0.8983    0.3604    0.9005    0.4937    0.2326    0.1776    0.0993    0.6726
      0.6266    0.4870    0.2549    0.6588    0.1910    0.2700    0.2321    0.9447
      0.7941    0.3395    0.1504    0.7590    0.4357    0.6872    0.4027    0.1852
      0.2297    0.2515    0.1127    0.3491    0.4391    0.9263    0.5809    0.6315
      0.1691    0.2689    0.0470    0.5043    0.3199    0.7685    0.5641    0.0307
      0.3373    0.4140    0.2757    0.2534    0.4646    0.4187    0.3651    0.7299
      0.9195    0.6866    0.2279    0.8536    0.2704    0.9609    0.2567    0.2485
      0.1582    0.1609    0.1027    0.5038    0.4312    0.8924    0.1186    0.6852
   
   C = 
   
      2.0255    1.4680    1.1189    2.2880    1.6398    2.8959    1.6135    2.0528
      1.5307    1.0423    0.8917    1.6937    1.0889    2.0891    0.9875    1.4336
      1.4542    0.9411    0.8775    1.5398    1.1341    1.8162    0.8149    1.8966
      2.5404    1.7659    1.3073    2.5577    1.4668    2.8962    1.4692    2.0505
      2.2346    1.6904    1.3719    2.2767    1.4794    2.6397    1.4656    2.1876
      2.6780    1.9236    1.5084    2.6695    1.7250    3.1223    1.4669    2.8357
      1.9258    1.3395    0.9640    2.1363    1.3390    2.6825    1.3355    1.5221
      2.4569    1.5941    1.1896    2.3666    1.2064    2.4327    0.9393    1.9683
   
   D = 
   
      2.0255    1.4680    1.1189    2.2880    1.6398    2.8959    1.6135    2.0528
      1.5307    1.0423    0.8917    1.6937    1.0889    2.0891    0.9875    1.4336
      1.4542    0.9411    0.8775    1.5398    1.1341    1.8162    0.8149    1.8966
      2.5404    1.7659    1.3073    2.5577    1.4668    2.8962    1.4692    2.0505
      2.2346    1.6904    1.3719    2.2767    1.4794    2.6397    1.4656    2.1876
      2.6780    1.9236    1.5084    2.6695    1.7250    3.1223    1.4669    2.8357
      1.9258    1.3395    0.9640    2.1363    1.3390    2.6825    1.3355    1.5221
      2.4569    1.5941    1.1896    2.3666    1.2064    2.4327    0.9393    1.9683
   


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

   
      0.4361    0.4621    0.3800    0.1333    0.3601    0.1662
      0.5042    0.3236    0.6503    0.5396    0.8917    0.8949
      0.3311    0.5623    0.8652    0.6854    0.1553    0.2460
      0.6292    0.1763    0.4524    0.1771    0.3131    0.7902
      0.5833    0.9622    0.7241    0.5451    0.7038    0.4589
   
   
      0.5042
      0.6292
      0.5833
      0.5623
      0.9622
      0.6503
      0.8652
      0.7241
      0.5396
      0.6854
      0.5451
      0.8917
      0.7038
      0.8949
      0.7902
   

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

   
      8.0598    8.6614    4.4350    1.3198    1.2932    1.1302
      9.7011    0.7301    0.4896    5.5799    8.1902    0.2852
      0.0089    2.3935    4.1702    7.8708    8.8060    4.5636
      9.5744    7.0879    9.7995    0.9814    3.4680    3.3561
      7.7060    9.0835    9.1687    9.7290    6.9259    8.9656
   
   
      8.0598    8.6614    0.0000    0.0000    0.0000    0.0000
      9.7011    0.0000    0.0000    5.5799    8.1902    0.0000
      0.0000    0.0000    0.0000    7.8708    8.8060    0.0000
      9.5744    7.0879    9.7995    0.0000    0.0000    0.0000
      7.7060    9.0835    9.1687    9.7290    6.9259    8.9656
   
   
      8.0598    8.6614    0.0000    0.0000    0.0000    0.0000
         NaN    0.0000    0.0000    5.5799    8.1902    0.0000
      0.0000    0.0000    0.0000    7.8708    8.8060    0.0000
         NaN    7.0879       NaN    0.0000    0.0000    0.0000
      7.7060       NaN       NaN       NaN    6.9259    8.9656
   

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

   
      9.0557    3.8793    6.5000    1.4778    3.2323    4.4822
      9.4076    0.1294    1.5754    4.2842    4.9622    1.9209
      6.5000    6.5000    8.4215    6.5000    2.3869    6.5000
      2.4367    1.7953    9.3693    6.5000    8.2905    2.2617
      0.6787    4.2970    6.5000    6.5000    8.6805    0.3665
   
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
   
