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
      0.8635    0.9152    0.8900    0.1913    0.0953    0.0415    0.2252
   
   C = 
      0.5785
      0.8921
      0.5195
      0.7971
      0.9405
   
   M = 
      0.5774    0.9438    0.2098    0.7637    0.1112    0.0794    0.6032
      0.1069    0.2733    0.6908    0.6111    0.9846    0.4372    0.4862
      0.0087    0.7247    0.3306    0.1463    0.4555    0.3349    0.1177
      0.5502    0.0564    0.0498    0.2728    0.9863    0.5587    0.4987
      0.9473    0.7663    0.5970    0.3062    0.1008    0.0725    0.6638
      0.4358    0.5057    0.1776    0.3059    0.6535    0.7825    0.6471
      0.3655    0.5846    0.1670    0.0720    0.4518    0.8922    0.1793
      0.7251    0.8491    0.6636    0.0731    0.7538    0.2967    0.5460
   

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
      0.5429    0.3633    0.1643    0.5804
   
   R2 = 
      0.3452    0.1720    0.7623    0.7517    0.1068
   
   R3 = 
      0.5429    0.3633    0.1643    0.5804    0.3452    0.1720    0.7623    0.7517    0.1068
   
   C1 = 
      0.5351
      0.3491
      0.3467
      0.9890
      0.2559
      0.5065
      0.7133
      0.1033
      0.1498
      0.0514
   
   C2 = 
      0.7689
      0.6943
      0.6605
      0.0868
      0.0251
      0.8108
      0.3628
      0.1273
      0.0679
      0.4167
   
   M = 
      0.5351    0.7689
      0.3491    0.6943
      0.3467    0.6605
      0.9890    0.0868
      0.2559    0.0251
      0.5065    0.8108
      0.7133    0.3628
      0.1033    0.1273
      0.1498    0.0679
      0.0514    0.4167
   


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
      0.9901    0.4219    0.6034    0.5945
   
   R2 = 
      0.6219    0.4277    0.5197    0.3413
   
   M = 
      0.9901    0.4219    0.6034    0.5945
      0.6219    0.4277    0.5197    0.3413
   
   C1 = 
      0.2150
      0.0571
      0.2199
      0.8748
      0.6962
      0.0588
      0.0101
      0.7613
      0.0332
      0.0812
   
   C2 = 
      0.1636
      0.6384
   
   C3 = 
      0.2150
      0.0571
      0.2199
      0.8748
      0.6962
      0.0588
      0.0101
      0.7613
      0.0332
      0.0812
      0.1636
      0.6384
   

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
   

