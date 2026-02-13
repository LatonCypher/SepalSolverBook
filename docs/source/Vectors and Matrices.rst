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
      0.7521    0.2439    0.8186    0.1968    0.0316    0.8263    0.2635
   
   C = 
      0.6554
      0.2634
      0.1822
      0.3533
      0.8908
   
   M = 
      0.1030    0.8947    0.1582    0.9284    0.1762    0.5518    0.7125
      0.5580    0.3491    0.6369    0.7034    0.8119    0.4012    0.1269
      0.1136    0.0743    0.8816    0.9555    0.2093    0.1232    0.0349
      0.5334    0.9741    0.0949    0.0284    0.8674    0.8385    0.6528
      0.7340    0.0190    0.2091    0.6165    0.6973    0.0522    0.3977
      0.3144    0.1216    0.5959    0.3236    0.0284    0.6596    0.4502
      0.7536    0.0102    0.6766    0.8391    0.0437    0.0114    0.3756
      0.5449    0.0671    0.8309    0.0635    0.6404    0.5756    0.7429
   

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
      0.7994    0.3297    0.2671    0.1672
   
   R2 = 
      0.7734    0.0107    0.5978    0.6130    0.6222
   
   R3 = 
      0.7994    0.3297    0.2671    0.1672    0.7734    0.0107    0.5978    0.6130    0.6222
   
   C1 = 
      0.3410
      0.7650
      0.8898
      0.0444
      0.4247
      0.7097
      0.3348
      0.9873
      0.1047
      0.4890
   
   C2 = 
      0.8621
      0.2354
      0.7546
      0.7108
      0.0280
      0.2553
      0.5643
      0.5288
      0.2394
      0.7163
   
   M = 
      0.3410    0.8621
      0.7650    0.2354
      0.8898    0.7546
      0.0444    0.7108
      0.4247    0.0280
      0.7097    0.2553
      0.3348    0.5643
      0.9873    0.5288
      0.1047    0.2394
      0.4890    0.7163
   


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
      0.4970    0.7409    0.4920    0.1292
   
   R2 = 
      0.4071    0.2596    0.3505    0.1039
   
   M = 
      0.4970    0.7409    0.4920    0.1292
      0.4071    0.2596    0.3505    0.1039
   
   C1 = 
      0.2967
      0.0825
      0.5517
      0.3916
      0.4824
      0.8489
      0.6201
      0.0586
      0.2866
      0.3847
   
   C2 = 
      0.6629
      0.0801
   
   C3 = 
      0.2967
      0.0825
      0.5517
      0.3916
      0.4824
      0.8489
      0.6201
      0.0586
      0.2866
      0.3847
      0.6629
      0.0801
   

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
   

