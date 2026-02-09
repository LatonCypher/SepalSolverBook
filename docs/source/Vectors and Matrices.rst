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
      0.5043    0.2403    0.2736    0.3908    0.6728    0.2082    0.3871
   
   C = 
      0.2831
      0.2682
      0.3156
      0.7743
      0.7079
   
   M = 
      0.6474    0.7003    0.4743    0.1161    0.9119    0.0118    0.8366
      0.8639    0.2210    0.2688    0.8405    0.4485    0.3683    0.8113
      0.4348    0.7146    0.3135    0.4467    0.1021    0.7922    0.9091
      0.5302    0.3916    0.3281    0.6157    0.4167    0.3700    0.4443
      0.4572    0.7301    0.9820    0.2107    0.9472    0.3292    0.9710
      0.1874    0.6422    0.5012    0.6208    0.1830    0.6546    0.1455
      0.5432    0.3889    0.7008    0.9851    0.8677    0.1740    0.6390
      0.6400    0.1138    0.1182    0.7463    0.2330    0.0544    0.9230
   

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
      0.3176    0.6005    0.2630    0.3275
   
   R2 = 
      0.3235    0.6799    0.1505    0.9895    0.0512
   
   R3 = 
      0.3176    0.6005    0.2630    0.3275    0.3235    0.6799    0.1505    0.9895    0.0512
   
   C1 = 
      0.9672
      0.2281
      0.2906
      0.0728
      0.6686
      0.2790
      0.4149
      0.0126
      0.3422
      0.7427
   
   C2 = 
      0.1749
      0.2021
      0.2969
      0.2073
      0.4188
      0.0797
      0.1995
      0.2100
      0.9446
      0.5245
   
   M = 
      0.9672    0.1749
      0.2281    0.2021
      0.2906    0.2969
      0.0728    0.2073
      0.6686    0.4188
      0.2790    0.0797
      0.4149    0.1995
      0.0126    0.2100
      0.3422    0.9446
      0.7427    0.5245
   


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
      0.2954    0.8643    0.9670    0.0690
   
   R2 = 
      0.0983    0.9749    0.7833    0.9775
   
   M = 
      0.2954    0.8643    0.9670    0.0690
      0.0983    0.9749    0.7833    0.9775
   
   C1 = 
      0.9456
      0.5773
      0.9992
      0.0088
      0.4047
      0.4106
      0.9668
      0.2039
      0.6127
      0.5139
   
   C2 = 
      0.1800
      0.5939
   
   C3 = 
      0.9456
      0.5773
      0.9992
      0.0088
      0.4047
      0.4106
      0.9668
      0.2039
      0.6127
      0.5139
      0.1800
      0.5939
   

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
   

