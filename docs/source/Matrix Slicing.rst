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
      0.8570    0.8525    0.3567    0.1079
   
   R1[2] = 0.35665595885471657
   C1 = 
      0.1928
      0.2681
      0.4244
      0.5805
      0.4746
      0.1173
      0.8271
      0.8215
   
   C1[5] = 0.11730040880144676

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
      0.5930    0.8307    0.4259    0.2193    0.2396
      0.7176    0.0703    0.0201    0.4971    0.1091
   

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
   
      0.6085    0.9411    0.5135    0.7086    0.8824    0.3324    0.7961    0.3755
      0.1433    0.2346    0.7887    0.6523    0.4677    0.6151    0.4473    0.8693
      0.5431    0.5814    0.1421    0.4947    0.3939    0.4378    0.9321    0.4491
      0.5478    0.1822    0.9162    0.3502    0.3657    0.9410    0.3952    0.8847
      0.7186    0.7400    0.7360    0.3489    0.5652    0.6322    0.5098    0.1881
      0.3914    0.2785    0.2577    0.7276    0.6117    0.2121    0.3164    0.9925
      0.8851    0.1911    0.8092    0.8498    0.4895    0.3432    0.7250    0.0039
      0.4723    0.0944    0.7928    0.1661    0.2147    0.5030    0.8374    0.9706
   
   B = 
   
      0.8007    0.0300    0.8756    0.9025    0.8251    0.3411    0.6356    0.5661
      0.6999    0.3235    0.8527    0.2877    0.0786    0.1672    0.0995    0.7969
      0.7299    0.5301    0.7682    0.1832    0.9217    0.5151    0.5350    0.9084
      0.2836    0.2780    0.3597    0.3722    0.9142    0.8312    0.7474    0.8373
      0.3383    0.3041    0.3837    0.7483    0.1988    0.1168    0.8748    0.5343
      0.2794    0.7968    0.4029    0.8372    0.7595    0.8178    0.5481    0.7519
      0.4246    0.1782    0.1631    0.3761    0.2329    0.3674    0.0148    0.4670
      0.7942    0.6181    0.4154    0.9322    0.4417    0.6150    0.7465    0.3638
   
   C = 
   
      2.7493    1.6991    2.7430    2.7658    2.4763    2.1168    2.5310    3.3840
      2.2501    1.9290    2.0274    2.4277    2.5083    2.2932    2.4258    2.7682
      2.0938    1.3295    1.9245    2.2980    1.9033    1.7896    1.7824    2.4523
      2.5914    2.1367    2.4163    2.8799    2.9012    2.4819    2.6203    2.9902
      2.4632    1.6308    2.5840    2.4456    2.4427    1.9240    2.1739    3.0410
      2.0916    1.4656    1.8240    2.4310    2.0423    1.8892    2.3552    2.2820
      2.2466    1.3075    2.3112    2.2483    2.7966    2.0638    2.2798    2.9595
      2.4096    1.7264    1.9876    2.4620    2.3282    2.0644    2.0584    2.4390
   
   D = 
   
      2.7493    1.6991    2.7430    2.7658    2.4763    2.1168    2.5310    3.3840
      2.2501    1.9290    2.0274    2.4277    2.5083    2.2932    2.4258    2.7682
      2.0938    1.3295    1.9245    2.2980    1.9033    1.7896    1.7824    2.4523
      2.5914    2.1367    2.4163    2.8799    2.9012    2.4819    2.6203    2.9902
      2.4632    1.6308    2.5840    2.4456    2.4427    1.9240    2.1739    3.0410
      2.0916    1.4656    1.8240    2.4310    2.0423    1.8892    2.3552    2.2820
      2.2466    1.3075    2.3112    2.2483    2.7966    2.0638    2.2798    2.9595
      2.4096    1.7264    1.9876    2.4620    2.3282    2.0644    2.0584    2.4390
   


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

   
      0.1676    0.4168    0.6513    0.4200    0.1660    0.2471
      0.4112    0.6546    0.4387    0.8210    0.0471    0.5574
      0.0698    0.1789    0.1083    0.1764    0.4132    0.7403
      0.4217    0.3875    0.7506    0.4856    0.6657    0.0994
      0.7196    0.4291    0.1347    0.3167    0.2496    0.1979
   
   
      0.7196
      0.6546
      0.6513
      0.7506
      0.8210
      0.6657
      0.5574
      0.7403
   

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

   
      7.4550    1.0256    9.3140    2.3815    5.5977    2.2567
      7.4945    1.1708    0.7722    6.4703    8.2059    3.0947
      8.8298    5.9857    7.0165    2.9701    2.0106    2.2457
      3.1930    2.9694    2.6208    8.9201    3.7072    5.7546
      0.2837    0.7592    2.4424    7.1199    1.6617    6.3649
   
   
      7.4550    0.0000    9.3140    0.0000    5.5977    0.0000
      7.4945    0.0000    0.0000    6.4703    8.2059    0.0000
      8.8298    5.9857    7.0165    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.9201    0.0000    5.7546
      0.0000    0.0000    0.0000    7.1199    0.0000    6.3649
   
   
      7.4550    0.0000       NaN    0.0000    5.5977    0.0000
      7.4945    0.0000    0.0000    6.4703    8.2059    0.0000
      8.8298    5.9857    7.0165    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.9201    0.0000    5.7546
      0.0000    0.0000    0.0000    7.1199    0.0000    6.3649
   

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

   
      2.7744    8.6131    1.7027    8.6966    6.5000    2.7460
      6.5000    0.6843    2.1840    8.2185    6.5000    4.3804
      6.5000    1.8764    8.0584    0.0610    3.8981    6.5000
      3.2725    6.5000    1.2483    9.0747    4.1219    6.5000
      8.6815    1.5239    1.7891    8.8494    0.2054    0.9591
   
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
   
