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
      0.9028    0.4951    0.1151    0.9559    0.8903    0.5615    0.2300
   
   C = 
      0.5937
      0.9297
      0.7793
      0.7199
      0.0484
   
   M = 
      0.4406    0.8756    0.3183    0.0436    0.8590    0.7365    0.6787
      0.1093    0.0828    0.3973    0.0375    0.8688    0.0549    0.1046
      0.1979    0.7480    0.9289    0.5257    0.7965    0.4696    0.5479
      0.2076    0.8675    0.9089    0.6506    0.3881    0.8727    0.1268
      0.2986    0.1353    0.5831    0.7706    0.5179    0.3167    0.2661
      0.4642    0.4056    0.5862    0.1894    0.7746    0.4122    0.9169
      0.4001    0.0070    0.6646    0.0905    0.8225    0.0678    0.1062
      0.6667    0.5132    0.2937    0.2808    0.1174    0.6109    0.2341
   

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
      0.2000    0.1596    0.9826    0.9225
   
   R2 = 
      0.3629    0.3438    0.4456    0.8816    0.9018
   
   R3 = 
      0.2000    0.1596    0.9826    0.9225    0.3629    0.3438    0.4456    0.8816    0.9018
   
   C1 = 
      0.4987
      0.0986
      0.0513
      0.5284
      0.5749
      0.1578
      0.8633
      0.1447
      0.9234
      0.0816
   
   C2 = 
      0.0043
      0.5390
      0.2234
      0.1855
      0.6611
      0.4261
      0.1441
      0.3107
      0.7868
      0.0970
   
   M = 
      0.4987    0.0043
      0.0986    0.5390
      0.0513    0.2234
      0.5284    0.1855
      0.5749    0.6611
      0.1578    0.4261
      0.8633    0.1441
      0.1447    0.3107
      0.9234    0.7868
      0.0816    0.0970
   


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
      0.1135    0.9072    0.5187    0.6138
   
   R2 = 
      0.2323    0.7126    0.4512    0.9170
   
   M = 
      0.1135    0.9072    0.5187    0.6138
      0.2323    0.7126    0.4512    0.9170
   
   C1 = 
      0.5425
      0.6769
      0.6862
      0.0947
      0.7554
      0.6909
      0.2350
      0.2167
      0.4853
      0.8366
   
   C2 = 
      0.0336
      0.8781
   
   C3 = 
      0.5425
      0.6769
      0.6862
      0.0947
      0.7554
      0.6909
      0.2350
      0.2167
      0.4853
      0.8366
      0.0336
      0.8781
   

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
   

