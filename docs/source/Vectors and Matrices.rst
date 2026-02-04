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
      0.3096    0.8210    0.2046    0.8110    0.5011    0.5881    0.2937
   
   C = 
      0.5543
      0.6581
      0.7477
      0.8827
      0.9832
   
   M = 
      0.0386    0.6103    0.1428    0.9698    0.4565    0.8752    0.0774
      0.2889    0.9571    0.6527    0.8409    0.5227    0.7672    0.0572
      0.6215    0.1278    0.4679    0.6928    0.9170    0.8547    0.5758
      0.4910    0.3976    0.7924    0.4307    0.8392    0.5849    0.4188
      0.5919    0.3115    0.3386    0.2956    0.0476    0.5559    0.3261
      0.2292    0.3247    0.9865    0.0349    0.7713    0.0090    0.0540
      0.6443    0.2699    0.8543    0.0975    0.0595    0.9342    0.7896
      0.1917    0.2428    0.4534    0.2043    0.3142    0.8726    0.9729
   

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
      0.1435    0.0216    0.3060    0.1105
   
   R2 = 
      0.6630    0.7691    0.0642    0.6889    0.3562
   
   R3 = 
      0.1435    0.0216    0.3060    0.1105    0.6630    0.7691    0.0642    0.6889    0.3562
   
   C1 = 
      0.6815
      0.0633
      0.7360
      0.1344
      0.6519
      0.6911
      0.5838
      0.2951
      0.0017
      0.3691
   
   C2 = 
      0.6522
      0.8315
      0.6106
      0.1391
      0.6937
      0.4265
      0.2443
      0.2085
      0.7997
      0.7408
   
   M = 
      0.6815    0.6522
      0.0633    0.8315
      0.7360    0.6106
      0.1344    0.1391
      0.6519    0.6937
      0.6911    0.4265
      0.5838    0.2443
      0.2951    0.2085
      0.0017    0.7997
      0.3691    0.7408
   


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
      0.8140    0.3053    0.3428    0.4118
   
   R2 = 
      0.8588    0.4549    0.4916    0.2032
   
   M = 
      0.8140    0.3053    0.3428    0.4118
      0.8588    0.4549    0.4916    0.2032
   
   C1 = 
      0.5804
      0.4642
      0.4069
      0.9187
      0.7194
      0.7701
      0.7648
      0.3535
      0.2668
      0.5106
   
   C2 = 
      0.3335
      0.5947
   
   C3 = 
      0.5804
      0.4642
      0.4069
      0.9187
      0.7194
      0.7701
      0.7648
      0.3535
      0.2668
      0.5106
      0.3335
      0.5947
   

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
   

