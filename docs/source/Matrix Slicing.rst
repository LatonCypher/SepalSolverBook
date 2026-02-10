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
      0.2304    0.2980    0.9156    0.5047
   
   R1[2] = 0.9155532668570637
   C1 = 
      0.4939
      0.6538
      0.9496
      0.8175
      0.6046
      0.9414
      0.1455
      0.6666
   
   C1[5] = 0.9413529754748456

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
      0.9166    0.5697    0.0485    0.7116    0.0590
      0.1192    0.3979    0.2057    0.0555    0.6743
   

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
   
      0.4754    0.2496    0.2128    0.8730    0.2111    0.3607    0.7924    0.4876
      0.6516    0.2986    0.3752    0.1490    0.8715    0.3295    0.0969    0.4320
      0.5598    0.6260    0.8232    0.6511    0.4901    0.5795    0.3898    0.7793
      0.1519    0.9563    0.8685    0.7335    0.5074    0.8208    0.5306    0.2394
      0.7351    0.5234    0.1132    0.6939    0.2173    0.5021    0.6015    0.4050
      0.3003    0.3097    0.3319    0.7329    0.2138    0.9140    0.2461    0.7182
      0.6465    0.9436    0.8784    0.7417    0.8418    0.3800    0.7030    0.8265
      0.2506    0.1460    0.6094    0.9370    0.0334    0.3917    0.0859    0.3112
   
   B = 
   
      0.3713    0.0504    0.3101    0.7078    0.7316    0.6285    0.1450    0.7024
      0.8962    0.0120    0.4828    0.4690    0.8183    0.1275    0.7746    0.0326
      0.4401    0.9760    0.1701    0.6734    0.9025    0.8441    0.1333    0.7937
      0.1186    0.6725    0.9053    0.1855    0.5536    0.7228    0.8499    0.2142
      0.8977    0.9026    0.2379    0.2077    0.4517    0.0565    0.6131    0.8756
      0.5805    0.8331    0.8793    0.8297    0.4533    0.8996    0.3163    0.6345
      0.1385    0.8802    0.0697    0.2748    0.5922    0.8709    0.5902    0.4301
      0.2258    0.9689    0.6826    0.3563    0.3030    0.1049    0.0097    0.5449
   
   C = 
   
      1.2161    2.4827    1.8499    1.4933    2.1032    2.2189    1.7486    1.7182
      1.7768    2.0678    1.3437    1.5164    1.8734    1.3473    1.2024    2.0463
      2.2148    3.3005    2.3906    2.3323    2.9762    2.5674    1.9506    2.5957
      2.4422    3.2009    2.3635    2.2945    3.0716    2.7351    2.3882    2.3082
      1.5354    2.1567    1.9395    1.7420    2.2570    2.1561    1.7678    1.7601
      1.5408    2.7025    2.3245    1.8435    2.0526    2.1622    1.5230    1.9058
      2.8206    3.8960    2.6245    2.6070    3.6677    2.8925    2.6313    3.0719
      0.9428    1.9728    1.6709    1.2964    1.7093    1.8295    1.2251    1.3495
   
   D = 
   
      1.2161    2.4827    1.8499    1.4933    2.1032    2.2189    1.7486    1.7182
      1.7768    2.0678    1.3437    1.5164    1.8734    1.3473    1.2024    2.0463
      2.2148    3.3005    2.3906    2.3323    2.9762    2.5674    1.9506    2.5957
      2.4422    3.2009    2.3635    2.2945    3.0716    2.7351    2.3882    2.3082
      1.5354    2.1567    1.9395    1.7420    2.2570    2.1561    1.7678    1.7601
      1.5408    2.7025    2.3245    1.8435    2.0526    2.1622    1.5230    1.9058
      2.8206    3.8960    2.6245    2.6070    3.6677    2.8925    2.6313    3.0719
      0.9428    1.9728    1.6709    1.2964    1.7093    1.8295    1.2251    1.3495
   


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

   
      0.5056    0.2442    0.4687    0.3583    0.2907    0.9776
      0.0396    0.8246    0.4377    0.3411    0.3110    0.1989
      0.4637    0.5517    0.3468    0.1386    0.8404    0.6906
      0.8254    0.0328    0.9726    0.2565    0.5324    0.0591
      0.4157    0.1933    0.3210    0.9532    0.1537    0.4226
   
   
      0.5056
      0.8254
      0.8246
      0.5517
      0.9726
      0.9532
      0.8404
      0.5324
      0.9776
      0.6906
   

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

   
      4.2195    5.5806    7.5227    2.0726    4.8381    4.4536
      9.3318    0.9919    1.6727    4.8438    3.0870    2.9140
      0.1719    8.9933    7.8714    6.2554    4.8746    5.5985
      2.0203    9.6187    4.4325    2.5426    1.4889    6.1243
      5.5454    4.2624    9.8328    3.0392    2.3149    2.3200
   
   
      0.0000    5.5806    7.5227    0.0000    0.0000    0.0000
      9.3318    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    8.9933    7.8714    6.2554    0.0000    5.5985
      0.0000    9.6187    0.0000    0.0000    0.0000    6.1243
      5.5454    0.0000    9.8328    0.0000    0.0000    0.0000
   
   
      0.0000    5.5806    7.5227    0.0000    0.0000    0.0000
         NaN    0.0000    0.0000    0.0000    0.0000    0.0000
      0.0000    8.9933    7.8714    6.2554    0.0000    5.5985
      0.0000       NaN    0.0000    0.0000    0.0000    6.1243
      5.5454    0.0000       NaN    0.0000    0.0000    0.0000
   

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

   
      3.3512    6.5000    6.5000    6.5000    2.2913    6.5000
      4.2312    0.5715    6.5000    0.2594    6.5000    2.9048
      0.2782    1.2741    0.0855    6.5000    6.5000    6.5000
      9.3296    4.9721    4.8719    3.0985    6.5000    1.6029
      8.3187    6.5000    8.7145    6.5000    8.6542    2.2932
   
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
   
