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
      0.8878    0.1982    0.8269    0.4462    0.0692    0.0815    0.5699
   
   C = 
      0.7764
      0.7877
      0.5430
      0.6872
      0.5015
   
   M = 
      0.0783    0.2985    0.1783    0.8475    0.7215    0.9456    0.7101
      0.5997    0.6746    0.1613    0.3808    0.6327    0.8925    0.3716
      0.3920    0.7334    0.4359    0.8141    0.5201    0.3443    0.8429
      0.4267    0.2677    0.4571    0.0004    0.9838    0.8187    0.0909
      0.9075    0.9752    0.1477    0.0562    0.8415    0.1335    0.0524
      0.4143    0.1107    0.9325    0.2499    0.8971    0.0452    0.6935
      0.8558    0.7659    0.6711    0.5713    0.9611    0.5551    0.6114
      0.2777    0.9445    0.2523    0.4222    0.7499    0.9961    0.3952
   

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
      0.1051    0.4878    0.1448    0.9931
   
   R2 = 
      0.1929    0.7769    0.1997    0.2287    0.8900
   
   R3 = 
      0.1051    0.4878    0.1448    0.9931    0.1929    0.7769    0.1997    0.2287    0.8900
   
   C1 = 
      0.8527
      0.8553
      0.0086
      0.8577
      0.7866
      0.0648
      0.9416
      0.0332
      0.0536
      0.2747
   
   C2 = 
      0.6516
      0.6717
      0.5436
      0.8047
      0.2041
      0.3956
      0.2868
      0.1485
      0.4777
      0.9928
   
   M = 
      0.8527    0.6516
      0.8553    0.6717
      0.0086    0.5436
      0.8577    0.8047
      0.7866    0.2041
      0.0648    0.3956
      0.9416    0.2868
      0.0332    0.1485
      0.0536    0.4777
      0.2747    0.9928
   


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
      0.4347    0.3939    0.3396    0.0471
   
   R2 = 
      0.5564    0.9221    0.4410    0.8295
   
   M = 
      0.4347    0.3939    0.3396    0.0471
      0.5564    0.9221    0.4410    0.8295
   
   C1 = 
      0.3003
      0.6570
      0.1631
      0.0427
      0.6379
      0.4053
      0.7609
      0.3055
      0.9235
      0.3551
   
   C2 = 
      0.0383
      0.7629
   
   C3 = 
      0.3003
      0.6570
      0.1631
      0.0427
      0.6379
      0.4053
      0.7609
      0.3055
      0.9235
      0.3551
      0.0383
      0.7629
   

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
   

