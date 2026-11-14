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
      0.4909    0.3383    0.7047    0.9957
   
   R1[2] = 0.7046985180149669
   C1 = 
      0.8025
      0.9281
      0.8009
      0.4335
      0.6239
      0.3272
      0.2179
      0.7307
   
   C1[5] = 0.32723172462549177

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
      0.4101    0.6628    0.3725    0.8929    0.8587
      0.0835    0.9119    0.3174    0.7994    0.7968
   

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
   
      0.7867    0.1683    0.0098    0.8053    0.5829    0.7574    0.0121    0.5661
      0.6041    0.0853    0.9421    0.0156    0.5463    0.1738    0.4945    0.4006
      0.2088    0.5198    0.6475    0.1659    0.9322    0.0355    0.1761    0.3743
      0.8551    0.9862    0.6525    0.2779    0.2719    0.8278    0.6074    0.2327
      0.0593    0.6578    0.3841    0.7159    0.9573    0.5172    0.5223    0.7062
      0.4931    0.3009    0.3444    0.4849    0.0091    0.8861    0.5290    0.7608
      0.0036    0.7634    0.8650    0.5770    0.5016    0.5998    0.6334    0.7707
      0.1243    0.0621    0.8521    0.4613    0.8530    0.4923    0.6447    0.4948
   
   B = 
   
      0.2048    0.5130    0.7982    0.5523    0.8899    0.8720    0.3510    0.8837
      0.1973    0.4525    0.3270    0.5001    0.1631    0.7250    0.5882    0.2934
      0.1424    0.8922    0.4353    0.2143    0.2787    0.9787    0.6124    0.7675
      0.7634    0.1358    0.9886    0.4956    0.7105    0.2038    0.6546    0.7371
      0.3666    0.5917    0.1089    0.8841    0.9555    0.5316    0.7543    0.4449
      0.4532    0.5353    0.5245    0.5559    0.7585    0.2077    0.1839    0.7772
      0.8319    0.6632    0.2783    0.2651    0.0863    0.3968    0.5256    0.0171
      0.5416    0.1827    0.3330    0.7321    0.4710    0.2844    0.6866    0.0759
   
   C = 
   
      1.6841    1.4596    2.1360    2.2739    2.7015    1.6147    1.8823    2.2367
      1.1941    2.0086    1.3573    1.5898    1.7103    2.1505    1.8285    1.7104
      1.0712    1.6983    1.0763    1.7607    1.6780    1.9058    1.9434    1.4299
      1.7810    2.5542    2.2742    2.2749    2.3508    2.7796    2.2981    2.5430
      2.1456    2.0867    1.8935    2.5882    2.4606    2.0750    2.6883    1.9583
      1.8366    1.7318    1.9876    1.9348    2.0132    1.6993    1.8487    1.9053
      2.1151    2.3761    2.0016    2.3640    2.1305    2.3826    2.7084    2.0749
      1.8514    2.2010    1.6419    2.0718    2.1632    2.0337    2.3166    1.9328
   
   D = 
   
      1.6841    1.4596    2.1360    2.2739    2.7015    1.6147    1.8823    2.2367
      1.1941    2.0086    1.3573    1.5898    1.7103    2.1505    1.8285    1.7104
      1.0712    1.6983    1.0763    1.7607    1.6780    1.9058    1.9434    1.4299
      1.7810    2.5542    2.2742    2.2749    2.3508    2.7796    2.2981    2.5430
      2.1456    2.0867    1.8935    2.5882    2.4606    2.0750    2.6883    1.9583
      1.8366    1.7318    1.9876    1.9348    2.0132    1.6993    1.8487    1.9053
      2.1151    2.3761    2.0016    2.3640    2.1305    2.3826    2.7084    2.0749
      1.8514    2.2010    1.6419    2.0718    2.1632    2.0337    2.3166    1.9328
   


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

   
      0.7117    0.2567    0.8846    0.9495    0.2226    0.6674
      0.2945    0.4334    0.2650    0.3639    0.8236    0.5797
      0.1537    0.4434    0.6422    0.6646    0.8036    0.0478
      0.4784    0.5245    0.0858    0.8838    0.9071    0.4110
      0.6401    0.4773    0.5253    0.8831    0.1408    0.5980
   
   
      0.7117
      0.6401
      0.5245
      0.8846
      0.6422
      0.5253
      0.9495
      0.6646
      0.8838
      0.8831
      0.8236
      0.8036
      0.9071
      0.6674
      0.5797
      0.5980
   

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

   
      7.6296    6.0576    0.3086    9.2901    2.7103    3.9791
      0.6066    6.5380    1.6889    3.7347    4.5962    2.1463
      2.2130    3.4467    3.0405    8.6992    3.6202    6.8810
      0.2369    2.4177    3.3054    9.3452    2.7091    8.1380
      4.9193    5.6964    0.4276    4.5165    1.6482    0.9739
   
   
      7.6296    6.0576    0.0000    9.2901    0.0000    0.0000
      0.0000    6.5380    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.6992    0.0000    6.8810
      0.0000    0.0000    0.0000    9.3452    0.0000    8.1380
      0.0000    5.6964    0.0000    0.0000    0.0000    0.0000
   
   
      7.6296    6.0576    0.0000       NaN    0.0000    0.0000
      0.0000    6.5380    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    8.6992    0.0000    6.8810
      0.0000    0.0000    0.0000       NaN    0.0000    8.1380
      0.0000    5.6964    0.0000    0.0000    0.0000    0.0000
   

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

   
      6.5000    6.5000    8.7352    6.5000    6.5000    4.7417
      8.8452    1.8517    2.7438    6.5000    2.2614    9.9812
      4.9177    9.4284    6.5000    6.5000    3.8868    8.6400
      0.4681    8.9478    6.5000    0.5146    2.5058    4.0209
      2.5140    6.5000    3.9764    8.3025    6.5000    6.5000
   
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
   
