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
      0.4277    0.0135    0.1728    0.0193    0.1604    0.1624    0.7193
   
   C = 
      0.5385
      0.2046
      0.9919
      0.7798
      0.7541
   
   M = 
      0.8410    0.3983    0.2653    0.1751    0.7051    0.8770    0.6620
      0.5116    0.3645    0.3655    0.4612    0.8903    0.0258    0.8096
      0.0583    0.3512    0.3823    0.4885    0.1150    0.9817    0.4456
      0.9653    0.4427    0.0698    0.1778    0.0649    0.1042    0.6558
      0.9947    0.5771    0.7731    0.7590    0.6851    0.2623    0.8535
      0.5984    0.1898    0.0351    0.8786    0.4399    0.6648    0.6135
      0.5193    0.9613    0.8553    0.9079    0.9063    0.5430    0.8555
      0.8700    0.2924    0.9650    0.9220    0.2947    0.0207    0.5497
   

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
      0.5212    0.5547    0.6716    0.5456
   
   R2 = 
      0.1569    0.3053    0.6895    0.9380    0.8480
   
   R3 = 
      0.5212    0.5547    0.6716    0.5456    0.1569    0.3053    0.6895    0.9380    0.8480
   
   C1 = 
      0.8465
      0.4376
      0.2617
      0.0594
      0.0242
      0.2599
      0.1088
      0.1643
      0.8123
      0.8876
   
   C2 = 
      0.1347
      0.1758
      0.9151
      0.2688
      0.9881
      0.9347
      0.1514
      0.6739
      0.3696
      0.9056
   
   M = 
      0.8465    0.1347
      0.4376    0.1758
      0.2617    0.9151
      0.0594    0.2688
      0.0242    0.9881
      0.2599    0.9347
      0.1088    0.1514
      0.1643    0.6739
      0.8123    0.3696
      0.8876    0.9056
   


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
      0.2986    0.4743    0.8840    0.9440
   
   R2 = 
      0.0065    0.3111    0.5457    0.3465
   
   M = 
      0.2986    0.4743    0.8840    0.9440
      0.0065    0.3111    0.5457    0.3465
   
   C1 = 
      0.8095
      0.3103
      0.9858
      0.8366
      0.0117
      0.9294
      0.6626
      0.5034
      0.2918
      0.9875
   
   C2 = 
      0.0662
      0.9279
   
   C3 = 
      0.8095
      0.3103
      0.9858
      0.8366
      0.0117
      0.9294
      0.6626
      0.5034
      0.2918
      0.9875
      0.0662
      0.9279
   

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
   

