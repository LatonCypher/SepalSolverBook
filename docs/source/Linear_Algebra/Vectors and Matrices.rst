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
      0.0380    0.1416    0.2651    0.7565    0.2167    0.3526    0.6324
   
   C = 
      0.6599
      0.3535
      0.7342
      0.8369
      0.4236
   
   M = 
      0.6766    0.4195    0.3107    0.0737    0.0954    0.2859    0.9554
      0.6321    0.6025    0.7351    0.6651    0.3116    0.6906    0.7380
      0.4671    0.6736    0.4286    0.4685    0.1884    0.9165    0.9908
      0.1172    0.1590    0.0108    0.4341    0.3389    0.4331    0.5274
      0.6359    0.1741    0.7839    0.8567    0.2936    0.6673    0.0329
      0.9370    0.7954    0.2946    0.4210    0.6713    0.5512    0.7229
      0.8627    0.1410    0.2845    0.0914    0.8971    0.5336    0.6434
      0.0292    0.9271    0.0342    0.7296    0.3650    0.5064    0.1121
   

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
      0.1833    0.2947    0.2121    0.8312
   
   R2 = 
      0.8825    0.9372    0.6388    0.5582    0.1712
   
   R3 = 
      0.1833    0.2947    0.2121    0.8312    0.8825    0.9372    0.6388    0.5582    0.1712
   
   C1 = 
      0.8718
      0.6585
      0.0480
      0.5929
      0.9575
      0.0303
      0.7080
      0.6527
      0.9024
      0.0923
   
   C2 = 
      0.1274
      0.6944
      0.6841
      0.6489
      0.1025
      0.0722
      0.6648
      0.1906
      0.6452
      0.7823
   
   M = 
      0.8718    0.1274
      0.6585    0.6944
      0.0480    0.6841
      0.5929    0.6489
      0.9575    0.1025
      0.0303    0.0722
      0.7080    0.6648
      0.6527    0.1906
      0.9024    0.6452
      0.0923    0.7823
   


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
      0.3185    0.9374    0.7695    0.2652
   
   R2 = 
      0.6380    0.0561    0.3780    0.0655
   
   M = 
      0.3185    0.9374    0.7695    0.2652
      0.6380    0.0561    0.3780    0.0655
   
   C1 = 
      0.8415
      0.4781
      0.1516
      0.6224
      0.8636
      0.7409
      0.6047
      0.5706
      0.9937
      0.6085
   
   C2 = 
      0.0591
      0.2946
   
   C3 = 
      0.8415
      0.4781
      0.1516
      0.6224
      0.8636
      0.7409
      0.6047
      0.5706
      0.9937
      0.6085
      0.0591
      0.2946
   

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
   

