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
      0.3387    0.8838    0.2779    0.5297
   
   R1[2] = 0.27792774676130483
   C1 = 
      0.3188
      0.5056
      0.5668
      0.6017
      0.1443
      0.6342
      0.3080
      0.4621
   
   C1[5] = 0.6341731499374039

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
      0.5770    0.6479    0.6704    0.6235    0.0335
      0.7943    0.2186    0.5142    0.6901    0.7924
   

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
   
      0.0679    0.8586    0.5551    0.8107    0.1507    0.8380    0.1823    0.2893
      0.3886    0.2873    0.2435    0.7904    0.4101    0.0637    0.7224    0.3327
      0.9422    0.0428    0.5791    0.4304    0.8270    0.2240    0.2166    0.0519
      0.1278    0.7781    0.2798    0.9607    0.7688    0.0567    0.6016    0.7484
      0.7378    0.3117    0.2007    0.7719    0.2472    0.0942    0.1235    0.4120
      0.6850    0.6894    0.8142    0.8745    0.1539    0.8926    0.1951    0.1429
      0.1538    0.1821    0.6532    0.8298    0.4482    0.0642    0.7581    0.0648
      0.8896    0.1773    0.7197    0.6575    0.6436    0.5591    0.7498    0.1164
   
   B = 
   
      0.1499    0.5957    0.4860    0.8032    0.5695    0.8376    0.6703    0.0198
      0.7999    0.6578    0.3334    0.2060    0.0224    0.4305    0.3079    0.5879
      0.1825    0.3163    0.7845    0.1600    0.3656    0.1386    0.7222    0.1665
      0.0706    0.3301    0.3933    0.2880    0.0057    0.9705    0.7027    0.5871
      0.8999    0.3678    0.9983    0.0625    0.3737    0.6177    0.9019    0.7227
      0.0104    0.2434    0.9374    0.9861    0.9658    0.7397    0.1164    0.7347
      0.9020    0.8631    0.4446    0.0947    0.0355    0.3920    0.2619    0.1400
      0.0047    0.7119    0.8807    0.5938    0.6667    0.0919    0.2639    0.6643
   
   C = 
   
      1.1656    1.6712    2.3454    1.5785    1.3305    2.1013    1.6380    2.0169
      1.4113    1.7851    1.8699    0.9923    0.7836    1.8642    1.7345    1.3465
      1.2537    1.4973    2.2733    1.3061    1.3195    2.0718    2.2077    1.2200
      1.9991    2.3422    2.6662    1.1896    1.0605    2.2345    2.2574    2.2493
      0.7879    1.4766    1.6764    1.2759    0.9674    1.8377    1.6528    1.2234
      1.1889    1.9517    2.7483    2.0673    1.7300    2.6770    2.2053    1.9571
      1.4345    1.5728    1.8759    0.7060    0.6349    1.7305    1.8412    1.2262
      1.7147    2.1940    2.9170    1.7871    1.6622    2.6750    2.5052    1.6859
   
   D = 
   
      1.1656    1.6712    2.3454    1.5785    1.3305    2.1013    1.6380    2.0169
      1.4113    1.7851    1.8699    0.9923    0.7836    1.8642    1.7345    1.3465
      1.2537    1.4973    2.2733    1.3061    1.3195    2.0718    2.2077    1.2200
      1.9991    2.3422    2.6662    1.1896    1.0605    2.2345    2.2574    2.2493
      0.7879    1.4766    1.6764    1.2759    0.9674    1.8377    1.6528    1.2234
      1.1889    1.9517    2.7483    2.0673    1.7300    2.6770    2.2053    1.9571
      1.4345    1.5728    1.8759    0.7060    0.6349    1.7305    1.8412    1.2262
      1.7147    2.1940    2.9170    1.7871    1.6622    2.6750    2.5052    1.6859
   


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

   
      0.5836    0.4588    0.7946    0.8552    0.8613    0.0774
      0.2587    0.6789    0.8060    0.9881    0.1063    0.9495
      0.5718    0.6913    0.1735    0.8993    0.5934    0.9755
      0.0471    0.1435    0.7512    0.8126    0.3388    0.4697
      0.1783    0.9631    0.3266    0.5689    0.1389    0.1701
   
   
      0.5836
      0.5718
      0.6789
      0.6913
      0.9631
      0.7946
      0.8060
      0.7512
      0.8552
      0.9881
      0.8993
      0.8126
      0.5689
      0.8613
      0.5934
      0.9495
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

   
      2.3232    7.7482    6.1772    7.2864    2.7390    5.0732
      1.7012    4.3469    6.2255    0.7605    9.1650    3.8250
      5.0863    3.4034    6.5253    8.0933    8.7641    4.6075
      4.4058    0.1630    1.5097    5.9260    7.1341    6.1738
      2.5865    4.8112    2.5967    1.7719    8.5788    9.1961
   
   
      0.0000    7.7482    6.1772    7.2864    0.0000    5.0732
      0.0000    0.0000    6.2255    0.0000    9.1650    0.0000
      5.0863    0.0000    6.5253    8.0933    8.7641    0.0000
      0.0000    0.0000    0.0000    5.9260    7.1341    6.1738
      0.0000    0.0000    0.0000    0.0000    8.5788    9.1961
   
   
      0.0000    7.7482    6.1772    7.2864    0.0000    5.0732
      0.0000    0.0000    6.2255    0.0000       NaN    0.0000
      5.0863    0.0000    6.5253    8.0933    8.7641    0.0000
      0.0000    0.0000    0.0000    5.9260    7.1341    6.1738
      0.0000    0.0000    0.0000    0.0000    8.5788       NaN
   

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

   
      0.0363    8.6054    8.1744    6.5000    3.7637    4.9615
      0.8027    1.7467    3.5126    9.3324    4.1633    1.9328
      0.0394    1.5445    1.6992    2.3752    6.5000    2.9561
      2.8441    4.7751    1.6889    9.2047    1.1263    2.4636
      4.5765    1.5013    1.7973    6.5000    6.5000    0.8269
   
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
   
