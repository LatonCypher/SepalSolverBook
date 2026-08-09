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
      0.1173    0.0571    0.0031    0.2253    0.6934    0.8769    0.5320
   
   C = 
      0.8757
      0.3808
      0.8382
      0.4138
      0.8368
   
   M = 
      0.8322    0.2730    0.5880    0.4872    0.7206    0.8948    0.6947
      0.1818    0.8389    0.8045    0.5659    0.1917    0.5644    0.5272
      0.8249    0.9868    0.9554    0.8388    0.6767    0.6248    0.3707
      0.2504    0.3012    0.3326    0.5731    0.5102    0.1900    0.9573
      0.1088    0.7774    0.3401    0.9477    0.6318    0.9793    0.1451
      0.1147    0.5098    0.3117    0.3515    0.5691    0.0039    0.9254
      0.8700    0.3126    0.0654    0.0185    0.5743    0.8801    0.3849
      0.5440    0.0326    0.0881    0.2486    0.7044    0.8706    0.7672
   

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
      0.8995    0.0199    0.2016    0.4980
   
   R2 = 
      0.3875    0.9430    0.5774    0.3866    0.6450
   
   R3 = 
      0.8995    0.0199    0.2016    0.4980    0.3875    0.9430    0.5774    0.3866    0.6450
   
   C1 = 
      0.9479
      0.5858
      0.5790
      0.2048
      0.5510
      0.3437
      0.2096
      0.2305
      0.7572
      0.5612
   
   C2 = 
      0.0517
      0.6906
      0.3351
      0.7732
      0.5752
      0.3319
      0.6737
      0.4213
      0.3223
      0.6616
   
   M = 
      0.9479    0.0517
      0.5858    0.6906
      0.5790    0.3351
      0.2048    0.7732
      0.5510    0.5752
      0.3437    0.3319
      0.2096    0.6737
      0.2305    0.4213
      0.7572    0.3223
      0.5612    0.6616
   


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
      0.4540    0.0709    0.5442    0.7554
   
   R2 = 
      0.2507    0.7940    0.5362    0.8002
   
   M = 
      0.4540    0.0709    0.5442    0.7554
      0.2507    0.7940    0.5362    0.8002
   
   C1 = 
      0.9433
      0.1999
      0.3101
      0.2513
      0.2748
      0.9872
      0.8982
      0.2639
      0.8226
      0.5176
   
   C2 = 
      0.8246
      0.7945
   
   C3 = 
      0.9433
      0.1999
      0.3101
      0.2513
      0.2748
      0.9872
      0.8982
      0.2639
      0.8226
      0.5176
      0.8246
      0.7945
   

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
   

