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
      0.1333    0.1923    0.0782    0.8762    0.0103    0.8388    0.7766
   
   C = 
      0.1648
      0.7260
      0.1011
      0.0499
      0.9088
   
   M = 
      0.0275    0.6285    0.5953    0.1539    0.4301    0.5508    0.8061
      0.2821    0.3557    0.7290    0.3986    0.9479    0.1418    0.8666
      0.2111    0.9127    0.1362    0.6889    0.4234    0.1051    0.6047
      0.2063    0.2864    0.8421    0.9203    0.5926    0.0384    0.6035
      0.1915    0.5752    0.3647    0.6283    0.6952    0.1057    0.3527
      0.0503    0.4601    0.5543    0.7556    0.3420    0.8149    0.8679
      0.1504    0.4547    0.7224    0.9516    0.8330    0.1650    0.3460
      0.3149    0.1788    0.1119    0.6600    0.6973    0.5382    0.0390
   

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
      0.1975    0.0930    0.7619    0.2395
   
   R2 = 
      0.7915    0.2426    0.6745    0.9700    0.3610
   
   R3 = 
      0.1975    0.0930    0.7619    0.2395    0.7915    0.2426    0.6745    0.9700    0.3610
   
   C1 = 
      0.3496
      0.8278
      0.1926
      0.6319
      0.2880
      0.1699
      0.3700
      0.4208
      0.7946
      0.3050
   
   C2 = 
      0.8224
      0.2295
      0.7437
      0.2175
      0.7010
      0.8234
      0.1675
      0.1560
      0.4182
      0.8411
   
   M = 
      0.3496    0.8224
      0.8278    0.2295
      0.1926    0.7437
      0.6319    0.2175
      0.2880    0.7010
      0.1699    0.8234
      0.3700    0.1675
      0.4208    0.1560
      0.7946    0.4182
      0.3050    0.8411
   


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
      0.6480    0.2397    0.2302    0.4329
   
   R2 = 
      0.5519    0.3737    0.2047    0.4317
   
   M = 
      0.6480    0.2397    0.2302    0.4329
      0.5519    0.3737    0.2047    0.4317
   
   C1 = 
      0.7532
      0.2105
      0.0324
      0.7267
      0.4901
      0.1049
      0.2860
      0.8421
      0.8785
      0.9206
   
   C2 = 
      0.1013
      0.6388
   
   C3 = 
      0.7532
      0.2105
      0.0324
      0.7267
      0.4901
      0.1049
      0.2860
      0.8421
      0.8785
      0.9206
      0.1013
      0.6388
   

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
   

