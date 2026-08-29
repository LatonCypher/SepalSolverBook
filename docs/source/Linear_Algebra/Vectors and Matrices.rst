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
      0.2488    0.6732    0.2097    0.7198    0.6386    0.0799    0.6921
   
   C = 
      0.8695
      0.1094
      0.1423
      0.5473
      0.5651
   
   M = 
      0.2187    0.6802    0.6611    0.7269    0.7545    0.6655    0.4264
      0.0775    0.3509    0.4354    0.5614    0.4703    0.1516    0.9262
      0.5018    0.3394    0.6449    0.2406    0.2373    0.7641    0.9545
      0.9007    0.4564    0.7479    0.2881    0.1859    0.4580    0.4792
      0.0134    0.3794    0.4091    0.1775    0.9060    0.3745    0.7340
      0.0437    0.2276    0.3928    0.9624    0.5484    0.2791    0.5699
      0.1060    0.6272    0.6194    0.4092    0.5803    0.1899    0.9447
      0.9134    0.1556    0.7146    0.6955    0.9785    0.7919    0.4395
   

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
      0.5922    0.9569    0.8036    0.0132
   
   R2 = 
      0.3547    0.4825    0.9567    0.9844    0.1628
   
   R3 = 
      0.5922    0.9569    0.8036    0.0132    0.3547    0.4825    0.9567    0.9844    0.1628
   
   C1 = 
      0.2894
      0.8454
      0.4701
      0.7985
      0.3867
      0.5015
      0.3612
      0.2134
      0.3732
      0.7504
   
   C2 = 
      0.1511
      0.2969
      0.7321
      0.8136
      0.5867
      0.0331
      0.2828
      0.3631
      0.9442
      0.4171
   
   M = 
      0.2894    0.1511
      0.8454    0.2969
      0.4701    0.7321
      0.7985    0.8136
      0.3867    0.5867
      0.5015    0.0331
      0.3612    0.2828
      0.2134    0.3631
      0.3732    0.9442
      0.7504    0.4171
   


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
      0.9896    0.5463    0.5201    0.0533
   
   R2 = 
      0.9910    0.3278    0.7745    0.9454
   
   M = 
      0.9896    0.5463    0.5201    0.0533
      0.9910    0.3278    0.7745    0.9454
   
   C1 = 
      0.7134
      0.4107
      0.9401
      0.5946
      0.2455
      0.7181
      0.0384
      0.0223
      0.1234
      0.0573
   
   C2 = 
      0.8177
      0.0325
   
   C3 = 
      0.7134
      0.4107
      0.9401
      0.5946
      0.2455
      0.7181
      0.0384
      0.0223
      0.1234
      0.0573
      0.8177
      0.0325
   

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
   

