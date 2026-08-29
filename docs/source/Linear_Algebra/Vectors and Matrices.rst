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
      0.4807    0.2133    0.5495    0.4442    0.5400    0.1519    0.6165
   
   C = 
      0.4645
      0.7228
      0.3388
      0.3875
      0.5345
   
   M = 
      0.5215    0.8956    0.8746    0.8336    0.7008    0.5086    0.6218
      0.1354    0.5005    0.3939    0.0269    0.6995    0.7856    0.8128
      0.6488    0.4552    0.7159    0.1185    0.4179    0.0540    0.5659
      0.7616    0.4045    0.4371    0.8029    0.9324    0.8116    0.2614
      0.3296    0.0603    0.6851    0.0169    0.2414    0.4317    0.1197
      0.9909    0.5848    0.1614    0.6033    0.8021    0.7306    0.3694
      0.5505    0.4035    0.4349    0.2818    0.8179    0.9235    0.0354
      0.4480    0.4826    0.5479    0.8781    0.2547    0.1135    0.7606
   

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
      0.7491    0.1023    0.2834    0.0296
   
   R2 = 
      0.3088    0.9611    0.6394    0.6331    0.3636
   
   R3 = 
      0.7491    0.1023    0.2834    0.0296    0.3088    0.9611    0.6394    0.6331    0.3636
   
   C1 = 
      0.0300
      0.2254
      0.6842
      0.3372
      0.9561
      0.9224
      0.9728
      0.9388
      0.0088
      0.2538
   
   C2 = 
      0.7090
      0.1233
      0.7305
      0.2260
      0.3807
      0.9970
      0.8103
      0.9737
      0.8702
      0.1115
   
   M = 
      0.0300    0.7090
      0.2254    0.1233
      0.6842    0.7305
      0.3372    0.2260
      0.9561    0.3807
      0.9224    0.9970
      0.9728    0.8103
      0.9388    0.9737
      0.0088    0.8702
      0.2538    0.1115
   


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
      0.3661    0.6074    0.9406    0.6823
   
   R2 = 
      0.1396    0.9560    0.9327    0.8281
   
   M = 
      0.3661    0.6074    0.9406    0.6823
      0.1396    0.9560    0.9327    0.8281
   
   C1 = 
      0.8117
      0.4973
      0.8231
      0.4172
      0.4731
      0.7301
      0.7394
      0.1320
      0.1005
      0.6024
   
   C2 = 
      0.4090
      0.5394
   
   C3 = 
      0.8117
      0.4973
      0.8231
      0.4172
      0.4731
      0.7301
      0.7394
      0.1320
      0.1005
      0.6024
      0.4090
      0.5394
   

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
   

