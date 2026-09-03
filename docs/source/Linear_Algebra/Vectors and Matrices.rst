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
      0.8621    0.8426    0.9190    0.9537    0.0773    0.1414    0.9471
   
   C = 
      0.1613
      0.1744
      0.1435
      0.0812
      0.9487
   
   M = 
      0.7247    0.8369    0.4006    0.8919    0.7056    0.3128    0.9584
      0.9632    0.1453    0.4170    0.0147    0.5435    0.9142    0.0109
      0.4258    0.6591    0.3830    0.3791    0.8130    0.6320    0.0350
      0.7030    0.9102    0.5271    0.9482    0.7068    0.4351    0.1689
      0.9420    0.9102    0.4707    0.7123    0.2905    0.4847    0.5284
      0.2555    0.3072    0.1146    0.0011    0.8009    0.4341    0.4902
      0.4600    0.2232    0.6026    0.0281    0.1249    0.8395    0.7017
      0.3476    0.1753    0.0895    0.4885    0.2150    0.6665    0.6708
   

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
      0.7449    0.0718    0.1280    0.0374
   
   R2 = 
      0.1654    0.3957    0.2428    0.0241    0.5994
   
   R3 = 
      0.7449    0.0718    0.1280    0.0374    0.1654    0.3957    0.2428    0.0241    0.5994
   
   C1 = 
      0.0468
      0.2938
      0.0745
      0.7938
      0.0122
      0.0625
      0.7923
      0.2434
      0.4588
      0.7313
   
   C2 = 
      0.2826
      0.9202
      0.0806
      0.8878
      0.7986
      0.8775
      0.2468
      0.8770
      0.2347
      0.4593
   
   M = 
      0.0468    0.2826
      0.2938    0.9202
      0.0745    0.0806
      0.7938    0.8878
      0.0122    0.7986
      0.0625    0.8775
      0.7923    0.2468
      0.2434    0.8770
      0.4588    0.2347
      0.7313    0.4593
   


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
      0.6263    0.8820    0.7963    0.3771
   
   R2 = 
      0.5105    0.6062    0.6777    0.6675
   
   M = 
      0.6263    0.8820    0.7963    0.3771
      0.5105    0.6062    0.6777    0.6675
   
   C1 = 
      0.2444
      0.6772
      0.2508
      0.2007
      0.1671
      0.5690
      0.6831
      0.3445
      0.7628
      0.8321
   
   C2 = 
      0.1138
      0.0483
   
   C3 = 
      0.2444
      0.6772
      0.2508
      0.2007
      0.1671
      0.5690
      0.6831
      0.3445
      0.7628
      0.8321
      0.1138
      0.0483
   

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
   

