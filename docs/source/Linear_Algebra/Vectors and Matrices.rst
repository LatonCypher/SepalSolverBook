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
      0.2936    0.3236    0.2247    0.0078    0.1148    0.6581    0.1723
   
   C = 
      0.9747
      0.0948
      0.6577
      0.2485
      0.6956
   
   M = 
      0.2224    0.3683    0.7517    0.4834    0.9781    0.2930    0.6003
      0.3697    0.3661    0.2051    0.5107    0.9285    0.7904    0.3161
      0.8207    0.3358    0.3638    0.2157    0.8151    0.0462    0.0542
      0.5172    0.9280    0.9590    0.1418    0.0059    0.4485    0.8475
      0.6752    0.3034    0.1352    0.1420    0.3895    0.5738    0.3636
      0.5896    0.8050    0.7388    0.7909    0.0247    0.4339    0.9055
      0.6418    0.7114    0.0148    0.9685    0.0366    0.5247    0.9739
      0.9029    0.6575    0.4906    0.2934    0.0744    0.5266    0.1152
   

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
      0.9358    0.6718    0.3231    0.0792
   
   R2 = 
      0.1663    0.6838    0.5164    0.5408    0.4115
   
   R3 = 
      0.9358    0.6718    0.3231    0.0792    0.1663    0.6838    0.5164    0.5408    0.4115
   
   C1 = 
      0.1946
      0.9347
      0.5804
      0.7605
      0.3811
      0.6589
      0.2381
      0.8573
      0.9498
      0.5792
   
   C2 = 
      0.8008
      0.8727
      0.1620
      0.6973
      0.4703
      0.2298
      0.3380
      0.4133
      0.2125
      0.9891
   
   M = 
      0.1946    0.8008
      0.9347    0.8727
      0.5804    0.1620
      0.7605    0.6973
      0.3811    0.4703
      0.6589    0.2298
      0.2381    0.3380
      0.8573    0.4133
      0.9498    0.2125
      0.5792    0.9891
   


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
      0.7448    0.4685    0.2110    0.5573
   
   R2 = 
      0.7358    0.5767    0.2442    0.7125
   
   M = 
      0.7448    0.4685    0.2110    0.5573
      0.7358    0.5767    0.2442    0.7125
   
   C1 = 
      0.2090
      0.6380
      0.8710
      0.4608
      0.5952
      0.8603
      0.9492
      0.5562
      0.4484
      0.3926
   
   C2 = 
      0.4605
      0.6189
   
   C3 = 
      0.2090
      0.6380
      0.8710
      0.4608
      0.5952
      0.8603
      0.9492
      0.5562
      0.4484
      0.3926
      0.4605
      0.6189
   

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
   

