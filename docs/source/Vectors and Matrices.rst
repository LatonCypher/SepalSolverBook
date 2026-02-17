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
      0.3174    0.7849    0.8122    0.2040    0.8222    0.9472    0.0823
   
   C = 
      0.9176
      0.4155
      0.6167
      0.0867
      0.1331
   
   M = 
      0.9562    0.2750    0.8168    0.2060    0.2218    0.9576    0.4321
      0.5684    0.4261    0.4678    0.7105    0.0938    0.0568    0.2353
      0.0230    0.8682    0.9064    0.1267    0.8827    0.1552    0.4702
      0.1242    0.5621    0.5938    0.3978    0.9853    0.7562    0.0887
      0.2407    0.3332    0.2534    0.0437    0.8073    0.1713    0.2841
      0.0771    0.2756    0.6412    0.1791    0.7506    0.8827    0.3178
      0.4199    0.4510    0.8330    0.0165    0.8763    0.4467    0.7249
      0.7014    0.1739    0.1966    0.8280    0.0743    0.9105    0.7018
   

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
      0.2888    0.3862    0.3540    0.4934
   
   R2 = 
      0.4971    0.9529    0.9564    0.0258    0.1884
   
   R3 = 
      0.2888    0.3862    0.3540    0.4934    0.4971    0.9529    0.9564    0.0258    0.1884
   
   C1 = 
      0.6108
      0.6581
      0.0411
      0.9476
      0.9761
      0.0101
      0.6866
      0.1427
      0.4633
      0.9377
   
   C2 = 
      0.2294
      0.5260
      0.9572
      0.1665
      0.9631
      0.8166
      0.4288
      0.5004
      0.9769
      0.3795
   
   M = 
      0.6108    0.2294
      0.6581    0.5260
      0.0411    0.9572
      0.9476    0.1665
      0.9761    0.9631
      0.0101    0.8166
      0.6866    0.4288
      0.1427    0.5004
      0.4633    0.9769
      0.9377    0.3795
   


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
      0.0123    0.9188    0.7425    0.5737
   
   R2 = 
      0.7841    0.0271    0.7712    0.0194
   
   M = 
      0.0123    0.9188    0.7425    0.5737
      0.7841    0.0271    0.7712    0.0194
   
   C1 = 
      0.5708
      0.5727
      0.4868
      0.3395
      0.0480
      0.4143
      0.5027
      0.0924
      0.7644
      0.8732
   
   C2 = 
      0.7701
      0.1270
   
   C3 = 
      0.5708
      0.5727
      0.4868
      0.3395
      0.0480
      0.4143
      0.5027
      0.0924
      0.7644
      0.8732
      0.7701
      0.1270
   

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
   

