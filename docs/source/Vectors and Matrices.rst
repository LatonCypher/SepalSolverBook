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
      0.5439    0.0382    0.2943    0.7320    0.8032    0.2924    0.5340
   
   C = 
      0.3787
      0.6595
      0.0089
      0.7448
      0.0934
   
   M = 
      0.1745    0.5373    0.7856    0.8538    0.6226    0.5815    0.0047
      0.6056    0.1022    0.1069    0.4635    0.0536    0.4604    0.2530
      0.8240    0.6180    0.4828    0.1462    0.6907    0.5861    0.5604
      0.0897    0.1694    0.6625    0.9127    0.6094    0.3992    0.5190
      0.8624    0.8269    0.5778    0.6343    0.4677    0.1922    0.7748
      0.5256    0.0715    0.5772    0.2577    0.7375    0.6046    0.7175
      0.0343    0.7215    0.5799    0.3669    0.8630    0.5735    0.8521
      0.4396    0.2046    0.9893    0.2151    0.4243    0.1639    0.6479
   

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
      0.7380    0.8027    0.3595    0.6343
   
   R2 = 
      0.7219    0.5503    0.4855    0.6491    0.6475
   
   R3 = 
      0.7380    0.8027    0.3595    0.6343    0.7219    0.5503    0.4855    0.6491    0.6475
   
   C1 = 
      0.6980
      0.2893
      0.8222
      0.3329
      0.9199
      0.4039
      0.1686
      0.4205
      0.1248
      0.5370
   
   C2 = 
      0.5718
      0.1691
      0.5789
      0.4566
      0.1199
      0.4898
      0.8484
      0.8316
      0.3571
      0.0589
   
   M = 
      0.6980    0.5718
      0.2893    0.1691
      0.8222    0.5789
      0.3329    0.4566
      0.9199    0.1199
      0.4039    0.4898
      0.1686    0.8484
      0.4205    0.8316
      0.1248    0.3571
      0.5370    0.0589
   


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
      0.7750    0.0173    0.6164    0.7892
   
   R2 = 
      0.5004    0.4471    0.7481    0.6395
   
   M = 
      0.7750    0.0173    0.6164    0.7892
      0.5004    0.4471    0.7481    0.6395
   
   C1 = 
      0.4538
      0.0787
      0.2999
      0.7448
      0.4956
      0.3383
      0.1928
      0.8060
      0.1019
      0.3223
   
   C2 = 
      0.5561
      0.1097
   
   C3 = 
      0.4538
      0.0787
      0.2999
      0.7448
      0.4956
      0.3383
      0.1928
      0.8060
      0.1019
      0.3223
      0.5561
      0.1097
   

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
   

