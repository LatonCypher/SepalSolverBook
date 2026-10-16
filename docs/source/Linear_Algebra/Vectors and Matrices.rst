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
      0.3470    0.6823    0.6837    0.9499    0.4237    0.1355    0.5521
   
   C = 
      0.3793
      0.1513
      0.3083
      0.8230
      0.8596
   
   M = 
      0.8651    0.1098    0.2873    0.2669    0.1376    0.3010    0.5420
      0.4685    0.4817    0.7983    0.5483    0.3887    0.5150    0.8909
      0.3447    0.0233    0.0513    0.7113    0.3272    0.7095    0.1592
      0.0655    0.0422    0.2373    0.8007    0.9224    0.4629    0.7991
      0.3550    0.0684    0.3542    0.9552    0.2490    0.5183    0.0750
      0.8014    0.2350    0.8480    0.3558    0.2977    0.8178    0.8717
      0.8774    0.9885    0.3345    0.1650    0.8273    0.5631    0.7277
      0.1673    0.5984    0.2374    0.7271    0.3540    0.1631    0.5025
   

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
      0.1069    0.1065    0.1645    0.1493
   
   R2 = 
      0.2160    0.0278    0.9656    0.2541    0.0892
   
   R3 = 
      0.1069    0.1065    0.1645    0.1493    0.2160    0.0278    0.9656    0.2541    0.0892
   
   C1 = 
      0.9834
      0.7255
      0.8070
      0.9634
      0.5531
      0.9607
      0.6416
      0.3350
      0.7410
      0.8187
   
   C2 = 
      0.5141
      0.8815
      0.3675
      0.1513
      0.8399
      0.4919
      0.5008
      0.1790
      0.1404
      0.1722
   
   M = 
      0.9834    0.5141
      0.7255    0.8815
      0.8070    0.3675
      0.9634    0.1513
      0.5531    0.8399
      0.9607    0.4919
      0.6416    0.5008
      0.3350    0.1790
      0.7410    0.1404
      0.8187    0.1722
   


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
      0.8011    0.3728    0.3478    0.6352
   
   R2 = 
      0.2586    0.5965    0.8070    0.6966
   
   M = 
      0.8011    0.3728    0.3478    0.6352
      0.2586    0.5965    0.8070    0.6966
   
   C1 = 
      0.7955
      0.7816
      0.8602
      0.8661
      0.0074
      0.9574
      0.2556
      0.4543
      0.5362
      0.9653
   
   C2 = 
      0.5737
      0.7499
   
   C3 = 
      0.7955
      0.7816
      0.8602
      0.8661
      0.0074
      0.9574
      0.2556
      0.4543
      0.5362
      0.9653
      0.5737
      0.7499
   

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
   

