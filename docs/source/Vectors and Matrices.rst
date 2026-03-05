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
      0.3874    0.7896    0.1952    0.1474    0.0034    0.0547    0.5212
   
   C = 
      0.4043
      0.1611
      0.6917
      0.0457
      0.7321
   
   M = 
      0.9417    0.9542    0.3745    0.4458    0.6342    0.5587    0.4141
      0.0245    0.6146    0.4120    0.8659    0.9903    0.9137    0.1438
      0.8337    0.1298    0.7390    0.1286    0.2676    0.0266    0.9500
      0.0556    0.9771    0.9438    0.3103    0.9371    0.5370    0.7043
      0.7965    0.6914    0.5221    0.1805    0.9699    0.5855    0.3111
      0.0381    0.3526    0.3542    0.6435    0.3648    0.8624    0.7027
      0.7034    0.0242    0.7977    0.2875    0.2626    0.5111    0.4704
      0.5651    0.3376    0.1385    0.7235    0.6089    0.6619    0.1137
   

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
      0.9630    0.4217    0.8636    0.5233
   
   R2 = 
      0.2061    0.2135    0.4894    0.2921    0.0556
   
   R3 = 
      0.9630    0.4217    0.8636    0.5233    0.2061    0.2135    0.4894    0.2921    0.0556
   
   C1 = 
      0.1946
      0.4283
      0.4101
      0.7421
      0.9541
      0.1967
      0.7888
      0.5975
      0.0602
      0.3821
   
   C2 = 
      0.3669
      0.7790
      0.8157
      0.4169
      0.1496
      0.5181
      0.5076
      0.9380
      0.8901
      0.6840
   
   M = 
      0.1946    0.3669
      0.4283    0.7790
      0.4101    0.8157
      0.7421    0.4169
      0.9541    0.1496
      0.1967    0.5181
      0.7888    0.5076
      0.5975    0.9380
      0.0602    0.8901
      0.3821    0.6840
   


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
      0.4554    0.5378    0.4738    0.5346
   
   R2 = 
      0.8451    0.0560    0.0298    0.0299
   
   M = 
      0.4554    0.5378    0.4738    0.5346
      0.8451    0.0560    0.0298    0.0299
   
   C1 = 
      0.6788
      0.1264
      0.4902
      0.3355
      0.6708
      0.9814
      0.9713
      0.7211
      0.6951
      0.4559
   
   C2 = 
      0.2602
      0.9414
   
   C3 = 
      0.6788
      0.1264
      0.4902
      0.3355
      0.6708
      0.9814
      0.9713
      0.7211
      0.6951
      0.4559
      0.2602
      0.9414
   

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
   

