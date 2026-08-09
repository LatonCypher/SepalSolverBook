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
      0.1308    0.6536    0.7761    0.2089    0.7132    0.5785    0.7365
   
   C = 
      0.2775
      0.0839
      0.0337
      0.5234
      0.2180
   
   M = 
      0.1269    0.7488    0.5741    0.9432    0.6999    0.7278    0.5085
      0.3444    0.4567    0.9077    0.4377    0.7241    0.5893    0.9264
      0.7608    0.8124    0.0240    0.1461    0.7890    0.1767    0.5686
      0.1005    0.1851    0.9083    0.6203    0.5371    0.0253    0.5892
      0.6770    0.6930    0.7812    0.9932    0.0994    0.8895    0.4147
      0.8396    0.6761    0.2195    0.5163    0.1020    0.3525    0.9709
      0.2709    0.0137    0.9242    0.7320    0.5722    0.3135    0.5718
      0.6501    0.6937    0.0981    0.8733    0.8564    0.9318    0.0804
   

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
      0.1226    0.9622    0.9445    0.0154
   
   R2 = 
      0.8578    0.1463    0.9248    0.4773    0.7679
   
   R3 = 
      0.1226    0.9622    0.9445    0.0154    0.8578    0.1463    0.9248    0.4773    0.7679
   
   C1 = 
      0.1872
      0.3887
      0.9289
      0.7525
      0.2721
      0.0982
      0.3306
      0.1153
      0.1483
      0.3284
   
   C2 = 
      0.6659
      0.2169
      0.3737
      0.5826
      0.5526
      0.9001
      0.3782
      0.6073
      0.4912
      0.0630
   
   M = 
      0.1872    0.6659
      0.3887    0.2169
      0.9289    0.3737
      0.7525    0.5826
      0.2721    0.5526
      0.0982    0.9001
      0.3306    0.3782
      0.1153    0.6073
      0.1483    0.4912
      0.3284    0.0630
   


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
      0.2368    0.5920    0.0057    0.1535
   
   R2 = 
      0.5464    0.3059    0.7948    0.9342
   
   M = 
      0.2368    0.5920    0.0057    0.1535
      0.5464    0.3059    0.7948    0.9342
   
   C1 = 
      0.2409
      0.9021
      0.9696
      0.3278
      0.7728
      0.9493
      0.1388
      0.5904
      0.5814
      0.0678
   
   C2 = 
      0.1686
      0.9111
   
   C3 = 
      0.2409
      0.9021
      0.9696
      0.3278
      0.7728
      0.9493
      0.1388
      0.5904
      0.5814
      0.0678
      0.1686
      0.9111
   

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
   

