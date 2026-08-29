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
      0.0467    0.9582    0.1666    0.7356    0.0020    0.7093    0.7884
   
   C = 
      0.4791
      0.6914
      0.8373
      0.3898
      0.3908
   
   M = 
      0.4485    0.2434    0.5359    0.2119    0.1350    0.5896    0.2509
      0.0291    0.9566    0.9446    0.1773    0.2393    0.0157    0.5958
      0.7281    0.7260    0.7150    0.4419    0.2219    0.6826    0.6799
      0.0094    0.4704    0.1856    0.2903    0.7816    0.6336    0.3635
      0.7161    0.6825    0.2280    0.9852    0.5788    0.1617    0.2642
      0.6710    0.2136    0.3197    0.0221    0.3363    0.9072    0.9865
      0.2119    0.7983    0.7941    0.5243    0.4641    0.6634    0.4653
      0.7817    0.0178    0.4532    0.1248    0.5999    0.9783    0.8644
   

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
      0.4120    0.2828    0.5714    0.4588
   
   R2 = 
      0.3442    0.2662    0.0236    0.4419    0.4761
   
   R3 = 
      0.4120    0.2828    0.5714    0.4588    0.3442    0.2662    0.0236    0.4419    0.4761
   
   C1 = 
      0.4739
      0.3833
      0.5537
      0.3959
      0.8297
      0.5596
      0.1167
      0.1744
      0.4402
      0.5095
   
   C2 = 
      0.1934
      0.6669
      0.4969
      0.7744
      0.5361
      0.6103
      0.7625
      0.6394
      0.6255
      0.2008
   
   M = 
      0.4739    0.1934
      0.3833    0.6669
      0.5537    0.4969
      0.3959    0.7744
      0.8297    0.5361
      0.5596    0.6103
      0.1167    0.7625
      0.1744    0.6394
      0.4402    0.6255
      0.5095    0.2008
   


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
      0.8321    0.2719    0.6894    0.2842
   
   R2 = 
      0.0421    0.3028    0.3983    0.0181
   
   M = 
      0.8321    0.2719    0.6894    0.2842
      0.0421    0.3028    0.3983    0.0181
   
   C1 = 
      0.0121
      0.5550
      0.1730
      0.4649
      0.9503
      0.9683
      0.5645
      0.8800
      0.1339
      0.6892
   
   C2 = 
      0.4458
      0.0045
   
   C3 = 
      0.0121
      0.5550
      0.1730
      0.4649
      0.9503
      0.9683
      0.5645
      0.8800
      0.1339
      0.6892
      0.4458
      0.0045
   

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
   

