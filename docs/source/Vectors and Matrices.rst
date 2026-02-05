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
      0.1253    0.8567    0.7274    0.5769    0.4104    0.6581    0.1962
   
   C = 
      0.3606
      0.6075
      0.0983
      0.7324
      0.3861
   
   M = 
      0.0018    0.3312    0.5766    0.9491    0.6429    0.2802    0.8348
      0.1873    0.2502    0.8644    0.0704    0.0524    0.5239    0.8342
      0.3497    0.6576    0.0864    0.5191    0.0417    0.1981    0.1548
      0.2515    0.6167    0.1991    0.3322    0.3473    0.6293    0.0713
      0.9753    0.2379    0.9550    0.8898    0.9478    0.9809    0.1790
      0.7130    0.6197    0.8487    0.0915    0.3056    0.1796    0.6846
      0.9827    0.7842    0.3138    0.8534    0.5206    0.5644    0.1064
      0.3554    0.7072    0.5626    0.0685    0.1612    0.7368    0.5877
   

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
      0.1094    0.3298    0.4818    0.9707
   
   R2 = 
      0.1820    0.2588    0.9018    0.1033    0.7321
   
   R3 = 
      0.1094    0.3298    0.4818    0.9707    0.1820    0.2588    0.9018    0.1033    0.7321
   
   C1 = 
      0.5115
      0.2460
      0.1523
      0.1858
      0.9512
      0.5463
      0.1122
      0.6328
      0.8010
      0.0318
   
   C2 = 
      0.1003
      0.7026
      0.0600
      0.1012
      0.6474
      0.2345
      0.9718
      0.7911
      0.1484
      0.4896
   
   M = 
      0.5115    0.1003
      0.2460    0.7026
      0.1523    0.0600
      0.1858    0.1012
      0.9512    0.6474
      0.5463    0.2345
      0.1122    0.9718
      0.6328    0.7911
      0.8010    0.1484
      0.0318    0.4896
   


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
      0.1028    0.6735    0.1050    0.0610
   
   R2 = 
      0.3984    0.4164    0.0780    0.7170
   
   M = 
      0.1028    0.6735    0.1050    0.0610
      0.3984    0.4164    0.0780    0.7170
   
   C1 = 
      0.4762
      0.9747
      0.8873
      0.6888
      0.1883
      0.1658
      0.4931
      0.4545
      0.6390
      0.2314
   
   C2 = 
      0.2510
      0.7057
   
   C3 = 
      0.4762
      0.9747
      0.8873
      0.6888
      0.1883
      0.1658
      0.4931
      0.4545
      0.6390
      0.2314
      0.2510
      0.7057
   

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
   

