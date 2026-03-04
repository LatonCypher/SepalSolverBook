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
      0.6980    0.5953    0.0576    0.7204    0.6145    0.0391    0.6041
   
   C = 
      0.6361
      0.3126
      0.8765
      0.3932
      0.6476
   
   M = 
      0.2296    0.4940    0.4792    0.5570    0.0999    0.3392    0.8582
      0.3949    0.4209    0.9331    0.3135    0.2766    0.7610    0.9368
      0.1027    0.1423    0.1104    0.8046    0.8938    0.6740    0.2204
      0.4845    0.6583    0.7428    0.6642    0.5987    0.0774    0.3751
      0.1033    0.0447    0.1157    0.1329    0.8254    0.5100    0.3159
      0.0515    0.0703    0.1165    0.4968    0.6352    0.0805    0.5604
      0.6650    0.1663    0.4894    0.8581    0.3831    0.5082    0.0192
      0.8727    0.5007    0.3505    0.8607    0.4806    0.7638    0.9417
   

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
      0.7294    0.4025    0.6639    0.6460
   
   R2 = 
      0.5305    0.2899    0.6089    0.2409    0.5939
   
   R3 = 
      0.7294    0.4025    0.6639    0.6460    0.5305    0.2899    0.6089    0.2409    0.5939
   
   C1 = 
      0.0336
      0.2778
      0.2075
      0.2681
      0.9197
      0.3422
      0.2445
      0.1685
      0.8269
      0.7540
   
   C2 = 
      0.4771
      0.1951
      0.6465
      0.5500
      0.2440
      0.6241
      0.2346
      0.7581
      0.4654
      0.4575
   
   M = 
      0.0336    0.4771
      0.2778    0.1951
      0.2075    0.6465
      0.2681    0.5500
      0.9197    0.2440
      0.3422    0.6241
      0.2445    0.2346
      0.1685    0.7581
      0.8269    0.4654
      0.7540    0.4575
   


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
      0.8135    0.7868    0.6944    0.6137
   
   R2 = 
      0.0740    0.1891    0.5910    0.0008
   
   M = 
      0.8135    0.7868    0.6944    0.6137
      0.0740    0.1891    0.5910    0.0008
   
   C1 = 
      0.9758
      0.3446
      0.4386
      0.3509
      0.6989
      0.5300
      0.7248
      0.2906
      0.9572
      0.5399
   
   C2 = 
      0.4546
      0.5583
   
   C3 = 
      0.9758
      0.3446
      0.4386
      0.3509
      0.6989
      0.5300
      0.7248
      0.2906
      0.9572
      0.5399
      0.4546
      0.5583
   

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
   

