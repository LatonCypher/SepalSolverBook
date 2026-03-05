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
      0.2472    0.5286    0.1118    0.4135    0.8707    0.7198    0.8882
   
   C = 
      0.4848
      0.5441
      0.7573
      0.2472
      0.6451
   
   M = 
      0.3010    0.8543    0.1357    0.4367    0.4143    0.9350    0.4362
      0.8928    0.4900    0.0429    0.7123    0.4638    0.7743    0.5594
      0.3874    0.0356    0.6330    0.9255    0.2110    0.7421    0.4998
      0.0974    0.8782    0.1612    0.1410    0.0097    0.3597    0.9755
      0.9433    0.1563    0.5054    0.1584    0.0755    0.0549    0.1188
      0.7787    0.7926    0.2480    0.4293    0.8776    0.1034    0.4314
      0.7477    0.1905    0.3925    0.3601    0.7058    0.1104    0.5073
      0.3295    0.0817    0.8565    0.0576    0.3814    0.0497    0.0441
   

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
      0.4733    0.8723    0.7445    0.4742
   
   R2 = 
      0.2414    0.2176    0.0607    0.7756    0.3329
   
   R3 = 
      0.4733    0.8723    0.7445    0.4742    0.2414    0.2176    0.0607    0.7756    0.3329
   
   C1 = 
      0.5940
      0.2890
      0.0885
      0.1639
      0.9625
      0.7567
      0.2859
      0.6168
      0.6458
      0.4953
   
   C2 = 
      0.6083
      0.9217
      0.6709
      0.4638
      0.8714
      0.9013
      0.2735
      0.1771
      0.1252
      0.0960
   
   M = 
      0.5940    0.6083
      0.2890    0.9217
      0.0885    0.6709
      0.1639    0.4638
      0.9625    0.8714
      0.7567    0.9013
      0.2859    0.2735
      0.6168    0.1771
      0.6458    0.1252
      0.4953    0.0960
   


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
      0.8195    0.3564    0.3642    0.9079
   
   R2 = 
      0.1205    0.2157    0.5041    0.5878
   
   M = 
      0.8195    0.3564    0.3642    0.9079
      0.1205    0.2157    0.5041    0.5878
   
   C1 = 
      0.5829
      0.8498
      0.2967
      0.8230
      0.4577
      0.2132
      0.3395
      0.6693
      0.5342
      0.0209
   
   C2 = 
      0.5410
      0.0846
   
   C3 = 
      0.5829
      0.8498
      0.2967
      0.8230
      0.4577
      0.2132
      0.3395
      0.6693
      0.5342
      0.0209
      0.5410
      0.0846
   

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
   

