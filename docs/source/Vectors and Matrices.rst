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
      0.6034    0.5736    0.2321    0.4871    0.1184    0.6752    0.3331
   
   C = 
      0.2146
      0.4921
      0.7177
      0.5283
      0.9773
   
   M = 
      0.9636    0.1163    0.0996    0.9692    0.0232    0.5129    0.7584
      0.1463    0.3033    0.9469    0.9942    0.8126    0.9126    0.8812
      0.9864    0.2841    0.7611    0.9589    0.6479    0.5474    0.4261
      0.0360    0.0565    0.0033    0.1250    0.5807    0.6819    0.0573
      0.4891    0.6282    0.3031    0.1052    0.0271    0.6667    0.2903
      0.1963    0.6818    0.1515    0.8615    0.2730    0.5726    0.3596
      0.1930    0.1503    0.8545    0.9086    0.0793    0.8406    0.6430
      0.8821    0.7666    0.2622    0.3247    0.0269    0.8491    0.7075
   

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
      0.3046    0.4105    0.2916    0.9888
   
   R2 = 
      0.7607    0.8897    0.8491    0.7407    0.4796
   
   R3 = 
      0.3046    0.4105    0.2916    0.9888    0.7607    0.8897    0.8491    0.7407    0.4796
   
   C1 = 
      0.1137
      0.3479
      0.6752
      0.3239
      0.9639
      0.7407
      0.5932
      0.2752
      0.0436
      0.8412
   
   C2 = 
      0.5344
      0.8533
      0.5818
      0.5139
      0.3844
      0.1713
      0.9268
      0.9566
      0.4256
      0.7225
   
   M = 
      0.1137    0.5344
      0.3479    0.8533
      0.6752    0.5818
      0.3239    0.5139
      0.9639    0.3844
      0.7407    0.1713
      0.5932    0.9268
      0.2752    0.9566
      0.0436    0.4256
      0.8412    0.7225
   


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
      0.2823    0.1858    0.6532    0.8754
   
   R2 = 
      0.8027    0.2729    0.1382    0.4091
   
   M = 
      0.2823    0.1858    0.6532    0.8754
      0.8027    0.2729    0.1382    0.4091
   
   C1 = 
      0.0735
      0.8787
      0.5521
      0.6268
      0.7230
      0.4931
      0.9571
      0.1507
      0.7661
      0.0348
   
   C2 = 
      0.2816
      0.8588
   
   C3 = 
      0.0735
      0.8787
      0.5521
      0.6268
      0.7230
      0.4931
      0.9571
      0.1507
      0.7661
      0.0348
      0.2816
      0.8588
   

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
   

