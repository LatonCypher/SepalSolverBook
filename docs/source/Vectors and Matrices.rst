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
      0.2510    0.7675    0.7911    0.2884    0.1122    0.8176    0.8411
   
   C = 
      0.2477
      0.6874
      0.9581
      0.0716
      0.7882
   
   M = 
      0.9478    0.3038    0.3147    0.5258    0.2325    0.0232    0.6675
      0.8347    0.0135    0.6751    0.5685    0.3362    0.3727    0.6459
      0.7522    0.1696    0.4024    0.4577    0.1418    0.7324    0.8850
      0.8071    0.9284    0.4247    0.5897    0.7297    0.0774    0.5689
      0.4276    0.6344    0.0463    0.8463    0.9136    0.5906    0.3294
      0.0112    0.7153    0.7649    0.6501    0.0074    0.0387    0.1512
      0.8667    0.9479    0.0494    0.1849    0.8457    0.9212    0.4017
      0.5416    0.9061    0.1603    0.2555    0.6368    0.4492    0.2778
   

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
      0.7921    0.6380    0.5013    0.6702
   
   R2 = 
      0.9616    0.3072    0.2206    0.9317    0.2040
   
   R3 = 
      0.7921    0.6380    0.5013    0.6702    0.9616    0.3072    0.2206    0.9317    0.2040
   
   C1 = 
      0.1136
      0.4383
      0.5002
      0.5662
      0.5719
      0.3978
      0.8150
      0.6606
      0.5805
      0.4156
   
   C2 = 
      0.0128
      0.2166
      0.1161
      0.5650
      0.7281
      0.1641
      0.6981
      0.8190
      0.9379
      0.9392
   
   M = 
      0.1136    0.0128
      0.4383    0.2166
      0.5002    0.1161
      0.5662    0.5650
      0.5719    0.7281
      0.3978    0.1641
      0.8150    0.6981
      0.6606    0.8190
      0.5805    0.9379
      0.4156    0.9392
   


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
      0.5094    0.3884    0.0005    0.9746
   
   R2 = 
      0.6322    0.3751    0.0326    0.4654
   
   M = 
      0.5094    0.3884    0.0005    0.9746
      0.6322    0.3751    0.0326    0.4654
   
   C1 = 
      0.4103
      0.9731
      0.1748
      0.6345
      0.7265
      0.0287
      0.3939
      0.2837
      0.0992
      0.8698
   
   C2 = 
      0.3145
      0.8633
   
   C3 = 
      0.4103
      0.9731
      0.1748
      0.6345
      0.7265
      0.0287
      0.3939
      0.2837
      0.0992
      0.8698
      0.3145
      0.8633
   

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
   

