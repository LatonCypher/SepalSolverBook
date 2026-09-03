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
      0.9880    0.4894    0.4950    0.6970    0.0949    0.3112    0.8740
   
   C = 
      0.2177
      0.3156
      0.0839
      0.5500
      0.0474
   
   M = 
      0.7323    0.0874    0.1571    0.6331    0.5519    0.3607    0.2312
      0.0228    0.3577    0.4271    0.1183    0.9738    0.8048    0.0629
      0.9087    0.1048    0.9999    0.9064    0.1246    0.1079    0.9444
      0.5039    0.5866    0.1189    0.2763    0.5538    0.8266    0.5669
      0.0295    0.3548    0.8768    0.7754    0.1230    0.9363    0.0240
      0.1469    0.4937    0.8641    0.6636    0.1185    0.0019    0.2685
      0.9739    0.6861    0.9968    0.8889    0.1484    0.1730    0.0224
      0.7629    0.9537    0.9112    0.1531    0.0501    0.7099    0.2594
   

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
      0.7898    0.1472    0.0698    0.7551
   
   R2 = 
      0.4581    0.1021    0.5215    0.1107    0.4352
   
   R3 = 
      0.7898    0.1472    0.0698    0.7551    0.4581    0.1021    0.5215    0.1107    0.4352
   
   C1 = 
      0.9519
      0.3127
      0.6312
      0.2631
      0.2759
      0.9110
      0.1404
      0.6515
      0.6956
      0.0237
   
   C2 = 
      0.5954
      0.9442
      0.9470
      0.1754
      0.7149
      0.0122
      0.5143
      0.2540
      0.3508
      0.4311
   
   M = 
      0.9519    0.5954
      0.3127    0.9442
      0.6312    0.9470
      0.2631    0.1754
      0.2759    0.7149
      0.9110    0.0122
      0.1404    0.5143
      0.6515    0.2540
      0.6956    0.3508
      0.0237    0.4311
   


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
      0.6640    0.1134    0.3900    0.0573
   
   R2 = 
      0.3972    0.8970    0.7985    0.5964
   
   M = 
      0.6640    0.1134    0.3900    0.0573
      0.3972    0.8970    0.7985    0.5964
   
   C1 = 
      0.3478
      0.7537
      0.6045
      0.2255
      0.2135
      0.0775
      0.1441
      0.8574
      0.2945
      0.1134
   
   C2 = 
      0.3256
      0.9217
   
   C3 = 
      0.3478
      0.7537
      0.6045
      0.2255
      0.2135
      0.0775
      0.1441
      0.8574
      0.2945
      0.1134
      0.3256
      0.9217
   

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
   

