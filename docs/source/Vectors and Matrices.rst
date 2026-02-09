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
      0.0136    0.9115    0.0430    0.7012    0.5671    0.4626    0.0602
   
   C = 
      0.5643
      0.3073
      0.0230
      0.1312
      0.6402
   
   M = 
      0.5127    0.2993    0.0476    0.3332    0.2367    0.0330    0.9036
      0.2725    0.4678    0.9843    0.4609    0.3545    0.7622    0.3896
      0.0319    0.9666    0.5866    0.4079    0.5828    0.3658    0.1227
      0.6068    0.2215    0.2984    0.5238    0.9266    0.5264    0.2358
      0.1997    0.6972    0.4472    0.8540    0.6848    0.7710    0.6023
      0.4212    0.1561    0.2779    0.0560    0.7276    0.6879    0.2094
      0.0004    0.3850    0.8633    0.3865    0.8223    0.3128    0.3913
      0.5604    0.4474    0.5447    0.7207    0.1225    0.7827    0.3915
   

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
      0.3464    0.7548    0.4716    0.7172
   
   R2 = 
      0.8944    0.1252    0.6862    0.2290    0.3231
   
   R3 = 
      0.3464    0.7548    0.4716    0.7172    0.8944    0.1252    0.6862    0.2290    0.3231
   
   C1 = 
      0.6186
      0.8971
      0.8626
      0.5898
      0.1575
      0.6003
      0.0965
      0.9070
      0.3311
      0.0342
   
   C2 = 
      0.4181
      0.3263
      0.5122
      0.6463
      0.6341
      0.0456
      0.3003
      0.8382
      0.5926
      0.3979
   
   M = 
      0.6186    0.4181
      0.8971    0.3263
      0.8626    0.5122
      0.5898    0.6463
      0.1575    0.6341
      0.6003    0.0456
      0.0965    0.3003
      0.9070    0.8382
      0.3311    0.5926
      0.0342    0.3979
   


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
      0.8260    0.2396    0.5208    0.2409
   
   R2 = 
      0.5510    0.8716    0.9261    0.1093
   
   M = 
      0.8260    0.2396    0.5208    0.2409
      0.5510    0.8716    0.9261    0.1093
   
   C1 = 
      0.5590
      0.9045
      0.2594
      0.9130
      0.4615
      0.8504
      0.5895
      0.2762
      0.6280
      0.5969
   
   C2 = 
      0.7488
      0.0861
   
   C3 = 
      0.5590
      0.9045
      0.2594
      0.9130
      0.4615
      0.8504
      0.5895
      0.2762
      0.6280
      0.5969
      0.7488
      0.0861
   

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
   

