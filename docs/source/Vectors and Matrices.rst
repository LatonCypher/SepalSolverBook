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
      0.5029    0.6243    0.6690    0.1507    0.2918    0.9872    0.3179
   
   C = 
      0.9504
      0.4165
      0.0163
      0.4355
      0.6995
   
   M = 
      0.2625    0.7152    0.7129    0.3066    0.1279    0.8000    0.9724
      0.8900    0.3419    0.7240    0.1543    0.7311    0.3115    0.0559
      0.4575    0.4845    0.7942    0.0974    0.0648    0.5142    0.5205
      0.5042    0.2426    0.7751    0.7851    0.2315    0.2660    0.4789
      0.8418    0.6301    0.8982    0.7788    0.3028    0.9498    0.5524
      0.5341    0.4066    0.4741    0.0359    0.3638    0.7121    0.0425
      0.9364    0.5179    0.4731    0.6126    0.1292    0.5970    0.7635
      0.7644    0.5540    0.3339    0.6256    0.9421    0.6070    0.5224
   

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
      0.9300    0.4015    0.0026    0.7359
   
   R2 = 
      0.6880    0.7130    0.9638    0.3695    0.1562
   
   R3 = 
      0.9300    0.4015    0.0026    0.7359    0.6880    0.7130    0.9638    0.3695    0.1562
   
   C1 = 
      0.4256
      0.6264
      0.1937
      0.4278
      0.0481
      0.2506
      0.5076
      0.7323
      0.5780
      0.9146
   
   C2 = 
      0.7796
      0.6216
      0.6296
      0.5869
      0.4438
      0.7385
      0.4968
      0.0695
      0.0988
      0.1352
   
   M = 
      0.4256    0.7796
      0.6264    0.6216
      0.1937    0.6296
      0.4278    0.5869
      0.0481    0.4438
      0.2506    0.7385
      0.5076    0.4968
      0.7323    0.0695
      0.5780    0.0988
      0.9146    0.1352
   


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
      0.0339    0.4076    0.0197    0.8680
   
   R2 = 
      0.6632    0.9357    0.4620    0.3228
   
   M = 
      0.0339    0.4076    0.0197    0.8680
      0.6632    0.9357    0.4620    0.3228
   
   C1 = 
      0.2096
      0.5793
      0.3395
      0.8699
      0.6697
      0.6109
      0.0087
      0.9359
      0.1834
      0.7955
   
   C2 = 
      0.8349
      0.8618
   
   C3 = 
      0.2096
      0.5793
      0.3395
      0.8699
      0.6697
      0.6109
      0.0087
      0.9359
      0.1834
      0.7955
      0.8349
      0.8618
   

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
   

