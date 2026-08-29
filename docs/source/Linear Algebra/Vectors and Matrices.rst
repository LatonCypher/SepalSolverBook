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
      0.9654    0.3226    0.8970    0.5406    0.0614    0.0742    0.2438
   
   C = 
      0.0652
      0.1774
      0.8323
      0.1961
      0.7544
   
   M = 
      0.2587    0.3014    0.3388    0.9334    0.3183    0.4308    0.9680
      0.2607    0.8926    0.3213    0.0935    0.7260    0.5041    0.7306
      0.4113    0.1747    0.4775    0.7599    0.2526    0.3062    0.8205
      0.3024    0.1556    0.9476    0.8085    0.9984    0.9643    0.5696
      0.1816    0.5826    0.3952    0.3344    0.7975    0.5270    0.5212
      0.8153    0.9242    0.1418    0.0068    0.7879    0.5516    0.4994
      0.1472    0.9800    0.7081    0.4273    0.5806    0.8229    0.5335
      0.1024    0.7420    0.6099    0.6797    0.2169    0.4193    0.5335
   

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
      0.6729    0.6913    0.9323    0.4475
   
   R2 = 
      0.0642    0.1199    0.4749    0.3086    0.9852
   
   R3 = 
      0.6729    0.6913    0.9323    0.4475    0.0642    0.1199    0.4749    0.3086    0.9852
   
   C1 = 
      0.3555
      0.3021
      0.7349
      0.9401
      0.6100
      0.0161
      0.6176
      0.3160
      0.4661
      0.5063
   
   C2 = 
      0.0446
      0.8368
      0.7233
      0.1652
      0.2929
      0.1947
      0.4965
      0.1954
      0.9380
      0.3832
   
   M = 
      0.3555    0.0446
      0.3021    0.8368
      0.7349    0.7233
      0.9401    0.1652
      0.6100    0.2929
      0.0161    0.1947
      0.6176    0.4965
      0.3160    0.1954
      0.4661    0.9380
      0.5063    0.3832
   


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
      0.7829    0.9658    0.3283    0.6227
   
   R2 = 
      0.3175    0.2261    0.6193    0.6728
   
   M = 
      0.7829    0.9658    0.3283    0.6227
      0.3175    0.2261    0.6193    0.6728
   
   C1 = 
      0.8600
      0.5493
      0.4662
      0.6918
      0.3785
      0.2778
      0.9995
      0.7884
      0.2956
      0.0953
   
   C2 = 
      0.7603
      0.2635
   
   C3 = 
      0.8600
      0.5493
      0.4662
      0.6918
      0.3785
      0.2778
      0.9995
      0.7884
      0.2956
      0.0953
      0.7603
      0.2635
   

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
   

