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
      0.0258    0.5844    0.1270    0.1935    0.6098    0.7055    0.3017
   
   C = 
      0.6570
      0.2297
      0.6893
      0.5534
      0.2698
   
   M = 
      0.7443    0.8137    0.8729    0.5831    0.5132    0.2413    0.7522
      0.2914    0.4567    0.3994    0.2244    0.0448    0.1400    0.3053
      0.3220    0.0461    0.1848    0.2593    0.7245    0.5553    0.5843
      0.1362    0.7920    0.4783    0.2051    0.9939    0.0524    0.1584
      0.6191    0.7946    0.1331    0.9211    0.9734    0.3244    0.8257
      0.2097    0.6570    0.1785    0.8440    0.2436    0.9563    0.9977
      0.6929    0.6735    0.7979    0.5548    0.5348    0.6715    0.7779
      0.8055    0.2105    0.0313    0.3794    0.5746    0.3016    0.1463
   

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
      0.1915    0.5010    0.0976    0.7538
   
   R2 = 
      0.7495    0.2293    0.6606    0.2598    0.8002
   
   R3 = 
      0.1915    0.5010    0.0976    0.7538    0.7495    0.2293    0.6606    0.2598    0.8002
   
   C1 = 
      0.7700
      0.3064
      0.4695
      0.1086
      0.4112
      0.6501
      0.0785
      0.6760
      0.0963
      0.7886
   
   C2 = 
      0.5475
      0.6529
      0.4998
      0.2339
      0.4597
      0.7764
      0.8411
      0.0156
      0.7897
      0.4093
   
   M = 
      0.7700    0.5475
      0.3064    0.6529
      0.4695    0.4998
      0.1086    0.2339
      0.4112    0.4597
      0.6501    0.7764
      0.0785    0.8411
      0.6760    0.0156
      0.0963    0.7897
      0.7886    0.4093
   


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
      0.1563    0.6014    0.4305    0.0043
   
   R2 = 
      0.1905    0.4341    0.7721    0.9228
   
   M = 
      0.1563    0.6014    0.4305    0.0043
      0.1905    0.4341    0.7721    0.9228
   
   C1 = 
      0.0120
      0.9120
      0.4253
      0.8708
      0.4809
      0.8378
      0.8973
      0.4336
      0.4719
      0.0983
   
   C2 = 
      0.9427
      0.8176
   
   C3 = 
      0.0120
      0.9120
      0.4253
      0.8708
      0.4809
      0.8378
      0.8973
      0.4336
      0.4719
      0.0983
      0.9427
      0.8176
   

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
   

