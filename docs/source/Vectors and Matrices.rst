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
      0.4001    0.1919    0.7538    0.9829    0.1836    0.9632    0.4699
   
   C = 
      0.9386
      0.3043
      0.4309
      0.0459
      0.5516
   
   M = 
      0.8329    0.1642    0.6409    0.3062    0.0460    0.9489    0.5658
      0.9770    0.3085    0.4058    0.6697    0.5952    0.0534    0.2184
      0.8073    0.7481    0.9836    0.9420    0.5838    0.8883    0.9575
      0.5508    0.6238    0.4653    0.9684    0.0385    0.1901    0.6282
      0.0301    0.7650    0.0801    0.6933    0.8456    0.1091    0.7196
      0.1646    0.1469    0.7598    0.1886    0.4988    0.1674    0.3670
      0.2992    0.0922    0.7291    0.2936    0.0822    0.0920    0.7104
      0.1366    0.1121    0.1307    0.2035    0.9389    0.7614    0.2181
   

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
      0.6117    0.5606    0.0826    0.6297
   
   R2 = 
      0.2988    0.7681    0.5396    0.8080    0.7385
   
   R3 = 
      0.6117    0.5606    0.0826    0.6297    0.2988    0.7681    0.5396    0.8080    0.7385
   
   C1 = 
      0.3758
      0.5264
      0.3981
      0.1990
      0.2861
      0.8430
      0.5511
      0.6109
      0.7540
      0.6028
   
   C2 = 
      0.7213
      0.6246
      0.1006
      0.7476
      0.3602
      0.3921
      0.6092
      0.8909
      0.9614
      0.1252
   
   M = 
      0.3758    0.7213
      0.5264    0.6246
      0.3981    0.1006
      0.1990    0.7476
      0.2861    0.3602
      0.8430    0.3921
      0.5511    0.6092
      0.6109    0.8909
      0.7540    0.9614
      0.6028    0.1252
   


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
      0.8403    0.9410    0.8255    0.5443
   
   R2 = 
      0.5147    0.3040    0.9776    0.0162
   
   M = 
      0.8403    0.9410    0.8255    0.5443
      0.5147    0.3040    0.9776    0.0162
   
   C1 = 
      0.8008
      0.4534
      0.8128
      0.8399
      0.1855
      0.4615
      0.4932
      0.7936
      0.1628
      0.2707
   
   C2 = 
      0.7120
      0.5355
   
   C3 = 
      0.8008
      0.4534
      0.8128
      0.8399
      0.1855
      0.4615
      0.4932
      0.7936
      0.1628
      0.2707
      0.7120
      0.5355
   

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
   

