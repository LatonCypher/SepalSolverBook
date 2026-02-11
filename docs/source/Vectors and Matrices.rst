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
      0.3474    0.5409    0.8626    0.1589    0.3926    0.0439    0.9756
   
   C = 
      0.7441
      0.7920
      0.6340
      0.6964
      0.9805
   
   M = 
      0.8696    0.1113    0.4842    0.0877    0.8987    0.8761    0.2396
      0.0092    0.7740    0.0675    0.0693    0.4272    0.5485    0.3438
      0.5366    0.3262    0.0873    0.9642    0.4566    0.6303    0.4245
      0.8548    0.0313    0.2614    0.2466    0.2352    0.0891    0.1717
      0.0667    0.2944    0.0370    0.2779    0.9967    0.8577    0.2209
      0.0953    0.4630    0.2122    0.1759    0.3221    0.6660    0.8518
      0.9673    0.5458    0.7290    0.4362    0.4362    0.1878    0.3187
      0.0006    0.3384    0.3426    0.5253    0.6227    0.0066    0.5535
   

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
      0.5707    0.0215    0.1321    0.5717
   
   R2 = 
      0.0315    0.1042    0.3927    0.5277    0.4104
   
   R3 = 
      0.5707    0.0215    0.1321    0.5717    0.0315    0.1042    0.3927    0.5277    0.4104
   
   C1 = 
      0.4510
      0.7760
      0.0680
      0.7523
      0.9192
      0.0843
      0.8447
      0.2343
      0.5326
      0.6460
   
   C2 = 
      0.6172
      0.1909
      0.2219
      0.3460
      0.6064
      0.9785
      0.6597
      0.1317
      0.3209
      0.3403
   
   M = 
      0.4510    0.6172
      0.7760    0.1909
      0.0680    0.2219
      0.7523    0.3460
      0.9192    0.6064
      0.0843    0.9785
      0.8447    0.6597
      0.2343    0.1317
      0.5326    0.3209
      0.6460    0.3403
   


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
      0.9858    0.2338    0.4412    0.4418
   
   R2 = 
      0.8210    0.9198    0.7776    0.0079
   
   M = 
      0.9858    0.2338    0.4412    0.4418
      0.8210    0.9198    0.7776    0.0079
   
   C1 = 
      0.7661
      0.7644
      0.5295
      0.2582
      0.9420
      0.6057
      0.5893
      0.3068
      0.4298
      0.9457
   
   C2 = 
      0.4317
      0.8088
   
   C3 = 
      0.7661
      0.7644
      0.5295
      0.2582
      0.9420
      0.6057
      0.5893
      0.3068
      0.4298
      0.9457
      0.4317
      0.8088
   

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
   

