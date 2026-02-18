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
      0.3016    0.8480    0.5732    0.5864    0.7432    0.4105    0.1103
   
   C = 
      0.1589
      0.0347
      0.6109
      0.8711
      0.7724
   
   M = 
      0.9171    0.8979    0.4985    0.4173    0.7363    0.5214    0.6730
      0.9824    0.2000    0.5661    0.8329    0.2825    0.6032    0.8978
      0.1521    0.0154    0.0090    0.1420    0.9355    0.5256    0.7447
      0.5244    0.3686    0.2796    0.0181    0.3367    0.2829    0.3785
      0.8438    0.3891    0.7295    0.0727    0.1635    0.3188    0.3751
      0.3938    0.4831    0.6485    0.1290    0.6424    0.4799    0.2666
      0.7529    0.7001    0.4420    0.5107    0.1213    0.9770    0.6812
      0.6315    0.2452    0.6242    0.8092    0.6470    0.8019    0.1653
   

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
      0.9284    0.7166    0.4578    0.2530
   
   R2 = 
      0.7144    0.5058    0.3917    0.5683    0.5521
   
   R3 = 
      0.9284    0.7166    0.4578    0.2530    0.7144    0.5058    0.3917    0.5683    0.5521
   
   C1 = 
      0.7572
      0.4831
      0.9355
      0.0057
      0.2326
      0.6072
      0.0698
      0.3202
      0.8353
      0.8930
   
   C2 = 
      0.3201
      0.8942
      0.1725
      0.7306
      0.5701
      0.5777
      0.6490
      0.0052
      0.1550
      0.7215
   
   M = 
      0.7572    0.3201
      0.4831    0.8942
      0.9355    0.1725
      0.0057    0.7306
      0.2326    0.5701
      0.6072    0.5777
      0.0698    0.6490
      0.3202    0.0052
      0.8353    0.1550
      0.8930    0.7215
   


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
      0.6725    0.5264    0.1765    0.4116
   
   R2 = 
      0.4360    0.6565    0.4253    0.8404
   
   M = 
      0.6725    0.5264    0.1765    0.4116
      0.4360    0.6565    0.4253    0.8404
   
   C1 = 
      0.7813
      0.5483
      0.4502
      0.0137
      0.1720
      0.0577
      0.4671
      0.1534
      0.7713
      0.9605
   
   C2 = 
      0.7510
      0.4070
   
   C3 = 
      0.7813
      0.5483
      0.4502
      0.0137
      0.1720
      0.0577
      0.4671
      0.1534
      0.7713
      0.9605
      0.7510
      0.4070
   

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
   

