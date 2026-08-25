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
      0.0278    0.1983    0.2019    0.2993    0.7669    0.3473    0.2232
   
   C = 
      0.3563
      0.3599
      0.8444
      0.0992
      0.1454
   
   M = 
      0.7114    0.3880    0.8519    0.4577    0.6391    0.6803    0.4213
      0.1621    0.7613    0.0991    0.6209    0.4071    0.6084    0.4774
      0.5780    0.2098    0.6757    0.7879    0.9505    0.7255    0.8209
      0.7000    0.6845    0.6153    0.8240    0.6014    0.8160    0.5448
      0.9562    0.5901    0.4419    0.9938    0.0911    0.2232    0.3314
      0.5107    0.8500    0.2002    0.8318    0.9228    0.0975    0.7602
      0.7454    0.7621    0.4374    0.2185    0.6071    0.0560    0.0124
      0.5650    0.4471    0.1272    0.1358    0.1906    0.6640    0.8513
   

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
      0.3939    0.9191    0.4037    0.4890
   
   R2 = 
      0.7896    0.7491    0.3965    0.6712    0.8669
   
   R3 = 
      0.3939    0.9191    0.4037    0.4890    0.7896    0.7491    0.3965    0.6712    0.8669
   
   C1 = 
      0.3350
      0.9250
      0.2495
      0.6710
      0.3387
      0.6557
      0.6085
      0.1152
      0.8151
      0.4620
   
   C2 = 
      0.9135
      0.6312
      0.8382
      0.4101
      0.7698
      0.5382
      0.0229
      0.4518
      0.5414
      0.4871
   
   M = 
      0.3350    0.9135
      0.9250    0.6312
      0.2495    0.8382
      0.6710    0.4101
      0.3387    0.7698
      0.6557    0.5382
      0.6085    0.0229
      0.1152    0.4518
      0.8151    0.5414
      0.4620    0.4871
   


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
      0.5876    0.4330    0.0052    0.3147
   
   R2 = 
      0.2295    0.2035    0.9048    0.2836
   
   M = 
      0.5876    0.4330    0.0052    0.3147
      0.2295    0.2035    0.9048    0.2836
   
   C1 = 
      0.6119
      0.4623
      0.9948
      0.5693
      0.4779
      0.2908
      0.7092
      0.9420
      0.4762
      0.6935
   
   C2 = 
      0.8036
      0.0451
   
   C3 = 
      0.6119
      0.4623
      0.9948
      0.5693
      0.4779
      0.2908
      0.7092
      0.9420
      0.4762
      0.6935
      0.8036
      0.0451
   

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
   

