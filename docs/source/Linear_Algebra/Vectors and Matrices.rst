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
      0.2757    0.6906    0.2373    0.8114    0.6289    0.7567    0.4177
   
   C = 
      0.2663
      0.4285
      0.6633
      0.9525
      0.0208
   
   M = 
      0.0251    0.6590    0.7182    0.7979    0.5011    0.0520    0.9257
      0.0442    0.5968    0.8639    0.0567    0.6499    0.7511    0.3336
      0.4163    0.0734    0.7774    0.0425    0.7841    0.9981    0.1679
      0.6835    0.2724    0.8570    0.1650    0.7278    0.3621    0.8554
      0.7293    0.0909    0.6030    0.8701    0.2384    0.2043    0.8148
      0.3984    0.8972    0.0257    0.3624    0.0083    0.5616    0.5576
      0.8659    0.4525    0.6792    0.3860    0.4576    0.9426    0.9305
      0.7389    0.8216    0.7482    0.6320    0.5954    0.4873    0.7977
   

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
      0.4915    0.3625    0.6447    0.0812
   
   R2 = 
      0.1257    0.9371    0.3280    0.0248    0.8850
   
   R3 = 
      0.4915    0.3625    0.6447    0.0812    0.1257    0.9371    0.3280    0.0248    0.8850
   
   C1 = 
      0.0937
      0.8987
      0.4482
      0.4506
      0.0728
      0.1185
      0.6667
      0.3494
      0.3552
      0.3346
   
   C2 = 
      0.4892
      0.0981
      0.7091
      0.5809
      0.4151
      0.2745
      0.0263
      0.0427
      0.2149
      0.0505
   
   M = 
      0.0937    0.4892
      0.8987    0.0981
      0.4482    0.7091
      0.4506    0.5809
      0.0728    0.4151
      0.1185    0.2745
      0.6667    0.0263
      0.3494    0.0427
      0.3552    0.2149
      0.3346    0.0505
   


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
      0.2976    0.1314    0.2661    0.7036
   
   R2 = 
      0.5473    0.2819    0.0918    0.0506
   
   M = 
      0.2976    0.1314    0.2661    0.7036
      0.5473    0.2819    0.0918    0.0506
   
   C1 = 
      0.5644
      0.0354
      0.1369
      0.2734
      0.5096
      0.9667
      0.6821
      0.4286
      0.2974
      0.6332
   
   C2 = 
      0.0912
      0.8435
   
   C3 = 
      0.5644
      0.0354
      0.1369
      0.2734
      0.5096
      0.9667
      0.6821
      0.4286
      0.2974
      0.6332
      0.0912
      0.8435
   

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
   

