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
      0.2952    0.5468    0.0853    0.3942    0.1641    0.5438    0.1507
   
   C = 
      0.9988
      0.9368
      0.2675
      0.4968
      0.0500
   
   M = 
      0.2286    0.9120    0.5108    0.6335    0.7538    0.6215    0.7387
      0.3326    0.2740    0.9628    0.7048    0.2438    0.2820    0.8843
      0.4616    0.5661    0.3795    0.5851    0.2297    0.7115    0.9814
      0.6222    0.8135    0.8306    0.4059    0.9618    0.0780    0.0511
      0.3022    0.0735    0.3948    0.6334    0.2116    0.7106    0.3330
      0.2983    0.8411    0.4283    0.0366    0.2328    0.4599    0.3309
      0.6411    0.9641    0.2781    0.7607    0.6375    0.6572    0.5299
      0.4361    0.8439    0.8267    0.2032    0.0163    0.2091    0.2713
   

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
      0.7571    0.6750    0.8649    0.7697
   
   R2 = 
      0.6171    0.2968    0.7220    0.5569    0.2301
   
   R3 = 
      0.7571    0.6750    0.8649    0.7697    0.6171    0.2968    0.7220    0.5569    0.2301
   
   C1 = 
      0.6904
      0.1176
      0.1540
      0.4752
      0.1901
      0.8952
      0.4265
      0.8819
      0.7769
      0.2038
   
   C2 = 
      0.3083
      0.6041
      0.9069
      0.4646
      0.7965
      0.2046
      0.3318
      0.3565
      0.7318
      0.5294
   
   M = 
      0.6904    0.3083
      0.1176    0.6041
      0.1540    0.9069
      0.4752    0.4646
      0.1901    0.7965
      0.8952    0.2046
      0.4265    0.3318
      0.8819    0.3565
      0.7769    0.7318
      0.2038    0.5294
   


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
      0.6403    0.9434    0.7035    0.1573
   
   R2 = 
      0.5131    0.4409    0.2564    0.3713
   
   M = 
      0.6403    0.9434    0.7035    0.1573
      0.5131    0.4409    0.2564    0.3713
   
   C1 = 
      0.3936
      0.1374
      0.7812
      0.7324
      0.1829
      0.5372
      0.0327
      0.2558
      0.1518
      0.3426
   
   C2 = 
      0.5792
      0.9231
   
   C3 = 
      0.3936
      0.1374
      0.7812
      0.7324
      0.1829
      0.5372
      0.0327
      0.2558
      0.1518
      0.3426
      0.5792
      0.9231
   

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
   

