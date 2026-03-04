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
      0.3058    0.0559    0.5055    0.7420    0.5061    0.2591    0.7957
   
   C = 
      0.6722
      0.7179
      0.2246
      0.0353
      0.6629
   
   M = 
      0.8214    0.4076    0.8318    0.9955    0.1974    0.3423    0.4773
      0.0654    0.6066    0.6467    0.5590    0.5165    0.8875    0.3861
      0.3558    0.5593    0.4351    0.1507    0.5383    0.7071    0.6311
      0.0606    0.5121    0.6350    0.4234    0.7585    0.0156    0.7341
      0.7459    0.1565    0.9548    0.3439    0.0683    0.9678    0.2495
      0.3547    0.9610    0.3629    0.1959    0.6057    0.5467    0.8789
      0.5971    0.6190    0.3620    0.1360    0.4399    0.7042    0.9899
      0.7391    0.0951    0.0168    0.6689    0.1374    0.6139    0.9392
   

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
      0.9006    0.2043    0.7607    0.7218
   
   R2 = 
      0.0275    0.1688    0.4912    0.2566    0.6373
   
   R3 = 
      0.9006    0.2043    0.7607    0.7218    0.0275    0.1688    0.4912    0.2566    0.6373
   
   C1 = 
      0.6577
      0.0826
      0.7916
      0.5016
      0.6297
      0.9362
      0.5388
      0.6362
      0.9652
      0.6954
   
   C2 = 
      0.3495
      0.5305
      0.2789
      0.4289
      0.8762
      0.6703
      0.8879
      0.9686
      0.6865
      0.1824
   
   M = 
      0.6577    0.3495
      0.0826    0.5305
      0.7916    0.2789
      0.5016    0.4289
      0.6297    0.8762
      0.9362    0.6703
      0.5388    0.8879
      0.6362    0.9686
      0.9652    0.6865
      0.6954    0.1824
   


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
      0.1224    0.9097    0.1596    0.8319
   
   R2 = 
      0.8423    0.2625    0.9588    0.3639
   
   M = 
      0.1224    0.9097    0.1596    0.8319
      0.8423    0.2625    0.9588    0.3639
   
   C1 = 
      0.9582
      0.7378
      0.9982
      0.6147
      0.6933
      0.4106
      0.9889
      0.7209
      0.5957
      0.3413
   
   C2 = 
      0.3742
      0.3709
   
   C3 = 
      0.9582
      0.7378
      0.9982
      0.6147
      0.6933
      0.4106
      0.9889
      0.7209
      0.5957
      0.3413
      0.3742
      0.3709
   

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
   

