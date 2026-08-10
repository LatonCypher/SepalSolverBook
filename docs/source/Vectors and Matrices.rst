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
      0.4822    0.9016    0.8616    0.7440    0.7250    0.5283    0.2632
   
   C = 
      0.3922
      0.4815
      0.0967
      0.4871
      0.5931
   
   M = 
      0.1366    0.8323    0.7765    0.8594    0.9660    0.4962    0.4706
      0.7871    0.9285    0.1652    0.6654    0.0274    0.0684    0.5595
      0.4719    0.3661    0.4743    0.5389    0.0310    0.1709    0.5297
      0.3851    0.2524    0.8298    0.4277    0.3886    0.1380    0.3653
      0.8673    0.0711    0.3471    0.8090    0.3597    0.2615    0.2361
      0.2588    0.1353    0.6274    0.9572    0.6590    0.9339    0.4364
      0.5350    0.9599    0.5322    0.6111    0.7923    0.2052    0.7365
      0.8106    0.3191    0.5148    0.5817    0.1806    0.1695    0.0961
   

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
      0.8461    0.1388    0.0409    0.7184
   
   R2 = 
      0.4176    0.4644    0.2744    0.3278    0.6384
   
   R3 = 
      0.8461    0.1388    0.0409    0.7184    0.4176    0.4644    0.2744    0.3278    0.6384
   
   C1 = 
      0.3273
      0.8240
      0.0328
      0.2401
      0.7180
      0.1251
      0.0989
      0.1526
      0.9578
      0.0656
   
   C2 = 
      0.4700
      0.6475
      0.2394
      0.3987
      0.9856
      0.3335
      0.4910
      0.4684
      0.0154
      0.0609
   
   M = 
      0.3273    0.4700
      0.8240    0.6475
      0.0328    0.2394
      0.2401    0.3987
      0.7180    0.9856
      0.1251    0.3335
      0.0989    0.4910
      0.1526    0.4684
      0.9578    0.0154
      0.0656    0.0609
   


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
      0.9217    0.6016    0.8276    0.9785
   
   R2 = 
      0.5676    0.9407    0.2734    0.2475
   
   M = 
      0.9217    0.6016    0.8276    0.9785
      0.5676    0.9407    0.2734    0.2475
   
   C1 = 
      0.4521
      0.1132
      0.1063
      0.7257
      0.9815
      0.5629
      0.7154
      0.6101
      0.6654
      0.3347
   
   C2 = 
      0.3399
      0.6296
   
   C3 = 
      0.4521
      0.1132
      0.1063
      0.7257
      0.9815
      0.5629
      0.7154
      0.6101
      0.6654
      0.3347
      0.3399
      0.6296
   

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
   

