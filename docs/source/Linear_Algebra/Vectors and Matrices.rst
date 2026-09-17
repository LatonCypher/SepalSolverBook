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
      0.8732    0.5705    0.2058    0.5349    0.7038    0.5347    0.6883
   
   C = 
      0.1829
      0.4696
      0.6351
      0.9017
      0.2961
   
   M = 
      0.0383    0.8965    0.0581    0.0630    0.0034    0.8032    0.4416
      0.1358    0.3650    0.3620    0.3939    0.8184    0.0700    0.9265
      0.2182    0.7233    0.5567    0.7303    0.7642    0.3426    0.9672
      0.0914    0.0709    0.1976    0.8827    0.8967    0.4091    0.7711
      0.6732    0.8065    0.1725    0.0715    0.9483    0.3987    0.4446
      0.6421    0.8440    0.3602    0.9491    0.1581    0.8286    0.1401
      0.9138    0.9386    0.6356    0.6641    0.9917    0.0111    0.7618
      0.6959    0.0342    0.2175    0.6351    0.0364    0.1230    0.9276
   

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
      0.6966    0.1432    0.4614    0.2655
   
   R2 = 
      0.9934    0.5627    0.7388    0.6510    0.0032
   
   R3 = 
      0.6966    0.1432    0.4614    0.2655    0.9934    0.5627    0.7388    0.6510    0.0032
   
   C1 = 
      0.5231
      0.1743
      0.8283
      0.3233
      0.7244
      0.9107
      0.4733
      0.2409
      0.7647
      0.0640
   
   C2 = 
      0.3617
      0.7922
      0.5430
      0.5596
      0.2924
      0.7772
      0.1847
      0.9931
      0.4334
      0.4297
   
   M = 
      0.5231    0.3617
      0.1743    0.7922
      0.8283    0.5430
      0.3233    0.5596
      0.7244    0.2924
      0.9107    0.7772
      0.4733    0.1847
      0.2409    0.9931
      0.7647    0.4334
      0.0640    0.4297
   


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
      0.2670    0.6065    0.2237    0.1546
   
   R2 = 
      0.8833    0.5497    0.4689    0.3769
   
   M = 
      0.2670    0.6065    0.2237    0.1546
      0.8833    0.5497    0.4689    0.3769
   
   C1 = 
      0.2963
      0.6962
      0.3148
      0.8687
      0.7094
      0.4285
      0.2086
      0.0265
      0.0562
      0.9875
   
   C2 = 
      0.6573
      0.6883
   
   C3 = 
      0.2963
      0.6962
      0.3148
      0.8687
      0.7094
      0.4285
      0.2086
      0.0265
      0.0562
      0.9875
      0.6573
      0.6883
   

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
   

