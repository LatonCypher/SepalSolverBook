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
      0.2150    0.8752    0.9758    0.5001    0.8759    0.4596    0.7494
   
   C = 
      0.6539
      0.1991
      0.3646
      0.3842
      0.9719
   
   M = 
      0.9521    0.3957    0.3446    0.3212    0.6661    0.4110    0.2337
      0.4670    0.7268    0.0495    0.3395    0.2714    0.4949    0.7342
      0.9960    0.9316    0.5489    0.1364    0.0655    0.8546    0.8096
      0.0062    0.1980    0.4590    0.5672    0.7751    0.4726    0.1441
      0.3239    0.7169    0.8477    0.3671    0.6633    0.3159    0.0726
      0.0316    0.9102    0.4678    0.8088    0.9221    0.9490    0.3851
      0.9363    0.6591    0.7099    0.5674    0.8614    0.0229    0.1675
      0.5651    0.8589    0.0175    0.1098    0.7065    0.6068    0.8190
   

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
      0.7241    0.5074    0.7715    0.5531
   
   R2 = 
      0.0264    0.6376    0.2509    0.2681    1.0000
   
   R3 = 
      0.7241    0.5074    0.7715    0.5531    0.0264    0.6376    0.2509    0.2681    1.0000
   
   C1 = 
      0.0845
      0.7091
      0.4953
      0.0115
      0.0770
      0.0496
      0.8588
      0.3168
      0.6111
      0.3983
   
   C2 = 
      0.3663
      0.3233
      0.5047
      0.6528
      0.4543
      0.9887
      0.0529
      0.9685
      0.2512
      0.8313
   
   M = 
      0.0845    0.3663
      0.7091    0.3233
      0.4953    0.5047
      0.0115    0.6528
      0.0770    0.4543
      0.0496    0.9887
      0.8588    0.0529
      0.3168    0.9685
      0.6111    0.2512
      0.3983    0.8313
   


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
      0.4865    0.0991    0.1060    0.2217
   
   R2 = 
      0.2777    0.2816    0.7838    0.5463
   
   M = 
      0.4865    0.0991    0.1060    0.2217
      0.2777    0.2816    0.7838    0.5463
   
   C1 = 
      0.4136
      0.9398
      0.1797
      0.1903
      0.0848
      0.9616
      0.3738
      0.4688
      0.0027
      0.6932
   
   C2 = 
      0.3921
      0.1490
   
   C3 = 
      0.4136
      0.9398
      0.1797
      0.1903
      0.0848
      0.9616
      0.3738
      0.4688
      0.0027
      0.6932
      0.3921
      0.1490
   

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
   

