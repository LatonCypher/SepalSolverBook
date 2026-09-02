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
      0.0501    0.9395    0.0735    0.9266    0.5491    0.9936    0.4437
   
   C = 
      0.4158
      0.1814
      0.8486
      0.8885
      0.4472
   
   M = 
      0.5434    0.6980    0.1408    0.6581    0.7353    0.4122    0.4108
      0.2568    0.7059    0.8070    0.3668    0.4757    0.5374    0.7146
      0.1328    0.3970    0.2479    0.4851    0.0871    0.8676    0.0896
      0.5351    0.0248    0.6281    0.5562    0.2589    0.6216    0.9641
      0.5139    0.4516    0.6927    0.6072    0.5696    0.6025    0.3239
      0.4341    0.2938    0.4983    0.0445    0.9161    0.9793    0.6688
      0.9537    0.6282    0.8118    0.7886    0.7390    0.9944    0.2457
      0.5185    0.2043    0.2737    0.6705    0.4984    0.5554    0.1395
   

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
      0.0137    0.4431    0.2582    0.5521
   
   R2 = 
      0.5970    0.3461    0.6144    0.5981    0.9278
   
   R3 = 
      0.0137    0.4431    0.2582    0.5521    0.5970    0.3461    0.6144    0.5981    0.9278
   
   C1 = 
      0.7288
      0.6445
      0.3761
      0.3211
      0.8319
      0.9859
      0.1260
      0.2950
      0.4911
      0.1599
   
   C2 = 
      0.9953
      0.6813
      0.0658
      0.4707
      0.9341
      0.2030
      0.8175
      0.9838
      0.3957
      0.8761
   
   M = 
      0.7288    0.9953
      0.6445    0.6813
      0.3761    0.0658
      0.3211    0.4707
      0.8319    0.9341
      0.9859    0.2030
      0.1260    0.8175
      0.2950    0.9838
      0.4911    0.3957
      0.1599    0.8761
   


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
      0.4536    0.3888    0.3007    0.5130
   
   R2 = 
      0.7595    0.0159    0.5539    0.4749
   
   M = 
      0.4536    0.3888    0.3007    0.5130
      0.7595    0.0159    0.5539    0.4749
   
   C1 = 
      0.5139
      0.4888
      0.6931
      0.2243
      0.3200
      0.5039
      0.6565
      0.3233
      0.6750
      0.0435
   
   C2 = 
      0.4542
      0.4673
   
   C3 = 
      0.5139
      0.4888
      0.6931
      0.2243
      0.3200
      0.5039
      0.6565
      0.3233
      0.6750
      0.0435
      0.4542
      0.4673
   

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
   

