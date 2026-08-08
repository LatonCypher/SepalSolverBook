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
      0.8204    0.9417    0.3198    0.6743    0.0061    0.8709    0.8589
   
   C = 
      0.0325
      0.5209
      0.1896
      0.0022
      0.8862
   
   M = 
      0.7444    0.9055    0.0419    0.3161    0.7908    0.5096    0.5801
      0.5994    0.5943    0.6162    0.2691    0.5950    0.2153    0.3856
      0.1172    0.3206    0.4283    0.5725    0.1645    0.5630    0.6980
      0.1699    0.8425    0.6401    0.2835    0.6930    0.3329    0.0195
      0.7077    0.0637    0.6246    0.4075    0.7016    0.8323    0.4947
      0.3924    0.7125    0.8176    0.6957    0.5250    0.0713    0.4103
      0.5238    0.9088    0.3404    0.7731    0.7343    0.0343    0.5492
      0.2318    0.8999    0.7739    0.5277    0.2501    0.7557    0.3314
   

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
      0.5881    0.1744    0.3191    0.3974
   
   R2 = 
      0.2748    0.0369    0.0725    0.7371    0.7204
   
   R3 = 
      0.5881    0.1744    0.3191    0.3974    0.2748    0.0369    0.0725    0.7371    0.7204
   
   C1 = 
      0.7222
      0.1073
      0.3642
      0.8834
      0.7409
      0.0608
      0.7481
      0.9333
      0.5613
      0.1768
   
   C2 = 
      0.3798
      0.6492
      0.4341
      0.2955
      0.5964
      0.5645
      0.1860
      0.6985
      0.4845
      0.5332
   
   M = 
      0.7222    0.3798
      0.1073    0.6492
      0.3642    0.4341
      0.8834    0.2955
      0.7409    0.5964
      0.0608    0.5645
      0.7481    0.1860
      0.9333    0.6985
      0.5613    0.4845
      0.1768    0.5332
   


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
      0.1838    0.0641    0.5056    0.1040
   
   R2 = 
      0.6787    0.6013    0.3336    0.3546
   
   M = 
      0.1838    0.0641    0.5056    0.1040
      0.6787    0.6013    0.3336    0.3546
   
   C1 = 
      0.8690
      0.5859
      0.9873
      0.1491
      0.5969
      0.5417
      0.1095
      0.0839
      0.2354
      0.2523
   
   C2 = 
      0.4553
      0.7527
   
   C3 = 
      0.8690
      0.5859
      0.9873
      0.1491
      0.5969
      0.5417
      0.1095
      0.0839
      0.2354
      0.2523
      0.4553
      0.7527
   

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
   

