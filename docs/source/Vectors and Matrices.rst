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
      0.5826    0.6403    0.2375    0.6152    0.6444    0.3038    0.2489
   
   C = 
      0.0354
      0.7331
      0.3644
      0.8302
      0.8577
   
   M = 
      0.0400    0.2117    0.3919    0.6588    0.7065    0.8366    0.2906
      0.4930    0.3486    0.5777    0.0172    0.7605    0.9961    0.6935
      0.8825    0.9478    0.5467    0.2753    0.9335    0.0898    0.0951
      0.1565    0.2528    0.0568    0.9654    0.5770    0.5595    0.9143
      0.4809    0.4125    0.9989    0.7277    0.3344    0.7045    0.7511
      0.7630    0.9473    0.1845    0.2554    0.2795    0.6390    0.8760
      0.0262    0.7311    0.1000    0.0328    0.5733    0.9434    0.0093
      0.3759    0.6999    0.2523    0.7307    0.8138    0.7693    0.5683
   

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
      0.1011    0.8959    0.5077    0.5496
   
   R2 = 
      0.8399    0.9972    0.0907    0.8249    0.5042
   
   R3 = 
      0.1011    0.8959    0.5077    0.5496    0.8399    0.9972    0.0907    0.8249    0.5042
   
   C1 = 
      0.7186
      0.5653
      0.0283
      0.9201
      0.3069
      0.4202
      0.6127
      0.1823
      0.0081
      0.4477
   
   C2 = 
      0.1017
      0.4201
      0.2107
      0.8413
      0.2730
      0.6268
      0.0921
      0.0559
      0.2338
      0.4755
   
   M = 
      0.7186    0.1017
      0.5653    0.4201
      0.0283    0.2107
      0.9201    0.8413
      0.3069    0.2730
      0.4202    0.6268
      0.6127    0.0921
      0.1823    0.0559
      0.0081    0.2338
      0.4477    0.4755
   


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
      0.5962    0.4118    0.2431    0.2335
   
   R2 = 
      0.9747    0.3585    0.9982    0.1107
   
   M = 
      0.5962    0.4118    0.2431    0.2335
      0.9747    0.3585    0.9982    0.1107
   
   C1 = 
      0.5759
      0.1590
      0.4174
      0.5784
      0.5442
      0.6256
      0.9502
      0.1115
      0.9689
      0.5014
   
   C2 = 
      0.3044
      0.7028
   
   C3 = 
      0.5759
      0.1590
      0.4174
      0.5784
      0.5442
      0.6256
      0.9502
      0.1115
      0.9689
      0.5014
      0.3044
      0.7028
   

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
   

