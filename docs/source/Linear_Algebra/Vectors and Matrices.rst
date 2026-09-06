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
      0.8592    0.2967    0.5256    0.3866    0.2841    0.9317    0.0406
   
   C = 
      0.5933
      0.2355
      0.4654
      0.0118
      0.3125
   
   M = 
      0.6549    0.6919    0.8277    0.3687    0.5710    0.1836    0.4141
      0.5295    0.4008    0.2730    0.3772    0.8713    0.4299    0.3070
      0.1186    0.2322    0.8753    0.8399    0.4553    0.8167    0.7453
      0.4879    0.1450    0.9314    0.3548    0.4724    0.2373    0.8013
      0.2176    0.6475    0.8131    0.8187    0.8336    0.4986    0.4119
      0.8382    0.8809    0.4135    0.9413    0.8690    0.4299    0.2595
      0.1553    0.2541    0.8347    0.5617    0.2393    0.0935    0.0452
      0.2502    0.7054    0.8423    0.7478    0.8150    0.8076    0.5465
   

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
      0.3618    0.5651    0.9717    0.4103
   
   R2 = 
      0.9118    0.5280    0.5934    0.5569    0.2448
   
   R3 = 
      0.3618    0.5651    0.9717    0.4103    0.9118    0.5280    0.5934    0.5569    0.2448
   
   C1 = 
      0.1229
      0.4376
      0.5080
      0.8226
      0.4502
      0.9347
      0.3048
      0.0497
      0.9920
      0.7907
   
   C2 = 
      0.7654
      0.0172
      0.7535
      0.4027
      0.4480
      0.9861
      0.8437
      0.3283
      0.7942
      0.9236
   
   M = 
      0.1229    0.7654
      0.4376    0.0172
      0.5080    0.7535
      0.8226    0.4027
      0.4502    0.4480
      0.9347    0.9861
      0.3048    0.8437
      0.0497    0.3283
      0.9920    0.7942
      0.7907    0.9236
   


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
      0.0058    0.2321    0.8268    0.2873
   
   R2 = 
      0.7729    0.8461    0.4115    0.3682
   
   M = 
      0.0058    0.2321    0.8268    0.2873
      0.7729    0.8461    0.4115    0.3682
   
   C1 = 
      0.9431
      0.7025
      0.8307
      0.5588
      0.8145
      0.3516
      0.0280
      0.3499
      0.1963
      0.9069
   
   C2 = 
      0.2285
      0.0788
   
   C3 = 
      0.9431
      0.7025
      0.8307
      0.5588
      0.8145
      0.3516
      0.0280
      0.3499
      0.1963
      0.9069
      0.2285
      0.0788
   

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
   

