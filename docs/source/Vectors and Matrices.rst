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
      0.8733    0.2520    0.3793    0.8365    0.5521    0.0664    0.1996
   
   C = 
      0.1054
      0.4307
      0.6596
      0.0960
      0.4651
   
   M = 
      0.8047    0.8891    0.8181    0.5456    0.3260    0.4671    0.4902
      0.3749    0.8462    0.7345    0.4038    0.6077    0.8225    0.9741
      0.8034    0.1782    0.4874    0.3829    0.8654    0.8533    0.3778
      0.0208    0.9499    0.2797    0.6603    0.8354    0.4765    0.6911
      0.3239    0.0261    0.9784    0.1541    0.8933    0.4971    0.0124
      0.0588    0.6900    0.6908    0.4094    0.0879    0.2617    0.1662
      0.1391    0.1695    0.3902    0.0904    0.4789    0.1951    0.8136
      0.3847    0.8464    0.5459    0.3361    0.6480    0.4603    0.2339
   

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
      0.1910    0.0622    0.3339    0.8434
   
   R2 = 
      0.1935    0.4437    0.2411    0.8123    0.6358
   
   R3 = 
      0.1910    0.0622    0.3339    0.8434    0.1935    0.4437    0.2411    0.8123    0.6358
   
   C1 = 
      0.5659
      0.1548
      0.9179
      0.2598
      0.1750
      0.4252
      0.0439
      0.7031
      0.0386
      0.6366
   
   C2 = 
      0.9524
      0.1303
      0.1235
      0.4084
      0.6130
      0.9967
      0.1267
      0.5273
      0.1411
      0.6416
   
   M = 
      0.5659    0.9524
      0.1548    0.1303
      0.9179    0.1235
      0.2598    0.4084
      0.1750    0.6130
      0.4252    0.9967
      0.0439    0.1267
      0.7031    0.5273
      0.0386    0.1411
      0.6366    0.6416
   


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
      0.6907    0.4145    0.3264    0.8831
   
   R2 = 
      0.5695    0.6932    0.3214    0.7743
   
   M = 
      0.6907    0.4145    0.3264    0.8831
      0.5695    0.6932    0.3214    0.7743
   
   C1 = 
      0.6848
      0.0920
      0.4566
      0.2528
      0.0355
      0.9182
      0.1608
      0.1273
      0.0382
      0.1038
   
   C2 = 
      0.6953
      0.0677
   
   C3 = 
      0.6848
      0.0920
      0.4566
      0.2528
      0.0355
      0.9182
      0.1608
      0.1273
      0.0382
      0.1038
      0.6953
      0.0677
   

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
   

