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
      0.1360    0.9199    0.0496    0.4105    0.1832    0.9738    0.0065
   
   C = 
      0.9756
      0.7090
      0.5775
      0.6195
      0.5603
   
   M = 
      0.6577    0.2817    0.5097    0.7194    0.1418    0.0020    0.9316
      0.0048    0.2924    0.9118    0.0633    0.5615    0.8694    0.0684
      0.2730    0.4359    0.9028    0.7265    0.6199    0.6923    0.7854
      0.8335    0.6382    0.3985    0.5555    0.2919    0.9671    0.3642
      0.3661    0.7295    0.3708    0.5610    0.7459    0.5067    0.2673
      0.6627    0.7843    0.3392    0.4908    0.4280    0.0218    0.3508
      0.8467    0.5970    0.6760    0.6490    0.7252    0.5114    0.8502
      0.8810    0.4673    0.4436    0.9473    0.1174    0.1092    0.3123
   

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
      0.4627    0.5442    0.4001    0.9138
   
   R2 = 
      0.0743    0.8901    0.5092    0.9953    0.1863
   
   R3 = 
      0.4627    0.5442    0.4001    0.9138    0.0743    0.8901    0.5092    0.9953    0.1863
   
   C1 = 
      0.6749
      0.7304
      0.0342
      0.1977
      0.3249
      0.0366
      0.9820
      0.0976
      0.4561
      0.2766
   
   C2 = 
      0.3452
      0.0506
      0.9371
      0.4316
      0.6341
      0.9860
      0.1140
      0.6148
      0.9411
      0.8150
   
   M = 
      0.6749    0.3452
      0.7304    0.0506
      0.0342    0.9371
      0.1977    0.4316
      0.3249    0.6341
      0.0366    0.9860
      0.9820    0.1140
      0.0976    0.6148
      0.4561    0.9411
      0.2766    0.8150
   


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
      0.4931    0.5929    0.4984    0.6813
   
   R2 = 
      0.4894    0.0663    0.2308    0.1806
   
   M = 
      0.4931    0.5929    0.4984    0.6813
      0.4894    0.0663    0.2308    0.1806
   
   C1 = 
      0.9536
      0.9306
      0.3067
      0.4676
      0.6003
      0.7351
      0.2036
      0.0277
      0.2367
      0.4064
   
   C2 = 
      0.6599
      0.9170
   
   C3 = 
      0.9536
      0.9306
      0.3067
      0.4676
      0.6003
      0.7351
      0.2036
      0.0277
      0.2367
      0.4064
      0.6599
      0.9170
   

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
   

