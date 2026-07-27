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
      0.6453    0.1559    0.3297    0.9067    0.5429    0.2817    0.7405
   
   C = 
      0.2113
      0.7783
      0.5975
      0.3732
      0.4991
   
   M = 
      0.4170    0.4953    0.6041    0.7968    0.3760    0.2475    0.2678
      0.5123    0.9817    0.2360    0.5407    0.5470    0.2176    0.1077
      0.6722    0.9267    0.9393    0.7998    0.8501    0.5374    0.3454
      0.9856    0.4224    0.2799    0.0130    0.1580    0.5606    0.5452
      0.9948    0.5160    0.5145    0.1323    0.9460    0.2031    0.1516
      0.5529    0.1312    0.1957    0.4382    0.6699    0.0233    0.3323
      0.9462    0.2600    0.9597    0.8214    0.4526    0.3323    0.5315
      0.1661    0.8374    0.0136    0.7111    0.1234    0.3275    0.9879
   

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
      0.7284    0.4525    0.8831    0.9348
   
   R2 = 
      0.0830    0.5234    0.5006    0.1327    0.2141
   
   R3 = 
      0.7284    0.4525    0.8831    0.9348    0.0830    0.5234    0.5006    0.1327    0.2141
   
   C1 = 
      0.7949
      0.7938
      0.6035
      0.8437
      0.4493
      0.3417
      0.2810
      0.9493
      0.8903
      0.0077
   
   C2 = 
      0.2661
      0.0094
      0.0316
      0.0685
      0.7568
      0.5413
      0.7439
      0.1173
      0.0991
      0.1815
   
   M = 
      0.7949    0.2661
      0.7938    0.0094
      0.6035    0.0316
      0.8437    0.0685
      0.4493    0.7568
      0.3417    0.5413
      0.2810    0.7439
      0.9493    0.1173
      0.8903    0.0991
      0.0077    0.1815
   


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
      0.6104    0.8277    0.1786    0.1494
   
   R2 = 
      0.0693    0.4722    0.7105    0.0516
   
   M = 
      0.6104    0.8277    0.1786    0.1494
      0.0693    0.4722    0.7105    0.0516
   
   C1 = 
      0.0614
      0.1845
      0.9762
      0.7395
      0.4167
      0.5034
      0.6333
      0.3162
      0.0403
      0.2055
   
   C2 = 
      0.8293
      0.3703
   
   C3 = 
      0.0614
      0.1845
      0.9762
      0.7395
      0.4167
      0.5034
      0.6333
      0.3162
      0.0403
      0.2055
      0.8293
      0.3703
   

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
   

