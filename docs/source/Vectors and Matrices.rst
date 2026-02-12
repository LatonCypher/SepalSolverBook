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
      0.3108    0.2109    0.8024    0.9053    0.2090    0.9706    0.6567
   
   C = 
      0.1217
      0.9139
      0.2574
      0.9952
      0.9033
   
   M = 
      0.8073    0.6215    0.6940    0.3521    0.2344    0.7828    0.8586
      0.3953    0.1645    0.1272    0.3381    0.5038    0.3346    0.7975
      0.3484    0.2837    0.6120    0.6035    0.3723    0.4667    0.0537
      0.2234    0.0359    0.9584    0.0795    0.0447    0.9175    0.0766
      0.5368    0.8149    0.0537    0.8990    0.9196    0.3807    0.6717
      0.1325    0.7203    0.7473    0.2089    0.3694    0.7564    0.0216
      0.6149    0.0458    0.4824    0.7415    0.6679    0.1357    0.7687
      0.0773    0.6818    0.0404    0.6643    0.5891    0.9591    0.5476
   

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
      0.7162    0.1451    0.3953    0.4598
   
   R2 = 
      0.7512    0.6645    0.5134    0.5580    0.9269
   
   R3 = 
      0.7162    0.1451    0.3953    0.4598    0.7512    0.6645    0.5134    0.5580    0.9269
   
   C1 = 
      0.1473
      0.8770
      0.7590
      0.8937
      0.2505
      0.1020
      0.1355
      0.3695
      0.0527
      0.6679
   
   C2 = 
      0.4547
      0.4034
      0.4866
      0.4816
      0.8528
      0.7201
      0.3260
      0.4133
      0.8126
      0.5758
   
   M = 
      0.1473    0.4547
      0.8770    0.4034
      0.7590    0.4866
      0.8937    0.4816
      0.2505    0.8528
      0.1020    0.7201
      0.1355    0.3260
      0.3695    0.4133
      0.0527    0.8126
      0.6679    0.5758
   


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
      0.1497    0.8411    0.5069    0.9870
   
   R2 = 
      0.9994    0.8638    0.4787    0.1596
   
   M = 
      0.1497    0.8411    0.5069    0.9870
      0.9994    0.8638    0.4787    0.1596
   
   C1 = 
      0.7217
      0.8525
      0.6080
      0.1408
      0.9756
      0.0448
      0.0977
      0.7462
      0.4062
      0.5143
   
   C2 = 
      0.5405
      0.9818
   
   C3 = 
      0.7217
      0.8525
      0.6080
      0.1408
      0.9756
      0.0448
      0.0977
      0.7462
      0.4062
      0.5143
      0.5405
      0.9818
   

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
   

