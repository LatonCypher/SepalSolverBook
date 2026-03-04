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
      0.2710    0.9879    0.1794    0.2784    0.6730    0.2603    0.9973
   
   C = 
      0.3417
      0.1914
      0.8687
      0.2264
      0.8033
   
   M = 
      0.5117    0.2858    0.3338    0.1609    0.1529    0.0501    0.7324
      0.7800    0.9997    0.2176    0.3276    0.6877    0.3377    0.4302
      0.6909    0.3487    0.8261    0.3780    0.4927    0.3890    0.9914
      0.4867    0.5246    0.6077    0.3009    0.7065    0.4313    0.6236
      0.5583    0.6976    0.5916    0.6830    0.0075    0.8177    0.9826
      0.9381    0.9074    0.4088    0.7687    0.3563    0.4687    0.5127
      0.6707    0.5925    0.6979    0.2502    0.5637    0.0500    0.8181
      0.7874    0.7399    0.4651    0.9171    0.5800    0.6743    0.5070
   

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
      0.2557    0.1598    0.3521    0.9094
   
   R2 = 
      0.2067    0.5538    0.1597    0.6692    0.2449
   
   R3 = 
      0.2557    0.1598    0.3521    0.9094    0.2067    0.5538    0.1597    0.6692    0.2449
   
   C1 = 
      0.1344
      0.4660
      0.0297
      0.4645
      0.2354
      0.4375
      0.1147
      0.1320
      0.4877
      0.3486
   
   C2 = 
      0.3156
      0.5015
      0.5510
      0.3812
      0.8346
      0.3416
      0.2171
      0.1220
      0.0691
      0.7811
   
   M = 
      0.1344    0.3156
      0.4660    0.5015
      0.0297    0.5510
      0.4645    0.3812
      0.2354    0.8346
      0.4375    0.3416
      0.1147    0.2171
      0.1320    0.1220
      0.4877    0.0691
      0.3486    0.7811
   


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
      0.2358    0.7915    0.4654    0.7536
   
   R2 = 
      0.6944    0.5935    0.8700    0.9148
   
   M = 
      0.2358    0.7915    0.4654    0.7536
      0.6944    0.5935    0.8700    0.9148
   
   C1 = 
      0.1125
      0.4721
      0.6887
      0.4463
      0.2080
      0.7168
      0.1346
      0.3981
      0.7297
      0.6114
   
   C2 = 
      0.7137
      0.2736
   
   C3 = 
      0.1125
      0.4721
      0.6887
      0.4463
      0.2080
      0.7168
      0.1346
      0.3981
      0.7297
      0.6114
      0.7137
      0.2736
   

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
   

