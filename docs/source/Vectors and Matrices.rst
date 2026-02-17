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
      0.3576    0.4568    0.4240    0.8735    0.1212    0.6086    0.5865
   
   C = 
      0.0145
      0.6815
      0.7501
      0.1371
      0.7344
   
   M = 
      0.6888    0.6846    0.0722    0.9988    0.4615    0.1865    0.7338
      0.8310    0.5708    0.8894    0.1914    0.9271    0.5292    0.9381
      0.4487    0.8596    0.7015    0.6212    0.8203    0.4210    0.4027
      0.7608    0.0334    0.5807    0.9402    0.9723    0.4373    0.9644
      0.3027    0.9616    0.1998    0.3058    0.4106    0.6359    0.8619
      0.8460    0.1250    0.7178    0.7415    0.2410    0.4947    0.8803
      0.9638    0.2020    0.4027    0.0073    0.4711    0.2727    0.8447
      0.7880    0.9709    0.6754    0.5180    0.9852    0.4453    0.1922
   

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
      0.1034    0.3259    0.1936    0.7325
   
   R2 = 
      0.1509    0.4402    0.9917    0.0183    0.7514
   
   R3 = 
      0.1034    0.3259    0.1936    0.7325    0.1509    0.4402    0.9917    0.0183    0.7514
   
   C1 = 
      0.2565
      0.8348
      0.5761
      0.1942
      0.2626
      0.8965
      0.1876
      0.1575
      0.8995
      0.2161
   
   C2 = 
      0.2195
      0.2002
      0.9823
      0.9186
      0.1064
      0.0292
      0.1554
      0.4257
      0.8000
      0.9739
   
   M = 
      0.2565    0.2195
      0.8348    0.2002
      0.5761    0.9823
      0.1942    0.9186
      0.2626    0.1064
      0.8965    0.0292
      0.1876    0.1554
      0.1575    0.4257
      0.8995    0.8000
      0.2161    0.9739
   


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
      0.0918    0.3789    0.0440    0.2179
   
   R2 = 
      0.6680    0.5326    0.8013    0.0288
   
   M = 
      0.0918    0.3789    0.0440    0.2179
      0.6680    0.5326    0.8013    0.0288
   
   C1 = 
      0.4458
      0.3597
      0.7033
      0.4857
      0.3598
      0.7550
      0.0306
      0.0537
      0.6879
      0.2403
   
   C2 = 
      0.7949
      0.3055
   
   C3 = 
      0.4458
      0.3597
      0.7033
      0.4857
      0.3598
      0.7550
      0.0306
      0.0537
      0.6879
      0.2403
      0.7949
      0.3055
   

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
   

