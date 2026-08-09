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
      0.9135    0.2100    0.8477    0.7516    0.0185    0.6911    0.9240
   
   C = 
      0.2619
      0.6737
      0.5919
      0.1347
      0.1897
   
   M = 
      0.7748    0.3577    0.8085    0.7310    0.2314    0.9834    0.4119
      0.3117    0.6638    0.1679    0.6461    0.8225    0.0992    0.7828
      0.4322    0.6430    0.0294    0.3220    0.0271    0.5377    0.4214
      0.9528    0.6736    0.0784    0.6368    0.0248    0.4835    0.5614
      0.1920    0.1920    0.9391    0.6929    0.4297    0.7822    0.7625
      0.1109    0.5909    0.8882    0.4065    0.7080    0.6233    0.9918
      0.4257    0.5339    0.4296    0.2213    0.2772    0.0404    0.8900
      0.0496    0.7602    0.5066    0.3391    0.4224    0.0731    0.4920
   

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
      0.0621    0.3603    0.7982    0.7081
   
   R2 = 
      0.3542    0.8125    0.3753    0.4016    0.4945
   
   R3 = 
      0.0621    0.3603    0.7982    0.7081    0.3542    0.8125    0.3753    0.4016    0.4945
   
   C1 = 
      0.4572
      0.7488
      0.1337
      0.6187
      0.0323
      0.8079
      0.5418
      0.8423
      0.6528
      0.5475
   
   C2 = 
      0.8539
      0.1085
      0.9171
      0.9807
      0.6165
      0.4564
      0.4234
      0.3162
      0.1428
      0.8851
   
   M = 
      0.4572    0.8539
      0.7488    0.1085
      0.1337    0.9171
      0.6187    0.9807
      0.0323    0.6165
      0.8079    0.4564
      0.5418    0.4234
      0.8423    0.3162
      0.6528    0.1428
      0.5475    0.8851
   


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
      0.2856    0.2870    0.6350    0.6352
   
   R2 = 
      0.4804    0.1475    0.4972    0.2966
   
   M = 
      0.2856    0.2870    0.6350    0.6352
      0.4804    0.1475    0.4972    0.2966
   
   C1 = 
      0.5307
      0.1186
      0.3308
      0.3126
      0.5619
      0.4993
      0.4503
      0.1841
      0.0489
      0.2645
   
   C2 = 
      0.5678
      0.0588
   
   C3 = 
      0.5307
      0.1186
      0.3308
      0.3126
      0.5619
      0.4993
      0.4503
      0.1841
      0.0489
      0.2645
      0.5678
      0.0588
   

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
   

