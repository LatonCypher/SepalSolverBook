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
      0.0994    0.6648    0.4617    0.8285    0.6750    0.6812    0.2345
   
   C = 
      0.6968
      0.7341
      0.4991
      0.6133
      0.5821
   
   M = 
      0.2581    0.6685    0.0132    0.9222    0.3347    0.3042    0.0384
      0.6625    0.0151    0.6186    0.4830    0.8683    0.9406    0.4441
      0.2756    0.9567    0.7851    0.3226    0.5873    0.7775    0.4977
      0.6871    0.3662    0.9623    0.5905    0.5602    0.4580    0.6489
      0.3514    0.9623    0.1862    0.7083    0.9614    0.6050    0.9374
      0.0044    0.9327    0.7484    0.2670    0.6555    0.0469    0.5377
      0.6049    0.6122    0.0302    0.4623    0.3755    0.0658    0.8352
      0.8720    0.2926    0.0044    0.0093    0.1529    0.7586    0.0933
   

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
      0.0418    0.4678    0.1900    0.4363
   
   R2 = 
      0.2004    0.1371    0.9290    0.9947    0.4655
   
   R3 = 
      0.0418    0.4678    0.1900    0.4363    0.2004    0.1371    0.9290    0.9947    0.4655
   
   C1 = 
      0.7893
      0.9089
      0.2946
      0.1138
      0.7520
      0.5477
      0.7923
      0.9619
      0.9364
      0.2413
   
   C2 = 
      0.5977
      0.6420
      0.2353
      0.2624
      0.9056
      0.3251
      0.1572
      0.5010
      0.5306
      0.0189
   
   M = 
      0.7893    0.5977
      0.9089    0.6420
      0.2946    0.2353
      0.1138    0.2624
      0.7520    0.9056
      0.5477    0.3251
      0.7923    0.1572
      0.9619    0.5010
      0.9364    0.5306
      0.2413    0.0189
   


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
      0.1140    0.0526    0.4691    0.0879
   
   R2 = 
      0.1803    0.3291    0.4337    0.3840
   
   M = 
      0.1140    0.0526    0.4691    0.0879
      0.1803    0.3291    0.4337    0.3840
   
   C1 = 
      0.2024
      0.9312
      0.8154
      0.0823
      0.0186
      0.6854
      0.2242
      0.4648
      0.5715
      0.0502
   
   C2 = 
      0.9171
      0.3944
   
   C3 = 
      0.2024
      0.9312
      0.8154
      0.0823
      0.0186
      0.6854
      0.2242
      0.4648
      0.5715
      0.0502
      0.9171
      0.3944
   

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
   

