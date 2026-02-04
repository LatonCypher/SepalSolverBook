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
      0.7743    0.5771    0.2543    0.3704    0.2507    0.2558    0.0737
   
   C = 
      0.2332
      0.0549
      0.9950
      0.9135
      0.3927
   
   M = 
      0.0728    0.3843    0.2298    0.5686    0.5683    0.2161    0.3312
      0.0882    0.2486    0.5104    0.3305    0.6113    0.3621    0.9369
      0.5735    0.6404    0.0200    0.9109    0.6266    0.3761    0.2132
      0.6959    0.6243    0.1826    0.0601    0.3765    0.3340    0.3805
      0.2980    0.3234    0.4247    0.5014    0.9706    0.3349    0.5888
      0.3001    0.2463    0.7241    0.2821    0.9748    0.4458    0.0962
      0.9852    0.4768    0.4162    0.9280    0.4320    0.9844    0.4777
      0.2864    0.2330    0.8272    0.4110    0.3550    0.3200    0.6007
   

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
      0.6093    0.2727    0.2968    0.8102
   
   R2 = 
      0.0277    0.1016    0.1964    0.0509    0.1340
   
   R3 = 
      0.6093    0.2727    0.2968    0.8102    0.0277    0.1016    0.1964    0.0509    0.1340
   
   C1 = 
      0.3987
      0.6011
      0.5472
      0.0587
      0.0563
      0.1067
      0.5604
      0.6365
      0.0699
      0.0447
   
   C2 = 
      0.2079
      0.3810
      0.3621
      0.7971
      0.0633
      0.3910
      0.2439
      0.4825
      0.0921
      0.0920
   
   M = 
      0.3987    0.2079
      0.6011    0.3810
      0.5472    0.3621
      0.0587    0.7971
      0.0563    0.0633
      0.1067    0.3910
      0.5604    0.2439
      0.6365    0.4825
      0.0699    0.0921
      0.0447    0.0920
   


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
      0.8236    0.7317    0.0827    0.8011
   
   R2 = 
      0.8765    0.6682    0.6355    0.4933
   
   M = 
      0.8236    0.7317    0.0827    0.8011
      0.8765    0.6682    0.6355    0.4933
   
   C1 = 
      0.6697
      0.9141
      0.0574
      0.7746
      0.9222
      0.2900
      0.9824
      0.8079
      0.8417
      0.7489
   
   C2 = 
      0.3584
      0.6814
   
   C3 = 
      0.6697
      0.9141
      0.0574
      0.7746
      0.9222
      0.2900
      0.9824
      0.8079
      0.8417
      0.7489
      0.3584
      0.6814
   

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
   

