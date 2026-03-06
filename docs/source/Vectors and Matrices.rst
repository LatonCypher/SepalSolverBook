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
      0.9044    0.2129    0.6301    0.0799    0.5761    0.3360    0.0261
   
   C = 
      0.8775
      0.2700
      0.1396
      0.9140
      0.9803
   
   M = 
      0.0908    0.4469    0.3526    0.4473    0.2346    0.9876    0.5941
      0.5199    0.5109    0.4402    0.4638    0.8644    0.6478    0.9048
      0.8075    0.5495    0.2422    0.1217    0.9251    0.1398    0.9689
      0.6893    0.6574    0.4721    0.5031    0.0275    0.3303    0.4001
      0.5180    0.4855    0.2763    0.7250    0.9753    0.8254    0.3107
      0.1516    0.5867    0.6197    0.5130    0.5547    0.9987    0.3717
      0.7267    0.2290    0.0515    0.4825    0.7547    0.0460    0.5255
      0.9086    0.4419    0.9602    0.2259    0.3005    0.3048    0.2969
   

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
      0.4692    0.6763    0.6535    0.7109
   
   R2 = 
      0.9617    0.3841    0.2997    0.3149    0.4530
   
   R3 = 
      0.4692    0.6763    0.6535    0.7109    0.9617    0.3841    0.2997    0.3149    0.4530
   
   C1 = 
      0.7242
      0.4295
      0.9035
      0.5520
      0.7195
      0.5608
      0.1240
      0.7077
      0.8321
      0.7573
   
   C2 = 
      0.3100
      0.0666
      0.3671
      0.8269
      0.5300
      0.6101
      0.4739
      0.5126
      0.6524
      0.8054
   
   M = 
      0.7242    0.3100
      0.4295    0.0666
      0.9035    0.3671
      0.5520    0.8269
      0.7195    0.5300
      0.5608    0.6101
      0.1240    0.4739
      0.7077    0.5126
      0.8321    0.6524
      0.7573    0.8054
   


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
      0.5930    0.9207    0.5253    0.2887
   
   R2 = 
      0.0814    0.2441    0.8799    0.5386
   
   M = 
      0.5930    0.9207    0.5253    0.2887
      0.0814    0.2441    0.8799    0.5386
   
   C1 = 
      0.3567
      0.5170
      0.2163
      0.6192
      0.9745
      0.8844
      0.9503
      0.2770
      0.7792
      0.5245
   
   C2 = 
      0.9542
      0.9990
   
   C3 = 
      0.3567
      0.5170
      0.2163
      0.6192
      0.9745
      0.8844
      0.9503
      0.2770
      0.7792
      0.5245
      0.9542
      0.9990
   

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
   

