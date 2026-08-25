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
      0.1372    0.7773    0.5621    0.5225
   
   R1[2] = 0.5621054802752614
   C1 = 
      0.0778
      0.1887
      0.2647
      0.3288
      0.0682
      0.4298
      0.7338
      0.3385
   
   C1[5] = 0.4297637232217181

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
      0.8024    0.0045    0.5910    0.9905    0.0940
      0.4589    0.6015    0.4861    0.9442    0.6116
   

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
   
      0.8235    0.0393    0.2376    0.6839    0.9683    0.0175    0.7497    0.6064
      0.3647    0.5317    0.5336    0.3101    0.7181    0.0961    0.7586    0.0306
      0.9770    0.3266    0.1556    0.4305    0.6726    0.0001    0.9825    0.0914
      0.6241    0.8984    0.9936    0.2581    0.3797    0.2476    0.7919    0.4295
      0.6107    0.8467    0.3316    0.0962    0.7693    0.6087    0.5015    0.0764
      0.4883    0.9078    0.7084    0.6953    0.5678    0.2423    0.7204    0.6825
      0.2316    0.3956    0.8140    0.3553    0.3469    0.7572    0.8756    0.5145
      0.9092    0.0856    0.2440    0.7163    0.5625    0.8465    0.0255    0.3900
   
   B = 
   
      0.1609    0.7264    0.9006    0.8823    0.2411    0.1251    0.2544    0.1596
      0.5233    0.2080    0.6197    0.9951    0.5927    0.3602    0.6595    0.0583
      0.5450    0.7308    0.5885    0.4034    0.2149    0.0645    0.8781    0.0411
      0.8752    0.4670    0.9258    0.7277    0.9647    0.8731    0.8414    0.0593
      0.5532    0.5952    0.8742    0.2396    0.1035    0.1377    0.8691    0.5462
      0.0947    0.1658    0.9146    0.5581    0.1617    0.2938    0.6459    0.2257
      0.6417    0.2624    0.6374    0.5808    0.9212    0.1154    0.3651    0.8570
      0.4201    0.9826    0.2732    0.2168    0.3157    0.1853    0.3207    0.7775
   
   C = 
   
      2.1543    2.4713    3.0451    2.1679    1.9179    1.0670    2.3406    1.8308
      1.8051    1.5827    2.4665    1.9646    1.6153    0.7626    2.1458    1.2173
      1.8306    1.8405    2.8117    2.3147    1.8815    0.8487    1.9354    1.4874
      2.2601    2.3837    3.1230    2.8155    2.0900    0.9871    2.7577    1.4841
      1.6433    1.6724    2.9285    2.4169    1.4773    0.8435    2.3552    1.2129
      2.6343    2.6238    3.4265    2.9635    2.4560    1.3998    3.0619    1.7141
      2.0404    2.0786    2.9561    2.3106    1.9354    1.0008    2.6087    1.6253
      1.5225    2.0561    3.0673    2.2136    1.3552    1.1871    2.2747    1.0260
   
   D = 
   
      2.1543    2.4713    3.0451    2.1679    1.9179    1.0670    2.3406    1.8308
      1.8051    1.5827    2.4665    1.9646    1.6153    0.7626    2.1458    1.2173
      1.8306    1.8405    2.8117    2.3147    1.8815    0.8487    1.9354    1.4874
      2.2601    2.3837    3.1230    2.8155    2.0900    0.9871    2.7577    1.4841
      1.6433    1.6724    2.9285    2.4169    1.4773    0.8435    2.3552    1.2129
      2.6343    2.6238    3.4265    2.9635    2.4560    1.3998    3.0619    1.7141
      2.0404    2.0786    2.9561    2.3106    1.9354    1.0008    2.6087    1.6253
      1.5225    2.0561    3.0673    2.2136    1.3552    1.1871    2.2747    1.0260
   


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

   
      0.8503    0.6530    0.8307    0.9700    0.7125    0.5994
      0.9421    0.9984    0.6022    0.9319    0.5573    0.5505
      0.4747    0.7492    0.8980    0.1113    0.3278    0.9454
      0.4697    0.7040    0.1066    0.1561    0.4022    0.2511
      0.9148    0.0769    0.8298    0.3491    0.3727    0.7673
   
   
      0.8503
      0.9421
      0.9148
      0.6530
      0.9984
      0.7492
      0.7040
      0.8307
      0.6022
      0.8980
      0.8298
      0.9700
      0.9319
      0.7125
      0.5573
      0.5994
      0.5505
      0.9454
      0.7673
   

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

   
      5.7930    8.2325    0.4037    7.2858    5.1760    1.0253
      3.0333    1.0130    3.2993    9.1469    3.5890    9.4730
      2.5630    3.1279    7.1507    9.2471    2.3567    2.3837
      8.6228    0.7494    8.4445    9.3441    0.3387    4.5297
      4.0197    4.1317    8.7553    7.4647    2.5315    6.0317
   
   
      5.7930    8.2325    0.0000    7.2858    5.1760    0.0000
      0.0000    0.0000    0.0000    9.1469    0.0000    9.4730
      0.0000    0.0000    7.1507    9.2471    0.0000    0.0000
      8.6228    0.0000    8.4445    9.3441    0.0000    0.0000
      0.0000    0.0000    8.7553    7.4647    0.0000    6.0317
   
   
      5.7930    8.2325    0.0000    7.2858    5.1760    0.0000
      0.0000    0.0000    0.0000       NaN    0.0000       NaN
      0.0000    0.0000    7.1507       NaN    0.0000    0.0000
      8.6228    0.0000    8.4445       NaN    0.0000    0.0000
      0.0000    0.0000    8.7553    7.4647    0.0000    6.0317
   

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

   
      1.0183    6.5000    3.1785    9.5806    8.4659    4.0804
      6.5000    1.8373    4.6813    0.8862    2.0813    0.0040
      6.5000    3.6919    4.9322    1.6013    8.4001    6.5000
      3.1072    4.6067    1.8040    0.0750    9.2235    6.5000
      3.8138    2.0675    2.8650    1.5863    2.6958    6.5000
   
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
   
