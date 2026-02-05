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
      0.6174    0.8718    0.3441    0.0226    0.3030    0.4162    0.8913
   
   C = 
      0.7189
      0.0549
      0.9102
      0.5229
      0.7483
   
   M = 
      0.9640    0.1534    0.8630    0.8150    0.9990    0.2792    0.0728
      0.4972    0.8939    0.8925    0.0234    0.5622    0.5003    0.9587
      0.0393    0.9831    0.6825    0.8341    0.7319    0.5491    0.2518
      0.6948    0.9163    0.2481    0.3248    0.8990    0.9349    0.4987
      0.2079    0.3557    0.0171    0.5756    0.3349    0.6623    0.0484
      0.1053    0.7411    0.1377    0.8016    0.2397    0.2178    0.0043
      0.2541    0.3562    0.7654    0.2922    0.0901    0.9533    0.6511
      0.1527    0.2983    0.2211    0.2749    0.6397    0.3686    0.3391
   

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
      0.2029    0.9365    0.1042    0.3932
   
   R2 = 
      0.7791    0.7131    0.5641    0.7417    0.1416
   
   R3 = 
      0.2029    0.9365    0.1042    0.3932    0.7791    0.7131    0.5641    0.7417    0.1416
   
   C1 = 
      0.2366
      0.3886
      0.5674
      0.3625
      0.3208
      0.3304
      0.2137
      0.5913
      0.6981
      0.4522
   
   C2 = 
      0.4746
      0.1776
      0.6952
      0.3201
      0.9766
      0.1132
      0.3262
      0.8551
      0.9554
      0.4115
   
   M = 
      0.2366    0.4746
      0.3886    0.1776
      0.5674    0.6952
      0.3625    0.3201
      0.3208    0.9766
      0.3304    0.1132
      0.2137    0.3262
      0.5913    0.8551
      0.6981    0.9554
      0.4522    0.4115
   


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
      0.9701    0.0728    0.6773    0.7522
   
   R2 = 
      0.2687    0.2984    0.6125    0.7348
   
   M = 
      0.9701    0.0728    0.6773    0.7522
      0.2687    0.2984    0.6125    0.7348
   
   C1 = 
      0.3757
      0.5360
      0.7933
      0.5687
      0.8500
      0.6099
      0.7475
      0.5756
      0.9190
      0.5166
   
   C2 = 
      0.1931
      0.2422
   
   C3 = 
      0.3757
      0.5360
      0.7933
      0.5687
      0.8500
      0.6099
      0.7475
      0.5756
      0.9190
      0.5166
      0.1931
      0.2422
   

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
   

