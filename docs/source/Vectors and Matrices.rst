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
      0.4634    0.3427    0.5816    0.4862    0.3415    0.1068    0.8805
   
   C = 
      0.2357
      0.3232
      0.6342
      0.4310
      0.4762
   
   M = 
      0.7184    0.1469    0.5037    0.4067    0.6771    0.0084    0.4751
      0.4284    0.6736    0.8395    0.5953    0.0733    0.4631    0.6348
      0.7094    0.9171    0.7474    0.0876    0.2407    0.0121    0.4539
      0.4813    0.6714    0.5437    0.5738    0.8389    0.6358    0.2067
      0.9008    0.8174    0.0035    0.4929    0.2791    0.4938    0.1719
      0.1127    0.6209    0.6264    0.5463    0.0187    0.6728    0.1118
      0.7131    0.2520    0.5635    0.1467    0.9945    0.9047    0.6900
      0.3379    0.9882    0.8732    0.3491    0.9103    0.4888    0.6280
   

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
      0.7435    0.6630    0.4194    0.9631
   
   R2 = 
      0.7281    0.6443    0.6470    0.9076    0.1949
   
   R3 = 
      0.7435    0.6630    0.4194    0.9631    0.7281    0.6443    0.6470    0.9076    0.1949
   
   C1 = 
      0.2449
      0.0406
      0.1859
      0.8533
      0.5014
      0.7368
      0.7825
      0.8981
      0.1609
      0.6177
   
   C2 = 
      0.0751
      0.7244
      0.1912
      0.5022
      0.6142
      0.6586
      0.8996
      0.9497
      0.1922
      0.6068
   
   M = 
      0.2449    0.0751
      0.0406    0.7244
      0.1859    0.1912
      0.8533    0.5022
      0.5014    0.6142
      0.7368    0.6586
      0.7825    0.8996
      0.8981    0.9497
      0.1609    0.1922
      0.6177    0.6068
   


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
      0.8783    0.5995    0.0065    0.0761
   
   R2 = 
      0.7737    0.5578    0.2803    0.2247
   
   M = 
      0.8783    0.5995    0.0065    0.0761
      0.7737    0.5578    0.2803    0.2247
   
   C1 = 
      0.0087
      0.4043
      0.7866
      0.9119
      0.8568
      0.9339
      0.5618
      0.5857
      0.0278
      0.8679
   
   C2 = 
      0.6597
      0.8340
   
   C3 = 
      0.0087
      0.4043
      0.7866
      0.9119
      0.8568
      0.9339
      0.5618
      0.5857
      0.0278
      0.8679
      0.6597
      0.8340
   

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
   

