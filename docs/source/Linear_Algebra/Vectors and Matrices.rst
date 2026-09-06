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
      0.6538    0.8695    0.4777    0.0887    0.1781    0.2183    0.6189
   
   C = 
      0.1783
      0.1056
      0.8826
      0.8116
      0.0464
   
   M = 
      0.9277    0.4920    0.8876    0.8622    0.5190    0.9323    0.1147
      0.2470    0.6675    0.2705    0.6441    0.9897    0.0141    0.7880
      0.2953    0.2214    0.7711    0.2840    0.6973    0.7277    0.9022
      0.2256    0.4330    0.7960    0.5360    0.1674    0.0708    0.0879
      0.1378    0.1453    0.3025    0.4074    0.2885    0.9512    0.1822
      0.0517    0.2855    0.0489    0.9589    0.9781    0.6494    0.3411
      0.2771    0.8812    0.2158    0.3680    0.1665    0.2842    0.9978
      0.1341    0.8243    0.1127    0.5223    0.2765    0.8094    0.2144
   

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
      0.0854    0.3949    0.1771    0.1246
   
   R2 = 
      0.2005    0.2924    0.1761    0.8547    0.5324
   
   R3 = 
      0.0854    0.3949    0.1771    0.1246    0.2005    0.2924    0.1761    0.8547    0.5324
   
   C1 = 
      0.4184
      0.5693
      0.3628
      0.9284
      0.9632
      0.0438
      0.8236
      0.2844
      0.8270
      0.6827
   
   C2 = 
      0.9667
      0.9431
      0.0943
      0.0013
      0.1883
      0.1350
      0.5887
      0.3646
      0.9920
      0.4987
   
   M = 
      0.4184    0.9667
      0.5693    0.9431
      0.3628    0.0943
      0.9284    0.0013
      0.9632    0.1883
      0.0438    0.1350
      0.8236    0.5887
      0.2844    0.3646
      0.8270    0.9920
      0.6827    0.4987
   


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
      0.1661    0.2500    0.3998    0.9506
   
   R2 = 
      0.2995    0.5916    0.1311    0.2076
   
   M = 
      0.1661    0.2500    0.3998    0.9506
      0.2995    0.5916    0.1311    0.2076
   
   C1 = 
      0.5389
      0.4803
      0.7541
      0.4846
      0.3012
      0.5061
      0.1342
      0.7158
      0.0195
      0.1158
   
   C2 = 
      0.2546
      0.5697
   
   C3 = 
      0.5389
      0.4803
      0.7541
      0.4846
      0.3012
      0.5061
      0.1342
      0.7158
      0.0195
      0.1158
      0.2546
      0.5697
   

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
   

