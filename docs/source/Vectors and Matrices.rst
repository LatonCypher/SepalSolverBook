Vectors and Matrices
====================

Vectors and Matrices are fundamental to Linear Algebra. SepalSolver provides three array types: ``RowVec``, ``ColVec`` and ``Matrix``. ``RowVec`` and ``ColVec`` are 1D arrays while ``Matrix`` is a 2D array. 

Creating Vectors and Matrices
-----------------------------


.. code-block:: csharp

   // Row vector
   RowVec R = new double[] { 5, 6, 7, 1 };
   Console.WriteLine($"R = {R}");

   // Column vector
   ColVec C = new double[] { 8, 3, 4, 2, 7 };
   Console.WriteLine($"C = {C}");

   // Matrix
   Matrix M = new double[,] 
   {
       {5, -2, 3, 7 },
       {2, 1, -7, 3 },
       {4, 8, 9, 1 },
       {0, 5, -6, -3 }
   };
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R = 
    5   6   7   1 
   
   C = 
    8 
    3 
    4 
    2 
    7 
   
   M = 
    5  -2   3   7 
    2   1  -7   3 
    4   8   9   1 
    0   5  -6  -3 
   


Vectors and Matrices can also be initialized using random
---------------------------------------------------------

.. code-block:: csharp

   // Row vector
   RowVec R = Rand(7);
   Console.WriteLine($"R = {R}");

   // Column vector
   ColVec C = Rand(5);
   Console.WriteLine($"C = {C}");

   // Matrix
   Matrix M = Rand(8, 7);
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R = 
      0.7678    0.4353    0.3857    0.1020    0.5446    0.7436    0.8042
   
   C = 
      0.6257
      0.5019
      0.3519
      0.8434
      0.9383
   
   M = 
      0.4124    0.6269    0.3672    0.2946    0.0318    0.3173    0.3403
      0.0190    0.3856    0.9192    0.8393    0.5060    0.8582    0.2476
      0.0450    0.4913    0.5286    0.5274    0.2753    0.2179    0.9380
      0.7607    0.6491    0.3474    0.6190    0.1705    0.6793    0.9461
      0.7757    0.0747    0.6875    0.8345    0.6637    0.3262    0.3971
      0.2659    0.8999    0.8392    0.5163    0.7158    0.9925    0.0810
      0.4388    0.6176    0.2665    0.6769    0.1780    0.2805    0.2102
      0.6289    0.2554    0.3002    0.6733    0.3332    0.8269    0.4368
   

Vectors can be initialized using Zeros, Ones, Eye etc
-----------------------------------------------------

.. code-block:: csharp

   // Row vector
   RowVec R = Zeros(7);
   Console.WriteLine($"R = {R}");

   // Column vector
   ColVec C = Ones(5);
   Console.WriteLine($"C = {C}");

   // Matrix
   Matrix M = Eye(7, 7);
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R = 
    0   0   0   0   0   0   0 
   
   C = 
    1 
    1 
    1 
    1 
    1 
   
   M = 
    1   0   0   0   0   0   0 
    0   1   0   0   0   0   0 
    0   0   1   0   0   0   0 
    0   0   0   1   0   0   0 
    0   0   0   0   1   0   0 
    0   0   0   0   0   1   0 
    0   0   0   0   0   0   1 
   

Vectors and Matrices can be concatenated
----------------------------------------

.. code-block:: csharp

   RowVec R1 = Rand(4);
   Console.WriteLine($"R1 = {R1}");
   RowVec R2 = Rand(5);
   Console.WriteLine($"R2 = {R2}");

   // Horizontal concatenation
   RowVec R3 = Hcart(R1, R2);
   Console.WriteLine($"R3 = {R3}");

   ColVec C1 = Rand(10);
   Console.WriteLine($"C1 = {C1}");
   ColVec C2 = Rand(10);
   Console.WriteLine($"C2 = {C2}");

   // Horizontal concatenation
   Matrix M = Hcart(C1, C2);
   Console.WriteLine($"M = {M}");


Ouput

