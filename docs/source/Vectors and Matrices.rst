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
      0.4833    0.3645    0.5752    0.8897    0.0243    0.1890    0.9943
   
   C = 
      0.1189
      0.0510
      0.9990
      0.3083
      0.0895
   
   M = 
      0.4024    0.4415    0.1181    0.2530    0.1535    0.8512    0.5352
      0.1590    0.9414    0.8439    0.5410    0.8214    0.7783    0.3203
      0.4542    0.3897    0.7019    0.4335    0.3970    0.3638    0.7252
      0.2572    0.5472    0.2569    0.0438    0.1241    0.8322    0.7616
      0.2410    0.3629    0.7987    0.0277    0.0945    0.9218    0.7931
      0.6444    0.1582    0.0299    0.1109    0.9044    0.6321    0.9582
      0.1469    0.5862    0.6238    0.6317    0.1762    0.4971    0.7135
      0.5321    0.3362    0.9143    0.3469    0.3915    0.2498    0.6706
   

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
      0.4239    0.3279    0.4326    0.3407
   
   R2 = 
      0.1257    0.9502    0.2875    0.8746    0.2829
   
   R3 = 
      0.4239    0.3279    0.4326    0.3407    0.1257    0.9502    0.2875    0.8746    0.2829
   
   C1 = 
      0.9108
      0.2754
      0.7118
      0.8593
      0.8424
      0.9271
      0.4079
      0.6422
      0.9772
      0.3133
   
   C2 = 
      0.6069
      0.2843
      0.3647
      0.2352
      0.0583
      0.5675
      0.4421
      0.4526
      0.1356
      0.5704
   
   M = 
      0.9108    0.6069
      0.2754    0.2843
      0.7118    0.3647
      0.8593    0.2352
      0.8424    0.0583
      0.9271    0.5675
      0.4079    0.4421
      0.6422    0.4526
      0.9772    0.1356
      0.3133    0.5704
   


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
      0.7618    0.1321    0.3119    0.9234
   
   R2 = 
      0.5377    0.6255    0.7115    0.8720
   
   M = 
      0.7618    0.1321    0.3119    0.9234
      0.5377    0.6255    0.7115    0.8720
   
   C1 = 
      0.2865
      0.3810
      0.4044
      0.5326
      0.7414
      0.8386
      0.9265
      0.9867
      0.3728
      0.4946
   
   C2 = 
      0.2887
      0.4771
   
   C3 = 
      0.2865
      0.3810
      0.4044
      0.5326
      0.7414
      0.8386
      0.9265
      0.9867
      0.3728
      0.4946
      0.2887
      0.4771
   

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
   

