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
      0.5998    0.1921    0.4642    0.0241    0.7738    0.8568    0.9155
   
   C = 
      0.0659
      0.7188
      0.6858
      0.1079
      0.6280
   
   M = 
      0.3028    0.3549    0.4271    0.3772    0.9099    0.5240    0.0101
      0.4816    0.3528    0.4401    0.9345    0.7562    0.4677    0.2745
      0.7698    0.7741    0.7724    0.0769    0.0239    0.8510    0.7758
      0.3491    0.0579    0.8083    0.0707    0.7746    0.8893    0.1422
      0.9354    0.7404    0.8714    0.7218    0.1191    0.4020    0.1549
      0.3300    0.8574    0.8161    0.0256    0.1609    0.0527    0.0546
      0.5576    0.6027    0.7116    0.6237    0.6934    0.2301    0.9169
      0.3409    0.8783    0.4542    0.3796    0.1613    0.3738    0.1072
   

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
      0.2586    0.8047    0.7065    0.3564
   
   R2 = 
      0.2890    0.4274    0.4745    0.6218    0.7499
   
   R3 = 
      0.2586    0.8047    0.7065    0.3564    0.2890    0.4274    0.4745    0.6218    0.7499
   
   C1 = 
      0.7515
      0.8921
      0.1825
      0.0173
      0.1486
      0.9628
      0.2957
      0.0578
      0.3029
      0.3661
   
   C2 = 
      0.2215
      0.6508
      0.0160
      0.5850
      0.4053
      0.0224
      0.4341
      0.1826
      0.1053
      0.1039
   
   M = 
      0.7515    0.2215
      0.8921    0.6508
      0.1825    0.0160
      0.0173    0.5850
      0.1486    0.4053
      0.9628    0.0224
      0.2957    0.4341
      0.0578    0.1826
      0.3029    0.1053
      0.3661    0.1039
   


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
      0.3789    0.9859    0.6481    0.1865
   
   R2 = 
      0.1087    0.1356    0.5583    0.3900
   
   M = 
      0.3789    0.9859    0.6481    0.1865
      0.1087    0.1356    0.5583    0.3900
   
   C1 = 
      0.2321
      0.3355
      0.1261
      0.8403
      0.3146
      0.4125
      0.1228
      0.7230
      0.1325
      0.1024
   
   C2 = 
      0.3822
      0.7157
   
   C3 = 
      0.2321
      0.3355
      0.1261
      0.8403
      0.3146
      0.4125
      0.1228
      0.7230
      0.1325
      0.1024
      0.3822
      0.7157
   

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
   

