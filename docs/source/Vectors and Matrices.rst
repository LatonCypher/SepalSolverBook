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
      0.6478    0.9841    0.1765    0.7535    0.7791    0.1843    0.8852
   
   C = 
      0.9497
      0.5210
      0.8104
      0.2287
      0.4922
   
   M = 
      0.8365    0.0533    0.9241    0.2852    0.3275    0.2249    0.5452
      0.2605    0.7786    0.1705    0.5097    0.3759    0.5858    0.4487
      0.2684    0.3204    0.2659    0.8917    0.7424    0.1769    0.2648
      0.5169    0.9656    0.2740    0.2454    0.2089    0.3625    0.5361
      0.9051    0.9703    0.4330    0.0583    0.0654    0.0072    0.5177
      0.7716    0.1724    0.8514    0.4134    0.1063    0.7224    0.6027
      0.7506    0.8492    0.1392    0.8628    0.0492    0.3095    0.4052
      0.9411    0.3806    0.6681    0.5711    0.4578    0.4557    0.6856
   

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
      0.1301    0.3023    0.5166    0.7519
   
   R2 = 
      0.3997    0.6061    0.2968    0.3426    0.1525
   
   R3 = 
      0.1301    0.3023    0.5166    0.7519    0.3997    0.6061    0.2968    0.3426    0.1525
   
   C1 = 
      0.4642
      0.0425
      0.9216
      0.2026
      0.6825
      0.2581
      0.0773
      0.5641
      0.4228
      0.4932
   
   C2 = 
      0.7197
      0.8972
      0.2383
      0.1531
      0.1048
      0.8443
      0.3327
      0.8580
      0.8161
      0.7570
   
   M = 
      0.4642    0.7197
      0.0425    0.8972
      0.9216    0.2383
      0.2026    0.1531
      0.6825    0.1048
      0.2581    0.8443
      0.0773    0.3327
      0.5641    0.8580
      0.4228    0.8161
      0.4932    0.7570
   


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
      0.1025    0.5810    0.0621    0.7055
   
   R2 = 
      0.4707    0.0300    0.6295    0.4064
   
   M = 
      0.1025    0.5810    0.0621    0.7055
      0.4707    0.0300    0.6295    0.4064
   
   C1 = 
      0.7122
      0.9746
      0.6095
      0.4997
      0.3503
      0.9487
      0.1068
      0.7792
      0.2368
      0.8176
   
   C2 = 
      0.8623
      0.3893
   
   C3 = 
      0.7122
      0.9746
      0.6095
      0.4997
      0.3503
      0.9487
      0.1068
      0.7792
      0.2368
      0.8176
      0.8623
      0.3893
   

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
   

