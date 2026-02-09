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
      0.8496    0.0482    0.6838    0.6214    0.5576    0.1087    0.8931
   
   C = 
      0.5243
      0.5902
      0.0585
      0.2939
      0.8724
   
   M = 
      0.1687    0.3015    0.0692    0.9030    0.9482    0.3020    0.2164
      0.7226    0.8965    0.3075    0.3052    0.5590    0.7716    0.9089
      0.4598    0.6153    0.2050    0.6084    0.8124    0.6240    0.8398
      0.9651    0.3531    0.0309    0.6052    0.1424    0.5236    0.9797
      0.9892    0.7558    0.1508    0.3342    0.4097    0.6611    0.8351
      0.9421    0.2298    0.3112    0.2964    0.3695    0.0398    0.6163
      0.0058    0.1424    0.6844    0.7377    0.7881    0.5486    0.5928
      0.5412    0.1681    0.8355    0.9690    0.7224    0.4159    0.7446
   

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
      0.8929    0.3745    0.2831    0.4702
   
   R2 = 
      0.3673    0.1736    0.2447    0.7204    0.4019
   
   R3 = 
      0.8929    0.3745    0.2831    0.4702    0.3673    0.1736    0.2447    0.7204    0.4019
   
   C1 = 
      0.0979
      0.6422
      0.3481
      0.4518
      0.8222
      0.4954
      0.4023
      0.9475
      0.0282
      0.8990
   
   C2 = 
      0.4402
      0.2324
      0.4945
      0.8579
      0.7920
      0.2989
      0.6613
      0.1768
      0.6652
      0.3059
   
   M = 
      0.0979    0.4402
      0.6422    0.2324
      0.3481    0.4945
      0.4518    0.8579
      0.8222    0.7920
      0.4954    0.2989
      0.4023    0.6613
      0.9475    0.1768
      0.0282    0.6652
      0.8990    0.3059
   


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
      0.3229    0.6016    0.9381    0.0771
   
   R2 = 
      0.4675    0.5941    0.5893    0.0763
   
   M = 
      0.3229    0.6016    0.9381    0.0771
      0.4675    0.5941    0.5893    0.0763
   
   C1 = 
      0.1014
      0.0588
      0.2972
      0.9243
      0.3111
      0.8955
      0.7879
      0.9111
      0.3742
      0.7095
   
   C2 = 
      0.9963
      0.9506
   
   C3 = 
      0.1014
      0.0588
      0.2972
      0.9243
      0.3111
      0.8955
      0.7879
      0.9111
      0.3742
      0.7095
      0.9963
      0.9506
   

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
   

