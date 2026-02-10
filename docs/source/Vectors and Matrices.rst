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
      0.5785    0.7782    0.7676    0.5969    0.7677    0.8730    0.0430
   
   C = 
      0.8209
      0.0692
      0.7181
      0.6337
      0.1996
   
   M = 
      0.3091    0.3265    0.3635    0.6868    0.6552    0.4192    0.1517
      0.6851    0.9833    0.4929    0.0949    0.4963    0.7907    0.6443
      0.3411    0.5536    0.8024    0.0041    0.3103    0.2750    0.9299
      0.3691    0.5077    0.7142    0.9281    0.2712    0.8743    0.7686
      0.4376    0.2617    0.2290    0.0117    0.5560    0.2213    0.2348
      0.8454    0.1281    0.0537    0.8599    0.1090    0.5366    0.8905
      0.9489    0.0814    0.2164    0.6639    0.4208    0.1732    0.6052
      0.6219    0.0748    0.6856    0.8299    0.5698    0.6226    0.2193
   

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
      0.6420    0.0913    0.0753    0.5550
   
   R2 = 
      0.2708    0.3552    0.0667    0.1850    0.0102
   
   R3 = 
      0.6420    0.0913    0.0753    0.5550    0.2708    0.3552    0.0667    0.1850    0.0102
   
   C1 = 
      0.8295
      0.4053
      0.1398
      0.8244
      0.6043
      0.4684
      0.4386
      0.4031
      0.5408
      0.9744
   
   C2 = 
      0.7343
      0.5238
      0.4422
      0.9320
      0.9167
      0.0021
      0.5620
      0.2186
      0.3189
      0.5299
   
   M = 
      0.8295    0.7343
      0.4053    0.5238
      0.1398    0.4422
      0.8244    0.9320
      0.6043    0.9167
      0.4684    0.0021
      0.4386    0.5620
      0.4031    0.2186
      0.5408    0.3189
      0.9744    0.5299
   


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
      0.8211    0.2598    0.8705    0.8829
   
   R2 = 
      0.2147    0.2421    0.8324    0.7877
   
   M = 
      0.8211    0.2598    0.8705    0.8829
      0.2147    0.2421    0.8324    0.7877
   
   C1 = 
      0.4994
      0.0677
      0.4722
      0.3957
      0.2122
      0.5359
      0.8864
      0.2728
      0.6579
      0.6966
   
   C2 = 
      0.9840
      0.1390
   
   C3 = 
      0.4994
      0.0677
      0.4722
      0.3957
      0.2122
      0.5359
      0.8864
      0.2728
      0.6579
      0.6966
      0.9840
      0.1390
   

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
   

