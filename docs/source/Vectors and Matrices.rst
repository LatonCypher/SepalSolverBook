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
      0.5465    0.9980    0.0444    0.6672    0.8207    0.4878    0.5953
   
   C = 
      0.8150
      0.2930
      0.1641
      0.2958
      0.7808
   
   M = 
      0.7557    0.8104    0.3914    0.2321    0.7073    0.5037    0.1101
      0.2209    0.1588    0.2948    0.7913    0.8908    0.9370    0.9131
      0.9394    0.8715    0.9561    0.5665    0.5065    0.9415    0.7761
      0.2811    0.7788    0.6013    0.0558    0.8318    0.1024    0.2613
      0.5124    0.9861    0.7850    0.5256    0.7901    0.7336    0.2979
      0.9444    0.3682    0.3221    0.1602    0.3093    0.7255    0.0432
      0.0994    0.2045    0.8002    0.4601    0.6800    0.6856    0.6983
      0.9670    0.6862    0.9378    0.4022    0.6107    0.6733    0.6106
   

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
      0.2869    0.1715    0.6958    0.1992
   
   R2 = 
      0.5214    0.7611    0.8520    0.7493    0.5160
   
   R3 = 
      0.2869    0.1715    0.6958    0.1992    0.5214    0.7611    0.8520    0.7493    0.5160
   
   C1 = 
      0.6935
      0.1353
      0.0712
      0.5573
      0.4348
      0.8719
      0.5319
      0.5131
      0.8372
      0.0689
   
   C2 = 
      0.1251
      0.8524
      0.0320
      0.7671
      0.0381
      0.4150
      0.9719
      0.4236
      0.3625
      0.1851
   
   M = 
      0.6935    0.1251
      0.1353    0.8524
      0.0712    0.0320
      0.5573    0.7671
      0.4348    0.0381
      0.8719    0.4150
      0.5319    0.9719
      0.5131    0.4236
      0.8372    0.3625
      0.0689    0.1851
   


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
      0.5026    0.9525    0.8543    0.9325
   
   R2 = 
      0.0279    0.3513    0.4930    0.9242
   
   M = 
      0.5026    0.9525    0.8543    0.9325
      0.0279    0.3513    0.4930    0.9242
   
   C1 = 
      0.6468
      0.6223
      0.2031
      0.2705
      0.0920
      0.3283
      0.8605
      0.0590
      0.9215
      0.2934
   
   C2 = 
      0.6220
      0.0649
   
   C3 = 
      0.6468
      0.6223
      0.2031
      0.2705
      0.0920
      0.3283
      0.8605
      0.0590
      0.9215
      0.2934
      0.6220
      0.0649
   

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
   