.. terminal::

   R1 = 
      0.8752    0.3243    0.7658    0.9954
   
   R2 = 
      0.2901    0.9732    0.6310    0.3591    0.1829
   
   R3 = 
      0.8752    0.3243    0.7658    0.9954    0.2901    0.9732    0.6310    0.3591    0.1829
   
   C1 = 
      0.3952
      0.6728
      0.4486
      0.2308
      0.8953
      0.3137
      0.6215
      0.6070
      0.7234
      0.7295
   
   C2 = 
      0.1237
      0.9162
      0.3545
      0.7736
      0.4826
      0.4755
      0.5738
      0.4509
      0.2042
      0.8109
   
   M = 
      0.3952    0.1237
      0.6728    0.9162
      0.4486    0.3545
      0.2308    0.7736
      0.8953    0.4826
      0.3137    0.4755
      0.6215    0.5738
      0.6070    0.4509
      0.7234    0.2042
      0.7295    0.8109
   


Vertical Concatenation
----------------------

.. code-block:: csharp

   RowVec R1 = Rand(4);
   Console.WriteLine($"R1 = {R1}");
   RowVec R2 = Rand(4);
   Console.WriteLine($"R2 = {R2}");

   // Vertical concatenation
   Matrix M = Vcart(R1, R2);
   Console.WriteLine($"M = {M}");

   ColVec C1 = Rand(10);
   Console.WriteLine($"C1 = {C1}");
   ColVec C2 = Rand(2);
   Console.WriteLine($"C2 = {C2}");

   // Vertical concatenation
   ColVec C3 = Vcart(C1, C2);
   Console.WriteLine($"C3 = {C3}");


Ouput

.. terminal::

   R1 = 
      0.8185    0.2725    0.5754    0.5583
   
   R2 = 
      0.9793    0.8284    0.5184    0.3252
   
   M = 
      0.8185    0.2725    0.5754    0.5583
      0.9793    0.8284    0.5184    0.3252
   
   C1 = 
      0.1991
      0.3311
      0.9309
      0.9055
      0.1880
      0.2583
      0.7563
      0.0438
      0.7918
      0.0445
   
   C2 = 
      0.6141
      0.8908
   
   C3 = 
      0.1991
      0.3311
      0.9309
      0.9055
      0.1880
      0.2583
      0.7563
      0.0438
      0.7918
      0.0445
      0.6141
      0.8908
   

Flipping a Matrix
-----------------
We can flip a Matrix vertically (flipud) or horizontally (fliplr). 


.. code-block:: csharp


   Matrix M = new double[,]
   {
       {5, -2, 3, 7 },
       {2, 1, -7, 3 },
       {4, 8, 9, 1 },
       {0, 5, -6, -3 }
   };
   Console.WriteLine($"M = {M}");
   Console.WriteLine($"Flipud(M) = {Flipud(M)}");
   Console.WriteLine($"Fliplr(M) = {Fliplr(M)}");


Ouput

.. terminal::

   M = 
    5  -2   3   7 
    2   1  -7   3 
    4   8   9   1 
    0   5  -6  -3 
   
   Flipud(M) = 
    0   5  -6  -3 
    4   8   9   1 
    2   1  -7   3 
    5  -2   3   7 
   
   Fliplr(M) = 
    7   3  -2   5 
    3  -7   1   2 
    1   9   8   4 
   -3  -6   5   0 
   

Extract a Triangular Portion of Matrix
--------------------------------------

.. code-block:: csharp

   Matrix M = new double[,]
   {
       {5, -2, 3, 7 },
       {2, 1, -7, 3 },
       {4, 8, 9, 1 },
       {0, 5, -6, -3 }
   };

   Console.WriteLine($"Triu(M) = {Triu(M)}");
   Console.WriteLine($"Tril(M) = {Tril(M)}");



Ouput

.. terminal::

   Triu(M) = 
    5  -2   3   7 
    0   1  -7   3 
    0   0   9   1 
    0   0   0  -3 
   
   Tril(M) = 
    5   0   0   0 
    2   1   0   0 
    4   8   9   0 
    0   5  -6  -3 
   

