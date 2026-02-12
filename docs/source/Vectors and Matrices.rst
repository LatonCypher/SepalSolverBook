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
      0.6116    0.1020    0.4357    0.1166    0.9058    0.1129    0.9289
   
   C = 
      0.2220
      0.1485
      0.6385
      0.4475
      0.0171
   
   M = 
      0.0031    0.1886    0.6779    0.2620    0.0092    0.7826    0.4666
      0.5523    0.5812    0.1120    0.8984    0.3870    0.5685    0.4012
      0.6514    0.2792    0.8720    0.2746    0.0839    0.7724    0.8144
      0.7376    0.7447    0.0106    0.9252    0.6978    0.8049    0.8684
      0.1137    0.8196    0.9757    0.2507    0.0503    0.1568    0.6094
      0.0299    0.9994    0.8439    0.3325    0.5825    0.1827    0.4960
      0.2665    0.3905    0.9059    0.4777    0.5008    0.8749    0.7079
      0.8919    0.9106    0.4580    0.1819    0.9851    0.5674    0.1455
   

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
      0.5666    0.5953    0.8621    0.4178
   
   R2 = 
      0.9474    0.0617    0.8370    0.6959    0.3075
   
   R3 = 
      0.5666    0.5953    0.8621    0.4178    0.9474    0.0617    0.8370    0.6959    0.3075
   
   C1 = 
      0.8430
      0.6819
      0.7180
      0.9072
      0.3525
      0.6873
      0.0282
      0.5028
      0.0189
      0.5734
   
   C2 = 
      0.9968
      0.2188
      0.4686
      0.5657
      0.5059
      0.5116
      0.7704
      0.1499
      0.4557
      0.1587
   
   M = 
      0.8430    0.9968
      0.6819    0.2188
      0.7180    0.4686
      0.9072    0.5657
      0.3525    0.5059
      0.6873    0.5116
      0.0282    0.7704
      0.5028    0.1499
      0.0189    0.4557
      0.5734    0.1587
   


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
      0.0167    0.5830    0.6067    0.6763
   
   R2 = 
      0.6247    0.8681    0.1810    0.2137
   
   M = 
      0.0167    0.5830    0.6067    0.6763
      0.6247    0.8681    0.1810    0.2137
   
   C1 = 
      0.9030
      0.4411
      0.4691
      0.0503
      0.3564
      0.0285
      0.6310
      0.4841
      0.9107
      0.5325
   
   C2 = 
      0.4553
      0.3337
   
   C3 = 
      0.9030
      0.4411
      0.4691
      0.0503
      0.3564
      0.0285
      0.6310
      0.4841
      0.9107
      0.5325
      0.4553
      0.3337
   

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
   

