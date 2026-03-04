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
      0.8720    0.6392    0.9868    0.5894    0.8587    0.6412    0.4448
   
   C = 
      0.7601
      0.5445
      0.7323
      0.1504
      0.0322
   
   M = 
      0.4658    0.1198    0.7910    0.0755    0.9858    0.8645    0.0241
      0.3481    0.0652    0.9804    0.0384    0.1147    0.8699    0.4919
      0.8142    0.6830    0.1352    0.7142    0.5836    0.5357    0.2836
      0.3171    0.7663    0.9771    0.4344    0.7574    0.4851    0.8215
      0.8397    0.0403    0.4429    0.1671    0.4156    0.7559    0.8616
      0.3268    0.1572    0.6543    0.3931    0.2349    0.0155    0.9676
      0.1545    0.8915    0.1627    0.8647    0.7909    0.6147    0.7514
      0.5669    0.2310    0.5667    0.2304    0.6398    0.1903    0.5541
   

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
      0.5831    0.8349    0.5060    0.7551
   
   R2 = 
      0.2534    0.7020    0.5777    0.3234    0.0498
   
   R3 = 
      0.5831    0.8349    0.5060    0.7551    0.2534    0.7020    0.5777    0.3234    0.0498
   
   C1 = 
      0.4227
      0.9558
      0.2490
      0.2972
      0.3567
      0.7639
      0.9674
      0.2409
      0.6665
      0.4041
   
   C2 = 
      0.6123
      0.6622
      0.8552
      0.4394
      0.5623
      0.1532
      0.6291
      0.5626
      0.3330
      0.8520
   
   M = 
      0.4227    0.6123
      0.9558    0.6622
      0.2490    0.8552
      0.2972    0.4394
      0.3567    0.5623
      0.7639    0.1532
      0.9674    0.6291
      0.2409    0.5626
      0.6665    0.3330
      0.4041    0.8520
   


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
      0.1378    0.0266    0.8992    0.4792
   
   R2 = 
      0.9673    0.0857    0.4924    0.2426
   
   M = 
      0.1378    0.0266    0.8992    0.4792
      0.9673    0.0857    0.4924    0.2426
   
   C1 = 
      0.6819
      0.4359
      0.6227
      0.4639
      0.7787
      0.2460
      0.6909
      0.0034
      0.4440
      0.5948
   
   C2 = 
      0.0459
      0.8797
   
   C3 = 
      0.6819
      0.4359
      0.6227
      0.4639
      0.7787
      0.2460
      0.6909
      0.0034
      0.4440
      0.5948
      0.0459
      0.8797
   

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
   

