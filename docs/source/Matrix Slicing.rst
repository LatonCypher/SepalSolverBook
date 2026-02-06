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
      0.3015    0.0859    0.1845    0.5098
   
   R1[2] = 0.1844598937919345
   C1 = 
      0.2768
      0.6971
      0.3592
      0.4082
      0.9779
      0.2681
      0.3732
      0.1290
   
   C1[5] = 0.26805687186673344

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
      0.4168    0.7795    0.2008    0.1242    0.3824
      0.4219    0.7678    0.4384    0.5583    0.1622
   

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
   
      0.6573    0.7914    0.6289    0.9960    0.5709    0.5012    0.5059    0.7624
      0.2172    0.6798    0.6537    0.4134    0.1858    0.8499    0.9152    0.7821
      0.0332    0.6752    0.9295    0.0455    0.6913    0.1497    0.7090    0.6494
      0.0393    0.6532    0.0573    0.4877    0.3841    0.5511    0.4423    0.1613
      0.8364    0.5959    0.8436    0.3902    0.0099    0.2757    0.1949    0.6244
      0.0686    0.2715    0.0175    0.8332    0.1483    0.0113    0.4082    0.7977
      0.7550    0.9078    0.7972    0.2689    0.2925    0.4780    0.6365    0.1447
      0.6607    0.5993    0.7794    0.1427    0.4683    0.6039    0.9364    0.0330
   
   B = 
   
      0.2814    0.5135    0.5503    0.8051    0.2648    0.4912    0.6232    0.7817
      0.1857    0.5603    0.8444    0.4540    0.8814    0.1200    0.4165    0.4351
      0.6666    0.2672    0.0761    0.8252    0.7129    0.6088    0.9847    0.6673
      0.9697    0.2230    0.7914    0.2238    0.1327    0.0542    0.2155    0.9173
      0.2696    0.5157    0.2806    0.8338    0.7430    0.2838    0.9125    0.5651
      0.7015    0.3291    0.8917    0.4452    0.7923    0.1879    0.1614    0.2595
      0.2234    0.4717    0.4935    0.2967    0.7521    0.6851    0.3828    0.5761
      0.0880    0.8810    0.8050    0.6701    0.1075    0.7995    0.3547    0.1266
   
   C = 
   
      2.4025    2.5407    3.3366    2.9904    2.7358    2.0670    2.6391    3.0320
      1.9434    2.2556    2.9618    2.4444    2.7614    2.0734    2.0858    2.2327
      1.3054    1.9662    1.8954    2.3991    2.5079    1.8950    2.3839    1.9019
      1.2467    1.2405    1.9111    1.2897    1.7638    0.8036    1.1242    1.4359
      1.5813    1.8136    2.1839    2.3345    1.8394    1.7044    2.0339    2.1024
      1.0987    1.3534    1.8232    1.1638    0.8924    1.0838    0.9291    1.3707
      1.7422    1.9051    2.3944    2.4805    2.6943    1.7042    2.3307    2.4380
      1.7170    1.8261    2.2004    2.4383    2.8119    1.7929    2.3546    2.3930
   
   D = 
   
      2.4025    2.5407    3.3366    2.9904    2.7358    2.0670    2.6391    3.0320
      1.9434    2.2556    2.9618    2.4444    2.7614    2.0734    2.0858    2.2327
      1.3054    1.9662    1.8954    2.3991    2.5079    1.8950    2.3839    1.9019
      1.2467    1.2405    1.9111    1.2897    1.7638    0.8036    1.1242    1.4359
      1.5813    1.8136    2.1839    2.3345    1.8394    1.7044    2.0339    2.1024
      1.0987    1.3534    1.8232    1.1638    0.8924    1.0838    0.9291    1.3707
      1.7422    1.9051    2.3944    2.4805    2.6943    1.7042    2.3307    2.4380
      1.7170    1.8261    2.2004    2.4383    2.8119    1.7929    2.3546    2.3930
   


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

   
      0.4340    0.0452    0.0082    0.3359    0.1979    0.1949
      0.3815    0.2864    0.6701    0.5416    0.8062    0.0039
      0.9572    0.0097    0.3508    0.3763    0.9653    0.5991
      0.5271    0.1325    0.2098    0.9262    0.6196    0.0899
      0.1003    0.6262    0.8143    0.5647    0.9957    0.5786
   
   
      0.9572
      0.5271
      0.6262
      0.6701
      0.8143
      0.5416
      0.9262
      0.5647
      0.8062
      0.9653
      0.6196
      0.9957
      0.5991
      0.5786
   

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

   
      8.1542    4.9883    7.8066    1.0753    5.4295    5.2087
      9.7610    7.9338    1.9065    4.4720    7.8675    4.2063
      7.5057    2.3070    1.7090    3.8221    6.7364    4.4211
      9.3768    0.5577    5.4136    6.2558    2.4418    3.7481
      9.6404    1.3027    4.2278    1.3580    7.6352    2.3643
   
   
      8.1542    0.0000    7.8066    0.0000    5.4295    5.2087
      9.7610    7.9338    0.0000    0.0000    7.8675    0.0000
      7.5057    0.0000    0.0000    0.0000    6.7364    0.0000
      9.3768    0.0000    5.4136    6.2558    0.0000    0.0000
      9.6404    0.0000    0.0000    0.0000    7.6352    0.0000
   
   
      8.1542    0.0000    7.8066    0.0000    5.4295    5.2087
         NaN    7.9338    0.0000    0.0000    7.8675    0.0000
      7.5057    0.0000    0.0000    0.0000    6.7364    0.0000
         NaN    0.0000    5.4136    6.2558    0.0000    0.0000
         NaN    0.0000    0.0000    0.0000    7.6352    0.0000
   

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

   
      4.0282    3.1427    6.5000    6.5000    6.5000    3.3174
      1.2230    9.9781    8.5326    0.4884    9.9130    0.9265
      6.5000    2.7635    6.5000    8.0702    2.3928    3.7920
      0.8605    6.5000    4.9445    8.6603    3.4562    0.1329
      6.5000    8.2333    9.4244    8.8848    3.3057    0.8128
   
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
   
