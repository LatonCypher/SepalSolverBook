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
      0.3354    0.2113    0.4664    0.2903    0.2227    0.4742    0.2116
   
   C = 
      0.8450
      0.7118
      0.9577
      0.8223
      0.5308
   
   M = 
      0.7182    0.6566    0.9831    0.0116    0.7235    0.8129    0.0162
      0.4355    0.7856    0.4614    0.2846    0.6245    0.4845    0.6011
      0.6666    0.2187    0.3334    0.8783    0.9836    0.7674    0.6647
      0.4225    0.0582    0.6467    0.4517    0.7253    0.4979    0.8858
      0.4167    0.0551    0.1653    0.9656    0.9313    0.0012    0.3159
      0.7473    0.5289    0.7003    0.1141    0.7281    0.6668    0.0655
      0.6116    0.2337    0.2537    0.5653    0.5331    0.8994    0.5473
      0.9089    0.7932    0.0972    0.5164    0.9012    0.9588    0.4712
   

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
      0.9721    0.9171    0.2869    0.5929
   
   R2 = 
      0.4804    0.6599    0.1032    0.5995    0.8339
   
   R3 = 
      0.9721    0.9171    0.2869    0.5929    0.4804    0.6599    0.1032    0.5995    0.8339
   
   C1 = 
      0.0655
      0.7411
      0.2225
      0.5942
      0.4405
      0.3421
      0.7373
      0.8867
      0.1519
      0.5100
   
   C2 = 
      0.8643
      0.1728
      0.9410
      0.8679
      0.4454
      0.1249
      0.1525
      0.2116
      0.7742
      0.2468
   
   M = 
      0.0655    0.8643
      0.7411    0.1728
      0.2225    0.9410
      0.5942    0.8679
      0.4405    0.4454
      0.3421    0.1249
      0.7373    0.1525
      0.8867    0.2116
      0.1519    0.7742
      0.5100    0.2468
   


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
      0.4650    0.5795    0.1073    0.6334
   
   R2 = 
      0.8410    0.2758    0.1927    0.2048
   
   M = 
      0.4650    0.5795    0.1073    0.6334
      0.8410    0.2758    0.1927    0.2048
   
   C1 = 
      0.6566
      0.2032
      0.7469
      0.9711
      0.3519
      0.6197
      0.4591
      0.2780
      0.2815
      0.7396
   
   C2 = 
      0.8516
      0.2735
   
   C3 = 
      0.6566
      0.2032
      0.7469
      0.9711
      0.3519
      0.6197
      0.4591
      0.2780
      0.2815
      0.7396
      0.8516
      0.2735
   

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
   

