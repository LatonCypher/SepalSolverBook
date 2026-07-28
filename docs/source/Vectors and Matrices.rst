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
      0.5654    0.9749    0.5581    0.6976    0.4020    0.0983    0.8854
   
   C = 
      0.6807
      0.4034
      0.0328
      0.0969
      0.2444
   
   M = 
      0.2409    0.8041    0.8586    0.6833    0.0986    0.5225    0.5038
      0.7861    0.4462    0.6359    0.1817    0.2646    0.1027    0.8544
      0.1467    0.4845    0.5233    0.4696    0.1437    0.6322    0.1645
      0.6581    0.6133    0.5118    0.9207    0.7766    0.3030    0.6606
      0.7786    0.7701    0.2878    0.1557    0.1928    0.3187    0.2153
      0.0326    0.6005    0.3006    0.7355    0.7884    0.1992    0.8547
      0.8827    0.6704    0.6282    0.6478    0.7438    0.5971    0.7268
      0.0153    0.8246    0.0544    0.4386    0.2564    0.9621    0.1403
   

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
      0.5714    0.2479    0.5638    0.8301
   
   R2 = 
      0.4385    0.5733    0.7149    0.9913    0.8193
   
   R3 = 
      0.5714    0.2479    0.5638    0.8301    0.4385    0.5733    0.7149    0.9913    0.8193
   
   C1 = 
      0.3436
      0.6680
      0.7232
      0.2561
      0.8011
      0.6636
      0.2106
      0.9222
      0.3979
      0.2492
   
   C2 = 
      0.7064
      0.3271
      0.3361
      0.6278
      0.4435
      0.3913
      0.6552
      0.8753
      0.1956
      0.9309
   
   M = 
      0.3436    0.7064
      0.6680    0.3271
      0.7232    0.3361
      0.2561    0.6278
      0.8011    0.4435
      0.6636    0.3913
      0.2106    0.6552
      0.9222    0.8753
      0.3979    0.1956
      0.2492    0.9309
   


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
      0.3159    0.3658    0.9685    0.3101
   
   R2 = 
      0.5005    0.1758    0.3452    0.7144
   
   M = 
      0.3159    0.3658    0.9685    0.3101
      0.5005    0.1758    0.3452    0.7144
   
   C1 = 
      0.1625
      0.7977
      0.0986
      0.6032
      0.3967
      0.2513
      0.6865
      0.1027
      0.1859
      0.9934
   
   C2 = 
      0.7745
      0.5777
   
   C3 = 
      0.1625
      0.7977
      0.0986
      0.6032
      0.3967
      0.2513
      0.6865
      0.1027
      0.1859
      0.9934
      0.7745
      0.5777
   

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
   

