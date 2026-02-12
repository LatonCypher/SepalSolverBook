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
      0.1860    0.5870    0.2352    0.4627    0.4230    0.4840    0.3192
   
   C = 
      0.1933
      0.5283
      0.6838
      0.9387
      0.6748
   
   M = 
      0.5979    0.5428    0.6306    0.3815    0.8590    0.3994    0.2263
      0.3680    0.6047    0.5923    0.0985    0.3074    0.7689    0.9471
      0.4555    0.6646    0.0677    0.4841    0.4525    0.6740    0.0249
      0.7869    0.1046    0.5392    0.0448    0.5984    0.2028    0.4347
      0.3355    0.8418    0.3076    0.6114    0.5708    0.3761    0.6695
      0.2553    0.7187    0.6188    0.3992    0.3720    0.8570    0.2977
      0.0499    0.0469    0.4753    0.3633    0.6938    0.9886    0.7434
      0.9035    0.7854    0.5879    0.5262    0.2971    0.8923    0.1156
   

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
      0.0713    0.2887    0.6971    0.5397
   
   R2 = 
      0.0928    0.4825    0.7604    0.4853    0.6582
   
   R3 = 
      0.0713    0.2887    0.6971    0.5397    0.0928    0.4825    0.7604    0.4853    0.6582
   
   C1 = 
      0.3837
      0.9186
      0.8689
      0.8873
      0.9781
      0.1131
      0.5264
      0.3782
      0.6927
      0.1068
   
   C2 = 
      0.2804
      0.5667
      0.1374
      0.9542
      0.3760
      0.1362
      0.4210
      0.3000
      0.7977
      0.5054
   
   M = 
      0.3837    0.2804
      0.9186    0.5667
      0.8689    0.1374
      0.8873    0.9542
      0.9781    0.3760
      0.1131    0.1362
      0.5264    0.4210
      0.3782    0.3000
      0.6927    0.7977
      0.1068    0.5054
   


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
      0.0047    0.4025    0.4862    0.7542
   
   R2 = 
      0.4473    0.7580    0.9980    0.9382
   
   M = 
      0.0047    0.4025    0.4862    0.7542
      0.4473    0.7580    0.9980    0.9382
   
   C1 = 
      0.4774
      0.5653
      0.3794
      0.1294
      0.4714
      0.4605
      0.0670
      0.3272
      0.3989
      0.0954
   
   C2 = 
      0.3765
      0.3634
   
   C3 = 
      0.4774
      0.5653
      0.3794
      0.1294
      0.4714
      0.4605
      0.0670
      0.3272
      0.3989
      0.0954
      0.3765
      0.3634
   

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
   

