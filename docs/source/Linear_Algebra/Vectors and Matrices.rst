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
      0.4792    0.8449    0.4485    0.3243    0.0943    0.3003    0.4192
   
   C = 
      0.0456
      0.4836
      0.1259
      0.7394
      0.2493
   
   M = 
      0.0383    0.7977    0.6443    0.1980    0.2338    0.1473    0.4776
      0.4302    0.1493    0.3775    0.4864    0.7342    0.8781    0.9023
      0.2715    0.9058    0.7471    0.3919    0.4674    0.0790    0.4569
      0.0315    0.4547    0.2771    0.4454    0.9052    0.7634    0.9553
      0.6507    0.3113    0.6613    0.6156    0.2499    0.6474    0.8181
      0.5115    0.4641    0.0450    0.8878    0.8072    0.6170    0.7523
      0.7242    0.9051    0.1725    0.0151    0.8197    0.2563    0.1505
      0.9047    0.5304    0.8641    0.3862    0.5664    0.8487    0.1465
   

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
      0.1482    0.8805    0.2629    0.4972
   
   R2 = 
      0.1734    0.0179    0.1194    0.1938    0.8898
   
   R3 = 
      0.1482    0.8805    0.2629    0.4972    0.1734    0.0179    0.1194    0.1938    0.8898
   
   C1 = 
      0.1674
      0.8963
      0.5778
      0.6303
      0.2979
      0.5939
      0.7858
      0.6576
      0.8259
      0.5258
   
   C2 = 
      0.3608
      0.8144
      0.2652
      0.2591
      0.5351
      0.7378
      0.1224
      0.3258
      0.2537
      0.6086
   
   M = 
      0.1674    0.3608
      0.8963    0.8144
      0.5778    0.2652
      0.6303    0.2591
      0.2979    0.5351
      0.5939    0.7378
      0.7858    0.1224
      0.6576    0.3258
      0.8259    0.2537
      0.5258    0.6086
   


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
      0.5995    0.9707    0.0386    0.7871
   
   R2 = 
      0.5307    0.2171    0.0956    0.3729
   
   M = 
      0.5995    0.9707    0.0386    0.7871
      0.5307    0.2171    0.0956    0.3729
   
   C1 = 
      0.6092
      0.4380
      0.1246
      0.1773
      0.5424
      0.3644
      0.4682
      0.0140
      0.1058
      0.3770
   
   C2 = 
      0.5214
      0.0558
   
   C3 = 
      0.6092
      0.4380
      0.1246
      0.1773
      0.5424
      0.3644
      0.4682
      0.0140
      0.1058
      0.3770
      0.5214
      0.0558
   

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
   

