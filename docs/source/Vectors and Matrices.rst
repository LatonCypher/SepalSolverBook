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
      0.7780    0.0114    0.5604    0.8419    0.7854    0.4417    0.7729
   
   C = 
      0.4902
      0.3395
      0.5393
      0.1420
      0.4272
   
   M = 
      0.8581    0.7282    0.4525    0.6939    0.2472    0.6708    0.9205
      0.2799    0.4412    0.7264    0.7998    0.4343    0.6019    0.4821
      0.4561    0.4240    0.1935    0.0111    0.4915    0.2003    0.6862
      0.0803    0.5578    0.0082    0.3495    0.4827    0.7863    0.0611
      0.4855    0.0821    0.3272    0.7736    0.0087    0.8305    0.3650
      0.5278    0.8850    0.8158    0.5803    0.6699    0.8140    0.0789
      0.3700    0.9046    0.5927    0.4371    0.1264    0.4182    0.1220
      0.9946    0.7204    0.3066    0.9677    0.3533    0.3691    0.0629
   

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
      0.4271    0.8259    0.6258    0.3556
   
   R2 = 
      0.1171    0.6044    0.0794    0.5156    0.0678
   
   R3 = 
      0.4271    0.8259    0.6258    0.3556    0.1171    0.6044    0.0794    0.5156    0.0678
   
   C1 = 
      0.9512
      0.1091
      0.7760
      0.7879
      0.8143
      0.0793
      0.3541
      0.7933
      0.6596
      0.2314
   
   C2 = 
      0.7114
      0.0334
      0.8776
      0.7824
      0.9245
      0.7906
      0.0518
      0.9672
      0.2623
      0.0902
   
   M = 
      0.9512    0.7114
      0.1091    0.0334
      0.7760    0.8776
      0.7879    0.7824
      0.8143    0.9245
      0.0793    0.7906
      0.3541    0.0518
      0.7933    0.9672
      0.6596    0.2623
      0.2314    0.0902
   


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
      0.1405    0.0221    0.3862    0.7350
   
   R2 = 
      0.7887    0.1651    0.0514    0.9462
   
   M = 
      0.1405    0.0221    0.3862    0.7350
      0.7887    0.1651    0.0514    0.9462
   
   C1 = 
      0.4879
      0.4350
      0.1687
      0.3762
      0.6745
      0.0016
      0.8916
      0.0218
      0.1163
      0.4639
   
   C2 = 
      0.5453
      0.3167
   
   C3 = 
      0.4879
      0.4350
      0.1687
      0.3762
      0.6745
      0.0016
      0.8916
      0.0218
      0.1163
      0.4639
      0.5453
      0.3167
   

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
   

