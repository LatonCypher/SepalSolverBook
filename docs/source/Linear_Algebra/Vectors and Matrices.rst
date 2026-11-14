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
      0.3517    0.0883    0.0008    0.5460    0.6662    0.6234    0.0033
   
   C = 
      0.2227
      0.4259
      0.4696
      0.3821
      0.1507
   
   M = 
      0.3870    0.3796    0.4416    0.3044    0.6525    0.3560    0.8545
      0.5026    0.7390    0.7402    0.8025    0.5076    0.2942    0.2750
      0.3857    0.2112    0.2289    0.3605    0.1467    0.6396    0.6819
      0.6356    0.2563    0.0141    0.0200    0.7936    0.8960    0.2689
      0.0251    0.9851    0.7645    0.1346    0.4414    0.7405    0.9153
      0.0884    0.0083    0.4223    0.3948    0.0223    0.2451    0.8858
      0.4277    0.2169    0.9622    0.1309    0.9638    0.9760    0.5260
      0.5194    0.3317    0.5320    0.9129    0.9252    0.0843    0.2406
   

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
      0.2969    0.7248    0.4069    0.3483
   
   R2 = 
      0.0191    0.1120    0.9190    0.6668    0.1610
   
   R3 = 
      0.2969    0.7248    0.4069    0.3483    0.0191    0.1120    0.9190    0.6668    0.1610
   
   C1 = 
      0.4592
      0.2624
      0.4063
      0.1043
      0.7386
      0.4235
      0.5045
      0.1400
      0.1581
      0.1067
   
   C2 = 
      0.9804
      0.6136
      0.8004
      0.2644
      0.3631
      0.9058
      0.2633
      0.4817
      0.3154
      0.8280
   
   M = 
      0.4592    0.9804
      0.2624    0.6136
      0.4063    0.8004
      0.1043    0.2644
      0.7386    0.3631
      0.4235    0.9058
      0.5045    0.2633
      0.1400    0.4817
      0.1581    0.3154
      0.1067    0.8280
   


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
      0.2820    0.2670    0.2423    0.2799
   
   R2 = 
      0.4155    0.7233    0.0239    0.1620
   
   M = 
      0.2820    0.2670    0.2423    0.2799
      0.4155    0.7233    0.0239    0.1620
   
   C1 = 
      0.2389
      0.4396
      0.7089
      0.3473
      0.0299
      0.7083
      0.8938
      0.8927
      0.5934
      0.6782
   
   C2 = 
      0.5440
      0.4377
   
   C3 = 
      0.2389
      0.4396
      0.7089
      0.3473
      0.0299
      0.7083
      0.8938
      0.8927
      0.5934
      0.6782
      0.5440
      0.4377
   

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
   

