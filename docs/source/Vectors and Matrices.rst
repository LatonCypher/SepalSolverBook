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
      0.8974    0.3867    0.6240    0.8600    0.0080    0.2496    0.3397
   
   C = 
      0.7589
      0.5832
      0.6294
      0.9273
      0.7931
   
   M = 
      0.9001    0.3929    0.8537    0.1748    0.6118    0.3477    0.2185
      0.4862    0.8091    0.5980    0.6574    0.2223    0.9323    0.1014
      0.4982    0.9328    0.8124    0.8991    0.7977    0.2062    0.3466
      0.0055    0.0524    0.9776    0.1600    0.3628    0.1305    0.9707
      0.9696    0.9579    0.4803    0.0991    0.0363    0.0278    0.7197
      0.6312    0.4318    0.0355    0.9709    0.6203    0.6836    0.2049
      0.9365    0.5381    0.8661    0.3200    0.6336    0.9657    0.5116
      0.4568    0.6497    0.3921    0.9015    0.9816    0.3557    0.5432
   

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
      0.0358    0.7077    0.8991    0.2360
   
   R2 = 
      0.4740    0.2888    0.0642    0.4449    0.0377
   
   R3 = 
      0.0358    0.7077    0.8991    0.2360    0.4740    0.2888    0.0642    0.4449    0.0377
   
   C1 = 
      0.1492
      0.6829
      0.7571
      0.4472
      0.7140
      0.0954
      0.9294
      0.1936
      0.0823
      0.2625
   
   C2 = 
      0.2261
      0.1824
      0.3862
      0.9228
      0.9949
      0.6046
      0.5646
      0.8729
      0.6003
      0.2870
   
   M = 
      0.1492    0.2261
      0.6829    0.1824
      0.7571    0.3862
      0.4472    0.9228
      0.7140    0.9949
      0.0954    0.6046
      0.9294    0.5646
      0.1936    0.8729
      0.0823    0.6003
      0.2625    0.2870
   


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
      0.6030    0.8592    0.4317    0.8939
   
   R2 = 
      0.2606    0.9982    0.8832    0.7367
   
   M = 
      0.6030    0.8592    0.4317    0.8939
      0.2606    0.9982    0.8832    0.7367
   
   C1 = 
      0.0593
      0.1408
      0.8931
      0.7421
      0.4535
      0.0121
      0.3084
      0.4642
      0.4485
      0.2816
   
   C2 = 
      0.6592
      0.5264
   
   C3 = 
      0.0593
      0.1408
      0.8931
      0.7421
      0.4535
      0.0121
      0.3084
      0.4642
      0.4485
      0.2816
      0.6592
      0.5264
   

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
   

