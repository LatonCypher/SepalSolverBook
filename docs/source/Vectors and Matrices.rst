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
      0.8199    0.3874    0.0572    0.5254    0.5966    0.5447    0.3232
   
   C = 
      0.4809
      0.8177
      0.9991
      0.1378
      0.9898
   
   M = 
      0.9324    0.5097    0.4678    0.8794    0.4093    0.9805    0.4715
      0.5945    0.7567    0.3243    0.6746    0.8039    0.4210    0.8835
      0.9838    0.6024    0.9431    0.5747    0.0505    0.0289    0.3914
      0.2783    0.7266    0.3361    0.4947    0.4949    0.8364    0.9327
      0.4749    0.5558    0.2221    0.2863    0.1275    0.1120    0.5075
      0.4009    0.8421    0.1922    0.1486    0.0192    0.5359    0.7416
      0.3046    0.9442    0.3083    0.8552    0.2970    0.3751    0.7317
      0.5324    0.3536    0.0676    0.7470    0.0531    0.9720    0.0887
   

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
      0.9259    0.6252    0.9956    0.5559
   
   R2 = 
      0.7160    0.1785    0.8643    0.6340    0.0563
   
   R3 = 
      0.9259    0.6252    0.9956    0.5559    0.7160    0.1785    0.8643    0.6340    0.0563
   
   C1 = 
      0.6816
      0.4407
      0.6611
      0.9946
      0.6227
      0.5079
      0.2639
      0.9937
      0.9228
      0.9993
   
   C2 = 
      0.6569
      0.2817
      0.4897
      0.2550
      0.0956
      0.0548
      0.5366
      0.8180
      0.9622
      0.5504
   
   M = 
      0.6816    0.6569
      0.4407    0.2817
      0.6611    0.4897
      0.9946    0.2550
      0.6227    0.0956
      0.5079    0.0548
      0.2639    0.5366
      0.9937    0.8180
      0.9228    0.9622
      0.9993    0.5504
   


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
      0.9777    0.0850    0.9325    0.1109
   
   R2 = 
      0.3313    0.6658    0.7906    0.4992
   
   M = 
      0.9777    0.0850    0.9325    0.1109
      0.3313    0.6658    0.7906    0.4992
   
   C1 = 
      0.4699
      0.6473
      0.8396
      0.8320
      0.5804
      0.9314
      0.7214
      0.7163
      0.2933
      0.2927
   
   C2 = 
      0.6109
      0.5649
   
   C3 = 
      0.4699
      0.6473
      0.8396
      0.8320
      0.5804
      0.9314
      0.7214
      0.7163
      0.2933
      0.2927
      0.6109
      0.5649
   

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
   

