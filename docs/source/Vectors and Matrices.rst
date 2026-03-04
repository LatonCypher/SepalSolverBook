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
      0.8932    0.3363    0.5598    0.7364    0.5077    0.6706    0.8537
   
   C = 
      0.0239
      0.1449
      0.7252
      0.1977
      0.5705
   
   M = 
      0.3560    0.7949    0.6069    0.0921    0.3463    0.8834    0.8378
      0.5842    0.7711    0.4872    0.7866    0.0173    0.4881    0.2971
      0.7997    0.2797    0.7295    0.5366    0.9878    0.1890    0.7521
      0.7894    0.6420    0.6170    0.2044    0.7127    0.5650    0.0255
      0.1042    0.6157    0.0028    0.4534    0.1653    0.8408    0.7740
      0.9049    0.6945    0.2260    0.4792    0.5127    0.9553    0.0711
      0.3236    0.6063    0.2522    0.8993    0.9461    0.4658    0.4542
      0.3626    0.5697    0.0854    0.3294    0.7610    0.9561    0.4659
   

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
      0.7036    0.9124    0.8135    0.5541
   
   R2 = 
      0.8530    0.0285    0.8976    0.9116    0.7852
   
   R3 = 
      0.7036    0.9124    0.8135    0.5541    0.8530    0.0285    0.8976    0.9116    0.7852
   
   C1 = 
      0.1531
      0.5709
      0.2933
      0.3786
      0.2047
      0.0313
      0.1383
      0.2731
      0.5181
      0.5082
   
   C2 = 
      0.8015
      0.4479
      0.7332
      0.9947
      0.3114
      0.1890
      0.3305
      0.6045
      0.4507
      0.7774
   
   M = 
      0.1531    0.8015
      0.5709    0.4479
      0.2933    0.7332
      0.3786    0.9947
      0.2047    0.3114
      0.0313    0.1890
      0.1383    0.3305
      0.2731    0.6045
      0.5181    0.4507
      0.5082    0.7774
   


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
      0.4202    0.3729    0.7543    0.7600
   
   R2 = 
      0.4814    0.8678    0.4178    0.1157
   
   M = 
      0.4202    0.3729    0.7543    0.7600
      0.4814    0.8678    0.4178    0.1157
   
   C1 = 
      0.0394
      0.2638
      0.0662
      0.5141
      0.9796
      0.3403
      0.4687
      0.2176
      0.5119
      0.4271
   
   C2 = 
      0.0757
      0.9488
   
   C3 = 
      0.0394
      0.2638
      0.0662
      0.5141
      0.9796
      0.3403
      0.4687
      0.2176
      0.5119
      0.4271
      0.0757
      0.9488
   

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
   

