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
      0.3305    0.0380    0.5067    0.8347
   
   R1[2] = 0.50666325677234
   C1 = 
      0.5809
      0.4925
      0.3472
      0.6171
      0.4132
      0.6139
      0.2962
      0.4750
   
   C1[5] = 0.6138749913893788

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
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000    6.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   
   A[1,2] = 6
   A[5] = 7
   A[2..5] = 
      4.0000
      1.0000
      5.0000
   
   A[1, 2..4] = 
      6.0000    2.0000
   
   A[0..3, 3] = 
      1.0000
      2.0000
      1.0000
   
   A[0..3, 1..3] = 
      1.0000    6.0000
      5.0000    6.0000
      7.0000    2.0000
   
   A[1, ..] = 
      3.0000    5.0000    6.0000    2.0000   15.0000
   
   A[1..3, ..] = 
      3.0000    5.0000    6.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   

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
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000    6.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000  125.0000    2.0000   15.0000
      4.0000    7.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000    1.0000    6.0000    1.0000   16.0000
      3.0000    5.0000  125.0000    2.0000   15.0000
      4.0000  110.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000   15.0000    6.0000    1.0000   16.0000
      3.0000   20.0000  125.0000    2.0000   15.0000
     10.0000  110.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000   15.0000    6.0000    1.0000   16.0000
      3.0000   20.0000  150.0000  200.0000   15.0000
     10.0000  110.0000    2.0000    1.0000   14.0000
   
   A = 
      8.0000   15.0000    6.0000  100.0000   16.0000
      3.0000   20.0000  150.0000  150.0000   15.0000
     10.0000  110.0000    2.0000  200.0000   14.0000
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      3.0000  100.0000  150.0000  150.0000   15.0000
     10.0000  100.0000  150.0000  200.0000   14.0000
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      1.0000    2.0000    3.0000    4.0000    5.0000
     10.0000  100.0000  150.0000  200.0000   14.0000
   
   A = 
      8.0000  100.0000  150.0000  100.0000   16.0000
      0.9854    0.5504    0.7328    0.2236    0.0423
      0.7868    0.7854    0.5215    0.4838    0.6727
   

Application of Matrix Slicing: Strassen Multiplication
------------------------------------------------------
Strassen’s Matrix Multiplication
Overview
--------

- **Inventor**: Volker Strassen, 1969
- **Purpose**: Improve efficiency of matrix multiplication beyond the classical cubic-time algorithm.
- **Key Idea**: Replace some multiplications with additions/subtractions by reorganizing computation.

Standard vs. Strassen Multiplication
-----------------------------------


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
     - O(n^(log2 7)) ≈  O(n^2.81)
   * - Best Use Case
     - Small matrices
     - Large matrices

Algorithm Steps
---------------

1. **Divide**: Split each n×n matrix into four (n/2)×(n/2) submatrices::

A = [A11, A12,
A21, A22]

B = [B11, B12,
B21, B22]

2. **Compute 7 products (instead of 8)**::
M1 = (A11 + A22)(B11 + B22)
M2 = (A21 + A22)B11
M3 = A11(B12 - B22)
M4 = A22(B21 - B11)
M5 = (A11 + A12)B22
M6 = (A21 - A11)(B11 + B12)
M7 = (A12 - A22)(B21 + B22)

3. **Combine results** to form the product matrix::
C11 = M1 + M4 - M5 + M7
C12 = M3 + M5
C21 = M2 + M4
C22 = M1 - M2 + M3 + M6

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

           {

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
                       C = new Matrix[,] { { C11, C12 }, { C21, C22 } };
                       return C;
                   }
               }

               Matrix A = Rand(8, 8), B = Rand(8, 8), C = Strass(A, B), D = A * B;
               Console.WriteLine($"A = \n{A}");
               Console.WriteLine($"B = \n{B}");
               Console.WriteLine($"C = \n{C}");
               Console.WriteLine($"D = \n{D}");
           }

Ouput

.. terminal::

   A = 
   
      0.4894    0.4253    0.8343    0.8373    0.3190    0.8723    0.6683    0.8622
      0.7713    0.7852    0.0395    0.8711    0.8852    0.4723    0.1579    0.7747
      0.8206    0.3253    0.7983    0.9679    0.6057    0.0142    0.2473    0.2503
      0.8846    0.7880    0.8575    0.3584    0.8362    0.9136    0.5480    0.6288
      0.5371    0.8921    0.7289    0.5714    0.0263    0.1315    0.3281    0.8912
      0.6350    0.2571    0.6290    0.4780    0.5498    0.3177    0.9620    0.4259
      0.0748    0.1261    0.5083    0.4655    0.2828    0.4935    0.8743    0.8282
      0.6305    0.9835    0.5748    0.3755    0.3755    0.8950    0.0220    0.4773
   
   B = 
   
      0.9397    0.3689    0.1243    0.4587    0.9301    0.9916    0.5967    0.4124
      0.6721    0.5402    0.7511    0.6404    0.6372    0.6248    0.0882    0.0194
      0.6472    0.6179    0.8106    0.3041    0.5882    0.1414    0.1945    0.2998
      0.9300    0.3555    0.2185    0.2563    0.0123    0.4720    0.6422    0.4354
      0.2783    0.4506    0.4621    0.2944    0.7305    0.7870    0.3311    0.0225
      0.7236    0.2483    0.8976    0.2214    0.7451    0.1861    0.8523    0.9635
      0.5632    0.9459    0.4511    0.6438    0.0925    0.1946    0.9787    0.0469
      0.9284    0.6780    0.8029    0.5372    0.6630    0.5867    0.1765    0.8399
   
   C = 
   
      3.9613    2.8005    3.1637    2.1455    2.7437    2.3136    2.6849    2.4279
      3.4844    2.2334    2.4341    1.9748    2.7784    2.9420    2.0835    1.8574
      2.9570    1.9958    1.8100    1.5506    2.0938    2.2610    1.7941    1.2545
      4.0354    2.9576    3.4337    2.4019    3.5929    2.9637    2.6972    2.2461
      3.2220    2.2927    2.4463    1.9125    2.2421    2.0947    1.5071    1.5972
      2.9411    2.4570    2.2014    1.8499    2.1399    2.0338    2.3004    1.3848
      2.6140    2.2137    2.2506    1.5891    1.6589    1.4150    1.9697    1.6067
      3.1823    1.9884    2.7351    1.7692    2.8155    2.2446    1.8088    1.8876
   
   D = 
   
      3.9613    2.8005    3.1637    2.1455    2.7437    2.3136    2.6849    2.4279
      3.4844    2.2334    2.4341    1.9748    2.7784    2.9420    2.0835    1.8574
      2.9570    1.9958    1.8100    1.5506    2.0938    2.2610    1.7941    1.2545
      4.0354    2.9576    3.4337    2.4019    3.5929    2.9637    2.6972    2.2461
      3.2220    2.2927    2.4463    1.9125    2.2421    2.0947    1.5071    1.5972
      2.9411    2.4570    2.2014    1.8499    2.1399    2.0338    2.3004    1.3848
      2.6140    2.2137    2.2506    1.5891    1.6589    1.4150    1.9697    1.6067
      3.1823    1.9884    2.7351    1.7692    2.8155    2.2446    1.8088    1.8876
   
