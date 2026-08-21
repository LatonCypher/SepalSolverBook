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
      0.7455    0.6624    0.6251    0.2052    0.7521    0.9578    0.9021
   
   C = 
      0.1059
      0.3000
      0.3930
      0.5447
      0.0855
   
   M = 
      0.7836    0.7710    0.1573    0.2333    0.4767    0.5822    0.7402
      0.4792    0.5060    0.8210    0.0267    0.1016    0.5353    0.9124
      0.1171    0.6265    0.3454    0.0004    0.4371    0.5138    0.5373
      0.3732    0.6297    0.1810    0.7773    0.8544    0.4306    0.6169
      0.8135    0.9381    0.0000    0.3924    0.4994    0.9272    0.5031
      0.0962    0.0334    0.2434    0.0117    0.7451    0.5043    0.9341
      0.7450    0.7992    0.9048    0.2020    0.5210    0.6646    0.5309
      0.4455    0.6507    0.6926    0.4313    0.7853    0.6867    0.3254
   

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
      0.2825    0.2727    0.8346    0.5244
   
   R2 = 
      0.0344    0.6297    0.4120    0.5560    0.3391
   
   R3 = 
      0.2825    0.2727    0.8346    0.5244    0.0344    0.6297    0.4120    0.5560    0.3391
   
   C1 = 
      0.1834
      0.1688
      0.7568
      0.2620
      0.1443
      0.1360
      0.7148
      0.7040
      0.0987
      0.1503
   
   C2 = 
      0.6777
      0.4947
      0.5559
      0.4339
      0.3630
      0.4361
      0.0667
      0.5238
      0.7736
      0.6254
   
   M = 
      0.1834    0.6777
      0.1688    0.4947
      0.7568    0.5559
      0.2620    0.4339
      0.1443    0.3630
      0.1360    0.4361
      0.7148    0.0667
      0.7040    0.5238
      0.0987    0.7736
      0.1503    0.6254
   


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
      0.6450    0.0107    0.3402    0.5499
   
   R2 = 
      0.9215    0.8219    0.3312    0.1066
   
   M = 
      0.6450    0.0107    0.3402    0.5499
      0.9215    0.8219    0.3312    0.1066
   
   C1 = 
      0.9429
      0.8727
      0.6269
      0.7735
      0.9399
      0.0223
      0.5523
      0.1019
      0.2641
      0.5642
   
   C2 = 
      0.2306
      0.3837
   
   C3 = 
      0.9429
      0.8727
      0.6269
      0.7735
      0.9399
      0.0223
      0.5523
      0.1019
      0.2641
      0.5642
      0.2306
      0.3837
   

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
   

