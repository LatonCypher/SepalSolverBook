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
      0.5143    0.3153    0.0135    0.3189    0.5413    0.7619    0.0086
   
   C = 
      0.3784
      0.9433
      0.6513
      0.9869
      0.5089
   
   M = 
      0.7741    0.1490    0.3551    0.7791    0.0177    0.9028    0.7795
      0.3084    0.6355    0.0694    0.6762    0.6484    0.3402    0.7818
      0.7595    0.9492    0.2364    0.3161    0.1158    0.4486    0.4938
      0.5891    0.2425    0.0122    0.6509    0.6118    0.3185    0.6353
      0.5395    0.3078    0.0396    0.1304    0.1289    0.8579    0.5518
      0.5186    0.3603    0.2521    0.1729    0.2936    0.9022    0.1408
      0.4384    0.0403    0.8868    0.1830    0.9769    0.0992    0.9120
      0.7647    0.6287    0.9697    0.3839    0.8382    0.1297    0.1585
   

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
      0.8462    0.3050    0.8095    0.4973
   
   R2 = 
      0.6429    0.3257    0.3030    0.2645    0.0158
   
   R3 = 
      0.8462    0.3050    0.8095    0.4973    0.6429    0.3257    0.3030    0.2645    0.0158
   
   C1 = 
      0.7896
      0.8524
      0.9137
      0.9019
      0.1382
      0.6092
      0.5061
      0.7992
      0.8546
      0.1190
   
   C2 = 
      0.9788
      0.3176
      0.7420
      0.2732
      0.4034
      0.3947
      0.7512
      0.6752
      0.3967
      0.2129
   
   M = 
      0.7896    0.9788
      0.8524    0.3176
      0.9137    0.7420
      0.9019    0.2732
      0.1382    0.4034
      0.6092    0.3947
      0.5061    0.7512
      0.7992    0.6752
      0.8546    0.3967
      0.1190    0.2129
   


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
      0.8766    0.3517    0.8389    0.1694
   
   R2 = 
      0.1575    0.7302    0.0024    0.5676
   
   M = 
      0.8766    0.3517    0.8389    0.1694
      0.1575    0.7302    0.0024    0.5676
   
   C1 = 
      0.4253
      0.7916
      0.9304
      0.1098
      0.4987
      0.9791
      0.4047
      0.9006
      0.9248
      0.6147
   
   C2 = 
      0.3889
      0.0511
   
   C3 = 
      0.4253
      0.7916
      0.9304
      0.1098
      0.4987
      0.9791
      0.4047
      0.9006
      0.9248
      0.6147
      0.3889
      0.0511
   

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
   

