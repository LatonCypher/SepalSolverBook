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
      0.3925    0.0719    0.7740    0.8077    0.2646    0.2134    0.3146
   
   C = 
      0.5146
      0.1755
      0.6703
      0.6098
      0.9658
   
   M = 
      0.8331    0.7427    0.5486    0.9486    0.6335    0.9298    0.0455
      0.5634    0.1386    0.5335    0.2182    0.1198    0.3839    0.8512
      0.5070    0.3108    0.4359    0.0040    0.3062    0.0047    0.0303
      0.7724    0.0117    0.1438    0.8470    0.0739    0.6355    0.5435
      0.5404    0.6354    0.6436    0.7547    0.0280    0.5451    0.4744
      0.8971    0.9099    0.9347    0.0012    0.4100    0.7267    0.6717
      0.4061    0.3817    0.4462    0.2942    0.3867    0.5551    0.2565
      0.4355    0.4336    0.9597    0.2577    0.5341    0.2375    0.2967
   

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
      0.2602    0.6530    0.0966    0.4799
   
   R2 = 
      0.7507    0.5628    0.3062    0.8891    0.1175
   
   R3 = 
      0.2602    0.6530    0.0966    0.4799    0.7507    0.5628    0.3062    0.8891    0.1175
   
   C1 = 
      0.0347
      0.4219
      0.0509
      0.8443
      0.2665
      0.3453
      0.1035
      0.4672
      0.4795
      0.2145
   
   C2 = 
      0.1561
      0.6316
      0.6392
      0.2144
      0.0654
      0.8071
      0.4311
      0.5630
      0.4209
      0.5320
   
   M = 
      0.0347    0.1561
      0.4219    0.6316
      0.0509    0.6392
      0.8443    0.2144
      0.2665    0.0654
      0.3453    0.8071
      0.1035    0.4311
      0.4672    0.5630
      0.4795    0.4209
      0.2145    0.5320
   


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
      0.4475    0.0007    0.5211    0.5427
   
   R2 = 
      0.5280    0.4803    0.5084    0.5478
   
   M = 
      0.4475    0.0007    0.5211    0.5427
      0.5280    0.4803    0.5084    0.5478
   
   C1 = 
      0.7299
      0.2533
      0.4713
      0.7563
      0.3689
      0.5720
      0.5697
      0.0803
      0.5372
      0.8651
   
   C2 = 
      0.5682
      0.6683
   
   C3 = 
      0.7299
      0.2533
      0.4713
      0.7563
      0.3689
      0.5720
      0.5697
      0.0803
      0.5372
      0.8651
      0.5682
      0.6683
   

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
   

