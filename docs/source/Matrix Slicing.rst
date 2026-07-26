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
      0.5259    0.4016    0.3739    0.2235
   
   R1[2] = 0.37389777720964945
   C1 = 
      0.6492
      0.4586
      0.5124
      0.3628
      0.2405
      0.1114
      0.5272
      0.2184
   
   C1[5] = 0.1113742711405784

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
      0.7177    0.4496    0.8195    0.4495    0.6551
      0.3011    0.7979    0.9213    0.6093    0.4531
   

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
   
      0.7564    0.9559    0.2167    0.9466    0.4404    0.7120    0.7404    0.7741
      0.0910    0.5570    0.9034    0.9028    0.6718    0.6338    0.8717    0.4961
      0.7305    0.6366    0.5357    0.3825    0.4588    0.9225    0.9914    0.0491
      0.3856    0.8957    0.9254    0.1924    0.0256    0.0640    0.2313    0.1021
      0.1511    0.2120    0.9202    0.6908    0.9865    0.0973    0.9450    0.2625
      0.9582    0.6862    0.9764    0.0091    0.4160    0.2309    0.7354    0.6565
      0.2483    0.2587    0.5055    0.3967    0.0535    0.3703    0.1324    0.2295
      0.9230    0.3014    0.0345    0.2352    0.3597    0.0893    0.1966    0.1132
   
   B = 
   
      0.8242    0.1806    0.6034    0.9877    0.0873    0.3607    0.9997    0.3735
      0.8411    0.0941    0.9226    0.5383    0.6664    0.3571    0.8346    0.0483
      0.4479    0.5614    0.6292    0.4824    0.6916    0.8701    0.6878    0.3137
      0.3590    0.7019    0.4381    0.8037    0.8822    0.2089    0.3281    0.2204
      0.0581    0.1968    0.0400    0.5017    0.4518    0.6874    0.5919    0.9071
      0.5075    0.3105    0.1374    0.8998    0.4060    0.1257    0.6509    0.7817
      0.9436    0.4515    0.4573    0.3635    0.7816    0.5458    0.1314    0.8620
      0.5117    0.4772    0.7368    0.7229    0.7250    0.6890    0.5805    0.5676
   
   C = 
   
      3.3459    2.0241    2.9138    3.8173    3.3159    2.3302    3.2843    2.6389
      2.7093    2.1690    2.4109    3.1340    3.4021    2.5654    2.6861    2.6810
      2.9703    1.6089    2.1675    3.0863    2.5882    2.0432    2.7864    2.5758
      1.8592    0.9866    1.9165    1.6924    1.7326    1.5265    1.9792    0.8506
      2.0957    1.8250    1.8468    2.3783    2.8144    2.4622    2.1113    2.4421
      2.9787    1.5911    2.6980    2.9526    2.5567    2.6109    3.0794    2.2638
      1.2245    0.9262    1.1631    1.5213    1.3380    1.0184    1.3653    0.9337
      1.4238    0.6209    1.1598    1.6937    0.9473    0.9635    1.6378    1.0518
   
   D = 
   
      3.3459    2.0241    2.9138    3.8173    3.3159    2.3302    3.2843    2.6389
      2.7093    2.1690    2.4109    3.1340    3.4021    2.5654    2.6861    2.6810
      2.9703    1.6089    2.1675    3.0863    2.5882    2.0432    2.7864    2.5758
      1.8592    0.9866    1.9165    1.6924    1.7326    1.5265    1.9792    0.8506
      2.0957    1.8250    1.8468    2.3783    2.8144    2.4622    2.1113    2.4421
      2.9787    1.5911    2.6980    2.9526    2.5567    2.6109    3.0794    2.2638
      1.2245    0.9262    1.1631    1.5213    1.3380    1.0184    1.3653    0.9337
      1.4238    0.6209    1.1598    1.6937    0.9473    0.9635    1.6378    1.0518
   


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

   
      0.5129    0.3813    0.5947    0.7639    0.9051    0.8498
      0.1331    0.8183    0.3552    0.7451    0.0526    0.9386
      0.9409    0.4214    0.6927    0.5672    0.6259    0.0624
      0.9177    0.4406    0.6722    0.2248    0.2703    0.5503
      0.5748    0.5533    0.2911    0.9792    0.3483    0.6161
   
   
      0.5129
      0.9409
      0.9177
      0.5748
      0.8183
      0.5533
      0.5947
      0.6927
      0.6722
      0.7639
      0.7451
      0.5672
      0.9792
      0.9051
      0.6259
      0.8498
      0.9386
      0.5503
      0.6161
   

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

   
      3.8029    9.0893    9.8173    7.5209    3.8660    5.4501
      4.8590    1.2918    0.0767    4.2802    3.1938    1.1146
      2.1837    4.3020    6.6844    5.6843    6.7892    8.0511
      5.8786    9.1940    8.4967    2.8888    8.8003    7.6685
      9.6484    6.4812    6.4366    3.2445    7.9349    8.1755
   
   
      0.0000    9.0893    9.8173    7.5209    0.0000    5.4501
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.6844    5.6843    6.7892    8.0511
      5.8786    9.1940    8.4967    0.0000    8.8003    7.6685
      9.6484    6.4812    6.4366    0.0000    7.9349    8.1755
   
   
      0.0000       NaN       NaN    7.5209    0.0000    5.4501
      0.0000    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    6.6844    5.6843    6.7892    8.0511
      5.8786       NaN    8.4967    0.0000    8.8003    7.6685
         NaN    6.4812    6.4366    0.0000    7.9349    8.1755
   

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

   
      1.4794    0.9729    8.8525    1.5493    2.1817    2.6328
      9.2375    3.5501    8.5416    6.5000    6.5000    9.9227
      6.5000    9.1683    6.5000    6.5000    9.7100    8.6814
      0.1127    9.5159    6.5000    6.5000    2.4477    0.4298
      6.5000    1.8105    2.4704    6.5000    6.5000    6.5000
   
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
   
