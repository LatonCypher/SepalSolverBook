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
      0.8599    0.4218    0.6026    0.2904
   
   R1[2] = 0.6026111505245125
   C1 = 
      0.3389
      0.3679
      0.3072
      0.9356
      0.3635
      0.3178
      0.1128
      0.6736
   
   C1[5] = 0.3178347883537409

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
      0.2561    0.0684    0.8555    0.7187    0.5898
      0.5658    0.6849    0.8760    0.4824    0.7983
   

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
   
      0.5356    0.6146    0.5084    0.6987    0.1045    0.7168    0.7785    0.6205
      0.2862    0.5094    0.2282    0.1889    0.6616    0.6656    0.1946    0.1733
      0.8258    0.9073    0.8028    0.6991    0.4977    0.7305    0.5014    0.8788
      0.8456    0.2698    0.9280    0.8837    0.5688    0.3574    0.3444    0.8387
      0.7431    0.9001    0.0790    0.2307    0.3067    0.7881    0.5890    0.5733
      0.4878    0.0854    0.3635    0.3865    0.6067    0.8213    0.2295    0.3970
      0.5183    0.0639    0.1606    0.5361    0.7593    0.6398    0.2873    0.9015
      0.2646    0.6401    0.1639    0.2574    0.1780    0.7536    0.4174    0.7447
   
   B = 
   
      0.4237    0.0895    0.7990    0.5524    0.7548    0.8158    0.2619    0.8008
      0.1896    0.3749    0.2384    0.4794    0.4594    0.8376    0.4975    0.1814
      0.9521    0.5025    0.5281    0.7268    0.6988    0.7354    0.8417    0.7879
      0.3240    0.0185    0.7916    0.1710    0.3244    0.1020    0.9719    0.0863
      0.9415    0.7672    0.5934    0.3449    0.3988    0.3889    0.1075    0.6320
      0.7010    0.4281    0.4028    0.4195    0.7511    0.8508    0.7197    0.3323
      0.2884    0.8540    0.7884    0.7351    0.5222    0.4140    0.7961    0.4789
      0.6999    0.7037    0.1506    0.4238    0.7775    0.6577    0.3188    0.4717
   
   C = 
   
      2.3137    2.0353    2.4540    2.2514    2.7377    2.7778    2.8979    1.9712
      1.7632    1.4154    1.4603    1.3242    1.6709    1.8654    1.4644    1.3320
      3.2532    2.5715    2.9707    2.8131    3.5202    3.6960    3.2815    2.7308
      3.0516    2.1331    2.8088    2.3766    3.0245    2.9078    2.8556    2.5724
      2.0479    1.9270    2.0828    2.0511    2.5721    2.8525    2.1851    1.8488
      2.1852    1.5579    1.8396    1.5313    2.0742    2.0671    1.8175    1.6795
      2.4355    1.8971    2.0090    1.6487    2.3410    2.2006    1.8820    1.8548
      1.8104    1.6905    1.5047    1.6160    2.1258    2.2718    1.9072    1.3935
   
   D = 
   
      2.3137    2.0353    2.4540    2.2514    2.7377    2.7778    2.8979    1.9712
      1.7632    1.4154    1.4603    1.3242    1.6709    1.8654    1.4644    1.3320
      3.2532    2.5715    2.9707    2.8131    3.5202    3.6960    3.2815    2.7308
      3.0516    2.1331    2.8088    2.3766    3.0245    2.9078    2.8556    2.5724
      2.0479    1.9270    2.0828    2.0511    2.5721    2.8525    2.1851    1.8488
      2.1852    1.5579    1.8396    1.5313    2.0742    2.0671    1.8175    1.6795
      2.4355    1.8971    2.0090    1.6487    2.3410    2.2006    1.8820    1.8548
      1.8104    1.6905    1.5047    1.6160    2.1258    2.2718    1.9072    1.3935
   


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

   
      0.0397    0.6968    0.8176    0.3048    0.7223    0.4042
      0.0477    0.1150    0.8676    0.2929    0.9354    0.8007
      0.0415    0.5713    0.4234    0.7172    0.0465    0.1003
      0.0398    0.6521    0.0437    0.3787    0.7198    0.4715
      0.7460    0.4138    0.6747    0.9519    0.2537    0.4930
   
   
      0.7460
      0.6968
      0.5713
      0.6521
      0.8176
      0.8676
      0.6747
      0.7172
      0.9519
      0.7223
      0.9354
      0.7198
      0.8007
   

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

   
      9.4085    7.2815    4.4115    3.9867    0.1048    5.9130
      8.3050    7.8298    7.5599    7.6216    6.2274    9.1025
      0.2811    8.5013    9.6304    5.1351    4.6310    6.1738
      6.1385    7.2899    8.1135    8.6764    3.0431    1.6270
      0.2159    9.2671    7.7839    8.9477    4.4191    1.7294
   
   
      9.4085    7.2815    0.0000    0.0000    0.0000    5.9130
      8.3050    7.8298    7.5599    7.6216    6.2274    9.1025
      0.0000    8.5013    9.6304    5.1351    0.0000    6.1738
      6.1385    7.2899    8.1135    8.6764    0.0000    0.0000
      0.0000    9.2671    7.7839    8.9477    0.0000    0.0000
   
   
         NaN    7.2815    0.0000    0.0000    0.0000    5.9130
      8.3050    7.8298    7.5599    7.6216    6.2274       NaN
      0.0000    8.5013       NaN    5.1351    0.0000    6.1738
      6.1385    7.2899    8.1135    8.6764    0.0000    0.0000
      0.0000       NaN    7.7839    8.9477    0.0000    0.0000
   

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

   
      9.8694    2.9724    8.0695    0.9721    6.5000    9.1166
      9.9729    3.5844    2.2902    8.9750    3.6498    3.1377
      1.6286    6.5000    1.3503    1.2518    3.8836    6.5000
      6.5000    2.3857    8.4737    3.8467    0.4036    0.0958
      1.4965    8.7145    4.6732    0.1217    1.1475    4.3407
   
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
   
