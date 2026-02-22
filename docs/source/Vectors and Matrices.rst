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
      0.6168    0.6916    0.9191    0.7824    0.4066    0.6837    0.4758
   
   C = 
      0.9188
      0.8468
      0.5868
      0.0686
      0.9998
   
   M = 
      0.4247    0.5285    0.6861    0.8215    0.2176    0.6005    0.2963
      0.0088    0.7492    0.0226    0.9975    0.8321    0.5121    0.7395
      0.1304    0.0613    0.0006    0.0485    0.5288    0.5851    0.3255
      0.2433    0.6363    0.3320    0.8471    0.8635    0.5034    0.9848
      0.4679    0.3907    0.7320    0.5525    0.7409    0.2691    0.9237
      0.7569    0.7163    0.6701    0.2310    0.6607    0.9290    0.4325
      0.3541    0.8324    0.4792    0.8545    0.8060    0.6512    0.3555
      0.4142    0.8606    0.6914    0.2244    0.7941    0.8379    0.3708
   

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
      0.1709    0.4220    0.6771    0.5295
   
   R2 = 
      0.4292    0.3939    0.4757    0.9753    0.9568
   
   R3 = 
      0.1709    0.4220    0.6771    0.5295    0.4292    0.3939    0.4757    0.9753    0.9568
   
   C1 = 
      0.6534
      0.7671
      0.2988
      0.2040
      0.8060
      0.6315
      0.1720
      0.2389
      0.5454
      0.9930
   
   C2 = 
      0.6980
      0.3448
      0.2207
      0.2450
      0.3925
      0.4213
      0.9585
      0.9485
      0.0537
      0.6219
   
   M = 
      0.6534    0.6980
      0.7671    0.3448
      0.2988    0.2207
      0.2040    0.2450
      0.8060    0.3925
      0.6315    0.4213
      0.1720    0.9585
      0.2389    0.9485
      0.5454    0.0537
      0.9930    0.6219
   


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
      0.7857    0.1195    0.8076    0.3280
   
   R2 = 
      0.7247    0.5642    0.7882    0.6824
   
   M = 
      0.7857    0.1195    0.8076    0.3280
      0.7247    0.5642    0.7882    0.6824
   
   C1 = 
      0.4516
      0.4937
      0.5018
      0.6521
      0.2518
      0.8361
      0.5774
      0.1645
      0.5536
      0.7822
   
   C2 = 
      0.1137
      0.6897
   
   C3 = 
      0.4516
      0.4937
      0.5018
      0.6521
      0.2518
      0.8361
      0.5774
      0.1645
      0.5536
      0.7822
      0.1137
      0.6897
   

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
   

