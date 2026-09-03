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
      0.5455    0.5796    0.5908    0.3759    0.1055    0.1128    0.7676
   
   C = 
      0.8352
      0.1650
      0.1350
      0.3453
      0.2715
   
   M = 
      0.8938    0.3529    0.8208    0.0006    0.6320    0.5386    0.0901
      0.5756    0.9837    0.3469    0.8743    0.7339    0.4208    0.3674
      0.4501    0.9024    0.7345    0.4463    0.5421    0.0827    0.2755
      0.2884    0.1914    0.8931    0.3010    0.7843    0.9261    0.2271
      0.3333    0.6811    0.8081    0.3440    0.2961    0.6430    0.1076
      0.3953    0.5820    0.5246    0.5419    0.4386    0.7954    0.8413
      0.8927    0.9648    0.2172    0.1916    0.1880    0.4169    0.9169
      0.6259    0.9221    0.2027    0.1345    0.1511    0.7874    0.2879
   

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
      0.2314    0.6323    0.2783    0.0317
   
   R2 = 
      0.5816    0.2664    0.3612    0.9116    0.8971
   
   R3 = 
      0.2314    0.6323    0.2783    0.0317    0.5816    0.2664    0.3612    0.9116    0.8971
   
   C1 = 
      0.5933
      0.9312
      0.4104
      0.3761
      0.7085
      0.5024
      0.5620
      0.0557
      0.9838
      0.1519
   
   C2 = 
      0.6632
      0.6365
      0.3812
      0.5505
      0.4385
      0.2735
      0.1826
      0.7765
      0.3927
      0.7594
   
   M = 
      0.5933    0.6632
      0.9312    0.6365
      0.4104    0.3812
      0.3761    0.5505
      0.7085    0.4385
      0.5024    0.2735
      0.5620    0.1826
      0.0557    0.7765
      0.9838    0.3927
      0.1519    0.7594
   


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
      0.4470    0.3136    0.1990    0.3349
   
   R2 = 
      0.7536    0.6084    0.8298    0.7685
   
   M = 
      0.4470    0.3136    0.1990    0.3349
      0.7536    0.6084    0.8298    0.7685
   
   C1 = 
      0.4888
      0.4147
      0.2999
      0.7725
      0.5158
      0.9774
      0.1042
      0.7240
      0.3993
      0.5083
   
   C2 = 
      0.1633
      0.5868
   
   C3 = 
      0.4888
      0.4147
      0.2999
      0.7725
      0.5158
      0.9774
      0.1042
      0.7240
      0.3993
      0.5083
      0.1633
      0.5868
   

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
   

