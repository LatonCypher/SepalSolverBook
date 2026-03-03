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
      0.7497    0.1652    0.6840    0.4642    0.3455    0.7192    0.8530
   
   C = 
      0.7990
      0.2151
      0.2088
      0.9688
      0.4763
   
   M = 
      0.3878    0.6350    0.5292    0.7651    0.3745    0.7477    0.0238
      0.2545    0.7318    0.6364    0.1177    0.6429    0.3050    0.3567
      0.6005    0.5055    0.2082    0.7014    0.6531    0.8419    0.7986
      0.4668    0.1720    0.4559    0.4939    0.9189    0.7425    0.7479
      0.8877    0.0327    0.4249    0.5339    0.5218    0.9804    0.3040
      0.5657    0.6764    0.8623    0.0579    0.7829    0.3060    0.5865
      0.9534    0.9232    0.2097    0.0323    0.1570    0.9966    0.9248
      0.2854    0.8761    0.0684    0.3097    0.1135    0.1813    0.7652
   

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
      0.5466    0.1764    0.3535    0.8496
   
   R2 = 
      0.4579    0.0341    0.6187    0.5558    0.7310
   
   R3 = 
      0.5466    0.1764    0.3535    0.8496    0.4579    0.0341    0.6187    0.5558    0.7310
   
   C1 = 
      0.3758
      0.9158
      0.4076
      0.1044
      0.1833
      0.4541
      0.2406
      0.1433
      0.7853
      0.8008
   
   C2 = 
      0.7162
      0.6635
      0.0875
      0.3930
      0.3998
      0.4040
      0.7837
      0.1716
      0.2817
      0.7296
   
   M = 
      0.3758    0.7162
      0.9158    0.6635
      0.4076    0.0875
      0.1044    0.3930
      0.1833    0.3998
      0.4541    0.4040
      0.2406    0.7837
      0.1433    0.1716
      0.7853    0.2817
      0.8008    0.7296
   


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
      0.2777    0.7967    0.9156    0.6897
   
   R2 = 
      0.4463    0.7908    0.2994    0.9892
   
   M = 
      0.2777    0.7967    0.9156    0.6897
      0.4463    0.7908    0.2994    0.9892
   
   C1 = 
      0.5837
      0.5353
      0.5736
      0.8404
      0.7029
      0.5759
      0.8282
      0.3423
      0.8432
      0.9118
   
   C2 = 
      0.8757
      0.8338
   
   C3 = 
      0.5837
      0.5353
      0.5736
      0.8404
      0.7029
      0.5759
      0.8282
      0.3423
      0.8432
      0.9118
      0.8757
      0.8338
   

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
   

