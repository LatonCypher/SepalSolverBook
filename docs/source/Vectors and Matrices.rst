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
      0.1315    0.7758    0.0949    0.6614    0.9723    0.0625    0.0583
   
   C = 
      0.7974
      0.5146
      0.2845
      0.9237
      0.4361
   
   M = 
      0.0201    0.3550    0.2929    0.6903    0.1390    0.0276    0.5688
      0.4530    0.5052    0.4577    0.4679    0.9931    0.7990    0.8999
      0.8134    0.4708    0.4279    0.9911    0.7932    0.6522    0.4337
      0.2239    0.1433    0.1162    0.7927    0.5006    0.2492    0.6784
      0.0870    0.6153    0.7587    0.7124    0.3255    0.3641    0.4809
      0.2248    0.7977    0.6471    0.8839    0.0815    0.8674    0.3836
      0.0775    0.1855    0.1645    0.0694    0.9546    0.1208    0.1138
      0.9756    0.2513    0.1089    0.1881    0.2412    0.6917    0.2726
   

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
      0.3018    0.8902    0.1170    0.3883
   
   R2 = 
      0.9090    0.9748    0.2868    0.4030    0.9406
   
   R3 = 
      0.3018    0.8902    0.1170    0.3883    0.9090    0.9748    0.2868    0.4030    0.9406
   
   C1 = 
      0.2605
      0.6491
      0.7556
      0.7453
      0.5763
      0.4918
      0.6865
      0.2827
      0.3504
      0.5695
   
   C2 = 
      0.5608
      0.9562
      0.1279
      0.9086
      0.9611
      0.5567
      0.6654
      0.8162
      0.2291
      0.5375
   
   M = 
      0.2605    0.5608
      0.6491    0.9562
      0.7556    0.1279
      0.7453    0.9086
      0.5763    0.9611
      0.4918    0.5567
      0.6865    0.6654
      0.2827    0.8162
      0.3504    0.2291
      0.5695    0.5375
   


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
      0.9910    0.2837    0.1076    0.4851
   
   R2 = 
      0.5526    0.1056    0.8968    0.3091
   
   M = 
      0.9910    0.2837    0.1076    0.4851
      0.5526    0.1056    0.8968    0.3091
   
   C1 = 
      0.9380
      0.7121
      0.6497
      0.6164
      0.3332
      0.9111
      0.1087
      0.3221
      0.6770
      0.3424
   
   C2 = 
      0.1047
      0.6808
   
   C3 = 
      0.9380
      0.7121
      0.6497
      0.6164
      0.3332
      0.9111
      0.1087
      0.3221
      0.6770
      0.3424
      0.1047
      0.6808
   

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
   

