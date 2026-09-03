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
      0.3797    0.6787    0.2451    0.1426    0.9226    0.9161    0.4068
   
   C = 
      0.8285
      0.4666
      0.9479
      0.4458
      0.6097
   
   M = 
      0.8467    0.9454    0.2649    0.9519    0.1486    0.2329    0.2135
      0.7107    0.5487    0.6193    0.4537    0.5514    0.3057    0.6957
      0.7351    0.2133    0.7400    0.2523    0.5445    0.9858    0.1147
      0.6694    0.6409    0.6093    0.7495    0.9619    0.5195    0.3435
      0.0138    0.8448    0.8082    0.9843    0.2633    0.2419    0.6029
      0.0346    0.1292    0.1512    0.4803    0.4812    0.3571    0.8668
      0.3342    0.5591    0.7285    0.6382    0.5792    0.9096    0.7993
      0.3322    0.3027    0.1185    0.6214    0.6755    0.0790    0.3911
   

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
      0.0535    0.8932    0.0465    0.3477
   
   R2 = 
      0.5374    0.8923    0.6443    0.6319    0.1798
   
   R3 = 
      0.0535    0.8932    0.0465    0.3477    0.5374    0.8923    0.6443    0.6319    0.1798
   
   C1 = 
      0.5517
      0.0004
      0.0776
      0.5210
      0.6355
      0.7832
      0.7053
      0.7274
      0.3472
      0.0549
   
   C2 = 
      0.3860
      0.6875
      0.5296
      0.7289
      0.0346
      0.2420
      0.0572
      0.5957
      0.0666
      0.6163
   
   M = 
      0.5517    0.3860
      0.0004    0.6875
      0.0776    0.5296
      0.5210    0.7289
      0.6355    0.0346
      0.7832    0.2420
      0.7053    0.0572
      0.7274    0.5957
      0.3472    0.0666
      0.0549    0.6163
   


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
      0.7208    0.2846    0.7959    0.2662
   
   R2 = 
      0.6616    0.9327    0.8304    0.4493
   
   M = 
      0.7208    0.2846    0.7959    0.2662
      0.6616    0.9327    0.8304    0.4493
   
   C1 = 
      0.2076
      0.8941
      0.9617
      0.8177
      0.2902
      0.7269
      0.3452
      0.7937
      0.7786
      0.3272
   
   C2 = 
      0.3133
      0.1322
   
   C3 = 
      0.2076
      0.8941
      0.9617
      0.8177
      0.2902
      0.7269
      0.3452
      0.7937
      0.7786
      0.3272
      0.3133
      0.1322
   

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
   

