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
      0.8041    0.3422    0.1921    0.4348    0.7593    0.7328    0.9370
   
   C = 
      0.5873
      0.4362
      0.6929
      0.7954
      0.4677
   
   M = 
      0.2869    0.7289    0.1977    0.1617    0.2248    0.1588    0.5954
      0.5210    0.6267    0.7490    0.6218    0.1698    0.7062    0.5318
      0.4551    0.8846    0.4340    0.5336    0.7744    0.9609    0.0239
      0.7240    0.9248    0.7601    0.9022    0.8322    0.5651    0.0514
      0.2897    0.4728    0.4049    0.8551    0.8413    0.1940    0.1659
      0.7852    0.0957    0.5093    0.2472    0.6573    0.3062    0.9066
      0.7098    0.7407    0.0078    0.5891    0.6997    0.3633    0.6043
      0.2164    0.7611    0.2556    0.7247    0.4556    0.4810    0.1526
   

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
      0.2616    0.3413    0.8414    0.1539
   
   R2 = 
      0.1678    0.7814    0.0614    0.4251    0.3287
   
   R3 = 
      0.2616    0.3413    0.8414    0.1539    0.1678    0.7814    0.0614    0.4251    0.3287
   
   C1 = 
      0.4050
      0.0911
      0.5920
      0.2602
      0.8302
      0.5227
      0.9490
      0.1886
      0.7550
      0.8634
   
   C2 = 
      0.4242
      0.9736
      0.5875
      0.5982
      0.6779
      0.6743
      0.2032
      0.7392
      0.8530
      0.7316
   
   M = 
      0.4050    0.4242
      0.0911    0.9736
      0.5920    0.5875
      0.2602    0.5982
      0.8302    0.6779
      0.5227    0.6743
      0.9490    0.2032
      0.1886    0.7392
      0.7550    0.8530
      0.8634    0.7316
   


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
      0.6904    0.1928    0.1503    0.5708
   
   R2 = 
      0.0847    0.3656    0.7193    0.2870
   
   M = 
      0.6904    0.1928    0.1503    0.5708
      0.0847    0.3656    0.7193    0.2870
   
   C1 = 
      0.4238
      0.8955
      0.4894
      0.2534
      0.8061
      0.3543
      0.0739
      0.4281
      0.0258
      0.2418
   
   C2 = 
      0.4673
      0.6279
   
   C3 = 
      0.4238
      0.8955
      0.4894
      0.2534
      0.8061
      0.3543
      0.0739
      0.4281
      0.0258
      0.2418
      0.4673
      0.6279
   

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
   

