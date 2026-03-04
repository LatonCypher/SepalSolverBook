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
      0.9668    0.3806    0.1209    0.9460    0.4263    0.2251    0.7004
   
   C = 
      0.4618
      0.8817
      0.3495
      0.7572
      0.0354
   
   M = 
      0.5774    0.9929    0.4272    0.8136    0.7429    0.7287    0.6324
      0.2943    0.2475    0.8291    0.8419    0.9999    0.4388    0.6613
      0.3208    0.3725    0.2513    0.2224    0.7679    0.0069    0.8989
      0.9913    0.0237    0.3563    0.5096    0.7854    0.8097    0.4578
      0.9088    0.1257    0.4915    0.5524    0.8185    0.8771    0.6285
      0.4900    0.4449    0.5748    0.6785    0.5069    0.9870    0.2211
      0.0389    0.4633    0.0617    0.1248    0.1694    0.6588    0.6764
      0.2604    0.4477    0.6979    0.3569    0.0092    0.3415    0.3324
   

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
      0.1327    0.8757    0.3999    0.9910
   
   R2 = 
      0.0094    0.8132    0.6862    0.8034    0.3988
   
   R3 = 
      0.1327    0.8757    0.3999    0.9910    0.0094    0.8132    0.6862    0.8034    0.3988
   
   C1 = 
      0.6969
      0.1611
      0.6071
      0.3015
      0.8912
      0.3628
      0.7388
      0.5747
      0.4453
      0.7347
   
   C2 = 
      0.0742
      0.9463
      0.8160
      0.3466
      0.7202
      0.8561
      0.1862
      0.4546
      0.7051
      0.1263
   
   M = 
      0.6969    0.0742
      0.1611    0.9463
      0.6071    0.8160
      0.3015    0.3466
      0.8912    0.7202
      0.3628    0.8561
      0.7388    0.1862
      0.5747    0.4546
      0.4453    0.7051
      0.7347    0.1263
   


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
      0.9953    0.3015    0.4807    0.5849
   
   R2 = 
      0.5615    0.3302    0.2138    0.0701
   
   M = 
      0.9953    0.3015    0.4807    0.5849
      0.5615    0.3302    0.2138    0.0701
   
   C1 = 
      0.3404
      0.8338
      0.8221
      0.2745
      0.5209
      0.8329
      0.2431
      0.5594
      0.6051
      0.8151
   
   C2 = 
      0.7935
      0.2002
   
   C3 = 
      0.3404
      0.8338
      0.8221
      0.2745
      0.5209
      0.8329
      0.2431
      0.5594
      0.6051
      0.8151
      0.7935
      0.2002
   

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
   

