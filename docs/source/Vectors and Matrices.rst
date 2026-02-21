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
      0.6608    0.8185    0.0567    0.5145    0.2179    0.8449    0.5768
   
   C = 
      0.5616
      0.7927
      0.9219
      0.2515
      0.4529
   
   M = 
      0.2843    0.9596    0.3348    0.8211    0.5029    0.2668    0.3521
      0.3326    0.4158    0.6105    0.2120    0.6522    0.6446    0.8750
      0.4637    0.6594    0.8830    0.3490    0.9483    0.8960    0.4490
      0.7955    0.6187    0.1095    0.9309    0.5660    0.4364    0.3833
      0.0983    0.5598    0.9010    0.5471    0.8054    0.7860    0.1359
      0.2889    0.6183    0.4181    0.8406    0.5879    0.8322    0.7907
      0.9047    0.7985    0.6853    0.1834    0.5238    0.0080    0.0469
      0.3624    0.5584    0.8851    0.8473    0.6915    0.0569    0.4520
   

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
      0.7558    0.8688    0.5765    0.3914
   
   R2 = 
      0.9253    0.7918    0.5361    0.1060    0.2908
   
   R3 = 
      0.7558    0.8688    0.5765    0.3914    0.9253    0.7918    0.5361    0.1060    0.2908
   
   C1 = 
      0.7388
      0.4940
      0.7387
      0.9207
      0.0698
      0.0659
      0.6597
      0.6634
      0.1251
      0.5305
   
   C2 = 
      0.5322
      0.3672
      0.3303
      0.7946
      0.2183
      0.9678
      0.7341
      0.6235
      0.0458
      0.3118
   
   M = 
      0.7388    0.5322
      0.4940    0.3672
      0.7387    0.3303
      0.9207    0.7946
      0.0698    0.2183
      0.0659    0.9678
      0.6597    0.7341
      0.6634    0.6235
      0.1251    0.0458
      0.5305    0.3118
   


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
      0.8797    0.3080    0.4190    0.6517
   
   R2 = 
      0.7228    0.4890    0.4115    0.8595
   
   M = 
      0.8797    0.3080    0.4190    0.6517
      0.7228    0.4890    0.4115    0.8595
   
   C1 = 
      0.5305
      0.0189
      0.9461
      0.0268
      0.9030
      0.3726
      0.9239
      0.9613
      0.2078
      0.0846
   
   C2 = 
      0.7211
      0.1326
   
   C3 = 
      0.5305
      0.0189
      0.9461
      0.0268
      0.9030
      0.3726
      0.9239
      0.9613
      0.2078
      0.0846
      0.7211
      0.1326
   

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
   

