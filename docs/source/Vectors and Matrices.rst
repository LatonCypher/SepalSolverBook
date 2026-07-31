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
      0.7780    0.5900    0.0544    0.7733    0.7979    0.1316    0.2873
   
   C = 
      0.5584
      0.3479
      0.7444
      0.9173
      0.1892
   
   M = 
      0.8439    0.3585    0.5192    0.1082    0.1725    0.9728    0.8969
      0.7413    0.3051    0.7690    0.2839    0.4833    0.4344    0.1031
      0.4817    0.5146    0.9327    0.8585    0.9299    0.6131    0.4419
      0.9622    0.1982    0.6632    0.0964    0.1739    0.0672    0.6313
      0.0231    0.8152    0.4721    0.4570    0.7487    0.4177    0.7778
      0.4129    0.9981    0.5702    0.4419    0.8707    0.4374    0.7767
      0.2807    0.6985    0.2804    0.3768    0.8733    0.0197    0.9240
      0.7879    0.3347    0.3191    0.7819    0.4860    0.1967    0.8395
   

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
      0.6021    0.7428    0.8376    0.6396
   
   R2 = 
      0.8580    0.2118    0.3208    0.6989    0.2527
   
   R3 = 
      0.6021    0.7428    0.8376    0.6396    0.8580    0.2118    0.3208    0.6989    0.2527
   
   C1 = 
      0.7724
      0.6990
      0.9202
      0.1737
      0.9481
      0.6968
      0.4629
      0.8221
      0.1159
      0.5843
   
   C2 = 
      0.7237
      0.0075
      0.7265
      0.8938
      0.2383
      0.2596
      0.1290
      0.1028
      0.6793
      0.8870
   
   M = 
      0.7724    0.7237
      0.6990    0.0075
      0.9202    0.7265
      0.1737    0.8938
      0.9481    0.2383
      0.6968    0.2596
      0.4629    0.1290
      0.8221    0.1028
      0.1159    0.6793
      0.5843    0.8870
   


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
      0.8794    0.6401    0.2416    0.5839
   
   R2 = 
      0.6389    0.6955    0.4926    0.2027
   
   M = 
      0.8794    0.6401    0.2416    0.5839
      0.6389    0.6955    0.4926    0.2027
   
   C1 = 
      0.5668
      0.9042
      0.7336
      0.4748
      0.0225
      0.7965
      0.3246
      0.9767
      0.8224
      0.1129
   
   C2 = 
      0.5495
      0.7250
   
   C3 = 
      0.5668
      0.9042
      0.7336
      0.4748
      0.0225
      0.7965
      0.3246
      0.9767
      0.8224
      0.1129
      0.5495
      0.7250
   

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
   

