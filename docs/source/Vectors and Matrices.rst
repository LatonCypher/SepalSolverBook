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
      0.7733    0.5271    0.9037    0.0497    0.4564    0.3896    0.1536
   
   C = 
      0.7767
      0.7405
      0.3167
      0.7202
      0.9290
   
   M = 
      0.2525    0.7458    0.5020    0.3933    0.5713    0.6242    0.4643
      0.9689    0.1376    0.3481    0.3890    0.6300    0.9097    0.9919
      0.2681    0.1733    0.3485    0.1216    0.4084    0.1201    0.8567
      0.6601    0.5745    0.3726    0.8301    0.0309    0.3100    0.9470
      0.8807    0.5682    0.5544    0.9734    0.5267    0.0953    0.2025
      0.1737    0.2052    0.0735    0.9286    0.4026    0.6534    0.6077
      0.1921    0.3627    0.7598    0.5793    0.7169    0.1962    0.1762
      0.5348    0.6982    0.6332    0.7581    0.7479    0.6684    0.5240
   

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
      0.4278    0.9436    0.4987    0.8834
   
   R2 = 
      0.0109    0.2421    0.9141    0.6595    0.6170
   
   R3 = 
      0.4278    0.9436    0.4987    0.8834    0.0109    0.2421    0.9141    0.6595    0.6170
   
   C1 = 
      0.0685
      0.1526
      0.6494
      0.1110
      0.4720
      0.5319
      0.1999
      0.3450
      0.6743
      0.8864
   
   C2 = 
      0.0705
      0.7284
      0.2391
      0.9535
      0.9390
      0.4048
      0.6202
      0.5759
      0.1028
      0.0811
   
   M = 
      0.0685    0.0705
      0.1526    0.7284
      0.6494    0.2391
      0.1110    0.9535
      0.4720    0.9390
      0.5319    0.4048
      0.1999    0.6202
      0.3450    0.5759
      0.6743    0.1028
      0.8864    0.0811
   


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
      0.0717    0.2942    0.3070    0.9631
   
   R2 = 
      0.1657    0.5540    0.7678    0.3568
   
   M = 
      0.0717    0.2942    0.3070    0.9631
      0.1657    0.5540    0.7678    0.3568
   
   C1 = 
      0.2267
      0.4362
      0.8905
      0.6595
      0.8043
      0.1563
      0.4277
      0.8096
      0.1241
      0.7347
   
   C2 = 
      0.6415
      0.9663
   
   C3 = 
      0.2267
      0.4362
      0.8905
      0.6595
      0.8043
      0.1563
      0.4277
      0.8096
      0.1241
      0.7347
      0.6415
      0.9663
   

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
   

