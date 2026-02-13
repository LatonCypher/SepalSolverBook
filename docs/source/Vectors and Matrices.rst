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
      0.7182    0.6327    0.7408    0.7536    0.3111    0.7788    0.8655
   
   C = 
      0.8930
      0.6728
      0.8991
      0.5850
      0.2193
   
   M = 
      0.4541    0.2073    0.0745    0.4653    0.0247    0.4370    0.7333
      0.9076    0.7544    0.0885    0.9502    0.6627    0.6314    0.1537
      0.4542    0.9767    0.5340    0.4391    0.6540    0.1360    0.2027
      0.0862    0.5879    0.0095    0.5493    0.2938    0.8748    0.4873
      0.4632    0.4959    0.3507    0.0805    0.0073    0.2578    0.0735
      0.0222    0.5365    0.2608    0.2073    0.7992    0.3242    0.0535
      0.0607    0.8016    0.2383    0.8594    0.9381    0.4285    0.7892
      0.5805    0.3746    0.9031    0.2554    0.1090    0.6851    0.4593
   

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
      0.3951    0.3811    0.3156    0.4667
   
   R2 = 
      0.3073    0.5808    0.2750    0.2081    0.6015
   
   R3 = 
      0.3951    0.3811    0.3156    0.4667    0.3073    0.5808    0.2750    0.2081    0.6015
   
   C1 = 
      0.8418
      0.9944
      0.0337
      0.2614
      0.0954
      0.3625
      0.2079
      0.7060
      0.1391
      0.3577
   
   C2 = 
      0.4684
      0.5178
      0.7122
      0.3704
      0.3384
      0.8432
      0.6815
      0.3922
      0.5064
      0.0601
   
   M = 
      0.8418    0.4684
      0.9944    0.5178
      0.0337    0.7122
      0.2614    0.3704
      0.0954    0.3384
      0.3625    0.8432
      0.2079    0.6815
      0.7060    0.3922
      0.1391    0.5064
      0.3577    0.0601
   


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
      0.3338    0.0939    0.0799    0.9071
   
   R2 = 
      0.2962    0.0079    0.3632    0.0165
   
   M = 
      0.3338    0.0939    0.0799    0.9071
      0.2962    0.0079    0.3632    0.0165
   
   C1 = 
      0.9067
      0.0628
      0.6402
      0.0124
      0.2980
      0.7871
      0.5766
      0.4176
      0.7556
      0.1452
   
   C2 = 
      0.1435
      0.6272
   
   C3 = 
      0.9067
      0.0628
      0.6402
      0.0124
      0.2980
      0.7871
      0.5766
      0.4176
      0.7556
      0.1452
      0.1435
      0.6272
   

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
   

