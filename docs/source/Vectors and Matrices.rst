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
      0.2337    0.3469    0.9815    0.3384    0.7032    0.6446    0.6328
   
   C = 
      0.5925
      0.1136
      0.8372
      0.0017
      0.3069
   
   M = 
      0.9796    0.0192    0.7175    0.2200    0.9908    0.3564    0.1273
      0.6365    0.6521    0.1420    0.2400    0.1573    0.4559    0.9898
      0.4566    0.8502    0.7435    0.7189    0.9772    0.9762    0.8231
      0.8544    0.3492    0.7564    0.9202    0.4174    0.9054    0.6595
      0.0289    0.2265    0.9290    0.6146    0.8667    0.7785    0.8055
      0.7465    0.8246    0.6021    0.4631    0.4430    0.5740    0.3499
      0.7716    0.7666    0.4716    0.1439    0.1908    0.5884    0.1358
      0.9956    0.2146    0.6037    0.9250    0.7115    0.3043    0.3068
   

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
      0.4125    0.2512    0.2535    0.6876
   
   R2 = 
      0.9256    0.1974    0.3115    0.7478    0.9439
   
   R3 = 
      0.4125    0.2512    0.2535    0.6876    0.9256    0.1974    0.3115    0.7478    0.9439
   
   C1 = 
      0.4165
      0.7424
      0.8321
      0.1388
      0.2104
      0.0506
      0.9412
      0.2988
      0.2162
      0.8571
   
   C2 = 
      0.9376
      0.1357
      0.1391
      0.2708
      0.5858
      0.5670
      0.5032
      0.0222
      0.1039
      0.4055
   
   M = 
      0.4165    0.9376
      0.7424    0.1357
      0.8321    0.1391
      0.1388    0.2708
      0.2104    0.5858
      0.0506    0.5670
      0.9412    0.5032
      0.2988    0.0222
      0.2162    0.1039
      0.8571    0.4055
   


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
      0.0901    0.5958    0.3363    0.0129
   
   R2 = 
      0.4234    0.1914    0.4772    0.3518
   
   M = 
      0.0901    0.5958    0.3363    0.0129
      0.4234    0.1914    0.4772    0.3518
   
   C1 = 
      0.9373
      0.8298
      0.9745
      0.4489
      0.1923
      0.6746
      0.8368
      0.0432
      0.8573
      0.5220
   
   C2 = 
      0.2719
      0.4383
   
   C3 = 
      0.9373
      0.8298
      0.9745
      0.4489
      0.1923
      0.6746
      0.8368
      0.0432
      0.8573
      0.5220
      0.2719
      0.4383
   

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
   

