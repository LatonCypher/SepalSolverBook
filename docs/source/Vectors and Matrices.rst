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
      0.6490    0.1610    0.4746    0.1501    0.9140    0.9005    0.9213
   
   C = 
      0.4832
      0.0402
      0.5288
      0.2571
      0.2858
   
   M = 
      0.0054    0.6777    0.0701    0.3498    0.6848    0.0570    0.4328
      0.7604    0.4319    0.9198    0.8858    0.9180    0.5820    0.4850
      0.9171    0.3382    0.5546    0.1844    0.5604    0.6755    0.5616
      0.7620    0.8979    0.7146    0.8993    0.0940    0.3861    0.2669
      0.9663    0.2243    0.4673    0.2012    0.7403    0.0436    0.7480
      0.2938    0.3682    0.9358    0.6148    0.1567    0.7531    0.7932
      0.5730    0.1633    0.1834    0.2111    0.6117    0.8419    0.2283
      0.0774    0.5049    0.8750    0.8916    0.5385    0.1284    0.4359
   

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
      0.0994    0.8498    0.3734    0.6213
   
   R2 = 
      0.3446    0.2974    0.8378    0.6806    0.1366
   
   R3 = 
      0.0994    0.8498    0.3734    0.6213    0.3446    0.2974    0.8378    0.6806    0.1366
   
   C1 = 
      0.7411
      0.8560
      0.6987
      0.3240
      0.9950
      0.4229
      0.3074
      0.1402
      0.6263
      0.8547
   
   C2 = 
      0.2578
      0.6972
      0.2035
      0.0307
      0.1550
      0.9664
      0.1038
      0.2796
      0.7497
      0.4688
   
   M = 
      0.7411    0.2578
      0.8560    0.6972
      0.6987    0.2035
      0.3240    0.0307
      0.9950    0.1550
      0.4229    0.9664
      0.3074    0.1038
      0.1402    0.2796
      0.6263    0.7497
      0.8547    0.4688
   


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
      0.9260    0.9642    0.3435    0.0460
   
   R2 = 
      0.6423    0.3301    0.4265    0.1145
   
   M = 
      0.9260    0.9642    0.3435    0.0460
      0.6423    0.3301    0.4265    0.1145
   
   C1 = 
      0.3673
      0.1376
      0.1559
      0.4027
      0.8066
      0.7958
      0.1140
      0.3833
      0.2289
      0.1939
   
   C2 = 
      0.0377
      0.4163
   
   C3 = 
      0.3673
      0.1376
      0.1559
      0.4027
      0.8066
      0.7958
      0.1140
      0.3833
      0.2289
      0.1939
      0.0377
      0.4163
   

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
   

