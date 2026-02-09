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
      0.0790    0.3450    0.2246    0.3584    0.3502    0.3529    0.3430
   
   C = 
      0.8140
      0.3532
      0.7222
      0.1658
      0.7550
   
   M = 
      0.5559    0.6989    0.6533    0.2091    0.4827    0.8343    0.9993
      0.0367    0.2668    0.1309    0.4124    0.7839    0.7578    0.6815
      0.0465    0.4471    0.0632    0.6573    0.5825    0.8202    0.8077
      0.7185    0.2432    0.4359    0.6919    0.5299    0.3100    0.0616
      0.4662    0.0365    0.7062    0.4845    0.0209    0.1917    0.0941
      0.1337    0.6583    0.1083    0.1703    0.2485    0.5858    0.7271
      0.7861    0.1176    0.9498    0.4782    0.5969    0.0475    0.5726
      0.0502    0.7972    0.4479    0.8567    0.1642    0.4592    0.0318
   

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
      0.3208    0.3881    0.9505    0.8747
   
   R2 = 
      0.5081    0.8190    0.4411    0.5105    0.5326
   
   R3 = 
      0.3208    0.3881    0.9505    0.8747    0.5081    0.8190    0.4411    0.5105    0.5326
   
   C1 = 
      0.5371
      0.9342
      0.3313
      0.3832
      0.7969
      0.2798
      0.0281
      0.7970
      0.6188
      0.9538
   
   C2 = 
      0.2205
      0.5732
      0.4027
      0.2904
      0.7869
      0.0500
      0.4962
      0.3869
      0.9627
      0.7246
   
   M = 
      0.5371    0.2205
      0.9342    0.5732
      0.3313    0.4027
      0.3832    0.2904
      0.7969    0.7869
      0.2798    0.0500
      0.0281    0.4962
      0.7970    0.3869
      0.6188    0.9627
      0.9538    0.7246
   


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
      0.0317    0.0305    0.8845    0.3525
   
   R2 = 
      0.1423    0.9178    0.4251    0.7098
   
   M = 
      0.0317    0.0305    0.8845    0.3525
      0.1423    0.9178    0.4251    0.7098
   
   C1 = 
      0.0422
      0.7078
      0.1350
      0.0823
      0.4427
      0.1736
      0.7287
      0.5301
      0.2416
      0.8464
   
   C2 = 
      0.2782
      0.1102
   
   C3 = 
      0.0422
      0.7078
      0.1350
      0.0823
      0.4427
      0.1736
      0.7287
      0.5301
      0.2416
      0.8464
      0.2782
      0.1102
   

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
   

