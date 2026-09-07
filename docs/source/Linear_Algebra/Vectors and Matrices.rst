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
      0.8191    0.9799    0.7922    0.2323    0.9187    0.0199    0.6980
   
   C = 
      0.8595
      0.0866
      0.2489
      0.9840
      0.6160
   
   M = 
      0.9004    0.1482    0.4076    0.0770    0.7952    0.9628    0.1385
      0.3023    0.2535    0.4635    0.5151    0.6960    0.0906    0.4729
      0.1195    0.3775    0.6394    0.4942    0.3952    0.0321    0.3352
      0.3648    0.8743    0.5295    0.9148    0.1082    0.5968    0.7830
      0.6056    0.5678    0.9359    0.9741    0.9217    0.1052    0.4492
      0.1539    0.8298    0.4905    0.0096    0.9955    0.3264    0.2177
      0.9521    0.7667    0.2734    0.6760    0.7597    0.9542    0.3448
      0.8548    0.7708    0.4647    0.6167    0.1231    0.7817    0.9656
   

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
      0.9661    0.2879    0.1578    0.4471
   
   R2 = 
      0.6394    0.6777    0.3538    0.9199    0.3914
   
   R3 = 
      0.9661    0.2879    0.1578    0.4471    0.6394    0.6777    0.3538    0.9199    0.3914
   
   C1 = 
      0.2144
      0.4534
      0.7552
      0.0724
      0.5749
      0.6563
      0.6387
      0.6162
      0.8037
      0.9366
   
   C2 = 
      0.3204
      0.4657
      0.3085
      0.7037
      0.6401
      0.9615
      0.6473
      0.9296
      0.3244
      0.2227
   
   M = 
      0.2144    0.3204
      0.4534    0.4657
      0.7552    0.3085
      0.0724    0.7037
      0.5749    0.6401
      0.6563    0.9615
      0.6387    0.6473
      0.6162    0.9296
      0.8037    0.3244
      0.9366    0.2227
   


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
      0.2882    0.1058    0.6222    0.4040
   
   R2 = 
      0.4890    0.8901    0.4658    0.6933
   
   M = 
      0.2882    0.1058    0.6222    0.4040
      0.4890    0.8901    0.4658    0.6933
   
   C1 = 
      0.3608
      0.9806
      0.9393
      0.6727
      0.1538
      0.2463
      0.5423
      0.4744
      0.9694
      0.7234
   
   C2 = 
      0.7614
      0.7122
   
   C3 = 
      0.3608
      0.9806
      0.9393
      0.6727
      0.1538
      0.2463
      0.5423
      0.4744
      0.9694
      0.7234
      0.7614
      0.7122
   

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
   

