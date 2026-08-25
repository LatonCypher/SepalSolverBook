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
      0.3090    0.6057    0.8118    0.4724    0.8808    0.2824    0.5260
   
   C = 
      0.4619
      0.4610
      0.5820
      0.2516
      0.1522
   
   M = 
      0.3568    0.0936    0.3188    0.5310    0.6756    0.0745    0.6595
      0.7886    0.6566    0.7655    0.0529    0.3266    0.0653    0.0040
      0.3737    0.8939    0.6435    0.3242    0.0243    0.2085    0.2749
      0.5553    0.0541    0.3230    0.9084    0.4163    0.9308    0.9764
      0.9244    0.2047    0.6905    0.0691    0.1173    0.9541    0.8588
      0.8686    0.9435    0.8466    0.6920    0.2888    0.7341    0.3661
      0.8843    0.1789    0.5333    0.1925    0.1552    0.9258    0.2514
      0.0560    0.2812    0.5712    0.1539    0.3925    0.9102    0.6011
   

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
      0.3563    0.3733    0.7066    0.3977
   
   R2 = 
      0.4951    0.6207    0.3358    0.2481    0.0368
   
   R3 = 
      0.3563    0.3733    0.7066    0.3977    0.4951    0.6207    0.3358    0.2481    0.0368
   
   C1 = 
      0.2746
      0.7969
      0.1995
      0.3508
      0.5086
      0.2858
      0.9717
      0.1108
      0.0192
      0.6075
   
   C2 = 
      0.6655
      0.0072
      0.2368
      0.7963
      0.9998
      0.6121
      0.4707
      0.4703
      0.1107
      0.9951
   
   M = 
      0.2746    0.6655
      0.7969    0.0072
      0.1995    0.2368
      0.3508    0.7963
      0.5086    0.9998
      0.2858    0.6121
      0.9717    0.4707
      0.1108    0.4703
      0.0192    0.1107
      0.6075    0.9951
   


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
      0.2723    0.3747    0.6950    0.4416
   
   R2 = 
      0.7891    0.5048    0.7944    0.2400
   
   M = 
      0.2723    0.3747    0.6950    0.4416
      0.7891    0.5048    0.7944    0.2400
   
   C1 = 
      0.2507
      0.7322
      0.2896
      0.6297
      0.7026
      0.4279
      0.8473
      0.8624
      0.7414
      0.6982
   
   C2 = 
      0.5489
      0.3208
   
   C3 = 
      0.2507
      0.7322
      0.2896
      0.6297
      0.7026
      0.4279
      0.8473
      0.8624
      0.7414
      0.6982
      0.5489
      0.3208
   

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
   

