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
      0.3678    0.0744    0.5320    0.6989
   
   R1[2] = 0.5320146725122806
   C1 = 
      0.5197
      0.7640
      0.5711
      0.4909
      0.0179
      0.1153
      0.1820
      0.6999
   
   C1[5] = 0.1152711805364649

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
      0.1835    0.6487    0.8741    0.1786    0.8721
      0.9149    0.1394    0.4682    0.1860    0.8596
   

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
   
      0.3596    0.8217    0.1754    0.7106    0.4665    0.1285    0.2513    0.0169
      0.2798    0.0850    0.9510    0.7540    0.9084    0.0469    0.3564    0.0144
      0.1277    0.0987    0.0740    0.9468    0.3920    0.2957    0.0355    0.5178
      0.4005    0.5890    0.4222    0.7160    0.5649    0.7114    0.5548    0.4344
      0.3553    0.9395    0.8103    0.4895    0.4648    0.5941    0.2393    0.7023
      0.6085    0.1357    0.1234    0.8542    0.8896    0.8252    0.7947    0.5041
      0.1283    0.3247    0.0095    0.6268    0.9629    0.5258    0.1267    0.1700
      0.0191    0.0556    0.4809    0.5836    0.8707    0.1825    0.8914    0.0369
   
   B = 
   
      0.8103    0.3899    0.6297    0.8148    0.1745    0.7533    0.4721    0.7572
      0.9149    0.3159    0.5177    0.2664    0.3446    0.1006    0.1311    0.7494
      0.1943    0.7522    0.4946    0.9666    0.3540    0.0422    0.7216    0.7273
      0.1114    0.4078    0.2862    0.8939    0.6021    0.3473    0.9416    0.9487
      0.1246    0.0627    0.1603    0.4767    0.7057    0.0554    0.5475    0.2857
      0.9879    0.5731    0.7549    0.1302    0.5045    0.2499    0.6189    0.0078
      0.7688    0.3045    0.0923    0.1492    0.1646    0.9825    0.9161    0.9873
      0.6507    0.7586    0.5342    0.8528    0.6793    0.7161    0.9157    0.9331
   
   C = 
   
      1.5456    1.0137    1.1460    1.6076    1.2828    0.9246    1.6537    2.0878
      1.0163    1.3620    1.1280    2.3485    1.6020    0.9439    2.4055    2.3077
      1.0188    1.1203    1.0050    1.7204    1.4360    0.9394    1.9223    1.7552
      2.5076    1.8933    1.8817    2.3465    1.9975    1.6927    2.9006    2.8509
      2.6453    2.2196    2.1713    2.6942    2.1116    1.4787    2.8210    3.0558
      2.6016    1.8744    1.8672    2.4950    2.2285    2.1713    3.3859    2.9783
      1.3202    0.9446    1.0868    1.4519    1.5962    0.7784    1.8245    1.5050
      1.2230    1.0833    0.8250    1.6202    1.4225    1.2390    2.3528    2.1242
   
   D = 
   
      1.5456    1.0137    1.1460    1.6076    1.2828    0.9246    1.6537    2.0878
      1.0163    1.3620    1.1280    2.3485    1.6020    0.9439    2.4055    2.3077
      1.0188    1.1203    1.0050    1.7204    1.4360    0.9394    1.9223    1.7552
      2.5076    1.8933    1.8817    2.3465    1.9975    1.6927    2.9006    2.8509
      2.6453    2.2196    2.1713    2.6942    2.1116    1.4787    2.8210    3.0558
      2.6016    1.8744    1.8672    2.4950    2.2285    2.1713    3.3859    2.9783
      1.3202    0.9446    1.0868    1.4519    1.5962    0.7784    1.8245    1.5050
      1.2230    1.0833    0.8250    1.6202    1.4225    1.2390    2.3528    2.1242
   


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

   
      0.2749    0.3204    0.8896    0.9548    0.8246    0.0788
      0.5603    0.8821    0.1400    0.7308    0.5298    0.5426
      0.8860    0.9621    0.3163    0.6195    0.0845    0.8322
      0.9104    0.0971    0.1030    0.7963    0.2128    0.7057
      0.4984    0.4835    0.6255    0.7557    0.8449    0.6951
   
   
      0.5603
      0.8860
      0.9104
      0.8821
      0.9621
      0.8896
      0.6255
      0.9548
      0.7308
      0.6195
      0.7963
      0.7557
      0.8246
      0.5298
      0.8449
      0.5426
      0.8322
      0.7057
      0.6951
   

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

   
      4.3727    6.4333    3.7280    6.9938    7.7118    2.0911
      2.1063    6.5547    3.7204    7.6820    1.2550    7.6477
      8.4995    5.8487    5.2659    2.8537    9.8775    4.6911
      8.6223    8.7007    8.4828    8.7996    5.5065    1.9907
      5.8202    4.5781    1.2722    4.5074    8.0221    7.6148
   
   
      0.0000    6.4333    0.0000    6.9938    7.7118    0.0000
      0.0000    6.5547    0.0000    7.6820    0.0000    7.6477
      8.4995    5.8487    5.2659    0.0000    9.8775    0.0000
      8.6223    8.7007    8.4828    8.7996    5.5065    0.0000
      5.8202    0.0000    0.0000    0.0000    8.0221    7.6148
   
   
      0.0000    6.4333    0.0000    6.9938    7.7118    0.0000
      0.0000    6.5547    0.0000    7.6820    0.0000    7.6477
      8.4995    5.8487    5.2659    0.0000       NaN    0.0000
      8.6223    8.7007    8.4828    8.7996    5.5065    0.0000
      5.8202    0.0000    0.0000    0.0000    8.0221    7.6148
   

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

   
      0.2915    9.8051    4.8343    6.5000    8.9505    6.5000
      1.7832    8.3726    3.7355    6.5000    6.5000    6.5000
      6.5000    1.9163    2.7355    6.5000    6.5000    3.3900
      1.7558    9.9529    6.5000    0.7272    3.9980    3.3474
      3.7996    0.1879    6.5000    4.3364    2.9571    8.8503
   
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
   
