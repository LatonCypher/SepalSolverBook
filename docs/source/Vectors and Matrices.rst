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
      0.7426    0.6072    0.0096    0.5634    0.1256    0.2412    0.8868
   
   C = 
      0.1612
      0.7007
      0.1999
      0.9903
      0.7959
   
   M = 
      0.5026    0.0947    0.8927    0.0146    0.5724    0.0570    0.3961
      0.6043    0.6573    0.1408    0.0679    0.5130    0.5872    0.0305
      0.3585    0.9119    0.2031    0.3388    0.9034    0.2265    0.1496
      0.5785    0.8533    0.9973    0.2182    0.5389    0.9147    0.7751
      0.2621    0.0885    0.4994    0.3543    0.5595    0.4012    0.5933
      0.6182    0.2082    0.5547    0.8852    0.6390    0.3988    0.1873
      0.1177    0.6532    0.3253    0.4521    0.4004    0.9023    0.9488
      0.0043    0.4777    0.9022    0.6722    0.3805    0.9777    0.9843
   

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
      0.5083    0.1993    0.8734    0.3743
   
   R2 = 
      0.0432    0.3144    0.5732    0.7411    0.7638
   
   R3 = 
      0.5083    0.1993    0.8734    0.3743    0.0432    0.3144    0.5732    0.7411    0.7638
   
   C1 = 
      0.5782
      0.4992
      0.9778
      0.2570
      0.6823
      0.9282
      0.4803
      0.5697
      0.7331
      0.3855
   
   C2 = 
      0.7084
      0.5352
      0.5623
      0.9350
      0.5661
      0.1956
      0.6975
      0.0359
      0.2233
      0.9320
   
   M = 
      0.5782    0.7084
      0.4992    0.5352
      0.9778    0.5623
      0.2570    0.9350
      0.6823    0.5661
      0.9282    0.1956
      0.4803    0.6975
      0.5697    0.0359
      0.7331    0.2233
      0.3855    0.9320
   


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
      0.5087    0.3453    0.0500    0.0384
   
   R2 = 
      0.7261    0.4425    0.0746    0.1714
   
   M = 
      0.5087    0.3453    0.0500    0.0384
      0.7261    0.4425    0.0746    0.1714
   
   C1 = 
      0.9447
      0.1439
      0.6053
      0.1936
      0.6240
      0.4386
      0.7685
      0.0856
      0.6905
      0.5535
   
   C2 = 
      0.7973
      0.9203
   
   C3 = 
      0.9447
      0.1439
      0.6053
      0.1936
      0.6240
      0.4386
      0.7685
      0.0856
      0.6905
      0.5535
      0.7973
      0.9203
   

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
   

