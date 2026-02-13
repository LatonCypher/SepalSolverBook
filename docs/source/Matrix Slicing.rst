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
      0.1349    0.5201    0.3190    0.7755
   
   R1[2] = 0.3189545197633794
   C1 = 
      0.0020
      0.5557
      0.3486
      0.7177
      0.5459
      0.6386
      0.6161
      0.4594
   
   C1[5] = 0.638628968150802

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
      0.0370    0.8561    0.1011    0.8108    0.5466
      0.7764    0.8824    0.1240    0.8726    0.3319
   

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
   
      0.3987    0.5066    0.5552    0.0184    0.4722    0.8609    0.1837    0.2629
      0.9695    0.9919    0.6647    0.5817    0.9991    0.6581    0.0699    0.5787
      0.5189    0.9448    0.6206    0.8847    0.8995    0.5425    0.6195    0.8491
      0.2705    0.0812    0.0072    0.7923    0.8519    0.7173    0.2312    0.3360
      0.0351    0.9682    0.8055    0.1100    0.8276    0.6318    0.8983    0.6920
      0.3801    0.1088    0.6286    0.5142    0.0553    0.4613    0.9640    0.3098
      0.5615    0.3532    0.4601    0.9849    0.4173    0.3582    0.0033    0.8234
      0.6926    0.9415    0.3717    0.0980    0.1919    0.9086    0.4967    0.0375
   
   B = 
   
      0.9343    0.8892    0.7839    0.5706    0.7299    0.5021    0.9361    0.0845
      0.3810    0.3655    0.9619    0.5714    0.5505    0.6593    0.9804    0.9009
      0.3110    0.6127    0.9004    0.1423    0.8183    0.4574    0.6401    0.9299
      0.7271    0.1635    0.8569    0.5981    0.0162    0.0426    0.0652    0.1575
      0.7582    0.9484    0.1358    0.5606    0.0241    0.0882    0.5840    0.2088
      0.7966    0.2616    0.2397    0.8179    0.0628    0.7770    0.4841    0.0006
      0.5172    0.0560    0.9927    0.6613    0.9618    0.7866    0.4720    0.5958
      0.9840    0.4352    0.7851    0.9681    0.1920    0.2834    0.1608    0.7630
   
   C = 
   
      2.1490    1.6805    1.9747    1.9517    1.3171    1.7185    2.0480    1.4183
      3.8007    3.1024    3.6281    3.2673    2.0507    2.2880    3.3715    2.3774
      3.9511    2.7308    4.1662    3.6330    2.2356    2.4338    3.0839    2.8165
      2.5296    1.5589    1.7565    2.2181    0.6133    1.1360    1.3970    0.7999
      3.0084    2.1982    3.4771    2.9983    2.2759    2.4957    2.8294    2.8779
      2.1787    1.2089    2.7275    2.0218    1.8770    1.7814    1.6589    1.6182
      2.9320    1.9192    2.8303    2.5030    1.1905    1.3183    1.7817    1.6661
      2.3558    1.6675    2.6336    2.2602    1.8762    2.2669    2.6081    1.6330
   
   D = 
   
      2.1490    1.6805    1.9747    1.9517    1.3171    1.7185    2.0480    1.4183
      3.8007    3.1024    3.6281    3.2673    2.0507    2.2880    3.3715    2.3774
      3.9511    2.7308    4.1662    3.6330    2.2356    2.4338    3.0839    2.8165
      2.5296    1.5589    1.7565    2.2181    0.6133    1.1360    1.3970    0.7999
      3.0084    2.1982    3.4771    2.9983    2.2759    2.4957    2.8294    2.8779
      2.1787    1.2089    2.7275    2.0218    1.8770    1.7814    1.6589    1.6182
      2.9320    1.9192    2.8303    2.5030    1.1905    1.3183    1.7817    1.6661
      2.3558    1.6675    2.6336    2.2602    1.8762    2.2669    2.6081    1.6330
   


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

   
      0.1357    0.2434    0.8836    0.4028    0.9748    0.7911
      0.2913    0.1850    0.8637    0.5958    0.7496    0.6529
      0.3404    0.4771    0.2227    0.8873    0.8492    0.7567
      0.5780    0.4251    0.3020    0.9294    0.3327    0.1895
      0.5420    0.0615    0.8155    0.3539    0.0958    0.9423
   
   
      0.5780
      0.5420
      0.8836
      0.8637
      0.8155
      0.5958
      0.8873
      0.9294
      0.9748
      0.7496
      0.8492
      0.7911
      0.6529
      0.7567
      0.9423
   

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

   
      5.6759    3.5621    5.9059    3.5142    7.8643    7.7774
      9.7079    4.0368    9.4221    1.2175    2.5489    4.6050
      5.0999    8.8269    2.3930    0.0397    3.4404    2.2502
      0.1427    1.5375    3.4383    0.8040    9.7308    0.8055
      4.2075    9.7560    4.5292    1.3592    2.9455    0.4702
   
   
      5.6759    0.0000    5.9059    0.0000    7.8643    7.7774
      9.7079    0.0000    9.4221    0.0000    0.0000    0.0000
      5.0999    8.8269    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000    9.7308    0.0000
      0.0000    9.7560    0.0000    0.0000    0.0000    0.0000
   
   
      5.6759    0.0000    5.9059    0.0000    7.8643    7.7774
         NaN    0.0000       NaN    0.0000    0.0000    0.0000
      5.0999    8.8269    0.0000    0.0000    0.0000    0.0000
      0.0000    0.0000    0.0000    0.0000       NaN    0.0000
      0.0000       NaN    0.0000    0.0000    0.0000    0.0000
   

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

   
      3.7899    8.3053    9.4048    9.8634    6.5000    4.4091
      9.4630    2.9411    2.3243    2.4581    8.4855    0.9897
      6.5000    0.2978    1.5089    6.5000    8.4275    6.5000
      3.6506    1.7421    6.5000    4.5751    2.2254    2.2718
      1.9233    9.7972    0.8082    2.6052    3.5071    6.5000
   
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
   
