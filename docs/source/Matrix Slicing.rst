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
      0.3439    0.2877    0.8323    0.7583
   
   R1[2] = 0.8323129328313076
   C1 = 
      0.8670
      0.7694
      0.4098
      0.2201
      0.4170
      0.8796
      0.6087
      0.7505
   
   C1[5] = 0.8795780911421804

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
      0.3886    0.1382    0.7390    0.8210    0.1257
      0.0889    0.5791    0.3242    0.5836    0.0492
   

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
   
      0.2990    0.1413    0.4942    0.3161    0.1539    0.6262    0.6331    0.9929
      0.3645    0.1906    0.6809    0.7832    0.9788    0.4842    0.2724    0.6876
      0.1999    0.9176    0.6927    0.3160    0.9110    0.8887    0.1217    0.0881
      0.0695    0.4565    0.8141    0.6411    0.2544    0.2320    0.9990    0.5444
      0.2813    0.5881    0.3265    0.1473    0.7763    0.6902    0.3647    0.1347
      0.2679    0.3579    0.0437    0.4039    0.3027    0.4970    0.7427    0.5653
      0.3319    0.7105    0.5671    0.0286    0.4082    0.3713    0.7099    0.4126
      0.4538    0.0752    0.8149    0.4177    0.1766    0.2195    0.8986    0.0944
   
   B = 
   
      0.1212    0.3970    0.3706    0.9276    0.8920    0.5212    0.7150    0.4968
      0.9645    0.6475    0.3215    0.8768    0.3953    0.2279    0.0276    0.4292
      0.7550    0.5127    0.9032    0.1919    0.8179    0.5397    0.9589    0.0202
      0.3248    0.2913    0.5852    0.6193    0.3797    0.4356    0.1370    0.7320
      0.2083    0.9444    0.7498    0.8166    0.1131    0.9537    0.8133    0.1197
      0.4916    0.9882    0.9320    0.3829    0.6078    0.5661    0.8124    0.6978
      0.0269    0.5191    0.5851    0.5305    0.0515    0.5553    0.1505    0.5756
      0.3768    0.9842    0.5672    0.8945    0.2999    0.1931    0.6822    0.6938
   
   C = 
   
      1.3794    2.6256    2.4202    2.2813    1.5752    1.6369    2.1414    1.9592
      1.7049    3.0664    3.0043    2.8653    1.8801    2.4337    2.7256    1.9389
      2.1981    3.0091    2.8122    2.5463    1.9035    2.2813    2.4170    1.5988
      1.6706    2.4511    2.5832    2.3314    1.5362    1.8927    1.8478    1.8611
      1.4573    2.4398    2.1896    2.1426    1.3729    1.8806    1.8895    1.3845
      1.0823    2.1971    1.9356    2.1580    1.1137    1.5124    1.4460    1.7860
      1.6052    2.4179    2.1819    2.2786    1.4838    1.7268    1.8265    1.5050
      1.0829    1.7114    2.0891    1.6914    1.4876    1.6853    1.6867    1.3370
   
   D = 
   
      1.3794    2.6256    2.4202    2.2813    1.5752    1.6369    2.1414    1.9592
      1.7049    3.0664    3.0043    2.8653    1.8801    2.4337    2.7256    1.9389
      2.1981    3.0091    2.8122    2.5463    1.9035    2.2813    2.4170    1.5988
      1.6706    2.4511    2.5832    2.3314    1.5362    1.8927    1.8478    1.8611
      1.4573    2.4398    2.1896    2.1426    1.3729    1.8806    1.8895    1.3845
      1.0823    2.1971    1.9356    2.1580    1.1137    1.5124    1.4460    1.7860
      1.6052    2.4179    2.1819    2.2786    1.4838    1.7268    1.8265    1.5050
      1.0829    1.7114    2.0891    1.6914    1.4876    1.6853    1.6867    1.3370
   


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

   
      0.7804    0.1899    0.7342    0.1039    0.1220    0.4882
      0.5109    0.8449    0.5176    0.3732    0.1915    0.1784
      0.6494    0.0628    0.6398    0.2912    0.4059    0.8525
      0.6524    0.5322    0.0007    0.1713    0.0726    0.2873
      0.0165    0.7001    0.8022    0.0338    0.7437    0.2401
   
   
      0.7804
      0.5109
      0.6494
      0.6524
      0.8449
      0.5322
      0.7001
      0.7342
      0.5176
      0.6398
      0.8022
      0.7437
      0.8525
   

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

   
      4.7564    0.4215    1.4063    7.9948    3.4239    9.6525
      4.4145    2.7463    0.2471    0.5959    6.7400    8.4635
      0.1033    4.5312    3.7661    4.3735    9.0522    6.2458
      7.2845    2.3110    9.0198    8.3620    0.0981    6.8294
      6.8400    5.3535    4.1875    3.0731    7.6942    2.3455
   
   
      0.0000    0.0000    0.0000    7.9948    0.0000    9.6525
      0.0000    0.0000    0.0000    0.0000    6.7400    8.4635
      0.0000    0.0000    0.0000    0.0000    9.0522    6.2458
      7.2845    0.0000    9.0198    8.3620    0.0000    6.8294
      6.8400    5.3535    0.0000    0.0000    7.6942    0.0000
   
   
      0.0000    0.0000    0.0000    7.9948    0.0000       NaN
      0.0000    0.0000    0.0000    0.0000    6.7400    8.4635
      0.0000    0.0000    0.0000    0.0000       NaN    6.2458
      7.2845    0.0000       NaN    8.3620    0.0000    6.8294
      6.8400    5.3535    0.0000    0.0000    7.6942    0.0000
   

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

   
      6.5000    6.5000    6.5000    4.0669    4.9327    0.1417
      0.4490    0.3987    1.9858    8.7395    9.4780    9.6840
      6.5000    3.1596    9.2934    6.5000    4.9024    6.5000
      4.2601    3.1339    8.3614    1.0843    2.0903    8.2666
      6.5000    2.2198    8.2771    2.7578    6.5000    1.2420
   
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
   
