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
      0.6797    0.3274    0.0515    0.3892    0.3447    0.9833    0.7960
   
   C = 
      0.6771
      0.6372
      0.2606
      0.1504
      0.2054
   
   M = 
      0.2027    0.2134    0.7640    0.7736    0.9908    0.7842    0.4919
      0.7395    0.7328    0.3727    0.2610    0.4362    0.6651    0.7945
      0.7743    0.6968    0.0505    0.4287    0.0144    0.6170    0.0052
      0.8612    0.7905    0.5811    0.1886    0.3152    0.0867    0.2725
      0.7564    0.5679    0.1633    0.5995    0.3067    0.0462    0.7256
      0.6817    0.8886    0.8187    0.3334    0.8491    0.4873    0.3371
      0.8599    0.0589    0.0111    0.8133    0.7815    0.0052    0.0535
      0.5492    0.8554    0.8751    0.1176    0.1319    0.8581    0.6868
   

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
      0.4507    0.1225    0.5943    0.2414
   
   R2 = 
      0.6742    0.3982    0.4318    0.3340    0.6131
   
   R3 = 
      0.4507    0.1225    0.5943    0.2414    0.6742    0.3982    0.4318    0.3340    0.6131
   
   C1 = 
      0.0122
      0.8772
      0.9531
      0.8041
      0.5492
      0.2189
      0.9523
      0.1683
      0.4091
      0.3226
   
   C2 = 
      0.4933
      0.8484
      0.2230
      0.2408
      0.0408
      0.4653
      0.8779
      0.8393
      0.3954
      0.1833
   
   M = 
      0.0122    0.4933
      0.8772    0.8484
      0.9531    0.2230
      0.8041    0.2408
      0.5492    0.0408
      0.2189    0.4653
      0.9523    0.8779
      0.1683    0.8393
      0.4091    0.3954
      0.3226    0.1833
   


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
      0.2175    0.7676    0.3325    0.7348
   
   R2 = 
      0.5835    0.8803    0.1203    0.9923
   
   M = 
      0.2175    0.7676    0.3325    0.7348
      0.5835    0.8803    0.1203    0.9923
   
   C1 = 
      0.9601
      0.3661
      0.3279
      0.0812
      0.0477
      0.2492
      0.4617
      0.3912
      0.1388
      0.2520
   
   C2 = 
      0.1439
      0.5249
   
   C3 = 
      0.9601
      0.3661
      0.3279
      0.0812
      0.0477
      0.2492
      0.4617
      0.3912
      0.1388
      0.2520
      0.1439
      0.5249
   

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
   

