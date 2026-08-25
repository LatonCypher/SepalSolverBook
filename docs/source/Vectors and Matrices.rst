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
      0.5692    0.5551    0.0914    0.5091    0.4136    0.9606    0.5166
   
   C = 
      0.6663
      0.9039
      0.5879
      0.6652
      0.1407
   
   M = 
      0.4067    0.1556    0.3147    0.7618    0.4031    0.5569    0.0198
      0.6585    0.9252    0.5304    0.8744    0.2603    0.4938    0.6236
      0.8164    0.3575    0.9750    0.1850    0.1372    0.8960    0.3341
      0.7883    0.7055    0.8298    0.0739    0.8678    0.4123    0.9300
      0.6704    0.4151    0.6862    0.9805    0.5785    0.4191    0.2857
      0.9859    0.5860    0.8318    0.9439    0.6984    0.5841    0.2015
      0.8851    0.1507    0.5597    0.4923    0.6922    0.4589    0.0906
      0.7329    0.6443    0.0235    0.3665    0.1522    0.7261    0.0117
   

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
      0.1583    0.2330    0.9165    0.0736
   
   R2 = 
      0.5448    0.0189    0.1916    0.7778    0.8204
   
   R3 = 
      0.1583    0.2330    0.9165    0.0736    0.5448    0.0189    0.1916    0.7778    0.8204
   
   C1 = 
      0.3666
      0.0008
      0.7644
      0.8829
      0.3895
      0.8684
      0.1860
      0.8131
      0.1527
      0.4202
   
   C2 = 
      0.0898
      0.4921
      0.5773
      0.3759
      0.6056
      0.9756
      0.3575
      0.3280
      0.0121
      0.2391
   
   M = 
      0.3666    0.0898
      0.0008    0.4921
      0.7644    0.5773
      0.8829    0.3759
      0.3895    0.6056
      0.8684    0.9756
      0.1860    0.3575
      0.8131    0.3280
      0.1527    0.0121
      0.4202    0.2391
   


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
      0.7846    0.2018    0.0530    0.4050
   
   R2 = 
      0.9559    0.5297    0.5162    0.4346
   
   M = 
      0.7846    0.2018    0.0530    0.4050
      0.9559    0.5297    0.5162    0.4346
   
   C1 = 
      0.2424
      0.4875
      0.4668
      0.0855
      0.0276
      0.8606
      0.9489
      0.4610
      0.2679
      0.0394
   
   C2 = 
      0.5357
      0.2447
   
   C3 = 
      0.2424
      0.4875
      0.4668
      0.0855
      0.0276
      0.8606
      0.9489
      0.4610
      0.2679
      0.0394
      0.5357
      0.2447
   

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
   

