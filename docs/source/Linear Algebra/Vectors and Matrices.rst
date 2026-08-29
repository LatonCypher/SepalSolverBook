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
      0.9619    0.6967    0.3738    0.2550    0.1765    0.8536    0.3878
   
   C = 
      0.7023
      0.8686
      0.1650
      0.1145
      0.0719
   
   M = 
      0.6033    0.0859    0.5653    0.3960    0.9648    0.3219    0.3398
      0.1123    0.7604    0.3490    0.0861    0.1684    0.9495    0.1808
      0.4838    0.2735    0.6799    0.2101    0.9513    0.2918    0.9086
      0.7740    0.0238    0.7924    0.5906    0.9868    0.6623    0.1183
      0.2530    0.7811    0.4059    0.7683    0.6510    0.8847    0.9330
      0.3776    0.0754    0.6816    0.2112    0.3213    0.8492    0.5324
      0.5402    0.8015    0.9538    0.3172    0.6427    0.1844    0.3035
      0.3316    0.3356    0.7630    0.6140    0.2687    0.1303    0.6374
   

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
      0.1291    0.3179    0.4906    0.1346
   
   R2 = 
      0.6495    0.9098    0.3837    0.0835    0.8864
   
   R3 = 
      0.1291    0.3179    0.4906    0.1346    0.6495    0.9098    0.3837    0.0835    0.8864
   
   C1 = 
      0.5821
      0.2518
      0.0897
      0.3893
      0.3380
      0.2871
      0.1756
      0.5079
      0.3842
      0.0746
   
   C2 = 
      0.3572
      0.4363
      0.4706
      0.7253
      0.5080
      0.4904
      0.1275
      0.4689
      0.4227
      0.1706
   
   M = 
      0.5821    0.3572
      0.2518    0.4363
      0.0897    0.4706
      0.3893    0.7253
      0.3380    0.5080
      0.2871    0.4904
      0.1756    0.1275
      0.5079    0.4689
      0.3842    0.4227
      0.0746    0.1706
   


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
      0.5864    0.0046    0.5161    0.8833
   
   R2 = 
      0.8582    0.6840    0.6538    0.6874
   
   M = 
      0.5864    0.0046    0.5161    0.8833
      0.8582    0.6840    0.6538    0.6874
   
   C1 = 
      0.0673
      0.5507
      0.5467
      0.7431
      0.9380
      0.8458
      0.7156
      0.4690
      0.5197
      0.0941
   
   C2 = 
      0.5889
      0.1545
   
   C3 = 
      0.0673
      0.5507
      0.5467
      0.7431
      0.9380
      0.8458
      0.7156
      0.4690
      0.5197
      0.0941
      0.5889
      0.1545
   

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
   

