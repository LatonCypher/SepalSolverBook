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
      0.6938    0.5284    0.3883    0.5432
   
   R1[2] = 0.3882872445536224
   C1 = 
      0.1101
      0.2491
      0.5608
      0.2559
      0.6607
      0.3643
      0.4724
      0.1220
   
   C1[5] = 0.3642722765558484

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
      0.2912    0.1992    0.2761    0.6614    0.9981
      0.0603    0.2748    0.9844    0.3968    0.1582
   

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
   
      0.1511    0.6260    0.3004    0.6014    0.1652    0.6806    0.5099    0.0050
      0.7705    0.8877    0.7628    0.6581    0.9825    0.9406    0.3761    0.4635
      0.8919    0.7963    0.8587    0.3388    0.9169    0.4113    0.1300    0.9321
      0.0455    0.3152    0.3526    0.4802    0.4945    0.9039    0.7274    0.4958
      0.1359    0.6187    0.2147    0.7841    0.6749    0.4060    0.1842    0.8982
      0.5826    0.0294    0.3521    0.5469    0.5309    0.2352    0.9312    0.1230
      0.0639    0.6077    0.5022    0.2912    0.7192    0.2319    0.7467    0.5091
      0.4876    0.1668    0.6734    0.9977    0.7129    0.5045    0.6377    0.1709
   
   B = 
   
      0.0948    0.0462    0.3605    0.0641    0.6806    0.0306    0.9022    0.3409
      0.6289    0.4377    0.4460    0.5566    0.0056    0.4949    0.5345    0.8555
      0.4799    0.0816    0.6842    0.6727    0.6040    0.1185    0.2902    0.7189
      0.0508    0.3768    0.4447    0.8086    0.4929    0.2909    0.5664    0.5168
      0.4560    0.0565    0.6224    0.5995    0.5607    0.8824    0.1958    0.9415
      0.8757    0.7324    0.3857    0.9076    0.9288    0.0102    0.5235    0.8585
      0.2777    0.9801    0.9390    0.7991    0.1977    0.6056    0.6742    0.7727
      0.3476    0.8328    0.4608    0.2805    0.9811    0.7315    0.7018    0.6895
   
   C = 
   
      1.3974    1.5438    1.6530    2.1720    1.4146    0.9901    1.6346    2.2510
      2.5681    2.2334    3.0291    3.4619    3.2679    2.1880    3.0273    4.2532
      2.1530    1.8443    2.6957    2.6403    3.1333    2.1956    2.8080    3.7372
      1.7875    2.1656    2.1797    2.6410    2.2293    1.5876    1.9922    2.9323
      1.5714    1.8540    1.9839    2.3036    2.2851    1.9321    2.0588    2.8807
      1.0199    1.4920    2.0594    2.0431    1.6998    1.3579    1.8945    2.2656
      1.5595    1.7860    2.2400    2.2968    1.7595    1.9085    1.8161    2.8575
      1.5283    1.7036    2.4704    2.8266    2.3933    1.6128    2.2431    3.0235
   
   D = 
   
      1.3974    1.5438    1.6530    2.1720    1.4146    0.9901    1.6346    2.2510
      2.5681    2.2334    3.0291    3.4619    3.2679    2.1880    3.0273    4.2532
      2.1530    1.8443    2.6957    2.6403    3.1333    2.1956    2.8080    3.7372
      1.7875    2.1656    2.1797    2.6410    2.2293    1.5876    1.9922    2.9323
      1.5714    1.8540    1.9839    2.3036    2.2851    1.9321    2.0588    2.8807
      1.0199    1.4920    2.0594    2.0431    1.6998    1.3579    1.8945    2.2656
      1.5595    1.7860    2.2400    2.2968    1.7595    1.9085    1.8161    2.8575
      1.5283    1.7036    2.4704    2.8266    2.3933    1.6128    2.2431    3.0235
   


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

   
      0.2106    0.7513    0.4321    0.3027    0.4080    0.3780
      0.9174    0.0859    0.3674    0.2333    0.1943    0.4180
      0.3262    0.0985    0.0592    0.3587    0.1222    0.5972
      0.4554    0.5335    0.7500    0.0863    0.1649    0.9196
      0.1533    0.8573    0.3288    0.8334    0.9623    0.8852
   
   
      0.9174
      0.7513
      0.5335
      0.8573
      0.7500
      0.8334
      0.9623
      0.5972
      0.9196
      0.8852
   

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

   
      2.7387    3.3941    3.2321    6.0475    5.7227    1.0304
      7.1115    6.0255    1.6645    3.2055    1.5034    7.5014
      7.0617    3.9779    7.6129    5.9622    5.5928    1.9629
      4.4130    4.2818    4.8195    1.8253    3.7642    7.5135
      0.3527    8.8123    2.1082    3.1679    6.0507    9.9517
   
   
      0.0000    0.0000    0.0000    6.0475    5.7227    0.0000
      7.1115    6.0255    0.0000    0.0000    0.0000    7.5014
      7.0617    0.0000    7.6129    5.9622    5.5928    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    7.5135
      0.0000    8.8123    0.0000    0.0000    6.0507    9.9517
   
   
      0.0000    0.0000    0.0000    6.0475    5.7227    0.0000
      7.1115    6.0255    0.0000    0.0000    0.0000    7.5014
      7.0617    0.0000    7.6129    5.9622    5.5928    0.0000
      0.0000    0.0000    0.0000    0.0000    0.0000    7.5135
      0.0000    8.8123    0.0000    0.0000    6.0507       NaN
   

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

   
      6.5000    4.8489    2.5806    6.5000    9.2010    6.5000
      9.9758    4.0569    4.8493    6.5000    6.5000    4.3082
      0.5082    9.9262    1.6237    6.5000    6.5000    0.6618
      6.5000    6.5000    1.2750    8.3927    2.7850    1.1231
      6.5000    6.5000    3.7780    3.1329    6.5000    4.5302
   
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
   
