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
      0.4252    0.1254    0.9278    0.9867    0.4558    0.2802    0.3150
   
   C = 
      0.1380
      0.6356
      0.3772
      0.7528
      0.2018
   
   M = 
      0.5902    0.1338    0.1261    0.0645    0.4370    0.0662    0.0015
      0.8419    0.2567    0.6044    0.0318    0.6559    0.0913    0.6347
      0.0266    0.0452    0.3209    0.3249    0.2748    0.7203    0.3794
      0.9335    0.3128    0.9652    0.9181    0.5956    0.3805    0.0211
      0.9305    0.0686    0.5795    0.5518    0.9708    0.4142    0.1399
      0.8503    0.7442    0.3769    0.5860    0.7485    0.9271    0.1428
      0.0386    0.5755    0.5658    0.6441    0.7260    0.8842    0.6213
      0.4194    0.9613    0.5148    0.3040    0.5957    0.6930    0.3483
   

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
      0.1757    0.7276    0.6672    0.4895
   
   R2 = 
      0.4302    0.3457    0.5102    0.3302    0.5027
   
   R3 = 
      0.1757    0.7276    0.6672    0.4895    0.4302    0.3457    0.5102    0.3302    0.5027
   
   C1 = 
      0.9046
      0.0237
      0.3740
      0.7644
      0.6909
      0.4286
      0.2233
      0.3176
      0.7435
      0.0200
   
   C2 = 
      0.5732
      0.3114
      0.9334
      0.3653
      0.1951
      0.5584
      0.3411
      0.1769
      0.7099
      0.4507
   
   M = 
      0.9046    0.5732
      0.0237    0.3114
      0.3740    0.9334
      0.7644    0.3653
      0.6909    0.1951
      0.4286    0.5584
      0.2233    0.3411
      0.3176    0.1769
      0.7435    0.7099
      0.0200    0.4507
   


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
      0.5241    0.6782    0.0149    0.7785
   
   R2 = 
      0.4285    0.2850    0.7337    0.9464
   
   M = 
      0.5241    0.6782    0.0149    0.7785
      0.4285    0.2850    0.7337    0.9464
   
   C1 = 
      0.8754
      0.5025
      0.5350
      0.2404
      0.0458
      0.7035
      0.7284
      0.1904
      0.3697
      0.5589
   
   C2 = 
      0.6506
      0.9068
   
   C3 = 
      0.8754
      0.5025
      0.5350
      0.2404
      0.0458
      0.7035
      0.7284
      0.1904
      0.3697
      0.5589
      0.6506
      0.9068
   

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
   

