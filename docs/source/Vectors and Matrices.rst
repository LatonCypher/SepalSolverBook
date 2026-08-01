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
      0.1003    0.5013    0.8604    0.1230    0.1870    0.9624    0.7530
   
   C = 
      0.3986
      0.4029
      0.6735
      0.4268
      0.0872
   
   M = 
      0.6251    0.0074    0.3777    0.2946    0.8016    0.8086    0.1601
      0.2170    0.7518    0.1960    0.9411    0.4186    0.3072    0.3001
      0.7021    0.6882    0.6546    0.2052    0.8847    0.1706    0.1893
      0.9103    0.7418    0.6889    0.2163    0.0996    0.7679    0.5033
      0.6039    0.2182    0.6245    0.8964    0.3721    0.3801    0.8840
      0.0602    0.4356    0.4794    0.6981    0.7373    0.3464    0.4251
      0.2274    0.7012    0.9407    0.9775    0.9089    0.8860    0.2069
      0.8774    0.6825    0.7141    0.5925    0.3830    0.3194    0.3770
   

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
      0.3151    0.3935    0.7536    0.0531
   
   R2 = 
      0.4974    0.3631    0.4432    0.4845    0.4435
   
   R3 = 
      0.3151    0.3935    0.7536    0.0531    0.4974    0.3631    0.4432    0.4845    0.4435
   
   C1 = 
      0.4811
      0.8992
      0.4812
      0.6717
      0.9843
      0.1461
      0.3866
      0.5970
      0.4669
      0.6535
   
   C2 = 
      0.5841
      0.3346
      0.3490
      0.3616
      0.2089
      0.1656
      0.4539
      0.3806
      0.0996
      0.0357
   
   M = 
      0.4811    0.5841
      0.8992    0.3346
      0.4812    0.3490
      0.6717    0.3616
      0.9843    0.2089
      0.1461    0.1656
      0.3866    0.4539
      0.5970    0.3806
      0.4669    0.0996
      0.6535    0.0357
   


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
      0.9215    0.2774    0.2250    0.2590
   
   R2 = 
      0.6867    0.8065    0.3900    0.7460
   
   M = 
      0.9215    0.2774    0.2250    0.2590
      0.6867    0.8065    0.3900    0.7460
   
   C1 = 
      0.3427
      0.1420
      0.0207
      0.9615
      0.3073
      0.9295
      0.6434
      0.3563
      0.2797
      0.1370
   
   C2 = 
      0.4614
      0.4308
   
   C3 = 
      0.3427
      0.1420
      0.0207
      0.9615
      0.3073
      0.9295
      0.6434
      0.3563
      0.2797
      0.1370
      0.4614
      0.4308
   

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
   

