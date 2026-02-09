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
      0.5142    0.8824    0.5374    0.5427    0.3332    0.2049    0.2041
   
   C = 
      0.5427
      0.5421
      0.9543
      0.7713
      0.2173
   
   M = 
      0.7765    0.2822    0.3070    0.0143    0.2861    0.9558    0.2374
      0.0767    0.6650    0.9208    0.1318    0.8613    0.4591    0.9037
      0.6915    0.8848    0.3358    0.5600    0.6264    0.7909    0.4098
      0.8856    0.1818    0.0059    0.2506    0.5600    0.2668    0.9883
      0.3213    0.3929    0.2524    0.2774    0.2498    0.9829    0.8400
      0.3255    0.2785    0.0278    0.7917    0.8475    0.0376    0.6879
      0.3288    0.4154    0.8417    0.8466    0.8745    0.2722    0.0511
      0.4671    0.2314    0.4766    0.6177    0.8128    0.1029    0.9994
   

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
      0.2986    0.9912    0.1688    0.2059
   
   R2 = 
      0.7960    0.3837    0.5786    0.4271    0.6557
   
   R3 = 
      0.2986    0.9912    0.1688    0.2059    0.7960    0.3837    0.5786    0.4271    0.6557
   
   C1 = 
      0.4254
      0.8814
      0.2475
      0.1479
      0.4587
      0.2739
      0.3767
      0.0483
      0.3477
      0.9297
   
   C2 = 
      0.6340
      0.3503
      0.2800
      0.8178
      0.0352
      0.0320
      0.3584
      0.7059
      0.5900
      0.2048
   
   M = 
      0.4254    0.6340
      0.8814    0.3503
      0.2475    0.2800
      0.1479    0.8178
      0.4587    0.0352
      0.2739    0.0320
      0.3767    0.3584
      0.0483    0.7059
      0.3477    0.5900
      0.9297    0.2048
   


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
      0.3860    0.1949    0.0203    0.5535
   
   R2 = 
      0.8282    0.1791    0.7296    0.6703
   
   M = 
      0.3860    0.1949    0.0203    0.5535
      0.8282    0.1791    0.7296    0.6703
   
   C1 = 
      0.1530
      0.1172
      0.1433
      0.4729
      0.3412
      0.4954
      0.5179
      0.9570
      0.0949
      0.1460
   
   C2 = 
      0.0564
      0.3394
   
   C3 = 
      0.1530
      0.1172
      0.1433
      0.4729
      0.3412
      0.4954
      0.5179
      0.9570
      0.0949
      0.1460
      0.0564
      0.3394
   

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
   

