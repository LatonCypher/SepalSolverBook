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
      0.7316    0.2310    0.8120    0.8907    0.8633    0.3079    0.3926
   
   C = 
      0.8316
      0.8694
      0.1055
      0.5804
      0.3615
   
   M = 
      0.1535    0.0073    0.4306    0.3227    0.1023    0.5115    0.3951
      0.2360    0.7803    0.3013    0.9711    0.8327    0.6072    0.5777
      0.9552    0.0482    0.3672    0.4647    0.3123    0.4901    0.2631
      0.1038    0.3479    0.4876    0.3747    0.9657    0.1644    0.2196
      0.1101    0.1912    0.1694    0.0330    0.5032    0.6466    0.1420
      0.5240    0.4701    0.9970    0.9760    0.9719    0.2382    0.3434
      0.5959    0.1693    0.8055    0.1198    0.9724    0.0718    0.3745
      0.1689    0.3297    0.7421    0.9823    0.3000    0.0388    0.9675
   

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
      0.3845    0.1878    0.1889    0.5803
   
   R2 = 
      0.7050    0.2874    0.4683    0.4222    0.2926
   
   R3 = 
      0.3845    0.1878    0.1889    0.5803    0.7050    0.2874    0.4683    0.4222    0.2926
   
   C1 = 
      0.5629
      0.0284
      0.4916
      0.1739
      0.3441
      0.0499
      0.2296
      0.2066
      0.3960
      0.0319
   
   C2 = 
      0.6457
      0.2133
      0.6355
      0.1952
      0.0745
      0.0799
      0.1121
      0.6904
      0.4834
      0.7304
   
   M = 
      0.5629    0.6457
      0.0284    0.2133
      0.4916    0.6355
      0.1739    0.1952
      0.3441    0.0745
      0.0499    0.0799
      0.2296    0.1121
      0.2066    0.6904
      0.3960    0.4834
      0.0319    0.7304
   


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
      0.0813    0.6210    0.0398    0.3447
   
   R2 = 
      0.5870    0.6272    0.7926    0.0888
   
   M = 
      0.0813    0.6210    0.0398    0.3447
      0.5870    0.6272    0.7926    0.0888
   
   C1 = 
      0.5531
      0.8012
      0.0245
      0.5853
      0.2939
      0.2919
      0.1136
      0.8150
      0.8824
      0.6441
   
   C2 = 
      0.5819
      0.6580
   
   C3 = 
      0.5531
      0.8012
      0.0245
      0.5853
      0.2939
      0.2919
      0.1136
      0.8150
      0.8824
      0.6441
      0.5819
      0.6580
   

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
   

