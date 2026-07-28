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
      0.9179    0.1886    0.4387    0.7937    0.3062    0.1313    0.3193
   
   C = 
      0.9880
      0.1825
      0.4698
      0.5311
      0.5953
   
   M = 
      0.4808    0.1443    0.5090    0.9952    0.8454    0.9026    0.4024
      0.7064    0.9986    0.4754    0.2959    0.9499    0.1012    0.9754
      0.4753    0.7527    0.7792    0.8973    0.8781    0.0340    0.9229
      0.1653    0.3728    0.9879    0.5273    0.7922    0.8377    0.1386
      0.6008    0.3801    0.3241    0.6319    0.5642    0.8064    0.6531
      0.6732    0.8551    0.0490    0.5913    0.3357    0.4132    0.6223
      0.7965    0.3148    0.8718    0.0034    0.6149    0.9362    0.7402
      0.6664    0.4192    0.8949    0.0901    0.7772    0.3641    0.3297
   

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
      0.5258    0.2799    0.4463    0.1610
   
   R2 = 
      0.5526    0.2750    0.4911    0.1890    0.9938
   
   R3 = 
      0.5258    0.2799    0.4463    0.1610    0.5526    0.2750    0.4911    0.1890    0.9938
   
   C1 = 
      0.6299
      0.2276
      0.9466
      0.5459
      0.1676
      0.8551
      0.8728
      0.5509
      0.4190
      0.4096
   
   C2 = 
      0.3179
      0.9340
      0.9691
      0.3334
      0.4851
      0.0244
      0.8658
      0.9361
      0.4822
      0.9597
   
   M = 
      0.6299    0.3179
      0.2276    0.9340
      0.9466    0.9691
      0.5459    0.3334
      0.1676    0.4851
      0.8551    0.0244
      0.8728    0.8658
      0.5509    0.9361
      0.4190    0.4822
      0.4096    0.9597
   


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
      0.4258    0.3710    0.8928    0.9598
   
   R2 = 
      0.6160    0.8127    0.1586    0.7250
   
   M = 
      0.4258    0.3710    0.8928    0.9598
      0.6160    0.8127    0.1586    0.7250
   
   C1 = 
      0.3869
      0.3832
      0.1552
      0.9736
      0.8155
      0.0898
      0.3827
      0.8949
      0.6427
      0.8650
   
   C2 = 
      0.6244
      0.6968
   
   C3 = 
      0.3869
      0.3832
      0.1552
      0.9736
      0.8155
      0.0898
      0.3827
      0.8949
      0.6427
      0.8650
      0.6244
      0.6968
   

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
   

