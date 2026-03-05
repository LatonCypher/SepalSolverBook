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
      0.0278    0.0462    0.0261    0.7844    0.5998    0.5766    0.3545
   
   C = 
      0.8294
      0.6482
      0.0372
      0.9504
      0.4720
   
   M = 
      0.1451    0.7576    0.3916    0.5155    0.3970    0.5067    0.4070
      0.4701    0.4828    0.3880    0.1558    0.2898    0.7251    0.3530
      0.7897    0.5442    0.7377    0.6637    0.2961    0.6511    0.9522
      0.2219    0.9517    0.7287    0.7801    0.2996    0.8767    0.9824
      0.9156    0.9706    0.4369    0.5636    0.4520    0.7561    0.3156
      0.7003    0.6616    0.2429    0.5383    0.5107    0.0778    0.0008
      0.7383    0.7087    0.9823    0.0588    0.6262    0.5222    0.2886
      0.5668    0.5365    0.3996    0.4807    0.4189    0.2490    0.7868
   

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
      0.8731    0.1217    0.8001    0.2763
   
   R2 = 
      0.7287    0.5209    0.7876    0.6549    0.4236
   
   R3 = 
      0.8731    0.1217    0.8001    0.2763    0.7287    0.5209    0.7876    0.6549    0.4236
   
   C1 = 
      0.3031
      0.2770
      0.2719
      0.2800
      0.8417
      0.4891
      0.5475
      0.5193
      0.2647
      0.1422
   
   C2 = 
      0.1721
      0.2456
      0.8990
      0.9662
      0.5001
      0.6345
      0.2639
      0.8584
      0.3378
      0.1533
   
   M = 
      0.3031    0.1721
      0.2770    0.2456
      0.2719    0.8990
      0.2800    0.9662
      0.8417    0.5001
      0.4891    0.6345
      0.5475    0.2639
      0.5193    0.8584
      0.2647    0.3378
      0.1422    0.1533
   


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
      0.0641    0.2078    0.6394    0.4738
   
   R2 = 
      0.5484    0.0153    0.8205    0.7532
   
   M = 
      0.0641    0.2078    0.6394    0.4738
      0.5484    0.0153    0.8205    0.7532
   
   C1 = 
      0.7939
      0.1850
      0.7245
      0.7056
      0.9924
      0.3201
      0.6971
      0.3051
      0.8258
      0.8025
   
   C2 = 
      0.2307
      0.6399
   
   C3 = 
      0.7939
      0.1850
      0.7245
      0.7056
      0.9924
      0.3201
      0.6971
      0.3051
      0.8258
      0.8025
      0.2307
      0.6399
   

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
   

