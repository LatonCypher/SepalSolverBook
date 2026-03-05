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
      0.6389    0.3094    0.7373    0.8382    0.5846    0.9582    0.0930
   
   C = 
      0.7087
      0.2691
      0.0500
      0.3416
      0.4546
   
   M = 
      0.9532    0.5029    0.2104    0.0333    0.1842    0.2381    0.5896
      0.9745    0.5303    0.1852    0.3651    0.7151    0.2947    0.2087
      0.9543    0.9909    0.9791    0.2908    0.0515    0.8014    0.0282
      0.5515    0.3515    0.4920    0.0469    0.3253    0.5212    0.3437
      0.2869    0.9646    0.7518    0.6523    0.9521    0.7987    0.9033
      0.8542    0.0988    0.3793    0.8992    0.9457    0.5801    0.1342
      0.8792    0.2969    0.6313    0.9945    0.9144    0.0730    0.7740
      0.6503    0.9040    0.9129    0.3433    0.6765    0.3245    0.9273
   

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
      0.3771    0.7841    0.0563    0.2103
   
   R2 = 
      0.3809    0.8290    0.2882    0.1112    0.7353
   
   R3 = 
      0.3771    0.7841    0.0563    0.2103    0.3809    0.8290    0.2882    0.1112    0.7353
   
   C1 = 
      0.2283
      0.2835
      0.9966
      0.7337
      0.6578
      0.5932
      0.1999
      0.4209
      0.1787
      0.2947
   
   C2 = 
      0.6611
      0.2003
      0.2399
      0.6828
      0.3506
      0.8725
      0.3297
      0.3772
      0.8967
      0.0550
   
   M = 
      0.2283    0.6611
      0.2835    0.2003
      0.9966    0.2399
      0.7337    0.6828
      0.6578    0.3506
      0.5932    0.8725
      0.1999    0.3297
      0.4209    0.3772
      0.1787    0.8967
      0.2947    0.0550
   


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
      0.5958    0.3802    0.8934    0.0497
   
   R2 = 
      0.6302    0.2140    0.7867    0.7441
   
   M = 
      0.5958    0.3802    0.8934    0.0497
      0.6302    0.2140    0.7867    0.7441
   
   C1 = 
      0.4506
      0.7966
      0.9295
      0.2269
      0.6505
      0.4313
      0.1107
      0.7087
      0.6101
      0.9602
   
   C2 = 
      0.6987
      0.1067
   
   C3 = 
      0.4506
      0.7966
      0.9295
      0.2269
      0.6505
      0.4313
      0.1107
      0.7087
      0.6101
      0.9602
      0.6987
      0.1067
   

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
   

