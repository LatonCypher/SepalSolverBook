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
      0.5056    0.5898    0.4660    0.2320    0.1605    0.5277    0.1453
   
   C = 
      0.3049
      0.5354
      0.8839
      0.2673
      0.8313
   
   M = 
      0.8193    0.6149    0.3088    0.5233    0.7421    0.1701    0.1244
      0.1391    0.0827    0.7277    0.7620    0.2295    0.7186    0.2370
      0.6310    0.2295    0.3740    0.9053    0.8150    0.6847    0.6708
      0.9325    0.3600    0.2096    0.3089    0.2818    0.1988    0.2243
      0.8950    0.8415    0.9357    0.4052    0.1098    0.2686    0.0236
      0.5141    0.9533    0.0332    0.2375    0.6804    0.1972    0.6055
      0.6065    0.3218    0.6578    0.0335    0.1545    0.1077    0.3667
      0.7122    0.7524    0.1182    0.7869    0.6871    0.7099    0.2172
   

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
      0.2409    0.1243    0.1607    0.2689
   
   R2 = 
      0.8460    0.1416    0.4650    0.1349    0.8511
   
   R3 = 
      0.2409    0.1243    0.1607    0.2689    0.8460    0.1416    0.4650    0.1349    0.8511
   
   C1 = 
      0.9051
      0.0236
      0.8291
      0.0801
      0.3425
      0.5650
      0.7112
      0.1973
      0.0657
      0.1374
   
   C2 = 
      0.1629
      0.7987
      0.6567
      0.3937
      0.1338
      0.0227
      0.6930
      0.0202
      0.5115
      0.9373
   
   M = 
      0.9051    0.1629
      0.0236    0.7987
      0.8291    0.6567
      0.0801    0.3937
      0.3425    0.1338
      0.5650    0.0227
      0.7112    0.6930
      0.1973    0.0202
      0.0657    0.5115
      0.1374    0.9373
   


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
      0.4381    0.3918    0.6334    0.5179
   
   R2 = 
      0.6602    0.6940    0.9426    0.9678
   
   M = 
      0.4381    0.3918    0.6334    0.5179
      0.6602    0.6940    0.9426    0.9678
   
   C1 = 
      0.4932
      0.3617
      0.8326
      0.4821
      0.3971
      0.4885
      0.3934
      0.9782
      0.1171
      0.4486
   
   C2 = 
      0.4864
      0.6574
   
   C3 = 
      0.4932
      0.3617
      0.8326
      0.4821
      0.3971
      0.4885
      0.3934
      0.9782
      0.1171
      0.4486
      0.4864
      0.6574
   

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
   

