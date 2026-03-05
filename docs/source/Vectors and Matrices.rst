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
      0.6853    0.6680    0.2321    0.8545    0.7544    0.4681    0.0542
   
   C = 
      0.2233
      0.5931
      0.8367
      0.3416
      0.3634
   
   M = 
      0.7985    0.9199    0.5446    0.1851    0.7181    0.4777    0.2910
      0.6022    0.7644    0.7266    0.6911    0.8523    0.5774    0.4894
      0.5582    0.9848    0.1608    0.1636    0.4602    0.9242    0.7530
      0.0638    0.4885    0.1537    0.3888    0.1062    0.8022    0.7425
      0.2073    0.6759    0.0563    0.3289    0.6380    0.6362    0.6399
      0.7085    0.4403    0.9360    0.2855    0.7799    0.6795    0.9245
      0.5042    0.8920    0.3417    0.1799    0.6066    0.5808    0.6901
      0.2503    0.1658    0.2927    0.6800    0.8031    0.7705    0.4360
   

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
      0.2669    0.6962    0.4311    0.1615
   
   R2 = 
      0.7521    0.3375    0.3278    0.9532    0.2332
   
   R3 = 
      0.2669    0.6962    0.4311    0.1615    0.7521    0.3375    0.3278    0.9532    0.2332
   
   C1 = 
      0.8371
      0.2355
      0.0568
      0.6848
      0.7279
      0.5721
      0.6806
      0.4579
      0.9143
      0.3871
   
   C2 = 
      0.9086
      0.1350
      0.6373
      0.6623
      0.3646
      0.4722
      0.5598
      0.3754
      0.3519
      0.3684
   
   M = 
      0.8371    0.9086
      0.2355    0.1350
      0.0568    0.6373
      0.6848    0.6623
      0.7279    0.3646
      0.5721    0.4722
      0.6806    0.5598
      0.4579    0.3754
      0.9143    0.3519
      0.3871    0.3684
   


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
      0.1304    0.0668    0.1435    0.1390
   
   R2 = 
      0.3102    0.8443    0.2138    0.9870
   
   M = 
      0.1304    0.0668    0.1435    0.1390
      0.3102    0.8443    0.2138    0.9870
   
   C1 = 
      0.9545
      0.6255
      0.0819
      0.6086
      0.1514
      0.6726
      0.0704
      0.3148
      0.8080
      0.3690
   
   C2 = 
      0.3384
      0.7651
   
   C3 = 
      0.9545
      0.6255
      0.0819
      0.6086
      0.1514
      0.6726
      0.0704
      0.3148
      0.8080
      0.3690
      0.3384
      0.7651
   

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
   

