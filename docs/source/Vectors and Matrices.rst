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
      0.1396    0.5228    0.5336    0.6827    0.7280    0.2326    0.7912
   
   C = 
      0.2212
      0.2848
      0.0623
      0.9976
      0.3340
   
   M = 
      0.6088    0.0906    0.0702    0.9923    0.6660    0.0212    0.9243
      0.6815    0.2230    0.8000    0.7931    0.4366    0.4139    0.6834
      0.1839    0.2553    0.9611    0.5591    0.6338    0.1451    0.4031
      0.2415    0.4423    0.1478    0.4487    0.2130    0.7504    0.6969
      0.9987    0.5413    0.1725    0.0608    0.1316    0.9717    0.7143
      0.0045    0.7444    0.6373    0.4787    0.6182    0.8953    0.2090
      0.1737    0.6654    0.8964    0.8558    0.2564    0.9092    0.4301
      0.5576    0.2906    0.4793    0.9972    0.0194    0.3203    0.4833
   

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
      0.7721    0.6269    0.6879    0.3071
   
   R2 = 
      0.6635    0.7302    0.1186    0.4483    0.2504
   
   R3 = 
      0.7721    0.6269    0.6879    0.3071    0.6635    0.7302    0.1186    0.4483    0.2504
   
   C1 = 
      0.0137
      0.6470
      0.6452
      0.6952
      0.1934
      0.3267
      0.9710
      0.7023
      0.2290
      0.3102
   
   C2 = 
      0.4329
      0.4980
      0.5930
      0.6739
      0.4477
      0.9069
      0.0827
      0.5981
      0.2280
      0.2369
   
   M = 
      0.0137    0.4329
      0.6470    0.4980
      0.6452    0.5930
      0.6952    0.6739
      0.1934    0.4477
      0.3267    0.9069
      0.9710    0.0827
      0.7023    0.5981
      0.2290    0.2280
      0.3102    0.2369
   


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
      0.7386    0.0426    0.0681    0.6997
   
   R2 = 
      0.7191    0.2318    0.5652    0.9690
   
   M = 
      0.7386    0.0426    0.0681    0.6997
      0.7191    0.2318    0.5652    0.9690
   
   C1 = 
      0.8286
      0.3308
      0.7325
      0.7466
      0.0134
      0.1676
      0.7724
      0.9838
      0.5506
      0.6185
   
   C2 = 
      0.5273
      0.5788
   
   C3 = 
      0.8286
      0.3308
      0.7325
      0.7466
      0.0134
      0.1676
      0.7724
      0.9838
      0.5506
      0.6185
      0.5273
      0.5788
   

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
   

