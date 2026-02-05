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
      0.2519    0.8664    0.9361    0.4503    0.2960    0.8749    0.6262
   
   C = 
      0.7542
      0.8377
      0.8445
      0.1895
      0.9732
   
   M = 
      0.2000    0.4091    0.5598    0.2453    0.4816    0.3980    0.3007
      0.8881    0.6033    0.8045    0.7041    0.3174    0.5329    0.4009
      0.4636    0.1684    0.4847    0.8729    0.1388    0.7858    0.2152
      0.6486    0.1786    0.8735    0.0043    0.3404    0.9772    0.2045
      0.3717    0.9558    0.3251    0.2105    0.6955    0.1022    0.8206
      0.4998    0.6388    0.8576    0.4861    0.9642    0.7237    0.1554
      0.0853    0.6616    0.4872    0.0681    0.8665    0.7510    0.1684
      0.2783    0.8847    0.1314    0.7175    0.7514    0.7990    0.0568
   

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
      0.9083    0.5074    0.0703    0.4064
   
   R2 = 
      0.1899    0.7763    0.7753    0.0401    0.0542
   
   R3 = 
      0.9083    0.5074    0.0703    0.4064    0.1899    0.7763    0.7753    0.0401    0.0542
   
   C1 = 
      0.8351
      0.2122
      0.5501
      0.7284
      0.3363
      0.5964
      0.9704
      0.9199
      0.1488
      0.3107
   
   C2 = 
      0.8778
      0.6955
      0.3059
      0.0501
      0.6082
      0.2200
      0.6992
      0.0468
      0.1180
      0.9633
   
   M = 
      0.8351    0.8778
      0.2122    0.6955
      0.5501    0.3059
      0.7284    0.0501
      0.3363    0.6082
      0.5964    0.2200
      0.9704    0.6992
      0.9199    0.0468
      0.1488    0.1180
      0.3107    0.9633
   


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
      0.2863    0.9892    0.9887    0.9898
   
   R2 = 
      0.8413    0.3340    0.5983    0.7487
   
   M = 
      0.2863    0.9892    0.9887    0.9898
      0.8413    0.3340    0.5983    0.7487
   
   C1 = 
      0.9907
      0.6280
      0.8740
      0.3210
      0.4149
      0.7577
      0.6179
      0.3759
      0.9874
      0.3440
   
   C2 = 
      0.3480
      0.9045
   
   C3 = 
      0.9907
      0.6280
      0.8740
      0.3210
      0.4149
      0.7577
      0.6179
      0.3759
      0.9874
      0.3440
      0.3480
      0.9045
   

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
   

