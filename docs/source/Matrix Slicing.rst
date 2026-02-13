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
      0.6486    0.4149    0.8833    0.4952
   
   R1[2] = 0.8833204789142048
   C1 = 
      0.4977
      0.5723
      0.8073
      0.0905
      0.9350
      0.9844
      0.9940
      0.7603
   
   C1[5] = 0.9844145519046853

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
      0.9485    0.8139    0.3058    0.2687    0.7869
      0.3006    0.4454    0.0633    0.2806    0.0335
   

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
   
      0.9956    0.7837    0.1616    0.3175    0.4625    0.1257    0.5938    0.2600
      0.2962    0.6667    0.2935    0.9965    0.2360    0.0087    0.7796    0.0958
      0.5751    0.7839    0.0998    0.0888    0.9668    0.5088    0.6287    0.7212
      0.1846    0.8989    0.2842    0.6121    0.9759    0.6449    0.0222    0.6662
      0.5962    0.5106    0.4196    0.2716    0.8781    0.1253    0.9466    0.3219
      0.7826    0.9200    0.9346    0.5380    0.9303    0.7780    0.6098    0.8644
      0.9622    0.2663    0.4318    0.9495    0.3824    0.0806    0.4752    0.2423
      0.8443    0.1474    0.2464    0.9137    0.4922    0.1333    0.0755    0.4615
   
   B = 
   
      0.3966    0.1988    0.5778    0.3004    0.5302    0.6113    0.2274    0.1579
      0.0221    0.4179    0.0928    0.8390    0.3229    0.7313    0.9253    0.8352
      0.7780    0.3613    0.8993    0.9625    0.8456    0.8422    0.3662    0.6082
      0.5176    0.8316    0.5350    0.9250    0.8263    0.5340    0.2964    0.6202
      0.0366    0.5782    0.3647    0.8005    0.5330    0.4721    0.3799    0.6905
      0.8040    0.4552    0.4227    0.1394    0.4155    0.2456    0.1697    0.8447
      0.3126    0.4015    0.7105    0.4363    0.5409    0.9590    0.2430    0.2291
      0.2943    0.0147    0.6969    0.1420    0.8233    0.3689    0.8233    0.5636
   
   C = 
   
      1.0824    1.4148    1.7882    2.0897    2.0140    2.4021    1.6604    1.8151
      1.1638    1.7270    1.7406    2.3965    2.0739    2.3445    1.4466    1.8030
      1.2222    1.6054    2.0592    2.2301    2.3763    2.5066    2.1192    2.5091
      1.3882    1.9005    1.8817    2.6248    2.4827    2.2226    2.1933    2.9314
      1.2383    1.6590    2.1848    2.4418    2.3574    2.7082    1.6922    2.0547
      2.4408    2.4747    3.3697    3.6461    3.8074    3.7595    2.8763    3.7203
      1.5135    1.7003    2.1570    2.3654    2.4397    2.3990    1.3787    1.8033
      1.2873    1.4607    1.8231    1.9707    2.1971    1.8271    1.2974    1.7028
   
   D = 
   
      1.0824    1.4148    1.7882    2.0897    2.0140    2.4021    1.6604    1.8151
      1.1638    1.7270    1.7406    2.3965    2.0739    2.3445    1.4466    1.8030
      1.2222    1.6054    2.0592    2.2301    2.3763    2.5066    2.1192    2.5091
      1.3882    1.9005    1.8817    2.6248    2.4827    2.2226    2.1933    2.9314
      1.2383    1.6590    2.1848    2.4418    2.3574    2.7082    1.6922    2.0547
      2.4408    2.4747    3.3697    3.6461    3.8074    3.7595    2.8763    3.7203
      1.5135    1.7003    2.1570    2.3654    2.4397    2.3990    1.3787    1.8033
      1.2873    1.4607    1.8231    1.9707    2.1971    1.8271    1.2974    1.7028
   


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

   
      0.2280    0.2677    0.5671    0.5055    0.7606    0.9512
      0.8825    0.6442    0.5101    0.7428    0.6031    0.1325
      0.5301    0.4939    0.0860    0.7176    0.2555    0.0563
      0.9123    0.4751    0.0561    0.5551    0.3365    0.6858
      0.4140    0.2125    0.8173    0.4514    0.0317    0.5571
   
   
      0.8825
      0.5301
      0.9123
      0.6442
      0.5671
      0.5101
      0.8173
      0.5055
      0.7428
      0.7176
      0.5551
      0.7606
      0.6031
      0.9512
      0.6858
      0.5571
   

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

   
      2.7747    3.0122    1.6699    5.4480    4.2330    6.9861
      7.3665    1.3620    7.6368    8.7257    8.7703    9.6475
      5.5068    4.5735    9.4361    5.3819    5.3109    5.9762
      9.1324    3.5287    9.5168    3.9536    2.4576    9.5648
      4.4873    4.1552    5.6273    7.1590    1.4085    7.8571
   
   
      0.0000    0.0000    0.0000    5.4480    0.0000    6.9861
      7.3665    0.0000    7.6368    8.7257    8.7703    9.6475
      5.5068    0.0000    9.4361    5.3819    5.3109    5.9762
      9.1324    0.0000    9.5168    0.0000    0.0000    9.5648
      0.0000    0.0000    5.6273    7.1590    0.0000    7.8571
   
   
      0.0000    0.0000    0.0000    5.4480    0.0000    6.9861
      7.3665    0.0000    7.6368    8.7257    8.7703       NaN
      5.5068    0.0000       NaN    5.3819    5.3109    5.9762
         NaN    0.0000       NaN    0.0000    0.0000       NaN
      0.0000    0.0000    5.6273    7.1590    0.0000    7.8571
   

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

   
      6.5000    6.5000    6.5000    6.5000    0.2350    6.5000
      8.9309    9.0098    4.1268    6.5000    2.2890    6.5000
      6.5000    8.4618    0.0911    0.4522    8.5733    6.5000
      3.2132    6.5000    6.5000    8.8180    8.6936    1.1243
      0.1060    9.8377    3.3452    3.4085    1.1100    2.2007
   
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
   
