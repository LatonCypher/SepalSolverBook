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
      0.9089    0.5080    0.5964    0.6974    0.9866    0.4260    0.9672
   
   C = 
      0.8690
      0.6364
      0.5581
      0.2919
      0.5276
   
   M = 
      0.1649    0.4959    0.4219    0.7973    0.3650    0.7522    0.0623
      0.6451    0.0190    0.1308    0.8745    0.9323    0.2373    0.5556
      0.8831    0.6028    0.6949    0.8479    0.2290    0.5077    0.6270
      0.7940    0.0284    0.4987    0.8803    0.4203    0.4764    0.7290
      0.6978    0.2247    0.6597    0.6509    0.9360    0.9679    0.3078
      0.5681    0.9151    0.6773    0.5368    0.0434    0.2086    0.8567
      0.5877    0.6937    0.6534    0.6274    0.7147    0.1111    0.3408
      0.4502    0.6384    0.2029    0.0378    0.4271    0.1412    0.7601
   

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
      0.7027    0.4283    0.6585    0.3899
   
   R2 = 
      0.5935    0.3967    0.8081    0.2523    0.4515
   
   R3 = 
      0.7027    0.4283    0.6585    0.3899    0.5935    0.3967    0.8081    0.2523    0.4515
   
   C1 = 
      0.2421
      0.6880
      0.9195
      0.1758
      0.1424
      0.2812
      0.3630
      0.1827
      0.5318
      0.2855
   
   C2 = 
      0.2489
      0.1391
      0.3517
      0.8086
      0.8587
      0.2341
      0.0141
      0.5770
      0.9037
      0.7878
   
   M = 
      0.2421    0.2489
      0.6880    0.1391
      0.9195    0.3517
      0.1758    0.8086
      0.1424    0.8587
      0.2812    0.2341
      0.3630    0.0141
      0.1827    0.5770
      0.5318    0.9037
      0.2855    0.7878
   


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
      0.3143    0.8318    0.9998    0.2622
   
   R2 = 
      0.1376    0.2008    0.4075    0.0508
   
   M = 
      0.3143    0.8318    0.9998    0.2622
      0.1376    0.2008    0.4075    0.0508
   
   C1 = 
      0.5472
      0.4277
      0.4557
      0.2429
      0.7177
      0.5075
      0.5684
      0.9701
      0.1310
      0.1853
   
   C2 = 
      0.6209
      0.8160
   
   C3 = 
      0.5472
      0.4277
      0.4557
      0.2429
      0.7177
      0.5075
      0.5684
      0.9701
      0.1310
      0.1853
      0.6209
      0.8160
   

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
   

