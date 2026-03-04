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
      0.6734    0.2726    0.5183    0.8454    0.6777    0.1929    0.7485
   
   C = 
      0.3425
      0.9546
      0.1705
      0.7983
      0.8961
   
   M = 
      0.1197    0.0016    0.1843    0.8996    0.1301    0.2358    0.6307
      0.9488    0.9903    0.9881    0.2860    0.0329    0.1484    0.9932
      0.5940    0.7549    0.6704    0.8777    0.4807    0.8213    0.6036
      0.7307    0.6592    0.4299    0.5965    0.8560    0.4842    0.6662
      0.2707    0.6117    0.2735    0.8039    0.5867    0.6511    0.8025
      0.9476    0.6093    0.6876    0.4239    0.0743    0.9086    0.8370
      0.7462    0.7537    0.7746    0.5130    0.1787    0.8855    0.5467
      0.2726    0.9298    0.1635    0.0587    0.5302    0.7776    0.8892
   

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
      0.7326    0.6730    0.8417    0.4625
   
   R2 = 
      0.0182    0.3875    0.0759    0.1140    0.6753
   
   R3 = 
      0.7326    0.6730    0.8417    0.4625    0.0182    0.3875    0.0759    0.1140    0.6753
   
   C1 = 
      0.2522
      0.9675
      0.1233
      0.3970
      0.5820
      0.3102
      0.0748
      0.7044
      0.6724
      0.0238
   
   C2 = 
      0.1842
      0.3527
      0.9247
      0.5925
      0.9705
      0.1567
      0.5945
      0.5326
      0.5518
      0.9527
   
   M = 
      0.2522    0.1842
      0.9675    0.3527
      0.1233    0.9247
      0.3970    0.5925
      0.5820    0.9705
      0.3102    0.1567
      0.0748    0.5945
      0.7044    0.5326
      0.6724    0.5518
      0.0238    0.9527
   


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
      0.5436    0.4688    0.5078    0.4983
   
   R2 = 
      0.0982    0.7453    0.3145    0.9369
   
   M = 
      0.5436    0.4688    0.5078    0.4983
      0.0982    0.7453    0.3145    0.9369
   
   C1 = 
      0.3853
      0.8995
      0.6999
      0.9060
      0.2580
      0.6600
      0.4121
      0.2513
      0.5521
      0.9449
   
   C2 = 
      0.6664
      0.1590
   
   C3 = 
      0.3853
      0.8995
      0.6999
      0.9060
      0.2580
      0.6600
      0.4121
      0.2513
      0.5521
      0.9449
      0.6664
      0.1590
   

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
   

