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
      0.2921    0.8296    0.5454    0.1841    0.9504    0.1852    0.5567
   
   C = 
      0.6511
      0.8787
      0.0897
      0.5680
      0.1147
   
   M = 
      0.0206    0.7611    0.6278    0.6648    0.4093    0.8212    0.4312
      0.9356    0.1890    0.6228    0.5138    0.9914    0.6944    0.1061
      0.6083    0.5475    0.9847    0.6835    0.4506    0.3715    0.8441
      0.2256    0.9269    0.8692    0.3952    0.5103    0.6940    0.8651
      0.0815    0.6646    0.3287    0.4199    0.4732    0.1846    0.0166
      0.2491    0.8631    0.3702    0.3206    0.6226    0.9057    0.1467
      0.8931    0.5512    0.3899    0.1482    0.4095    0.7038    0.3559
      0.0975    0.0139    0.1547    0.0206    0.2359    0.5060    0.2366
   

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
      0.6078    0.5284    0.0165    0.3344
   
   R2 = 
      0.1503    0.9460    0.2958    0.7724    0.2941
   
   R3 = 
      0.6078    0.5284    0.0165    0.3344    0.1503    0.9460    0.2958    0.7724    0.2941
   
   C1 = 
      0.7324
      0.3956
      0.0196
      0.8161
      0.7051
      0.1031
      0.8282
      0.8432
      0.5526
      0.6769
   
   C2 = 
      0.8391
      0.9819
      0.3889
      0.8871
      0.1270
      0.1108
      0.7025
      0.2478
      0.9558
      0.5730
   
   M = 
      0.7324    0.8391
      0.3956    0.9819
      0.0196    0.3889
      0.8161    0.8871
      0.7051    0.1270
      0.1031    0.1108
      0.8282    0.7025
      0.8432    0.2478
      0.5526    0.9558
      0.6769    0.5730
   


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
      0.7445    0.7427    0.3553    0.2611
   
   R2 = 
      0.2928    0.2986    0.6663    0.0412
   
   M = 
      0.7445    0.7427    0.3553    0.2611
      0.2928    0.2986    0.6663    0.0412
   
   C1 = 
      0.5715
      0.1046
      0.1893
      0.6700
      0.5441
      0.0810
      0.6374
      0.4040
      0.9553
      0.7206
   
   C2 = 
      0.4164
      0.0743
   
   C3 = 
      0.5715
      0.1046
      0.1893
      0.6700
      0.5441
      0.0810
      0.6374
      0.4040
      0.9553
      0.7206
      0.4164
      0.0743
   

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
   

