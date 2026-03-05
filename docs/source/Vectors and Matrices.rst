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
      0.7680    0.7887    0.6955    0.0534    0.8571    0.4112    0.4804
   
   C = 
      0.3447
      0.8420
      0.9667
      0.6472
      0.6725
   
   M = 
      0.4366    0.6623    0.3451    0.2982    0.5692    0.3992    0.2901
      0.9092    0.7048    0.8831    0.1528    0.3500    0.3269    0.9841
      0.8088    0.7010    0.9760    0.6724    0.7156    0.2763    0.5831
      0.3072    0.1265    0.8404    0.3012    0.3144    0.1080    0.3467
      0.2397    0.8555    0.5360    0.8863    0.2128    0.8395    0.8164
      0.1747    0.3669    0.8147    0.9346    0.1819    0.9925    0.7816
      0.0675    0.5931    0.0679    0.1211    0.1579    0.3681    0.0923
      0.6720    0.5023    0.1297    0.3466    0.7148    0.3442    0.5298
   

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
      0.5474    0.7340    0.1207    0.0628
   
   R2 = 
      0.5214    0.8066    0.9690    0.4758    0.7980
   
   R3 = 
      0.5474    0.7340    0.1207    0.0628    0.5214    0.8066    0.9690    0.4758    0.7980
   
   C1 = 
      0.3852
      0.3998
      0.8937
      0.6908
      0.3267
      0.8116
      0.2365
      0.6511
      0.4396
      0.9676
   
   C2 = 
      0.7083
      0.4501
      0.4790
      0.1799
      0.6291
      0.9843
      0.8975
      0.9715
      0.3950
      0.4295
   
   M = 
      0.3852    0.7083
      0.3998    0.4501
      0.8937    0.4790
      0.6908    0.1799
      0.3267    0.6291
      0.8116    0.9843
      0.2365    0.8975
      0.6511    0.9715
      0.4396    0.3950
      0.9676    0.4295
   


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
      0.2003    0.9538    0.5126    0.5981
   
   R2 = 
      0.8760    0.7974    0.3403    0.8204
   
   M = 
      0.2003    0.9538    0.5126    0.5981
      0.8760    0.7974    0.3403    0.8204
   
   C1 = 
      0.7097
      0.6183
      0.1172
      0.0991
      0.4525
      0.0385
      0.2120
      0.4263
      0.2779
      0.5021
   
   C2 = 
      0.9389
      0.6834
   
   C3 = 
      0.7097
      0.6183
      0.1172
      0.0991
      0.4525
      0.0385
      0.2120
      0.4263
      0.2779
      0.5021
      0.9389
      0.6834
   

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
   

