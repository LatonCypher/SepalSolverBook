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
      0.7834    0.1877    0.2840    0.5012    0.2048    0.1246    0.2821
   
   C = 
      0.5860
      0.7158
      0.7948
      0.5806
      0.0020
   
   M = 
      0.3395    0.5577    0.6207    0.3851    0.6563    0.3819    0.7827
      0.2166    0.6771    0.9954    0.5717    0.4024    0.9268    0.9460
      0.2999    0.3181    0.9108    0.8671    0.0469    0.0257    0.7462
      0.8310    0.1776    0.0075    0.8369    0.7419    0.9119    0.8072
      0.3500    0.6237    0.3111    0.0606    0.5039    0.1286    0.5320
      0.0566    0.7589    0.2530    0.3241    0.0410    0.0632    0.5687
      0.3483    0.3785    0.1414    0.2777    0.4327    0.2433    0.6601
      0.7700    0.9670    0.7864    0.5891    0.4804    0.9264    0.6938
   

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
      0.5309    0.6008    0.6623    0.4361
   
   R2 = 
      0.3719    0.4891    0.7314    0.1332    0.1726
   
   R3 = 
      0.5309    0.6008    0.6623    0.4361    0.3719    0.4891    0.7314    0.1332    0.1726
   
   C1 = 
      0.2887
      0.5123
      0.8476
      0.3674
      0.7300
      0.3602
      0.7245
      0.2239
      0.4949
      0.2419
   
   C2 = 
      0.0192
      0.4705
      0.8449
      0.0376
      0.0109
      0.7618
      0.9086
      0.7734
      0.7139
      0.5467
   
   M = 
      0.2887    0.0192
      0.5123    0.4705
      0.8476    0.8449
      0.3674    0.0376
      0.7300    0.0109
      0.3602    0.7618
      0.7245    0.9086
      0.2239    0.7734
      0.4949    0.7139
      0.2419    0.5467
   


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
      0.7395    0.5645    0.9727    0.7318
   
   R2 = 
      0.7286    0.3358    0.0706    0.7042
   
   M = 
      0.7395    0.5645    0.9727    0.7318
      0.7286    0.3358    0.0706    0.7042
   
   C1 = 
      0.1674
      0.9110
      0.9438
      0.7851
      0.5433
      0.0133
      0.7365
      0.7049
      0.3755
      0.9144
   
   C2 = 
      0.6152
      0.7996
   
   C3 = 
      0.1674
      0.9110
      0.9438
      0.7851
      0.5433
      0.0133
      0.7365
      0.7049
      0.3755
      0.9144
      0.6152
      0.7996
   

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
   

