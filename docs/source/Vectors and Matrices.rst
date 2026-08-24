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
      0.8893    0.7344    0.9073    0.9454    0.1296    0.1850    0.3133
   
   C = 
      0.8160
      0.7399
      0.2550
      0.1515
      0.5541
   
   M = 
      0.8141    0.1846    0.0169    0.7143    0.1176    0.4419    0.8571
      0.2681    0.3958    0.8936    0.3965    0.4390    0.5217    0.0509
      0.8097    0.7698    0.0196    0.2492    0.5682    0.9268    0.9692
      0.3426    0.9041    0.5486    0.2157    0.1396    0.7107    0.7091
      0.6549    0.4366    0.1731    0.3341    0.5796    0.8212    0.1519
      0.7609    0.2922    0.8533    0.1864    0.4937    0.3114    0.6966
      0.9622    0.1779    0.6953    0.0071    0.8331    0.8765    0.6154
      0.2220    0.1501    0.5183    0.1201    0.6291    0.0891    0.1779
   

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
      0.5089    0.4783    0.7384    0.7693
   
   R2 = 
      0.7665    0.7922    0.3343    0.5736    0.5819
   
   R3 = 
      0.5089    0.4783    0.7384    0.7693    0.7665    0.7922    0.3343    0.5736    0.5819
   
   C1 = 
      0.5603
      0.0533
      0.3269
      0.7061
      0.8014
      0.3179
      0.9503
      0.7528
      0.7228
      0.7406
   
   C2 = 
      0.3449
      0.8792
      0.4583
      0.3753
      0.3510
      0.7101
      0.1219
      0.5327
      0.9868
      0.2975
   
   M = 
      0.5603    0.3449
      0.0533    0.8792
      0.3269    0.4583
      0.7061    0.3753
      0.8014    0.3510
      0.3179    0.7101
      0.9503    0.1219
      0.7528    0.5327
      0.7228    0.9868
      0.7406    0.2975
   


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
      0.8853    0.2314    0.7826    0.9790
   
   R2 = 
      0.7982    0.9380    0.6096    0.7268
   
   M = 
      0.8853    0.2314    0.7826    0.9790
      0.7982    0.9380    0.6096    0.7268
   
   C1 = 
      0.8494
      0.3607
      0.9064
      0.4482
      0.2850
      0.9885
      0.9195
      0.8265
      0.5822
      0.3183
   
   C2 = 
      0.4935
      0.1906
   
   C3 = 
      0.8494
      0.3607
      0.9064
      0.4482
      0.2850
      0.9885
      0.9195
      0.8265
      0.5822
      0.3183
      0.4935
      0.1906
   

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
   

